using System.Text.Json.Serialization;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

public abstract record GenerationResult;

// No Scheme in the included boxes can be set up legally at this player count.
public sealed record NoEligibleScheme(int Players) : GenerationResult;

// A legal random setup and the checklist for laying it out, under the Ruleset whose rules it follows.
// Steps are the labels of the setup steps its Scheme and Mastermind print that change no count, Scheme first.
// When the included boxes follow more than one ruleset, the drawn cards decide the Ruleset and RulesReason
// says why; Mixed is true when the drawn cards themselves come from more than one ruleset.
public sealed record SetupResult(
    int Players,
    Ruleset Ruleset,
    Scheme Scheme,
    Mastermind Mastermind,
    IReadOnlyList<VillainGroup> VillainGroups,
    IReadOnlyList<HenchmanGroup> HenchmanGroups,
    IReadOnlyList<Hero> Heroes,
    VillainDeck VillainDeck,
    HeroDeck HeroDeck,
    int TwistsBesideScheme,
    SetupStacks Stacks,
    PlayerDeck PlayerDeck,
    IReadOnlyList<MovedCards> Moves,
    IReadOnlyList<OutsideHero> OutsideHeroes,
    IReadOnlyList<OutsideHenchmanGroup> OutsideHenchmen,
    IReadOnlyList<string> Steps,
    IReadOnlyList<RuleNote> Notes,
    IReadOnlyList<Box> Boxes,
    bool Mixed = false,
    RuleNote? RulesReason = null) : GenerationResult;

// MovedIn and MovedOut count the cards the setup's moves put in the deck and take out of it. The
// response lists each move under the setup's moves instead, so they are left out of it. OutsideHeroCards
// counts the cards of the Heroes drawn outside the Hero Deck that go into the Villain Deck.
public sealed record VillainDeck(
    int Twists,
    int MasterStrikes,
    int VillainCards,
    int HenchmanCards,
    int Bystanders,
    [property: JsonIgnore] int MovedIn,
    [property: JsonIgnore] int MovedOut = 0,
    int OutsideHeroCards = 0)
{
    public int Total => Twists + MasterStrikes + VillainCards + HenchmanCards + Bystanders + OutsideHeroCards + MovedIn - MovedOut;
}

// OutsideHenchmanCards counts the Henchmen of the Henchman Groups drawn outside the Villain Deck that go into
// the Hero Deck. The response lists each of those groups under the setup's outside Henchmen, so it is left out.
public sealed record HeroDeck(
    int HeroCards,
    [property: JsonIgnore] int MovedOut,
    [property: JsonIgnore] int MovedIn = 0,
    [property: JsonIgnore] int OutsideHenchmanCards = 0)
{
    public int Total => HeroCards + MovedIn + OutsideHenchmanCards - MovedOut;
}

// Cards a Scheme moves during setup, from the pile their kind comes from. Count is what the
// destination gets: with To StartingDecks, what each player's deck gets. Total is what leaves From.
public sealed record MovedCards(CardKind Card, Pile From, Pile To, int Count, int Total);

// A Hero a Scheme draws outside the Hero Deck, and the pile all its cards go to.
public sealed record OutsideHero(Hero Hero, Pile To, int Cards);

// A Henchman Group a Scheme draws outside the Villain Deck, and the pile Cards of its cards go to. The rest
// of the group stays out of the game.
public sealed record OutsideHenchmanGroup(HenchmanGroup Group, Pile To, int Cards);

// Each stack holds the cards the included boxes of the drawn cards' rulesets add to it, or the size the
// Scheme sets, less what the setup moves out. A stack is laid out only when the setup's rules or one of its
// drawn cards use it (D-uses); any other is null and left out of the response. So a First Edition setup has
// no Bindings, Madame HYDRA or New Recruits, a Villainous one no Wounds or Officers, and Wounds, Bindings and
// Sidekicks appear only when a drawn card uses them. Every setup lays out Bystanders.
public sealed record SetupStacks(
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? Wounds,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? Officers,
    int Bystanders,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? Sidekicks = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? Bindings = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? MadameHydra = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? NewRecruits = null);

// Each player's starting deck: S.H.I.E.L.D. Agents and Troopers, or under Villainous HYDRA Operatives and Soldiers.
// Choices lists the rulesets whose starting decks the players choose between in a mixed setup that includes
// base games of both (VIL p.21); it is null when there is no choice.
public sealed record PlayerDeck(
    int Agents,
    int Troopers,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] IReadOnlyList<Ruleset>? Choices = null);

// One rule that changed the result, with its source key (for example "R p.6") and,
// when the source is published, a link to it. Box names the box the rule comes from once
// a setup includes more than one box; with one box it is left out of the response.
public sealed record RuleNote(
    string Text,
    string Citation,
    string? Link,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Box = null);
