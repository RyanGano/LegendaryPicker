using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Captain America 75th Anniversary box file (Data/Boxes/captain-america-75th-anniversary.json), pinned to the rulesheet
// (CA75), the card catalog and the card faces it links, and drawn with the core box. Catalog order puts its cards before the
// core box's, so its Schemes are draws 0 to 3 at every player count and its Masterminds 0 Arnim Zola and 1 Baron Heinrich Zemo.
public class CaptainAmerica75Tests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box CaptainAmerica = Catalog.Boxes.Single(box => box.Id == "captain-america-75th-anniversary");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "captain-america-75th-anniversary"];

    private static readonly string[] SchemeNames =
    [
        "Brainwash the Military",
        "Change the Outcome of WWII",
        "Go Back in Time to Slay Heroes' Ancestors",
        "The Unbreakable Enigma Code",
    ];

    private const int Zola = 0;
    private const int Zemo = 1;

    // Catalog

    [Fact]
    public void Captain_America_75th_Anniversary_is_an_expansion_with_the_contents_of_the_rulesheet()
    {
        Assert.Equal("Captain America 75th Anniversary", CaptainAmerica.Name);
        Assert.False(CaptainAmerica.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, CaptainAmerica.Ruleset);
        Assert.Equal(new Sourced<string>("2016-03", "Q"), CaptainAmerica.About.Released);

        var components = CaptainAmerica.Components;
        Assert.Equal(new Sourced<int>(14, "CA75 p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "CA75 p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "CA75 p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "CA75 p.2"), components.SchemeTwists);
        Assert.Null(components.Bystanders);
        Assert.Null(components.Sidekicks);
    }

    [Fact]
    public void Captain_America_75th_Anniversary_has_the_5_Heroes_2_Villain_Groups_2_Masterminds_and_4_Schemes()
    {
        Assert.Equal(
            ["Agent X-13", "Captain America (Falcon)", "Captain America 1941", "Steve Rogers, Director of S.H.I.E.L.D.", "Winter Soldier"],
            CaptainAmerica.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Zola's Creations", "Masters of Evil (WWII)"], CaptainAmerica.VillainGroups.Select(group => group.Name));
        Assert.Empty(CaptainAmerica.HenchmanGroups);
        Assert.Equal(["Arnim Zola", "Baron Heinrich Zemo"], CaptainAmerica.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, CaptainAmerica.Schemes.Select(scheme => scheme.Name));
    }

    // Each Hero takes its team, the classes its four cards show and its keywords from the C1 card index and the card faces.
    [Theory]
    [InlineData("Agent X-13", "S.H.I.E.L.D.", "Covert Instinct Ranged Tech", "Man Out of Time Savior")]
    [InlineData("Captain America (Falcon)", "Avengers", "Covert Instinct Ranged Tech", "Savior")]
    [InlineData("Captain America 1941", "Avengers", "Covert Instinct Strength Tech", "Man Out of Time Savior")]
    [InlineData("Steve Rogers, Director of S.H.I.E.L.D.", "S.H.I.E.L.D.", "Covert Instinct Strength Tech", "Man Out of Time Savior")]
    [InlineData("Winter Soldier", null, "Covert Strength Tech", "Man Out of Time")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string? team, string classes, string keywords)
    {
        var entry = CaptainAmerica.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, entry.Team is null ? null : TermName(entry.Team));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Each version of Captain America and Steve Rogers prints its own Hero Name on its cards, so each is its own Hero.
    [Fact]
    public void Each_Hero_prints_a_Hero_Name_of_its_own()
    {
        Assert.All(CaptainAmerica.Heroes, hero => Assert.Equal(hero.Name, hero.NameOfHero));
    }

    // Officers: Spy Network lets a player gain one, and Brainwash the Military puts 12 in the Villain Deck. Wounds: Man-Fish
    // and Radioactive Man, Crush Pacifist Resistance and Baron Zemo's Master Strike.
    [Fact]
    public void Captain_America_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. CaptainAmerica.Heroes, .. CaptainAmerica.VillainGroups, .. CaptainAmerica.HenchmanGroups, .. CaptainAmerica.Masterminds, .. CaptainAmerica.Schemes];
        Assert.Equal(
            [
                "captain-america-75th-anniversary_hero_agent-x-13: Officers",
                "captain-america-75th-anniversary_villain_zola-s-creations: Wounds",
                "captain-america-75th-anniversary_villain_masters-of-evil-wwii: Wounds",
                "captain-america-75th-anniversary_mastermind_arnim-zola: Wounds",
                "captain-america-75th-anniversary_mastermind_baron-heinrich-zemo: Wounds",
                "captain-america-75th-anniversary_scheme_brainwash-the-military: Officers",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = CaptainAmerica.Schemes.ToDictionary(scheme => scheme.Name);

        var brainwash = schemes["Brainwash the Military"];
        Assert.Equal(["all: 7 Card"], Twists(brainwash));
        var move = Assert.Single(brainwash.Setup.Moves!);
        Assert.Equal((CardKind.Officer, Pile.VillainDeck, false), (move.Card, move.To, move.PerPlayer));
        Assert.Equal([new PlayerCountValue(null, 12, "Card")], move.Count);

        var outcome = schemes["Change the Outcome of WWII"];
        Assert.Equal(["all: 7 Card"], Twists(outcome));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], outcome.Setup.ExtraVillainGroups);

        var ancestors = schemes["Go Back in Time to Slay Heroes' Ancestors"];
        Assert.Equal(["all: 9 Card"], Twists(ancestors));
        Assert.Equal([new PlayerCountValue(null, 8, "Card")], ancestors.Setup.Heroes);

        Assert.Equal(["all: 6 Card"], Twists(schemes["The Unbreakable Enigma Code"]));

        static IEnumerable<string> Twists(Scheme scheme) =>
            scheme.Setup.Twists.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Arnim Zola", "Zola's Creations")]
    [InlineData("Baron Heinrich Zemo", "Masters of Evil (WWII)")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = CaptainAmerica.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((GroupType.Villain, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        Assert.Equal(group, CaptainAmerica.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Captain_America_glossary_adds_its_keywords_from_the_rulesheet()
    {
        Assert.Equal(
            ["Man Out of Time keyword CA75 p.1", "Savior keyword CA75 p.1", "Abomination keyword CA75 p.1"],
            CaptainAmerica.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.Equal("CA75 p.2; C1", CaptainAmerica.CatalogSource);
    }

    // Draws

    [Theory]
    [InlineData("Brainwash the Military", 1, 7)]
    [InlineData("Brainwash the Military", 4, 7)]
    [InlineData("Change the Outcome of WWII", 2, 7)]
    [InlineData("Go Back in Time to Slay Heroes' Ancestors", 1, 9)]
    [InlineData("Go Back in Time to Slay Heroes' Ancestors", 5, 9)]
    [InlineData("The Unbreakable Enigma Code", 3, 6)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, Zola);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Arnim Zola", setup.Mastermind.Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
    }

    // The Officer-stack case: the 12 Officers the Scheme moves into the Villain Deck leave 18 of the core box's 30.
    [Fact]
    public void Brainwash_the_Military_moves_12_Officers_into_the_Villain_Deck()
    {
        var setup = Draw(2, "Brainwash the Military", Zola);

        Assert.Equal([new MovedCards(CardKind.Officer, Pile.Officers, Pile.VillainDeck, 12, 12)], setup.Moves);
        Assert.Equal(18, setup.Stacks.Officers);
        Assert.Contains(new RuleNote("Scheme moves 12 S.H.I.E.L.D. Officers into the Villain Deck", "Card", null, "Captain America 75th Anniversary"), setup.Notes);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(5, 5)]
    public void Change_the_Outcome_of_WWII_adds_a_Villain_Group(int players, int groups)
    {
        var setup = Draw(players, "Change the Outcome of WWII", Zola);

        Assert.Equal(groups, setup.VillainGroups.Count);
        Assert.Contains(new RuleNote("Scheme adds 1 Villain Group", "Card", null, "Captain America 75th Anniversary"), setup.Notes);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Go_Back_in_Time_puts_8_Heroes_in_the_Hero_Deck(int players)
    {
        Assert.Equal(8, Draw(players, "Go Back in Time to Slay Heroes' Ancestors", Zola).Heroes.Count);
    }

    [Theory]
    [InlineData(Zola, "Arnim Zola", "Zola's Creations")]
    [InlineData(Zemo, "Baron Heinrich Zemo", "Masters of Evil (WWII)")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "The Unbreakable Enigma Code", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name));
    }

    // A Mastermind whose Master Strike gives Wounds lays the Wound stack out.
    [Fact]
    public void A_Mastermind_that_gives_Wounds_lays_out_the_Wounds()
    {
        Assert.NotNull(Draw(2, "The Unbreakable Enigma Code", Zemo).Stacks.Wounds);
    }

    // The exclusion case: with every other box of its ruleset included, Captain America 75th Anniversary changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Captain_America_75th_Anniversary_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("captain-america-75th-anniversary.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("captain-america-75th-anniversary_"));
        }
    }

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(Array.IndexOf(SchemeNames, scheme), mastermind)));

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
