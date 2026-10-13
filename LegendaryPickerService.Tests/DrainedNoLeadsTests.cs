using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Mastermind a Scheme sets aside that leads no group (#211), using Fixtures/DrainedNoLeads: a copy of the core box
// and a made-up expansion with Test Planet, a Mastermind with no Always Leads, and the Scheme Test Drain. Solo draws
// 6 core Schemes before Test Drain; Dr. Doom is Mastermind draw 0 and Test Planet the last of the 4 others.
public sealed class DrainedNoLeadsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "DrainedNoLeads"));

    private static readonly SetupGenerator Generator = new(Catalog);

    [Fact]
    public void A_set_aside_Mastermind_with_no_Always_Leads_adds_one_extra_Villain_Group_drawn_by_the_normal_rules()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, ["core", "planet"], new ScriptedRandom(6, 0, 3)));

        Assert.Equal(("Test Drain", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal("Test Planet", Assert.Single(setup.OutsideMasterminds!).Mastermind.Name);
        Assert.Equal(2, setup.VillainGroups.Count);
        Assert.Equal(2, setup.VillainGroups.Select(group => group.Id).Distinct().Count());
        Assert.Contains(setup.Notes, note => note.Text == "Scheme adds an extra Villain Group for Test Planet, who leads no group");
    }
}
