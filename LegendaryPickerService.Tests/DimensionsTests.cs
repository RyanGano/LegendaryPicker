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
    public void The_6_Special_Bystanders_are_shuffled_into_the_Bystander_stack()
    {
        var withBox = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, 0)));
        var coreOnly = Assert.IsType<SetupResult>(Generator.Generate(2, ["core"], new ScriptedRandom(0, 0)));

        Assert.Equal(coreOnly.Stacks.Bystanders + 6, withBox.Stacks.Bystanders);
    }

    [Fact]
    public void The_response_names_the_Special_Bystanders_only_when_the_box_is_included()
    {
        var withBox = Assert.IsType<SetupBody>(SetupResponse.From(Generator.Generate(2, Boxes, new ScriptedRandom(0, 0)), Catalog));
        var coreOnly = Assert.IsType<SetupBody>(SetupResponse.From(Generator.Generate(2, ["core"], new ScriptedRandom(0, 0)), Catalog));

        var special = Assert.Single(withBox.SpecialCards!);
        Assert.Equal(("bystanders", "Dimensions"), (special.Stack, special.Box));
        Assert.Equal(6, special.Cards.Count);
        Assert.Contains("Forklift Driver", special.Cards);
        Assert.Null(coreOnly.SpecialCards);
    }

    // Its own Mastermind is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_J_Jonah_Jameson_rates_with_a_reason_naming_him()
    {
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "The Legacy Virus", "J. Jonah Jameson");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(DrawIndexes.Scheme(Catalog, Boxes, 2, "The Legacy Virus"), mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("J. Jonah Jameson's Master Strikes"));
    }
}
