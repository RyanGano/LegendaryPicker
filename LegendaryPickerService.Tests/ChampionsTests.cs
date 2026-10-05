using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Champions box file (Data/Boxes/champions.json), pinned to the rules insert (CH), the card catalog and the card
// faces it links, and drawn with the core box.
public class ChampionsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Champions = Catalog.Boxes.Single(box => box.Id == "champions");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "champions"];

    private static readonly string[] SchemeNames =
    [
        "Clash of the Monsters Unleashed",
        "Divide and Conquer",
        "Hypnotize Every Human",
        "Steal All Oxygen on Earth",
    ];

    private const string EpicStep = "Optional: play the Epic side instead; the Tactics stay the same";

    // Catalog

    [Fact]
    public void Champions_is_an_expansion_with_the_contents_of_the_insert()
    {
        Assert.Equal("Champions", Champions.Name);
        Assert.False(Champions.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, Champions.Ruleset);
        Assert.Equal(new Sourced<string>("2018-02", "Q"), Champions.About.Released);
        Assert.Equal(new Sourced<int>(14, "CH p.2"), Champions.Components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "CH p.2"), Champions.Components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(0, "CH p.2"), Champions.Components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "CH p.2"), Champions.Components.SchemeTwists);
        Assert.Equal("CH p.2; C1", Champions.CatalogSource);
    }

    [Fact]
    public void Champions_has_the_5_Heroes_2_Villain_Groups_2_Masterminds_and_4_Schemes()
    {
        Assert.Equal(
            ["Gwenpool", "Ms. Marvel", "Nova", "Totally Awesome Hulk", "Viv Vision"],
            Champions.Heroes.Select(hero => hero.Name));
        Assert.Equal(["Monsters Unleashed", "Wrecking Crew"], Champions.VillainGroups.Select(group => group.Name));
        Assert.Empty(Champions.HenchmanGroups);
        Assert.Equal(["Fin Fang Foom", "Pagliacci"], Champions.Masterminds.Select(mastermind => mastermind.Name));
        Assert.Equal(SchemeNames, Champions.Schemes.Select(scheme => scheme.Name));
    }

    [Theory]
    [InlineData("Gwenpool", "Covert Instinct", "Versatile Size-Changing Cheering Crowds Demolish")]
    [InlineData("Ms. Marvel", "Covert Strength", "Size-Changing Versatile Cheering Crowds")]
    [InlineData("Nova", "Ranged Strength", "Versatile Cheering Crowds Size-Changing")]
    [InlineData("Totally Awesome Hulk", "Strength Tech", "Size-Changing Cheering Crowds")]
    [InlineData("Viv Vision", "Ranged Tech", "Size-Changing Versatile Cheering Crowds")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string classes, string keywords)
    {
        var entry = Champions.Heroes.Single(h => h.Name == hero);

        Assert.Equal("Champions", TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Cheering Crowds returns Bystanders to the Bystander Stack, which every setup has. Wounds are the only other part:
    // Totally Awesome Hulk's Growing Pains, Fin Fang Foom's Multipronged Assault and Pagliacci's Commedia Dell'Morte
    // each gain one, and Clash's Wounds come from its Setup line.
    [Fact]
    public void Only_Wound_cards_use_a_part()
    {
        ICard[] cards = [.. Champions.Heroes, .. Champions.VillainGroups, .. Champions.Masterminds, .. Champions.Schemes];

        Assert.Equal(
            [
                "champions_hero_totally-awesome-hulk: Wounds",
                "champions_mastermind_fin-fang-foom: Wounds",
                "champions_mastermind_pagliacci: Wounds",
                "champions_scheme_clash-of-the-monsters-unleashed: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card.Parts)}"));
    }

    // Steal All Oxygen on Earth uses no part, so the Wound Stack in these draws comes from the Mastermind's Tactics.
    [Theory]
    [InlineData("Fin Fang Foom")]
    [InlineData("Pagliacci")]
    public void Mastermind_lays_out_the_Wound_Stack_its_Tactics_use(string mastermind)
    {
        Assert.NotNull(Draw(2, "Steal All Oxygen on Earth", mastermind).Stacks.Wounds);
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = Champions.Schemes.ToDictionary(scheme => scheme.Name);

        Assert.Equal([10, 8, 8, 8], SchemeNames.Select(name => Assert.Single(schemes[name].Setup.Twists).Value));
        Assert.All(schemes.Values, scheme => Assert.Equal("Card", Assert.Single(scheme.Setup.Twists).Source));

        var clash = schemes["Clash of the Monsters Unleashed"].Setup;
        Assert.Equal(new Sourced<int>(6, "Card"), clash.WoundsPerPlayer);
        var beside = Assert.Single(clash.CardsBeside!);
        Assert.Equal(
            ("champions_villain_monsters-unleashed", GroupType.Villain, 8),
            (beside.GroupId, beside.GroupType, Assert.Single(beside.Count).Value));

        var divide = Assert.Single(schemes["Divide and Conquer"].Setup.Heroes!);
        Assert.Equal((null, 7, "Card"), (divide.Players, divide.Value, divide.Source));

        var hypnotize = schemes["Hypnotize Every Human"].Setup;
        Assert.Equal(new Sourced<int>(0, "Card"), hypnotize.VillainDeckBystanders);
        Assert.Equal(1, Assert.Single(hypnotize.ExtraHenchmanGroups!).Value);

        Assert.Equal("Card", Assert.Single(schemes["Steal All Oxygen on Earth"].Setup.Steps!).Source);
    }

    [Theory]
    [InlineData("Fin Fang Foom", "Monsters Unleashed")]
    [InlineData("Pagliacci", "Wrecking Crew")]
    public void Mastermind_Always_Leads_the_group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = Champions.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal((GroupType.Villain, "Card"), (alwaysLeads.GroupType, alwaysLeads.Source));
        Assert.Equal(group, Champions.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Champions_glossary_adds_its_team_and_three_keywords_and_reuses_Size_Changing()
    {
        Assert.Equal(
            ["Champions team CH p.1", "Cheering Crowds keyword CH p.1", "Versatile keyword CH p.1", "Demolish keyword CH p.2"],
            Champions.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
        Assert.Contains("civil-war_term_size-changing", Champions.Heroes.SelectMany(hero => hero.Terms));
    }

    // Draws

    [Theory]
    [InlineData("Clash of the Monsters Unleashed", 1, 10)]
    [InlineData("Divide and Conquer", 2, 8)]
    [InlineData("Hypnotize Every Human", 3, 8)]
    [InlineData("Steal All Oxygen on Earth", 5, 8)]
    public void Scheme_puts_the_Twists_of_its_card_in_the_Villain_Deck(string scheme, int players, int twists)
    {
        var setup = Draw(players, scheme, "Pagliacci");

        Assert.Equal(twists, setup.VillainDeck.Twists);
        Assert.Contains(EpicStep, setup.Steps);
    }

    // In Solo too: the card's 6 Wounds per player and its 8 Monsters Unleashed Villains stand.
    [Theory]
    [InlineData(1, 6)]
    [InlineData(3, 18)]
    [InlineData(5, 30)]
    public void Clash_of_the_Monsters_Unleashed_sets_the_Wounds_and_the_Monster_Pit_aside(int players, int wounds)
    {
        var setup = Draw(players, "Clash of the Monsters Unleashed", "Pagliacci");

        Assert.Equal(wounds, setup.Stacks.Wounds);
        var beside = Assert.Single(setup.CardsBeside);
        Assert.Equal(("Monsters Unleashed", 8, 0), (beside.Group.Name, beside.Count, beside.FromVillainDeck));
        Assert.DoesNotContain("Monsters Unleashed", setup.VillainGroups.Select(group => group.Name));
        Assert.Contains("Shuffle the 8 Monsters Unleashed Villains into a face-down Monster Pit deck", setup.Steps);
    }

    // Fin Fang Foom always leads the group Clash sets entirely aside, so no legal draw pairs them.
    [Fact]
    public void Clash_of_the_Monsters_Unleashed_is_never_drawn_with_Fin_Fang_Foom()
    {
        var probe = new ScriptedRandom();
        Generator.Generate(2, Boxes, probe);
        var clash = Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(s))).Scheme.Name == "Clash of the Monsters Unleashed");
        var masterminds = new ScriptedRandom(clash);
        Generator.Generate(2, Boxes, masterminds);

        var drawn = Enumerable.Range(0, masterminds.Options[1])
            .Select(m => Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(clash, m))).Mastermind.Name)
            .ToList();

        Assert.Contains("Pagliacci", drawn);
        Assert.DoesNotContain("Fin Fang Foom", drawn);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Divide_and_Conquer_puts_7_Heroes_in_the_Hero_Deck_at_every_player_count(int players)
    {
        var setup = Draw(players, "Divide and Conquer", "Fin Fang Foom");

        Assert.Equal(7, setup.Heroes.Count);
        Assert.Contains("Sort the Hero Deck into five smaller decks by Hero Class, under the HQ spaces", setup.Steps);
    }

    // In Solo too: the Henchman Group is an extra one, so the card's group never goes missing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Hypnotize_Every_Human_adds_a_Henchman_Group_and_leaves_Bystanders_out_of_the_Villain_Deck(int players)
    {
        var setup = Draw(players, "Hypnotize Every Human", "Pagliacci");
        var plain = Draw(players, "Steal All Oxygen on Earth", "Pagliacci");

        Assert.Equal(plain.HenchmanGroups.Count + 1, setup.HenchmanGroups.Count);
        Assert.Equal(0, setup.VillainDeck.Bystanders);
    }

    [Fact]
    public void Steal_All_Oxygen_on_Earth_lists_the_Oxygen_Level_step()
    {
        var setup = Draw(2, "Steal All Oxygen on Earth", "Fin Fang Foom");

        Assert.Contains("Start the Oxygen Level at 8", setup.Steps);
    }

    [Theory]
    [InlineData("Fin Fang Foom", "Monsters Unleashed")]
    [InlineData("Pagliacci", "Wrecking Crew")]
    public void Mastermind_brings_its_Always_Leads_group(string mastermind, string group)
    {
        Assert.Contains(group, Draw(2, "Steal All Oxygen on Earth", mastermind).VillainGroups.Select(g => g.Name));
    }

    // The exclusion case: with every other box of its ruleset included, Champions changes nothing. With
    // thirteen boxes each draw is slow, so it tries 10 seeds per player count rather than 40.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Champions_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("champions.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool", "noir", "x-men", "spider-man-homecoming"];

        for (var seed = 0; seed < 10; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("champions_"));
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
