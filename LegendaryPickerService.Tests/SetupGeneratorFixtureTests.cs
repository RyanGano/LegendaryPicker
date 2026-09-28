using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// Rules the core box never exercises, checked against edited copies of the core box file.
public sealed class SetupGeneratorFixtureTests : IDisposable
{
    private readonly string _directory = Directory.CreateTempSubdirectory("legendary-fixture-").FullName;

    public void Dispose() => Directory.Delete(_directory, recursive: true);

    [Fact]
    public void A_Schemes_required_group_displaces_Always_Leads_when_only_one_slot_exists()
    {
        // Two players get one Villain Group slot. Secret Invasion requires Skrulls; Magneto leads Brotherhood.
        var generator = Generator(core => Row(core, 2)["villainGroups"] = 1);

        var setup = Assert.IsType<SetupResult>(generator.Generate(2, new ScriptedRandom(5, 2)));

        Assert.Equal("Secret Invasion of the Skrull Shapeshifters", setup.Scheme.Name);
        Assert.Equal("Magneto", setup.Mastermind.Name);
        Assert.Equal(["Skrulls"], setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(
            new RuleNote("Scheme requires Skrulls, so Magneto's Always Leads group Brotherhood is dropped", "D1",
                "https://boardgamegeek.com/thread/993341/article/12653573"),
            setup.Notes);
        Assert.DoesNotContain(setup.Notes, note => note.Citation == "R p.6");
    }

    [Fact]
    public void A_catalog_with_no_Scheme_allowed_at_the_player_count_returns_NoEligibleScheme()
    {
        var generator = Generator(core =>
        {
            foreach (var scheme in core["schemes"]!.AsArray())
            {
                scheme!["setup"]!["allowedPlayerCounts"] = new JsonObject
                {
                    ["value"] = new JsonArray(2, 3, 4, 5),
                    ["source"] = "R p.20",
                };
            }
        });
        var random = new ScriptedRandom();

        Assert.Equal(new NoEligibleScheme(1), generator.Generate(1, random));
        Assert.Empty(random.Options);
    }

    [Fact]
    public void A_catalog_with_no_Mastermind_returns_NoEligibleScheme_without_drawing()
    {
        var generator = Generator(core => core["masterminds"] = new JsonArray());
        var random = new ScriptedRandom();

        Assert.Equal(new NoEligibleScheme(2), generator.Generate(2, random));
        Assert.Empty(random.Options);
    }

    [Fact]
    public void A_Scheme_needing_more_Heroes_than_the_catalog_holds_is_dropped_before_the_draw()
    {
        // Secret Invasion asks for 16 Heroes; the core box holds 15.
        var generator = Generator(core =>
            Scheme(core, "core_scheme_secret-invasion-of-the-skrull-shapeshifters")["setup"]!["heroes"]![0]!["value"] = 16);
        var random = new ScriptedRandom();

        var setup = Assert.IsType<SetupResult>(generator.Generate(2, random));

        Assert.Equal(7, random.Options[0]);
        Assert.Equal("Legacy Virus", setup.Scheme.Name);
    }

    private SetupGenerator Generator(Action<JsonObject> edit)
    {
        var core = JsonNode.Parse(File.ReadAllText(Path.Combine(BoxCatalog.DefaultDirectory, "core.json")))!.AsObject();
        edit(core);
        File.WriteAllText(Path.Combine(_directory, "core.json"), core.ToJsonString());

        return new SetupGenerator(BoxCatalog.Load(_directory));
    }

    private static JsonObject Row(JsonObject core, int players) =>
        core["setup"]!["playerCounts"]!.AsArray().Single(row => (int)row!["players"]! == players)!.AsObject();

    private static JsonObject Scheme(JsonObject core, string id) =>
        core["schemes"]!.AsArray().Single(scheme => (string?)scheme!["id"] == id)!.AsObject();
}
