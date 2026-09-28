using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Plays back a fixed list of draws, then draws the first remaining option for every later draw.
// Records how many options each draw had, so a test can assert what was drawn from.
internal sealed class ScriptedRandom(params int[] draws) : IRandomSource
{
    private int _position;

    public List<int> Options { get; } = [];

    public int Next(int exclusiveMax)
    {
        Options.Add(exclusiveMax);
        var value = _position < draws.Length ? draws[_position] : 0;
        _position++;

        if (value < 0 || value >= exclusiveMax)
        {
            throw new InvalidOperationException($"Draw {_position} scripted {value}, but it has only {exclusiveMax} options.");
        }

        return value;
    }
}

// Repeats a pattern of draws, wrapped to the options each draw has.
internal sealed class CyclingRandom(params int[] pattern) : IRandomSource
{
    private int _position;

    public int Next(int exclusiveMax) => pattern[_position++ % pattern.Length] % exclusiveMax;
}
