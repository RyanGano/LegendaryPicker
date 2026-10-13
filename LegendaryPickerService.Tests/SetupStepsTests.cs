using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Setup steps that change no counts, using Fixtures/SetupSteps: a copy of the core box and a made-up
// expansion. Catalog order puts the fixture's Schemes after the core box's, so at 2–5 players they are
// 8 Test Plain (no steps) and 9 Test Vigil (two steps, identical otherwise); Solo allows 6 core Schemes,
// so there they are 6 and 7. The Masterminds are the core box's 4 (Red Skull is 3, who leads HYDRA), then
// 4 Test Watcher, who leads the fixture's Test Lookouts and prints one step.
public sealed class SetupStepsTests
{
    public static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "SetupSteps");

    private const string FixtureName = "Steps Fixture";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "steps"];

    [Fact]
    public void A_Schemes_setup_steps_are_listed_with_a_note_each()
    {
        var setup = Draw(2, 9, 3);

        Assert.Equal("Test Vigil", setup.Scheme.Name);
        Assert.Equal(["Place the test beacon token on the Scheme", "Split the Villain Deck into two equal piles"], setup.Steps);
        Assert.Equal(
            [
                new RuleNote("Scheme adds a setup step: Place the test beacon token on the Scheme", "Card", null, FixtureName),
                new RuleNote(
                    "Scheme adds a setup step: Split the Villain Deck into two equal piles", "R p.2", "https://example.test/steps-rules.pdf", FixtureName),
            ],
            setup.Notes.Take(2));
    }

    [Theory]
    [InlineData(1, 6, 7)]
    [InlineData(2, 8, 9)]
    [InlineData(5, 8, 9)]
    public void Setup_steps_change_no_counts(int players, int plain, int vigil)
    {
        var without = Draw(players, plain, 3);
        var with = Draw(players, vigil, 3);

        Assert.Empty(without.Steps);
        Assert.Equal(2, with.Steps.Count);
        Assert.Equal(without.VillainGroups, with.VillainGroups);
        Assert.Equal(without.HenchmanGroups, with.HenchmanGroups);
        Assert.Equal(without.Heroes, with.Heroes);
        Assert.Equal(without.VillainDeck, with.VillainDeck);
        Assert.Equal(without.HeroDeck, with.HeroDeck);
        Assert.Equal(without.TwistsBesideScheme, with.TwistsBesideScheme);
        Assert.Equal(without.Stacks, with.Stacks);
        Assert.Equal(without.PlayerDeck, with.PlayerDeck);
        Assert.Equal(without.Moves, with.Moves);
        Assert.Equal(without.OutsideHeroes, with.OutsideHeroes);
    }

    [Fact]
    public void A_Masterminds_setup_step_follows_the_Schemes()
    {
        var setup = Draw(2, 9, 4);

        Assert.Equal("Test Watcher", setup.Mastermind.Name);
        Assert.Equal(
            [
                "Place the test beacon token on the Scheme",
                "Split the Villain Deck into two equal piles",
                "Put the test lookout token on the first city space",
            ],
            setup.Steps);
        Assert.Equal(
            new RuleNote("Test Watcher adds a setup step: Put the test lookout token on the first city space", "Card", null, FixtureName),
            setup.Notes[2]);
    }

    [Fact]
    public void A_Masterminds_setup_step_changes_no_counts()
    {
        var redSkull = Draw(3, 8, 3);
        var watcher = Draw(3, 8, 4);

        Assert.Equal("Red Skull", redSkull.Mastermind.Name);
        Assert.Empty(redSkull.Steps);
        Assert.Equal(["Put the test lookout token on the first city space"], watcher.Steps);
        Assert.Equal(redSkull.VillainDeck, watcher.VillainDeck);
        Assert.Equal(redSkull.HeroDeck, watcher.HeroDeck);
        Assert.Equal(redSkull.Stacks, watcher.Stacks);
        Assert.Equal(redSkull.PlayerDeck, watcher.PlayerDeck);
    }

    // A step that would leave a Hero to the players names one the app draws, from the included Heroes the setup doesn't
    // already use, with its box since the setup has two (#242). Test Lottery is the 10th Scheme at 2 players; every later
    // draw takes the first option.
    [Fact]
    public void A_step_that_leaves_a_Hero_to_the_player_names_one_drawn_outside_the_setup()
    {
        var random = new ScriptedRandom(10, 3);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        var unused = IncludedHeroes.Except(setup.Heroes.Select(hero => hero.Name)).ToList();
        Assert.Equal("Test Lottery", setup.Scheme.Name);
        Assert.Equal(unused.Count, random.Options[^1]);
        Assert.Equal([$"If the test coin lands heads, add {unused[0]} (from {CoreName}) to the Hero Deck"], setup.Steps);
        Assert.Equal(
            "Scheme adds a setup step: If the test coin lands heads, add an extra Hero to the Hero Deck",
            setup.Notes[0].Text);
    }

    // The extra Hero joins the Hero Deck, so it keeps the Scheme's Hero rules: with exactly 1 X-Men Hero allowed and one
    // already drawn, it is never another X-Men Hero (#242). Test Census is the 12th Scheme at 2 players.
    [Fact]
    public void A_drawn_extra_Hero_keeps_the_Schemes_Hero_rules()
    {
        var random = new ScriptedRandom(12, 3);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        var xMen = Catalog.Boxes.SelectMany(box => box.Heroes).Where(hero => hero.HasTeam("core_term_x-men")).Select(hero => hero.Name).ToList();
        var unused = IncludedHeroes.Except(setup.Heroes.Select(hero => hero.Name)).Except(xMen).ToList();
        Assert.Equal("Test Census", setup.Scheme.Name);
        Assert.Equal(unused.Count, random.Options[^1]);
        Assert.Equal([$"If the test coin lands heads, add {unused[0]} (from {CoreName}) to the Hero Deck"], setup.Steps);
    }

    // A Scheme that has each player choose a Hero (#259) draws the Hero Deck at its normal size as if no one chose, and marks
    // the last Heroes, one per player, as fill-ins. Test Homecoming is the 11th Scheme at 3 players.
    [Fact]
    public void A_Scheme_that_has_players_choose_Heroes_lists_the_normal_Hero_Deck_and_marks_one_fill_in_per_player()
    {
        var setup = Draw(3, 11, 3);
        var plain = Draw(3, 8, 3);

        Assert.Equal("Test Homecoming", setup.Scheme.Name);
        Assert.Equal(plain.Heroes.Count, setup.Heroes.Count);
        Assert.Equal(3, setup.FillInHeroes);
        Assert.Equal(["Each player chooses a different Hero for the Hero Deck, listed or not"], setup.Steps);
        Assert.Contains(setup.Notes, note => note.Text == "Scheme has each player choose a different Hero for the Hero Deck; the last 3 Heroes listed join it only in place of an earlier listed Hero a player chose");
        Assert.Equal(0, plain.FillInHeroes);
    }

    private static IEnumerable<string> IncludedHeroes => Catalog.Boxes.SelectMany(box => box.Heroes).Select(hero => hero.Name);

    private static string CoreName => Catalog.Boxes.Single(box => box.Id == "core").Name;

    private static SetupResult Draw(int players, params int[] draws) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(draws)));
}
