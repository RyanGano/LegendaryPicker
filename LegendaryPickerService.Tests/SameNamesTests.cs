using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Display names are the printed card names, so two boxes can hold a card of one name (#243). Fixtures/SameNames: copies of the
// core and Second Edition boxes and a made-up expansion with its own Cyclops, required by its Mastermind, and a Scheme that
// requires the core box's HYDRA, which Second Edition prints as Hydra. Catalog order puts the expansion's Schemes after the
// core box's 8, or first when the core box isn't included, and its Mastermind after the core box's 4.
public sealed class SameNamesTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "SameNames"));

    [Fact]
    public void A_note_names_the_box_of_a_card_whose_name_another_included_box_prints()
    {
        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, ["core", "samenames"], new ScriptedRandom(8, 4)));

        Assert.Equal("Test Warden", setup.Mastermind.Name);
        Assert.Contains(setup.Notes, note => note.Text == "Test Warden requires Cyclops (from Same Names Fixture)");
    }

    // The note already names the box a required group comes from when that box isn't included, so it names it only once.
    [Fact]
    public void A_required_group_from_a_box_not_included_is_named_with_its_box_once()
    {
        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, ["second-edition", "samenames"], new ScriptedRandom(1)));

        Assert.Equal("Test Hydra Call", setup.Scheme.Name);
        Assert.Contains(setup.Notes, note => note.Text == "Scheme requires HYDRA, from Marvel Legendary First Edition core box, which isn't included");
    }
}
