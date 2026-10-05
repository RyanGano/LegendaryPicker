using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Deadpool box file (Data/Boxes/deadpool.json), pinned to the rulesheet (DP), the card catalog and the card faces it links,
// and drawn with the core box. Catalog order puts the core box's cards first, so its Schemes follow the core Schemes the player
// count allows and its Masterminds follow the core Masterminds.
public class DeadpoolTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Deadpool = Catalog.Boxes.Single(box => box.Id == "deadpool");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "deadpool"];

    private static readonly string[] SchemeNames =
    [
        "Deadpool Kills the Marvel Universe",
        "Deadpool Wants a Chimichanga",
        "Deadpool Writes a Scheme",
        "Everybody Hates Deadpool",
    ];

    private static readonly Box Core = Catalog.Boxes.Single(box => box.Id == "core");
    private static readonly int EvilDeadpool = Core.Masterminds.Count;
    private static readonly int MachoGomez = EvilDeadpool + 1;

    // Catalog

    [Fact]
    public void Deadpool_is_an_expansion_with_the_contents_of_the_rulesheet()
    {
        Assert.Equal("Deadpool", Deadpool.Name);
        Assert.False(Deadpool.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, Deadpool.Ruleset);
        Assert.Equal(new Sourced<string>("2016-10", "Q"), Deadpool.About.Released);
        Assert.Equal(new Sourced<int>(14, "DP p.2"), Deadpool.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "DP p.2"), Deadpool.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "DP p.2"), Deadpool.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "DP p.2"), Deadpool.Components.SchemeTwists);
    }

    [Fact]
    public void Deadpool_has_the_5_Heroes_2_Villain_Groups_2_Masterminds_and_4_Schemes()
    {
        Assert.Equal(
            ["Bob, Agent of HYDRA", "Deadpool (Mercs for Money)", "Slapstick", "Solo", "Stingray"],
            Deadpool.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Deadpool's Friends", "Evil Deadpool Corpse"], Deadpool.VillainGroups.Select(group => group.Name));
        Assert.Empty(Deadpool.HenchmanGroups);
        Assert.Equal(["Evil Deadpool", "Macho Gomez"], Deadpool.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, Deadpool.Schemes.Select(scheme => scheme.Name));
    }

    // The box's Deadpool shares the core Deadpool's Hero Name but needs a display name of its own.
    [Fact]
    public void The_Deadpool_Hero_has_a_distinguishing_name_and_shares_the_Hero_Name()
    {
        var hero = Deadpool.Heroes.Single(h => h.Name == "Deadpool (Mercs for Money)");

        Assert.Equal("Deadpool", hero.NameOfHero);
        Assert.Equal("Deadpool", Catalog.Boxes.Single(box => box.Id == "core").Heroes.Single(h => h.Name == "Deadpool").NameOfHero);
    }

    [Theory]
    [InlineData("Bob, Agent of HYDRA", "HYDRA", "Covert Tech", "Excessive Violence")]
    [InlineData("Deadpool (Mercs for Money)", "Mercs for Money", "Covert Instinct Strength Tech", "Excessive Violence")]
    [InlineData("Slapstick", "Mercs for Money", "Ranged Strength", "Excessive Violence Rescue a Bystander")]
    [InlineData("Solo", "Mercs for Money", "Instinct Tech", "Excessive Violence")]
    [InlineData("Stingray", "Mercs for Money", "Ranged Tech", "Excessive Violence")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string team, string classes, string keywords)
    {
        var entry = Deadpool.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Wounds: Deadpool's It'll Grow Back, Sluggo's Escape and Wolverinepool's Ambush, Fight and Escape, both Masterminds' Wound
    // effects, and the Wounds three Schemes' Twists give out. Kills the Marvel Universe gives none.
    [Fact]
    public void Deadpool_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. Deadpool.Heroes, .. Deadpool.VillainGroups, .. Deadpool.HenchmanGroups, .. Deadpool.Masterminds, .. Deadpool.Schemes];
        Assert.Equal(
            [
                "deadpool_hero_deadpool-mercs-for-money: Wounds",
                "deadpool_villain_deadpool-s-friends: Wounds",
                "deadpool_villain_evil-deadpool-corpse: Wounds",
                "deadpool_mastermind_evil-deadpool: Wounds",
                "deadpool_mastermind_macho-gomez: Wounds",
                "deadpool_scheme_deadpool-wants-a-chimichanga: Wounds",
                "deadpool_scheme_deadpool-writes-a-scheme: Wounds",
                "deadpool_scheme_everybody-hates-deadpool: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = Deadpool.Schemes.ToDictionary(scheme => scheme.Name);
        var deadpool = new HeroCount("Card", HeroName: "Deadpool", AtLeast: 1);

        var kills = schemes["Deadpool Kills the Marvel Universe"];
        Assert.Equal(["1,2,3: 6 Card", "4,5: 5 Card"], Twists(kills));
        var heroes = Assert.Single(kills.Setup.Heroes!);
        Assert.Equal("2: 4 Card", $"{string.Join(",", heroes.Players!)}: {heroes.Value} {heroes.Source}");
        Assert.Equal([deadpool], kills.Setup.HeroCounts);

        var chimichanga = schemes["Deadpool Wants a Chimichanga"];
        Assert.Equal(["all: 6 Card"], Twists(chimichanga));
        Assert.Equal(new Sourced<int>(12, "Card"), chimichanga.Setup.VillainDeckBystanders);
        var extra = Assert.Single(chimichanga.Setup.ExtraVillainGroups!);
        Assert.Equal("3,4,5: 1 Card", $"{string.Join(",", extra.Players!)}: {extra.Value} {extra.Source}");

        var writes = schemes["Deadpool Writes a Scheme"];
        Assert.Equal(["all: 6 Card"], Twists(writes));
        Assert.Equal([deadpool], writes.Setup.HeroCounts);

        var hates = schemes["Everybody Hates Deadpool"];
        Assert.Equal(["all: 6 Card"], Twists(hates));
        var mercs = Assert.Single(hates.Setup.HeroCounts!);
        Assert.Equal(("Mercs for Money", 1), (TermName(mercs.Team!), mercs.AtLeast));
        // Its Special Rules give every Villain Revenge, so the Revenge note comes with the Scheme even when no Deadpool Villain is drawn.
        Assert.Equal(["Scheme Twist", "Revenge"], hates.Terms.Select(TermName));

        static IEnumerable<string> Twists(Scheme scheme) =>
            scheme.Setup.Twists.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Evil Deadpool", "Evil Deadpool Corpse")]
    [InlineData("Macho Gomez", "Deadpool's Friends")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = Deadpool.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((GroupType.Villain, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        Assert.Equal(group, Deadpool.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Deadpool_glossary_adds_its_teams_and_keywords_from_the_rulesheet()
    {
        Assert.Equal(
            ["Mercs for Money team DP p.1", "HYDRA team DP p.1", "Excessive Violence keyword DP p.1", "Revenge keyword DP p.2"],
            Deadpool.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.Equal("DP p.2; C1", Deadpool.CatalogSource);
    }

    // Draws

    [Theory]
    [InlineData("Deadpool Kills the Marvel Universe", 1, 6)]
    [InlineData("Deadpool Kills the Marvel Universe", 3, 6)]
    [InlineData("Deadpool Kills the Marvel Universe", 4, 5)]
    [InlineData("Deadpool Kills the Marvel Universe", 5, 5)]
    [InlineData("Deadpool Wants a Chimichanga", 2, 6)]
    [InlineData("Deadpool Writes a Scheme", 5, 6)]
    [InlineData("Everybody Hates Deadpool", 3, 6)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, EvilDeadpool);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
    }

    [Fact]
    public void Kills_the_Marvel_Universe_uses_4_Heroes_with_2_players()
    {
        Assert.Equal(4, Draw(2, "Deadpool Kills the Marvel Universe", EvilDeadpool).Heroes.Count);
        Assert.Equal(5, Draw(3, "Deadpool Kills the Marvel Universe", EvilDeadpool).Heroes.Count);
    }

    // Either Deadpool Hero (the core box's or this box's) satisfies the card, so the Hero Deck always holds one.
    [Theory]
    [InlineData("Deadpool Kills the Marvel Universe")]
    [InlineData("Deadpool Writes a Scheme")]
    public void A_Scheme_that_uses_Deadpool_puts_a_Deadpool_Hero_in_the_Hero_Deck(string scheme)
    {
        foreach (var seed in Enumerable.Range(0, 40))
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new SeededRandom(SchemeDraw(2, scheme), seed)));

            Assert.Equal(scheme, setup.Scheme.Name);
            Assert.Contains(setup.Heroes, hero => hero.NameOfHero == "Deadpool");
        }
    }

    [Fact]
    public void Everybody_Hates_Deadpool_puts_a_Mercs_for_Money_Hero_in_the_Hero_Deck()
    {
        foreach (var seed in Enumerable.Range(0, 40))
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new SeededRandom(SchemeDraw(2, "Everybody Hates Deadpool"), seed)));

            Assert.Equal("Everybody Hates Deadpool", setup.Scheme.Name);
            Assert.Contains(setup.Heroes, hero => hero.Team is not null && TermName(hero.Team) == "Mercs for Money");
        }
    }

    // The Villain Deck holds 12 Bystanders in all, in place of the core box's count.
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void Chimichanga_puts_12_Bystanders_in_the_Villain_Deck(int players)
    {
        Assert.Equal(12, Draw(players, "Deadpool Wants a Chimichanga", EvilDeadpool).VillainDeck.Bystanders);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 4)]
    [InlineData(4, 4)]
    [InlineData(5, 5)]
    public void Chimichanga_adds_a_Villain_Group_only_with_3_to_5_players(int players, int groups)
    {
        Assert.Equal(groups, Draw(players, "Deadpool Wants a Chimichanga", EvilDeadpool).VillainGroups.Count);
    }

    [Theory]
    [InlineData(0, "Evil Deadpool", "Evil Deadpool Corpse")]
    [InlineData(1, "Macho Gomez", "Deadpool's Friends")]
    public void Mastermind_brings_its_Always_Leads_group(int offset, string mastermind, string group)
    {
        var setup = Draw(2, "Everybody Hates Deadpool", EvilDeadpool + offset);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name));
    }

    [Fact]
    public void Macho_Gomez_lays_out_the_Wounds_its_Master_Strike_gives()
    {
        Assert.NotNull(Draw(2, "Deadpool Kills the Marvel Universe", MachoGomez).Stacks.Wounds);
    }

    // The exclusion case: with every other box of its ruleset included, Deadpool changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Deadpool_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("deadpool.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("deadpool_"));
        }
    }

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(SchemeDraw(players, scheme), mastermind)));

    // The Schemes the player count allows come in catalog order, the core box's before Deadpool's.
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

    // Scripts the Scheme draw and the first Mastermind draw, then varies every later draw by seed.
    private sealed class SeededRandom(int scheme, int seed) : IRandomSource
    {
        private int _position;

        public int Next(int exclusiveMax) => _position++ switch
        {
            0 => scheme,
            1 => 0,
            var position => (seed * 7 + position * 13) % exclusiveMax,
        };
    }

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
