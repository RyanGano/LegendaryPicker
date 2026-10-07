using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Marvel Studios' Guardians of the Galaxy box file (Data/Boxes/marvel-studios-guardians-of-the-galaxy.json), drawn with the core box.
public class MarvelStudiosGuardiansTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "marvel-studios-guardians-of-the-galaxy"];

    // Ego's Always Leads allows any group and adds one, even in Solo, where First Edition otherwise ignores Always Leads.
    [Fact]
    public void Ego_adds_a_Villain_Group_even_in_Solo()
    {
        var setup = Draw(1, "Provoke the Sovereign War Fleet", "Ego, the Living Planet");

        Assert.Equal(3, setup.VillainGroups.Count);
    }

    // Star-Lord's Awesome Mix Tape doubles the groups, uses half the cards of each, and needs a Guardians Hero among its 7.
    [Fact]
    public void Awesome_Mix_Tape_doubles_the_groups_and_halves_their_cards()
    {
        var setup = Draw(2, "Star-Lord's Awesome Mix Tape", "Ronan the Accuser");

        Assert.Equal(4, setup.VillainGroups.Count);
        Assert.Equal(2, setup.HenchmanGroups.Count);
        Assert.Equal(7, setup.Heroes.Count);
        Assert.Contains(setup.Heroes, hero => hero.Team == "guardians-of-the-galaxy_term_guardians-of-the-galaxy");
        Assert.Equal(4 * 4, setup.VillainDeck.VillainCards);
        Assert.Equal(2 * 5, setup.VillainDeck.HenchmanCards);
    }

    // Draws the box's Scheme and Mastermind by name: the box's Schemes and Masterminds follow the core box's in catalog order.
    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var included = Catalog.Boxes.Where(box => Boxes.Contains(box.Id)).ToList();
        var s = included.SelectMany(box => box.AllSchemes)
            .Where(candidate => candidate.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = included.SelectMany(box => box.AllMasterminds).ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal(mastermind, setup.Mastermind.Name);
        return setup;
    }
}
