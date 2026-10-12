using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Playing a Mastermind's Epic side (#185), using Fixtures/Epic: a copy of the core box and a made-up expansion whose
// Test Titan leads Test Keepers and has an Epic side with a Setup of its own, an extra Villain Group and a step, and
// whose Test Binder's Epic side uses Bindings, which no First Edition box supplies. The Masterminds are the core box's
// 4, then Test Titan and Test Binder; the first Scheme is the core box's.
public sealed class EpicMastermindsTests
{
    private static readonly SetupGenerator Generator =
        new(BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "Epic")));

    private static readonly string[] Boxes = ["core", "epic"];
    private const string Offer = "Optional: play the Epic side instead; the Tactics stay the same";

    [Fact]
    public void The_normal_side_is_drawn_and_offered_when_Epic_is_off()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, 4)));

        Assert.Equal("Test Titan", setup.Mastermind.Name);
        Assert.Contains(Offer, setup.Steps);
        Assert.DoesNotContain("Test step only the Epic side has", setup.Steps);
        Assert.Equal(2, setup.VillainGroups.Count);
    }

    [Fact]
    public void The_Epic_side_is_played_with_its_own_setup_when_Epic_is_on()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, 4), epic: true));

        Assert.Equal("Epic Test Titan", setup.Mastermind.Name);
        Assert.DoesNotContain(Offer, setup.Steps);
        Assert.Contains("Test step only the Epic side has", setup.Steps);
        Assert.Equal(3, setup.VillainGroups.Count);
        Assert.Equal("Test Keepers", setup.VillainGroups[0].Name);
        Assert.Contains(setup.Notes, note => note.Text == "Epic Test Titan adds 1 Villain Group");
    }

    [Fact]
    public void An_Epic_side_whose_parts_no_included_box_supplies_stays_on_its_normal_side()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, 5), epic: true));

        Assert.Equal("Test Binder", setup.Mastermind.Name);
        Assert.Contains(Offer, setup.Steps);
    }

    [Fact]
    public void A_Mastermind_without_an_Epic_side_is_unchanged_when_Epic_is_on()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, 0), epic: true));

        Assert.Equal("Dr. Doom", setup.Mastermind.Name);
    }
}
