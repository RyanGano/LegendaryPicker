using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Setup effects that add to a count, using Fixtures/SetupEffects: a copy of the core box and a
// made-up expansion. Catalog order puts the fixture's cards after the core box's, so at 2–5 players the
// Schemes are the core box's 8, then 8 Test Uprising (+1 Villain Group), 9 Test Recruitment (+1 Hero)
// and 10 Test Crowd (+1 Bystander in Solo, +2 at 3–5 players, nothing at 2), then 11 Test Muster (4 Henchmen of each
// Henchman Group). Solo allows 6 core Schemes, so there they are 6, 7, 8 and 9. The Masterminds are the core box's 4,
// then 4 Test Recruiter, who always leads the fixture's Test Recruits and adds 1 Hero, and 5 Test Swarm Lord, who
// puts 6 Henchmen of each Henchman Group in the Villain Deck in Solo.
public sealed class SetupEffectsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "SetupEffects");

    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string FixtureName = "Effects Fixture";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "effects"];

    [Theory]
    [InlineData(2, 3)]
    [InlineData(5, 5)]
    public void A_Scheme_can_add_a_Villain_Group(int players, int villainGroups)
    {
        // Test Uprising with Dr. Doom, who leads a Henchman Group, so every Villain Group is drawn.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(8, 0)));

        Assert.Equal("Test Uprising", setup.Scheme.Name);
        Assert.Equal(villainGroups, setup.VillainGroups.Count);
        Assert.Equal(villainGroups * 8, setup.VillainDeck.VillainCards);
        Assert.Equal(new RuleNote("Scheme adds 1 Villain Group", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Fact]
    public void A_Scheme_that_adds_a_Hero_adds_it_to_the_Solo_count()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(7, 0)));

        Assert.Equal("Test Recruitment", setup.Scheme.Name);
        Assert.Equal(4, setup.Heroes.Count);
        Assert.Equal(new HeroDeck(56, 0), setup.HeroDeck);
        Assert.Equal(
            [
                new RuleNote("Scheme adds 1 Hero", "Card", null, FixtureName),
                new RuleNote("Solo ignores Dr. Doom's Always Leads", "R p.20", Rulebook, CoreName),
                new RuleNote("Solo: After each Twist, KO a Hero costing 6 or less from the HQ", "R p.20", Rulebook, CoreName),
            ],
            setup.Notes);
    }

    [Fact]
    public void A_Scheme_that_adds_a_Hero_adds_it_to_the_multiplayer_count()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(9, 0)));

        Assert.Equal("Test Recruitment", setup.Scheme.Name);
        Assert.Equal(6, setup.Heroes.Count);
        Assert.Equal(new HeroDeck(84, 0), setup.HeroDeck);
        Assert.Equal(new RuleNote("Scheme adds 1 Hero", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Theory]
    [InlineData(1, 8, 2)]
    [InlineData(3, 10, 10)]
    [InlineData(4, 10, 10)]
    [InlineData(5, 10, 14)]
    public void An_effect_applies_only_at_the_player_counts_it_names(int players, int scheme, int bystanders)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(scheme, 0)));

        Assert.Equal("Test Crowd", setup.Scheme.Name);
        Assert.Equal(bystanders, setup.VillainDeck.Bystanders);
        Assert.Equal(30 - bystanders, setup.Stacks.Bystanders);
        Assert.Equal(
            players == 1
                ? new RuleNote("Scheme adds 1 Bystander to the Villain Deck", "R p.2", "https://example.test/effects-rules.pdf", FixtureName)
                : new RuleNote("Scheme adds 2 Bystanders to the Villain Deck", "Card", null, FixtureName),
            setup.Notes[0]);
    }

    [Fact]
    public void An_effect_does_nothing_at_a_player_count_it_does_not_name()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(10, 0)));

        Assert.Equal("Test Crowd", setup.Scheme.Name);
        Assert.Equal(2, setup.VillainDeck.Bystanders);
        Assert.Equal([new RuleNote("Dr. Doom always leads Doombot Legion", "R p.6", Rulebook, CoreName)], setup.Notes);
    }

    [Fact]
    public void A_Mastermind_can_add_a_Hero()
    {
        // Unleash the Power of the Cosmic Cube changes no counts.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(7, 4)));

        Assert.Equal("Test Recruiter", setup.Mastermind.Name);
        Assert.Equal(6, setup.Heroes.Count);
        Assert.Equal(
            [
                new RuleNote("Test Recruiter adds 1 Hero", "Card", null, FixtureName),
                new RuleNote("Test Recruiter always leads Test Recruits", "R p.6", Rulebook, CoreName),
            ],
            setup.Notes);
    }

    [Fact]
    public void A_Masterminds_effect_applies_after_the_Schemes()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(9, 4)));

        Assert.Equal(7, setup.Heroes.Count);
        Assert.Equal(
            ["Scheme adds 1 Hero", "Test Recruiter adds 1 Hero", "Test Recruiter always leads Test Recruits"],
            setup.Notes.Select(note => note.Text));
    }

    [Fact]
    public void A_Mastermind_can_set_the_Solo_Henchman_count()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, 5)));

        Assert.Equal("Test Swarm Lord", setup.Mastermind.Name);
        Assert.Equal(6, setup.VillainDeck.HenchmanCards);
        Assert.Contains(
            new RuleNote("Test Swarm Lord puts 6 Henchmen of each Henchman Group in the Villain Deck", "Card", null, FixtureName),
            setup.Notes);
    }

    [Fact]
    public void A_Schemes_Henchman_count_wins_over_the_Masterminds()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(9, 5)));

        Assert.Equal("Test Muster", setup.Scheme.Name);
        Assert.Equal(4, setup.VillainDeck.HenchmanCards);
        Assert.DoesNotContain(setup.Notes, note => note.Text.StartsWith("Test Swarm Lord puts"));
    }
}
