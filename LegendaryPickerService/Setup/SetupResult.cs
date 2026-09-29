using System.Text.Json.Serialization;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

public abstract record GenerationResult;

// No Scheme in the included boxes can be set up legally at this player count.
public sealed record NoEligibleScheme(int Players) : GenerationResult;

// A legal random setup and the checklist for laying it out, under the Ruleset of its base games.
// Steps are the labels of the setup steps its Scheme and Mastermind print that change no count, Scheme first.
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
    IReadOnlyList<string> Steps,
    IReadOnlyList<RuleNote> Notes,
    IReadOnlyList<Box> Boxes) : GenerationResult;

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

public sealed record HeroDeck(int HeroCards, [property: JsonIgnore] int MovedOut, [property: JsonIgnore] int MovedIn = 0)
{
    public int Total => HeroCards + MovedIn - MovedOut;
}

// Cards a Scheme moves during setup, from the pile their kind comes from. Count is what the
// destination gets: with To StartingDecks, what each player's deck gets. Total is what leaves From.
public sealed record MovedCards(CardKind Card, Pile From, Pile To, int Count, int Total);

// A Hero a Scheme draws outside the Hero Deck, and the pile all its cards go to.
public sealed record OutsideHero(Hero Hero, Pile To, int Cards);

// Each stack holds the cards every included box adds to it, or the size the Scheme sets, less what
// the setup moves out. Sidekicks is null, and left out of the response, when no included box has any.
public sealed record SetupStacks(
    int Wounds,
    int Officers,
    int Bystanders,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] int? Sidekicks = null);

// Each player's starting deck.
public sealed record PlayerDeck(int Agents, int Troopers);

// One rule that changed the result, with its source key (for example "R p.6") and,
// when the source is published, a link to it. Box names the box the rule comes from once
// a setup includes more than one box; with one box it is left out of the response.
public sealed record RuleNote(
    string Text,
    string Citation,
    string? Link,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? Box = null);
