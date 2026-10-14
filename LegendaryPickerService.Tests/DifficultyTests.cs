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

    // The opposition (#265)

    [Fact]
    public void The_same_Hero_Deck_rates_harder_against_a_check_it_cannot_show_and_names_it()
    {
        var tuning = DifficultyTuning.Default;
        var deck = new[] { Hero(["x-men"], 5, 3, 5, 3, 3, 4, 1, 5) };
        ScoredOpposition Strikes(string heroClass) => new(
            [new OppositionPart("Test Master Strikes", "core",
                new OppositionTag(Check: new OppositionCheck(new Requirement(RequirementKind.Class, heroClass), OppositionAxis.Wounds), Severity: 3),
                [Timing.Strike], 8)],
            [], new Dictionary<string, string>());

        var met = DifficultyScorer.Score(deck, tuning, Strikes("strength"));
        var failed = DifficultyScorer.Score(deck, tuning, Strikes("tech"));

        Assert.True(failed.Rating > met.Rating, $"met {met.Rating}, failed {failed.Rating}");
        Assert.Contains(failed.Reasons, reason => reason.StartsWith("Test Master Strikes: show Tech or suffer Wounds; 0 of 14 Hero cards are Tech"));
        Assert.DoesNotContain(met.Reasons, reason => reason.Contains("Test Master Strikes"));
    }

    [Fact]
    public void A_Scheme_that_amplifies_an_axis_adds_to_its_load()
    {
        var deck = new[] { Hero(["x-men"], 5, 3, 5, 3, 3, 4, 1, 5) };
        ScoredOpposition Twists(params OppositionAxis[] amplified) => new(
            [new OppositionPart("Test Twists", "core", new OppositionTag(Inflicts: [new Inflict(OppositionAxis.Wounds, 2)]), [Timing.Twist], 8)],
            amplified, new Dictionary<string, string>());

        var plain = DifficultyScorer.Score(deck, DifficultyTuning.Default, Twists());
        var amplified = DifficultyScorer.Score(deck, DifficultyTuning.Default, Twists(OppositionAxis.Wounds));

        Assert.EndsWith("Wounds.", plain.Reasons[0]);
        Assert.EndsWith("Wounds, which this Scheme makes worse.", amplified.Reasons[0]);
    }

    [Fact]
    public void A_setup_with_no_opposition_tags_still_scores_on_the_Hero_Deck()
    {
        var deck = new[] { Hero(["x-men"], 5, 3, 5, 3, 3, 4, 1, 5) };

        Assert.Equal(DifficultyScorer.Score(deck, DifficultyTuning.Default).Rating,
            DifficultyScorer.Score(deck, DifficultyTuning.Default, new ScoredOpposition([], [], new Dictionary<string, string>())).Rating);
    }

    [Fact]
    public void Changing_only_the_Scheme_changes_the_reasons()
    {
        var generator = new SetupGenerator(Catalog);
        SetupBody Draw(string schemeId)
        {
            var index = Catalog.Boxes.Single(box => box.Id == "core").Schemes.ToList().FindIndex(scheme => scheme.Id == schemeId);
            return Assert.IsType<SetupBody>(SetupResponse.From(generator.Generate(2, ["core"], new ScriptedRandom(index, 0)), Catalog));
        }

        var virus = Draw("core_scheme_legacy-virus");
        var breakout = Draw("core_scheme_negative-zone-prison-breakout");

        Assert.Contains(virus.Difficulty.Reasons, reason => reason.StartsWith("The Legacy Virus's Twists: show Tech or suffer Wounds, which this Scheme makes worse;"));
        Assert.DoesNotContain(breakout.Difficulty.Reasons, reason => reason.Contains("Legacy Virus"));
        Assert.NotEqual(virus.Difficulty.Reasons, breakout.Difficulty.Reasons);
    }

    [Fact]
    public void Rejects_a_Mastermind_the_box_does_not_declare()
    {
        Write(file => file["masterminds"] = Parse("""[{ "mastermind": "core_mastermind_nobody" }]"""));

        AssertRejected("core_mastermind_nobody");
    }

    [Fact]
    public void Rejects_a_tag_with_no_effect()
    {
        Write(file => file["villainGroups"] = Parse("""[{ "group": "core_villain_hydra", "cards": [{ "name": "Viper", "copies": 1, "on": ["fight"] }] }]"""));

        AssertRejected("no check, gate or inflicts");
    }

    [Fact]
    public void Rejects_a_severity_out_of_range()
    {
        Write(file => file["masterminds"] = Parse("""
            [{ "mastermind": "core_mastermind_loki", "masterStrike": { "check": { "requires": ["class", "tech"], "onFail": "wounds" }, "severity": 4 } }]
            """));

        AssertRejected("severity");
    }

    [Fact]
    public void Rejects_an_axis_outside_the_closed_vocabulary()
    {
        Write(file => file["masterminds"] = Parse("""
            [{ "mastermind": "core_mastermind_loki", "masterStrike": { "inflicts": [["despair", 1]] } }]
            """));

        AssertRejected("OppositionAxis");
    }

    [Fact]
    public void Rejects_a_class_outside_the_five_even_as_a_number()
    {
        Write(file => file["masterminds"] = Parse("""
            [{ "mastermind": "core_mastermind_loki", "masterStrike": { "check": { "requires": ["class", "3"], "onFail": "wounds" }, "severity": 1 } }]
            """));

        AssertRejected("class 3");
    }

    [Fact]
    public void Rejects_a_team_the_box_does_not_declare()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [{ "name": "Viper", "copies": 1, "gate": ["team", "inhumans"], "severity": 1 }] }]
            """));

        AssertRejected("inhumans");
    }

    [Fact]
    public void Rejects_a_timing_that_does_not_fit_its_card()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [{ "name": "Viper", "copies": 1, "inflicts": [["wounds", 1]], "on": ["twist"] }] }]
            """));

        AssertRejected("timing");
    }

    [Fact]
    public void Rejects_a_Villain_card_that_does_not_say_when_it_fires()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [{ "name": "Viper", "copies": 1, "inflicts": [["wounds", 1]] }] }]
            """));

        AssertRejected("\"on\"");
    }

    private static JsonNode Parse(string json) => JsonNode.Parse(json)!;

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
              "schemaVersion": 2,
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
