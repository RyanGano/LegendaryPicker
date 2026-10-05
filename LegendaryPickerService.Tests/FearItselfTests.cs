using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Fear Itself box file (Data/Boxes/fear-itself.json), pinned to the rules insert (FI), the card catalog and the
// scanned card faces, and drawn with Legendary: Villains, whose rules its Plots and Commander follow. Catalog order
// puts its cards before the Villains box's, so at 2–5 players its Plots are 0 Fear Itself, 1 Last Stand at Avengers
// Tower and 2 The Traitor; Solo leaves out The Traitor. Its Commander, Uru-Enchanted Iron Man, is Commander 0.
public class FearItselfTests
{
    private const string FearItselfName = "Fear Itself";
    private const string Insert = "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Fear_Itself.pdf";
    private const string OwnerDecision = "https://github.com/RyanGano/LegendaryPicker/issues/110";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box FearItself = Catalog.Boxes.Single(box => box.Id == "fear-itself");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["villains", "fear-itself"];
    private static readonly string[] CoreAndFearItself = ["core", "fear-itself"];

    private const int FearItselfPlot = 0;
    private const int LastStand = 1;
    private const int TheTraitor = 2;

    private const int UruEnchantedIronMan = 0;

    // Catalog

    [Fact]
    public void Fear_Itself_is_a_Villainous_expansion_with_the_cards_of_the_rules_insert()
    {
        Assert.Equal(FearItselfName, FearItself.Name);
        Assert.Equal(6, FearItself.SchemaVersion);
        Assert.False(FearItself.IsBaseGame);
        Assert.Equal(Ruleset.Villainous, FearItself.Ruleset);

        var components = FearItself.Components;
        Assert.Equal(new Sourced<int>(14, "FI p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "FI p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "FI p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "FI p.2"), components.SchemeTwists);
        Assert.Null(components.Bystanders);
        Assert.Null(components.Bindings);
        Assert.Null(components.MadameHydra);
        Assert.Null(components.NewRecruits);
    }

    [Fact]
    public void Fear_Itself_has_the_6_Allies_1_Adversary_Group_1_Commander_and_3_Plots_in_the_rules_insert()
    {
        Assert.Equal(
            ["Greithoth, Breaker of Wills", "Kuurth, Breaker of Stone", "Nerkkod, Breaker of Oceans", "Nul, Breaker of Worlds", "Skadi", "Skirn, Breaker of Men"],
            FearItself.Heroes.Select(hero => hero.Name));
        Assert.Equal(["The Mighty"], FearItself.VillainGroups.Select(group => group.Name));
        Assert.Empty(FearItself.HenchmanGroups);
        var commander = Assert.Single(FearItself.Masterminds);
        Assert.Equal("Uru-Enchanted Iron Man", commander.Name);
        Assert.Null(commander.Setup);
        Assert.Equal(new AlwaysLeadsGroup("fear-itself_villain_the-mighty", GroupType.Villain, "Card"), commander.AlwaysLeads);
        Assert.Equal(["Fear Itself", "Last Stand at Avengers Tower", "The Traitor"], FearItself.Schemes.Select(scheme => scheme.Name));
    }

    // Nerkkod and Skirn gain New Recruits, Skadi a Madame HYDRA; Nul, The Mighty and a Commander Tactic give
    // Bindings; The Traitor takes its Betrayal Deck's Bindings from the stack (Card).
    [Fact]
    public void Fear_Itself_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. FearItself.Heroes, .. FearItself.VillainGroups, .. FearItself.Masterminds, .. FearItself.Schemes];
        Assert.Equal(
            [
                "fear-itself_hero_nerkkod-breaker-of-oceans: NewRecruits",
                "fear-itself_hero_nul-breaker-of-worlds: Bindings",
                "fear-itself_hero_skadi: MadameHydra",
                "fear-itself_hero_skirn-breaker-of-men: NewRecruits",
                "fear-itself_villain_the-mighty: Bindings",
                "fear-itself_mastermind_uru-enchanted-iron-man: Bindings",
                "fear-itself_scheme_the-traitor: Bindings",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Plot_carries_the_Setup_line_on_its_card()
    {
        var (fear, lastStand, traitor) = (FearItself.Schemes[0], FearItself.Schemes[1], FearItself.Schemes[2]);

        Assert.Equal([new PlayerCountValue(null, 10, "Card")], fear.Setup.Twists);
        Assert.Equal(
            [
                new SetupStep("Start the Fear Level at 8 and keep 8 Allies in the Lair", "Card"),
                new SetupStep("Lay the 6th to 8th Lair Allies in a second row under the Lair", "FI p.2"),
            ],
            fear.Setup.Steps);

        Assert.Equal([new PlayerCountValue(null, 6, "Card")], lastStand.Setup.Twists);
        Assert.Null(lastStand.Setup.Steps);

        Assert.Equal([new PlayerCountValue(null, 8, "Card")], traitor.Setup.Twists);
        Assert.Equal([2, 3, 4, 5], traitor.Setup.AllowedPlayerCounts!.Value);
        Assert.Equal("Card", traitor.Setup.AllowedPlayerCounts.Source);
        Assert.Equal(
            [
                (CardKind.Binding, Pile.SetAside, true, new PlayerCountValue(null, 3, "Card")),
                (CardKind.Twist, Pile.SetAside, false, new PlayerCountValue(null, 1, "Card")),
            ],
            traitor.Setup.Moves!.Select(move => (move.Card, move.To, move.PerPlayer, Assert.Single(move.Count))));
        Assert.Equal([new SetupStep("Shuffle all the set-aside cards face down as the Betrayal Deck", "Card")], traitor.Setup.Steps);

        Assert.All([fear, lastStand], plot => Assert.Null(plot.Setup.AllowedPlayerCounts));
    }

    [Fact]
    public void The_rules_insert_and_the_owner_decision_are_the_source_keys_and_its_glossary_adds_HYDRA_and_three_keywords()
    {
        Assert.Equal([new SourceLink("FI", Insert), new SourceLink("D-heroic", OwnerDecision)], FearItself.Sources);
        Assert.Equal("FI p.2; C1", FearItself.CatalogSource);
        Assert.Equal(
            ["HYDRA team FI p.1", "Thrown Artifact keyword FI p.1", "Uru-Enchanted Weapons keyword FI p.1", "Fight or Fail keyword FI p.1"],
            FearItself.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    [Theory]
    [InlineData("Greithoth, Breaker of Wills", "Foes of Asgard", "Covert Instinct Strength", "Thrown Artifact")]
    [InlineData("Nul, Breaker of Worlds", "Foes of Asgard", "Instinct Strength", "Demolish Thrown Artifact")]
    [InlineData("Skadi", "HYDRA", "Covert Strength Tech", "Thrown Artifact")]
    public void Ally_lists_its_team_classes_and_keywords(string ally, string team, string classes, string keywords)
    {
        var entry = FearItself.Heroes.Single(h => h.Name == ally);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Fixed-result draws: each Plot at 1, 2 and 5 players with Uru-Enchanted Iron Man, who leads The Mighty (except in
    // Solo, where The Mighty is still the first Adversary Group drawn), and the first remaining option for every later
    // draw. Villains gives 41 Bystanders, 30 Bindings, 12 Madame HYDRA and 15 New Recruits; The Mighty uses Bindings.

    [Theory]
    [InlineData(FearItselfPlot, 1, 10, 8, 3, 1, 27, 42, 40, 30)]
    [InlineData(FearItselfPlot, 2, 10, 16, 10, 2, 43, 70, 39, 30)]
    [InlineData(FearItselfPlot, 5, 10, 32, 20, 12, 79, 84, 29, 30)]
    [InlineData(LastStand, 1, 6, 8, 3, 1, 23, 42, 40, 30)]
    [InlineData(LastStand, 2, 6, 16, 10, 2, 39, 70, 39, 30)]
    [InlineData(LastStand, 5, 6, 32, 20, 12, 75, 84, 29, 30)]
    [InlineData(TheTraitor, 2, 8, 16, 10, 2, 41, 70, 39, 24)]
    [InlineData(TheTraitor, 5, 8, 32, 20, 12, 77, 84, 29, 15)]
    public void Plot_lays_out_its_decks_and_stacks(
        int plot, int players, int twists, int villainCards, int henchmanCards, int bystanders,
        int adversaryDeck, int allyDeck, int bystanderStack, int bindings)
    {
        var setup = Draw(players, plot, UruEnchantedIronMan);

        Assert.Equal(FearItself.Schemes[plot].Name, setup.Scheme.Name);
        Assert.Equal("Uru-Enchanted Iron Man", setup.Mastermind.Name);
        Assert.Equal(Ruleset.Villainous, setup.Ruleset);
        Assert.Equal("The Mighty", setup.VillainGroups[0].Name);
        Assert.Equal(new VillainDeck(twists, 5, villainCards, henchmanCards, bystanders, 0), setup.VillainDeck);
        Assert.Equal(adversaryDeck, setup.VillainDeck.Total);
        Assert.Equal(allyDeck, setup.HeroDeck.Total);
        Assert.Equal(new SetupStacks(null, null, bystanderStack, Bindings: bindings, MadameHydra: 12, NewRecruits: 15), setup.Stacks);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void The_Fear_Itself_Plot_starts_the_Lair_with_8_Allies(int players)
    {
        var setup = Draw(players, FearItselfPlot, UruEnchantedIronMan);

        Assert.Equal(
            ["Start the Fear Level at 8 and keep 8 Allies in the Lair", "Lay the 6th to 8th Lair Allies in a second row under the Lair"],
            setup.Steps);
        Assert.Contains(
            new RuleNote("Plot adds a setup step: Lay the 6th to 8th Lair Allies in a second row under the Lair", "FI p.2", Insert, FearItselfName),
            setup.Notes);
    }

    // The Betrayal Deck takes 3 Bindings per player from the Bindings stack and a 9th Twist from those the Adversary
    // Deck doesn't use.
    [Theory]
    [InlineData(2, 6)]
    [InlineData(3, 9)]
    [InlineData(5, 15)]
    public void The_Traitor_sets_aside_3_Bindings_per_player_and_a_9th_Twist_as_the_Betrayal_Deck(int players, int bindings)
    {
        var setup = Draw(players, TheTraitor, UruEnchantedIronMan);

        Assert.Equal(
            [
                new MovedCards(CardKind.Binding, Pile.Bindings, Pile.SetAside, bindings, bindings),
                new MovedCards(CardKind.Twist, Pile.Twists, Pile.SetAside, 1, 1),
            ],
            setup.Moves);
        Assert.Equal(8, setup.VillainDeck.Twists);
        Assert.Equal(30 - bindings, setup.Stacks.Bindings);
        Assert.Equal(["Shuffle all the set-aside cards face down as the Betrayal Deck"], setup.Steps);
        Assert.Contains(new RuleNote($"Plot moves {bindings} Bindings into a stack set aside, 3 per player", "Card", null, FearItselfName), setup.Notes);
        Assert.Contains(new RuleNote("Plot moves 1 Plot Twist into a stack set aside", "Card", null, FearItselfName), setup.Notes);
    }

    // The Traitor is for 2 or more players, so Solo draws from the other 2 Fear Itself Plots and the 8 Villains Plots.
    [Fact]
    public void The_Traitor_is_never_drawn_in_Solo()
    {
        var solo = new ScriptedRandom();
        Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, solo));
        var twoPlayers = new ScriptedRandom();
        Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, twoPlayers));

        Assert.Equal((10, 11), (solo.Options[0], twoPlayers.Options[0]));
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            Assert.NotEqual("The Traitor", setup.Scheme.Name);
        }
    }

    [Fact]
    public void Uru_Enchanted_Iron_Man_brings_The_Mighty()
    {
        var setup = Draw(3, LastStand, UruEnchantedIronMan);

        Assert.Equal(["The Mighty", "Avengers", "Defenders"], setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(
            new RuleNote("Uru-Enchanted Iron Man always leads The Mighty", "VIL p.6", "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf", "Legendary: Villains"),
            setup.Notes);
    }

    // Without a Villainous base game (D-heroic, #110)

    // The insert lets Fear Itself be played with Heroic sets alone (FI p.2), and the owner decided it should be: its
    // Plots and Commander then follow the core box's rules, and parts no included box has get stand-ins.
    [Fact]
    public void Fear_Itself_can_be_drawn_with_the_core_box_alone()
    {
        var other = FearItself.OtherRuleset!;
        Assert.Equal("D-heroic", other.Source);
        Assert.Equal(
            [
                new StandIn(Part.Bindings, "FI p.2", Part.Wounds),
                new StandIn(Part.MadameHydra, "FI p.2", Part.Officers),
                new StandIn(Part.NewRecruits, "FI p.2"),
            ],
            other.StandIns);
        Assert.Null(Generator.CheckBoxes(["core", "fear-itself"]));
        Assert.Null(Generator.CheckBoxes(["core", "villains", "fear-itself"]));
    }

    // Core Schemes come first: 8 of them, then Fear Itself's Plots. Uru-Enchanted Iron Man follows the 4 core
    // Masterminds. The core box's 30 Wounds stand in for the Bindings The Mighty and Iron Man use.
    [Theory]
    [InlineData(FearItselfPlot, 2, 10, 30)]
    [InlineData(FearItselfPlot, 5, 10, 30)]
    [InlineData(LastStand, 2, 6, 30)]
    [InlineData(LastStand, 5, 6, 30)]
    [InlineData(TheTraitor, 2, 8, 24)]
    [InlineData(TheTraitor, 5, 8, 15)]
    public void Each_Plot_with_the_core_box_alone_follows_the_First_Edition_rules_with_Wounds_for_Bindings(
        int plot, int players, int twists, int wounds)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, CoreAndFearItself, new ScriptedRandom(8 + plot, 4)));

        Assert.Equal((FearItself.Schemes[plot].Name, "Uru-Enchanted Iron Man"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
        Assert.Equal(
            new RuleNote("First Edition rules: no Villainous base game is included", "D-heroic", OwnerDecision),
            setup.RulesReason);
        Assert.Equal("The Mighty", setup.VillainGroups[0].Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Equal(new PlayerDeck(8, 4, null), setup.PlayerDeck);
        Assert.Equal(wounds, setup.Stacks.Wounds);
        Assert.Null(setup.Stacks.Bindings);
        Assert.Contains(new StandIn(Part.Bindings, "FI p.2", Part.Wounds), setup.StandIns!);
        Assert.Contains(
            new RuleNote(
                "No included box has Bindings cards: use Wound cards for them, or Bindings cards if you have them", "FI p.2", Insert, FearItselfName),
            setup.Notes);
    }

    // The Betrayal Deck takes Wounds in place of Bindings when no included box has Bindings.
    [Theory]
    [InlineData(2, 6)]
    [InlineData(5, 15)]
    public void The_Traitor_with_the_core_box_alone_sets_aside_Wounds(int players, int wounds)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, CoreAndFearItself, new ScriptedRandom(8 + TheTraitor, 4)));

        Assert.Equal(
            [
                new MovedCards(CardKind.Wound, Pile.Wounds, Pile.SetAside, wounds, wounds),
                new MovedCards(CardKind.Twist, Pile.Twists, Pile.SetAside, 1, 1),
            ],
            setup.Moves);
        Assert.Equal(30 - wounds, setup.Stacks.Wounds);
        Assert.Contains(new RuleNote($"Plot moves {wounds} Wounds into a stack set aside, 3 per player", "Card", null, FearItselfName), setup.Notes);
    }

    // With Villains included its Bindings are used, so nothing stands in for them.
    [Fact]
    public void The_Traitor_with_the_core_box_and_Villains_still_sets_aside_Bindings()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "villains", "fear-itself"], new ScriptedRandom(8 + TheTraitor, 4)));

        Assert.Equal(("The Traitor", Ruleset.Villainous), (setup.Scheme.Name, setup.Ruleset));
        Assert.Equal(new MovedCards(CardKind.Binding, Pile.Bindings, Pile.SetAside, 6, 6), setup.Moves[0]);
        Assert.Equal(24, setup.Stacks.Bindings);
        Assert.Empty(setup.StandIns!);
    }

    // Every Plot can be drawn with the core box alone at 2 and 5 players, whatever the other draws.
    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    public void Every_Plot_is_drawable_with_the_core_box_alone(int players)
    {
        foreach (var plot in new[] { FearItselfPlot, LastStand, TheTraitor })
        {
            for (var seed = 0; seed < 20; seed++)
            {
                var random = new CyclingRandom(8 + plot, seed, seed + 3, seed + 1, seed + 4, seed + 1, seed + 5, seed + 9, seed + 2, seed + 6);
                var setup = Assert.IsType<SetupResult>(Generator.Generate(players, CoreAndFearItself, random));
                Assert.Equal(FearItself.Schemes[plot].Name, setup.Scheme.Name);
                Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
                Assert.Null(setup.Stacks.Bindings);
            }
        }
    }

    // Skadi gains Madame HYDRA, so with the core box alone she uses its S.H.I.E.L.D. Officers instead (FI p.2).
    [Fact]
    public void Skadi_with_the_core_box_alone_uses_SHIELD_Officers_for_Madame_HYDRA()
    {
        // Legacy Virus and Dr. Doom, two core Villain Groups, then Skadi (Hero 19: 15 core, then Fear Itself's).
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, CoreAndFearItself, new ScriptedRandom(0, 0, 0, 0, 19)));

        Assert.Equal("Skadi", setup.Heroes[0].Name);
        Assert.Equal(30, setup.Stacks.Officers);
        Assert.Null(setup.Stacks.MadameHydra);
        Assert.Contains(new StandIn(Part.MadameHydra, "FI p.2", Part.Officers), setup.StandIns!);
        Assert.Contains(
            new RuleNote(
                "No included box has Madame HYDRA cards: use S.H.I.E.L.D. Officer cards for them, or Madame HYDRA cards if you have them",
                "FI p.2", Insert, FearItselfName),
            setup.Notes);
    }

    // A core Scheme and Mastermind with no drawn card that uses a missing part need no stand-in.
    [Fact]
    public void A_core_draw_with_the_core_box_and_Fear_Itself_needs_no_stand_in_when_no_card_uses_a_missing_part()
    {
        // Legacy Virus and Dr. Doom, then the first options: core Villain Groups and Heroes.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, CoreAndFearItself, new ScriptedRandom(0, 0)));

        Assert.DoesNotContain(ComponentIds(setup), id => id.StartsWith("fear-itself_"));
        Assert.Empty(setup.StandIns!);
        Assert.Equal("First Edition rules: no Villainous base game is included", setup.RulesReason!.Text);
    }

    // With the core box and Villains, a Heroic Scheme and Mastermind keep the First Edition rules (#88), and Skadi, a
    // Fear Itself Ally, brings the Madame HYDRA she gains (#87) beside the S.H.I.E.L.D. Officers.
    [Fact]
    public void A_Heroic_draw_with_a_Fear_Itself_Ally_follows_the_First_Edition_rules_and_lays_out_what_she_uses()
    {
        // Legacy Virus and Dr. Doom, two core Villain Groups, then Skadi (Hero 19: 15 core, then Fear Itself's).
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "villains", "fear-itself"], new ScriptedRandom(0, 0, 0, 0, 19)));

        Assert.Equal(("Legacy Virus", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
        Assert.True(setup.Mixed);
        Assert.Equal("Skadi", setup.Heroes[0].Name);
        Assert.Equal(12, setup.Stacks.MadameHydra);
        Assert.Equal(30, setup.Stacks.Officers);
        Assert.Null(setup.Stacks.NewRecruits);
        Assert.Equal(new PlayerDeck(8, 4, null), setup.PlayerDeck);
    }

    // A Fear Itself Plot with a core Mastermind follows the Villains rules (D-mixed).
    [Fact]
    public void A_Fear_Itself_Plot_with_a_core_Mastermind_follows_the_Villains_rules()
    {
        // Core Schemes come first: 8 of them, then Fear Itself's Plots. Dr. Doom is the first completion.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "villains", "fear-itself"], new ScriptedRandom(8, 0)));

        Assert.Equal(("Fear Itself", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(Ruleset.Villainous, setup.Ruleset);
        Assert.Equal(10, setup.VillainDeck.Twists);
        Assert.Equal("Villains rules: the Plot is Fear Itself", setup.RulesReason!.Text);
    }

    // The exclusion case: with every other box included, Fear Itself changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Fear_Itself_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("fear-itself.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "guardians-of-the-galaxy", "paint-the-town-red", "villains"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("fear-itself_"));
        }
    }

    private static SetupResult Draw(int players, int plot, int commander) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(plot, commander)));

    private static string TermName(string id) =>
        Catalog.Boxes.SelectMany(box => box.Glossary).Single(term => term.Id == id).Name;

    private static IEnumerable<string> ComponentIds(SetupResult setup) =>
        new[] { setup.Scheme.Id, setup.Mastermind.Id }
            .Concat(setup.VillainGroups.Select(group => group.Id))
            .Concat(setup.HenchmanGroups.Select(group => group.Id))
            .Concat(setup.Heroes.Select(hero => hero.Id))
            .Concat(setup.OutsideHeroes.Select(outside => outside.Hero.Id));

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
