using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Revised ruleset and the capabilities its first base game needs (#203), using Fixtures/Revised: a copy of the core box and a
// made-up Revised base game. Its Test Warden always leads Test Golems even in Solo and requires Test Zulu, the last of its 6 Heroes;
// Test Overseer leads Test Golems the usual way. Its one Scheme, Test Menagerie, puts 8 cards of an extra Hero in the Villain Deck.
// Mastermind draw 0 is Test Warden and 1 is Test Overseer.
public sealed class RevisedRulesetTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Revised");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["revised"];

    private const int Warden = 0;
    private const int Overseer = 1;

    private static SetupResult Draw(int players, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(0, mastermind)));

    [Fact]
    public void A_Revised_setup_follows_the_Revised_base_game_and_warms_up_at_4_and_5_players()
    {
        var four = Draw(4, Warden);

        Assert.Equal(Ruleset.Revised, four.Ruleset);
        Assert.Equal(new PlayerDeck(8, 4), four.PlayerDeck);
        Assert.Contains(new RuleNote("Warmup Round: on each player's first turn, no card is played from the Villain Deck", "R p.2", "https://example.test/revised-rules.pdf"), four.Notes);
        Assert.DoesNotContain(Draw(2, Warden).Notes, note => note.Text.StartsWith("Warmup Round"));
    }

    [Fact]
    public void Solo_sets_aside_Henchmen_that_enter_the_city_before_the_first_turn()
    {
        var solo = Draw(1, Warden);

        Assert.Equal(2, solo.VillainDeck.HenchmanCards);
        Assert.Contains(
            new RuleNote("Solo: set aside 2 more Henchmen of the Henchman Group to enter the city before the first turn", "R p.3", "https://example.test/revised-rules.pdf"),
            solo.Notes);
    }

    // Solo ignores Always Leads, so the first Henchman Group, Test Drones, is drawn for Test Overseer, but Test Warden's card says
    // it leads Test Golems in Solo too.
    [Fact]
    public void A_Mastermind_that_leads_its_group_even_in_Solo_keeps_it_and_the_others_ignore_it()
    {
        var warden = Draw(1, Warden);
        var overseer = Draw(1, Overseer);

        Assert.Equal(["Test Golems"], warden.HenchmanGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Test Warden leads Test Golems even in Solo", "Card", null), warden.Notes);
        Assert.Equal(["Test Drones"], overseer.HenchmanGroups.Select(group => group.Name));
        Assert.Contains(overseer.Notes, note => note.Text == "Solo ignores Test Overseer's Always Leads");
    }

    [Fact]
    public void A_Masterminds_required_Hero_is_in_the_Hero_Deck_whatever_else_is_drawn()
    {
        var setup = Draw(2, Warden);

        Assert.Contains("Test Zulu", setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(5, setup.Heroes.Count);
        Assert.Contains(new RuleNote("Test Warden requires Test Zulu", "Card", null), setup.Notes);
        Assert.DoesNotContain("Test Zulu", Draw(2, Overseer).Heroes.Select(hero => hero.Name));
    }

    [Fact]
    public void A_Scheme_can_put_only_some_cards_of_an_extra_Hero_in_the_Villain_Deck()
    {
        var setup = Draw(2, Overseer);

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal((Pile.VillainDeck, 8), (outside.To, outside.Cards));
        Assert.Equal(8, setup.VillainDeck.OutsideHeroCards);
        Assert.Contains(
            new RuleNote($"Scheme draws 1 extra Hero outside the Hero Deck and puts 8 of its cards into the Villain Deck", "Card", null),
            setup.Notes);
    }

    [Fact]
    public void A_Revised_box_is_not_combined_with_a_box_of_another_ruleset()
    {
        const string Refusal = "Marvel Legendary First Edition core box and Revised Fixture follow different rulesets, and Revised boxes can't be combined with First Edition or Villainous boxes.";

        Assert.Equal(Refusal, Generator.CheckBoxes(["core", "revised"]));
        var error = Assert.Throws<ArgumentException>(() => Generator.Generate(2, ["revised", "core"], new ScriptedRandom()));
        Assert.StartsWith(Refusal, error.Message);
        Assert.Null(Generator.CheckBoxes(["revised"]));
    }
}
