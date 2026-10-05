using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Civil War box file (Data/Boxes/civil-war.json), pinned to the rules insert (CW), the card catalogs and the card faces
// they link, and drawn with the core box. Catalog order puts its cards before the core box's, so its Schemes are draws 0 to 7
// at every player count and its Masterminds 0 Authoritarian Iron Man, 1 Baron Helmut Zemo, 2 Maria Hill, 3 Misty Knight and
// 4 Ragnarok.
public class CivilWarTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box CivilWar = Catalog.Boxes.Single(box => box.Id == "civil-war");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "civil-war"];

    private static readonly string[] SchemeNames =
    [
        "Avengers vs. X-Men",
        "Dark Reign of H.A.M.M.E.R. Officers",
        "Epic Super Hero Civil War",
        "Imprison Unregistered Superhumans",
        "Nitro the Supervillain Threatens Crowds",
        "Predict Future Crime",
        "Reveal Heroes' Secret Identities",
        "United States Split by Civil War",
    ];

    private const int IronMan = 0;
    private const int MariaHill = 2;
    private const int Ragnarok = 4;

    // Catalog

    // 16 Heroes of 14 cards, a Divided Card counting as one; 7 Villain Groups of 8; 2 Henchman Groups of 10; and the special
    // Bystanders, Grievous Wounds and Sidekicks that join the shared stacks (CW p.2).
    [Fact]
    public void Civil_War_is_an_expansion_with_the_contents_of_the_rules_insert()
    {
        Assert.Equal("Civil War", CivilWar.Name);
        Assert.False(CivilWar.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, CivilWar.Ruleset);
        Assert.Equal(new Sourced<string>("2016-08", "Q"), CivilWar.About.Released);

        var components = CivilWar.Components;
        Assert.Equal(new Sourced<int>(14, "CW p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "CW p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "CW p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "CW p.2"), components.SchemeTwists);
        Assert.Equal(new Sourced<int>(7, "CW p.2"), components.Bystanders);
        Assert.Equal(new Sourced<int>(15, "CW p.2"), components.Wounds);
        Assert.Equal(new Sourced<int>(15, "CW p.2"), components.Sidekicks);
        Assert.Null(components.Officers);
        Assert.Equal("CW p.2; C1; C2", CivilWar.CatalogSource);
    }

    [Fact]
    public void Civil_War_has_the_16_Heroes_7_Villain_Groups_2_Henchman_Groups_5_Masterminds_and_8_Schemes()
    {
        Assert.Equal(
            [
                "Captain America, Secret Avenger", "Cloak & Dagger", "Daredevil (Civil War)", "Falcon", "Goliath", "Hercules", "Hulkling",
                "Luke Cage", "Patriot", "Peter Parker", "Speedball", "Stature", "Storm & Black Panther", "Tigra", "Vision", "Wiccan",
            ],
            CivilWar.Heroes.Select(hero => hero.Name));
        Assert.Equal(
            [
                "CSA Special Marshals", "Great Lakes Avengers", "Heroes for Hire", "Registration Enforcers", "S.H.I.E.L.D. Elite",
                "Superhuman Registration Act", "Thunderbolts",
            ],
            CivilWar.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Cape-Killers", "Mandroid"], CivilWar.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(
            ["Authoritarian Iron Man", "Baron Helmut Zemo", "Maria Hill, Director of S.H.I.E.L.D.", "Misty Knight", "Ragnarok"],
            CivilWar.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, CivilWar.Schemes.Select(scheme => scheme.Name));
    }

    // Each Hero takes the team on the left half of its Divided Cards, the classes its cards show and its keywords, from the card
    // catalogs and the card faces.
    [Theory]
    [InlineData("Captain America, Secret Avenger", "Avengers", "Covert Instinct Ranged Strength Tech", "Divided Card Sidekick")]
    [InlineData("Cloak & Dagger", "Avengers", "Covert Ranged", "Divided Card Phasing")]
    [InlineData("Luke Cage", "Avengers", "Instinct Strength", "Divided Card Fortify")]
    [InlineData("Speedball", "New Warriors", "Covert Ranged", "Divided Card Rescue a Bystander")]
    [InlineData("Storm & Black Panther", "X-Men", "Covert Instinct Ranged Tech", "Divided Card Sidekick")]
    [InlineData("Vision", "Avengers", "Ranged Tech", "Divided Card Size-Changing Phasing")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string team, string classes, string keywords)
    {
        var entry = CivilWar.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Setup uses the Hero Name on a Divided Card's left half (CW p.1): Storm for Storm & Black Panther and Cloak for Cloak &
    // Dagger. This Daredevil is a new version of Dark City's, so it takes a name of its own and keeps the Hero Name (#76).
    [Fact]
    public void A_Hero_whose_left_halves_name_another_character_keeps_that_Hero_Name()
    {
        Assert.Equal(
            ["Cloak & Dagger: Cloak", "Daredevil (Civil War): Daredevil", "Storm & Black Panther: Storm"],
            CivilWar.Heroes.Where(hero => hero.HeroName is not null).Select(hero => $"{hero.Name}: {hero.HeroName}"));
    }

    // Sidekicks: Heroes and the Aspiring Hero Bystander that gain one. Wounds: Heroes, Villains, Masterminds and Avengers vs.
    // X-Men that give them. Officers: S.H.I.E.L.D. Elite, Maria Hill and Dark Reign of H.A.M.M.E.R. Officers.
    [Fact]
    public void Civil_War_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. CivilWar.Heroes, .. CivilWar.VillainGroups, .. CivilWar.HenchmanGroups, .. CivilWar.Masterminds, .. CivilWar.Schemes];
        Assert.Equal(
            [
                "civil-war_hero_captain-america-secret-avenger: Sidekicks",
                "civil-war_hero_daredevil-civil-war: Sidekicks",
                "civil-war_hero_falcon: Sidekicks",
                "civil-war_hero_hulkling: Wounds",
                "civil-war_hero_luke-cage: Wounds",
                "civil-war_hero_peter-parker: Sidekicks",
                "civil-war_hero_storm-and-black-panther: Sidekicks",
                "civil-war_hero_tigra: Sidekicks",
                "civil-war_villain_great-lakes-avengers: Wounds",
                "civil-war_villain_heroes-for-hire: Wounds",
                "civil-war_villain_s-h-i-e-l-d-elite: Wounds Officers",
                "civil-war_villain_superhuman-registration-act: Wounds",
                "civil-war_villain_thunderbolts: Wounds",
                "civil-war_mastermind_baron-helmut-zemo: Wounds",
                "civil-war_mastermind_maria-hill-director-of-s-h-i-e-l-d: Officers",
                "civil-war_mastermind_misty-knight: Wounds",
                "civil-war_scheme_avengers-vs-x-men: Wounds",
                "civil-war_scheme_dark-reign-of-h-a-m-m-e-r-officers: Officers",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
        Assert.Equal([new PartUse(Part.Sidekicks, "Card")], CivilWar.BystanderUses);
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = CivilWar.Schemes.ToDictionary(scheme => scheme.Name);

        var avengersVsXMen = schemes["Avengers vs. X-Men"].Setup;
        Assert.Equal(["all: 9 Card"], Twists(avengersVsXMen));
        Assert.Equal([new PlayerCountValue(null, 6, "Card")], avengersVsXMen.Heroes);
        Assert.Equal([3, 3], avengersVsXMen.TeamSplit!.Value);
        Assert.Equal("Card", avengersVsXMen.TeamSplit.Source);

        Assert.Equal(["all: 7 Card"], Twists(schemes["Dark Reign of H.A.M.M.E.R. Officers"].Setup));

        var epic = schemes["Epic Super Hero Civil War"].Setup;
        Assert.Equal(["1,2,3: 9 Card", "4,5: 6 Card"], Twists(epic));
        Assert.Equal(["1: 4 Card"], Values(epic.Heroes!));

        Assert.Equal(["all: 11 Card"], Twists(schemes["Imprison Unregistered Superhumans"].Setup));
        Assert.Equal(["all: 8 Card"], Twists(schemes["Nitro the Supervillain Threatens Crowds"].Setup));

        var predict = schemes["Predict Future Crime"].Setup;
        Assert.Equal(["all: 6 Card"], Twists(predict));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], predict.ExtraVillainGroups);

        var reveal = schemes["Reveal Heroes' Secret Identities"].Setup;
        Assert.Equal(["all: 6 Card"], Twists(reveal));
        Assert.Equal([new PlayerCountValue(null, 7, "Card")], reveal.Heroes);

        Assert.Equal(["all: 10 Card"], Twists(schemes["United States Split by Civil War"].Setup));

        static IEnumerable<string> Twists(SchemeSetup setup) => Values(setup.Twists);

        static IEnumerable<string> Values(IReadOnlyList<PlayerCountValue> values) =>
            values.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Authoritarian Iron Man", "Superhuman Registration Act")]
    [InlineData("Baron Helmut Zemo", "Thunderbolts")]
    [InlineData("Maria Hill, Director of S.H.I.E.L.D.", "S.H.I.E.L.D. Elite")]
    [InlineData("Misty Knight", "Heroes for Hire")]
    [InlineData("Ragnarok", "Registration Enforcers")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = CivilWar.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((GroupType.Villain, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        Assert.Equal(group, CivilWar.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Civil_War_glossary_adds_its_team_and_keywords_from_the_rules_insert()
    {
        Assert.Equal(
            [
                "New Warriors team CW p.1", "Divided Card keyword CW p.1", "Size-Changing keyword CW p.1", "Phasing keyword CW p.1",
                "Fortify keyword CW p.2", "S.H.I.E.L.D. Clearance keyword CW p.2",
            ],
            CivilWar.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    // Draws

    [Theory]
    [InlineData("Avengers vs. X-Men", 1, 9)]
    [InlineData("Dark Reign of H.A.M.M.E.R. Officers", 2, 7)]
    [InlineData("Epic Super Hero Civil War", 1, 9)]
    [InlineData("Epic Super Hero Civil War", 3, 9)]
    [InlineData("Epic Super Hero Civil War", 4, 6)]
    [InlineData("Epic Super Hero Civil War", 5, 6)]
    [InlineData("Imprison Unregistered Superhumans", 2, 11)]
    [InlineData("Nitro the Supervillain Threatens Crowds", 3, 8)]
    [InlineData("Predict Future Crime", 4, 6)]
    [InlineData("Reveal Heroes' Secret Identities", 5, 6)]
    [InlineData("United States Split by Civil War", 2, 10)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, Ragnarok);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Ragnarok", setup.Mastermind.Name);
        Assert.Equal(twists, setup.VillainDeck.Twists);
    }

    [Theory]
    [InlineData("Epic Super Hero Civil War", 1, 4)]
    [InlineData("Epic Super Hero Civil War", 2, 5)]
    [InlineData("Reveal Heroes' Secret Identities", 1, 7)]
    [InlineData("Reveal Heroes' Secret Identities", 4, 7)]
    [InlineData("Avengers vs. X-Men", 1, 6)]
    [InlineData("Avengers vs. X-Men", 5, 6)]
    public void Scheme_sets_the_Heroes_in_the_Hero_Deck(string scheme, int players, int heroes)
    {
        Assert.Equal(heroes, Draw(players, scheme, Ragnarok).Heroes.Count);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    public void Predict_Future_Crime_adds_a_Villain_Group(int players, int groups)
    {
        var setup = Draw(players, "Predict Future Crime", Ragnarok);

        Assert.Equal(groups, setup.VillainGroups.Count);
        Assert.Contains(new RuleNote("Scheme adds 1 Villain Group", "Card", null, "Civil War"), setup.Notes);
    }

    // Whatever else is drawn, the Hero Deck is 3 Heroes of one team and 3 of another.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Avengers_vs_X_Men_draws_3_Heroes_of_one_team_and_3_of_another(int players)
    {
        for (var seed = 0; seed < 4; seed++)
        {
            var setup = Assert.IsType<SetupResult>(
                Generator.Generate(players, Boxes, new CyclingRandom(0, seed % 5, seed, seed + 3, seed * 7 + 1, seed + 11)));

            Assert.Equal("Avengers vs. X-Men", setup.Scheme.Name);
            Assert.Equal([3, 3], setup.Heroes.GroupBy(hero => hero.Team).Select(team => team.Count()));
            Assert.DoesNotContain(null, setup.Heroes.Select(hero => hero.Team));
            Assert.Contains(
                new RuleNote("Scheme requires 3 Heroes of one team and 3 Heroes of another", "Card", null, "Civil War"), setup.Notes);
            Assert.NotNull(setup.Stacks.Wounds);
        }
    }

    // The team split's search stays fast with every box included, where many teams could take each side (#93). One scripted
    // draw at 5 players. It takes about 0.3 s alone and took about 3 s before #93, so the 1.5 s budget leaves room for a slow
    // runner yet still fails on a return to multi-second draws.
    [Fact]
    public void Avengers_vs_X_Men_draws_quickly_with_every_box()
    {
        var everyBox = Catalog.Boxes.Select(box => box.Id).ToList();
        var schemes = Catalog.Boxes.SelectMany(box => box.Schemes)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(5) ?? true)
            .ToList();
        var random = new ScriptedRandom(schemes.FindIndex(scheme => scheme.Name == "Avengers vs. X-Men"));

        var timer = System.Diagnostics.Stopwatch.StartNew();
        var setup = Assert.IsType<SetupResult>(Generator.Generate(5, everyBox, random));
        timer.Stop();

        Assert.Equal("Avengers vs. X-Men", setup.Scheme.Name);
        Assert.Equal([3, 3], setup.Heroes.GroupBy(hero => hero.Team).Select(team => team.Count()));
        Assert.InRange(timer.ElapsedMilliseconds, 0, 1500);
    }

    // The Officer case: Dark Reign of H.A.M.M.E.R. Officers and Maria Hill make Officers Villains, so they lay the Officers out.
    [Theory]
    [InlineData("Dark Reign of H.A.M.M.E.R. Officers", Ragnarok)]
    [InlineData("Nitro the Supervillain Threatens Crowds", MariaHill)]
    public void A_Scheme_or_Mastermind_that_turns_Officers_into_Villains_lays_out_the_Officers(string scheme, int mastermind)
    {
        Assert.Equal(30, Draw(2, scheme, mastermind).Stacks.Officers);
    }

    [Theory]
    [InlineData(0, "Authoritarian Iron Man", "Superhuman Registration Act")]
    [InlineData(1, "Baron Helmut Zemo", "Thunderbolts")]
    [InlineData(2, "Maria Hill, Director of S.H.I.E.L.D.", "S.H.I.E.L.D. Elite")]
    [InlineData(3, "Misty Knight", "Heroes for Hire")]
    [InlineData(4, "Ragnarok", "Registration Enforcers")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "Nitro the Supervillain Threatens Crowds", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Contains(group, setup.VillainGroups.Select(g => g.Name));
    }

    // Stacks: the 15 Grievous Wounds join the core box's 30 Wounds, making 45 (CW p.2), whenever something uses Wounds.
    [Fact]
    public void The_Grievous_Wounds_join_the_Wound_stack()
    {
        Assert.Equal(45, Draw(2, "Avengers vs. X-Men", IronMan).Stacks.Wounds);
    }

    // Authoritarian Iron Man, Superhuman Registration Act and a Scheme that use no Wounds would leave them out, but the Hero
    // Deck's draw can still bring a Hero that gives Wounds; with Heroes from the core box that give none, the stack stays out.
    [Fact]
    public void Wounds_stay_out_when_nothing_drawn_uses_them()
    {
        // Ragnarok leads Registration Enforcers; the second Villain Group and the Henchman Group are the first left, and the
        // Heroes are core Heroes that give no Wounds, skipping Deadpool.
        var random = new ScriptedRandom(4, Ragnarok, 0, 0, 16, 16, 16, 17, 17);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(["Registration Enforcers", "CSA Special Marshals"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Emma Frost", "Gambit"], setup.Heroes.Select(hero => hero.Name));
        Assert.Null(setup.Stacks.Wounds);
    }

    // The Aspiring Hero Bystanders gain a Sidekick when rescued, and every setup shuffles them in with the other Bystanders,
    // so with Civil War included the Sidekick stack is always laid out: its 15 Pet Avengers, with Secret Wars Volume 1's 15
    // too when that box is included (CW p.2). The 7 special Bystanders join the core box's 30 (and Secret Wars Volume 1's 1),
    // less the 2 in the Villain Deck.
    [Theory]
    [InlineData(new[] { "core", "civil-war" }, 15, 35)]
    [InlineData(new[] { "core", "civil-war", "secret-wars-volume-1" }, 30, 36)]
    public void The_special_Sidekicks_and_Bystanders_join_their_stacks(string[] boxes, int sidekicks, int bystanders)
    {
        // Ragnarok and the first draw of everything else: no drawn card gains a Sidekick.
        var random = new ScriptedRandom(Array.IndexOf(SchemeNames, "United States Split by Civil War"), Ragnarok, 0, 0, 16, 16, 16, 17, 17);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, random));

        Assert.Equal(sidekicks, setup.Stacks.Sidekicks);
        Assert.Equal(bystanders, setup.Stacks.Bystanders);
    }

    // The exclusion case: with every other box of its ruleset included, Civil War changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Civil_War_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("civil-war.json", "x-men.json", "champions.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary"];

        for (var seed = 0; seed < 4; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("civil-war_"));
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

    // A copy of the box directory without some box files, so a catalog loaded from it has never seen those boxes. X-Men's
    // Divided Cards use Civil War's term, so it can't load without Civil War.
    private sealed class DirectoryWithout : IDisposable
    {
        public string Path { get; } = Directory.CreateTempSubdirectory("legendary-without-").FullName;

        public DirectoryWithout(params string[] fileNames)
        {
            foreach (var file in Directory.GetFiles(BoxCatalog.DefaultDirectory, "*.json").Where(file => !fileNames.Contains(System.IO.Path.GetFileName(file))))
            {
                File.Copy(file, System.IO.Path.Combine(Path, System.IO.Path.GetFileName(file)));
            }
        }

        public void Dispose() => Directory.Delete(Path, recursive: true);
    }
}
