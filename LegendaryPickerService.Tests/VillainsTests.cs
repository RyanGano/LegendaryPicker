using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Legendary: Villains box file (Data/Boxes/villains.json) is a base game on the Villainous ruleset, drawn alone: its Plots in catalog order are
// 0 Build an Underground MegaVault Prison, 1 Cage Villains in Power-Suppressing Cells, 2 Crown Thor King of
// Asgard, 3 Crush HYDRA, 4 Graduation at Xavier's X-Academy, 5 Infiltrate the Lair with Spies, 6 Mass Produce
// War Machine Armor and 7 Resurrect Heroes with Norn Stones, at every player count; its Commanders are
// 0 Dr. Strange, 1 Nick Fury, 2 Odin and 3 Professor X.
public class VillainsTests
{
    private const string Rulebook = "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Villains = Catalog.Boxes.Single(box => box.Id == "villains");
    private static readonly SetupGenerator Generator = new(Catalog);

    private const int MassProduce = 6;

    private const int Odin = 2;

    [Fact]
    public void The_Villainous_setup_follows_the_rulebook()
    {
        var setup = Villains.Setup!;

        Assert.Equal(
            [
                new PlayerCountSetup(2, 2, 1, 2, "VIL p.6"),
                new PlayerCountSetup(3, 3, 1, 8, "VIL p.6"),
                new PlayerCountSetup(4, 3, 2, 8, "VIL p.6"),
                new PlayerCountSetup(5, 4, 2, 12, "VIL p.6"),
            ],
            setup.PlayerCounts);
        Assert.Equal(new Sourced<int>(5, "VIL p.7"), setup.Heroes);
        var sixthAlly = Assert.Single(setup.ExtraHeroes!);
        Assert.Equal([5], sixthAlly.Players!);
        Assert.Equal((1, "VIL p.7"), (sixthAlly.Value, sixthAlly.Source));
        Assert.Equal(new Sourced<int>(5, "VIL p.6"), setup.MasterStrikes);
        Assert.Equal(new StartingDeck(new Sourced<int>(8, "VIL p.5"), new Sourced<int>(4, "VIL p.5")), setup.StartingDeck);
        Assert.Equal(new Rulings("VIL p.6", "VIL p.18", "VIL p.18"), setup.Rulings);
    }

    [Fact]
    public void Villainous_Solo_follows_the_rulebook()
    {
        var solo = Villains.Setup!.Solo;

        Assert.Equal(new Sourced<int>(3, "VIL p.19"), solo.Heroes);
        Assert.Equal(new Sourced<int>(1, "VIL p.20"), solo.VillainGroups);
        Assert.Equal(new Sourced<int>(1, "VIL p.20"), solo.HenchmanGroups);
        Assert.Equal(new Sourced<int>(3, "VIL p.20"), solo.HenchmanCards);
        Assert.Equal(new Sourced<int>(1, "VIL p.20"), solo.Bystanders);
        Assert.Equal(new Sourced<int>(5, "VIL p.20"), solo.MasterStrikes);
        Assert.Equal(new Sourced<bool>(true, "VIL pp.19-20"), solo.IgnoresAlwaysLeads);
        Assert.Equal(
            [
                new PlayRule("Plot Twists also send a Lair Ally costing 6 or less under the Ally Deck", "VIL p.20"),
                new PlayRule("After each Command Strike, play another card from the Adversary Deck", "VIL p.20"),
            ],
            solo.PlayRules);
    }

    // The rulebook's own example (VIL p.18): with Odin and 2 or 3 players, Mass Produce War Machine Armor's
    // S.H.I.E.L.D. Assault Squad takes the one Backup Adversary slot, and Odin's Asgardian Warriors are dropped.
    [Fact]
    public void Mass_Produce_War_Machine_Armor_displaces_Odins_Asgardian_Warriors_with_2_players()
    {
        var setup = Draw(2, MassProduce, Odin);

        Assert.Equal(["S.H.I.E.L.D. Assault Squad"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Contains(
            new RuleNote(
                "Plot requires S.H.I.E.L.D. Assault Squad, so Odin's Always Leads group Asgardian Warriors is dropped", "VIL p.18", Rulebook),
            setup.Notes);
    }

    private static SetupResult Draw(int players, int plot, int commander) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, ["villains"], new ScriptedRandom(plot, commander)));
}
