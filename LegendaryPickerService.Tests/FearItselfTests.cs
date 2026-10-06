using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Fear Itself box file (Data/Boxes/fear-itself.json), drawn with Legendary: Villains, whose rules its Plots and Commander follow. Catalog order
// puts its cards before the Villains box's, so at 2–5 players its Plots are 0 Fear Itself, 1 Last Stand at Avengers
// Tower and 2 The Traitor; Solo leaves out The Traitor. Its Commander, Uru-Enchanted Iron Man, is Commander 0.
public class FearItselfTests
{
    private const string FearItselfName = "Fear Itself";
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box FearItself = Catalog.Boxes.Single(box => box.Id == "fear-itself");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["villains", "fear-itself"];
    private static readonly string[] CoreAndFearItself = ["core", "fear-itself"];

    private const int TheTraitor = 2;

    // Fixed-result draws: each Plot at 1, 2 and 5 players with Uru-Enchanted Iron Man, who leads The Mighty (except in
    // The Traitor is for 2 or more players, so Solo draws from the other 2 Fear Itself Plots and the 8 Villains Plots.
    [Fact]
    public void The_Traitor_is_never_drawn_in_Solo()
    {
        var solo = new ScriptedRandom();
        Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, solo));
        var twoPlayers = new ScriptedRandom();
        Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, twoPlayers));

        Assert.Equal((10, 11), (solo.Options[0], twoPlayers.Options[0]));
        for (var seed = 0; seed < 4; seed++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            Assert.NotEqual("The Traitor", setup.Scheme.Name);
        }
    }

    // The insert lets Fear Itself be played with Heroic sets alone (FI p.2), and the owner decided it should be: its
    // Plots and Commander then follow the core box's rules, and parts no included box has get stand-ins.
    [Fact]
    public void Fear_Itself_can_be_drawn_with_the_core_box_alone()
    {
        var other = FearItself.OtherRuleset!;
        Assert.Equal("D-heroic", other.Source);
        Assert.Equal(
            [
                new StandIn(Part.Bindings, "FI p.2", Part.Wounds),
                new StandIn(Part.MadameHydra, "FI p.2", Part.Officers),
                new StandIn(Part.NewRecruits, "FI p.2"),
            ],
            other.StandIns);
        Assert.Null(Generator.CheckBoxes(["core", "fear-itself"]));
        Assert.Null(Generator.CheckBoxes(["core", "villains", "fear-itself"]));
    }

    // The Betrayal Deck takes Wounds in place of Bindings when no included box has Bindings.
    [Theory]
    [InlineData(2, 6)]
    [InlineData(5, 15)]
    public void The_Traitor_with_the_core_box_alone_sets_aside_Wounds(int players, int wounds)
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, CoreAndFearItself, new ScriptedRandom(8 + TheTraitor, 4)));

        Assert.Equal(
            [
                new MovedCards(CardKind.Wound, Pile.Wounds, Pile.SetAside, wounds, wounds),
                new MovedCards(CardKind.Twist, Pile.Twists, Pile.SetAside, 1, 1),
            ],
            setup.Moves);
        Assert.Equal(30 - wounds, setup.Stacks.Wounds);
        Assert.Contains(new RuleNote($"Plot moves {wounds} Wounds into a stack set aside, 3 per player", "Card", null, FearItselfName), setup.Notes);
    }
}
