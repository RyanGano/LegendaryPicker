using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Civil War box file (Data/Boxes/civil-war.json), drawn with the core box. Catalog order puts its cards before the core box's, so its Schemes are draws 0 to 7
// at every player count and its Masterminds 0 Authoritarian Iron Man, 1 Baron Helmut Zemo, 2 Maria Hill, 3 Misty Knight and
// 4 Ragnarok.
public class CivilWarTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "civil-war"];

    private static readonly string[] SchemeNames =
    [
        "Avengers vs. X-Men",
        "Dark Reign of H.A.M.M.E.R. Officers",
        "Epic Super Hero Civil War",
        "Imprison Unregistered Superhumans",
        "Nitro the Supervillain Threatens Crowds",
        "Predict Future Crime",
        "Reveal Heroes' Secret Identities",
        "United States Split by Civil War",
    ];

    private const int Ragnarok = 4;

    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    public void Predict_Future_Crime_adds_a_Villain_Group(int players, int groups)
    {
        var setup = Draw(players, "Predict Future Crime", Ragnarok);

        Assert.Equal(groups, setup.VillainGroups.Count);
        Assert.Contains(new RuleNote("Scheme adds 1 Villain Group", "Card", null, "Civil War"), setup.Notes);
    }

    // The team split's search stays fast with every box included, where many teams could take each side (#93). One scripted
    // draw at 5 players. It takes about 0.3 s alone and took about 3 s before #93, so the 1.5 s budget leaves room for a slow
    // runner yet still fails on a return to multi-second draws.
    [Fact]
    public void Avengers_vs_X_Men_draws_quickly_with_every_box()
    {
        var everyBox = Catalog.Boxes.Select(box => box.Id).ToList();
        var schemes = Catalog.Boxes.SelectMany(box => box.Schemes)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(5) ?? true)
            .ToList();
        var random = new ScriptedRandom(schemes.FindIndex(scheme => scheme.Name == "Avengers vs. X-Men"));

        var timer = System.Diagnostics.Stopwatch.StartNew();
        var setup = Assert.IsType<SetupResult>(Generator.Generate(5, everyBox, random));
        timer.Stop();

        Assert.Equal("Avengers vs. X-Men", setup.Scheme.Name);
        Assert.Equal([3, 3], setup.Heroes.GroupBy(hero => hero.Team).Select(team => team.Count()));
        Assert.InRange(timer.ElapsedMilliseconds, 0, 1500);
    }

    // The Aspiring Hero Bystanders gain a Sidekick when rescued, and every setup shuffles them in with the other Bystanders,
    // so with Civil War included the Sidekick stack is always laid out: its 15 Pet Avengers, with Secret Wars Volume 1's 15
    // too when that box is included (CW p.2). The 7 special Bystanders join the core box's 30 (and Secret Wars Volume 1's 1),
    // less the 2 in the Villain Deck.
    [Theory]
    [InlineData(new[] { "core", "civil-war" }, 15, 35)]
    [InlineData(new[] { "core", "civil-war", "secret-wars-volume-1" }, 30, 36)]
    public void The_special_Sidekicks_and_Bystanders_join_their_stacks(string[] boxes, int sidekicks, int bystanders)
    {
        // Ragnarok and the first draw of everything else: no drawn card gains a Sidekick.
        var random = new ScriptedRandom(Array.IndexOf(SchemeNames, "United States Split by Civil War"), Ragnarok, 0, 0, 16, 16, 16, 17, 17);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, random));

        Assert.Equal(sidekicks, setup.Stacks.Sidekicks);
        Assert.Equal(bystanders, setup.Stacks.Bystanders);
    }

    private static SetupResult Draw(int players, string scheme, int mastermind) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(Array.IndexOf(SchemeNames, scheme), mastermind)));
}
