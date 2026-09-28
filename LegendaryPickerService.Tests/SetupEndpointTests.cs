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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task Every_term_a_drawn_component_uses_appears_once_in_the_glossary(int players)
    {
        // One random source across the requests, so each draws a different setup; 25 stays under the rate limit.
        var client = Client(new CyclingRandom(3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2));
        for (var request = 0; request < 25; request++)
        {
            var body = await client.GetFromJsonAsync<JsonObject>($"/api/setup?players={players}");

            var components = new[] { body!["scheme"]!, body["mastermind"]! }
                .Concat(body["villainGroups"]!.AsArray()!)
                .Concat(body["henchmanGroups"]!.AsArray()!)
                .Concat(body["heroes"]!.AsArray()!);
            var used = components.SelectMany(component => component!["terms"]!.AsArray().Select(term => (string)term!)).ToHashSet();
            var glossary = body["glossary"]!.AsArray().Select(term => (string)term!["id"]!).ToList();

            Assert.Equal(glossary.Count, glossary.Distinct().Count());
            Assert.Equal(used.Order(StringComparer.Ordinal), glossary.Order(StringComparer.Ordinal));
        }
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
          "scheme": { "id": "core_scheme_unleash-the-power-of-the-cosmic-cube", "name": "Unleash the Power of the Cosmic Cube", "terms": ["core_term_scheme-twist"] },
          "mastermind": { "id": "core_mastermind_red-skull", "name": "Red Skull", "terms": ["core_term_always-leads", "core_term_fight", "core_term_master-strike", "core_term_mastermind-tactic"] },
          "villainGroups": [
            { "id": "core_villain_hydra", "name": "HYDRA", "terms": ["core_term_escape", "core_term_fight"] },
            { "id": "core_villain_brotherhood", "name": "Brotherhood", "terms": ["core_term_ambush", "core_term_escape", "core_term_fight"] }
          ],
          "henchmanGroups": [
            { "id": "core_henchman_doombot-legion", "name": "Doombot Legion", "terms": ["core_term_fight"] }
          ],
          "heroes": [
            { "id": "core_hero_black-widow", "name": "Black Widow", "terms": ["core_term_avengers", "core_term_covert", "core_term_tech", "core_term_rescue-a-bystander"] },
            { "id": "core_hero_captain-america", "name": "Captain America", "terms": ["core_term_avengers", "core_term_covert", "core_term_instinct", "core_term_strength", "core_term_tech"] },
            { "id": "core_hero_cyclops", "name": "Cyclops", "terms": ["core_term_x-men", "core_term_ranged", "core_term_strength"] },
            { "id": "core_hero_deadpool", "name": "Deadpool", "terms": ["core_term_covert", "core_term_instinct", "core_term_tech"] },
            { "id": "core_hero_emma-frost", "name": "Emma Frost", "terms": ["core_term_x-men", "core_term_covert", "core_term_instinct", "core_term_ranged", "core_term_strength"] }
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
          ],
          "glossary": [
            {
              "id": "core_term_avengers",
              "name": "Avengers",
              "kind": "team",
              "summary": "A Hero team. In the core box its members are Black Widow, Captain America, Hawkeye, Hulk, Iron Man and Thor.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_x-men",
              "name": "X-Men",
              "kind": "team",
              "summary": "A Hero team. In the core box its members are Cyclops, Emma Frost, Gambit, Rogue, Storm and Wolverine.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_covert",
              "name": "Covert",
              "kind": "class",
              "summary": "A Hero class of fighters who prefer trickery and careful plans to open force.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_instinct",
              "name": "Instinct",
              "kind": "class",
              "summary": "A Hero class of fighters who trust their gut and their senses in close combat.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_ranged",
              "name": "Ranged",
              "kind": "class",
              "summary": "A Hero class of fighters who deal damage from a distance, by any kind of projected power.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_strength",
              "name": "Strength",
              "kind": "class",
              "summary": "A Hero class of fighters whose power is might, whether of body or of will and leadership.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_tech",
              "name": "Tech",
              "kind": "class",
              "summary": "A Hero class of fighters whose power comes from gear and science rather than the body.",
              "citation": "R p.18",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_always-leads",
              "name": "Always Leads",
              "kind": "keyword",
              "summary": "Names the one Villain or Henchman Group this Mastermind brings to every game. That group takes one of the setup's group slots; random draws fill the rest.",
              "citation": "R p.6",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_ambush",
              "name": "Ambush",
              "kind": "keyword",
              "summary": "Triggers as this Villain arrives in the city from the Villain Deck. When its arrival shoves another Villain out of the city, resolve that escape before this effect.",
              "citation": "R p.9",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_escape",
              "name": "Escape",
              "kind": "keyword",
              "summary": "Triggers when this Villain is shoved past the final city space and escapes, in addition to the standard escape penalties.",
              "citation": "R p.9",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_fight",
              "name": "Fight",
              "kind": "keyword",
              "summary": "Triggers when a player spends enough Attack to defeat this card.",
              "citation": "R p.13",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_master-strike",
              "name": "Master Strike",
              "kind": "keyword",
              "summary": "Drawing a Master Strike from the Villain Deck makes the Mastermind act in person: resolve the Master Strike line on its card. The Strike card is then KO'd.",
              "citation": "R p.10",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_mastermind-tactic",
              "name": "Mastermind Tactic",
              "kind": "keyword",
              "summary": "Beating the Mastermind's Attack wins one of its four face-down Tactics at random: keep it for Victory Points and do its Fight effect. Take all four to win.",
              "citation": "R p.14",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_rescue-a-bystander",
              "name": "Rescue a Bystander",
              "kind": "keyword",
              "summary": "Add a Bystander from the shared Bystander stack to your Victory Pile, where it scores at game end. This never frees a Bystander a Villain holds; defeat that Villain for it.",
              "citation": "R p.15",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            },
            {
              "id": "core_term_scheme-twist",
              "name": "Scheme Twist",
              "kind": "keyword",
              "summary": "Drawing a Twist from the Villain Deck advances the evil plan: resolve the Twist line on the Scheme card. The Twist is then KO'd, unless the Scheme sends it elsewhere.",
              "citation": "R p.10",
              "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf"
            }
          ]
        }
        """;
}
