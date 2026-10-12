using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Scheme's setup.mastermindTwists, using Fixtures/MastermindTwists: a copy of the core box and a made-up expansion
// whose one Scheme, Test Ritual (7 Twists), prints 1 Twist with its Mastermind Test Lord. Catalog order puts Test Ritual
// after the core box's 8 Schemes and Test Lord after its 4 Masterminds; Mastermind draw 0 is Dr. Doom.
public sealed class MastermindTwistsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "MastermindTwists");

    private static readonly SetupGenerator Generator = new(BoxCatalog.Load(FixtureDirectory));

    private static readonly string[] Boxes = ["core", "rituals"];

    private const int TestRitual = 8;
    private const int DrDoom = 0;
    private const int TestLord = 4;

    [Fact]
    public void The_named_Mastermind_replaces_the_Schemes_Twists()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(TestRitual, TestLord)));

        Assert.Equal(("Test Ritual", "Test Lord"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(1, setup.VillainDeck.Twists);
        Assert.Contains(new RuleNote("Scheme uses 1 Twist total with Test Lord", "Card", null, "Rituals Fixture"), setup.Notes);
    }

    [Fact]
    public void Any_other_Mastermind_leaves_the_Schemes_Twists()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(TestRitual, DrDoom)));

        Assert.Equal("Dr. Doom", setup.Mastermind.Name);
        Assert.Equal(7, setup.VillainDeck.Twists);
    }
}
