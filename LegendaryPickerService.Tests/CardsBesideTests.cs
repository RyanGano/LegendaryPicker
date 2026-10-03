using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that set cards of a group beside them, using Fixtures/CardsBeside: a copy of the core box and a made-up
// expansion. Catalog order puts the fixture's Schemes after the core box's 8 (6 in Solo): Test Holding Cells (all 10
// Doombot Legion cards beside it) and Test Overfull Cells (4 Sentinel cards per player, more than the group's 10
// from 3 players). Masterminds in catalog order are 0 Dr. Doom (who leads Doombot Legion), 1 Loki, 2 Magneto and
// 3 Red Skull; the Henchman Groups 0 Doombot Legion, 1 Hand Ninjas, 2 Savage Land Mutates and 3 Sentinel.
public sealed class CardsBesideTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "CardsBeside");

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "sideline"];

    private const int OverfullCells = 9;
    private const int Magneto = 2;

    [Fact]
    public void A_drawn_group_puts_only_the_cards_left_after_those_beside_the_Scheme_in_the_Villain_Deck()
    {
        // Magneto leads Brotherhood; the next Villain Group draw takes the first left, and the Henchman draw Sentinel.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(OverfullCells, Magneto, 0, 3)));

        Assert.Equal("Test Overfull Cells", setup.Scheme.Name);
        Assert.Equal(["Sentinel"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal([new GroupCardsBeside(HenchmanGroup("core_henchman_sentinel"), null, 8, 8)], setup.CardsBeside);
        Assert.Equal(new VillainDeck(8, 5, 16, 10, 2, 0, 0, 0, 8), setup.VillainDeck);
        Assert.Equal(33, setup.VillainDeck.Total);
        Assert.Equal(new RuleNote("Scheme sets 8 Sentinel beside it, 4 per player", "Card", null, "Sideline Fixture"), setup.Notes[0]);
    }

    [Fact]
    public void A_group_that_is_not_drawn_still_has_its_cards_set_beside_the_Scheme()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(OverfullCells, Magneto, 0, 1)));

        Assert.Equal(["Hand Ninjas"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal([new GroupCardsBeside(HenchmanGroup("core_henchman_sentinel"), null, 8, 0)], setup.CardsBeside);
        Assert.Equal(41, setup.VillainDeck.Total);
    }

    // With every Doombot Legion card beside the Scheme, Doombot Legion can't be drawn, and Dr. Doom, who always
    // leads it, can't complete the Scheme.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void A_group_with_every_card_beside_the_Scheme_is_never_drawn_and_its_Mastermind_is_dropped(int players)
    {
        for (var seed = 0; seed < 20; seed++)
        {
            var random = new ScriptedRandom([CoreSchemes(players), seed % 3, .. Enumerable.Repeat(seed % 2, 8)]);
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, random));

            Assert.Equal("Test Holding Cells", setup.Scheme.Name);
            Assert.DoesNotContain("Doombot Legion", setup.HenchmanGroups.Select(group => group.Name));
            Assert.Equal(players == 1 ? 4 : 3, random.Options[1]);
            Assert.Equal([new GroupCardsBeside(HenchmanGroup("core_henchman_doombot-legion"), null, 10, 0)], setup.CardsBeside);
        }
    }

    // From 3 players Test Overfull Cells asks for more Sentinel cards than the group holds, so it is dropped.
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(5, false)]
    public void A_Scheme_that_sets_more_cards_beside_it_than_a_group_holds_is_dropped(int players, bool eligible)
    {
        var both = new ScriptedRandom();
        Generator.Generate(players, Boxes, both);

        Assert.Equal(CoreSchemes(players) + (eligible ? 2 : 1), both.Options[0]);
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
