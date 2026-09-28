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
        WriteCoreBox(core => core["heroes"]!.AsArray().Add(new JsonObject { ["id"] = "core_hero_thor", ["name"] = "Thor again" }));

        AssertRejected("declares id core_hero_thor, already declared in");
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
        WriteBox("extra.json", extra);

        var catalog = BoxCatalog.Load(_directory);

        Assert.Equal(["core", "extra"], catalog.Boxes.Select(box => box.Id));
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

    private static JsonObject Entry(JsonObject box, string list, string id) =>
        box[list]!.AsArray().Single(entry => (string?)entry!["id"] == id)!.AsObject();
}
