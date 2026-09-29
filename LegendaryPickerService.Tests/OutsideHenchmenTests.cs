using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that draw a Henchman Group outside the Villain Deck and put some of its cards in the Hero Deck,
// using Fixtures/OutsideHenchmen: a copy of the core box and a made-up expansion. Catalog order puts the
// fixture's Schemes after the core box's 8 (6 in Solo): Test Newsroom (6 Henchmen of an extra group into the
// Hero Deck, 4 in Solo, and 3 extra Henchman Groups in the Villain Deck at 2 players) and Test Overfull
// Newsroom (11 Henchmen of an extra group, more than a group holds). The core box has 4 Henchman Groups.
// Mastermind draw 0 is Dr. Doom, who leads Doombot Legion.
public sealed class OutsideHenchmenTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "OutsideHenchmen");

    private const string FixtureName = "Henchmen Fixture";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "henchmen"];

    [Fact]
    public void An_extra_Henchman_Group_puts_its_cards_in_the_Hero_Deck_and_its_total()
    {
        // At 3 players the Villain Deck's one Henchman Group is Doombot Legion, and the extra group is drawn
        // from the other 3 after the 3 Villain Group draws.
        var random = new ScriptedRandom(8, 0, 0, 0, 0, 2);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, Boxes, random));

        Assert.Equal("Test Newsroom", setup.Scheme.Name);
        Assert.Equal(3, random.Options[5]);
        Assert.Equal(["Doombot Legion"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal([new OutsideHenchmanGroup(HenchmanGroup("core_henchman_sentinel"), Pile.HeroDeck, 6)], setup.OutsideHenchmen);
        Assert.Equal(new HeroDeck(70, 0, 0, 6), setup.HeroDeck);
        Assert.Equal(76, setup.HeroDeck.Total);
        Assert.Equal(new VillainDeck(8, 5, 24, 10, 8, 0), setup.VillainDeck);
        Assert.Equal(55, setup.VillainDeck.Total);
        Assert.Equal(
            new RuleNote("Scheme draws 1 extra Henchman Group outside the Villain Deck and puts 6 of its Henchmen into the Hero Deck", "Card", null, FixtureName),
            setup.Notes[0]);
    }

    [Fact]
    public void An_extra_Henchman_Group_uses_its_Solo_value_in_Solo()
    {
        // Solo ignores Always Leads, so the Villain Deck's Henchman Group is drawn too: Doombot Legion, then
        // Hand Ninjas from the 3 left.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(6, 0)));

        Assert.Equal("Test Newsroom", setup.Scheme.Name);
        Assert.Equal(["Doombot Legion"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal([new OutsideHenchmanGroup(HenchmanGroup("core_henchman_hand-ninjas"), Pile.HeroDeck, 4)], setup.OutsideHenchmen);
        Assert.Equal(46, setup.HeroDeck.Total);
        Assert.Equal(3, setup.VillainDeck.HenchmanCards);
        Assert.Equal(
            new RuleNote(
                "Scheme draws 1 extra Henchman Group outside the Villain Deck and puts 4 of its Henchmen into the Hero Deck",
                "R p.2", "https://example.test/henchmen-rules.pdf", FixtureName),
            setup.Notes[0]);
    }

    // At 2 players Test Newsroom puts all 4 core Henchman Groups in the Villain Deck, which leaves none to draw
    // outside it, so it is dropped there. Test Overfull Newsroom needs more cards than any group has, so it is
    // never drawable.
    [Theory]
    [InlineData(1, 7)]
    [InlineData(2, 8)]
    [InlineData(3, 9)]
    [InlineData(4, 9)]
    [InlineData(5, 9)]
    public void A_Scheme_with_no_Henchman_Group_left_to_draw_outside_the_Villain_Deck_is_dropped(int players, int schemes)
    {
        var random = new ScriptedRandom();

        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, random));

        Assert.Equal(schemes, random.Options[0]);
    }

    // With no Scheme that draws one, the setup lists no Henchmen outside the Villain Deck and draws nothing more.
    [Fact]
    public void A_setup_without_the_effect_lists_no_Henchmen_outside_the_Villain_Deck()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(7, 3)));

        Assert.Empty(setup.OutsideHenchmen);
        Assert.Equal(new HeroDeck(70, 0), setup.HeroDeck);
    }

    private static HenchmanGroup HenchmanGroup(string id) =>
        Catalog.Boxes.SelectMany(box => box.HenchmanGroups).Single(group => group.Id == id);
}
