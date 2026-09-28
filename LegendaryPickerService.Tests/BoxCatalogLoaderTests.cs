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

    [Fact]
    public void Rejects_another_schema_version_before_reading_its_fields()
    {
        WriteCoreBox(core =>
        {
            core["schemaVersion"] = 2;
            core["fieldOnlyVersion2Has"] = true;
        });

        AssertRejected("schemaVersion 2; this service reads version 1");
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
        var extra = JsonNode.Parse(ReadCoreBox().ToJsonString().Replace("core_", "extra_"))!.AsObject();
        extra["id"] = "extra";
        Mastermind(extra, "extra_mastermind_red-skull")["alwaysLeads"]!["groupId"] = "core_villain_hydra";
        Entry(extra, "heroes", "extra_hero_thor")["team"] = "core_term_avengers";
        WriteBox("extra.json", extra);

        var catalog = BoxCatalog.Load(_directory);

        Assert.Equal(["core", "extra"], catalog.Boxes.Select(box => box.Id));
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

    private static JsonObject Scheme(JsonObject box, string id) => Entry(box, "schemes", id);

    private static JsonObject Mastermind(JsonObject box, string id) => Entry(box, "masterminds", id);

    private static JsonObject Term(JsonObject box, string id) => Entry(box, "glossary", id);

    private static JsonObject Entry(JsonObject box, string list, string id) =>
        box[list]!.AsArray().Single(entry => (string?)entry!["id"] == id)!.AsObject();
}
