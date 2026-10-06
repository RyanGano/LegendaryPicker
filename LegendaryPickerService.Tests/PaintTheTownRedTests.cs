using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Paint the Town Red box file (Data/Boxes/paint-the-town-red.json), drawn with the core box. Catalog order puts its cards after the core box's, so
// at 2–5 players its Schemes are 8 Invade the Daily Bugle News HQ, 9 Splice Humans with Spider DNA, 10 The
// Clone Saga and 11 Weave a Web of Lies; Solo allows 6 core Schemes, so there they are 6 to 9. Its Masterminds
// are 4 Carnage and 5 Mysterio.
public class PaintTheTownRedTests
{
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "paint-the-town-red"];

    private static readonly string[] SchemeNames =
    [
        "Invade the Daily Bugle News HQ",
        "Splice Humans with Spider DNA",
        "The Clone Saga",
        "Weave a Web of Lies",
    ];

    private const int Carnage = 4;
    private const int Mysterio = 5;

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Carnage (who leads Maximum Carnage,
    [Theory]
    [InlineData(Carnage, "Carnage", "Maximum Carnage")]
    [InlineData(Mysterio, "Mysterio", "Sinister Six")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "The Clone Saga", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Equal([group, "Brotherhood"], setup.VillainGroups.Select(g => g.Name));
        Assert.Contains(new RuleNote($"{mastermind} always leads {group}", "R p.6", Rulebook, CoreName), setup.Notes);
    }

    [Fact]
    public void A_core_Scheme_can_draw_a_Paint_the_Town_Red_Mastermind_and_Heroes_from_both_boxes()
    {
        // Legacy Virus with Mysterio at 3 players: Sinister Six fills one of the 3 Villain Group slots, and the
        // 5 Heroes are drawn from the 15 core and 5 Paint the Town Red Heroes; the last draw takes Moon Knight.
        var random = new ScriptedRandom(0, 5, 0, 0, 0, 0, 0, 0, 0, 12);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(3, Boxes, random));

        Assert.Equal([12, 6], random.Options.Take(2));
        Assert.Equal("Legacy Virus", setup.Scheme.Name);
        Assert.Equal("Mysterio", setup.Mastermind.Name);
        Assert.Equal(["Sinister Six", "Brotherhood", "Enemies of Asgard"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal([20, 19, 18, 17, 16], random.Options[^5..]);
        Assert.Equal(
            ["Black Widow", "Captain America", "Cyclops", "Deadpool", "Moon Knight"],
            setup.Heroes.Select(hero => hero.Name));
        Assert.Equal(["core", "paint-the-town-red"], setup.Boxes.Select(box => box.Id));
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }
}
