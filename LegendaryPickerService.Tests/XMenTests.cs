using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The X-Men box file (Data/Boxes/x-men.json), drawn with the core box and Dark City, whose Jean Grey The Dark Phoenix Saga needs.
public class XMenTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "dark-city", "x-men"];

    [Fact]
    public void The_Dark_Phoenix_Saga_requires_Hellfire_Club_and_puts_a_Jean_Grey_in_the_Villain_Deck()
    {
        var setup = Draw(3, "The Dark Phoenix Saga", "Onslaught");

        Assert.Contains("Hellfire Club", setup.VillainGroups.Select(group => group.Name));
        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Equal(("Jean Grey", Pile.VillainDeck, 14), (outside.Hero.NamesOfHero.Single(), outside.To, outside.Cards));
        Assert.Equal(14, setup.VillainDeck.OutsideHeroCards);
    }

    // The card lets another Henchman Group stand in for the core box's Sentinels, so without the core box the Scheme
    // draws one of the included groups in their place, with its 10 cards, besides its extra group.
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void Without_the_core_box_Mutant_Hunting_Super_Sentinels_uses_another_Henchman_Group(int players)
    {
        string[] boxes = ["villains", "x-men"];
        var schemes = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).SelectMany(box => box.Schemes)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .Select(scheme => scheme.Name)
            .ToList();
        var setup = Assert.IsType<SetupResult>(
            Generator.Generate(players, boxes, new ScriptedRandom(schemes.IndexOf("Mutant-Hunting Super Sentinels"))));

        Assert.Equal(2, setup.HenchmanGroups.Count);
        Assert.DoesNotContain("Sentinel", setup.HenchmanGroups.Select(group => group.Name));
        var standIn = setup.HenchmanGroups[0];
        Assert.Contains(
            new RuleNote($"Scheme uses {standIn.Name} in place of Sentinel: Marvel Legendary First Edition core box isn't included", "Card", null, "X-Men"),
            setup.Notes);
        Assert.Equal(players == 1 ? 13 : 20, setup.VillainDeck.HenchmanCards);
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
}
