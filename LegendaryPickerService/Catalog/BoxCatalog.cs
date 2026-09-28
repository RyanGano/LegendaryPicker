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
    public const int SchemaVersion = 2;

    // Glossary summaries are short paraphrases in our own words, never rulebook text.
    public const int MaxSummaryWords = 40;

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
            ValidateRuleSources(path, box, sourceKeys);

            // Every setup includes its base game, so the base game's stacks can't be left to an expansion;
            // a stack it forgot to list would otherwise come out as 0.
            if (box.Setup is not null)
            {
                (string Name, Sourced<int>? Count)[] stacks =
                    [("bystanders", box.Components.Bystanders), ("wounds", box.Components.Wounds), ("officers", box.Components.Officers)];
                foreach (var (name, _) in stacks.Where(stack => stack.Count is null))
                {
                    throw new InvalidDataException($"{path}: base game {box.Id} has a setup section but no components.{name}.");
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

            var words = term.Summary.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
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

    private static IEnumerable<(string Owner, string GroupId, GroupType GroupType)> GroupReferences(Box box) =>
        box.Masterminds.Select(m => (m.Id, m.AlwaysLeads.GroupId, m.AlwaysLeads.GroupType))
            .Concat(box.Schemes.SelectMany(s =>
                (s.Setup.RequiredGroups ?? []).Select(g => (s.Id, g.GroupId, g.GroupType))));

    // A Hero's team and classes, then every component's keywords.
    private static IEnumerable<(string Owner, string TermId, TermKind Kind)> TermReferences(Box box) =>
        box.Heroes.SelectMany(h => (h.Team is null ? [] : new[] { (h.Id, h.Team, TermKind.Team) })
                .Concat(h.Classes.Select(c => (h.Id, c, TermKind.Class)))
                .Concat(h.Terms.Select(t => (h.Id, t, TermKind.Keyword))))
            .Concat(box.VillainGroups.SelectMany(g => g.Terms.Select(t => (g.Id, t, TermKind.Keyword))))
            .Concat(box.HenchmanGroups.SelectMany(g => g.Terms.Select(t => (g.Id, t, TermKind.Keyword))))
            .Concat(box.Masterminds.SelectMany(m => m.Terms.Select(t => (m.Id, t, TermKind.Keyword))))
            .Concat(box.Schemes.SelectMany(s => s.Terms.Select(t => (s.Id, t, TermKind.Keyword))));

    // Every rule value and setup effect a rule note can cite, named by where it sits in the box file.
    private static IEnumerable<(string Rule, string Source)> RuleSources(Box box)
    {
        var components = box.Components;
        yield return ("components.heroCards", components.HeroCards.Source);
        yield return ("components.villainGroupCards", components.VillainGroupCards.Source);
        yield return ("components.henchmanGroupCards", components.HenchmanGroupCards.Source);
        yield return ("components.schemeTwists", components.SchemeTwists.Source);
        (string Name, Sourced<int>? Count)[] stacks =
            [("bystanders", components.Bystanders), ("wounds", components.Wounds), ("officers", components.Officers), ("sidekicks", components.Sidekicks)];
        foreach (var (name, count) in stacks)
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
            yield return ("setup.solo.twistKosHeroCostingAtMost", setup.Solo.TwistKosHeroCostingAtMost.Source);
            yield return ("setup.rulings.alwaysLeadsFillsSlot", setup.Rulings.AlwaysLeadsFillsSlot);
            yield return ("setup.rulings.requiredGroupDisplacesAlwaysLeads", setup.Rulings.RequiredGroupDisplacesAlwaysLeads);
            yield return ("setup.rulings.schemeOverridesSolo", setup.Rulings.SchemeOverridesSolo);
        }

        foreach (var mastermind in box.Masterminds)
        {
            yield return ($"{mastermind.Id} alwaysLeads", mastermind.AlwaysLeads.Source);
            if (mastermind.Setup is { } effects)
            {
                foreach (var rule in EffectSources(mastermind.Id, effects)) yield return rule;
            }
        }

        foreach (var scheme in box.Schemes)
        {
            var effect = scheme.Setup;
            foreach (var value in effect.Twists) yield return ($"{scheme.Id} setup.twists", value.Source);
            if (effect.AllowedPlayerCounts is { } allowed) yield return ($"{scheme.Id} setup.allowedPlayerCounts", allowed.Source);
            foreach (var value in effect.Heroes ?? []) yield return ($"{scheme.Id} setup.heroes", value.Source);
            if (effect.VillainDeckBystanders is { } bystanders) yield return ($"{scheme.Id} setup.villainDeckBystanders", bystanders.Source);
            if (effect.WoundsPerPlayer is { } wounds) yield return ($"{scheme.Id} setup.woundsPerPlayer", wounds.Source);
            foreach (var group in effect.RequiredGroups ?? []) yield return ($"{scheme.Id} setup.requiredGroups", group.Source);
            if (effect.HeroCardsInVillainDeck is { } heroCards) yield return ($"{scheme.Id} setup.heroCardsInVillainDeck", heroCards.Source);
            if (effect.TwistsBesideScheme is { } beside) yield return ($"{scheme.Id} setup.twistsBesideScheme", beside.Source);
            foreach (var rule in EffectSources(scheme.Id, effect)) yield return rule;
        }
    }

    private static IEnumerable<(string Rule, string Source)> EffectSources(string owner, SetupEffects effects)
    {
        (string Name, IReadOnlyList<PlayerCountValue>? Values)[] counts =
        [
            ("extraHeroes", effects.ExtraHeroes), ("extraVillainGroups", effects.ExtraVillainGroups),
            ("extraHenchmanGroups", effects.ExtraHenchmanGroups), ("extraVillainDeckBystanders", effects.ExtraVillainDeckBystanders),
        ];
        return counts.SelectMany(count => (count.Values ?? []).Select(value => ($"{owner} setup.{count.Name}", value.Source)));
    }
}
