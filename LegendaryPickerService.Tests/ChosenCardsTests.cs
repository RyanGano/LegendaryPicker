using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace LegendaryPickerService.Tests;

// Cards the player chooses to be in the game (#337): each is in the setup as a required card would be, a repair toward a
// target never swaps it, and a choice no legal setup can hold gives a message that says which choice to change.
public sealed class ChosenCardsTests : IDisposable
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    private readonly List<IDisposable> _hosts = [];

    public void Dispose() => _hosts.ForEach(host => host.Dispose());

    private static GenerationResult Draw(string[] boxes, string[] chosen, int players = 2, int? difficulty = null) =>
        new SetupGenerator(Catalog).Generate(players, boxes, new ScriptedRandom(), difficulty: difficulty, chosenCards: chosen);

    [Theory]
    [InlineData(null)]
    [InlineData(1)]
    [InlineData(5)]
    public void Each_kind_of_chosen_card_is_in_the_setup_and_a_repair_toward_a_target_keeps_it(int? difficulty)
    {
        var setup = Assert.IsType<SetupResult>(Draw(
            ["core"],
            ["core_hero_wolverine", "core_scheme_legacy-virus", "core_mastermind_loki", "core_villain_skrulls", "core_henchman_sentinel", "core_hero_thor"],
            difficulty: difficulty));

        Assert.Equal(("The Legacy Virus", "Loki"), (setup.Scheme.Name, setup.Mastermind.Name));
        Assert.Equal(["Enemies of Asgard", "Skrulls"], setup.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Sentinel"], setup.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(5, setup.Heroes.Count);
        Assert.Equal(["Wolverine", "Thor"], setup.Heroes.Take(2).Select(hero => hero.Name));
    }

    [Fact]
    public void A_chosen_Mastermind_the_chosen_Scheme_excludes_says_to_choose_another_Mastermind()
    {
        var result = Draw(["core", "champions"], ["champions_scheme_clash-of-the-monsters-unleashed", "champions_mastermind_fin-fang-foom"]);

        Assert.Equal(
            "The Scheme Clash of the Monsters Unleashed can't be played with the Mastermind Fin Fang Foom: choose another Mastermind.",
            Assert.IsType<ChosenCardsConflict>(result).Message);
    }

    [Fact]
    public void A_chosen_Mastermind_alone_is_drawn_with_a_Scheme_that_allows_it()
    {
        // Every Scheme of core and Champions at 2 players but Clash of the Monsters Unleashed, which excludes Fin Fang Foom.
        var random = new ScriptedRandom();
        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(
            2, ["core", "champions"], random, chosenCards: ["champions_mastermind_fin-fang-foom"]));
        var schemes = Catalog.Boxes.Where(box => box.Id is "core" or "champions").SelectMany(box => box.Schemes)
            .Count(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(2) ?? true);

        Assert.Equal("Fin Fang Foom", setup.Mastermind.Name);
        Assert.Equal(schemes - 1, random.Options[0]);
        Assert.Equal(1, random.Options[1]);
    }

    [Fact]
    public void Chosen_Heroes_that_break_the_Schemes_Hero_rules_say_so()
    {
        // Fall of the Hulks takes exactly 2 Heroes with "Hulk" in their Hero Names.
        var result = Draw(
            ["core", "world-war-hulk"],
            ["world-war-hulk_scheme_fall-of-the-hulks", "core_hero_hulk", "world-war-hulk_hero_she-hulk", "world-war-hulk_hero_skaar-son-of-hulk"]);

        Assert.Equal(
            "The Hero rules of the Scheme Fall of the Hulks can't be met with Hulk, She-Hulk and Skaar, Son of Hulk: remove a chosen Hero, or choose another Scheme.",
            Assert.IsType<ChosenCardsConflict>(result).Message);
    }

    [Fact]
    public void Chosen_Villain_Groups_past_the_slots_the_rules_leave_say_how_many_fit()
    {
        // Two Villain Groups at 2 players, and Loki always leads Enemies of Asgard.
        var result = Draw(["core"], ["core_scheme_portals-to-the-dark-dimension", "core_mastermind_loki", "core_villain_skrulls", "core_villain_hydra"]);

        Assert.Equal(
            "The Scheme Portals to the Dark Dimension with the Mastermind Loki leaves room for 1 chosen Villain Group, but 2 are chosen: remove 1, or choose another Scheme or Mastermind.",
            Assert.IsType<ChosenCardsConflict>(result).Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Chosen_Villain_Groups_are_drawn_only_with_a_Mastermind_that_leaves_room_for_them(int mastermind)
    {
        // At 2 players, Skrulls and HYDRA fill both Villain Group slots, so of the core Masterminds only Dr. Doom (who leads
        // Henchmen) and Red Skull (who leads HYDRA) are drawn with Portals to the Dark Dimension; Loki and Magneto would
        // need a third slot for their groups.
        var random = new ScriptedRandom(DrawIndexes.Scheme(Catalog, ["core"], 2, "Portals to the Dark Dimension"), mastermind);
        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, ["core"], random, chosenCards: ["core_villain_skrulls", "core_villain_hydra"]));

        Assert.Equal(2, random.Options[1]);
        Assert.Equal(new[] { "Dr. Doom", "Red Skull" }[mastermind], setup.Mastermind.Name);
        Assert.Equal(["HYDRA", "Skrulls"], setup.VillainGroups.Select(group => group.Name).Order());
    }

    [Fact]
    public void Chosen_Heroes_are_drawn_only_with_a_Scheme_that_has_room_for_them()
    {
        // Super Hero Civil War uses 4 Heroes at 2 players, so 5 chosen Heroes leave it out of the Scheme draw; the last
        // Scheme left is then one with 5.
        string[] heroes = ["core_hero_wolverine", "core_hero_thor", "core_hero_storm", "core_hero_hulk", "core_hero_rogue"];
        var schemes = Catalog.Boxes.Single(box => box.Id == "core").Schemes.Count(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(2) ?? true);
        var random = new ScriptedRandom(schemes - 2);
        var setup = Assert.IsType<SetupResult>(new SetupGenerator(Catalog).Generate(2, ["core"], random, chosenCards: heroes));

        Assert.Equal(schemes - 1, random.Options[0]);
        Assert.NotEqual("Super Hero Civil War", setup.Scheme.Name);
        Assert.Equal(heroes, setup.Heroes.Select(hero => hero.Id));
    }

    [Fact]
    public void The_limits_are_the_most_any_Scheme_holds_or_the_chosen_Scheme_holds()
    {
        var generator = new SetupGenerator(Catalog);

        // At 2 players: Secret Invasion of the Skrull Shapeshifters uses 6 Heroes, and Negative Zone Prison Breakout adds a
        // Henchman Group to the table's 1.
        Assert.Equal(new ChoiceLimits(6, 2, 2), generator.Limits(2, ["core"]));
        Assert.Equal(new ChoiceLimits(4, 2, 1), generator.Limits(2, ["core"], chosenCards: ["core_scheme_super-hero-civil-war"]));
        // In Solo the Scheme's 6 Heroes override Solo's 3.
        Assert.Equal(new ChoiceLimits(6, 1, 1), generator.Limits(1, ["core"], chosenCards: ["core_hero_wolverine"]));
        // Epic Annihilus adds a Villain Group his normal side doesn't, so with Epic Masterminds the setup holds one more.
        string[] annihilus = ["core_scheme_super-hero-civil-war", "annihilation_mastermind_annihilus"];
        Assert.Equal(new ChoiceLimits(4, 2, 1), generator.Limits(2, ["core", "annihilation"], chosenCards: annihilus));
        Assert.Equal(new ChoiceLimits(4, 3, 1), generator.Limits(2, ["core", "annihilation"], chosenCards: annihilus, epic: true));
    }

    [Fact]
    public void Chosen_cards_no_Scheme_and_Mastermind_can_hold_say_so()
    {
        // Three Villain Groups at 2 players: no core Scheme adds a Villain Group.
        var result = Draw(["core"], ["core_villain_skrulls", "core_villain_hydra", "core_villain_spider-foes"]);

        Assert.Equal(
            "No Scheme and Mastermind of the included boxes leave room for all the chosen cards at 2 players: remove a chosen card, or include more boxes.",
            Assert.IsType<ChosenCardsConflict>(result).Message);
    }

    [Fact]
    public void A_card_chosen_from_a_box_that_isnt_ticked_includes_its_box_and_its_setup_code_reloads_it()
    {
        var generator = new SetupGenerator(Catalog);
        var setup = Assert.IsType<SetupResult>(generator.Generate(2, ["core"], new ScriptedRandom(), chosenCards: ["dark-city_hero_angel"]));

        Assert.Equal(["core", "dark-city"], setup.Boxes.Select(box => box.Id));
        Assert.Equal("Angel", setup.Heroes[0].Name);
        var codes = new SetupCodes(Catalog, generator);
        var reloaded = Assert.IsType<SetupCodes.Loaded>(codes.Load(codes.For(setup, null, false)!)).Setup;
        Assert.Equal(setup.Heroes.Select(hero => hero.Id), reloaded.Heroes.Select(hero => hero.Id));
        Assert.Equal(["core", "dark-city"], reloaded.Boxes.Select(box => box.Id));
    }

    [Fact]
    public async Task The_setup_endpoint_takes_chosen_cards_as_one_list_of_ids_and_refuses_one_that_isnt_a_card()
    {
        var client = Client(new ScriptedRandom());

        var drawn = await client.GetFromJsonAsync<JsonObject>("/api/setup?players=2&include=core_mastermind_magneto,dark-city_hero_angel");
        Assert.Equal("Magneto", (string)drawn!["mastermind"]!["name"]!);
        Assert.Contains(drawn["heroes"]!.AsArray(), hero => (string)hero!["name"]! == "Angel");

        var conflict = await client.GetFromJsonAsync<JsonObject>(
            "/api/setup?players=2&boxes=core,world-war-hulk&include=world-war-hulk_scheme_fall-of-the-hulks,core_hero_hulk,world-war-hulk_hero_she-hulk,world-war-hulk_hero_skaar-son-of-hulk");
        Assert.Equal("chosenCardsConflict", (string)conflict!["kind"]!);

        var wrong = await client.GetAsync("/api/setup?players=2&include=core_scheme_nothing");
        Assert.Equal(HttpStatusCode.BadRequest, wrong.StatusCode);
        var problem = await wrong.Content.ReadFromJsonAsync<JsonObject>();
        Assert.Equal("No card has id core_scheme_nothing.", (string)problem!["errors"]!["include"]![0]!);
    }

    [Fact]
    public async Task The_limits_endpoint_reads_the_draws_parameters()
    {
        var client = Client();

        var limits = await client.GetFromJsonAsync<JsonObject>("/api/limits?players=2&boxes=core&include=core_scheme_super-hero-civil-war");
        Assert.Equal((4, 2, 1), ((int)limits!["heroes"]!, (int)limits["villainGroups"]!, (int)limits["henchmanGroups"]!));
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync("/api/limits?players=6")).StatusCode);
    }

    [Fact]
    public async Task The_cards_endpoint_lists_each_choosable_card_with_its_kind_and_the_boxes_that_hold_it()
    {
        var cards = await Client().GetFromJsonAsync<JsonArray>("/api/cards");

        Assert.Equal(SetupCodes.CardIds(Catalog).Count(), cards!.Count);
        var wolverine = cards.Single(card => (string)card!["id"]! == "core_hero_wolverine")!;
        Assert.Equal(("Wolverine", "hero", "core"), ((string)wolverine["name"]!, (string)wolverine["kind"]!, (string)wolverine["boxes"]![0]!));
    }

    private HttpClient Client(IRandomSource? random = null)
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(host => host.ConfigureTestServices(services =>
        {
            if (random is not null)
            {
                services.AddSingleton(random);
            }
        }));
        _hosts.Add(factory);
        return factory.CreateClient();
    }
}
