using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// The one source of randomness in a table draw. Tests pass a scripted sequence,
// so a given sequence always produces the same setup.
public interface IRandomSource
{
    // A value from 0 up to, but not including, exclusiveMax; each equally likely.
    int Next(int exclusiveMax);

    // The card a draw takes from its options, in catalog order: one at random, unless the source knows which card it
    // wants, as a setup code's replay does (#335).
    T Pick<T>(IReadOnlyList<T> options) where T : ICard => options[Next(options.Count)];
}

public sealed class SharedRandomSource : IRandomSource
{
    public int Next(int exclusiveMax) => Random.Shared.Next(exclusiveMax);
}
