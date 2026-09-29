using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// The words a ruleset's rulebook uses for the parts of a setup, which rule notes use in turn. The model keeps
// the First Edition names; Villainous calls the same parts by the equivalent terms in the Villains rulebook
// (VIL p.21): an Ally is a Hero, an Adversary Group a Villain Group, a Backup Adversary group a Henchman
// Group, a Plot a Scheme, the Adversary Deck the Villain Deck, and so on.
public sealed record RulesetTerms(
    string Scheme,
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
    string Twists)
{
    public static readonly RulesetTerms FirstEdition = new(
        "Scheme", "Hero", "Heroes", "Villain Group", "Villain Groups", "Henchman Group", "Henchman Groups",
        "Henchman", "Henchmen", "Villain Deck", "Hero Deck", "Twists");

    public static readonly RulesetTerms Villainous = new(
        "Plot", "Ally", "Allies", "Adversary Group", "Adversary Groups", "Backup Adversary group", "Backup Adversary groups",
        "Backup Adversary", "Backup Adversaries", "Adversary Deck", "Ally Deck", "Plot Twists");

    public static RulesetTerms For(Ruleset ruleset) => ruleset switch
    {
        Ruleset.FirstEdition => FirstEdition,
        Ruleset.Villainous => Villainous,
        _ => throw new ArgumentOutOfRangeException(nameof(ruleset), ruleset, null),
    };
}
