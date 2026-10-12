using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Midnight Sons box file (Data/Boxes/midnight-sons.json), drawn with the core box.
public class MidnightSonsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "midnight-sons"];

    // Ritual Sacrifice to Summon Chthon prints 6 Twists plus 1 per player, and 1 Twist total with Lilith.
    [Fact]
    public void Ritual_Sacrifice_prints_1_Twist_with_Lilith_and_6_plus_the_players_otherwise()
    {
        Assert.Equal(1, Draw(3, "Ritual Sacrifice to Summon Chthon", "Lilith, Mother of Demons").VillainDeck.Twists);
        Assert.Equal(9, Draw(3, "Ritual Sacrifice to Summon Chthon", "Zarathos").VillainDeck.Twists);
    }

    // Midnight Massacre puts all 14 cards of a Blade Hero in the Villain Deck, beside the Hero Deck's own Heroes.
    [Fact]
    public void Midnight_Massacre_puts_a_Blade_Heros_14_cards_in_the_Villain_Deck()
    {
        var setup = Draw(2, "Midnight Massacre", "Zarathos");

        Assert.Equal(14, setup.VillainDeck.OutsideHeroCards);
        Assert.Equal(5, setup.Heroes.Count);
    }

    // Draws the Scheme and Mastermind by name: the box's follow the core box's in catalog order.
    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var included = Catalog.Boxes.Where(box => Boxes.Contains(box.Id)).ToList();
        var s = included.SelectMany(box => box.AllSchemes)
            .Where(candidate => candidate.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = included.SelectMany(box => box.AllMasterminds).ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal(mastermind, setup.Mastermind.Name);
        return setup;
    }
}
