using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Messiah Complex box file (Data/Boxes/messiah-complex.json), drawn with the core box.
public class MessiahComplexTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "messiah-complex"];

    // Bastion leads the Purifiers and any Sentinel Henchman Group: the core box's Sentinel is one of them.
    [Fact]
    public void Bastion_leads_the_Purifiers_and_one_of_the_Sentinel_Henchman_Groups()
    {
        var setup = Draw(2, "Raid Gene Banks To...", "Bastion, Fused Sentinel");

        Assert.Contains("Purifiers", setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(setup.Notes, note => note.Text.StartsWith("Bastion, Fused Sentinel also always leads ", StringComparison.Ordinal)
            && note.Text.EndsWith(", one of Sentinel and Sentinel Squad O*N*E*", StringComparison.Ordinal));
    }

    // Drain Mutants' Powers takes its Kidnapped Mutants from the Sidekick Stack, which holds the box's 14 Sidekicks.
    [Fact]
    public void Drain_Mutants_Powers_lays_out_the_box_s_14_Sidekicks()
    {
        var setup = Draw(2, "Drain Mutants' Powers To...", "Dr. Doom");

        Assert.Equal(14, setup.Stacks.Sidekicks);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Hire Singularity Investigations To...");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Hire Singularity Investigations To...", "Exodus");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Hire Singularity Investigations To...'s Twists"));
    }

    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var s = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var m = DrawIndexes.Mastermind(Catalog, Boxes, scheme, mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
