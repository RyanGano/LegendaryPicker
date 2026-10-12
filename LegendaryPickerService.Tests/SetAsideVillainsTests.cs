using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Mastermind that sets aside the Villains of the extra Villain Group it adds (#208), using Fixtures/SetAside: a copy
// of the core box and a made-up expansion. At 2 players Test Chronarch, who leads Test Timekeepers and adds 1 Villain
// Group, gets 3 groups. After Test Timekeepers and the first group, Brotherhood, the 6 other core groups are followed
// by 6 Test Drifters (7 Villains and 1 Ambush Scheme) and 7 Test Wanderers (8 Villains). The Schemes are the core box's
// 8, then 8 Test Loop and 9 Test Surge, which adds a Villain Group of its own, and the Masterminds the core box's 4, then 4 Test Chronarch.
public sealed class SetAsideVillainsTests
{
    private static readonly SetupGenerator Generator =
        new(BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "SetAside")));

    private static readonly string[] Boxes = ["core", "setaside"];

    [Theory]
    [InlineData(6, 7, "Test Drifters")]
    [InlineData(7, 8, "Test Wanderers")]
    public void The_extra_groups_Villains_are_set_aside_and_leave_the_Villain_Deck_total(int extraDraw, int setAside, string name)
    {
        // Test Loop and Test Chronarch, then the one group drawn beside the Mastermind's: the first left and the extra one.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(8, 4, 0, extraDraw)));

        Assert.Equal(["Test Timekeepers", "Brotherhood", name], setup.VillainGroups.Select(group => group.Name));
        var aside = Assert.Single(setup.VillainsSetAside!);
        Assert.Equal((name, setAside), (aside.Group.Name, aside.Count));
        Assert.Equal(3 * 8, setup.VillainDeck.VillainCards);
        Assert.Equal(8 + 5 + 3 * 8 - setAside + 10 + 2, setup.VillainDeck.Total);
        Assert.Contains(setup.Notes, note => note.Text.StartsWith("Test Chronarch sets aside the Villains of " + name));
    }

    [Fact]
    public void Only_the_Masterminds_extra_group_is_set_aside_when_the_Scheme_adds_one_too()
    {
        // Test Surge and Test Chronarch: 4 groups, Test Timekeepers then Brotherhood, Enemies of Asgard and Test Drifters.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(9, 4, 0, 0, 5)));

        Assert.Equal(["Test Timekeepers", "Brotherhood", "Enemies of Asgard", "Test Drifters"], setup.VillainGroups.Select(group => group.Name));
        var aside = Assert.Single(setup.VillainsSetAside!);
        Assert.Equal(("Test Drifters", 7), (aside.Group.Name, aside.Count));
        Assert.Equal(8 + 5 + 4 * 8 - 7 + 10 + 2, setup.VillainDeck.Total);
    }
}
