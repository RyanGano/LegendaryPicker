using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Mastermind's required Heroes against a Scheme's Hero rules (#205), using Fixtures/RequiredHeroes: a copy of the core box and a
// made-up Revised base game. Its Test Warden requires Test Zulu; its one Scheme, Test Cap, allows no Test Zulu, so it must list
// Test Warden in excludesMasterminds, which the file does. Mastermind draw 0 is then Test Overseer.
public sealed class RequiredHeroesTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "RequiredHeroes");

    [Fact]
    public void A_Scheme_that_cannot_take_a_Masterminds_required_Heroes_must_exclude_it()
    {
        var setup = Assert.IsType<SetupResult>(new SetupGenerator(BoxCatalog.Load(FixtureDirectory)).Generate(2, ["limited"], new ScriptedRandom(0, 0)));

        Assert.Equal(("Test Cap", "Test Overseer"), (setup.Scheme.Name, setup.Mastermind.Name));
    }

    [Fact]
    public void Loading_refuses_a_Scheme_that_leaves_out_such_a_Mastermind_and_names_both()
    {
        var directory = Directory.CreateTempSubdirectory("required-heroes").FullName;
        try
        {
            foreach (var file in Directory.GetFiles(FixtureDirectory, "*.json"))
            {
                var text = File.ReadAllText(file);
                if (Path.GetFileName(file) == "limited.json")
                {
                    var exclusion = text.IndexOf("excludesMasterminds", StringComparison.Ordinal);
                    text = string.Concat(text.AsSpan(0, exclusion), text[exclusion..].Replace("test-warden", "test-overseer"));
                }

                File.WriteAllText(Path.Combine(directory, Path.GetFileName(file)), text);
            }

            var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(directory));

            Assert.Contains("limited_scheme_test-cap has Hero rules limited_mastermind_test-warden's required Heroes can't meet", error.Message);
            Assert.Contains("excludesMasterminds", error.Message);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }
}
