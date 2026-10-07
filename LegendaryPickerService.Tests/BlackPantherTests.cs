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

    private static SetupResult Draw(int players, string scheme)
    {
        var probe = new ScriptedRandom();
        Generator.Generate(players, Boxes, probe);
        var index = Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s))).Scheme.Name == scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
    }
}
