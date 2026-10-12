using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The 2099 box file (Data/Boxes/2099.json), a Revised expansion drawn with the What If...? base game (#213).
public class TwentyNinetyNineTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["marvel-studios-what-if", "2099"];

    // Subjugate Earth adds an extra Hero and its Mega-Corp effects gain Wounds (Card); Alchemax Executives lead
    // Alchemax Enforcers, which the loader keeps in the box.
    [Fact]
    public void Subjugate_Earth_adds_a_Hero_and_lays_out_Wounds_with_the_Executives_and_their_Enforcers()
    {
        var setup = Draw(2, "Subjugate Earth with Mega-Corporations", "Alchemax Executives", epic: false);

        Assert.Equal(6, setup.Heroes.Count);
        Assert.Contains("Alchemax Enforcers", setup.VillainGroups.Select(group => group.Name));
        Assert.NotNull(setup.Stacks.Wounds);
        Assert.Contains("Adapt: shuffle the 4 Tactics. If Avatarr is on top, add an extra Hero", setup.Steps);
    }

    // With Epic Masterminds on, the Adapting Mastermind plays on its Epic side and the step offering it goes (#212).
    [Fact]
    public void Epic_Masterminds_plays_the_Sinister_Six_on_their_Epic_side()
    {
        var setup = Draw(2, "Pull Reality Into Cyberspace", "Sinister Six 2099", epic: true);

        Assert.Equal("Epic Sinister Six 2099", setup.Mastermind.Name);
        Assert.Contains("Alchemax Enforcers", setup.VillainGroups.Select(group => group.Name));
        Assert.DoesNotContain(setup.Steps, step => step.Contains("play the Epic side"));
    }

    private static SetupResult Draw(int players, string scheme, string mastermind, bool epic)
    {
        var included = Catalog.Boxes.Where(box => Boxes.Contains(box.Id)).ToList();
        var s = included.SelectMany(box => box.AllSchemes).ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = included.SelectMany(box => box.AllMasterminds).ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m), epic));
        Assert.Equal((scheme, epic ? "Epic " + mastermind : mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
