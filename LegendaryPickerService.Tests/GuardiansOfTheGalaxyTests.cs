using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Guardians of the Galaxy box file (Data/Boxes/guardians-of-the-galaxy.json), drawn with the core box. Catalog order puts its cards
// after the core box's, so at 2–5 players its Schemes are 8 Forge the Infinity Gauntlet, 9 Intergalactic Kree
// Nega-Bomb, 10 The Kree-Skrull War and 11 Unite the Shards; Solo allows 6 core Schemes, so there they are 6 to
// 9. Its Masterminds are 4 Supreme Intelligence of the Kree and 5 Thanos.
public class GuardiansOfTheGalaxyTests
{
    private const string GuardiansName = "Guardians of the Galaxy";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string SchemeFirst = "https://github.com/RyanGano/LegendaryPicker/issues/138";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Guardians = Catalog.Boxes.Single(box => box.Id == "guardians-of-the-galaxy");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "guardians-of-the-galaxy"];

    private static readonly string[] SchemeNames =
    [
        "Forge the Infinity Gauntlet",
        "Intergalactic Kree Nega-Bomb",
        "The Kree-Skrull War",
        "Unite the Shards",
    ];

    private const int SupremeIntelligence = 4;
    private const int Thanos = 5;

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with the Supreme Intelligence (who leads Kree
    // Without the core box's Skrulls The Kree-Skrull War can't be completed, so it is never drawn.
    // Without the core box the Kree-Skrull War is still drawn: the player owns the core box's Skrulls, so they fill a
    // Villain Group slot and the checklist names the box to pull them from (D-scheme-first, #138). Kree Starforce gives
    // Wounds, which no included box has, so the core box's Wound stack is laid out too. Guardians of the Galaxy sorts
    // before Villains, so the Kree-Skrull War is Scheme 2; a Guardians Mastermind would need First Edition rules, so
    // the Mastermind draw is from the Villains Commanders.
    [Fact]
    public void Without_the_core_box_the_Kree_Skrull_War_uses_the_core_box_Skrulls()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, ["villains", "guardians-of-the-galaxy"], new ScriptedRandom(2, 0)));

        Assert.Equal("The Kree-Skrull War", setup.Scheme.Name);
        Assert.StartsWith("villains_", setup.Mastermind.Id);
        Assert.Equal(["Kree Starforce", "Skrulls"], setup.VillainGroups.Take(2).Select(group => group.Name));
        Assert.Equal(30, setup.Stacks.Wounds);
        Assert.Contains(new RuleNote($"Scheme requires Skrulls, from {CoreName}, which isn't included", "D-scheme-first", SchemeFirst, GuardiansName), setup.Notes);
        Assert.Contains(new RuleNote($"No included box has Wound cards: the setup uses those of {CoreName}", "D-scheme-first", SchemeFirst, "Legendary: Villains"), setup.Notes);

        var skrulls = Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog)).VillainGroups.Single(group => group.Name == "Skrulls");
        Assert.Equal((CoreName, true), (skrulls.Box, skrulls.NotIncluded));
    }

    [Fact]
    public void Unite_the_Shards_lays_out_all_the_Shard_tokens_rather_than_a_count_of_30()
    {
        var setup = Draw(3, "Unite the Shards", SupremeIntelligence);

        Assert.Equal(8, setup.VillainDeck.Twists);
        Assert.Equal(18, setup.Stacks.Shards);
        Assert.Equal(["Put all the Shard tokens from the included boxes in the supply"], setup.Steps);
    }

    // Shards are laid out only when a drawn card uses them (D-uses): Legacy Virus with Dr. Doom and the first core
    // Heroes in Solo uses none, so the Shard supply is not laid out, and the checklist says nothing of it (#148).
    [Fact]
    public void A_setup_whose_cards_use_no_Shards_lays_out_no_Shard_supply()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(1, Boxes, new ScriptedRandom(0, 0)));

        Assert.Equal(("Legacy Virus", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Null(setup.Stacks.Shards);
        Assert.DoesNotContain(setup.Notes, note => note.Text.StartsWith("Leave out", StringComparison.Ordinal));
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }
}
