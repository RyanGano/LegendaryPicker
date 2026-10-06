using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Dimensions box file (Data/Boxes/dimensions.json), drawn with the core box. It has no Schemes or Villain Groups, so a
// draw uses the core Schemes; its one Mastermind follows the core Masterminds.
public class DimensionsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "dimensions"];

    private static readonly int Jameson = Catalog.Boxes.Single(box => box.Id == "core").Masterminds.Count;

    [Fact]
    public void J_Jonah_Jameson_leads_Spider_Slayers_and_sets_up_the_Angry_Mobs_stack()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, Boxes, new ScriptedRandom(0, Jameson)));

        Assert.Equal("J. Jonah Jameson", setup.Mastermind.Name);
        Assert.Contains(setup.HenchmanGroups, group => group.Name == "Spider-Slayer");
        Assert.Contains(setup.Steps, step => step.StartsWith("Put 2 Officers per player"));
    }

    [Fact]
    public void The_5_Special_Bystanders_are_shuffled_into_the_Bystander_stack()
    {
        var withBox = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, 0)));
        var coreOnly = Assert.IsType<SetupResult>(Generator.Generate(2, ["core"], new ScriptedRandom(0, 0)));

        Assert.Equal(coreOnly.Stacks.Bystanders + 5, withBox.Stacks.Bystanders);
    }
}
