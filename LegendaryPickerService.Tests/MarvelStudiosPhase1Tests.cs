using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Studios Phase 1 box file (Data/Boxes/marvel-studios-phase-1.json), a second First Edition base game, pinned to
// its rulebook (P1), the card catalog and the card faces it links, and drawn alone, with the core box and with an expansion.
// Its printings of core box cards are the core box's cards (#34 D5): drawn with Phase 1 alone, and once with both.
public class MarvelStudiosPhase1Tests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Phase1 = Catalog.Boxes.Single(box => box.Id == "marvel-studios-phase-1");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Alone = ["marvel-studios-phase-1"];

    private static readonly string[] SchemeNames =
    [
        "Asgard Under Siege",
        "Destroy the Cities of Earth!",
        "Enslave Minds with the Chitauri Scepter",
        "Invade Asgard",
        "Radioactive Palladium Poisoning",
        "Replace Earth's Leaders with HYDRA",
        "Super Hero Civil War",
        "Unleash the Power of the Cosmic Cube",
    ];

    // Its own Mastermind, then the core box's two it reprints.
    private static readonly string[] MastermindNames = ["Iron Monger", "Loki", "Red Skull"];

    private static readonly Box Core = Catalog.Boxes.Single(box => box.Id == "core");

    // Catalog

    [Fact]
    public void Phase_1_is_a_First_Edition_base_game_with_the_contents_of_its_rulebook()
    {
        Assert.Equal("Marvel Studios Phase 1", Phase1.Name);
        Assert.True(Phase1.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, Phase1.Ruleset);
        Assert.Null(Phase1.Setup!.Mixing);
        Assert.Equal(new Sourced<string>("2018-08", "Q"), Phase1.About.Released);
        Assert.Equal(new Sourced<int>(14, "P1 p.18"), Phase1.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "P1 p.18"), Phase1.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "P1 p.18"), Phase1.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(12, "P1 p.18"), Phase1.Components.SchemeTwists);
        // The contents list 42 Bystanders (30 plus 12 special); setup uses 41 (P1 p.6), and setup's number is used.
        Assert.Equal(new Sourced<int>(41, "P1 p.6"), Phase1.Components.Bystanders);
        Assert.Equal(new Sourced<int>(30, "P1 p.18"), Phase1.Components.Wounds);
        Assert.Equal(new Sourced<int>(30, "P1 p.18"), Phase1.Components.Officers);
    }

    [Fact]
    public void Phase_1_has_the_7_Heroes_5_Villain_Groups_4_Henchman_Groups_3_Masterminds_and_8_Schemes()
    {
        Assert.Equal(
            ["Black Widow", "Captain America", "Hawkeye", "Hulk", "Iron Man", "Nick Fury", "Thor"],
            Phase1.AllHeroes.Select(hero => hero.Name));
        Assert.Equal(
            ["Chitauri", "Gamma Hunters", "Iron Foes", "Enemies of Asgard", "HYDRA"],
            Phase1.AllVillainGroups.Select(group => group.Name));
        Assert.Equal(["Hammer Drone Army", "HYDRA Pilots", "HYDRA Spies", "Ten Rings Fanatics"], Phase1.AllHenchmanGroups.Select(group => group.Name));
        Assert.Equal(MastermindNames, Phase1.AllMasterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, Phase1.AllSchemes.Select(scheme => scheme.Name));
    }

    // Each reprint is the core box's card itself, with its name, Hero Name, team, classes, uses and Setup line: the
    // Phase 1 printings were checked against them on C1 and the card faces, and none differs (#34 D5).
    [Fact]
    public void Reprints_are_the_core_boxs_cards()
    {
        ICard[] coreCards = [.. Core.Heroes, .. Core.VillainGroups, .. Core.HenchmanGroups, .. Core.Masterminds, .. Core.Schemes];

        Assert.Equal("D5", Phase1.Reprints!.Source);
        Assert.Empty(Phase1.Heroes);
        Assert.Equal(Phase1.Reprints.Value, Phase1.ReprintedCards.Select(card => card.Id));
        Assert.All(Phase1.ReprintedCards, card => Assert.Same(coreCards.Single(core => core.Id == card.Id), card));
    }

    // Read from the C1-linked faces: cards that gain or hand out a Wound, and the cards that gain a S.H.I.E.L.D. Officer
    // (Nick Fury's Battlefield Promotion, the HYDRA Motorcycle Squad). The special Bystanders only KO a Wound a player has.
    [Fact]
    public void Phase_1_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. Phase1.AllHeroes, .. Phase1.AllVillainGroups, .. Phase1.AllHenchmanGroups, .. Phase1.AllMasterminds, .. Phase1.AllSchemes];

        Assert.Equal(
            [
                "core_hero_hulk: Wounds",
                "core_hero_nick-fury: Officers",
                "marvel-studios-phase-1_villain_chitauri: Wounds",
                "marvel-studios-phase-1_villain_gamma-hunters: Wounds",
                "marvel-studios-phase-1_villain_iron-foes: Wounds",
                "core_villain_enemies-of-asgard: Wounds",
                "core_villain_hydra: Wounds Officers",
                "marvel-studios-phase-1_mastermind_iron-monger: Wounds",
                "core_mastermind_loki: Wounds",
                "marvel-studios-phase-1_scheme_radioactive-palladium-poisoning: Wounds",
                "core_scheme_unleash-the-power-of-the-cosmic-cube: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
        Assert.Equal([new PartUse(Part.Officers, "P1 p.11")], Phase1.Setup!.Uses);
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = Phase1.AllSchemes.ToDictionary(scheme => scheme.Name);

        Assert.All(schemes.Values, scheme => Assert.All(scheme.Setup.Twists, twists => Assert.Equal("Card", twists.Source)));
        Assert.All(schemes.Values, scheme => Assert.Null(scheme.ExcludesMasterminds));
        Assert.Equal(1, Assert.Single(schemes["Asgard Under Siege"].Setup.ExtraHenchmanGroups!).Value);
        Assert.Equal(new Sourced<int>(12, "Card"), schemes["Destroy the Cities of Earth!"].Setup.VillainDeckBystanders);
        Assert.Equal(new Sourced<int>(6, "Card"), schemes["Radioactive Palladium Poisoning"].Setup.WoundsPerPlayer);
        Assert.Equal(new Sourced<int>(3, "Card"), schemes["Replace Earth's Leaders with HYDRA"].Setup.TwistsBesideScheme);
        Assert.Equal(new Sourced<int>(18, "Card"), schemes["Replace Earth's Leaders with HYDRA"].Setup.VillainDeckBystanders);
        foreach (var name in new[] { "Asgard Under Siege", "Super Hero Civil War" })
        {
            Assert.Equal([2, 3, 4, 5], schemes[name].Setup.AllowedPlayerCounts!.Value);
        }
    }

    [Theory]
    [InlineData("Iron Monger", "Iron Foes")]
    [InlineData("Loki", "Enemies of Asgard")]
    [InlineData("Red Skull", "HYDRA")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var entry = Phase1.AllMasterminds.Single(m => m.Name == mastermind);

        Assert.Equal((GroupType.Villain, "Card"), (entry.AlwaysLeads.GroupType, entry.AlwaysLeads.Source));
        Assert.Equal(group, Phase1.AllVillainGroups.Single(g => g.Id == entry.AlwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Phase_1_glossary_adds_Conqueror_and_reuses_the_core_box_terms()
    {
        var term = Assert.Single(Phase1.Glossary);
        Assert.Equal(("Conqueror", TermKind.Keyword, "P1", 13), (term.Name, term.Kind, term.Source, term.Page));
        Assert.Equal(
            ["Chitauri", "Gamma Hunters", "Iron Foes", "Iron Monger"],
            ((ICard[])[.. Phase1.VillainGroups, .. Phase1.Masterminds]).Where(card => card.Terms.Contains(term.Id)).Select(card => card.Name));
    }

    // Draws, with Phase 1 the only box: its own table, starting deck and Solo.

    [Theory]
    [InlineData(2, 2, 1, 2)]
    [InlineData(3, 3, 1, 8)]
    [InlineData(4, 3, 2, 8)]
    [InlineData(5, 4, 2, 12)]
    public void Phase_1_alone_follows_its_own_player_count_table(int players, int villainGroups, int henchmanGroups, int bystanders)
    {
        var setup = Draw(players, "Invade Asgard", "Red Skull");

        Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
        Assert.Equal([Phase1], setup.Boxes);
        Assert.Equal(villainGroups, setup.VillainGroups.Count);
        Assert.Equal(henchmanGroups, setup.HenchmanGroups.Count);
        Assert.Equal((7, 5, bystanders), (setup.VillainDeck.Twists, setup.VillainDeck.MasterStrikes, setup.VillainDeck.Bystanders));
        Assert.Equal(players == 5 ? 6 : 5, setup.Heroes.Count);
        Assert.Equal((8, 4), (setup.PlayerDeck.Agents, setup.PlayerDeck.Troopers));
        Assert.Equal((30, 41 - bystanders), (setup.Stacks.Officers, setup.Stacks.Bystanders));
        Assert.Contains("HYDRA", setup.VillainGroups.Select(group => group.Name));
        Assert.All(setup.Heroes, hero => Assert.Contains(hero, Phase1.AllHeroes));
    }

    [Fact]
    public void Phase_1_alone_plays_Solo_from_its_rulebook()
    {
        var setup = Draw(1, "Invade Asgard", "Red Skull");

        Assert.Equal((3, 1, 1), (setup.Heroes.Count, setup.VillainGroups.Count, setup.HenchmanGroups.Count));
        Assert.Equal((7, 1, 1, 3), (setup.VillainDeck.Twists, setup.VillainDeck.MasterStrikes, setup.VillainDeck.Bystanders, setup.VillainDeck.HenchmanCards));
        Assert.Contains(new RuleNote("Solo: After each Twist, KO a Hero costing 6 or less from the HQ", "P1 p.17", Phase1.Sources.Single(s => s.Key == "P1").Url), setup.Notes);
    }

    [Fact]
    public void Solo_never_draws_Asgard_Under_Siege_or_Super_Hero_Civil_War()
    {
        var probe = new ScriptedRandom();
        Generator.Generate(1, Alone, probe);

        Assert.Equal(6, probe.Options[0]);
        Assert.Equal(SchemeNames.Except(["Asgard Under Siege", "Super Hero Civil War"]), SchemesAt(1));
    }

    [Theory]
    [InlineData("Asgard Under Siege", 2, 8)]
    [InlineData("Destroy the Cities of Earth!", 3, 8)]
    [InlineData("Enslave Minds with the Chitauri Scepter", 4, 8)]
    [InlineData("Invade Asgard", 5, 7)]
    [InlineData("Radioactive Palladium Poisoning", 1, 8)]
    [InlineData("Replace Earth's Leaders with HYDRA", 2, 5)]
    [InlineData("Super Hero Civil War", 4, 5)]
    [InlineData("Unleash the Power of the Cosmic Cube", 3, 8)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        Assert.Equal(twists, Draw(players, scheme, "Iron Monger").VillainDeck.Twists);
    }

    [Fact]
    public void Asgard_Under_Siege_adds_a_Henchman_Group()
    {
        Assert.Equal(2, Draw(2, "Asgard Under Siege", "Loki").HenchmanGroups.Count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void Destroy_the_Cities_of_Earth_puts_12_Bystanders_in_the_Villain_Deck(int players)
    {
        var setup = Draw(players, "Destroy the Cities of Earth!", "Loki");

        Assert.Equal((12, 41 - 12), (setup.VillainDeck.Bystanders, setup.Stacks.Bystanders));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void Enslave_Minds_uses_6_Heroes_the_Chitauri_and_12_Hero_cards_in_the_Villain_Deck(int players)
    {
        var setup = Draw(players, "Enslave Minds with the Chitauri Scepter", "Red Skull");

        Assert.Equal(6, setup.Heroes.Count);
        Assert.Contains("Chitauri", setup.VillainGroups.Select(group => group.Name));
        var moved = Assert.Single(setup.Moves);
        Assert.Equal((CardKind.Hero, Pile.HeroDeck, Pile.VillainDeck, 12), (moved.Card, moved.From, moved.To, moved.Total));
    }

    [Theory]
    [InlineData(1, 6)]
    [InlineData(4, 24)]
    public void Radioactive_Palladium_Poisoning_holds_6_Wounds_per_player(int players, int wounds)
    {
        Assert.Equal(wounds, Draw(players, "Radioactive Palladium Poisoning", "Red Skull").Stacks.Wounds);
    }

    [Fact]
    public void Replace_Earths_Leaders_with_HYDRA_sets_3_Twists_beside_it_and_18_Bystanders_in_the_Villain_Deck()
    {
        var setup = Draw(3, "Replace Earth's Leaders with HYDRA", "Iron Monger");

        Assert.Equal((5, 3, 18), (setup.VillainDeck.Twists, setup.TwistsBesideScheme, setup.VillainDeck.Bystanders));
    }

    [Theory]
    [InlineData(2, 8, 4)]
    [InlineData(3, 8, 5)]
    [InlineData(4, 5, 5)]
    public void Super_Hero_Civil_War_sets_its_Twists_and_Heroes_by_player_count(int players, int twists, int heroes)
    {
        var setup = Draw(players, "Super Hero Civil War", "Loki");

        Assert.Equal((twists, heroes), (setup.VillainDeck.Twists, setup.Heroes.Count));
    }

    // Always Leads, and the Wound Stack a Mastermind's Master Strike uses; Red Skull's KOs a Hero, so it brings none of its own.
    [Theory]
    [InlineData("Iron Monger", "Iron Foes")]
    [InlineData("Loki", "Enemies of Asgard")]
    [InlineData("Red Skull", "HYDRA")]
    public void Mastermind_brings_its_Always_Leads_group_and_the_Wounds_it_uses(string mastermind, string group)
    {
        var setup = Draw(2, "Invade Asgard", mastermind);

        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name));
        Assert.NotNull(setup.Stacks.Wounds);
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

    // With the core box, the core box's rules apply (first base game in catalog order) and both boxes' stacks are summed.
    [Fact]
    public void With_the_core_box_both_base_games_are_drawn_from_and_their_stacks_are_summed()
    {
        string[] boxes = ["core", "marvel-studios-phase-1"];

        // Scheme 8 is Phase 1's Asgard Under Siege, after the core box's 8; Mastermind 4 is Iron Monger.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, new ScriptedRandom(8, 4)));

        Assert.Equal(("Asgard Under Siege", "Iron Monger"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(30 + 41 - 2, setup.Stacks.Bystanders);
        Assert.Equal(60, setup.Stacks.Officers);
        Assert.Contains("Iron Foes", setup.VillainGroups.Select(group => group.Name));
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

    // With the core box unticked, a reprint's rule notes and glossary chips name the ticked box that holds it, never the
    // core box (#153), while their citations keep the original's source.
    [Fact]
    public void A_reprints_notes_and_glossary_name_the_ticked_box_not_the_core_box()
    {
        var setup = Response(3, ["marvel-studios-phase-1", "dark-city"], "Super Hero Civil War", "Red Skull");

        var notes = setup.Notes.Where(note => note.Text.StartsWith("Scheme ", StringComparison.Ordinal) || note.Text.StartsWith("Red Skull ", StringComparison.Ordinal)).ToList();
        Assert.NotEmpty(notes);
        Assert.All(notes, note => Assert.Equal("Marvel Studios Phase 1", note.Box));
        Assert.DoesNotContain(setup.Notes, note => note.Box == "Marvel Legendary First Edition core box");
        Assert.DoesNotContain(setup.Glossary, entry => entry.Box == "Marvel Legendary First Edition core box");
        Assert.Contains(setup.Glossary, entry => entry.Box == "Marvel Studios Phase 1");
    }

    // Phase 1 is the base game an expansion is played with when the core box isn't ticked.
    [Fact]
    public void Phase_1_is_the_base_game_for_an_expansion_without_the_core_box()
    {
        string[] boxes = ["marvel-studios-phase-1", "dark-city"];
        Assert.Null(Generator.CheckBoxes(boxes));

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, boxes, new ScriptedRandom(0, 0)));

        Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
        Assert.Equal(41 - 8 + 11, setup.Stacks.Bystanders);
        Assert.Equal(["dark-city", "marvel-studios-phase-1"], setup.Boxes.Select(box => box.Id));
        Assert.Equal((8, 4), (setup.PlayerDeck.Agents, setup.PlayerDeck.Troopers));
    }

    // Phase 1 and Civil War hold only one team with 3 Heroes (Avengers), so Avengers vs. X-Men's second team comes from
    // another First Edition box the player owns, named with its box (D-scheme-first); every other Hero is an included one.
    [Fact]
    public void Avengers_vs_X_Men_with_Phase_1_and_Civil_War_takes_its_second_team_from_another_box()
    {
        string[] boxes = ["civil-war", "marvel-studios-phase-1"];
        var schemes = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).SelectMany(box => box.Schemes)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(2) ?? true).ToList();

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, new ScriptedRandom(schemes.FindIndex(scheme => scheme.Name == "Avengers vs. X-Men"))));

        var teams = setup.Heroes.GroupBy(hero => hero.Team).ToDictionary(team => team.Key!, team => team.ToList());
        Assert.Equal([3, 3], teams.Values.Select(team => team.Count));
        bool Included(Hero hero) => Catalog.Boxes.Any(box => boxes.Contains(box.Id) && box.Holds(hero.Id));
        Assert.All(teams["core_term_avengers"], hero => Assert.True(Included(hero)));
        var other = teams.Single(team => team.Key != "core_term_avengers").Value;
        Assert.Equal(2, other.Count(hero => !Included(hero)));
    }

    // The exclusion case: with every other First Edition box included, Phase 1 changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Phase_1_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("marvel-studios-phase-1.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool", "noir", "x-men", "spider-man-homecoming", "champions", "world-war-hulk"];

        for (var seed = 0; seed < 4; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("marvel-studios-phase-1_"));
        }
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

    private static string TermName(string id) =>
        Catalog.Boxes.SelectMany(box => box.Glossary).Single(term => term.Id == id).Name;

    private static IEnumerable<string> ComponentIds(SetupResult setup) =>
        new[] { setup.Scheme.Id, setup.Mastermind.Id }
            .Concat(setup.VillainGroups.Select(group => group.Id))
            .Concat(setup.HenchmanGroups.Select(group => group.Id))
            .Concat(setup.Heroes.Select(hero => hero.Id))
            .Concat(setup.OutsideHeroes.Select(outside => outside.Hero.Id))
            .Concat((setup.OutsideMasterminds ?? []).Select(outside => outside.Mastermind.Id));

    // A copy of the box directory without one box file, so a catalog loaded from it has never seen that box.
    private sealed class DirectoryWithout : IDisposable
    {
        public string Path { get; } = Directory.CreateTempSubdirectory("legendary-without-").FullName;

        public DirectoryWithout(string fileName)
        {
            foreach (var file in Directory.GetFiles(BoxCatalog.DefaultDirectory, "*.json").Where(file => System.IO.Path.GetFileName(file) != fileName))
            {
                File.Copy(file, System.IO.Path.Combine(Path, System.IO.Path.GetFileName(file)));
            }
        }

        public void Dispose() => Directory.Delete(Path, recursive: true);
    }
}
