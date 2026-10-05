using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Guardians of the Galaxy box file (Data/Boxes/guardians-of-the-galaxy.json), pinned to the rules insert
// (GG), the card catalogs and the scanned card faces, and drawn with the core box. Catalog order puts its cards
// after the core box's, so at 2–5 players its Schemes are 8 Forge the Infinity Gauntlet, 9 Intergalactic Kree
// Nega-Bomb, 10 The Kree-Skrull War and 11 Unite the Shards; Solo allows 6 core Schemes, so there they are 6 to
// 9. Its Masterminds are 4 Supreme Intelligence of the Kree and 5 Thanos.
public class GuardiansOfTheGalaxyTests
{
    private const string GuardiansName = "Guardians of the Galaxy";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";
    private const string UsesDecision = "https://github.com/RyanGano/LegendaryPicker/issues/87";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Guardians = Catalog.Boxes.Single(box => box.Id == "guardians-of-the-galaxy");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "guardians-of-the-galaxy"];

    private static readonly string[] SchemeNames =
    [
        "Forge the Infinity Gauntlet",
        "Intergalactic Kree Nega-Bomb",
        "The Kree-Skrull War",
        "Unite the Shards",
    ];

    private const int SupremeIntelligence = 4;
    private const int Thanos = 5;

    // Catalog

    [Fact]
    public void Guardians_of_the_Galaxy_is_an_expansion_with_the_Shard_tokens_of_the_rules_insert()
    {
        Assert.Equal(GuardiansName, Guardians.Name);
        Assert.Equal(7, Guardians.SchemaVersion);
        Assert.False(Guardians.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, Guardians.Ruleset);

        var components = Guardians.Components;
        Assert.Equal(new Sourced<int>(14, "GG p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "GG p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "GG p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "GG p.2"), components.SchemeTwists);
        Assert.Equal(new Sourced<int>(18, "GG p.2"), components.Shards);
        Assert.Null(components.Bystanders);
        Assert.Null(components.Wounds);
        Assert.Null(components.Officers);
    }

    [Fact]
    public void Guardians_of_the_Galaxy_has_the_5_Heroes_2_Villain_Groups_2_Masterminds_and_4_Schemes_in_the_rules_insert()
    {
        Assert.Equal(
            ["Drax the Destroyer", "Gamora", "Groot", "Rocket Raccoon", "Star-Lord"],
            Guardians.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Infinity Gems", "Kree Starforce"], Guardians.VillainGroups.Select(group => group.Name));
        Assert.Empty(Guardians.HenchmanGroups);
        Assert.Equal(["Supreme Intelligence of the Kree", "Thanos"], Guardians.Masterminds.Select(mastermind => mastermind.Name));
        Assert.All(Guardians.Masterminds, mastermind => Assert.Null(mastermind.Setup));
        Assert.Equal(SchemeNames, Guardians.Schemes.Select(scheme => scheme.Name));
    }

    // Every Hero, both Villain Groups, the Supreme Intelligence and two Schemes gain or spend Shards; Kree Starforce,
    // Thanos (a Tactic) and the Nega-Bomb give Wounds (Card).
    [Fact]
    public void Guardians_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. Guardians.Heroes, .. Guardians.VillainGroups, .. Guardians.Masterminds, .. Guardians.Schemes];
        Assert.Equal(
            [
                "guardians-of-the-galaxy_hero_drax-the-destroyer: Shards",
                "guardians-of-the-galaxy_hero_gamora: Shards",
                "guardians-of-the-galaxy_hero_groot: Shards",
                "guardians-of-the-galaxy_hero_rocket-raccoon: Shards",
                "guardians-of-the-galaxy_hero_star-lord: Shards",
                "guardians-of-the-galaxy_villain_infinity-gems: Shards",
                "guardians-of-the-galaxy_villain_kree-starforce: Wounds Shards",
                "guardians-of-the-galaxy_mastermind_supreme-intelligence-of-the-kree: Shards",
                "guardians-of-the-galaxy_mastermind_thanos: Wounds",
                "guardians-of-the-galaxy_scheme_forge-the-infinity-gauntlet: Shards",
                "guardians-of-the-galaxy_scheme_intergalactic-kree-nega-bomb: Wounds",
                "guardians-of-the-galaxy_scheme_unite-the-shards: Shards",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var (forge, negaBomb, kreeSkrull, unite) = (Guardians.Schemes[0], Guardians.Schemes[1], Guardians.Schemes[2], Guardians.Schemes[3]);

        Assert.Equal(["all: 8 Card"], Twists(forge));
        Assert.Equal([new RequiredGroup("guardians-of-the-galaxy_villain_infinity-gems", GroupType.Villain, "Card")], forge.Setup.RequiredGroups);

        Assert.Equal(["all: 8 Card"], Twists(negaBomb));
        var move = Assert.Single(negaBomb.Setup.Moves!);
        Assert.Equal((CardKind.Bystander, Pile.SetAside, false), (move.Card, move.To, move.PerPlayer));
        Assert.Equal([new PlayerCountValue(null, 6, "Card")], move.Count);
        Assert.Equal([new SetupStep("Shuffle the set-aside Bystanders face down as the Nega-Bomb Deck", "Card")], negaBomb.Setup.Steps);

        Assert.Equal(["all: 8 Card"], Twists(kreeSkrull));
        Assert.Equal(
            [
                new RequiredGroup("guardians-of-the-galaxy_villain_kree-starforce", GroupType.Villain, "Card"),
                new RequiredGroup("core_villain_skrulls", GroupType.Villain, "Card"),
            ],
            kreeSkrull.Setup.RequiredGroups);

        // Twists equal to the number of players plus 5.
        Assert.Equal(["1: 6 Card", "2: 7 Card", "3: 8 Card", "4: 9 Card", "5: 10 Card"], Twists(unite));
        // The Shard tokens are double-sided, so the box covers the Scheme's 30 Shards: it lays out all of them (D-shards).
        Assert.Equal([new SetupStep("Put all the Shard tokens from the included boxes in the supply", "D-shards")], unite.Setup.Steps);

        Assert.Equal([negaBomb], Guardians.Schemes.Where(scheme => scheme.Setup.Moves is not null));
        Assert.All(Guardians.Schemes, scheme => Assert.Null(scheme.Setup.AllowedPlayerCounts));

        static IEnumerable<string> Twists(Scheme scheme) =>
            scheme.Setup.Twists.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Supreme Intelligence of the Kree", "Kree Starforce")]
    [InlineData("Thanos", "Infinity Gems")]
    public void Mastermind_Always_Leads_the_Villain_Group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = Guardians.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal(GroupType.Villain, alwaysLeads.GroupType);
        Assert.Equal("Card", alwaysLeads.Source);
        Assert.Equal(group, Guardians.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Ids_name_the_Guardians_of_the_Galaxy_box_and_the_rules_insert_is_its_one_source_key()
    {
        var ids = Guardians.Heroes.Select(x => x.Id)
            .Concat(Guardians.VillainGroups.Select(x => x.Id))
            .Concat(Guardians.Masterminds.Select(x => x.Id))
            .Concat(Guardians.Schemes.Select(x => x.Id))
            .Concat(Guardians.Glossary.Select(x => x.Id));

        Assert.All(ids, id => Assert.Matches("^guardians-of-the-galaxy_(hero|villain|mastermind|scheme|term)_[a-z0-9]+(-[a-z0-9]+)*$", id));
        Assert.Equal(
            [new SourceLink("GG", "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Guardians_of_the_Galaxy.pdf"), new SourceLink("D-shards", "https://github.com/RyanGano/LegendaryPicker/issues/107"), new SourceLink("Q", "https://github.com/RyanGano/LegendaryPicker/blob/main/Docs/BoxResearch/README.md")],
            Guardians.Sources);
        Assert.Equal("GG p.2; C1; C2", Guardians.CatalogSource);
    }

    [Fact]
    public void Guardians_glossary_adds_its_team_Shards_and_Artifacts_from_the_rules_insert()
    {
        Assert.Equal(
            ["Guardians of the Galaxy team GG p.1", "Shard keyword GG p.1", "Artifact keyword GG p.1"],
            Guardians.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    [Theory]
    [InlineData("Drax the Destroyer", "Instinct Strength", "Artifact Shard")]
    [InlineData("Gamora", "Covert Instinct", "Artifact Shard")]
    [InlineData("Groot", "Covert Strength", "Shard")]
    [InlineData("Rocket Raccoon", "Instinct Ranged Tech", "Artifact Shard")]
    [InlineData("Star-Lord", "Covert Ranged Tech", "Artifact Shard")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string classes, string keywords)
    {
        var entry = Guardians.Heroes.Single(h => h.Name == hero);

        Assert.Equal("Guardians of the Galaxy", TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
        Assert.Equal(hero, entry.NameOfHero);
    }

    [Theory]
    [InlineData("Infinity Gems", "Ambush Fight Artifact Shard")]
    [InlineData("Kree Starforce", "Ambush Escape Fight Shard")]
    [InlineData("Supreme Intelligence of the Kree", "Always Leads Fight Master Strike Mastermind Tactic Shard")]
    [InlineData("Thanos", "Always Leads Fight Master Strike Mastermind Tactic Artifact")]
    [InlineData("Forge the Infinity Gauntlet", "Scheme Twist Artifact Shard")]
    [InlineData("Intergalactic Kree Nega-Bomb", "Scheme Twist Rescue a Bystander")]
    [InlineData("The Kree-Skrull War", "Scheme Twist")]
    [InlineData("Unite the Shards", "Scheme Twist Shard")]
    public void Component_lists_its_keywords(string component, string keywords)
    {
        var terms = Guardians.VillainGroups.Where(g => g.Name == component).Select(g => g.Terms)
            .Concat(Guardians.Masterminds.Where(m => m.Name == component).Select(m => m.Terms))
            .Concat(Guardians.Schemes.Where(s => s.Name == component).Select(s => s.Terms))
            .Single();

        Assert.Equal(keywords, string.Join(" ", terms.Select(TermName)));
    }

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with the Supreme Intelligence (who leads Kree
    // Starforce, except in Solo) and the first remaining option for every later draw. The core box gives 30 Wounds,
    // 30 Officers and 30 Bystanders, and this box 18 Shards. In Solo the first three core Heroes and Forge's Infinity
    // Gems give no Wounds, so that setup has no Wound stack.

    [Theory]
    [InlineData("Forge the Infinity Gauntlet", 1, 8, 1, 8, 3, 1, 21, 42, null, 29, 18)]
    [InlineData("Forge the Infinity Gauntlet", 2, 8, 5, 16, 10, 2, 41, 70, 30, 28, 18)]
    [InlineData("Forge the Infinity Gauntlet", 5, 8, 5, 32, 20, 12, 77, 70, 30, 18, 18)]
    [InlineData("Intergalactic Kree Nega-Bomb", 1, 8, 1, 8, 3, 1, 21, 42, 30, 23, 18)]
    [InlineData("Intergalactic Kree Nega-Bomb", 2, 8, 5, 16, 10, 2, 41, 70, 30, 22, 18)]
    [InlineData("Intergalactic Kree Nega-Bomb", 5, 8, 5, 32, 20, 12, 77, 70, 30, 12, 18)]
    [InlineData("The Kree-Skrull War", 1, 8, 1, 16, 3, 1, 29, 42, 30, 29, 18)]
    [InlineData("The Kree-Skrull War", 2, 8, 5, 16, 10, 2, 41, 70, 30, 28, 18)]
    [InlineData("The Kree-Skrull War", 5, 8, 5, 32, 20, 12, 77, 70, 30, 18, 18)]
    [InlineData("Unite the Shards", 1, 6, 1, 8, 3, 1, 19, 42, 30, 29, 18)]
    [InlineData("Unite the Shards", 2, 7, 5, 16, 10, 2, 40, 70, 30, 28, 18)]
    [InlineData("Unite the Shards", 5, 10, 5, 32, 20, 12, 79, 70, 30, 18, 18)]
    public void Scheme_lays_out_its_decks_and_stacks(
        string scheme, int players, int twists, int strikes, int villainCards, int henchmanCards, int bystanders,
        int villainDeck, int heroDeck, int? wounds, int bystanderStack, int shards)
    {
        var setup = Draw(players, scheme, SupremeIntelligence);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Supreme Intelligence of the Kree", setup.Mastermind.Name);
        Assert.Equal(new VillainDeck(twists, strikes, villainCards, henchmanCards, bystanders, 0), setup.VillainDeck);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
        Assert.Equal(heroDeck, setup.HeroDeck.Total);
        Assert.Equal(new SetupStacks(wounds, 30, bystanderStack, Shards: shards), setup.Stacks);
    }

    [Theory]
    [InlineData(1, "Infinity Gems")]
    [InlineData(2, "Infinity Gems|Kree Starforce")]
    [InlineData(5, "Infinity Gems|Kree Starforce|Brotherhood|Enemies of Asgard")]
    public void Forge_the_Infinity_Gauntlet_includes_Infinity_Gems(int players, string groups)
    {
        var setup = Draw(players, "Forge the Infinity Gauntlet", SupremeIntelligence);

        Assert.Equal(groups.Split('|'), setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme requires Infinity Gems", "Card", null, GuardiansName), setup.Notes);
    }

    // The Nega-Bomb Deck is 6 Bystanders set aside from the Bystander stack, at every player count.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Intergalactic_Kree_Nega_Bomb_sets_6_Bystanders_aside_as_the_Nega_Bomb_Deck(int players)
    {
        var setup = Draw(players, "Intergalactic Kree Nega-Bomb", SupremeIntelligence);

        Assert.Equal([new MovedCards(CardKind.Bystander, Pile.Bystanders, Pile.SetAside, 6, 6)], setup.Moves);
        Assert.Equal(["Shuffle the set-aside Bystanders face down as the Nega-Bomb Deck"], setup.Steps);
        Assert.Contains(new RuleNote("Scheme moves 6 Bystanders into a stack set aside", "Card", null, GuardiansName), setup.Notes);
    }

    // Both groups fill the Villain Group slots, in Solo too, where the Villain Deck is larger than normal (GG p.2);
    // at 2 players Thanos's Always Leads group gives way (D1, which the insert repeats for this Scheme).
    [Theory]
    [InlineData(1, SupremeIntelligence, "Kree Starforce|Skrulls")]
    [InlineData(2, SupremeIntelligence, "Kree Starforce|Skrulls")]
    [InlineData(5, SupremeIntelligence, "Kree Starforce|Skrulls|Brotherhood|Enemies of Asgard")]
    [InlineData(2, Thanos, "Kree Starforce|Skrulls")]
    public void The_Kree_Skrull_War_includes_Kree_Starforce_and_Skrulls(int players, int mastermind, string groups)
    {
        var setup = Draw(players, "The Kree-Skrull War", mastermind);

        Assert.Equal(groups.Split('|'), setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme requires Kree Starforce", "Card", null, GuardiansName), setup.Notes);
        Assert.Contains(new RuleNote("Scheme requires Skrulls", "Card", null, GuardiansName), setup.Notes);
        if (mastermind == Thanos)
        {
            Assert.Contains(
                new RuleNote(
                    "Scheme requires Kree Starforce and Skrulls, so Thanos's Always Leads group Infinity Gems is dropped",
                    "D1", "https://boardgamegeek.com/thread/993341/article/12653573", CoreName),
                setup.Notes);
        }
    }

    // Without the core box's Skrulls The Kree-Skrull War can't be completed, so it is never drawn.
    [Fact]
    public void The_Kree_Skrull_War_needs_the_core_box_Skrulls()
    {
        for (var seed = 0; seed < 40; seed++)
        {
            var setup = Assert.IsType<SetupResult>(
                Generator.Generate(2, ["villains", "guardians-of-the-galaxy"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            Assert.NotEqual("The Kree-Skrull War", setup.Scheme.Name);
        }
    }

    [Fact]
    public void Unite_the_Shards_lays_out_all_the_Shard_tokens_rather_than_a_count_of_30()
    {
        var setup = Draw(3, "Unite the Shards", SupremeIntelligence);

        Assert.Equal(8, setup.VillainDeck.Twists);
        Assert.Equal(18, setup.Stacks.Shards);
        Assert.Equal(["Put all the Shard tokens from the included boxes in the supply"], setup.Steps);
    }

    [Theory]
    [InlineData(SupremeIntelligence, "Supreme Intelligence of the Kree", "Kree Starforce")]
    [InlineData(Thanos, "Thanos", "Infinity Gems")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "Intergalactic Kree Nega-Bomb", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Equal([group, "Brotherhood"], setup.VillainGroups.Select(g => g.Name));
        Assert.Contains(new RuleNote($"{mastermind} always leads {group}", "R p.6", Rulebook, CoreName), setup.Notes);
    }

    // Shards are laid out only when a drawn card uses them (D-uses): Legacy Virus with Dr. Doom and the first core
    // Heroes in Solo uses none, so the Shard supply is left out with a note.
    [Fact]
    public void A_setup_whose_cards_use_no_Shards_leaves_the_Shard_supply_out()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, 0)));

        Assert.Equal(("Legacy Virus", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Null(setup.Stacks.Shards);
        Assert.Contains(new RuleNote("Leave out the Shard supply: no drawn card uses it", "D-uses", UsesDecision, CoreName), setup.Notes);
    }

    // Guardians cards that give Wounds need a box that supplies them; with Legendary: Villains as the only base
    // game, Kree Starforce, Thanos and the Nega-Bomb are never drawn, while the Shard cards still are.
    [Fact]
    public void Without_a_First_Edition_box_that_supplies_Wounds_the_Guardians_cards_that_give_them_are_never_drawn()
    {
        string[] dropped =
        [
            "guardians-of-the-galaxy_villain_kree-starforce",
            "guardians-of-the-galaxy_mastermind_thanos",
            "guardians-of-the-galaxy_scheme_intergalactic-kree-nega-bomb",
        ];

        for (var seed = 0; seed < 60; seed++)
        {
            var setup = Assert.IsType<SetupResult>(
                Generator.Generate(3, ["villains", "guardians-of-the-galaxy"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            Assert.DoesNotContain(ComponentIds(setup), id => dropped.Contains(id));
        }
    }

    // The exclusion case: with every other box of its ruleset included, Guardians of the Galaxy changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Guardians_of_the_Galaxy_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("guardians-of-the-galaxy.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("guardians-of-the-galaxy_"));
            Assert.Null(withItLoaded.Stacks.Shards);
        }
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }

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
