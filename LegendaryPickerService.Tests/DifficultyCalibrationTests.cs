using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The reference setups the difficulty bands were fitted to (#268): three Hero decks (cheap and concentrated, mixed,
// scattered and costly) against three oppositions (light, middle, heavy by the tagged load of their Mastermind, Scheme
// and groups), 8 of the 9 pairings, each with the band its Hero deck and opposition suggest. Each is rated from its named
// cards through the same inputs the response uses, with the usual 2-player Villain Deck (5 Master Strikes, 8 Twists,
// 10 Henchman cards), so no draw is involved. The expected bands are proposed, not owner-confirmed.
public sealed class DifficultyCalibrationTests
{
    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box Core = Catalog.Boxes.Single(box => box.Id == "core");

    private sealed record Reference(
        string Name, int Band, string[] Heroes, string Mastermind, string Scheme, string[] Villains, string Henchmen);

    private static readonly string[] EasyDeck = ["black-widow", "iron-man", "hawkeye", "hulk", "spider-man"];
    private static readonly string[] MixedDeck = ["storm", "wolverine", "captain-america", "emma-frost", "spider-man"];
    private static readonly string[] HardDeck = ["nick-fury", "cyclops", "rogue", "thor", "deadpool"];

    private static Reference Setup(string name, int band, string[] heroes, string opposition) => opposition switch
    {
        "light" => new(name, band, heroes, "loki", "portals-to-the-dark-dimension", ["enemies-of-asgard", "masters-of-evil"], "sentinel"),
        "middle" => new(name, band, heroes, "dr-doom", "negative-zone-prison-breakout", ["hydra", "spider-foes"], "sentinel"),
        _ => new(name, band, heroes, "red-skull", "replace-earths-leaders-with-killbots", ["skrulls", "brotherhood"], "sentinel"),
    };

    private static readonly Reference[] References =
    [
        Setup("Concentrated cheap Avengers against a light opposition", 1, EasyDeck, "light"),
        Setup("A mixed deck against a light opposition", 2, MixedDeck, "light"),
        Setup("Concentrated cheap Avengers against a middle opposition", 2, EasyDeck, "middle"),
        Setup("A mixed deck against a middle opposition", 3, MixedDeck, "middle"),
        Setup("Scattered costly Heroes against a middle opposition", 4, HardDeck, "middle"),
        Setup("Concentrated cheap Avengers against a heavy opposition", 3, EasyDeck, "heavy"),
        Setup("A mixed deck against a heavy opposition", 4, MixedDeck, "heavy"),
        Setup("Scattered costly Heroes against a heavy opposition", 5, HardDeck, "heavy"),
    ];

    private static Difficulty Rate(Reference reference, DifficultyTuning tuning, out double expected)
    {
        var heroes = reference.Heroes.Select(name => Core.Heroes.Single(hero => hero.Id == $"core_hero_{name}"))
            .Select(hero => DifficultyInputs.Scored(Catalog, hero)).ToList();
        var opposition = DifficultyInputs.Opposition(
            Catalog,
            Core.Schemes.Single(scheme => scheme.Id == $"core_scheme_{reference.Scheme}"),
            Core.Masterminds.Single(mastermind => mastermind.Id == $"core_mastermind_{reference.Mastermind}"),
            reference.Villains.Select(group => Core.VillainGroups.Single(card => card.Id == $"core_villain_{group}")),
            [Core.HenchmanGroups.Single(card => card.Id == $"core_henchman_{reference.Henchmen}")],
            5, 8, 10, 2, Ruleset.FirstEdition, new Dictionary<string, string>());
        expected = DifficultyScorer.Expected(heroes, tuning, opposition);
        return DifficultyScorer.Score(heroes, tuning, opposition);
    }

    [Fact]
    public void Each_reference_setup_lands_in_its_band()
    {
        var misses = new List<string>();
        foreach (var reference in References)
        {
            var rating = Rate(reference, DifficultyTuning.Default, out var expected);
            if (rating.Rating != reference.Band)
            {
                misses.Add($"{reference.Name}: expected {reference.Band}, rated {rating.Rating}");
            }

            // The continuous rating a target draw weighs by agrees with the band the same raw score falls in.
            Assert.Equal(rating.Rating, (int)Math.Round(expected));
        }

        Assert.Empty(misses);
    }

    // The rating is relative to the player count (#334). At 5 players the Villain Deck holds 4 Villain Groups and 2 Henchman
    // Groups of 10, so the cheap deck against the light opposition with its 5-player groups still rates 1 and the costly
    // deck against the heavy one 5; rated as a 2-player setup, the same cards rate harder.
    [Fact]
    public void The_extremes_keep_their_bands_at_5_players_and_the_same_cards_rate_harder_at_2()
    {
        double FivePlayerCards(string[] deck, string mastermind, string scheme, string[] villains, string[] henchmen, int ratedAt)
        {
            var heroes = deck.Select(name => DifficultyInputs.Scored(Catalog, Core.Heroes.Single(hero => hero.Id == $"core_hero_{name}"))).ToList();
            var opposition = DifficultyInputs.Opposition(
                Catalog,
                Core.Schemes.Single(card => card.Id == $"core_scheme_{scheme}"),
                Core.Masterminds.Single(card => card.Id == $"core_mastermind_{mastermind}"),
                villains.Select(group => Core.VillainGroups.Single(card => card.Id == $"core_villain_{group}")),
                henchmen.Select(group => Core.HenchmanGroups.Single(card => card.Id == $"core_henchman_{group}")),
                5, 8, 20, ratedAt, Ruleset.FirstEdition, new Dictionary<string, string>());
            return DifficultyScorer.Expected(heroes, DifficultyTuning.Default, opposition);
        }

        double Light(int ratedAt) => FivePlayerCards(EasyDeck, "loki", "portals-to-the-dark-dimension",
            ["enemies-of-asgard", "masters-of-evil", "spider-foes", "hydra"], ["sentinel", "hand-ninjas"], ratedAt);
        var heavy = FivePlayerCards(HardDeck, "red-skull", "replace-earths-leaders-with-killbots",
            ["skrulls", "brotherhood", "radiation", "spider-foes"], ["sentinel", "savage-land-mutates"], 5);

        Assert.Equal((1, 5), ((int)Math.Round(Light(5)), (int)Math.Round(heavy)));
        Assert.True(Light(2) > Light(5) + 0.5, $"{Light(2)} rated at 2 players against {Light(5)} at 5");
    }

    [Fact]
    public void The_expected_rating_follows_the_band_starts()
    {
        var reference = References[3];
        var evenBands = DifficultyTuning.Default with { BandStarts = [0.2, 0.4, 0.6, 0.8] };
        var lowBands = DifficultyTuning.Default with { BandStarts = [0.1, 0.2, 0.3, 0.4] };

        var even = Rate(reference, evenBands, out var evenExpected);
        var low = Rate(reference, lowBands, out var lowExpected);

        Assert.Equal((3, 5), (even.Rating, low.Rating));
        Assert.Equal(even.Rating, (int)Math.Round(evenExpected));
        Assert.Equal(low.Rating, (int)Math.Round(lowExpected));
    }
}
