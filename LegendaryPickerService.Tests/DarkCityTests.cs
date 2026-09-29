using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Dark City box file (Data/Boxes/dark-city.json), pinned to the rules insert (DC) and the card
// catalogs, and drawn with the core box. Catalog order puts Dark City's cards after the core box's, so at
// 2–5 players its Schemes are 8 Capture Baby Hope, 9 Detonate the Helicarrier, 10 Massive Earthquake
// Generator, 11 Organized Crime Wave, 12 Save Humanity, 13 Steal the Weaponized Plutonium, 14 Transform
// Citizens Into Demons and 15 X-Cutioner's Song; Solo allows 6 core Schemes, so there they are 6 to 13.
// Its Masterminds are 4 Apocalypse, 5 Kingpin, 6 Mephisto, 7 Mr. Sinister and 8 Stryfe. Together the
// boxes hold 30 + 11 Bystanders.
public class DarkCityTests
{
    private const string DarkCityName = "Dark City";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box DarkCity = Catalog.Boxes.Single(box => box.Id == "dark-city");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "dark-city"];

    private static readonly string[] SchemeNames =
    [
        "Capture Baby Hope",
        "Detonate the Helicarrier",
        "Massive Earthquake Generator",
        "Organized Crime Wave",
        "Save Humanity",
        "Steal the Weaponized Plutonium",
        "Transform Citizens Into Demons",
        "X-Cutioner's Song",
    ];

    private const int Mephisto = 6;

    // Catalog

    [Fact]
    public void Dark_City_is_an_expansion_listed_after_the_core_box()
    {
        Assert.Equal(["core", "dark-city"], Catalog.Boxes.Select(box => box.Id).Take(2));
        Assert.Equal(DarkCityName, DarkCity.Name);
        Assert.Equal(5, DarkCity.SchemaVersion);
        Assert.False(DarkCity.IsBaseGame);
    }

    [Fact]
    public void Dark_City_has_the_17_Heroes_in_the_rules_insert()
    {
        Assert.Equal(
            [
                "Angel", "Bishop", "Blade", "Cable", "Colossus", "Daredevil", "Domino", "Elektra", "Forge",
                "Ghost Rider", "Iceman", "Iron Fist", "Jean Grey", "Nightcrawler", "Professor X", "Punisher",
                "Wolverine (X-Force)",
            ],
            DarkCity.Heroes.Select(hero => hero.Name));
    }

    [Fact]
    public void Dark_Citys_Wolverine_shares_its_Hero_Name_with_the_core_boxs()
    {
        var core = Catalog.Boxes.Single(box => box.Id == "core");

        Assert.Equal("Wolverine", DarkCity.Heroes.Single(hero => hero.Id == "dark-city_hero_wolverine-x-force").NameOfHero);
        Assert.Equal("Wolverine", core.Heroes.Single(hero => hero.Id == "core_hero_wolverine").NameOfHero);
    }

    [Fact]
    public void Dark_City_has_the_6_Villain_Groups_and_2_Henchman_Groups_in_the_rules_insert()
    {
        Assert.Equal(
            ["Emissaries of Evil", "Four Horsemen", "Marauders", "Mutant Liberation Front", "Streets of New York", "Underworld"],
            DarkCity.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Maggia Goons", "Phalanx"], DarkCity.HenchmanGroups.Select(group => group.Name));
    }

    [Fact]
    public void Dark_City_has_the_5_Masterminds_in_the_rules_insert()
    {
        Assert.Equal(
            ["Apocalypse", "Kingpin", "Mephisto", "Mr. Sinister", "Stryfe"],
            DarkCity.Masterminds.Select(mastermind => mastermind.Name));
        Assert.All(DarkCity.Masterminds, mastermind => Assert.Null(mastermind.Setup));
    }

    [Fact]
    public void Dark_City_has_the_8_Schemes_in_the_rules_insert()
    {
        Assert.Equal(SchemeNames, DarkCity.Schemes.Select(scheme => scheme.Name));
        Assert.All(DarkCity.Schemes, scheme => Assert.Equal([new PlayerCountValue(null, 8, "Card")], scheme.Setup.Twists));
        Assert.All(DarkCity.Schemes, scheme => Assert.Null(scheme.Setup.AllowedPlayerCounts));
    }

    [Fact]
    public void Dark_City_component_counts_match_the_rules_insert()
    {
        var components = DarkCity.Components;

        Assert.Equal(new Sourced<int>(14, "DC p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "DC p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "DC p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "DC p.2"), components.SchemeTwists);
        Assert.Equal(new Sourced<int>(11, "DC p.2"), components.Bystanders);
        Assert.Null(components.Wounds);
        Assert.Null(components.Officers);
        Assert.Null(components.Sidekicks);
    }

    [Theory]
    [InlineData("Apocalypse", "Four Horsemen")]
    [InlineData("Kingpin", "Streets of New York")]
    [InlineData("Mephisto", "Underworld")]
    [InlineData("Mr. Sinister", "Marauders")]
    [InlineData("Stryfe", "Mutant Liberation Front")]
    public void Mastermind_Always_Leads_the_Villain_Group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = DarkCity.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal(GroupType.Villain, alwaysLeads.GroupType);
        Assert.Equal("Card", alwaysLeads.Source);
        Assert.Equal(group, DarkCity.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Ids_name_the_Dark_City_box()
    {
        var ids = DarkCity.Heroes.Select(x => x.Id)
            .Concat(DarkCity.VillainGroups.Select(x => x.Id))
            .Concat(DarkCity.HenchmanGroups.Select(x => x.Id))
            .Concat(DarkCity.Masterminds.Select(x => x.Id))
            .Concat(DarkCity.Schemes.Select(x => x.Id))
            .Concat(DarkCity.Glossary.Select(x => x.Id))
            .ToList();

        Assert.All(ids, id => Assert.Matches("^dark-city_(hero|villain|henchman|mastermind|scheme|term)_[a-z0-9]+(-[a-z0-9]+)*$", id));
        Assert.Equal("dark-city_scheme_x-cutioners-song", DarkCity.Schemes[^1].Id);
    }

    [Fact]
    public void The_rules_insert_is_the_one_source_key()
    {
        Assert.Equal(
            [new SourceLink("DC", "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Dark_City.pdf")],
            DarkCity.Sources);
        Assert.Equal("DC p.2; C1; C2", DarkCity.CatalogSource);
    }

    [Fact]
    public void Dark_City_glossary_adds_2_teams_and_3_keywords_from_the_rules_insert()
    {
        Assert.Equal(
            ["Marvel Knights team DC p.1", "X-Force team DC p.1", "Bribe keyword DC p.1", "Teleport keyword DC p.1", "Versatile keyword DC p.1"],
            DarkCity.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    [Fact]
    public void Heroes_per_team_match_the_card_catalogs()
    {
        var teams = DarkCity.Heroes.GroupBy(hero => TermName(hero.Team!)).Select(group => $"{group.Key} {group.Count()}");

        Assert.Equal(["X-Men 6", "Marvel Knights 6", "X-Force 5"], teams);
    }

    [Theory]
    [InlineData("Angel", "X-Men", "Covert Instinct Strength", "Rescue a Bystander")]
    [InlineData("Cable", "X-Force", "Covert Ranged Tech", "Teleport")]
    [InlineData("Forge", "X-Force", "Tech", "Versatile")]
    [InlineData("Punisher", "Marvel Knights", "Strength Tech", "")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string team, string classes, string keywords)
    {
        var entry = DarkCity.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    [Theory]
    [InlineData("Streets of New York", "Ambush Escape Fight Bribe")]
    [InlineData("Underworld", "Ambush Escape Fight Teleport")]
    [InlineData("Maggia Goons", "Fight Bribe")]
    [InlineData("Kingpin", "Always Leads Fight Master Strike Mastermind Tactic Bribe")]
    public void Component_lists_its_keywords(string component, string keywords)
    {
        var terms = DarkCity.VillainGroups.Where(g => g.Name == component).Select(g => g.Terms)
            .Concat(DarkCity.HenchmanGroups.Where(g => g.Name == component).Select(g => g.Terms))
            .Concat(DarkCity.Masterminds.Where(m => m.Name == component).Select(m => m.Terms))
            .Single();

        Assert.Equal(keywords, string.Join(" ", terms.Select(TermName)));
    }

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Mephisto (who leads Underworld,
    // except in Solo) and the first remaining option for every later draw. Stacks hold 30 Wounds, 30
    // Officers and the Bystanders left of 41.

    [Theory]
    [InlineData("Capture Baby Hope", 1, 8, 1, 8, 3, 1, 0, 21, 42, 40)]
    [InlineData("Capture Baby Hope", 2, 8, 5, 16, 10, 2, 0, 41, 70, 39)]
    [InlineData("Capture Baby Hope", 5, 8, 5, 32, 20, 12, 0, 77, 70, 29)]
    [InlineData("Detonate the Helicarrier", 1, 8, 1, 8, 3, 1, 0, 21, 84, 40)]
    [InlineData("Detonate the Helicarrier", 2, 8, 5, 16, 10, 2, 0, 41, 84, 39)]
    [InlineData("Detonate the Helicarrier", 5, 8, 5, 32, 20, 12, 0, 77, 84, 29)]
    [InlineData("Massive Earthquake Generator", 1, 8, 1, 8, 3, 1, 0, 21, 42, 40)]
    [InlineData("Massive Earthquake Generator", 2, 8, 5, 16, 10, 2, 0, 41, 70, 39)]
    [InlineData("Massive Earthquake Generator", 5, 8, 5, 32, 20, 12, 0, 77, 70, 29)]
    [InlineData("Organized Crime Wave", 1, 8, 1, 8, 10, 1, 0, 28, 42, 40)]
    [InlineData("Organized Crime Wave", 2, 8, 5, 16, 10, 2, 0, 41, 70, 39)]
    [InlineData("Organized Crime Wave", 5, 8, 5, 32, 20, 12, 0, 77, 70, 29)]
    [InlineData("Save Humanity", 1, 8, 1, 8, 3, 1, 0, 21, 54, 28)]
    [InlineData("Save Humanity", 2, 8, 5, 16, 10, 2, 0, 41, 94, 15)]
    [InlineData("Save Humanity", 5, 8, 5, 32, 20, 12, 0, 77, 94, 5)]
    [InlineData("Steal the Weaponized Plutonium", 1, 8, 1, 16, 3, 1, 0, 29, 42, 40)]
    [InlineData("Steal the Weaponized Plutonium", 2, 8, 5, 24, 10, 2, 0, 49, 70, 39)]
    [InlineData("Steal the Weaponized Plutonium", 5, 8, 5, 40, 20, 12, 0, 85, 70, 29)]
    [InlineData("Transform Citizens Into Demons", 1, 8, 1, 8, 3, 0, 14, 34, 42, 41)]
    [InlineData("Transform Citizens Into Demons", 2, 8, 5, 16, 10, 0, 14, 53, 70, 41)]
    [InlineData("Transform Citizens Into Demons", 5, 8, 5, 32, 20, 0, 14, 79, 70, 41)]
    [InlineData("X-Cutioner's Song", 1, 8, 1, 8, 3, 0, 14, 34, 42, 41)]
    [InlineData("X-Cutioner's Song", 2, 8, 5, 16, 10, 0, 14, 53, 70, 41)]
    [InlineData("X-Cutioner's Song", 5, 8, 5, 32, 20, 0, 14, 79, 70, 41)]
    public void Scheme_lays_out_its_decks_and_stacks(
        string scheme, int players, int twists, int strikes, int villainCards, int henchmanCards, int bystanders,
        int outsideHeroCards, int villainDeck, int heroDeck, int bystanderStack)
    {
        var setup = Draw(players, scheme);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Mephisto", setup.Mastermind.Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Equal(strikes, setup.VillainDeck.MasterStrikes);
        Assert.Equal(villainCards, setup.VillainDeck.VillainCards);
        Assert.Equal(henchmanCards, setup.VillainDeck.HenchmanCards);
        Assert.Equal(bystanders, setup.VillainDeck.Bystanders);
        Assert.Equal(outsideHeroCards, setup.VillainDeck.OutsideHeroCards);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
        Assert.Equal(heroDeck, setup.HeroDeck.Total);
        Assert.Equal(new SetupStacks(30, 30, bystanderStack), setup.Stacks);
    }

    [Fact]
    public void Capture_Baby_Hope_lists_its_token_as_a_setup_step()
    {
        var setup = Draw(2, "Capture Baby Hope");

        Assert.Equal(["Place the Baby Hope token on the Scheme"], setup.Steps);
        Assert.Contains(
            new RuleNote("Scheme adds a setup step: Place the Baby Hope token on the Scheme", "Card", null, DarkCityName), setup.Notes);
    }

    [Fact]
    public void Detonate_the_Helicarrier_uses_6_Heroes_even_in_Solo()
    {
        var two = Draw(2, "Detonate the Helicarrier");
        var solo = Draw(1, "Detonate the Helicarrier");

        Assert.Equal(6, two.Heroes.Count);
        Assert.Contains(new RuleNote("Scheme uses 6 Heroes", "Card", null, DarkCityName), two.Notes);
        Assert.Equal(6, solo.Heroes.Count);
        Assert.Contains(
            new RuleNote("Scheme overrides Solo: 6 Heroes", "D2", "https://boardgamegeek.com/thread/884926", CoreName), solo.Notes);
    }

    [Fact]
    public void Organized_Crime_Wave_puts_all_10_Maggia_Goons_in_the_Solo_Villain_Deck()
    {
        var setup = Draw(1, "Organized Crime Wave");

        Assert.Equal(["Maggia Goons"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(10, setup.VillainDeck.HenchmanCards);
        Assert.Contains(new RuleNote("Scheme requires Maggia Goons", "Card", null, DarkCityName), setup.Notes);
        Assert.Contains(
            new RuleNote("Scheme overrides Solo: 10 Henchmen of each Henchman Group", "D2", "https://boardgamegeek.com/thread/884926", CoreName),
            setup.Notes);
    }

    [Fact]
    public void Organized_Crime_Wave_puts_10_Henchmen_of_each_Henchman_Group_in_the_Villain_Deck()
    {
        var setup = Draw(5, "Organized Crime Wave");

        Assert.Equal(["Maggia Goons", "Doombot Legion"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Contains(
            new RuleNote("Scheme puts 10 Henchmen of each Henchman Group in the Villain Deck", "Card", null, DarkCityName), setup.Notes);
    }

    [Theory]
    [InlineData(1, 12)]
    [InlineData(2, 24)]
    [InlineData(5, 24)]
    public void Save_Humanity_moves_Bystanders_into_the_Hero_Deck(int players, int moved)
    {
        var setup = Draw(players, "Save Humanity");

        Assert.Equal([new MovedCards(CardKind.Bystander, Pile.Bystanders, Pile.HeroDeck, moved, moved)], setup.Moves);
        Assert.Contains(new RuleNote($"Scheme moves {moved} Bystanders into the Hero Deck", "Card", null, DarkCityName), setup.Notes);
    }

    [Fact]
    public void Steal_the_Weaponized_Plutonium_adds_a_Villain_Group()
    {
        var setup = Draw(2, "Steal the Weaponized Plutonium");

        Assert.Equal(["Underworld", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme adds 1 Villain Group", "Card", null, DarkCityName), setup.Notes);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Transform_Citizens_Into_Demons_puts_Jean_Grey_in_the_Villain_Deck_and_no_Bystanders(int players)
    {
        var setup = Draw(players, "Transform Citizens Into Demons");

        var jeanGrey = DarkCity.Heroes.Single(hero => hero.Name == "Jean Grey");
        Assert.Equal([new OutsideHero(jeanGrey, Pile.VillainDeck, 14)], setup.OutsideHeroes);
        Assert.DoesNotContain(jeanGrey, setup.Heroes);
        Assert.Contains(
            new RuleNote("Scheme draws 1 extra Jean Grey Hero outside the Hero Deck and puts its cards into the Villain Deck", "Card", null, DarkCityName),
            setup.Notes);
    }

    [Fact]
    public void X_Cutioners_Song_puts_a_Hero_from_outside_the_Hero_Deck_in_the_Villain_Deck()
    {
        var setup = Draw(2, "X-Cutioner's Song");

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal(new OutsideHero(outside.Hero, Pile.VillainDeck, 14), outside);
        Assert.DoesNotContain(outside.Hero, setup.Heroes);
        Assert.Contains(new RuleNote("Scheme puts 0 Bystanders in the Villain Deck", "Card", null, DarkCityName), setup.Notes);
    }

    // Drawing with the core box

    [Fact]
    public void A_core_Scheme_can_draw_a_Dark_City_Mastermind_and_Heroes_from_both_boxes()
    {
        // Legacy Virus with Kingpin at 3 players: Streets of New York fills one of the 3 Villain Group slots,
        // and the 5 Heroes are drawn from the 15 core and 17 Dark City Heroes; the last draw takes Wolverine (X-Force).
        var random = new ScriptedRandom(0, 5, 0, 0, 0, 0, 0, 0, 0, 27);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, Boxes, random));

        Assert.Equal([16, 9], random.Options.Take(2));
        Assert.Equal("Legacy Virus", setup.Scheme.Name);
        Assert.Equal("Kingpin", setup.Mastermind.Name);
        Assert.Equal(["Streets of New York", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal([32, 31, 30, 29, 28], random.Options[^5..]);
        Assert.Equal(
            ["Black Widow", "Captain America", "Cyclops", "Deadpool", "Wolverine (X-Force)"],
            setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(new VillainDeck(8, 5, 24, 10, 8, 0), setup.VillainDeck);
        Assert.Equal(new SetupStacks(18, 30, 33), setup.Stacks);
        Assert.Equal(
            [
                new RuleNote("Scheme sets the Wound stack to 6 per player", "Card", null, CoreName),
                new RuleNote("Kingpin always leads Streets of New York", "R p.6", Rulebook, CoreName),
            ],
            setup.Notes);
    }

    // The exclusion case: with only the core box included, Dark City changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void With_only_the_core_box_included_Dark_City_changes_no_draw(int players)
    {
        using var coreOnly = new CoreOnlyDirectory();
        var coreAlone = new SetupGenerator(BoxCatalog.Load(coreOnly.Path));

        for (var seed = 0; seed < 40; seed++)
        {
            var withDarkCityLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, ["core"], new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(coreAlone.Generate(players, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withDarkCityLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withDarkCityLoaded), id => id.StartsWith("dark-city_"));
            Assert.Equal(["core"], withDarkCityLoaded.Boxes.Select(box => box.Id));
        }
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, Mephisto)));
    }

    private static string TermName(string id) =>
        Catalog.Boxes.SelectMany(box => box.Glossary).Single(term => term.Id == id).Name;

    private static IEnumerable<string> ComponentIds(SetupResult setup) =>
        new[] { setup.Scheme.Id, setup.Mastermind.Id }
            .Concat(setup.VillainGroups.Select(group => group.Id))
            .Concat(setup.HenchmanGroups.Select(group => group.Id))
            .Concat(setup.Heroes.Select(hero => hero.Id))
            .Concat(setup.OutsideHeroes.Select(outside => outside.Hero.Id));

    // A directory holding only the core box file, so a catalog loaded from it has never seen Dark City.
    private sealed class CoreOnlyDirectory : IDisposable
    {
        public string Path { get; } = Directory.CreateTempSubdirectory("legendary-core-only-").FullName;

        public CoreOnlyDirectory() =>
            File.Copy(System.IO.Path.Combine(BoxCatalog.DefaultDirectory, "core.json"), System.IO.Path.Combine(Path, "core.json"));

        public void Dispose() => Directory.Delete(Path, recursive: true);
    }
}
