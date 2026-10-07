using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Tests;

// Every box file loads (BoxCatalog.Load checks its structure, ids, references, own-box requirements and names) and holds
// the cards the product holds. A new box adds its row here; its own test file covers only what is unique to it.
public class BoxDataTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    public static TheoryData<string, int, int, int, int, int> Rows => new()
    {
        { "ant-man", 5, 2, 0, 2, 4 },
        { "venom", 5, 2, 0, 2, 4 },
        { "dimensions", 5, 0, 2, 1, 0 },
        { "revelations", 9, 4, 2, 3, 4 },
        { "shield", 4, 2, 0, 2, 4 },
        { "heroes-of-asgard", 5, 2, 0, 2, 4 },
        { "the-new-mutants", 5, 2, 0, 2, 4 },
        { "into-the-cosmos", 9, 4, 2, 3, 4 },
        { "realm-of-kings", 5, 2, 0, 2, 4 },
        { "annihilation", 5, 2, 0, 2, 4 },
        { "messiah-complex", 8, 4, 2, 3, 4 },
        { "doctor-strange-and-the-shadows-of-nightmare", 5, 2, 0, 2, 4 },
        { "captain-america-75th-anniversary", 5, 2, 0, 2, 4 },
        { "champions", 5, 2, 0, 2, 4 },
        { "civil-war", 16, 7, 2, 5, 8 },
        { "core", 15, 7, 4, 4, 8 },
        { "dark-city", 17, 6, 2, 5, 8 },
        { "deadpool", 5, 2, 0, 2, 4 },
        { "fantastic-four", 5, 2, 0, 2, 4 },
        { "fear-itself", 6, 1, 0, 1, 3 },
        { "guardians-of-the-galaxy", 5, 2, 0, 2, 4 },
        { "marvel-studios-guardians-of-the-galaxy", 5, 2, 0, 2, 4 },
        { "marvel-studios-phase-1", 7, 5, 4, 3, 8 },
        { "noir", 5, 2, 0, 2, 4 },
        { "paint-the-town-red", 5, 2, 0, 2, 4 },
        { "secret-wars-volume-1", 14, 6, 3, 4, 8 },
        { "secret-wars-volume-2", 16, 6, 3, 4, 8 },
        { "spider-man-homecoming", 5, 2, 0, 2, 4 },
        { "villains", 15, 7, 4, 4, 8 },
        { "world-war-hulk", 15, 7, 3, 6, 8 },
        { "x-men", 15, 7, 5, 6, 8 },
    };

    [Theory]
    [MemberData(nameof(Rows))]
    public void Box_file_loads_with_its_cards(string boxId, int heroes, int villainGroups, int henchmanGroups, int masterminds, int schemes)
    {
        var box = Catalog.Boxes.Single(b => b.Id == boxId);

        Assert.Equal(heroes, box.AllHeroes.Count());
        Assert.Equal(villainGroups, box.AllVillainGroups.Count());
        Assert.Equal(henchmanGroups, box.AllHenchmanGroups.Count());
        Assert.Equal(masterminds, box.AllMasterminds.Count());
        Assert.Equal(schemes, box.AllSchemes.Count());
    }

    [Fact]
    public void Every_box_file_has_a_row()
    {
        Assert.Equal(Catalog.Boxes.Select(b => b.Id).Order(), Rows.Select(row => (string)row[0]).Order());
    }

    // A Mastermind whose setup offers its Epic side has it recorded in `epic`, and the record is only on such a Mastermind (#151).
    [Fact]
    public void Every_Mastermind_offering_its_Epic_side_records_it()
    {
        var offers = Catalog.Boxes.SelectMany(box => box.Masterminds)
            .Where(m => m.Setup?.Steps?.Any(step => step.Label.Contains("play the Epic side")) ?? false)
            .Select(m => m.Id).Order().ToList();
        var recorded = Catalog.Boxes.SelectMany(box => box.Masterminds).Where(m => m.Epic is not null).Select(m => m.Id).Order().ToList();

        Assert.Equal(offers, recorded);
        Assert.Equal(36, recorded.Count);
    }
}
