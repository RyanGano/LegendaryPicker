using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The S.H.I.E.L.D. box file (Data/Boxes/shield.json), drawn with the core box.
public class ShieldTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "shield"];

    private const string Aim = "A.I.M., Hydra Offshoot";
    private const string HydraElite = "Hydra Elite";

    // The 16 special Officers join the core box's 30 (SH p.1).
    [Fact]
    public void The_special_Officers_make_a_46_card_Officer_stack()
    {
        var setup = Draw(2, "Hail Hydra");

        Assert.Equal(46, setup.Stacks.Officers);
        Assert.Equal(11, setup.VillainDeck.Twists);
    }

    // Each Mastermind the Scheme can be drawn with: exactly one of the two groups is in the setup, and it is the
    // Mastermind's own Always Leads group when that is one of them.
    [Fact]
    public void SHIELD_vs_HYDRA_War_takes_one_of_its_two_Villain_Groups_never_both()
    {
        var scheme = SchemeIndex(2, "S.H.I.E.L.D. vs. HYDRA War");
        var probe = new ScriptedRandom(scheme);
        Generator.Generate(2, Boxes, probe);

        var leads = new Dictionary<string, string>();
        for (var mastermind = 0; mastermind < probe.Options[1]; mastermind++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind)));
            var group = Assert.Single(setup.VillainGroups, g => g.Name is Aim or HydraElite);
            leads[setup.Mastermind.Name] = group.Name;
        }

        Assert.Equal(HydraElite, leads["Hydra High Council"]);
        Assert.Equal(Aim, leads["Hydra Super-Adaptoid"]);
    }

    private static int SchemeIndex(int players, string scheme)
    {
        var probe = new ScriptedRandom();
        Generator.Generate(players, Boxes, probe);
        return Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s))).Scheme.Name == scheme);
    }

    private static SetupResult Draw(int players, string scheme) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(SchemeIndex(players, scheme))));
}
