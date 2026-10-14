using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegendaryPickerService.Catalog;

// The difficulty tags for one box, read from Data/Difficulty/<box-id>.json beside the box files (#263, #264).
// Names, numbers and closed-vocabulary tags only, never card text. Source is a key in the box file's sources for
// where the cards were read. A box with no file is valid and rates neutral.
// The opposition lists are optional: a box may tag its Heroes only (#265).
public sealed record BoxDifficulty(
    int SchemaVersion,
    string Box,
    string Source,
    IReadOnlyList<HeroDifficulty> Heroes,
    IReadOnlyList<MastermindDifficulty>? Masterminds = null,
    IReadOnlyList<GroupDifficulty>? VillainGroups = null,
    IReadOnlyList<GroupDifficulty>? HenchmanGroups = null,
    IReadOnlyList<SchemeDifficulty>? Schemes = null);

// What a Mastermind inflicts: its Master Strike and the Tactics whose Fight effects hurt the players.
public sealed record MastermindDifficulty(string Mastermind, OppositionTag? MasterStrike = null, IReadOnlyList<OppositionTag>? Tactics = null);

// A Villain Group or Henchman Group: each card with an effect, by name. A Villain card says how many copies it has.
public sealed record GroupDifficulty(string Group, IReadOnlyList<OppositionTag> Cards);

// What a Scheme's Twist does, and which axes its loss condition makes worse.
public sealed record SchemeDifficulty(string Scheme, OppositionTag? Twist = null, IReadOnlyList<OppositionAxis>? Amplifies = null);

// One effect of an opposition card, from the closed vocabulary (#263). Inflicts happen whatever the Heroes hold; Check
// is a requirement the players show or suffer OnFail; Gate is a requirement to defeat the card. Severity (1..3) belongs
// to a Check or Gate, and each Inflicts pair carries its own. On says when it fires; it defaults to Strike for a Master
// Strike, Twist for a Scheme's Twist, Fight for a Tactic or a Gate, and is required otherwise.
public sealed record OppositionTag(
    string? Name = null,
    int? Copies = null,
    OppositionCheck? Check = null,
    [property: JsonConverter(typeof(RequirementConverter))] Requirement? Gate = null,
    [property: JsonConverter(typeof(InflictsConverter))] IReadOnlyList<Inflict>? Inflicts = null,
    int? Severity = null,
    IReadOnlyList<Timing>? On = null);

public sealed record OppositionCheck([property: JsonConverter(typeof(RequirementConverter))] Requirement Requires, OppositionAxis OnFail);

public sealed record Requirement(RequirementKind Kind, string Value);

public sealed record Inflict(OppositionAxis Axis, int Severity);

public enum RequirementKind { Team, Class }

public enum OppositionAxis
{
    Wounds, BystanderPressure, HqDisruption, HandDisruption, AttackInflation, EscapePressure, DeckThinning, TwistDensity,
}

public enum Timing { Ambush, Fight, Escape, Strike, Twist }

internal sealed class KebabEnumConverter<T>() : JsonStringEnumConverter<T>(JsonNamingPolicy.KebabCaseLower, allowIntegerValues: false)
    where T : struct, Enum;

// Reads ["team", "x-men"] as a requirement.
internal sealed class RequirementConverter : JsonConverter<Requirement>
{
    public override Requirement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray || !reader.Read())
        {
            throw new JsonException("A requirement is [\"team\" or \"class\", value].");
        }

        var kind = JsonSerializer.Deserialize<RequirementKind>(ref reader, options);
        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("A requirement's value is a string.");
        }

        var value = reader.GetString()!;
        if (!reader.Read() || reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("A requirement is exactly two items.");
        }

        return new Requirement(kind, value);
    }

    public override void Write(Utf8JsonWriter writer, Requirement value, JsonSerializerOptions options) =>
        throw new NotSupportedException();
}

// Reads [["wounds", 2], ...] as inflicts.
internal sealed class InflictsConverter : JsonConverter<IReadOnlyList<Inflict>>
{
    public override IReadOnlyList<Inflict> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var inflicts = new List<Inflict>();
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Inflicts is a list of [axis, severity] pairs.");
        }

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType != JsonTokenType.StartArray || !reader.Read())
            {
                throw new JsonException("An inflict is [axis, severity].");
            }

            var axis = JsonSerializer.Deserialize<OppositionAxis>(ref reader, options);
            if (!reader.Read() || reader.TokenType != JsonTokenType.Number || !reader.TryGetInt32(out var severity))
            {
                throw new JsonException("An inflict's severity is a whole number.");
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.EndArray)
            {
                throw new JsonException("An inflict is exactly two items.");
            }

            inflicts.Add(new Inflict(axis, severity));
        }

        return inflicts;
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyList<Inflict> value, JsonSerializerOptions options) =>
        throw new NotSupportedException();
}

// One Hero's cards as drawn from the Hero Deck: Cards lists each named card once with how many copies there are.
public sealed record HeroDifficulty(string Hero, IReadOnlyList<HeroCardDifficulty> Cards);

// Copies is how many of the Hero's 14 cards are this one; Cost is the printed recruit cost. A multiclass card lists
// every class it has, as one value or an array.
public sealed record HeroCardDifficulty(
    string Name,
    int Copies,
    [property: JsonConverter(typeof(OneOrManyConverter<HeroClass>))] IReadOnlyList<HeroClass> Class,
    int Cost,
    CardEconomy Economy);

public enum HeroClass { Covert, Instinct, Ranged, Strength, Tech }

// What a card mainly gives when played: Recruit, Attack, both, or neither.
public enum CardEconomy { Recruit, Attack, Both, None }

// Reads a JSON value that is one item or an array of them as a list.
internal sealed class OneOrManyConverter<T> : JsonConverter<IReadOnlyList<T>>
{
    public override IReadOnlyList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.StartArray
            ? JsonSerializer.Deserialize<List<T>>(ref reader, options)!
            : [JsonSerializer.Deserialize<T>(ref reader, options)!];

    public override void Write(Utf8JsonWriter writer, IReadOnlyList<T> value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, value.ToList(), options);
}
