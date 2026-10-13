using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Tests;

// The load-time check that a Scheme's Hero rules can be met searches within a budget (#145), using
// Fixtures/HeroRulesBudget: one made-up box, with its own teams, whose one Scheme, Test Tangle, counts
// Heroes of three teams at once.
public sealed class HeroRulesBudgetTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "HeroRulesBudget");

    [Fact]
    public void A_Scheme_whose_Hero_rules_outrun_the_budget_fails_the_load_naming_it()
    {
        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(FixtureDirectory, heroSearchBudget: 3));

        Assert.Contains("tangle.json", error.Message);
        Assert.Contains("tangle_scheme_test-tangle", error.Message);
        Assert.Contains("within 3 search steps", error.Message);
    }

    [Fact]
    public void The_same_Scheme_loads_within_the_default_budget()
    {
        var catalog = BoxCatalog.Load(FixtureDirectory);

        Assert.Contains(catalog.Boxes, box => box.Schemes.Any(scheme => scheme.Name == "Test Tangle"));
    }
}
