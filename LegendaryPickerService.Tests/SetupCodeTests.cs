using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// Setup codes (#335): every drawn setup carries a code, and GET /api/setup/{code} gives that setup back.
public sealed class SetupCodeTests : IDisposable
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    private readonly List<IDisposable> _hosts = [];

    public void Dispose() => _hosts.ForEach(host => host.Dispose());

    // Every box, a difficulty target and Epic Masterminds, so the draws reach the Schemes that draw cards outside their
    // decks and the repair toward a target swaps picks; the code's setup is the shown one, ratings and target note included.
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

        var response = await client.GetAsync($"/api/setup/{code}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var reloaded = JsonNode.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(JsonNode.DeepEquals(drawn, reloaded), reloaded?.ToJsonString());
    }

    // The code names cards, not a seed: from other box data it still reloads while the draw lands on its cards, and a
    // card the data no longer has makes it say it can't, never show another setup.
    [Fact]
    public async Task A_code_from_other_data_reloads_its_cards_or_says_it_no_longer_can()
    {
        var client = Client(new ScriptedRandom(7, 3));
        var code = (string)(await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2"))!["code"]!;
        var parts = code.Split('-');
        var otherData = string.Join('-', parts[0], SetupCodes.Token("older data"), parts[2], parts[3], parts[4]);
        var goneCard = string.Join('-', parts[0], parts[1], parts[2], parts[3], parts[4][..^6] + SetupCodes.Token("core_hero_retired"));

        var reloaded = await client.GetFromJsonAsync<JsonObject>($"/api/setup/{otherData}");
        var gone = await client.GetAsync($"/api/setup/{goneCard}");

        Assert.Equal("Unleash the Power of the Cosmic Cube", (string?)reloaded!["scheme"]!["name"]);
        Assert.Equal(code, (string?)reloaded["code"]);
        Assert.Equal(HttpStatusCode.Conflict, gone.StatusCode);
        Assert.Equal(
            "This setup code can no longer give the same cards: the card data or setup rules have changed since it was made.",
            (string?)(await gone.Content.ReadFromJsonAsync<JsonObject>())!["detail"]);
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync("/api/setup/not-a-code")).StatusCode);
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

        var reloaded = Assert.IsType<SetupCodes.Loaded>(codes.Load(codes.For(setup, null, false, null, null)!));

        Assert.Equal("Test Lottery", reloaded.Setup.Scheme.Name);
        Assert.Equal(setup.StepHeroes!.Select(hero => hero.Id), reloaded.Setup.StepHeroes!.Select(hero => hero.Id));
        Assert.Equal(setup.Steps, reloaded.Setup.Steps);
    }

    // A code names each box and card by a short token of its id, so no two loaded ids may share one.
    [Fact]
    public void No_two_loaded_ids_share_a_token()
    {
        var ids = Catalog.Boxes.Select(box => box.Id)
            .Concat(Catalog.Boxes.SelectMany(box => box.Heroes.Cast<ICard>().Concat(box.VillainGroups).Concat(box.HenchmanGroups).Concat(box.Masterminds).Concat(box.Schemes)).Select(card => card.Id))
            .ToList();

        Assert.Equal(ids.Count, ids.Select(SetupCodes.Token).Distinct().Count());
    }

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
