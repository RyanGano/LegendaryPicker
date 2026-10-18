using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Every constant the scorer uses (#264). Each *Easy and *Hard pair maps a measure to 0 (easy) .. 1 (hard), clamped.
// Calibration (#268) set them against the reference setups in DifficultyCalibrationTests, and #281 re-fitted the band
// starts once every box had data; change the numbers here and nowhere else.
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

    // The raw score (0..1) at which each band above the first starts. Fitted to the core reference setups (#268) and
    // re-fitted with every box's data in (#281), so they are not equal widths.
    public IReadOnlyList<double> BandStarts { get; init; } = [0.29, 0.39, 0.58, 0.74];

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
    public double LoadEasy { get; init; } = 15;
    public double LoadHard { get; init; } = 55;
    public double OppositionWeight { get; init; } = 0.4;
    public double ReasonLoadMin { get; init; } = 0.75;
    public int MaxOppositionReasons { get; init; } = 3;

    // The Hero Deck's engine (#266). A want is met by the share of the other drawn cards that have its team or class
    // (min(1, share / target)); diversity counts a class as present at DiversityClassShare of the other cards and is met
    // at DiversityTarget classes; a stack want is met by the opposition's load on that stack's axis, StackWantLoad being
    // enough. A card's unmet strength per card of the deck maps to the raw score between the Easy and Hard pair.
    public double TeamWantTarget { get; init; } = 0.25;
    public double ClassWantTarget { get; init; } = 0.25;
    public double DiversityClassShare { get; init; } = 0.10;
    public double DiversityTarget { get; init; } = 4;
    public double StackWantLoad { get; init; } = 3;
    public double EngineUnmetEasy { get; init; } = 0.1;
    public double EngineUnmetHard { get; init; } = 0.6;
    public double EngineWeight { get; init; } = 0.25;

    // Extra cards drawn outright, per card of the deck: more is easier.
    public double DrawMeanEasy { get; init; } = 0.5;
    public double DrawMeanHard { get; init; } = 0.1;
    public double DrawWeight { get; init; } = 0.10;

    // What the Hero Deck does about the opposition's pressure: its copy-weighted share of answers (strength / 3 per card)
    // on an axis cuts that axis's load by up to MaxMitigation, reached at AnswerTarget. A card's self cost adds
    // SelfCostPerCopy to the opposition load per copy.
    public double AnswerTarget { get; init; } = 0.15;
    public double MaxMitigation { get; init; } = 0.5;
    public double SelfCostPerCopy { get; init; } = 0.1;

    // The least unmet want (strength * copies), saved load or self-cost load worth a reason, and how many such Hero-side
    // reasons there are.
    public double ReasonWantMin { get; init; } = 4;
    public int MaxHeroSideReasons { get; init; } = 2;
}

// A drawn Hero as the scorer sees it: its teams and its tagged cards, or none when its box has no difficulty file.
// Weight counts the Hero's cards that many times: 1 for a drawn Hero, and a fraction for one of the pool's Heroes standing
// in for the slots a draw has not filled yet (#267).
public sealed record ScoredHero(IReadOnlyList<string> Teams, IReadOnlyList<HeroCardDifficulty>? Cards, double Weight = 1);

// One tagged card or ability the setup holds. Subject is how a reason names it ("Magneto's Master Strikes",
// "Sabretooth"), Count how many of it the setup holds (Twists, Master Strikes, copies), Timings when it fires, and Box
// the box whose glossary names its team.
public sealed record OppositionPart(string Subject, string Box, OppositionTag Tag, IReadOnlyList<Timing> Timings, double Count);

// The drawn opposition as the scorer sees it: its tagged parts, the axes the Scheme amplifies, and the display names of
// glossary terms by id. A setup with none of its opposition tagged has no parts and rates on the Hero Deck alone.
public sealed record ScoredOpposition(
    IReadOnlyList<OppositionPart> Parts, IReadOnlyList<OppositionAxis> Amplified, IReadOnlyDictionary<string, string> TermNames);

// What the response shows: rating 1 (easiest) to 5, its band name and up to three reasons in plain words. Target is the
// difficulty the request asked for (#267), absent when it asked for none; the rating is what the draw really has. With a
// target, Reached says whether the rating is the target, and Note, written only when it isn't, names the closest band the
// draw found (#329).
public sealed record Difficulty(int Rating, string Band, IReadOnlyList<string> Reasons,
    [property: System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)] int? Target = null,
    [property: System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)] bool? Reached = null,
    [property: System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)] string? Note = null);

// Rates how hard a drawn setup is to win: the Hero Deck's cost curve and team and class mix (#264), and, when the
// opposition is tagged, how often its checks, gates and inflicts land against that deck (#265). Every share is weighted
// by copies. Pure and deterministic. Heroes without difficulty data are left out, and a deck with none rates neutral.
public static class DifficultyScorer
{
    public static Difficulty Score(IReadOnlyList<ScoredHero> heroes, DifficultyTuning tuning, ScoredOpposition? opposition = null)
    {
        if (Evaluate(heroes, tuning, opposition) is not { } evaluation)
        {
            return new Difficulty(tuning.NeutralRating, tuning.BandNames[tuning.NeutralRating - 1],
                ["No card data for these Heroes yet, so the rating is average."]);
        }

        var rating = 1 + tuning.BandStarts.Count(start => evaluation.Raw >= start);
        return new Difficulty(rating, tuning.BandNames[rating - 1], evaluation.Reasons);
    }

    // The rating a setup is expected to have on a continuous 1..5 scale, so a draw can tell how near a choice is to a
    // target (#267). A band runs from n - 0.5 at its start to n + 0.5 at its end, so it rounds to the rating Score gives
    // and follows BandStarts when the bands are not equal widths. Neutral when there is nothing to measure.
    public static double Expected(IReadOnlyList<ScoredHero> heroes, DifficultyTuning tuning, ScoredOpposition? opposition = null) =>
        Measure(heroes, tuning, opposition).Expected;

    // The rating Score gives and the continuous rating Expected gives, from one evaluation, so the generator can judge a
    // choice by the band the response will show and by how far it is from a target (#329).
    public static (int Rating, double Expected) Measure(IReadOnlyList<ScoredHero> heroes, DifficultyTuning tuning, ScoredOpposition? opposition = null)
    {
        if (Evaluate(heroes, tuning, opposition) is not { } evaluation)
        {
            return (tuning.NeutralRating, tuning.NeutralRating);
        }

        var edges = new[] { 0.0 }.Concat(tuning.BandStarts).Append(1.0).ToArray();
        var started = tuning.BandStarts.Count(start => evaluation.Raw >= start);
        var band = Math.Min(started, edges.Length - 2);
        var along = (evaluation.Raw - edges[band]) / (edges[band + 1] - edges[band]);
        return (1 + started, Math.Clamp(band + 0.5 + along, 1, edges.Length - 1));
    }

    private sealed record Evaluation(double Raw, IReadOnlyList<string> Reasons);

    private sealed record Candidate(double Value, string Reason);

    private static Evaluation? Evaluate(IReadOnlyList<ScoredHero> heroes, DifficultyTuning tuning, ScoredOpposition? opposition)
    {
        var tagged = heroes.Where(hero => hero.Cards is { Count: > 0 } && hero.Weight > 0).ToList();
        var cards = tagged.SelectMany(hero => hero.Cards!.Select(card => (card.Cost, card.Class, Copies: card.Copies * hero.Weight))).ToList();
        var copies = cards.Sum(card => card.Copies);
        if (copies == 0)
        {
            return null;
        }

        var meanCost = cards.Sum(card => card.Cost * card.Copies) / copies;
        var highShare = cards.Where(card => card.Cost >= tuning.HighCost).Sum(card => card.Copies) / copies;

        // A card with several teams or classes counts for each, its copies split between them.
        var teams = new Dictionary<string, double>(StringComparer.Ordinal);
        foreach (var hero in tagged.Where(hero => hero.Teams.Count > 0))
        {
            var each = hero.Cards!.Sum(card => card.Copies) * hero.Weight / hero.Teams.Count;
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
                classes[heroClass] = classes.GetValueOrDefault(heroClass) + card.Copies / card.Class.Count;
            }
        }

        var topTeam = teams.Count == 0 ? 0 : teams.Values.Max() / copies;
        var topClass = classes.Values.Max() / copies;

        var measures = new List<(double Hard, double Weight, string HardReason, string EasyReason)>
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

        // What the Hero Deck does about the opposition: each card's answers cut the load on the axes they answer.
        var answerStrength = new Dictionary<AnswerAxis, double>();
        var answerCards = new Dictionary<AnswerAxis, double>();
        foreach (var hero in tagged)
        {
            foreach (var card in hero.Cards!.Where(card => card.Answers is not null))
            {
                foreach (var answer in card.Answers!)
                {
                    answerStrength[answer.Axis] = answerStrength.GetValueOrDefault(answer.Axis) + card.Copies * hero.Weight * answer.Strength / 3.0;
                    answerCards[answer.Axis] = answerCards.GetValueOrDefault(answer.Axis) + card.Copies * hero.Weight;
                }
            }
        }

        double Mitigation(OppositionAxis axis) =>
            tuning.MaxMitigation * Math.Min(1, AnswersFor(axis).Sum(answer => answerStrength.GetValueOrDefault(answer)) / copies / tuning.AnswerTarget);

        var contributions = opposition is { Parts.Count: > 0 } ? Contribute(opposition, teams, classes, copies, tuning, Mitigation) : [];
        var heroSide = new List<Candidate>();

        // The engine: how well each card's wants are met by the other drawn cards (a Hero's own cards never meet its wants).
        var axisLoad = new Dictionary<OppositionAxis, double>();
        foreach (var contribution in contributions.Where(contribution => contribution.Axis is not null))
        {
            axisLoad[contribution.Axis!.Value] = axisLoad.GetValueOrDefault(contribution.Axis.Value) + contribution.Load + contribution.Saved;
        }

        // Per want: the unmet total, how many Heroes want it, and what the least-met wanter finds among the other Heroes
        // (other Heroes of the team, other cards of the class, or other classes present), which its reason names.
        var unmetByWant = new Dictionary<(WantKind Kind, string Value), (double Unmet, int Wanters, double Satisfaction, double Found)>();
        var unmetTotal = 0.0;
        var anyWants = false;
        foreach (var hero in tagged)
        {
            var own = hero.Cards!.Sum(card => card.Copies) * hero.Weight;
            var others = copies - own;
            var ownTeam = hero.Teams.Count == 0 ? 0 : own / hero.Teams.Count;
            double OtherClass(HeroClass heroClass) =>
                classes.GetValueOrDefault(heroClass) - hero.Cards!.Where(card => card.Class.Contains(heroClass)).Sum(card => card.Copies * hero.Weight / card.Class.Count);

            double Share(double cards) => others <= 0 ? 0 : Math.Clamp(cards / others, 0, 1);
            (double Satisfaction, double Found) Satisfaction(HeroWant want)
            {
                switch (want.Kind)
                {
                    case WantKind.Team:
                        var onTeam = hero.Teams.Any(team => TeamValue(team) == want.Value);
                        var teamCards = teams.Where(team => TeamValue(team.Key) == want.Value).Sum(team => team.Value) - (onTeam ? ownTeam : 0);
                        var teamHeroes = tagged.Where(other => other.Teams.Any(team => TeamValue(team) == want.Value)).Sum(other => other.Weight)
                            - (onTeam ? hero.Weight : 0);
                        return (Math.Min(1, Share(teamCards) / tuning.TeamWantTarget), teamHeroes);
                    case WantKind.Class:
                        var classCards = OtherClass(Enum.Parse<HeroClass>(want.Value, ignoreCase: true));
                        return (Math.Min(1, Share(classCards) / tuning.ClassWantTarget), classCards);
                    case WantKind.Diversity:
                        var present = Enum.GetValues<HeroClass>().Sum(heroClass => Math.Min(1, Share(OtherClass(heroClass)) / tuning.DiversityClassShare));
                        return (Math.Min(1, present / tuning.DiversityTarget), Math.Floor(present));
                    case WantKind.Stack when opposition is { Parts.Count: > 0 } && StackAxis(want.Value) is { } axis:
                        return (Math.Min(1, axisLoad.GetValueOrDefault(axis) / tuning.StackWantLoad), 0);
                    default:
                        // A keyword, a board state, another stack or a draw is not scored yet; it counts as met.
                        return (1, 0);
                }
            }

            foreach (var card in hero.Cards!.Where(card => card.Wants is not null))
            {
                foreach (var want in card.Wants!)
                {
                    anyWants = true;
                    var (satisfaction, found) = Satisfaction(want);
                    var unmet = card.Copies * hero.Weight * want.Strength * (1 - satisfaction);
                    unmetTotal += unmet;
                    var key = (want.Kind, want.Value);
                    unmetByWant[key] = unmetByWant.TryGetValue(key, out var so)
                        ? (so.Unmet + unmet, so.Wanters, Math.Min(so.Satisfaction, satisfaction), satisfaction < so.Satisfaction ? found : so.Found)
                        : (unmet, 0, satisfaction, found);
                }
            }

            foreach (var key in hero.Cards!.SelectMany(card => card.Wants ?? []).Select(want => (want.Kind, want.Value)).Distinct())
            {
                unmetByWant[key] = unmetByWant[key] with { Wanters = unmetByWant[key].Wanters + 1 };
            }
        }

        foreach (var ((kind, value), (unmet, wanters, _, found)) in unmetByWant.Where(want => want.Value.Unmet >= tuning.ReasonWantMin))
        {
            var who = wanters == 1 ? "1 Hero wants" : $"{wanters} Heroes want";
            string? reason = kind switch
            {
                WantKind.Team => TeamReason(who, TeamName(opposition?.TermNames, value), (int)Math.Round(found)),
                WantKind.Class => $"{who} {Capitalize(value)} cards from the other Heroes, but they have {(found < 0.05 ? "none" : $"only {found:0.#}")}.",
                WantKind.Diversity => $"{who} a mix of classes from the other Heroes, but they bring only {found:0} of the five.",
                WantKind.Stack => $"{who} {Capitalize(value)} in play, but this opposition inflicts few.",
                _ => null,
            };
            if (reason is not null)
            {
                heroSide.Add(new Candidate(unmet, reason));
            }
        }

        foreach (var group in contributions.Where(contribution => contribution.Axis is not null && contribution.Saved > 0).GroupBy(contribution => contribution.Axis!.Value))
        {
            var saved = group.Sum(contribution => contribution.Saved);
            var answering = AnswersFor(group.Key).Sum(answer => answerCards.GetValueOrDefault(answer));
            if (saved >= tuning.ReasonLoadMin)
            {
                heroSide.Add(new Candidate(saved,
                    $"{answering:0.#} of {copies:0.#} Hero cards answer {AxisNames[group.Key]}, which cuts it by about {Mitigation(group.Key):0%}."));
            }
        }

        var selfLoad = tagged.Sum(hero => hero.Cards!.Sum(card => card.Copies * (card.SelfCost?.Count ?? 0)) * hero.Weight) * tuning.SelfCostPerCopy;
        var selfCards = tagged.Sum(hero => hero.Cards!.Where(card => card.SelfCost is not null).Sum(card => card.Copies) * hero.Weight);
        if (contributions.Count > 0 && selfLoad >= tuning.ReasonLoadMin)
        {
            heroSide.Add(new Candidate(selfLoad, $"{selfCards:0.#} Hero cards cost you a discard, a Wound or a KO to play."));
        }

        var anyDraw = tagged.Any(hero => hero.Cards!.Any(card => card.Draw > 0));
        if (anyWants)
        {
            measures.Add((Scale(unmetTotal / copies, tuning.EngineUnmetEasy, tuning.EngineUnmetHard), tuning.EngineWeight,
                "", ""));
        }

        if (anyDraw)
        {
            var drawMean = tagged.Sum(hero => hero.Cards!.Sum(card => card.Copies * (card.Draw ?? 0)) * hero.Weight) / copies;
            measures.Add((Scale(drawMean, tuning.DrawMeanEasy, tuning.DrawMeanHard), tuning.DrawWeight,
                "Few Hero cards draw extra cards.", "Many Hero cards draw extra cards."));
        }

        var raw = measures.Sum(measure => measure.Hard * measure.Weight) / measures.Sum(measure => measure.Weight);
        var oppositionReasons = new List<string>();
        if (contributions.Count > 0)
        {
            var load = contributions.Sum(contribution => contribution.Load) + selfLoad;
            raw = (1 - tuning.OppositionWeight) * raw + tuning.OppositionWeight * Scale(load, tuning.LoadEasy, tuning.LoadHard);
            oppositionReasons = contributions
                .Where(contribution => contribution.Load >= tuning.ReasonLoadMin)
                .OrderByDescending(contribution => contribution.Load)
                .Take(tuning.MaxOppositionReasons)
                .Select(contribution => contribution.Reason)
                .ToList();
        }

        var heroSideReasons = heroSide
            .OrderByDescending(candidate => candidate.Value)
            .Take(tuning.MaxHeroSideReasons)
            .Select(candidate => candidate.Reason)
            .ToList();

        // The engine measure has no reason of its own: the unmet wants name themselves above.
        var reasons = measures
            .Where(measure => measure.HardReason.Length > 0 && Math.Abs(measure.Hard - 0.5) >= tuning.ReasonMargin)
            .OrderByDescending(measure => Math.Abs(measure.Hard - 0.5) * measure.Weight)
            .Take(Math.Max(tuning.MaxReasons - heroSideReasons.Count, 1))
            .Select(measure => measure.Hard > 0.5 ? measure.HardReason : measure.EasyReason)
            .ToList();

        return new Evaluation(raw, [.. oppositionReasons, .. heroSideReasons, .. reasons]);
    }

    // Saved is the load the Hero Deck's answers took off an Axis, so a reason can say how much it cut.
    private sealed record Contribution(double Load, string Reason, OppositionAxis? Axis = null, double Saved = 0);

    // The load each tag adds: occurrences * severity, less the chance the players can show a check's requirement or meet
    // a gate's, and worse on the axes the Scheme amplifies.
    private static List<Contribution> Contribute(
        ScoredOpposition opposition, Dictionary<string, double> teams, Dictionary<HeroClass, double> classes, double copies,
        DifficultyTuning tuning, Func<OppositionAxis, double> mitigation)
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
                name = opposition.TermNames.GetValueOrDefault($"{part.Box}_term_{requirement.Value}")
                    ?? TeamName(opposition.TermNames, requirement.Value);
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
                var unanswered = occurrences * tag.Severity!.Value * (1 - chance) * Amplify(check.OnFail);
                var load = unanswered * (1 - mitigation(check.OnFail));
                var outcome = chance < 0.6 ? "so it often fails" : "so it is usually met";
                contributions.Add(new Contribution(load,
                    $"{part.Subject}{when}: show {name} or suffer {AxisNames[check.OnFail]}{Worse(check.OnFail)}; {cards:0.#} of {copies} Hero cards are {name}, {outcome}.", check.OnFail, unanswered - load));
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
                var unanswered = occurrences * inflict.Severity * Amplify(inflict.Axis);
                var load = unanswered * (1 - mitigation(inflict.Axis));
                contributions.Add(new Contribution(
                    load, $"{part.Subject}{when}: {AxisNames[inflict.Axis]}{Worse(inflict.Axis)}.", inflict.Axis, unanswered - load));
            }
        }

        return contributions;
    }

    // The Hero answers that cut an opposition axis. Deck thinning and extra Twists have none.
    private static IEnumerable<AnswerAxis> AnswersFor(OppositionAxis axis) => axis switch
    {
        OppositionAxis.Wounds => [AnswerAxis.Wounds],
        OppositionAxis.BystanderPressure => [AnswerAxis.Bystanders],
        OppositionAxis.HqDisruption => [AnswerAxis.Hq],
        OppositionAxis.HandDisruption => [AnswerAxis.Hand],
        OppositionAxis.AttackInflation => [AnswerAxis.AttackInflation],
        OppositionAxis.EscapePressure => [AnswerAxis.Escape, AnswerAxis.VillainFlow],
        _ => [],
    };

    // The axis an opposition card loads when a Hero wants that stack in play; the other stacks are not scored (Bystanders can always be rescued).
    private static OppositionAxis? StackAxis(string stack) => stack switch
    {
        "wounds" => OppositionAxis.Wounds,
        _ => null,
    };

    private static string TeamReason(string who, string team, int others) => others switch
    {
        0 => $"{who} {team} teammates, but no other {team} Hero is drawn.",
        1 => $"{who} {team} teammates, but only 1 other {team} Hero is drawn.",
        _ => $"{who} {team} teammates, but only {others} other {team} Heroes are drawn.",
    };

    private static string Capitalize(string value) => char.ToUpperInvariant(value[0]) + value[1..];

    // A team's display name from any box's term of that value ("x-men"), or the value itself when no name is known.
    private static string TeamName(IReadOnlyDictionary<string, string>? names, string value) =>
        names?.FirstOrDefault(name => name.Key.EndsWith($"_term_{value}", StringComparison.Ordinal)).Value ?? value;

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
