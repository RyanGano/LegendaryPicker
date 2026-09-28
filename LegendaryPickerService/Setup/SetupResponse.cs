using System.Text.Json.Serialization;

namespace LegendaryPickerService.Setup;

// The body GET /api/setup returns: a generator result reduced to what the result checklist shows.
// Kind tells the client which shape it holds, "setup" or "noEligibleScheme".
public abstract record SetupResponse
{
    public abstract string Kind { get; }

    public static SetupResponse From(GenerationResult result) => result switch
    {
        SetupResult setup => new SetupBody(
            setup.Players,
            new Component(setup.Scheme.Id, setup.Scheme.Name),
            new Component(setup.Mastermind.Id, setup.Mastermind.Name),
            setup.VillainGroups.Select(group => new Component(group.Id, group.Name)).ToList(),
            setup.HenchmanGroups.Select(group => new Component(group.Id, group.Name)).ToList(),
            setup.Heroes.Select(hero => new Component(hero.Id, hero.Name)).ToList(),
            setup.VillainDeck,
            setup.HeroDeck,
            setup.TwistsBesideScheme,
            setup.Stacks,
            setup.PlayerDeck,
            setup.Notes),
        NoEligibleScheme none => new NoEligibleSchemeBody(
            none.Players,
            $"No Scheme can be set up legally for {none.Players} player{(none.Players == 1 ? "" : "s")} with the included boxes."),
        _ => throw new ArgumentOutOfRangeException(nameof(result), result, "Unknown generation result."),
    };
}

public sealed record SetupBody(
    int Players,
    Component Scheme,
    Component Mastermind,
    IReadOnlyList<Component> VillainGroups,
    IReadOnlyList<Component> HenchmanGroups,
    IReadOnlyList<Component> Heroes,
    VillainDeck VillainDeck,
    HeroDeck HeroDeck,
    int TwistsBesideScheme,
    SetupStacks Stacks,
    PlayerDeck PlayerDeck,
    IReadOnlyList<RuleNote> Notes) : SetupResponse
{
    [JsonPropertyOrder(-1)]
    public override string Kind => "setup";
}

public sealed record NoEligibleSchemeBody(int Players, string Message) : SetupResponse
{
    [JsonPropertyOrder(-1)]
    public override string Kind => "noEligibleScheme";
}

// A chosen Scheme, Mastermind, group or Hero: its catalog id and display name.
public sealed record Component(string Id, string Name);
