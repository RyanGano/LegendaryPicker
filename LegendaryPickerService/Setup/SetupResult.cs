using System.Text.Json.Serialization;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

public abstract record GenerationResult;

// No Scheme in the included boxes can be set up legally at this player count.
public sealed record NoEligibleScheme(int Players) : GenerationResult;

// A legal random setup and the checklist for laying it out.
public sealed record SetupResult(
    int Players,
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
    IReadOnlyList<RuleNote> Notes,
    IReadOnlyList<Box> Boxes) : GenerationResult;

public sealed record VillainDeck(
    int Twists,
    int MasterStrikes,
    int VillainCards,
    int HenchmanCards,
    int Bystanders,
    int HeroCards)
{
    public int Total => Twists + MasterStrikes + VillainCards + HenchmanCards + Bystanders + HeroCards;
}

public sealed record HeroDeck(int HeroCards, int MovedToVillainDeck)
{
    public int Total => HeroCards - MovedToVillainDeck;
}

public sealed record SetupStacks(int Wounds, int Officers, int Bystanders);

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
