using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Heroes that count under more than one Hero Name or team (#149), using Fixtures/HeroIdentity: a copy of the core box and
// a made-up expansion. Catalog order puts the fixture's Heroes after the core box's 15: Test Storm & Panther (a Divided
// Card with the Hero Names Test Storm and Test Panther, X-Men on its left halves and Avengers on its right), Test
// Colossus & Wolverine (Hero Names Test Colossus and Wolverine, X-Men) and Test Wolverine Noir (a version of Wolverine,
// X-Men). At 2 players the fixture's Schemes are 8 Test Claws (1 extra Wolverine Hero into the Villain Deck), 9 Test Pair
// (2 Heroes: exactly 1 Avenger and exactly 1 X-Man) and 10 Test Rivalry (6 Heroes, 3 of one team and 3 of another).
// Mastermind draw 0 is Dr. Doom, who leads a Henchman Group, so a 2-player setup draws 2 Villain Groups and then its Heroes.
public sealed class HeroIdentityTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "HeroIdentity");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "identity"];

    // The core box's Wolverine, the Divided Card with Wolverine on its right halves, and Wolverine's Noir version all
    // have the Hero Name Wolverine.
    [Theory]
    [InlineData(0, "Wolverine")]
    [InlineData(1, "Test Colossus & Wolverine")]
    [InlineData(2, "Test Wolverine Noir")]
    public void A_Hero_Name_draw_takes_either_name_of_a_Divided_Card_and_any_version(int pick, string expected)
    {
        var random = new ScriptedRandom(8, 0, 0, 0, 0, 0, 0, 0, 0, pick);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Claws", setup.Scheme.Name);
        Assert.Equal(3, random.Options[^1]);
        Assert.Equal(expected, Assert.Single(setup.OutsideHeroes).Hero.Name);
    }

    // Test Storm & Panther, drawn first, is the Avenger beside an X-Man or the X-Man beside an Avenger, but never both: the
    // second Hero can be any of the other 14 Avengers and X-Men, and not Deadpool, Nick Fury or Spider-Man, which a Hero
    // counting as both teams would leave as the only choices.
    [Theory]
    [InlineData(0, "Black Widow")]
    [InlineData(2, "Cyclops")]
    public void A_Hero_with_two_teams_meets_either_teams_count_but_not_both(int pick, string second)
    {
        var random = new ScriptedRandom(9, 0, 0, 0, 12, pick);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Pair", setup.Scheme.Name);
        Assert.Equal([15, 14], random.Options[^2..]);
        Assert.Equal(["Test Storm & Panther", second], setup.Heroes.Select(hero => hero.Name));
    }

    // Test Storm & Panther and 3 X-Men leave the X-Men side full, so Test Storm & Panther fills the Avengers side and the
    // last two Heroes can only be Avengers.
    [Fact]
    public void A_Hero_with_two_teams_fills_either_side_of_a_team_split()
    {
        var random = new ScriptedRandom(10, 0, 0, 0, 12, 2, 2, 2);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Rivalry", setup.Scheme.Name);
        Assert.Equal(
            ["Test Storm & Panther", "Cyclops", "Emma Frost", "Gambit", "Black Widow", "Captain America"],
            setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([6, 5], random.Options[^2..]);
    }
    // A two-team Hero whose sides meet the Scheme's team counts alike is still either team to a draw outside the Hero Deck
    // by team. Here the core box's Spider-Man is also an Avenger, and Legacy Virus counts X-Men and draws 7 Avengers
    // outside the Hero Deck: the core box's 6 Avengers and Spider-Man.
    [Fact]
    public void A_Hero_with_two_teams_is_either_team_to_a_draw_outside_the_Hero_Deck()
    {
        var directory = Directory.CreateTempSubdirectory("legendary-identity-").FullName;
        try
        {
            var core = JsonNode.Parse(File.ReadAllText(Path.Combine(FixtureDirectory, "core.json")))!.AsObject();
            core["heroes"]!.AsArray().Single(hero => (string?)hero!["id"] == "core_hero_spider-man")!["alsoTeam"] = "core_term_avengers";
            var schemes = core["schemes"]!.AsArray();
            var scheme = schemes.Single(scheme => (string?)scheme!["id"] == "core_scheme_legacy-virus")!.DeepClone();
            schemes.Clear();
            schemes.Add(scheme);
            scheme["setup"]!["heroCounts"] = JsonNode.Parse("""[{ "team": "core_term_x-men", "atLeast": 1, "source": "Card" }]""");
            scheme["setup"]!["outsideHeroes"] = JsonNode.Parse(
                """[{ "to": "villainDeck", "team": "core_term_avengers", "count": [{ "players": null, "value": 7, "source": "Card" }] }]""");
            File.WriteAllText(Path.Combine(directory, "core.json"), core.ToJsonString());

            var setup = Assert.IsType<SetupResult>(new SetupGenerator(BoxCatalog.Load(directory)).Generate(2, ["core"], new ScriptedRandom()));

            Assert.Contains("Spider-Man", setup.OutsideHeroes.Select(outside => outside.Hero.Name));
            Assert.Equal(7, setup.OutsideHeroes.Count);
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }
}
