using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// A requested target difficulty weights the draw among the options the rules allow (#267).
public sealed class DifficultyTargetTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    // Every draw asks for a number below 1000 with a target, so 500 is the middle of the cumulative weights each time.
    private static ScriptedRandom Middle() => new(Enumerable.Repeat(500, 40).ToArray());

    private static SetupResult Draw(int? difficulty, IRandomSource random) =>
        Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, ["core"], random, difficulty: difficulty));

    [Fact]
    public void The_same_scripted_number_picks_the_easier_option_for_a_target_of_1_and_the_harder_for_5()
    {
        var expected = new[] { 1.5, 4.5 };

        var easy = new DifficultyWeights(Catalog, DifficultyTuning.Default, 1, []).Pick(new ScriptedRandom(500), expected);
        var hard = new DifficultyWeights(Catalog, DifficultyTuning.Default, 5, []).Pick(new ScriptedRandom(500), expected);

        Assert.Equal((0, 1), (easy, hard));
    }

    [Fact]
    public void Four_seeds_at_a_target_of_1_rate_lower_in_total_than_at_5()
    {
        int Total(int target) => Enumerable.Range(0, 4).Sum(seed =>
            Rating(Draw(target, new CyclingRandom(Enumerable.Range(0, 30).Select(i => (seed * 7919 + i * 104729 + 13) % 100003).ToArray()))).Rating);

        Assert.True(Total(1) < Total(5), $"target 1 {Total(1)}, target 5 {Total(5)}");
    }

    [Fact]
    public void A_target_that_no_draw_reaches_still_returns_a_legal_setup_with_its_real_rating()
    {
        // The last option in every draw, at the hardest target.
        var setup = Draw(5, new ScriptedRandom(Enumerable.Repeat(999, 40).ToArray()));

        Assert.Equal(5, setup.Heroes.Count);
        Assert.InRange(Rating(setup).Rating, 1, 5);
    }

    [Fact]
    public void Without_a_target_no_draw_is_weighted()
    {
        var random = new ScriptedRandom();

        Draw(null, random);

        Assert.DoesNotContain(1000, random.Options);
    }

    [Fact]
    public async Task The_response_carries_the_requested_target_and_the_real_rating()
    {
        var body = await Client(Middle()).GetFromJsonAsync<JsonObject>("/api/setup?players=2&difficulty=1");

        Assert.Equal(1, (int?)body!["difficulty"]!["target"]);
        Assert.InRange((int)body["difficulty"]!["rating"]!, 1, 5);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("6")]
    [InlineData("hard")]
    public async Task A_difficulty_outside_1_to_5_returns_a_validation_problem(string difficulty)
    {
        var response = await Client(Middle()).GetAsync($"/api/setup?players=2&difficulty={difficulty}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal("difficulty must be a whole number from 1 to 5.", (string?)problem!["errors"]!["difficulty"]![0]);
    }

    private static Difficulty Rating(SetupResult setup) =>
        Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog)).Difficulty;

    private static HttpClient Client(IRandomSource random)
    {
        var host = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.AddSingleton(random)));
        return host.CreateClient();
    }
}
