using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Mastermind whose Always Leads lets it lead one of several groups (#217), using Fixtures/AlwaysLeadsChoice: a copy of
// the core box and a made-up expansion with Test Chooser, who leads Test Alpha or Test Beta, and the Schemes Test Plain,
// Test Needs Beta, Test Drain and Test Needs Two. The box ids sort "choice" before "core", so they are Scheme draws 0 to
// 3, Test Chooser is Mastermind draw 0 and Dr. Doom draw 1.
public sealed class AlwaysLeadsChoiceTests
{
    private const string FixtureName = "Choice Fixture";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "AlwaysLeadsChoice")));

    private static readonly string[] Boxes = ["core", "choice"];

    [Fact]
    public void A_Mastermind_that_leads_one_of_several_groups_draws_one_at_random_into_a_slot()
    {
        var random = new ScriptedRandom(0, 0, 1);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Plain", "Test Chooser"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal("Test Beta", setup.VillainGroups[0].Name);
        Assert.Equal(2, random.Options[2]);
        Assert.Contains(new RuleNote("Test Chooser always leads Test Beta, one of Test Alpha and Test Beta", "Card", null, FixtureName), setup.Notes);
    }

    [Fact]
    public void A_group_the_Scheme_requires_meets_the_choice_without_a_draw()
    {
        var random = new ScriptedRandom(1, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Needs Beta", "Test Chooser"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal("Test Beta", setup.VillainGroups[0].Name);
        Assert.Equal(1, setup.VillainGroups.Count(group => group.Name == "Test Beta"));
        Assert.Contains(setup.Notes, note => note.Text == "Test Chooser always leads Test Beta, one of Test Alpha and Test Beta");
    }

    [Fact]
    public void Solo_ignores_a_choice_of_groups_like_any_Always_Leads()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, 0)));

        Assert.Equal("Test Chooser", setup.Mastermind.Name);
        Assert.DoesNotContain(setup.Notes, note => note.Text.Contains("always leads"));
        Assert.Contains(setup.Notes, note => note.Text == "Solo ignores Test Chooser's Always Leads");
    }

    [Fact]
    public void A_set_aside_Mastermind_that_leads_one_of_several_groups_adds_one_of_them()
    {
        // Dr. Doom is the Mastermind, then Test Chooser the first of the 4 others, then its group: Test Beta.
        var random = new ScriptedRandom(2, 1, 0, 1);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Drain", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal("Test Chooser", Assert.Single(setup.OutsideMasterminds!).Mastermind.Name);
        Assert.Contains("Test Beta", setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(setup.Notes, note => note.Text == "Scheme adds Test Chooser's Always Leads group Test Beta");
    }

    [Fact]
    public void Required_groups_that_fill_every_slot_drop_the_choice_without_a_draw()
    {
        var random = new ScriptedRandom(3, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Needs Two", "Test Chooser"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(["Test Gamma", "Test Delta"], setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(setup.Notes, note => note.Text == "Scheme requires Test Gamma and Test Delta, so Test Chooser's Always Leads group Test Alpha or Test Beta is dropped");
    }
}
