namespace LegendaryPickerService.Catalog;

// The catalog and setup rules of one box, as stored in Data/Boxes/<id>.json.
// Every rule value carries a Source: a key from the Sources list in Docs/Plan.md,
// usually with a page (for example "R p.20"). Names and counts only; no card text.

public enum GroupType
{
    Villain,
    Henchman,
}

public sealed record Sourced<T>(T Value, string Source);

// A count that can depend on the player count. Players is null when the value
// applies at every player count the Scheme allows.
public sealed record PlayerCountValue(int[]? Players, int Value, string Source);

public sealed record Box(
    int SchemaVersion,
    string Id,
    string Name,
    string CatalogSource,
    IReadOnlyList<SourceLink> Sources,
    BoxComponents Components,
    IReadOnlyList<Hero> Heroes,
    IReadOnlyList<VillainGroup> VillainGroups,
    IReadOnlyList<HenchmanGroup> HenchmanGroups,
    IReadOnlyList<Mastermind> Masterminds,
    IReadOnlyList<Scheme> Schemes,
    IReadOnlyList<GlossaryTerm> Glossary,
    SetupRules Setup);

// Where a source key (the part of a Source before the first space, such as "R" in "R p.20") is published.
// "Card" has no link: it is the printed card itself.
public sealed record SourceLink(string Key, string Url);

public sealed record BoxComponents(
    Sourced<int> HeroCards,
    Sourced<int> VillainGroupCards,
    Sourced<int> HenchmanGroupCards,
    Sourced<int> SchemeTwists);

// Team, Classes and Terms name glossary term ids. Team is null for an unaffiliated Hero.
public sealed record Hero(string Id, string Name, string? Team, IReadOnlyList<string> Classes, IReadOnlyList<string> Terms);

public sealed record VillainGroup(string Id, string Name, IReadOnlyList<string> Terms);

public sealed record HenchmanGroup(string Id, string Name, IReadOnlyList<string> Terms);

public sealed record Mastermind(string Id, string Name, IReadOnlyList<string> Terms, AlwaysLeadsGroup AlwaysLeads);

public sealed record AlwaysLeadsGroup(string GroupId, GroupType GroupType, string Source);

public sealed record Scheme(string Id, string Name, IReadOnlyList<string> Terms, SchemeSetup Setup);

public enum TermKind
{
    Team,
    Class,
    Keyword,
}

// A team, Hero class or keyword a component uses, explained in a short summary written in our own
// words: never rulebook or card text. Source is a key in the box's Sources and Page the rulebook
// page that defines the term, so a reader can follow it to the full rule.
public sealed record GlossaryTerm(string Id, string Name, TermKind Kind, string Summary, string Source, int Page);

// A Scheme's Setup line as data. Absent values leave the box's setup rules unchanged.
public sealed record SchemeSetup(
    IReadOnlyList<PlayerCountValue> Twists,
    Sourced<int[]>? AllowedPlayerCounts = null,
    IReadOnlyList<PlayerCountValue>? Heroes = null,
    Sourced<int>? VillainDeckBystanders = null,
    Sourced<int>? WoundsPerPlayer = null,
    Sourced<int>? ExtraHenchmanGroups = null,
    IReadOnlyList<RequiredGroup>? RequiredGroups = null,
    Sourced<int>? HeroCardsInVillainDeck = null,
    Sourced<int>? TwistsBesideScheme = null);

public sealed record RequiredGroup(string GroupId, GroupType GroupType, string Source);

public sealed record SetupRules(
    IReadOnlyList<PlayerCountSetup> PlayerCounts,
    Sourced<int> Heroes,
    Sourced<int> MasterStrikes,
    StartingDeck StartingDeck,
    SharedStacks SharedStacks,
    SoloSetup Solo,
    Rulings Rulings);

public sealed record PlayerCountSetup(int Players, int VillainGroups, int HenchmanGroups, int Bystanders, string Source);

public sealed record StartingDeck(Sourced<int> Agents, Sourced<int> Troopers);

public sealed record SharedStacks(Sourced<int> Officers, Sourced<int> Wounds, Sourced<int> Bystanders);

public sealed record SoloSetup(
    Sourced<int> Heroes,
    Sourced<int> VillainGroups,
    Sourced<int> HenchmanGroups,
    Sourced<int> HenchmanCards,
    Sourced<int> Bystanders,
    Sourced<int> MasterStrikes,
    Sourced<bool> IgnoresAlwaysLeads,
    Sourced<int> TwistKosHeroCostingAtMost);

// The sources of how rules combine, rather than of a single value.
public sealed record Rulings(
    // An Always Leads group fills one of the player-count group slots.
    string AlwaysLeadsFillsSlot,
    // When a Scheme's required groups leave no slot for the Always Leads group, the Always Leads group is dropped.
    string RequiredGroupDisplacesAlwaysLeads,
    // A Scheme's Setup line overrides the Solo setup.
    string SchemeOverridesSolo);
