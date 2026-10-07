using System.Text.Json.Serialization;

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
// parts out whatever it draws (#125). Reprints lists the ids of another box's Heroes, groups, Masterminds and Schemes
// this box holds a printing of: each is one card with the original, in the pool once however many boxes holding it are
// included (#34 D5, #147). OptionalTokens lists the ids of the box's own cards that its optional Token cards can be
// used with; a setup that draws one tells the player the Tokens could be used (#148). The Tokens are never counted.
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
    IReadOnlyList<PartUse>? BystanderUses = null,
    Sourced<string[]>? Reprints = null,
    Sourced<string[]>? OptionalTokens = null)
{
    // Only a base game supplies setup rules; an expansion's box file has no setup section.
    public bool IsBaseGame => Setup is not null;

    // The cards Reprints names, which the catalog resolves when it loads.
    [JsonIgnore]
    public IReadOnlyList<ICard> ReprintedCards { get; init; } = [];

    // Whether the box holds a card: one it declares or one it reprints.
    public bool Holds(string id) => id.StartsWith(Id + "_", StringComparison.Ordinal) || (Reprints?.Value.Contains(id) ?? false);

    // The cards a setup that includes the box can draw: its own, then its reprints.
    public IEnumerable<Hero> AllHeroes => Heroes.Concat(ReprintedCards.OfType<Hero>());

    public IEnumerable<VillainGroup> AllVillainGroups => VillainGroups.Concat(ReprintedCards.OfType<VillainGroup>());

    public IEnumerable<HenchmanGroup> AllHenchmanGroups => HenchmanGroups.Concat(ReprintedCards.OfType<HenchmanGroup>());

    public IEnumerable<Mastermind> AllMasterminds => Masterminds.Concat(ReprintedCards.OfType<Mastermind>());

    public IEnumerable<Scheme> AllSchemes => Schemes.Concat(ReprintedCards.OfType<Scheme>());
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
// same way. Horrors are X-Men's Horror cards (XM p.2), a stack a setup lays out only when a drawn card plays them (#132).
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
    Sourced<int>? Ambitions = null,
    Sourced<int>? Horrors = null)
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
        Part.Horrors => Horrors,
        _ => throw new ArgumentOutOfRangeException(nameof(part), part, null),
    };
}

// A part of the game a card or a base game's rules can use, which a setup lays out only when something in it
// uses the part (owner decision D-uses, #87). So far the parts are the shared stacks other than Bystanders,
// which every setup lays out, the Shard supply (#95), the Ambition cards (#119) and the Horrors (#132). Written camelCase in box
// files ("madameHydra").
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
    Horrors,
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

// Team, Classes and Terms name glossary term ids. Team is null for an unaffiliated Hero. AlsoTeam is the second team of a
// Divided Card Hero whose right halves show another team, as Storm & Black Panther's Avengers beside its X-Men; the Hero
// counts as either team, one at a time (owner rule, #149). HeroName is the Hero Name several Heroes can share, such as a
// version of a character (Spider-Man Noir is Spider-Man); it is left out when the Hero's own name is its Hero Name.
// HeroNames replaces it for a Hero whose halves show two names, as Colossus & Wolverine counts as Colossus and as
// Wolverine (#149).
public sealed record Hero(
    string Id, string Name, string? Team, IReadOnlyList<string> Classes, IReadOnlyList<string> Terms, string? HeroName = null,
    IReadOnlyList<PartUse>? Uses = null, IReadOnlyList<string>? HeroNames = null, string? AlsoTeam = null) : ICard
{
    // Every Hero Name the Hero counts under for Hero Name rules.
    public IReadOnlyList<string> NamesOfHero => HeroNames ?? [HeroName ?? Name];

    // Every team the Hero counts as: none, its team, or a Divided Card's two.
    public IReadOnlyList<string> Teams => Team is null ? [] : AlsoTeam is null ? [Team] : [Team, AlsoTeam];

    public bool HasTeam(string team) => Team == team || AlsoTeam == team;

    public bool HasHeroName(string name) => HeroNames?.Contains(name) ?? (HeroName ?? Name) == name;

    // Whether the two Heroes have a Hero Name in common, so a Scheme allowing no two Heroes with one Hero Name can't take both.
    public bool SharesHeroName(Hero other) => HeroNames?.Any(other.HasHeroName) ?? other.HasHeroName(HeroName ?? Name);

    // Whether a word is in the Hero Name as the card prints it, the display name less any bracketed version: "Hulk" is in
    // She-Hulk and Hulkbuster Iron Man, not in Bruce Banner.
    public bool HasInHeroName(string word) => Name.Split(" (")[0].Contains(word, StringComparison.Ordinal);
}

public sealed record VillainGroup(string Id, string Name, IReadOnlyList<string> Terms, IReadOnlyList<PartUse>? Uses = null) : ICard;

public sealed record HenchmanGroup(string Id, string Name, IReadOnlyList<string> Terms, IReadOnlyList<PartUse>? Uses = null) : ICard;

// Setup is null for a Mastermind whose card does not change the setup. AlwaysLeads is null for a Mastermind whose Always
// Leads ability allows any group, as Ego's does (it only adds a Villain Group in its Setup), so it constrains no draw. AlsoLeads is a second group its card always
// leads, picked from several, as Deathbird leads a Shi'ar Henchman Group as well as the Shi'ar Imperial Guard (#132).
// Epic is the Mastermind's Epic side, on the same card; draws don't use it yet (#151).
public sealed record Mastermind(
    string Id, string Name, IReadOnlyList<string> Terms, AlwaysLeadsGroup? AlwaysLeads = null, SetupEffects? Setup = null,
    IReadOnlyList<PartUse>? Uses = null, AlsoLeadsGroup? AlsoLeads = null, EpicSide? Epic = null) : ICard;

// A Mastermind's Epic side, which shares the normal side's Tactics: its printed title, and in Uses the parts its text
// uses beyond the normal side's Uses, which the setup would have to lay out (the Horrors an Epic X-Men Master Strike plays). Source is the card face.
public sealed record EpicSide(string Name, string Source, IReadOnlyList<PartUse>? Uses = null);

public sealed record AlwaysLeadsGroup(string GroupId, GroupType GroupType, string Source);

// Groups of one type a Mastermind always leads one of, besides its Always Leads group: the setup takes one of them
// that is drawn already, or draws one of those included into a slot, which it fills as the Always Leads group does. One
// of the groups is in the Mastermind's box; the others can be another box's, as Bastion leads any Sentinel Henchman Group.
public sealed record AlsoLeadsGroup(IReadOnlyList<string> GroupIds, GroupType GroupType, string Source);

// ExcludesMasterminds lists the Masterminds the Scheme's card rules out, recorded when its box is entered: those whose
// Always Leads group it sets every card of beside it, as Clash of the Monsters Unleashed does Fin Fang Foom's
// (D-scheme-first, #138). The draw reads the list and computes nothing; a test derives it from the card data.
public sealed record Scheme(
    string Id, string Name, IReadOnlyList<string> Terms, SchemeSetup Setup, IReadOnlyList<PartUse>? Uses = null,
    IReadOnlyList<ExcludedMastermind>? ExcludesMasterminds = null) : ICard
{
    // A Scheme also uses the stacks its Setup line sizes or moves cards from, so its uses needn't repeat them.
    public IEnumerable<Part> Parts => (Uses ?? []).Select(use => use.Part)
        .Concat(Setup.WoundsPerPlayer is null && Setup.Wounds is null ? [] : [Part.Wounds])
        .Concat(Setup.Officers is null ? [] : [Part.Officers])
        .Concat(Setup.BindingsPerPlayer is null ? [] : [Part.Bindings])
        .Concat((Setup.Moves ?? []).Select(move => CardMove.PartOf(move.Card)).OfType<Part>())
        .Distinct();
}

// A Mastermind a Scheme rules out, with the source that says so.
public sealed record ExcludedMastermind(string MastermindId, string Source);

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
// and a player count no entry names adds nothing. Steps are setup steps that change no count. HenchmanCards sets
// how many cards of each Henchman Group go in the Villain Deck, replacing the table or Solo count, as Annihilus's
// 6 in Solo; a Mastermind's applies only when its Scheme sets none.
public record SetupEffects(
    IReadOnlyList<PlayerCountValue>? ExtraHeroes = null,
    IReadOnlyList<PlayerCountValue>? ExtraVillainGroups = null,
    IReadOnlyList<PlayerCountValue>? ExtraHenchmanGroups = null,
    IReadOnlyList<PlayerCountValue>? ExtraVillainDeckBystanders = null,
    IReadOnlyList<SetupStep>? Steps = null,
    IReadOnlyList<PlayerCountValue>? HenchmanCards = null);

// A setup step that changes no count, such as placing a token on the Scheme, which the setup lists as a
// line to tick. Label is a short instruction in our own words, never card text.
public sealed record SetupStep(string Label, string Source);

// A Scheme's Setup line as data. Absent values leave the box's setup rules unchanged.
// Heroes, HenchmanCards and VillainDeckBystanders set a count, replacing the table or Solo value.
// HenchmanCards is how many cards of each Henchman Group go in the Villain Deck. WoundsPerPlayer and
// BindingsPerPlayer set the size of those stacks. TeamSplit asks for Heroes of as many different teams as it has
// values, that many of each team, with the teams left to the draw, as Avengers vs. X-Men's 3 Heroes of one team and
// 3 of another (#125), or House of M's 4 Heroes of one team, the X-Men it prints or any team it lets the player name
// instead (#170), the rest of the Hero Deck then of other teams. OwnTactics is how many of the drawn Mastermind's own Tactics the Scheme shuffles into the
// Villain Deck as Villains, as Noir's Hidden Heart of Darkness does (#130). Wounds sets the Wound stack to a size
// whatever the player count, as Anti-Mutant Hatred's 30 Wounds (#132); a Scheme sets it this way or per player, not both.
// ExtraHenchmanCards is how many cards each Henchman Group the Scheme's ExtraHenchmanGroups adds puts in the Villain Deck,
// in place of the usual count, Solo's 3 or HenchmanCards, as Scavenge Alien Weaponry's 10 Smugglers even in Solo (#134).
// OneOfGroups requires exactly one of several groups and leaves the others out, as S.H.I.E.L.D. vs. HYDRA War takes
// A.I.M., Hydra Offshoot or Hydra Elite but not both (#172). Officers sets the S.H.I.E.L.D. Officer stack to a size, as
// Secret HYDRA Corruption's 30 Officers even when S.H.I.E.L.D.'s special Officers are included (#172).
// VillainCards sets how many cards of each Villain Group go in the Villain Deck, replacing all of it, as Star-Lord's Awesome
// Mix Tape's half of each group.
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
    Sourced<int>? OwnTactics = null,
    Sourced<int>? Wounds = null,
    Sourced<int>? ExtraHenchmanCards = null,
    GroupChoice? OneOfGroups = null,
    Sourced<int>? Officers = null,
    Sourced<int>? VillainCards = null)
    : SetupEffects(ExtraHeroes, ExtraVillainGroups, ExtraHenchmanGroups, ExtraVillainDeckBystanders, Steps, HenchmanCards);

// Groups of one type a Scheme requires exactly one of: the setup takes the drawn Mastermind's Always Leads group when
// it is one of them, or else draws one, and leaves the others out of the setup.
public sealed record GroupChoice(IReadOnlyList<string> GroupIds, GroupType GroupType, string Source);

// Cards, for a Henchman Group, is how many of its cards go in the Villain Deck in place of the usual count, as
// Alien Brood Encounters adds 10 Brood even in Solo (#132); it is left out to use the usual count. A required group
// is in the Scheme's own box unless OtherBox allows one from another box (D-scheme-first, #138).
public sealed record RequiredGroup(string GroupId, GroupType GroupType, string Source, int? Cards = null, OtherBox? OtherBox = null);

// A Scheme's requirement from another box, allowed by Source. A setup that doesn't include that box still uses the
// card, which the player owns, and the checklist says the Scheme requires it from that box, as The Kree-Skrull War's
// Skrulls without the core box. Substitute, the source of the card's own fallback, replaces it instead with any
// included group of its type, as Mutant-Hunting Super Sentinels takes another Henchman Group for the Sentinels.
public sealed record OtherBox(string Source, string? Substitute = null);

// Cards of one group a Scheme sets beside it, whether or not the group is drawn: Count cards at each player
// count, multiplied by the player count when PerPlayer. Card names the one card of the group to set aside, such
// as "Thor" of the Avengers; without it any of the group's cards will do. The cards leave the group, so a drawn
// group puts only what is left of it in the Villain Deck.
public sealed record CardsBeside(
    string GroupId, GroupType GroupType, IReadOnlyList<PlayerCountValue> Count, bool PerPlayer = false, string? Card = null);

// A Hero a Scheme puts in the Hero Deck. Like a required group, it fills one of the Hero slots.
public sealed record RequiredHero(string HeroId, string Source);

// How many of the Hero Deck's Heroes must be of a team (a team term id), have a Hero Name, or have a word in their
// Hero Name, as Fall of the Hulks' two Heroes with "Hulk" in their Hero Names (#143): at least AtLeast, or exactly
// Exactly. Each constraint names one of Team, HeroName and HeroNameContains and one of the two counts.
public sealed record HeroCount(
    string Source, string? Team = null, string? HeroName = null, int? AtLeast = null, int? Exactly = null, string? HeroNameContains = null)
{
    public bool Matches(Hero hero) =>
        Team is { } team ? hero.HasTeam(team)
        : HeroNameContains is { } word ? hero.HasInHeroName(word)
        : hero.HasHeroName(HeroName!);
}

// Heroes a Scheme draws outside the Hero Deck, whose cards all go To one pile: the Villain Deck, beside
// the Scheme, or a set-aside stack. Count says how many Heroes at each player count. Hero names the one
// Hero to draw, HeroName limits the draw to Heroes with that Hero Name, HeroNameContains to Heroes with that word in
// their Hero Name, as Shoot Hulk into Space's extra Hulk (#143), and Team to Heroes of that team; with none of them any
// Hero not in the Hero Deck can be drawn. HeroNames limits the draw to Heroes with any of
// those Hero Names, with the source that says they all count (an owner decision, as every Jean Grey does for Dark
// City's Transform Citizens into Demons, #122). OtherBox lets a draw limited by Hero Name take a Hero from a box the
// setup doesn't include when no included Hero has the name, as The Dark Phoenix Saga's Jean Grey (#138).
public sealed record OutsideHeroes(
    Pile To, IReadOnlyList<PlayerCountValue> Count, string? Hero = null, string? HeroName = null, string? Team = null,
    Sourced<string[]>? HeroNames = null, OtherBox? OtherBox = null, string? HeroNameContains = null)
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
// BringsAlwaysLeads, only for those set aside, adds each drawn Mastermind's Always Leads group as an extra group, which
// Symbiotic Absorption's Drained Mastermind does; the Mastermind is drawn before the groups.
public sealed record OutsideMasterminds(
    Pile To, IReadOnlyList<PlayerCountValue> Count, Sourced<int>? Tactics = null, Sourced<string>? Joins = null,
    Sourced<bool>? BringsAlwaysLeads = null)
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
    string SchemeOverridesSolo);

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
