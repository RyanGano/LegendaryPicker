using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Secret Wars Volume 1 box file (Data/Boxes/secret-wars-volume-1.json), drawn with the core box. Catalog order puts its cards after the core
// box's, so at 2–5 players its Schemes are 8 Build an Army of Annihilation to 15 Smash Two Dimensions Together;
// Solo allows 6 core Schemes, so there they are 6 to 13. Its Masterminds are 4 Madelyne Pryor, Goblin Queen,
// 5 Nimrod, Super Sentinel, 6 Wasteland Hulk and 7 Zombie Green Goblin.
public class SecretWarsVolume1Tests
{
    private const string SecretWarsName = "Secret Wars Volume 1";
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "secret-wars-volume-1"];

    private static readonly string[] SchemeNames =
    [
        "Build an Army of Annihilation",
        "Corrupt the Next Generation of Heroes",
        "Crush Them With My Bare Hands",
        "Dark Alliance",
        "Fragmented Realities",
        "Master of Tyrants",
        "Pan-Dimensional Plague",
        "Smash Two Dimensions Together",
    ];

    private const int Madelyne = 4;
    private const int Nimrod = 5;

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Madelyne (who leads Limbo, except in Solo)
    // The Sidekick-stack case: the 10 Sidekicks the Scheme moves into the Villain Deck leave 5 in the stack.
    [Fact]
    public void Corrupt_the_Next_Generation_of_Heroes_moves_10_Sidekicks_into_the_Villain_Deck()
    {
        var setup = Draw(3, "Corrupt the Next Generation of Heroes", Madelyne);

        Assert.Equal([new MovedCards(CardKind.Sidekick, Pile.Sidekicks, Pile.VillainDeck, 10, 10)], setup.Moves);
        Assert.Equal(5, setup.Stacks.Sidekicks);
        Assert.Contains(new RuleNote("Scheme moves 10 Sidekicks into the Villain Deck", "Card", null, SecretWarsName), setup.Notes);
    }

    // The second Mastermind is drawn from the others and set aside until the first Twist; its Always Leads group isn't
    // added, since the Mastermind arrives mid-game.
    [Fact]
    public void Dark_Alliance_sets_a_second_Mastermind_aside()
    {
        var setup = Draw(2, "Dark Alliance", Madelyne);

        var second = Assert.Single(setup.OutsideMasterminds!);
        Assert.Equal(("Dr. Doom", Pile.SetAside, (int?)null), (second.Mastermind.Name, second.To, second.Tactics));
        Assert.Equal(["Limbo", "Brotherhood"], setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme draws 1 other Mastermind and sets it aside", "Card", null, SecretWarsName), setup.Notes);
        Assert.Equal("Twist 1", second.Joins);
        Assert.Contains(new RuleNote("Scheme: the Mastermind set aside joins on Twist 1", "Card", null, SecretWarsName), setup.Notes);
    }

    [Fact]
    public void Master_of_Tyrants_shuffles_12_Tactics_of_3_other_Masterminds_into_the_Villain_Deck()
    {
        var setup = Draw(2, "Master of Tyrants", Madelyne);

        Assert.Equal(["Dr. Doom", "Loki", "Magneto"], setup.OutsideMasterminds!.Select(other => other.Mastermind.Name));
        Assert.All(setup.OutsideMasterminds!, other => Assert.Equal((Pile.VillainDeck, (int?)4), (other.To, other.Tactics)));
        Assert.Equal(12, setup.VillainDeck.MastermindTactics);
        Assert.Contains(
            new RuleNote("Scheme draws 3 other Masterminds and shuffles 12 of their Tactics into the Villain Deck", "Card", null, SecretWarsName),
            setup.Notes);
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }
}
