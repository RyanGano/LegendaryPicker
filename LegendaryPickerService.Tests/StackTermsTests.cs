using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Glossary terms a box attaches to the stacks it adds to (#127), using Fixtures/StackTerms: a copy of the core box and a
// made-up expansion with 5 Wounds and 4 Sidekicks, each with a term. The core box has 4 Masterminds, so Test Recruiter
// (uses Sidekicks) is draw 4 and Test Brute (uses Wounds) draw 5; Test Patrol is the Scheme draw 8.
public sealed class StackTermsTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "StackTerms"));

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "stacks"];

    [Fact]
    public void A_stack_laid_out_brings_its_term_and_a_stack_left_out_does_not()
    {
        Assert.Equal(["Test Wound"], GlossaryNames(Draw(mastermind: 5)));
        Assert.Contains("Test Sidekick", GlossaryNames(Draw(mastermind: 4)));
    }

    private static string[] GlossaryNames(SetupBody setup) =>
        [.. setup.Glossary.Where(entry => entry.Id.StartsWith("stacks_term_", StringComparison.Ordinal)).Select(entry => entry.Name)];

    private static SetupBody Draw(int mastermind) =>
        Assert.IsType<SetupBody>(SetupResponse.From(Generator.Generate(2, Boxes, new ScriptedRandom(8, mastermind)), Catalog));
}
