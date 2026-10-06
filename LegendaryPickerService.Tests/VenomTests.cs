using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Venom box file (Data/Boxes/venom.json), drawn with the core box.
public class VenomTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "venom"];

    [Fact]
    public void Maximum_Carnage_lays_out_6_Wounds_per_player()
    {
        var setup = Draw(3, "Maximum Carnage");

        Assert.Equal(10, setup.VillainDeck.Twists);
        Assert.Equal(18, setup.Stacks.Wounds);
    }

    [Fact]
    public void Symbiotic_Absorption_sets_one_other_Mastermind_aside_and_adds_its_Always_Leads_group()
    {
        var setup = Draw(2, "Symbiotic Absorption");

        var drained = Assert.Single(setup.OutsideMasterminds!);
        Assert.Equal((Pile.SetAside, null), (drained.To, drained.Joins));
        Assert.NotEqual(setup.Mastermind.AlwaysLeads.GroupId, drained.Mastermind.AlwaysLeads.GroupId);
        Assert.Contains(drained.Mastermind.AlwaysLeads.GroupId, setup.VillainGroups.Select(group => group.Id));
        Assert.Equal(11, setup.VillainDeck.Twists);
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var probe = new ScriptedRandom();
        Generator.Generate(players, Boxes, probe);
        var index = Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s))).Scheme.Name == scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
    }
}
