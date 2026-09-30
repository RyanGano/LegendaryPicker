using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Setups drawn from more than one box, using Fixtures/Boxes: a copy of the core box and a
// made-up expansion. Catalog order puts the fixture's cards after the core box's, so at 2 players
// the Schemes are the core box's 8 then 8 Test Heist, and the Masterminds the core box's 4 then 4 Test Tyrant.
// Test Heist requires HYDRA, citing the fixture's own source R, and puts 35 Bystanders in the Villain
// Deck; Test Tyrant always leads Test Cult, which uses Sidekicks. The fixture adds 11 Bystanders, 5 Wounds
// and 16 Sidekicks to the core box's 30 Bystanders, 30 Wounds and 30 Officers. Its Hero Test Warden uses
// Bindings, which no First Edition box supplies, so it is never drawn.
public class MultiBoxSetupTests
{
    public static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Boxes");

    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string FixtureName = "Fixture Expansion";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));
    private static readonly SetupGenerator CoreOnly = new(BoxCatalog.Load(BoxCatalog.DefaultDirectory));

    [Fact]
    public void The_fixture_expansion_loads_beside_the_core_box_without_setup_rules()
    {
        var boxes = BoxCatalog.Load(FixtureDirectory).Boxes;

        Assert.Equal(["core", "fixture"], boxes.Select(box => box.Id));
        Assert.Equal([true, false], boxes.Select(box => box.IsBaseGame));
    }

    // The exclusion case: a loaded expansion that is not included changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void With_only_the_core_box_included_the_expansion_changes_no_draw(int players)
    {
        for (var seed = 0; seed < 40; seed++)
        {
            var withExpansionLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, ["core"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var coreAlone = Assert.IsType<SetupResult>(CoreOnly.Generate(players, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(coreAlone, withExpansionLoaded, strict: true);
            Assert.All(withExpansionLoaded.Notes, note => Assert.Null(note.Box));
        }
    }

    [Fact]
    public void Including_the_expansion_adds_its_Scheme_Mastermind_and_groups_to_the_draw()
    {
        var random = new ScriptedRandom(8, 4);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "fixture"], random));

        Assert.Equal([9, 5], random.Options.Take(2));
        Assert.Equal("Test Heist", setup.Scheme.Name);
        Assert.Equal("Test Tyrant", setup.Mastermind.Name);
        Assert.Equal(["HYDRA", "Test Cult"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(new VillainDeck(6, 5, 16, 10, 35, 0), setup.VillainDeck);
        Assert.Equal(
            [
                new RuleNote("Scheme puts 35 Bystanders in the Villain Deck", "Card", null, FixtureName),
                new RuleNote("Scheme requires HYDRA", "R p.3", "https://example.test/fixture-rules.pdf", FixtureName),
                new RuleNote("Test Tyrant always leads Test Cult", "R p.6", Rulebook, CoreName),
            ],
            setup.Notes);
    }

    [Fact]
    public void The_Bystander_stack_holds_every_included_boxs_Bystanders_less_the_Villain_Decks()
    {
        // Midtown Bank Robbery puts 12 of the 30 + 11 Bystanders in the Villain Deck.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "fixture"], new ScriptedRandom(1)));

        Assert.Equal("Midtown Bank Robbery", setup.Scheme.Name);
        Assert.Equal(new SetupStacks(35, 30, 29), setup.Stacks);
    }

    [Fact]
    public void A_Scheme_needing_more_Bystanders_than_one_box_has_is_eligible_with_the_combined_stack()
    {
        // Test Heist's 35 Bystanders fit only in the combined 41.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, ["core", "fixture"], new ScriptedRandom(8)));

        Assert.Equal("Test Heist", setup.Scheme.Name);
        Assert.Equal(6, setup.Stacks.Bystanders);
    }

    [Fact]
    public void A_Scheme_that_sets_the_Wound_stack_overrides_the_sum()
    {
        // Legacy Virus sets 6 Wounds per player; Midtown Bank Robbery keeps the 30 + 5 the boxes hold.
        var legacyVirus = Assert.IsType<SetupResult>(Generator.Generate(3, ["core", "fixture"], new ScriptedRandom(0)));
        var midtown = Assert.IsType<SetupResult>(Generator.Generate(3, ["core", "fixture"], new ScriptedRandom(1)));

        Assert.Equal("Legacy Virus", legacyVirus.Scheme.Name);
        Assert.Equal(18, legacyVirus.Stacks.Wounds);
        Assert.Equal(35, midtown.Stacks.Wounds);
    }

    // Test Tyrant always leads Test Cult, which uses Sidekicks. Midtown Bank Robbery and Dr. Doom's first
    // groups and Heroes use none, so the fixture's Sidekicks are left out, with a note.
    [Fact]
    public void Sidekicks_are_laid_out_only_when_a_drawn_card_uses_them()
    {
        var cult = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "fixture"], new ScriptedRandom(8, 4)));
        var midtown = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "fixture"], new ScriptedRandom(1)));

        Assert.Contains(cult.VillainGroups, group => group.Name == "Test Cult");
        Assert.Equal(16, cult.Stacks.Sidekicks);
        Assert.DoesNotContain(cult.Notes, note => note.Text.StartsWith("Leave out", StringComparison.Ordinal));
        Assert.Null(midtown.Stacks.Sidekicks);
        Assert.Contains(
            new RuleNote("Leave out the Sidekick stack: no drawn card uses it", "D-uses", "https://github.com/RyanGano/LegendaryPicker/issues/87", CoreName),
            midtown.Notes);
    }

    // No First Edition box supplies Bindings, so Test Warden, which uses them, is not among the 17 Heroes
    // the first Hero draw picks from, and no draw takes it.
    [Fact]
    public void A_card_that_uses_a_part_no_included_box_supplies_is_dropped_before_the_draw()
    {
        var random = new ScriptedRandom(7, 3);
        Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "fixture"], random));
        Assert.Equal(17, random.Options[4]);

        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(5, ["core", "fixture"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            Assert.DoesNotContain(setup.Heroes, hero => hero.Name == "Test Warden");
        }
    }

    [Fact]
    public void Heroes_are_drawn_from_every_included_box()
    {
        // Cosmic Cube and Red Skull draw one Villain and one Henchman Group, then 5 Heroes from the
        // 15 core Heroes, Test Pilot and Test Medic. The last Hero draw takes Test Medic, the last option left.
        var random = new ScriptedRandom(7, 3, 0, 0, 0, 0, 0, 0, 12);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "fixture"], random));

        Assert.Equal([17, 16, 15, 14, 13], random.Options.Skip(4));
        Assert.Equal("Test Medic", setup.Heroes[^1].Name);
    }

    [Fact]
    public void The_order_boxes_are_named_in_does_not_change_the_draw()
    {
        var coreFirst = Generator.Generate(3, ["core", "fixture"], new CyclingRandom(8, 4, 2, 7));
        var fixtureFirst = Generator.Generate(3, ["fixture", "core"], new CyclingRandom(8, 4, 2, 7));

        Assert.Equivalent(coreFirst, fixtureFirst, strict: true);
    }

    [Fact]
    public void An_expansion_without_a_base_game_is_rejected()
    {
        var error = Assert.Throws<ArgumentException>(() => Generator.Generate(2, ["fixture"], new ScriptedRandom()));

        Assert.StartsWith("Include at least one base game.", error.Message);
    }
}
