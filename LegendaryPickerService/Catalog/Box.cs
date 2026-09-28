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
    BoxComponents Components,
    IReadOnlyList<Hero> Heroes,
    IReadOnlyList<VillainGroup> VillainGroups,
    IReadOnlyList<HenchmanGroup> HenchmanGroups,
    IReadOnlyList<Mastermind> Masterminds,
    IReadOnlyList<Scheme> Schemes,
    SetupRules Setup);

public sealed record BoxComponents(
    Sourced<int> HeroCards,
    Sourced<int> VillainGroupCards,
    Sourced<int> HenchmanGroupCards,
    Sourced<int> SchemeTwists);

public sealed record Hero(string Id, string Name);

public sealed record VillainGroup(string Id, string Name);

public sealed record HenchmanGroup(string Id, string Name);

public sealed record Mastermind(string Id, string Name, AlwaysLeadsGroup AlwaysLeads);

public sealed record AlwaysLeadsGroup(string GroupId, GroupType GroupType, string Source);

public sealed record Scheme(string Id, string Name, SchemeSetup Setup);

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
    SoloSetup Solo);

public sealed record PlayerCountSetup(int Players, int VillainGroups, int HenchmanGroups, int Bystanders, string Source);

public sealed record StartingDeck(Sourced<int> Agents, Sourced<int> Troopers);

public sealed record SharedStacks(Sourced<int> Officers, Sourced<int> Wounds, Sourced<int> Bystanders);

public sealed record SoloSetup(
    Sourced<int> Heroes,
    Sourced<int> VillainGroups,
    Sourced<int> HenchmanCards,
    Sourced<int> Bystanders,
    Sourced<int> MasterStrikes,
    Sourced<bool> IgnoresAlwaysLeads);
