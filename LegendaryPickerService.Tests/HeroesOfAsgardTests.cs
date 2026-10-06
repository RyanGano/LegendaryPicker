using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Heroes of Asgard box file (Data/Boxes/heroes-of-asgard.json), drawn with the core box.
public class HeroesOfAsgardTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "heroes-of-asgard"];

    // Each Epic Mastermind leads the group its card names; Hela's Master Strike and Surtur's Fight (Omens) gain Wounds, so a
    // setup with Hela lays the Wound stack out.
    [Fact]
    public void Hela_leads_Omens_of_Ragnarok_with_Wounds_and_Malekith_leads_the_Dark_Council()
    {
        var probe = new ScriptedRandom(0);
        Generator.Generate(2, Boxes, probe);

        var results = new Dictionary<string, SetupResult>();
        for (var mastermind = 0; mastermind < probe.Options[1]; mastermind++)
        {
            var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(0, mastermind)));
            results[setup.Mastermind.Name] = setup;
        }

        Assert.Contains(results["Hela, Goddess of Death"].VillainGroups, g => g.Name == "Omens of Ragnarok");
        Assert.NotNull(results["Hela, Goddess of Death"].Stacks.Wounds);
        Assert.Contains(results["Malekith the Accursed"].VillainGroups, g => g.Name == "Dark Council");
    }

    // The box's Thor shares the core Thor's Hero Name but must name the physical card apart from it.
    [Fact]
    public void This_Thor_has_its_own_display_name_beside_the_core_Thor()
    {
        var thor = Catalog.Boxes.Single(b => b.Id == "heroes-of-asgard").Heroes.Single(h => h.HeroName == "Thor");

        Assert.Equal("Thor (Heroes of Asgard)", thor.Name);
        Assert.Contains(Catalog.Boxes.Single(b => b.Id == "core").Heroes, h => h.Name == "Thor");
    }
}
