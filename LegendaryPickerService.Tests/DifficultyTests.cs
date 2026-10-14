using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The difficulty rating (#264): the Data/Difficulty loader, the Hero-Deck scorer, and the response field.
public sealed class DifficultyTests : IDisposable
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);

    private readonly string _boxes = Directory.CreateTempSubdirectory("legendary-boxes-").FullName;
    private readonly string _difficulty = Directory.CreateTempSubdirectory("legendary-difficulty-").FullName;

    public DifficultyTests() =>
        File.Copy(Path.Combine(BoxCatalog.DefaultDirectory, "core.json"), Path.Combine(_boxes, "core.json"));

    public void Dispose()
    {
        Directory.Delete(_boxes, recursive: true);
        Directory.Delete(_difficulty, recursive: true);
    }

    [Fact]
    public void The_core_box_has_difficulty_data_for_all_15_Heroes()
    {
        Assert.Equal(15, Catalog.Difficulty["core"].Heroes.Count);
    }

    [Fact]
    public void A_valid_file_loads()
    {
        Write(_ => { });

        Assert.Single(BoxCatalog.Load(_boxes, _difficulty).Difficulty["core"].Heroes);
    }

    [Fact]
    public void Rejects_an_unknown_field()
    {
        Write(file => Card(file)["wants"] = new JsonArray());

        AssertRejected("wants");
    }

    [Fact]
    public void Rejects_a_missing_value()
    {
        Write(file => Card(file).Remove("cost"));

        AssertRejected("cost");
    }

    [Fact]
    public void Rejects_a_Hero_the_box_does_not_declare()
    {
        Write(file => file["heroes"]![0]!["hero"] = "core_hero_not-a-hero");

        AssertRejected("core_hero_not-a-hero");
    }

    [Fact]
    public void Rejects_a_class_outside_the_closed_vocabulary()
    {
        Write(file => Card(file)["class"] = "psychic");

        AssertRejected("HeroClass");
    }

    [Fact]
    public void Rejects_a_source_the_box_does_not_list()
    {
        Write(file => file["source"] = "nope");

        AssertRejected("nope");
    }

    [Fact]
    public void A_deck_of_expensive_cards_rates_harder_than_a_deck_of_cheap_ones()
    {
        var tuning = DifficultyTuning.Default;
        var cheap = Score(tuning, Hero(["x-men"], 5, 2, 5, 3, 3, 4, 1, 5));
        var costly = Score(tuning, Hero(["x-men"], 5, 6, 5, 7, 3, 7, 1, 8));

        Assert.True(costly.Rating > cheap.Rating, $"cheap {cheap.Rating}, costly {costly.Rating}");
        Assert.Equal(tuning.BandNames[cheap.Rating - 1], cheap.Band);
    }

    [Fact]
    public void A_box_with_no_difficulty_data_rates_3()
    {
        var rating = DifficultyScorer.Score([new ScoredHero(["x-men"], null)], DifficultyTuning.Default);

        Assert.Equal(3, rating.Rating);
        Assert.Equal("Heavy Hitter", rating.Band);
    }

    [Fact]
    public void A_scripted_draw_carries_a_difficulty_block()
    {
        var generator = new SetupGenerator(Catalog);

        var body = Assert.IsType<SetupBody>(SetupResponse.From(generator.Generate(2, ["core"], new ScriptedRandom(0, 0)), Catalog));

        Assert.InRange(body.Difficulty.Rating, 1, 5);
        Assert.Equal(DifficultyTuning.Default.BandNames[body.Difficulty.Rating - 1], body.Difficulty.Band);
        Assert.NotEmpty(body.Difficulty.Reasons);
    }

    // One Hero of one team with four named cards, given as (copies, cost) pairs in order, all of one class.
    private static ScoredHero Hero(string[] teams, params int[] copiesAndCosts) =>
        new(teams, Enumerable.Range(0, copiesAndCosts.Length / 2)
            .Select(i => new HeroCardDifficulty($"Card {i}", copiesAndCosts[i * 2], [HeroClass.Strength], copiesAndCosts[i * 2 + 1], CardEconomy.Attack))
            .ToList());

    private static Difficulty Score(DifficultyTuning tuning, ScoredHero hero) => DifficultyScorer.Score([hero], tuning);

    private static JsonObject Card(JsonObject file) => file["heroes"]![0]!["cards"]![0]!.AsObject();

    private void Write(Action<JsonObject> edit)
    {
        var file = JsonNode.Parse("""
            {
              "schemaVersion": 1,
              "box": "core",
              "source": "Q",
              "heroes": [
                { "hero": "core_hero_cyclops", "cards": [
                  { "name": "Determination", "copies": 5, "class": "strength", "cost": 2, "economy": "recruit" },
                  { "name": "Optic Blast", "copies": 5, "class": ["ranged", "tech"], "cost": 3, "economy": "attack" } ] }
              ]
            }
            """)!.AsObject();
        edit(file);
        File.WriteAllText(Path.Combine(_difficulty, "core.json"), file.ToJsonString());
    }

    private void AssertRejected(string expectedInMessage)
    {
        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(_boxes, _difficulty));

        Assert.Contains("core.json", error.Message);
        Assert.Contains(expectedInMessage, error.Message);
    }
}
