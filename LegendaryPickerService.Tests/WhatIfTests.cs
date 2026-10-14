using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Studios' What If...? box file (Data/Boxes/marvel-studios-what-if.json), a Revised base game drawn on its own (#203).
public class WhatIfTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private const string BoxId = "marvel-studios-what-if";

    private static readonly string[] Boxes = [BoxId];

    // At 5 players the setup is 5 Villain Groups, 2 Henchman Groups, 16 Bystanders and 6 Heroes, with a warmup round (WI pp.6, 8).
    [Fact]
    public void Five_players_use_16_Bystanders_and_warm_up()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(5, Boxes, new ScriptedRandom()));

        Assert.Equal(Ruleset.Revised, setup.Ruleset);
        Assert.Equal((5, 2, 16, 6), (setup.VillainGroups.Count, setup.HenchmanGroups.Count, setup.VillainDeck.Bystanders, setup.Heroes.Count));
        Assert.Equal(new PlayerDeck(8, 4), setup.PlayerDeck);
        Assert.Contains(setup.Notes, note => note.Text.StartsWith("Warmup Round") && note.Citation == "WI p.8");
    }

    // Ultron Infinity leads the Ultron Sentries even in Solo, where Always Leads is otherwise ignored (WI p.24, Card). Solo is 3 Heroes,
    // 1 Villain Group, 2 Henchmen with 2 more entering the city, 1 Bystander and 5 Master Strikes.
    [Fact]
    public void Solo_with_Ultron_Infinity_keeps_the_Ultron_Sentries()
    {
        var setup = Draw(1, "Collect an Interstellar Zoo", "Ultron Infinity");

        Assert.Equal(["Ultron Sentries"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal((3, 1, 2, 1, 5), (setup.Heroes.Count, setup.VillainGroups.Count, setup.VillainDeck.HenchmanCards, setup.VillainDeck.Bystanders, setup.VillainDeck.MasterStrikes));
        Assert.Contains(setup.Notes, note => note.Text.StartsWith("Solo: set aside 2 more Henchmen"));
    }

    // Killmonger, The Betrayer always includes Killmonger, Spec Ops; Marvel Zombies adds 8 random cards of an extra Hero to the Villain
    // Deck, Zombie Avengers as its Villain Group and 8 extra Bystanders with 1-2 players.
    [Fact]
    public void Killmonger_requires_his_Hero_and_Marvel_Zombies_adds_its_extra_Hero_cards()
    {
        var setup = Draw(2, "Marvel Zombies", "Killmonger, The Betrayer");

        Assert.Contains("Killmonger, Spec Ops", setup.Heroes.Select(hero => hero.Name));
        Assert.Contains("Zombie Avengers", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(8, setup.VillainDeck.OutsideHeroCards);
        Assert.Equal(10, setup.VillainDeck.Bystanders);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var result = Draw(2, "Marvel Zombies", "Ultron Infinity");

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Marvel Zombies's Twists"));
    }

    // Draws the Scheme and Mastermind by name, as the generator lists them at the player count.
    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var box = Catalog.Boxes.Single(candidate => candidate.Id == BoxId);
        var s = box.AllSchemes.ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = box.AllMasterminds.ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
