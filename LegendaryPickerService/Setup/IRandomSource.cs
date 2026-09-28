namespace LegendaryPickerService.Setup;

// The one source of randomness in a table draw. Tests pass a scripted sequence,
// so a given sequence always produces the same setup.
public interface IRandomSource
{
    // A value from 0 up to, but not including, exclusiveMax; each equally likely.
    int Next(int exclusiveMax);
}

public sealed class SharedRandomSource : IRandomSource
{
    public int Next(int exclusiveMax) => Random.Shared.Next(exclusiveMax);
}
