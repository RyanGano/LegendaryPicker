using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Ant-Man box file (Data/Boxes/ant-man.json), drawn with the core box.
public class AntManTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "ant-man"];

    // The Scheme's Twist count is the number of players plus 6 at every count, Solo included (a Scheme's printed count wins over Solo's).
    [Theory]
    [InlineData(1, 7)]
    [InlineData(3, 9)]
    [InlineData(5, 11)]
    public void Giant_Ants_has_6_more_Twists_than_players(int players, int twists)
    {
        var setup = Draw(players, "Transform Commuters into Giant Ants");

        Assert.Equal(twists, setup.VillainDeck.Twists);
    }

    [Fact]
    public void Trap_Heroes_in_the_Microverse_puts_an_extra_Hero_with_its_14_cards_in_the_Villain_Deck()
    {
        var setup = Draw(2, "Trap Heroes in the Microverse");

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal((Pile.VillainDeck, 14), (outside.To, outside.Cards));
        Assert.DoesNotContain(outside.Hero, setup.Heroes);
        Assert.Equal(11, setup.VillainDeck.Twists);
    }

    // "4-5 players: Add another Hero" on top of the table's Hero count.
    [Theory]
    [InlineData(3, 5)]
    [InlineData(4, 6)]
    public void Age_of_Ultron_adds_a_Hero_only_at_4_and_5_players(int players, int heroes)
    {
        Assert.Equal(heroes, Draw(players, "Age of Ultron").Heroes.Count);
    }

    // Its own Scheme and Mastermind are tagged for difficulty (#269); its Heroes are in the same draw.
    [Fact]
    public void A_draw_of_its_own_Scheme_and_Mastermind_rates_with_reasons_naming_them()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Age of Ultron");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Age of Ultron", "Ultron");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Age of Ultron's Twists"));
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Ultron's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
