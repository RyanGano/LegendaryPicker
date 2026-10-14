using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegendaryPickerService.Catalog;

// The difficulty tags for one box, read from Data/Difficulty/<box-id>.json beside the box files (#263, #264).
// Names, numbers and closed-vocabulary tags only, never card text. Source is a key in the box file's sources for
// where the cards were read. A box with no file is valid and rates neutral.
public sealed record BoxDifficulty(int SchemaVersion, string Box, string Source, IReadOnlyList<HeroDifficulty> Heroes);

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
