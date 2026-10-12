using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Studios' Ant-Man and the Wasp box file (Data/Boxes/marvel-studios-ant-man-and-the-wasp.json), a Revised
// expansion drawn with the What If...? base game (#206).
public class AntManAndTheWaspTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private const string BoxId = "marvel-studios-ant-man-and-the-wasp";

    private static readonly string[] Boxes = ["marvel-studios-what-if", BoxId];

    // Siphon Energy sets the Quantum Realm Villains aside as an extra group; only its Ambush Scheme, one of the group's
    // 8 cards, stays in the Villain Deck (Card).
    [Fact]
    public void Siphon_Energy_keeps_only_the_Quantum_Realm_Ambush_Scheme_in_the_Villain_Deck()
    {
        var setup = Draw(2, "Siphon Energy from the Quantum Realm", "Ghost, Master Thief");

        Assert.Contains("Quantum Realm", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(3, setup.VillainGroups.Count);
        var beside = Assert.Single(setup.CardsBeside);
        Assert.Equal(("Quantum Realm", 7, 7), (beside.Group.Name, beside.Count, beside.FromVillainDeck));
        Assert.Equal(3 * 8 - 7, setup.VillainDeck.VillainCards - beside.FromVillainDeck);
    }

    // Auction Shrink Tech sets aside the 14 cards of an extra Hero that has Size-Changing cards (Card), never one that
    // has none, such as Scott Lang.
    [Fact]
    public void Auction_Shrink_Tech_sets_aside_a_Hero_with_Size_Changing_cards()
    {
        var setup = Draw(2, "Auction Shrink Tech to Highest Bidder", "Darren Cross");

        var extra = Assert.Single(setup.OutsideHeroes);
        Assert.Equal((Pile.SetAside, 14), (extra.To, extra.Cards));
        Assert.Contains(extra.Hero.NamesOfHero.Single(), new[] { "Ant Army", "Ant-Man", "Cassie Lang", "Janet van Dyne", "Wasp" });
        Assert.DoesNotContain(extra.Hero, setup.Heroes);
    }

    // Kang's card adds an extra Villain Group beside Armada of Kang, which leads (Card).
    [Fact]
    public void Kang_adds_an_extra_Villain_Group_to_Armada_of_Kang()
    {
        var setup = Draw(2, "Safeguard Dark Secrets", "Kang, Quantum Conqueror");

        Assert.Equal(3, setup.VillainGroups.Count);
        Assert.Contains("Armada of Kang", setup.VillainGroups.Select(group => group.Name));
    }

    // Draws the Scheme and Mastermind by name, in the order the generator lists them across the two boxes.
    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var included = Catalog.Boxes.Where(box => Boxes.Contains(box.Id)).ToList();
        var s = included.SelectMany(box => box.AllSchemes).ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = included.SelectMany(box => box.AllMasterminds).ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
