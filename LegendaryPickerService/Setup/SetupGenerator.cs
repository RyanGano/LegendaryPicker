using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Draws a random legal setup the way players would at the table: the Scheme from every included Scheme its card
// allows at the player count, then a Mastermind from those the Scheme doesn't rule out, then the groups they require,
// then the remaining Villain and Henchman Groups, then any Henchman Groups the Scheme draws outside the
// Villain Deck, then the Heroes, then any Heroes the Scheme draws outside the Hero Deck, then any other
// Masterminds the Scheme draws. Each draw goes
// through the IRandomSource and picks from the options left, in catalog order, so a fixed sequence of draws
// always gives the same setup.
//
// Nothing is checked per draw (D-scheme-first, #138): every requirement a card makes is met inside its own box, which
// the loader enforces, so any Scheme of the included boxes can be set up with any Mastermind it doesn't exclude. A
// requirement the box data allows from another box is still in the setup when that box isn't included, since the
// player owns the card, unless the card gives a substitute, which is drawn from the included cards instead. A part a
// required card uses that no included box supplies is laid out from the base game that has it.
//
// When the included boxes follow more than one ruleset, the Scheme and Mastermind decide which rules apply
// (D-mixed, #85, #88): a pair with a card of the mixing base game's ruleset (a Villains Plot or Commander)
// follows that base game's rules, and any other pair follows its own ruleset's base game. Every other card is
// drawn from every included box whatever the pair, so no Hero or group is locked out by it, and since the pair
// fixed the rules first, no later draw can change the rules the counts came from. A Mastermind whose pair with the
// drawn Scheme has no included base game for its rules can't be set up with it, so it isn't drawn.
//
// A Scheme can set cards of a group beside it whether or not the group is drawn (#84). Those cards fill no group
// slot (VIL p.17); a drawn group puts only what is left of it in the Villain Deck, and one with nothing left can't
// be drawn there.
//
// The setup lays out a stack only when its rules or a drawn card use it (D-uses, #87). A Hero, group or Mastermind
// that uses a stack no included box of its own ruleset supplies isn't drawn, unless the setup requires it.
//
// An expansion can be played without a base game of its own ruleset when its box says how (OtherRuleset, D-heroic,
// #110): its Schemes and Masterminds then follow the included base game's rules, and each part it uses that no
// included box supplies is replaced by its stand-in, as Fear Itself's Bindings become Wounds (FI p.2). A base game can
// give stand-ins too: Villains' Bindings serve a card that uses Wounds when no included box has them (#241).
//
// Any boxes with a base game can be drawn together, whatever their rulesets, under one rule edition (D-editions, #240):
// the one the player chooses, or else Revised when a Revised base game is included, and otherwise the Scheme and
// Mastermind decide as above, with a Revised card following First Edition rules. An edition whose base game isn't
// included takes its rules from a loaded one, and the included base game closest to it supplies the parts they use.
//
// Every count and rule comes from the box data; nothing here compares a card name.
public sealed class SetupGenerator(BoxCatalog catalog)
{
    public const string CoreBoxId = "core";

    // The source key of the owner decision that every requirement is met, from another box when need be (#138).
    private const string SchemeFirst = "D-scheme-first";

    // The source key of the owner decision that any boxes can be drawn together under one rule edition (#240).
    private const string Editions = "D-editions";

    public GenerationResult Generate(int players, IRandomSource random) => Generate(players, [CoreBoxId], random);

    // Every included box contributes its cards and stacks, drawn in catalog order whatever order the boxes
    // are named in. The rules of each ruleset come from its first included base game in catalog order. With epic, a drawn
    // Mastermind that has an Epic side is played on that side, with its setup effects (#185). With rules, the whole
    // setup follows that rule edition, from the first loaded base game of it when none is included (#240). With a
    // difficulty from 1 to 5 (#267), a complexity from 1 to 5 (#270), or both, the plain draw is made and its open picks
    // are then repaired toward those ratings (#329): the Scheme and Mastermind stay as drawn, and no rule is relaxed.
    // Without a target the draw is the plain one. chosenCards are the ids of the cards the player chose to be in the game
    // (#337): each is in the setup as a card a rule requires would be, and a box that holds one is included.
    public GenerationResult Generate(
        int players, IReadOnlyCollection<string> includedBoxes, IRandomSource random, bool epic = false, Ruleset? rules = null,
        int? difficulty = null, int? complexity = null, IReadOnlyCollection<string>? chosenCards = null)
    {
        if (CheckChosenCards(chosenCards ?? []) is { } wrongCard)
        {
            throw new ArgumentException(wrongCard, nameof(chosenCards));
        }

        var chosen = ChosenCards.Of(catalog, chosenCards ?? []);
        includedBoxes = BoxesWith(includedBoxes, chosenCards ?? []);
        if (CheckBoxes(includedBoxes, rules) is { } problem)
        {
            throw new ArgumentException(problem, nameof(includedBoxes));
        }

        var targets = difficulty is not null || complexity is not null ? new RatingTargets(catalog, difficulty, complexity) : null;
        var (boxes, draws) = Draws(players, includedBoxes, random, rules, targets, chosen);
        try
        {
            return Run(boxes, draws, players, random, epic, chosen);
        }
        catch (ChosenCardsConflictException conflict)
        {
            return new ChosenCardsConflict(conflict.Message);
        }
    }

    // The most Heroes, Villain Groups and Henchman Groups a player can choose for a setup of these boxes and player count
    // (#337): with a chosen Scheme, the most the Hero Deck and group slots hold with it and a Mastermind it can be drawn
    // with (or the chosen one); otherwise the player count's usual numbers, with a chosen Mastermind's extras. Either way
    // the groups the chosen Scheme requires and the chosen Mastermind always leads keep their slots. It is an upper bound:
    // a drawn Mastermind's Always Leads group can take a slot, and the draw then narrows to the pairs that leave room.
    // With epic, each Mastermind counts on the side a draw with epic plays, as Epic Annihilus's extra Villain Group.
    public ChoiceLimits Limits(
        int players, IReadOnlyCollection<string> includedBoxes, Ruleset? rules = null, IReadOnlyCollection<string>? chosenCards = null, bool epic = false)
    {
        if (CheckChosenCards(chosenCards ?? []) is { } wrongCard)
        {
            throw new ArgumentException(wrongCard, nameof(chosenCards));
        }

        var chosen = ChosenCards.Of(catalog, chosenCards ?? []);
        includedBoxes = BoxesWith(includedBoxes, chosenCards ?? []);
        if (CheckBoxes(includedBoxes, rules) is { } problem)
        {
            throw new ArgumentException(problem, nameof(includedBoxes));
        }

        var (boxes, draws) = Draws(players, includedBoxes, new FirstOption(), rules, null, chosen);
        var schemes = boxes.SelectMany(box => box.AllSchemes).DistinctBy(scheme => scheme.Id)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList();
        var chosenGroups = chosen.VillainGroups.Select(group => group.Id).Concat(chosen.HenchmanGroups.Select(group => group.Id)).ToHashSet();
        ChoiceLimits Most(Func<TableDraw, Mastermind?, ChoiceLimits> limitsOf) => draws
            .Select(draw =>
            {
                var mastermind = chosen.Mastermind is { } chosenMastermind && epic ? draw.PlayedEpic(chosenMastermind) : chosen.Mastermind;
                return draw.LessRequiredGroups(limitsOf(draw, mastermind), chosen.Scheme, mastermind, chosenGroups);
            })
            .Aggregate(new ChoiceLimits(0, 0, 0), (most, limits) => new ChoiceLimits(
                Math.Max(most.Heroes, limits.Heroes), Math.Max(most.VillainGroups, limits.VillainGroups), Math.Max(most.HenchmanGroups, limits.HenchmanGroups)));

        // Without a chosen Scheme the limits are the player count's usual ones: a Scheme that holds more is a rare draw, so
        // a player chooses it to choose more. A chosen Scheme and Mastermind no setup can pair leave nothing to count; the
        // draw says why, so the limits are then the usual ones too.
        if (chosen.Scheme is { } scheme
            && Most((draw, mastermind) => draw.Limits(
                schemes.Where(other => other.Id == scheme.Id).ToList(),
                mastermind is null ? draw.Masterminds.Select(other => epic ? draw.PlayedEpic(other) : other).ToList() : [mastermind]))
                is { Heroes: > 0 } limits)
        {
            return limits;
        }

        return Most((draw, mastermind) => draw.Usual(mastermind));
    }

    // The included boxes, in catalog order, and the sets of rules a draw from them can follow.
    private (List<Box> Boxes, List<TableDraw> Draws) Draws(
        int players, IReadOnlyCollection<string> includedBoxes, IRandomSource random, Ruleset? rules, RatingTargets? targets, ChosenCards chosen)
    {
        var boxes = catalog.Boxes.Where(box => includedBoxes.Contains(box.Id)).ToList();
        var mixingBox = boxes.Select(box => box.Ruleset).Distinct().Count() > 1
            ? boxes.FirstOrDefault(box => box.Setup?.Mixing is not null)
            : null;

        // The stand-ins of the parts no included box supplies, from the first included box that gives one.
        var standIns = boxes
            .SelectMany(box => (box.OtherRuleset?.StandIns ?? []).Concat(box.Setup?.StandIns ?? []).Select(standIn => new ActiveStandIn(standIn, box)))
            .Where(active => !boxes.Any(box => box.Components.Supplies(active.Rule.Part)?.Value > 0))
            .DistinctBy(active => active.Rule.Part)
            .ToDictionary(active => active.Rule.Part);
        var baseRulesets = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).ToHashSet();

        TableDraw Table(Box rulesBox, RuleNote? edition)
        {
            var rules = rulesBox.Setup!;
            var row = rules.PlayerCounts.SingleOrDefault(r => r.Players == players);
            if (players != 1 && row is null)
            {
                throw new ArgumentOutOfRangeException(nameof(players), players,
                    $"Supported player counts are 1 (Solo) and {string.Join(", ", rules.PlayerCounts.Select(r => r.Players))}.");
            }

            return new TableDraw(catalog.Boxes, boxes, rulesBox, mixingBox, baseRulesets, standIns, rules, players, row, random, edition, targets, chosen);
        }

        // One rule edition for the whole setup, when the player chose one or the boxes call for one; otherwise one set of
        // rules per ruleset that has an included base game, each drawing from every included box.
        var draws = (rules ?? DefaultEdition(boxes, mixingBox)) is { } edition
            ? [Table(EditionBox(boxes, edition), EditionReason(boxes, edition, chosen: rules is not null))]
            : boxes.Where(box => box.IsBaseGame)
                .GroupBy(box => box.Ruleset)
                .Select(group => Table(group.Key == mixingBox?.Ruleset ? mixingBox : group.First(), null))
                .ToList();
        return (boxes, draws);
    }

    // The kinds of card a player can choose to be in the game (#337), by the kind part of their ids.
    private static readonly HashSet<string> ChoosableKinds = ["scheme", "mastermind", "villain", "henchman", "hero"];

    // Why a list of chosen card ids can't be used, or null when it can: each must be a loaded Scheme, Mastermind, Villain
    // Group, Henchman Group or Hero, and a setup has one Scheme and one Mastermind, so at most one of each is chosen.
    public string? CheckChosenCards(IReadOnlyCollection<string> chosenCards)
    {
        var known = SetupCodes.CardIds(catalog).ToHashSet(StringComparer.Ordinal);
        if (chosenCards.FirstOrDefault(id => !known.Contains(id) || !ChoosableKinds.Contains(id.Split('_')[1])) is { } unknown)
        {
            return $"No card has id {unknown}.";
        }

        var distinct = chosenCards.Distinct(StringComparer.Ordinal).ToList();
        return distinct.Count(id => id.Split('_')[1] == "scheme") > 1 ? "Choose at most one Scheme."
            : distinct.Count(id => id.Split('_')[1] == "mastermind") > 1 ? "Choose at most one Mastermind."
            : null;
    }

    // The included boxes and, for each chosen card none of them holds, the box that declares it (#337): a chosen card's box
    // is included rather than the card refused, so the setup draws it as one of its own.
    public IReadOnlyCollection<string> BoxesWith(IReadOnlyCollection<string> includedBoxes, IReadOnlyCollection<string> chosenCards) =>
        includedBoxes
            .Concat(chosenCards
                .Where(id => !catalog.Boxes.Any(box => includedBoxes.Contains(box.Id) && box.Holds(id)))
                .Select(id => id[..id.IndexOf('_')]))
            .ToHashSet(StringComparer.Ordinal);

    // The cards the player chose for the setup (#337), by kind, each in the order chosen.
    private sealed record ChosenCards(
        Scheme? Scheme, Mastermind? Mastermind, IReadOnlyList<VillainGroup> VillainGroups, IReadOnlyList<HenchmanGroup> HenchmanGroups,
        IReadOnlyList<Hero> Heroes)
    {
        public static ChosenCards Of(BoxCatalog catalog, IReadOnlyCollection<string> ids)
        {
            var order = ids.Distinct(StringComparer.Ordinal).ToList();
            List<T> Chosen<T>(Func<Box, IEnumerable<T>> cards) where T : ICard =>
                catalog.Boxes.SelectMany(cards).Where(card => order.Contains(card.Id)).OrderBy(card => order.IndexOf(card.Id)).ToList();
            return new ChosenCards(
                Chosen(box => box.Schemes).SingleOrDefault(),
                Chosen(box => box.Masterminds).SingleOrDefault(),
                Chosen(box => box.VillainGroups),
                Chosen(box => box.HenchmanGroups),
                Chosen(box => box.Heroes));
        }
    }

    // Ends a draw whose chosen cards can't all be in one legal setup; Generate returns its message as the result.
    private sealed class ChosenCardsConflictException(string message) : Exception(message);

    // What a card's own ruleset calls its kind, for a message about it: "the Plot" for a Villainous Scheme.
    private string Word(ICard card, Func<RulesetTerms, string> kind) =>
        kind(RulesetTerms.For(catalog.Boxes.Single(box => card.Id.StartsWith(box.Id + "_", StringComparison.Ordinal)).Ruleset));

    // The rule edition a setup follows when the player chooses none (D-editions, #240), or null when its boxes follow one
    // ruleset or its Scheme and Mastermind decide. A Revised base game brings the newest rules. Without one, First Edition
    // and Villainous boxes keep D-mixed, or D-heroic for an expansion played under the other ruleset's base game, and a
    // Revised card among them follows First Edition rules; any other mix follows its base games' one ruleset.
    private static Ruleset? DefaultEdition(IReadOnlyList<Box> boxes, Box? mixingBox)
    {
        var bases = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).ToHashSet();
        if (boxes.Select(box => box.Ruleset).Distinct().Count() == 1)
        {
            return null;
        }

        if (bases.Contains(Ruleset.Revised))
        {
            return Ruleset.Revised;
        }

        return mixingBox is not null || bases.Count > 1 || boxes.All(box => bases.Contains(box.Ruleset) || box.OtherRuleset is not null)
            ? null
            : bases.Single();
    }

    // The base game whose rules an edition follows: the first included one of it, or else the first loaded one, whose
    // rules then apply with none of its cards (#240).
    private Box EditionBox(IReadOnlyList<Box> boxes, Ruleset edition) =>
        boxes.FirstOrDefault(box => box.IsBaseGame && box.Ruleset == edition)
        ?? catalog.Boxes.First(box => box.IsBaseGame && box.Ruleset == edition);

    // Why a setup follows a rule edition, or null when the player chose the one ruleset its boxes follow anyway.
    private RuleNote? EditionReason(IReadOnlyList<Box> boxes, Ruleset edition, bool chosen)
    {
        var name = RulesetTerms.RulesName(edition);
        var link = LinkOf(Editions, EditionBox(boxes, edition));
        if (chosen)
        {
            return boxes.All(box => box.Ruleset == edition) ? null : new RuleNote($"{name} rules: chosen by the player", Editions, link);
        }

        return new RuleNote(
            edition == Ruleset.Revised ? $"{name} rules: a Revised base game is included" : $"{name} rules: the included base games follow them",
            Editions,
            link);
    }

    private static string? LinkOf(string citation, Box from) =>
        from.Sources.FirstOrDefault(source => source.Key == citation.Split(' ')[0])?.Url;

    // Why a set of box ids can't be drawn from, or null when it can. A setup needs at least one base game for its
    // rules; any boxes with one can be drawn together, whatever their rulesets (D-editions, #240). A chosen rule edition
    // needs a loaded base game of it for its rules.
    public string? CheckBoxes(IReadOnlyCollection<string> includedBoxes, Ruleset? rules = null)
    {
        if (includedBoxes.FirstOrDefault(id => catalog.Boxes.All(box => box.Id != id)) is { } unknown)
        {
            return $"No box has id {unknown}.";
        }

        if (!catalog.Boxes.Any(box => includedBoxes.Contains(box.Id) && box.IsBaseGame))
        {
            return "Include at least one base game.";
        }

        return rules is { } edition && !catalog.Boxes.Any(box => box.IsBaseGame && box.Ruleset == edition)
            ? $"No base game has {RulesetTerms.RulesName(edition)} rules."
            : null;
    }

    // Draws the Scheme from every included Scheme its card allows at the player count, then the Mastermind from the
    // included ones the Scheme doesn't exclude and some set of rules covers, in catalog order; the rest of the draw
    // follows that pair's rules. A chosen Scheme is the Scheme, and a chosen Mastermind the Mastermind (#337): with a chosen
    // Mastermind and no chosen Scheme, the Scheme is drawn from those that allow it.
    private GenerationResult Run(
        IReadOnlyList<Box> boxes, IReadOnlyList<TableDraw> draws, int players, IRandomSource random, bool epic, ChosenCards chosen)
    {
        var schemes = boxes.SelectMany(box => box.AllSchemes).DistinctBy(scheme => scheme.Id)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList();
        var masterminds = draws[0].Masterminds;
        if (schemes.Count == 0 || masterminds.Count == 0)
        {
            return new NoEligibleScheme(players);
        }

        var count = players == 1 ? "1 player" : $"{players} players";
        if (chosen.Scheme is { } chosenScheme)
        {
            schemes = schemes.Where(scheme => scheme.Id == chosenScheme.Id).ToList();
            if (schemes.Count == 0)
            {
                throw new ChosenCardsConflictException(
                    $"The {Word(chosenScheme, terms => terms.Scheme)} {chosenScheme.Name} can't be played with {count}: choose another {Word(chosenScheme, terms => terms.Scheme)} or player count.");
            }
        }

        if (chosen.Mastermind is { } chosenMastermind)
        {
            var mastermindWord = Word(chosenMastermind, terms => terms.Mastermind);
            masterminds = masterminds.Where(mastermind => mastermind.Id == chosenMastermind.Id).ToList();
            if (masterminds.Count == 0)
            {
                throw new ChosenCardsConflictException(
                    $"The {mastermindWord} {chosenMastermind.Name} uses cards none of the included boxes has: include a box that has them, or choose another {mastermindWord}.");
            }

            var allowing = schemes.Where(scheme => !chosenMastermind.RuledOutBy(scheme) && draws.Any(draw => draw.Follows(scheme, chosenMastermind))).ToList();
            if (allowing.Count == 0)
            {
                throw new ChosenCardsConflictException(chosen.Scheme is { } picked
                    ? $"The {Word(picked, terms => terms.Scheme)} {picked.Name} can't be played with the {mastermindWord} {chosenMastermind.Name}: choose another {mastermindWord}."
                    : $"No {Word(chosenMastermind, terms => terms.Scheme)} of the included boxes can be played with the {mastermindWord} {chosenMastermind.Name} at {count}: choose another {mastermindWord}.");
            }

            schemes = allowing;
        }

        // With chosen groups or Heroes, the Scheme and then the Mastermind are drawn only from the pairs that leave room for
        // every chosen card, as a chosen Mastermind narrows the Schemes: each pair is laid out on a Probe up to its open
        // picks. The pools narrow before each draw, so no drawn card is dropped or redrawn and no rule is relaxed; only when
        // no pair can hold the chosen cards is the answer a conflict.
        var misfits = new Dictionary<(string, string), string?>();
        var narrows = chosen.VillainGroups.Count + chosen.HenchmanGroups.Count + chosen.Heroes.Count > 0;
        if (narrows)
        {
            var probes = draws.ToDictionary(draw => draw, draw => draw.Probe());
            var fitting = schemes
                .Where(scheme => masterminds
                    .Where(mastermind => !mastermind.RuledOutBy(scheme))
                    .Select(mastermind => (Mastermind: mastermind, Draw: draws.FirstOrDefault(draw => draw.Follows(scheme, mastermind))))
                    .Where(pair => pair.Draw is not null)
                    .Any(pair => (misfits[(scheme.Id, pair.Mastermind.Id)] =
                        probes[pair.Draw!].Misfit(scheme, epic ? pair.Draw!.PlayedEpic(pair.Mastermind) : pair.Mastermind)) is null))
                .ToList();
            if (fitting.Count == 0)
            {
                var reasons = misfits.Values.Distinct().ToList();
                var instead = (chosen.Scheme, chosen.Mastermind) switch
                {
                    (_, { } chosenOne) when chosen.Scheme is null =>
                        $"No {Word(chosenOne, terms => terms.Scheme)} of the included boxes leaves room for the chosen cards with the {Word(chosenOne, terms => terms.Mastermind)} {chosenOne.Name} at {count}: remove a chosen card, or choose another {Word(chosenOne, terms => terms.Mastermind)}.",
                    ({ } chosenOne, null) =>
                        $"No {Word(chosenOne, terms => terms.Mastermind)} of the included boxes leaves room for the chosen cards with the {Word(chosenOne, terms => terms.Scheme)} {chosenOne.Name} at {count}: remove a chosen card, or choose another {Word(chosenOne, terms => terms.Scheme)}.",
                    _ => $"No Scheme and Mastermind of the included boxes leave room for all the chosen cards at {count}: remove a chosen card, or include more boxes.",
                };
                // One reason for every pair is the answer: for a chosen pair, its own; otherwise only when it names no
                // Mastermind, since a kept answer can name another one than the pair's (TableDraw.Misfit).
                var only = reasons.Count == 1 ? reasons[0] : null;
                throw new ChosenCardsConflictException(
                    only is not null && (chosen.Scheme is not null && chosen.Mastermind is not null || !masterminds.Any(mastermind => only.Contains(mastermind.Name)))
                        ? only
                        : instead);
            }

            schemes = fitting;
        }

        var scheme = random.Pick(schemes);
        var pairs = masterminds
            .Where(mastermind => !mastermind.RuledOutBy(scheme))
            .Select(mastermind => (Mastermind: mastermind, Draw: draws.FirstOrDefault(draw => draw.Follows(scheme, mastermind))))
            .Where(pair => pair.Draw is not null)
            .ToList();
        if (narrows)
        {
            // The Mastermind is drawn from those that leave room with the drawn Scheme; the search above stopped at the first.
            var probes = pairs.Select(pair => pair.Draw!).Distinct().ToDictionary(draw => draw, draw => draw.Probe());
            pairs = pairs
                .Where(pair => (misfits.TryGetValue((scheme.Id, pair.Mastermind.Id), out var known)
                    ? known
                    : probes[pair.Draw!].Misfit(scheme, epic ? pair.Draw!.PlayedEpic(pair.Mastermind) : pair.Mastermind)) is null)
                .ToList();
        }
        var drawn = random.Pick(pairs.Select(pair => pair.Mastermind).ToList());
        var draw = pairs.Single(pair => pair.Mastermind == drawn).Draw;
        return draw!.Finish(scheme, epic ? draw.PlayedEpic(drawn) : drawn);
    }

    // One set of rules a table draw can follow: every loaded box, which holds the cards a Scheme requires from a box
    // the setup doesn't include, the included boxes, which it draws every other card from, the base game whose rules
    // they are, the mixing base game when the included boxes follow more than one ruleset and
    // one of them has mixing rules, the rulesets of the included base games, the stand-ins of the parts no
    // included box supplies, and the random source. row is the player-count table row, or null in Solo. edition is
    // present when the setup follows one rule edition whatever its Scheme and Mastermind (D-editions, #240), and is why.
    // targets are the ratings the request asks for, or null when it asks for none (#329). chosenCards are the cards the
    // player chose to be in the game (#337), which fill slots as required cards do and which a repair never swaps.
    private sealed class TableDraw(
        IReadOnlyList<Box> catalogBoxes, IReadOnlyList<Box> boxes, Box rulesBox, Box? mixingBox, IReadOnlySet<Ruleset> baseRulesets,
        IReadOnlyDictionary<Part, ActiveStandIn> standIns, SetupRules rules, int players, PlayerCountSetup? row,
        IRandomSource random, RuleNote? edition, RatingTargets? targets, ChosenCards chosenCards)
    {
        // The most rounds of swaps the repair toward a target makes (#329).
        private const int RepairRounds = 6;

        // The included base game whose parts stand for those of the rules' base game when that isn't included: the first
        // that uses the same words, or else the first (D-editions, #240).
        private readonly Box _componentsBox = boxes.Contains(rulesBox)
            ? rulesBox
            : boxes.Where(box => box.IsBaseGame).OrderByDescending(box => RulesetTerms.For(box.Ruleset) == RulesetTerms.For(rulesBox.Ruleset)).First();

        // A reprint is one card with the original (#34 D5), so each card is in its pool once however many included boxes hold it.
        private readonly List<VillainGroup> _villainGroups = boxes.SelectMany(box => box.AllVillainGroups).DistinctBy(group => group.Id).Where(group => Supplied(boxes, standIns, group)).ToList();
        private readonly List<HenchmanGroup> _henchmanGroups = boxes.SelectMany(box => box.AllHenchmanGroups).DistinctBy(group => group.Id).Where(group => Supplied(boxes, standIns, group)).ToList();
        private readonly List<Hero> _heroes = boxes.SelectMany(box => box.AllHeroes).DistinctBy(hero => hero.Id).Where(hero => Supplied(boxes, standIns, hero)).ToList();
        private readonly List<Mastermind> _masterminds = boxes.SelectMany(box => box.AllMasterminds).DistinctBy(mastermind => mastermind.Id).Where(mastermind => Supplied(boxes, standIns, mastermind)).ToList();

        // The words the ruleset's rulebook uses, for rule notes. A setup whose drawn cards follow both rulesets
        // switches to the mixed words once they are drawn.
        private RulesetTerms _terms = RulesetTerms.For(rulesBox.Ruleset);

        private bool Solo => row is null;

        private bool IgnoresAlwaysLeads => Solo && rules.Solo.IgnoresAlwaysLeads.Value;

        // Whether Solo's rule to ignore Always Leads applies to the Mastermind: not when its card says it always leads
        // its group in Solo too, as Ultron Infinity's does (#203).
        private bool Ignores(Mastermind mastermind) => IgnoresAlwaysLeads && mastermind.AlwaysLeads?.EvenInSolo != true;

        // The included Masterminds a setup can draw: those whose parts the included boxes supply.
        public IReadOnlyList<Mastermind> Masterminds => _masterminds;

        // The side the setup plays a drawn Mastermind on when the player asks for Epic Masterminds: its Epic side, unless
        // that side uses a part no included box supplies, when it stays on its normal side as a Mastermind would be
        // dropped for it (#185).
        public Mastermind PlayedEpic(Mastermind mastermind) =>
            mastermind.PlayedEpic() is var epic && Supplied(boxes, standIns, epic) ? epic : mastermind;

        // Whether a Scheme and Mastermind pair follows these rules: every pair does under one rule edition.
        public bool Follows(Scheme scheme, Mastermind mastermind) => edition is not null || RulesOf(scheme, mastermind) == rulesBox.Ruleset;

        // A copy of these rules that draws the first option every time, so Misfit can try a pair without using up the
        // setup's random draws (#337).
        public TableDraw Probe() =>
            new(catalogBoxes, boxes, rulesBox, mixingBox, baseRulesets, standIns, rules, players, row, new FirstOption(), edition, null, chosenCards)
            {
                IsProbe = true,
            };

        private bool IsProbe { get; init; }

        // The most Heroes, Villain Groups and Henchman Groups a setup of these rules holds with one of the Schemes and a
        // Mastermind of these it can be drawn with (#337). A Mastermind's extra groups and Heroes add to any Scheme's, and a
        // Mastermind the Scheme sets aside with its Always Leads group may add a group of either type.
        public ChoiceLimits Limits(IReadOnlyList<Scheme> schemes, IReadOnlyList<Mastermind> masterminds)
        {
            var most = new ChoiceLimits(0, 0, 0);
            foreach (var scheme in schemes)
            {
                var pairs = masterminds.Where(mastermind => !mastermind.RuledOutBy(scheme) && Follows(scheme, mastermind))
                    .Select(mastermind => AsSeen(scheme, mastermind).Setup).ToList();
                if (pairs.Count == 0)
                {
                    continue;
                }

                var plan = Plan(scheme);
                var drained = plan.OutsideMasterminds.Where(draw => draw.Rule.BringsAlwaysLeads?.Value == true).Sum(draw => draw.Count);
                int Extra(Func<SetupEffects, IReadOnlyList<PlayerCountValue>?> effect) =>
                    pairs.Max(effects => effects is null ? 0 : ForPlayers(effect(effects) ?? [])?.Value ?? 0);
                most = new ChoiceLimits(
                    Math.Max(most.Heroes, plan.Heroes + Extra(effects => effects.ExtraHeroes)),
                    Math.Max(most.VillainGroups, plan.VillainGroups + drained + Extra(effects => effects.ExtraVillainGroups)),
                    Math.Max(most.HenchmanGroups, plan.HenchmanGroups + drained + Extra(effects => effects.ExtraHenchmanGroups)));
            }

            return most;
        }

        // The Heroes, Villain Groups and Henchman Groups these rules hold at the player count before a Scheme changes them,
        // with what a chosen Mastermind's setup adds (#337).
        public ChoiceLimits Usual(Mastermind? mastermind)
        {
            int Extra(IReadOnlyList<PlayerCountValue>? effect) => ForPlayers(effect ?? [])?.Value ?? 0;
            return new ChoiceLimits(
                (Solo ? rules.Solo.Heroes.Value : rules.Heroes.Value + Extra(rules.ExtraHeroes)) + Extra(mastermind?.Setup?.ExtraHeroes),
                (Solo ? rules.Solo.VillainGroups.Value : row!.VillainGroups) + Extra(mastermind?.Setup?.ExtraVillainGroups),
                (Solo ? rules.Solo.HenchmanGroups.Value : row!.HenchmanGroups) + Extra(mastermind?.Setup?.ExtraHenchmanGroups));
        }

        // The limits less the slots the groups a chosen Scheme requires and a chosen Mastermind always leads take, so the
        // player leaves room for them (#337). A group the player chose takes no more, nor does a required one the Mastermind
        // also leads, nor an Always Leads group these rules ignore.
        public ChoiceLimits LessRequiredGroups(ChoiceLimits limits, Scheme? scheme, Mastermind? mastermind, IReadOnlySet<string> chosenGroups)
        {
            if (limits.Heroes == 0)
            {
                return limits;
            }

            // Each group the setup must hold, as the ids any one of which meets it, by type.
            var claims = new List<(GroupType Type, IReadOnlyList<string> Ids)>();
            foreach (var group in scheme?.Setup.RequiredGroups ?? [])
            {
                claims.Add((group.GroupType, [group.GroupId]));
            }

            if (scheme?.Setup.OneOfGroups is { } oneOf)
            {
                claims.Add((oneOf.GroupType, oneOf.GroupIds));
            }

            if (mastermind?.AlwaysLeads is { } leads && !Ignores(mastermind)
                && !claims.Any(claim => claim.Type == leads.GroupType && claim.Ids.Any(leads.GroupIds.Contains)))
            {
                claims.Add((leads.GroupType, leads.GroupIds));
            }

            int Taken(GroupType type) => claims.Count(claim => claim.Type == type && !claim.Ids.Any(chosenGroups.Contains));
            return limits with
            {
                VillainGroups = Math.Max(limits.VillainGroups - Taken(GroupType.Villain), 0),
                HenchmanGroups = Math.Max(limits.HenchmanGroups - Taken(GroupType.Henchman), 0),
            };
        }

        // A Probe's plans by Scheme, and why its chosen Heroes don't fit a Hero Deck, by what the deck depends on.
        private readonly Dictionary<string, SetupPlan> _plans = [];
        private readonly Dictionary<(string Scheme, int Heroes, string Required), string?> _deckMisfits = [];
        private readonly Dictionary<(string Scheme, string Mastermind), string?> _groupMisfits = [];

        // What a Mastermind brings to a Scheme's group slots, so Masterminds that bring the same fill them alike: the type
        // of the group it always leads and the extra groups and Henchmen its setup adds, or its own id when the group it
        // leads is one the player chose or the Scheme requires, or it leads a second group, or the Scheme sets Masterminds
        // aside.
        private string SlotsKey(Scheme scheme, Mastermind mastermind)
        {
            var seen = AsSeen(scheme, mastermind);
            var named = chosenCards.VillainGroups.Select(group => group.Id).Concat(chosenCards.HenchmanGroups.Select(group => group.Id))
                .Concat(scheme.Setup.RequiredGroups?.Select(group => group.GroupId) ?? []).Concat(scheme.Setup.OneOfGroups?.GroupIds ?? []);
            if (seen.AlsoLeads is not null || scheme.Setup.OutsideMasterminds is not null || seen.AlwaysLeads?.GroupIds.Intersect(named).Any() == true)
            {
                return seen.Id;
            }

            var effects = seen.Setup;
            return string.Join('|',
                seen.AlwaysLeads is { } leads ? $"{leads.GroupType}{(leads.EvenInSolo ? "+" : "")}{(leads.OrGroupIds is null ? "" : "*")}" : "",
                ForPlayers(effects?.ExtraVillainGroups ?? [])?.Value,
                ForPlayers(effects?.ExtraHenchmanGroups ?? [])?.Value,
                ForPlayers(effects?.HenchmanCards ?? [])?.Value);
        }

        // Why the chosen cards can't all be in a setup of this Scheme and Mastermind, or null when they can (#337): the
        // groups and Hero Deck are laid out as a draw lays them out, up to the open picks. Called on a Probe. Each answer is
        // kept for the next Mastermind that would give the same one: for the groups, one that brings the same to the slots
        // (SlotsKey); for the Hero Deck, one with the same Hero count and required Heroes. A kept answer about the groups
        // can name the Mastermind it was found with.
        public string? Misfit(Scheme scheme, Mastermind mastermind)
        {
            string? Conflict(Action check)
            {
                _conflict = null;
                check();
                var message = _conflict;
                _conflict = null;
                return message;
            }

            if (chosenCards.VillainGroups.Count + chosenCards.HenchmanGroups.Count > 0)
            {
                var slots = (scheme.Id, SlotsKey(scheme, mastermind));
                if (!_groupMisfits.TryGetValue(slots, out var groups))
                {
                    _groupMisfits[slots] = groups = Conflict(() => Place(scheme, mastermind));
                }

                if (groups is not null)
                {
                    return groups;
                }
            }

            if (chosenCards.Heroes.Count == 0)
            {
                return null;
            }

            var plan = WithMastermind(PlanOf(scheme), AsSeen(scheme, mastermind));
            var key = (scheme.Id, plan.Heroes, string.Join(',', plan.Mastermind?.Setup?.RequiredHeroes?.Select(rule => rule.HeroId) ?? []));
            if (!_deckMisfits.TryGetValue(key, out var heroes))
            {
                _deckMisfits[key] = heroes = Conflict(() => StartDeck(plan));
            }

            return heroes;
        }

        // Draws the rest of the setup for a Scheme and Mastermind pair that follows these rules.
        public SetupResult Finish(Scheme scheme, Mastermind drawnMastermind)
        {
            var (plan, mastermind, joinsLater, slotNotes, drainedLeads, villainSlots, henchmanSlots) = Place(scheme, drawnMastermind);
            var (villainGroups, henchmanGroups) = (villainSlots.Groups, henchmanSlots.Groups);
            // Each Henchman Group drawn outside the Villain Deck is one the Villain Deck doesn't use (D-readings).
            var outsideHenchmen = plan.OutsideHenchmen
                .Zip(DrawMany(_henchmanGroups.Except(henchmanGroups), plan.OutsideHenchmen.Count), (draw, group) => new OutsideHenchmanGroup(group, draw.To, draw.Cards))
                .ToList();
            var deck = DrawDeck(plan);
            var heroes = deck.Heroes;

            // With a target, the open picks are repaired toward it before the rest of the setup is built from them (#329).
            if (targets is not null)
            {
                Repair(targets, plan, drawnMastermind, villainSlots, henchmanSlots, deck, outsideHenchmen);
            }

            var villainsSetAside = VillainsSetAside(plan, mastermind, villainGroups, drainedLeads, slotNotes);
            var outside = DrawOutsideHeroes(deck);
            var (steps, stepHeroes) = FillHeroSteps(plan, heroes, outside);
            plan = plan with { Steps = steps };

            // The Masterminds the Scheme draws besides its own, from the included ones the setup doesn't otherwise use.
            var drawnDrained = drainedLeads.Select(entry => entry.Drained).ToList();
            var otherMasterminds = new Queue<Mastermind>(DrawMany(
                _masterminds.Where(other => other.Id != mastermind.Id && !drawnDrained.Contains(other)),
                plan.OutsideMasterminds.Where(draw => draw.Rule.BringsAlwaysLeads?.Value != true).Sum(draw => draw.Count)));
            var drainedQueue = new Queue<Mastermind>(drawnDrained);
            var outsideMasterminds = plan.OutsideMasterminds
                .SelectMany(draw => Enumerable.Range(0, draw.Count).Select(_ => new OutsideMastermind(
                    (draw.Rule.BringsAlwaysLeads?.Value == true ? drainedQueue : otherMasterminds).Dequeue(), draw.Rule.To, draw.Rule.Tactics?.Value, draw.Rule.Joins?.Value)))
                .ToList();

            // The cards of each group the Scheme sets beside it, and how many fewer the group puts in the Villain
            // Deck when it is drawn there too.
            var inDeck = villainGroups.Select(g => g.Id).Concat(henchmanGroups.Select(g => g.Id)).ToHashSet();
            var beside = plan.Beside
                .Select(draw =>
                {
                    var (id, type) = (draw.Rule.GroupId, draw.Rule.GroupType);
                    var fromDeck = inDeck.Contains(id) ? ShareOfDeck(plan, id, type) - DeckCards(plan, id, type) : 0;
                    return new GroupCardsBeside(CatalogGroup(id), draw.Rule.Card, draw.Count, fromDeck);
                })
                .ToList();

            // The rulesets of the drawn cards decide which boxes' stacks are laid out, and whether the setup is mixed.
            // The groups whose cards sit beside the Scheme count among them.
            ICard[] cards =
            [
                plan.Scheme, drawnMastermind, .. villainGroups, .. henchmanGroups, .. outsideHenchmen.Select(group => group.Group),
                .. heroes, .. outside.Select(hero => hero.Hero), .. beside.Select(cards => cards.Group),
                .. outsideMasterminds.Select(other => other.Mastermind),
            ];
            var (drawn, mixed, stackBoxes, used, stoodIn) = LayOut(cards, outsideMasterminds.Where(other => other.To == Pile.VillainDeck).Select(other => other.Mastermind));
            var mixingRules = rulesBox == mixingBox;

            // A part the boxes of the drawn cards' rulesets lack comes from an included box of another that has it, as a
            // Revised base game's Wounds serve a First Edition card (D-editions, #240). A part only a required card uses
            // can be one no included box supplies: the first loaded box that has it, a base game first, lays it out, as
            // the player owns it (D-scheme-first, #138).
            var borrowed = used
                .Where(part => !stackBoxes.Any(box => box.Components.Supplies(part)?.Value > 0))
                .ToDictionary(part => part, part => boxes.Concat(catalogBoxes.OrderByDescending(box => box.IsBaseGame)).First(box => box.Components.Supplies(part)?.Value > 0));

            // A setup's notes use its drawn cards' words: both rulesets' when they name its parts by different words, or
            // the cards' own when the rule edition is another ruleset's (D-editions, #247), so its plan's notes are
            // written again. First Edition and Revised use the same words.
            var notes = new List<RuleNote>();
            var cardTerms = drawn.Select(RulesetTerms.For).Distinct().ToList();
            if (cardTerms.Count > 1 || cardTerms[0] != _terms)
            {
                _terms = cardTerms.Count > 1 ? RulesetTerms.Mixed : cardTerms[0];
                notes.AddRange(WithMastermind(Plan(plan.Scheme), mastermind).Notes);
            }
            else
            {
                notes.AddRange(plan.Notes);
            }

            notes.AddRange(slotNotes);

            foreach (var (part, from) in borrowed.Where(entry => !boxes.Contains(entry.Value)))
            {
                notes.Add(Note($"No included box has {StackName(part)} cards: the setup uses those of {from.Name}", SchemeFirst, rulesBox));
            }

            // Each stand-in the drawn cards need, with the part it replaces, which works too for a player who has it.
            foreach (var active in stoodIn)
            {
                notes.Add(Note(
                    active.Rule.With is { } with
                        ? $"No included box has {StackName(active.Rule.Part)} cards: use {StackName(with)} cards for them, or {StackName(active.Rule.Part)} cards if you have them"
                        : $"No included box has {StackName(active.Rule.Part)} cards: a card that gains one gives +1 Recruit instead",
                    active.Rule.Source, active.From));
            }

            // A mixed setup draws from shared pools and shuffles all Bystanders together (VIL pp.20-21). Under the
            // mixing base game's rules it also lays out every included base game's recruit stacks and lets the
            // players choose between the starting decks of the included base games (VIL p.21); under the other
            // rules it keeps that ruleset's starting deck (#88).
            IReadOnlyList<Ruleset>? choices = null;
            if (mixed && mixingBox is not null)
            {
                var mixing = mixingBox.Setup!.Mixing!;
                notes.Add(Note(
                    "Mixed sets: Schemes and Plots, Masterminds and Commanders, Heroes and Allies and the groups each come from one pool",
                    mixing.Pools, mixingBox));
                notes.Add(Note(
                    mixingRules
                        ? "Mixed sets: lay out the recruit stacks of every included base game, and shuffle all Bystanders together"
                        : "Mixed sets: shuffle all Bystanders together",
                    mixing.Stacks, mixingBox));
                var teams = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).DistinctBy(RulesetTerms.StartingTeam).ToList();
                if (mixingRules && teams.Count > 1)
                {
                    choices = teams;
                    notes.Add(Note(
                        $"Mixed sets: the players choose {string.Join(" or ", teams.Select(RulesetTerms.StartingTeam))} starting decks",
                        mixing.StartingDeckChoice, mixingBox));
                }
            }

            // A box's optional Token cards could be used with a drawn card they go with; the Tokens stay out of the counts.
            foreach (var box in boxes.Where(box => box.OptionalTokens is not null))
            {
                var tokens = box.OptionalTokens!;
                var withTokens = cards.Where(card => tokens.Value.Contains(card.Id)).Select(Called).Distinct().ToList();
                if (withTokens.Count > 0)
                {
                    notes.Add(Note($"Optional: the Token cards could be used with {string.Join(" and ", withTokens)}", tokens.Source, box));
                }
            }

            if (Solo)
            {
                if (rules.Solo.HenchmenInCity is { } inCity)
                {
                    notes.Add(Note(
                        $"Solo: set aside {inCity.Value} more {CardName(CardKind.Henchman, inCity.Value)} of the {_terms.HenchmanGroup} to enter the city before the first turn",
                        inCity.Source, rulesBox));
                }

                notes.AddRange(rules.Solo.PlayRules.Select(rule => Note($"Solo: {rule.Label}", rule.Source, rulesBox)));
            }

            if (rules.WarmupRound is { } warmup && warmup.Value.Contains(players))
            {
                notes.Add(Note($"Warmup Round: on each player's first turn, no card is played from the {_terms.VillainDeck}", warmup.Source, rulesBox));
            }

            // With boxes of more than one ruleset included, the result names the card that decided its rules
            // ("Villains rules: the Commander is Dr. Strange"), or says the pair has no card of the mixing ruleset.
            // Without a mixing base game, every card plays under the one included base game's rules (D-heroic). A setup
            // that follows one rule edition says why instead (D-editions, #240).
            var reason = edition;
            if (edition is null && boxes.Any(box => !baseRulesets.Contains(box.Ruleset)) && mixingBox is null)
            {
                var other = boxes.First(box => !baseRulesets.Contains(box.Ruleset));
                var source = other.OtherRuleset!.Source;
                reason = new RuleNote(
                    $"{RulesetTerms.RulesName(rulesBox.Ruleset)} rules: no {RulesetTerms.Side(other.Ruleset)} base game is included",
                    source,
                    LinkOf(source, other));
            }
            else if (edition is null && mixingBox is not null)
            {
                var source = mixingBox.Setup!.Mixing!.Rules;
                var words = RulesetTerms.For(mixingBox.Ruleset);
                var deciders = new List<string>();
                if (RulesetOf(plan.Scheme.Id) == mixingBox.Ruleset)
                {
                    deciders.Add($"the {words.Scheme} is {Called(plan.Scheme)}");
                }

                if (RulesetOf(mastermind.Id) == mixingBox.Ruleset)
                {
                    deciders.Add(joinsLater is not null ? $"the {words.Mastermind} is one of its" : $"the {words.Mastermind} is {Called(mastermind)}");
                }

                reason = new RuleNote(
                    deciders.Count > 0
                        ? $"{RulesetTerms.RulesName(rulesBox.Ruleset)} rules: {string.Join(" and ", deciders)}"
                        : $"{RulesetTerms.RulesName(rulesBox.Ruleset)} rules: no {RulesetTerms.Side(mixingBox.Ruleset)} {words.Scheme} or {words.Mastermind}",
                    source,
                    LinkOf(source, mixingBox));
            }

            // What the boxes of the drawn cards' rulesets hold of a part between them, and a part's stack size,
            // or null when nothing in the setup uses the part, so the setup leaves it out.
            int Supply(Part part) => borrowed.TryGetValue(part, out var from)
                ? from.Components.Supplies(part)!.Value
                : stackBoxes.Sum(box => box.Components.Supplies(part)?.Value ?? 0);
            int? Stack(Part part, int size) => used.Contains(part) ? size : null;

            // The size a Scheme sets for a part's stack, which sets its stand-in's stack when the part is stood in for,
            // as Maximum Carnage's 6 Wounds per player set the Bindings standing in for them (#241).
            int? Planned(Part part) => part switch
            {
                Part.Wounds => plan.Wounds,
                Part.Officers => plan.Officers,
                Part.Bindings => plan.Bindings,
                _ => null,
            };
            int? Sized(Part part) => Planned(part)
                ?? stoodIn.Where(active => active.Rule.With == part).Select(active => Planned(active.Rule.Part)).FirstOrDefault(size => size is not null);

            return new SetupResult(
                players,
                rulesBox.Ruleset,
                plan.Scheme,
                drawnMastermind,
                villainGroups,
                henchmanGroups,
                heroes,
                new VillainDeck(
                    plan.Twists,
                    plan.MasterStrikes,
                    villainGroups.Sum(group => ShareOfDeck(plan, group.Id, GroupType.Villain)),
                    HenchmanCards(plan, henchmanGroups),
                    plan.Bystanders,
                    plan.MovedIn(Pile.VillainDeck),
                    plan.MovedOut(Pile.VillainDeck),
                    outside.Where(hero => hero.To == Pile.VillainDeck).Sum(hero => hero.Cards),
                    beside.Sum(cards => cards.FromVillainDeck),
                    outsideMasterminds.Sum(other => other.Tactics ?? 0),
                    plan.Scheme.Setup.OwnTactics?.Value ?? 0,
                    villainsSetAside.Sum(aside => aside.Count)),
                new HeroDeck(
                    heroes.Sum(hero => BoxOf(hero.Id).Components.HeroCards.Value),
                    plan.MovedOut(Pile.HeroDeck),
                    plan.MovedIn(Pile.HeroDeck),
                    outsideHenchmen.Where(outside => outside.To == Pile.HeroDeck).Sum(outside => outside.Cards)),
                plan.TwistsBeside,
                new SetupStacks(
                    Stack(Part.Wounds, (Sized(Part.Wounds) ?? Supply(Part.Wounds)) - plan.MovedOut(Pile.Wounds)),
                    Stack(Part.Officers, (Sized(Part.Officers) ?? Supply(Part.Officers)) - plan.MovedOut(Pile.Officers)),
                    stackBoxes.Sum(box => box.Components.Bystanders?.Value ?? 0) - plan.Bystanders - plan.MovedOut(Pile.Bystanders),
                    Stack(Part.Sidekicks, Supply(Part.Sidekicks) - plan.MovedOut(Pile.Sidekicks)),
                    Stack(Part.Bindings, (Sized(Part.Bindings) ?? Supply(Part.Bindings)) - plan.MovedOut(Pile.Bindings)),
                    Stack(Part.MadameHydra, Supply(Part.MadameHydra)),
                    Stack(Part.NewRecruits, Supply(Part.NewRecruits)),
                    Stack(Part.Shards, Supply(Part.Shards)),
                    Stack(Part.Horrors, Supply(Part.Horrors))),
                new PlayerDeck(rules.StartingDeck.Agents.Value, rules.StartingDeck.Troopers.Value, choices, StartingTeam()),
                plan.Moves,
                outside,
                outsideHenchmen,
                beside,
                plan.Steps,
                notes,
                boxes,
                mixed,
                reason,
                stoodIn.Select(active => active.Rule).ToList(),
                outsideMasterminds,
                villainsSetAside,
                joinsLater is not null ? Reveal(plan.Scheme, drawnMastermind, joinsLater) : null,
                plan.Scheme.Setup.PlayersChooseHeroes is not null ? Math.Min(players, heroes.Count) : 0,
                stepHeroes);
        }

        // The Mastermind as the setup sees it: without its Always Leads groups and setup effects when the Scheme adds it
        // mid-game (#245).
        private static Mastermind AsSeen(Scheme scheme, Mastermind mastermind) =>
            scheme.Setup.MastermindJoinsLater is not null ? mastermind with { AlwaysLeads = null, AlsoLeads = null, Setup = null } : mastermind;

        // A Scheme's plan, kept per Scheme on a Probe, which tries each Scheme with many Masterminds.
        private SetupPlan PlanOf(Scheme scheme)
        {
            if (!IsProbe)
            {
                return Plan(scheme);
            }

            if (!_plans.TryGetValue(scheme.Id, out var plan))
            {
                _plans[scheme.Id] = plan = Plan(scheme);
            }

            return plan;
        }

        // Lays out a pair's plan and fills its Villain and Henchman Group slots, the first steps of Finish.
        private Placed Place(Scheme scheme, Mastermind drawnMastermind)
        {
            // A Scheme that adds its Mastermind mid-game (Enshrouded Identity, #245) still has one drawn, but its Always Leads
            // groups and setup effects are no part of the setup: the rest of the draw sees it without them, and they are
            // returned as the lines shown when the player reveals it.
            var joinsLater = scheme.Setup.MastermindJoinsLater?.Value;
            var mastermind = AsSeen(scheme, drawnMastermind);
            var plan = WithMastermind(PlanOf(scheme), mastermind);
            var slotNotes = new List<RuleNote>();

            // A Mastermind the Scheme sets aside and whose Always Leads group it adds (Symbiotic Absorption) is drawn
            // before the groups, since its group is one more of them.
            var drainedDraws = plan.OutsideMasterminds.Where(draw => draw.Rule.BringsAlwaysLeads?.Value == true).ToList();
            // Its group differs from the main Mastermind's, and the Scheme's card doesn't rule it out. One with no Always
            // Leads group (Ego) is drawable too: it adds a Villain Group drawn by the normal rules instead (#211).
            var drained = new Queue<Mastermind>(DrawMany(
                _masterminds.Where(other => other.Id != mastermind.Id && (other.AlwaysLeads is null || !other.AlwaysLeads.GroupIds.Intersect(mastermind.AlwaysLeads?.GroupIds ?? []).Any()) && !other.RuledOutBy(scheme)),
                drainedDraws.Sum(draw => draw.Count)));
            var drainedLeads = drainedDraws
                .SelectMany(draw => Enumerable.Repeat(draw.Rule.BringsAlwaysLeads!.Source, draw.Count))
                .Select(source => (other: drained.Dequeue(), source))
                .ToList();
            plan = plan with
            {
                VillainGroups = plan.VillainGroups + drainedLeads.Count(entry => entry.other.AlwaysLeads is null || entry.other.AlwaysLeads.GroupType == GroupType.Villain),
                HenchmanGroups = plan.HenchmanGroups + drainedLeads.Count(entry => entry.other.AlwaysLeads?.GroupType == GroupType.Henchman),
            };

            // A group the Scheme sets every card of beside it has none left for the Villain Deck, so it isn't drawn there.
            // A required group whose card gives a substitute is replaced by the group drawn for it.
            var required = new Dictionary<string, RequiredGroup>(plan.Required);
            var villainSlots = FillSlots(
                DeckPool(_villainGroups, plan, GroupType.Villain), GroupType.Villain,
                plan.VillainGroups, plan, mastermind, required, slotNotes, drainedLeads, chosenCards.VillainGroups);
            var henchmanSlots = FillSlots(
                DeckPool(_henchmanGroups, plan, GroupType.Henchman), GroupType.Henchman,
                plan.HenchmanGroups, plan, mastermind, required, slotNotes, drainedLeads, chosenCards.HenchmanGroups);
            return new Placed(plan with { Required = required }, mastermind, joinsLater, slotNotes, drainedLeads, villainSlots, henchmanSlots);
        }

        // The groups of a pool that can put cards in the Villain Deck with this plan. A Probe keeps them by what they depend
        // on besides the Scheme's own cards: its count of cards of each group.
        private List<T> DeckPool<T>(List<T> groups, SetupPlan plan, GroupType type) where T : ICard
        {
            var key = (plan.Scheme.Id, type, plan.VillainCards, plan.HenchmanCards);
            if (IsProbe && _deckPools.TryGetValue(key, out var kept))
            {
                return [.. kept.Cast<T>()];
            }

            var pool = groups.Where(group => DeckCards(plan, group.Id, type) > 0).ToList();
            if (IsProbe)
            {
                _deckPools[key] = [.. pool.Cast<ICard>()];
            }

            return pool;
        }

        private readonly Dictionary<(string, GroupType, int?, int?), List<ICard>> _deckPools = [];

        // What a setup with these cards lays out: the rulesets of the cards, whether they are mixed, the boxes whose
        // stacks are laid out, the parts the setup uses and the stand-ins its cards need. tacticsOnly are the Masterminds
        // whose Tactics alone go into the Villain Deck.
        private SetupLayout LayOut(IReadOnlyCollection<ICard> cards, IEnumerable<ICard> tacticsOnly)
        {
            var drawn = cards.Select(card => RulesetOf(card.Id)).ToHashSet();
            var mixed = drawn.Count > 1;

            // The rules' own ruleset lays out its stacks too, which a pair played under another ruleset's base game
            // (D-heroic) doesn't otherwise bring, as does the base game standing for the rules' own.
            var stackBoxes = boxes.Where(box => drawn.Contains(box.Ruleset) || box.Ruleset == rulesBox.Ruleset || box.Ruleset == _componentsBox.Ruleset).ToList();

            // The parts the setup lays out: those its rules use, and those its drawn cards use (D-uses). Under the
            // mixing base game's rules a mixed setup's rules use the recruit stacks of every included base game
            // (VIL p.21); under the other rules its cards of the mixing ruleset bring only what they use (#88). Rules
            // whose recruit stacks no included box has use those of the base game standing for theirs (D-editions, #240).
            var rulesUse = mixed && rulesBox == mixingBox ? boxes.Where(box => box.IsBaseGame).SelectMany(box => box.Setup!.Uses) : rules.Uses;
            if (rulesUse.Any(use => !boxes.Any(box => box.Components.Supplies(use.Part)?.Value > 0)))
            {
                rulesUse = _componentsBox.Setup!.Uses;
            }
            // A part no included box supplies is replaced by its stand-in (D-heroic, FI p.2).
            // Tactics shuffled into the Villain Deck play with no abilities, so their Mastermind brings no parts (#113).
            // Every setup shuffles all the included Bystanders together, so special Bystanders bring their parts too (#125).
            var cardParts = cards
                .Except(tacticsOnly)
                .SelectMany(card => card.Parts)
                .Concat(stackBoxes.Where(box => box.Components.Bystanders?.Value > 0).SelectMany(box => box.BystanderUses ?? []).Select(use => use.Part))
                .ToHashSet();
            var used = rulesUse.Select(use => use.Part).Concat(cardParts.Select(StandInFor).OfType<Part>()).ToHashSet();
            var stoodIn = standIns.Values.Where(active => cardParts.Contains(active.Rule.Part)).ToList();
            return new SetupLayout(drawn, mixed, stackBoxes, used, stoodIn);
        }

        // What the player sees when the Mastermind of a Scheme that adds it mid-game is revealed: the Always Leads groups
        // it brings, which are not part of the Villain Deck, and the Start of Game effects its card prints, as notes.
        private MastermindReveal Reveal(Scheme scheme, Mastermind mastermind, string joins)
        {
            var groups = Ignores(mastermind)
                ? []
                : (mastermind.AlwaysLeads?.GroupIds ?? []).Concat(mastermind.AlsoLeads?.GroupIds ?? []).Select(CatalogGroup).ToList();
            return new MastermindReveal(groups, WithMastermind(Plan(scheme) with { Notes = [] }, mastermind).Notes, joins);
        }

        // The counts one Scheme sets at this player count: the player-count table or Solo,
        // with the Scheme's Setup line applied on top, because printed card text wins.
        private SetupPlan Plan(Scheme scheme)
        {
            var effect = scheme.Setup;
            var notes = new List<RuleNote>();

            // A note the Scheme card itself causes, cited from the box that holds the card.
            var schemeBox = BoxOf(scheme.Id);
            RuleNote Card(string text, string source) => Note(text, source, schemeBox, scheme.Id);

            // A Scheme value that replaces a Solo value cites the ruling that the Scheme wins;
            // otherwise the card itself is the source. A Scheme rule overrides a normal rule unless the rules
            // say otherwise, so a Scheme that sets a count replaces the Solo count (D-readings).
            // What the Scheme's own ruleset calls it, which every note here starts with.
            var schemeWord = SchemeWord(scheme);
            RuleNote Replaces(string soloText, string text, string source) => Solo
                ? Note($"{schemeWord} overrides Solo: {soloText}", rules.Rulings.SchemeOverridesSolo, rulesBox)
                : Card($"{schemeWord} {text}", source);

            // The base game's extra Heroes are part of its player-count table, so they give no note.
            var heroes = Solo ? rules.Solo.Heroes.Value : rules.Heroes.Value + (ForPlayers(rules.ExtraHeroes ?? [])?.Value ?? 0);
            if (ForPlayers(effect.Heroes ?? []) is { } schemeHeroes)
            {
                heroes = schemeHeroes.Value;
                notes.Add(Replaces($"{heroes} {_terms.Heroes}", $"uses {heroes} {_terms.Heroes}", schemeHeroes.Source));
            }

            int? villainCards = null;
            if (effect.VillainCards is { } schemeVillains)
            {
                villainCards = schemeVillains.Value;
                notes.Add(Replaces(
                    $"{villainCards} cards of each {_terms.VillainGroup}",
                    $"puts {villainCards} cards of each {_terms.VillainGroup} in the {_terms.VillainDeck}",
                    schemeVillains.Source));
            }

            int? henchmanCards = null;
            if (ForPlayers(effect.HenchmanCards ?? []) is { } schemeHenchmen)
            {
                henchmanCards = schemeHenchmen.Value;
                notes.Add(Replaces(
                    $"{henchmanCards} {_terms.Henchmen} of each {_terms.HenchmanGroup}",
                    $"puts {henchmanCards} {_terms.Henchmen} of each {_terms.HenchmanGroup} in the {_terms.VillainDeck}",
                    schemeHenchmen.Source));
            }

            // The Scheme's own count for the extra Henchman Groups it adds wins over Solo's 3 too (D-readings).
            if (effect.ExtraHenchmanCards is { } extraCards && ForPlayers(effect.ExtraHenchmanGroups ?? []) is { } extraGroups)
            {
                var which = extraGroups.Value == 1 ? $"its extra {_terms.HenchmanGroup}" : $"each of its extra {_terms.HenchmanGroups}";
                notes.Add(Replaces(
                    $"{extraCards.Value} {_terms.Henchmen} of {which}",
                    $"puts {extraCards.Value} {_terms.Henchmen} of {which} in the {_terms.VillainDeck}",
                    extraCards.Source));
            }

            var bystanders = Solo ? rules.Solo.Bystanders.Value : row!.Bystanders;
            if (effect.VillainDeckBystanders is { } schemeBystanders)
            {
                bystanders = schemeBystanders.Value;
                notes.Add(Replaces(
                    $"{bystanders} Bystanders in the {_terms.VillainDeck}", $"puts {bystanders} Bystanders in the {_terms.VillainDeck}", schemeBystanders.Source));
            }

            // The Wound, Officer and Bindings stacks hold what the boxes supply unless the Scheme sets their size. A Scheme
            // that sets one stack's size sets that stack only.
            int? wounds = null;
            if (effect.WoundsPerPlayer is { } woundsPerPlayer)
            {
                wounds = woundsPerPlayer.Value * players;
                notes.Add(Card($"{schemeWord} sets the {StackOf(CardKind.Wound, "Wound")} stack to {woundsPerPlayer.Value} per player", woundsPerPlayer.Source));
            }

            if (effect.Wounds is { } woundStack)
            {
                wounds = woundStack.Value;
                notes.Add(Card($"{schemeWord} sets the {StackOf(CardKind.Wound, "Wound")} stack to {woundStack.Value}", woundStack.Source));
            }

            int? officers = null;
            if (effect.Officers is { } officerStack)
            {
                officers = officerStack.Value;
                notes.Add(Card($"{schemeWord} sets the {Named(CardKind.Officer, 2)} stack to {officerStack.Value}", officerStack.Source));
            }

            int? bindings = null;
            if (effect.BindingsPerPlayer is { } bindingsPerPlayer)
            {
                bindings = bindingsPerPlayer.Value * players;
                notes.Add(Card($"{schemeWord} sets the {Named(CardKind.Binding, 2)} stack to {bindingsPerPlayer.Value} per player", bindingsPerPlayer.Source));
            }

            var moves = new List<MovedCards>();
            foreach (var move in effect.Moves ?? [])
            {
                if (ForPlayers(move.Count) is not { } count)
                {
                    continue;
                }

                var each = move.PerPlayer ? count.Value * players : count.Value;
                var total = move.To == Pile.StartingDecks ? each * players : each;

                // A card whose stack no included box supplies comes from its stand-in's (D-heroic, FI p.2).
                var card = CardMove.PartOf(move.Card) is { } part && StandInFor(part) is { } standIn && CardMove.KindOf(standIn) is { } kind
                    ? move with { Card = kind }
                    : move;
                moves.Add(new MovedCards(card.Card, card.From(), card.To, each, total, card.Card == move.Card ? null : move.Card));
                notes.Add(Card(
                    $"{schemeWord} moves {each} {Named(move.Card, each)} {Into(card.To)}{(card.PerPlayer ? $", {count.Value} per player" : "")}",
                    count.Source));
            }

            if (effect.TwistsBesideScheme is { } beside)
            {
                notes.Add(Card($"{schemeWord} puts {beside.Value} {_terms.Twists} beside it", beside.Source));
            }

            foreach (var required in effect.RequiredHeroes ?? [])
            {
                notes.Add(Card($"{schemeWord} requires {Called(CatalogHero(required.HeroId))}", required.Source));
            }

            foreach (var count in effect.HeroCounts ?? [])
            {
                var (bound, value) = count.AtLeast is { } atLeast ? ("at least", atLeast) : ("exactly", count.Exactly!.Value);
                notes.Add(Card($"{schemeWord} requires {bound} {value} {HeroesOf(value, count.Team, count.HeroName, count.HeroNameContains)}", count.Source));
            }

            if (effect.TeamSplit is { } split)
            {
                var parts = split.Value.Select((count, index) => $"{count} {HeroesOf(count, null, null)} of {(index == 0 ? "one team" : "another")}");
                notes.Add(Card($"{schemeWord} requires {string.Join(" and ", parts)}", split.Source));
            }

            if (effect.DistinctHeroNames is { Value: true } distinct)
            {
                notes.Add(Card($"{schemeWord} allows no two {_terms.Heroes} with the same Hero Name", distinct.Source));
            }

            var outside = new List<OutsideDraw>();
            foreach (var rule in effect.OutsideHeroes ?? [])
            {
                if (ForPlayers(rule.Count) is not { } count)
                {
                    continue;
                }

                outside.Add(new OutsideDraw(rule, count.Value));
                var which = rule.Hero is { } heroId
                    ? Called(CatalogHero(heroId))
                    : $"{count.Value} extra {HeroesOf(count.Value, rule.Team, rule.HeroName ?? (rule.HeroNames is { } any ? string.Join(" or ", any.Value) : null), rule.HeroNameContains)}";
                var cards = rule.Cards is { } some ? $"{some} of {(count.Value == 1 ? "its" : "their")}" : count.Value == 1 ? "its" : "their";
                notes.Add(Card($"{schemeWord} draws {which} outside the {_terms.HeroDeck} and puts {cards} cards {Onto(rule.To)}", count.Source));
                if (rule.HeroNames is { } names)
                {
                    notes.Add(Card($"{schemeWord} takes any of {string.Join(" and ", names.Value)} as its {_terms.Hero}", names.Source));
                }
            }

            var outsideHenchmen = new List<HenchmenDraw>();
            foreach (var rule in effect.OutsideHenchmen ?? [])
            {
                if (ForPlayers(rule.Cards) is not { } cards)
                {
                    continue;
                }

                outsideHenchmen.Add(new HenchmenDraw(rule.To, cards.Value));
                notes.Add(Card(
                    $"{schemeWord} draws 1 extra {_terms.HenchmanGroup} outside the {_terms.VillainDeck} and puts {cards.Value} of its {CardName(CardKind.Henchman, cards.Value)} {Into(rule.To)}",
                    cards.Source));
            }

            var cardsBeside = new List<BesideDraw>();
            foreach (var rule in effect.CardsBeside ?? [])
            {
                if (ForPlayers(rule.Count) is not { } count)
                {
                    continue;
                }

                var each = rule.PerPlayer ? count.Value * players : count.Value;
                cardsBeside.Add(new BesideDraw(rule, each));
                var group = Called(CatalogGroup(rule.GroupId));
                var what = rule.Card is { } card ? $"{card} of {group}" : $"{each} {group}";
                notes.Add(Card($"{schemeWord} sets {what} beside it{(rule.PerPlayer ? $", {count.Value} per player" : "")}", count.Source));
            }

            if (effect.PlayersChooseHeroes is { } chooses)
            {
                notes.Add(Card($"{schemeWord} has each player choose a different {_terms.Hero} for the {_terms.HeroDeck}; the last {players} {_terms.Heroes} listed join it only in place of an earlier listed {_terms.Hero} a player chose", chooses.Source));
            }

            if (effect.OwnTactics is { } ownTactics)
            {
                notes.Add(Card($"{schemeWord} shuffles the {ownTactics.Value} Tactics of its {_terms.Mastermind} into the {_terms.VillainDeck}", ownTactics.Source));
            }

            var otherMasterminds = new List<MastermindsDraw>();
            foreach (var rule in effect.OutsideMasterminds ?? [])
            {
                if (ForPlayers(rule.Count) is not { } count)
                {
                    continue;
                }

                otherMasterminds.Add(new MastermindsDraw(rule, count.Value));
                var which = $"{count.Value} other {_terms.Mastermind}{(count.Value == 1 ? "" : "s")}";
                notes.Add(rule.Tactics is { } tactics
                    ? Card($"{schemeWord} draws {which} and shuffles {tactics.Value * count.Value} of {(count.Value == 1 ? "its" : "their")} Tactics into the {_terms.VillainDeck}", tactics.Source)
                    : Card($"{schemeWord} draws {which} and sets {(count.Value == 1 ? "it" : "them")} aside", count.Source));
                if (rule.Joins is { } joins)
                {
                    notes.Add(Card($"{schemeWord}: the {_terms.Mastermind}{(count.Value == 1 ? " set aside joins" : "s set aside join")} on {joins.Value}", joins.Source));
                }
            }

            var twists = ForPlayers(effect.Twists)
                ?? throw new InvalidDataException($"{scheme.Id} has no Twist count for {players} players.");

            var plan = new SetupPlan(
                scheme,
                null,
                heroes,
                Solo ? rules.Solo.VillainGroups.Value : row!.VillainGroups,
                Solo ? rules.Solo.HenchmanGroups.Value : row!.HenchmanGroups,
                henchmanCards,
                villainCards,
                twists.Value,
                effect.TwistsBesideScheme?.Value ?? 0,
                Solo ? rules.Solo.MasterStrikes.Value : rules.MasterStrikes.Value,
                bystanders,
                wounds,
                officers,
                bindings,
                moves,
                outside,
                outsideHenchmen,
                cardsBeside,
                otherMasterminds,
                (effect.RequiredGroups ?? []).ToDictionary(group => group.GroupId),
                [],
                notes);

            return Add(plan, effect, schemeWord, schemeBox, scheme.Id);
        }

        // The Scheme's plan with the Mastermind's setup effects added after the Scheme's. A Mastermind's own count of
        // Henchmen of each Henchman Group, such as Annihilus's 6 in Solo, replaces the table or Solo count, but not a
        // count the Scheme sets.
        private SetupPlan WithMastermind(SetupPlan plan, Mastermind mastermind)
        {
            plan = plan with { Mastermind = mastermind };
            if (plan.Scheme.Setup.MastermindTwists?.FirstOrDefault(twists => twists.MastermindId == mastermind.Id) is { } printed)
            {
                var note = Note(
                    $"{SchemeWord(plan.Scheme)} uses {printed.Value} {(printed.Value == 1 ? _terms.Twist : _terms.Twists)} total with {Called(mastermind)}",
                    printed.Source, BoxOf(plan.Scheme.Id), plan.Scheme.Id);
                plan = plan with { Twists = printed.Value, Notes = [.. plan.Notes, note] };
            }

            if (mastermind.Setup is not { } effects)
            {
                return plan;
            }

            plan = plan with
            {
                Notes =
                [
                    .. plan.Notes,
                    .. (effects.RequiredHeroes ?? []).Select(required =>
                        Note($"{Called(mastermind)} requires {Called(CatalogHero(required.HeroId))}", required.Source, BoxOf(mastermind.Id), mastermind.Id)),
                ],
            };

            if (plan.HenchmanCards is null && ForPlayers(effects.HenchmanCards ?? []) is { } henchmen)
            {
                var note = Note(
                    $"{Called(mastermind)} puts {henchmen.Value} {_terms.Henchmen} of each {_terms.HenchmanGroup} in the {_terms.VillainDeck}",
                    henchmen.Source, BoxOf(mastermind.Id), mastermind.Id);
                plan = plan with { HenchmanCards = henchmen.Value, Notes = [.. plan.Notes, note] };
            }

            return Add(plan, effects, Called(mastermind), BoxOf(mastermind.Id), mastermind.Id);
        }

        // Adds each effect that applies at this player count to the plan's counts, and each setup step to
        // its steps, with a note citing the card that prints it. An effect only adds to the count, so it
        // replaces no Solo value: a Scheme's "+N Heroes" applies on top of the Solo count (D-readings).
        private SetupPlan Add(SetupPlan plan, SetupEffects effects, string by, Box from, string cardId)
        {
            var notes = new List<RuleNote>(plan.Notes);

            int Extra(IReadOnlyList<PlayerCountValue>? effect, string one, string many, string where = "")
            {
                if (ForPlayers(effect ?? []) is not { } extra)
                {
                    return 0;
                }

                notes.Add(Note($"{by} adds {extra.Value} {(extra.Value == 1 ? one : many)}{where}", extra.Source, from, cardId));
                return extra.Value;
            }

            string Step(SetupStep step)
            {
                notes.Add(Note($"{by} adds a setup step: {Unfilled(step.Label)}", step.Source, from, cardId));
                return step.Label;
            }

            return plan with
            {
                Heroes = plan.Heroes + Extra(effects.ExtraHeroes, _terms.Hero, _terms.Heroes),
                VillainGroups = plan.VillainGroups + Extra(effects.ExtraVillainGroups, _terms.VillainGroup, _terms.VillainGroups),
                HenchmanGroups = plan.HenchmanGroups + Extra(effects.ExtraHenchmanGroups, _terms.HenchmanGroup, _terms.HenchmanGroups),
                Bystanders = plan.Bystanders + Extra(
                    effects.ExtraVillainDeckBystanders, "Bystander", "Bystanders", $" to the {_terms.VillainDeck}"),
                Steps = [.. plan.Steps, .. (effects.Steps ?? []).Select(Step)],
                Notes = notes,
            };
        }

        // The group a Mastermind leads: its one group, or of several the one already chosen, else one drawn from those the
        // pool still holds (all of them when it holds none), as the owner's ruling on an open choice says (#217).
        // A group the player chose (#337) is taken over a draw, so the chosen cards fit wherever the rules allow them.
        private T LeadGroup<T>(AlwaysLeadsGroup leads, List<T> chosen, List<T> pool, IReadOnlyList<T> playerChosen) where T : ICard
        {
            if (leads.OrGroupIds is null)
            {
                return (T)CatalogGroup(leads.GroupId);
            }

            var options = leads.GroupIds.Select(id => (T)CatalogGroup(id)).ToList();
            var drawable = options.Intersect(pool).ToList();
            return chosen.FirstOrDefault(options.Contains)
                ?? drawable.FirstOrDefault(option => playerChosen.Any(other => other.Id == option.Id))
                ?? DrawOne(drawable.Count > 0 ? drawable : options);
        }

        // Fills one group type's slots: the Scheme's required groups first, then the one it requires of several, then the Always Leads
        // group if a slot is left, then draws for the rest. A required group keeps its slot over
        // the Always Leads group. A required group from a box the setup doesn't include is still drawn, as the
        // player owns it, unless its card gives a substitute: then a group drawn from the pool takes its place, and
        // required records the substitute in its place. The groups drawn for the rest are the open picks a repair toward a
        // target may swap for others of the pool (#329). The groups the player chose (#337) come after every group a rule
        // places, so none displaces one, and before the open picks, so a repair never swaps them.
        private FilledSlots<T> FillSlots<T>(
            List<T> pool, GroupType type, int slots, SetupPlan plan, Mastermind mastermind,
            Dictionary<string, RequiredGroup> required, List<RuleNote> notes,
            List<(Mastermind Drained, string Source)> drained, IReadOnlyList<T> playerChosen) where T : ICard
        {
            var scheme = plan.Scheme;
            var chosen = new List<T>();
            foreach (var rule in (scheme.Setup.RequiredGroups ?? []).Where(group => group.GroupType == type))
            {
                var group = (T)CatalogGroup(rule.GroupId);
                var from = BoxOf(rule.GroupId);
                var included = boxes.Any(box => box.Holds(rule.GroupId));
                if (!included && rule.OtherBox?.Substitute is { } substitute)
                {
                    var standIn = DrawOne(pool.Except(chosen).ToList());
                    required.Remove(rule.GroupId);
                    required[standIn.Id] = rule;
                    chosen.Add(standIn);
                    notes.Add(Note(
                        $"{SchemeWord(scheme)} uses {(SameName(standIn, group) ? Labelled(standIn) : Called(standIn))} in place of {group.Name}: {from.Name} isn't included",
                        substitute, BoxOf(scheme.Id), scheme.Id));
                    continue;
                }

                chosen.Add(group);
                // A group from a box the setup doesn't include is named with that box below, so it needs no label of its own.
                var name = included ? Called(group) : group.Name;
                var text = rule.Cards is { } cards
                    ? $"{SchemeWord(scheme)} requires {name}, {cards} of its {CardName(CardKind.Henchman, cards)} in the {_terms.VillainDeck}"
                    : $"{SchemeWord(scheme)} requires {name}";
                notes.Add(included
                    ? Note(text, rule.Source, BoxOf(scheme.Id), scheme.Id)
                    : Note($"{text}, from {from.Name}, which isn't included", rule.OtherBox!.Source, BoxOf(scheme.Id), scheme.Id));
            }

            // The one group the Scheme requires of several: the Mastermind's Always Leads group when it is one of them, so
            // both rules are met, or else one drawn; a group the player chose (#337) is taken first. The others are left out
            // of every later draw (#172).
            if (scheme.Setup.OneOfGroups is { } choice && choice.GroupType == type)
            {
                var options = choice.GroupIds.Select(id => (T)CatalogGroup(id)).ToList();
                var leadable = Ignores(mastermind) || mastermind.AlwaysLeads is not { } alwaysLeads || alwaysLeads.GroupType != type
                    ? []
                    : alwaysLeads.GroupIds.Where(choice.GroupIds.Contains).Select(id => (T)CatalogGroup(id)).ToList();
                var group = options.FirstOrDefault(option => playerChosen.Any(other => other.Id == option.Id))
                    ?? leadable.Count switch { 0 => DrawOne(options), 1 => leadable[0], _ => DrawOne(leadable) };
                chosen.Add(group);
                required[group.Id] = new RequiredGroup(group.Id, type, choice.Source);
                pool = pool.Where(g => g.Id == group.Id || !choice.GroupIds.Contains(g.Id)).ToList();
                notes.Add(Note(
                    $"{SchemeWord(scheme)} requires one of {string.Join(" and ", options.Select(g => Called(g)))}, not both: {Called(group)}",
                    choice.Source, BoxOf(scheme.Id), scheme.Id));
            }

            // The Always Leads group of each Mastermind the Scheme sets aside, as an extra group of its own.
            foreach (var (other, source) in drained.Where(entry => entry.Drained.AlwaysLeads?.GroupType == type))
            {
                var group = LeadGroup(other.AlwaysLeads!, chosen, pool, playerChosen);
                if (!chosen.Contains(group))
                {
                    chosen.Add(group);
                }

                notes.Add(Note($"{SchemeWord(scheme)} adds {Called(other)}'s Always Leads group {Called(group)}", source, BoxOf(scheme.Id), scheme.Id));
            }

            // A set-aside Mastermind with no Always Leads group adds one extra Villain Group, drawn with the rest.
            if (type == GroupType.Villain)
            {
                foreach (var (other, source) in drained.Where(entry => entry.Drained.AlwaysLeads is null))
                {
                    notes.Add(Note($"{SchemeWord(scheme)} adds an extra {_terms.VillainGroup} for {Called(other)}, who leads no group", source, BoxOf(scheme.Id), scheme.Id));
                }
            }

            var leads = mastermind.AlwaysLeads;
            if (leads is not null && leads.GroupType == type)
            {
                if (Ignores(mastermind))
                {
                    notes.Add(Note($"Solo ignores {Called(mastermind)}'s Always Leads", rules.Solo.IgnoresAlwaysLeads.Source, rulesBox));
                }
                else
                {
                    // With every slot taken by groups that aren't one of its options, nothing is drawn: the note names
                    // every option, since none of them is played.
                    if (!chosen.Any(g => leads.GroupIds.Contains(g.Id)) && chosen.Count >= slots)
                    {
                        notes.Add(Note(
                            $"{SchemeWord(scheme)} requires {string.Join(" and ", chosen.Select(g => Called(g)))}, so {Called(mastermind)}'s Always Leads group {string.Join(" or ", leads.GroupIds.Select(id => Called(CatalogGroup(id))))} is dropped",
                            rules.Rulings.RequiredGroupDisplacesAlwaysLeads, rulesBox));
                    }
                    else
                    {
                        var group = LeadGroup(leads, chosen, pool, playerChosen);
                        if (!chosen.Contains(group))
                        {
                            chosen.Add(group);
                        }

                        notes.Add(leads.OrGroupIds is null
                            ? Note($"{Called(mastermind)} always leads {Called(group)}", rules.Rulings.AlwaysLeadsFillsSlot, rulesBox)
                            : Note(
                                $"{Called(mastermind)} always leads {Called(group)}, one of {string.Join(" and ", leads.GroupIds.Select(id => Called(CatalogGroup(id))))}",
                                leads.Source, BoxOf(mastermind.Id), mastermind.Id));
                        if (IgnoresAlwaysLeads)
                        {
                            notes.Add(Note($"{Called(mastermind)} leads {Called(group)} even in Solo", leads.Source, BoxOf(mastermind.Id), mastermind.Id));
                        }
                    }
                }
            }

            // The Mastermind's second group, one of several: one already chosen serves, or one the player chose or else one
            // drawn goes into a slot left.
            // Solo's note that it ignores Always Leads covers this group too.
            if (mastermind.AlsoLeads is { } also && also.GroupType == type && !Ignores(mastermind))
            {
                var options = pool.Where(g => also.GroupIds.Contains(g.Id)).ToList();
                var group = chosen.FirstOrDefault(options.Contains);
                if (group is null && chosen.Count >= slots)
                {
                    notes.Add(Note(
                        $"{SchemeWord(scheme)} requires {string.Join(" and ", chosen.Select(g => Called(g)))}, so {Called(mastermind)}'s other Always Leads group is dropped",
                        rules.Rulings.RequiredGroupDisplacesAlwaysLeads, rulesBox));
                }
                else
                {
                    if (group is null)
                    {
                        group = options.FirstOrDefault(option => playerChosen.Any(other => other.Id == option.Id)) ?? DrawOne(options);
                        chosen.Add(group);
                    }

                    notes.Add(Note(
                        $"{Called(mastermind)} also always leads {Called(group)}, one of {string.Join(" and ", options.Select(g => Called(g)))}",
                        also.Source, BoxOf(mastermind.Id), mastermind.Id));
                }
            }

            // The player's chosen groups: one a rule already placed, or whose every card the Scheme sets beside it, is in
            // the game already and takes no slot.
            var placedByRules = chosen.Count;
            var toPlace = playerChosen.Where(group => !chosen.Any(other => other.Id == group.Id) && Beside(plan, group.Id) == 0).ToList();
            var groupWord = type == GroupType.Villain ? _terms.VillainGroup : _terms.HenchmanGroup;
            foreach (var group in toPlace)
            {
                if (!pool.Any(other => other.Id == group.Id))
                {
                    if (Conflicts(Supplied(boxes, standIns, group)
                        ? $"{TheScheme(scheme)} rules out the {groupWord} {group.Name}: remove it, or {Instead(null, $"choose another {SchemeWord(scheme)}")}."
                        : $"The {groupWord} {group.Name} uses cards none of the included boxes has: include a box that has them, or remove it."))
                    {
                        return new FilledSlots<T>(chosen, chosen.Count, 0, pool);
                    }
                }
            }

            if (toPlace.Count > 0 && placedByRules + toPlace.Count > slots)
            {
                var room = Math.Max(slots - placedByRules, 0);
                var groupsWord = type == GroupType.Villain ? _terms.VillainGroups : _terms.HenchmanGroups;
                if (Conflicts(Crowded(
                    $"{TheScheme(scheme)} with the {(chosenCards.Mastermind is null ? "drawn " : "")}{_terms.Mastermind} {mastermind.Name}", room, toPlace.Count,
                    groupWord, groupsWord, Instead(mastermind, $"choose another {SchemeWord(scheme)} or {_terms.Mastermind}"))))
                {
                    return new FilledSlots<T>(chosen, chosen.Count, 0, pool);
                }
            }

            chosen.AddRange(toPlace.Select(group => pool.First(other => other.Id == group.Id)));

            // A Probe asks only whether the chosen groups fit, so it draws no open picks.
            if (IsProbe)
            {
                return new FilledSlots<T>(chosen, chosen.Count, 0, pool);
            }

            var firstOpen = chosen.Count;
            var open = pool.Except(chosen).ToList();
            chosen.AddRange(DrawMany(open, Math.Min(open.Count, slots - chosen.Count)));
            var opened = chosen.Count - firstOpen;

            // Slots the included boxes' groups can't fill, as Star-Lord's Awesome Mix Tape's doubled groups can't with few
            // boxes, are filled from groups of the same ruleset in boxes that aren't included, which the player adds (#138).
            // A group already chosen, as one a Scheme requires from a box that isn't included, isn't drawn twice.
            if (chosen.Count < slots)
            {
                var others = catalogBoxes
                    .Where(box => box.Ruleset == rulesBox.Ruleset && !boxes.Contains(box))
                    .SelectMany(box => type == GroupType.Villain ? box.VillainGroups.Cast<ICard>() : box.HenchmanGroups)
                    .Cast<T>().Except(chosen).ToList();
                foreach (var group in DrawMany(others, Math.Min(others.Count, slots - chosen.Count)))
                {
                    chosen.Add(group);
                    notes.Add(Note(
                        $"The included boxes have too few groups for {SchemeWord(scheme)}, so {group.Name} comes from {BoxOf(group.Id).Name}, which isn't included",
                        SchemeFirst, rulesBox));
                }
            }

            return new FilledSlots<T>(chosen, firstOpen, opened, pool);
        }

        // Draws the Hero Deck's Heroes, the Scheme's required Heroes first. Every draw picks from the Heroes that leave the
        // rest of the setup's Heroes drawable within the Scheme's Hero rules, so the draw never reaches a dead end. The
        // Hero Deck draws from the included Heroes whose parts the included boxes supply, or from every included Hero when
        // the Scheme's Hero rules need one that uses a part none supplies, which the setup then lays out from the box that
        // has it. The Heroes the Scheme draws outside the Hero Deck come after it, from DrawOutsideHeroes.
        private DeckDraw DrawDeck(SetupPlan plan)
        {
            var (deck, pool, preferred, heroes, included, rules) = StartDeck(plan);
            var required = deck.Count;
            while (deck.Count < rules.DeckSlots)
            {
                var options = pool.Where(hero => rules.CanComplete([.. deck, hero], [])).ToList();
                var fromIncluded = options.Where(preferred.Contains).ToList();
                deck.Add(DrawOne(fromIncluded.Count > 0 ? fromIncluded : options));
            }

            return new DeckDraw(deck, required, preferred, heroes, included, rules);
        }

        // The Hero Deck before its draws: the required and chosen Heroes, the pool the draws take from, the Heroes they
        // prefer, every Hero a draw may take, the included Heroes and the Scheme's Hero rules. It draws nothing.
        private (List<Hero> Deck, List<Hero> Pool, List<Hero> Preferred, List<Hero> Heroes, List<Hero> Included, HeroRules Rules) StartDeck(SetupPlan plan)
        {
            // The Mastermind's required Heroes are the Scheme's too, in the Hero Deck (#203).
            var setup = plan.Scheme.Setup with
            {
                RequiredHeroes = [.. plan.Scheme.Setup.RequiredHeroes ?? [], .. plan.Mastermind?.Setup?.RequiredHeroes ?? []],
            };
            setup = setup with { RequiredHeroes = setup.RequiredHeroes!.DistinctBy(rule => rule.HeroId).ToList() };
            var outsideRules = plan.Outside.SelectMany(draw => Enumerable.Repeat(draw.Rule, draw.Count)).ToList();

            // Required Heroes fill Hero Deck slots, so a Scheme that requires more Heroes than it has slots
            // uses them all. Each of the Scheme's draws outside the Hero Deck takes one slot per Hero.
            var deckSlots = Math.Max(plan.Heroes, setup.RequiredHeroes?.Count ?? 0);
            (List<Hero> Pool, List<Hero> Heroes, HeroRules Rules) From(List<Hero> pool)
            {
                var fromOtherBoxes = outsideRules
                    .Where(rule => rule.OtherBox is not null && !pool.Any(hero => HeroRules.Selects(rule, hero)))
                    .SelectMany(rule => catalogBoxes.SelectMany(box => box.Heroes).Where(hero => HeroRules.Selects(rule, hero)))
                    .Distinct();
                var heroes = pool.Concat(fromOtherBoxes).ToList();
                return (pool, heroes, new HeroRules(heroes, deckSlots, setup, outsideRules));
            }

            var included = boxes.SelectMany(box => box.AllHeroes).DistinctBy(hero => hero.Id).ToList();
            var deck = (setup.RequiredHeroes ?? []).Select(rule => included.Single(hero => hero.Id == rule.HeroId)).ToList();
            var (pool, heroes, rules) = From(_heroes);
            if (!rules.CanComplete(deck, []))
            {
                (pool, heroes, rules) = From(included);
            }

            // When the included Heroes can't meet the Scheme's Hero rules, as Avengers vs. X-Men's two teams of three
            // can't be met by Marvel Studios Phase 1 and Civil War alone, the Heroes it still needs come from the other
            // loaded boxes of its ruleset, since the player owns the card (D-scheme-first, #138), and each is named with
            // its box. Each draw still takes an included Hero whenever one keeps the Hero rules completable.
            var preferred = pool;
            if (!rules.CanComplete(deck, []))
            {
                var ruleset = BoxOf(plan.Scheme.Id).Ruleset;
                (pool, heroes, rules) = From(catalogBoxes.Where(box => box.Ruleset == ruleset).SelectMany(box => box.Heroes).ToList());
            }

            // The Heroes the player chose (#337) come after the required ones, so a repair never swaps them, and each must
            // be one a plain draw could take, so the setup's code reloads it. The pools above are chosen as a plain draw's
            // are, by the required Heroes alone.
            var toPlace = chosenCards.Heroes.Where(hero => !deck.Any(other => other.Id == hero.Id)).ToList();
            if (toPlace.Count > 0)
            {
                if (toPlace.FirstOrDefault(hero => !preferred.Any(other => other.Id == hero.Id)) is { } missing)
                {
                    if (Conflicts(
                        $"The {_terms.Hero} {missing.Name} uses cards none of the included boxes has: include a box that has them, or remove it."))
                    {
                        return (deck, pool, preferred, heroes, included, rules);
                    }
                }

                if (deck.Count + toPlace.Count > rules.DeckSlots)
                {
                    if (Conflicts(Crowded(
                        $"{TheScheme(plan.Scheme)} at {(players == 1 ? "1 player" : $"{players} players")}", rules.DeckSlots - deck.Count, toPlace.Count, _terms.Hero,
                        _terms.Heroes, Instead(null, $"choose another {SchemeWord(plan.Scheme)} or player count"))))
                    {
                        return (deck, pool, preferred, heroes, included, rules);
                    }
                }

                deck.AddRange(toPlace.Select(hero => preferred.First(other => other.Id == hero.Id)));
                if (!rules.CanComplete(deck, []))
                {
                    var names = toPlace.Select(hero => hero.Name).ToList();
                    var listed = names.Count == 1 ? names[0] : $"{string.Join(", ", names[..^1])} and {names[^1]}";
                    if (Conflicts(
                        $"The {_terms.Hero} rules of the {(chosenCards.Scheme is null ? "drawn " : "")}{SchemeWord(plan.Scheme)} {plan.Scheme.Name} can't be met with {listed}: remove a chosen {_terms.Hero}, or {Instead(null, $"choose another {SchemeWord(plan.Scheme)}")}."))
                    {
                        return (deck, pool, preferred, heroes, included, rules);
                    }
                }
            }

            return (deck, pool, preferred, heroes, included, rules);
        }

        // Draws each Hero the Scheme draws outside the Hero Deck, once the Hero Deck is final. A draw the box data lets take
        // a Hero from another box draws from the loaded Heroes it allows when no included Hero it can draw has one of its
        // Hero Names (D-scheme-first, #138), and still takes an included one first, as one whose part no included box
        // supplies (#150), so a Scheme's named Hero comes from another box only when no included one can be used.
        private List<OutsideHero> DrawOutsideHeroes(DeckDraw deck)
        {
            var outside = new List<Hero>();
            while (outside.Count < deck.Rules.OutsideSlots.Count)
            {
                var options = deck.Drawable.Where(hero => deck.Rules.CanComplete(deck.Heroes, [.. outside, hero])).ToList();
                var fromIncluded = options.Where(deck.Included.Contains).ToList();
                outside.Add(DrawOne(fromIncluded.Count > 0 ? fromIncluded : options));
            }

            return outside
                .Zip(deck.Rules.OutsideSlots, (hero, rule) => new OutsideHero(hero, rule.To, rule.Cards ?? BoxOf(hero.Id).Components.HeroCards.Value))
                .ToList();
        }

        // Repairs a plain draw's open picks toward the targets (#329): the Hero Deck's Heroes no card requires and the
        // Villain and Henchman Groups drawn into open slots. Each round rates every swap of one open pick for another of its
        // pool with the scorers the response uses, on the Villain Deck's real counts. When some swaps land in every target
        // band, one of them is drawn, so setups stay varied; otherwise the swap that comes closest is taken. It stops in
        // band, when no swap comes closer, or after RepairRounds, leaving the nearest choice it found. A Hero swap must keep
        // the Scheme's Hero rules completable, and a Henchman Group the Scheme draws outside the Villain Deck is not swapped in.
        private void Repair(
            RatingTargets targets, SetupPlan plan, Mastermind drawnMastermind, FilledSlots<VillainGroup> villains,
            FilledSlots<HenchmanGroup> henchmen, DeckDraw deck, IReadOnlyList<OutsideHenchmanGroup> outsideHenchmen)
        {
            var heroes = deck.Heroes;
            // The fill-in Heroes a player's choice may bring in are not rated (#259), so they are not swapped either.
            var rated = heroes.Count - (plan.Scheme.Setup.PlayersChooseHeroes is not null ? Math.Min(players, heroes.Count) : 0);
            ICard[] alongside = [.. outsideHenchmen.Select(outside => outside.Group), .. plan.Beside.Select(beside => CatalogGroup(beside.Rule.GroupId))];
            RatingTargets.Rated Rate() => targets.Rate(
                plan.Scheme, drawnMastermind, villains.Groups, henchmen.Groups, heroes.Take(rated).ToList(),
                plan.MasterStrikes, plan.Twists, HenchmanCards(plan, henchmen.Groups), players, rulesBox.Ruleset,
                () =>
                {
                    var layout = LayOut([plan.Scheme, drawnMastermind, .. villains.Groups, .. henchmen.Groups, .. heroes, .. alongside], []);
                    return (layout.Used, layout.StoodIn.Select(active => active.Rule).ToList());
                });

            var current = Rate();
            for (var round = 0; round < RepairRounds && !targets.Reached(current); round++)
            {
                var swaps = new List<Swap>();
                void Consider<T>(List<T> picks, int from, int count, IEnumerable<T> pool, Func<bool> legal)
                {
                    var candidates = pool.Except(picks).ToList();
                    for (var slot = from; slot < from + count; slot++)
                    {
                        var (at, was) = (slot, picks[slot]);
                        foreach (var with in candidates)
                        {
                            picks[at] = with;
                            swaps.Add(new Swap(Rate(), () =>
                            {
                                picks[at] = with;
                                if (legal())
                                {
                                    return true;
                                }

                                picks[at] = was;
                                return false;
                            }));
                        }

                        picks[at] = was;
                    }
                }

                Consider(heroes, deck.Required, Math.Max(rated - deck.Required, 0), deck.Preferred, () => deck.Rules.CanComplete(heroes, []));
                Consider(villains.Groups, villains.FirstOpen, villains.Open, villains.Pool, () => true);
                Consider(henchmen.Groups, henchmen.FirstOpen, henchmen.Open, henchmen.Pool.Except(outsideHenchmen.Select(outside => outside.Group)), () => true);

                var inBand = swaps.Where(swap => targets.Reached(swap.Rated)).ToList();
                while (inBand.Count > 0)
                {
                    var pick = inBand[random.Next(inBand.Count)];
                    if (pick.Apply())
                    {
                        return;
                    }

                    inBand.Remove(pick);
                }

                var distance = targets.Distance(current);
                var taken = false;
                foreach (var swap in swaps.Where(swap => targets.Distance(swap.Rated) < distance).OrderBy(swap => targets.Distance(swap.Rated)))
                {
                    if (swap.Apply())
                    {
                        current = swap.Rated;
                        taken = true;
                        break;
                    }
                }

                if (!taken)
                {
                    return;
                }
            }
        }

        // A setup step that would leave a Hero to the players names a placeholder the app fills by drawing (#242): {hero}
        // is an extra Hero for the Hero Deck, as Avatarr's, drawn from the included Heroes the setup doesn't use that keep
        // the Scheme's Hero rules met with it in the Hero Deck, else from any it doesn't use. With none to draw, the step
        // keeps the words its rule note uses. The Heroes drawn for the steps come back with them, for the setup code (#335).
        private const string HeroPlaceholder = "{hero}";

        private static string Unfilled(string label) =>
            label.Replace(HeroPlaceholder, "an extra Hero");

        private (IReadOnlyList<string> Steps, IReadOnlyList<Hero> Extras) FillHeroSteps(SetupPlan plan, IReadOnlyList<Hero> deck, IReadOnlyList<OutsideHero> outside)
        {
            var outsideHeroes = outside.Select(drawn => drawn.Hero).ToList();
            var outsideRules = plan.Outside.SelectMany(draw => Enumerable.Repeat(draw.Rule, draw.Count)).ToList();
            var extras = new List<Hero>();

            string Fill(string label)
            {
                if (label.Contains(HeroPlaceholder))
                {
                    var unused = _heroes.Where(hero => !deck.Concat(outsideHeroes).Concat(extras).Any(other => other.Id == hero.Id)).ToList();
                    var rules = new HeroRules(_heroes, deck.Count + extras.Count + 1, plan.Scheme.Setup, outsideRules);
                    var fitting = unused.Where(hero => rules.CanComplete([.. deck, .. extras, hero], outsideHeroes)).ToList();
                    if ((fitting.Count > 0 ? fitting : unused) is { Count: > 0 } options)
                    {
                        var drawn = DrawOne(options);
                        extras.Add(drawn);
                        var named = boxes.Count > 1 ? $"{drawn.Name} (from {BoxLabel(BoxOf(drawn.Id), drawn.Id)})" : drawn.Name;
                        label = label.Replace(HeroPlaceholder, named);
                    }
                }

                return Unfilled(label);
            }

            return (plan.Steps.Select(Fill).ToList(), extras);
        }

        // "The Scheme X" for the chosen Scheme, or "The drawn Scheme X" for one the draw took (#337), so a message never asks
        // the player to change a card they didn't choose.
        private string TheScheme(Scheme scheme) => $"The {(chosenCards.Scheme is null ? "drawn " : "")}{SchemeWord(scheme)} {scheme.Name}";

        // What to change besides the chosen cards (#337): the chosen Scheme (and Mastermind, when one is passed) as instead
        // says, or, when the draw took either, another draw.
        private string Instead(Mastermind? mastermind, string instead) =>
            chosenCards.Scheme is null || (mastermind is not null && chosenCards.Mastermind is null) ? "generate again" : instead;

        // A conflict over the chosen cards (#337) ends a draw with its message. A Probe, which tries thousands of pairs,
        // records the first one instead, since throwing costs too much there; its caller then returns at once.
        private bool Conflicts(string message)
        {
            if (!IsProbe)
            {
                throw new ChosenCardsConflictException(message);
            }

            _conflict ??= message;
            return true;
        }

        private string? _conflict;

        // Why count chosen cards don't fit the room the rest of the setup leaves them, and what to change instead (#337).
        private static string Crowded(string setup, int room, int count, string one, string many, string instead) => room == 0
            ? $"{setup} leaves no room for a chosen {one}: remove {(count == 1 ? "it" : "them")}, or {instead}."
            : $"{setup} leaves room for {room} chosen {(room == 1 ? one : many)}, but {count} are chosen: remove {count - room}, or {instead}.";

        // "X-Men Heroes", "Jean Grey Hero" or "Heroes": what count Heroes of a team or a Hero Name are called.
        // nameContains is a word the Heroes have in their Hero Names: 'Heroes with "Hulk" in their Hero Names'.
        private string HeroesOf(int count, string? team, string? heroName, string? nameContains = null)
        {
            var kind = team is null ? heroName : boxes.SelectMany(box => box.Glossary).FirstOrDefault(term => term.Id == team)?.Name ?? team;
            var heroes = $"{(kind is null ? "" : kind + " ")}{(count == 1 ? _terms.Hero : _terms.Heroes)}";
            return nameContains is null ? heroes : $"{heroes} with \"{nameContains}\" in {(count == 1 ? "its Hero Name" : "their Hero Names")}";
        }

        // Any loaded Hero, since a Scheme's named Hero is drawn even when no included box supplies a part it uses.
        private Hero CatalogHero(string heroId) => catalogBoxes.SelectMany(box => box.Heroes).Single(hero => hero.Id == heroId);

        // A card's printed name, with its box when another card of its kind in the included boxes prints the same name
        // and more than one box is included, so a note never leaves the player unsure which physical card it means (#243).
        // A Probe's notes are never shown, so it names each card plainly.
        private string Called(ICard card) => !IsProbe && boxes.Count > 1 && SharedName(card) ? Labelled(card) : card.Name;

        private string Labelled(ICard card) => $"{card.Name} (from {BoxLabel(BoxOf(card.Id), card.Id)})";

        private static bool SameName(ICard card, ICard other) =>
            other.Id != card.Id && string.Equals(other.Name, card.Name, StringComparison.OrdinalIgnoreCase);

        private bool SharedName(ICard card)
        {
            var kind = card.Id.Split('_')[1];
            return boxes
                .SelectMany(box => box.Heroes.Cast<ICard>().Concat(box.VillainGroups).Concat(box.HenchmanGroups).Concat(box.Masterminds).Concat(box.Schemes))
                .Any(other => other.Id.Split('_')[1] == kind && SameName(card, other));
        }

        private string Onto(Pile to) => to switch
        {
            Pile.VillainDeck => $"into the {_terms.VillainDeck}",
            Pile.BesideScheme => $"beside the {_terms.Scheme}",
            Pile.SetAside => "in a stack set aside",
            Pile.KoPile => "into the KO pile",
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null),
        };

        private List<T> DrawMany<T>(IEnumerable<T> options, int count) where T : ICard
        {
            var left = options.ToList();
            var drawn = new List<T>();
            for (var i = 0; i < count; i++)
            {
                var pick = DrawOne(left);
                left.Remove(pick);
                drawn.Add(pick);
            }

            return drawn;
        }

        private T DrawOne<T>(IReadOnlyList<T> options) where T : ICard => random.Pick(options);

        // The drawn Henchman Groups that are the Scheme's extra groups when it gives them a card count of their own: the
        // last of those drawn into open slots, as every drawn group is random, never a group the Scheme requires or the
        // Mastermind leads. Empty when the Scheme gives no count or adds no group at this player count.
        private IReadOnlySet<string> ExtraHenchmanGroups(SetupPlan plan, IReadOnlyList<string> drawn)
        {
            var setup = plan.Scheme.Setup;
            if (setup.ExtraHenchmanCards is null || ForPlayers(setup.ExtraHenchmanGroups ?? []) is not { } extra)
            {
                return new HashSet<string>();
            }

            var placed = plan.Required.Keys.ToHashSet();
            if (!Ignores(plan.Mastermind!))
            {
                placed.UnionWith(plan.Mastermind!.AlwaysLeads?.GroupIds ?? []);
                placed.UnionWith(plan.Mastermind!.AlsoLeads?.GroupIds ?? []);
            }

            return drawn.Where(id => !placed.Contains(id)).TakeLast(extra.Value).ToHashSet();
        }

        // How many Henchman cards the drawn Henchman Groups put in the Villain Deck: each group's share, or the Scheme's
        // count for its extra groups.
        private int HenchmanCards(SetupPlan plan, IReadOnlyList<HenchmanGroup> groups)
        {
            var extra = ExtraHenchmanGroups(plan, groups.Select(group => group.Id).ToList());
            return groups.Sum(group => extra.Contains(group.Id)
                ? plan.Scheme.Setup.ExtraHenchmanCards!.Value
                : ShareOfDeck(plan, group.Id, GroupType.Henchman));
        }

        // The Villains of the extra Villain Groups the Mastermind sets aside, when its card does (#208). The extra groups are
        // drawn from those no rule placed, so they are the last drawn that are neither required nor the Mastermind's, or a
        // drained Mastermind's, Always Leads group. A group's Ambush Schemes stay in the Villain Deck.
        private List<VillainsSetAside> VillainsSetAside(
            SetupPlan plan, Mastermind mastermind, IReadOnlyList<VillainGroup> drawn,
            IReadOnlyList<(Mastermind Drained, string Source)> drained, List<RuleNote> notes)
        {
            if (mastermind.Setup is not { SetAsideExtraVillains: { } setAside } effects || ForPlayers(effects.ExtraVillainGroups ?? []) is not { } extra)
            {
                return [];
            }

            var placed = plan.Required.Keys.Concat(drained.Where(entry => entry.Drained.AlwaysLeads is not null).SelectMany(entry => entry.Drained.AlwaysLeads!.GroupIds)).ToHashSet();
            if (!Ignores(mastermind))
            {
                placed.UnionWith(mastermind.AlwaysLeads?.GroupIds ?? []);
                placed.UnionWith(mastermind.AlsoLeads?.GroupIds ?? []);
            }

            var aside = drawn.Where(group => !placed.Contains(group.Id)).TakeLast(extra.Value)
                .Select(group => new VillainsSetAside(group, Math.Max(0, DeckCards(plan, group.Id, GroupType.Villain) - group.AmbushSchemes)))
                .ToList();
            notes.AddRange(aside.Select(entry => Note(
                $"{Called(mastermind)} sets aside the Villains of {Called(entry.Group)}, its extra {_terms.VillainGroup}; their Ambush Schemes stay in the {_terms.VillainDeck}",
                setAside.Source, BoxOf(mastermind.Id), mastermind.Id)));
            return aside;
        }

        // How many cards a group puts in the Villain Deck when the Scheme sets none of them beside it: all of a
        // Villain Group, and of a Henchman Group the Scheme's count, Solo's, or all of it.
        // A Henchman Group the Scheme requires with a count of its own puts that many in.
        private int ShareOfDeck(SetupPlan plan, string groupId, GroupType type) => type == GroupType.Villain
            ? plan.VillainCards ?? GroupCards(groupId, type)
            : plan.Required.GetValueOrDefault(groupId)?.Cards
                ?? plan.HenchmanCards ?? (Solo ? rules.Solo.HenchmanCards.Value : GroupCards(groupId, type));

        // How many cards a group puts in the Villain Deck once the Scheme has set some of them beside it: its share,
        // or what is left of the group when that is fewer. With none left the group can't be drawn there.
        private int DeckCards(SetupPlan plan, string groupId, GroupType type) =>
            Math.Min(ShareOfDeck(plan, groupId, type), GroupCards(groupId, type) - Beside(plan, groupId));

        private int GroupCards(string groupId, GroupType type) => type == GroupType.Villain
            ? BoxOf(groupId).Components.VillainGroupCards.Value
            : BoxOf(groupId).Components.HenchmanGroupCards.Value;

        // How many of a group's cards the Scheme sets beside it.
        private static int Beside(SetupPlan plan, string groupId) =>
            plan.Beside.Where(draw => draw.Rule.GroupId == groupId).Sum(draw => draw.Count);

        // A Villain or Henchman Group of any loaded box, since a Scheme can require one from a box the setup doesn't include.
        private ICard CatalogGroup(string groupId) =>
            catalogBoxes.SelectMany(box => box.VillainGroups.Cast<ICard>().Concat(box.HenchmanGroups)).Single(group => group.Id == groupId);

        private string CardName(CardKind card, int count) => (card, count == 1) switch
        {
            (CardKind.Hero, true) => $"{_terms.Hero} card",
            (CardKind.Hero, false) => $"{_terms.Hero} cards",
            (CardKind.Henchman, true) => _terms.Henchman,
            (CardKind.Henchman, false) => _terms.Henchmen,
            (CardKind.Bystander, true) => "Bystander",
            (CardKind.Bystander, false) => "Bystanders",
            (CardKind.Wound, true) => "Wound",
            (CardKind.Wound, false) => "Wounds",
            (CardKind.Officer, true) => "S.H.I.E.L.D. Officer",
            (CardKind.Officer, false) => "S.H.I.E.L.D. Officers",
            (CardKind.Sidekick, true) => "Sidekick",
            (CardKind.Sidekick, false) => "Sidekicks",
            (CardKind.Binding, true) => "Binding",
            (CardKind.Binding, false) => "Bindings",
            (CardKind.Twist, true) => _terms.Twist,
            (CardKind.Twist, false) => _terms.Twists,
            (CardKind.Ambition, true) => "Ambition card",
            (CardKind.Ambition, false) => "Ambition cards",
            _ => throw new ArgumentOutOfRangeException(nameof(card), card, null),
        };

        // A kind of card's name, or "<stand-in>/<real>" when the setup lays out a stand-in for its part because no
        // included box supplies it, so the player can use whichever cards they own (owner ruling, #141, #258).
        private string Named(CardKind card, int count) =>
            CardMove.PartOf(card) is { } part && standIns.TryGetValue(part, out var active)
            && active.Rule.With is { } with && CardMove.KindOf(with) is { } stoodIn
                ? $"{CardName(stoodIn, count)}/{CardName(card, count)}"
                : CardName(card, count);

        // A stack's name in a rule note: the plain word, or the stand-in's slashed name in plural.
        private string StackOf(CardKind card, string plain) => Named(card, 2) == CardName(card, 2) ? plain : Named(card, 2);

        private string Into(Pile to) => to switch
        {
            Pile.VillainDeck => $"into the {_terms.VillainDeck}",
            Pile.HeroDeck => $"into the {_terms.HeroDeck}",
            Pile.BesideScheme => "beside it",
            Pile.StartingDecks => "into each starting deck",
            Pile.SetAside => "into a stack set aside",
            Pile.KoPile => "into the KO pile",
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null),
        };

        private PlayerCountValue? ForPlayers(IReadOnlyList<PlayerCountValue> values) =>
            values.SingleOrDefault(value => value.Players?.Contains(players) ?? true);

        // Ids start with the declaring box's id, and box ids contain no underscore. A required card can come from a
        // box the setup doesn't include, so every loaded box is looked in.
        private Box BoxOf(string id)
        {
            if (!_boxesOf.TryGetValue(id, out var box))
            {
                _boxesOf[id] = box = catalogBoxes.Single(box => id.StartsWith(box.Id + "_", StringComparison.Ordinal));
            }

            return box;
        }

        private readonly Dictionary<string, Box> _boxesOf = [];

        private Ruleset RulesetOf(string id) => BoxOf(id).Ruleset;

        // The ruleset whose rules a Scheme and Mastermind pair follows (D-mixed, #88): the mixing base game's when
        // either card is of its ruleset, so a Villains Plot or Commander means the Villains rules; otherwise the
        // pair's own, since only two rulesets exist and a pair with no card of the mixing one shares the other.
        // A pair of a ruleset with no included base game follows the included base game's rules when the boxes of
        // its cards of that ruleset can be played under another's (D-heroic), and is otherwise dropped.
        // A Revised card with no Revised base game included is on the First Edition side, as is any card that isn't
        // Villainous (D-editions, #240).
        private Ruleset RulesOf(Scheme scheme, Mastermind mastermind)
        {
            Ruleset Side(Ruleset ruleset) => ruleset == Ruleset.Revised && !baseRulesets.Contains(ruleset) ? Ruleset.FirstEdition : ruleset;
            var ruleset = mixingBox is not null && RulesetOf(mastermind.Id) == mixingBox.Ruleset ? mixingBox.Ruleset : Side(RulesetOf(scheme.Id));
            var pair = new[] { BoxOf(scheme.Id), BoxOf(mastermind.Id) }.Where(box => Side(box.Ruleset) == ruleset);
            return baseRulesets.Contains(ruleset) || pair.Any(box => box.OtherRuleset is null) ? ruleset : baseRulesets.Single();
        }

        // The ruleset whose starting cards the players use when the rules' base game isn't included and the base game
        // standing for it deals another team's, or null (D-editions, #240).
        private Ruleset? StartingTeam() =>
            RulesetTerms.StartingTeam(_componentsBox.Ruleset) == RulesetTerms.StartingTeam(rulesBox.Ruleset) ? null : _componentsBox.Ruleset;

        // The part a setup lays out for a card that uses this one: the part itself, its stand-in when no included box
        // supplies it, or null when the stand-in needs no part.
        private Part? StandInFor(Part part) => standIns.TryGetValue(part, out var active) ? active.Rule.With : part;

        // What the Scheme's own ruleset calls it: a Villainous Scheme is a Plot.
        private string SchemeWord(Scheme scheme) => RulesetTerms.For(RulesetOf(scheme.Id)).Scheme;

        // A note cites a source key of the box whose rule it is, since each box lists its own sources.
        // Once more than one box is included, the note also names that box.
        // A note a card causes cites the box that declares it but names the included boxes that hold it, as the card's
        // own label does: a reprint's note names the reprinting box when the original isn't included (#153).
        private RuleNote Note(string text, string citation, Box from, string? cardId = null) =>
            new(text, citation, IsProbe ? null : LinkOf(citation, from), !IsProbe && boxes.Count > 1 ? BoxLabel(from, cardId) : null);

        private string BoxLabel(Box from, string? cardId)
        {
            var holders = cardId is null ? [] : boxes.Where(box => box.Holds(cardId)).ToList();
            return holders.Count == 0 ? from.Name : string.Join(" or ", holders.Select(box => box.Name));
        }

        // A pair's plan with its groups placed, the first steps of Finish: the Mastermind as the setup sees it, when it joins
        // later, the slot notes, the Masterminds the Scheme sets aside with their Always Leads groups, and the filled slots.
        private sealed record Placed(
            SetupPlan Plan, Mastermind Mastermind, string? JoinsLater, List<RuleNote> SlotNotes,
            List<(Mastermind Drained, string Source)> DrainedLeads, FilledSlots<VillainGroup> VillainSlots, FilledSlots<HenchmanGroup> HenchmanSlots);

        // The groups filling one type's slots, and the open picks among them: Open groups from FirstOpen, drawn from Pool.
        private sealed record FilledSlots<T>(List<T> Groups, int FirstOpen, int Open, IReadOnlyList<T> Pool);

        // The Hero Deck's Heroes, the first Required of them the ones the Scheme or Mastermind requires or the player chose;
        // Preferred, the included Heroes each draw takes first; Drawable, every Hero a draw may take; Included, the included
        // boxes' Heroes; and the Scheme's Hero rules.
        private sealed record DeckDraw(
            List<Hero> Heroes, int Required, IReadOnlyList<Hero> Preferred, IReadOnlyList<Hero> Drawable, IReadOnlyList<Hero> Included, HeroRules Rules);

        // One swap the repair can make, its rating, and Apply, which makes it when it is legal and says whether it did.
        private sealed record Swap(RatingTargets.Rated Rated, Func<bool> Apply);
    }

    // What a setup lays out for its cards: the rulesets they follow, whether that is more than one, the boxes whose
    // stacks are laid out, the parts it uses, and the stand-ins its cards need.
    private sealed record SetupLayout(
        HashSet<Ruleset> Rulesets, bool Mixed, List<Box> StackBoxes, HashSet<Part> Used, List<ActiveStandIn> StoodIn);

    // A card is set up only when an included box supplies every part it uses, or the part has a stand-in some included
    // box supplies, or one that needs no part (D-heroic). A part is the same card whichever ruleset's box holds it, as a
    // Revised base game's Wounds serve a First Edition card (D-editions, #240).
    private static bool Supplied(IReadOnlyList<Box> boxes, IReadOnlyDictionary<Part, ActiveStandIn> standIns, ICard card) =>
        card.Parts.All(part => standIns.TryGetValue(part, out var active)
            ? active.Rule.With is not { } with || boxes.Any(box => box.Components.Supplies(with)?.Value > 0)
            : boxes.Any(box => box.Components.Supplies(part)?.Value > 0));

    // The random source of a Probe: the first option of every draw.
    private sealed class FirstOption : IRandomSource
    {
        public int Next(int exclusiveMax) => 0;
    }

    // A stand-in that applies to a setup, and the box that gives it.
    private sealed record ActiveStandIn(StandIn Rule, Box From);

    // What a part's stack is called in a rule note.
    private static string StackName(Part part) => part switch
    {
        Part.Wounds => "Wound",
        Part.Officers => "S.H.I.E.L.D. Officer",
        Part.Sidekicks => "Sidekick",
        Part.Bindings => "Bindings",
        Part.MadameHydra => "Madame HYDRA",
        Part.NewRecruits => "New Recruit",
        Part.Shards => "Shard",
        Part.Ambitions => "Ambition",
        Part.Horrors => "Horror",
        _ => throw new ArgumentOutOfRangeException(nameof(part), part, null),
    };

    // The counts a setup uses, from the rules and the setup effects of its Scheme and, once one is
    // paired with it, its Mastermind. HenchmanCards is the Scheme's (or else the Mastermind's) count of cards of each Henchman
    // Group, or null when the table or Solo sets it. Required holds the Scheme's required groups by the id of the
    // group the setup uses for each, which is a substitute's once one is drawn. OutsideHenchmen has one entry per Henchman Group
    // the Scheme draws outside the Villain Deck.
    private sealed record SetupPlan(
        Scheme Scheme,
        Mastermind? Mastermind,
        int Heroes,
        int VillainGroups,
        int HenchmanGroups,
        int? HenchmanCards,
        int? VillainCards,
        int Twists,
        int TwistsBeside,
        int MasterStrikes,
        int Bystanders,
        int? Wounds,
        int? Officers,
        int? Bindings,
        IReadOnlyList<MovedCards> Moves,
        IReadOnlyList<OutsideDraw> Outside,
        IReadOnlyList<HenchmenDraw> OutsideHenchmen,
        IReadOnlyList<BesideDraw> Beside,
        IReadOnlyList<MastermindsDraw> OutsideMasterminds,
        IReadOnlyDictionary<string, RequiredGroup> Required,
        IReadOnlyList<string> Steps,
        IReadOnlyList<RuleNote> Notes)
    {
        public int MovedIn(Pile to) => Moves.Where(move => move.To == to).Sum(move => move.Count);

        public int MovedOut(Pile from) => Moves.Where(move => move.From == from).Sum(move => move.Total);
    }

    // How many Heroes one of the Scheme's draws outside the Hero Deck takes at this player count.
    private sealed record OutsideDraw(OutsideHeroes Rule, int Count);

    // How many cards of a Henchman Group the Scheme draws outside the Villain Deck at this player count, and where they go.
    private sealed record HenchmenDraw(Pile To, int Cards);

    // How many cards of a group the Scheme sets beside it at this player count.
    private sealed record BesideDraw(CardsBeside Rule, int Count);

    // How many other Masterminds one of the Scheme's draws takes at this player count.
    private sealed record MastermindsDraw(OutsideMasterminds Rule, int Count);
}
