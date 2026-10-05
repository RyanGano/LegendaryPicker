using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Noir box file (Data/Boxes/noir.json), pinned to the rules insert (N), the card catalog and the card faces it links,
// and drawn with the core box. Catalog order puts the core box's cards first, so its Schemes follow the core Schemes the
// player count allows and its Masterminds follow the core Masterminds.
public class NoirTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Noir = Catalog.Boxes.Single(box => box.Id == "noir");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "noir"];

    private static readonly string[] SchemeNames =
    [
        "Find the Split Personality Killer",
        "Silence the Witnesses",
        "Five Families of Crime",
        "Hidden Heart of Darkness",
    ];

    private static readonly Box Core = Catalog.Boxes.Single(box => box.Id == "core");
    private static readonly int CharlesXavier = Core.Masterminds.Count;
    private static readonly int Goblin = CharlesXavier + 1;

    // Catalog

    [Fact]
    public void Noir_is_an_expansion_with_the_contents_of_the_insert()
    {
        Assert.Equal("Noir", Noir.Name);
        Assert.False(Noir.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, Noir.Ruleset);
        Assert.Equal(new Sourced<string>("2017-02", "Q"), Noir.About.Released);
        Assert.Equal(new Sourced<int>(14, "N p.2"), Noir.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "N p.2"), Noir.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "N p.2"), Noir.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "N p.2"), Noir.Components.SchemeTwists);
    }

    [Fact]
    public void Noir_has_the_5_Heroes_2_Villain_Groups_2_Masterminds_and_4_Schemes()
    {
        Assert.Equal(
            ["Angel Noir", "Daredevil Noir", "Iron Man Noir", "Luke Cage Noir", "Spider-Man Noir"],
            Noir.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Goblin's Freak Show", "X-Men Noir"], Noir.VillainGroups.Select(group => group.Name));
        Assert.Empty(Noir.HenchmanGroups);
        Assert.Equal(["Charles Xavier, Professor of Crime", "The Goblin, Underworld Boss"], Noir.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, Noir.Schemes.Select(scheme => scheme.Name));
    }

    [Theory]
    [InlineData("Angel Noir", "X-Men", "Covert Instinct Strength", "Investigate")]
    [InlineData("Daredevil Noir", "Marvel Knights", "Covert Instinct", "Investigate")]
    [InlineData("Iron Man Noir", "Avengers", "Ranged Tech", "Investigate")]
    [InlineData("Luke Cage Noir", "Marvel Knights", "Covert Strength", "Investigate")]
    [InlineData("Spider-Man Noir", "Spider Friends", "Instinct Ranged Strength Tech", "Investigate Rescue a Bystander")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string team, string classes, string keywords)
    {
        var entry = Noir.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Only Wound effects: Vulture's Fight and Escape, Charles Xavier's X-Con Men and the Goblin's Master Strike and Sinister Dreams.
    // Hidden Witnesses come from the Bystander Stack, which every setup has, so they bring no part.
    [Fact]
    public void Noir_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. Noir.Heroes, .. Noir.VillainGroups, .. Noir.HenchmanGroups, .. Noir.Masterminds, .. Noir.Schemes];
        Assert.Equal(
            [
                "noir_villain_goblin-s-freak-show: Wounds",
                "noir_mastermind_charles-xavier-professor-of-crime: Wounds",
                "noir_mastermind_the-goblin-underworld-boss: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = Noir.Schemes.ToDictionary(scheme => scheme.Name);

        Assert.Equal(["all: 8 Card"], Twists(schemes["Find the Split Personality Killer"]));
        Assert.Equal(["all: 6 Card"], Twists(schemes["Silence the Witnesses"]));

        var families = schemes["Five Families of Crime"];
        Assert.Equal(["all: 8 Card"], Twists(families));
        var extra = Assert.Single(families.Setup.ExtraVillainGroups!);
        Assert.Equal((null, 2, "Card"), (extra.Players, extra.Value, extra.Source));
        Assert.Equal([new SetupStep("Deal the Villain Deck into 5 shuffled piles, one over each city space", "Card")], families.Setup.Steps);

        var heart = schemes["Hidden Heart of Darkness"];
        Assert.Equal(["all: 8 Card"], Twists(heart));
        Assert.Equal(new Sourced<int>(4, "Card"), heart.Setup.OwnTactics);

        static IEnumerable<string> Twists(Scheme scheme) =>
            scheme.Setup.Twists.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Charles Xavier, Professor of Crime", "X-Men Noir")]
    [InlineData("The Goblin, Underworld Boss", "Goblin's Freak Show")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = Noir.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((GroupType.Villain, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        Assert.Equal(group, Noir.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Noir_glossary_adds_its_two_keywords_from_the_insert()
    {
        Assert.Equal(
            ["Investigate keyword N p.1", "Hidden Witnesses keyword N p.1"],
            Noir.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.Equal("N p.2; C1", Noir.CatalogSource);
    }

    // Draws

    [Theory]
    [InlineData("Find the Split Personality Killer", 1, 8)]
    [InlineData("Silence the Witnesses", 3, 6)]
    [InlineData("Five Families of Crime", 2, 8)]
    [InlineData("Hidden Heart of Darkness", 5, 8)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, CharlesXavier);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Five_Families_of_Crime_adds_two_Villain_Groups_and_a_step_to_split_the_Villain_Deck(int players)
    {
        var setup = Draw(players, "Five Families of Crime", CharlesXavier);
        var plain = Draw(players, "Silence the Witnesses", CharlesXavier);

        Assert.Equal(plain.VillainGroups.Count + 2, setup.VillainGroups.Count);
        Assert.Contains("Deal the Villain Deck into 5 shuffled piles, one over each city space", setup.Steps);
        Assert.DoesNotContain("Deal the Villain Deck into 5 shuffled piles, one over each city space", plain.Steps);
    }

    // The Mastermind's four Tactics go into the Villain Deck as Villains, so the deck's total includes them.
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void Hidden_Heart_of_Darkness_shuffles_the_4_Tactics_of_its_Mastermind_into_the_Villain_Deck(int players)
    {
        var setup = Draw(players, "Hidden Heart of Darkness", Goblin);
        var plain = Draw(players, "Find the Split Personality Killer", Goblin);

        Assert.Equal(4, setup.VillainDeck.OwnTactics);
        Assert.Equal(0, setup.VillainDeck.MastermindTactics);
        Assert.Equal(plain.VillainDeck.Total + 4, setup.VillainDeck.Total);
        Assert.Contains(setup.Notes, note => note.Text == "Scheme shuffles the 4 Tactics of its Mastermind into the Villain Deck" && note.Citation == "Card");
        Assert.Equal(0, plain.VillainDeck.OwnTactics);
    }

    [Theory]
    [InlineData(0, "Charles Xavier, Professor of Crime", "X-Men Noir")]
    [InlineData(1, "The Goblin, Underworld Boss", "Goblin's Freak Show")]
    public void Mastermind_brings_its_Always_Leads_group(int offset, string mastermind, string group)
    {
        var setup = Draw(2, "Silence the Witnesses", CharlesXavier + offset);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name));
    }

    [Fact]
    public void The_Goblin_lays_out_the_Wounds_its_Master_Strike_gives()
    {
        Assert.NotNull(Draw(2, "Silence the Witnesses", Goblin).Stacks.Wounds);
    }

    // The exclusion case: with every other box of its ruleset included, Noir changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Noir_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("noir.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("noir_"));
        }
    }

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(SchemeDraw(players, scheme), mastermind)));

    // The Schemes the player count allows come in catalog order, the core box's before Noir's.
    private static int SchemeDraw(int players, string scheme) =>
        Core.Schemes.Count(s => s.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true) + Array.IndexOf(SchemeNames, scheme);

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
