using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Paint the Town Red box file (Data/Boxes/paint-the-town-red.json), pinned to the rules insert (PTTR)
// and the card catalogs, and drawn with the core box. Catalog order puts its cards after the core box's, so
// at 2–5 players its Schemes are 8 Splice Humans with Spider DNA, 9 The Clone Saga and 10 Weave a Web of
// Lies; Solo allows 6 core Schemes, so there they are 6 to 8. Its Masterminds are 4 Carnage and 5 Mysterio.
public class PaintTheTownRedTests
{
    private const string PaintTheTownRedName = "Paint the Town Red";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box PaintTheTownRed = Catalog.Boxes.Single(box => box.Id == "paint-the-town-red");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "paint-the-town-red"];

    private static readonly string[] SchemeNames =
    [
        "Splice Humans with Spider DNA",
        "The Clone Saga",
        "Weave a Web of Lies",
    ];

    private const int Carnage = 4;
    private const int Mysterio = 5;

    // Catalog

    [Fact]
    public void Paint_the_Town_Red_is_an_expansion_listed_after_Fantastic_Four()
    {
        Assert.Equal(["core", "dark-city", "fantastic-four", "paint-the-town-red"], Catalog.Boxes.Select(box => box.Id).Take(4));
        Assert.Equal(PaintTheTownRedName, PaintTheTownRed.Name);
        Assert.Equal(3, PaintTheTownRed.SchemaVersion);
        Assert.False(PaintTheTownRed.IsBaseGame);
    }

    [Fact]
    public void Paint_the_Town_Red_has_the_5_Heroes_2_Villain_Groups_and_2_Masterminds_in_the_rules_insert()
    {
        Assert.Equal(
            ["Black Cat", "Moon Knight", "Scarlet Spider", "Spider-Woman", "Symbiote Spider-Man"],
            PaintTheTownRed.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Maximum Carnage", "Sinister Six"], PaintTheTownRed.VillainGroups.Select(group => group.Name));
        Assert.Empty(PaintTheTownRed.HenchmanGroups);
        Assert.Equal(["Carnage", "Mysterio"], PaintTheTownRed.Masterminds.Select(mastermind => mastermind.Name));
        Assert.All(PaintTheTownRed.Masterminds, mastermind => Assert.Null(mastermind.Setup));
    }

    // Invade the Daily Bugle News HQ is the insert's fourth Scheme. It is left out: its card adds 6 extra
    // Henchmen from one Henchman Group to the Hero Deck. A move takes Henchmen out of the Villain Deck's own
    // groups, leaving it 6 short, and an extra Henchman Group puts all 10 of its cards in the Villain Deck, so
    // no setup field can say that only 6 cards of a group are used, and in the Hero Deck.
    [Fact]
    public void Paint_the_Town_Red_has_3_of_the_4_Schemes_in_the_rules_insert()
    {
        Assert.Equal(SchemeNames, PaintTheTownRed.Schemes.Select(scheme => scheme.Name));
        Assert.Equal(
            [new PlayerCountValue(null, 8, "Card"), new PlayerCountValue(null, 8, "Card"), new PlayerCountValue(null, 7, "Card")],
            PaintTheTownRed.Schemes.Select(scheme => Assert.Single(scheme.Setup.Twists)));
        Assert.All(PaintTheTownRed.Schemes, scheme => Assert.Null(scheme.Setup.AllowedPlayerCounts));
    }

    [Fact]
    public void Only_Splice_Humans_with_Spider_DNA_changes_more_than_its_Twists()
    {
        var splice = PaintTheTownRed.Schemes[0];

        Assert.Equal(
            [new RequiredGroup("paint-the-town-red_villain_sinister-six", GroupType.Villain, "Card")],
            splice.Setup.RequiredGroups);
        Assert.Equal(new SchemeSetup(splice.Setup.Twists, RequiredGroups: splice.Setup.RequiredGroups), splice.Setup);
        Assert.All(PaintTheTownRed.Schemes.Skip(1), scheme => Assert.Equal(new SchemeSetup(scheme.Setup.Twists), scheme.Setup));
    }

    [Fact]
    public void Paint_the_Town_Red_component_counts_match_the_rules_insert()
    {
        var components = PaintTheTownRed.Components;

        Assert.Equal(new Sourced<int>(14, "PTTR p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "PTTR p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "PTTR p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "PTTR p.2"), components.SchemeTwists);
        Assert.Null(components.Bystanders);
        Assert.Null(components.Wounds);
        Assert.Null(components.Officers);
        Assert.Null(components.Sidekicks);
    }

    [Theory]
    [InlineData("Carnage", "Maximum Carnage")]
    [InlineData("Mysterio", "Sinister Six")]
    public void Mastermind_Always_Leads_the_Villain_Group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = PaintTheTownRed.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal(GroupType.Villain, alwaysLeads.GroupType);
        Assert.Equal("Card", alwaysLeads.Source);
        Assert.Equal(group, PaintTheTownRed.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Ids_name_the_Paint_the_Town_Red_box()
    {
        var ids = PaintTheTownRed.Heroes.Select(x => x.Id)
            .Concat(PaintTheTownRed.VillainGroups.Select(x => x.Id))
            .Concat(PaintTheTownRed.Masterminds.Select(x => x.Id))
            .Concat(PaintTheTownRed.Schemes.Select(x => x.Id))
            .Concat(PaintTheTownRed.Glossary.Select(x => x.Id))
            .ToList();

        Assert.All(ids, id => Assert.Matches("^paint-the-town-red_(hero|villain|mastermind|scheme|term)_[a-z0-9]+(-[a-z0-9]+)*$", id));
        Assert.Equal("paint-the-town-red_hero_symbiote-spider-man", PaintTheTownRed.Heroes[^1].Id);
    }

    [Fact]
    public void The_rules_insert_is_the_one_source_key()
    {
        Assert.Equal(
            [new SourceLink("PTTR", "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Paint_The_Town_Red.pdf")],
            PaintTheTownRed.Sources);
        Assert.Equal("PTTR p.2; C1; C2", PaintTheTownRed.CatalogSource);
    }

    [Fact]
    public void Paint_the_Town_Red_glossary_adds_2_keywords_from_the_rules_insert()
    {
        Assert.Equal(
            ["Wall-Crawl keyword PTTR p.1", "Feast keyword PTTR p.1"],
            PaintTheTownRed.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    // Its teams are the core box's Spider Friends and Dark City's Marvel Knights, so it adds no team term.
    [Theory]
    [InlineData("Black Cat", "Black Cat", "Spider Friends", "Covert Instinct", "Rescue a Bystander Wall-Crawl")]
    [InlineData("Moon Knight", "Moon Knight", "Marvel Knights", "Instinct Tech", "Rescue a Bystander Wall-Crawl")]
    [InlineData("Scarlet Spider", "Scarlet Spider", "Spider Friends", "Covert Instinct Strength", "Wall-Crawl")]
    [InlineData("Spider-Woman", "Spider-Woman", "Spider Friends", "Covert Ranged Strength", "Wall-Crawl")]
    [InlineData("Symbiote Spider-Man", "Spider-Man", "Spider Friends", "Covert Instinct Ranged Strength", "Wall-Crawl")]
    public void Hero_lists_its_Hero_Name_team_classes_and_keywords(string hero, string heroName, string team, string classes, string keywords)
    {
        var entry = PaintTheTownRed.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
        Assert.Equal(heroName, entry.NameOfHero);
    }

    // Hero Name is the character, so Symbiote Spider-Man and the core box's Spider-Man are two Heroes with one
    // Hero Name. A made-up Scheme that needs exactly 2 Spider-Man Heroes can only be completed if they share it.
    [Fact]
    public void Symbiote_Spider_Man_shares_the_Hero_Name_Spider_Man_with_the_core_box()
    {
        using var directory = new DirectoryWithout(fileName: null);
        File.WriteAllText(System.IO.Path.Combine(directory.Path, "spider-names.json"), SpiderNamesBox);
        var generator = new SetupGenerator(BoxCatalog.Load(directory.Path));

        // At 2 players the core box allows 8 Schemes and Paint the Town Red 3, so draw 11 is the made-up Scheme.
        var random = new ScriptedRandom(11);
        var setup = Assert.IsType<SetupResult>(generator.Generate(2, ["core", "paint-the-town-red", "spider-names"], random));

        Assert.Equal(12, random.Options[0]);
        Assert.Equal("Test Two Spider-Men", setup.Scheme.Name);
        Assert.Equal(
            ["core_hero_spider-man", "paint-the-town-red_hero_symbiote-spider-man"],
            setup.Heroes.Where(hero => hero.Id.EndsWith("spider-man")).Select(hero => hero.Id));
    }

    private const string SpiderNamesBox = """
        {
          "schemaVersion": 3,
          "id": "spider-names",
          "name": "Spider Names Fixture",
          "catalogSource": "R p.1",
          "sources": [{ "key": "R", "url": "https://example.test/spider-names.pdf" }],
          "components": {
            "heroCards": { "value": 14, "source": "R p.1" },
            "villainGroupCards": { "value": 8, "source": "R p.1" },
            "henchmanGroupCards": { "value": 10, "source": "R p.1" },
            "schemeTwists": { "value": 0, "source": "R p.1" }
          },
          "heroes": [],
          "villainGroups": [],
          "henchmanGroups": [],
          "masterminds": [],
          "schemes": [
            {
              "id": "spider-names_scheme_test-two-spider-men",
              "name": "Test Two Spider-Men",
              "terms": ["core_term_scheme-twist"],
              "setup": {
                "twists": [{ "players": null, "value": 8, "source": "Card" }],
                "heroCounts": [{ "heroName": "Spider-Man", "exactly": 2, "source": "Card" }]
              }
            }
          ],
          "glossary": []
        }
        """;

    [Theory]
    [InlineData("Maximum Carnage", "Ambush Escape Fight Feast")]
    [InlineData("Sinister Six", "Ambush Escape Fight")]
    [InlineData("Carnage", "Always Leads Fight Master Strike Mastermind Tactic Feast")]
    [InlineData("Mysterio", "Always Leads Fight Master Strike Mastermind Tactic Rescue a Bystander")]
    [InlineData("Splice Humans with Spider DNA", "Scheme Twist Wall-Crawl")]
    [InlineData("The Clone Saga", "Scheme Twist")]
    [InlineData("Weave a Web of Lies", "Scheme Twist Rescue a Bystander")]
    public void Component_lists_its_keywords(string component, string keywords)
    {
        var terms = PaintTheTownRed.VillainGroups.Where(g => g.Name == component).Select(g => g.Terms)
            .Concat(PaintTheTownRed.Masterminds.Where(m => m.Name == component).Select(m => m.Terms))
            .Concat(PaintTheTownRed.Schemes.Where(s => s.Name == component).Select(s => s.Terms))
            .Single();

        Assert.Equal(keywords, string.Join(" ", terms.Select(TermName)));
    }

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Carnage (who leads Maximum Carnage,
    // except in Solo) and the first remaining option for every later draw. Paint the Town Red adds nothing to
    // the stacks, so they hold the core box's 30 Wounds, 30 Officers and the Bystanders left of 30.

    [Theory]
    [InlineData("Splice Humans with Spider DNA", 1, 8, 1, 8, 3, 1, 21, 42, 29)]
    [InlineData("Splice Humans with Spider DNA", 2, 8, 5, 16, 10, 2, 41, 70, 28)]
    [InlineData("Splice Humans with Spider DNA", 5, 8, 5, 32, 20, 12, 77, 70, 18)]
    [InlineData("The Clone Saga", 1, 8, 1, 8, 3, 1, 21, 42, 29)]
    [InlineData("The Clone Saga", 2, 8, 5, 16, 10, 2, 41, 70, 28)]
    [InlineData("The Clone Saga", 5, 8, 5, 32, 20, 12, 77, 70, 18)]
    [InlineData("Weave a Web of Lies", 1, 7, 1, 8, 3, 1, 20, 42, 29)]
    [InlineData("Weave a Web of Lies", 2, 7, 5, 16, 10, 2, 40, 70, 28)]
    [InlineData("Weave a Web of Lies", 5, 7, 5, 32, 20, 12, 76, 70, 18)]
    public void Scheme_lays_out_its_decks_and_stacks(
        string scheme, int players, int twists, int strikes, int villainCards, int henchmanCards, int bystanders,
        int villainDeck, int heroDeck, int bystanderStack)
    {
        var setup = Draw(players, scheme, Carnage);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Carnage", setup.Mastermind.Name);
        Assert.Equal(new VillainDeck(twists, strikes, villainCards, henchmanCards, bystanders, 0), setup.VillainDeck);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
        Assert.Equal(heroDeck, setup.HeroDeck.Total);
        Assert.Equal(new SetupStacks(30, 30, bystanderStack), setup.Stacks);
    }

    [Theory]
    [InlineData(1, Carnage, "Sinister Six")]
    [InlineData(2, Carnage, "Sinister Six|Maximum Carnage")]
    [InlineData(5, Carnage, "Sinister Six|Maximum Carnage|Brotherhood|Enemies of Asgard")]
    [InlineData(2, Mysterio, "Sinister Six|Brotherhood")]
    public void Splice_Humans_with_Spider_DNA_includes_Sinister_Six(int players, int mastermind, string groups)
    {
        var setup = Draw(players, "Splice Humans with Spider DNA", mastermind);

        Assert.Equal(groups.Split('|'), setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme requires Sinister Six", "Card", null, PaintTheTownRedName), setup.Notes);
    }

    [Theory]
    [InlineData(Carnage, "Carnage", "Maximum Carnage")]
    [InlineData(Mysterio, "Mysterio", "Sinister Six")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "The Clone Saga", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Equal([group, "Brotherhood"], setup.VillainGroups.Select(g => g.Name));
        Assert.Contains(new RuleNote($"{mastermind} always leads {group}", "R p.6", Rulebook, CoreName), setup.Notes);
    }

    // Drawing with the core box

    [Fact]
    public void A_core_Scheme_can_draw_a_Paint_the_Town_Red_Mastermind_and_Heroes_from_both_boxes()
    {
        // Legacy Virus with Mysterio at 3 players: Sinister Six fills one of the 3 Villain Group slots, and the
        // 5 Heroes are drawn from the 15 core and 5 Paint the Town Red Heroes; the last draw takes Moon Knight.
        var random = new ScriptedRandom(0, 5, 0, 0, 0, 0, 0, 0, 0, 12);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, Boxes, random));

        Assert.Equal([11, 6], random.Options.Take(2));
        Assert.Equal("Legacy Virus", setup.Scheme.Name);
        Assert.Equal("Mysterio", setup.Mastermind.Name);
        Assert.Equal(["Sinister Six", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal([20, 19, 18, 17, 16], random.Options[^5..]);
        Assert.Equal(
            ["Black Widow", "Captain America", "Cyclops", "Deadpool", "Moon Knight"],
            setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(["core", "paint-the-town-red"], setup.Boxes.Select(box => box.Id));
    }

    // The exclusion case: with the core box, Dark City and Fantastic Four included, Paint the Town Red changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Paint_the_Town_Red_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("paint-the-town-red.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("paint-the-town-red_"));
            Assert.Equal(boxes, withItLoaded.Boxes.Select(box => box.Id));
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

    // A copy of the box directory without one box file (or with all of them when fileName is null), so a
    // catalog loaded from it has never seen that box.
    private sealed class DirectoryWithout : IDisposable
    {
        public string Path { get; } = Directory.CreateTempSubdirectory("legendary-without-").FullName;

        public DirectoryWithout(string? fileName)
        {
            foreach (var file in Directory.GetFiles(BoxCatalog.DefaultDirectory, "*.json").Where(file => System.IO.Path.GetFileName(file) != fileName))
            {
                File.Copy(file, System.IO.Path.Combine(Path, System.IO.Path.GetFileName(file)));
            }
        }

        public void Dispose() => Directory.Delete(Path, recursive: true);
    }
}
