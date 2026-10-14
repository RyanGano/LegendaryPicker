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

    // Its own Scheme and Mastermind are tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_and_Mastermind_rates_with_reasons_naming_them()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Tornado of Terrigen Mists");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Tornado of Terrigen Mists", "Emperor Vulcan of the Shi'ar");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Tornado of Terrigen Mists's Twists"));
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Emperor Vulcan of the Shi'ar's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
