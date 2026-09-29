using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Draws a random legal setup the way players would at the table: the Scheme from those allowed
// at the player count, then a Mastermind that can complete it, then the groups they require, then
// the remaining Villain and Henchman Groups, then any Henchman Groups the Scheme draws outside the
// Villain Deck, then the Heroes, then any Heroes the Scheme draws outside the Hero Deck. Each draw goes
// through the IRandomSource and picks from the options left, in catalog order, so a fixed sequence of draws
// always gives the same setup.
//
// When the included boxes follow more than one ruleset, the drawn cards decide which rules apply (#85): a
// setup with any card of the mixing base game's ruleset (Villainous) follows that base game's rules, and one
// with none follows the other ruleset's base game. The counts depend on those rules, so the Scheme and
// Mastermind decide them: a pair with a Villainous card draws the rest from every included box under the
// Villains rules, and an all-Heroic pair draws the rest from the Heroic boxes only under First Edition rules,
// so no later draw can change the rules the counts came from. A pair whose rules have no included base game
// can't be set up and is dropped before the draw.
//
// Every count and rule comes from the box data; nothing here compares a card name.
public sealed class SetupGenerator(BoxCatalog catalog)
{
    public const string CoreBoxId = "core";

    public GenerationResult Generate(int players, IRandomSource random) => Generate(players, [CoreBoxId], random);

    // Every included box contributes its cards and stacks, drawn in catalog order whatever order the boxes
    // are named in. The rules of each ruleset come from its first included base game in catalog order.
    public GenerationResult Generate(int players, IReadOnlyCollection<string> includedBoxes, IRandomSource random)
    {
        if (CheckBoxes(includedBoxes) is { } problem)
        {
            throw new ArgumentException(problem, nameof(includedBoxes));
        }

        var boxes = catalog.Boxes.Where(box => includedBoxes.Contains(box.Id)).ToList();
        var mixingBox = boxes.Select(box => box.Ruleset).Distinct().Count() > 1
            ? boxes.First(box => box.Setup?.Mixing is not null)
            : null;

        // One set of rules per ruleset that has an included base game. The mixing base game's rules draw from
        // every included box; any other ruleset's draw from its own boxes.
        var draws = boxes.Where(box => box.IsBaseGame)
            .GroupBy(box => box.Ruleset)
            .Select(rulesets =>
            {
                var rulesBox = rulesets.Key == mixingBox?.Ruleset ? mixingBox : rulesets.First();
                var pool = rulesBox == mixingBox ? boxes : boxes.Where(box => box.Ruleset == rulesets.Key).ToList();
                var rules = rulesBox.Setup!;
                var row = rules.PlayerCounts.SingleOrDefault(r => r.Players == players);
                if (players != 1 && row is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(players), players,
                        $"Supported player counts are 1 (Solo) and {string.Join(", ", rules.PlayerCounts.Select(r => r.Players))}.");
                }

                return new TableDraw(boxes, pool, rulesBox, mixingBox, rules, players, row, random);
            })
            .ToList();

        return Run(boxes, draws, players, random);
    }

    // Why a set of box ids can't be drawn from, or null when it can. A setup needs at least one base game for
    // its rules, and boxes of different rulesets need a base game that gives rules for mixing them.
    public string? CheckBoxes(IReadOnlyCollection<string> includedBoxes)
    {
        if (includedBoxes.FirstOrDefault(id => catalog.Boxes.All(box => box.Id != id)) is { } unknown)
        {
            return $"No box has id {unknown}.";
        }

        var included = catalog.Boxes.Where(box => includedBoxes.Contains(box.Id)).ToList();
        if (!included.Any(box => box.IsBaseGame))
        {
            return "Include at least one base game.";
        }

        return included.Select(box => box.Ruleset).Distinct().Count() > 1 && !included.Any(box => box.Setup?.Mixing is not null)
            ? $"{string.Join(" and ", included.Select(box => box.Name))} follow different rulesets, and no included base game has rules for mixing them."
            : null;
    }

    // Draws the Scheme from those some set of rules can complete, then the Mastermind from the completions
    // across every set of rules, in catalog order; the rest of the draw follows that pair's rules.
    private static GenerationResult Run(IReadOnlyList<Box> boxes, IReadOnlyList<TableDraw> draws, int players, IRandomSource random)
    {
        var masterminds = boxes.SelectMany(box => box.Masterminds).ToList();
        List<(TableDraw Draw, SetupPlan Plan)> Completions(Scheme scheme) => draws
            .SelectMany(draw => draw.Completions(scheme).Select(plan => (Draw: draw, Plan: plan)))
            .OrderBy(completion => masterminds.IndexOf(completion.Plan.Mastermind!))
            .ToList();

        var schemes = boxes.SelectMany(box => box.Schemes)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .Where(scheme => Completions(scheme).Count > 0)
            .ToList();
        if (schemes.Count == 0)
        {
            return new NoEligibleScheme(players);
        }

        var schemeDrawn = schemes[random.Next(schemes.Count)];
        var completions = Completions(schemeDrawn);
        var (draw, plan) = completions[random.Next(completions.Count)];
        return draw.Finish(plan);
    }

    // One set of rules a table draw can follow: the included boxes, the boxes it draws cards from (pool), the
    // base game whose rules they are, the mixing base game when the included boxes follow more than one
    // ruleset, and the random source. row is the player-count table row, or null in Solo.
    private sealed class TableDraw(
        IReadOnlyList<Box> boxes, IReadOnlyList<Box> pool, Box rulesBox, Box? mixingBox, SetupRules rules, int players, PlayerCountSetup? row,
        IRandomSource random)
    {
        private readonly List<VillainGroup> _villainGroups = pool.SelectMany(box => box.VillainGroups).ToList();
        private readonly List<HenchmanGroup> _henchmanGroups = pool.SelectMany(box => box.HenchmanGroups).ToList();
        private readonly List<Hero> _heroes = pool.SelectMany(box => box.Heroes).ToList();
        private readonly Dictionary<(string Scheme, int Heroes), bool> _heroesFit = [];

        // The words the ruleset's rulebook uses, for rule notes. A setup whose drawn cards follow both rulesets
        // switches to the mixed words once they are drawn.
        private RulesetTerms _terms = RulesetTerms.For(rulesBox.Ruleset);

        private bool Solo => row is null;

        private bool IgnoresAlwaysLeads => Solo && rules.Solo.IgnoresAlwaysLeads.Value;

        // The Scheme's completions under these rules: each Mastermind that pairs with it here and leaves a legal
        // setup, as the Scheme's plan with the Mastermind's effects added, since a Mastermind's setup effects
        // can ask for more cards. A pair follows these rules when both its cards are in the pool and at least
        // one is of this ruleset: under the mixing base game's rules a pair needs a Villainous card, and a pair
        // with none follows the other ruleset's rules, drawn from that ruleset's boxes alone.
        public List<SetupPlan> Completions(Scheme scheme)
        {
            if (!pool.Any(box => box.Schemes.Contains(scheme)))
            {
                return [];
            }

            // A Mastermind whose Always Leads group is not in the pool cannot be set up legally.
            var plan = Plan(scheme);
            return pool.SelectMany(box => box.Masterminds)
                .Where(mastermind => IgnoresAlwaysLeads || GroupIds().Contains(mastermind.AlwaysLeads.GroupId))
                .Where(mastermind => RulesetOf(scheme.Id) == rulesBox.Ruleset || RulesetOf(mastermind.Id) == rulesBox.Ruleset)
                .Select(mastermind => WithMastermind(plan, mastermind))
                .Where(Fits)
                .ToList();
        }

        // Draws the rest of the setup for a Scheme and Mastermind pair these rules complete.
        public SetupResult Finish(SetupPlan plan)
        {
            var mastermind = plan.Mastermind!;
            var slotNotes = new List<RuleNote>();

            var villainGroups = FillSlots(_villainGroups, g => g.Id, g => g.Name, GroupType.Villain, plan.VillainGroups, plan.Scheme, mastermind, slotNotes);
            var henchmanGroups = FillSlots(_henchmanGroups, g => g.Id, g => g.Name, GroupType.Henchman, plan.HenchmanGroups, plan.Scheme, mastermind, slotNotes);
            var outsideHenchmen = plan.OutsideHenchmen
                .Zip(DrawMany(_henchmanGroups.Except(henchmanGroups), plan.OutsideHenchmen.Count), (draw, group) => new OutsideHenchmanGroup(group, draw.To, draw.Cards))
                .ToList();
            var (heroes, outside) = DrawHeroes(plan);

            // The rulesets of the drawn cards decide which boxes' stacks are laid out, and whether the setup is mixed.
            var drawn = new[] { plan.Scheme.Id, mastermind.Id }
                .Concat(villainGroups.Select(group => group.Id))
                .Concat(henchmanGroups.Select(group => group.Id))
                .Concat(outsideHenchmen.Select(group => group.Group.Id))
                .Concat(heroes.Select(hero => hero.Id))
                .Concat(outside.Select(hero => hero.Hero.Id))
                .Select(RulesetOf)
                .ToHashSet();
            var stackBoxes = boxes.Where(box => drawn.Contains(box.Ruleset)).ToList();
            var mixed = drawn.Count > 1;

            // A mixed setup's notes name its parts in both rulesets' words, so its plan's notes are written again.
            var notes = new List<RuleNote>();
            if (mixed)
            {
                _terms = RulesetTerms.Mixed;
                notes.AddRange(WithMastermind(Plan(plan.Scheme), mastermind).Notes);
            }
            else
            {
                notes.AddRange(plan.Notes);
            }

            notes.AddRange(slotNotes);

            // A mixed setup draws from shared pools, lays out every stack, and lets the players choose between
            // the starting decks of the included base games (VIL p.21).
            IReadOnlyList<Ruleset>? choices = null;
            if (mixed)
            {
                var mixing = mixingBox!.Setup!.Mixing!;
                notes.Add(Note(
                    "Mixed sets: Schemes and Plots, Masterminds and Commanders, Heroes and Allies and the groups each come from one pool",
                    mixing.Pools, mixingBox));
                notes.Add(Note("Mixed sets: lay out every Wound, Bindings and recruit stack, and shuffle all Bystanders together", mixing.Stacks, mixingBox));
                var teams = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).Distinct().ToList();
                if (teams.Count > 1)
                {
                    choices = teams;
                    notes.Add(Note(
                        $"Mixed sets: the players choose {string.Join(" or ", teams.Select(RulesetTerms.StartingTeam))} starting decks",
                        mixing.StartingDeckChoice, mixingBox));
                }
            }

            if (Solo)
            {
                notes.AddRange(rules.Solo.PlayRules.Select(rule => Note($"Solo: {rule.Label}", rule.Source, rulesBox)));
            }

            // With boxes of more than one ruleset included, the result says which rules its cards gave it.
            RuleNote? reason = null;
            if (mixingBox is not null)
            {
                var source = mixingBox.Setup!.Mixing!.Rules;
                var side = RulesetTerms.Side(mixingBox.Ruleset);
                reason = new RuleNote(
                    drawn.Contains(mixingBox.Ruleset)
                        ? $"{RulesetTerms.RulesName(rulesBox.Ruleset)} rules: the setup includes {side} cards"
                        : $"{RulesetTerms.RulesName(rulesBox.Ruleset)} rules: the setup has no {side} cards",
                    source,
                    LinkOf(source, mixingBox));
            }

            // What the boxes of the drawn cards' rulesets hold between them, and a stack's size, or null when none
            // of them has that stack, so the setup leaves it out.
            int Supplied(Func<BoxComponents, Sourced<int>?> count) => stackBoxes.Sum(box => count(box.Components)?.Value ?? 0);
            int? Stack(Func<BoxComponents, Sourced<int>?> count, int size) =>
                stackBoxes.Any(box => count(box.Components) is not null) ? size : null;

            return new SetupResult(
                players,
                rulesBox.Ruleset,
                plan.Scheme,
                mastermind,
                villainGroups,
                henchmanGroups,
                heroes,
                new VillainDeck(
                    plan.Twists,
                    plan.MasterStrikes,
                    villainGroups.Sum(group => BoxOf(group.Id).Components.VillainGroupCards.Value),
                    henchmanGroups.Sum(group => plan.HenchmanCards ?? (Solo ? rules.Solo.HenchmanCards.Value : BoxOf(group.Id).Components.HenchmanGroupCards.Value)),
                    plan.Bystanders,
                    plan.MovedIn(Pile.VillainDeck),
                    plan.MovedOut(Pile.VillainDeck),
                    outside.Where(hero => hero.To == Pile.VillainDeck).Sum(hero => hero.Cards)),
                new HeroDeck(
                    heroes.Sum(hero => BoxOf(hero.Id).Components.HeroCards.Value),
                    plan.MovedOut(Pile.HeroDeck),
                    plan.MovedIn(Pile.HeroDeck),
                    outsideHenchmen.Where(outside => outside.To == Pile.HeroDeck).Sum(outside => outside.Cards)),
                plan.TwistsBeside,
                new SetupStacks(
                    Stack(components => components.Wounds, (plan.Wounds ?? Supplied(components => components.Wounds)) - plan.MovedOut(Pile.Wounds)),
                    Stack(components => components.Officers, Supplied(components => components.Officers) - plan.MovedOut(Pile.Officers)),
                    Supplied(components => components.Bystanders) - plan.Bystanders - plan.MovedOut(Pile.Bystanders),
                    Stack(components => components.Sidekicks, Supplied(components => components.Sidekicks) - plan.MovedOut(Pile.Sidekicks)),
                    Stack(components => components.Bindings, plan.Bindings ?? Supplied(components => components.Bindings)),
                    Stack(components => components.MadameHydra, Supplied(components => components.MadameHydra)),
                    Stack(components => components.NewRecruits, Supplied(components => components.NewRecruits))),
                new PlayerDeck(rules.StartingDeck.Agents.Value, rules.StartingDeck.Troopers.Value, choices),
                plan.Moves,
                outside,
                outsideHenchmen,
                plan.Steps,
                notes,
                boxes,
                mixed,
                reason);
        }

        // The counts one Scheme sets at this player count: the player-count table or Solo,
        // with the Scheme's Setup line applied on top, because printed card text wins.
        private SetupPlan Plan(Scheme scheme)
        {
            var effect = scheme.Setup;
            var notes = new List<RuleNote>();

            // A note the Scheme card itself causes, cited from the box that holds the card.
            var schemeBox = BoxOf(scheme.Id);
            RuleNote Card(string text, string source) => Note(text, source, schemeBox);

            // A Scheme value that replaces a Solo value cites the ruling that the Scheme wins;
            // otherwise the card itself is the source.
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

            int? henchmanCards = null;
            if (ForPlayers(effect.HenchmanCards ?? []) is { } schemeHenchmen)
            {
                henchmanCards = schemeHenchmen.Value;
                notes.Add(Replaces(
                    $"{henchmanCards} {_terms.Henchmen} of each {_terms.HenchmanGroup}",
                    $"puts {henchmanCards} {_terms.Henchmen} of each {_terms.HenchmanGroup} in the {_terms.VillainDeck}",
                    schemeHenchmen.Source));
            }

            var bystanders = Solo ? rules.Solo.Bystanders.Value : row!.Bystanders;
            if (effect.VillainDeckBystanders is { } schemeBystanders)
            {
                bystanders = schemeBystanders.Value;
                notes.Add(Replaces(
                    $"{bystanders} Bystanders in the {_terms.VillainDeck}", $"puts {bystanders} Bystanders in the {_terms.VillainDeck}", schemeBystanders.Source));
            }

            // The Wound and Bindings stacks hold what the boxes supply unless the Scheme sets their size.
            int? wounds = null;
            if (effect.WoundsPerPlayer is { } woundsPerPlayer)
            {
                wounds = woundsPerPlayer.Value * players;
                notes.Add(Card($"{schemeWord} sets the Wound stack to {woundsPerPlayer.Value} per player", woundsPerPlayer.Source));
            }

            int? bindings = null;
            if (effect.BindingsPerPlayer is { } bindingsPerPlayer)
            {
                bindings = bindingsPerPlayer.Value * players;
                notes.Add(Card($"{schemeWord} sets the Bindings stack to {bindingsPerPlayer.Value} per player", bindingsPerPlayer.Source));
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
                moves.Add(new MovedCards(move.Card, move.From(), move.To, each, total));
                notes.Add(Card(
                    $"{schemeWord} moves {each} {CardName(move.Card, each)} {Into(move.To)}{(move.PerPlayer ? $", {count.Value} per player" : "")}",
                    count.Source));
            }

            if (effect.TwistsBesideScheme is { } beside)
            {
                notes.Add(Card($"{schemeWord} puts {beside.Value} {_terms.Twists} beside it", beside.Source));
            }

            foreach (var required in effect.RequiredHeroes ?? [])
            {
                notes.Add(Card($"{schemeWord} requires {HeroNamed(required.HeroId)}", required.Source));
            }

            foreach (var count in effect.HeroCounts ?? [])
            {
                var (bound, value) = count.AtLeast is { } atLeast ? ("at least", atLeast) : ("exactly", count.Exactly!.Value);
                notes.Add(Card($"{schemeWord} requires {bound} {value} {HeroesOf(value, count.Team, count.HeroName)}", count.Source));
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
                    ? HeroNamed(heroId)
                    : $"{count.Value} extra {HeroesOf(count.Value, rule.Team, rule.HeroName)}";
                notes.Add(Card(
                    $"{schemeWord} draws {which} outside the {_terms.HeroDeck} and puts {(count.Value == 1 ? "its" : "their")} cards {Onto(rule.To)}",
                    count.Source));
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

            var twists = ForPlayers(effect.Twists)
                ?? throw new InvalidDataException($"{scheme.Id} has no Twist count for {players} players.");

            var plan = new SetupPlan(
                scheme,
                null,
                heroes,
                Solo ? rules.Solo.VillainGroups.Value : row!.VillainGroups,
                Solo ? rules.Solo.HenchmanGroups.Value : row!.HenchmanGroups,
                henchmanCards,
                twists.Value,
                effect.TwistsBesideScheme?.Value ?? 0,
                Solo ? rules.Solo.MasterStrikes.Value : rules.MasterStrikes.Value,
                bystanders,
                wounds,
                bindings,
                moves,
                outside,
                outsideHenchmen,
                [],
                notes);

            return Add(plan, effect, schemeWord, schemeBox);
        }

        // The Scheme's plan with the Mastermind's setup effects added after the Scheme's.
        private SetupPlan WithMastermind(SetupPlan plan, Mastermind mastermind)
        {
            plan = plan with { Mastermind = mastermind };
            return mastermind.Setup is { } effects ? Add(plan, effects, mastermind.Name, BoxOf(mastermind.Id)) : plan;
        }

        // Adds each effect that applies at this player count to the plan's counts, and each setup step to
        // its steps, with a note citing the card that prints it. An effect only adds to the count, so it
        // replaces no Solo value.
        private SetupPlan Add(SetupPlan plan, SetupEffects effects, string by, Box from)
        {
            var notes = new List<RuleNote>(plan.Notes);

            int Extra(IReadOnlyList<PlayerCountValue>? effect, string one, string many, string where = "")
            {
                if (ForPlayers(effect ?? []) is not { } extra)
                {
                    return 0;
                }

                notes.Add(Note($"{by} adds {extra.Value} {(extra.Value == 1 ? one : many)}{where}", extra.Source, from));
                return extra.Value;
            }

            string Step(SetupStep step)
            {
                notes.Add(Note($"{by} adds a setup step: {step.Label}", step.Source, from));
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

        // A Scheme, or a Scheme and Mastermind pair, the included cards cannot complete is dropped
        // before the draw rather than relaxed. The stacks it can count on are those of the boxes of the pair's
        // own rulesets, which every setup it completes lays out.
        private bool Fits(SetupPlan plan)
        {
            var sides = new[] { RulesetOf(plan.Scheme.Id), RulesetOf(plan.Mastermind!.Id) };
            int Supply(Func<BoxComponents, Sourced<int>?> count) =>
                pool.Where(box => sides.Contains(box.Ruleset)).Sum(box => count(box.Components)?.Value ?? 0);
            var required = plan.Scheme.Setup.RequiredGroups ?? [];
            int Slots(int slots, GroupType type) => Math.Max(slots, required.Count(group => group.GroupType == type));
            var henchmanGroupCards = _henchmanGroups.Select(group => BoxOf(group.Id).Components.HenchmanGroupCards.Value).DefaultIfEmpty(0).Min();

            // A Henchman Group drawn outside the Villain Deck is one the Villain Deck doesn't use.
            return required.All(group => GroupIds().Contains(group.GroupId))
                && Slots(plan.VillainGroups, GroupType.Villain) <= _villainGroups.Count
                && Slots(plan.HenchmanGroups, GroupType.Henchman) + plan.OutsideHenchmen.Count <= _henchmanGroups.Count
                && plan.OutsideHenchmen.All(draw => draw.Cards <= henchmanGroupCards)
                && HeroesFit(plan)
                && plan.MovedOut(Pile.HeroDeck) <= plan.Heroes * pool.Min(box => box.Components.HeroCards.Value)
                && plan.MovedOut(Pile.VillainDeck) <= plan.HenchmanGroups * (plan.HenchmanCards ?? (Solo
                    ? rules.Solo.HenchmanCards.Value
                    : henchmanGroupCards))
                && plan.Bystanders + plan.MovedOut(Pile.Bystanders) <= Supply(components => components.Bystanders)
                && plan.MovedOut(Pile.Wounds) <= (plan.Wounds ?? Supply(components => components.Wounds))
                && plan.MovedOut(Pile.Officers) <= Supply(components => components.Officers)
                && plan.MovedOut(Pile.Sidekicks) <= Supply(components => components.Sidekicks)
                && plan.Twists + plan.TwistsBeside <= Supply(components => components.SchemeTwists);
        }

        // Fills one group type's slots: the Scheme's required groups first, then the Always Leads
        // group if a slot is left, then draws for the rest. A required group keeps its slot over
        // the Always Leads group.
        private List<T> FillSlots<T>(
            List<T> pool, Func<T, string> id, Func<T, string> name, GroupType type, int slots,
            Scheme scheme, Mastermind mastermind, List<RuleNote> notes)
        {
            var chosen = new List<T>();
            foreach (var required in (scheme.Setup.RequiredGroups ?? []).Where(group => group.GroupType == type))
            {
                var group = pool.Single(g => id(g) == required.GroupId);
                chosen.Add(group);
                notes.Add(Note($"{SchemeWord(scheme)} requires {name(group)}", required.Source, BoxOf(scheme.Id)));
            }

            var leads = mastermind.AlwaysLeads;
            if (leads.GroupType == type)
            {
                if (IgnoresAlwaysLeads)
                {
                    notes.Add(Note($"Solo ignores {mastermind.Name}'s Always Leads", rules.Solo.IgnoresAlwaysLeads.Source, rulesBox));
                }
                else
                {
                    var group = pool.Single(g => id(g) == leads.GroupId);
                    if (!chosen.Contains(group) && chosen.Count >= slots)
                    {
                        notes.Add(Note(
                            $"{SchemeWord(scheme)} requires {string.Join(" and ", chosen.Select(name))}, so {mastermind.Name}'s Always Leads group {name(group)} is dropped",
                            rules.Rulings.RequiredGroupDisplacesAlwaysLeads, rulesBox));
                    }
                    else
                    {
                        if (!chosen.Contains(group))
                        {
                            chosen.Add(group);
                        }

                        notes.Add(Note($"{mastermind.Name} always leads {name(group)}", rules.Rulings.AlwaysLeadsFillsSlot, rulesBox));
                    }
                }
            }

            chosen.AddRange(DrawMany(pool.Except(chosen).ToList(), slots - chosen.Count));
            return chosen;
        }

        // Draws the Hero Deck's Heroes, the Scheme's required Heroes first, then each Hero the Scheme draws
        // outside the Hero Deck. Every draw picks from the Heroes that leave the rest of the setup's Heroes
        // drawable within the Scheme's Hero rules, so the draw never reaches a dead end.
        private (List<Hero> Deck, List<OutsideHero> Outside) DrawHeroes(SetupPlan plan)
        {
            var rules = HeroRulesOf(plan);
            var deck = RequiredHeroes(plan)!;
            while (deck.Count < rules.DeckSlots)
            {
                deck.Add(DrawOne(_heroes.Where(hero => rules.CanComplete([.. deck, hero], [])).ToList()));
            }

            var outside = new List<Hero>();
            while (outside.Count < rules.OutsideSlots.Count)
            {
                outside.Add(DrawOne(_heroes.Where(hero => rules.CanComplete(deck, [.. outside, hero])).ToList()));
            }

            return (deck, outside
                .Zip(rules.OutsideSlots, (hero, rule) => new OutsideHero(hero, rule.To, BoxOf(hero.Id).Components.HeroCards.Value))
                .ToList());
        }

        // Whether the included Heroes can meet the plan's Hero rules. Eligibility asks this for every Scheme
        // and Mastermind pair, but the answer depends only on the Scheme and its number of Hero slots, so
        // each is searched once per draw rather than once per Mastermind.
        private bool HeroesFit(SetupPlan plan)
        {
            var key = (plan.Scheme.Id, plan.Heroes);
            if (!_heroesFit.TryGetValue(key, out var fits))
            {
                fits = RequiredHeroes(plan) is { } required && HeroRulesOf(plan).CanComplete(required, []);
                _heroesFit[key] = fits;
            }

            return fits;
        }

        // The Scheme's required Heroes, or null when one is not in an included box.
        private List<Hero>? RequiredHeroes(SetupPlan plan)
        {
            var required = (plan.Scheme.Setup.RequiredHeroes ?? [])
                .Select(rule => _heroes.FirstOrDefault(hero => hero.Id == rule.HeroId))
                .ToList();
            return required.Contains(null) ? null : required.OfType<Hero>().ToList();
        }

        // Required Heroes fill Hero Deck slots, so a Scheme that requires more Heroes than it has slots
        // uses them all. Each of the Scheme's draws outside the Hero Deck takes one slot per Hero.
        private HeroRules HeroRulesOf(SetupPlan plan) => new(
            _heroes,
            Math.Max(plan.Heroes, plan.Scheme.Setup.RequiredHeroes?.Count ?? 0),
            plan.Scheme.Setup,
            plan.Outside.SelectMany(draw => Enumerable.Repeat(draw.Rule, draw.Count)).ToList());

        // "X-Men Heroes", "Jean Grey Hero" or "Heroes": what count Heroes of a team or a Hero Name are called.
        private string HeroesOf(int count, string? team, string? heroName)
        {
            var kind = team is null ? heroName : boxes.SelectMany(box => box.Glossary).FirstOrDefault(term => term.Id == team)?.Name ?? team;
            return $"{(kind is null ? "" : kind + " ")}{(count == 1 ? _terms.Hero : _terms.Heroes)}";
        }

        private string HeroNamed(string heroId) => _heroes.FirstOrDefault(hero => hero.Id == heroId)?.Name ?? heroId;

        private string Onto(Pile to) => to switch
        {
            Pile.VillainDeck => $"into the {_terms.VillainDeck}",
            Pile.BesideScheme => $"beside the {_terms.Scheme}",
            Pile.SetAside => "in a stack set aside",
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null),
        };

        private List<T> DrawMany<T>(IEnumerable<T> options, int count)
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

        private T DrawOne<T>(IReadOnlyList<T> options) => options[random.Next(options.Count)];

        private HashSet<string> GroupIds() =>
            _villainGroups.Select(g => g.Id).Concat(_henchmanGroups.Select(g => g.Id)).ToHashSet();

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
            _ => throw new ArgumentOutOfRangeException(nameof(card), card, null),
        };

        private string Into(Pile to) => to switch
        {
            Pile.VillainDeck => $"into the {_terms.VillainDeck}",
            Pile.HeroDeck => $"into the {_terms.HeroDeck}",
            Pile.BesideScheme => "beside it",
            Pile.StartingDecks => "into each starting deck",
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null),
        };

        private PlayerCountValue? ForPlayers(IReadOnlyList<PlayerCountValue> values) =>
            values.SingleOrDefault(value => value.Players?.Contains(players) ?? true);

        // Ids start with the declaring box's id, and box ids contain no underscore.
        private Box BoxOf(string id) => boxes.Single(box => id.StartsWith(box.Id + "_", StringComparison.Ordinal));

        private Ruleset RulesetOf(string id) => BoxOf(id).Ruleset;

        // What the Scheme's own ruleset calls it: a Villainous Scheme is a Plot.
        private string SchemeWord(Scheme scheme) => RulesetTerms.For(RulesetOf(scheme.Id)).Scheme;

        // A note cites a source key of the box whose rule it is, since each box lists its own sources.
        // Once more than one box is included, the note also names that box.
        private RuleNote Note(string text, string citation, Box from) =>
            new(text, citation, LinkOf(citation, from), boxes.Count > 1 ? from.Name : null);

        private static string? LinkOf(string citation, Box from) =>
            from.Sources.FirstOrDefault(source => source.Key == citation.Split(' ')[0])?.Url;
    }

    // The counts a setup uses, from the rules and the setup effects of its Scheme and, once one is
    // paired with it, its Mastermind. HenchmanCards is the Scheme's count of cards of each Henchman
    // Group, or null when the table or Solo sets it. OutsideHenchmen has one entry per Henchman Group
    // the Scheme draws outside the Villain Deck.
    private sealed record SetupPlan(
        Scheme Scheme,
        Mastermind? Mastermind,
        int Heroes,
        int VillainGroups,
        int HenchmanGroups,
        int? HenchmanCards,
        int Twists,
        int TwistsBeside,
        int MasterStrikes,
        int Bystanders,
        int? Wounds,
        int? Bindings,
        IReadOnlyList<MovedCards> Moves,
        IReadOnlyList<OutsideDraw> Outside,
        IReadOnlyList<HenchmenDraw> OutsideHenchmen,
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
}
