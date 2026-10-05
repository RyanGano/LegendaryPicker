using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that count or draw Heroes by a word in their Hero Names, as World War Hulk's Fall of the Hulks and Shoot Hulk
// into Space ask for Heroes with "Hulk" in their Hero Names (#143), using Fixtures/HeroNameWords: a copy of the core box
// and a made-up expansion. Catalog order puts the fixture's Heroes after the core box's 15: Test Banner, Test She-Hulk and
// Test Hulkling, so the Heroes with "Hulk" in their Hero Names are the core box's Hulk (8th) and the last two. At 2
// players the fixture's Schemes are 8 Test Fall (exactly 2 Heroes with "Hulk" in their Hero Names) and 9 Test Exile (1
// extra Hero with "Hulk" in its Hero Name, set aside). Mastermind draw 0 is Dr. Doom, who leads a Henchman Group, so a
// 2-player setup draws 2 Villain Groups and then its Heroes.
public sealed class HeroNameWordsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "HeroNameWords");

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "hulks"];

    private const int FirstHeroDraw = 4;

    // Once the slots left are only enough for the count, only Heroes with the word in their Hero Name can fill them.
    [Fact]
    public void Exactly_N_Heroes_with_a_word_in_their_Hero_Names_fill_the_Hero_Deck()
    {
        var random = new ScriptedRandom(8, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Fall", setup.Scheme.Name);
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Hulk", "Test She-Hulk"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([18, 17, 16, 3, 2], random.Options[FirstHeroDraw..]);
        Assert.Equal(
            new RuleNote("Scheme requires exactly 2 Heroes with \"Hulk\" in their Hero Names", "Card", null, "Hulks Fixture"),
            setup.Notes[0]);
    }

    // Hulk and Test She-Hulk drawn first meet the count, so the third Hero with "Hulk" in its Hero Name is left out.
    [Fact]
    public void Heroes_with_the_word_beyond_an_exact_count_are_not_drawn()
    {
        var random = new ScriptedRandom(8, 0, 0, 0, 7, 15);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal(["Hulk", "Test She-Hulk", "Black Widow", "Captain America", "Cyclops"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([18, 17, 15, 14, 13], random.Options[FirstHeroDraw..]);
    }

    [Fact]
    public void An_extra_Hero_with_a_word_in_its_Hero_Name_is_drawn_from_the_Heroes_with_it()
    {
        var random = new ScriptedRandom(9, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Exile", setup.Scheme.Name);
        Assert.Equal(3, random.Options[^1]);
        Assert.Equal([new OutsideHero(Catalog.Boxes.SelectMany(box => box.Heroes).Single(hero => hero.Name == "Hulk"), Pile.SetAside, 14)], setup.OutsideHeroes);
        Assert.Equal(
            "Scheme draws 1 extra Hero with \"Hulk\" in its Hero Name outside the Hero Deck and puts its cards in a stack set aside",
            setup.Notes[0].Text);
    }
}
