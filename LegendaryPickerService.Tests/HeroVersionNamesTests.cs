using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// A Hero that is a bracketed version of a character counts for a Scheme asking for a word in Hero Names by the Hero Name it
// shares with the character, as Weapon X (Wolverine) does for Condition Logan into Weapon X. Fixtures/HeroVersionNames: a
// copy of the core box and a made-up expansion with Test Gamma (Hulk), whose Hero Name is Hulk, and Test One Hulk (exactly 1
// Hero with "Hulk" in its Hero Name). Catalog order puts that Scheme after the core box's 8, and the 2-player draw goes
// Scheme, Mastermind, 2 Villain Groups, then the Heroes.
public sealed class HeroVersionNamesTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(Path.Combine(AppContext.BaseDirectory, "Fixtures", "HeroVersionNames"));

    [Fact]
    public void A_bracketed_version_counts_as_a_Hero_with_the_word_in_its_Hero_Name()
    {
        var random = new ScriptedRandom(8, 0, 0, 0, 0, 0, 0, 0, 1);

        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, ["core", "versions"], random));

        Assert.Equal("Test One Hulk", setup.Scheme.Name);
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Deadpool", "Test Gamma (Hulk)"], setup.Heroes.Select(hero => hero.Name));
    }
}
