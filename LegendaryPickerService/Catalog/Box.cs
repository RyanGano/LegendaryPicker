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

// Facts about the product itself, for listing it. Released is the year and month it first came out, as "2014-10" (#106).
public sealed record About(Sourced<string> Released);

// A count that can depend on the player count. Players is null when the value
// applies at every player count the Scheme allows; player count 1 is Solo.
public sealed record PlayerCountValue(int[]? Players, int Value, string Source);

// The rules a box's cards are played under, separate from the box: First Edition for the core box and the
// Heroic expansions, and Villainous for Legendary: Villains. Written camelCase in box files ("firstEdition").
// A setup can include boxes of both when a base game gives rules for mixing them (SetupRules.Mixing); the
// Scheme and Mastermind it draws then decide which ruleset it follows (#73, #85, #88). An expansion with an
// OtherRuleset section can also be included with base games of the other ruleset alone, whose rules it follows (#110).
public enum Ruleset
{
    FirstEdition,
    Villainous,
}

// BystanderUses lists the parts the box's special Bystanders use, such as Civil War's Aspiring Hero gaining a Sidekick
// when rescued. Every setup shuffles all included Bystanders together, so a setup with the box's Bystanders lays these
// parts out whatever it draws (#125).
public sealed record Box(
    int SchemaVersion,
    string Id,
    string Name,
    Ruleset Ruleset,
    string CatalogSource,
    About About,
    IReadOnlyList<SourceLink> Sources,
    BoxComponents Components,
    IReadOnlyList<Hero> Heroes,
    IReadOnlyList<VillainGroup> VillainGroups,
    IReadOnlyList<HenchmanGroup> HenchmanGroups,
    IReadOnlyList<Mastermind> Masterminds,
    IReadOnlyList<Scheme> Schemes,
    IReadOnlyList<GlossaryTerm> Glossary,
    SetupRules? Setup = null,
    OtherRuleset? OtherRuleset = null,
    IReadOnlyList<PartUse>? BystanderUses = null)
{
    // Only a base game supplies setup rules; an expansion's box file has no setup section.
    public bool IsBaseGame => Setup is not null;
}

// An expansion's rules for playing it without a base game of its own ruleset, under an included base game of
// another, as Fear Itself's insert allows with Heroic sets alone (FI p.2, #110). Source cites the rule that its
// Schemes and Masterminds then follow that base game's rules. StandIns are the parts it replaces when no included
// box supplies them.
public sealed record OtherRuleset(string Source, IReadOnlyList<StandIn> StandIns);

// A part a setup replaces when no included box supplies it: cards that use Part use With instead, as Wounds stand in
// for Bindings (FI p.2). With is null when the replacement needs no part, as a New Recruit gain becomes +1 Recruit.
public sealed record StandIn(Part Part, string Source, Part? With = null);

// Where a source key (the part of a Source before the first space, such as "R" in "R p.20") is published.
// "Card" has no link: it is the printed card itself.
public sealed record SourceLink(string Key, string Url);

// The cards a box puts in the shared stacks, which a setup sums across its included boxes. A stack
// the box adds nothing to is left out. A box's special cards of a stack, such as Special Bystanders
// shuffled in with the Bystanders, count toward that stack. Ambitions are the box's Ambition cards, a supply a
// Scheme draws from rather than a stack players use (#119). Bindings, Madame HYDRA and New Recruits are
// the Villainous stacks; the Villains rulebook says they are not the same cards as Wounds and Officers,
// so each is a stack of its own. Shards are tokens rather than cards, but a setup lays out their supply the
// same way.
public sealed record BoxComponents(
    Sourced<int> HeroCards,
    Sourced<int> VillainGroupCards,
    Sourced<int> HenchmanGroupCards,
    Sourced<int> SchemeTwists,
    Sourced<int>? Bystanders = null,
    Sourced<int>? Wounds = null,
    Sourced<int>? Officers = null,
    Sourced<int>? Sidekicks = null,
    Sourced<int>? Bindings = null,
    Sourced<int>? MadameHydra = null,
    Sourced<int>? NewRecruits = null,
    Sourced<int>? Shards = null,
    Sourced<int>? Ambitions = null)
{
    // What the box adds to a part's stack, or null when it adds none.
    public Sourced<int>? Supplies(Part part) => part switch
    {
        Part.Wounds => Wounds,
        Part.Officers => Officers,
        Part.Sidekicks => Sidekicks,
        Part.Bindings => Bindings,
        Part.MadameHydra => MadameHydra,
        Part.NewRecruits => NewRecruits,
        Part.Shards => Shards,
        Part.Ambitions => Ambitions,
        _ => throw new ArgumentOutOfRangeException(nameof(part), part, null),
    };
}

// A part of the game a card or a base game's rules can use, which a setup lays out only when something in it
// uses the part (owner decision D-uses, #87). So far the parts are the shared stacks other than Bystanders,
// which every setup lays out, the Shard supply (#95) and the Ambition cards (#119). Written camelCase in box files ("madameHydra").
public enum Part
{
    Wounds,
    Officers,
    Sidekicks,
    Bindings,
    MadameHydra,
    NewRecruits,
    Shards,
    Ambitions,
}

// A part a card or a base game's rules use, with its source: Card for a card whose text gains, captures or
// otherwise takes cards from the part's stack, or the rulebook page for the rules.
public sealed record PartUse(Part Part, string Source);

// A Hero, group, Mastermind or Scheme a setup can draw, with its display name and the keywords it uses. Uses lists
// the parts its card text uses; it is left out when the card uses none.
public interface ICard
{
    string Id { get; }

    string Name { get; }

    IReadOnlyList<string> Terms { get; }

    IReadOnlyList<PartUse>? Uses { get; }

    // The parts a setup that draws this card must lay out.
    IEnumerable<Part> Parts => (Uses ?? []).Select(use => use.Part);
}

// Team, Classes and Terms name glossary term ids. Team is null for an unaffiliated Hero. HeroName is the
// Hero Name several Heroes can share, such as two versions of one character; it is left out when the
// Hero's own name is its Hero Name.
public sealed record Hero(
    string Id, string Name, string? Team, IReadOnlyList<string> Classes, IReadOnlyList<string> Terms, string? HeroName = null,
    IReadOnlyList<PartUse>? Uses = null) : ICard
{
    public string NameOfHero => HeroName ?? Name;
}

public sealed record VillainGroup(string Id, string Name, IReadOnlyList<string> Terms, IReadOnlyList<PartUse>? Uses = null) : ICard;

public sealed record HenchmanGroup(string Id, string Name, IReadOnlyList<string> Terms, IReadOnlyList<PartUse>? Uses = null) : ICard;

// Setup is null for a Mastermind whose card does not change the setup.
public sealed record Mastermind(
    string Id, string Name, IReadOnlyList<string> Terms, AlwaysLeadsGroup AlwaysLeads, SetupEffects? Setup = null,
    IReadOnlyList<PartUse>? Uses = null) : ICard;

public sealed record AlwaysLeadsGroup(string GroupId, GroupType GroupType, string Source);

public sealed record Scheme(string Id, string Name, IReadOnlyList<string> Terms, SchemeSetup Setup, IReadOnlyList<PartUse>? Uses = null) : ICard
{
    // A Scheme also uses the stacks its Setup line sizes or moves cards from, so its uses needn't repeat them.
    public IEnumerable<Part> Parts => (Uses ?? []).Select(use => use.Part)
        .Concat(Setup.WoundsPerPlayer is null ? [] : [Part.Wounds])
        .Concat(Setup.BindingsPerPlayer is null ? [] : [Part.Bindings])
        .Concat((Setup.Moves ?? []).Select(move => CardMove.PartOf(move.Card)).OfType<Part>())
        .Distinct();
}

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
// HenchmanCards is how many cards of each Henchman Group go in the Villain Deck. WoundsPerPlayer and
// BindingsPerPlayer set the size of those stacks. TeamSplit asks for Heroes of as many different teams as it has
// values, that many of each team, with the teams left to the draw, as Avengers vs. X-Men's 3 Heroes of one team and
// 3 of another (#125). OwnTactics is how many of the drawn Mastermind's own Tactics the Scheme shuffles into the
// Villain Deck as Villains, as Noir's Hidden Heart of Darkness does (#130).
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
    IReadOnlyList<OutsideHenchmen>? OutsideHenchmen = null,
    Sourced<int>? BindingsPerPlayer = null,
    IReadOnlyList<CardsBeside>? CardsBeside = null,
    IReadOnlyList<OutsideMasterminds>? OutsideMasterminds = null,
    Sourced<int[]>? TeamSplit = null,
    Sourced<int>? OwnTactics = null)
    : SetupEffects(ExtraHeroes, ExtraVillainGroups, ExtraHenchmanGroups, ExtraVillainDeckBystanders, Steps);

public sealed record RequiredGroup(string GroupId, GroupType GroupType, string Source);

// Cards of one group a Scheme sets beside it, whether or not the group is drawn: Count cards at each player
// count, multiplied by the player count when PerPlayer. Card names the one card of the group to set aside, such
// as "Thor" of the Avengers; without it any of the group's cards will do. The cards leave the group, so a drawn
// group puts only what is left of it in the Villain Deck.
public sealed record CardsBeside(
    string GroupId, GroupType GroupType, IReadOnlyList<PlayerCountValue> Count, bool PerPlayer = false, string? Card = null);

// A Hero a Scheme puts in the Hero Deck. Like a required group, it fills one of the Hero slots.
public sealed record RequiredHero(string HeroId, string Source);

// How many of the Hero Deck's Heroes must be of a team (a team term id) or have a Hero Name: at least
// AtLeast, or exactly Exactly. Each constraint names one of Team and HeroName and one of the two counts.
public sealed record HeroCount(string Source, string? Team = null, string? HeroName = null, int? AtLeast = null, int? Exactly = null);

// Heroes a Scheme draws outside the Hero Deck, whose cards all go To one pile: the Villain Deck, beside
// the Scheme, or a set-aside stack. Count says how many Heroes at each player count. Hero names the one
// Hero to draw, HeroName limits the draw to Heroes with that Hero Name and Team to Heroes of that team;
// with none of them any Hero not in the Hero Deck can be drawn. HeroNames limits the draw to Heroes with any of
// those Hero Names, with the source that says they all count (an owner decision, as every Jean Grey does for Dark
// City's Transform Citizens into Demons, #122).
public sealed record OutsideHeroes(
    Pile To, IReadOnlyList<PlayerCountValue> Count, string? Hero = null, string? HeroName = null, string? Team = null,
    Sourced<string[]>? HeroNames = null)
{
    public static readonly Pile[] Destinations = [Pile.VillainDeck, Pile.BesideScheme, Pile.SetAside];
}

// A Henchman Group a Scheme draws outside the Villain Deck, from the included groups the setup doesn't
// already use, and puts Cards of its cards To one pile; the rest of the group stays out of the game.
// Cards says how many at each player count. Each entry draws a group of its own.
public sealed record OutsideHenchmen(Pile To, IReadOnlyList<PlayerCountValue> Cards)
{
    public static readonly Pile[] Destinations = [Pile.HeroDeck, Pile.KoPile];
}

// Masterminds a Scheme draws besides its own, from the included Masterminds the setup doesn't otherwise use (#113).
// Count says how many at each player count. Set aside, a Mastermind waits whole until the Scheme brings it into
// play, as Dark Alliance adds a second Mastermind at its first Twist. Into the Villain Deck go only Tactics of each
// drawn Mastermind, as Master of Tyrants shuffles 12 Tactics of 3 Masterminds in; those play as plain Villains with
// no abilities, so they bring no parts. Tactics is required for, and only for, the Villain Deck. Joins says when a
// set-aside Mastermind comes into play, in a few words ("Twist 1", "Twists 1-3"), and is allowed only for those.
public sealed record OutsideMasterminds(
    Pile To, IReadOnlyList<PlayerCountValue> Count, Sourced<int>? Tactics = null, Sourced<string>? Joins = null)
{
    public static readonly Pile[] Destinations = [Pile.VillainDeck, Pile.SetAside];
}

// A kind of card a Scheme can move during setup. A Twist comes from the Twists the Villain Deck doesn't use, as
// The Traitor adds a 9th Twist to its Betrayal Deck (#96).
public enum CardKind
{
    Hero,
    Henchman,
    Bystander,
    Wound,
    Officer,
    Sidekick,
    Binding,
    Twist,
    Ambition,
}

// A deck, pile or shared stack cards are laid out in. A move takes cards from the pile its card kind
// comes from and puts them in one of the destinations: the Villain Deck, the Hero Deck, beside the
// Scheme, each player's starting deck, or a stack of their own set aside, such as Guardians of the Galaxy's
// Nega-Bomb Deck of Bystanders (#95). Twists is the Twists the boxes hold beyond those the Villain Deck and the
// Scheme use. Ambitions is the boxes' Ambition cards (#119), and KoPile the pile of KO'd cards, which a Scheme can
// start with some Henchmen of a group in it (#116).
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
    Bindings,
    Twists,
    Ambitions,
    KoPile,
}

// A Scheme moving cards of one kind from their own pile to another during setup. Count says how many
// at each player count, so Solo can have its own value; PerPlayer multiplies it by the player count.
// A move into the starting decks puts Count cards in each one, so it is already per player.
public sealed record CardMove(CardKind Card, Pile To, IReadOnlyList<PlayerCountValue> Count, bool PerPlayer = false)
{
    public static readonly Pile[] Destinations = [Pile.VillainDeck, Pile.HeroDeck, Pile.BesideScheme, Pile.StartingDecks, Pile.SetAside];

    // The pile the cards come from, which the card kind decides, so a box file never names it.
    // Hero cards come from the drawn Heroes and Henchmen from the drawn Henchman Groups, so both
    // leave a deck; Twists come from those left over, and every other kind from its shared stack.
    public Pile From() => Card switch
    {
        CardKind.Hero => Pile.HeroDeck,
        CardKind.Henchman => Pile.VillainDeck,
        CardKind.Bystander => Pile.Bystanders,
        CardKind.Wound => Pile.Wounds,
        CardKind.Officer => Pile.Officers,
        CardKind.Sidekick => Pile.Sidekicks,
        CardKind.Binding => Pile.Bindings,
        CardKind.Twist => Pile.Twists,
        CardKind.Ambition => Pile.Ambitions,
        _ => throw new ArgumentOutOfRangeException(nameof(Card), Card, null),
    };

    // The part whose stack a kind of card comes from, or null for a kind that comes from a deck or the Twists.
    public static Part? PartOf(CardKind card) => card switch
    {
        CardKind.Wound => Part.Wounds,
        CardKind.Officer => Part.Officers,
        CardKind.Sidekick => Part.Sidekicks,
        CardKind.Binding => Part.Bindings,
        CardKind.Ambition => Part.Ambitions,
        _ => null,
    };

    // The kind of card a part's stack holds, or null for a part with no card kind a Scheme moves.
    public static CardKind? KindOf(Part part) => part switch
    {
        Part.Wounds => CardKind.Wound,
        Part.Officers => CardKind.Officer,
        Part.Sidekicks => CardKind.Sidekick,
        Part.Bindings => CardKind.Binding,
        Part.Ambitions => CardKind.Ambition,
        _ => null,
    };
}

// A base game's setup rules, for its box's Ruleset: base games of one ruleset can be combined in a setup,
// which then follows the first one's rules. Uses lists the parts the rules themselves use in every setup
// that follows them, such as the stacks players recruit from. ExtraHeroes adds to Heroes at some player
// counts, as Villains adds a 6th Ally with 5 players; it is part of the table, not a rule note. Mixing is
// present on a base game whose rules cover a setup that also includes boxes of another ruleset.
public sealed record SetupRules(
    IReadOnlyList<PlayerCountSetup> PlayerCounts,
    Sourced<int> Heroes,
    Sourced<int> MasterStrikes,
    StartingDeck StartingDeck,
    SoloSetup Solo,
    Rulings Rulings,
    IReadOnlyList<PartUse> Uses,
    IReadOnlyList<PlayerCountValue>? ExtraHeroes = null,
    Mixing? Mixing = null);

public sealed record PlayerCountSetup(int Players, int VillainGroups, int HenchmanGroups, int Bystanders, string Source);

// Each player's two kinds of starting card: S.H.I.E.L.D. Agents and Troopers under First Edition, HYDRA
// Operatives and Soldiers under Villainous. The ruleset names them.
public sealed record StartingDeck(Sourced<int> Agents, Sourced<int> Troopers);

public sealed record SoloSetup(
    Sourced<int> Heroes,
    Sourced<int> VillainGroups,
    Sourced<int> HenchmanGroups,
    Sourced<int> HenchmanCards,
    Sourced<int> Bystanders,
    Sourced<int> MasterStrikes,
    Sourced<bool> IgnoresAlwaysLeads,
    IReadOnlyList<PlayRule> PlayRules);

// A rule Solo play adds that changes no setup count, such as KOing a Hero after each Twist. The setup
// reminds the player of it in a rule note. Label is a short instruction in our own words, never rulebook text.
public sealed record PlayRule(string Label, string Source);

// The sources of how rules combine, rather than of a single value.
public sealed record Rulings(
    // An Always Leads group fills one of the player-count group slots.
    string AlwaysLeadsFillsSlot,
    // When a Scheme's required groups leave no slot for the Always Leads group, the Always Leads group is dropped.
    string RequiredGroupDisplacesAlwaysLeads,
    // A Scheme's Setup line overrides the Solo setup.
    string SchemeOverridesSolo,
    // A part no drawn card or rule uses is left out of the setup, even where the rulebook lays it out.
    string UnusedPartsLeftOut);

// The sources of a base game's rules for mixing its ruleset with another in one setup. A draw whose Scheme or
// Mastermind is of this base game's ruleset follows this base game's rules; any other draw follows its own
// ruleset's base game. A draw with cards of both rulesets draws from shared pools with all Bystanders
// together, and under this base game's rules also lays out the recruit stacks of every included base game
// and offers a choice of starting deck.
public sealed record Mixing(
    // Which base game's rules a draw that includes boxes of several rulesets follows, decided by its Scheme
    // and Mastermind.
    string Rules,
    // Schemes, Masterminds, Heroes and groups are each drawn from one pool across the rulesets.
    string Pools,
    // Players can recruit from the recruit stacks of every included base game, and all Bystanders are
    // shuffled together.
    string Stacks,
    // The players choose which base game's starting deck everyone uses.
    string StartingDeckChoice);
