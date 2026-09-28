using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// GET /api/setup through the real HTTP pipeline. Each test hosts its own app, so each starts
// with a fresh rate-limit window.
public sealed class SetupEndpointTests : IDisposable
{
    private readonly List<IDisposable> _hosts = [];
    private readonly List<string> _directories = [];

    public void Dispose()
    {
        _hosts.ForEach(host => host.Dispose());
        _directories.ForEach(directory => Directory.Delete(directory, recursive: true));
    }

    [Fact]
    public async Task Two_players_with_a_scripted_draw_return_the_exact_setup()
    {
        // Cosmic Cube, Red Skull, then the first option left for every later draw.
        var response = await Client(new ScriptedRandom(7, 3)).GetAsync("/api/setup?players=2");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        var body = JsonNode.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse(ExpectedTwoPlayerSetup), body), body?.ToJsonString());
    }

    [Fact]
    public async Task One_player_returns_a_Solo_setup_with_1_Master_Strike()
    {
        var body = await Client(new ScriptedRandom()).GetFromJsonAsync<JsonObject>("/api/setup?players=1");

        Assert.Equal("setup", (string?)body!["kind"]);
        Assert.Equal(1, (int?)body["players"]);
        Assert.Equal(1, (int?)body["villainDeck"]!["masterStrikes"]);
    }

    [Fact]
    public async Task No_eligible_Scheme_is_a_200_with_kind_noEligibleScheme()
    {
        // A catalog without Masterminds leaves no Scheme a legal completion.
        var core = JsonNode.Parse(File.ReadAllText(Path.Combine(BoxCatalog.DefaultDirectory, "core.json")))!.AsObject();
        core["masterminds"] = new JsonArray();
        var directory = Directory.CreateTempSubdirectory("legendary-endpoint-").FullName;
        _directories.Add(directory);
        File.WriteAllText(Path.Combine(directory, "core.json"), core.ToJsonString());

        var response = await Client(catalog: BoxCatalog.Load(directory)).GetAsync("/api/setup?players=4");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = JsonNode.Parse(await response.Content.ReadAsStringAsync());
        var expected = JsonNode.Parse("""
            {
              "kind": "noEligibleScheme",
              "players": 4,
              "message": "No Scheme can be set up legally for 4 players with the included boxes."
            }
            """);
        Assert.True(JsonNode.DeepEquals(expected, body), body?.ToJsonString());
    }

    [Theory]
    [InlineData("/api/setup?players=0")]
    [InlineData("/api/setup?players=6")]
    [InlineData("/api/setup?players=abc")]
    [InlineData("/api/setup")]
    public async Task A_missing_or_invalid_player_count_returns_a_validation_problem(string url)
    {
        var response = await Client().GetAsync(url);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        var problem = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal(400, (int?)problem!["status"]);
        Assert.Equal("players must be a whole number from 1 to 5.", (string?)problem["errors"]!["players"]![0]);
    }

    [Fact]
    public async Task The_31st_setup_in_a_minute_is_rejected_but_health_still_answers()
    {
        var client = Client();
        for (var i = 1; i <= 30; i++)
        {
            Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/setup?players=3")).StatusCode);
        }

        // From the frontend's origin, so the browser lets the app read the 429 instead of a network error.
        var rejected = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/setup?players=3")
        {
            Headers = { { "Origin", "http://localhost:5173" } },
        });
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);
        Assert.Equal(["http://localhost:5173"], rejected.Headers.GetValues("Access-Control-Allow-Origin"));
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/health")).StatusCode);
    }

    [Fact]
    public async Task Behind_the_platform_proxy_each_forwarded_client_gets_its_own_budget()
    {
        var server = Server();
        for (var i = 1; i <= 30; i++)
        {
            Assert.Equal(200, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: "198.51.100.7"));
        }

        Assert.Equal(429, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: "198.51.100.7"));
        Assert.Equal(200, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: "198.51.100.8"));
    }

    [Fact]
    public async Task A_client_that_forges_X_Forwarded_For_still_spends_its_own_budget()
    {
        var server = Server();
        for (var i = 1; i <= 30; i++)
        {
            Assert.Equal(200, await SetupStatus(server, IPAddress.Parse("203.0.113.9"), forwardedFor: $"198.51.100.{i}"));
        }

        Assert.Equal(429, await SetupStatus(server, IPAddress.Parse("203.0.113.9"), forwardedFor: "198.51.100.99"));
    }

    [Fact]
    public async Task A_forged_X_Forwarded_For_passed_on_by_the_platform_proxy_does_not_buy_a_new_budget()
    {
        // The front end appends the real caller to whatever X-Forwarded-For the client sent,
        // so only the rightmost entry can be trusted.
        var server = Server();
        for (var i = 1; i <= 30; i++)
        {
            Assert.Equal(200, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: $"198.51.100.{i}, 203.0.113.9"));
        }

        Assert.Equal(429, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: "198.51.100.99, 203.0.113.9"));
    }

    [Fact]
    public async Task IPv6_clients_in_one_64_share_a_budget_and_another_64_does_not()
    {
        var server = Server();
        for (var i = 1; i <= 30; i++)
        {
            Assert.Equal(200, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: $"2001:db8:1:2::{i:x}"));
        }

        Assert.Equal(429, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: "2001:db8:1:2:ffff::1"));
        Assert.Equal(200, await SetupStatus(server, AppServiceFrontEnd, forwardedFor: "2001:db8:1:3::1"));
    }

    [Theory]
    [InlineData("/api/setup?players=3")]
    [InlineData("/api/setup?players=9")]
    public async Task Setup_responses_are_never_cached(string url)
    {
        var response = await Client().GetAsync(url);

        Assert.Equal("no-store", response.Headers.CacheControl?.ToString());
    }

    [Fact]
    public async Task A_preflight_from_a_configured_origin_is_allowed()
    {
        var response = await Client().SendAsync(Preflight("http://localhost:5173"));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(["http://localhost:5173"], response.Headers.GetValues("Access-Control-Allow-Origin"));
    }

    [Fact]
    public async Task A_preflight_from_an_unlisted_origin_is_not_allowed()
    {
        var response = await Client().SendAsync(Preflight("https://example.com"));

        Assert.False(response.Headers.Contains("Access-Control-Allow-Origin"));
    }

    // The App Service front end as the deployed app sees it: an IPv4-mapped link-local address.
    private static readonly IPAddress AppServiceFrontEnd = IPAddress.Parse("::ffff:169.254.129.1");

    private static async Task<int> SetupStatus(TestServer server, IPAddress remote, string forwardedFor)
    {
        var context = await server.SendAsync(context =>
        {
            context.Request.Path = "/api/setup";
            context.Request.QueryString = new QueryString("?players=3");
            context.Request.Headers["X-Forwarded-For"] = forwardedFor;
            context.Connection.RemoteIpAddress = remote;
        });
        return context.Response.StatusCode;
    }

    private TestServer Server()
    {
        var factory = new WebApplicationFactory<Program>();
        _hosts.Add(factory);
        return factory.Server;
    }

    private HttpClient Client(IRandomSource? random = null, BoxCatalog? catalog = null)
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(host => host.ConfigureTestServices(services =>
        {
            if (random is not null)
            {
                services.AddSingleton(random);
            }

            if (catalog is not null)
            {
                services.AddSingleton(catalog);
            }
        }));
        _hosts.Add(factory);
        return factory.CreateClient();
    }

    private static HttpRequestMessage Preflight(string origin) => new(HttpMethod.Options, "/api/setup?players=3")
    {
        Headers =
        {
            { "Origin", origin },
            { "Access-Control-Request-Method", "GET" },
        },
    };

    private const string ExpectedTwoPlayerSetup = """
        {
          "kind": "setup",
          "players": 2,
          "scheme": { "id": "core_scheme_unleash-the-power-of-the-cosmic-cube", "name": "Unleash the Power of the Cosmic Cube" },
          "mastermind": { "id": "core_mastermind_red-skull", "name": "Red Skull" },
          "villainGroups": [
            { "id": "core_villain_hydra", "name": "HYDRA" },
            { "id": "core_villain_brotherhood", "name": "Brotherhood" }
          ],
          "henchmanGroups": [
            { "id": "core_henchman_doombot-legion", "name": "Doombot Legion" }
          ],
          "heroes": [
            { "id": "core_hero_black-widow", "name": "Black Widow" },
            { "id": "core_hero_captain-america", "name": "Captain America" },
            { "id": "core_hero_cyclops", "name": "Cyclops" },
            { "id": "core_hero_deadpool", "name": "Deadpool" },
            { "id": "core_hero_emma-frost", "name": "Emma Frost" }
          ],
          "villainDeck": { "twists": 8, "masterStrikes": 5, "villainCards": 16, "henchmanCards": 10, "bystanders": 2, "heroCards": 0, "total": 41 },
          "heroDeck": { "heroCards": 70, "movedToVillainDeck": 0, "total": 70 },
          "twistsBesideScheme": 0,
          "stacks": { "wounds": 30, "officers": 30, "bystanders": 28 },
          "playerDeck": { "agents": 8, "troopers": 4 },
          "notes": [
            {
              "text": "Red Skull always leads HYDRA",
              "citation": "R p.6",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            }
          ]
        }
        """;
}
