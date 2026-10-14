using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Venom box file (Data/Boxes/venom.json), drawn with the core box.
public class VenomTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "venom"];

    [Fact]
    public void Maximum_Carnage_lays_out_6_Wounds_per_player()
    {
        var setup = Draw(3, "Maximum Carnage");

        Assert.Equal(10, setup.VillainDeck.Twists);
        Assert.Equal(18, setup.Stacks.Wounds);
    }

    [Fact]
    public void Symbiotic_Absorption_sets_one_other_Mastermind_aside_and_adds_its_Always_Leads_group()
    {
        var setup = Draw(2, "Symbiotic Absorption");

        var drained = Assert.Single(setup.OutsideMasterminds!);
        Assert.Equal((Pile.SetAside, null), (drained.To, drained.Joins));
        Assert.NotEqual(setup.Mastermind.AlwaysLeads.GroupId, drained.Mastermind.AlwaysLeads.GroupId);
        Assert.Contains(drained.Mastermind.AlwaysLeads.GroupId, setup.VillainGroups.Select(group => group.Id));
        Assert.Equal(11, setup.VillainDeck.Twists);
    }

    // Its own Scheme and Mastermind are tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_and_Mastermind_rates_with_reasons_naming_them()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Maximum Carnage");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Maximum Carnage", "Poison Thanos");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Maximum Carnage's Twists"));
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Poison Thanos's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
