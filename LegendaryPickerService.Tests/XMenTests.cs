using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The X-Men box file (Data/Boxes/x-men.json), pinned to the rules insert (XM), the card catalog and the card faces it
// links, and drawn with the core box and Dark City, whose Jean Grey The Dark Phoenix Saga needs.
public class XMenTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box XMen = Catalog.Boxes.Single(box => box.Id == "x-men");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "dark-city", "x-men"];

    private static readonly string[] SchemeNames =
    [
        "Alien Brood Encounters",
        "Anti-Mutant Hatred",
        "The Dark Phoenix Saga",
        "Horror of Horrors",
        "Mutant-Hunting Super Sentinels",
        "Nuclear Armageddon",
        "Televised Deathtraps of Mojoworld",
        "X-Men Danger Room Goes Berserk",
    ];

    private static readonly string[] MastermindNames = ["Arcade", "Dark Phoenix", "Deathbird", "Mojo", "Onslaught", "Shadow King"];

    private const string EpicStep = "Optional: play the Epic side instead; it also brings Horrors into the game";

    // Catalog

    [Fact]
    public void X_Men_is_an_expansion_with_the_contents_of_the_insert()
    {
        Assert.Equal("X-Men", XMen.Name);
        Assert.False(XMen.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, XMen.Ruleset);
        Assert.Equal(new Sourced<string>("2017-06", "Q"), XMen.About.Released);
        Assert.Equal(new Sourced<int>(14, "XM p.2"), XMen.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "XM p.2"), XMen.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "XM p.2"), XMen.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(1, "XM p.2"), XMen.Components.SchemeTwists);
        Assert.Equal(new Sourced<int>(9, "XM p.2"), XMen.Components.Bystanders);
        Assert.Equal(new Sourced<int>(20, "XM p.2"), XMen.Components.Horrors);
        Assert.Null(XMen.Components.Wounds);
    }

    [Fact]
    public void X_Men_has_the_15_Heroes_7_Villain_Groups_5_Henchman_Groups_6_Masterminds_and_8_Schemes()
    {
        Assert.Equal(
            [
                "Aurora & Northstar", "Banshee", "Beast (X-Men)", "Cannonball", "Colossus & Wolverine", "Dazzler", "Havok", "Jubilee",
                "Kitty Pryde", "Legion", "Longshot", "Phoenix", "Polaris", "Psylocke", "X-23",
            ],
            XMen.Heroes.Select(hero => hero.Name));
        Assert.Equal(
            ["Dark Descendants", "Hellfire Club", "Mojoverse", "Murderworld", "Shadow-X", "Shi'ar Imperial Guard", "Sisterhood of Mutants"],
            XMen.VillainGroups.Select(group => group.Name));
        Assert.Equal(
            ["The Brood", "Hellfire Cult", "Sapien League", "Shi'ar Death Commandos", "Shi'ar Patrol Craft"],
            XMen.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(MastermindNames, XMen.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, XMen.Schemes.Select(scheme => scheme.Name));
    }

    // Secret Wars Volume 2 has a Beast too, so this one carries its set in its name and keeps the Hero Name Beast (#76).
    [Fact]
    public void Every_Hero_is_an_X_Man_and_Beast_is_told_apart_from_the_other_Beast()
    {
        Assert.All(XMen.Heroes, hero => Assert.Equal("core_term_x-men", hero.Team));
        Assert.Equal("Beast", XMen.Heroes.Single(hero => hero.Name == "Beast (X-Men)").NameOfHero);
    }

    [Theory]
    [InlineData("Aurora & Northstar", "Covert Instinct Ranged Strength", "Divided Card Berserk Soaring Flight Lightshow")]
    [InlineData("Banshee", "Covert Ranged", "X-Gene Piercing Energy Soaring Flight")]
    [InlineData("Legion", "Covert Instinct Ranged Strength Tech", "Divided Card Piercing Energy Berserk Soaring Flight Lightshow")]
    [InlineData("Phoenix", "Covert Ranged Strength", "Piercing Energy Berserk Soaring Flight")]
    [InlineData("X-23", "Covert Instinct Tech", "X-Gene Berserk")]
    public void Hero_lists_its_classes_and_keywords(string hero, string classes, string keywords)
    {
        var entry = XMen.Heroes.Single(h => h.Name == hero);

        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Wound effects on the C1-linked faces, and Horror of Horrors' Horrors. Human Shields come from the Bystander Stack,
    // which every setup has, and the special Bystanders become Heroes, so neither brings a part.
    [Fact]
    public void X_Men_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. XMen.Heroes, .. XMen.VillainGroups, .. XMen.HenchmanGroups, .. XMen.Masterminds, .. XMen.Schemes];
        Assert.Equal(
            [
                "x-men_hero_colossus-and-wolverine: Wounds",
                "x-men_villain_hellfire-club: Wounds",
                "x-men_villain_mojoverse: Wounds",
                "x-men_villain_murderworld: Wounds",
                "x-men_villain_shi-ar-imperial-guard: Wounds",
                "x-men_villain_sisterhood-of-mutants: Wounds",
                "x-men_mastermind_arcade: Wounds",
                "x-men_mastermind_deathbird: Wounds",
                "x-men_mastermind_mojo: Wounds",
                "x-men_scheme_alien-brood-encounters: Wounds",
                "x-men_scheme_anti-mutant-hatred: Wounds",
                "x-men_scheme_horror-of-horrors: Horrors",
                "x-men_scheme_televised-deathtraps-of-mojoworld: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
        Assert.Null(XMen.BystanderUses);
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = XMen.Schemes.ToDictionary(scheme => scheme.Name);
        Assert.Equal(
            [8, 11, 10, 6, 9, 5, 11, 8],
            SchemeNames.Select(name => Assert.Single(schemes[name].Setup.Twists).Value));
        Assert.All(XMen.Schemes, scheme => Assert.Equal("Card", Assert.Single(scheme.Setup.Twists).Source));

        var brood = schemes["Alien Brood Encounters"].Setup;
        Assert.Equal(new Sourced<int>(0, "Card"), brood.VillainDeckBystanders);
        Assert.Equal([new RequiredGroup("x-men_henchman_the-brood", GroupType.Henchman, "Card", 10)], brood.RequiredGroups);
        Assert.Equal(1, Assert.Single(brood.ExtraHenchmanGroups!).Value);

        Assert.Equal(new Sourced<int>(30, "Card"), schemes["Anti-Mutant Hatred"].Setup.Wounds);

        var saga = schemes["The Dark Phoenix Saga"].Setup;
        Assert.Equal([new RequiredGroup("x-men_villain_hellfire-club", GroupType.Villain, "Card")], saga.RequiredGroups);
        var jeanGrey = Assert.Single(saga.OutsideHeroes!);
        Assert.Equal((Pile.VillainDeck, 1), (jeanGrey.To, Assert.Single(jeanGrey.Count).Value));
        Assert.Equal(["Jean Grey", "Time-Traveling Jean Grey"], jeanGrey.HeroNames!.Value);

        var sentinels = schemes["Mutant-Hunting Super Sentinels"].Setup;
        Assert.Equal([new RequiredGroup("core_henchman_sentinel", GroupType.Henchman, "Card", 10)], sentinels.RequiredGroups);
        Assert.Equal(1, Assert.Single(sentinels.ExtraHenchmanGroups!).Value);

        Assert.Equal(new Sourced<int>(6, "Card"), schemes["Televised Deathtraps of Mojoworld"].Setup.WoundsPerPlayer);
    }

    [Theory]
    [InlineData("Arcade", "Murderworld")]
    [InlineData("Dark Phoenix", "Hellfire Club")]
    [InlineData("Deathbird", "Shi'ar Imperial Guard")]
    [InlineData("Mojo", "Mojoverse")]
    [InlineData("Onslaught", "Dark Descendants")]
    [InlineData("Shadow King", "Shadow-X")]
    public void Mastermind_Always_Leads_the_group_on_its_card_and_offers_its_Epic_side(string mastermind, string group)
    {
        var entry = XMen.Masterminds.Single(m => m.Name == mastermind);

        Assert.Equal((GroupType.Villain, "Card"), (entry.AlwaysLeads.GroupType, entry.AlwaysLeads.Source));
        Assert.Equal(group, XMen.VillainGroups.Single(g => g.Id == entry.AlwaysLeads.GroupId).Name);
        Assert.Equal([new SetupStep(EpicStep, "XM p.2")], entry.Setup!.Steps);
    }

    [Fact]
    public void Deathbird_also_leads_a_Shi_ar_Henchman_Group()
    {
        var also = XMen.Masterminds.Single(m => m.Name == "Deathbird").AlsoLeads!;

        Assert.Equal(["x-men_henchman_shi-ar-death-commandos", "x-men_henchman_shi-ar-patrol-craft"], also.GroupIds);
        Assert.Equal((GroupType.Henchman, "Card"), (also.GroupType, also.Source));
        Assert.All(XMen.Masterminds.Where(m => m.Name != "Deathbird"), m => Assert.Null(m.AlsoLeads));
    }

    [Fact]
    public void X_Men_glossary_adds_its_keywords_from_the_insert()
    {
        Assert.Equal(
            [
                "X-Gene XM p.1", "Piercing Energy XM p.1", "Berserk XM p.1", "Soaring Flight XM p.1", "Lightshow XM p.1",
                "Dominate XM p.1", "Human Shields XM p.1", "Trap XM p.1", "Horror XM p.2",
            ],
            XMen.Glossary.Select(term => $"{term.Name} {term.Source} p.{term.Page}"));
        Assert.All(XMen.Glossary, term => Assert.Equal(TermKind.Keyword, term.Kind));
        Assert.Equal("XM p.2; C1", XMen.CatalogSource);
    }

    // Draws

    [Theory]
    [InlineData("Alien Brood Encounters", 2, 8)]
    [InlineData("Anti-Mutant Hatred", 3, 11)]
    [InlineData("The Dark Phoenix Saga", 4, 10)]
    [InlineData("Horror of Horrors", 1, 6)]
    [InlineData("Mutant-Hunting Super Sentinels", 5, 9)]
    [InlineData("Nuclear Armageddon", 2, 5)]
    [InlineData("Televised Deathtraps of Mojoworld", 3, 11)]
    [InlineData("X-Men Danger Room Goes Berserk", 1, 8)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, "Onslaught");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Contains(EpicStep, setup.Steps);
    }

    // The Brood are an extra Henchman Group, all 10 of them even in Solo, and no Bystanders go in the Villain Deck.
    [Theory]
    [InlineData(1, 13)]
    [InlineData(2, 20)]
    [InlineData(5, 30)]
    public void Alien_Brood_Encounters_adds_all_10_Brood_and_no_Bystanders(int players, int henchmen)
    {
        var setup = Draw(players, "Alien Brood Encounters", "Onslaught");
        var plain = Draw(players, "Nuclear Armageddon", "Onslaught");

        Assert.Contains("The Brood", setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(plain.HenchmanGroups.Count + 1, setup.HenchmanGroups.Count);
        Assert.Equal(henchmen, setup.VillainDeck.HenchmanCards);
        Assert.Equal(0, setup.VillainDeck.Bystanders);
        Assert.NotNull(setup.Stacks.Wounds);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void Anti_Mutant_Hatred_sets_the_Wound_stack_to_30(int players)
    {
        var setup = Draw(players, "Anti-Mutant Hatred", "Onslaught");

        Assert.Equal(30, setup.Stacks.Wounds);
        Assert.Contains(setup.Notes, note => note.Text == "Scheme sets the Wound stack to 30" && note.Citation == "Card");
    }

    [Theory]
    [InlineData(2, 12)]
    [InlineData(4, 24)]
    public void Televised_Deathtraps_of_Mojoworld_sets_6_Wounds_per_player(int players, int wounds)
    {
        Assert.Equal(wounds, Draw(players, "Televised Deathtraps of Mojoworld", "Onslaught").Stacks.Wounds);
    }

    [Fact]
    public void The_Dark_Phoenix_Saga_requires_Hellfire_Club_and_puts_a_Jean_Grey_in_the_Villain_Deck()
    {
        var setup = Draw(3, "The Dark Phoenix Saga", "Onslaught");

        Assert.Contains("Hellfire Club", setup.VillainGroups.Select(group => group.Name));
        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal(("Jean Grey", Pile.VillainDeck, 14), (outside.Hero.NameOfHero, outside.To, outside.Cards));
        Assert.Equal(14, setup.VillainDeck.OutsideHeroCards);
    }

    // No Jean Grey Hero is in the core box or X-Men, so without Dark City the Scheme can't be set up.
    [Fact]
    public void The_Dark_Phoenix_Saga_is_dropped_without_a_Jean_Grey_Hero()
    {
        var random = new ScriptedRandom();
        Generator.Generate(2, ["core", "x-men"], random);

        Assert.Equal(8 + SchemeNames.Length - 1, random.Options[0]);
    }

    [Fact]
    public void Horror_of_Horrors_lays_out_the_20_Horrors()
    {
        Assert.Equal(20, Draw(2, "Horror of Horrors", "Onslaught").Stacks.Horrors);
        Assert.Null(Draw(2, "Nuclear Armageddon", "Onslaught").Stacks.Horrors);
    }

    [Theory]
    [InlineData(1, 13)]
    [InlineData(3, 20)]
    public void Mutant_Hunting_Super_Sentinels_adds_10_Sentinels_as_an_extra_Henchman_Group(int players, int henchmen)
    {
        var setup = Draw(players, "Mutant-Hunting Super Sentinels", "Onslaught");

        Assert.Contains("Sentinel", setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(2, setup.HenchmanGroups.Count);
        Assert.Equal(henchmen, setup.VillainDeck.HenchmanCards);
    }

    [Theory]
    [InlineData("Arcade", "Murderworld")]
    [InlineData("Dark Phoenix", "Hellfire Club")]
    [InlineData("Mojo", "Mojoverse")]
    [InlineData("Onslaught", "Dark Descendants")]
    [InlineData("Shadow King", "Shadow-X")]
    public void Mastermind_brings_its_Always_Leads_group(string mastermind, string group)
    {
        Assert.Contains(group, Draw(2, "Nuclear Armageddon", mastermind).VillainGroups.Select(g => g.Name));
    }

    [Fact]
    public void Deathbird_brings_the_Shi_ar_Imperial_Guard_and_a_Shi_ar_Henchman_Group()
    {
        var setup = Draw(2, "Nuclear Armageddon", "Deathbird");

        Assert.Contains("Shi'ar Imperial Guard", setup.VillainGroups.Select(g => g.Name));
        var henchmen = Assert.Single(setup.HenchmanGroups);
        Assert.Contains(henchmen.Name, new[] { "Shi'ar Death Commandos", "Shi'ar Patrol Craft" });
        Assert.NotNull(setup.Stacks.Wounds);
    }

    // The exclusion case: with every other box of its ruleset included, X-Men changes nothing. With eleven boxes each draw
    // is slow, so it tries 10 seeds per player count rather than 40.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_X_Men_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("x-men.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool", "noir"];

        for (var seed = 0; seed < 10; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("x-men_"));
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
