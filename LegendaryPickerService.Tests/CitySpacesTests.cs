using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Mastermind that isn't played with a Scheme that changes the number of city spaces, as Ego's insert says (#192),
// using Fixtures/CitySpaces: a copy of the core box and a made-up expansion. The expansion comes first in the
// catalog, so its Schemes are Test Flood, which changes the city spaces, then Test Calm, before the core box's 8, and
// its Mastermind Test Titan is before the core box's 4.
public sealed class CitySpacesTests
{
    private static readonly SetupGenerator Generator =
        new(BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "CitySpaces")));

    private static readonly string[] Boxes = ["core", "cityspaces"];

    [Fact]
    public void A_Scheme_that_changes_the_city_spaces_never_draws_the_Mastermind_that_cant_be_played_with_it()
    {
        var random = new ScriptedRandom(0, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Flood", 4), (setup.Scheme.Name, random.Options[1]));
        Assert.NotEqual("Test Titan", setup.Mastermind.Name);
    }

    [Fact]
    public void Any_other_Scheme_can_draw_it()
    {
        var random = new ScriptedRandom(1, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(("Test Calm", 5, "Test Titan"), (setup.Scheme.Name, random.Options[1], setup.Mastermind.Name));
    }
}
