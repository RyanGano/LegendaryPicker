using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Weights a draw toward a target difficulty (#267). It only ever chooses among options the draw's rules already allow:
// every option has a weight above zero, so none is ruled out, and an unreachable target just picks the nearest.
// An option's weight is exp(-|expected rating - target| / Temperature), the expected rating being the setup so far, the
// option, and the pool-average card for whatever is still undrawn. A target complexity (#270) works the same way, with
// the two distances added, so an option far from either target weighs less; a target left out adds nothing.
public sealed class DifficultyWeights(
    BoxCatalog catalog, DifficultyTuning tuning, int? target, IReadOnlyList<Hero> heroPool, int? complexityTarget = null,
    ComplexityMeter? meter = null)
{
    private readonly ComplexityMeter _meter = meter ?? new ComplexityMeter(catalog);

    // The cards drawn so far. Deck is the Hero Deck's Heroes so far and DeckSlots how many it will hold; with no
    // DeckSlots the whole deck is the pool average.
    public sealed record Drawn(
        Scheme? Scheme = null, Mastermind? Mastermind = null, IReadOnlyList<VillainGroup>? VillainGroups = null,
        IReadOnlyList<HenchmanGroup>? HenchmanGroups = null, IReadOnlyList<Hero>? Deck = null, int DeckSlots = 0);

    private static readonly IReadOnlyDictionary<string, string> NoNames = new Dictionary<string, string>();

    public double Expected(Drawn drawn)
    {
        var deck = drawn.Deck ?? [];
        var heroes = deck.Select(hero => DifficultyInputs.Scored(catalog, hero)).ToList();

        // The slots still to fill count as the pool's average Hero: each pool Hero at an equal share of them.
        var open = drawn.DeckSlots == 0 ? 1 : Math.Max(drawn.DeckSlots - deck.Count, 0);
        if (open > 0 && heroPool.Count > 0)
        {
            heroes.AddRange(heroPool.Select(hero => DifficultyInputs.Scored(catalog, hero, open / (double)heroPool.Count)));
        }

        var opposition = DifficultyInputs.Opposition(
            catalog, drawn.Scheme, drawn.Mastermind, drawn.VillainGroups ?? [], drawn.HenchmanGroups ?? [],
            tuning.EstimatedMasterStrikes, tuning.EstimatedTwists, tuning.EstimatedHenchmanCards, NoNames);
        return DifficultyScorer.Expected(heroes, tuning, opposition);
    }

    // How far the draw is from the targets: the difference between its expected difficulty and the target difficulty, and
    // between its expected complexity and the target complexity, each only when that target was asked for.
    public double Distance(Drawn drawn)
    {
        var distance = target is { } rating ? Math.Abs(Expected(drawn) - rating) : 0;
        if (complexityTarget is { } wanted)
        {
            distance += Math.Abs(ExpectedComplexity(drawn) - wanted);
        }

        return distance;
    }

    // The draw so far, with the slots still to fill at the pool's average Hero, as a continuous complexity rating. The
    // outside Heroes and the other draws a Scheme asks for are not known yet and count for nothing.
    public double ExpectedComplexity(Drawn drawn)
    {
        var deck = drawn.Deck ?? [];
        ICard?[] cards = [drawn.Scheme, drawn.Mastermind, .. drawn.VillainGroups ?? [], .. drawn.HenchmanGroups ?? [], .. deck];
        var inputs = _meter.Inputs(cards.OfType<ICard>());
        var open = drawn.DeckSlots == 0 ? 0 : Math.Max(drawn.DeckSlots - deck.Count, 0);
        return _meter.Expected(_meter.Points(inputs) + open * _meter.MeanHeroPoints(inputs, heroPool));
    }

    // The index of the option the random source picks, given each option's expected difficulty (measured against the
    // target difficulty, when there is one).
    public int Pick(IRandomSource random, IReadOnlyList<double> expected) =>
        PickByDistance(random, expected.Select(rating => target is { } wanted ? Math.Abs(rating - wanted) : 0).ToList());

    private int PickByDistance(IRandomSource random, IReadOnlyList<double> distances)
    {
        var weights = distances.Select(distance => Math.Exp(-distance / tuning.Temperature)).ToList();
        var at = random.Next(tuning.WeightResolution) / (double)tuning.WeightResolution * weights.Sum();
        for (var i = 0; i < weights.Count; i++)
        {
            at -= weights[i];
            if (at < 0)
            {
                return i;
            }
        }

        return weights.Count - 1;
    }

    // One option drawn from options, each measured by its distance from the targets.
    public T Pick<T>(IRandomSource random, IReadOnlyList<T> options, Func<T, double> distance) =>
        options[PickByDistance(random, options.Select(distance).ToList())];
}
