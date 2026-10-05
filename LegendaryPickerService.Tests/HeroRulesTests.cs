using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that constrain the Hero Deck or draw Heroes outside it, using Fixtures/HeroRules: a copy of the
// core box and a made-up expansion. Catalog order puts the fixture's Heroes after the core box's 15: Test
// Jean Grey and Test Phoenix (both Hero Name Test Jean, X-Men), Test Nova Rider and Test Nova Prime (both
// Test Nova) and Test Spider-Girl (Spider Friends). At 2–5 players the fixture's Schemes are 8 Test Song
// (1 extra Hero into the Villain Deck), 9 Test Demons (1 extra Test Jean Hero into the Villain Deck),
// 10 Test Summons (requires Test Nova Rider), 11 Test Web (at least 1 Spider Friends Hero), 12 Test
// Corps (exactly 2 Test Nova Heroes), 13 Test Unique (requires Test Jean Grey, no two Heroes with the same
// Hero Name), 14 Test Vault (2 extra X-Men Heroes set aside, 1 in Solo), 15 Test Lair (Test Phoenix
// beside the Scheme) and 16 Test Guest Star (1 Test Visitor, from the Visitors fixture, into the Villain Deck).
// Solo allows 6 core Schemes, so there Test Vault is 12. Mastermind draw 0 is Dr. Doom,
// who leads a Henchman Group, so a 2-player setup draws 2 Villain Groups and then its Heroes.
public sealed class HeroRulesTests
{
    public static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "HeroRules");

    private const string FixtureName = "Heroes Fixture";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "heroes"];

    // The first Hero draw of a 2-player setup with Dr. Doom follows the Scheme, Mastermind and 2 Villain Group draws.
    private const int FirstHeroDraw = 4;

    [Fact]
    public void An_extra_Heros_cards_go_into_the_Villain_Deck_and_its_total()
    {
        var setup = Draw(2, 8, 0);

        Assert.Equal("Test Song", setup.Scheme.Name);
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Deadpool", "Emma Frost"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([new OutsideHero(Hero("core_hero_gambit"), Pile.VillainDeck, 14)], setup.OutsideHeroes);
        Assert.Equal(new VillainDeck(8, 5, 16, 10, 2, 0, 0, 14), setup.VillainDeck);
        Assert.Equal(55, setup.VillainDeck.Total);
        Assert.Equal(new HeroDeck(70, 0), setup.HeroDeck);
        Assert.Equal(
            new RuleNote("Scheme draws 1 extra Hero outside the Hero Deck and puts its cards into the Villain Deck", "Card", null, FixtureName),
            setup.Notes[0]);
    }

    [Fact]
    public void An_extra_Hero_with_a_Hero_Name_is_drawn_from_the_Heroes_with_that_name()
    {
        var random = new ScriptedRandom(9, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Demons", setup.Scheme.Name);
        Assert.Equal(2, random.Options[^1]);
        Assert.Equal([new OutsideHero(Hero("heroes_hero_test-phoenix"), Pile.VillainDeck, 14)], setup.OutsideHeroes);
        Assert.Equal(
            "Scheme draws 1 extra Test Jean Hero outside the Hero Deck and puts its cards into the Villain Deck", setup.Notes[0].Text);
    }

    [Fact]
    public void A_required_Hero_takes_a_Hero_Deck_slot()
    {
        var random = new ScriptedRandom(10, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Summons", setup.Scheme.Name);
        Assert.Equal(["Test Nova Rider", "Black Widow", "Captain America", "Cyclops", "Deadpool"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([19, 18, 17, 16], random.Options[FirstHeroDraw..]);
        Assert.Equal(new RuleNote("Scheme requires Test Nova Rider", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Fact]
    public void The_last_Hero_draw_satisfies_a_team_the_earlier_draws_left_out()
    {
        var random = new ScriptedRandom(11, 0, 0, 0, 0, 0, 0, 0, 1);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Web", setup.Scheme.Name);
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Deadpool", "Test Spider-Girl"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([20, 19, 18, 17, 2], random.Options[FirstHeroDraw..]);
        Assert.Equal(new RuleNote("Scheme requires at least 1 Spider Friends Hero", "Card", null, FixtureName), setup.Notes[0]);
    }

    [Fact]
    public void A_team_Hero_drawn_first_leaves_the_later_draws_free()
    {
        var random = new ScriptedRandom(11, 0, 0, 0, 11);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Spider-Man", setup.Heroes[0].Name);
        Assert.Equal([20, 19, 18, 17, 16], random.Options[FirstHeroDraw..]);
    }

    [Fact]
    public void Exactly_N_Heroes_with_a_Hero_Name_are_drawn()
    {
        var random = new ScriptedRandom(12, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Corps", setup.Scheme.Name);
        Assert.Equal(["Black Widow", "Captain America", "Cyclops", "Test Nova Rider", "Test Nova Prime"], setup.Heroes.Select(hero => hero.Name));
        Assert.Equal([20, 19, 18, 2, 1], random.Options[FirstHeroDraw..]);
        Assert.Equal(new RuleNote("Scheme requires exactly 2 Test Nova Heroes", "Card", null, FixtureName), setup.Notes[0]);
    }

    // Test Guest Star draws a Test Visitor outside the Hero Deck, which only the Visitors fixture holds, and its draw may
    // take the Hero from another box (otherBox). Without that box the setup still uses the Hero the player owns, and the
    // checklist says which box to pull it from; with it, the Hero is an ordinary included one.
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void A_Hero_a_Scheme_may_take_from_another_box_is_drawn_whether_or_not_that_box_is_included(bool visitors)
    {
        string[] boxes = visitors ? ["core", "heroes", "visitors"] : Boxes;
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, boxes, new ScriptedRandom(16, 0)));

        Assert.Equal("Test Guest Star", setup.Scheme.Name);
        Assert.Equal([new OutsideHero(Hero("visitors_hero_test-visitor"), Pile.VillainDeck, 14)], setup.OutsideHeroes);
        Assert.DoesNotContain(Hero("visitors_hero_test-visitor"), setup.Heroes);
        var guest = Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog)).OutsideHeroes.Single().Hero;
        Assert.Equal(("Visitors Fixture", !visitors), (guest.Box, guest.NotIncluded));
    }

    [Fact]
    public void No_two_Heroes_share_a_Hero_Name_when_the_Scheme_says_so()
    {
        var random = new ScriptedRandom(13, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Unique", setup.Scheme.Name);
        Assert.Equal("Test Jean Grey", setup.Heroes[0].Name);
        // Neither Test Jean Grey nor Test Phoenix, who shares its Hero Name, is left to draw.
        Assert.Equal(18, random.Options[FirstHeroDraw]);
        Assert.Equal(
            [
                new RuleNote("Scheme requires Test Jean Grey", "Card", null, FixtureName),
                new RuleNote("Scheme allows no two Heroes with the same Hero Name", "Card", null, FixtureName),
            ],
            setup.Notes.Take(2));
    }

    [Fact]
    public void Extra_Heroes_of_a_team_go_to_a_stack_set_aside()
    {
        var random = new ScriptedRandom(14, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Vault", setup.Scheme.Name);
        Assert.Equal(
            [new OutsideHero(Hero("core_hero_gambit"), Pile.SetAside, 14), new OutsideHero(Hero("core_hero_rogue"), Pile.SetAside, 14)],
            setup.OutsideHeroes);
        // Gambit, Rogue, Storm, Wolverine, Test Jean Grey and Test Phoenix: the X-Men not in the Hero Deck.
        Assert.Equal([6, 5], random.Options[^2..]);
        Assert.Equal(new VillainDeck(8, 5, 16, 10, 2, 0), setup.VillainDeck);
        Assert.Equal(
            new RuleNote("Scheme draws 2 extra X-Men Heroes outside the Hero Deck and puts their cards in a stack set aside", "Card", null, FixtureName),
            setup.Notes[0]);
    }

    [Fact]
    public void Extra_Heroes_use_their_Solo_count_in_Solo()
    {
        var setup = Draw(1, 12, 0);

        Assert.Equal("Test Vault", setup.Scheme.Name);
        Assert.Equal([new OutsideHero(Hero("core_hero_emma-frost"), Pile.SetAside, 14)], setup.OutsideHeroes);
        Assert.Equal(
            new RuleNote(
                "Scheme draws 1 extra X-Men Hero outside the Hero Deck and puts its cards in a stack set aside",
                "R p.2", "https://example.test/heroes-rules.pdf", FixtureName),
            setup.Notes[0]);
    }

    [Fact]
    public void A_named_extra_Hero_is_kept_out_of_the_Hero_Deck()
    {
        var random = new ScriptedRandom(15, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, random));

        Assert.Equal("Test Lair", setup.Scheme.Name);
        Assert.Equal(19, random.Options[FirstHeroDraw]);
        Assert.Equal([new OutsideHero(Hero("heroes_hero_test-phoenix"), Pile.BesideScheme, 14)], setup.OutsideHeroes);
        Assert.Equal(
            new RuleNote("Scheme draws Test Phoenix outside the Hero Deck and puts its cards beside the Scheme", "Card", null, FixtureName),
            setup.Notes[0]);
    }

    private static Hero Hero(string id) => Catalog.Boxes.SelectMany(box => box.Heroes).Single(hero => hero.Id == id);

    private static SetupResult Draw(int players, params int[] draws) =>
        Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(draws)));
}
