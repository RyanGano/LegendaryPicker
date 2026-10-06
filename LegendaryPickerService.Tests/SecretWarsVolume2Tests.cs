using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Secret Wars Volume 2 box file (Data/Boxes/secret-wars-volume-2.json), drawn with the core box. Catalog order puts its cards after the core box's,
// so at 2–5 players its Schemes are 8 Deadlands Hordes Charge the Wall to 15 Sinister Ambitions; Solo allows 6 core
// Schemes, so there they are 6 to 13. Its Masterminds are 4 Immortal Emperor Zheng-Zhu, 5 King Hyperion, 6 Shiklah and
// 7 Spider-Queen.
public class SecretWarsVolume2Tests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "secret-wars-volume-2"];

    private static readonly string[] SchemeNames =
    [
        "Deadlands Hordes Charge the Wall",
        "Enthrone the Barons of Battleworld",
        "The Fountain of Eternal Life",
        "The God-Emperor of Battleworld",
        "The Mark of Khonshu",
        "Master the Mysteries of Kung-Fu",
        "Secret Wars",
        "Sinister Ambitions",
    ];

    private const int Zheng = 4;

    [Fact]
    public void The_Mark_of_Khonshu_always_includes_the_Khonshu_Guardians_and_adds_a_Hero_to_the_Villain_Deck()
    {
        var setup = Draw(2, "The Mark of Khonshu", Zheng);

        Assert.Contains("Khonshu Guardians", setup.HenchmanGroups.Select(group => group.Name));
        var extra = Assert.Single(setup.OutsideHeroes);
        Assert.Equal((Pile.VillainDeck, 14), (extra.To, extra.Cards));
        Assert.Equal(14, setup.VillainDeck.OutsideHeroCards);
        Assert.DoesNotContain(extra.Hero, setup.Heroes);
    }

    // With both Secret Wars boxes included, their Ambition cards form one supply, and Sinister Ambitions still moves 10.
    [Fact]
    public void Sinister_Ambitions_draws_from_the_Ambition_cards_of_both_Secret_Wars_boxes()
    {
        var both = Assert.IsType<SetupResult>(
            Generator.Generate(2, ["core", "secret-wars-volume-1", "secret-wars-volume-2"], new ScriptedRandom(23, 4)));

        Assert.Equal("Sinister Ambitions", both.Scheme.Name);
        Assert.Equal(new VillainDeck(6, 5, 16, 10, 2, 10), both.VillainDeck);
    }

    [Fact]
    public void Both_Jean_Greys_can_be_in_one_setup_one_in_the_Hero_Deck_and_one_outside_it()
    {
        string[] boxes = ["core", "dark-city", "secret-wars-volume-2"];
        var heroes = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).SelectMany(box => box.Heroes).ToList();
        var jeanGrey = heroes.FindIndex(hero => hero.Name == "Jean Grey");

        // The first Hero drawn is Dark City's Jean Grey; the one left outside the Hero Deck is Time-Traveling Jean Grey.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, new ScriptedRandom(14, 0, 0, 0, jeanGrey, 0, 0, 0, 0, 0)));

        Assert.Equal("Transform Citizens Into Demons", setup.Scheme.Name);
        Assert.Contains("Jean Grey", setup.Heroes.Select(hero => hero.Name));
        Assert.Equal("Time-Traveling Jean Grey", Assert.Single(setup.OutsideHeroes).Hero.Name);
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }
}
