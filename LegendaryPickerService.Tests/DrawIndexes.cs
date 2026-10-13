using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Tests;

// Looks up the ScriptedRandom draws that give a named Scheme and Mastermind, from the catalog alone: the Scheme draw
// indexes the included Schemes its card allows at the player count, and the Mastermind draw the included Masterminds
// that Scheme doesn't exclude, both in catalog order. It skips the generator's other Mastermind filters (parts no
// included box supplies, rule editions), so every caller asserts the drawn names and a lookup gone wrong fails there.
internal static class DrawIndexes
{
    public static int Scheme(BoxCatalog catalog, IReadOnlyCollection<string> boxIds, int players, string scheme) =>
        catalog.Boxes.Where(box => boxIds.Contains(box.Id)).SelectMany(box => box.AllSchemes).DistinctBy(s => s.Id)
            .Where(s => s.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList().FindIndex(s => s.Name == scheme);

    public static int Mastermind(BoxCatalog catalog, IReadOnlyCollection<string> boxIds, string scheme, string mastermind)
    {
        var included = catalog.Boxes.Where(box => boxIds.Contains(box.Id)).ToList();
        var drawn = included.SelectMany(box => box.AllSchemes).First(s => s.Name == scheme);
        return included.SelectMany(box => box.AllMasterminds).DistinctBy(m => m.Id)
            .Where(m => !m.RuledOutBy(drawn))
            .ToList().FindIndex(m => m.Name == mastermind);
    }
}
