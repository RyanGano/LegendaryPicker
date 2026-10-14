using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Black Widow box file (Data/Boxes/black-widow.json), drawn with the core box.
public class BlackWidowTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "black-widow"];

    // Twists are 8 minus 1 per player, and the 8 Officers the Scheme adds to the Villain Deck leave 22 of the core box's 30.
    [Fact]
    public void Train_Black_Widows_takes_a_Twist_per_player_off_8_and_moves_8_Officers_into_the_Villain_Deck()
    {
        var setup = Draw(3, "Train Black Widows in the Red Room");

        Assert.Equal(5, setup.VillainDeck.Twists);
        Assert.Equal([new MovedCards(CardKind.Officer, Pile.Officers, Pile.VillainDeck, 8, 8)], setup.Moves);
        Assert.Equal(22, setup.Stacks.Officers);
    }

    // The Scheme's printed 6 Heroes win over the 2-player table's 5.
    [Fact]
    public void Frame_Heroes_for_Murder_uses_6_Heroes()
    {
        Assert.Equal(6, Draw(2, "Frame Heroes for Murder").Heroes.Count);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Sniper Rifle Assassins");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Sniper Rifle Assassins", "Taskmaster");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Sniper Rifle Assassins's Twists"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
