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

    // The opposition (#265). A check is met when a card of the hand has the team or class: each of the HandSize cards is
    // a Hero Deck card with probability HeroDeckShare (the starting deck dilutes the rest) and then has the team or class
    // with the Hero Deck's copy-weighted share, so chanceToShow = 1 - (1 - A * share)^H.
    public double HeroDeckShare { get; init; } = 0.6;
    public int HandSize { get; init; } = 6;

    // How much of a card's printed occurrences a game sees (it is rarely played to the last card of the Villain Deck),
    // and how much each time an effect fires counts: Ambush, Master Strikes and Twists always happen, a Fight or an
    // Escape only sometimes, since the players choose whom to fight and can leave a Villain.
    public double OccurrenceShare { get; init; } = 0.7;
    public double AmbushWeight { get; init; } = 1.0;
    public double FightWeight { get; init; } = 0.5;
    public double EscapeWeight { get; init; } = 0.5;
    public double StrikeWeight { get; init; } = 1.0;
    public double TwistWeight { get; init; } = 1.0;

    // What a Scheme's loss condition does to an axis it amplifies.
    public double AmplifyFactor { get; init; } = 1.5;

    // The opposition's total load (a sum of occurrences * severity) that is easy and hard, how much it counts toward the
    // raw score against the Hero Deck's terms, and the least load worth a reason.
    public double LoadEasy { get; init; } = 4;
    public double LoadHard { get; init; } = 24;
    public double OppositionWeight { get; init; } = 0.5;
    public double ReasonLoadMin { get; init; } = 0.75;
    public int MaxOppositionReasons { get; init; } = 3;
}

// A drawn Hero as the scorer sees it: its teams and its tagged cards, or none when its box has no difficulty file.
public sealed record ScoredHero(IReadOnlyList<string> Teams, IReadOnlyList<HeroCardDifficulty>? Cards);

// One tagged card or ability the setup holds. Subject is how a reason names it ("Magneto's Master Strikes",
// "Sabretooth"), Count how many of it the setup holds (Twists, Master Strikes, copies), Timings when it fires, and Box
// the box whose glossary names its team.
public sealed record OppositionPart(string Subject, string Box, OppositionTag Tag, IReadOnlyList<Timing> Timings, double Count);

// The drawn opposition as the scorer sees it: its tagged parts, the axes the Scheme amplifies, and the display names of
// glossary terms by id. A setup with none of its opposition tagged has no parts and rates on the Hero Deck alone.
public sealed record ScoredOpposition(
    IReadOnlyList<OppositionPart> Parts, IReadOnlyList<OppositionAxis> Amplified, IReadOnlyDictionary<string, string> TermNames);

// What the response shows: rating 1 (easiest) to 5, its band name and up to three reasons in plain words.
public sealed record Difficulty(int Rating, string Band, IReadOnlyList<string> Reasons);

// Rates how hard a drawn setup is to win: the Hero Deck's cost curve and team and class mix (#264), and, when the
// opposition is tagged, how often its checks, gates and inflicts land against that deck (#265). Every share is weighted
// by copies. Pure and deterministic. Heroes without difficulty data are left out, and a deck with none rates neutral.
public static class DifficultyScorer
{
    public static Difficulty Score(IReadOnlyList<ScoredHero> heroes, DifficultyTuning tuning, ScoredOpposition? opposition = null)
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
        var oppositionReasons = new List<string>();
        if (opposition is { Parts.Count: > 0 })
        {
            var contributions = Contribute(opposition, teams, classes, copies, tuning);
            var load = contributions.Sum(contribution => contribution.Load);
            raw = (1 - tuning.OppositionWeight) * raw + tuning.OppositionWeight * Scale(load, tuning.LoadEasy, tuning.LoadHard);
            oppositionReasons = contributions
                .Where(contribution => contribution.Load >= tuning.ReasonLoadMin)
                .OrderByDescending(contribution => contribution.Load)
                .Take(tuning.MaxOppositionReasons)
                .Select(contribution => contribution.Reason)
                .ToList();
        }

        var rating = 1 + tuning.BandStarts.Count(start => raw >= start);

        var reasons = measures
            .Where(measure => Math.Abs(measure.Hard - 0.5) >= tuning.ReasonMargin)
            .OrderByDescending(measure => Math.Abs(measure.Hard - 0.5) * measure.Weight)
            .Take(tuning.MaxReasons)
            .Select(measure => measure.Hard > 0.5 ? measure.HardReason : measure.EasyReason)
            .ToList();

        return new Difficulty(rating, tuning.BandNames[rating - 1], [.. oppositionReasons, .. reasons]);
    }

    private sealed record Contribution(double Load, string Reason);

    // The load each tag adds: occurrences * severity, less the chance the players can show a check's requirement or meet
    // a gate's, and worse on the axes the Scheme amplifies.
    private static List<Contribution> Contribute(
        ScoredOpposition opposition, Dictionary<string, double> teams, Dictionary<HeroClass, double> classes, int copies,
        DifficultyTuning tuning)
    {
        double Weight(Timing timing) => timing switch
        {
            Timing.Ambush => tuning.AmbushWeight,
            Timing.Fight => tuning.FightWeight,
            Timing.Escape => tuning.EscapeWeight,
            Timing.Strike => tuning.StrikeWeight,
            _ => tuning.TwistWeight,
        };

        double Amplify(OppositionAxis axis) => opposition.Amplified.Contains(axis) ? tuning.AmplifyFactor : 1;
        string Worse(OppositionAxis axis) => opposition.Amplified.Contains(axis) ? ", which this Scheme makes worse" : "";

        // How many Hero Deck cards have the team or class, and the chance one shows in a hand.
        (double Cards, double Chance, string Name) Show(OppositionPart part, Requirement requirement)
        {
            double cards;
            string name;
            if (requirement.Kind == RequirementKind.Team)
            {
                cards = teams.Where(team => TeamValue(team.Key) == requirement.Value).Sum(team => team.Value);
                name = opposition.TermNames.GetValueOrDefault($"{part.Box}_term_{requirement.Value}", requirement.Value);
            }
            else
            {
                var heroClass = Enum.Parse<HeroClass>(requirement.Value, ignoreCase: true);
                cards = classes.GetValueOrDefault(heroClass);
                name = heroClass.ToString();
            }

            var chance = 1 - Math.Pow(1 - tuning.HeroDeckShare * cards / copies, tuning.HandSize);
            return (cards, chance, name);
        }

        var contributions = new List<Contribution>();
        foreach (var part in opposition.Parts)
        {
            var tag = part.Tag;
            var occurrences = part.Count * tuning.OccurrenceShare * part.Timings.Sum(Weight);
            // A Master Strike or a Twist says when it fires in its name.
            var when = part.Timings.All(timing => timing is Timing.Strike or Timing.Twist)
                ? ""
                : " on " + string.Join(" and ", part.Timings.Select(timing => timing.ToString().ToLowerInvariant()));

            if (tag.Check is { } check)
            {
                var (cards, chance, name) = Show(part, check.Requires);
                var load = occurrences * tag.Severity!.Value * (1 - chance) * Amplify(check.OnFail);
                var outcome = chance < 0.6 ? "so it often fails" : "so it is usually met";
                contributions.Add(new Contribution(load,
                    $"{part.Subject}{when}: show {name} or suffer {AxisNames[check.OnFail]}{Worse(check.OnFail)}; {cards:0.#} of {copies} Hero cards are {name}, {outcome}."));
            }

            if (tag.Gate is { } gate)
            {
                var (cards, chance, name) = Show(part, gate);
                var load = part.Count * tuning.OccurrenceShare * tuning.FightWeight * tag.Severity!.Value * (1 - chance);
                contributions.Add(new Contribution(load,
                    $"{part.Subject} can't be defeated without {name}; {cards:0.#} of {copies} Hero cards are {name}."));
            }

            foreach (var inflict in tag.Inflicts ?? [])
            {
                contributions.Add(new Contribution(
                    occurrences * inflict.Severity * Amplify(inflict.Axis),
                    $"{part.Subject}{when}: {AxisNames[inflict.Axis]}{Worse(inflict.Axis)}."));
            }
        }

        return contributions;
    }

    // "core_term_x-men" and "x-men" are both the team x-men.
    private static string TeamValue(string team) =>
        team.IndexOf("_term_", StringComparison.Ordinal) is var at and >= 0 ? team[(at + "_term_".Length)..] : team;

    private static readonly Dictionary<OppositionAxis, string> AxisNames = new()
    {
        [OppositionAxis.Wounds] = "Wounds",
        [OppositionAxis.BystanderPressure] = "Bystanders carried off",
        [OppositionAxis.HqDisruption] = "HQ disruption",
        [OppositionAxis.HandDisruption] = "hand disruption",
        [OppositionAxis.AttackInflation] = "Attack inflation",
        [OppositionAxis.EscapePressure] = "Villains pushed toward escape",
        [OppositionAxis.DeckThinning] = "Heroes KO'd",
        [OppositionAxis.TwistDensity] = "extra Scheme Twists",
    };

    // Maps value onto 0 at easy and 1 at hard, clamped; easy may be above hard.
    private static double Scale(double value, double easy, double hard) =>
        Math.Clamp((value - easy) / (hard - easy), 0, 1);
}
