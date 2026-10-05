using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Draws a random legal setup the way players would at the table: the Scheme from those allowed
// at the player count, then a Mastermind that can complete it, then the groups they require, then
// the remaining Villain and Henchman Groups, then any Henchman Groups the Scheme draws outside the
// Villain Deck, then the Heroes, then any Heroes the Scheme draws outside the Hero Deck. Each draw goes
// through the IRandomSource and picks from the options left, in catalog order, so a fixed sequence of draws
// always gives the same setup.
//
// When the included boxes follow more than one ruleset, the Scheme and Mastermind decide which rules apply
// (D-mixed, #85, #88): a pair with a card of the mixing base game's ruleset (a Villains Plot or Commander)
// follows that base game's rules, and any other pair follows its own ruleset's base game. Every other card is
// drawn from every included box whatever the pair, so no Hero or group is locked out by it, and since the pair
// fixed the rules first, no later draw can change the rules the counts came from. A pair whose rules have no
// included base game can't be set up and is dropped before the draw.
//
// A Scheme can set cards of a group beside it whether or not the group is drawn (#84). Those cards fill no group
// slot (VIL p.17); a drawn group puts only what is left of it in the Villain Deck, and one with nothing left can't
// be drawn there.
//
// The setup lays out a stack only when its rules or a drawn card use it (D-uses, #87). A card that uses a stack
// no included box of its own ruleset supplies can't be set up, so it is dropped before the draw.
//
// An expansion can be played without a base game of its own ruleset when its box says how (OtherRuleset, D-heroic,
// #110): its Schemes and Masterminds then follow the included base game's rules, and each part it uses that no
// included box supplies is replaced by its stand-in, as Fear Itself's Bindings become Wounds (FI p.2).
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
            ? boxes.FirstOrDefault(box => box.Setup?.Mixing is not null)
            : null;

        // The stand-ins of the parts no included box supplies, from the first included box that gives one.
        var standIns = boxes
            .SelectMany(box => (box.OtherRuleset?.StandIns ?? []).Select(standIn => new ActiveStandIn(standIn, box)))
            .Where(active => !boxes.Any(box => box.Components.Supplies(active.Rule.Part)?.Value > 0))
            .DistinctBy(active => active.Rule.Part)
            .ToDictionary(active => active.Rule.Part);
        var baseRulesets = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).ToHashSet();

        // One set of rules per ruleset that has an included base game, each drawing from every included box.
        var draws = boxes.Where(box => box.IsBaseGame)
            .GroupBy(box => box.Ruleset)
            .Select(rulesets =>
            {
                var rulesBox = rulesets.Key == mixingBox?.Ruleset ? mixingBox : rulesets.First();
                var rules = rulesBox.Setup!;
                var row = rules.PlayerCounts.SingleOrDefault(r => r.Players == players);
                if (players != 1 && row is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(players), players,
                        $"Supported player counts are 1 (Solo) and {string.Join(", ", rules.PlayerCounts.Select(r => r.Players))}.");
                }

                return new TableDraw(boxes, rulesBox, mixingBox, baseRulesets, standIns, rules, players, row, random);
            })
            .ToList();

        return Run(boxes, draws, players, random);
    }

    // Why a set of box ids can't be drawn from, or null when it can. A setup needs at least one base game for
    // its rules, and boxes of different rulesets need a base game that gives rules for mixing them, unless the base
    // games are of one ruleset and every box of the other can be played under it (D-heroic).
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

        var baseRulesets = included.Where(box => box.IsBaseGame).Select(box => box.Ruleset).ToHashSet();
        return included.Select(box => box.Ruleset).Distinct().Count() > 1
            && !included.Any(box => box.Setup?.Mixing is not null)
            && (baseRulesets.Count > 1 || included.Any(box => !baseRulesets.Contains(box.Ruleset) && box.OtherRuleset is null))
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

    // One set of rules a table draw can follow: the included boxes, which it draws every card from, the base
    // game whose rules they are, the mixing base game when the included boxes follow more than one ruleset and
    // one of them has mixing rules, the rulesets of the included base games, the stand-ins of the parts no
    // included box supplies, and the random source. row is the player-count table row, or null in Solo.
    private sealed class TableDraw(
        IReadOnlyList<Box> boxes, Box rulesBox, Box? mixingBox, IReadOnlySet<Ruleset> baseRulesets,
        IReadOnlyDictionary<Part, ActiveStandIn> standIns, SetupRules rules, int players, PlayerCountSetup? row,
        IRandomSource random)
    {
        private readonly List<VillainGroup> _villainGroups = boxes.SelectMany(box => box.VillainGroups).Where(group => Supplied(boxes, standIns, group)).ToList();
        private readonly List<HenchmanGroup> _henchmanGroups = boxes.SelectMany(box => box.HenchmanGroups).Where(group => Supplied(boxes, standIns, group)).ToList();
        private readonly List<Hero> _heroes = boxes.SelectMany(box => box.Heroes).Where(hero => Supplied(boxes, standIns, hero)).ToList();
        private readonly Dictionary<(string Scheme, int Heroes), bool> _heroesFit = [];

        // The words the ruleset's rulebook uses, for rule notes. A setup whose drawn cards follow both rulesets
        // switches to the mixed words once they are drawn.
        private RulesetTerms _terms = RulesetTerms.For(rulesBox.Ruleset);

        private bool Solo => row is null;

        private bool IgnoresAlwaysLeads => Solo && rules.Solo.IgnoresAlwaysLeads.Value;

        // The Scheme's completions under these rules: each Mastermind that pairs with it here and leaves a legal
        // setup, as the Scheme's plan with the Mastermind's effects added, since a Mastermind's setup effects
        // can ask for more cards. A pair is completed here only when these are the rules it follows.
        public List<SetupPlan> Completions(Scheme scheme)
        {
            if (!Supplied(boxes, standIns, scheme))
            {
                return [];
            }

            // A Mastermind whose Always Leads group is not included cannot be set up legally.
            var plan = Plan(scheme);
            return boxes.SelectMany(box => box.Masterminds)
                .Where(mastermind => Supplied(boxes, standIns, mastermind))
                .Where(mastermind => IgnoresAlwaysLeads || GroupIds().Contains(mastermind.AlwaysLeads.GroupId))
                .Where(mastermind => RulesOf(scheme, mastermind) == rulesBox.Ruleset)
                .Select(mastermind => WithMastermind(plan, mastermind))
                .Where(Fits)
                .ToList();
        }

        // Draws the rest of the setup for a Scheme and Mastermind pair these rules complete.
        public SetupResult Finish(SetupPlan plan)
        {
            var mastermind = plan.Mastermind!;
            var slotNotes = new List<RuleNote>();

            // A group the Scheme sets every card of beside it has none left for the Villain Deck, so it isn't drawn there.
            var villainGroups = FillSlots(
                _villainGroups.Where(g => DeckCards(plan, g.Id, GroupType.Villain) > 0).ToList(), g => g.Id, g => g.Name, GroupType.Villain,
                plan.VillainGroups, plan.Scheme, mastermind, slotNotes);
            var henchmanGroups = FillSlots(
                _henchmanGroups.Where(g => DeckCards(plan, g.Id, GroupType.Henchman) > 0).ToList(), g => g.Id, g => g.Name, GroupType.Henchman,
                plan.HenchmanGroups, plan.Scheme, mastermind, slotNotes);
            // Each Henchman Group drawn outside the Villain Deck is one the Villain Deck doesn't use (D-readings).
            var outsideHenchmen = plan.OutsideHenchmen
                .Zip(DrawMany(_henchmanGroups.Except(henchmanGroups), plan.OutsideHenchmen.Count), (draw, group) => new OutsideHenchmanGroup(group, draw.To, draw.Cards))
                .ToList();
            var (heroes, outside) = DrawHeroes(plan);

            // The cards of each group the Scheme sets beside it, and how many fewer the group puts in the Villain
            // Deck when it is drawn there too.
            var inDeck = villainGroups.Select(g => g.Id).Concat(henchmanGroups.Select(g => g.Id)).ToHashSet();
            var beside = plan.Beside
                .Select(draw =>
                {
                    var (id, type) = (draw.Rule.GroupId, draw.Rule.GroupType);
                    var fromDeck = inDeck.Contains(id) ? ShareOfDeck(plan, id, type) - DeckCards(plan, id, type) : 0;
                    return new GroupCardsBeside(GroupOf(id), draw.Rule.Card, draw.Count, fromDeck);
                })
                .ToList();

            // The rulesets of the drawn cards decide which boxes' stacks are laid out, and whether the setup is mixed.
            // The groups whose cards sit beside the Scheme count among them.
            ICard[] cards =
            [
                plan.Scheme, mastermind, .. villainGroups, .. henchmanGroups, .. outsideHenchmen.Select(group => group.Group),
                .. heroes, .. outside.Select(hero => hero.Hero), .. beside.Select(cards => cards.Group),
            ];
            var drawn = cards.Select(card => RulesetOf(card.Id)).ToHashSet();
            var mixed = drawn.Count > 1;

            // The rules' own ruleset lays out its stacks too, which a pair played under another ruleset's base game
            // (D-heroic) doesn't otherwise bring.
            var stackBoxes = boxes.Where(box => drawn.Contains(box.Ruleset) || box.Ruleset == rulesBox.Ruleset).ToList();

            // The parts the setup lays out: those its rules use, and those its drawn cards use (D-uses). Under the
            // mixing base game's rules a mixed setup's rules use the recruit stacks of every included base game
            // (VIL p.21); under the other rules its cards of the mixing ruleset bring only what they use (#88).
            var mixingRules = rulesBox == mixingBox;
            var rulesUse = mixed && mixingRules ? boxes.Where(box => box.IsBaseGame).SelectMany(box => box.Setup!.Uses) : rules.Uses;
            // A part no included box supplies is replaced by its stand-in (D-heroic, FI p.2).
            var cardParts = cards.SelectMany(card => card.Parts).ToHashSet();
            var used = rulesUse.Select(use => use.Part).Concat(cardParts.Select(StandInFor).OfType<Part>()).ToHashSet();
            var stoodIn = standIns.Values.Where(active => cardParts.Contains(active.Rule.Part)).ToList();

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
                var teams = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).Distinct().ToList();
                if (mixingRules && teams.Count > 1)
                {
                    choices = teams;
                    notes.Add(Note(
                        $"Mixed sets: the players choose {string.Join(" or ", teams.Select(RulesetTerms.StartingTeam))} starting decks",
                        mixing.StartingDeckChoice, mixingBox));
                }
            }

            // A stack the boxes of the drawn cards' rulesets supply but nothing in the setup uses is left out,
            // Wounds included (D-readings).
            var leftOut = Enum.GetValues<Part>()
                .Where(part => !used.Contains(part) && stackBoxes.Any(box => box.Components.Supplies(part) is not null))
                .ToList();
            if (leftOut.Count > 0)
            {
                notes.Add(Note(
                    $"Leave out the {string.Join(" and ", leftOut.Select(StackName))} {(leftOut.Count == 1 ? "stack: no drawn card uses it" : "stacks: no drawn card uses them")}",
                    rules.Rulings.UnusedPartsLeftOut, rulesBox));
            }

            if (Solo)
            {
                notes.AddRange(rules.Solo.PlayRules.Select(rule => Note($"Solo: {rule.Label}", rule.Source, rulesBox)));
            }

            // With boxes of more than one ruleset included, the result names the card that decided its rules
            // ("Villains rules: the Commander is Dr. Strange"), or says the pair has no card of the mixing ruleset.
            // Without a mixing base game, every card plays under the one included base game's rules (D-heroic).
            RuleNote? reason = null;
            if (boxes.Any(box => !baseRulesets.Contains(box.Ruleset)) && mixingBox is null)
            {
                var other = boxes.First(box => !baseRulesets.Contains(box.Ruleset));
                var source = other.OtherRuleset!.Source;
                reason = new RuleNote(
                    $"{RulesetTerms.RulesName(rulesBox.Ruleset)} rules: no {RulesetTerms.Side(other.Ruleset)} base game is included",
                    source,
                    LinkOf(source, other));
            }
            else if (mixingBox is not null)
            {
                var source = mixingBox.Setup!.Mixing!.Rules;
                var words = RulesetTerms.For(mixingBox.Ruleset);
                var deciders = new List<string>();
                if (RulesetOf(plan.Scheme.Id) == mixingBox.Ruleset)
                {
                    deciders.Add($"the {words.Scheme} is {plan.Scheme.Name}");
                }

                if (RulesetOf(mastermind.Id) == mixingBox.Ruleset)
                {
                    deciders.Add($"the {words.Mastermind} is {mastermind.Name}");
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
            int Supply(Part part) => stackBoxes.Sum(box => box.Components.Supplies(part)?.Value ?? 0);
            int? Stack(Part part, int size) => used.Contains(part) ? size : null;

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
                    villainGroups.Sum(group => ShareOfDeck(plan, group.Id, GroupType.Villain)),
                    henchmanGroups.Sum(group => ShareOfDeck(plan, group.Id, GroupType.Henchman)),
                    plan.Bystanders,
                    plan.MovedIn(Pile.VillainDeck),
                    plan.MovedOut(Pile.VillainDeck),
                    outside.Where(hero => hero.To == Pile.VillainDeck).Sum(hero => hero.Cards),
                    beside.Sum(cards => cards.FromVillainDeck)),
                new HeroDeck(
                    heroes.Sum(hero => BoxOf(hero.Id).Components.HeroCards.Value),
                    plan.MovedOut(Pile.HeroDeck),
                    plan.MovedIn(Pile.HeroDeck),
                    outsideHenchmen.Where(outside => outside.To == Pile.HeroDeck).Sum(outside => outside.Cards)),
                plan.TwistsBeside,
                new SetupStacks(
                    Stack(Part.Wounds, (plan.Wounds ?? Supply(Part.Wounds)) - plan.MovedOut(Pile.Wounds)),
                    Stack(Part.Officers, Supply(Part.Officers) - plan.MovedOut(Pile.Officers)),
                    stackBoxes.Sum(box => box.Components.Bystanders?.Value ?? 0) - plan.Bystanders - plan.MovedOut(Pile.Bystanders),
                    Stack(Part.Sidekicks, Supply(Part.Sidekicks) - plan.MovedOut(Pile.Sidekicks)),
                    Stack(Part.Bindings, (plan.Bindings ?? Supply(Part.Bindings)) - plan.MovedOut(Pile.Bindings)),
                    Stack(Part.MadameHydra, Supply(Part.MadameHydra)),
                    Stack(Part.NewRecruits, Supply(Part.NewRecruits)),
                    Stack(Part.Shards, Supply(Part.Shards))),
                new PlayerDeck(rules.StartingDeck.Agents.Value, rules.StartingDeck.Troopers.Value, choices),
                plan.Moves,
                outside,
                outsideHenchmen,
                beside,
                plan.Steps,
                notes,
                boxes,
                mixed,
                reason,
                stoodIn.Select(active => active.Rule).ToList());
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

            // The Wound and Bindings stacks hold what the boxes supply unless the Scheme sets their size. A Scheme
            // that sets one stack's size sets that stack only.
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

                // A card whose stack no included box supplies comes from its stand-in's (D-heroic, FI p.2).
                var card = CardMove.PartOf(move.Card) is { } part && StandInFor(part) is { } standIn && CardMove.KindOf(standIn) is { } kind
                    ? move with { Card = kind }
                    : move;
                moves.Add(new MovedCards(card.Card, card.From(), card.To, each, total));
                notes.Add(Card(
                    $"{schemeWord} moves {each} {CardName(card.Card, each)} {Into(card.To)}{(card.PerPlayer ? $", {count.Value} per player" : "")}",
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

            var cardsBeside = new List<BesideDraw>();
            foreach (var rule in effect.CardsBeside ?? [])
            {
                if (ForPlayers(rule.Count) is not { } count)
                {
                    continue;
                }

                var each = rule.PerPlayer ? count.Value * players : count.Value;
                cardsBeside.Add(new BesideDraw(rule, each));
                var group = GroupIds().Contains(rule.GroupId) ? GroupOf(rule.GroupId).Name : rule.GroupId;
                var what = rule.Card is { } card ? $"{card} of {group}" : $"{each} {group}";
                notes.Add(Card($"{schemeWord} sets {what} beside it{(rule.PerPlayer ? $", {count.Value} per player" : "")}", count.Source));
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
                cardsBeside,
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
        // replaces no Solo value: a Scheme's "+N Heroes" applies on top of the Solo count (D-readings).
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
            var sides = new[] { RulesetOf(plan.Scheme.Id), RulesetOf(plan.Mastermind!.Id), rulesBox.Ruleset };
            int Supply(Func<BoxComponents, Sourced<int>?> count) =>
                boxes.Where(box => sides.Contains(box.Ruleset)).Sum(box => count(box.Components)?.Value ?? 0);
            var required = plan.Scheme.Setup.RequiredGroups ?? [];
            int Slots(int slots, GroupType type) => Math.Max(slots, required.Count(group => group.GroupType == type));
            var henchmanGroupCards = _henchmanGroups.Select(group => BoxOf(group.Id).Components.HenchmanGroupCards.Value).DefaultIfEmpty(0).Min();

            // The Scheme sets cards beside it from included groups only, never more than a group holds. A group it
            // sets every card of beside it can't fill a slot in the Villain Deck, so a Scheme that requires that
            // group, or a Mastermind that always leads it, is dropped.
            bool InDeck(string id, GroupType type) => GroupIds().Contains(id) && DeckCards(plan, id, type) > 0;
            int Drawable(IEnumerable<string> ids, GroupType type) => ids.Count(id => InDeck(id, type));
            var leads = plan.Mastermind.AlwaysLeads;

            // A Henchman Group drawn outside the Villain Deck is one the Villain Deck doesn't use (D-readings).
            return required.All(group => InDeck(group.GroupId, group.GroupType))
                && (IgnoresAlwaysLeads || InDeck(leads.GroupId, leads.GroupType))
                && plan.Beside.All(draw => GroupIds().Contains(draw.Rule.GroupId)
                    && Beside(plan, draw.Rule.GroupId) <= GroupCards(draw.Rule.GroupId, draw.Rule.GroupType))
                && Slots(plan.VillainGroups, GroupType.Villain) <= Drawable(_villainGroups.Select(g => g.Id), GroupType.Villain)
                && Slots(plan.HenchmanGroups, GroupType.Henchman) + plan.OutsideHenchmen.Count
                    <= Drawable(_henchmanGroups.Select(g => g.Id), GroupType.Henchman)
                && plan.OutsideHenchmen.All(draw => draw.Cards <= henchmanGroupCards)
                && HeroesFit(plan)
                && plan.MovedOut(Pile.HeroDeck) <= plan.Heroes * boxes.Min(box => box.Components.HeroCards.Value)
                && plan.MovedOut(Pile.VillainDeck) <= plan.HenchmanGroups * (plan.HenchmanCards ?? (Solo
                    ? rules.Solo.HenchmanCards.Value
                    : henchmanGroupCards))
                && plan.Bystanders + plan.MovedOut(Pile.Bystanders) <= Supply(components => components.Bystanders)
                && plan.MovedOut(Pile.Wounds) <= (plan.Wounds ?? Supply(components => components.Wounds))
                && plan.MovedOut(Pile.Officers) <= Supply(components => components.Officers)
                && plan.MovedOut(Pile.Sidekicks) <= Supply(components => components.Sidekicks)
                && plan.MovedOut(Pile.Bindings) <= (plan.Bindings ?? Supply(components => components.Bindings))
                && plan.Twists + plan.TwistsBeside + plan.MovedOut(Pile.Twists) <= Supply(components => components.SchemeTwists);
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

        // How many cards a group puts in the Villain Deck when the Scheme sets none of them beside it: all of a
        // Villain Group, and of a Henchman Group the Scheme's count, Solo's, or all of it.
        private int ShareOfDeck(SetupPlan plan, string groupId, GroupType type) => type == GroupType.Villain
            ? GroupCards(groupId, type)
            : plan.HenchmanCards ?? (Solo ? rules.Solo.HenchmanCards.Value : GroupCards(groupId, type));

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

        // An included Villain or Henchman Group.
        private ICard GroupOf(string groupId) =>
            _villainGroups.Cast<ICard>().Concat(_henchmanGroups).Single(group => group.Id == groupId);

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
            (CardKind.Binding, true) => "Binding",
            (CardKind.Binding, false) => "Bindings",
            (CardKind.Twist, true) => _terms.Twist,
            (CardKind.Twist, false) => _terms.Twists,
            _ => throw new ArgumentOutOfRangeException(nameof(card), card, null),
        };

        private string Into(Pile to) => to switch
        {
            Pile.VillainDeck => $"into the {_terms.VillainDeck}",
            Pile.HeroDeck => $"into the {_terms.HeroDeck}",
            Pile.BesideScheme => "beside it",
            Pile.StartingDecks => "into each starting deck",
            Pile.SetAside => "into a stack set aside",
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null),
        };

        private PlayerCountValue? ForPlayers(IReadOnlyList<PlayerCountValue> values) =>
            values.SingleOrDefault(value => value.Players?.Contains(players) ?? true);

        // Ids start with the declaring box's id, and box ids contain no underscore.
        private Box BoxOf(string id) => boxes.Single(box => id.StartsWith(box.Id + "_", StringComparison.Ordinal));

        private Ruleset RulesetOf(string id) => BoxOf(id).Ruleset;

        // The ruleset whose rules a Scheme and Mastermind pair follows (D-mixed, #88): the mixing base game's when
        // either card is of its ruleset, so a Villains Plot or Commander means the Villains rules; otherwise the
        // pair's own, since only two rulesets exist and a pair with no card of the mixing one shares the other.
        // A pair of a ruleset with no included base game follows the included base game's rules when the boxes of
        // its cards of that ruleset can be played under another's (D-heroic), and is otherwise dropped.
        private Ruleset RulesOf(Scheme scheme, Mastermind mastermind)
        {
            var ruleset = mixingBox is not null && RulesetOf(mastermind.Id) == mixingBox.Ruleset ? mixingBox.Ruleset : RulesetOf(scheme.Id);
            var pair = new[] { BoxOf(scheme.Id), BoxOf(mastermind.Id) }.Where(box => box.Ruleset == ruleset);
            return baseRulesets.Contains(ruleset) || pair.Any(box => box.OtherRuleset is null) ? ruleset : baseRulesets.Single();
        }

        // The part a setup lays out for a card that uses this one: the part itself, its stand-in when no included box
        // supplies it, or null when the stand-in needs no part.
        private Part? StandInFor(Part part) => standIns.TryGetValue(part, out var active) ? active.Rule.With : part;

        // What the Scheme's own ruleset calls it: a Villainous Scheme is a Plot.
        private string SchemeWord(Scheme scheme) => RulesetTerms.For(RulesetOf(scheme.Id)).Scheme;

        // A note cites a source key of the box whose rule it is, since each box lists its own sources.
        // Once more than one box is included, the note also names that box.
        private RuleNote Note(string text, string citation, Box from) =>
            new(text, citation, LinkOf(citation, from), boxes.Count > 1 ? from.Name : null);

        private static string? LinkOf(string citation, Box from) =>
            from.Sources.FirstOrDefault(source => source.Key == citation.Split(' ')[0])?.Url;
    }

    // A card is set up only when an included box of its own ruleset supplies every part it uses, or the part has a
    // stand-in some included box supplies, or one that needs no part (D-heroic). Those boxes' stacks are laid out in
    // every setup that draws the card.
    private static bool Supplied(IReadOnlyList<Box> boxes, IReadOnlyDictionary<Part, ActiveStandIn> standIns, ICard card)
    {
        var ruleset = boxes.Single(box => card.Id.StartsWith(box.Id + "_", StringComparison.Ordinal)).Ruleset;
        return card.Parts.All(part => standIns.TryGetValue(part, out var active)
            ? active.Rule.With is not { } with || boxes.Any(box => box.Components.Supplies(with)?.Value > 0)
            : boxes.Any(box => box.Ruleset == ruleset && box.Components.Supplies(part)?.Value > 0));
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
        _ => throw new ArgumentOutOfRangeException(nameof(part), part, null),
    };

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
        IReadOnlyList<BesideDraw> Beside,
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
}
