using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Revelations box file (Data/Boxes/revelations.json), drawn with the core box.
public class RevelationsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "revelations"];

    [Fact]
    public void House_of_M_draws_4_Heroes_of_one_team_and_2_of_others_and_puts_Scarlet_Witch_in_the_Villain_Deck()
    {
        var setup = Draw(2, "House of M");

        Assert.Equal(6, setup.Heroes.Count);
        var team = Assert.Single(setup.Heroes.GroupBy(hero => hero.Team), heroes => heroes.Count() == 4).Key;
        Assert.Equal(2, setup.Heroes.Count(hero => !hero.HasTeam(team!)));
        var witch = Assert.Single(setup.OutsideHeroes);
        Assert.Equal(("Scarlet Witch", Pile.VillainDeck, 14), (witch.Hero.Name, witch.To, witch.Cards));
        Assert.Equal(8, setup.VillainDeck.Twists);
    }

    [Theory]
    [InlineData(1, 7)]
    [InlineData(3, 9)]
    [InlineData(5, 11)]
    public void Secret_HYDRA_Corruption_sets_its_Twists_by_player_count_and_caps_the_Officer_stack_at_30(int players, int twists)
    {
        var setup = Draw(players, "Secret HYDRA Corruption");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Contains("Use exactly 30 Officers in the S.H.I.E.L.D. Officer stack", setup.Steps);
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
