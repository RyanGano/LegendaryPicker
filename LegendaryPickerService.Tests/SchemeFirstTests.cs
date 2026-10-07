using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Scheme-first drawing (owner decision D-scheme-first, #138): every included Scheme its card allows at the player count
// can be drawn, then a Mastermind it doesn't exclude, and every requirement is met with no check per draw.
public sealed class SchemeFirstTests : IDisposable
{
    private const string CoreName = "Marvel Legendary First Edition core box";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly SetupGenerator Generator = new(Catalog);

    private readonly string _directory = Directory.CreateTempSubdirectory("legendary-scheme-first-").FullName;

    public void Dispose() => Directory.Delete(_directory, recursive: true);

    // Each box's Schemes, with each base game the box can be drawn with: the fewest cards any setup holding the box
    // draws from. Every Scheme is drawn at every player count its card allows, with each Mastermind it can be paired
    // with, and the rest of the draw is the first option each time.
    public static TheoryData<string, string> BoxesWithBaseGames()
    {
        var data = new TheoryData<string, string>();
        foreach (var box in Catalog.Boxes)
        {
            foreach (var baseGame in Catalog.Boxes.Where(other => other.IsBaseGame))
            {
                if (box == baseGame || (!box.IsBaseGame && Generator.CheckBoxes([baseGame.Id, box.Id]) is null))
                {
                    data.Add(box.Id, baseGame.Id);
                }
            }
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(BoxesWithBaseGames))]
    public void Every_Scheme_is_drawn_at_every_player_count_its_card_allows(string boxId, string baseGameId)
    {
        string[] boxes = boxId == baseGameId ? [boxId] : [baseGameId, boxId];
        var included = Catalog.Boxes.Where(box => boxes.Contains(box.Id)).ToList();
        foreach (var players in Enumerable.Range(BoxCatalog.MinPlayers, BoxCatalog.MaxPlayers - BoxCatalog.MinPlayers + 1))
        {
            var schemes = included.SelectMany(box => box.AllSchemes).DistinctBy(scheme => scheme.Id)
                .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
                .ToList();
            foreach (var scheme in Catalog.Boxes.Single(box => box.Id == boxId).AllSchemes.Where(schemes.Contains))
            {
                var probe = new ScriptedRandom(schemes.IndexOf(scheme));
                Assert.IsType<SetupResult>(Generator.Generate(players, boxes, probe));
                for (var mastermind = 1; mastermind < probe.Options[1]; mastermind++)
                {
                    var setup = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new ScriptedRandom(schemes.IndexOf(scheme), mastermind)));
                    Assert.Equal(scheme, setup.Scheme);
                }
            }
        }
    }

    // A Scheme excludes a Mastermind when it sets every card of the group the Mastermind always leads beside it, at a
    // player count it allows; the box files record each one when the box is entered, so the draw computes nothing.
    [Fact]
    public void Each_Schemes_excluded_Masterminds_are_those_its_card_data_rules_out()
    {
        var masterminds = Catalog.Boxes.SelectMany(box => box.Masterminds).ToList();
        foreach (var box in Catalog.Boxes)
        {
            foreach (var scheme in box.Schemes)
            {
                var allowed = scheme.Setup.AllowedPlayerCounts?.Value ?? Enumerable.Range(BoxCatalog.MinPlayers, BoxCatalog.MaxPlayers - BoxCatalog.MinPlayers + 1).ToArray();
                bool AllBeside(string groupId, GroupType type) => (scheme.Setup.CardsBeside ?? [])
                    .Where(beside => beside.GroupId == groupId && beside.Card is null)
                    .Any(beside => allowed.Any(players =>
                    {
                        var count = beside.Count.SingleOrDefault(value => value.Players?.Contains(players) ?? true)?.Value ?? 0;
                        var size = type == GroupType.Villain ? box.Components.VillainGroupCards.Value : box.Components.HenchmanGroupCards.Value;
                        return (beside.PerPlayer ? count * players : count) >= size;
                    }));

                var derived = masterminds
                    .Where(mastermind => (mastermind.AlwaysLeads is { } leads && AllBeside(leads.GroupId, leads.GroupType))
                        || (mastermind.AlsoLeads is { } also && also.GroupIds.All(id => AllBeside(id, also.GroupType))))
                    .Select(mastermind => mastermind.Id)
                    .Order();

                Assert.Equal(derived, (scheme.ExcludesMasterminds ?? []).Select(exclusion => exclusion.MastermindId).Order());
            }
        }
    }

    [Fact]
    public void Clash_of_the_Monsters_Unleashed_excludes_Fin_Fang_Foom_on_its_card()
    {
        var clash = Catalog.Boxes.SelectMany(box => box.Schemes).Single(scheme => scheme.Name == "Clash of the Monsters Unleashed");

        Assert.Equal([new ExcludedMastermind("champions_mastermind_fin-fang-foom", "Card")], clash.ExcludesMasterminds);
    }

    // Using Fixtures/Boxes, whose Test Heist requires the core box's HYDRA with otherBox, beside a made-up base game,
    // Frontier: a copy of the core box with its own ids and names. With Frontier and the fixture included and the core
    // box not, the fixture's Scheme is draw 0 and Frontier's Dr. Doom Mastermind draw 1.
    [Fact]
    public void A_required_group_from_a_box_that_is_not_included_is_still_in_the_setup_and_names_its_box()
    {
        var catalog = FrontierCatalog(substitute: false);

        var setup = Assert.IsType<SetupResult>(new SetupGenerator(catalog).Generate(2, ["frontier", "fixture"], new ScriptedRandom(0, 1)));

        Assert.Equal("Test Heist", setup.Scheme.Name);
        Assert.Equal("HYDRA", setup.VillainGroups[0].Name);
        Assert.Contains(
            new RuleNote($"Scheme requires HYDRA, from {CoreName}, which isn't included", "R p.4", "https://example.test/fixture-rules.pdf", "Fixture Expansion"),
            setup.Notes);
        var hydra = Assert.IsType<SetupBody>(SetupResponse.From(setup, catalog)).VillainGroups[0];
        Assert.Equal((CoreName, true), (hydra.Box, hydra.NotIncluded));
    }

    // With a substitute, a group drawn from the included ones takes the required group's slot instead.
    [Fact]
    public void A_required_group_with_a_substitute_is_replaced_by_an_included_group_when_its_box_is_not_included()
    {
        var catalog = FrontierCatalog(substitute: true);

        var setup = Assert.IsType<SetupResult>(new SetupGenerator(catalog).Generate(2, ["frontier", "fixture"], new ScriptedRandom(0, 1, 2)));

        Assert.DoesNotContain("HYDRA", setup.VillainGroups.Select(group => group.Name));
        var standIn = setup.VillainGroups[0];
        Assert.StartsWith("Frontier ", standIn.Name);
        Assert.Contains(
            new RuleNote($"Scheme uses {standIn.Name} in place of HYDRA: {CoreName} isn't included", "R p.5", "https://example.test/fixture-rules.pdf", "Fixture Expansion"),
            setup.Notes);
        Assert.All(Assert.IsType<SetupBody>(SetupResponse.From(setup, catalog)).VillainGroups, group => Assert.False(group.NotIncluded));
    }

    // With the core box included, the required group is the ordinary included one.
    [Fact]
    public void A_required_group_from_another_included_box_is_drawn_as_usual()
    {
        var catalog = FrontierCatalog(substitute: true);

        var setup = Assert.IsType<SetupResult>(new SetupGenerator(catalog).Generate(2, ["core", "fixture"], new ScriptedRandom(8, 0)));

        Assert.Equal("Test Heist", setup.Scheme.Name);
        Assert.Contains("HYDRA", setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme requires HYDRA", "R p.3", "https://example.test/fixture-rules.pdf", "Fixture Expansion"), setup.Notes);
    }

    // A box the setup includes that reprints the required group holds it, so the group is an ordinary included one and the
    // checklist sends the player to that box rather than to the core box (#150).
    [Fact]
    public void A_required_group_an_included_box_reprints_is_drawn_from_that_box()
    {
        var catalog = FrontierCatalog(substitute: false, reprintsHydra: true);

        var setup = Assert.IsType<SetupResult>(new SetupGenerator(catalog).Generate(2, ["frontier", "fixture"], new ScriptedRandom(0, 1)));

        Assert.Equal("Test Heist", setup.Scheme.Name);
        Assert.Equal("HYDRA", setup.VillainGroups[0].Name);
        Assert.Contains(new RuleNote("Scheme requires HYDRA", "R p.3", "https://example.test/fixture-rules.pdf", "Fixture Expansion"), setup.Notes);
        var hydra = Assert.IsType<SetupBody>(SetupResponse.From(setup, catalog)).VillainGroups[0];
        Assert.Equal(("Frontier", false), (hydra.Box, hydra.NotIncluded));
    }

    // The real core box, the fixture expansion, its required group given a substitute when asked, and Frontier, which
    // reprints the core box's HYDRA when asked.
    private BoxCatalog FrontierCatalog(bool substitute, bool reprintsHydra = false)
    {
        var core = File.ReadAllText(Path.Combine(BoxCatalog.DefaultDirectory, "core.json"));
        File.WriteAllText(Path.Combine(_directory, "core.json"), core);

        var fixture = JsonNode.Parse(File.ReadAllText(Path.Combine(MultiBoxSetupTests.FixtureDirectory, "fixture.json")))!.AsObject();
        if (substitute)
        {
            fixture["schemes"]![0]!["setup"]!["requiredGroups"]![0]!["otherBox"]!["substitute"] = "R p.5";
        }

        File.WriteAllText(Path.Combine(_directory, "fixture.json"), fixture.ToJsonString());

        var frontier = JsonNode.Parse(core.Replace("\"core_", "\"frontier_"))!.AsObject();
        frontier["id"] = "frontier";
        frontier["name"] = "Frontier";
        if (reprintsHydra)
        {
            frontier["reprints"] = new JsonObject { ["value"] = new JsonArray("core_villain_hydra"), ["source"] = "R p.1" };
        }
        foreach (var list in new[] { "heroes", "villainGroups", "henchmanGroups", "masterminds", "schemes" })
        {
            foreach (var entry in frontier[list]!.AsArray())
            {
                entry!["name"] = $"Frontier {entry["name"]}";
            }
        }

        File.WriteAllText(Path.Combine(_directory, "frontier.json"), frontier.ToJsonString());
        return BoxCatalog.Load(_directory);
    }
}
