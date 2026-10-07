using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Doctor Strange and the Shadows of Nightmare box file (Data/Boxes/doctor-strange-and-the-shadows-of-nightmare.json), drawn with the core box.
public class DoctorStrangeTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "doctor-strange-and-the-shadows-of-nightmare"];

    // Nightmare always leads the Fear Lords, and War for the Dream Dimension adds one more Villain Group on top of the 2-player count.
    [Fact]
    public void Nightmare_leads_the_Fear_Lords_and_War_for_the_Dream_Dimension_adds_a_group()
    {
        var setup = Draw(2, "War for the Dream Dimension", "Nightmare");

        Assert.Contains("Fear Lords", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(3, setup.VillainGroups.Count);
    }

    // The core box's own rules lay out no Wounds; Nightmare's Night Terrors Tactic gains one, so Nightmare brings the
    // Wound Stack. The rest of the draw is scripted to cards that take no Wounds: Skrulls and five such core Heroes.
    [Fact]
    public void Nightmare_lays_out_the_Wound_Stack()
    {
        var setup = Draw(2, "Duels of Science and Magic", "Nightmare", 5, 0, 0, 0, 0, 1, 1);

        Assert.Equal(["Fear Lords", "Skrulls"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Emma Frost", "Gambit"], setup.Heroes.Select(hero => hero.Name).Order());
        Assert.NotNull(setup.Stacks.Wounds);
    }

    private static SetupResult Draw(int players, string scheme, string mastermind, params int[] rest)
    {
        var probe = new ScriptedRandom();
        Generator.Generate(players, Boxes, probe);
        var s = Enumerable.Range(0, probe.Options[0])
            .First(i => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(i))).Scheme.Name == scheme);
        var mastermindProbe = new ScriptedRandom(s);
        Generator.Generate(players, Boxes, mastermindProbe);
        var m = Enumerable.Range(0, mastermindProbe.Options[1])
            .First(i => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, i))).Mastermind.Name == mastermind);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom([s, m, .. rest])));
    }
}
