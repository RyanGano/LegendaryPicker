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
        var clash = DrawIndexes.Scheme(Catalog, Boxes, 2, "Clash of the Monsters Unleashed");
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

    [Fact]
    public void A_draw_with_Steal_All_Oxygen_on_Earth_and_Fin_Fang_Foom_rates_with_reasons_naming_them()
    {
        var setup = Draw(2, "Steal All Oxygen on Earth", "Fin Fang Foom");

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Steal All Oxygen on Earth's Twists"));
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Fin Fang Foom's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var s = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var m = DrawIndexes.Mastermind(Catalog, Boxes, scheme, mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
