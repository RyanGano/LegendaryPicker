using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Champions box file (Data/Boxes/champions.json), drawn with the core box.
public class ChampionsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Champions = Catalog.Boxes.Single(box => box.Id == "champions");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "champions"];

    // Fin Fang Foom always leads the group Clash sets entirely aside, so no legal draw pairs them.
    [Fact]
    public void Clash_of_the_Monsters_Unleashed_is_never_drawn_with_Fin_Fang_Foom()
    {
        var probe = new ScriptedRandom();
        Generator.Generate(2, Boxes, probe);
        var clash = Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(s))).Scheme.Name == "Clash of the Monsters Unleashed");
        var masterminds = new ScriptedRandom(clash);
        Generator.Generate(2, Boxes, masterminds);

        var drawn = Enumerable.Range(0, masterminds.Options[1])
            .Select(m => Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(clash, m))).Mastermind.Name)
            .ToList();

        Assert.Contains("Pagliacci", drawn);
        Assert.DoesNotContain("Fin Fang Foom", drawn);
    }

    [Fact]
    public void Steal_All_Oxygen_on_Earth_lists_the_Oxygen_Level_step()
    {
        var setup = Draw(2, "Steal All Oxygen on Earth", "Fin Fang Foom");

        Assert.Contains("Start the Oxygen Level at 8", setup.Steps);
    }

    // Finds the Scheme and Mastermind draws that give this pair, since which Schemes and Masterminds can be drawn
    // depends on the player count. Each player count's Schemes, and each Scheme's Masterminds, are looked up once.
    private static readonly ConcurrentDictionary<(int Players, string Scheme), Dictionary<string, int[]>> Pairs = new();

    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var pairs = Pairs.GetOrAdd((players, scheme), key =>
        {
            var probe = new ScriptedRandom();
            Generator.Generate(key.Players, Boxes, probe);
            var s = Enumerable.Range(0, probe.Options[0])
                .First(s => Assert.IsType<SetupResult>(Generator.Generate(key.Players, Boxes, new ScriptedRandom(s))).Scheme.Name == key.Scheme);
            var masterminds = new ScriptedRandom(s);
            Generator.Generate(key.Players, Boxes, masterminds);
            return Enumerable.Range(0, masterminds.Options[1]).ToDictionary(
                m => Assert.IsType<SetupResult>(Generator.Generate(key.Players, Boxes, new ScriptedRandom(s, m))).Mastermind.Name,
                m => new[] { s, m });
        });

        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(pairs[mastermind])));
    }
}
