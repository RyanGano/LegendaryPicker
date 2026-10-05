using System.Text;
using System.Text.Json.Nodes;
using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Tests;

// A malformed box file must stop the service at startup, naming the file,
// rather than load with a silently defaulted value or a dangling reference.
public sealed class BoxCatalogLoaderTests : IDisposable
{
    private readonly string _directory = Directory.CreateTempSubdirectory("legendary-boxes-").FullName;

    public void Dispose() => Directory.Delete(_directory, recursive: true);

    [Fact]
    public void Loads_an_unchanged_copy_of_the_core_box()
    {
        WriteCoreBox(_ => { });

        var box = Assert.Single(BoxCatalog.Load(_directory).Boxes);
        Assert.Equal("core", box.Id);
    }

    [Fact]
    public void Loads_a_file_saved_with_a_UTF8_byte_order_mark()
    {
        WriteCoreBox(_ => { }, byteOrderMark: true);

        Assert.Equal("core", Assert.Single(BoxCatalog.Load(_directory).Boxes).Id);
    }

    [Fact]
    public void Rejects_a_misspelled_field()
    {
        WriteCoreBox(core =>
        {
            var setup = Scheme(core, "core_scheme_legacy-virus")["setup"]!.AsObject();
            setup["woundPerPlayer"] = setup["woundsPerPlayer"]!.DeepClone();
            setup.Remove("woundsPerPlayer");
        });

        AssertRejected("woundPerPlayer");
    }

    [Fact]
    public void Rejects_a_null_source()
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_legacy-virus")["setup"]!["woundsPerPlayer"]!["source"] = null);

        AssertRejected("source");
    }

    [Fact]
    public void Rejects_a_missing_required_value()
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_portals-to-the-dark-dimension")["setup"]!.AsObject().Remove("twists"));

        AssertRejected("twists");
    }

    [Theory]
    [InlineData("bystanders")]
    [InlineData("wounds")]
    [InlineData("officers")]
    public void Rejects_a_base_game_that_does_not_list_a_shared_stack(string stack)
    {
        WriteCoreBox(core => core["components"]!.AsObject().Remove(stack));

        AssertRejected($"base game core has a setup section but no components.{stack}");
    }

    // A Villainous base game lays out Bindings, Madame HYDRA and New Recruits instead of Wounds and Officers.
    [Theory]
    [InlineData("bystanders")]
    [InlineData("bindings")]
    [InlineData("madameHydra")]
    [InlineData("newRecruits")]
    public void Rejects_a_Villainous_base_game_that_does_not_list_a_Villainous_stack(string stack)
    {
        WriteCoreBox(core =>
        {
            AsVillainous(core);
            core["components"]!.AsObject().Remove(stack);
        });

        AssertRejected($"base game core has a setup section but no components.{stack}");
    }

    [Fact]
    public void Accepts_a_Villainous_base_game_without_Wounds_or_Officers()
    {
        WriteCoreBox(AsVillainous);

        Assert.Equal(Ruleset.Villainous, Assert.Single(BoxCatalog.Load(_directory).Boxes).Ruleset);
    }

    [Fact]
    public void Rejects_a_card_that_lists_a_part_twice()
    {
        WriteCoreBox(core => Entry(core, "villainGroups", "core_villain_hydra")["uses"]!.AsArray()
            .Add(new JsonObject { ["part"] = "wounds", ["source"] = "Card" }));

        AssertRejected("core_villain_hydra uses lists part wounds more than once");
    }

    [Fact]
    public void Rejects_a_part_use_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => Entry(core, "heroes", "core_hero_hulk")["uses"]![0]!["source"] = "X9 p.3");

        AssertRejected("core_hero_hulk uses cites source X9, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_part_that_is_not_a_stack()
    {
        WriteCoreBox(core => Entry(core, "heroes", "core_hero_hulk")["uses"]![0]!["part"] = "bystanders");

        AssertRejected("heroes[7].uses[0].part");
    }

    // A base game can be included alone, so its rules can use only the parts it supplies.
    [Fact]
    public void Rejects_a_base_game_whose_rules_use_a_part_it_does_not_supply()
    {
        WriteCoreBox(core => core["setup"]!["uses"]!.AsArray().Add(new JsonObject { ["part"] = "sidekicks", ["source"] = "R p.12" }));

        AssertRejected("base game core uses sidekicks in setup.uses but has no components.sidekicks");
    }

    [Theory]
    [InlineData("  ", "setup.solo has a play rule with no label")]
    [InlineData("one two three four five six seven eight nine ten eleven twelve thirteen fourteen fifteen sixteen",
        "setup.solo has a 16-word play rule label; labels are at most 15 words")]
    public void Rejects_a_Solo_play_rule_label_that_is_empty_or_too_long(string label, string expected)
    {
        WriteCoreBox(core => core["setup"]!["solo"]!["playRules"] = new JsonArray(new JsonObject { ["label"] = label, ["source"] = "R p.20" }));

        AssertRejected(expected);
    }

    [Fact]
    public void Rejects_a_Solo_play_rule_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => core["setup"]!["solo"]!["playRules"]![0]!["source"] = "X9 p.3");

        AssertRejected("setup.solo.playRules cites source X9, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_base_game_extra_Hero_count_outside_1_to_5()
    {
        WriteCoreBox(core => core["setup"]!["extraHeroes"] = new JsonArray(
            new JsonObject { ["players"] = new JsonArray(6), ["value"] = 1, ["source"] = "R p.4" }));

        AssertRejected("setup.extraHeroes names player count 6; player counts are 1 to 5");
    }

    [Fact]
    public void Rejects_another_schema_version_before_reading_its_fields()
    {
        WriteCoreBox(core =>
        {
            core["schemaVersion"] = 8;
            core["fieldOnlyVersion8Has"] = true;
        });

        AssertRejected("schemaVersion 8; this service reads version 7");
    }

    [Theory]
    [InlineData("2014")]
    [InlineData("2014-13")]
    [InlineData("October 2014")]
    public void Rejects_a_release_that_is_not_a_year_and_month(string released)
    {
        WriteCoreBox(core => core["about"]!["released"]!["value"] = released);

        AssertRejected($"box core has released {released}");
    }

    [Fact]
    public void Rejects_a_box_with_no_release()
    {
        WriteCoreBox(core => core.Remove("about"));

        AssertRejected("about");
    }

    [Fact]
    public void Rejects_a_release_citing_a_source_the_box_does_not_list()
    {
        WriteCoreBox(core => core["about"]!["released"]!["source"] = "X9");

        AssertRejected("about.released cites source X9, which is not in this box's sources");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("secondEdition")]
    public void Rejects_a_box_without_a_known_ruleset(string? ruleset)
    {
        WriteCoreBox(core =>
        {
            core.Remove("ruleset");
            if (ruleset is not null)
            {
                core["ruleset"] = ruleset;
            }
        });

        AssertRejected("ruleset");
    }

    [Fact]
    public void Rejects_a_file_without_a_schema_version()
    {
        WriteCoreBox(core => core.Remove("schemaVersion"));

        AssertRejected("schemaVersion (missing)");
    }

    [Fact]
    public void Rejects_an_Always_Leads_group_no_box_declares()
    {
        WriteCoreBox(core => Mastermind(core, "core_mastermind_loki")["alwaysLeads"]!["groupId"] = "core_villain_frost-giants");

        AssertRejected("core_mastermind_loki references villain group core_villain_frost-giants, which no loaded box declares");
    }

    [Fact]
    public void Rejects_an_Always_Leads_group_of_the_wrong_type()
    {
        WriteCoreBox(core => Mastermind(core, "core_mastermind_dr-doom")["alwaysLeads"]!["groupType"] = "villain");

        AssertRejected("references core_henchman_doombot-legion as a villain group, but it is a henchman group");
    }

    [Fact]
    public void Rejects_a_required_group_no_box_declares()
    {
        WriteCoreBox(core =>
            Scheme(core, "core_scheme_secret-invasion-of-the-skrull-shapeshifters")["setup"]!["requiredGroups"]![0]!["groupId"] = "core_villain_kree");

        AssertRejected("references villain group core_villain_kree, which no loaded box declares");
    }

    [Fact]
    public void Rejects_an_id_declared_twice()
    {
        WriteCoreBox(core => core["heroes"]!.AsArray().Add(Entry(core, "heroes", "core_hero_thor").DeepClone()));

        AssertRejected("declares id core_hero_thor, already declared in");
    }

    [Fact]
    public void Rejects_a_glossary_term_declared_twice()
    {
        WriteCoreBox(core => core["glossary"]!.AsArray().Add(Term(core, "core_term_ambush").DeepClone()));

        AssertRejected("declares id core_term_ambush, already declared in");
    }

    [Fact]
    public void Rejects_a_glossary_term_id_that_does_not_match_its_box_and_kind()
    {
        WriteCoreBox(core => Term(core, "core_term_ambush")["id"] = "core_keyword_ambush");

        AssertRejected("declares term id core_keyword_ambush; ids in this box have the form core_term_<kebab-case-name>");
    }

    [Fact]
    public void Rejects_a_term_reference_no_box_declares()
    {
        WriteCoreBox(core => Entry(core, "villainGroups", "core_villain_skrulls")["terms"]!.AsArray().Add("core_term_shapeshift"));

        AssertRejected("core_villain_skrulls references keyword term core_term_shapeshift, which no loaded box declares");
    }

    [Theory]
    [InlineData("team", "core_term_covert", "references core_term_covert as a team term, but it is a class term")]
    [InlineData("classes", "core_term_x-men", "references core_term_x-men as a class term, but it is a team term")]
    [InlineData("terms", "core_term_tech", "references core_term_tech as a keyword term, but it is a class term")]
    public void Rejects_a_Hero_term_reference_of_the_wrong_kind(string field, string termId, string expected)
    {
        WriteCoreBox(core =>
        {
            var hero = Entry(core, "heroes", "core_hero_storm");
            if (field == "team")
            {
                hero["team"] = termId;
            }
            else
            {
                hero[field]!.AsArray().Add(termId);
            }
        });

        AssertRejected($"core_hero_storm {expected}");
    }

    [Fact]
    public void Rejects_a_Scheme_that_lists_a_team_as_a_keyword()
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_legacy-virus")["terms"]!.AsArray().Add("core_term_x-men"));

        AssertRejected("core_scheme_legacy-virus references core_term_x-men as a keyword term, but it is a team term");
    }

    [Fact]
    public void Rejects_a_term_without_a_summary()
    {
        WriteCoreBox(core => Term(core, "core_term_fight").Remove("summary"));

        AssertRejected("summary");
    }

    [Fact]
    public void Rejects_a_blank_summary()
    {
        WriteCoreBox(core => Term(core, "core_term_fight")["summary"] = "  ");

        AssertRejected("core_term_fight has no summary");
    }

    [Fact]
    public void Rejects_a_summary_over_40_words()
    {
        WriteCoreBox(core => Term(core, "core_term_fight")["summary"] = string.Join(" ", Enumerable.Repeat("word", 41)));

        AssertRejected("core_term_fight has a 41-word summary; summaries are at most 40 words");
    }

    [Fact]
    public void Accepts_a_summary_of_exactly_40_words()
    {
        WriteCoreBox(core => Term(core, "core_term_fight")["summary"] = string.Join(" ", Enumerable.Repeat("word", 40)));

        Assert.Single(BoxCatalog.Load(_directory).Boxes);
    }

    [Fact]
    public void Rejects_a_term_without_a_source()
    {
        WriteCoreBox(core => Term(core, "core_term_fight").Remove("source"));

        AssertRejected("source");
    }

    [Fact]
    public void Rejects_a_term_source_the_box_does_not_link()
    {
        WriteCoreBox(core => Term(core, "core_term_fight")["source"] = "Card");

        AssertRejected("core_term_fight cites source Card, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_term_without_a_page()
    {
        WriteCoreBox(core => Term(core, "core_term_fight")["page"] = 0);

        AssertRejected("core_term_fight has page 0; a term cites the page that defines it");
    }

    [Fact]
    public void Rejects_a_source_key_listed_twice()
    {
        WriteCoreBox(core => core["sources"]!.AsArray().Add(new JsonObject { ["key"] = "R", ["url"] = "https://example.test/other" }));

        AssertRejected("box core lists source R more than once");
    }

    [Fact]
    public void Rejects_a_setup_rule_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => core["setup"]!["solo"]!["heroes"]!["source"] = "X9 p.4");

        AssertRejected("setup.solo.heroes cites source X9, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_ruling_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => core["setup"]!["rulings"]!["schemeOverridesSolo"] = "D3");

        AssertRejected("setup.rulings.schemeOverridesSolo cites source D3, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_mixing_rule_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => core["setup"]!["mixing"] = JsonNode.Parse(
            """{ "rules": "D1", "pools": "R p.6", "stacks": "X9 p.1", "startingDeckChoice": "R p.6" }"""));

        AssertRejected("setup.mixing.stacks cites source X9, which is not in this box's sources");
    }

    // Only an expansion can be played under another ruleset's base game (D-heroic), and each of its stand-ins replaces
    // one part with another.
    [Theory]
    [InlineData(true, """{ "source": "R p.6", "standIns": [] }""", "base game core has an otherRuleset section; only an expansion has one")]
    [InlineData(false, """{ "source": "X9", "standIns": [] }""", "otherRuleset.source cites source X9, which is not in this box's sources")]
    [InlineData(false, """{ "source": "R p.6", "standIns": [{ "part": "bindings", "with": "wounds", "source": "X9 p.2" }] }""",
        "otherRuleset.standIns (bindings) cites source X9, which is not in this box's sources")]
    [InlineData(false, """{ "source": "R p.6", "standIns": [{ "part": "bindings", "with": "wounds", "source": "R p.6" }, { "part": "bindings", "source": "R p.6" }] }""",
        "otherRuleset.standIns lists part bindings more than once")]
    [InlineData(false, """{ "source": "R p.6", "standIns": [{ "part": "wounds", "with": "wounds", "source": "R p.6" }] }""",
        "otherRuleset.standIns has wounds stand in for itself")]
    public void Rejects_a_malformed_other_ruleset_section(bool baseGame, string otherRuleset, string expected)
    {
        WriteCoreBox(core =>
        {
            if (!baseGame)
            {
                core.Remove("setup");
            }

            core["otherRuleset"] = JsonNode.Parse(otherRuleset);
        });

        AssertRejected(expected);
    }

    [Fact]
    public void Rejects_a_Scheme_effect_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_super-hero-civil-war")["setup"]!["heroes"]![0]!["source"] = "X9");

        AssertRejected("core_scheme_super-hero-civil-war setup.heroes cites source X9, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_Mastermind_effect_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => Mastermind(core, "core_mastermind_loki")["setup"] = new JsonObject
        {
            ["extraHeroes"] = new JsonArray(new JsonObject { ["players"] = null, ["value"] = 1, ["source"] = "X9" }),
        });

        AssertRejected("core_mastermind_loki setup.extraHeroes cites source X9, which is not in this box's sources");
    }

    [Theory]
    [InlineData("  ", "core_scheme_legacy-virus has a setup step with no label")]
    [InlineData("one two three four five six seven eight nine ten eleven twelve thirteen fourteen fifteen sixteen",
        "core_scheme_legacy-virus has a 16-word setup step label; labels are at most 15 words")]
    public void Rejects_a_setup_step_label_that_is_empty_or_too_long(string label, string expected)
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["steps"] = new JsonArray(new JsonObject { ["label"] = label, ["source"] = "Card" }));

        AssertRejected(expected);
    }

    [Fact]
    public void Rejects_a_setup_step_a_card_lists_twice()
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["steps"] = new JsonArray(
            new JsonObject { ["label"] = "Place a token on the Scheme", ["source"] = "Card" },
            new JsonObject { ["label"] = "place a token on the scheme", ["source"] = "Card" }));

        AssertRejected("core_scheme_legacy-virus lists the setup step \"place a token on the scheme\" twice");
    }

    [Fact]
    public void Accepts_a_setup_step_label_of_exactly_15_words()
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["steps"] = new JsonArray(
            new JsonObject { ["label"] = string.Join(" ", Enumerable.Repeat("word", 15)), ["source"] = "Card" }));

        Assert.Single(BoxCatalog.Load(_directory).Boxes);
    }

    [Fact]
    public void Rejects_a_Mastermind_setup_step_citing_a_source_key_the_box_does_not_list()
    {
        WriteCoreBox(core => Mastermind(core, "core_mastermind_loki")["setup"] = new JsonObject
        {
            ["steps"] = new JsonArray(new JsonObject { ["label"] = "Place a token on the Scheme", ["source"] = "X9 p.3" }),
        });

        AssertRejected("core_mastermind_loki setup.steps cites source X9, which is not in this box's sources");
    }

    [Fact]
    public void Rejects_a_player_count_listed_twice()
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_super-hero-civil-war")["setup"]!["twists"]![1]!["players"] = new JsonArray(3, 4, 5));

        AssertRejected("core_scheme_super-hero-civil-war setup.twists has more than one entry for player count 3");
    }

    [Fact]
    public void Rejects_an_entry_for_every_player_count_beside_another_entry()
    {
        WriteCoreBox(core => Mastermind(core, "core_mastermind_loki")["setup"] = new JsonObject
        {
            ["extraHeroes"] = new JsonArray(
                new JsonObject { ["players"] = null, ["value"] = 1, ["source"] = "Card" },
                new JsonObject { ["players"] = new JsonArray(1), ["value"] = 2, ["source"] = "Card" }),
        });

        AssertRejected("core_mastermind_loki setup.extraHeroes has more than one entry for player count 1");
    }

    [Fact]
    public void Rejects_a_Scheme_Henchman_card_count_listed_twice_for_a_player_count()
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["henchmanCards"] = new JsonArray(
            new JsonObject { ["players"] = null, ["value"] = 10, ["source"] = "Card" },
            new JsonObject { ["players"] = new JsonArray(1), ["value"] = 5, ["source"] = "Card" }));

        AssertRejected("core_scheme_legacy-virus setup.henchmanCards has more than one entry for player count 1");
    }

    [Fact]
    public void Rejects_a_setup_player_count_row_listed_twice()
    {
        WriteCoreBox(core => core["setup"]!["playerCounts"]![3]!["players"] = 4);

        AssertRejected("setup.playerCounts has more than one entry for player count 4");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void Rejects_a_player_count_outside_1_to_5(int players)
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_super-hero-civil-war")["setup"]!["heroes"]![0]!["players"] = new JsonArray(players));

        AssertRejected($"core_scheme_super-hero-civil-war setup.heroes names player count {players}; player counts are 1 to 5");
    }

    [Fact]
    public void Rejects_a_setup_player_count_row_outside_1_to_5()
    {
        WriteCoreBox(core => core["setup"]!["playerCounts"]![3]!["players"] = 6);

        AssertRejected("setup.playerCounts names player count 6; player counts are 1 to 5");
    }

    [Fact]
    public void Rejects_an_entry_that_names_no_player_count()
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_super-hero-civil-war")["setup"]!["heroes"]![0]!["players"] = new JsonArray());

        AssertRejected("core_scheme_super-hero-civil-war setup.heroes has an entry that names no player count");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Rejects_a_per_player_count_value_below_1(int value)
    {
        WriteCoreBox(core => Scheme(core, "core_scheme_negative-zone-prison-breakout")["setup"]!["extraHenchmanGroups"]![0]!["value"] = value);

        AssertRejected($"core_scheme_negative-zone-prison-breakout setup.extraHenchmanGroups has value {value}; values are at least 1");
    }

    [Theory]
    [InlineData("hero", "bystanders", false, "moves cards to bystanders; cards move to villainDeck, heroDeck, besideScheme, startingDecks")]
    [InlineData("hero", "heroDeck", false, "moves hero cards to heroDeck, where they come from")]
    [InlineData("wound", "startingDecks", true, "moves cards to startingDecks per player; a move to startingDecks already puts its count in each player's deck")]
    public void Rejects_a_move_with_no_legal_destination(string card, string to, bool perPlayer, string expected)
    {
        WriteCoreBox(core =>
        {
            var move = SecretInvasionMove(core);
            move["card"] = card;
            move["to"] = to;
            move["perPlayer"] = perPlayer;
        });

        AssertRejected($"core_scheme_secret-invasion-of-the-skrull-shapeshifters {expected}");
    }

    [Fact]
    public void Rejects_a_move_count_below_1()
    {
        WriteCoreBox(core => SecretInvasionMove(core)["count"]![0]!["value"] = 0);

        AssertRejected("core_scheme_secret-invasion-of-the-skrull-shapeshifters setup.moves[0].count has value 0; values are at least 1");
    }

    [Fact]
    public void Rejects_a_move_that_names_its_source()
    {
        // A move's source follows from its card kind, so a box file can't name another.
        WriteCoreBox(core => SecretInvasionMove(core)["from"] = "wounds");

        AssertRejected("from");
    }

    [Fact]
    public void Rejects_a_required_Hero_no_box_declares()
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["requiredHeroes"] = JsonNode.Parse("""[{ "heroId": "core_hero_nova", "source": "Card" }]"""));

        AssertRejected("core_scheme_legacy-virus references Hero core_hero_nova, which no loaded box declares");
    }

    [Fact]
    public void Rejects_a_Hero_count_whose_team_is_a_class()
    {
        WriteCoreBox(core =>
            LegacyVirusSetup(core)["heroCounts"] = JsonNode.Parse("""[{ "team": "core_term_tech", "atLeast": 1, "source": "Card" }]"""));

        AssertRejected("core_scheme_legacy-virus references core_term_tech as a team term, but it is a class term");
    }

    [Theory]
    [InlineData("""{ "team": "core_term_x-men", "heroName": "Storm", "atLeast": 1, "source": "Card" }""", "has a Hero count that names both a team and a Hero Name")]
    [InlineData("""{ "atLeast": 1, "source": "Card" }""", "has a Hero count that names neither a team nor a Hero Name")]
    [InlineData("""{ "heroName": "Storm", "atLeast": 1, "exactly": 1, "source": "Card" }""", "has a Hero count with both atLeast and exactly")]
    [InlineData("""{ "heroName": "Storm", "source": "Card" }""", "has a Hero count with neither atLeast nor exactly")]
    [InlineData("""{ "heroName": "Storm", "atLeast": 0, "source": "Card" }""", "has a Hero count of 0")]
    public void Rejects_a_Hero_count_that_is_not_one_bound_on_one_thing(string count, string expected)
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["heroCounts"] = new JsonArray(JsonNode.Parse(count)));

        AssertRejected($"core_scheme_legacy-virus {expected}");
    }

    [Theory]
    [InlineData("""{ "to": "heroDeck", "count": [{ "players": null, "value": 1, "source": "Card" }] }""",
        "puts Heroes outside the Hero Deck in heroDeck; they go to villainDeck, besideScheme, setAside")]
    [InlineData("""{ "to": "villainDeck", "hero": "core_hero_storm", "team": "core_term_x-men", "count": [{ "players": null, "value": 1, "source": "Card" }] }""",
        "chooses Heroes outside the Hero Deck by more than one of hero, heroName, heroNames and team")]
    [InlineData("""{ "to": "villainDeck", "count": [{ "players": null, "value": 0, "source": "Card" }] }""",
        "setup.outsideHeroes[0].count has value 0; values are at least 1")]
    public void Rejects_Heroes_outside_the_Hero_Deck_with_no_legal_draw(string outside, string expected)
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["outsideHeroes"] = new JsonArray(JsonNode.Parse(outside)));

        AssertRejected($"core_scheme_legacy-virus {expected}");
    }

    [Theory]
    [InlineData("""{ "to": "villainDeck", "cards": [{ "players": null, "value": 6, "source": "Card" }] }""",
        "puts Henchmen from outside the Villain Deck in villainDeck; they go to heroDeck, koPile")]
    [InlineData("""{ "to": "heroDeck", "cards": [{ "players": null, "value": 0, "source": "Card" }] }""",
        "setup.outsideHenchmen[0].cards has value 0; values are at least 1")]
    [InlineData("""{ "to": "heroDeck", "cards": [{ "players": null, "value": 6, "source": "Rumor" }] }""",
        "setup.outsideHenchmen[0].cards cites source Rumor, which is not in this box's sources")]
    public void Rejects_Henchmen_outside_the_Villain_Deck_with_no_legal_draw(string outside, string expected)
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["outsideHenchmen"] = new JsonArray(JsonNode.Parse(outside)));

        AssertRejected($"core_scheme_legacy-virus {expected}");
    }

    [Theory]
    [InlineData("""{ "to": "heroDeck", "count": [{ "players": null, "value": 1, "source": "Card" }] }""",
        "puts other Masterminds in heroDeck; they go to villainDeck, setAside")]
    [InlineData("""{ "to": "villainDeck", "count": [{ "players": null, "value": 3, "source": "Card" }] }""",
        "draws other Masterminds into villainDeck; tactics says how many of each one's Tactics go to the villainDeck, and only there")]
    [InlineData("""{ "to": "setAside", "count": [{ "players": null, "value": 1, "source": "Card" }], "tactics": { "value": 4, "source": "Card" } }""",
        "draws other Masterminds into setAside; tactics says how many of each one's Tactics go to the villainDeck, and only there")]
    [InlineData("""{ "to": "villainDeck", "count": [{ "players": null, "value": 3, "source": "Card" }], "tactics": { "value": 4, "source": "Card" }, "joins": { "value": "Twist 1", "source": "Card" } }""",
        "draws other Masterminds into villainDeck; joins says when one comes into play, and only for those set aside")]
    [InlineData("""{ "to": "villainDeck", "count": [{ "players": null, "value": 3, "source": "Card" }], "tactics": { "value": 0, "source": "Card" } }""",
        "puts 0 Tactics of each other Mastermind in the villainDeck; values are at least 1")]
    [InlineData("""{ "to": "setAside", "count": [{ "players": null, "value": 0, "source": "Card" }] }""",
        "setup.outsideMasterminds[0].count has value 0; values are at least 1")]
    [InlineData("""{ "to": "villainDeck", "count": [{ "players": null, "value": 3, "source": "Card" }], "tactics": { "value": 4, "source": "Rumor" } }""",
        "setup.outsideMasterminds.tactics cites source Rumor, which is not in this box's sources")]
    public void Rejects_other_Masterminds_with_no_legal_draw(string outside, string expected)
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["outsideMasterminds"] = new JsonArray(JsonNode.Parse(outside)));

        AssertRejected($"core_scheme_legacy-virus {expected}");
    }

    [Theory]
    [InlineData("""{ "groupId": "core_henchman_nobody", "groupType": "henchman", "count": [{ "players": null, "value": 2, "source": "Card" }] }""",
        "references henchman group core_henchman_nobody, which no loaded box declares")]
    [InlineData("""{ "groupId": "core_villain_hydra", "groupType": "henchman", "count": [{ "players": null, "value": 2, "source": "Card" }] }""",
        "references core_villain_hydra as a henchman group, but it is a villain group")]
    [InlineData("""{ "groupId": "core_villain_hydra", "groupType": "villain", "count": [{ "players": null, "value": 0, "source": "Card" }] }""",
        "setup.cardsBeside[0].count has value 0; values are at least 1")]
    [InlineData("""{ "groupId": "core_villain_hydra", "groupType": "villain", "count": [{ "players": null, "value": 1, "source": "Rumor" }] }""",
        "setup.cardsBeside[0].count cites source Rumor, which is not in this box's sources")]
    [InlineData("""{ "groupId": "core_villain_hydra", "groupType": "villain", "card": " ", "count": [{ "players": null, "value": 1, "source": "Card" }] }""",
        "sets a card of core_villain_hydra beside it with an empty card name")]
    public void Rejects_cards_beside_the_Scheme_without_a_group_a_count_or_a_card_name(string beside, string expected)
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["cardsBeside"] = new JsonArray(JsonNode.Parse(beside)));

        AssertRejected($"core_scheme_legacy-virus {expected}");
    }

    // Each of these rules is well formed, but no draw can meet it: without the check the Scheme would
    // silently drop out of every draw.
    [Theory]
    [InlineData(null,
        """[{ "to": "villainDeck", "hero": "core_hero_storm", "count": [{ "players": [1, 2, 3, 4], "value": 1, "source": "Card" }, { "players": [5], "value": 2, "source": "Card" }] }]""",
        "setup.outsideHeroes[0] draws 2 of Hero core_hero_storm; there is one of each Hero")]
    [InlineData("""[{ "heroId": "core_hero_storm", "source": "Card" }, { "heroId": "core_hero_storm", "source": "Card" }]""",
        null,
        "setup.requiredHeroes lists core_hero_storm more than once")]
    [InlineData("""[{ "heroId": "core_hero_storm", "source": "Card" }]""",
        """[{ "to": "setAside", "hero": "core_hero_storm", "count": [{ "players": null, "value": 1, "source": "Card" }] }]""",
        "setup.outsideHeroes[0] draws core_hero_storm outside the Hero Deck, but setup.requiredHeroes puts it in the Hero Deck")]
    [InlineData(null,
        """[{ "to": "setAside", "hero": "core_hero_storm", "count": [{ "players": [1, 2], "value": 1, "source": "Card" }] }, { "to": "villainDeck", "hero": "core_hero_storm", "count": [{ "players": [2, 3], "value": 1, "source": "Card" }] }]""",
        "setup.outsideHeroes[1] draws core_hero_storm, which an earlier entry also draws at 2 players; there is one of each Hero")]
    public void Rejects_Hero_rules_no_draw_can_meet(string? required, string? outside, string expected)
    {
        WriteCoreBox(core =>
        {
            var setup = LegacyVirusSetup(core);
            if (required is not null)
            {
                setup["requiredHeroes"] = JsonNode.Parse(required);
            }

            if (outside is not null)
            {
                setup["outsideHeroes"] = JsonNode.Parse(outside);
            }
        });

        AssertRejected($"core_scheme_legacy-virus {expected}");
    }

    // Each Scheme's own choices break one of its rules, whatever other Heroes are included, so the Scheme
    // would drop out of every draw. Rogue is given Storm's Hero Name where a case needs two Heroes to share one.
    [Theory]
    [InlineData(
        """{ "requiredHeroes": [{ "heroId": "core_hero_storm", "source": "Card" }, { "heroId": "core_hero_rogue", "source": "Card" }], "distinctHeroNames": { "value": true, "source": "Card" } }""",
        "at 1 player, whichever Heroes are included; it can be met without setup.requiredHeroes[0] or setup.requiredHeroes[1] or setup.distinctHeroNames")]
    [InlineData(
        """{ "requiredHeroes": [{ "heroId": "core_hero_storm", "source": "Card" }, { "heroId": "core_hero_wolverine", "source": "Card" }], "heroCounts": [{ "team": "core_term_x-men", "exactly": 1, "source": "Card" }] }""",
        "at 1 player, whichever Heroes are included; it can be met without setup.requiredHeroes[0] or setup.requiredHeroes[1] or setup.heroCounts[0]")]
    [InlineData(
        """{ "requiredHeroes": [{ "heroId": "core_hero_storm", "source": "Card" }], "heroCounts": [{ "team": "core_term_x-men", "exactly": 0, "source": "Card" }] }""",
        "at 1 player, whichever Heroes are included; it can be met without setup.requiredHeroes[0] or setup.heroCounts[0]")]
    [InlineData(
        """{ "outsideHeroes": [{ "to": "villainDeck", "hero": "core_hero_rogue", "count": [{ "players": [3, 4, 5], "value": 1, "source": "Card" }] }], "heroCounts": [{ "heroName": "Storm", "atLeast": 1, "source": "Card" }], "distinctHeroNames": { "value": true, "source": "Card" } }""",
        "at 3 players, whichever Heroes are included; it can be met without setup.heroCounts[0] or setup.distinctHeroNames or setup.outsideHeroes[0]")]
    [InlineData(
        """{ "requiredHeroes": [{ "heroId": "core_hero_storm", "source": "Card" }, { "heroId": "core_hero_rogue", "source": "Card" }], "heroCounts": [{ "team": "core_term_x-men", "exactly": 0, "source": "Card" }], "distinctHeroNames": { "value": true, "source": "Card" } }""",
        "at 1 player, whichever Heroes are included; no one of them can be left out to meet the rest")]
    public void Rejects_Hero_rules_that_contradict_themselves(string rules, string expected)
    {
        WriteCoreBox(core =>
        {
            Entry(core, "heroes", "core_hero_rogue")["heroName"] = "Storm";
            var setup = LegacyVirusSetup(core);
            foreach (var (name, rule) in JsonNode.Parse(rules)!.AsObject())
            {
                setup[name] = rule!.DeepClone();
            }
        });

        AssertRejected($"core_scheme_legacy-virus has Hero rules no draw can meet {expected}.");
    }

    // Only one Storm is loaded and no Nova, but an expansion could add them; until one does, the Scheme
    // drops out of the draw rather than the box failing to load.
    [Fact]
    public void Accepts_Hero_rules_only_Heroes_from_another_box_can_meet()
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["heroCounts"] = JsonNode.Parse(
            """[{ "heroName": "Storm", "exactly": 2, "source": "Card" }, { "heroName": "Nova", "atLeast": 1, "source": "Card" }]"""));

        Assert.Single(BoxCatalog.Load(_directory).Boxes);
    }

    [Fact]
    public void Accepts_one_Hero_drawn_outside_the_Hero_Deck_by_two_entries_at_different_player_counts()
    {
        WriteCoreBox(core => LegacyVirusSetup(core)["outsideHeroes"] = JsonNode.Parse(
            """[{ "to": "setAside", "hero": "core_hero_storm", "count": [{ "players": [1, 2], "value": 1, "source": "Card" }] }, { "to": "villainDeck", "hero": "core_hero_storm", "count": [{ "players": [3, 4, 5], "value": 1, "source": "Card" }] }]"""));

        Assert.Single(BoxCatalog.Load(_directory).Boxes);
    }

    [Fact]
    public void Rejects_two_boxes_with_the_same_id()
    {
        WriteCoreBox(_ => { });
        WriteBox("core-copy.json", ReadCoreBox());

        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(_directory));

        // Files load in ordinal order, so core-copy.json declares "core" first.
        Assert.Contains("core.json declares id core, already declared in", error.Message);
        Assert.Contains("core-copy.json", error.Message);
    }

    [Theory]
    [InlineData("other_hero_thor")]
    [InlineData("core_villain_thor")]
    [InlineData("core_hero_Thor")]
    [InlineData("core_hero_thor_odinson")]
    [InlineData("thor")]
    public void Rejects_a_Hero_id_that_does_not_match_its_box_and_kind(string id)
    {
        WriteCoreBox(core => core["heroes"]![0]!["id"] = id);

        AssertRejected($"declares hero id {id}; ids in this box have the form core_hero_<kebab-case-name>");
    }

    [Fact]
    public void Rejects_a_box_id_containing_an_underscore()
    {
        WriteCoreBox(core => core["id"] = "core_set");

        AssertRejected("has box id core_set");
    }

    [Fact]
    public void Resolves_a_reference_into_another_box()
    {
        WriteCoreBox(_ => { });
        var extra = ExtraCopyOfCore();
        Mastermind(extra, "extra_mastermind_red-skull")["alwaysLeads"]!["groupId"] = "core_villain_hydra";
        Entry(extra, "heroes", "extra_hero_thor")["team"] = "core_term_avengers";
        WriteBox("extra.json", extra);

        var catalog = BoxCatalog.Load(_directory);

        Assert.Equal(["core", "extra"], catalog.Boxes.Select(box => box.Id));
    }

    [Fact]
    public void Rejects_a_Hero_in_another_box_with_the_same_name()
    {
        WriteCoreBox(_ => { });
        var extra = ExtraCopyOfCore();
        Entry(extra, "heroes", "extra_hero_wolverine")["name"] = "Wolverine";
        WriteBox("extra.json", extra);

        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(_directory));

        Assert.Contains("extra.json: hero extra_hero_wolverine is named \"Wolverine\", like core_hero_wolverine in", error.Message);
        Assert.Contains("core.json", error.Message);
    }

    // A group named "Hydra" beside the core box's "HYDRA" is no easier to tell apart.
    [Fact]
    public void Rejects_a_Villain_Group_name_that_differs_only_in_case()
    {
        WriteCoreBox(_ => { });
        var extra = ExtraCopyOfCore();
        Entry(extra, "villainGroups", "extra_villain_hydra")["name"] = "Hydra";
        WriteBox("extra.json", extra);

        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(_directory));

        Assert.Contains("villain extra_villain_hydra is named \"Hydra\", like core_villain_hydra", error.Message);
    }

    // A second version of a character loads under its own name, keeping the shared Hero Name for Hero rules.
    [Fact]
    public void Accepts_a_second_version_of_a_Hero_with_a_distinguishing_name()
    {
        WriteCoreBox(_ => { });
        var extra = ExtraCopyOfCore();
        var wolverine = Entry(extra, "heroes", "extra_hero_wolverine");
        wolverine["name"] = "Wolverine (X-Force)";
        wolverine["heroName"] = "Wolverine";
        WriteBox("extra.json", extra);

        Assert.Equal(["core", "extra"], BoxCatalog.Load(_directory).Boxes.Select(box => box.Id));
    }

    [Fact]
    public void Rejects_an_expansion_whose_reference_names_a_box_that_is_not_loaded()
    {
        File.Copy(Path.Combine(MultiBoxSetupTests.FixtureDirectory, "fixture.json"), Path.Combine(_directory, "fixture.json"));

        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(_directory));

        Assert.Contains("fixture.json", error.Message);
        Assert.Contains(
            "fixture_scheme_test-heist references villain group core_villain_hydra from box core, which is not loaded", error.Message);
    }

    private void AssertRejected(string expectedInMessage)
    {
        var error = Assert.Throws<InvalidDataException>(() => BoxCatalog.Load(_directory));

        Assert.Contains("core.json", error.Message);
        Assert.Contains(expectedInMessage, error.Message);
    }

    // A copy of the core box as box "extra", every id and display name its own. Hero rules match on the
    // Hero Name, so each copied Hero keeps the core one's.
    private static JsonObject ExtraCopyOfCore()
    {
        var extra = JsonNode.Parse(ReadCoreBox().ToJsonString().Replace("core_", "extra_"))!.AsObject();
        extra["id"] = "extra";
        foreach (var list in new[] { "heroes", "villainGroups", "henchmanGroups", "masterminds", "schemes" })
        {
            foreach (var entry in extra[list]!.AsArray())
            {
                if (list == "heroes")
                {
                    entry!["heroName"] ??= (string?)entry["name"];
                }

                entry!["name"] = $"Extra {entry["name"]}";
            }
        }

        return extra;
    }

    private static JsonObject ReadCoreBox() =>
        JsonNode.Parse(File.ReadAllText(Path.Combine(BoxCatalog.DefaultDirectory, "core.json")))!.AsObject();

    private void WriteCoreBox(Action<JsonObject> edit, bool byteOrderMark = false)
    {
        var core = ReadCoreBox();
        edit(core);
        WriteBox("core.json", core, byteOrderMark);
    }

    private void WriteBox(string fileName, JsonObject box, bool byteOrderMark = false) =>
        File.WriteAllText(Path.Combine(_directory, fileName), box.ToJsonString(), new UTF8Encoding(byteOrderMark));

    // Turns a copy of the core box into a Villainous base game, with the Villainous stacks in place of
    // Wounds and Officers, which its rules recruit from.
    private static void AsVillainous(JsonObject core)
    {
        core["ruleset"] = "villainous";
        core["setup"]!["uses"] = new JsonArray(
            new JsonObject { ["part"] = "madameHydra", ["source"] = "R p.12" },
            new JsonObject { ["part"] = "newRecruits", ["source"] = "R p.12" });
        var components = core["components"]!.AsObject();
        components.Remove("wounds");
        components.Remove("officers");
        components["bindings"] = new JsonObject { ["value"] = 30, ["source"] = "R p.22" };
        components["madameHydra"] = new JsonObject { ["value"] = 12, ["source"] = "R p.22" };
        components["newRecruits"] = new JsonObject { ["value"] = 15, ["source"] = "R p.22" };
    }

    private static JsonObject Scheme(JsonObject box, string id) => Entry(box, "schemes", id);

    private static JsonObject Mastermind(JsonObject box, string id) => Entry(box, "masterminds", id);

    private static JsonObject SecretInvasionMove(JsonObject core) =>
        Scheme(core, "core_scheme_secret-invasion-of-the-skrull-shapeshifters")["setup"]!["moves"]![0]!.AsObject();

    private static JsonObject LegacyVirusSetup(JsonObject core) => Scheme(core, "core_scheme_legacy-virus")["setup"]!.AsObject();

    private static JsonObject Term(JsonObject box, string id) => Entry(box, "glossary", id);

    private static JsonObject Entry(JsonObject box, string list, string id) =>
        box[list]!.AsArray().Single(entry => (string?)entry!["id"] == id)!.AsObject();
}
