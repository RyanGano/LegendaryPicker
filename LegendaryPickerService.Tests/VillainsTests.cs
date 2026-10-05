using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Legendary: Villains box file (Data/Boxes/villains.json), pinned to its rulebook (VIL) and the card
// catalogs. Villains is a base game on the Villainous ruleset, drawn alone: its Plots in catalog order are
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

    private static readonly string[] PlotNames =
    [
        "Build an Underground MegaVault Prison",
        "Cage Villains in Power-Suppressing Cells",
        "Crown Thor King of Asgard",
        "Crush HYDRA",
        "Graduation at Xavier's X-Academy",
        "Infiltrate the Lair with Spies",
        "Mass Produce War Machine Armor",
        "Resurrect Heroes with Norn Stones",
    ];

    private const int CageVillains = 1;
    private const int CrownThor = 2;
    private const int CrushHydra = 3;
    private const int MassProduce = 6;

    private const int DrStrange = 0;
    private const int Odin = 2;

    // Catalog

    // The Villains cards whose text gives Bindings or takes New Recruits or Madame HYDRA (C1, C2). Players recruit
    // from the Madame HYDRA and New Recruit stacks in every Villainous game (VIL p.11).
    [Fact]
    public void Villains_cards_that_take_from_a_stack_list_it()
    {
        ICard[] cards = [.. Villains.Heroes, .. Villains.VillainGroups, .. Villains.HenchmanGroups, .. Villains.Masterminds, .. Villains.Schemes];
        Assert.Equal(
            [
                "villains_hero_enchantress: NewRecruits",
                "villains_hero_kingpin: NewRecruits",
                "villains_hero_loki: Bindings NewRecruits",
                "villains_hero_magneto: Bindings",
                "villains_villain_avengers: Bindings",
                "villains_villain_defenders: Bindings",
                "villains_villain_marvel-knights: Bindings",
                "villains_villain_spider-friends: Bindings",
                "villains_villain_uncanny-avengers: Bindings",
                "villains_villain_uncanny-x-men: Bindings",
                "villains_henchman_cops: NewRecruits",
                "villains_mastermind_dr-strange: Bindings",
                "villains_mastermind_nick-fury: MadameHydra",
                "villains_mastermind_odin: Bindings",
                "villains_mastermind_professor-x: Bindings",
                "villains_scheme_build-an-underground-megavault-prison: Bindings",
                "villains_scheme_crush-hydra: MadameHydra NewRecruits",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
        Assert.Equal([new PartUse(Part.MadameHydra, "VIL p.11"), new PartUse(Part.NewRecruits, "VIL p.11")], Villains.Setup!.Uses);
    }

    [Fact]
    public void Villains_is_a_Villainous_base_game_listed_after_the_expansions_before_it()
    {
        Assert.Equal(["captain-america-75th-anniversary", "civil-war", "core", "dark-city", "deadpool", "fantastic-four", "fear-itself", "guardians-of-the-galaxy", "paint-the-town-red", "secret-wars-volume-1", "secret-wars-volume-2", "villains"], Catalog.Boxes.Select(box => box.Id));
        Assert.Equal("Legendary: Villains", Villains.Name);
        Assert.Equal(7, Villains.SchemaVersion);
        Assert.True(Villains.IsBaseGame);
        Assert.Equal(Ruleset.Villainous, Villains.Ruleset);
    }

    [Fact]
    public void Villains_has_the_15_Allies_7_Adversary_Groups_4_Backup_Adversary_groups_and_4_Commanders_in_the_rulebook()
    {
        Assert.Equal(
            ["Bullseye", "Dr. Octopus", "Electro", "Enchantress", "Green Goblin", "Juggernaut", "Kingpin", "Kraven",
                "Loki", "Magneto", "Mysterio", "Mystique", "Sabretooth", "Ultron", "Venom"],
            Villains.Heroes.Select(hero => hero.Name));
        Assert.Equal(
            ["Avengers", "Defenders", "Marvel Knights", "Spider Friends", "Uncanny Avengers", "Uncanny X-Men", "X-Men First Class"],
            Villains.VillainGroups.Select(group => group.Name));
        Assert.Equal(
            ["Asgardian Warriors", "Cops", "Multiple Man", "S.H.I.E.L.D. Assault Squad"],
            Villains.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(["Dr. Strange", "Nick Fury", "Odin", "Professor X"], Villains.Masterminds.Select(mastermind => mastermind.Name));
        Assert.All(Villains.Masterminds, mastermind => Assert.Null(mastermind.Setup));
    }

    [Fact]
    public void Villains_has_all_8_Plots_each_with_8_Plot_Twists()
    {
        Assert.Equal(PlotNames, Villains.Schemes.Select(scheme => scheme.Name));
        Assert.All(Villains.Schemes, scheme => Assert.Equal(new PlayerCountValue(null, 8, "Card"), Assert.Single(scheme.Setup.Twists)));
        Assert.All(Villains.Schemes, scheme => Assert.Null(scheme.Setup.AllowedPlayerCounts));
    }

    [Fact]
    public void Villains_component_counts_follow_the_rulebook()
    {
        Assert.Equal(
            new BoxComponents(
                new Sourced<int>(14, "VIL p.7"),
                new Sourced<int>(8, "VIL p.6"),
                new Sourced<int>(10, "VIL p.6"),
                new Sourced<int>(12, "VIL p.22"),
                Bystanders: new Sourced<int>(41, "VIL p.5"),
                Bindings: new Sourced<int>(30, "VIL p.5"),
                MadameHydra: new Sourced<int>(12, "VIL p.5"),
                NewRecruits: new Sourced<int>(15, "VIL p.5")),
            Villains.Components);
    }

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
        Assert.Equal(new Rulings("VIL p.6", "VIL p.18", "VIL p.18", "D-uses"), setup.Rulings);
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

    [Theory]
    [InlineData("Dr. Strange", "Defenders", GroupType.Villain)]
    [InlineData("Nick Fury", "Avengers", GroupType.Villain)]
    [InlineData("Odin", "Asgardian Warriors", GroupType.Henchman)]
    [InlineData("Professor X", "X-Men First Class", GroupType.Villain)]
    public void Commander_Always_Leads_the_group_on_its_card(string commander, string group, GroupType type)
    {
        var alwaysLeads = Villains.Masterminds.Single(m => m.Name == commander).AlwaysLeads;

        Assert.Equal(type, alwaysLeads.GroupType);
        Assert.Equal("Card", alwaysLeads.Source);
        var groups = type == GroupType.Villain
            ? Villains.VillainGroups.Select(g => (g.Id, g.Name))
            : Villains.HenchmanGroups.Select(g => (g.Id, g.Name));
        Assert.Equal(group, groups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void The_rulebook_and_the_owner_decision_on_mixed_setups_are_the_source_keys()
    {
        Assert.Equal(
            [
                new SourceLink("VIL", Rulebook),
                new SourceLink("D-mixed", "https://github.com/RyanGano/LegendaryPicker/issues/88"),
                new SourceLink("D-uses", "https://github.com/RyanGano/LegendaryPicker/issues/87"),
                new SourceLink("Q", "https://github.com/RyanGano/LegendaryPicker/blob/main/Docs/BoxResearch/README.md"),
            ],
            Villains.Sources);
        Assert.Equal("VIL p.22; C1; C2", Villains.CatalogSource);
    }

    [Fact]
    public void Villains_glossary_has_its_teams_classes_and_keywords_from_the_rulebook()
    {
        Assert.Equal(
            [
                "Brotherhood team p.19", "Crime Syndicate team p.19", "Foes of Asgard team p.19", "Sinister Six team p.19",
                "Covert class p.19", "Instinct class p.19", "Ranged class p.19", "Strength class p.19", "Tech class p.19",
                "Always Leads keyword p.6", "Ambush keyword p.9", "Command Strike keyword p.10", "Commander Tactic keyword p.13",
                "Demolish keyword p.13", "Dodge keyword p.14", "Elusive keyword p.14", "Fight keyword p.12",
                "Kidnap a Bystander keyword p.15", "Overrun keyword p.8", "Plot Twist keyword p.9", "X-Treme Attack keyword p.14",
            ],
            Villains.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} p.{term.Page}"));
        Assert.All(Villains.Glossary, term => Assert.Equal("VIL", term.Source));
    }

    [Theory]
    [InlineData("Bullseye", "Crime Syndicate", "Covert Instinct Ranged", "Dodge")]
    [InlineData("Green Goblin", "Sinister Six", "Instinct Tech", "Dodge Kidnap a Bystander")]
    [InlineData("Juggernaut", "Brotherhood", "Strength", "")]
    [InlineData("Loki", "Foes of Asgard", "Covert Ranged", "")]
    public void Ally_lists_its_team_classes_and_keywords(string ally, string team, string classes, string keywords)
    {
        var entry = Villains.Heroes.Single(h => h.Name == ally);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    [Fact]
    public void Ultron_is_the_one_unaffiliated_Ally()
    {
        Assert.Equal(["Ultron"], Villains.Heroes.Where(hero => hero.Team is null).Select(hero => hero.Name));
    }

    // Draws: one fixed result per Plot at 1, 2 and 5 players, with Dr. Strange as the Commander. Dr. Strange
    // always leads Defenders except in Solo, which ignores it; every other draw takes the first option left.

    [Theory]
    [InlineData(0, 1, "25 40 5", "Plot sets the Bindings stack to 5 per player|Card")]
    [InlineData(0, 2, "41 39 10", "Plot sets the Bindings stack to 5 per player|Card")]
    [InlineData(0, 5, "77 29 25", "Plot sets the Bindings stack to 5 per player|Card")]
    [InlineData(3, 1, "25 40 30", "")]
    [InlineData(3, 2, "41 39 30", "")]
    [InlineData(3, 5, "77 29 30", "")]
    [InlineData(4, 1, "25 32 30", "Plot moves 8 Bystanders beside it|Card")]
    [InlineData(4, 2, "41 31 30", "Plot moves 8 Bystanders beside it|Card")]
    [InlineData(4, 5, "77 21 30", "Plot moves 8 Bystanders beside it|Card")]
    [InlineData(5, 1, "25 19 30", "Plot moves 21 Bystanders beside it|Card")]
    [InlineData(5, 2, "41 18 30", "Plot moves 21 Bystanders beside it|Card")]
    [InlineData(5, 5, "77 8 30", "Plot moves 21 Bystanders beside it|Card")]
    [InlineData(6, 1, "32 40 30", "Plot overrides Solo: 10 Backup Adversaries of each Backup Adversary group|VIL p.18;Plot requires S.H.I.E.L.D. Assault Squad|Card")]
    [InlineData(6, 2, "41 39 30", "Plot requires S.H.I.E.L.D. Assault Squad|Card")]
    [InlineData(6, 5, "77 29 30", "Plot requires S.H.I.E.L.D. Assault Squad|Card")]
    [InlineData(7, 1, "25 40 30", "")]
    [InlineData(7, 2, "41 39 30", "")]
    [InlineData(7, 5, "77 29 30", "")]
    public void Each_Plot_sets_up_as_its_card_says(int plot, int players, string deckBystandersBindings, string plotNotes)
    {
        var setup = Draw(players, plot, DrStrange);
        var massProduce = plot == MassProduce;

        Assert.Equal(PlotNames[plot], setup.Scheme.Name);
        Assert.Equal("Dr. Strange", setup.Mastermind.Name);
        Assert.Equal(Ruleset.Villainous, setup.Ruleset);

        // The player-count table: Adversary Groups, Backup Adversary groups, Bystanders, and 5 Allies (a 6th
        // with 5 players, VIL p.7); Solo uses 1 group, 3 Backup cards, 1 Bystander and 3 Allies.
        var (groups, backups, allies, bystanders) = players switch
        {
            1 => ("Avengers", massProduce ? "S.H.I.E.L.D. Assault Squad" : "Asgardian Warriors", 3, 1),
            2 => ("Defenders Avengers", massProduce ? "S.H.I.E.L.D. Assault Squad" : "Asgardian Warriors", 5, 2),
            _ => ("Defenders Avengers Marvel Knights Spider Friends",
                massProduce ? "S.H.I.E.L.D. Assault Squad Asgardian Warriors" : "Asgardian Warriors Cops", 6, 12),
        };
        Assert.Equal(groups, string.Join(" ", setup.VillainGroups.Select(group => group.Name)));
        Assert.Equal(backups, string.Join(" ", setup.HenchmanGroups.Select(group => group.Name)));
        Assert.Equal(Villains.Heroes.Take(allies), setup.Heroes);

        var backupCards = players == 1 ? (massProduce ? 10 : 3) : setup.HenchmanGroups.Count * 10;
        Assert.Equal(new VillainDeck(8, 5, setup.VillainGroups.Count * 8, backupCards, bystanders, 0), setup.VillainDeck);
        Assert.Equal(new HeroDeck(allies * 14, 0), setup.HeroDeck);
        Assert.Equal(new PlayerDeck(8, 4), setup.PlayerDeck);

        var expected = deckBystandersBindings.Split(' ').Select(int.Parse).ToArray();
        Assert.Equal(expected[0], setup.VillainDeck.Total);
        Assert.Equal(new SetupStacks(null, null, expected[1], Bindings: expected[2], MadameHydra: 12, NewRecruits: 15), setup.Stacks);

        foreach (var note in plotNotes.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var (text, citation) = (note.Split('|')[0], note.Split('|')[1]);
            Assert.Contains(new RuleNote(text, citation, citation == "Card" ? null : Rulebook), setup.Notes);
        }

        if (players == 1)
        {
            Assert.Contains(new RuleNote("Solo ignores Dr. Strange's Always Leads", "VIL pp.19-20", Rulebook), setup.Notes);
            Assert.Equal(
                [
                    new RuleNote("Solo: Plot Twists also send a Lair Ally costing 6 or less under the Ally Deck", "VIL p.20", Rulebook),
                    new RuleNote("Solo: After each Command Strike, play another card from the Adversary Deck", "VIL p.20", Rulebook),
                ],
                setup.Notes.TakeLast(2));
        }
        else
        {
            Assert.Contains(new RuleNote("Dr. Strange always leads Defenders", "VIL p.6", Rulebook), setup.Notes);
        }
    }

    [Theory]
    [InlineData(4, "Plot moves 8 Bystanders beside it")]
    [InlineData(5, "Plot moves 21 Bystanders beside it")]
    public void A_Plot_that_stacks_Bystanders_beside_it_moves_them_from_the_Bystander_stack(int plot, string note)
    {
        var setup = Draw(2, plot, DrStrange);
        var count = plot == 4 ? 8 : 21;

        Assert.Equal([new MovedCards(CardKind.Bystander, Pile.Bystanders, Pile.BesideScheme, count, count)], setup.Moves);
        Assert.Contains(new RuleNote(note, "Card", null), setup.Notes);
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

    [Theory]
    [InlineData(0, "Dr. Strange always leads Defenders", "Defenders")]
    [InlineData(1, "Nick Fury always leads Avengers", "Avengers")]
    [InlineData(2, "Odin always leads Asgardian Warriors", "Asgardian Warriors")]
    [InlineData(3, "Professor X always leads X-Men First Class", "X-Men First Class")]
    public void Each_Commander_brings_its_Always_Leads_group(int commander, string note, string group)
    {
        var setup = Draw(3, CrushHydra, commander);

        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name).Concat(setup.HenchmanGroups.Select(g => g.Name)));
        Assert.Contains(new RuleNote(note, "VIL p.6", Rulebook), setup.Notes);
    }

    // Cage Villains in Power-Suppressing Cells stacks 2 Cops per player beside it (Card). Those Cops don't count as
    // a Backup Adversary group in the Adversary Deck (VIL p.17), so Cops can still be drawn as one, with what is
    // left of its 10 cards. With Dr. Strange the draws after the Commander pick the next Adversary Group, then the
    // Backup Adversary groups: 0 Asgardian Warriors, 1 Cops.
    [Theory]
    [InlineData(1, new[] { 0, 0 }, "Asgardian Warriors", 2, 0, 25)]
    [InlineData(1, new[] { 0, 1 }, "Cops", 2, 0, 25)]
    [InlineData(2, new[] { 0, 0 }, "Asgardian Warriors", 4, 0, 41)]
    [InlineData(2, new[] { 0, 1 }, "Cops", 4, 4, 37)]
    [InlineData(5, new int[0], "Asgardian Warriors Multiple Man", 10, 0, 77)]
    public void Cage_Villains_stacks_2_Cops_per_player_beside_the_Plot(
        int players, int[] draws, string backups, int cops, int fromDeck, int deckTotal)
    {
        var setup = Assert.IsType<SetupResult>(
            Generator.Generate(players, ["villains"], new ScriptedRandom([CageVillains, DrStrange, .. draws])));

        Assert.Equal(PlotNames[CageVillains], setup.Scheme.Name);
        Assert.Equal(backups, string.Join(" ", setup.HenchmanGroups.Select(group => group.Name)));
        Assert.Equal(
            [new GroupCardsBeside(Villains.HenchmanGroups.Single(group => group.Name == "Cops"), null, cops, fromDeck)],
            setup.CardsBeside);
        Assert.Equal(deckTotal, setup.VillainDeck.Total);
        Assert.Contains(new RuleNote($"Plot sets {cops} Cops beside it, 2 per player", "Card", null), setup.Notes);
    }

    // With 5 players all 10 Cops sit beside the Plot, so none are left to draw as a Backup Adversary group.
    [Fact]
    public void Cage_Villains_with_5_players_never_draws_Cops_into_the_Adversary_Deck()
    {
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(
                Generator.Generate(5, ["villains"], new CyclingRandom(CageVillains, seed, seed + 1, seed + 2, seed + 3, seed + 4)));

            Assert.Equal(PlotNames[CageVillains], setup.Scheme.Name);
            Assert.DoesNotContain("Cops", setup.HenchmanGroups.Select(group => group.Name));
        }
    }

    // Crown Thor King of Asgard sets the Thor Adversary beside it (Card) whether or not the Avengers are in the
    // Adversary Deck (VIL p.17); when they are, the Avengers put their other 7 cards in it.
    [Theory]
    [InlineData(1, new[] { 0 }, "Avengers", 1, 24)]
    [InlineData(2, new[] { 0 }, "Defenders Avengers", 1, 40)]
    [InlineData(2, new[] { 1 }, "Defenders Marvel Knights", 0, 41)]
    [InlineData(5, new int[0], "Defenders Avengers Marvel Knights Spider Friends", 1, 76)]
    public void Crown_Thor_sets_the_Thor_Adversary_beside_the_Plot(int players, int[] draws, string groups, int fromDeck, int deckTotal)
    {
        var setup = Assert.IsType<SetupResult>(
            Generator.Generate(players, ["villains"], new ScriptedRandom([CrownThor, DrStrange, .. draws])));

        Assert.Equal(PlotNames[CrownThor], setup.Scheme.Name);
        Assert.Equal(groups, string.Join(" ", setup.VillainGroups.Select(group => group.Name)));
        Assert.Equal(setup.VillainGroups.Count * 8, setup.VillainDeck.VillainCards);
        Assert.Equal(
            [new GroupCardsBeside(Villains.VillainGroups.Single(group => group.Name == "Avengers"), "Thor", 1, fromDeck)],
            setup.CardsBeside);
        Assert.Equal(deckTotal, setup.VillainDeck.Total);
        Assert.Contains(new RuleNote("Plot sets Thor of Avengers beside it", "Card", null), setup.Notes);
    }

    // Which boxes a draw comes from.

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void A_Villains_only_draw_holds_no_card_from_another_box(int players)
    {
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, ["villains"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.All(ComponentIds(setup), id => Assert.StartsWith("villains_", id));
            Assert.Equal(Ruleset.Villainous, setup.Ruleset);
            Assert.Equal(["villains"], setup.Boxes.Select(box => box.Id));
            Assert.False(setup.Mixed);
            Assert.Null(setup.RulesReason);
            Assert.Null(setup.PlayerDeck.Choices);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void A_core_only_draw_holds_no_Villains_card_and_no_Villainous_stack(int players)
    {
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, ["core"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.All(ComponentIds(setup), id => Assert.StartsWith("core_", id));
            Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
            Assert.False(setup.Mixed);
            Assert.Null(setup.RulesReason);
            Assert.Null(setup.Stacks.Bindings);
            Assert.Null(setup.Stacks.MadameHydra);
            Assert.Null(setup.Stacks.NewRecruits);
        }
    }

    private static SetupResult Draw(int players, int plot, int commander) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, ["villains"], new ScriptedRandom(plot, commander)));

    private static string TermName(string id) =>
        Catalog.Boxes.SelectMany(box => box.Glossary).Single(term => term.Id == id).Name;

    private static IEnumerable<string> ComponentIds(SetupResult setup) =>
        new[] { setup.Scheme.Id, setup.Mastermind.Id }
            .Concat(setup.VillainGroups.Select(group => group.Id))
            .Concat(setup.HenchmanGroups.Select(group => group.Id))
            .Concat(setup.Heroes.Select(hero => hero.Id))
            .Concat(setup.OutsideHeroes.Select(outside => outside.Hero.Id));
}
