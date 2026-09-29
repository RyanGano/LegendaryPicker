using System.Text.Json.Serialization;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// The body GET /api/setup returns: a generator result reduced to what the result checklist shows.
// Kind tells the client which shape it holds, "setup" or "noEligibleScheme".
public abstract record SetupResponse
{
    public abstract string Kind { get; }

    public static SetupResponse From(GenerationResult result, BoxCatalog catalog) => result switch
    {
        SetupResult setup => FromSetup(setup, new Glossary(catalog, nameBoxes: setup.Boxes.Count > 1)),
        NoEligibleScheme none => new NoEligibleSchemeBody(
            none.Players,
            $"No Scheme can be set up legally for {none.Players} player{(none.Players == 1 ? "" : "s")} with the included boxes."),
        _ => throw new ArgumentOutOfRangeException(nameof(result), result, "Unknown generation result."),
    };

    private static SetupBody FromSetup(SetupResult setup, Glossary glossary)
    {
        var scheme = glossary.Component(setup.Scheme.Id, setup.Scheme.Name, setup.Scheme.Terms);
        var mastermind = glossary.Component(setup.Mastermind.Id, setup.Mastermind.Name, setup.Mastermind.Terms);
        var villainGroups = setup.VillainGroups.Select(group => glossary.Component(group.Id, group.Name, group.Terms)).ToList();
        var henchmanGroups = setup.HenchmanGroups.Select(group => glossary.Component(group.Id, group.Name, group.Terms)).ToList();
        Component Hero(Hero hero) => glossary.Component(
            hero.Id, hero.Name, [.. hero.Team is null ? [] : new[] { hero.Team }, .. hero.Classes, .. hero.Terms]);
        var heroes = setup.Heroes.Select(Hero).ToList();
        var outsideHeroes = setup.OutsideHeroes.Select(outside => new OutsideHeroBody(Hero(outside.Hero), outside.To, outside.Cards)).ToList();

        Component[] components = [scheme, mastermind, .. villainGroups, .. henchmanGroups, .. heroes, .. outsideHeroes.Select(outside => outside.Hero)];

        return new SetupBody(
            setup.Players,
            setup.Ruleset,
            scheme,
            mastermind,
            villainGroups,
            henchmanGroups,
            heroes,
            setup.VillainDeck,
            setup.HeroDeck,
            setup.TwistsBesideScheme,
            setup.Stacks,
            setup.PlayerDeck,
            setup.Moves,
            outsideHeroes,
            setup.Steps,
            setup.Notes,
            glossary.Entries(components.SelectMany(component => component.Terms)));
    }

    // Every loaded box's glossary terms, ordered teams, then classes, then keywords, each in catalog order.
    // A term links to the URL its box lists for its source key; the loader guarantees one per key.
    // When the setup includes more than one box, each entry names the box that defines the term, and
    // each component the box it comes from.
    private sealed class Glossary(BoxCatalog catalog, bool nameBoxes)
    {
        private readonly Dictionary<string, ((TermKind Kind, int Index) Order, GlossaryEntry Entry)> _terms = catalog.Boxes
            .SelectMany(box => box.Glossary.Select(term => (Term: term, Box: box, Link: box.Sources.First(source => source.Key == term.Source).Url)))
            .Select((x, index) => (x.Term, x.Box, x.Link, Index: index))
            .ToDictionary(
                x => x.Term.Id,
                x => ((x.Term.Kind, x.Index),
                    new GlossaryEntry(
                        x.Term.Id, x.Term.Name, BoxCatalog.KindOf(x.Term.Kind), x.Term.Summary, $"{x.Term.Source} p.{x.Term.Page}", x.Link,
                        nameBoxes ? x.Box.Name : null)),
                StringComparer.Ordinal);

        // A catalog id starts with its box's id, which the loader checks.
        private readonly Dictionary<string, string> _boxNames = catalog.Boxes.ToDictionary(box => box.Id, box => box.Name, StringComparer.Ordinal);

        public Component Component(string id, string name, IEnumerable<string> terms) =>
            new(id, name, Ordered(terms).ToList(), nameBoxes ? _boxNames[id.Split('_')[0]] : null);

        public IReadOnlyList<GlossaryEntry> Entries(IEnumerable<string> terms) =>
            Ordered(terms).Select(term => _terms[term].Entry).ToList();

        private IEnumerable<string> Ordered(IEnumerable<string> terms) =>
            terms.Distinct(StringComparer.Ordinal).OrderBy(term => _terms[term].Order);
    }
}

// Ruleset is written camelCase, as in box files ("firstEdition").
public sealed record SetupBody(
    int Players,
    Ruleset Ruleset,
    Component Scheme,
    Component Mastermind,
    IReadOnlyList<Component> VillainGroups,
    IReadOnlyList<Component> HenchmanGroups,
    IReadOnlyList<Component> Heroes,
    VillainDeck VillainDeck,
    HeroDeck HeroDeck,
    int TwistsBesideScheme,
    SetupStacks Stacks,
    PlayerDeck PlayerDeck,
    IReadOnlyList<MovedCards> Moves,
    IReadOnlyList<OutsideHeroBody> OutsideHeroes,
    IReadOnlyList<string> Steps,
    IReadOnlyList<RuleNote> Notes,
    IReadOnlyList<GlossaryEntry> Glossary) : SetupResponse
{
    [JsonPropertyOrder(-1)]
    public override string Kind => "setup";
}

public sealed record NoEligibleSchemeBody(int Players, string Message) : SetupResponse
{
    [JsonPropertyOrder(-1)]
    public override string Kind => "noEligibleScheme";
}

// A chosen Scheme, Mastermind, group or Hero: its catalog id, display name, and the ids of the
// glossary terms it uses (for a Hero, its team and classes too). Box names the box it comes from once
// a setup includes more than one box, as on RuleNote, so the player can tell which card to pull.
public sealed record Component(
    string Id,
    string Name,
    IReadOnlyList<string> Terms,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Box = null);

// A Hero drawn outside the Hero Deck, the pile its cards go to ("villainDeck", "besideScheme" or
// "setAside"), and how many cards that is.
public sealed record OutsideHeroBody(Component Hero, Pile To, int Cards);

// One glossary term the setup uses: kind is "team", "class" or "keyword"; citation is the source
// key and page (for example "R p.9"), and link is where that source is published. Box names the
// box that defines the term once a setup includes more than one box, as on RuleNote.
public sealed record GlossaryEntry(
    string Id,
    string Name,
    string Kind,
    string Summary,
    string Citation,
    string Link,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Box = null);
