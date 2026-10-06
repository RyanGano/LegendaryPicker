using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Realm of Kings box file (Data/Boxes/realm-of-kings.json), drawn with the core box.
public class RealmOfKingsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "realm-of-kings"];

    // Ruin the Perfect Wedding sets two extra Heroes aside as 14-card Wedding Hero stacks, outside the Hero Deck.
    [Fact]
    public void Ruin_the_Perfect_Wedding_sets_two_Wedding_Heroes_aside()
    {
        var setup = Draw(2, "Ruin the Perfect Wedding");

        Assert.Equal(2, setup.OutsideHeroes.Count);
        Assert.All(setup.OutsideHeroes, outside => Assert.Equal((Pile.SetAside, 14), (outside.To, outside.Cards)));
        Assert.Empty(setup.Heroes.Select(hero => hero.Name).Intersect(setup.OutsideHeroes.Select(outside => outside.Hero.Name)));
    }

    // Devolve with Xerogen Crystals adds Twists equal to the players plus 8 and an extra 10-card Henchman Group even in Solo.
    [Theory]
    [InlineData(1, 9, 13)]
    [InlineData(4, 12, 30)]
    public void Devolve_with_Xerogen_Crystals_adds_players_plus_8_Twists_and_a_10_card_Henchman_Group(int players, int twists, int henchmen)
    {
        var setup = Draw(players, "Devolve with Xerogen Crystals");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Equal(henchmen, setup.VillainDeck.HenchmanCards);
    }

    // War of Kings takes Officers from the S.H.I.E.L.D. Officer Stack, so the setup lays the stack out.
    [Fact]
    public void War_of_Kings_lays_out_the_Officer_stack()
    {
        Assert.NotNull(Draw(3, "War of Kings").Stacks.Officers);
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
