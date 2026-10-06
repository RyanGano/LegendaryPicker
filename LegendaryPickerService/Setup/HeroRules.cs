using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// A Scheme's rules on the Heroes of one setup: how many Hero Deck slots there are, the Scheme's required
// Heroes and Hero counts, whether two Heroes may share a Hero Name, and each Hero it draws outside the Hero
// Deck. It answers whether a partial choice of Heroes can still be completed, so the generator can drop a
// Scheme whose rules the included Heroes can't meet and keep every draw away from a dead end. A Hero count
// counts only the Heroes in the Hero Deck, never those drawn outside it (D-readings). A Divided Card Hero with two
// teams fills either team's count but not both (#149): when the Scheme counts Heroes by team, the search sees each
// such Hero once per team, as a side, and uses at most one side of it.
internal sealed class HeroRules
{
    // How many times the search has checked the Hero Deck's counts on this thread, so a test can bound the search's
    // work without timing it (#161).
    [ThreadStatic]
    internal static int CountChecks;

    private readonly IReadOnlyList<Hero> _heroes;
    private readonly IReadOnlyList<HeroCount> _counts;
    private readonly bool _distinctNames;
    private readonly Dictionary<Hero, string> _kinds;

    // The Scheme's different draws outside the Hero Deck, and, when no two Heroes may share a Hero Name, the Hero
    // Names more than one included Hero has.
    private readonly List<OutsideHeroes> _outsideRules;
    private readonly HashSet<string> _sharedNames;

    // Whether a Hero counts as the one team it is used as, because the Scheme counts Heroes by team.
    private readonly bool _bySide;

    // With a team split, the rules for each way of giving its counts to different teams of the included Heroes, as
    // exact team counts; the Heroes can be completed when any one of them can.
    private readonly List<HeroRules>? _splits;

    // heroes are the included Heroes in catalog order; outsideSlots has one entry per Hero drawn outside
    // the Hero Deck, in the order the Scheme lists its draws.
    public HeroRules(IReadOnlyList<Hero> heroes, int deckSlots, SchemeSetup scheme, IReadOnlyList<OutsideHeroes> outsideSlots)
    {
        _counts = scheme.HeroCounts ?? [];
        _bySide = _counts.Any(count => count.Team is not null);
        _distinctNames = scheme.DistinctHeroNames?.Value ?? false;
        DeckSlots = deckSlots;
        OutsideSlots = outsideSlots;
        _outsideRules = outsideSlots.Distinct().ToList();
        _sharedNames = _distinctNames
            ? heroes.SelectMany(hero => hero.NamesOfHero).GroupBy(name => name).Where(name => name.Count() > 1).Select(name => name.Key).ToHashSet()
            : [];
        _heroes = heroes.SelectMany(SidesOf).ToList();
        _kinds = _heroes.ToDictionary(hero => hero, KindOf);

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
    }

    public int DeckSlots { get; }

    public IReadOnlyList<OutsideHeroes> OutsideSlots { get; }

    public static bool Selects(OutsideHeroes rule, Hero hero) =>
        (rule.Hero is null || hero.Id == rule.Hero)
        && (rule.HeroName is null || hero.HasHeroName(rule.HeroName))
        && (rule.HeroNames is null || rule.HeroNames.Value.Any(hero.HasHeroName))
        && (rule.HeroNameContains is null || hero.HasInHeroName(rule.HeroNameContains))
        && (rule.Team is null || hero.HasTeam(rule.Team));

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

        // A chosen Hero with two teams can be either side, so each way of using the chosen Heroes' sides is tried.
        IEnumerable<IReadOnlyList<Hero>> SidesChosen(int index) => index == chosenDeck.Count
            ? [[]]
            : SidesOf(chosenDeck[index]).SelectMany(side => SidesChosen(index + 1).Select(rest => (IReadOnlyList<Hero>)[side, .. rest]));

        return SidesChosen(0).Any(sides => CanCompleteWith(sides, chosenOutside));
    }

    private bool CanCompleteWith(IReadOnlyList<Hero> chosenDeck, IReadOnlyList<Hero> chosenOutside)
    {
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

        // Whether at least needed usable Heroes that fit are left from position from, counting each Hero once, and
        // one per first Hero Name when no two Heroes may share one. Heroes sharing a first Hero Name can't both be
        // used, so this never counts too few.
        bool Enough(int from, int needed, Func<Hero, bool> fits) =>
            _heroes.Skip(from)
                .Where(hero => fits(hero) && Usable(hero, deck, outside))
                .DistinctBy(hero => _distinctNames ? hero.NamesOfHero[0] : hero.Id)
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
        // is used as one team, so counts of different teams need different Heroes, and the slots left must hold the
        // most each team still needs, added up. Those cheap checks come before any search for usable Heroes,
        // so each way of a team split that the Heroes chosen so far rule out fails at once (#93).
        bool CountsReachable(int from)
        {
            CountChecks++;
            var left = DeckSlots - deck.Count;
            var needs = _counts
                .Select(count => (Count: count, Have: deck.Count(hero => count.Matches(hero))))
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

            return needs.All(need => need.Needed <= 0 || Enough(from, need.Needed, hero => need.Count.Matches(hero)));
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
        var teams = heroes.SelectMany(hero => hero.Teams).Distinct().ToList();
        IEnumerable<string[]> From(int index, string[] chosen)
        {
            if (index == counts.Length)
            {
                return [chosen];
            }

            var after = index > 0 && counts[index - 1] == counts[index] ? teams.IndexOf(chosen[index - 1]) + 1 : 0;
            return teams.Skip(after)
                .Where(team => !chosen.Contains(team) && heroes.Count(hero => hero.HasTeam(team)) >= counts[index])
                .SelectMany(team => From(index + 1, [.. chosen, team]));
        }

        return From(0, []);
    }

    // A Hero is used once per setup, as one side when it has two teams, and, when no two Heroes may share a Hero Name,
    // only while no chosen Hero has one of its Hero Names.
    private bool Usable(Hero hero, IEnumerable<Hero> deck, IEnumerable<Hero> outside) =>
        !deck.Concat(outside).Any(other => other.Id == hero.Id || (_distinctNames && other.SharesHeroName(hero)));

    // A Hero's kind: which counts it matches, which draws outside the Hero Deck it fits, and, when no two Heroes may
    // share a Hero Name, which of its Hero Names another included Hero has too. Heroes of one kind are interchangeable
    // to every rule here.
    private string KindOf(Hero hero) => string.Join(
        '|',
        string.Concat(_counts.Select(count => count.Matches(hero) ? '1' : '0')),
        string.Concat(_outsideRules.Select(rule => Selects(rule, hero) ? '1' : '0')),
        string.Join(',', hero.NamesOfHero.Where(_sharedNames.Contains)));

    // The Hero as the search sees it: when the Scheme counts Heroes by team, a Hero with two teams is two sides, each
    // of one team, unless both sides are of one kind; otherwise the Hero itself.
    private IEnumerable<Hero> SidesOf(Hero hero) =>
        _bySide && hero.AlsoTeam is not null
            ? hero.Teams.Select(team => hero with { Team = team, AlsoTeam = null }).DistinctBy(KindOf)
            : [hero];
}
