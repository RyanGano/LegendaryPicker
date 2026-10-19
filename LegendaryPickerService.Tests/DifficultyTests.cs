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
        Write(file => Card(file)["mood"] = new JsonArray());

        AssertRejected("mood");
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
                [Timing.Strike], 30)],
            [], new Dictionary<string, string>());

        var met = DifficultyScorer.Score(deck, tuning, Strikes("strength"));
        var failed = DifficultyScorer.Score(deck, tuning, Strikes("tech"));

        Assert.True(failed.Rating > met.Rating, $"met {met.Rating}, failed {failed.Rating}");
        Assert.Contains(failed.Reasons, reason => reason.StartsWith("Test Master Strikes: show Tech or suffer Wounds; 0 of 14 Hero cards are Tech"));
        Assert.DoesNotContain(met.Reasons, reason => reason.Contains("Test Master Strikes"));
    }

    [Fact]
    public void A_check_on_a_team_another_box_defines_names_that_team()
    {
        var deck = new[] { Hero(["avengers"], 5, 3, 5, 3, 3, 4, 1, 5) };
        var opposition = new ScoredOpposition(
            [new OppositionPart("Test Master Strikes", "dark-city",
                new OppositionTag(Check: new OppositionCheck(new Requirement(RequirementKind.Team, "x-men"), OppositionAxis.Wounds), Severity: 3),
                [Timing.Strike], 30)],
            [], new Dictionary<string, string> { ["core_term_x-men"] = "X-Men" });

        var reasons = DifficultyScorer.Score(deck, DifficultyTuning.Default, opposition).Reasons;

        Assert.Contains(reasons, reason => reason.StartsWith("Test Master Strikes: show X-Men or suffer Wounds"));
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
    public void A_card_listed_once_per_timing_scores_each_effect_at_only_its_own_timing()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [
              { "name": "Viper", "copies": 2, "inflicts": [["wounds", 1]], "on": ["ambush"] },
              { "name": "Viper", "copies": 2, "inflicts": [["deck-thinning", 1]], "on": ["fight", "escape"] }] }]
            """));
        var catalog = BoxCatalog.Load(_boxes, _difficulty);
        var hydra = catalog.Boxes.Single().VillainGroups.Single(group => group.Id == "core_villain_hydra");

        var parts = DifficultyInputs.Opposition(catalog, null, null, [hydra], [], 5, 8, 0, 2, Ruleset.FirstEdition, new Dictionary<string, string>()).Parts;

        Assert.Equal(
            [(OppositionAxis.Wounds, "Ambush"), (OppositionAxis.DeckThinning, "Fight, Escape")],
            parts.Select(part => (part.Tag.Inflicts!.Single().Axis, string.Join(", ", part.Timings))));
    }

    [Fact]
    public void Rejects_a_card_whose_entries_disagree_on_copies()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [
              { "name": "Viper", "copies": 2, "inflicts": [["wounds", 1]], "on": ["ambush"] },
              { "name": "Viper", "copies": 1, "inflicts": [["deck-thinning", 1]], "on": ["fight"] }] }]
            """));

        AssertRejected("different copies");
    }

    [Fact]
    public void Rejects_a_Villain_card_with_no_name()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [{ "copies": 2, "inflicts": [["wounds", 1]], "on": ["ambush"] }] }]
            """));

        AssertRejected("needs a name");
    }

    [Fact]
    public void Rejects_a_Villain_card_that_does_not_say_when_it_fires()
    {
        Write(file => file["villainGroups"] = Parse("""
            [{ "group": "core_villain_hydra", "cards": [{ "name": "Viper", "copies": 1, "inflicts": [["wounds", 1]] }] }]
            """));

        AssertRejected("\"on\"");
    }

    // The Heroes' wants and answers (#266)

    [Fact]
    public void A_Hero_that_answers_Wounds_rates_easier_against_a_Wound_heavy_opposition_and_says_so()
    {
        var tuning = DifficultyTuning.Default;
        var plain = Hero(["x-men"], 5, 3, 5, 3, 3, 4, 1, 5);
        var healer = new ScoredHero(["avengers"], [new HeroCardDifficulty("Healer", 6, [HeroClass.Strength], 3, CardEconomy.Attack,
            Answers: [new HeroAnswer(AnswerAxis.Wounds, 3)])]);
        var woundy = new ScoredOpposition(
            [new OppositionPart("Test Twists", "core", new OppositionTag(Inflicts: [new Inflict(OppositionAxis.Wounds, 3)]), [Timing.Twist], 20)],
            [], new Dictionary<string, string>());

        var without = DifficultyScorer.Expected([plain], tuning, woundy);
        var with = DifficultyScorer.Expected([plain, healer], tuning, woundy);
        var reasons = DifficultyScorer.Score([plain, healer], tuning, woundy).Reasons;

        Assert.True(with < without, $"without {without:0.00}, with {with:0.00}");
        Assert.Contains(reasons, reason => reason.Contains("Hero cards answer Wounds"));
    }

    [Fact]
    public void A_card_that_wants_X_Men_is_weaker_with_no_X_Men_in_the_rest_of_the_deck_and_says_so()
    {
        var tuning = DifficultyTuning.Default;
        // One card of the wanting Hero is an X-Men want itself: its own team never meets the want.
        var wanter = new ScoredHero(["x-men"], [new HeroCardDifficulty("United", 14, [HeroClass.Ranged], 4, CardEconomy.Attack,
            Wants: [new HeroWant(WantKind.Team, "x-men", 3)])]);
        var names = new Dictionary<string, string> { ["core_term_x-men"] = "X-Men" };
        var none = Hero(["avengers"], 5, 4, 5, 4, 3, 4, 1, 4);
        var some = Hero(["x-men"], 5, 4, 5, 4, 3, 4, 1, 4);
        var opposition = new ScoredOpposition([], [], names);

        var alone = DifficultyScorer.Score([wanter, none], tuning, opposition);
        var together = DifficultyScorer.Score([wanter, some], tuning, opposition);

        Assert.True(DifficultyScorer.Expected([wanter, none], tuning) > DifficultyScorer.Expected([wanter, some], tuning));
        Assert.Contains("1 Hero wants X-Men teammates, but no other X-Men Hero is drawn.", alone.Reasons);
        Assert.DoesNotContain(together.Reasons, reason => reason.Contains("teammates"));
    }

    [Fact]
    public void Rejects_a_want_kind_outside_the_closed_vocabulary()
    {
        Write(file => Card(file)["wants"] = Parse("""[["mood", "x-men", 1]]"""));

        AssertRejected("WantKind");
    }

    [Fact]
    public void Rejects_a_want_that_names_a_team_the_box_does_not_declare()
    {
        Write(file => Card(file)["wants"] = Parse("""[["team", "inhumans", 1]]"""));

        AssertRejected("inhumans");
    }

    [Fact]
    public void Accepts_a_want_that_names_a_team_another_loaded_box_declares()
    {
        File.Copy(Path.Combine(BoxCatalog.DefaultDirectory, "dark-city.json"), Path.Combine(_boxes, "dark-city.json"));
        Write(file => Card(file)["wants"] = Parse("""[["team", "x-force", 1]]"""));

        var wants = BoxCatalog.Load(_boxes, _difficulty).Difficulty["core"].Heroes[0].Cards[0].Wants;

        Assert.Equal("x-force", Assert.Single(wants!).Value);
    }

    [Fact]
    public void Rejects_a_want_strength_out_of_range()
    {
        Write(file => Card(file)["wants"] = Parse("""[["class", "tech", 4]]"""));

        AssertRejected("strength");
    }

    [Fact]
    public void Rejects_an_answer_axis_outside_the_closed_vocabulary()
    {
        Write(file => Card(file)["answers"] = Parse("""[["despair", 1]]"""));

        AssertRejected("AnswerAxis");
    }

    [Fact]
    public void Rejects_a_self_cost_outside_the_closed_vocabulary()
    {
        Write(file => Card(file)["selfCost"] = Parse("""["sulk"]"""));

        AssertRejected("SelfCost");
    }

    [Fact]
    public void Rejects_a_draw_out_of_range()
    {
        Write(file => Card(file)["draw"] = 4);

        AssertRejected("draw");
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
              "schemaVersion": 4,
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
