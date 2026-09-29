using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Setup steps that change no counts, using Fixtures/SetupSteps: a copy of the core box and a made-up
// expansion. Catalog order puts the fixture's Schemes after the core box's, so at 2–5 players they are
// 8 Test Plain (no steps) and 9 Test Vigil (two steps, identical otherwise); Solo allows 6 core Schemes,
// so there they are 6 and 7. The Masterminds are the core box's 4 (Red Skull is 3, who leads HYDRA), then
// 4 Test Watcher, who also leads HYDRA and prints one step.
public sealed class SetupStepsTests
{
    public static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "SetupSteps");

    private const string FixtureName = "Steps Fixture";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

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

    private static SetupResult Draw(int players, params int[] draws) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(draws)));
}
