using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Weights a draw toward a target difficulty (#267). It only ever chooses among options the draw's rules already allow:
// every option has a weight above zero, so none is ruled out, and an unreachable target just picks the nearest.
// An option's weight is exp(-|expected rating - target| / Temperature), the expected rating being the setup so far, the
// option, and the pool-average card for whatever is still undrawn.
public sealed class DifficultyWeights(BoxCatalog catalog, DifficultyTuning tuning, int target, IReadOnlyList<Hero> heroPool)
{
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

    // The index of the option the random source picks, given each option's expected rating.
    public int Pick(IRandomSource random, IReadOnlyList<double> expected)
    {
        var weights = expected.Select(rating => Math.Exp(-Math.Abs(rating - target) / tuning.Temperature)).ToList();
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

    // One option drawn from options, each rated by expected.
    public T Pick<T>(IRandomSource random, IReadOnlyList<T> options, Func<T, double> expected) =>
        options[Pick(random, options.Select(expected).ToList())];
}
