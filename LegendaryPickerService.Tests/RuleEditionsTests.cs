using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Drawing boxes of any rulesets together under one rule edition (owner ruling D-editions, 2026-10-13, #240), using
// Fixtures/Editions: copies of the core box and Legendary: Villains, the Revised fixture base game (16 Bystanders and a
// Warmup Round with 5 players, 30 Wounds and 8 Officers), and two made-up expansions with no stacks of their own: Heroic
// Fixture (First Edition: Test Uprising, Test Baron) and Revised Extra Fixture (Revised: Test Blockade, Test Admiral).
// Both Masterminds use Wounds. The core box puts 12 Bystanders in the Villain Deck with 5 players and has no Warmup Round.
public sealed class RuleEditionsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Editions");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private const string Warmup = "Warmup Round: on each player's first turn, no card is played from the Villain Deck";

    private static SetupResult Draw(string[] boxes, Ruleset? rules, params int[] draws) =>
        Assert.IsType<SetupResult>(Generator.Generate(5, boxes, new ScriptedRandom(draws), rules: rules));

    // Test Uprising is Scheme 0 and Test Baron Mastermind 0. Test Baron is drawable because the Revised base game's Wounds
    // serve a First Edition card.
    [Fact]
    public void A_First_Edition_expansion_with_a_Revised_base_game_follows_Revised_rules_unless_the_player_chooses_First_Edition()
    {
        string[] boxes = ["revised", "heroic"];

        var revised = Draw(boxes, null, 0, 0);
        var firstEdition = Draw(boxes, Ruleset.FirstEdition, 0, 0);

        Assert.Equal(("Test Uprising", "Test Baron"), (revised.Scheme.Name, revised.Mastermind.Name));
        Assert.Equal((Ruleset.Revised, 16, 30), (revised.Ruleset, revised.VillainDeck.Bystanders, revised.Stacks.Wounds));
        Assert.Contains(revised.Notes, note => note.Text == Warmup);
        Assert.Equal(new RuleNote("Revised rules: a Revised base game is included", "D-editions", null), revised.RulesReason);

        // The core box's rules apply without its cards: the Revised base game's Officers are the recruit stack.
        Assert.Equal((Ruleset.FirstEdition, 12, 8), (firstEdition.Ruleset, firstEdition.VillainDeck.Bystanders, firstEdition.Stacks.Officers));
        Assert.DoesNotContain(firstEdition.Notes, note => note.Text == Warmup || note.Text.StartsWith("No included box has"));
        Assert.Equal(new RuleNote("First Edition rules: chosen by the player", "D-editions", null), firstEdition.RulesReason);
    }

    // Test Blockade is Scheme 8, after the core box's 8, and Test Admiral Mastermind 4, after its 4.
    [Fact]
    public void A_Revised_expansion_with_a_First_Edition_base_game_follows_First_Edition_rules_unless_the_player_chooses_Revised()
    {
        string[] boxes = ["core", "revised-extra"];

        var firstEdition = Draw(boxes, null, 8, 4);
        var revised = Draw(boxes, Ruleset.Revised, 8, 4);

        Assert.Equal(("Test Blockade", "Test Admiral"), (firstEdition.Scheme.Name, firstEdition.Mastermind.Name));
        Assert.Equal((Ruleset.FirstEdition, 12), (firstEdition.Ruleset, firstEdition.VillainDeck.Bystanders));
        Assert.DoesNotContain(firstEdition.Notes, note => note.Text == Warmup);
        Assert.Equal(new RuleNote("First Edition rules: the included base games follow them", "D-editions", null), firstEdition.RulesReason);

        // The Revised fixture's rules apply without its cards, citing its rulebook.
        Assert.Equal((Ruleset.Revised, 16), (revised.Ruleset, revised.VillainDeck.Bystanders));
        Assert.Contains(new RuleNote(Warmup, "R p.2", "https://example.test/revised-rules.pdf", "Revised Fixture"), revised.Notes);
        Assert.Equal(new RuleNote("Revised rules: chosen by the player", "D-editions", null), revised.RulesReason);
    }

    // With the core box and Villains, a pair with no Villainous card follows First Edition rules (D-mixed), a Revised one too.
    [Fact]
    public void A_Revised_card_follows_First_Edition_rules_when_the_Scheme_and_Mastermind_decide()
    {
        var setup = Draw(["core", "villains", "revised-extra"], null, 8, 4);

        Assert.Equal(("Test Blockade", "Test Admiral", Ruleset.FirstEdition), (setup.Scheme.Name, setup.Mastermind.Name, setup.Ruleset));
        Assert.Equal("First Edition rules: no Villainous Plot or Commander", setup.RulesReason?.Text);
    }

    // Test Baron is Mastermind 0 and the first Villains Plot Scheme 1, after Test Uprising. No included box has Wounds, so
    // Test Baron, whose card uses them, is drawn with the Bindings laid out as the Wound stack
    // (owner ruling D-bindings-as-wounds, 2026-10-13, #241).
    [Fact]
    public void A_card_that_uses_Wounds_is_drawn_with_Bindings_as_Wounds_when_no_included_box_has_Wounds()
    {
        var setup = Draw(["villains", "heroic"], null, 1, 0);

        Assert.Equal((Ruleset.Villainous, "Test Baron"), (setup.Ruleset, setup.Mastermind.Name));
        Assert.Equal((25, null), (setup.Stacks.Bindings, setup.Stacks.Wounds));
        Assert.Contains(setup.Notes, note => note.Text == "No included box has Wound cards: use Bindings cards for them, or Wound cards if you have them"
            && note.Citation == "D-bindings-as-wounds");
    }

    // Villains rules with only the core box: its S.H.I.E.L.D. starting cards and Officers stand for the HYDRA ones.
    [Fact]
    public void Villains_rules_without_a_Villainous_base_game_use_the_included_base_games_parts()
    {
        var setup = Draw(["core"], Ruleset.Villainous, 0, 0);

        Assert.Equal(Ruleset.Villainous, setup.Ruleset);
        Assert.Equal(new PlayerDeck(8, 4, null, Ruleset.FirstEdition), setup.PlayerDeck);
        Assert.NotNull(setup.Stacks.Officers);
        Assert.Null(setup.Stacks.MadameHydra);
        Assert.Null(setup.Stacks.NewRecruits);
        Assert.Equal(new RuleNote("Villains rules: chosen by the player", "D-editions", null), setup.RulesReason);
    }
}
