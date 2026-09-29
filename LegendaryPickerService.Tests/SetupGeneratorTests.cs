using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Fixed-result table draws from the First Edition core box. Draws happen in table order:
// Scheme, Mastermind, each remaining Villain Group, each remaining Henchman Group, each Hero,
// each from the options left in catalog order. Indexes used in scripts:
//   Schemes, 2-5 players: 0 Legacy Virus, 1 Midtown Bank Robbery, 2 Negative Zone Prison Breakout,
//     3 Portals, 4 Killbots, 5 Secret Invasion, 6 Super Hero Civil War, 7 Cosmic Cube.
//   Schemes, Solo: 0 Legacy Virus, 1 Midtown Bank Robbery, 2 Portals, 3 Killbots, 4 Secret Invasion, 5 Cosmic Cube.
//   Masterminds: 0 Dr. Doom, 1 Loki, 2 Magneto, 3 Red Skull.
//   Villain Groups: 0 Brotherhood, 1 Enemies of Asgard, 2 HYDRA, 3 Masters of Evil, 4 Radiation, 5 Skrulls, 6 Spider-Foes.
//   Henchman Groups: 0 Doombot Legion, 1 Hand Ninjas, 2 Savage Land Mutates, 3 Sentinel.
// Every test asserts the drawn Scheme and Mastermind by name, so a script that misses its target fails.
public class SetupGeneratorTests
{
    private const string LegacyVirus = "Legacy Virus";
    private const string BankRobbery = "Midtown Bank Robbery";
    private const string NegativeZone = "Negative Zone Prison Breakout";
    private const string Portals = "Portals to the Dark Dimension";
    private const string Killbots = "Replace Earth's Leaders with Killbots";
    private const string SecretInvasion = "Secret Invasion of the Skrull Shapeshifters";
    private const string CivilWar = "Super Hero Civil War";
    private const string CosmicCube = "Unleash the Power of the Cosmic Cube";

    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";
    private const string SoloRuling = "https://boardgamegeek.com/thread/884926";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    // Eligibility

    [Fact]
    public void Solo_draws_only_from_the_6_Schemes_allowed_in_Solo()
    {
        var drawn = Enumerable.Range(0, 6).Select(index => Setup(1, index).Scheme.Name);

        Assert.Equal([LegacyVirus, BankRobbery, Portals, Killbots, SecretInvasion, CosmicCube], drawn);
        Assert.Equal(6, Draw(1, 0).Random.Options[0]);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Two_to_five_players_draw_from_all_8_Schemes(int players)
    {
        var drawn = Enumerable.Range(0, 8).Select(index => Setup(players, index).Scheme.Name);

        Assert.Equal([LegacyVirus, BankRobbery, NegativeZone, Portals, Killbots, SecretInvasion, CivilWar, CosmicCube], drawn);
        Assert.Equal(8, Draw(players, 0).Random.Options[0]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void Rejects_a_player_count_outside_one_to_five(int players)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Generator.Generate(players, new ScriptedRandom()));
    }

    [Fact]
    public void Rejects_a_box_the_catalog_does_not_hold()
    {
        Assert.Throws<ArgumentException>(() => Generator.Generate(2, ["expansion"], new ScriptedRandom()));
    }

    [Fact]
    public void Included_boxes_default_to_the_core_box()
    {
        var byDefault = Generator.Generate(2, new ScriptedRandom(7, 3));
        var explicitly = Generator.Generate(2, ["core"], new ScriptedRandom(7, 3));

        Assert.Equivalent(explicitly, byDefault, strict: true);
    }

    // Player-count table

    [Theory]
    [InlineData(2, 2, 1, 2, 41)]
    [InlineData(3, 3, 1, 8, 55)]
    [InlineData(4, 3, 2, 8, 65)]
    [InlineData(5, 4, 2, 12, 77)]
    public void Cosmic_Cube_with_Red_Skull_follows_the_player_count_table(
        int players, int villainGroups, int henchmanGroups, int bystanders, int villainDeck)
    {
        var setup = Setup(players, 7, 3);

        AssertDrawn(setup, CosmicCube, "Red Skull");
        Assert.Equal(villainGroups, setup.VillainGroups.Count);
        Assert.Contains("HYDRA", Names(setup.VillainGroups));
        Assert.Equal(henchmanGroups, setup.HenchmanGroups.Count);
        Assert.Equal(new VillainDeck(8, 5, 8 * villainGroups, 10 * henchmanGroups, bystanders, 0), setup.VillainDeck);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
        Assert.Equal(5, setup.Heroes.Count);
        Assert.Equal(70, setup.HeroDeck.Total);
    }

    // Always Leads

    [Theory]
    [InlineData(2, new[] { 8, 4, 7, 6, 15, 14, 13, 12, 11 })]
    [InlineData(3, new[] { 8, 4, 7, 6, 5, 15, 14, 13, 12, 11 })]
    public void Dr_Doom_at_2_and_3_players_leads_the_only_Henchman_Group_and_every_Villain_Group_is_drawn(int players, int[] options)
    {
        var (setup, random) = Draw(players, 7, 0);

        AssertDrawn(setup, CosmicCube, "Dr. Doom");
        Assert.Equal(["Doombot Legion"], Names(setup.HenchmanGroups));
        Assert.Equal(options, random.Options);
    }

    [Fact]
    public void Dr_Doom_at_4_players_leads_Doombot_Legion_plus_one_drawn_Henchman_Group()
    {
        // Three Villain draws, then Henchman draw 2 of [Hand Ninjas, Savage Land Mutates, Sentinel].
        var (setup, random) = Draw(4, 7, 0, 0, 0, 0, 2);

        AssertDrawn(setup, CosmicCube, "Dr. Doom");
        Assert.Equal(["Doombot Legion", "Sentinel"], Names(setup.HenchmanGroups));
        Assert.Equal([8, 4, 7, 6, 5, 3, 15, 14, 13, 12, 11], random.Options);
    }

    [Fact]
    public void Solo_Magneto_draws_the_Villain_Group_from_all_7_so_Brotherhood_is_not_forced()
    {
        var (setup, random) = Draw(1, 5, 2, 5);

        AssertDrawn(setup, CosmicCube, "Magneto");
        Assert.Equal(["Skrulls"], Names(setup.VillainGroups));
        Assert.Equal(7, random.Options[2]);
    }

    [Fact]
    public void Solo_Dr_Doom_draws_the_Henchman_Group_from_all_4()
    {
        // Henchman draw 3 of [Doombot Legion, Hand Ninjas, Savage Land Mutates, Sentinel].
        var (setup, random) = Draw(1, 5, 0, 0, 3);

        AssertDrawn(setup, CosmicCube, "Dr. Doom");
        Assert.Equal(["Sentinel"], Names(setup.HenchmanGroups));
        Assert.Equal([6, 4, 7, 4, 15, 14, 13], random.Options);
    }

    // Solo

    [Fact]
    public void Solo_Cosmic_Cube_uses_the_Solo_setup()
    {
        var setup = Setup(1, 5, 1);

        AssertDrawn(setup, CosmicCube, "Loki");
        Assert.Equal(3, setup.Heroes.Count);
        Assert.Equal(new HeroDeck(42, 0), setup.HeroDeck);
        Assert.Single(setup.VillainGroups);
        Assert.Single(setup.HenchmanGroups);
        Assert.Equal(new VillainDeck(8, 1, 8, 3, 1, 0), setup.VillainDeck);
        Assert.Equal(21, setup.VillainDeck.Total);
        Assert.Equal(0, setup.TwistsBesideScheme);
    }

    [Fact]
    public void Solo_Portals_has_a_20_card_Villain_Deck()
    {
        var setup = Setup(1, 2, 1);

        AssertDrawn(setup, Portals, "Loki");
        Assert.Equal(3, setup.Heroes.Count);
        Assert.Equal(new VillainDeck(7, 1, 8, 3, 1, 0), setup.VillainDeck);
        Assert.Equal(20, setup.VillainDeck.Total);
    }

    [Fact]
    public void Solo_Secret_Invasion_uses_6_Heroes_Skrulls_and_moves_12_Hero_cards()
    {
        var (setup, random) = Draw(1, 4, 1);

        AssertDrawn(setup, SecretInvasion, "Loki");
        Assert.Equal(6, setup.Heroes.Count);
        Assert.Equal(new HeroDeck(84, 12), setup.HeroDeck);
        Assert.Equal(72, setup.HeroDeck.Total);
        Assert.Equal(["Skrulls"], Names(setup.VillainGroups));
        Assert.Equal(new VillainDeck(8, 1, 8, 3, 1, 12), setup.VillainDeck);
        Assert.Equal(33, setup.VillainDeck.Total);
        // Skrulls fill the one Villain Group slot, so there is no Villain draw.
        Assert.Equal([6, 4, 4, 15, 14, 13, 12, 11, 10], random.Options);
    }

    [Fact]
    public void Solo_Midtown_Bank_Robbery_puts_12_Bystanders_in_the_Villain_Deck()
    {
        var setup = Setup(1, 1, 1);

        AssertDrawn(setup, BankRobbery, "Loki");
        Assert.Equal(3, setup.Heroes.Count);
        Assert.Equal(new VillainDeck(8, 1, 8, 3, 12, 0), setup.VillainDeck);
        Assert.Equal(32, setup.VillainDeck.Total);
    }

    [Fact]
    public void Solo_Killbots_uses_5_Twists_in_the_deck_3_beside_and_18_Bystanders()
    {
        var setup = Setup(1, 3, 1);

        AssertDrawn(setup, Killbots, "Loki");
        Assert.Equal(3, setup.Heroes.Count);
        Assert.Equal(new VillainDeck(5, 1, 8, 3, 18, 0), setup.VillainDeck);
        Assert.Equal(35, setup.VillainDeck.Total);
        Assert.Equal(3, setup.TwistsBesideScheme);
        Assert.Equal(12, setup.Stacks.Bystanders);
    }

    [Fact]
    public void Solo_Legacy_Virus_has_a_6_card_Wound_stack()
    {
        var setup = Setup(1, 0, 1);

        AssertDrawn(setup, LegacyVirus, "Loki");
        Assert.Equal(3, setup.Heroes.Count);
        Assert.Equal(21, setup.VillainDeck.Total);
        Assert.Equal(6, setup.Stacks.Wounds);
    }

    // Scheme effects in multiplayer

    [Fact]
    public void Legacy_Virus_at_3_players_has_an_18_card_Wound_stack()
    {
        var setup = Setup(3, 0, 1);

        AssertDrawn(setup, LegacyVirus, "Loki");
        Assert.Equal(18, setup.Stacks.Wounds);
    }

    [Theory]
    [InlineData(2, 2, 51)]
    [InlineData(4, 3, 75)]
    [InlineData(5, 3, 87)]
    public void Negative_Zone_adds_one_Henchman_Group(int players, int henchmanGroups, int villainDeck)
    {
        var setup = Setup(players, 2, 1);

        AssertDrawn(setup, NegativeZone, "Loki");
        Assert.Equal(henchmanGroups, setup.HenchmanGroups.Count);
        Assert.Equal(10 * henchmanGroups, setup.VillainDeck.HenchmanCards);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
    }

    [Fact]
    public void Negative_Zone_at_5_players_with_Dr_Doom_leads_Doombot_Legion_plus_2_drawn_Henchman_Groups()
    {
        // Four Villain draws, then Henchman draws 2 of [Hand Ninjas, Savage Land Mutates, Sentinel]
        // and 0 of [Hand Ninjas, Savage Land Mutates].
        var (setup, random) = Draw(5, 2, 0, 0, 0, 0, 0, 2, 0);

        AssertDrawn(setup, NegativeZone, "Dr. Doom");
        Assert.Equal(["Doombot Legion", "Sentinel", "Hand Ninjas"], Names(setup.HenchmanGroups));
        Assert.Equal([8, 4, 7, 6, 5, 4, 3, 2, 15, 14, 13, 12, 11], random.Options);
    }

    [Fact]
    public void Civil_War_at_2_players_uses_4_Heroes_and_8_Twists()
    {
        var setup = Setup(2, 6, 1);

        AssertDrawn(setup, CivilWar, "Loki");
        Assert.Equal(4, setup.Heroes.Count);
        Assert.Equal(new HeroDeck(56, 0), setup.HeroDeck);
        Assert.Equal(8, setup.VillainDeck.Twists);
    }

    [Fact]
    public void Civil_War_at_3_players_uses_5_Heroes_and_8_Twists()
    {
        var setup = Setup(3, 6, 1);

        AssertDrawn(setup, CivilWar, "Loki");
        Assert.Equal(5, setup.Heroes.Count);
        Assert.Equal(8, setup.VillainDeck.Twists);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    public void Civil_War_at_4_and_5_players_uses_5_Twists(int players)
    {
        var setup = Setup(players, 6, 1);

        AssertDrawn(setup, CivilWar, "Loki");
        Assert.Equal(5, setup.VillainDeck.Twists);
    }

    [Fact]
    public void Secret_Invasion_at_2_players_with_Magneto_fills_both_slots_without_a_Villain_draw()
    {
        var (setup, random) = Draw(2, 5, 2);

        AssertDrawn(setup, SecretInvasion, "Magneto");
        Assert.Equal(["Brotherhood", "Skrulls"], Names(setup.VillainGroups).Order());
        Assert.Equal(6, setup.Heroes.Count);
        Assert.Equal(new VillainDeck(8, 5, 16, 10, 2, 12), setup.VillainDeck);
        Assert.Equal(53, setup.VillainDeck.Total);
        Assert.Equal(72, setup.HeroDeck.Total);
        Assert.Equal([8, 4, 4, 15, 14, 13, 12, 11, 10], random.Options);
    }

    [Fact]
    public void Secret_Invasion_at_2_players_with_Loki_uses_Skrulls_and_Enemies_of_Asgard()
    {
        var setup = Setup(2, 5, 1);

        AssertDrawn(setup, SecretInvasion, "Loki");
        Assert.Equal(["Enemies of Asgard", "Skrulls"], Names(setup.VillainGroups).Order());
    }

    [Fact]
    public void Secret_Invasion_at_3_players_with_Red_Skull_uses_Skrulls_HYDRA_and_one_drawn_group()
    {
        // Villain draw 3 of [Brotherhood, Enemies of Asgard, Masters of Evil, Radiation, Spider-Foes].
        var (setup, random) = Draw(3, 5, 3, 3);

        AssertDrawn(setup, SecretInvasion, "Red Skull");
        Assert.Equal(["HYDRA", "Radiation", "Skrulls"], Names(setup.VillainGroups).Order());
        Assert.Equal(5, random.Options[2]);
    }

    [Fact]
    public void Killbots_at_5_players_puts_18_Bystanders_in_the_Villain_Deck_instead_of_12()
    {
        var setup = Setup(5, 4, 1);

        AssertDrawn(setup, Killbots, "Loki");
        Assert.Equal(new VillainDeck(5, 5, 32, 20, 18, 0), setup.VillainDeck);
        Assert.Equal(80, setup.VillainDeck.Total);
        Assert.Equal(12, setup.Stacks.Bystanders);
    }

    [Fact]
    public void Midtown_Bank_Robbery_at_2_players_puts_12_Bystanders_in_the_Villain_Deck()
    {
        var setup = Setup(2, 1, 1);

        AssertDrawn(setup, BankRobbery, "Loki");
        Assert.Equal(12, setup.VillainDeck.Bystanders);
        Assert.Equal(51, setup.VillainDeck.Total);
    }

    // Invariants over every Scheme and Mastermind at every player count, with several fill patterns.

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Every_setup_keeps_the_invariants(int players)
    {
        var core = Catalog.Boxes.Single(box => box.Id == "core");
        var villainIds = core.VillainGroups.Select(group => group.Id).ToHashSet();
        var henchmanIds = core.HenchmanGroups.Select(group => group.Id).ToHashSet();
        var setups = AllScriptedSetups(players).ToList();

        Assert.Equal((players == 1 ? 6 : 8) * 4 * FillPatterns.Length, setups.Count);
        Assert.All(setups, setup =>
        {
            Assert.Equal(setup.Heroes.Count, setup.Heroes.Distinct().Count());
            Assert.Equal(setup.VillainGroups.Count, setup.VillainGroups.Distinct().Count());
            Assert.Equal(setup.HenchmanGroups.Count, setup.HenchmanGroups.Distinct().Count());
            Assert.All(setup.VillainGroups, group => Assert.Contains(group.Id, villainIds));
            Assert.All(setup.HenchmanGroups, group => Assert.Contains(group.Id, henchmanIds));
            Assert.Equal(new PlayerDeck(8, 4), setup.PlayerDeck);
            Assert.Equal(30, setup.Stacks.Officers);
            Assert.Equal(setup.Scheme.Name == LegacyVirus ? 6 * players : 30, setup.Stacks.Wounds);
            Assert.Equal(30 - setup.VillainDeck.Bystanders, setup.Stacks.Bystanders);
        });
    }

    // Notes

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Every_setup_with_an_Always_Leads_group_has_the_R_p6_note(int players)
    {
        Assert.All(AllScriptedSetups(players), setup =>
        {
            var leads = setup.Mastermind.AlwaysLeads.GroupId;
            var group = setup.VillainGroups.Select(g => (g.Id, g.Name))
                .Concat(setup.HenchmanGroups.Select(g => (g.Id, g.Name)))
                .Single(g => g.Id == leads);

            Assert.Contains(new RuleNote($"{setup.Mastermind.Name} always leads {group.Name}", "R p.6", Rulebook), setup.Notes);
        });
    }

    [Fact]
    public void Every_Solo_setup_has_the_R_p20_KO_note_and_no_Always_Leads_note()
    {
        Assert.All(AllScriptedSetups(1), setup =>
        {
            Assert.Contains(new RuleNote("Solo: After each Twist, KO a Hero costing 6 or less from the HQ", "R p.20", Rulebook), setup.Notes);
            Assert.Contains(new RuleNote($"Solo ignores {setup.Mastermind.Name}'s Always Leads", "R p.20", Rulebook), setup.Notes);
            Assert.DoesNotContain(setup.Notes, note => note.Citation == "R p.6");
        });
    }

    [Fact]
    public void Solo_Secret_Invasion_notes_the_Scheme_overriding_Solo()
    {
        var setup = Setup(1, 4, 1);

        AssertDrawn(setup, SecretInvasion, "Loki");
        Assert.Equal(
            [
                new RuleNote("Scheme overrides Solo: 6 Heroes", "D2", SoloRuling),
                new RuleNote("Scheme moves 12 Hero cards into the Villain Deck", "Card", null),
                new RuleNote("Scheme requires Skrulls", "Card", null),
                new RuleNote("Solo ignores Loki's Always Leads", "R p.20", Rulebook),
                new RuleNote("Solo: After each Twist, KO a Hero costing 6 or less from the HQ", "R p.20", Rulebook),
            ],
            setup.Notes);
    }

    [Fact]
    public void Solo_Killbots_notes_the_Scheme_overriding_the_Solo_Bystanders()
    {
        var setup = Setup(1, 3, 1);

        AssertDrawn(setup, Killbots, "Loki");
        Assert.Contains(new RuleNote("Scheme overrides Solo: 18 Bystanders in the Villain Deck", "D2", SoloRuling), setup.Notes);
        Assert.Contains(new RuleNote("Scheme puts 3 Twists beside it", "Card", null), setup.Notes);
    }

    [Fact]
    public void Magneto_at_2_players_notes_only_Always_Leads()
    {
        var setup = Setup(2, 7, 2);

        AssertDrawn(setup, CosmicCube, "Magneto");
        Assert.Equal([new RuleNote("Magneto always leads Brotherhood", "R p.6", Rulebook)], setup.Notes);
    }

    [Theory]
    [InlineData(0, "Scheme sets the Wound stack to 6 per player")]
    [InlineData(1, "Scheme puts 12 Bystanders in the Villain Deck")]
    [InlineData(2, "Scheme adds 1 Henchman Group")]
    [InlineData(5, "Scheme uses 6 Heroes")]
    [InlineData(6, "Scheme uses 4 Heroes")]
    public void Scheme_effects_in_multiplayer_cite_the_card(int scheme, string note)
    {
        var setup = Setup(2, scheme, 1);

        Assert.Contains(new RuleNote(note, "Card", null), setup.Notes);
    }

    private static readonly int[][] FillPatterns = [[0], [14], [3, 1, 4, 1, 5, 9, 2, 6]];

    private static IEnumerable<SetupResult> AllScriptedSetups(int players)
    {
        var schemes = players == 1 ? 6 : 8;
        for (var scheme = 0; scheme < schemes; scheme++)
        {
            for (var mastermind = 0; mastermind < 4; mastermind++)
            {
                foreach (var pattern in FillPatterns)
                {
                    int[] draws = [scheme, mastermind, .. Enumerable.Repeat(pattern, 20).SelectMany(p => p)];
                    yield return Assert.IsType<SetupResult>(Generator.Generate(players, new CyclingRandom(draws)));
                }
            }
        }
    }

    private static SetupResult Setup(int players, params int[] draws) => Draw(players, draws).Setup;

    private static (SetupResult Setup, ScriptedRandom Random) Draw(int players, params int[] draws)
    {
        var random = new ScriptedRandom(draws);
        return (Assert.IsType<SetupResult>(Generator.Generate(players, random)), random);
    }

    private static void AssertDrawn(SetupResult setup, string scheme, string mastermind)
    {
        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal(mastermind, setup.Mastermind.Name);
    }

    private static IEnumerable<string> Names(IEnumerable<VillainGroup> groups) => groups.Select(group => group.Name);

    private static IEnumerable<string> Names(IEnumerable<HenchmanGroup> groups) => groups.Select(group => group.Name);
}
