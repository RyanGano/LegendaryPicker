using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The New Mutants box file (Data/Boxes/the-new-mutants.json), drawn with the core box.
public class TheNewMutantsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "the-new-mutants"];

    // The Demon Bear Saga requires Demons of Limbo and puts its Demon Bear beside the Scheme.
    [Fact]
    public void The_Demon_Bear_Saga_includes_Demons_of_Limbo()
    {
        var setup = Draw(2, "The Demon Bear Saga");

        Assert.Contains(setup.VillainGroups, g => g.Name == "Demons of Limbo");
        Assert.Contains("Put the Demon Bear Villain beside the Scheme", setup.Steps);
    }

    // The Insane Asylum sets 1 Twist plus 2 per player.
    [Theory]
    [InlineData(1, 3)]
    [InlineData(3, 7)]
    [InlineData(5, 11)]
    public void Trapped_in_the_Insane_Asylum_has_one_Twist_plus_two_per_player(int players, int twists)
    {
        Assert.Equal(twists, Draw(players, "Trapped in the Insane Asylum").VillainDeck.Twists);
    }

    // Superhuman Baseball Game adds a Villain Group to the two the core box's 2-player setup uses.
    [Fact]
    public void Superhuman_Baseball_Game_adds_a_Villain_Group()
    {
        Assert.Equal(3, Draw(2, "Superhuman Baseball Game").VillainGroups.Count);
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var probe = new ScriptedRandom();
        Generator.Generate(players, Boxes, probe);
        var index = Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s))).Scheme.Name == scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
    }
}
