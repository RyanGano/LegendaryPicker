using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Annihilation box file (Data/Boxes/annihilation.json), drawn with the core box.
public class AnnihilationTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "annihilation"];

    // Sneak Attack the Heroes' Homes puts 3 Hero cards and 3 Wounds in each starting deck, so the setup lays out Wounds.
    [Fact]
    public void Sneak_Attack_the_Heroes_Homes_adds_3_Heroes_and_3_Wounds_to_each_starting_deck()
    {
        var setup = Draw(2, "Sneak Attack the Heroes' Homes");

        Assert.Contains(setup.Moves, move => move is { Card: CardKind.Hero, To: Pile.StartingDecks, Count: 3 });
        Assert.Contains(setup.Moves, move => move is { Card: CardKind.Wound, To: Pile.StartingDecks, Count: 3 });
        Assert.NotNull(setup.Stacks.Wounds);
    }

    // Put Humanity on Trial sets 11 Bystanders beside the Scheme as the Galactic Jurors.
    [Fact]
    public void Put_Humanity_on_Trial_sets_11_Bystanders_beside_the_Scheme()
    {
        var setup = Draw(3, "Put Humanity on Trial");

        Assert.Equal(11, setup.VillainDeck.Twists);
        Assert.Contains(setup.Moves, move => move is { Card: CardKind.Bystander, To: Pile.BesideScheme, Count: 11 });
    }

    // Annihilus prints a Solo count on his Always Leads line: 6 Henchmen in place of Solo's 3.
    [Fact]
    public void Annihilus_puts_6_Henchmen_in_a_Solo_Villain_Deck()
    {
        var probe = new ScriptedRandom();
        Generator.Generate(1, Boxes, probe);
        var annihilus = Enumerable.Range(0, probe.Options[1])
            .First(m => Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, m))).Mastermind.Name == "Annihilus");
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, annihilus)));

        Assert.Equal(6, setup.VillainDeck.HenchmanCards);
        Assert.Contains(setup.Notes, note => note.Text == "Annihilus puts 6 Henchmen of each Henchman Group in the Villain Deck");
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var probe = new ScriptedRandom();
        Generator.Generate(players, Boxes, probe);
        var index = Enumerable.Range(0, probe.Options[0])
            .First(s => Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s))).Scheme.Name == scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
    }
}
