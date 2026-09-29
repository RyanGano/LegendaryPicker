using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that move cards between stacks and decks during setup, using Fixtures/CardMoves: a copy of
// the core box and a made-up expansion. Catalog order puts the fixture's Schemes after the core box's,
// so at 2–4 players they are 8 Test Rescue (20 Bystanders into the Hero Deck, 12 in Solo), 9 Test
// Henchman Army (6 Henchmen into the Hero Deck), 10 Test Muster (2 Officers per player into the Villain
// Deck), 11 Test Wounded (1 Wound into each starting deck) and 12 Test Lookout (3 Bystanders beside the
// Scheme). Mastermind draw 0 is Dr. Doom, who leads a Henchman Group.
public sealed class CardMovesTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "CardMoves");

    private const string FixtureName = "Moves Fixture";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "moves"];

    [Fact]
    public void Bystanders_moved_into_the_Hero_Deck_add_to_it_and_leave_the_Bystander_stack()
    {
        var setup = Draw(2, 8, 0);

        Assert.Equal("Test Rescue", setup.Scheme.Name);
        Assert.Equal(new HeroDeck(70, 0, 20), setup.HeroDeck);
        Assert.Equal(90, setup.HeroDeck.Total);
        Assert.Equal(2, setup.VillainDeck.Bystanders);
        Assert.Equal(8, setup.Stacks.Bystanders);
        Assert.Equal([new MovedCards(CardKind.Bystander, Pile.Bystanders, Pile.HeroDeck, 20, 20)], setup.Moves);
        Assert.Equal(new RuleNote("Scheme moves 20 Bystanders into the Hero Deck", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Fact]
    public void A_move_uses_its_Solo_value_in_Solo()
    {
        var setup = Draw(1, 6, 0);

        Assert.Equal("Test Rescue", setup.Scheme.Name);
        Assert.Equal(new HeroDeck(42, 0, 12), setup.HeroDeck);
        Assert.Equal(54, setup.HeroDeck.Total);
        Assert.Equal(17, setup.Stacks.Bystanders);
        Assert.Equal(
            new RuleNote("Scheme moves 12 Bystanders into the Hero Deck", "R p.2", "https://example.test/moves-rules.pdf", FixtureName),
            setup.Notes[0]);
    }

    [Theory]
    [InlineData(4, 13, "Test Rescue")]
    [InlineData(5, 12, "Test Henchman Army")]
    public void A_Scheme_whose_move_the_Bystander_stack_cannot_supply_is_dropped(int players, int schemes, string ninth)
    {
        // Five players put 12 Bystanders in the Villain Deck, which leaves 18 of the 30 for Test Rescue's 20.
        var random = new ScriptedRandom(8, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, random));

        Assert.Equal(schemes, random.Options[0]);
        Assert.Equal(ninth, setup.Scheme.Name);
    }

    [Fact]
    public void Henchmen_moved_into_the_Hero_Deck_leave_the_Villain_Deck()
    {
        var setup = Draw(2, 9, 0);

        Assert.Equal("Test Henchman Army", setup.Scheme.Name);
        Assert.Equal(new VillainDeck(8, 5, 16, 10, 2, 0, 6), setup.VillainDeck);
        Assert.Equal(35, setup.VillainDeck.Total);
        Assert.Equal(new HeroDeck(70, 0, 6), setup.HeroDeck);
        Assert.Equal(76, setup.HeroDeck.Total);
        Assert.Equal([new MovedCards(CardKind.Henchman, Pile.VillainDeck, Pile.HeroDeck, 6, 6)], setup.Moves);
        Assert.Equal(new RuleNote("Scheme moves 6 Henchmen into the Hero Deck", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Fact]
    public void A_Scheme_that_moves_more_Henchmen_than_Solo_draws_is_not_eligible_in_Solo()
    {
        // Solo draws 3 cards of 1 Henchman Group, too few for Test Henchman Army's 6.
        var schemes = new ScriptedRandom();
        Generator.Generate(1, Boxes, schemes);

        var drawn = Enumerable.Range(0, schemes.Options[0])
            .Select(scheme => Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(scheme))).Scheme.Name)
            .ToList();

        Assert.Equal(["Test Rescue", "Test Muster", "Test Wounded", "Test Lookout"], drawn[^4..]);
        Assert.DoesNotContain("Test Henchman Army", drawn);
    }

    [Theory]
    [InlineData(1, 7, 2, 1, 8, 3, 1)]
    [InlineData(4, 10, 8, 5, 24, 20, 8)]
    public void A_per_player_move_multiplies_by_the_player_count(
        int players, int scheme, int officers, int masterStrikes, int villainCards, int henchmanCards, int bystanders)
    {
        var setup = Draw(players, scheme, 0);

        Assert.Equal("Test Muster", setup.Scheme.Name);
        Assert.Equal(new VillainDeck(8, masterStrikes, villainCards, henchmanCards, bystanders, officers), setup.VillainDeck);
        Assert.Equal(8 + masterStrikes + villainCards + henchmanCards + bystanders + officers, setup.VillainDeck.Total);
        Assert.Equal(30 - officers, setup.Stacks.Officers);
        Assert.Equal([new MovedCards(CardKind.Officer, Pile.Officers, Pile.VillainDeck, officers, officers)], setup.Moves);
        Assert.Equal(
            new RuleNote($"Scheme moves {officers} S.H.I.E.L.D. Officers into the Villain Deck, 2 per player", "Card", null, FixtureName),
            setup.Notes[0]);
    }

    [Fact]
    public void A_move_into_the_starting_decks_puts_its_count_in_each_one()
    {
        var setup = Draw(3, 11, 0);

        Assert.Equal("Test Wounded", setup.Scheme.Name);
        Assert.Equal([new MovedCards(CardKind.Wound, Pile.Wounds, Pile.StartingDecks, 1, 3)], setup.Moves);
        Assert.Equal(27, setup.Stacks.Wounds);
        Assert.Equal(new PlayerDeck(8, 4), setup.PlayerDeck);
        Assert.Equal(new RuleNote("Scheme moves 1 Wound into each starting deck", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Fact]
    public void Cards_moved_beside_the_Scheme_leave_their_stack()
    {
        var setup = Draw(2, 12, 0);

        Assert.Equal("Test Lookout", setup.Scheme.Name);
        Assert.Equal([new MovedCards(CardKind.Bystander, Pile.Bystanders, Pile.BesideScheme, 3, 3)], setup.Moves);
        Assert.Equal(25, setup.Stacks.Bystanders);
        Assert.Equal(0, setup.TwistsBesideScheme);
        Assert.Equal(new RuleNote("Scheme moves 3 Bystanders beside it", "Card", null, FixtureName), setup.Notes[0]);
    }

    private static SetupResult Draw(int players, params int[] draws) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(draws)));
}
