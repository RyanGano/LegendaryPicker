using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Every constant the complexity rating uses (#270). Complexity is how much there is to learn and remember, not how likely
// the players are to lose, so it reads the drawn cards' keywords, the stacks they need and the rules that are mixed, and
// never the difficulty tags. Each measure earns rule-load points, the points map to 0..1 between PointsEasy and
// PointsHard, and the raw score falls into five equal bands. These are starting guesses; change the numbers here only.
public sealed record ComplexityTuning
{
    public static ComplexityTuning Default { get; } = new();

    // Points per distinct keyword term in play (teams and classes are not keywords), per stack beyond the baseline,
    // per ruleset beyond the first, per stand-in, per box beyond the first that supplies a drawn card, and per setup step
    // the Scheme or Mastermind adds. The box points are small on purpose: a rating that mostly counted boxes would say
    // nothing about the rules.
    public double KeywordPoints { get; init; } = 1.0;
    public double StackPoints { get; init; } = 2.0;
    public double MixedRulesetPoints { get; init; } = 3.0;
    public double StandInPoints { get; init; } = 1.5;
    public double BoxPoints { get; init; } = 0.5;
    public double StepPoints { get; init; } = 0.75;

    // Stacks every setup already knows from the base set; they add no points.
    public IReadOnlyList<Part> BaselineStacks { get; init; } = [Part.Wounds, Part.Officers];

    // Rule-load points at or below which the setup is as simple as it gets, and at or above which it is as complex.
    public double PointsEasy { get; init; } = 4;
    public double PointsHard { get; init; } = 20;

    // The raw score (0..1) at which each band above the first starts: five equal-width bands.
    public IReadOnlyList<double> BandStarts { get; init; } = [0.2, 0.4, 0.6, 0.8];

    // Band names by rating, 1 to 5; display strings only, renameable here.
    public IReadOnlyList<string> BandNames { get; init; } = ["Origin Story", "Team-Up", "Crossover", "Multiverse", "Event Series"];

    public int MaxReasons { get; init; } = 3;

    // How many keyword names a reason spells out before it counts the rest.
    public int NamedKeywords { get; init; } = 3;
}

// What the response shows: rating 1 (simplest) to 5, its band name and up to three reasons in plain words. Target is the
// complexity the request asked for, absent when it asked for none; the rating is what the draw really has.
public sealed record Complexity(int Rating, string Band, IReadOnlyList<string> Reasons,
    [property: System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)] int? Target = null);

// What the rating reads from a drawn setup: the keywords in play, the stacks beyond the baseline, how many rulesets are
// mixed, the stand-ins, the boxes that supply drawn cards, and the setup steps the Scheme and Mastermind add.
public sealed record ComplexityInputs(
    IReadOnlySet<string> Keywords, IReadOnlySet<Part> Stacks, int Rulesets, int StandIns, int Boxes, int Steps);

// Reads the glossary once and rates drawn cards (#270). Pure and deterministic.
public sealed class ComplexityMeter
{
    private readonly ComplexityTuning _tuning;
    private readonly Dictionary<string, GlossaryTerm> _terms;
    private readonly Dictionary<string, Ruleset> _rulesets;

    public ComplexityMeter(BoxCatalog catalog, ComplexityTuning? tuning = null)
    {
        _tuning = tuning ?? ComplexityTuning.Default;
        // A base game's own keywords are the game being learned, so only an expansion's keywords add rules to know.
        _terms = catalog.Boxes.Where(box => !box.IsBaseGame).SelectMany(box => box.Glossary).GroupBy(term => term.Id).ToDictionary(group => group.Key, group => group.First());
        _rulesets = catalog.Boxes.ToDictionary(box => box.Id, box => box.Ruleset, StringComparer.Ordinal);
    }

    // The inputs of the cards drawn so far. stacks are the stacks the finished setup lays out, when known, and standIns
    // the parts it replaces with a stand-in; the cards' own uses are always counted, except a part a stand-in replaces,
    // which the setup never lays out and which the stand-in already counts for.
    public ComplexityInputs Inputs(
        IEnumerable<ICard> cards, IEnumerable<Part>? stacks = null, IReadOnlyCollection<StandIn>? standIns = null)
    {
        var list = cards.ToList();
        var replaced = (standIns ?? []).Select(standIn => standIn.Part).ToHashSet();
        var keywords = list.SelectMany(card => card.Terms).Where(IsKeyword).ToHashSet(StringComparer.Ordinal);
        var parts = list.SelectMany(card => card is Scheme scheme ? scheme.Parts : ((ICard)card).Parts).Concat(stacks ?? [])
            .Where(part => part != Part.Ambitions && !_tuning.BaselineStacks.Contains(part) && !replaced.Contains(part)).ToHashSet();
        var boxes = list.Select(card => BoxOf(card.Id)).Distinct(StringComparer.Ordinal).ToList();
        var rulesets = boxes.Where(_rulesets.ContainsKey).Select(box => _rulesets[box]).Distinct().Count();
        var steps = list.Sum(card => card switch
        {
            Scheme scheme => scheme.Setup.Steps?.Count ?? 0,
            Mastermind mastermind => mastermind.Setup?.Steps?.Count ?? 0,
            _ => 0,
        });
        return new ComplexityInputs(keywords, parts, rulesets, standIns?.Count ?? 0, boxes.Count, steps);
    }

    private bool IsKeyword(string term) => _terms.TryGetValue(term, out var known) && known.Kind == TermKind.Keyword;

    private static string BoxOf(string id) => id.Split('_')[0];

    public double Points(ComplexityInputs inputs) =>
        inputs.Keywords.Count * _tuning.KeywordPoints
        + inputs.Stacks.Count * _tuning.StackPoints
        + Math.Max(inputs.Rulesets - 1, 0) * _tuning.MixedRulesetPoints
        + inputs.StandIns * _tuning.StandInPoints
        + Math.Max(inputs.Boxes - 1, 0) * _tuning.BoxPoints
        + inputs.Steps * _tuning.StepPoints;

    // The extra points one more Hero would add to a draw so far, on average over the pool: only the keywords and stacks
    // the draw does not have yet count.
    public double MeanHeroPoints(ComplexityInputs drawn, IReadOnlyList<Hero> pool) =>
        pool.Count == 0
            ? 0
            : pool.Average(hero =>
                hero.Terms.Where(term => IsKeyword(term) && !drawn.Keywords.Contains(term)).Distinct().Count() * _tuning.KeywordPoints
                + ((ICard)hero).Parts.Where(part => part != Part.Ambitions && !_tuning.BaselineStacks.Contains(part) && !drawn.Stacks.Contains(part)).Distinct().Count() * _tuning.StackPoints);

    public double Raw(double points) => Math.Clamp((points - _tuning.PointsEasy) / (_tuning.PointsHard - _tuning.PointsEasy), 0, 1);

    // A continuous rating for weighting a draw that rounds to the band Score gives: it runs from 1 at no points to 1.5
    // where band 2 starts, crosses k + 0.5 where band k + 1 starts, and reaches 5 at PointsHard. It keeps rising inside
    // the first band, so two options that both rate 1 still differ.
    public double Expected(double points)
    {
        var knots = new List<(double Points, double Rating)> { (0, 1) };
        knots.AddRange(_tuning.BandStarts.Select((start, i) =>
            (_tuning.PointsEasy + start * (_tuning.PointsHard - _tuning.PointsEasy), i + 1.5)));
        knots.Add((_tuning.PointsHard, 5));
        for (var i = 1; i < knots.Count; i++)
        {
            if (points <= knots[i].Points)
            {
                var (fromPoints, fromRating) = knots[i - 1];
                var (toPoints, toRating) = knots[i];
                return fromRating + (Math.Max(points, fromPoints) - fromPoints) / (toPoints - fromPoints) * (toRating - fromRating);
            }
        }

        return 5;
    }

    public Complexity Score(ComplexityInputs inputs)
    {
        var raw = Raw(Points(inputs));
        var rating = 1 + _tuning.BandStarts.Count(start => raw >= start);
        return new Complexity(rating, _tuning.BandNames[rating - 1], Reasons(inputs));
    }

    // The biggest contributors first, in plain words; a setup of the base set alone has none worth naming.
    private IReadOnlyList<string> Reasons(ComplexityInputs inputs)
    {
        var reasons = new List<(double Points, string Text)>();
        if (inputs.Stacks.Count > 0)
        {
            reasons.Add((inputs.Stacks.Count * _tuning.StackPoints, $"Adds {Join(inputs.Stacks.Select(StackName).OrderBy(name => name, StringComparer.Ordinal).ToList())}"));
        }

        if (inputs.Rulesets > 1)
        {
            reasons.Add(((inputs.Rulesets - 1) * _tuning.MixedRulesetPoints, "Mixes cards from more than one ruleset"));
        }

        if (inputs.StandIns > 0)
        {
            reasons.Add((inputs.StandIns * _tuning.StandInPoints, $"Uses {inputs.StandIns} stand-in part{(inputs.StandIns == 1 ? "" : "s")}"));
        }

        if (inputs.Keywords.Count > 0)
        {
            var names = inputs.Keywords.Select(term => _terms[term].Name).OrderBy(name => name, StringComparer.Ordinal).ToList();
            var shown = names.Take(_tuning.NamedKeywords).ToList();
            var more = names.Count - shown.Count;
            reasons.Add((inputs.Keywords.Count * _tuning.KeywordPoints,
                $"{names.Count} keyword{(names.Count == 1 ? "" : "s")} to know: {string.Join(", ", shown)}{(more > 0 ? $" and {more} more" : "")}"));
        }

        if (inputs.Steps > 0)
        {
            reasons.Add((inputs.Steps * _tuning.StepPoints, $"{inputs.Steps} extra setup step{(inputs.Steps == 1 ? "" : "s")} from the Scheme and Mastermind"));
        }

        if (inputs.Boxes > 1)
        {
            reasons.Add(((inputs.Boxes - 1) * _tuning.BoxPoints, $"Cards from {inputs.Boxes} boxes"));
        }

        return reasons.OrderByDescending(reason => reason.Points).Take(_tuning.MaxReasons).Select(reason => reason.Text).ToList();
    }

    private static string StackName(Part part) => part switch
    {
        Part.MadameHydra => "Madame HYDRA",
        Part.NewRecruits => "New Recruits",
        _ => part.ToString(),
    };

    private static string Join(IReadOnlyList<string> names) =>
        names.Count == 1 ? names[0] : $"{string.Join(", ", names.Take(names.Count - 1))} and {names[^1]}";
}
