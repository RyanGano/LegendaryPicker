using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that ask for Heroes of different teams and leave the teams to the draw (setup.teamSplit), using
// Fixtures/TeamSplit: a copy of the core box and a made-up expansion with no cards but its Schemes. The core box has 6
// Avengers, 6 X-Men, 1 S.H.I.E.L.D. Hero (Nick Fury), 1 Spider Friends Hero (Spider-Man) and 1 unaffiliated (Deadpool).
// Catalog order puts the fixture's Schemes after the core box's 8 (6 in Solo): Test Rivalry (6 Heroes, 3 of one team and 3
// of another), Test Uneven Rivalry (2 Heroes of one team and 1 of another) and Test Impossible Rivalry (7 and 7, which no
// core team has). Mastermind draw 0 is Dr. Doom, who leads a Henchman Group, so a 2-player setup draws 2 Villain Groups and
// then its Heroes.
public sealed class TeamSplitTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "TeamSplit");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "split"];

    [Theory]
    [InlineData(1, 6)]
    [InlineData(2, 8)]
    [InlineData(5, 8)]
    public void The_Hero_Deck_has_3_Heroes_of_one_team_and_3_of_another(int players, int rivalry)
    {
        for (var seed = 0; seed < 20; seed++)
        {
            var setup = Assert.IsType<SetupResult>(
                Generator.Generate(players, Boxes, new ScriptedRandom([rivalry, 0, .. Enumerable.Range(0, 20).Select(i => (seed + i * 5) % 3)])));

            Assert.Equal("Test Rivalry", setup.Scheme.Name);
            Assert.Equal(
                ["core_term_avengers 3", "core_term_x-men 3"],
                setup.Heroes.GroupBy(hero => hero.Team).Select(team => $"{team.Key} {team.Count()}").Order());
            Assert.Contains(
                new RuleNote("Scheme requires 3 Heroes of one team and 3 Heroes of another", "Card", null, "Team Split Fixture"),
                setup.Notes);
        }
    }

    // Drawing the first Hero that leaves the split possible each time: Black Widow and Captain America make 2 Avengers,
    // Cyclops 1 X-Man, and Deadpool belongs to no team. Emma Frost, Gambit and the other Avengers would leave no team with
    // exactly 1 beside one with exactly 2, so the last Hero is Nick Fury, of a team the split doesn't count.
    [Fact]
    public void Heroes_beyond_the_split_come_from_other_teams()
    {
        var random = new ScriptedRandom(9, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Uneven Rivalry", setup.Scheme.Name);
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Deadpool", "Nick Fury"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal("Scheme requires 2 Heroes of one team and 1 Hero of another", setup.Notes[0].Text);
        Assert.Equal("R p.2", setup.Notes[0].Citation);
    }

    // No core team has 7 Heroes, so Test Impossible Rivalry is dropped before the draw while the other two stay.
    [Fact]
    public void A_split_no_included_team_can_fill_drops_its_Scheme()
    {
        var random = new ScriptedRandom();

        Generator.Generate(2, Boxes, random);

        Assert.Equal(10, random.Options[0]);
    }
}
