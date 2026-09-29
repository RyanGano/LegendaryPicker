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
// applies at every player count the Scheme allows; player count 1 is Solo.
public sealed record PlayerCountValue(int[]? Players, int Value, string Source);

// The rules a base game is played under, separate from the box: First Edition's core box, and
// Legendary: Villains, which plays by its own. Written camelCase in box files ("firstEdition").
public enum Ruleset
{
    FirstEdition,
    Villainous,
}

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
    SetupRules? Setup = null)
{
    // Only a base game supplies setup rules; an expansion's box file has no setup section.
    public bool IsBaseGame => Setup is not null;
}

// Where a source key (the part of a Source before the first space, such as "R" in "R p.20") is published.
// "Card" has no link: it is the printed card itself.
public sealed record SourceLink(string Key, string Url);

// The cards a box puts in the shared stacks, which a setup sums across its included boxes. A stack
// the box adds nothing to is left out. A box's special cards of a stack, such as Special Bystanders
// shuffled in with the Bystanders, count toward that stack.
public sealed record BoxComponents(
    Sourced<int> HeroCards,
    Sourced<int> VillainGroupCards,
    Sourced<int> HenchmanGroupCards,
    Sourced<int> SchemeTwists,
    Sourced<int>? Bystanders = null,
    Sourced<int>? Wounds = null,
    Sourced<int>? Officers = null,
    Sourced<int>? Sidekicks = null);

// Team, Classes and Terms name glossary term ids. Team is null for an unaffiliated Hero. HeroName is the
// Hero Name several Heroes can share, such as two versions of one character; it is left out when the
// Hero's own name is its Hero Name.
public sealed record Hero(string Id, string Name, string? Team, IReadOnlyList<string> Classes, IReadOnlyList<string> Terms, string? HeroName = null)
{
    public string NameOfHero => HeroName ?? Name;
}

public sealed record VillainGroup(string Id, string Name, IReadOnlyList<string> Terms);

public sealed record HenchmanGroup(string Id, string Name, IReadOnlyList<string> Terms);

// Setup is null for a Mastermind whose card does not change the setup.
public sealed record Mastermind(string Id, string Name, IReadOnlyList<string> Terms, AlwaysLeadsGroup AlwaysLeads, SetupEffects? Setup = null);

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

// Setup effects that add to a count rather than set it, which a Scheme or a Mastermind can print.
// Each adds its value to the count the setup would otherwise use (the player-count table or Solo,
// after any Scheme value that sets it); a Scheme's are applied before its Mastermind's. Each is a
// list so its value can depend on the player count: an entry for players [1] applies only in Solo,
// and a player count no entry names adds nothing. Steps are setup steps that change no count.
public record SetupEffects(
    IReadOnlyList<PlayerCountValue>? ExtraHeroes = null,
    IReadOnlyList<PlayerCountValue>? ExtraVillainGroups = null,
    IReadOnlyList<PlayerCountValue>? ExtraHenchmanGroups = null,
    IReadOnlyList<PlayerCountValue>? ExtraVillainDeckBystanders = null,
    IReadOnlyList<SetupStep>? Steps = null);

// A setup step that changes no count, such as placing a token on the Scheme, which the setup lists as a
// line to tick. Label is a short instruction in our own words, never card text.
public sealed record SetupStep(string Label, string Source);

// A Scheme's Setup line as data. Absent values leave the box's setup rules unchanged.
// Heroes, HenchmanCards and VillainDeckBystanders set a count, replacing the table or Solo value.
// HenchmanCards is how many cards of each Henchman Group go in the Villain Deck.
public sealed record SchemeSetup(
    IReadOnlyList<PlayerCountValue> Twists,
    Sourced<int[]>? AllowedPlayerCounts = null,
    IReadOnlyList<PlayerCountValue>? Heroes = null,
    IReadOnlyList<PlayerCountValue>? HenchmanCards = null,
    Sourced<int>? VillainDeckBystanders = null,
    Sourced<int>? WoundsPerPlayer = null,
    IReadOnlyList<RequiredGroup>? RequiredGroups = null,
    IReadOnlyList<CardMove>? Moves = null,
    Sourced<int>? TwistsBesideScheme = null,
    IReadOnlyList<RequiredHero>? RequiredHeroes = null,
    IReadOnlyList<HeroCount>? HeroCounts = null,
    Sourced<bool>? DistinctHeroNames = null,
    IReadOnlyList<OutsideHeroes>? OutsideHeroes = null,
    IReadOnlyList<PlayerCountValue>? ExtraHeroes = null,
    IReadOnlyList<PlayerCountValue>? ExtraVillainGroups = null,
    IReadOnlyList<PlayerCountValue>? ExtraHenchmanGroups = null,
    IReadOnlyList<PlayerCountValue>? ExtraVillainDeckBystanders = null,
    IReadOnlyList<SetupStep>? Steps = null,
    IReadOnlyList<OutsideHenchmen>? OutsideHenchmen = null)
    : SetupEffects(ExtraHeroes, ExtraVillainGroups, ExtraHenchmanGroups, ExtraVillainDeckBystanders, Steps);

public sealed record RequiredGroup(string GroupId, GroupType GroupType, string Source);

// A Hero a Scheme puts in the Hero Deck. Like a required group, it fills one of the Hero slots.
public sealed record RequiredHero(string HeroId, string Source);

// How many of the Hero Deck's Heroes must be of a team (a team term id) or have a Hero Name: at least
// AtLeast, or exactly Exactly. Each constraint names one of Team and HeroName and one of the two counts.
public sealed record HeroCount(string Source, string? Team = null, string? HeroName = null, int? AtLeast = null, int? Exactly = null);

// Heroes a Scheme draws outside the Hero Deck, whose cards all go To one pile: the Villain Deck, beside
// the Scheme, or a set-aside stack. Count says how many Heroes at each player count. Hero names the one
// Hero to draw, HeroName limits the draw to Heroes with that Hero Name and Team to Heroes of that team;
// with none of them any Hero not in the Hero Deck can be drawn.
public sealed record OutsideHeroes(
    Pile To, IReadOnlyList<PlayerCountValue> Count, string? Hero = null, string? HeroName = null, string? Team = null)
{
    public static readonly Pile[] Destinations = [Pile.VillainDeck, Pile.BesideScheme, Pile.SetAside];
}

// A Henchman Group a Scheme draws outside the Villain Deck, from the included groups the setup doesn't
// already use, and puts Cards of its cards To one pile; the rest of the group stays out of the game.
// Cards says how many at each player count. Each entry draws a group of its own.
public sealed record OutsideHenchmen(Pile To, IReadOnlyList<PlayerCountValue> Cards)
{
    public static readonly Pile[] Destinations = [Pile.HeroDeck];
}

// A kind of card a Scheme can move during setup.
public enum CardKind
{
    Hero,
    Henchman,
    Bystander,
    Wound,
    Officer,
    Sidekick,
}

// A deck, pile or shared stack cards are laid out in. A move takes cards from the pile its card kind
// comes from and puts them in one of the destinations: the Villain Deck, the Hero Deck, beside the
// Scheme, or each player's starting deck. Heroes drawn outside the Hero Deck can also go to a stack of
// their own, set aside.
public enum Pile
{
    VillainDeck,
    HeroDeck,
    BesideScheme,
    StartingDecks,
    SetAside,
    Bystanders,
    Wounds,
    Officers,
    Sidekicks,
}

// A Scheme moving cards of one kind from their own pile to another during setup. Count says how many
// at each player count, so Solo can have its own value; PerPlayer multiplies it by the player count.
// A move into the starting decks puts Count cards in each one, so it is already per player.
public sealed record CardMove(CardKind Card, Pile To, IReadOnlyList<PlayerCountValue> Count, bool PerPlayer = false)
{
    public static readonly Pile[] Destinations = [Pile.VillainDeck, Pile.HeroDeck, Pile.BesideScheme, Pile.StartingDecks];

    // The pile the cards come from, which the card kind decides, so a box file never names it.
    // Hero cards come from the drawn Heroes and Henchmen from the drawn Henchman Groups, so both
    // leave a deck; every other kind comes from its shared stack.
    public Pile From() => Card switch
    {
        CardKind.Hero => Pile.HeroDeck,
        CardKind.Henchman => Pile.VillainDeck,
        CardKind.Bystander => Pile.Bystanders,
        CardKind.Wound => Pile.Wounds,
        CardKind.Officer => Pile.Officers,
        CardKind.Sidekick => Pile.Sidekicks,
        _ => throw new ArgumentOutOfRangeException(nameof(Card), Card, null),
    };
}

// A base game's setup rules. Ruleset names the rules they belong to: base games of one ruleset can be
// combined in a setup, which then follows the first one's rules.
public sealed record SetupRules(
    Ruleset Ruleset,
    IReadOnlyList<PlayerCountSetup> PlayerCounts,
    Sourced<int> Heroes,
    Sourced<int> MasterStrikes,
    StartingDeck StartingDeck,
    SoloSetup Solo,
    Rulings Rulings);

public sealed record PlayerCountSetup(int Players, int VillainGroups, int HenchmanGroups, int Bystanders, string Source);

public sealed record StartingDeck(Sourced<int> Agents, Sourced<int> Troopers);

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
