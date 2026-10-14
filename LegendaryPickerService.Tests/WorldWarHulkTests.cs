using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The World War Hulk box file (Data/Boxes/world-war-hulk.json), drawn with the core box.
public class WorldWarHulkTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "world-war-hulk"];

    // The 10 Spikes all go to the Infected Deck, so the group is never one of the Villain Deck's Henchman Groups.
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void Cytoplasm_Spike_Invasion_sets_20_Bystanders_and_all_10_Spikes_beside_it(int players)
    {
        var setup = Draw(players, "Cytoplasm Spike Invasion", "King Hulk, Sakaarson");

        var beside = Assert.Single(setup.CardsBeside);
        Assert.Equal(("Cytoplasm Spikes", 10, 0), (beside.Group.Name, beside.Count, beside.FromVillainDeck));
        Assert.DoesNotContain("Cytoplasm Spikes", setup.HenchmanGroups.Select(group => group.Name));
        var moved = Assert.Single(setup.Moves);
        Assert.Equal((CardKind.Bystander, Pile.BesideScheme, 20), (moved.Card, moved.To, moved.Total));
        Assert.Contains("Shuffle the 20 Bystanders and 10 Cytoplasm Spikes into a face-down Infected Deck", setup.Steps);
    }

    [Theory]
    [InlineData("Mutating Gamma Rays", "Lay the extra Hulk Hero's 14 cards face up as the Mutation Pile")]
    [InlineData("Shoot Hulk into Space", "Shuffle the extra Hulk Hero's 14 cards into a face-down Hulk Deck")]
    public void Scheme_sets_aside_an_extra_Hero_with_Hulk_in_its_Hero_Name(string scheme, string step)
    {
        var setup = Draw(2, scheme, "The Sentry");

        var outside = Assert.Single(setup.OutsideHeroes);
        Assert.Contains("Hulk", outside.Hero.Name);
        Assert.Equal((Pile.SetAside, 14), (outside.To, outside.Cards));
        Assert.DoesNotContain(outside.Hero, setup.Heroes);
        Assert.Contains(step, setup.Steps);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void World_War_Hulk_sets_3_other_Masterminds_aside_as_Lurking(int players)
    {
        var setup = Draw(players, "World War Hulk", "M.O.D.O.K.");

        var lurking = setup.OutsideMasterminds!;
        Assert.Equal(3, lurking.Count);
        Assert.All(lurking, other => Assert.Equal((Pile.SetAside, (int?)null), (other.To, other.Tactics)));
        Assert.Equal(4, lurking.Select(other => other.Mastermind.Name).Append(setup.Mastermind.Name).Distinct().Count());
        Assert.Contains("Keep the 3 extra Masterminds out of play as Lurking Masterminds", setup.Steps);
        Assert.Contains("Give each of the 4 Masterminds 2 random Tactics", setup.Steps);
    }

    [Fact]
    public void A_draw_with_Subjugate_with_Obedience_Disks_and_The_Sentry_rates_with_reasons_naming_them()
    {
        var setup = Draw(2, "Subjugate with Obedience Disks", "The Sentry");

        var difficulty = Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog)).Difficulty;
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("Subjugate with Obedience Disks's Twists"));
        Assert.Contains(difficulty.Reasons, reason => reason.StartsWith("The Sentry's Master Strikes"));
    }

    private static SetupResult Draw(int players, string scheme, string mastermind)
    {
        var s = DrawIndexes.Scheme(Catalog, Boxes, players, scheme);
        var m = DrawIndexes.Mastermind(Catalog, Boxes, scheme, mastermind);
        var setup = Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(s, m)));
        Assert.Equal((scheme, mastermind), (setup.Scheme.Name, setup.Mastermind.Name));
        return setup;
    }
}
