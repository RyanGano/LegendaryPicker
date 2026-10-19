using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// The difficulty (#267) and complexity (#270) a request asks a draw to land in, and the judge of a drawn choice against
// them (#329). It rates a choice with the same scorers and inputs the response shows, so a choice it finds in band is shown
// in band. A target is never a rule: the generator only swaps open picks among the options the rules allow, and a target
// it can't reach leaves the nearest choice it found. An untargeted scale is not rated.
public sealed class RatingTargets(BoxCatalog catalog, int? difficulty, int? complexity)
{
    private static readonly IReadOnlyDictionary<string, string> NoNames = new Dictionary<string, string>();

    private readonly ComplexityMeter _meter = new(catalog);
    private readonly Dictionary<string, ScoredHero> _scored = new(StringComparer.Ordinal);

    // A choice's band and continuous rating on each targeted scale; an untargeted scale stays at 0.
    public readonly record struct Rated(int Difficulty, double DifficultyExpected, int Complexity, double ComplexityExpected);

    // What the response rates of a choice: the Scheme, the drawn Mastermind, the groups and the Hero Deck's rated Heroes
    // against the Villain Deck's real Master Strikes, Twists and Henchman cards, and for complexity the cards it reads with
    // the stacks the setup lays out for them and the stand-ins it uses, which layout gives only when complexity is asked for.
    // players and ruleset are the setup's, which the difficulty is relative to (#334).
    public Rated Rate(
        ICard scheme, ICard mastermind, IReadOnlyList<ICard> villainGroups, IReadOnlyList<ICard> henchmanGroups, IReadOnlyList<Hero> heroes,
        int masterStrikes, int twists, int henchmanCards, int players, Ruleset ruleset,
        Func<(IEnumerable<Part> Stacks, IReadOnlyCollection<StandIn> StandIns)> layout)
    {
        var (rating, expected) = (0, 0.0);
        if (difficulty is not null)
        {
            var opposition = DifficultyInputs.Opposition(catalog, scheme, mastermind, villainGroups, henchmanGroups, masterStrikes, twists, henchmanCards, players, ruleset, NoNames);
            (rating, expected) = DifficultyScorer.Measure(heroes.Select(Scored).ToList(), DifficultyTuning.Default, opposition);
        }

        var (complexityRating, complexityExpected) = (0, 0.0);
        if (complexity is not null)
        {
            var (stacks, standIns) = layout();
            var points = _meter.Points(_meter.Inputs([scheme, mastermind, .. villainGroups, .. henchmanGroups, .. heroes], stacks, standIns));
            (complexityRating, complexityExpected) = (_meter.Rating(points), _meter.Expected(points));
        }

        return new Rated(rating, expected, complexityRating, complexityExpected);
    }

    // Whether a choice rates every target asked for.
    public bool Reached(Rated rated) =>
        (difficulty is null || rated.Difficulty == difficulty) && (complexity is null || rated.Complexity == complexity);

    // How far a choice is from the targets: the sum of its continuous distances from each target asked for.
    public double Distance(Rated rated) =>
        (difficulty is { } wanted ? Math.Abs(rated.DifficultyExpected - wanted) : 0)
        + (complexity is { } wantedComplexity ? Math.Abs(rated.ComplexityExpected - wantedComplexity) : 0);

    // A Hero's tagged cards, read once per draw.
    private ScoredHero Scored(Hero hero)
    {
        if (!_scored.TryGetValue(hero.Id, out var scored))
        {
            scored = DifficultyInputs.Scored(catalog, hero);
            _scored[hero.Id] = scored;
        }

        return scored;
    }
}
