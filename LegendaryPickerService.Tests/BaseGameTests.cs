using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Choosing base games, using Fixtures/BaseGames: a copy of the core box and two made-up base games with
// no cards of their own. Alternate Core Fixture follows the core box's First Edition ruleset but deals
// 7 Agents and 5 Troopers and cites its own R p.2 for Always Leads; its id sorts before core, so it comes
// first in catalog order. Villains Fixture follows the Villainous ruleset.
public class BaseGameTests
{
    public static readonly string Directory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "BaseGames");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(Directory));

    [Fact]
    public void A_setup_with_two_base_games_of_one_ruleset_follows_the_first_in_catalog_order()
    {
        // Cosmic Cube and Red Skull: the fixture adds no cards, so the draw is the core box's.
        foreach (var named in new[] { new[] { "core", "alt-core" }, ["alt-core", "core"] })
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(2, named, new ScriptedRandom(7, 3)));

            Assert.Equal("Red Skull", setup.Mastermind.Name);
            Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
            Assert.Equal(new PlayerDeck(7, 5), setup.PlayerDeck);
            Assert.Contains(
                new RuleNote("Red Skull always leads HYDRA", "R p.2", "https://example.test/alt-core-rules.pdf", "Alternate Core Fixture"),
                setup.Notes);
        }
    }

    // Villains Fixture has no rules for mixing, unlike Legendary: Villains, so nothing covers the mix.
    [Fact]
    public void Boxes_of_different_rulesets_with_no_rules_for_mixing_them_are_refused()
    {
        const string Refusal = "Marvel Legendary First Edition core box and Villains Fixture follow different rulesets, and no included base game has rules for mixing them.";

        Assert.Equal(Refusal, Generator.CheckBoxes(["core", "villains-fixture"]));
        var error = Assert.Throws<ArgumentException>(() => Generator.Generate(2, ["villains-fixture", "core"], new ScriptedRandom()));
        Assert.StartsWith(Refusal, error.Message);
    }

    [Fact]
    public void The_core_box_is_not_needed_when_another_base_game_is_included()
    {
        Assert.Null(Generator.CheckBoxes(["villains-fixture"]));
        Assert.Equal(new NoEligibleScheme(2), Generator.Generate(2, ["villains-fixture"], new ScriptedRandom()));
    }
}
