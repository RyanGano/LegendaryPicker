using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Fantastic Four box file (Data/Boxes/fantastic-four.json), pinned to the rules insert (FF) and the card
// catalogs, and drawn with the core box. Catalog order puts its cards after the core box's, so at 2–5
// players its Schemes are 8 Bathe the Earth in Cosmic Rays, 9 Flood the Planet with Melted Glaciers,
// 10 Invincible Force Field and 11 Pull Reality Into the Negative Zone; Solo allows 6 core Schemes, so
// there they are 6 to 9. Its Masterminds are 4 Galactus and 5 Mole Man.
public class FantasticFourTests
{
    private const string FantasticFourName = "Fantastic Four";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box FantasticFour = Catalog.Boxes.Single(box => box.Id == "fantastic-four");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "fantastic-four"];

    private static readonly string[] SchemeNames =
    [
        "Bathe the Earth in Cosmic Rays",
        "Flood the Planet with Melted Glaciers",
        "Invincible Force Field",
        "Pull Reality Into the Negative Zone",
    ];

    private const int Galactus = 4;
    private const int MoleMan = 5;

    // Catalog

    [Fact]
    public void Fantastic_Four_is_an_expansion_listed_after_Dark_City()
    {
        Assert.Equal(["core", "dark-city", "fantastic-four"], Catalog.Boxes.Select(box => box.Id).Take(3));
        Assert.Equal(FantasticFourName, FantasticFour.Name);
        Assert.Equal(3, FantasticFour.SchemaVersion);
        Assert.False(FantasticFour.IsBaseGame);
    }

    [Fact]
    public void Fantastic_Four_has_the_5_Heroes_2_Villain_Groups_and_2_Masterminds_in_the_rules_insert()
    {
        Assert.Equal(
            ["Human Torch", "Invisible Woman", "Mr. Fantastic", "Silver Surfer", "Thing"],
            FantasticFour.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Heralds of Galactus", "Subterranea"], FantasticFour.VillainGroups.Select(group => group.Name));
        Assert.Empty(FantasticFour.HenchmanGroups);
        Assert.Equal(["Galactus", "Mole Man"], FantasticFour.Masterminds.Select(mastermind => mastermind.Name));
        Assert.All(FantasticFour.Masterminds, mastermind => Assert.Null(mastermind.Setup));
    }

    [Fact]
    public void Fantastic_Four_has_the_4_Schemes_in_the_rules_insert_each_changing_only_its_Twists()
    {
        Assert.Equal(SchemeNames, FantasticFour.Schemes.Select(scheme => scheme.Name));
        Assert.Equal(
            [new PlayerCountValue(null, 6, "Card"), new PlayerCountValue(null, 8, "Card"), new PlayerCountValue(null, 7, "Card"), new PlayerCountValue(null, 8, "Card")],
            FantasticFour.Schemes.Select(scheme => Assert.Single(scheme.Setup.Twists)));
        Assert.All(FantasticFour.Schemes, scheme => Assert.Equal(new SchemeSetup(scheme.Setup.Twists), scheme.Setup));
    }

    [Fact]
    public void Fantastic_Four_component_counts_match_the_rules_insert()
    {
        var components = FantasticFour.Components;

        Assert.Equal(new Sourced<int>(14, "FF p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "FF p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "FF p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "FF p.2"), components.SchemeTwists);
        Assert.Null(components.Bystanders);
        Assert.Null(components.Wounds);
        Assert.Null(components.Officers);
        Assert.Null(components.Sidekicks);
    }

    [Theory]
    [InlineData("Galactus", "Heralds of Galactus")]
    [InlineData("Mole Man", "Subterranea")]
    public void Mastermind_Always_Leads_the_Villain_Group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = FantasticFour.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal(GroupType.Villain, alwaysLeads.GroupType);
        Assert.Equal("Card", alwaysLeads.Source);
        Assert.Equal(group, FantasticFour.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Ids_name_the_Fantastic_Four_box()
    {
        var ids = FantasticFour.Heroes.Select(x => x.Id)
            .Concat(FantasticFour.VillainGroups.Select(x => x.Id))
            .Concat(FantasticFour.Masterminds.Select(x => x.Id))
            .Concat(FantasticFour.Schemes.Select(x => x.Id))
            .Concat(FantasticFour.Glossary.Select(x => x.Id))
            .ToList();

        Assert.All(ids, id => Assert.Matches("^fantastic-four_(hero|villain|mastermind|scheme|term)_[a-z0-9]+(-[a-z0-9]+)*$", id));
        Assert.Equal("fantastic-four_hero_mr-fantastic", FantasticFour.Heroes[2].Id);
    }

    [Fact]
    public void The_rules_insert_is_the_one_source_key()
    {
        Assert.Equal(
            [new SourceLink("FF", "https://upperdeck.com/wp-content/uploads/2024/05/Fantastic-4-Rules.pdf")],
            FantasticFour.Sources);
        Assert.Equal("FF p.2; C1; C2", FantasticFour.CatalogSource);
    }

    [Fact]
    public void Fantastic_Four_glossary_adds_a_team_and_3_keywords_from_the_rules_insert()
    {
        Assert.Equal(
            ["Fantastic Four team FF p.1", "Focus keyword FF p.1", "Burrow keyword FF p.1", "Cosmic Threat keyword FF p.1"],
            FantasticFour.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    [Theory]
    [InlineData("Human Torch", "Fantastic Four", "Instinct Ranged", "Focus")]
    [InlineData("Invisible Woman", "Fantastic Four", "Covert Ranged", "Rescue a Bystander Focus")]
    [InlineData("Mr. Fantastic", "Fantastic Four", "Instinct Tech", "Focus")]
    [InlineData("Silver Surfer", null, "Covert Ranged Strength", "Focus")]
    [InlineData("Thing", "Fantastic Four", "Instinct Strength", "Rescue a Bystander Focus")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string? team, string classes, string keywords)
    {
        var entry = FantasticFour.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, entry.Team is null ? null : TermName(entry.Team));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    [Theory]
    [InlineData("Heralds of Galactus", "Ambush Escape Fight Cosmic Threat")]
    [InlineData("Subterranea", "Ambush Fight Burrow")]
    [InlineData("Galactus", "Always Leads Fight Master Strike Mastermind Tactic Rescue a Bystander Cosmic Threat")]
    [InlineData("Mole Man", "Always Leads Fight Master Strike Mastermind Tactic")]
    public void Component_lists_its_keywords(string component, string keywords)
    {
        var terms = FantasticFour.VillainGroups.Where(g => g.Name == component).Select(g => g.Terms)
            .Concat(FantasticFour.Masterminds.Where(m => m.Name == component).Select(m => m.Terms))
            .Single();

        Assert.Equal(keywords, string.Join(" ", terms.Select(TermName)));
    }

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Galactus (who leads the Heralds of
    // Galactus, except in Solo) and the first remaining option for every later draw. Fantastic Four adds
    // nothing to the stacks, so they hold the core box's 30 Wounds, 30 Officers and the Bystanders left of 30.

    [Theory]
    [InlineData("Bathe the Earth in Cosmic Rays", 1, 6, 1, 8, 3, 1, 19, 42, 29)]
    [InlineData("Bathe the Earth in Cosmic Rays", 2, 6, 5, 16, 10, 2, 39, 70, 28)]
    [InlineData("Bathe the Earth in Cosmic Rays", 5, 6, 5, 32, 20, 12, 75, 70, 18)]
    [InlineData("Flood the Planet with Melted Glaciers", 1, 8, 1, 8, 3, 1, 21, 42, 29)]
    [InlineData("Flood the Planet with Melted Glaciers", 2, 8, 5, 16, 10, 2, 41, 70, 28)]
    [InlineData("Flood the Planet with Melted Glaciers", 5, 8, 5, 32, 20, 12, 77, 70, 18)]
    [InlineData("Invincible Force Field", 1, 7, 1, 8, 3, 1, 20, 42, 29)]
    [InlineData("Invincible Force Field", 2, 7, 5, 16, 10, 2, 40, 70, 28)]
    [InlineData("Invincible Force Field", 5, 7, 5, 32, 20, 12, 76, 70, 18)]
    [InlineData("Pull Reality Into the Negative Zone", 1, 8, 1, 8, 3, 1, 21, 42, 29)]
    [InlineData("Pull Reality Into the Negative Zone", 2, 8, 5, 16, 10, 2, 41, 70, 28)]
    [InlineData("Pull Reality Into the Negative Zone", 5, 8, 5, 32, 20, 12, 77, 70, 18)]
    public void Scheme_lays_out_its_decks_and_stacks(
        string scheme, int players, int twists, int strikes, int villainCards, int henchmanCards, int bystanders,
        int villainDeck, int heroDeck, int bystanderStack)
    {
        var setup = Draw(players, scheme, Galactus);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Galactus", setup.Mastermind.Name);
        Assert.Equal(new VillainDeck(twists, strikes, villainCards, henchmanCards, bystanders, 0), setup.VillainDeck);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
        Assert.Equal(heroDeck, setup.HeroDeck.Total);
        Assert.Equal(new SetupStacks(30, 30, bystanderStack), setup.Stacks);
    }

    [Theory]
    [InlineData(Galactus, "Galactus", "Heralds of Galactus")]
    [InlineData(MoleMan, "Mole Man", "Subterranea")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "Invincible Force Field", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Equal([group, "Brotherhood"], setup.VillainGroups.Select(g => g.Name));
        Assert.Contains(new RuleNote($"{mastermind} always leads {group}", "R p.6", Rulebook, CoreName), setup.Notes);
    }

    // Drawing with the core box and Dark City

    [Fact]
    public void A_core_Scheme_can_draw_a_Fantastic_Four_Mastermind_and_Heroes_from_all_three_boxes()
    {
        // Super Hero Civil War with Mole Man at 3 players: Subterranea fills one of the 3 Villain Group slots,
        // and the Heroes come from the 15 core, 17 Dark City and 5 Fantastic Four Heroes; the last draw takes Thing.
        var random = new ScriptedRandom(6, 10, 0, 0, 0, 0, 0, 0, 0, 32);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, ["core", "dark-city", "fantastic-four"], random));

        Assert.Equal([19, 11], random.Options.Take(2));
        Assert.Equal("Super Hero Civil War", setup.Scheme.Name);
        Assert.Equal("Mole Man", setup.Mastermind.Name);
        Assert.Equal(["Subterranea", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal([37, 36, 35, 34, 33], random.Options[^5..]);
        Assert.Equal(
            ["Black Widow", "Captain America", "Cyclops", "Deadpool", "Thing"],
            setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(["core", "dark-city", "fantastic-four"], setup.Boxes.Select(box => box.Id));
    }

    // The exclusion case: with only the core box and Dark City included, Fantastic Four changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Fantastic_Four_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("fantastic-four.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("fantastic-four_"));
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
