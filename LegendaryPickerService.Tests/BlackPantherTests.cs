using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Black Panther box file (Data/Boxes/black-panther.json), drawn with the core box.
public class BlackPantherTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "black-panther"];

    // Poison Lakes with Nanite Microbots adds Twists equal to the players plus 5 and sets the Wound Stack to 30.
    [Theory]
    [InlineData(1, 6)]
    [InlineData(4, 9)]
    public void Poison_Lakes_adds_players_plus_5_Twists_and_sets_30_Wounds(int players, int twists)
    {
        var setup = Draw(players, "Poison Lakes with Nanite Microbots");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Equal(30, setup.Stacks.Wounds);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Poison Lakes with Nanite Microbots");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Poison Lakes with Nanite Microbots", "Klaw");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Poison Lakes with Nanite Microbots's Twists"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
