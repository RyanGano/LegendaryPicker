using System.Security.Cryptography;
using System.Text;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Setup codes (#335): a short string that reloads one setup, shown with it and read back by GET /api/setup/{code}.
//
// A code names the cards, not a seed, so it survives box data changes that leave its cards alone: a seed would draw
// other cards as soon as any included box gained or lost one. It reads "1-<data>-<options>-<boxes>-<cards>":
//   1        the format;
//   data     the box data version it was drawn from (BoxCatalog.DataVersion);
//   options  five digits: the players (1 to 5), the rule edition chosen (0 when the boxes decide, 1 First Edition,
//            2 Villains, 3 Revised), Epic Masterminds (0 or 1), and the difficulty and complexity targets (0 for none);
//   boxes    the included boxes;
//   cards    the drawn cards in setup order: the Scheme, the Mastermind, the Villain Groups, the Henchman Groups, the
//            Hero Deck's Heroes, the Henchman Groups, Heroes and Masterminds the Scheme draws outside their decks, and
//            the Heroes a setup step names.
// data, each box and each card is a token: six base-36 characters of the SHA-256 of its id or version. A card keeps
// its token for as long as it keeps its id.
//
// Reloading a code draws again with every draw taking the card the code names, so the rules, counts and notes are the
// current ones. A code drawn from other box data still reloads when the draw lands on the same cards; when it can't, as
// when a card it names is gone or the rules now draw other cards, the code says so instead of showing another setup.
public sealed class SetupCodes(BoxCatalog catalog, SetupGenerator generator)
{
    private const string Format = "1";
    private const int TokenLength = 6;
    private const string Digits = "0123456789abcdefghijklmnopqrstuvwxyz";
    private const ulong Tokens = 2_176_782_336; // 36^6

    private static readonly Ruleset?[] Editions = [null, Ruleset.FirstEdition, Ruleset.Villainous, Ruleset.Revised];

    private const string OutdatedMessage = "This setup code can no longer give the same cards: the card data or setup rules have changed since it was made.";

    // Each token's ids among the loaded boxes and cards. Two ids that share a token would make a code ambiguous; a test
    // checks the loaded data has none.
    private readonly ILookup<string, string> _boxes = catalog.Boxes.ToLookup(box => Token(box.Id), box => box.Id, StringComparer.Ordinal);
    private readonly ILookup<string, string> _cards = catalog.Boxes
        .SelectMany(box => box.Heroes.Cast<ICard>().Concat(box.VillainGroups).Concat(box.HenchmanGroups).Concat(box.Masterminds).Concat(box.Schemes))
        .Select(card => card.Id)
        .ToLookup(Token, StringComparer.Ordinal);

    // The outcome of reloading a code: the setup and what it was drawn with, or why there is none. Invalid means the
    // text is no setup code; Outdated means it was one, but the current data can't give its cards.
    public abstract record Reload;

    public sealed record Loaded(SetupResult Setup, Ruleset? Rules, bool Epic, int? Difficulty, int? Complexity, string Code) : Reload;

    public sealed record Invalid(string Message) : Reload;

    public sealed record Outdated(string Message) : Reload;

    // The code of a drawn setup, or null when the draw found no Scheme.
    public string? For(GenerationResult result, Ruleset? rules, bool epic, int? difficulty, int? complexity) => result is SetupResult setup
        ? string.Join('-',
            Format,
            Token(catalog.DataVersion),
            $"{setup.Players}{Array.IndexOf(Editions, rules)}{(epic ? 1 : 0)}{difficulty ?? 0}{complexity ?? 0}",
            string.Concat(setup.Boxes.Select(box => Token(box.Id))),
            string.Concat(CardsOf(setup).Select(Token)))
        : null;

    public Reload Load(string code)
    {
        var parts = code.Trim().ToLowerInvariant().Split('-');
        if (parts is not [Format, var data, var options, var boxTokens, var cardTokens]
            || data.Length != TokenLength
            || options.Length != 5 || !options.All(char.IsAsciiDigit)
            || boxTokens.Length == 0 || boxTokens.Length % TokenLength != 0
            || cardTokens.Length < 2 * TokenLength || cardTokens.Length % TokenLength != 0
            || !(data + boxTokens + cardTokens).All(Digits.Contains))
        {
            return new Invalid("That isn't a setup code.");
        }

        var players = options[0] - '0';
        var edition = options[1] - '0';
        var (epic, difficulty, complexity) = (options[2] - '0', options[3] - '0', options[4] - '0');
        if (players is < BoxCatalog.MinPlayers or > BoxCatalog.MaxPlayers || edition >= Editions.Length || epic > 1 || difficulty > 5 || complexity > 5)
        {
            return new Invalid("That isn't a setup code.");
        }

        var boxes = Resolve(_boxes, boxTokens);
        var cards = Resolve(_cards, cardTokens);
        var rules = Editions[edition];
        if (boxes is null || cards is null || generator.CheckBoxes(boxes, rules) is not null)
        {
            return new Outdated(OutdatedMessage);
        }

        var replay = new Replay(cards);
        var (wantDifficulty, wantComplexity) = (difficulty == 0 ? (int?)null : difficulty, complexity == 0 ? (int?)null : complexity);
        // The targets aren't asked of the draw: the code's cards already are the ones a targeted draw found.
        if (generator.Generate(players, boxes, replay, epic == 1, rules) is not SetupResult setup || replay.Missed || !CardsOf(setup).SequenceEqual(cards))
        {
            return new Outdated(OutdatedMessage);
        }

        return new Loaded(setup, rules, epic == 1, wantDifficulty, wantComplexity, For(setup, rules, epic == 1, wantDifficulty, wantComplexity)!);
    }

    // Every card a setup draws, in the order the code lists them.
    private static IEnumerable<string> CardsOf(SetupResult setup) =>
    [
        setup.Scheme.Id, setup.Mastermind.Id,
        .. setup.VillainGroups.Select(group => group.Id), .. setup.HenchmanGroups.Select(group => group.Id),
        .. setup.Heroes.Select(hero => hero.Id),
        .. setup.OutsideHenchmen.Select(outside => outside.Group.Id), .. setup.OutsideHeroes.Select(outside => outside.Hero.Id),
        .. (setup.OutsideMasterminds ?? []).Select(outside => outside.Mastermind.Id),
        .. (setup.StepHeroes ?? []).Select(hero => hero.Id),
    ];

    // The id of each token, or null when one names no loaded id or more than one.
    private static List<string>? Resolve(ILookup<string, string> ids, string tokens)
    {
        var resolved = new List<string>();
        for (var at = 0; at < tokens.Length; at += TokenLength)
        {
            if (ids[tokens.Substring(at, TokenLength)].ToList() is not [var id])
            {
                return null;
            }

            resolved.Add(id);
        }

        return resolved;
    }

    internal static string Token(string id)
    {
        var value = BitConverter.ToUInt64(SHA256.HashData(Encoding.UTF8.GetBytes(id)), 0) % Tokens;
        var token = new char[TokenLength];
        for (var at = TokenLength - 1; at >= 0; at--, value /= (ulong)Digits.Length)
        {
            token[at] = Digits[(int)(value % (ulong)Digits.Length)];
        }

        return new string(token);
    }

    // Draws the cards a code names: each draw takes, of its options, the card the code lists first among those it hasn't
    // drawn yet. The code lists the cards in the order the generator draws them, so each draw finds its own card. A draw
    // whose options hold none of them, or a draw of anything but a card, means the code's cards can't be drawn again.
    private sealed class Replay(IReadOnlyList<string> cards) : IRandomSource
    {
        private readonly List<string> _left = [.. cards];

        public bool Missed { get; private set; }

        public int Next(int exclusiveMax)
        {
            Missed = true;
            return 0;
        }

        public T Pick<T>(IReadOnlyList<T> options) where T : ICard
        {
            var at = _left.FindIndex(id => options.Any(option => option.Id == id));
            if (at < 0)
            {
                Missed = true;
                return options[0];
            }

            var card = options.First(option => option.Id == _left[at]);
            _left.RemoveAt(at);
            return card;
        }
    }
}
