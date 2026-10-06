using System.Collections.Concurrent;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Spider-Man Homecoming box file (Data/Boxes/spider-man-homecoming.json), drawn with the core box.
public class SpiderManHomecomingTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Homecoming = Catalog.Boxes.Single(box => box.Id == "spider-man-homecoming");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "spider-man-homecoming"];

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
