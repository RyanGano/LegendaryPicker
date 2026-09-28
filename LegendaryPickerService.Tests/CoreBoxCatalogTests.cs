using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Tests;

// Pins the First Edition core box data to the counts and names in Docs/Plan.md
// (rulebook pages cited there), so a typo in the data file fails the build.
public class CoreBoxCatalogTests
{
    private static readonly Box Core = BoxCatalog.Load(BoxCatalog.DefaultDirectory)
        .Boxes.Single(box => box.Id == "core");

    [Fact]
    public void Core_box_is_the_First_Edition_core_box()
    {
        Assert.Equal("Marvel Legendary First Edition core box", Core.Name);
        Assert.Equal(1, Core.SchemaVersion);
        Assert.True(Core.IsBaseGame);
    }

    [Fact]
    public void Core_box_has_the_15_Heroes_in_the_plan()
    {
        Assert.Equal(
            [
                "Black Widow", "Captain America", "Cyclops", "Deadpool", "Emma Frost",
                "Gambit", "Hawkeye", "Hulk", "Iron Man", "Nick Fury",
                "Rogue", "Spider-Man", "Storm", "Thor", "Wolverine",
            ],
            Core.Heroes.Select(hero => hero.Name));
    }

    [Fact]
    public void Core_box_has_the_7_Villain_Groups_in_the_plan()
    {
        Assert.Equal(
            ["Brotherhood", "Enemies of Asgard", "HYDRA", "Masters of Evil", "Radiation", "Skrulls", "Spider-Foes"],
            Core.VillainGroups.Select(group => group.Name));
    }

    [Fact]
    public void Core_box_has_the_4_Henchman_Groups_in_the_plan()
    {
        Assert.Equal(
            ["Doombot Legion", "Hand Ninjas", "Savage Land Mutates", "Sentinel"],
            Core.HenchmanGroups.Select(group => group.Name));
    }

    [Theory]
    [InlineData("Sentinel")]
    [InlineData("Hand Ninjas")]
    public void Sentinel_and_Hand_Ninjas_are_Henchman_Groups_not_Villain_Groups(string name)
    {
        Assert.Contains(Core.HenchmanGroups, group => group.Name == name);
        Assert.DoesNotContain(Core.VillainGroups, group => group.Name == name);
    }

    [Fact]
    public void Core_box_component_counts_match_the_rulebook()
    {
        Assert.Equal(14, Core.Components.HeroCards.Value);
        Assert.Equal(8, Core.Components.VillainGroupCards.Value);
        Assert.Equal(10, Core.Components.HenchmanGroupCards.Value);
        Assert.Equal(11, Core.Components.SchemeTwists.Value);
    }

    [Fact]
    public void Core_box_has_the_4_Masterminds_in_the_plan()
    {
        Assert.Equal(
            ["Dr. Doom", "Loki", "Magneto", "Red Skull"],
            Core.Masterminds.Select(mastermind => mastermind.Name));
    }

    [Theory]
    [InlineData("Dr. Doom", "Doombot Legion", GroupType.Henchman)]
    [InlineData("Loki", "Enemies of Asgard", GroupType.Villain)]
    [InlineData("Magneto", "Brotherhood", GroupType.Villain)]
    [InlineData("Red Skull", "HYDRA", GroupType.Villain)]
    public void Mastermind_Always_Leads_the_group_in_the_plan(string mastermind, string group, GroupType type)
    {
        var alwaysLeads = Core.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal(type, alwaysLeads.GroupType);
        Assert.Equal(group, GroupName(alwaysLeads.GroupId, alwaysLeads.GroupType));
    }

    [Fact]
    public void Core_box_has_the_8_Schemes_in_the_plan()
    {
        Assert.Equal(
            [
                "Legacy Virus",
                "Midtown Bank Robbery",
                "Negative Zone Prison Breakout",
                "Portals to the Dark Dimension",
                "Replace Earth's Leaders with Killbots",
                "Secret Invasion of the Skrull Shapeshifters",
                "Super Hero Civil War",
                "Unleash the Power of the Cosmic Cube",
            ],
            Core.Schemes.Select(scheme => scheme.Name));
    }

    [Fact]
    public void Ids_name_the_box_the_kind_and_the_card()
    {
        Assert.Equal("core", Core.Id);
        Assert.Equal("core_scheme_secret-invasion-of-the-skrull-shapeshifters", SchemeNamed("Secret Invasion of the Skrull Shapeshifters").Id);
        Assert.Equal("core_scheme_replace-earths-leaders-with-killbots", SchemeNamed("Replace Earth's Leaders with Killbots").Id);
        Assert.Equal("core_mastermind_dr-doom", Core.Masterminds.Single(m => m.Name == "Dr. Doom").Id);
        Assert.Equal("core_hero_spider-man", Core.Heroes.Single(h => h.Name == "Spider-Man").Id);
        Assert.Equal("core_villain_brotherhood", Core.VillainGroups.Single(g => g.Name == "Brotherhood").Id);
        Assert.Equal("core_henchman_doombot-legion", Core.HenchmanGroups.Single(g => g.Name == "Doombot Legion").Id);
        Assert.All(AllIds(), id => Assert.Matches("^core_(hero|villain|henchman|mastermind|scheme)_[a-z0-9]+(-[a-z0-9]+)*$", id));
        Assert.Equal(AllIds().Count(), AllIds().Distinct().Count());
    }

    [Theory]
    [InlineData("Legacy Virus", 8)]
    [InlineData("Midtown Bank Robbery", 8)]
    [InlineData("Negative Zone Prison Breakout", 8)]
    [InlineData("Portals to the Dark Dimension", 7)]
    [InlineData("Replace Earth's Leaders with Killbots", 5)]
    [InlineData("Secret Invasion of the Skrull Shapeshifters", 8)]
    [InlineData("Unleash the Power of the Cosmic Cube", 8)]
    public void Scheme_has_the_same_Twists_at_every_player_count(string scheme, int twists)
    {
        var entry = Assert.Single(SchemeNamed(scheme).Setup.Twists);

        Assert.Null(entry.Players);
        Assert.Equal(twists, entry.Value);
    }

    [Fact]
    public void Super_Hero_Civil_War_has_8_Twists_at_2_to_3_players_and_5_at_4_to_5()
    {
        var twists = SchemeNamed("Super Hero Civil War").Setup.Twists;

        Assert.Equal(
            ["2,3 -> 8", "4,5 -> 5"],
            twists.Select(t => $"{string.Join(",", t.Players!)} -> {t.Value}"));
    }

    [Fact]
    public void Super_Hero_Civil_War_uses_4_Heroes_at_2_players()
    {
        var heroes = Assert.Single(SchemeNamed("Super Hero Civil War").Setup.Heroes!);

        Assert.Equal([2], heroes.Players);
        Assert.Equal(4, heroes.Value);
    }

    [Theory]
    [InlineData("Super Hero Civil War")]
    [InlineData("Negative Zone Prison Breakout")]
    public void Scheme_is_not_allowed_in_Solo(string scheme)
    {
        Assert.Equal([2, 3, 4, 5], SchemeNamed(scheme).Setup.AllowedPlayerCounts!.Value);
    }

    [Fact]
    public void Only_the_Solo_excluded_Schemes_restrict_player_counts()
    {
        var restricted = Core.Schemes
            .Where(scheme => scheme.Setup.AllowedPlayerCounts is not null)
            .Select(scheme => scheme.Name);

        Assert.Equal(["Negative Zone Prison Breakout", "Super Hero Civil War"], restricted);
    }

    [Fact]
    public void Legacy_Virus_puts_6_Wounds_per_player_in_the_Wound_stack()
    {
        Assert.Equal(6, SchemeNamed("Legacy Virus").Setup.WoundsPerPlayer!.Value);
    }

    [Fact]
    public void Midtown_Bank_Robbery_puts_12_Bystanders_in_the_Villain_Deck()
    {
        Assert.Equal(12, SchemeNamed("Midtown Bank Robbery").Setup.VillainDeckBystanders!.Value);
    }

    [Fact]
    public void Negative_Zone_Prison_Breakout_adds_one_Henchman_Group()
    {
        Assert.Equal(1, SchemeNamed("Negative Zone Prison Breakout").Setup.ExtraHenchmanGroups!.Value);
    }

    [Fact]
    public void Killbots_places_3_Twists_beside_the_Scheme_and_18_Bystanders_in_the_Villain_Deck()
    {
        var setup = SchemeNamed("Replace Earth's Leaders with Killbots").Setup;

        Assert.Equal(3, setup.TwistsBesideScheme!.Value);
        Assert.Equal(18, setup.VillainDeckBystanders!.Value);
    }

    [Fact]
    public void Secret_Invasion_uses_6_Heroes_requires_Skrulls_and_moves_12_Hero_cards()
    {
        var setup = SchemeNamed("Secret Invasion of the Skrull Shapeshifters").Setup;

        var heroes = Assert.Single(setup.Heroes!);
        Assert.Null(heroes.Players);
        Assert.Equal(6, heroes.Value);

        var required = Assert.Single(setup.RequiredGroups!);
        Assert.Equal(GroupType.Villain, required.GroupType);
        Assert.Equal("Skrulls", GroupName(required.GroupId, required.GroupType));

        Assert.Equal(12, setup.HeroCardsInVillainDeck!.Value);
    }

    [Theory]
    [InlineData("Portals to the Dark Dimension")]
    [InlineData("Unleash the Power of the Cosmic Cube")]
    public void Scheme_has_no_setup_effect_beyond_its_Twists(string scheme)
    {
        Assert.Equal(new SchemeSetup(SchemeNamed(scheme).Setup.Twists), SchemeNamed(scheme).Setup);
    }

    [Fact]
    public void Largest_Scheme_demands_fit_the_box()
    {
        var twistsUsed = Core.Schemes.Select(scheme =>
            scheme.Setup.Twists.Max(t => t.Value) + (scheme.Setup.TwistsBesideScheme?.Value ?? 0));
        var villainDeckBystanders = Core.Schemes
            .Select(scheme => scheme.Setup.VillainDeckBystanders?.Value ?? 0)
            .Concat(Core.Setup!.PlayerCounts.Select(row => row.Bystanders));

        Assert.Equal(8, twistsUsed.Max());
        Assert.True(twistsUsed.Max() <= Core.Components.SchemeTwists.Value);
        Assert.Equal(18, villainDeckBystanders.Max());
        Assert.True(villainDeckBystanders.Max() <= Core.Setup!.SharedStacks.Bystanders.Value);
    }

    [Fact]
    public void Standard_setup_table_matches_the_rulebook()
    {
        Assert.Equal(
            [(2, 2, 1, 2), (3, 3, 1, 8), (4, 3, 2, 8), (5, 4, 2, 12)],
            Core.Setup!.PlayerCounts.Select(row => (row.Players, row.VillainGroups, row.HenchmanGroups, row.Bystanders)));
    }

    [Fact]
    public void Standard_setup_values_match_the_rulebook()
    {
        var setup = Core.Setup!;

        Assert.Equal(5, setup.Heroes.Value);
        Assert.Equal(5, setup.MasterStrikes.Value);
        Assert.Equal(8, setup.StartingDeck.Agents.Value);
        Assert.Equal(4, setup.StartingDeck.Troopers.Value);
        Assert.Equal(30, setup.SharedStacks.Officers.Value);
        Assert.Equal(30, setup.SharedStacks.Wounds.Value);
        Assert.Equal(30, setup.SharedStacks.Bystanders.Value);
    }

    [Fact]
    public void Solo_setup_matches_the_rulebook()
    {
        var solo = Core.Setup!.Solo;

        Assert.Equal(3, solo.Heroes.Value);
        Assert.Equal(1, solo.VillainGroups.Value);
        Assert.Equal(1, solo.HenchmanGroups.Value);
        Assert.Equal(3, solo.HenchmanCards.Value);
        Assert.Equal(1, solo.Bystanders.Value);
        Assert.Equal(1, solo.MasterStrikes.Value);
        Assert.True(solo.IgnoresAlwaysLeads.Value);
        Assert.Equal("R p.20", solo.IgnoresAlwaysLeads.Source);
        Assert.Equal(6, solo.TwistKosHeroCostingAtMost.Value);
    }

    [Fact]
    public void Rulings_cite_the_rulebook_and_the_designer()
    {
        Assert.Equal(new Rulings("R p.6", "D1", "D2"), Core.Setup!.Rulings);
    }

    [Fact]
    public void Source_keys_link_to_the_sources_in_the_plan()
    {
        Assert.Equal(
            [
                new SourceLink("R", "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"),
                new SourceLink("F", "https://boardgamegeek.com/wiki/page/Legendary_Marvel_FAQ"),
                new SourceLink("D1", "https://boardgamegeek.com/thread/993341/article/12653573"),
                new SourceLink("D2", "https://boardgamegeek.com/thread/884926"),
            ],
            Core.Sources);
    }

    [Fact]
    public void Every_rule_value_and_setup_effect_has_a_source()
    {
        var sources = RuleSources().ToList();

        // 1 catalog + 4 components + 4 player-count rows + 2 + 2 starting deck + 3 stacks + 8 solo
        // + 3 rulings + 4 Always Leads + 20 Scheme setup values.
        Assert.Equal(51, sources.Count);
        Assert.All(sources, source => Assert.False(string.IsNullOrWhiteSpace(source)));
    }

    [Fact]
    public void Core_glossary_has_4_teams_5_classes_and_8_keywords()
    {
        Assert.Equal(
            ["Avengers", "S.H.I.E.L.D.", "Spider Friends", "X-Men"],
            Core.Glossary.Where(term => term.Kind == TermKind.Team).Select(term => term.Name));
        Assert.Equal(
            ["Covert", "Instinct", "Ranged", "Strength", "Tech"],
            Core.Glossary.Where(term => term.Kind == TermKind.Class).Select(term => term.Name));
        Assert.Equal(
            [
                "Always Leads", "Ambush", "Escape", "Fight", "Master Strike",
                "Mastermind Tactic", "Rescue a Bystander", "Scheme Twist",
            ],
            Core.Glossary.Where(term => term.Kind == TermKind.Keyword).Select(term => term.Name));
    }

    [Fact]
    public void Every_core_term_cites_the_rulebook_page_that_defines_it()
    {
        Assert.Equal(
            [
                "Avengers R p.18", "S.H.I.E.L.D. R p.18", "Spider Friends R p.18", "X-Men R p.18",
                "Covert R p.18", "Instinct R p.18", "Ranged R p.18", "Strength R p.18", "Tech R p.18",
                "Always Leads R p.6", "Ambush R p.9", "Escape R p.9", "Fight R p.13", "Master Strike R p.10",
                "Mastermind Tactic R p.14", "Rescue a Bystander R p.15", "Scheme Twist R p.10",
            ],
            Core.Glossary.Select(term => $"{term.Name} {term.Source} p.{term.Page}"));
    }

    [Theory]
    [InlineData("Black Widow", "Avengers", "Covert Tech", "Rescue a Bystander")]
    [InlineData("Deadpool", null, "Covert Instinct Tech", "")]
    [InlineData("Nick Fury", "S.H.I.E.L.D.", "Covert Strength Tech", "")]
    [InlineData("Spider-Man", "Spider Friends", "Covert Instinct Strength Tech", "Rescue a Bystander")]
    [InlineData("Wolverine", "X-Men", "Instinct", "")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string? team, string classes, string keywords)
    {
        var entry = Core.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, entry.Team is null ? null : TermName(entry.Team));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    [Fact]
    public void Heroes_per_team_match_the_card_catalogs()
    {
        var teams = Core.Heroes.GroupBy(hero => hero.Team is null ? "none" : TermName(hero.Team))
            .Select(group => $"{group.Key} {group.Count()}");

        Assert.Equal(["Avengers 6", "X-Men 6", "none 1", "S.H.I.E.L.D. 1", "Spider Friends 1"], teams);
    }

    [Theory]
    [InlineData("Brotherhood", "Ambush Escape Fight")]
    [InlineData("HYDRA", "Escape Fight")]
    [InlineData("Radiation", "Ambush Escape Fight Rescue a Bystander")]
    [InlineData("Skrulls", "Ambush Fight")]
    public void Villain_Group_lists_its_keywords(string group, string keywords)
    {
        Assert.Equal(keywords, string.Join(" ", Core.VillainGroups.Single(g => g.Name == group).Terms.Select(TermName)));
    }

    [Fact]
    public void Every_Henchman_Group_uses_Fight_and_every_Scheme_uses_Scheme_Twist()
    {
        Assert.All(Core.HenchmanGroups, group => Assert.Equal(["Fight"], group.Terms.Select(TermName)));
        Assert.All(Core.Schemes, scheme => Assert.Equal(["Scheme Twist"], scheme.Terms.Select(TermName)));
    }

    [Theory]
    [InlineData("Dr. Doom", "Always Leads Fight Master Strike Mastermind Tactic")]
    [InlineData("Magneto", "Always Leads Fight Master Strike Mastermind Tactic Rescue a Bystander")]
    public void Mastermind_lists_its_keywords(string mastermind, string keywords)
    {
        Assert.Equal(keywords, string.Join(" ", Core.Masterminds.Single(m => m.Name == mastermind).Terms.Select(TermName)));
    }

    private static Scheme SchemeNamed(string name) => Core.Schemes.Single(scheme => scheme.Name == name);

    private static string TermName(string id) => Core.Glossary.Single(term => term.Id == id).Name;

    private static string GroupName(string id, GroupType type) => type switch
    {
        GroupType.Villain => Core.VillainGroups.Single(group => group.Id == id).Name,
        GroupType.Henchman => Core.HenchmanGroups.Single(group => group.Id == id).Name,
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

    private static IEnumerable<string> AllIds() =>
        Core.Heroes.Select(x => x.Id)
            .Concat(Core.VillainGroups.Select(x => x.Id))
            .Concat(Core.HenchmanGroups.Select(x => x.Id))
            .Concat(Core.Masterminds.Select(x => x.Id))
            .Concat(Core.Schemes.Select(x => x.Id));

    private static IEnumerable<string> RuleSources()
    {
        yield return Core.CatalogSource;

        var components = Core.Components;
        yield return components.HeroCards.Source;
        yield return components.VillainGroupCards.Source;
        yield return components.HenchmanGroupCards.Source;
        yield return components.SchemeTwists.Source;

        var setup = Core.Setup!;
        foreach (var row in setup.PlayerCounts) yield return row.Source;
        yield return setup.Heroes.Source;
        yield return setup.MasterStrikes.Source;
        yield return setup.StartingDeck.Agents.Source;
        yield return setup.StartingDeck.Troopers.Source;
        yield return setup.SharedStacks.Officers.Source;
        yield return setup.SharedStacks.Wounds.Source;
        yield return setup.SharedStacks.Bystanders.Source;
        yield return setup.Solo.Heroes.Source;
        yield return setup.Solo.VillainGroups.Source;
        yield return setup.Solo.HenchmanGroups.Source;
        yield return setup.Solo.HenchmanCards.Source;
        yield return setup.Solo.Bystanders.Source;
        yield return setup.Solo.MasterStrikes.Source;
        yield return setup.Solo.IgnoresAlwaysLeads.Source;
        yield return setup.Solo.TwistKosHeroCostingAtMost.Source;
        yield return setup.Rulings.AlwaysLeadsFillsSlot;
        yield return setup.Rulings.RequiredGroupDisplacesAlwaysLeads;
        yield return setup.Rulings.SchemeOverridesSolo;

        foreach (var mastermind in Core.Masterminds) yield return mastermind.AlwaysLeads.Source;

        foreach (var effect in Core.Schemes.Select(scheme => scheme.Setup))
        {
            foreach (var twists in effect.Twists) yield return twists.Source;
            foreach (var heroes in effect.Heroes ?? []) yield return heroes.Source;
            foreach (var group in effect.RequiredGroups ?? []) yield return group.Source;

            Sourced<int>?[] counts =
            [
                effect.VillainDeckBystanders, effect.WoundsPerPlayer, effect.ExtraHenchmanGroups,
                effect.HeroCardsInVillainDeck, effect.TwistsBesideScheme,
            ];
            foreach (var count in counts.OfType<Sourced<int>>()) yield return count.Source;
            if (effect.AllowedPlayerCounts is not null) yield return effect.AllowedPlayerCounts.Source;
        }
    }
}
