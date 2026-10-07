using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Using Fixtures/ShortGroups: a copy of the core box, a made-up expansion (Overflow) and a made-up box left out of every
// draw (Reserve) that holds one Villain Group, Test Reservists. Overflow has no groups of its own. Its Scheme, Test
// Overflow, adds 6 Villain Groups and puts 4 cards of each in the Villain Deck; its Mastermind, Test Wanderer, has no
// Always Leads group and adds 1 Villain Group. Catalog order puts Overflow's cards after the core box's, so Test Overflow
// is Scheme 8 at 2 players and Test Wanderer is Mastermind 4; Mastermind 0 is the core box's Dr. Doom, who leads a
// Henchman Group.
public sealed class ShortGroupsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "ShortGroups");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "overflow"];

    // 2 Villain Groups at 2 players plus the Scheme's 6 is 8, one more than the core box's 7: the eighth comes from the
    // box that isn't included, named in a note, and each group puts the Scheme's 4 cards in the Villain Deck.
    [Fact]
    public void A_Scheme_needing_more_groups_than_the_included_boxes_have_takes_the_rest_from_one_not_included()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(8, 0)));

        Assert.Equal("Test Overflow", setup.Scheme.Name);
        Assert.Equal(8, setup.VillainGroups.Count);
        Assert.Contains(setup.VillainGroups, group => group.Name == "Test Reservists");
        Assert.Equal(8 * 4, setup.VillainDeck.VillainCards);
        Assert.Single(setup.Notes, note => note.Text == "The included boxes have too few groups for Scheme, so Test Reservists comes from Reserve Fixture, which isn't included");
    }

    // A Mastermind with no Always Leads group leaves every group to the draw, and its Setup still adds one in Solo.
    [Fact]
    public void A_Mastermind_without_an_Always_Leads_group_constrains_no_draw()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, 4)));

        Assert.Equal("Test Wanderer", setup.Mastermind.Name);
        Assert.Equal(2, setup.VillainGroups.Count);
        Assert.DoesNotContain(setup.Notes, note => note.Text.Contains("Always Leads"));
    }
}
