using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Dark City box file (Data/Boxes/dark-city.json), drawn with the core box. Catalog order puts Dark City's cards after the core box's, so at
// 2–5 players its Schemes are 8 Capture Baby Hope, 9 Detonate the Helicarrier, 10 Massive Earthquake
// Generator, 11 Organized Crime Wave, 12 Save Humanity, 13 Steal the Weaponized Plutonium, 14 Transform
// Citizens Into Demons and 15 X-Cutioner's Song; Solo allows 6 core Schemes, so there they are 6 to 13.
// Its Masterminds are 4 Apocalypse, 5 Kingpin, 6 Mephisto, 7 Mr. Sinister and 8 Stryfe. Together the
// boxes hold 30 + 11 Bystanders.
public class DarkCityTests
{
    private const string DarkCityName = "Dark City";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box DarkCity = Catalog.Boxes.Single(box => box.Id == "dark-city");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "dark-city"];

    private static readonly string[] SchemeNames =
    [
        "Capture Baby Hope",
        "Detonate the Helicarrier",
        "Massive Earthquake Generator",
        "Organized Crime Wave",
        "Save Humanity",
        "Steal the Weaponized Plutonium",
        "Transform Citizens Into Demons",
        "X-Cutioner's Song",
    ];

    private const int Mephisto = 6;

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Mephisto (who leads Underworld,
    [Fact]
    public void Detonate_the_Helicarrier_uses_6_Heroes_even_in_Solo()
    {
        var two = Draw(2, "Detonate the Helicarrier");
        var solo = Draw(1, "Detonate the Helicarrier");

        Assert.Equal(6, two.Heroes.Count);
        Assert.Contains(new RuleNote("Scheme uses 6 Heroes", "Card", null, DarkCityName), two.Notes);
        Assert.Equal(6, solo.Heroes.Count);
        Assert.Contains(
            new RuleNote("Scheme overrides Solo: 6 Heroes", "D2", "https://boardgamegeek.com/thread/884926", CoreName), solo.Notes);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Transform_Citizens_Into_Demons_puts_Jean_Grey_in_the_Villain_Deck_and_no_Bystanders(int players)
    {
        var setup = Draw(players, "Transform Citizens Into Demons");

        var jeanGrey = DarkCity.Heroes.Single(hero => hero.Name == "Jean Grey");
        Assert.Equal([new OutsideHero(jeanGrey, Pile.VillainDeck, 14)], setup.OutsideHeroes);
        Assert.DoesNotContain(jeanGrey, setup.Heroes);
        Assert.Contains(
            new RuleNote("Scheme draws 1 extra Jean Grey or Time-Traveling Jean Grey Hero outside the Hero Deck and puts its cards into the Villain Deck", "Card", null, DarkCityName),
            setup.Notes);
    }

    [Fact]
    public void X_Cutioners_Song_puts_a_Hero_from_outside_the_Hero_Deck_in_the_Villain_Deck()
    {
        var setup = Draw(2, "X-Cutioner's Song");

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal(new OutsideHero(outside.Hero, Pile.VillainDeck, 14), outside);
        Assert.DoesNotContain(outside.Hero, setup.Heroes);
        Assert.Contains(new RuleNote("Scheme puts 0 Bystanders in the Villain Deck", "Card", null, DarkCityName), setup.Notes);
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, Mephisto)));
    }
}
