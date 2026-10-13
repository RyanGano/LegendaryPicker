using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Legendary Second Edition box file (Data/Boxes/second-edition.json), a base game on the Revised ruleset (#215).
public class SecondEditionTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["second-edition"];

    // Enshrouded Identity: 3 Twists plus 1 per player, and 3 Officers set aside as Bodyguards next to the Mastermind space,
    // not beside the Scheme (Card).
    [Fact]
    public void Enshrouded_Identity_adds_a_Twist_per_player_and_sets_three_Officers_aside_as_Bodyguards()
    {
        var setup = Draw(4, "Enshrouded Identity", "Red Skull, Hydra Overlord");

        Assert.Equal(7, setup.VillainDeck.Twists);
        Assert.Contains(new MovedCards(CardKind.Officer, Pile.Officers, Pile.SetAside, 3, 3), setup.Moves);
        Assert.Equal(5, setup.Heroes.Count);
        Assert.Equal(new PlayerDeck(8, 4), setup.PlayerDeck);
    }

    // Replace Earth's Leaders with Killbots: 10 Twists in the Villain Deck and 1 more beside the Scheme, the box's 11 Twists.
    [Fact]
    public void Killbots_keeps_one_extra_Twist_beside_the_Scheme()
    {
        var setup = Draw(2, "Replace Earth's Leaders with Killbots (Second Edition)", "Loki (Second Edition)");

        Assert.Equal(10, setup.VillainDeck.Twists);
        Assert.Equal(1, setup.TwistsBesideScheme);
    }

    // Doctor Octopus always leads any Sinister group, and the box holds two: one of them is always in the Villain Deck.
    [Fact]
    public void Doctor_Octopus_brings_a_Sinister_Villain_Group()
    {
        var setup = Draw(2, "Portals to the Dark Dimension (Second Edition)", "Doctor Octopus");

        Assert.Contains(setup.VillainGroups, group => group.Name is "Sinister Spider-Foes" or "Sinister Syndicate");
    }

    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var box = Catalog.Boxes.Single(b => b.Id == "second-edition");
        var s = box.AllSchemes.ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = box.AllMasterminds.ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
