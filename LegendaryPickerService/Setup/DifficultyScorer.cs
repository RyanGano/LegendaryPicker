using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Every constant the scorer uses (#264). Each *Easy and *Hard pair maps a measure to 0 (easy) .. 1 (hard), clamped.
// These start as guesses to be set by the calibration slice (#268), not swept: change the numbers here and nowhere else.
// BandNames are display strings only; the rating 1..5 is what the code uses.
public sealed record DifficultyTuning
{
    public static DifficultyTuning Default { get; } = new();

    // The copy-weighted mean cost of the Hero Deck, and the share of its cards costing HighCost or more.
    public double MeanCostEasy { get; init; } = 3.2;
    public double MeanCostHard { get; init; } = 4.2;
    public int HighCost { get; init; } = 6;
    public double HighCostShareEasy { get; init; } = 0.05;
    public double HighCostShareHard { get; init; } = 0.20;

    // The share of the deck in its largest team and its largest class: a bigger share means more team and class
    // bonuses fire, so it is easier.
    public double TeamShareHard { get; init; } = 0.20;
    public double TeamShareEasy { get; init; } = 0.50;
    public double ClassShareHard { get; init; } = 0.25;
    public double ClassShareEasy { get; init; } = 0.45;

    // How many distinct classes the deck has: this many or fewer is easy, all five is hard.
    public int ClassesEasy { get; init; } = 3;
    public int ClassesHard { get; init; } = 5;

    // How much each measure counts toward the raw score; they sum to 1.
    public double MeanCostWeight { get; init; } = 0.35;
    public double HighCostWeight { get; init; } = 0.25;
    public double TeamWeight { get; init; } = 0.15;
    public double ClassShareWeight { get; init; } = 0.10;
    public double ClassCountWeight { get; init; } = 0.15;

    // The raw score (0..1) at which each band above the first starts: five equal-width bands.
    public IReadOnlyList<double> BandStarts { get; init; } = [0.2, 0.4, 0.6, 0.8];

    // Band names by rating, 1 to 5.
    public IReadOnlyList<string> BandNames { get; init; } = ["Sidekick", "Street-Level", "Heavy Hitter", "Cosmic Threat", "Secret Wars"];

    // A measure this far toward easy or hard (0..1 around the 0.5 midpoint) is worth a reason.
    public double ReasonMargin { get; init; } = 0.15;

    public int MaxReasons { get; init; } = 3;

    // The rating when there is nothing to measure.
    public int NeutralRating { get; init; } = 3;
}

// A drawn Hero as the scorer sees it: its teams and its tagged cards, or none when its box has no difficulty file.
public sealed record ScoredHero(IReadOnlyList<string> Teams, IReadOnlyList<HeroCardDifficulty>? Cards);

// What the response shows: rating 1 (easiest) to 5, its band name and up to three reasons in plain words.
public sealed record Difficulty(int Rating, string Band, IReadOnlyList<string> Reasons);

// Rates how hard a drawn Hero Deck is to win with, from the Hero Deck alone (#264): its cost curve and its team
// and class mix, every share weighted by copies. Pure and deterministic. Heroes without difficulty data are left
// out, and a deck with none rates neutral.
public static class DifficultyScorer
{
    public static Difficulty Score(IReadOnlyList<ScoredHero> heroes, DifficultyTuning tuning)
    {
        var tagged = heroes.Where(hero => hero.Cards is { Count: > 0 }).ToList();
        var copies = tagged.Sum(hero => hero.Cards!.Sum(card => card.Copies));
        if (copies == 0)
        {
            return new Difficulty(tuning.NeutralRating, tuning.BandNames[tuning.NeutralRating - 1],
                ["No card data for these Heroes yet, so the rating is average."]);
        }

        var cards = tagged.SelectMany(hero => hero.Cards!).ToList();
        var meanCost = cards.Sum(card => card.Cost * card.Copies) / (double)copies;
        var highShare = cards.Where(card => card.Cost >= tuning.HighCost).Sum(card => card.Copies) / (double)copies;

        // A card with several teams or classes counts for each, its copies split between them.
        var teams = new Dictionary<string, double>(StringComparer.Ordinal);
        foreach (var hero in tagged.Where(hero => hero.Teams.Count > 0))
        {
            var each = hero.Cards!.Sum(card => card.Copies) / (double)hero.Teams.Count;
            foreach (var team in hero.Teams)
            {
                teams[team] = teams.GetValueOrDefault(team) + each;
            }
        }

        var classes = new Dictionary<HeroClass, double>();
        foreach (var card in cards)
        {
            foreach (var heroClass in card.Class)
            {
                classes[heroClass] = classes.GetValueOrDefault(heroClass) + card.Copies / (double)card.Class.Count;
            }
        }

        var topTeam = teams.Count == 0 ? 0 : teams.Values.Max() / copies;
        var topClass = classes.Values.Max() / copies;

        var measures = new (double Hard, double Weight, string HardReason, string EasyReason)[]
        {
            (Scale(meanCost, tuning.MeanCostEasy, tuning.MeanCostHard), tuning.MeanCostWeight,
                "The Hero cards cost a lot on average.", "The Hero cards are cheap on average."),
            (Scale(highShare, tuning.HighCostShareEasy, tuning.HighCostShareHard), tuning.HighCostWeight,
                $"Many Hero cards cost {tuning.HighCost} or more.", $"Few Hero cards cost {tuning.HighCost} or more."),
            (Scale(topTeam, tuning.TeamShareEasy, tuning.TeamShareHard), tuning.TeamWeight,
                "The Heroes are spread across teams, so team bonuses are rare.", "Heroes share teams, so team bonuses come up often."),
            (Scale(topClass, tuning.ClassShareEasy, tuning.ClassShareHard), tuning.ClassShareWeight,
                "No one class dominates, so class bonuses are rare.", "One class dominates, so class bonuses come up often."),
            (Scale(classes.Count, tuning.ClassesEasy, tuning.ClassesHard), tuning.ClassCountWeight,
                "All five classes are in the deck, which spreads the synergies thin.", "Few classes are in the deck, which concentrates the synergies."),
        };

        var raw = measures.Sum(measure => measure.Hard * measure.Weight) / measures.Sum(measure => measure.Weight);
        var rating = 1 + tuning.BandStarts.Count(start => raw >= start);

        var reasons = measures
            .Where(measure => Math.Abs(measure.Hard - 0.5) >= tuning.ReasonMargin)
            .OrderByDescending(measure => Math.Abs(measure.Hard - 0.5) * measure.Weight)
            .Take(tuning.MaxReasons)
            .Select(measure => measure.Hard > 0.5 ? measure.HardReason : measure.EasyReason)
            .ToList();

        return new Difficulty(rating, tuning.BandNames[rating - 1], reasons);
    }

    // Maps value onto 0 at easy and 1 at hard, clamped; easy may be above hard.
    private static double Scale(double value, double easy, double hard) =>
        Math.Clamp((value - easy) / (hard - easy), 0, 1);
}
