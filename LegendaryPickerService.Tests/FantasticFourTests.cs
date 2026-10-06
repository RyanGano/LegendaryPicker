using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Fantastic Four box file (Data/Boxes/fantastic-four.json), drawn with the core box. Catalog order puts its cards after the core box's, so at 2–5
// players its Schemes are 8 Bathe the Earth in Cosmic Rays, 9 Flood the Planet with Melted Glaciers,
// 10 Invincible Force Field and 11 Pull Reality Into the Negative Zone; Solo allows 6 core Schemes, so
// there they are 6 to 9. Its Masterminds are 4 Galactus and 5 Mole Man.
public class FantasticFourTests
{
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "fantastic-four"];

    private static readonly string[] SchemeNames =
    [
        "Bathe the Earth in Cosmic Rays",
        "Flood the Planet with Melted Glaciers",
        "Invincible Force Field",
        "Pull Reality Into the Negative Zone",
    ];

    private const int Galactus = 4;
    private const int MoleMan = 5;

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Galactus (who leads the Heralds of
    [Theory]
    [InlineData(Galactus, "Galactus", "Heralds of Galactus")]
    [InlineData(MoleMan, "Mole Man", "Subterranea")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "Invincible Force Field", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Equal([group, "Brotherhood"], setup.VillainGroups.Select(g => g.Name));
        Assert.Contains(new RuleNote($"{mastermind} always leads {group}", "R p.6", Rulebook, CoreName), setup.Notes);
    }

    [Fact]
    public void A_core_Scheme_can_draw_a_Fantastic_Four_Mastermind_and_Heroes_from_all_three_boxes()
    {
        // Super Hero Civil War with Mole Man at 3 players: Subterranea fills one of the 3 Villain Group slots,
        // and the Heroes come from the 15 core, 17 Dark City and 5 Fantastic Four Heroes; the last draw takes Thing.
        var random = new ScriptedRandom(6, 10, 0, 0, 0, 0, 0, 0, 0, 32);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, ["core", "dark-city", "fantastic-four"], random));

        Assert.Equal([20, 11], random.Options.Take(2));
        Assert.Equal("Super Hero Civil War", setup.Scheme.Name);
        Assert.Equal("Mole Man", setup.Mastermind.Name);
        Assert.Equal(["Subterranea", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal([37, 36, 35, 34, 33], random.Options[^5..]);
        Assert.Equal(
            ["Black Widow", "Captain America", "Cyclops", "Deadpool", "Thing"],
            setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(["core", "dark-city", "fantastic-four"], setup.Boxes.Select(box => box.Id));
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }
}
