using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Captain America 75th Anniversary box file (Data/Boxes/captain-america-75th-anniversary.json), drawn with the core box. Catalog order puts its cards before the
// core box's, so its Schemes are draws 0 to 3 at every player count and its Masterminds 0 Arnim Zola and 1 Baron Heinrich Zemo.
public class CaptainAmerica75Tests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "captain-america-75th-anniversary"];

    private static readonly string[] SchemeNames =
    [
        "Brainwash the Military",
        "Change the Outcome of WWII",
        "Go Back in Time to Slay Heroes' Ancestors",
        "The Unbreakable Enigma Code",
    ];

    private const int Zola = 0;
    private const int Zemo = 1;

    // The Officer-stack case: the 12 Officers the Scheme moves into the Villain Deck leave 18 of the core box's 30.
    [Fact]
    public void Brainwash_the_Military_moves_12_Officers_into_the_Villain_Deck()
    {
        var setup = Draw(2, "Brainwash the Military", Zola);

        Assert.Equal([new MovedCards(CardKind.Officer, Pile.Officers, Pile.VillainDeck, 12, 12)], setup.Moves);
        Assert.Equal(18, setup.Stacks.Officers);
        Assert.Contains(new RuleNote("Scheme moves 12 S.H.I.E.L.D. Officers into the Villain Deck", "Card", null, "Captain America 75th Anniversary"), setup.Notes);
    }

    // A Mastermind whose Master Strike gives Wounds lays the Wound stack out.
    [Fact]
    public void A_Mastermind_that_gives_Wounds_lays_out_the_Wounds()
    {
        Assert.NotNull(Draw(2, "The Unbreakable Enigma Code", Zemo).Stacks.Wounds);
    }

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(Array.IndexOf(SchemeNames, scheme), mastermind)));
}
