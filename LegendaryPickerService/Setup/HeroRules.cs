using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// A Scheme's rules on the Heroes of one setup: how many Hero Deck slots there are, the Scheme's required
// Heroes and Hero counts, whether two Heroes may share a Hero Name, and each Hero it draws outside the Hero
// Deck. It answers whether a partial choice of Heroes can still be completed, so the generator can drop a
// Scheme whose rules the included Heroes can't meet and keep every draw away from a dead end. A Hero count
// counts only the Heroes in the Hero Deck, never those drawn outside it (D-readings).
internal sealed class HeroRules
{
    private readonly IReadOnlyList<Hero> _heroes;
    private readonly IReadOnlyList<HeroCount> _counts;
    private readonly bool _distinctNames;
    private readonly Dictionary<Hero, string> _kinds;

    // With a team split, the rules for each way of giving its counts to different teams of the included Heroes, as
    // exact team counts; the Heroes can be completed when any one of them can.
    private readonly List<HeroRules>? _splits;

    // heroes are the included Heroes in catalog order; outsideSlots has one entry per Hero drawn outside
    // the Hero Deck, in the order the Scheme lists its draws.
    public HeroRules(IReadOnlyList<Hero> heroes, int deckSlots, SchemeSetup scheme, IReadOnlyList<OutsideHeroes> outsideSlots)
    {
        _heroes = heroes;
        _counts = scheme.HeroCounts ?? [];
        _distinctNames = scheme.DistinctHeroNames?.Value ?? false;
        DeckSlots = deckSlots;
        OutsideSlots = outsideSlots;

        if (scheme.TeamSplit is { } split)
        {
            _splits = TeamsFor(heroes, split.Value)
                .Select(teams => new HeroRules(heroes, deckSlots, scheme with
                {
                    TeamSplit = null,
                    HeroCounts = [.. _counts, .. teams.Zip(split.Value, (team, count) => new HeroCount(split.Source, Team: team, Exactly: count))],
                }, outsideSlots))
                .ToList();
        }

        // A Hero's kind: which counts it matches, which draws outside the Hero Deck it fits, and, when no
        // two Heroes may share a Hero Name, its Hero Name if another included Hero has it too. Heroes of one
        // kind are interchangeable to every rule here.
        var sharedNames = _distinctNames
            ? heroes.GroupBy(hero => hero.NameOfHero).Where(name => name.Count() > 1).Select(name => name.Key).ToHashSet()
            : [];
        var rules = outsideSlots.Distinct().ToList();
        _kinds = heroes.ToDictionary(hero => hero, hero => string.Join(
            '|',
            string.Concat(_counts.Select(count => Matches(count, hero) ? '1' : '0')),
            string.Concat(rules.Select(rule => Selects(rule, hero) ? '1' : '0')),
            sharedNames.Contains(hero.NameOfHero) ? hero.NameOfHero : ""));
    }

    public int DeckSlots { get; }

    public IReadOnlyList<OutsideHeroes> OutsideSlots { get; }

    public static bool Selects(OutsideHeroes rule, Hero hero) =>
        (rule.Hero is null || hero.Id == rule.Hero)
        && (rule.HeroName is null || hero.NameOfHero == rule.HeroName)
        && (rule.HeroNames is null || rule.HeroNames.Value.Contains(hero.NameOfHero))
        && (rule.Team is null || hero.Team == rule.Team);

    // Whether the Heroes chosen so far can be completed: every Hero outside the Hero Deck, then the rest of
    // the Hero Deck within the Scheme's Hero counts, each from the Heroes not yet used. chosenOutside fills
    // the first of OutsideSlots. The Heroes outside come first because their draws are few and often narrow,
    // so one that can't be met fails at once rather than after every possible Hero Deck.
    //
    // It searches the Heroes in catalog order and gives up on a branch as soon as too few usable Heroes are
    // left to finish it. At each step it tries only the first usable Hero of each kind, which keeps the
    // search to the few kinds of Hero the rules tell apart rather than every combination of Heroes.
    public bool CanComplete(IReadOnlyList<Hero> chosenDeck, IReadOnlyList<Hero> chosenOutside)
    {
        if (_splits is not null)
        {
            return _splits.Any(rules => rules.CanComplete(chosenDeck, chosenOutside));
        }

        var deck = new List<Hero>();
        var outside = new List<Hero>();

        // The chosen Heroes themselves must be usable together, each where it was chosen.
        foreach (var hero in chosenDeck)
        {
            if (!Usable(hero, deck, outside))
            {
                return false;
            }

            deck.Add(hero);
        }

        foreach (var (hero, rule) in chosenOutside.Zip(OutsideSlots))
        {
            if (!Selects(rule, hero) || !Usable(hero, deck, outside))
            {
                return false;
            }

            outside.Add(hero);
        }

        // Whether at least needed usable Heroes that fit are left from position from, counting one per Hero
        // Name when no two Heroes may share one.
        bool Enough(int from, int needed, Func<Hero, bool> fits) =>
            _heroes.Skip(from)
                .Where(hero => fits(hero) && Usable(hero, deck, outside))
                .DistinctBy(hero => _distinctNames ? hero.NameOfHero : hero.Id)
                .Take(needed)
                .Count() == needed;

        // Tries each kind of usable Hero that fits at this step, from position from, until one completes.
        bool TryEach(List<Hero> chosen, int from, Func<Hero, bool> fits, Func<int, bool> next)
        {
            var tried = new HashSet<string>();
            for (var i = from; i < _heroes.Count; i++)
            {
                var hero = _heroes[i];
                if (!fits(hero) || !Usable(hero, deck, outside) || !tried.Add(_kinds[hero]))
                {
                    continue;
                }

                chosen.Add(hero);
                var done = next(i + 1);
                chosen.RemoveAt(chosen.Count - 1);
                if (done)
                {
                    return true;
                }
            }

            return false;
        }

        // Whether the Hero Deck's counts can still be met from position from: none is past its exact bound,
        // the slots left can hold what they still need, and each has enough usable Heroes left for it. A Hero
        // has one team, so counts of different teams need different Heroes, and the slots left must hold the
        // most each team still needs, added up. Those cheap checks come before any search for usable Heroes,
        // so each way of a team split that the Heroes chosen so far rule out fails at once (#93).
        bool CountsReachable(int from)
        {
            var left = DeckSlots - deck.Count;
            var needs = _counts
                .Select(count => (Count: count, Have: deck.Count(hero => Matches(count, hero))))
                .Select(need => (need.Count, need.Have, Needed: (need.Count.AtLeast ?? need.Count.Exactly!.Value) - need.Have))
                .ToList();
            var neededByTeams = needs
                .Where(need => need.Count.Team is not null && need.Needed > 0)
                .GroupBy(need => need.Count.Team)
                .Sum(team => team.Max(need => need.Needed));
            if (neededByTeams > left || needs.Any(need => need.Have > need.Count.Exactly || need.Needed > left))
            {
                return false;
            }

            return needs.All(need => need.Needed <= 0 || Enough(from, need.Needed, hero => Matches(need.Count, hero)));
        }

        // The Heroes of one draw are tried in catalog order, so each set of them is tried once. The Hero
        // Deck's counts are checked at every step too, since each Hero outside can use up one the Hero Deck
        // needs; otherwise a Hero Deck that can't be completed would only fail after every set of Heroes
        // outside it had been tried.
        bool FillOutside(int from)
        {
            if (outside.Count == OutsideSlots.Count)
            {
                return FillDeck(0);
            }

            var rule = OutsideSlots[outside.Count];
            var sameDraw = outside.Count + 1 < OutsideSlots.Count && OutsideSlots[outside.Count + 1] == rule;
            return CountsReachable(0)
                && Enough(0, OutsideSlots.Skip(outside.Count).Count(other => other == rule), hero => Selects(rule, hero))
                && TryEach(outside, from, hero => Selects(rule, hero), next => FillOutside(sameDraw ? next : 0));
        }

        bool FillDeck(int from)
        {
            var left = DeckSlots - deck.Count;
            return CountsReachable(from)
                && (left == 0 || (Enough(from, left, _ => true) && TryEach(deck, from, _ => true, FillDeck)));
        }

        return deck.Count <= DeckSlots && FillOutside(0);
    }

    // Each way of giving a team split's counts to different teams, one team per count, from the teams with at least
    // that many included Heroes. Counts that are equal are given teams in catalog order, so no way is listed twice.
    private static IEnumerable<string[]> TeamsFor(IReadOnlyList<Hero> heroes, int[] counts)
    {
        var teams = heroes.Select(hero => hero.Team).OfType<string>().Distinct().ToList();
        IEnumerable<string[]> From(int index, string[] chosen)
        {
            if (index == counts.Length)
            {
                return [chosen];
            }

            var after = index > 0 && counts[index - 1] == counts[index] ? teams.IndexOf(chosen[index - 1]) + 1 : 0;
            return teams.Skip(after)
                .Where(team => !chosen.Contains(team) && heroes.Count(hero => hero.Team == team) >= counts[index])
                .SelectMany(team => From(index + 1, [.. chosen, team]));
        }

        return From(0, []);
    }

    private static bool Matches(HeroCount count, Hero hero) =>
        count.Team is { } team ? hero.Team == team : hero.NameOfHero == count.HeroName;

    // A Hero is used once per setup and, when no two Heroes may share a Hero Name, only while no chosen Hero
    // has its Hero Name.
    private bool Usable(Hero hero, IEnumerable<Hero> deck, IEnumerable<Hero> outside) =>
        !deck.Concat(outside).Any(other => other == hero || (_distinctNames && other.NameOfHero == hero.NameOfHero));
}
