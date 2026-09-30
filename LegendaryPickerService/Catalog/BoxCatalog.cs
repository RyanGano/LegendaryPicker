using LegendaryPickerService.Setup;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LegendaryPickerService.Catalog;

// Every box the service knows, read once from the JSON files in Data/Boxes.
//
// Ids have the form <boxId>_<kind>_<name>, for example core_mastermind_dr-doom:
// the declaring box's id, the kind of entry, and a kebab-case name. Underscores
// separate the segments; hyphens stay inside one. A box's own id is a single
// kebab-case segment. References name a full id, so a box can reference another box's groups.
public sealed partial class BoxCatalog
{
    public const int SchemaVersion = 6;

    // Glossary summaries are short paraphrases in our own words, never rulebook text.
    public const int MaxSummaryWords = 40;

    // Setup step and Solo play rule labels are one short instruction in our own words, never card or rulebook text.
    public const int MaxStepLabelWords = 15;

    // The player counts a setup can be drawn for; 1 is Solo.
    public const int MinPlayers = 1;
    public const int MaxPlayers = 5;

    public static string DefaultDirectory { get; } = Path.Combine(AppContext.BaseDirectory, "Data", "Boxes");

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        // A missing required field or a null where the model forbids one is a data error, not a default.
        RespectNullableAnnotations = true,
        RespectRequiredConstructorParameters = true,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) },
    };

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]
    private static partial Regex Segment();

    public IReadOnlyList<Box> Boxes { get; }

    private BoxCatalog(IReadOnlyList<Box> boxes) => Boxes = boxes;

    public static BoxCatalog Load(string directory)
    {
        var files = Directory.GetFiles(directory, "*.json")
            .Order(StringComparer.Ordinal)
            .Select(path => (Path: path, Box: LoadBox(path)))
            .ToList();

        Validate(files);

        return new BoxCatalog(files.Select(file => file.Box).ToList());
    }

    private static Box LoadBox(string path)
    {
        try
        {
            using var stream = File.OpenRead(path);
            using var document = JsonDocument.Parse(stream);

            // Check the version before binding, so a file written for another schema reports
            // the version mismatch rather than whichever of its fields this model lacks.
            var version = document.RootElement.ValueKind == JsonValueKind.Object
                && document.RootElement.TryGetProperty("schemaVersion", out var element)
                && element.TryGetInt32(out var value)
                    ? value
                    : (int?)null;
            if (version != SchemaVersion)
            {
                throw new InvalidDataException(
                    $"{path} has schemaVersion {version?.ToString() ?? "(missing)"}; this service reads version {SchemaVersion}.");
            }

            return document.Deserialize<Box>(JsonOptions)
                ?? throw new InvalidDataException($"{path} is empty.");
        }
        catch (JsonException ex)
        {
            // Name the file: with one file per box, the JSON path alone does not say which box is broken.
            throw new InvalidDataException($"{path} is not a valid box file: {ex.Message}", ex);
        }
    }

    // Checks across every loaded box, so a reference into another box resolves.
    private static void Validate(IReadOnlyList<(string Path, Box Box)> files)
    {
        var declaredIn = new Dictionary<string, string>(StringComparer.Ordinal);

        void Declare(string path, string id)
        {
            if (!declaredIn.TryAdd(id, path))
            {
                throw new InvalidDataException($"{path} declares id {id}, already declared in {declaredIn[id]}.");
            }
        }

        foreach (var (path, box) in files)
        {
            if (!Segment().IsMatch(box.Id))
            {
                throw new InvalidDataException(
                    $"{path} has box id {box.Id}; a box id is lowercase kebab-case with no underscores.");
            }

            Declare(path, box.Id);

            foreach (var (kind, id) in DeclaredIds(box))
            {
                var segments = id.Split('_');
                if (segments.Length != 3 || segments[0] != box.Id || segments[1] != kind || !Segment().IsMatch(segments[2]))
                {
                    throw new InvalidDataException(
                        $"{path} declares {kind} id {id}; ids in this box have the form {box.Id}_{kind}_<kebab-case-name>.");
                }

                Declare(path, id);
            }

            // Rule notes and glossary links look a key up by name, so a second URL for it would never show.
            var sourceKeys = new HashSet<string>(StringComparer.Ordinal);
            foreach (var source in box.Sources)
            {
                if (!sourceKeys.Add(source.Key))
                {
                    throw new InvalidDataException($"{path}: box {box.Id} lists source {source.Key} more than once.");
                }
            }

            ValidateTerms(path, box, sourceKeys);
            ValidateSteps(path, box);
            ValidateRuleSources(path, box, sourceKeys);
            ValidatePlayerCounts(path, box);
            ValidateMoves(path, box);
            ValidateHeroRules(path, box);

            // A setup can include a base game and no expansion, so a base game's stacks can't be left to
            // an expansion; a stack it forgot to list would otherwise come out as 0 or be left out.
            if (box.IsBaseGame)
            {
                foreach (var name in BaseGameStacks(box.Ruleset).Where(name => Stacks(box.Components).Single(stack => stack.Name == name).Count is null))
                {
                    throw new InvalidDataException($"{path}: base game {box.Id} has a setup section but no components.{name}.");
                }
            }

            ValidateUses(path, box);
        }

        // A setup names its cards by display name, so two Heroes, groups, Masterminds or Schemes sharing
        // one would leave the player unsure which physical card to use. A new version of a character
        // takes a distinguishing name, such as "Wolverine (X-Force)", and keeps heroName for Hero rules.
        // Names differing only in case or surrounding spaces read as the same name.
        var namedIn = new Dictionary<string, (string Id, string Path)>(StringComparer.OrdinalIgnoreCase);
        foreach (var (path, box) in files)
        {
            foreach (var (kind, id, name) in NamedComponents(box))
            {
                var key = $"{kind}:{name.Trim()}";
                if (!namedIn.TryAdd(key, (id, path)))
                {
                    var (otherId, otherPath) = namedIn[key];
                    throw new InvalidDataException(
                        $"{path}: {kind} {id} is named \"{name}\", like {otherId} in {otherPath}; a setup must tell them apart.");
                }
            }
        }

        // A reference into a box that isn't loaded says so, rather than that the card is missing.
        var loadedBoxes = files.Select(file => file.Box.Id).ToHashSet(StringComparer.Ordinal);
        string Unresolved(string id) => loadedBoxes.Contains(id.Split('_')[0])
            ? ", which no loaded box declares"
            : $" from box {id.Split('_')[0]}, which is not loaded";

        var groups = files
            .SelectMany(file => DeclaredIds(file.Box))
            .Where(entry => entry.Kind is "villain" or "henchman")
            .ToDictionary(entry => entry.Id, entry => entry.Kind, StringComparer.Ordinal);

        foreach (var (path, box) in files)
        {
            foreach (var (owner, groupId, groupType) in GroupReferences(box))
            {
                var kind = KindOf(groupType);
                if (!groups.TryGetValue(groupId, out var found))
                {
                    throw new InvalidDataException($"{path}: {owner} references {kind} group {groupId}{Unresolved(groupId)}.");
                }

                if (found != kind)
                {
                    throw new InvalidDataException($"{path}: {owner} references {groupId} as a {kind} group, but it is a {found} group.");
                }
            }
        }

        var heroes = files
            .SelectMany(file => file.Box.Heroes)
            .ToDictionary(hero => hero.Id, StringComparer.Ordinal);

        foreach (var (path, box) in files)
        {
            foreach (var (owner, heroId) in HeroReferences(box).Where(reference => !heroes.ContainsKey(reference.HeroId)))
            {
                throw new InvalidDataException($"{path}: {owner} references Hero {heroId}{Unresolved(heroId)}.");
            }

            ValidateHeroRulesCanBeMet(path, box, heroes);
        }

        var terms = files
            .SelectMany(file => file.Box.Glossary)
            .ToDictionary(term => term.Id, term => term.Kind, StringComparer.Ordinal);

        foreach (var (path, box) in files)
        {
            foreach (var (owner, termId, termKind) in TermReferences(box))
            {
                var kind = KindOf(termKind);
                if (!terms.TryGetValue(termId, out var found))
                {
                    throw new InvalidDataException($"{path}: {owner} references {kind} term {termId}{Unresolved(termId)}.");
                }

                if (found != termKind)
                {
                    throw new InvalidDataException(
                        $"{path}: {owner} references {termId} as a {kind} term, but it is a {KindOf(found)} term.");
                }
            }
        }
    }

    // A term needs a summary short enough to read at a glance, and a source this box links,
    // so the reader can follow it to the page with the full rule.
    private static void ValidateTerms(string path, Box box, IReadOnlySet<string> sourceKeys)
    {
        foreach (var term in box.Glossary)
        {
            if (string.IsNullOrWhiteSpace(term.Summary))
            {
                throw new InvalidDataException($"{path}: {term.Id} has no summary.");
            }

            var words = WordCount(term.Summary);
            if (words > MaxSummaryWords)
            {
                throw new InvalidDataException(
                    $"{path}: {term.Id} has a {words}-word summary; summaries are at most {MaxSummaryWords} words.");
            }

            if (!sourceKeys.Contains(term.Source))
            {
                throw new InvalidDataException($"{path}: {term.Id} cites source {term.Source}, which is not in this box's sources.");
            }

            if (term.Page < 1)
            {
                throw new InvalidDataException($"{path}: {term.Id} has page {term.Page}; a term cites the page that defines it.");
            }
        }
    }

    // A setup step is a line on the checklist, so its label must say something and stay short enough to
    // be one instruction rather than copied card text. A card listing one step twice would show it twice,
    // with two identical rule notes.
    // Solo play rules are rule notes, held to the same limits.
    private static void ValidateSteps(string path, Box box)
    {
        foreach (var (owner, steps) in StepLists(box))
        {
            CheckLabels(path, owner, "setup step", steps.Select(step => step.Label));
        }

        if (box.Setup is { } setup)
        {
            CheckLabels(path, "setup.solo", "play rule", setup.Solo.PlayRules.Select(rule => rule.Label));
        }
    }

    private static void CheckLabels(string path, string owner, string what, IEnumerable<string> labels)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var label in labels)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new InvalidDataException($"{path}: {owner} has a {what} with no label.");
            }

            var words = WordCount(label);
            if (words > MaxStepLabelWords)
            {
                throw new InvalidDataException(
                    $"{path}: {owner} has a {words}-word {what} label; labels are at most {MaxStepLabelWords} words.");
            }

            if (!seen.Add(label.Trim()))
            {
                throw new InvalidDataException($"{path}: {owner} lists the {what} \"{label}\" twice.");
            }
        }
    }

    private static int WordCount(string text) => text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;

    // A rule note links its citation's key through its own box's sources, so a key the box doesn't
    // list would show the note with no link. Card is the printed card itself, which has no link.
    private static void ValidateRuleSources(string path, Box box, IReadOnlySet<string> sourceKeys)
    {
        foreach (var (rule, source) in RuleSources(box))
        {
            var key = source.Split(' ')[0];
            if (key != "Card" && !sourceKeys.Contains(key))
            {
                throw new InvalidDataException($"{path}: {rule} cites source {key}, which is not in this box's sources.");
            }
        }
    }

    // A draw reads a player count's value from the one entry that names it, so each count may be named once,
    // by an entry that exists at a count the game supports and adds or sets at least 1.
    private static void ValidatePlayerCounts(string path, Box box)
    {
        if (box.Setup is { } setup)
        {
            CheckPlayerCounts(path, "setup.playerCounts", setup.PlayerCounts.Select(row => new[] { row.Players }));
        }

        foreach (var (rule, values) in PlayerCountLists(box))
        {
            if (values.FirstOrDefault(value => value.Players is []) is not null)
            {
                throw new InvalidDataException($"{path}: {rule} has an entry that names no player count.");
            }

            if (values.FirstOrDefault(value => value.Value < 1) is { } low)
            {
                throw new InvalidDataException($"{path}: {rule} has value {low.Value}; values are at least 1.");
            }

            // An entry with no players applies at every player count.
            CheckPlayerCounts(path, rule, values.Select(value => value.Players ?? Enumerable.Range(MinPlayers, MaxPlayers - MinPlayers + 1)));
        }
    }

    // A move puts cards somewhere they can be laid out from, never back where they came from, and
    // one into the starting decks already puts its count in each player's deck. Henchmen drawn outside
    // the Villain Deck go only where a Scheme is known to put them.
    private static void ValidateMoves(string path, Box box)
    {
        foreach (var scheme in box.Schemes)
        {
            foreach (var move in scheme.Setup.Moves ?? [])
            {
                var to = WireName(move.To);
                if (!CardMove.Destinations.Contains(move.To))
                {
                    throw new InvalidDataException(
                        $"{path}: {scheme.Id} moves cards to {to}; cards move to {string.Join(", ", CardMove.Destinations.Select(WireName))}.");
                }

                if (move.To == move.From())
                {
                    throw new InvalidDataException($"{path}: {scheme.Id} moves {WireName(move.Card)} cards to {to}, where they come from.");
                }

                if (move.PerPlayer && move.To == Pile.StartingDecks)
                {
                    throw new InvalidDataException(
                        $"{path}: {scheme.Id} moves cards to {to} per player; a move to {to} already puts its count in each player's deck.");
                }
            }

            foreach (var outside in scheme.Setup.OutsideHenchmen ?? [])
            {
                if (!OutsideHenchmen.Destinations.Contains(outside.To))
                {
                    throw new InvalidDataException(
                        $"{path}: {scheme.Id} puts Henchmen from outside the Villain Deck in {WireName(outside.To)}; they go to {string.Join(", ", OutsideHenchmen.Destinations.Select(WireName))}.");
                }
            }
        }
    }

    // A part listed twice would say nothing more. A base game's rules can use only parts it supplies, since a
    // setup can include it alone.
    private static void ValidateUses(string path, Box box)
    {
        foreach (var (owner, uses) in UseLists(box))
        {
            if (uses.GroupBy(use => use.Part).FirstOrDefault(part => part.Count() > 1) is { } twice)
            {
                throw new InvalidDataException($"{path}: {owner} lists part {WireName(twice.Key)} more than once.");
            }
        }

        foreach (var use in box.Setup?.Uses ?? [])
        {
            if (!(box.Components.Supplies(use.Part)?.Value > 0))
            {
                throw new InvalidDataException(
                    $"{path}: base game {box.Id} uses {WireName(use.Part)} in setup.uses but has no components.{WireName(use.Part)}.");
            }
        }
    }

    // The parts each card, and a base game's rules, list as used.
    private static IEnumerable<(string Owner, IReadOnlyList<PartUse> Uses)> UseLists(Box box) =>
        Cards(box).Where(card => card.Uses is not null).Select(card => ($"{card.Id} uses", card.Uses!))
            .Concat(box.Setup is { } setup ? [("setup.uses", setup.Uses)] : []);

    private static IEnumerable<ICard> Cards(Box box) =>
        box.Heroes.Cast<ICard>().Concat(box.VillainGroups).Concat(box.HenchmanGroups).Concat(box.Masterminds).Concat(box.Schemes);

    // A Hero constraint counts Heroes by one thing, a team or a Hero Name, against one bound. Heroes drawn
    // outside the Hero Deck go to a pile of their own rather than a stack or the Hero Deck, and are
    // chosen by at most one of a Hero, a Hero Name or a team. There is one of each Hero, so a rule that
    // needs one Hero twice, or both in the Hero Deck and outside it, has no draw.
    private static void ValidateHeroRules(string path, Box box)
    {
        foreach (var scheme in box.Schemes)
        {
            foreach (var count in scheme.Setup.HeroCounts ?? [])
            {
                if ((count.Team is null) == (count.HeroName is null))
                {
                    throw new InvalidDataException($"{path}: {scheme.Id} has a Hero count that names {(count.Team is null ? "neither a team nor" : "both a team and")} a Hero Name; it names one of them.");
                }

                if ((count.AtLeast is null) == (count.Exactly is null))
                {
                    throw new InvalidDataException($"{path}: {scheme.Id} has a Hero count with {(count.AtLeast is null ? "neither atLeast nor" : "both atLeast and")} exactly; it has one of them.");
                }

                if (count.AtLeast < 1 || count.Exactly < 0)
                {
                    throw new InvalidDataException(
                        $"{path}: {scheme.Id} has a Hero count of {count.AtLeast ?? count.Exactly}; atLeast is at least 1 and exactly at least 0.");
                }
            }

            var required = new HashSet<string>();
            foreach (var hero in scheme.Setup.RequiredHeroes ?? [])
            {
                if (!required.Add(hero.HeroId))
                {
                    throw new InvalidDataException($"{path}: {scheme.Id} setup.requiredHeroes lists {hero.HeroId} more than once.");
                }
            }

            // For each Hero an outsideHeroes entry names, the player counts at which an entry draws it.
            var drawnOutside = new Dictionary<string, HashSet<int>>();
            foreach (var (outside, index) in (scheme.Setup.OutsideHeroes ?? []).Select((outside, index) => (outside, index)))
            {
                if (outside.Hero is { } named)
                {
                    var counts = drawnOutside.TryGetValue(named, out var earlier) ? earlier : drawnOutside[named] = [];
                    var players = outside.Count.SelectMany(count => count.Players ?? Enumerable.Range(MinPlayers, MaxPlayers - MinPlayers + 1)).Distinct();
                    if (players.FirstOrDefault(player => !counts.Add(player)) is > 0 and var twice)
                    {
                        throw new InvalidDataException(
                            $"{path}: {scheme.Id} setup.outsideHeroes[{index}] draws {named}, which an earlier entry also draws at {twice} players; there is one of each Hero.");
                    }

                    if (outside.Count.FirstOrDefault(count => count.Value > 1) is { } many)
                    {
                        throw new InvalidDataException(
                            $"{path}: {scheme.Id} setup.outsideHeroes[{index}] draws {many.Value} of Hero {named}; there is one of each Hero.");
                    }

                    if (required.Contains(named))
                    {
                        throw new InvalidDataException(
                            $"{path}: {scheme.Id} setup.outsideHeroes[{index}] draws {named} outside the Hero Deck, but setup.requiredHeroes puts it in the Hero Deck.");
                    }
                }

                if (!OutsideHeroes.Destinations.Contains(outside.To))
                {
                    throw new InvalidDataException(
                        $"{path}: {scheme.Id} puts Heroes outside the Hero Deck in {WireName(outside.To)}; they go to {string.Join(", ", OutsideHeroes.Destinations.Select(WireName))}.");
                }

                if (new[] { outside.Hero, outside.HeroName, outside.Team }.Count(choice => choice is not null) > 1)
                {
                    throw new InvalidDataException(
                        $"{path}: {scheme.Id} chooses Heroes outside the Hero Deck by more than one of hero, heroName and team.");
                }
            }
        }
    }

    // A Scheme's own Hero choices must leave its Hero rules possible at every player count it allows. Which
    // boxes are included, and how many Hero Deck slots a setup has, are left open: the Heroes the Scheme
    // doesn't name are stand-ins of every team and Hero Name its rules or named Heroes mention, as many as a
    // setup could use, and the Hero Deck has room for every required Hero and every count. So a Scheme only
    // Heroes from another box could complete still loads, and drops out of the draw until they are included.
    private static void ValidateHeroRulesCanBeMet(string path, Box box, IReadOnlyDictionary<string, Hero> heroes)
    {
        foreach (var scheme in box.Schemes)
        {
            foreach (var players in scheme.Setup.AllowedPlayerCounts?.Value ?? Enumerable.Range(MinPlayers, MaxPlayers - MinPlayers + 1))
            {
                if (CanMeetHeroRules(scheme.Setup, players, heroes))
                {
                    continue;
                }

                var culprits = HeroRulesLeftOut(scheme.Setup)
                    .Where(rule => CanMeetHeroRules(rule.Without, players, heroes))
                    .Select(rule => rule.Name)
                    .ToList();
                throw new InvalidDataException(
                    $"{path}: {scheme.Id} has Hero rules no draw can meet at {players} {(players == 1 ? "player" : "players")}, whichever Heroes are included; "
                    + (culprits.Count > 0 ? $"it can be met without {string.Join(" or ", culprits)}." : "no one of them can be left out to meet the rest."));
            }
        }
    }

    private static bool CanMeetHeroRules(SchemeSetup setup, int players, IReadOnlyDictionary<string, Hero> heroes)
    {
        var required = (setup.RequiredHeroes ?? []).Select(rule => heroes[rule.HeroId]).ToList();
        var counts = setup.HeroCounts ?? [];
        var outside = (setup.OutsideHeroes ?? [])
            .SelectMany(rule => Enumerable.Repeat(rule, rule.Count.SingleOrDefault(count => count.Players?.Contains(players) ?? true)?.Value ?? 0))
            .ToList();
        var deckSlots = required.Count + counts.Sum(count => count.AtLeast ?? count.Exactly!.Value);
        var named = required.Concat(outside.Select(rule => rule.Hero).OfType<string>().Select(id => heroes[id])).Distinct().ToList();

        // Stand-ins of each team and Hero Name, including none and a Hero Name of their own, enough of each
        // to fill every slot.
        var teams = counts.Select(count => count.Team).Concat(outside.Select(rule => rule.Team)).Concat(named.Select(hero => hero.Team)).Append(null).Distinct();
        var names = counts.Select(count => count.HeroName).Concat(outside.Select(rule => rule.HeroName)).Concat(named.Select(hero => hero.NameOfHero)).Append(null).Distinct().ToList();
        var standIns = teams
            .SelectMany(team => names.SelectMany(name => Enumerable.Range(0, deckSlots + outside.Count).Select(_ => (Team: team, Name: name))))
            .Select((standIn, index) => new Hero($"stand-in_{index}", standIn.Name ?? $"stand-in {index}", standIn.Team, [], []))
            .ToList();

        return new HeroRules([.. named, .. standIns], deckSlots, setup, outside).CanComplete(required, []);
    }

    // The Scheme's Hero rules, each with the setup that leaves it out.
    private static IEnumerable<(string Name, SchemeSetup Without)> HeroRulesLeftOut(SchemeSetup setup)
    {
        static List<T> Except<T>(IReadOnlyList<T> list, int index) => list.Where((_, i) => i != index).ToList();

        var required = setup.RequiredHeroes ?? [];
        var counts = setup.HeroCounts ?? [];
        var outside = setup.OutsideHeroes ?? [];
        return required.Select((_, i) => ($"setup.requiredHeroes[{i}]", setup with { RequiredHeroes = Except(required, i) }))
            .Concat(counts.Select((_, i) => ($"setup.heroCounts[{i}]", setup with { HeroCounts = Except(counts, i) })))
            .Concat(setup.DistinctHeroNames?.Value == true ? [("setup.distinctHeroNames", setup with { DistinctHeroNames = null })] : [])
            .Concat(outside.Select((_, i) => ($"setup.outsideHeroes[{i}]", setup with { OutsideHeroes = Except(outside, i) })));
    }

    private static string WireName<T>(T value) where T : struct, Enum => JsonNamingPolicy.CamelCase.ConvertName(value.ToString());

    private static void CheckPlayerCounts(string path, string rule, IEnumerable<IEnumerable<int>> entries)
    {
        var named = new HashSet<int>();
        foreach (var players in entries.SelectMany(entry => entry))
        {
            if (players is < MinPlayers or > MaxPlayers)
            {
                throw new InvalidDataException(
                    $"{path}: {rule} names player count {players}; player counts are {MinPlayers} to {MaxPlayers}.");
            }

            if (!named.Add(players))
            {
                throw new InvalidDataException($"{path}: {rule} has more than one entry for player count {players}.");
            }
        }
    }

    // How a term kind is written in box files and API responses.
    public static string KindOf(TermKind kind) => kind switch
    {
        TermKind.Team => "team",
        TermKind.Class => "class",
        TermKind.Keyword => "keyword",
        _ => throw new ArgumentOutOfRangeException(nameof(kind)),
    };

    private static string KindOf(GroupType type) => type switch
    {
        GroupType.Villain => "villain",
        GroupType.Henchman => "henchman",
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

    private static IEnumerable<(string Kind, string Id)> DeclaredIds(Box box) =>
        box.Heroes.Select(x => ("hero", x.Id))
            .Concat(box.VillainGroups.Select(x => ("villain", x.Id)))
            .Concat(box.HenchmanGroups.Select(x => ("henchman", x.Id)))
            .Concat(box.Masterminds.Select(x => ("mastermind", x.Id)))
            .Concat(box.Schemes.Select(x => ("scheme", x.Id)))
            .Concat(box.Glossary.Select(x => ("term", x.Id)));

    // The components a setup names on the page, each with its kind, id and display name.
    private static IEnumerable<(string Kind, string Id, string Name)> NamedComponents(Box box) =>
        box.Heroes.Select(x => ("hero", x.Id, x.Name))
            .Concat(box.VillainGroups.Select(x => ("villain", x.Id, x.Name)))
            .Concat(box.HenchmanGroups.Select(x => ("henchman", x.Id, x.Name)))
            .Concat(box.Masterminds.Select(x => ("mastermind", x.Id, x.Name)))
            .Concat(box.Schemes.Select(x => ("scheme", x.Id, x.Name)));

    private static IEnumerable<(string Owner, string GroupId, GroupType GroupType)> GroupReferences(Box box) =>
        box.Masterminds.Select(m => (m.Id, m.AlwaysLeads.GroupId, m.AlwaysLeads.GroupType))
            .Concat(box.Schemes.SelectMany(s =>
                (s.Setup.RequiredGroups ?? []).Select(g => (s.Id, g.GroupId, g.GroupType))));

    // The Heroes a Scheme requires or draws outside the Hero Deck by id.
    private static IEnumerable<(string Owner, string HeroId)> HeroReferences(Box box) =>
        box.Schemes.SelectMany(s => (s.Setup.RequiredHeroes ?? []).Select(h => (s.Id, h.HeroId))
            .Concat((s.Setup.OutsideHeroes ?? []).Where(o => o.Hero is not null).Select(o => (s.Id, o.Hero!))));

    // A Hero's team and classes, then every component's keywords, then the teams a Scheme's Hero rules name.
    private static IEnumerable<(string Owner, string TermId, TermKind Kind)> TermReferences(Box box) =>
        box.Heroes.SelectMany(h => (h.Team is null ? [] : new[] { (h.Id, h.Team, TermKind.Team) })
                .Concat(h.Classes.Select(c => (h.Id, c, TermKind.Class)))
                .Concat(h.Terms.Select(t => (h.Id, t, TermKind.Keyword))))
            .Concat(box.VillainGroups.SelectMany(g => g.Terms.Select(t => (g.Id, t, TermKind.Keyword))))
            .Concat(box.HenchmanGroups.SelectMany(g => g.Terms.Select(t => (g.Id, t, TermKind.Keyword))))
            .Concat(box.Masterminds.SelectMany(m => m.Terms.Select(t => (m.Id, t, TermKind.Keyword))))
            .Concat(box.Schemes.SelectMany(s => s.Terms.Select(t => (s.Id, t, TermKind.Keyword))))
            .Concat(box.Schemes.SelectMany(s =>
                (s.Setup.HeroCounts ?? []).Select(c => c.Team)
                    .Concat((s.Setup.OutsideHeroes ?? []).Select(o => o.Team))
                    .OfType<string>()
                    .Select(team => (s.Id, team, TermKind.Team))));

    // Every rule value and setup effect a rule note can cite, named by where it sits in the box file.
    private static IEnumerable<(string Rule, string Source)> RuleSources(Box box)
    {
        var components = box.Components;
        yield return ("components.heroCards", components.HeroCards.Source);
        yield return ("components.villainGroupCards", components.VillainGroupCards.Source);
        yield return ("components.henchmanGroupCards", components.HenchmanGroupCards.Source);
        yield return ("components.schemeTwists", components.SchemeTwists.Source);
        foreach (var (name, count) in Stacks(components))
        {
            if (count is not null) yield return ($"components.{name}", count.Source);
        }

        if (box.Setup is { } setup)
        {
            foreach (var row in setup.PlayerCounts) yield return ($"setup.playerCounts ({row.Players} players)", row.Source);
            yield return ("setup.heroes", setup.Heroes.Source);
            yield return ("setup.masterStrikes", setup.MasterStrikes.Source);
            yield return ("setup.startingDeck.agents", setup.StartingDeck.Agents.Source);
            yield return ("setup.startingDeck.troopers", setup.StartingDeck.Troopers.Source);
            yield return ("setup.solo.heroes", setup.Solo.Heroes.Source);
            yield return ("setup.solo.villainGroups", setup.Solo.VillainGroups.Source);
            yield return ("setup.solo.henchmanGroups", setup.Solo.HenchmanGroups.Source);
            yield return ("setup.solo.henchmanCards", setup.Solo.HenchmanCards.Source);
            yield return ("setup.solo.bystanders", setup.Solo.Bystanders.Source);
            yield return ("setup.solo.masterStrikes", setup.Solo.MasterStrikes.Source);
            yield return ("setup.solo.ignoresAlwaysLeads", setup.Solo.IgnoresAlwaysLeads.Source);
            foreach (var rule in setup.Solo.PlayRules) yield return ("setup.solo.playRules", rule.Source);
            yield return ("setup.rulings.alwaysLeadsFillsSlot", setup.Rulings.AlwaysLeadsFillsSlot);
            yield return ("setup.rulings.requiredGroupDisplacesAlwaysLeads", setup.Rulings.RequiredGroupDisplacesAlwaysLeads);
            yield return ("setup.rulings.schemeOverridesSolo", setup.Rulings.SchemeOverridesSolo);
            yield return ("setup.rulings.unusedPartsLeftOut", setup.Rulings.UnusedPartsLeftOut);
            if (setup.Mixing is { } mixing)
            {
                yield return ("setup.mixing.rules", mixing.Rules);
                yield return ("setup.mixing.pools", mixing.Pools);
                yield return ("setup.mixing.stacks", mixing.Stacks);
                yield return ("setup.mixing.startingDeckChoice", mixing.StartingDeckChoice);
            }
        }

        foreach (var mastermind in box.Masterminds)
        {
            yield return ($"{mastermind.Id} alwaysLeads", mastermind.AlwaysLeads.Source);
        }

        foreach (var scheme in box.Schemes)
        {
            var effect = scheme.Setup;
            if (effect.AllowedPlayerCounts is { } allowed) yield return ($"{scheme.Id} setup.allowedPlayerCounts", allowed.Source);
            if (effect.VillainDeckBystanders is { } bystanders) yield return ($"{scheme.Id} setup.villainDeckBystanders", bystanders.Source);
            if (effect.WoundsPerPlayer is { } wounds) yield return ($"{scheme.Id} setup.woundsPerPlayer", wounds.Source);
            if (effect.BindingsPerPlayer is { } bindings) yield return ($"{scheme.Id} setup.bindingsPerPlayer", bindings.Source);
            foreach (var group in effect.RequiredGroups ?? []) yield return ($"{scheme.Id} setup.requiredGroups", group.Source);
            if (effect.TwistsBesideScheme is { } beside) yield return ($"{scheme.Id} setup.twistsBesideScheme", beside.Source);
            foreach (var hero in effect.RequiredHeroes ?? []) yield return ($"{scheme.Id} setup.requiredHeroes", hero.Source);
            foreach (var count in effect.HeroCounts ?? []) yield return ($"{scheme.Id} setup.heroCounts", count.Source);
            if (effect.DistinctHeroNames is { } distinct) yield return ($"{scheme.Id} setup.distinctHeroNames", distinct.Source);
        }

        foreach (var (rule, values) in PlayerCountLists(box))
        {
            foreach (var value in values) yield return (rule, value.Source);
        }

        foreach (var (owner, steps) in StepLists(box))
        {
            foreach (var step in steps) yield return ($"{owner} setup.steps", step.Source);
        }

        foreach (var (owner, uses) in UseLists(box))
        {
            foreach (var use in uses) yield return (owner, use.Source);
        }
    }

    // The setup steps of every Mastermind and Scheme that has any, by the id of the card that prints them.
    private static IEnumerable<(string Owner, IReadOnlyList<SetupStep> Steps)> StepLists(Box box) =>
        box.Masterminds.Select(m => (m.Id, m.Setup?.Steps))
            .Concat(box.Schemes.Select(s => (s.Id, s.Setup.Steps)))
            .Where(list => list.Steps is not null)
            .Select(list => (list.Id, list.Steps!));

    // The shared stacks a box can add cards to, by their name in box files.
    private static (string Name, Sourced<int>? Count)[] Stacks(BoxComponents components) =>
    [
        ("bystanders", components.Bystanders), ("wounds", components.Wounds), ("officers", components.Officers),
        ("sidekicks", components.Sidekicks), ("bindings", components.Bindings), ("madameHydra", components.MadameHydra),
        ("newRecruits", components.NewRecruits),
    ];

    // The stacks a base game of each ruleset lays out (R p.22; VIL p.5).
    private static string[] BaseGameStacks(Ruleset ruleset) => ruleset switch
    {
        Ruleset.FirstEdition => ["bystanders", "wounds", "officers"],
        Ruleset.Villainous => ["bystanders", "bindings", "madameHydra", "newRecruits"],
        _ => throw new ArgumentOutOfRangeException(nameof(ruleset), ruleset, null),
    };

    // Every list of per-player-count values in a box: the base game's extra Heroes, the Masterminds' setup
    // effects, then each Scheme's, with its moves, Heroes outside the Hero Deck and Henchmen outside the
    // Villain Deck last.
    private static IEnumerable<(string Rule, IReadOnlyList<PlayerCountValue> Values)> PlayerCountLists(Box box)
    {
        if (box.Setup?.ExtraHeroes is { } extraHeroes)
        {
            yield return ("setup.extraHeroes", extraHeroes);
        }

        foreach (var mastermind in box.Masterminds)
        {
            if (mastermind.Setup is { } effects)
            {
                foreach (var list in EffectLists(mastermind.Id, effects)) yield return list;
            }
        }

        foreach (var scheme in box.Schemes)
        {
            yield return ($"{scheme.Id} setup.twists", scheme.Setup.Twists);
            if (scheme.Setup.Heroes is { } heroes) yield return ($"{scheme.Id} setup.heroes", heroes);
            if (scheme.Setup.HenchmanCards is { } henchmen) yield return ($"{scheme.Id} setup.henchmanCards", henchmen);
            foreach (var list in EffectLists(scheme.Id, scheme.Setup)) yield return list;
            foreach (var (move, index) in (scheme.Setup.Moves ?? []).Select((move, index) => (move, index)))
            {
                yield return ($"{scheme.Id} setup.moves[{index}].count", move.Count);
            }

            foreach (var (outside, index) in (scheme.Setup.OutsideHeroes ?? []).Select((outside, index) => (outside, index)))
            {
                yield return ($"{scheme.Id} setup.outsideHeroes[{index}].count", outside.Count);
            }

            foreach (var (outside, index) in (scheme.Setup.OutsideHenchmen ?? []).Select((outside, index) => (outside, index)))
            {
                yield return ($"{scheme.Id} setup.outsideHenchmen[{index}].cards", outside.Cards);
            }
        }
    }

    private static IEnumerable<(string Rule, IReadOnlyList<PlayerCountValue> Values)> EffectLists(string owner, SetupEffects effects)
    {
        (string Name, IReadOnlyList<PlayerCountValue>? Values)[] counts =
        [
            ("extraHeroes", effects.ExtraHeroes), ("extraVillainGroups", effects.ExtraVillainGroups),
            ("extraHenchmanGroups", effects.ExtraHenchmanGroups), ("extraVillainDeckBystanders", effects.ExtraVillainDeckBystanders),
        ];
        return counts.Where(count => count.Values is not null).Select(count => ($"{owner} setup.{count.Name}", count.Values!));
    }
}
