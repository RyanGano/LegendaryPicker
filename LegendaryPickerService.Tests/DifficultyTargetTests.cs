using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// A requested target difficulty repairs the plain draw's open picks toward that band (#267, #329).
public sealed class DifficultyTargetTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    // The core box's Super Hero Civil War against Dr. Doom, which rates 3, Heavy Hitter, drawn plainly.
    private static ScriptedRandom CivilWar() => new(6);

    private static SetupResult Draw(int? difficulty, IRandomSource random, params string[] boxes) =>
        Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, boxes.Length > 0 ? boxes : ["core"], random, difficulty: difficulty));

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void A_draw_that_rates_3_is_repaired_into_the_target_band_and_keeps_its_Scheme_and_Mastermind(int target)
    {
        Assert.Equal(3, Rating(Draw(null, CivilWar())).Rating);

        var setup = Draw(target, CivilWar());

        Assert.Equal(("Super Hero Civil War", "Dr. Doom"), (setup.Scheme.Name, setup.Mastermind.Name));
        var difficulty = Rating(setup, target);
        Assert.Equal((target, true, null), (difficulty.Rating, difficulty.Reached, difficulty.Note));
    }

    [Fact]
    public void A_draw_already_in_the_target_band_is_the_plain_draw()
    {
        var plain = Draw(null, CivilWar());

        var setup = Draw(3, CivilWar());

        Assert.Equal(plain.Heroes.Concat<ICard>(plain.VillainGroups).Concat(plain.HenchmanGroups).Select(card => card.Id),
            setup.Heroes.Concat<ICard>(setup.VillainGroups).Concat(setup.HenchmanGroups).Select(card => card.Id));
    }

    [Fact]
    public void A_target_the_repair_does_not_reach_returns_the_nearest_band_it_found_and_says_so()
    {
        // The Legacy Virus against Dr. Doom comes no closer to Secret Wars than Cosmic Threat in the core box.
        var difficulty = Rating(Draw(5, new ScriptedRandom(0)), 5);

        Assert.Equal(
            (4, false, "The closest to Secret Wars the draw found with this Scheme is Cosmic Threat."),
            (difficulty.Rating, difficulty.Reached, difficulty.Note));
    }

    [Fact]
    public void A_Scheme_with_Hero_rules_keeps_them_when_the_repair_swaps_its_Heroes()
    {
        // Fall of the Hulks needs exactly 2 Heroes with "Hulk" in their Hero Names.
        var plain = Draw(null, new ScriptedRandom(10), "core", "world-war-hulk");

        var setup = Draw(5, new ScriptedRandom(10), "core", "world-war-hulk");

        Assert.Equal("Fall of the Hulks", setup.Scheme.Name);
        Assert.NotEqual(plain.Heroes.Select(hero => hero.Id), setup.Heroes.Select(hero => hero.Id));
        Assert.Equal(5, Rating(setup, 5).Rating);
        Assert.Equal(2, setup.Heroes.Count(hero => hero.HasInHeroName("Hulk")));
    }

    [Fact]
    public void Four_seeds_keep_the_plain_draws_Scheme_and_land_in_band_or_say_how_close_they_came()
    {
        foreach (var boxes in new[] { new[] { "core" }, ["core", "dark-city", "secret-wars-volume-1"] })
        {
            foreach (var target in new[] { 1, 5 })
            {
                for (var seed = 0; seed < 4; seed++)
                {
                    CyclingRandom Seeded() => new(Enumerable.Range(0, 30).Select(i => (seed * 7919 + i * 104729 + 13) % 100003).ToArray());

                    var setup = Draw(target, Seeded(), boxes);

                    Assert.Equal(Draw(null, Seeded(), boxes).Scheme.Id, setup.Scheme.Id);
                    var difficulty = Rating(setup, target);
                    Assert.True(difficulty.Reached == true ? difficulty.Rating == target && difficulty.Note is null : difficulty.Note is not null,
                        $"{string.Join(",", boxes)} target {target} seed {seed}: {difficulty}");
                }
            }
        }
    }

    // With the rating relative to the player count (#334), both extremes stay reachable in Solo and at 4 and 5 players.
    [Theory]
    [InlineData(1, 1, 3)]
    [InlineData(1, 5, 0)]
    [InlineData(4, 1, 0)]
    [InlineData(4, 5, 0)]
    [InlineData(5, 1, 0)]
    [InlineData(5, 5, 0)]
    public void Both_extremes_are_reached_at_other_player_counts(int players, int target, int seed)
    {
        var pattern = Enumerable.Range(0, 30).Select(i => (seed * 7919 + i * 104729 + 13) % 100003).ToArray();

        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(
            players, ["core", "dark-city", "secret-wars-volume-1"], new CyclingRandom(pattern), difficulty: target));

        Assert.Equal(target, Rating(setup, target).Rating);
    }

    [Fact]
    public async Task The_response_carries_the_requested_target_the_real_rating_and_whether_it_was_reached()
    {
        var body = await Client(CivilWar()).GetFromJsonAsync<JsonObject>("/api/setup?players=2&difficulty=1");

        Assert.Equal(1, (int?)body!["difficulty"]!["target"]);
        Assert.Equal(1, (int?)body["difficulty"]!["rating"]);
        Assert.True((bool?)body["difficulty"]!["reached"]);
        Assert.Null(body["difficulty"]!["note"]);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("6")]
    [InlineData("hard")]
    public async Task A_difficulty_outside_1_to_5_returns_a_validation_problem(string difficulty)
    {
        var response = await Client(new ScriptedRandom()).GetAsync($"/api/setup?players=2&difficulty={difficulty}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal("difficulty must be a whole number from 1 to 5.", (string?)problem!["errors"]!["difficulty"]![0]);
    }

    private static Difficulty Rating(SetupResult setup, int? target = null) =>
        Assert.IsType<SetupBody>(SetupResponse.From(setup, Catalog, target)).Difficulty;

    private static HttpClient Client(IRandomSource random)
    {
        var host = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.AddSingleton(random)));
        return host.CreateClient();
    }
}
