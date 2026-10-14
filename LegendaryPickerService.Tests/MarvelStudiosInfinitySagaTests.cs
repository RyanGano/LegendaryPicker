using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Studios' The Infinity Saga box file (Data/Boxes/marvel-studios-the-infinity-saga.json), drawn with the core box.
public class MarvelStudiosInfinitySagaTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "marvel-studios-the-infinity-saga"];

    // The Time Heist's printed 11 Twists and its 4 + 4 Heroes (the Hero Deck and the Past Hero Deck) win over the 2-player table.
    [Fact]
    public void The_Time_Heist_uses_8_Heroes_and_11_Twists()
    {
        var setup = Draw(2, "The Time Heist", "Ebony Maw");

        Assert.Equal(8, setup.Heroes.Count);
        Assert.Equal(11, setup.VillainDeck.Twists);
    }

    // Sacrifice for the Soul Stone prints Twists equal to the players plus 4.
    [Fact]
    public void Sacrifice_for_the_Soul_Stone_has_the_players_plus_4_Twists()
    {
        Assert.Equal(8, Draw(4, "Sacrifice for the Soul Stone", "Ebony Maw").VillainDeck.Twists);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Halve All Life in the Universe");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Halve All Life in the Universe", "Ebony Maw");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Halve All Life in the Universe's Twists"));
    }

    // Draws the Scheme and Mastermind by name: the box's follow the core box's in catalog order.
    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var included = Catalog.Boxes.Where(box => Boxes.Contains(box.Id)).ToList();
        var s = included.SelectMany(box => box.AllSchemes)
            .Where(candidate => candidate.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = included.SelectMany(box => box.AllMasterminds).ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal(mastermind, setup.Mastermind.Name);
        return setup;
    }
}
