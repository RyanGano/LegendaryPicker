using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// Setup codes (#335, #340): every drawn setup carries a code, and GET /api/setup/{code} gives that setup back.
public sealed class SetupCodeTests : IDisposable
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    private readonly List<IDisposable> _hosts = [];

    public void Dispose() => _hosts.ForEach(host => host.Dispose());

    // Every box, both targets and Epic Masterminds, so the draws reach the Schemes that draw cards outside their decks
    // and the repair toward a target swaps picks. The code's setup is the shown one, ratings included; a code keeps no
    // target, so the reload has no target note. With every box ticked, the code stays short.
    [Theory]
    [InlineData(2, 3, 1, 4, 1, 5)]
    [InlineData(3, 9, 2, 6, 5, 3)]
    [InlineData(1, 5, 8, 9, 7, 9)]
    [InlineData(5, 2, 7, 1, 8, 2)]
    public async Task A_setup_code_reloads_the_setup_it_was_shown_with(int players, params int[] pattern)
    {
        var client = Client(new CyclingRandom(pattern));
        var boxes = string.Join(',', Catalog.Boxes.Select(box => box.Id));
        var drawn = await client.GetFromJsonAsync<JsonObject>($"/api/setup?players={players}&boxes={boxes}&epic=true&difficulty=2&complexity=4");
        var code = (string)drawn!["code"]!;
        foreach (var rating in new[] { "difficulty", "complexity" })
        {
            var shown = drawn[rating]!.AsObject();
            shown.Remove("target");
            shown.Remove("reached");
            shown.Remove("note");
        }

        var response = await client.GetAsync($"/api/setup/{code}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var reloaded = JsonNode.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(JsonNode.DeepEquals(drawn, reloaded), reloaded?.ToJsonString());
        Assert.InRange(code.Length, 30, 60);
    }

    // The boxes come from the cards. Deadpool adds nothing to a setup of core cards, so the code leaves it out and the
    // reload names the core box alone; Dark City's Bystanders change the stack, so that code keeps the ticked boxes.
    [Theory]
    [InlineData("core,deadpool", 7, new[] { "core" })]
    [InlineData("core,dark-city", 8, new[] { "core", "dark-city" })]
    public async Task A_code_keeps_the_ticked_boxes_only_when_they_change_the_setup(string ticked, int fields, string[] reloadedBoxes)
    {
        var client = Client(new ScriptedRandom());
        var drawn = await client.GetFromJsonAsync<JsonObject>($"/api/setup?players=2&boxes={ticked}");
        var code = (string)drawn!["code"]!;

        var reloaded = await client.GetFromJsonAsync<JsonObject>($"/api/setup/{code}");

        Assert.Equal(fields, code.Split('-').Length);
        Assert.Equal(reloadedBoxes, reloaded!["boxes"]!.AsArray().Select(box => (string?)box));
        Assert.Equal((string?)drawn["scheme"]!["id"], (string?)reloaded["scheme"]!["id"]);
        Assert.Equal((int?)drawn["stacks"]!["bystanders"], (int?)reloaded["stacks"]!["bystanders"]);
    }

    // The code names cards, not a seed: from other box data it still reloads while a draw gives its cards, and a card
    // the data no longer has makes it say the data has changed, never show another setup. With this data's version, a
    // code naming no card is no setup code at all.
    [Fact]
    public async Task A_code_from_other_data_reloads_its_cards_or_says_it_no_longer_can()
    {
        var client = Client(new ScriptedRandom(7, 3));
        var code = (string)(await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2"))!["code"]!;
        var heroes = code.Split('-')[5];
        var otherData = code.Split('-')[6] == "zzz" ? "zzy" : "zzz";
        var noCard = With(code, 5, heroes[..^2] + "yz");

        var reloaded = await client.GetFromJsonAsync<JsonObject>($"/api/setup/{With(code, 6, otherData)}");
        var gone = await client.GetAsync($"/api/setup/{With(noCard, 6, otherData)}");

        Assert.Equal("Unleash the Power of the Cosmic Cube", (string?)reloaded!["scheme"]!["name"]);
        Assert.Equal(code, (string?)reloaded["code"]);
        Assert.Equal(HttpStatusCode.Conflict, gone.StatusCode);
        Assert.Equal(
            "This setup code can no longer give the same cards: the card data or setup rules have changed since it was made.",
            (string?)(await gone.Content.ReadFromJsonAsync<JsonObject>())!["detail"]);
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync($"/api/setup/{noCard}")).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync("/api/setup/not-a-code")).StatusCode);
    }

    // The reload searches for the code's cards rather than replaying the generator's order: a code that lists the Hero
    // Deck in another order than the draw took it reloads the same Heroes, listed as the code lists them.
    [Fact]
    public void A_code_reloads_whatever_order_it_lists_its_cards_in()
    {
        var generator = new SetupGenerator(Catalog);
        var codes = new SetupCodes(Catalog, generator);
        var setup = Assert.IsType<SetupResult>(generator.Generate(2, ["core"], new ScriptedRandom(7, 3)));
        var code = codes.For(setup, null, false)!;
        var heroes = code.Split('-')[5];

        var reloaded = Assert.IsType<SetupCodes.Loaded>(codes.Load(With(code, 5, heroes[^2..] + heroes[..^2])));

        Assert.Equal(setup.Heroes.Select(hero => hero.Id).Order(), reloaded.Setup.Heroes.Select(hero => hero.Id).Order());
        Assert.Equal(setup.Heroes[^1].Id, reloaded.Setup.Heroes[0].Id);
    }

    // A Hero a setup step names is no card of the decks, so the code names it too (#242). Test Lottery is the 10th Scheme
    // at 2 players in Fixtures/SetupSteps.
    [Fact]
    public void A_code_names_the_Hero_a_setup_step_draws()
    {
        var catalog = BoxCatalog.Load(SetupStepsTests.FixtureDirectory);
        var generator = new SetupGenerator(catalog);
        var codes = new SetupCodes(catalog, generator);
        var setup = Assert.IsType<SetupResult>(generator.Generate(2, ["core", "steps"], new LastOptions(10, 3)));

        var reloaded = Assert.IsType<SetupCodes.Loaded>(codes.Load(codes.For(setup, null, false)!));

        Assert.Equal("Test Lottery", reloaded.Setup.Scheme.Name);
        Assert.Equal(setup.StepHeroes!.Select(hero => hero.Id), reloaded.Setup.StepHeroes!.Select(hero => hero.Id));
        Assert.Equal(setup.Steps, reloaded.Setup.Steps);
    }

    // A card's number is its place in Data/CodeNumbers.json, so the list holds every box and card once: a new box appends
    // its own, and a removed card keeps its place.
    [Fact]
    public void The_code_numbers_list_every_box_and_card_once()
    {
        var listed = JsonSerializer.Deserialize<SetupCodes.NumbersList>(File.ReadAllText(SetupCodes.NumbersFile), JsonSerializerOptions.Web)!;

        Assert.Empty(Catalog.Boxes.Select(box => box.Id).Except(listed.Boxes));
        Assert.Empty(SetupCodes.CardIds(Catalog).Except(listed.Cards));
        Assert.Equal(listed.Boxes.Count, listed.Boxes.Distinct().Count());
        Assert.Equal(listed.Cards.Count, listed.Cards.Distinct().Count());
    }

    // The code with one field replaced.
    private static string With(string code, int field, string value) =>
        string.Join('-', code.Split('-').Select((old, at) => at == field ? value : old));

    // Plays back a fixed list of draws, then takes the last option of every later draw, so no draw is the replay's default.
    private sealed class LastOptions(params int[] draws) : IRandomSource
    {
        private int _position;

        public int Next(int exclusiveMax) => _position < draws.Length ? draws[_position++] : exclusiveMax - 1;
    }

    private HttpClient Client(IRandomSource random)
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(host => host.ConfigureTestServices(services => services.AddSingleton(random)));
        _hosts.Add(factory);
        return factory.CreateClient();
    }
}
