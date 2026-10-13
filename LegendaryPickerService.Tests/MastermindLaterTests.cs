using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Scheme that adds its Mastermind mid-game (setup.mastermindJoinsLater, #245), using Fixtures/MastermindLater: a copy of
// the core box and a made-up expansion with Late Arrival, which has the flag, and Early Arrival, which doesn't. The fixture's
// Mastermind, Late Lord, always leads Late Gang, adds a Hero and a setup step, and alone carries the keyword Lurk. Catalog order puts the Schemes after the
// core box's 8 and Late Lord after its 4 Masterminds.
public sealed class MastermindLaterTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "MastermindLater"));

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "later"];

    private const int LateArrival = 8;
    private const int EarlyArrival = 9;
    private const int LateLord = 4;

    [Fact]
    public void The_Mastermind_is_drawn_but_its_Always_Leads_group_and_setup_effects_wait_for_the_reveal()
    {
        var early = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(EarlyArrival, LateLord)));
        var late = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(LateArrival, LateLord)));

        // Drawn up front, the Mastermind puts its group in the Villain Deck and adds its Hero and step.
        Assert.Contains("Late Gang", early.VillainGroups.Select(group => group.Name));
        Assert.Equal(6, early.Heroes.Count);

        Assert.Equal("Late Lord", late.Mastermind.Name);
        Assert.DoesNotContain("Late Gang", late.VillainGroups.Select(group => group.Name));
        Assert.Equal(5, late.Heroes.Count);
        Assert.Empty(late.Steps);
        Assert.DoesNotContain(late.Notes, note => note.Text.Contains("Late Lord"));

        var reveal = Assert.IsType<MastermindReveal>(late.MastermindReveal);
        Assert.Equal(["Late Gang"], reveal.AlwaysLeads.Select(group => group.Name));
        Assert.Contains(reveal.Notes, note => note.Text == "Late Lord adds 1 Hero");
        Assert.Contains(reveal.Notes, note => note.Text == "Late Lord adds a setup step: Test Start of Game step");
        Assert.Null(early.MastermindReveal);
    }

    [Fact]
    public void The_response_names_the_glossary_terms_only_the_hidden_Mastermind_brings()
    {
        var late = Generator.Generate(2, Boxes, new ScriptedRandom(LateArrival, LateLord));

        var body = Assert.IsType<SetupBody>(SetupResponse.From(late, Catalog));

        // The Scheme's Scheme Twist and Late Gang's Fight, which the drawn Villain Groups carry too, stay in the glossary.
        Assert.Equal(["core_term_mastermind-tactic", "later_term_lurk"], body.MastermindReveal!.Terms);
        Assert.Contains("later_term_lurk", body.Glossary.Select(term => term.Id));
    }
}
