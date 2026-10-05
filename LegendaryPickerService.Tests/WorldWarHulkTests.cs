using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The World War Hulk box file (Data/Boxes/world-war-hulk.json), pinned to the rules insert (WWH), the card catalog and the
// card faces it links, and drawn with the core box.
public class WorldWarHulkTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box WorldWarHulk = Catalog.Boxes.Single(box => box.Id == "world-war-hulk");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "world-war-hulk"];

    private static readonly string[] SchemeNames =
    [
        "Break the Planet Asunder",
        "Cytoplasm Spike Invasion",
        "Fall of the Hulks",
        "Gladiator Pits of Sakaar",
        "Mutating Gamma Rays",
        "Shoot Hulk into Space",
        "Subjugate with Obedience Disks",
        "World War Hulk",
    ];

    private static readonly string[] MastermindNames =
        ["General \"Thunderbolt\" Ross", "Illuminati, Secret Society", "King Hulk, Sakaarson", "M.O.D.O.K.", "The Red King", "The Sentry"];

    private const string StartingSideStep = "Start with the side showing Always Leads face up";

    // Catalog

    [Fact]
    public void World_War_Hulk_is_an_expansion_with_the_contents_of_the_insert()
    {
        Assert.Equal("World War Hulk", WorldWarHulk.Name);
        Assert.False(WorldWarHulk.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, WorldWarHulk.Ruleset);
        Assert.Equal(new Sourced<string>("2018-06", "Q"), WorldWarHulk.About.Released);
        Assert.Equal(new Sourced<int>(14, "WWH p.2"), WorldWarHulk.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "WWH p.2"), WorldWarHulk.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "WWH p.2"), WorldWarHulk.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "WWH p.2"), WorldWarHulk.Components.SchemeTwists);
        Assert.Equal(new Sourced<int>(4, "WWH p.2"), WorldWarHulk.Components.Bystanders);
        Assert.Null(WorldWarHulk.Components.Wounds);
        Assert.Equal("WWH p.2; C1", WorldWarHulk.CatalogSource);
    }

    [Fact]
    public void World_War_Hulk_has_the_15_Heroes_7_Villain_Groups_3_Henchman_Groups_6_Masterminds_and_8_Schemes()
    {
        Assert.Equal(
            [
                "Amadeus Cho", "Bruce Banner", "Caiera", "Gladiator Hulk", "Hiroim", "Hulkbuster Iron Man", "Joe Fixit, Grey Hulk", "Korg",
                "Miek, The Unhived", "Namora", "No-Name, Brood Queen", "Rick Jones", "Sentry", "She-Hulk", "Skaar, Son of Hulk",
            ],
            WorldWarHulk.Heroes.Select(hero => hero.Name));
        Assert.Equal(
            ["Aspects of the Void", "Code Red", "Illuminati", "Intelligencia", "Sakaar Imperial Guard", "U-Foes", "Warbound"],
            WorldWarHulk.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Cytoplasm Spikes", "Death's Heads", "Sakaaran Hivelings"], WorldWarHulk.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(MastermindNames, WorldWarHulk.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, WorldWarHulk.Schemes.Select(scheme => scheme.Name));
    }

    [Theory]
    [InlineData("Amadeus Cho", "Champions", "Instinct Strength Tech", "Outwit Transform")]
    [InlineData("Gladiator Hulk", "Warbound", "Instinct Strength", "Smash Transform Cross-Dimensional Rampage Wounded Fury")]
    [InlineData("Joe Fixit, Grey Hulk", "Crime Syndicate", "Covert Instinct Strength", "Smash Transform")]
    [InlineData("Rick Jones", "S.H.I.E.L.D.", "Covert Instinct Ranged Strength Tech", "Transform Smash")]
    [InlineData("Sentry", "Avengers", "Covert Ranged Strength", "Transform Feast")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string team, string classes, string keywords)
    {
        var entry = WorldWarHulk.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Wound effects on the C1-linked faces: a gained Wound or a Cross-Dimensional Rampage, which gains one for each player
    // who can't reveal a Hulk. Wounded Fury only counts the Wounds already in a discard pile, so it brings no part, and the
    // Helicopters and special Bystanders come from the Bystander Stack every setup has.
    [Fact]
    public void World_War_Hulk_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. WorldWarHulk.Heroes, .. WorldWarHulk.VillainGroups, .. WorldWarHulk.HenchmanGroups, .. WorldWarHulk.Masterminds, .. WorldWarHulk.Schemes];

        Assert.Equal(
            [
                "world-war-hulk_hero_gladiator-hulk: Wounds",
                "world-war-hulk_hero_skaar-son-of-hulk: Wounds",
                "world-war-hulk_villain_code-red: Wounds",
                "world-war-hulk_villain_illuminati: Wounds",
                "world-war-hulk_villain_intelligencia: Wounds",
                "world-war-hulk_villain_sakaar-imperial-guard: Wounds",
                "world-war-hulk_villain_u-foes: Wounds",
                "world-war-hulk_villain_warbound: Wounds",
                "world-war-hulk_mastermind_general-thunderbolt-ross: Wounds",
                "world-war-hulk_mastermind_illuminati-secret-society: Wounds",
                "world-war-hulk_mastermind_king-hulk-sakaarson: Wounds",
                "world-war-hulk_mastermind_m-o-d-o-k: Wounds",
                "world-war-hulk_mastermind_the-red-king: Wounds",
                "world-war-hulk_mastermind_the-sentry: Wounds",
                "world-war-hulk_scheme_fall-of-the-hulks: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
        Assert.Null(WorldWarHulk.BystanderUses);
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = WorldWarHulk.Schemes.ToDictionary(scheme => scheme.Name);

        Assert.Equal([9, 10, 10, 6, 7, 8, 11, 9], SchemeNames.Select(name => Assert.Single(schemes[name].Setup.Twists).Value));
        Assert.All(schemes.Values, scheme => Assert.Equal("Card", Assert.Single(scheme.Setup.Twists).Source));
        Assert.All(schemes.Values, scheme => Assert.Null(scheme.ExcludesMasterminds));

        Assert.Equal(7, Assert.Single(schemes["Break the Planet Asunder"].Setup.Heroes!).Value);

        var fall = schemes["Fall of the Hulks"].Setup;
        Assert.Equal(new Sourced<int>(6, "Card"), fall.WoundsPerPlayer);
        Assert.Equal(new HeroCount("Card", Exactly: 2, HeroNameContains: "Hulk"), Assert.Single(fall.HeroCounts!));

        foreach (var name in new[] { "Mutating Gamma Rays", "Shoot Hulk into Space" })
        {
            var outside = Assert.Single(schemes[name].Setup.OutsideHeroes!);
            Assert.Equal((Pile.SetAside, "Hulk", 1), (outside.To, outside.HeroNameContains, Assert.Single(outside.Count).Value));
        }

        var lurking = Assert.Single(schemes["World War Hulk"].Setup.OutsideMasterminds!);
        Assert.Equal((Pile.SetAside, 3, null), (lurking.To, Assert.Single(lurking.Count).Value, lurking.Tactics));
    }

    [Theory]
    [InlineData("General \"Thunderbolt\" Ross", "Code Red")]
    [InlineData("Illuminati, Secret Society", "Illuminati")]
    [InlineData("King Hulk, Sakaarson", "Warbound")]
    [InlineData("M.O.D.O.K.", "Intelligencia")]
    [InlineData("The Red King", "Sakaar Imperial Guard")]
    [InlineData("The Sentry", "Aspects of the Void")]
    public void Mastermind_Always_Leads_the_group_on_its_card_and_starts_on_that_side(string mastermind, string group)
    {
        var entry = WorldWarHulk.Masterminds.Single(m => m.Name == mastermind);

        Assert.Equal((GroupType.Villain, "Card"), (entry.AlwaysLeads.GroupType, entry.AlwaysLeads.Source));
        Assert.Equal(group, WorldWarHulk.VillainGroups.Single(g => g.Id == entry.AlwaysLeads.GroupId).Name);
        Assert.Equal(new SetupStep(StartingSideStep, "WWH p.1"), entry.Setup!.Steps![0]);
    }

    [Fact]
    public void World_War_Hulk_glossary_adds_two_teams_and_four_keywords_and_reuses_Trap_Feast_and_Rampage()
    {
        Assert.Equal(
            [
                "Warbound team WWH p.1", "Crime Syndicate team WWH p.1", "Transform keyword WWH p.1", "Outwit keyword WWH p.1",
                "Smash keyword WWH p.2", "Wounded Fury keyword WWH p.2",
            ],
            WorldWarHulk.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.All(WorldWarHulk.VillainGroups, group => Assert.Contains("x-men_term_trap", group.Terms));
        Assert.Contains("paint-the-town-red_term_feast", WorldWarHulk.HenchmanGroups.SelectMany(group => group.Terms));
        Assert.Contains("secret-wars-volume-1_term_cross-dimensional-rampage", WorldWarHulk.Schemes.SelectMany(scheme => scheme.Terms));
    }

    // Draws

    [Theory]
    [InlineData("Break the Planet Asunder", 1, 9)]
    [InlineData("Cytoplasm Spike Invasion", 2, 10)]
    [InlineData("Fall of the Hulks", 3, 10)]
    [InlineData("Gladiator Pits of Sakaar", 4, 6)]
    [InlineData("Mutating Gamma Rays", 5, 7)]
    [InlineData("Shoot Hulk into Space", 1, 8)]
    [InlineData("Subjugate with Obedience Disks", 2, 11)]
    [InlineData("World War Hulk", 3, 9)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, "M.O.D.O.K.");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Contains(StartingSideStep, setup.Steps);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void Break_the_Planet_Asunder_puts_7_Heroes_in_the_Hero_Deck(int players)
    {
        Assert.Equal(7, Draw(players, "Break the Planet Asunder", "The Red King").Heroes.Count);
    }

    // The 10 Spikes all go to the Infected Deck, so the group is never one of the Villain Deck's Henchman Groups.
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void Cytoplasm_Spike_Invasion_sets_20_Bystanders_and_all_10_Spikes_beside_it(int players)
    {
        var setup = Draw(players, "Cytoplasm Spike Invasion", "King Hulk, Sakaarson");

        var beside = Assert.Single(setup.CardsBeside);
        Assert.Equal(("Cytoplasm Spikes", 10, 0), (beside.Group.Name, beside.Count, beside.FromVillainDeck));
        Assert.DoesNotContain("Cytoplasm Spikes", setup.HenchmanGroups.Select(group => group.Name));
        var moved = Assert.Single(setup.Moves);
        Assert.Equal((CardKind.Bystander, Pile.BesideScheme, 20), (moved.Card, moved.To, moved.Total));
        Assert.Contains("Shuffle the 20 Bystanders and 10 Cytoplasm Spikes into a face-down Infected Deck", setup.Steps);
    }

    [Theory]
    [InlineData(1, 6)]
    [InlineData(2, 12)]
    [InlineData(5, 30)]
    public void Fall_of_the_Hulks_uses_exactly_two_Heroes_with_Hulk_in_their_Hero_Names(int players, int wounds)
    {
        var setup = Draw(players, "Fall of the Hulks", "The Red King");

        Assert.Equal(2, setup.Heroes.Count(hero => hero.NameOfHero.Contains("Hulk")));
        Assert.Equal(wounds, setup.Stacks.Wounds);
        Assert.Contains("Scheme requires exactly 2 Heroes with \"Hulk\" in their Hero Names", setup.Notes.Select(note => note.Text));
    }

    [Theory]
    [InlineData("Mutating Gamma Rays", "Lay the extra Hulk Hero's 14 cards face up as the Mutation Pile")]
    [InlineData("Shoot Hulk into Space", "Shuffle the extra Hulk Hero's 14 cards into a face-down Hulk Deck")]
    public void Scheme_sets_aside_an_extra_Hero_with_Hulk_in_its_Hero_Name(string scheme, string step)
    {
        var setup = Draw(2, scheme, "The Sentry");

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Contains("Hulk", outside.Hero.NameOfHero);
        Assert.Equal((Pile.SetAside, 14), (outside.To, outside.Cards));
        Assert.DoesNotContain(outside.Hero, setup.Heroes);
        Assert.Contains(step, setup.Steps);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void World_War_Hulk_sets_3_other_Masterminds_aside_as_Lurking(int players)
    {
        var setup = Draw(players, "World War Hulk", "M.O.D.O.K.");

        var lurking = setup.OutsideMasterminds!;
        Assert.Equal(3, lurking.Count);
        Assert.All(lurking, other => Assert.Equal((Pile.SetAside, (int?)null), (other.To, other.Tactics)));
        Assert.Equal(4, lurking.Select(other => other.Mastermind.Name).Append(setup.Mastermind.Name).Distinct().Count());
        Assert.Contains("Keep the 3 extra Masterminds out of play as Lurking Masterminds", setup.Steps);
        Assert.Contains("Give each of the 4 Masterminds 2 random Tactics; leave the others out", setup.Steps);
    }

    [Theory]
    [InlineData("General \"Thunderbolt\" Ross", "Code Red", "Stack 8 Bystanders beside General Ross as Helicopter Villains")]
    [InlineData("Illuminati, Secret Society", "Illuminati", null)]
    [InlineData("King Hulk, Sakaarson", "Warbound", null)]
    [InlineData("M.O.D.O.K.", "Intelligencia", null)]
    [InlineData("The Red King", "Sakaar Imperial Guard", null)]
    [InlineData("The Sentry", "Aspects of the Void", "Shuffle 2 Wounds into each player's starting deck before the first draw")]
    public void Mastermind_brings_its_Always_Leads_group_its_Wounds_and_its_steps(string mastermind, string group, string? step)
    {
        var setup = Draw(2, "Gladiator Pits of Sakaar", mastermind);

        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name));
        Assert.NotNull(setup.Stacks.Wounds);
        Assert.Equal(StartingSideStep, setup.Steps[0]);
        if (step is not null)
        {
            Assert.Contains(step, setup.Steps);
        }
    }

    // The exclusion case: with every other box of its ruleset included, World War Hulk changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_World_War_Hulk_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("world-war-hulk.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool", "noir", "x-men", "spider-man-homecoming", "champions"];

        for (var seed = 0; seed < 4; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("world-war-hulk_"));
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
            .Concat(setup.OutsideHeroes.Select(outside => outside.Hero.Id))
            .Concat((setup.OutsideMasterminds ?? []).Select(outside => outside.Mastermind.Id));

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
