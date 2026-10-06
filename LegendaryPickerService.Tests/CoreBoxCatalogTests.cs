using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Tests;

// Pins the First Edition core box's setup rules to the rulebook
// and its Secret Invasion Scheme.
public class CoreBoxCatalogTests
{
    private static readonly Box Core = BoxCatalog.Load(BoxCatalog.DefaultDirectory)
        .Boxes.Single(box => box.Id == "core");

    [Fact]
    public void Secret_Invasion_uses_6_Heroes_requires_Skrulls_and_moves_12_Hero_cards()
    {
        var setup = SchemeNamed("Secret Invasion of the Skrull Shapeshifters").Setup;

        var heroes = Assert.Single(setup.Heroes!);
        Assert.Null(heroes.Players);
        Assert.Equal(6, heroes.Value);

        var required = Assert.Single(setup.RequiredGroups!);
        Assert.Equal(GroupType.Villain, required.GroupType);
        Assert.Equal("Skrulls", GroupName(required.GroupId, required.GroupType));

        var move = Assert.Single(setup.Moves!);
        Assert.Equal(CardKind.Hero, move.Card);
        Assert.Equal(Pile.VillainDeck, move.To);
        Assert.False(move.PerPlayer);
        var count = Assert.Single(move.Count);
        Assert.Null(count.Players);
        Assert.Equal(12, count.Value);
    }

    [Fact]
    public void Standard_setup_table_matches_the_rulebook()
    {
        Assert.Equal(
            [(2, 2, 1, 2), (3, 3, 1, 8), (4, 3, 2, 8), (5, 4, 2, 12)],
            Core.Setup!.PlayerCounts.Select(row => (row.Players, row.VillainGroups, row.HenchmanGroups, row.Bystanders)));
    }

    [Fact]
    public void Solo_setup_matches_the_rulebook()
    {
        var solo = Core.Setup!.Solo;

        Assert.Equal(3, solo.Heroes.Value);
        Assert.Equal(1, solo.VillainGroups.Value);
        Assert.Equal(1, solo.HenchmanGroups.Value);
        Assert.Equal(3, solo.HenchmanCards.Value);
        Assert.Equal(1, solo.Bystanders.Value);
        Assert.Equal(1, solo.MasterStrikes.Value);
        Assert.True(solo.IgnoresAlwaysLeads.Value);
        Assert.Equal("R p.20", solo.IgnoresAlwaysLeads.Source);
        Assert.Equal([new PlayRule("After each Twist, KO a Hero costing 6 or less from the HQ", "R p.20")], solo.PlayRules);
    }

    private static Scheme SchemeNamed(string name) => Core.Schemes.Single(scheme => scheme.Name == name);

    private static string GroupName(string id, GroupType type) => type switch
    {
        GroupType.Villain => Core.VillainGroups.Single(group => group.Id == id).Name,
        GroupType.Henchman => Core.HenchmanGroups.Single(group => group.Id == id).Name,
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };
}
