using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Noir box file (Data/Boxes/noir.json), drawn with the core box. Catalog order puts the core box's cards first, so its Schemes follow the core Schemes the
// player count allows and its Masterminds follow the core Masterminds.
public class NoirTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Noir = Catalog.Boxes.Single(box => box.Id == "noir");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "noir"];

    private static readonly string[] SchemeNames =
    [
        "Find the Split Personality Killer",
        "Silence the Witnesses",
        "Five Families of Crime",
        "Hidden Heart of Darkness",
    ];

    private static readonly Box Core = Catalog.Boxes.Single(box => box.Id == "core");
    private static readonly int CharlesXavier = Core.Masterminds.Count;
    private static readonly int Goblin = CharlesXavier + 1;

    // The Mastermind's four Tactics go into the Villain Deck as Villains, so the deck's total includes them.
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void Hidden_Heart_of_Darkness_shuffles_the_4_Tactics_of_its_Mastermind_into_the_Villain_Deck(int players)
    {
        var setup = Draw(players, "Hidden Heart of Darkness", Goblin);
        var plain = Draw(players, "Find the Split Personality Killer", Goblin);

        Assert.Equal(4, setup.VillainDeck.OwnTactics);
        Assert.Equal(0, setup.VillainDeck.MastermindTactics);
        Assert.Equal(plain.VillainDeck.Total + 4, setup.VillainDeck.Total);
        Assert.Contains(setup.Notes, note => note.Text == "Scheme shuffles the 4 Tactics of its Mastermind into the Villain Deck" && note.Citation == "Card");
        Assert.Equal(0, plain.VillainDeck.OwnTactics);
    }

    [Fact]
    public void The_Goblin_lays_out_the_Wounds_its_Master_Strike_gives()
    {
        Assert.NotNull(Draw(2, "Silence the Witnesses", Goblin).Stacks.Wounds);
    }

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(SchemeDraw(players, scheme), mastermind)));

    // The Schemes the player count allows come in catalog order, the core box's before Noir's.
    private static int SchemeDraw(int players, string scheme) =>
        Core.Schemes.Count(s => s.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true) + Array.IndexOf(SchemeNames, scheme);
}
