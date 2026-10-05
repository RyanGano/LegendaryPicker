using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Secret Wars Volume 2 box file (Data/Boxes/secret-wars-volume-2.json), pinned to the rules insert (SW2), the card
// catalog and the card faces it links, and drawn with the core box. Catalog order puts its cards after the core box's,
// so at 2–5 players its Schemes are 8 Deadlands Hordes Charge the Wall to 15 Sinister Ambitions; Solo allows 6 core
// Schemes, so there they are 6 to 13. Its Masterminds are 4 Immortal Emperor Zheng-Zhu, 5 King Hyperion, 6 Shiklah and
// 7 Spider-Queen.
public class SecretWarsVolume2Tests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box SecretWars = Catalog.Boxes.Single(box => box.Id == "secret-wars-volume-2");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "secret-wars-volume-2"];

    private static readonly string[] SchemeNames =
    [
        "Deadlands Hordes Charge the Wall",
        "Enthrone the Barons of Battleworld",
        "The Fountain of Eternal Life",
        "The God-Emperor of Battleworld",
        "The Mark of Khonshu",
        "Master the Mysteries of Kung-Fu",
        "Secret Wars",
        "Sinister Ambitions",
    ];

    private const int Zheng = 4;

    // Catalog

    [Fact]
    public void Secret_Wars_Volume_2_is_an_expansion_with_the_contents_of_the_rules_insert()
    {
        Assert.Equal("Secret Wars Volume 2", SecretWars.Name);
        Assert.False(SecretWars.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, SecretWars.Ruleset);
        Assert.Equal(new Sourced<string>("2015-12", "Q"), SecretWars.About.Released);

        var components = SecretWars.Components;
        Assert.Equal(new Sourced<int>(14, "SW2 p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "SW2 p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "SW2 p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "SW2 p.2"), components.SchemeTwists);
        Assert.Equal(new Sourced<int>(10, "SW2 p.2"), components.Bystanders);
        Assert.Null(components.Sidekicks);
        Assert.Equal(new Sourced<int>(10, "SW2 p.2"), components.Ambitions);
    }

    [Fact]
    public void Secret_Wars_Volume_2_has_the_16_Heroes_6_Villain_Groups_3_Henchman_Groups_4_Masterminds_and_its_8_Schemes()
    {
        Assert.Equal(
            [
                "Agent Venom", "Arkon the Magnificent", "Beast", "Black Swan", "The Captain and the Devil", "Captain Britain", "Corvus Glaive",
                "Dr. Punisher, Soldier Supreme", "Elsa Bloodstone", "Phoenix Force Cyclops", "Ruby Summers", "Shang-Chi", "Silk",
                "Soulsword Colossus", "Spider-Gwen", "Time-Traveling Jean Grey",
            ],
            SecretWars.Heroes.Select(hero => hero.Name));
        Assert.Equal(
            ["Deadpool's Secret Secret Wars", "Guardians of Knowhere", "K'un-Lun", "Monster Metropolis", "Utopolis", "X-Men '92"],
            SecretWars.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Khonshu Guardians", "Magma Men", "Spider-Infected"], SecretWars.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(
            ["Immortal Emperor Zheng-Zhu", "King Hyperion", "Shiklah, the Demon Bride", "Spider-Queen"],
            SecretWars.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, SecretWars.Schemes.Select(scheme => scheme.Name));
    }

    // Each Hero takes its team, classes and keywords from the C1 card index; Arkon is unaffiliated.
    [Theory]
    [InlineData("Agent Venom", "Spider Friends", "Covert Instinct Strength Tech", "Spectrum Wall-Crawl Patrol Multiclass")]
    [InlineData("Arkon the Magnificent", null, "Covert Instinct Ranged Strength", "Spectrum Wall-Crawl Patrol Multiclass")]
    [InlineData("Beast", "Illuminati", "Strength Tech", "Wall-Crawl Patrol Multiclass")]
    [InlineData("Black Swan", "Cabal", "Covert Instinct Ranged", "Multiclass")]
    [InlineData("Ruby Summers", "X-Men", "Ranged Strength", "Teleport Multiclass")]
    [InlineData("Soulsword Colossus", "X-Men", "Covert Instinct Strength", "Cross-Dimensional Rampage Multiclass")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string? team, string classes, string keywords)
    {
        var entry = SecretWars.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, entry.Team is null ? null : TermName(entry.Team));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Wounds: the Cross-Dimensional Rampages (Soulsword Colossus, Monster Metropolis, X-Men '92), Gamora's and Whizzer's
    // Patrol and Escape (Guardians of Knowhere, Utopolis), Rand K'ai's Escape (K'un-Lun), King Hyperion's Escape and
    // Spider-Queen's Master Strike.
    [Fact]
    public void Secret_Wars_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. SecretWars.Heroes, .. SecretWars.VillainGroups, .. SecretWars.HenchmanGroups, .. SecretWars.Masterminds, .. SecretWars.Schemes];
        Assert.Equal(
            [
                "secret-wars-volume-2_hero_soulsword-colossus",
                "secret-wars-volume-2_villain_guardians-of-knowhere",
                "secret-wars-volume-2_villain_k-un-lun",
                "secret-wars-volume-2_villain_monster-metropolis",
                "secret-wars-volume-2_villain_utopolis",
                "secret-wars-volume-2_villain_x-men-92",
                "secret-wars-volume-2_mastermind_king-hyperion",
                "secret-wars-volume-2_mastermind_spider-queen",
                "secret-wars-volume-2_scheme_sinister-ambitions",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => card.Id));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal(("wounds", "Card"), (use.Part.ToString().ToLowerInvariant(), use.Source)));
        // Sinister Ambitions uses the Ambition cards its Setup line moves, so it lists no use of its own.
        Assert.Equal([Part.Ambitions], SecretWars.Schemes.Single(scheme => scheme.Name == "Sinister Ambitions").Parts);
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = SecretWars.Schemes.ToDictionary(scheme => scheme.Name);

        Assert.Equal(["all: 8 Card"], Twists(schemes["Deadlands Hordes Charge the Wall"]));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], schemes["Deadlands Hordes Charge the Wall"].Setup.ExtraVillainGroups);

        Assert.Equal(["all: 8 Card"], Twists(schemes["Enthrone the Barons of Battleworld"]));
        Assert.Equal(["1: 4 Card", "2,3,4,5: 8 Card"], Twists(schemes["The Fountain of Eternal Life"]));
        Assert.Equal(["all: 8 Card"], Twists(schemes["The God-Emperor of Battleworld"]));
        Assert.Equal(["all: 8 Card"], Twists(schemes["Master the Mysteries of Kung-Fu"]));

        var khonshu = schemes["The Mark of Khonshu"];
        Assert.Equal(["all: 10 Card"], Twists(khonshu));
        var required = Assert.Single(khonshu.Setup.RequiredGroups!);
        Assert.Equal(("Khonshu Guardians", GroupType.Henchman, "Card"), (SecretWars.HenchmanGroups.Single(g => g.Id == required.GroupId).Name, required.GroupType, required.Source));
        var extraHero = Assert.Single(khonshu.Setup.OutsideHeroes!);
        Assert.Equal((Pile.VillainDeck, null, null, null), (extraHero.To, extraHero.Hero, extraHero.HeroName, extraHero.Team));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], extraHero.Count);

        var secretWars = schemes["Secret Wars"];
        Assert.Equal(["all: 8 Card"], Twists(secretWars));
        var others = Assert.Single(secretWars.Setup.OutsideMasterminds!);
        Assert.Equal((Pile.SetAside, (Sourced<int>?)null), (others.To, others.Tactics));
        Assert.Equal([new PlayerCountValue(null, 3, "Card")], others.Count);
        Assert.Equal(new Sourced<string>("Twists 1-3", "Card"), others.Joins);

        var ambitions = schemes["Sinister Ambitions"];
        Assert.Equal(["all: 6 Card"], Twists(ambitions));
        var move = Assert.Single(ambitions.Setup.Moves!);
        Assert.Equal((CardKind.Ambition, Pile.VillainDeck, false), (move.Card, move.To, move.PerPlayer));
        Assert.Equal([new PlayerCountValue(null, 10, "Card")], move.Count);

        static IEnumerable<string> Twists(Scheme scheme) =>
            scheme.Setup.Twists.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Immortal Emperor Zheng-Zhu", GroupType.Villain, "K'un-Lun")]
    [InlineData("King Hyperion", GroupType.Villain, "Utopolis")]
    [InlineData("Shiklah, the Demon Bride", GroupType.Villain, "Monster Metropolis")]
    [InlineData("Spider-Queen", GroupType.Henchman, "Spider-Infected")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, GroupType groupType, string group)
    {
        var alwaysLeads = SecretWars.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((groupType, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        var name = groupType == GroupType.Villain
            ? SecretWars.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name
            : SecretWars.HenchmanGroups.Single(g => g.Id == alwaysLeads.GroupId).Name;
        Assert.Equal(group, name);
    }

    [Fact]
    public void Secret_Wars_glossary_adds_its_keywords_from_the_rules_insert()
    {
        Assert.Equal(
            [
                "Spectrum keyword SW2 p.1", "Patrol keyword SW2 p.1", "Circle of Kung-Fu keyword SW2 p.1",
                "Fateful Resurrection keyword SW2 p.1", "Charge keyword SW2 p.1",
            ],
            SecretWars.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.Equal("SW2 p.2; C1", SecretWars.CatalogSource);
    }

    // Draws

    [Theory]
    [InlineData("Deadlands Hordes Charge the Wall", 1, 8)]
    [InlineData("Deadlands Hordes Charge the Wall", 4, 8)]
    [InlineData("Enthrone the Barons of Battleworld", 2, 8)]
    [InlineData("The Fountain of Eternal Life", 1, 4)]
    [InlineData("The Fountain of Eternal Life", 2, 8)]
    [InlineData("The Fountain of Eternal Life", 5, 8)]
    [InlineData("The God-Emperor of Battleworld", 3, 8)]
    [InlineData("The Mark of Khonshu", 2, 10)]
    [InlineData("Master the Mysteries of Kung-Fu", 5, 8)]
    [InlineData("Secret Wars", 2, 8)]
    [InlineData("Sinister Ambitions", 2, 6)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, Zheng);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Immortal Emperor Zheng-Zhu", setup.Mastermind.Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(5, 5)]
    public void Deadlands_Hordes_Charge_the_Wall_adds_a_Villain_Group(int players, int groups)
    {
        var setup = Draw(players, "Deadlands Hordes Charge the Wall", Zheng);

        Assert.Equal(groups, setup.VillainGroups.Count);
        Assert.Contains(new RuleNote("Scheme adds 1 Villain Group", "Card", null, "Secret Wars Volume 2"), setup.Notes);
    }

    [Fact]
    public void The_Mark_of_Khonshu_always_includes_the_Khonshu_Guardians_and_adds_a_Hero_to_the_Villain_Deck()
    {
        var setup = Draw(2, "The Mark of Khonshu", Zheng);

        Assert.Contains("Khonshu Guardians", setup.HenchmanGroups.Select(group => group.Name));
        var extra = Assert.Single(setup.OutsideHeroes);
        Assert.Equal((Pile.VillainDeck, 14), (extra.To, extra.Cards));
        Assert.Equal(14, setup.VillainDeck.OutsideHeroCards);
        Assert.DoesNotContain(extra.Hero, setup.Heroes);
    }

    // The three Masterminds the Scheme adds at its first Twists are drawn from the others and set aside whole.
    [Fact]
    public void Secret_Wars_sets_three_other_Masterminds_aside()
    {
        var setup = Draw(2, "Secret Wars", Zheng);

        var others = setup.OutsideMasterminds!;
        Assert.Equal(3, others.Count);
        Assert.All(others, other => Assert.Equal((Pile.SetAside, (int?)null), (other.To, other.Tactics)));
        Assert.DoesNotContain(setup.Mastermind, others.Select(other => other.Mastermind));
        Assert.Contains(new RuleNote("Scheme draws 3 other Masterminds and sets them aside", "Card", null, "Secret Wars Volume 2"), setup.Notes);
        Assert.All(others, other => Assert.Equal("Twists 1-3", other.Joins));
        Assert.Contains(new RuleNote("Scheme: the Masterminds set aside join on Twists 1-3", "Card", null, "Secret Wars Volume 2"), setup.Notes);
    }

    // The 10 Ambition cards the box holds all go into the Villain Deck, which they add to; they are a supply, not a
    // stack, so no stack is laid out for them.
    [Theory]
    [InlineData(1, 6, 1, 8, 3, 1, 29)]
    [InlineData(2, 6, 5, 16, 10, 2, 49)]
    [InlineData(5, 6, 5, 32, 20, 12, 85)]
    public void Sinister_Ambitions_adds_10_Ambition_cards_to_the_Villain_Deck(
        int players, int twists, int strikes, int villainCards, int henchmanCards, int bystanders, int total)
    {
        var setup = Draw(players, "Sinister Ambitions", Zheng);

        Assert.Equal([new MovedCards(CardKind.Ambition, Pile.Ambitions, Pile.VillainDeck, 10, 10)], setup.Moves);
        Assert.Equal(new VillainDeck(twists, strikes, villainCards, henchmanCards, bystanders, 10), setup.VillainDeck);
        Assert.Equal(total, setup.VillainDeck.Total);
        Assert.Contains(new RuleNote("Scheme moves 10 Ambition cards into the Villain Deck", "Card", null, "Secret Wars Volume 2"), setup.Notes);
    }

    // The other Schemes leave the Ambition cards in the box, and the setup says so.
    [Fact]
    public void A_draw_without_Sinister_Ambitions_has_no_Ambition_cards()
    {
        var setup = Draw(2, "Master the Mysteries of Kung-Fu", Zheng);

        Assert.Empty(setup.Moves);
        Assert.Contains(
            setup.Notes,
            note => note.Text.StartsWith("Leave out the") && note.Text.Contains("Ambition cards"));
    }

    // With both Secret Wars boxes included, their Ambition cards form one supply, and Sinister Ambitions still moves 10.
    [Fact]
    public void Sinister_Ambitions_draws_from_the_Ambition_cards_of_both_Secret_Wars_boxes()
    {
        var both = Assert.IsType<SetupResult>(
            Generator.Generate(2, ["core", "secret-wars-volume-1", "secret-wars-volume-2"], new ScriptedRandom(23, 4)));

        Assert.Equal("Sinister Ambitions", both.Scheme.Name);
        Assert.Equal(new VillainDeck(6, 5, 16, 10, 2, 10), both.VillainDeck);
    }

    // Transform Citizens Into Demons takes any Jean Grey (D-jean-grey): with Dark City and this box, the extra Hero is
    // one of two, and the other can still be in the Hero Deck, since Hero Names stay distinct only where a card says so.
    // Draws: Scheme 14 (8 core Schemes then Dark City's 7th), Dr. Doom, 2 Villain Groups, 5 Heroes, then the extra Hero.
    [Theory]
    [InlineData(0, "Jean Grey")]
    [InlineData(1, "Time-Traveling Jean Grey")]
    public void Transform_Citizens_Into_Demons_can_draw_either_Jean_Grey(int pick, string expected)
    {
        var random = new ScriptedRandom(14, 0, 0, 0, 0, 0, 0, 0, 0, pick);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["core", "dark-city", "secret-wars-volume-2"], random));

        Assert.Equal("Transform Citizens Into Demons", setup.Scheme.Name);
        Assert.Equal(2, random.Options[9]);
        Assert.Equal(expected, Assert.Single(setup.OutsideHeroes).Hero.Name);
        Assert.Contains(
            new RuleNote("Scheme takes any of Jean Grey and Time-Traveling Jean Grey as its Hero", "D-jean-grey", "https://github.com/RyanGano/LegendaryPicker/issues/122", "Dark City"),
            setup.Notes);
    }

    [Fact]
    public void Both_Jean_Greys_can_be_in_one_setup_one_in_the_Hero_Deck_and_one_outside_it()
    {
        string[] boxes = ["core", "dark-city", "secret-wars-volume-2"];
        var heroes = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).SelectMany(box => box.Heroes).ToList();
        var jeanGrey = heroes.FindIndex(hero => hero.Name == "Jean Grey");

        // The first Hero drawn is Dark City's Jean Grey; the one left outside the Hero Deck is Time-Traveling Jean Grey.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, new ScriptedRandom(14, 0, 0, 0, jeanGrey, 0, 0, 0, 0, 0)));

        Assert.Equal("Transform Citizens Into Demons", setup.Scheme.Name);
        Assert.Contains("Jean Grey", setup.Heroes.Select(hero => hero.Name));
        Assert.Equal("Time-Traveling Jean Grey", Assert.Single(setup.OutsideHeroes).Hero.Name);
    }

    // Soulsword Colossus is the 14th Secret Wars Hero, 29th after the core box's 15 in the draw.
    [Fact]
    public void A_Hero_with_a_Rampage_lays_out_the_Wounds()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(13, Zheng, 0, 0, 28)));

        Assert.Equal("Soulsword Colossus", setup.Heroes[0].Name);
        Assert.NotNull(setup.Stacks.Wounds);
    }

    [Theory]
    [InlineData(Zheng, "Immortal Emperor Zheng-Zhu", "K'un-Lun", "villain")]
    [InlineData(5, "King Hyperion", "Utopolis", "villain")]
    [InlineData(6, "Shiklah, the Demon Bride", "Monster Metropolis", "villain")]
    [InlineData(7, "Spider-Queen", "Spider-Infected", "henchman")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group, string kind)
    {
        var setup = Draw(2, "Master the Mysteries of Kung-Fu", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        var names = kind == "villain" ? setup.VillainGroups.Select(g => g.Name) : setup.HenchmanGroups.Select(g => g.Name);
        Assert.Contains(group, names);
    }

    // The exclusion case: with every other box of its ruleset included, Secret Wars Volume 2 changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Secret_Wars_Volume_2_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("secret-wars-volume-2.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("secret-wars-volume-2_"));
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
