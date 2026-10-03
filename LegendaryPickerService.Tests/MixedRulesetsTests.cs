using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Setups that include Heroic (First Edition) and Villainous boxes together, against the real box files. The
// drawn cards decide the rules (owner decision D-mixed, #85): no Villainous card means First Edition rules in
// full; any Villainous card means the Villains rules, and Heroic cards drawn with it make a mixed setup under
// the Villains rulebook's combined setup (VIL pp.20-21).
public class MixedRulesetsTests
{
    private const string Rulebook = "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf";
    private const string CoreRulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";
    private const string Decision = "https://github.com/RyanGano/LegendaryPicker/issues/85";
    private const string UsesDecision = "https://github.com/RyanGano/LegendaryPicker/issues/87";
    private const string VillainsBox = "Legendary: Villains";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);
    private static readonly string[] CoreAndVillains = ["core", "villains"];

    [Fact]
    public void Heroic_and_Villainous_boxes_can_be_included_together_once_a_base_game_has_rules_for_mixing_them()
    {
        Assert.Null(Generator.CheckBoxes(CoreAndVillains));
        Assert.Null(Generator.CheckBoxes(["villains", "dark-city", "fantastic-four", "paint-the-town-red"]));
    }

    // Legacy Virus and Dr. Doom are both Heroic, so the rest is drawn from the Heroic boxes under the core rules:
    // 5 Heroes with 5 players, the core Solo, and only the Heroic stacks and starting deck.
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void A_draw_with_no_Villainous_card_follows_the_First_Edition_rules_in_full(int players)
    {
        var setup = Draw(players, CoreAndVillains, "Legacy Virus", "Dr. Doom");

        Assert.Equal(Ruleset.FirstEdition, setup.Ruleset);
        Assert.False(setup.Mixed);
        Assert.Equal(new RuleNote("First Edition rules: the setup has no Villainous cards", "D-mixed", Decision), setup.RulesReason);
        Assert.All(ComponentIds(setup), id => Assert.StartsWith("core_", id));

        var solo = players == 1;
        Assert.Equal(solo ? 3 : 5, setup.Heroes.Count);
        Assert.Equal(solo ? 1 : 5, setup.VillainDeck.MasterStrikes);
        Assert.Equal(solo ? 1 : 12, setup.VillainDeck.Bystanders);
        Assert.Equal(new SetupStacks(6 * players, 30, 30 - setup.VillainDeck.Bystanders), setup.Stacks);
        Assert.Equal(new PlayerDeck(8, 4), setup.PlayerDeck);
        Assert.DoesNotContain(setup.Notes, note => note.Text.StartsWith("Mixed sets", StringComparison.Ordinal));
        if (solo)
        {
            Assert.Equal(
                new RuleNote("Solo: After each Twist, KO a Hero costing 6 or less from the HQ", "R p.20", CoreRulebook, "Marvel Legendary First Edition core box"),
                setup.Notes[^1]);
        }
    }

    // Crush HYDRA is a Villainous Plot, so the Villains table (a 6th Ally with 5 players, VIL p.7) and the
    // Villains Solo (VIL pp.19-20) apply; Dr. Doom and the first groups and Heroes left are Heroic, so the
    // setup is mixed: the recruit stacks of both base games, all Bystanders, and a choice of starting deck.
    // Brotherhood uses Wounds, but no drawn card uses Bindings, so they are left out.
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void A_draw_with_a_Villainous_card_follows_the_Villains_table_and_Solo(int players)
    {
        var setup = Draw(players, CoreAndVillains, "Crush HYDRA", "Dr. Doom");

        Assert.Equal(Ruleset.Villainous, setup.Ruleset);
        Assert.True(setup.Mixed);
        Assert.Equal(new RuleNote("Villains rules: the setup includes Villainous cards", "D-mixed", Decision), setup.RulesReason);

        var solo = players == 1;
        Assert.Equal(solo ? 3 : 6, setup.Heroes.Count);
        Assert.Equal(new VillainDeck(8, 5, solo ? 8 : 32, solo ? 3 : 20, solo ? 1 : 12, 0), setup.VillainDeck);
        Assert.Equal(new SetupStacks(30, 30, 30 + 41 - setup.VillainDeck.Bystanders, MadameHydra: 12, NewRecruits: 15), setup.Stacks);
        Assert.Equal((8, 4), (setup.PlayerDeck.Agents, setup.PlayerDeck.Troopers));
        Assert.Equal([Ruleset.FirstEdition, Ruleset.Villainous], setup.PlayerDeck.Choices);

        Assert.Contains(
            new RuleNote(
                "Mixed sets: Schemes and Plots, Masterminds and Commanders, Heroes and Allies and the groups each come from one pool",
                "VIL pp.20-21", Rulebook, VillainsBox),
            setup.Notes);
        Assert.Contains(
            new RuleNote(
                "Mixed sets: lay out the recruit stacks of every included base game, and shuffle all Bystanders together",
                "VIL p.21", Rulebook, VillainsBox),
            setup.Notes);
        Assert.Contains(new RuleNote("Leave out the Bindings stack: no drawn card uses it", "D-uses", UsesDecision, VillainsBox), setup.Notes);
        Assert.Contains(new RuleNote("Mixed sets: the players choose S.H.I.E.L.D. or HYDRA starting decks", "VIL p.21", Rulebook, VillainsBox), setup.Notes);
        if (solo)
        {
            Assert.Contains(new RuleNote("Solo ignores Dr. Doom's Always Leads", "VIL pp.19-20", Rulebook, VillainsBox), setup.Notes);
            Assert.Equal(
                [
                    new RuleNote("Solo: Plot Twists also send a Lair Ally costing 6 or less under the Ally Deck", "VIL p.20", Rulebook, VillainsBox),
                    new RuleNote("Solo: After each Command Strike, play another card from the Adversary Deck", "VIL p.20", Rulebook, VillainsBox),
                ],
                setup.Notes.TakeLast(2));
        }
        else
        {
            Assert.Contains(new RuleNote("Dr. Doom always leads Doombot Legion", "VIL p.6", Rulebook, VillainsBox), setup.Notes);
        }
    }

    // With Core + Villains, a draw whose cards are all Villainous follows the Villains rules without being mixed:
    // only the Villainous stacks and starting deck, and no mixed-set notes. Drawing the last option each time
    // takes Villains' cards, which follow the core box's in catalog order.
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void A_draw_with_only_Villainous_cards_follows_the_Villains_rules_and_is_not_mixed(int players)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, CoreAndVillains, new LastOption()));

        Assert.All(ComponentIds(setup), id => Assert.StartsWith("villains_", id));
        Assert.Equal(Ruleset.Villainous, setup.Ruleset);
        Assert.False(setup.Mixed);
        Assert.Equal(new RuleNote("Villains rules: the setup includes Villainous cards", "D-mixed", Decision), setup.RulesReason);
        Assert.Equal(players == 1 ? 3 : 6, setup.Heroes.Count);
        Assert.Null(setup.Stacks.Wounds);
        Assert.Null(setup.Stacks.Officers);
        Assert.Equal(41 - setup.VillainDeck.Bystanders, setup.Stacks.Bystanders);
        Assert.Null(setup.PlayerDeck.Choices);
        Assert.DoesNotContain(setup.Notes, note => note.Text.StartsWith("Mixed sets", StringComparison.Ordinal));
    }

    // Crush HYDRA and Dr. Strange with 3 players: Dr. Strange's Defenders take one Villain Group slot and the
    // first Heroic groups the rest; four Heroes, then the 12th left, the first Ally, Bullseye.
    [Fact]
    public void A_mixed_setup_draws_Heroes_and_Allies_and_groups_from_one_pool()
    {
        var scheme = IndexOf(3, CoreAndVillains, "Crush HYDRA");
        var mastermind = IndexOf(3, CoreAndVillains, "Dr. Strange", scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, CoreAndVillains, new ScriptedRandom(scheme, mastermind, 0, 0, 0, 0, 0, 0, 0, 11)));

        Assert.Equal(["Defenders", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Deadpool", "Bullseye"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(["core_hero_black-widow", "villains_hero_bullseye"], [setup.Heroes[0].Id, setup.Heroes[4].Id]);
        Assert.Equal(new HeroDeck(70, 0), setup.HeroDeck);
        Assert.True(setup.Mixed);
    }

    // A Heroic Scheme's own notes still call it a Scheme when a Villainous Commander makes the setup mixed.
    [Fact]
    public void A_mixed_setup_names_each_card_by_its_own_side()
    {
        var setup = Draw(3, CoreAndVillains, "Legacy Virus", "Dr. Strange");

        Assert.Equal(Ruleset.Villainous, setup.Ruleset);
        Assert.Contains(new RuleNote("Scheme sets the Wound stack to 6 per player", "Card", null, "Marvel Legendary First Edition core box"), setup.Notes);
        Assert.Equal(18, setup.Stacks.Wounds);
    }

    // A mixed setup lays out Wounds and Bindings only where a drawn card uses them (D-uses). Dr. Strange's card
    // gives Bindings, and Legacy Virus sets the Wound stack alone. In Solo, Nick Fury, X-Men First Class (the
    // 14th of the 7 Heroic and 7 Villainous groups), Doombot Legion and the first three Heroes use neither,
    // so Midtown Bank Robbery leaves both out; with Legacy Virus the Wounds are back and the Bindings stay out.
    // The recruit stacks of both base games stay, since players recruit from them (VIL p.21).
    [Fact]
    public void A_mixed_setup_lays_out_Wounds_and_Bindings_only_when_a_drawn_card_uses_them()
    {
        var strange = Draw(3, CoreAndVillains, "Legacy Virus", "Dr. Strange");
        Assert.True(strange.Mixed);
        Assert.Equal(18, strange.Stacks.Wounds);
        Assert.Equal(30, strange.Stacks.Bindings);

        var midtown = Draw(1, CoreAndVillains, "Midtown Bank Robbery", "Nick Fury", 13, 0, 0, 0, 0);
        Assert.True(midtown.Mixed);
        Assert.Equal(["X-Men First Class"], midtown.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Doombot Legion"], midtown.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(["Black Widow", "Captain America", "Cyclops"], midtown.Heroes.Select(hero => hero.Name));
        Assert.Equal(new SetupStacks(null, 30, 30 + 41 - 12, MadameHydra: 12, NewRecruits: 15), midtown.Stacks);
        Assert.Contains(new RuleNote("Leave out the Wound and Bindings stacks: no drawn card uses them", "D-uses", UsesDecision, VillainsBox), midtown.Notes);

        var legacyVirus = Draw(1, CoreAndVillains, "Legacy Virus", "Nick Fury", 13, 0, 0, 0, 0);
        Assert.Equal(new SetupStacks(6, 30, 30 + 41 - 1, MadameHydra: 12, NewRecruits: 15), legacyVirus.Stacks);
        Assert.Contains(new RuleNote("Leave out the Bindings stack: no drawn card uses it", "D-uses", UsesDecision, VillainsBox), legacyVirus.Notes);
    }

    // Villains with Dark City has no box that supplies Wounds, so Mephisto, Stryfe and the Dark City cards that
    // give Wounds can't be set up and are never drawn.
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void A_card_whose_Wounds_no_included_box_supplies_is_never_drawn(int players)
    {
        string[] boxes = ["villains", "dark-city"];
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            ICard[] cards = [setup.Scheme, setup.Mastermind, .. setup.VillainGroups, .. setup.HenchmanGroups, .. setup.Heroes];
            Assert.DoesNotContain(cards, card => card.Parts.Contains(Part.Wounds));
            Assert.Null(setup.Stacks.Wounds);
        }
    }

    // Villains with Heroic expansions and no Heroic base game: a draw with no Villainous card would need the
    // First Edition rules, which no included box has, so every draw holds a Villainous Plot or Commander.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Villains_with_Heroic_expansions_always_draws_a_Villainous_card(int players)
    {
        string[] boxes = ["villains", "dark-city"];
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equal(Ruleset.Villainous, setup.Ruleset);
            Assert.Contains(new[] { setup.Scheme.Id, setup.Mastermind.Id }, id => id.StartsWith("villains_", StringComparison.Ordinal));
            Assert.Equal("Villains rules: the setup includes Villainous cards", setup.RulesReason?.Text);
            Assert.Null(setup.PlayerDeck.Choices);
        }
    }

    // A Dark City Scheme needs a Villainous Commander: its Heroic Masterminds would need the First Edition rules.
    [Fact]
    public void A_Heroic_Scheme_in_a_Villains_setup_is_completed_only_by_Villainous_Commanders()
    {
        var random = new ScriptedRandom(0);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["villains", "dark-city"], random));

        Assert.StartsWith("dark-city_scheme_", setup.Scheme.Id);
        Assert.Equal(Catalog.Boxes.Single(box => box.Id == "villains").Masterminds.Count, random.Options[1]);
        Assert.StartsWith("villains_mastermind_", setup.Mastermind.Id);
        Assert.True(setup.Mixed);
        Assert.Null(setup.Stacks.Wounds);
        Assert.Null(setup.Stacks.Officers);
    }

    // Draws the named Scheme and Mastermind, then the scripted draws that follow them.
    private static SetupResult Draw(int players, string[] boxes, string scheme, string mastermind, params int[] rest)
    {
        var schemeIndex = IndexOf(players, boxes, scheme);
        return Assert.IsType<SetupResult>(
            Generator.Generate(players, boxes, new ScriptedRandom([schemeIndex, IndexOf(players, boxes, mastermind, schemeIndex), .. rest])));
    }

    // Where a Scheme, or with schemeIndex a Mastermind, sits among the draw's options, found by drawing each.
    private static int IndexOf(int players, string[] boxes, string name, int? schemeIndex = null)
    {
        for (var index = 0; ; index++)
        {
            var random = schemeIndex is { } scheme ? new ScriptedRandom(scheme, index) : new ScriptedRandom(index);
            var setup = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, random));
            if ((schemeIndex is null ? setup.Scheme.Name : setup.Mastermind.Name) == name)
            {
                return index;
            }
        }
    }

    private static IEnumerable<string> ComponentIds(SetupResult setup) =>
        new[] { setup.Scheme.Id, setup.Mastermind.Id }
            .Concat(setup.VillainGroups.Select(group => group.Id))
            .Concat(setup.HenchmanGroups.Select(group => group.Id))
            .Concat(setup.Heroes.Select(hero => hero.Id));

    private sealed class LastOption : IRandomSource
    {
        public int Next(int exclusiveMax) => exclusiveMax - 1;
    }
}
