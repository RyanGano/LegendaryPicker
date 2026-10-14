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
        // The players choose their own Heroes (#259): the Hero Deck is listed as if no one chose, its last 2 Heroes fill-ins.
        Assert.Equal(2, setup.FillInHeroes);
        Assert.DoesNotContain(setup.Steps, step => step.Contains('{'));
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
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 1, "Midtown Bank Robbery");
        var annihilus = DrawIndexes.Mastermind(Catalog, Boxes, "Midtown Bank Robbery", "Annihilus");
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(scheme, annihilus)));
        Assert.Equal("Annihilus", setup.Mastermind.Name);

        Assert.Equal(6, setup.VillainDeck.HenchmanCards);
        Assert.Contains(setup.Notes, note => note.Text == "Annihilus puts 6 Henchmen of each Henchman Group in the Villain Deck");
    }

    // Its own Scheme and Mastermind are tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_and_Mastermind_rates_with_reasons_naming_them()
    {
        var scheme = DrawIndexes.Scheme(Catalog, Boxes, 2, "Put Humanity on Trial");
        var mastermind = DrawIndexes.Mastermind(Catalog, Boxes, "Put Humanity on Trial", "Annihilus");
        var result = Generator.Generate(2, Boxes, new ScriptedRandom(scheme, mastermind));

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(result, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Put Humanity on Trial's Twists"));
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Annihilus's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme)
    {
        var index = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index)));
        Assert.Equal(scheme, setup.Scheme.Name);
        return setup;
    }
}
