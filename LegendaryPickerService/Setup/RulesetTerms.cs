using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// The words a ruleset's rulebook uses for the parts of a setup, which rule notes use in turn. The model keeps
// the First Edition names; Villainous calls the same parts by the equivalent terms in the Villains rulebook
// (VIL p.21): an Ally is a Hero, an Adversary Group a Villain Group, a Backup Adversary group a Henchman
// Group, a Plot a Scheme, the Adversary Deck the Villain Deck, and so on.
public sealed record RulesetTerms(
    string Scheme,
    string Mastermind,
    string Hero,
    string Heroes,
    string VillainGroup,
    string VillainGroups,
    string HenchmanGroup,
    string HenchmanGroups,
    string Henchman,
    string Henchmen,
    string VillainDeck,
    string HeroDeck,
    string Twists,
    string Twist)
{
    public static readonly RulesetTerms FirstEdition = new(
        "Scheme", "Mastermind", "Hero", "Heroes", "Villain Group", "Villain Groups", "Henchman Group", "Henchman Groups",
        "Henchman", "Henchmen", "Villain Deck", "Hero Deck", "Twists", "Twist");

    public static readonly RulesetTerms Villainous = new(
        "Plot", "Commander", "Ally", "Allies", "Adversary Group", "Adversary Groups", "Backup Adversary group", "Backup Adversary groups",
        "Backup Adversary", "Backup Adversaries", "Adversary Deck", "Ally Deck", "Plot Twists", "Plot Twist");

    // A setup that draws Heroic and Villainous cards together names a part by both words, since its cards
    // may be either (VIL p.21 treats them as equivalent).
    public static readonly RulesetTerms Mixed = new(
        "Scheme or Plot", "Mastermind or Commander", "Hero or Ally", "Heroes or Allies", "Villain or Adversary Group", "Villain or Adversary Groups",
        "Henchman or Backup Adversary group", "Henchman or Backup Adversary groups", "Henchman or Backup Adversary",
        "Henchmen or Backup Adversaries", "Villain or Adversary Deck", "Hero or Ally Deck", "Scheme Twists or Plot Twists",
        "Scheme Twist or Plot Twist");

    public static RulesetTerms For(Ruleset ruleset) => ruleset switch
    {
        Ruleset.FirstEdition => FirstEdition,
        Ruleset.Villainous => Villainous,
        _ => throw new ArgumentOutOfRangeException(nameof(ruleset), ruleset, null),
    };

    // What a setup's rules are called when it says which ruleset it followed.
    public static string RulesName(Ruleset ruleset) => ruleset switch
    {
        Ruleset.FirstEdition => "First Edition",
        Ruleset.Villainous => "Villains",
        _ => throw new ArgumentOutOfRangeException(nameof(ruleset), ruleset, null),
    };

    // What the rulebooks call a set of each ruleset (VIL p.21): Heroic or Villainous.
    public static string Side(Ruleset ruleset) => ruleset switch
    {
        Ruleset.FirstEdition => "Heroic",
        Ruleset.Villainous => "Villainous",
        _ => throw new ArgumentOutOfRangeException(nameof(ruleset), ruleset, null),
    };

    // The team each ruleset's starting deck belongs to (VIL p.21).
    public static string StartingTeam(Ruleset ruleset) => ruleset switch
    {
        Ruleset.FirstEdition => "S.H.I.E.L.D.",
        Ruleset.Villainous => "HYDRA",
        _ => throw new ArgumentOutOfRangeException(nameof(ruleset), ruleset, null),
    };
}
