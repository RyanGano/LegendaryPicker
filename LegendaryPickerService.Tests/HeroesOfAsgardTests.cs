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

    // Its own Mastermind is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_Malekith_rates_with_a_reason_naming_him()
    {
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "War of the Frost Giants", "Malekith the Accursed");
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "War of the Frost Giants");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Malekith the Accursed's Master Strikes"));
    }
}
