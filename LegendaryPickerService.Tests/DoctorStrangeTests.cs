using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Doctor Strange and the Shadows of Nightmare box file (Data/Boxes/doctor-strange-and-the-shadows-of-nightmare.json), drawn with the core box.
public class DoctorStrangeTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "doctor-strange-and-the-shadows-of-nightmare"];

    // Nightmare always leads the Fear Lords, and War for the Dream Dimension adds one more Villain Group on top of the 2-player count.
    [Fact]
    public void Nightmare_leads_the_Fear_Lords_and_War_for_the_Dream_Dimension_adds_a_group()
    {
        var setup = Draw(2, "War for the Dream Dimension", "Nightmare");

        Assert.Contains("Fear Lords", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(3, setup.VillainGroups.Count);
    }

    // The core box's own rules lay out no Wounds; Nightmare's Night Terrors Tactic gains one, so Nightmare brings the
    // Wound Stack. The rest of the draw is scripted to cards that take no Wounds: Skrulls and five such core Heroes.
    [Fact]
    public void Nightmare_lays_out_the_Wound_Stack()
    {
        var setup = Draw(2, "Duels of Science and Magic", "Nightmare", 5, 0, 0, 0, 0, 1, 1);

        Assert.Equal(["Fear Lords", "Skrulls"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Emma Frost", "Gambit"], setup.Heroes.Select(hero => hero.Name).Order());
        Assert.NotNull(setup.Stacks.Wounds);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Claim Souls for Demons");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Claim Souls for Demons", "Dormammu");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Claim Souls for Demons's Twists"));
    }

    private static SetupResult Draw(int players, string scheme, string mastermind, params int[] rest)
    {
        var s = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var m = DrawIndexes.Mastermind(Catalog, Boxes, scheme, mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom([s, m, .. rest])));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
