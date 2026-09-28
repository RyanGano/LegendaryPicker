using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LegendaryPickerService.Catalog;

// Every box the service knows, read once from the JSON files in Data/Boxes.
//
// Ids have the form <boxId>_<kind>_<name>, for example core_mastermind_dr-doom:
// the declaring box's id, the kind of entry, and a kebab-case name. Underscores
// separate the segments; hyphens stay inside one. A box's own id is a single
// kebab-case segment. References name a full id, so a box can reference another box's groups.
public sealed partial class BoxCatalog
{
    public const int SchemaVersion = 1;

    public static string DefaultDirectory { get; } = Path.Combine(AppContext.BaseDirectory, "Data", "Boxes");

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        // A missing required field or a null where the model forbids one is a data error, not a default.
        RespectNullableAnnotations = true,
        RespectRequiredConstructorParameters = true,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) },
    };

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]
    private static partial Regex Segment();

    public IReadOnlyList<Box> Boxes { get; }

    private BoxCatalog(IReadOnlyList<Box> boxes) => Boxes = boxes;

    public static BoxCatalog Load(string directory)
    {
        var files = Directory.GetFiles(directory, "*.json")
            .Order(StringComparer.Ordinal)
            .Select(path => (Path: path, Box: LoadBox(path)))
            .ToList();

        Validate(files);

        return new BoxCatalog(files.Select(file => file.Box).ToList());
    }

    private static Box LoadBox(string path)
    {
        try
        {
            using var stream = File.OpenRead(path);
            using var document = JsonDocument.Parse(stream);

            // Check the version before binding, so a file written for another schema reports
            // the version mismatch rather than whichever of its fields this model lacks.
            var version = document.RootElement.ValueKind == JsonValueKind.Object
                && document.RootElement.TryGetProperty("schemaVersion", out var element)
                && element.TryGetInt32(out var value)
                    ? value
                    : (int?)null;
            if (version != SchemaVersion)
            {
                throw new InvalidDataException(
                    $"{path} has schemaVersion {version?.ToString() ?? "(missing)"}; this service reads version {SchemaVersion}.");
            }

            return document.Deserialize<Box>(JsonOptions)
                ?? throw new InvalidDataException($"{path} is empty.");
        }
        catch (JsonException ex)
        {
            // Name the file: with one file per box, the JSON path alone does not say which box is broken.
            throw new InvalidDataException($"{path} is not a valid box file: {ex.Message}", ex);
        }
    }

    // Checks across every loaded box, so a reference into another box resolves.
    private static void Validate(IReadOnlyList<(string Path, Box Box)> files)
    {
        var declaredIn = new Dictionary<string, string>(StringComparer.Ordinal);

        void Declare(string path, string id)
        {
            if (!declaredIn.TryAdd(id, path))
            {
                throw new InvalidDataException($"{path} declares id {id}, already declared in {declaredIn[id]}.");
            }
        }

        foreach (var (path, box) in files)
        {
            if (!Segment().IsMatch(box.Id))
            {
                throw new InvalidDataException(
                    $"{path} has box id {box.Id}; a box id is lowercase kebab-case with no underscores.");
            }

            Declare(path, box.Id);

            foreach (var (kind, id) in DeclaredIds(box))
            {
                var segments = id.Split('_');
                if (segments.Length != 3 || segments[0] != box.Id || segments[1] != kind || !Segment().IsMatch(segments[2]))
                {
                    throw new InvalidDataException(
                        $"{path} declares {kind} id {id}; ids in this box have the form {box.Id}_{kind}_<kebab-case-name>.");
                }

                Declare(path, id);
            }
        }

        var groups = files
            .SelectMany(file => DeclaredIds(file.Box))
            .Where(entry => entry.Kind is "villain" or "henchman")
            .ToDictionary(entry => entry.Id, entry => entry.Kind, StringComparer.Ordinal);

        foreach (var (path, box) in files)
        {
            foreach (var (owner, groupId, groupType) in GroupReferences(box))
            {
                var kind = KindOf(groupType);
                if (!groups.TryGetValue(groupId, out var found))
                {
                    throw new InvalidDataException($"{path}: {owner} references {kind} group {groupId}, which no loaded box declares.");
                }

                if (found != kind)
                {
                    throw new InvalidDataException($"{path}: {owner} references {groupId} as a {kind} group, but it is a {found} group.");
                }
            }
        }
    }

    private static string KindOf(GroupType type) => type switch
    {
        GroupType.Villain => "villain",
        GroupType.Henchman => "henchman",
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

    private static IEnumerable<(string Kind, string Id)> DeclaredIds(Box box) =>
        box.Heroes.Select(x => ("hero", x.Id))
            .Concat(box.VillainGroups.Select(x => ("villain", x.Id)))
            .Concat(box.HenchmanGroups.Select(x => ("henchman", x.Id)))
            .Concat(box.Masterminds.Select(x => ("mastermind", x.Id)))
            .Concat(box.Schemes.Select(x => ("scheme", x.Id)));

    private static IEnumerable<(string Owner, string GroupId, GroupType GroupType)> GroupReferences(Box box) =>
        box.Masterminds.Select(m => (m.Id, m.AlwaysLeads.GroupId, m.AlwaysLeads.GroupType))
            .Concat(box.Schemes.SelectMany(s =>
                (s.Setup.RequiredGroups ?? []).Select(g => (s.Id, g.GroupId, g.GroupType))));
}
