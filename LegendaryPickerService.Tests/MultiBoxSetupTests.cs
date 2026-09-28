using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Setups drawn from more than one box, using Fixtures/Boxes: a copy of the core box and a
// made-up expansion. Catalog order puts the fixture's cards after the core box's, so at 2 players
// the Schemes are the core box's 8 then 8 Test Heist, and the Masterminds the core box's 4 then 4 Test Tyrant.
// Test Heist requires HYDRA, citing the fixture's own source R; Test Tyrant always leads Test Cult.
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
        Assert.Equal(new VillainDeck(6, 5, 16, 10, 2, 0), setup.VillainDeck);
        Assert.Equal(
            [
                new RuleNote("Scheme requires HYDRA", "R p.3", "https://example.test/fixture-rules.pdf", FixtureName),
                new RuleNote("Test Tyrant always leads Test Cult", "R p.6", Rulebook, CoreName),
            ],
            setup.Notes);
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

        Assert.StartsWith("Include exactly one base game.", error.Message);
    }
}
