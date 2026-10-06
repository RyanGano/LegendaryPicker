using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Deadpool box file (Data/Boxes/deadpool.json), drawn with the core box. Catalog order puts the core box's cards first, so its Schemes follow the core Schemes the player
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
        foreach (var seed in Enumerable.Range(0, 4))
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new SeededRandom(SchemeDraw(2, scheme), seed)));

            Assert.Equal(scheme, setup.Scheme.Name);
            Assert.Contains(setup.Heroes, hero => hero.NameOfHero == "Deadpool");
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

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(SchemeDraw(players, scheme), mastermind)));

    // The Schemes the player count allows come in catalog order, the core box's before Deadpool's.
    private static int SchemeDraw(int players, string scheme) =>
        Core.Schemes.Count(s => s.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true) + Array.IndexOf(SchemeNames, scheme);

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
}
