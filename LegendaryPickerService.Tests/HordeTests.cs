using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The X-Men capabilities (#132), using Fixtures/Horde: a copy of the core box and a made-up expansion. Its Mastermind,
// Test Commander, always leads Test Guard and also one of Test Drones and Test Gunships (alsoLeads); Test Overseer leads
// Test Guard and also one of Test Drones and the core box's Sentinel. Catalog order puts
// its Schemes after the core box's 8 (6 in Solo): Test Patrol (plain), Test Infestation (requires all 10 Test Swarm
// as an extra Henchman Group), Test Nest (requires Test Swarm with no extra slot), Test Riot (a 20-Wound stack) and Test
// Dread (uses Horrors). Mastermind draw
// 0 is Dr. Doom, 4 is Test Commander and 5 is Test Overseer.
public sealed class HordeTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Horde");

    private const string FixtureName = "Horde Fixture";

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "horde"];

    private const int DrDoom = 0;
    private const int TestCommander = 4;
    private const int TestOverseer = 5;

    [Fact]
    public void A_Mastermind_that_also_leads_one_of_several_groups_draws_one_of_them_into_a_slot()
    {
        // Two players: Test Guard and one drawn Villain Group, then the one Henchman slot is drawn from Test Commander's two.
        var random = new ScriptedRandom(8, TestCommander, 0, 1);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Patrol", "Test Commander"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Contains("Test Guard", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Test Gunships"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(2, random.Options[3]);
        Assert.Contains(new RuleNote("Test Commander also always leads Test Gunships, one of Test Drones and Test Gunships", "Card", null, FixtureName), setup.Notes);
    }

    // Bastion leads any Sentinel Henchman Group (#186): another box's group is one of the options when that box is included.
    [Fact]
    public void A_Mastermind_can_also_lead_a_group_of_another_included_box()
    {
        var random = new ScriptedRandom(8, TestOverseer, 0, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Patrol", "Test Overseer"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(["Sentinel"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(2, random.Options[3]);
        Assert.Contains(new RuleNote("Test Overseer also always leads Sentinel, one of Sentinel and Test Drones", "Card", null, FixtureName), setup.Notes);
    }

    [Fact]
    public void A_required_group_that_takes_the_last_slot_drops_the_other_Always_Leads_group()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(10, TestCommander)));

        Assert.Equal(("Test Nest", "Test Commander"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(["Test Swarm"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Contains(setup.Notes, note => note.Text == "Scheme requires Test Swarm, so Test Commander's other Always Leads group is dropped");
    }

    [Fact]
    public void Solo_ignores_the_other_Always_Leads_group_too()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(6, TestCommander)));

        Assert.Equal(("Test Patrol", "Test Commander"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(["Doombot Legion"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.DoesNotContain(setup.Notes, note => note.Text.Contains("also always leads"));
    }

    // In Solo each Henchman Group puts 3 cards in, but the Scheme's own count of 10 wins for the group it requires.
    [Theory]
    [InlineData(1, 7, 13)]
    [InlineData(3, 9, 20)]
    public void A_required_group_with_a_card_count_puts_that_many_in_the_Villain_Deck(int players, int scheme, int henchmen)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(scheme, DrDoom)));

        Assert.Equal("Test Infestation", setup.Scheme.Name);
        Assert.Equal(2, setup.HenchmanGroups.Count);
        Assert.Contains("Test Swarm", setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(henchmen, setup.VillainDeck.HenchmanCards);
        Assert.Contains(new RuleNote("Scheme requires Test Swarm, 10 of its Henchmen in the Villain Deck", "Card", null, FixtureName), setup.Notes);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    public void A_Scheme_can_set_the_Wound_stack_to_a_size_whatever_the_player_count(int players)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(11, DrDoom)));

        Assert.Equal("Test Riot", setup.Scheme.Name);
        Assert.Equal(20, setup.Stacks.Wounds);
        Assert.Contains(new RuleNote("Scheme sets the Wound stack to 20", "Card", null, FixtureName), setup.Notes);
    }

    [Fact]
    public void A_Scheme_that_uses_Horrors_lays_out_the_Horror_stack()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(12, DrDoom)));

        Assert.Equal("Test Dread", setup.Scheme.Name);
        Assert.Equal(5, setup.Stacks.Horrors);
    }

    [Fact]
    public void A_setup_with_no_card_that_uses_Horrors_lays_out_no_Horror_stack()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(8, DrDoom)));

        Assert.Equal("Test Patrol", setup.Scheme.Name);
        Assert.Null(setup.Stacks.Horrors);
        Assert.DoesNotContain(setup.Notes, note => note.Text.StartsWith("Leave out", StringComparison.Ordinal));
    }
}
