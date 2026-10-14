using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Turns drawn cards into what the scorer reads: a Hero's tagged cards and the tags of the opposition (#264, #265). The
// response rates a finished setup with it, and the generator rates the options of a draw with it (#267).
public static class DifficultyInputs
{
    public static ScoredHero Scored(BoxCatalog catalog, Hero hero, double weight = 1) =>
        new(hero.Teams, catalog.Difficulty.GetValueOrDefault(BoxOf(hero.Id))?.Heroes.FirstOrDefault(tags => tags.Hero == hero.Id)?.Cards, weight);

    // The tagged parts of a Scheme, Mastermind and groups. masterStrikes, twists and henchmanCards are how many the Villain
    // Deck holds (or is expected to hold); termNames name teams in a reason and may be empty when no reason is read.
    public static ScoredOpposition Opposition(
        BoxCatalog catalog, ICard? scheme, ICard? mastermind, IEnumerable<ICard> villainGroups, IEnumerable<ICard> henchmanGroups,
        int masterStrikes, int twists, int henchmanCards, IReadOnlyDictionary<string, string> termNames)
    {
        var parts = new List<OppositionPart>();
        var amplified = new List<OppositionAxis>();

        // A tag's timing: the ones it names, or the one its holder implies.
        IReadOnlyList<Timing> When(OppositionTag tag, Timing implied) => tag.On ?? [implied];
        BoxDifficulty? Of(string id) => catalog.Difficulty.GetValueOrDefault(BoxOf(id));

        if (mastermind is not null
            && Of(mastermind.Id)?.Masterminds?.FirstOrDefault(tags => tags.Mastermind == mastermind.Id) is { } mastermindTags)
        {
            var box = BoxOf(mastermind.Id);
            if (mastermindTags.MasterStrike is { } strike)
            {
                parts.Add(new OppositionPart($"{mastermind.Name}'s Master Strikes", box, strike, When(strike, Timing.Strike), masterStrikes));
            }

            foreach (var tactic in mastermindTags.Tactics ?? [])
            {
                parts.Add(new OppositionPart($"{mastermind.Name}'s {tactic.Name} Tactic", box, tactic, When(tactic, Timing.Fight), 1));
            }
        }

        foreach (var group in villainGroups)
        {
            foreach (var card in Of(group.Id)?.VillainGroups?.FirstOrDefault(tags => tags.Group == group.Id)?.Cards ?? [])
            {
                parts.Add(new OppositionPart(card.Name!, BoxOf(group.Id), card, When(card, Timing.Fight), card.Copies!.Value));
            }
        }

        // A Henchman Group's cards are copies of one card, as many as the Villain Deck holds of its groups.
        var henchmen = henchmanGroups.ToList();
        foreach (var group in henchmen)
        {
            var copies = henchmanCards / (double)henchmen.Count;
            foreach (var card in Of(group.Id)?.HenchmanGroups?.FirstOrDefault(tags => tags.Group == group.Id)?.Cards ?? [])
            {
                parts.Add(new OppositionPart(group.Name, BoxOf(group.Id), card, When(card, Timing.Fight), copies));
            }
        }

        if (scheme is not null && Of(scheme.Id)?.Schemes?.FirstOrDefault(tags => tags.Scheme == scheme.Id) is { } schemeTags)
        {
            amplified.AddRange(schemeTags.Amplifies ?? []);
            if (schemeTags.Twist is { } twist)
            {
                parts.Add(new OppositionPart($"{scheme.Name}'s Twists", BoxOf(scheme.Id), twist, When(twist, Timing.Twist), twists));
            }
        }

        return new ScoredOpposition(parts, amplified, termNames);
    }

    private static string BoxOf(string id) => id.Split('_')[0];
}
