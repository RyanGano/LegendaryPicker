using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that set cards of a group beside them, using Fixtures/CardsBeside: a copy of the core box and a made-up
// expansion. Catalog order puts the fixture's Schemes after the core box's 8 (6 in Solo): Test Holding Cells (all 10
// Test Guards cards beside it, so it excludes Test Jailer, who always leads them) and Test Crowded Cells (2 Test
// Inmates cards per player). Masterminds in catalog order are 0 Dr. Doom, 1 Loki, 2 Magneto, 3 Red Skull and 4 Test
// Jailer; the Henchman Groups 0 Doombot Legion, 1 Hand Ninjas, 2 Savage Land Mutates, 3 Sentinel, 4 Test Guards and
// 5 Test Inmates.
public sealed class CardsBesideTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "CardsBeside");

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "sideline"];

    private const int CrowdedCells = 9;
    private const int Magneto = 2;

    [Fact]
    public void A_drawn_group_puts_only_the_cards_left_after_those_beside_the_Scheme_in_the_Villain_Deck()
    {
        // Magneto leads Brotherhood; the next Villain Group draw takes the first left, and the Henchman draw Test Inmates.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(CrowdedCells, Magneto, 0, 5)));

        Assert.Equal("Test Crowded Cells", setup.Scheme.Name);
        Assert.Equal(["Test Inmates"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal([new GroupCardsBeside(HenchmanGroup("sideline_henchman_test-inmates"), null, 4, 4)], setup.CardsBeside);
        Assert.Equal(new VillainDeck(8, 5, 16, 10, 2, 0, 0, 0, 4), setup.VillainDeck);
        Assert.Equal(37, setup.VillainDeck.Total);
        Assert.Equal(new RuleNote("Scheme sets 4 Test Inmates beside it, 2 per player", "Card", null, "Sideline Fixture"), setup.Notes[0]);
    }

    [Fact]
    public void A_group_that_is_not_drawn_still_has_its_cards_set_beside_the_Scheme()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(CrowdedCells, Magneto, 0, 1)));

        Assert.Equal(["Hand Ninjas"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal([new GroupCardsBeside(HenchmanGroup("sideline_henchman_test-inmates"), null, 4, 0)], setup.CardsBeside);
        Assert.Equal(41, setup.VillainDeck.Total);
    }

    // With every Test Guards card beside the Scheme, Test Guards can't be drawn into the Villain Deck, and the Scheme's
    // data excludes Test Jailer, who always leads them, so the Mastermind draw has only the core box's 4.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void A_Scheme_never_draws_a_Mastermind_it_excludes_or_a_group_with_every_card_beside_it(int players)
    {
        for (var seed = 0; seed < 4; seed++)
        {
            var random = new ScriptedRandom([CoreSchemes(players), seed, .. Enumerable.Repeat(seed % 2, 8)]);
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, random));

            Assert.Equal("Test Holding Cells", setup.Scheme.Name);
            Assert.Equal(4, random.Options[1]);
            Assert.NotEqual("Test Jailer", setup.Mastermind.Name);
            Assert.DoesNotContain("Test Guards", setup.HenchmanGroups.Select(group => group.Name));
            Assert.Equal([new GroupCardsBeside(HenchmanGroup("sideline_henchman_test-guards"), null, 10, 0)], setup.CardsBeside);
        }
    }

    // Test Jailer is drawn with every other Scheme.
    [Fact]
    public void Another_Scheme_draws_the_Mastermind_one_Scheme_excludes()
    {
        var random = new ScriptedRandom(CrowdedCells, 4);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(5, random.Options[1]);
        Assert.Equal("Test Jailer", setup.Mastermind.Name);
        Assert.Contains("Test Guards", setup.HenchmanGroups.Select(group => group.Name));
    }

    // How many core Schemes can be drawn at a player count: 8, or 6 in Solo. The fixture's come after them.
    private static int CoreSchemes(int players)
    {
        var core = new ScriptedRandom();
        Generator.Generate(players, ["core"], core);
        return core.Options[0];
    }

    private static HenchmanGroup HenchmanGroup(string id) =>
        Catalog.Boxes.SelectMany(box => box.HenchmanGroups).Single(group => group.Id == id);
}
