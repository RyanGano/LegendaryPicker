using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Into the Cosmos box file (Data/Boxes/into-the-cosmos.json), drawn with the core box.
public class IntoTheCosmosTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "into-the-cosmos"];

    // Turn the Soul of Adam Warlock sets all 14 Adam Warlock cards aside as a stack, so he is never in the Hero Deck.
    [Fact]
    public void Turn_the_Soul_of_Adam_Warlock_sets_Adam_Warlock_aside()
    {
        var setup = Draw(2, "Turn the Soul of Adam Warlock");

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal(("Adam Warlock", Pile.SetAside, 14), (outside.Hero.Name, outside.To, outside.Cards));
        Assert.DoesNotContain(setup.Heroes, hero => hero.Name == "Adam Warlock");
        Assert.Equal(14, setup.VillainDeck.Twists);
    }

    // Destroy the Nova Corps takes exactly one Nova Hero and adds 2 Wounds, an Officer and a Nova card from the
    // Hero Deck to each starting deck, so the 3-player Hero Deck is 3 cards short.
    [Fact]
    public void Destroy_the_Nova_Corps_takes_one_Nova_and_seeds_the_starting_decks()
    {
        var setup = Draw(3, "Destroy the Nova Corps");

        Assert.Single(setup.Heroes, hero => hero.HasHeroName("Nova"));
        Assert.Contains(setup.Moves, move => move is { Card: CardKind.Wound, To: Pile.StartingDecks, Count: 2 });
        Assert.Contains(setup.Moves, move => move is { Card: CardKind.Officer, To: Pile.StartingDecks, Count: 1 });
        Assert.Contains(setup.Moves, move => move is { Card: CardKind.Hero, To: Pile.StartingDecks, Count: 1 });
        Assert.Equal(5 * 14 - 3, setup.HeroDeck.Total);
        Assert.NotNull(setup.Stacks.Shards);
    }

    // Solo, Destroy the Nova Corps uses 5 Heroes instead of Solo's 3.
    [Fact]
    public void Destroy_the_Nova_Corps_uses_five_Heroes_in_Solo()
    {
        Assert.Equal(5, Draw(1, "Destroy the Nova Corps").Heroes.Count);
    }

    // Its own Mastermind is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_Magus_rates_with_a_reason_naming_him()
    {
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Annihilation: Conquest", "Magus");
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Annihilation: Conquest");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Magus's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
