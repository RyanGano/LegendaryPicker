using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Weapon X box file (Data/Boxes/weapon-x.json), a Revised expansion drawn with the What If...? base game (#214).
public class WeaponXTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["marvel-studios-what-if", "weapon-x"];

    // Go After Heroes' Loved Ones adds an extra Hero of a different Hero Name and sets aside a card of each Hero with 2
    // Bystanders under it (Card), so a 2-player setup of 6 Heroes sets aside 6 Hero cards and 12 Bystanders.
    [Fact]
    public void Loved_Ones_sets_aside_a_Hero_card_and_2_Bystanders_for_each_Hero()
    {
        var setup = Draw(2, "Go After Heroes' Loved Ones", "Sabretooth", epic: false);

        Assert.Equal(6, setup.Heroes.Count);
        Assert.Equal(6, setup.Heroes.Select(hero => hero.NamesOfHero[0]).Distinct().Count());
        Assert.Contains(new MovedCards(CardKind.Hero, Pile.HeroDeck, Pile.SetAside, 6, 6), setup.Moves);
        Assert.Contains(new MovedCards(CardKind.Bystander, Pile.Bystanders, Pile.SetAside, 12, 12), setup.Moves);
    }

    // The Scheme's one Hero with Wolverine in its name is Weapon X (Wolverine), the box's only one, and Sabretooth leads Berserkers.
    [Fact]
    public void Condition_Logan_into_Weapon_X_includes_exactly_one_Wolverine_Hero_and_lays_out_Wounds()
    {
        var setup = Draw(2, "Condition Logan into Weapon X", "Sabretooth", epic: false);

        Assert.Single(setup.Heroes, hero => hero.Name == "Weapon X (Wolverine)");
        Assert.Contains("Berserkers", setup.VillainGroups.Select(group => group.Name));
        Assert.NotNull(setup.Stacks.Wounds);
    }

    // Enraging Wounds join the Wound stack, so the 30 What If...? Wounds become 40 once Romulus uses Wounds.
    [Fact]
    public void Epic_Romulus_leads_Weapon_Plus_and_the_ten_Enraging_Wounds_join_the_Wound_stack()
    {
        var setup = Draw(2, "Wipe Heroes' Memories", "Romulus", epic: true);

        Assert.Equal("Epic Romulus", setup.Mastermind.Name);
        Assert.Contains("Weapon Plus", setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(40, setup.Stacks.Wounds);
    }

    // Its own Scheme is tagged for difficulty (#269).
    [Fact]
    public void A_draw_of_its_own_Scheme_rates_with_a_reason_naming_it()
    {
        var setup = Draw(2, "Go After Heroes' Loved Ones", "Sabretooth", epic: false);

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Go After Heroes' Loved Ones's Twists"));
    }

    private static SetupResult Draw(int players, string scheme, string mastermind, bool epic)
    {
        var included = Catalog.Boxes.Where(box => Boxes.Contains(box.Id)).ToList();
        var s = included.SelectMany(box => box.AllSchemes).ToList().FindIndex(candidate => candidate.Name == scheme);
        var m = included.SelectMany(box => box.AllMasterminds).ToList().FindIndex(candidate => candidate.Name == mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m), epic));
        Assert.Equal((scheme, epic ? "Epic " + mastermind : mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
