using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Schemes that draw Masterminds besides their own (#113), using Fixtures/OutsideMasterminds: a copy of the core box
// and a made-up expansion. Catalog order puts the fixture's Schemes after the core box's 8 (6 in Solo): Test Alliance
// (sets 1 other Mastermind aside), Test Tyrants (shuffles 4 Tactics each of 3 other Masterminds, 2 in Solo, into the
// Villain Deck) and Test Hideout (shuffles its own Mastermind's 4 Tactics into the Villain Deck, setup.ownTactics).
// Each requires Test Mob, which uses no part. Mastermind draw 0 is Dr. Doom, who uses no part; the others, in order,
// are Loki and Magneto, who use Wounds, and Red Skull.
public sealed class OutsideMastermindsTests
{
    private static readonly string FixtureDirectory = Path.Combine(AppContext.BaseDirectory, "Fixtures", "OutsideMasterminds");

    private const string FixtureName = "Rivals Fixture";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(FixtureDirectory);

    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "rivals"];

    [Fact]
    public void A_Mastermind_set_aside_is_drawn_from_the_others_and_brings_the_parts_it_uses()
    {
        // In Solo, Test Mob fills the one Villain Group slot and the first three core Heroes use no Wounds, so only
        // Loki, set aside, brings the Wound stack. The last draw picks from the 3 Masterminds besides Dr. Doom.
        var random = new ScriptedRandom(6, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, random));

        Assert.Equal(("Test Alliance", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(3, random.Options[^1]);
        Assert.Equal([new OutsideMastermind(Mastermind("core_mastermind_loki"), Pile.SetAside, null)], setup.OutsideMasterminds);
        Assert.Equal(new VillainDeck(8, 1, 8, 3, 1, 0), setup.VillainDeck);
        Assert.Equal(30, setup.Stacks.Wounds);
        Assert.Contains(new RuleNote("Scheme draws 1 other Mastermind and sets it aside", "Card", null, FixtureName), setup.Notes);
    }

    [Fact]
    public void Tactics_shuffled_into_the_Villain_Deck_count_in_it_and_bring_no_parts()
    {
        // The same Solo draw as above, but Loki's and Magneto's Tactics play with no abilities, so nothing uses Wounds.
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(7, 0)));

        Assert.Equal("Test Tyrants", setup.Scheme.Name);
        Assert.Equal(
            [
                new OutsideMastermind(Mastermind("core_mastermind_loki"), Pile.VillainDeck, 4),
                new OutsideMastermind(Mastermind("core_mastermind_magneto"), Pile.VillainDeck, 4),
            ],
            setup.OutsideMasterminds);
        Assert.Equal(new VillainDeck(8, 1, 8, 3, 1, 0, MastermindTactics: 8), setup.VillainDeck);
        Assert.Equal(29, setup.VillainDeck.Total);
        Assert.Null(setup.Stacks.Wounds);
        Assert.Contains(
            new RuleNote("Scheme draws 2 other Masterminds and shuffles 8 of their Tactics into the Villain Deck", "Card", null, FixtureName),
            setup.Notes);
    }

    [Fact]
    public void Tactics_into_the_Villain_Deck_follow_the_player_count()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(9, 0)));

        Assert.Equal("Test Tyrants", setup.Scheme.Name);
        Assert.Equal(["Loki", "Magneto", "Red Skull"], setup.OutsideMasterminds!.Select(other => other.Mastermind.Name));
        Assert.Equal(12, setup.VillainDeck.MastermindTactics);
        Assert.Equal(53, setup.VillainDeck.Total);
    }

    [Fact]
    public void A_setup_without_the_effect_draws_no_other_Masterminds()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(7, 3)));

        Assert.Empty(setup.OutsideMasterminds!);
        Assert.Equal(0, setup.VillainDeck.MastermindTactics);
    }

    [Fact]
    public void Own_Tactics_shuffled_into_the_Villain_Deck_count_in_it_and_draw_no_other_Masterminds()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(8, 0)));

        Assert.Equal(("Test Hideout", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Empty(setup.OutsideMasterminds!);
        Assert.Equal(new VillainDeck(8, 1, 8, 3, 1, 0, OwnTactics: 4), setup.VillainDeck);
        Assert.Equal(25, setup.VillainDeck.Total);
        Assert.Contains(new RuleNote("Scheme shuffles the 4 Tactics of its Mastermind into the Villain Deck", "Card", null, FixtureName), setup.Notes);
    }

    [Fact]
    public void A_Mastermind_set_aside_that_brings_its_Always_Leads_adds_that_group_as_an_extra_Villain_Group()
    {
        // Solo draws the Scheme, then Dr. Doom, then the set-aside Mastermind from the 3 others, each leading a group
        // other than Dr. Doom's. Loki, drawn first, brings Enemies of Asgard on top of Solo's one Villain Group.
        var random = new ScriptedRandom(9, 0, 0);

        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, random));

        Assert.Equal(("Test Drain", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(3, random.Options[2]);
        Assert.Equal("Loki", Assert.Single(setup.OutsideMasterminds!).Mastermind.Name);
        Assert.Contains("Enemies of Asgard", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(2, setup.VillainGroups.Count);
    }

    private static Mastermind Mastermind(string id) =>
        Catalog.Boxes.SelectMany(box => box.Masterminds).Single(mastermind => mastermind.Id == id);
}
