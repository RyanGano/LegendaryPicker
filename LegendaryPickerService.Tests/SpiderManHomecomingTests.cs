using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Spider-Man Homecoming box file (Data/Boxes/spider-man-homecoming.json), pinned to the rules insert (SM), the card
// catalog and the card faces it links, and drawn with the core box.
public class SpiderManHomecomingTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Homecoming = Catalog.Boxes.Single(box => box.Id == "spider-man-homecoming");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "spider-man-homecoming"];

    private static readonly string[] SchemeNames =
    [
        "Distract the Hero",
        "Explosion at the Washington Monument",
        "Ferry Disaster",
        "Scavenge Alien Weaponry",
    ];

    private const string EpicStep = "Optional: play the Epic side instead; the Tactics stay the same";

    // Catalog

    [Fact]
    public void Spider_Man_Homecoming_is_an_expansion_with_the_contents_of_the_insert()
    {
        Assert.Equal("Spider-Man Homecoming", Homecoming.Name);
        Assert.False(Homecoming.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, Homecoming.Ruleset);
        Assert.Equal(new Sourced<string>("2017-10", "Q"), Homecoming.About.Released);
        Assert.Equal(new Sourced<int>(14, "SM p.2"), Homecoming.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "SM p.2"), Homecoming.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "SM p.2"), Homecoming.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "SM p.2"), Homecoming.Components.SchemeTwists);
        Assert.Equal("SM p.2; C1", Homecoming.CatalogSource);
    }

    [Fact]
    public void Spider_Man_Homecoming_has_the_5_Heroes_2_Villain_Groups_2_Masterminds_and_4_Schemes()
    {
        Assert.Equal(
            ["Happy Hogan", "High Tech Spider-Man", "Peter Parker, Homecoming", "Peter's Allies", "Tony Stark"],
            Homecoming.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Salvagers", "Vulture Tech"], Homecoming.VillainGroups.Select(group => group.Name));
        Assert.Empty(Homecoming.HenchmanGroups);
        Assert.Equal(["Adrian Toomes", "Vulture"], Homecoming.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, Homecoming.Schemes.Select(scheme => scheme.Name));
    }

    [Theory]
    [InlineData("Happy Hogan", null, "Instinct Tech", "Coordinate Danger Sense Striker")]
    [InlineData("High Tech Spider-Man", "Spider Friends", "Covert Tech", "Wall-Crawl Danger Sense Coordinate")]
    [InlineData("Peter Parker, Homecoming", "Spider Friends", "Covert Instinct Strength Tech", "Wall-Crawl Danger Sense Coordinate")]
    [InlineData("Peter's Allies", "Spider Friends", "Covert Instinct", "Coordinate")]
    [InlineData("Tony Stark", "Avengers", "Ranged Tech", "Coordinate Danger Sense")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string? team, string classes, string keywords)
    {
        var entry = Homecoming.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, entry.Team is null ? null : TermName(entry.Team));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Only the Scheme's Wound move uses a part; the Bystanders come from the Bystander stack, which every setup has.
    [Fact]
    public void Only_Explosion_at_the_Washington_Monument_uses_a_part()
    {
        ICard[] cards = [.. Homecoming.Heroes, .. Homecoming.VillainGroups, .. Homecoming.Masterminds, .. Homecoming.Schemes];

        Assert.Equal(
            ["spider-man-homecoming_scheme_explosion-at-the-washington-monument: Wounds"],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = Homecoming.Schemes.ToDictionary(scheme => scheme.Name);

        Assert.Equal([8, 8, 9, 7], SchemeNames.Select(name => Assert.Single(schemes[name].Setup.Twists).Value));
        Assert.All(schemes.Values, scheme => Assert.Equal("Card", Assert.Single(scheme.Setup.Twists).Source));

        var distract = Assert.Single(schemes["Distract the Hero"].Setup.HeroCounts!);
        Assert.Equal(("core_term_spider-friends", 1, "Card"), (distract.Team, distract.AtLeast, distract.Source));

        var explosion = schemes["Explosion at the Washington Monument"].Setup;
        Assert.Equal(
            [(CardKind.Bystander, 18), (CardKind.Wound, 14)],
            explosion.Moves!.Select(move => (move.Card, Assert.Single(move.Count).Value)));
        Assert.All(explosion.Moves!, move => Assert.Equal(Pile.SetAside, move.To));

        Assert.Equal("Card", Assert.Single(schemes["Ferry Disaster"].Setup.Steps!).Source);

        var scavenge = Assert.Single(schemes["Scavenge Alien Weaponry"].Setup.ExtraHenchmanGroups!);
        Assert.Equal((null, 1, "Card"), (scavenge.Players, scavenge.Value, scavenge.Source));
        Assert.Equal(new Sourced<int>(10, "Card"), schemes["Scavenge Alien Weaponry"].Setup.ExtraHenchmanCards);
    }

    [Theory]
    [InlineData("Adrian Toomes", "Salvagers")]
    [InlineData("Vulture", "Vulture Tech")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = Homecoming.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((GroupType.Villain, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        Assert.Equal(group, Homecoming.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Spider_Man_Homecoming_glossary_adds_its_three_keywords_and_reuses_Wall_Crawl()
    {
        Assert.Equal(
            ["Danger Sense keyword SM p.1", "Striker keyword SM p.1", "Coordinate keyword SM p.1"],
            Homecoming.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.Contains("paint-the-town-red_term_wall-crawl", Homecoming.Heroes.SelectMany(hero => hero.Terms));
    }

    // Draws

    [Theory]
    [InlineData("Distract the Hero", 1, 8)]
    [InlineData("Explosion at the Washington Monument", 2, 8)]
    [InlineData("Ferry Disaster", 3, 9)]
    [InlineData("Scavenge Alien Weaponry", 5, 7)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, "Vulture");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Contains(EpicStep, setup.Steps);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    public void Distract_the_Hero_puts_a_Spider_Friends_Hero_in_the_Hero_Deck(int players)
    {
        var setup = Draw(players, "Distract the Hero", "Vulture");

        Assert.Contains(setup.Heroes, hero => hero.Team == "core_term_spider-friends");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void Explosion_at_the_Washington_Monument_sets_aside_18_Bystanders_and_14_Wounds_for_the_Floor_decks(int players)
    {
        var setup = Draw(players, "Explosion at the Washington Monument", "Vulture");

        Assert.NotNull(setup.Stacks.Wounds);
        Assert.Equal(
            [(CardKind.Bystander, Pile.SetAside, 18), (CardKind.Wound, Pile.SetAside, 14)],
            setup.Moves.Select(move => (move.Card, move.To, move.Count)));
    }

    [Fact]
    public void Ferry_Disaster_lists_the_Ferry_step()
    {
        var setup = Draw(2, "Ferry Disaster", "Adrian Toomes");

        Assert.Contains("Place the Bystander stack above the Sewers as the Ferry", setup.Steps);
    }

    // In Solo too: the card's 10 Smugglers win over Solo's 3 Henchmen per group.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Scavenge_Alien_Weaponry_adds_an_extra_Henchman_Group_of_10(int players)
    {
        var setup = Draw(players, "Scavenge Alien Weaponry", "Vulture");
        var plain = Draw(players, "Ferry Disaster", "Vulture");

        Assert.Equal(plain.HenchmanGroups.Count + 1, setup.HenchmanGroups.Count);
        Assert.Equal(plain.VillainDeck.HenchmanCards + 10, setup.VillainDeck.HenchmanCards);
    }

    [Theory]
    [InlineData("Adrian Toomes", "Salvagers")]
    [InlineData("Vulture", "Vulture Tech")]
    public void Mastermind_brings_its_Always_Leads_group(string mastermind, string group)
    {
        Assert.Contains(group, Draw(2, "Ferry Disaster", mastermind).VillainGroups.Select(g => g.Name));
    }

    // The exclusion case: with every other box of its ruleset included, Spider-Man Homecoming changes nothing. With
    // twelve boxes each draw is slow, so it tries 10 seeds per player count rather than 40.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Spider_Man_Homecoming_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("spider-man-homecoming.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool", "noir", "x-men"];

        for (var seed = 0; seed < 10; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("spider-man-homecoming_"));
        }
    }

    // Finds the Scheme and Mastermind draws that give this pair, since which Schemes and Masterminds can be drawn
    // depends on the player count. Each player count's Schemes, and each Scheme's Masterminds, are looked up once.
    private static readonly ConcurrentDictionary<(int Players, string Scheme), Dictionary<string, int[]>> Pairs = new();

    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var pairs = Pairs.GetOrAdd((players, scheme), key =>
        {
            var probe = new ScriptedRandom();
            Generator.Generate(key.Players, Boxes, probe);
            var s = Enumerable.Range(0, probe.Options[0])
                .First(s => Assert.IsType<SetupResult>(Generator.Generate(key.Players, Boxes, new ScriptedRandom(s))).Scheme.Name == key.Scheme);
            var masterminds = new ScriptedRandom(s);
            Generator.Generate(key.Players, Boxes, masterminds);
            return Enumerable.Range(0, masterminds.Options[1]).ToDictionary(
                m => Assert.IsType<SetupResult>(Generator.Generate(key.Players, Boxes, new ScriptedRandom(s, m))).Mastermind.Name,
                m => new[] { s, m });
        });

        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(pairs[mastermind])));
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
