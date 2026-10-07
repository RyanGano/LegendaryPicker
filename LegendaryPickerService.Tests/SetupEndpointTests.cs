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
    public async Task A_Villains_setup_names_its_ruleset_and_lists_only_the_Villainous_stacks()
    {
        // Graduation at Xavier's X-Academy with Dr. Strange: 8 Bystanders beside the Plot, 2 in the Adversary Deck.
        var body = await Client(new ScriptedRandom(4, 0)).GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=villains");

        Assert.Equal("villainous", (string?)body!["ruleset"]);
        Assert.Equal("Graduation at Xavier's X-Academy", (string?)body["scheme"]!["name"]);
        Assert.True(
            JsonNode.DeepEquals(JsonNode.Parse("""{ "bystanders": 31, "bindings": 30, "madameHydra": 12, "newRecruits": 15 }"""), body["stacks"]),
            body["stacks"]?.ToJsonString());
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse("""{ "agents": 8, "troopers": 4 }"""), body["playerDeck"]), body["playerDeck"]?.ToJsonString());
        Assert.Equal(
            ["Plot moves 8 Bystanders beside it", "Dr. Strange always leads Defenders"],
            body["notes"]!.AsArray().Select(note => (string?)note!["text"]));
    }

    [Fact]
    public async Task A_mixed_setup_says_why_it_follows_the_Villains_rules_and_names_each_cards_ruleset()
    {
        // Crush HYDRA, the 12th Scheme or Plot, with Dr. Doom: a Villainous Plot drawn with Heroic cards.
        var body = await Client(new ScriptedRandom(11, 0)).GetFromJsonAsync<JsonObject>("/api/setup?players=3&boxes=core,villains");

        Assert.Equal("villainous", (string?)body!["ruleset"]);
        Assert.True((bool?)body["mixed"]);
        var reason = """
            { "text": "Villains rules: the Plot is Crush HYDRA", "citation": "D-mixed", "link": "https://github.com/RyanGano/LegendaryPicker/issues/88" }
            """;
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse(reason), body["rulesReason"]), body["rulesReason"]?.ToJsonString());
        Assert.Equal(("Crush HYDRA", "villainous"), ((string?)body["scheme"]!["name"], (string?)body["scheme"]!["ruleset"]));
        Assert.Equal(("Dr. Doom", "firstEdition"), ((string?)body["mastermind"]!["name"], (string?)body["mastermind"]!["ruleset"]));
        Assert.True(
            JsonNode.DeepEquals(JsonNode.Parse("""{ "agents": 8, "troopers": 4, "choices": ["firstEdition", "villainous"] }"""), body["playerDeck"]),
            body["playerDeck"]?.ToJsonString());
    }

    [Fact]
    public async Task A_Heroic_only_setup_with_Villains_included_says_why_and_is_not_mixed()
    {
        // Legacy Virus with Dr. Doom and then the first cards left, all core: First Edition rules and no mixed fields.
        var body = await Client(new ScriptedRandom(0, 0)).GetFromJsonAsync<JsonObject>("/api/setup?players=3&boxes=core,villains");

        Assert.Equal("firstEdition", (string?)body!["ruleset"]);
        Assert.Equal("First Edition rules: no Villainous Plot or Commander", (string?)body["rulesReason"]!["text"]);
        Assert.False(body.ContainsKey("mixed"));
        Assert.False(body["scheme"]!.AsObject().ContainsKey("ruleset"));
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse("""{ "agents": 8, "troopers": 4 }"""), body["playerDeck"]), body["playerDeck"]?.ToJsonString());
    }

    [Fact]
    public async Task A_move_names_its_card_and_piles_as_they_are_written_in_box_files()
    {
        // Solo Secret Invasion with Loki.
        var body = await Client(new ScriptedRandom(4, 1)).GetFromJsonAsync<JsonObject>("/api/setup?players=1");

        Assert.Equal("Secret Invasion of the Skrull Shapeshifters", (string?)body!["scheme"]!["name"]);
        var expected = JsonNode.Parse("""[{ "card": "hero", "from": "heroDeck", "to": "villainDeck", "count": 12, "total": 12 }]""");
        Assert.True(JsonNode.DeepEquals(expected, body["moves"]), body["moves"]?.ToJsonString());
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse("""{ "heroCards": 84, "total": 72 }"""), body["heroDeck"]), body["heroDeck"]?.ToJsonString());
    }

    [Fact]
    public async Task A_Hero_outside_the_Hero_Deck_is_listed_with_its_pile_and_cards()
    {
        // Test Song with Dr. Doom; Gambit is the first Hero left after the Hero Deck's five.
        var client = Client(new ScriptedRandom(8, 0), BoxCatalog.Load(HeroRulesTests.FixtureDirectory));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,heroes");

        var expected = JsonNode.Parse("""
            [{
              "hero": { "id": "core_hero_gambit", "name": "Gambit", "terms": ["core_term_x-men", "core_term_covert", "core_term_instinct", "core_term_ranged"],
                "box": "Marvel Legendary First Edition core box" },
              "to": "villainDeck",
              "cards": 14
            }]
            """);
        Assert.True(JsonNode.DeepEquals(expected, body!["outsideHeroes"]), body["outsideHeroes"]?.ToJsonString());
        Assert.Equal(14, (int?)body["villainDeck"]!["outsideHeroCards"]);
        Assert.Equal(55, (int?)body["villainDeck"]!["total"]);
    }

    [Fact]
    public async Task A_Henchman_Group_outside_the_Villain_Deck_is_listed_with_its_pile_and_cards()
    {
        // Invade the Daily Bugle News HQ with Carnage: Doombot Legion goes in the Villain Deck, and Hand Ninjas is
        // the first Henchman Group left.
        var client = Client(new ScriptedRandom(8, 4));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,paint-the-town-red");

        var expected = JsonNode.Parse("""
            [{
              "group": { "id": "core_henchman_hand-ninjas", "name": "Hand Ninjas", "terms": ["core_term_fight"],
                "box": "Marvel Legendary First Edition core box" },
              "to": "heroDeck",
              "cards": 6
            }]
            """);
        Assert.Equal("Invade the Daily Bugle News HQ", (string?)body!["scheme"]!["name"]);
        Assert.True(JsonNode.DeepEquals(expected, body["outsideHenchmen"]), body["outsideHenchmen"]?.ToJsonString());
        Assert.True(JsonNode.DeepEquals(JsonNode.Parse("""{ "heroCards": 70, "total": 76 }"""), body["heroDeck"]), body["heroDeck"]?.ToJsonString());
    }

    [Fact]
    public async Task Other_Masterminds_are_listed_with_their_pile_and_Tactics()
    {
        // Master of Tyrants with Madelyne: the 3 other Masterminds drawn are the first core ones, and their 12 Tactics
        // go in the Villain Deck.
        var client = Client(new ScriptedRandom(13, 4));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,secret-wars-volume-1");

        Assert.Equal("Master of Tyrants", (string?)body!["scheme"]!["name"]);
        Assert.Equal(
            ["core_mastermind_dr-doom villainDeck 4", "core_mastermind_loki villainDeck 4", "core_mastermind_magneto villainDeck 4"],
            body["outsideMasterminds"]!.AsArray().Select(other => $"{other!["mastermind"]!["id"]} {other["to"]} {other["tactics"]}"));
        Assert.Equal("Marvel Legendary First Edition core box", (string?)body["outsideMasterminds"]![0]!["mastermind"]!["box"]);
        // Those Tactics play with no abilities, so their Masterminds carry no terms.
        Assert.All(body["outsideMasterminds"]!.AsArray(), other => Assert.Empty(other!["mastermind"]!["terms"]!.AsArray()));
        Assert.Equal(12, (int?)body["villainDeck"]!["mastermindTactics"]);
        Assert.Equal(53, (int?)body["villainDeck"]!["total"]);
    }

    [Fact]
    public async Task A_set_aside_Mastermind_says_when_it_joins_and_a_KO_pile_Henchman_Group_is_listed()
    {
        // Dark Alliance with Madelyne: the second Mastermind is set aside until Twist 1.
        var alliance = await Client(new ScriptedRandom(11, 4)).GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,secret-wars-volume-1");

        Assert.Equal("Dark Alliance", (string?)alliance!["scheme"]!["name"]);
        Assert.Equal("setAside Twist 1", $"{alliance["outsideMasterminds"]![0]!["to"]} {alliance["outsideMasterminds"]![0]!["joins"]}");

        // Build an Army of Annihilation: 10 Henchmen of a group the Villain Deck doesn't use, into the KO pile.
        var army = await Client(new ScriptedRandom(8, 4)).GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,secret-wars-volume-1");

        Assert.Equal("Build an Army of Annihilation", (string?)army!["scheme"]!["name"]);
        Assert.Equal("koPile 10", $"{army["outsideHenchmen"]![0]!["to"]} {army["outsideHenchmen"]![0]!["cards"]}");
        Assert.Equal(70, (int?)army["heroDeck"]!["total"]);
    }

    [Fact]
    public async Task Setup_steps_are_listed_by_label()
    {
        // Test Vigil with Test Watcher: the Scheme's two steps, then the Mastermind's.
        var client = Client(new ScriptedRandom(9, 4), BoxCatalog.Load(SetupStepsTests.FixtureDirectory));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,steps");

        var expected = JsonNode.Parse("""
            [
              "Place the test beacon token on the Scheme",
              "Split the Villain Deck into two equal piles",
              "Put the test lookout token on the first city space"
            ]
            """);
        Assert.True(JsonNode.DeepEquals(expected, body!["steps"]), body["steps"]?.ToJsonString());
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

    [Fact]
    public async Task Boxes_lists_each_box_whether_it_is_a_base_game_and_its_ruleset()
    {
        var body = await Client(catalog: BoxCatalog.Load(MultiBoxSetupTests.FixtureDirectory)).GetStringAsync("/api/boxes");

        var expected = JsonNode.Parse("""
            [
              { "id": "core", "name": "Marvel Legendary First Edition core box", "baseGame": true, "ruleset": "firstEdition", "mixesRulesets": false, "playsWithOtherRulesets": false },
              { "id": "fixture", "name": "Fixture Expansion", "baseGame": false, "ruleset": "firstEdition", "mixesRulesets": false, "playsWithOtherRulesets": false }
            ]
            """);
        Assert.True(JsonNode.DeepEquals(expected, JsonNode.Parse(body)), body);
    }

    [Fact]
    public async Task Boxes_come_base_games_first_then_by_release_month()
    {
        var boxes = await Client().GetFromJsonAsync<JsonArray>("/api/boxes");

        Assert.Equal(
            ["core", "villains", "marvel-studios-phase-1", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy", "fear-itself", "secret-wars-volume-1", "secret-wars-volume-2", "captain-america-75th-anniversary", "civil-war", "deadpool", "noir", "x-men", "spider-man-homecoming", "champions", "world-war-hulk", "ant-man", "venom", "dimensions", "revelations", "shield", "heroes-of-asgard", "the-new-mutants", "into-the-cosmos", "realm-of-kings", "annihilation", "messiah-complex", "doctor-strange-and-the-shadows-of-nightmare", "marvel-studios-guardians-of-the-galaxy"],
            boxes!.Select(box => (string)box!["id"]!));
    }

    // Only Legendary: Villains has rules for mixing rulesets, and only Fear Itself can be played without a base game of
    // its own ruleset (D-heroic), so the app can tell a player which ticked boxes can't be drawn together.
    [Fact]
    public async Task Boxes_says_which_base_game_can_mix_rulesets_and_which_expansion_plays_with_other_rulesets()
    {
        var boxes = await Client().GetFromJsonAsync<JsonArray>("/api/boxes");

        Assert.Equal(
            [
                "core firstEdition base", "villains villainous base mixes", "marvel-studios-phase-1 firstEdition base", "dark-city firstEdition", "fantastic-four firstEdition",
                "paint-the-town-red firstEdition", "guardians-of-the-galaxy firstEdition", "fear-itself villainous plays",
                "secret-wars-volume-1 firstEdition", "secret-wars-volume-2 firstEdition", "captain-america-75th-anniversary firstEdition",
                "civil-war firstEdition",
                "deadpool firstEdition", "noir firstEdition", "x-men firstEdition", "spider-man-homecoming firstEdition", "champions firstEdition", "world-war-hulk firstEdition", "ant-man firstEdition", "venom firstEdition", "dimensions firstEdition", "revelations firstEdition", "shield firstEdition", "heroes-of-asgard firstEdition", "the-new-mutants firstEdition", "into-the-cosmos firstEdition", "realm-of-kings firstEdition", "annihilation firstEdition", "messiah-complex firstEdition", "doctor-strange-and-the-shadows-of-nightmare firstEdition", "marvel-studios-guardians-of-the-galaxy firstEdition",
            ],
            boxes!.Select(box =>
                $"{box!["id"]} {box["ruleset"]}{((bool)box["baseGame"]! ? " base" : "")}{((bool)box["mixesRulesets"]! ? " mixes" : "")}"
                + ((bool)box["playsWithOtherRulesets"]! ? " plays" : "")));
    }

    // The core box and Fear Itself alone draw a setup that says the core box's Wounds stand in for Bindings (D-heroic).
    [Fact]
    public async Task The_core_box_and_Fear_Itself_return_a_setup_with_Wounds_standing_in_for_Bindings()
    {
        // The Traitor (Plot 10 after the core box's 8 Schemes) and Uru-Enchanted Iron Man (after the 4 core Masterminds).
        var client = Client(new ScriptedRandom(10, 4));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,fear-itself");

        Assert.Equal("firstEdition", (string?)body!["ruleset"]);
        Assert.True(JsonNode.DeepEquals(
            JsonNode.Parse("""[{ "part": "bindings", "source": "FI p.2", "with": "wounds" }]"""), body["standIns"]), body["standIns"]?.ToJsonString());
        Assert.Equal(24, (int?)body["stacks"]!["wounds"]);
        Assert.False(body["stacks"]!.AsObject().ContainsKey("bindings"));
        Assert.Equal("wound", (string?)body["moves"]![0]!["card"]);
    }

    [Fact]
    public async Task Naming_only_the_core_box_returns_the_same_setup_as_naming_none()
    {
        // The fixture expansion is loaded but not included.
        var client = Client(new ScriptedRandom(7, 3), BoxCatalog.Load(MultiBoxSetupTests.FixtureDirectory));

        var body = JsonNode.Parse(await client.GetStringAsync("/api/setup?players=2&boxes=core"));

        Assert.True(JsonNode.DeepEquals(JsonNode.Parse(ExpectedTwoPlayerSetup), body), body?.ToJsonString());
    }

    [Fact]
    public async Task A_setup_from_two_boxes_names_the_box_on_each_note_and_glossary_term()
    {
        // Test Heist and Test Tyrant, the fixture's Scheme and Mastermind.
        var client = Client(new ScriptedRandom(8, 4), BoxCatalog.Load(MultiBoxSetupTests.FixtureDirectory));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,fixture");

        Assert.Equal("fixture_scheme_test-heist", (string?)body!["scheme"]!["id"]);
        var notes = JsonNode.Parse("""
            [
              { "text": "Scheme puts 35 Bystanders in the Villain Deck", "citation": "Card", "link": null, "box": "Fixture Expansion" },
              { "text": "Scheme requires HYDRA", "citation": "R p.3", "link": "https://example.test/fixture-rules.pdf", "box": "Fixture Expansion" },
              {
                "text": "Test Tyrant always leads Test Cult",
                "citation": "R p.6",
                "link": "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf",
                "box": "Marvel Legendary First Edition core box"
              }
            ]
            """);
        Assert.True(JsonNode.DeepEquals(notes, body["notes"]), body["notes"]?.ToJsonString());
        var boxOfTerm = body["glossary"]!.AsArray().ToDictionary(term => (string)term!["id"]!, term => (string?)term!["box"]);
        Assert.Equal("Marvel Legendary First Edition core box", boxOfTerm["core_term_scheme-twist"]);
        Assert.Equal("Fixture Expansion", boxOfTerm["fixture_term_overdrive"]);
        var stacks = JsonNode.Parse("""{ "wounds": 35, "officers": 30, "bystanders": 6, "sidekicks": 16 }""");
        Assert.True(JsonNode.DeepEquals(stacks, body["stacks"]), body["stacks"]?.ToJsonString());
    }

    [Fact]
    public async Task A_setup_from_two_boxes_names_the_box_of_each_drawn_card()
    {
        // Test Heist and Test Tyrant, then the first option left: HYDRA is required, Test Cult always led.
        var client = Client(new ScriptedRandom(8, 4), BoxCatalog.Load(MultiBoxSetupTests.FixtureDirectory));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core,fixture");

        string BoxOf(JsonNode component) => $"{component["name"]}: {component["box"]}";
        Assert.Equal("Test Heist: Fixture Expansion", BoxOf(body!["scheme"]!));
        Assert.Equal("Test Tyrant: Fixture Expansion", BoxOf(body["mastermind"]!));
        Assert.Equal(
            ["HYDRA: Marvel Legendary First Edition core box", "Test Cult: Fixture Expansion"],
            body["villainGroups"]!.AsArray().Select(group => BoxOf(group!)));
        var heroes = body["heroes"]!.AsArray();
        Assert.Equal(5, heroes.Count);
        Assert.All(heroes, hero => Assert.Contains((string?)hero!["box"], new[] { "Marvel Legendary First Edition core box", "Fixture Expansion" }));
    }

    [Fact]
    public async Task A_setup_from_one_box_leaves_the_box_off_every_drawn_card()
    {
        // The fixture expansion is loaded but not included.
        var client = Client(new ScriptedRandom(7, 3), BoxCatalog.Load(MultiBoxSetupTests.FixtureDirectory));

        var body = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&boxes=core");

        var components = new[] { body!["scheme"]!, body["mastermind"]! }
            .Concat(body["villainGroups"]!.AsArray()!)
            .Concat(body["henchmanGroups"]!.AsArray()!)
            .Concat(body["heroes"]!.AsArray()!);
        Assert.All(components, component => Assert.False(component!.AsObject().ContainsKey("box"), component.ToJsonString()));
    }

    [Theory]
    [InlineData("core,nope", "No box has id nope.")]
    [InlineData("fixture", "Include at least one base game.")]
    [InlineData("", "Include at least one base game.")]
    public async Task Boxes_that_cannot_make_a_setup_return_a_validation_problem(string boxes, string expected)
    {
        var client = Client(catalog: BoxCatalog.Load(MultiBoxSetupTests.FixtureDirectory));

        var response = await client.GetAsync($"/api/setup?players=2&boxes={boxes}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal(expected, (string?)problem!["errors"]!["boxes"]![0]);
    }

    [Fact]
    public async Task Boxes_of_different_rulesets_with_no_rules_for_mixing_them_return_a_validation_problem()
    {
        var client = Client(catalog: BoxCatalog.Load(BaseGameTests.Directory));

        var response = await client.GetAsync("/api/setup?players=2&boxes=core,villains-fixture");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal(
            "Marvel Legendary First Edition core box and Villains Fixture follow different rulesets, and no included base game has rules for mixing them.",
            (string?)problem!["errors"]!["boxes"]![0]);
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
    public async Task Health_answers_GET_with_status_ok()
    {
        var response = await Client().GetAsync("/api/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("""{"status":"ok"}""", await response.Content.ReadAsStringAsync());
    }

    // UptimeRobot's free tier checks with HEAD, so HEAD must be a 200 with no body.
    [Fact]
    public async Task Health_answers_HEAD_with_200_and_no_body()
    {
        var response = await Client().SendAsync(new HttpRequestMessage(HttpMethod.Head, "/api/health"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Empty(await response.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task Forty_HEAD_health_checks_in_a_minute_are_never_rate_limited()
    {
        var client = Client();
        for (var i = 1; i <= 40; i++)
        {
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, "/api/health"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
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
          "ruleset": "firstEdition",
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
          "villainDeck": { "twists": 8, "masterStrikes": 5, "villainCards": 16, "henchmanCards": 10, "bystanders": 2, "outsideHeroCards": 0, "total": 41 },
          "heroDeck": { "heroCards": 70, "total": 70 },
          "twistsBesideScheme": 0,
          "stacks": { "wounds": 30, "officers": 30, "bystanders": 28 },
          "playerDeck": { "agents": 8, "troopers": 4 },
          "moves": [],
          "outsideHeroes": [],
          "outsideHenchmen": [],
          "cardsBeside": [],
          "steps": [],
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
