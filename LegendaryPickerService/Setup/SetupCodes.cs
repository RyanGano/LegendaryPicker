using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Setup codes (#335, #340): a short string that reloads one setup, shown with it and read back by GET /api/setup/{code}.
//
// A code names the cards, not a seed, so it survives box data changes that leave its cards alone: a seed would draw
// other cards as soon as any included box gained or lost one. It reads
// "<players>-<schemes>-<masterminds>-<villains>-<henchmen>-<heroes>-<data>[-<options>]":
//   players     1 to 5;
//   schemes     the Scheme;
//   masterminds the Mastermind, then the Masterminds the Scheme draws besides it;
//   villains    the Villain Groups;
//   henchmen    the Henchman Groups, then those the Scheme draws outside the Villain Deck;
//   heroes      the Hero Deck's Heroes, then those the Scheme draws outside it, then those a setup step names;
//   data        three characters of the box data version (BoxCatalog.DataVersion);
//   options     written only when there are any: "e" for Epic Masterminds, then the rule edition the player chose (1 First
//               Edition, 2 Villains, 3 Revised), then "b" and the included boxes when the cards' boxes alone don't give the
//               same setup.
// Each card is its number in Data/CodeNumbers.json, an append-only list, as two base-36 characters; a number past the
// two-character ones takes a "z" in front for each 1260 it passes, so the lists need no separator. Each list keeps the
// setup's order, which says what each card is for: the last Henchman Groups drawn can be the Scheme's extra ones.
//
// The boxes come from the cards: a setup that the boxes of its cards give again, box labels aside, stores no boxes. When
// a ticked box with no drawn card changes the setup (its Bystanders, a part a card uses, its base game's rules), or a
// card came from a box that wasn't ticked, the options carry the included boxes as a mask of their numbers.
//
// Reloading a code draws again with every draw taking a card the code names, so the rules, counts and notes are the
// current ones. The reload searches for a draw that gives the code's lists, trying in turn each named card a draw could
// take, so it doesn't depend on the order the generator draws cards in. A code from other box data still reloads when a
// draw gives its lists; when none does, as when a card it names is gone or the rules now draw other cards, it says the
// data has changed instead of showing another setup.
public sealed class SetupCodes
{
    public static string NumbersFile { get; } = Path.Combine(AppContext.BaseDirectory, "Data", "CodeNumbers.json");

    private const string Digits = "0123456789abcdefghijklmnopqrstuvwxyz";
    private const char Longer = 'z';
    private const int Plain = 35 * 36; // the two-character numbers: those that don't start with Longer
    private const int DataLength = 3;
    private const char EpicOption = 'e';
    private const char BoxesOption = 'b';

    // How many draws a reload may try before it gives up on the code's lists. A code the service wrote reloads on its
    // first draw; the cap bounds what a made-up code can cost, about 60 ms a draw with every box (#340 review).
    private const int MaxReplays = 20;

    private static readonly Ruleset?[] Editions = [null, Ruleset.FirstEdition, Ruleset.Villainous, Ruleset.Revised];

    private const string InvalidMessage = "That isn't a setup code.";
    private const string OutdatedMessage = "This setup code can no longer give the same cards: the card data or setup rules have changed since it was made.";

    private readonly BoxCatalog _catalog;
    private readonly SetupGenerator _generator;
    private readonly string _data;
    private readonly IReadOnlyList<string> _boxIds;
    private readonly IReadOnlyList<string> _cardIds;
    private readonly Dictionary<string, int> _boxNumbers;
    private readonly Dictionary<string, int> _cardNumbers;

    public SetupCodes(BoxCatalog catalog, SetupGenerator generator)
    {
        _catalog = catalog;
        _generator = generator;
        _data = Short(catalog.DataVersion);

        // The listed numbers, then any loaded id the list lacks, such as a test fixture's, in catalog order. A test checks
        // that the list holds every box and card of the service's data, so their numbers never move.
        var listed = JsonSerializer.Deserialize<NumbersList>(File.ReadAllText(NumbersFile), JsonSerializerOptions.Web)!;
        _boxIds = [.. listed.Boxes, .. catalog.Boxes.Select(box => box.Id).Except(listed.Boxes)];
        _cardIds = [.. listed.Cards, .. CardIds(catalog).Except(listed.Cards)];
        _boxNumbers = _boxIds.Index().ToDictionary(entry => entry.Item, entry => entry.Index, StringComparer.Ordinal);
        _cardNumbers = _cardIds.Index().ToDictionary(entry => entry.Item, entry => entry.Index, StringComparer.Ordinal);
    }

    // Every card id a code can name, in catalog order.
    internal static IEnumerable<string> CardIds(BoxCatalog catalog) => catalog.Boxes
        .SelectMany(box => box.Heroes.Cast<ICard>().Concat(box.VillainGroups).Concat(box.HenchmanGroups).Concat(box.Masterminds).Concat(box.Schemes))
        .Select(card => card.Id);

    internal sealed record NumbersList(IReadOnlyList<string> Boxes, IReadOnlyList<string> Cards);

    // The outcome of reloading a code: the setup and its code for the current data, or why there is none. Invalid means
    // the text is no setup code; Outdated means it was one, but the current data can't give its cards.
    public abstract record Reload;

    public sealed record Loaded(SetupResult Setup, string Code) : Reload;

    public sealed record Invalid(string Message) : Reload;

    public sealed record Outdated(string Message) : Reload;

    // The code of a drawn setup, or null when the draw found no Scheme.
    public string? For(GenerationResult result, Ruleset? rules, bool epic)
    {
        if (result is not SetupResult setup)
        {
            return null;
        }

        var lists = ListsOf(setup);
        var ticked = setup.Boxes.Select(box => box.Id).ToList();
        var fromCards = BoxesOf(lists);
        var needsBoxes = !fromCards.SequenceEqual(ticked)
            && (_generator.CheckBoxes(fromCards, rules) is not null
                || Replay(setup.Players, fromCards, lists, epic, rules) is not { } again
                || !Same(setup, again));

        var options = new StringBuilder();
        if (epic)
        {
            options.Append(EpicOption);
        }

        if (rules is not null)
        {
            options.Append(Array.IndexOf(Editions, rules));
        }

        if (needsBoxes)
        {
            options.Append(BoxesOption).Append(Mask(ticked.Select(id => _boxNumbers[id])));
        }

        List<string> fields = [setup.Players.ToString(CultureInfo.InvariantCulture), .. lists.Select(list => string.Concat(list.Select(id => Number(_cardNumbers[id])))), _data];
        if (options.Length > 0)
        {
            fields.Add(options.ToString());
        }

        return string.Join('-', fields);
    }

    public Reload Load(string code)
    {
        var fields = code.Trim().ToLowerInvariant().Split('-');
        if (fields.Length is not (7 or 8)
            || fields[0] is not [var digit] || digit - '0' is < BoxCatalog.MinPlayers or > BoxCatalog.MaxPlayers
            || fields[6].Length != DataLength
            || !fields.All(field => field.All(Digits.Contains))
            || fields[1].Length == 0 || fields[2].Length == 0
            || ParseOptions(fields.Length == 8 ? fields[7] : null) is not (var epic, var edition, var mask)
            || fields[1..6].Select(ParseNumbers).ToList() is var numbers && numbers.Any(list => list is null))
        {
            return new Invalid(InvalidMessage);
        }

        // A code of this data that gives no setup was never one; one of other data names cards the data has changed since.
        var data = fields[6];
        Reload Failed() => data == _data ? new Invalid(InvalidMessage) : new Outdated(OutdatedMessage);

        if (numbers.SelectMany(list => list!).Any(number => number >= _cardIds.Count) || mask?.Any(number => number >= _boxIds.Count) == true)
        {
            return Failed();
        }

        var players = digit - '0';
        IReadOnlyList<IReadOnlyList<string>> lists = numbers.Select(list => (IReadOnlyList<string>)list!.Select(number => _cardIds[number]).ToList()).ToList();
        var boxes = mask?.Select(number => _boxIds[number]).ToList() ?? BoxesOf(lists);
        var rules = Editions[edition];
        if (_generator.CheckBoxes(boxes, rules) is not null || Replay(players, boxes, lists, epic, rules) is not { } setup)
        {
            return Failed();
        }

        return new Loaded(setup, For(setup, rules, epic)!);
    }

    // The code's five lists of a setup: its Scheme, Masterminds, Villain Groups, Henchman Groups and Heroes.
    private static List<IReadOnlyList<string>> ListsOf(SetupResult setup) =>
    [
        [setup.Scheme.Id],
        [setup.Mastermind.Id, .. (setup.OutsideMasterminds ?? []).Select(outside => outside.Mastermind.Id)],
        [.. setup.VillainGroups.Select(group => group.Id)],
        [.. setup.HenchmanGroups.Select(group => group.Id), .. setup.OutsideHenchmen.Select(outside => outside.Group.Id)],
        [.. setup.Heroes.Select(hero => hero.Id), .. setup.OutsideHeroes.Select(outside => outside.Hero.Id), .. (setup.StepHeroes ?? []).Select(hero => hero.Id)],
    ];

    // The boxes that hold the cards, in catalog order: a catalog id starts with its box's id.
    private List<string> BoxesOf(IEnumerable<IReadOnlyList<string>> lists)
    {
        var ids = lists.SelectMany(list => list).Select(id => id[..id.IndexOf('_')]).ToHashSet(StringComparer.Ordinal);
        return _catalog.Boxes.Select(box => box.Id).Where(ids.Contains).ToList();
    }

    // Whether two setups are the same, but for the box labels and box list that a setup of fewer boxes shows otherwise.
    private bool Same(SetupResult drawn, SetupResult again) => JsonNode.DeepEquals(WithoutBoxes(drawn), WithoutBoxes(again));

    private JsonNode WithoutBoxes(SetupResult setup)
    {
        var body = SetupResponse.From(setup, _catalog);
        var node = JsonSerializer.SerializeToNode(body, body.GetType(), JsonSerializerOptions.Web)!;
        Strip(node);
        return node;

        static void Strip(JsonNode? node)
        {
            IEnumerable<JsonNode?> children = [];
            if (node is JsonObject properties)
            {
                properties.Remove("box");
                properties.Remove("boxes");
                children = properties.Select(entry => entry.Value);
            }
            else if (node is JsonArray items)
            {
                children = items;
            }

            foreach (var child in children)
            {
                Strip(child);
            }
        }
    }

    // Draws the setup whose lists are the code's from these boxes, or null when no draw gives them. Each draw takes one
    // of the code's cards among its options, the one listed first to begin with; a draw with none of them, or a setup with
    // other lists, sends the search back to the last draw with another card left to try, until no draw has one. A draw
    // whose options hold none of the code's cards at all, not even one an earlier draw took, ends the search: no other
    // order of the earlier draws can give it a card, as when a card isn't drawable from these boxes.
    private SetupResult? Replay(int players, IReadOnlyCollection<string> boxes, IReadOnlyList<IReadOnlyList<string>> lists, bool epic, Ruleset? rules)
    {
        var cards = lists.SelectMany(list => list).ToList();
        var path = new List<(int Choice, int Options)>();
        for (var replay = 0; replay < MaxReplays; replay++)
        {
            try
            {
                if (_generator.Generate(players, boxes, new ReplaySource(cards, path), epic, rules) is SetupResult setup
                    && ListsOf(setup).Zip(lists).All(pair => pair.First.SequenceEqual(pair.Second)))
                {
                    return setup;
                }
            }
            catch (ReplayMiss miss) when (!miss.Hopeless)
            {
            }
            catch (ReplayMiss)
            {
                return null;
            }

            while (path.Count > 0 && path[^1].Choice + 1 >= path[^1].Options)
            {
                path.RemoveAt(path.Count - 1);
            }

            if (path.Count == 0)
            {
                return null;
            }

            path[^1] = (path[^1].Choice + 1, path[^1].Options);
        }

        return null;
    }

    private sealed class ReplayMiss(bool hopeless) : Exception
    {
        public bool Hopeless { get; } = hopeless;
    }

    // One replay: its draws take path's choices in turn, and extend path with a first choice past its end. A draw chooses
    // among the code's cards left in its options, in code order; one with none of them, or a draw of a number, misses.
    private sealed class ReplaySource(IReadOnlyList<string> cards, List<(int Choice, int Options)> path) : IRandomSource
    {
        private readonly List<string> _left = [.. cards];
        private int _draw;

        public int Next(int exclusiveMax) => throw new ReplayMiss(hopeless: true);

        public T Pick<T>(IReadOnlyList<T> options) where T : ICard
        {
            var candidates = _left.Distinct(StringComparer.Ordinal).Where(id => options.Any(option => option.Id == id)).ToList();
            if (candidates.Count == 0)
            {
                throw new ReplayMiss(hopeless: !options.Any(option => cards.Contains(option.Id)));
            }

            if (_draw == path.Count)
            {
                path.Add((0, candidates.Count));
            }

            var id = candidates[path[_draw++].Choice];
            _left.Remove(id);
            return options.First(option => option.Id == id);
        }
    }

    // The first characters of the data version in base 36: enough to tell one version from the next.
    private static string Short(string version) =>
        Base36(BigInteger.Parse("0" + version[..16], NumberStyles.HexNumber) % BigInteger.Pow(Digits.Length, DataLength), DataLength);

    private static string Number(int number)
    {
        var longer = new StringBuilder();
        for (; number >= Plain; number -= Plain)
        {
            longer.Append(Longer);
        }

        return longer.Append(Base36(number, 2)).ToString();
    }

    // The numbers a list names, or null when its characters don't split into numbers.
    private static List<int>? ParseNumbers(string list)
    {
        var numbers = new List<int>();
        for (var at = 0; at < list.Length; at += 2)
        {
            var number = 0;
            for (; at < list.Length && list[at] == Longer; at++)
            {
                number += Plain;
            }

            if (at + 2 > list.Length)
            {
                return null;
            }

            numbers.Add(number + Digits.IndexOf(list[at]) * Digits.Length + Digits.IndexOf(list[at + 1]));
        }

        return numbers;
    }

    // Epic, the chosen edition's index in Editions, and the included boxes' numbers when the options give them, or null
    // when the options aren't ones a code writes. A code without options has none.
    private static (bool Epic, int Edition, List<int>? Mask)? ParseOptions(string? options)
    {
        if (options is null)
        {
            return (false, 0, null);
        }

        var at = 0;
        var epic = at < options.Length && options[at] == EpicOption;
        if (epic)
        {
            at++;
        }

        var edition = 0;
        if (at < options.Length && options[at] - '0' is >= 1 and var index && index < Editions.Length)
        {
            edition = index;
            at++;
        }

        List<int>? mask = null;
        if (at + 1 < options.Length && options[at] == BoxesOption)
        {
            var value = options[(at + 1)..].Aggregate(BigInteger.Zero, (sum, digit) => sum * Digits.Length + Digits.IndexOf(digit));
            var bytes = value.ToByteArray(isUnsigned: true);
            mask = Enumerable.Range(0, bytes.Length * 8).Where(bit => (bytes[bit / 8] >> (bit % 8) & 1) == 1).ToList();
            at = options.Length;
        }

        return options.Length > 0 && at == options.Length ? (epic, edition, mask) : null;
    }

    private static string Mask(IEnumerable<int> numbers) =>
        Base36(numbers.Aggregate(BigInteger.Zero, (mask, number) => mask | BigInteger.One << number), 1);

    private static string Base36(BigInteger value, int width)
    {
        var digits = new StringBuilder();
        for (; value > 0 || digits.Length < width; value /= Digits.Length)
        {
            digits.Insert(0, Digits[(int)(value % Digits.Length)]);
        }

        return digits.ToString();
    }
}
