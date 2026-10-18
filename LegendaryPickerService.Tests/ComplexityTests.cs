using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// The complexity rating reads the rules a setup brings to learn, and a requested target repairs the draw toward it (#270, #329).
public sealed class ComplexityTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    private static Complexity Rate(params string[] boxes) =>
        Assert.IsType<SetupBody>(SetupResponse.From(
            new SetupGenerator(Catalog).Generate(2, boxes, new ScriptedRandom(0, 0)), Catalog)).Complexity;

    [Fact]
    public void The_core_box_alone_is_the_simplest_band()
    {
        var complexity = Rate("core");

        Assert.Equal((1, "Origin Story"), (complexity.Rating, complexity.Band));
    }

    [Fact]
    public void Several_expansions_rate_higher_than_the_core_box_alone()
    {
        Assert.True(Rate("core", "dark-city", "fear-itself", "world-war-hulk", "secret-wars-volume-1", "civil-war").Rating > Rate("core").Rating);
    }

    [Fact]
    public void A_Hero_that_needs_Shards_rates_higher_than_one_that_needs_nothing()
    {
        var meter = new ComplexityMeter(Catalog);
        var plain = new Hero("core_hero_a", "A", null, [], []);
        var shards = plain with { Uses = [new PartUse(Part.Shards, "Card")] };

        var without = meter.Score(meter.Inputs([plain]));
        var with = meter.Score(meter.Inputs([shards]));

        Assert.Empty(without.Reasons);
        Assert.Equal("Adds Shards", Assert.Single(with.Reasons));
        Assert.True(meter.Points(meter.Inputs([shards])) > meter.Points(meter.Inputs([plain])));
    }

    [Fact]
    public void Wounds_are_the_baseline_and_add_no_rule_load()
    {
        var meter = new ComplexityMeter(Catalog);
        var wounds = new Hero("core_hero_a", "A", null, [], [], Uses: [new PartUse(Part.Wounds, "Card")]);

        Assert.Equal(0, meter.Points(meter.Inputs([wounds])));
    }

    [Fact]
    public void A_multi_box_draw_at_a_target_of_1_is_repaired_to_the_simplest_band_and_keeps_its_Scheme()
    {
        // Save Humanity against Authoritarian Iron Man rates 2, Team-Up, drawn plainly from these boxes.
        string[] boxes = ["core", "dark-city", "fear-itself", "world-war-hulk", "secret-wars-volume-1", "civil-war"];
        SetupResult Draw(int? target) => Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, boxes, new ScriptedRandom(20), complexity: target));
        Assert.Equal(2, Assert.IsType<SetupBody>(SetupResponse.From(Draw(null), Catalog)).Complexity.Rating);

        var setup = Draw(1);

        Assert.Equal("Save Humanity", setup.Scheme.Name);
        var complexity = Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog, complexityTarget: 1)).Complexity;
        Assert.Equal((1, true), (complexity.Rating, complexity.Reached));
    }

    [Fact]
    public void A_part_a_stand_in_replaces_is_not_counted_as_an_added_stack()
    {
        var meter = new ComplexityMeter(Catalog);
        var bindings = new Hero("core_hero_a", "A", null, [], [], Uses: [new PartUse(Part.Bindings, "Card")]);

        var reasons = meter.Score(meter.Inputs([bindings], standIns: [new StandIn(Part.Bindings, "Card", Part.Wounds)])).Reasons;

        Assert.Equal(["Uses 1 stand-in part"], reasons);
    }

    [Fact]
    public async Task The_response_carries_the_complexity_and_the_requested_target()
    {
        var body = await Client(new ScriptedRandom())
            .GetFromJsonAsync<JsonObject>("/api/setup?players=2&complexity=1");

        Assert.Equal(1, (int?)body!["complexity"]!["target"]);
        Assert.InRange((int)body["complexity"]!["rating"]!, 1, 5);
    }

    [Fact]
    public async Task Without_a_target_the_response_has_no_target()
    {
        var body = await Client(new ScriptedRandom()).GetFromJsonAsync<JsonObject>("/api/setup?players=2");

        Assert.Null(body!["complexity"]!["target"]);
        Assert.Null(body["complexity"]!["reached"]);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("6")]
    [InlineData("simple")]
    public async Task A_complexity_outside_1_to_5_returns_a_validation_problem(string complexity)
    {
        var response = await Client(new ScriptedRandom()).GetAsync($"/api/setup?players=2&complexity={complexity}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal("complexity must be a whole number from 1 to 5.", (string?)problem!["errors"]!["complexity"]![0]);
    }

    private static HttpClient Client(IRandomSource random)
    {
        var host = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.AddSingleton(random)));
        return host.CreateClient();
    }
}
