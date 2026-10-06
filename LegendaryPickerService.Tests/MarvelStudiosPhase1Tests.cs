using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Studios Phase 1 box file (Data/Boxes/marvel-studios-phase-1.json), a second First Edition base game,
// drawn alone, with the core box and with an expansion.
// Its printings of core box cards are the core box's cards (#34 D5): drawn with Phase 1 alone, and once with both.
public class MarvelStudiosPhase1Tests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Phase1 = Catalog.Boxes.Single(box => box.Id == "marvel-studios-phase-1");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Alone = ["marvel-studios-phase-1"];

    // Its own Mastermind, then the core box's two it reprints.
    private static readonly string[] MastermindNames = ["Iron Monger", "Loki", "Red Skull"];

    // Draws, with Phase 1 the only box: its own table, starting deck and Solo.

    [Fact]
    public void Phase_1_alone_plays_Solo_from_its_rulebook()
    {
        var setup = Draw(1, "Invade Asgard", "Red Skull");

        Assert.Equal((3, 1, 1), (setup.Heroes.Count, setup.VillainGroups.Count, setup.HenchmanGroups.Count));
        Assert.Equal((7, 1, 1, 3), (setup.VillainDeck.Twists, setup.VillainDeck.MasterStrikes, setup.VillainDeck.Bystanders, setup.VillainDeck.HenchmanCards));
        Assert.Contains(new RuleNote("Solo: After each Twist, KO a Hero costing 6 or less from the HQ", "P1 p.17", Phase1.Sources.Single(s => s.Key == "P1").Url), setup.Notes);
    }

    // With the core box each reprint is in its pool once: 8 core and 6 new Schemes, 4 core Masterminds and Iron Monger,
    // 7 core and 3 new Villain Groups, and the core box's 15 Heroes, as with the core box alone. Legacy Virus and Dr. Doom
    // are drawn, whose Doombot Legion fills the one Henchman slot.
    [Fact]
    public void With_the_core_box_each_reprint_is_in_its_pool_once()
    {
        var probe = new ScriptedRandom();
        Generator.Generate(2, ["core", "marvel-studios-phase-1"], probe);
        var coreAlone = new ScriptedRandom();
        Generator.Generate(2, ["core"], coreAlone);

        Assert.Equal([14, 5, 10, 9, 15, 14, 13, 12, 11], probe.Options);
        Assert.Equal([8, 4, 7, 6, 15, 14, 13, 12, 11], coreAlone.Options);
    }

    // A reprint drawn with several boxes ticked names every ticked box that holds it, since either box's card will do; a
    // card only Phase 1 holds names Phase 1. Without the core box a reprint names only Phase 1, and isn't marked as from a
    // box the setup leaves out.
    [Fact]
    public void A_reprint_names_each_ticked_box_that_holds_it()
    {
        const string either = "Marvel Legendary First Edition core box or Marvel Studios Phase 1";
        var withCore = Response(2, ["core", "marvel-studios-phase-1"], "Asgard Under Siege", "Loki");

        Assert.Equal(("Marvel Studios Phase 1", either), (withCore.Scheme.Box, withCore.Mastermind.Box));
        Assert.Equal(either, withCore.VillainGroups.Single(group => group.Name == "Enemies of Asgard").Box);

        var withDarkCity = Response(3, ["marvel-studios-phase-1", "dark-city"], "Super Hero Civil War", "Red Skull");

        Assert.Equal(("Marvel Studios Phase 1", "Marvel Studios Phase 1"), (withDarkCity.Scheme.Box, withDarkCity.Mastermind.Box));
        var hydra = withDarkCity.VillainGroups.Single(group => group.Name == "HYDRA");
        Assert.Equal(("Marvel Studios Phase 1", false), (hydra.Box, hydra.NotIncluded));
    }

    // Phase 1 reprints core cards but none of its cards is X-Men, so beside the X-Men box the X-Men team chip names
    // the core box that defines the term, not Phase 1 (#156); its reprinted Black Widow is an Avenger, so the Avengers
    // chip still names Phase 1 (#153).
    [Fact]
    public void A_core_team_chip_names_Phase_1_only_for_its_own_printings()
    {
        string[] boxes = ["marvel-studios-phase-1", "x-men"];
        var included = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).ToList();
        var scheme = included.SelectMany(box => box.AllSchemes).DistinctBy(s => s.Id)
            .Where(s => s.Setup.AllowedPlayerCounts?.Value.Contains(2) ?? true).Select(s => s.Name).ToList().IndexOf("Anti-Mutant Hatred");
        var mastermind = included.SelectMany(box => box.AllMasterminds).DistinctBy(m => m.Id).Select(m => m.Name).ToList().IndexOf("Arcade");
        var probe = new ScriptedRandom(scheme, mastermind);
        Generator.Generate(2, boxes, probe);

        // The Hero draws are the ones with the whole pool to choose from: the first Hero in it is Phase 1's Black Widow,
        // and the last are the X-Men box's.
        var pool = included.SelectMany(box => box.AllHeroes).DistinctBy(hero => hero.Id).Count();
        var first = probe.Options.IndexOf(pool);
        var draws = new[] { scheme, mastermind }.Concat(new int[first - 2]).Append(0).Concat(Enumerable.Range(2, 4).Select(i => pool - i)).ToArray();
        var setup = Assert.IsType<SetupBody>(SetupResponse.From(Generator.Generate(2, boxes, new ScriptedRandom(draws)), Catalog));

        Assert.Contains(setup.Heroes, hero => hero.Name == "Black Widow");
        Assert.Equal(
            ("Marvel Legendary First Edition core box", "Marvel Studios Phase 1"),
            (Assert.Single(setup.Glossary, entry => entry.Id == "core_term_x-men").Box,
                Assert.Single(setup.Glossary, entry => entry.Id == "core_term_avengers").Box));
    }

    // With Phase 1 alone the Schemes and Masterminds are drawn in box-file order, so a pair is two indexes.
    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Alone,
            new ScriptedRandom(SchemesAt(players).ToList().IndexOf(scheme), Array.IndexOf(MastermindNames, mastermind))));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }

    // A scripted draw of a named Scheme and Mastermind from the included boxes' pools, each reprint in them once, as the
    // checklist gets it.
    private static SetupBody Response(int players, string[] boxes, string scheme, string mastermind)
    {
        var included = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).ToList();
        var schemes = included.SelectMany(box => box.AllSchemes).DistinctBy(s => s.Id)
            .Where(s => s.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true).Select(s => s.Name).ToList();
        var masterminds = included.SelectMany(box => box.AllMasterminds).DistinctBy(m => m.Id).Select(m => m.Name).ToList();
        var result = Generator.Generate(players, boxes, new ScriptedRandom(schemes.IndexOf(scheme), masterminds.IndexOf(mastermind)));

        var body = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog));
        Assert.Equal((scheme, mastermind), (body.Scheme.Name, body.Mastermind.Name));
        return body;
    }

    private static IEnumerable<string> SchemesAt(int players) =>
        Phase1.AllSchemes.Where(s => s.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true).Select(s => s.Name);
}
