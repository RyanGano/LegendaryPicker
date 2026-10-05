using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Scheme's setup.extraHenchmanCards (#134), using Fixtures/ExtraHenchmen: a copy of the core box and a made-up
// expansion. Catalog order puts its Schemes after the core box's 8 (6 in Solo): Test Smuggling (an extra Henchman
// Group of 10), Test Skimming (an extra Henchman Group of 6) and Test Overload (an extra Henchman Group of 12, more
// than any included group holds, so never drawn). Mastermind draw 0 is Dr. Doom, who always leads Doombot Legion.
public sealed class ExtraHenchmanCardsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "ExtraHenchmen");

    private const string FixtureName = "Smugglers Fixture";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "smugglers"];

    private const int DrDoom = 0;

    // Solo puts 3 cards of each Henchman Group in, but the Scheme's count of 10 wins for its extra group.
    [Fact]
    public void In_Solo_the_extra_Henchman_Group_puts_the_Schemes_count_in_the_Villain_Deck()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(6, DrDoom)));

        Assert.Equal("Test Smuggling", setup.Scheme.Name);
        Assert.Equal(2, setup.HenchmanGroups.Count);
        Assert.Equal(13, setup.VillainDeck.HenchmanCards);
        Assert.Contains(
            new RuleNote("Scheme overrides Solo: 10 Henchmen of its extra Henchman Group", "D2", "https://boardgamegeek.com/thread/884926", "Marvel Legendary First Edition core box"),
            setup.Notes);
    }

    // Dr. Doom's Doombot Legion keeps its usual 10; the group drawn into the extra slot puts the Scheme's 6 in.
    [Fact]
    public void The_Masterminds_Always_Leads_group_is_never_the_extra_group()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(9, DrDoom)));

        Assert.Equal(("Test Skimming", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(2, setup.HenchmanGroups.Count);
        Assert.Contains("Doombot Legion", setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(16, setup.VillainDeck.HenchmanCards);
        Assert.Contains(new RuleNote("Scheme puts 6 Henchmen of its extra Henchman Group in the Villain Deck", "Card", null, FixtureName), setup.Notes);
    }

    // The core box's 8 Schemes and the fixture's 3, less Test Overload, whose 12 cards no included Henchman Group holds.
    [Fact]
    public void A_Scheme_whose_extra_group_count_is_larger_than_any_group_is_dropped()
    {
        var random = new ScriptedRandom();

        Generator.Generate(2, Boxes, random);

        Assert.Equal(10, random.Options[0]);
    }
}
