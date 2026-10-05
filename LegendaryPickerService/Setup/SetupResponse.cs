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
        SetupResult setup => FromSetup(setup, new Glossary(catalog, setup.Boxes, nameRulesets: setup.Mixed)),
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

        var outsideHenchmen = setup.OutsideHenchmen
            .Select(outside => new OutsideHenchmenBody(glossary.Component(outside.Group.Id, outside.Group.Name, outside.Group.Terms), outside.To, outside.Cards))
            .ToList();

        var cardsBeside = setup.CardsBeside
            .Select(beside => new CardsBesideBody(
                glossary.Component(beside.Group.Id, beside.Group.Name, beside.Group.Terms), beside.Card, beside.Count, beside.FromVillainDeck))
            .ToList();

        // Tactics shuffled into the Villain Deck play with no abilities, so only a Mastermind set aside brings its terms,
        // on its tile and in the glossary.
        var outsideMasterminds = (setup.OutsideMasterminds ?? [])
            .Select(outside => new OutsideMastermindBody(
                glossary.Component(outside.Mastermind.Id, outside.Mastermind.Name, outside.To == Pile.SetAside ? outside.Mastermind.Terms : []),
                outside.To,
                outside.Tactics,
                outside.Joins))
            .ToList();

        Component[] components =
        [
            scheme, mastermind, .. villainGroups, .. henchmanGroups, .. outsideHenchmen.Select(outside => outside.Group),
            .. heroes, .. outsideHeroes.Select(outside => outside.Hero), .. cardsBeside.Select(beside => beside.Group),
            .. outsideMasterminds.Select(outside => outside.Mastermind),
        ];

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
            outsideHenchmen,
            cardsBeside,
            setup.Steps,
            setup.Notes,
            glossary.Entries(components.SelectMany(component => component.Terms)),
            setup.Mixed,
            setup.RulesReason,
            setup.StandIns is { Count: > 0 } standIns ? standIns : null,
            outsideMasterminds.Count > 0 ? outsideMasterminds : null);
    }

    // Every loaded box's glossary terms, ordered teams, then classes, then keywords, each in catalog order.
    // A term links to the URL its box lists for its source key; the loader guarantees one per key.
    // When the setup includes more than one box, each entry names the box that defines the term, and
    // each component the box it comes from. In a mixed setup each component also names its ruleset. A component
    // from a box the setup doesn't include, which a Scheme requires (D-scheme-first, #138), always names its box.
    private sealed class Glossary(BoxCatalog catalog, IReadOnlyList<Box> included, bool nameRulesets)
    {
        private bool NameBoxes => included.Count > 1;

        private readonly Dictionary<string, ((TermKind Kind, int Index) Order, GlossaryEntry Entry)> _terms = catalog.Boxes
            .SelectMany(box => box.Glossary.Select(term => (Term: term, Box: box, Link: box.Sources.First(source => source.Key == term.Source).Url)))
            .Select((x, index) => (x.Term, x.Box, x.Link, Index: index))
            .ToDictionary(
                x => x.Term.Id,
                x => ((x.Term.Kind, x.Index),
                    new GlossaryEntry(
                        x.Term.Id, x.Term.Name, BoxCatalog.KindOf(x.Term.Kind), x.Term.Summary, $"{x.Term.Source} p.{x.Term.Page}", x.Link,
                        included.Count > 1 ? x.Box.Name : null)),
                StringComparer.Ordinal);

        // A catalog id starts with its box's id, which the loader checks.
        private readonly Dictionary<string, Box> _boxes = catalog.Boxes.ToDictionary(box => box.Id, StringComparer.Ordinal);

        public Component Component(string id, string name, IEnumerable<string> terms)
        {
            var box = _boxes[id.Split('_')[0]];
            var notIncluded = !included.Contains(box);
            return new(id, name, Ordered(terms).ToList(), NameBoxes || notIncluded ? box.Name : null, nameRulesets ? box.Ruleset : null, notIncluded);
        }

        public IReadOnlyList<GlossaryEntry> Entries(IEnumerable<string> terms) =>
            Ordered(terms).Select(term => _terms[term].Entry).ToList();

        private IEnumerable<string> Ordered(IEnumerable<string> terms) =>
            terms.Distinct(StringComparer.Ordinal).OrderBy(term => _terms[term].Order);
    }
}

// Ruleset is written camelCase, as in box files ("firstEdition"): the ruleset whose rules the setup follows.
// Mixed is true, and written only then, when the drawn cards come from more than one ruleset. RulesReason says
// why the setup follows its ruleset, and is written only when the included boxes follow more than one. StandIns
// lists each part the drawn cards use that no included box supplies and what replaces it ("with", null when
// nothing does), written only when there is one, so the checklist can say a stack stands in for another.
// OutsideMasterminds lists the Masterminds the Scheme draws besides its own, written only when it draws any.
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
    IReadOnlyList<OutsideHenchmenBody> OutsideHenchmen,
    IReadOnlyList<CardsBesideBody> CardsBeside,
    IReadOnlyList<string> Steps,
    IReadOnlyList<RuleNote> Notes,
    IReadOnlyList<GlossaryEntry> Glossary,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] bool Mixed = false,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] RuleNote? RulesReason = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] IReadOnlyList<StandIn>? StandIns = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] IReadOnlyList<OutsideMastermindBody>? OutsideMasterminds = null) : SetupResponse
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
// a setup includes more than one box, as on RuleNote, so the player can tell which card to pull. Ruleset
// names the ruleset of its box in a mixed setup, so the page can call a Plot a Plot and a Scheme a Scheme.
// NotIncluded is true, and written only then, for a card the Scheme requires from a box the setup doesn't include,
// which the player owns and pulls from that box (D-scheme-first, #138); Box then always names the box.
public sealed record Component(
    string Id,
    string Name,
    IReadOnlyList<string> Terms,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Box = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] Ruleset? Ruleset = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] bool NotIncluded = false);

// A Hero drawn outside the Hero Deck, the pile its cards go to ("villainDeck", "besideScheme" or
// "setAside"), and how many cards that is.
public sealed record OutsideHeroBody(Component Hero, Pile To, int Cards);

// A Henchman Group a Scheme draws outside the Villain Deck, the pile its cards go to ("heroDeck" or "koPile"), and
// how many of its cards that is.
public sealed record OutsideHenchmenBody(Component Group, Pile To, int Cards);

// A Mastermind a Scheme draws besides its own: set aside whole ("setAside"), or tactics of its Tactics into the
// Villain Deck ("villainDeck"); tactics is written only then. joins says when a set-aside one comes into play, and is
// written only when the Scheme says.
public sealed record OutsideMastermindBody(
    Component Mastermind,
    Pile To,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? Tactics,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Joins = null);

// Cards of a group the Scheme sets beside it: count of them, or, when card is present, that one card of the group.
// fromVillainDeck is how many fewer cards the group puts in the Villain Deck because of it, 0 when the group isn't
// drawn there.
public sealed record CardsBesideBody(
    Component Group,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Card,
    int Count,
    int FromVillainDeck);

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
