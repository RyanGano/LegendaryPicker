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
// included box supplies is replaced by its stand-in, as Fear Itself's Bindings become Wounds (FI p.2).
//
// Every count and rule comes from the box data; nothing here compares a card name.
public sealed class SetupGenerator(BoxCatalog catalog)
{
    public const string CoreBoxId = "core";

    // The source key of the owner decision that every requirement is met, from another box when need be (#138).
    private const string SchemeFirst = "D-scheme-first";

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

                return new TableDraw(catalog.Boxes, boxes, rulesBox, mixingBox, baseRulesets, standIns, rules, players, row, random);
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

    // Draws the Scheme from every included Scheme its card allows at the player count, then the Mastermind from the
    // included ones the Scheme doesn't exclude and some set of rules covers, in catalog order; the rest of the draw
    // follows that pair's rules.
    private static GenerationResult Run(IReadOnlyList<Box> boxes, IReadOnlyList<TableDraw> draws, int players, IRandomSource random)
    {
        var schemes = boxes.SelectMany(box => box.AllSchemes).DistinctBy(scheme => scheme.Id)
            .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
            .ToList();
        var masterminds = draws[0].Masterminds;
        if (schemes.Count == 0 || masterminds.Count == 0)
        {
            return new NoEligibleScheme(players);
        }

        var scheme = schemes[random.Next(schemes.Count)];
        var excluded = (scheme.ExcludesMasterminds ?? []).Select(exclusion => exclusion.MastermindId).ToHashSet();
        var pairs = masterminds
            .Where(mastermind => !excluded.Contains(mastermind.Id))
            .Select(mastermind => (Mastermind: mastermind, Draw: draws.FirstOrDefault(draw => draw.Follows(scheme, mastermind))))
            .Where(pair => pair.Draw is not null)
            .ToList();
        var (drawn, draw) = pairs[random.Next(pairs.Count)];
        return draw!.Finish(scheme, drawn);
    }

    // One set of rules a table draw can follow: every loaded box, which holds the cards a Scheme requires from a box
    // the setup doesn't include, the included boxes, which it draws every other card from, the base game whose rules
    // they are, the mixing base game when the included boxes follow more than one ruleset and
    // one of them has mixing rules, the rulesets of the included base games, the stand-ins of the parts no
    // included box supplies, and the random source. row is the player-count table row, or null in Solo.
    private sealed class TableDraw(
        IReadOnlyList<Box> catalogBoxes, IReadOnlyList<Box> boxes, Box rulesBox, Box? mixingBox, IReadOnlySet<Ruleset> baseRulesets,
        IReadOnlyDictionary<Part, ActiveStandIn> standIns, SetupRules rules, int players, PlayerCountSetup? row,
        IRandomSource random)
    {
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

        // The included Masterminds a setup can draw: those whose parts the included boxes supply.
        public IReadOnlyList<Mastermind> Masterminds => _masterminds;

        // Whether a Scheme and Mastermind pair follows these rules.
        public bool Follows(Scheme scheme, Mastermind mastermind) => RulesOf(scheme, mastermind) == rulesBox.Ruleset;

        // Draws the rest of the setup for a Scheme and Mastermind pair that follows these rules.
        public SetupResult Finish(Scheme scheme, Mastermind mastermind)
        {
            var plan = WithMastermind(Plan(scheme), mastermind);
            var slotNotes = new List<RuleNote>();

            // A Mastermind the Scheme sets aside and whose Always Leads group it adds (Symbiotic Absorption) is drawn
            // before the groups, since its group is one more of them.
            var drainedDraws = plan.OutsideMasterminds.Where(draw => draw.Rule.BringsAlwaysLeads?.Value == true).ToList();
            // Its group differs from the main Mastermind's, and the Scheme's card doesn't rule it out.
            var ruledOut = (scheme.ExcludesMasterminds ?? []).Select(exclusion => exclusion.MastermindId).ToHashSet();
            var drained = new Queue<Mastermind>(DrawMany(
                _masterminds.Where(other => other != mastermind && other.AlwaysLeads.GroupId != mastermind.AlwaysLeads.GroupId && !ruledOut.Contains(other.Id)),
                drainedDraws.Sum(draw => draw.Count)));
            var drainedLeads = drainedDraws
                .SelectMany(draw => Enumerable.Repeat(draw.Rule.BringsAlwaysLeads!.Source, draw.Count))
                .Select(source => (other: drained.Dequeue(), source))
                .ToList();
            plan = plan with
            {
                VillainGroups = plan.VillainGroups + drainedLeads.Count(entry => entry.other.AlwaysLeads.GroupType == GroupType.Villain),
                HenchmanGroups = plan.HenchmanGroups + drainedLeads.Count(entry => entry.other.AlwaysLeads.GroupType == GroupType.Henchman),
            };

            // A group the Scheme sets every card of beside it has none left for the Villain Deck, so it isn't drawn there.
            // A required group whose card gives a substitute is replaced by the group drawn for it.
            var required = new Dictionary<string, RequiredGroup>(plan.Required);
            var villainGroups = FillSlots(
                _villainGroups.Where(g => DeckCards(plan, g.Id, GroupType.Villain) > 0).ToList(), GroupType.Villain,
                plan.VillainGroups, plan.Scheme, mastermind, required, slotNotes, drainedLeads);
            var henchmanGroups = FillSlots(
                _henchmanGroups.Where(g => DeckCards(plan, g.Id, GroupType.Henchman) > 0).ToList(), GroupType.Henchman,
                plan.HenchmanGroups, plan.Scheme, mastermind, required, slotNotes, drainedLeads);
            plan = plan with { Required = required };
            var extraHenchmen = ExtraHenchmanGroups(plan, henchmanGroups.Select(g => g.Id).ToList());
            // Each Henchman Group drawn outside the Villain Deck is one the Villain Deck doesn't use (D-readings).
            var outsideHenchmen = plan.OutsideHenchmen
                .Zip(DrawMany(_henchmanGroups.Except(henchmanGroups), plan.OutsideHenchmen.Count), (draw, group) => new OutsideHenchmanGroup(group, draw.To, draw.Cards))
                .ToList();
            var (heroes, outside) = DrawHeroes(plan);

            // The Masterminds the Scheme draws besides its own, from the included ones the setup doesn't otherwise use.
            var drawnDrained = drainedLeads.Select(entry => entry.other).ToList();
            var otherMasterminds = new Queue<Mastermind>(DrawMany(
                _masterminds.Where(other => other != mastermind && !drawnDrained.Contains(other)),
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
                plan.Scheme, mastermind, .. villainGroups, .. henchmanGroups, .. outsideHenchmen.Select(group => group.Group),
                .. heroes, .. outside.Select(hero => hero.Hero), .. beside.Select(cards => cards.Group),
                .. outsideMasterminds.Select(other => other.Mastermind),
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
            // Tactics shuffled into the Villain Deck play with no abilities, so their Mastermind brings no parts (#113).
            // Every setup shuffles all the included Bystanders together, so special Bystanders bring their parts too (#125).
            var cardParts = cards
                .Except(outsideMasterminds.Where(other => other.To == Pile.VillainDeck).Select(other => other.Mastermind))
                .SelectMany(card => card.Parts)
                .Concat(stackBoxes.Where(box => box.Components.Bystanders?.Value > 0).SelectMany(box => box.BystanderUses ?? []).Select(use => use.Part))
                .ToHashSet();
            var used = rulesUse.Select(use => use.Part).Concat(cardParts.Select(StandInFor).OfType<Part>()).ToHashSet();
            var stoodIn = standIns.Values.Where(active => cardParts.Contains(active.Rule.Part)).ToList();

            // A part only a required card uses can be one no included box supplies: the first loaded box that has it,
            // a base game first, lays it out, as the player owns it (D-scheme-first, #138).
            var borrowed = used
                .Where(part => !stackBoxes.Any(box => box.Components.Supplies(part)?.Value > 0))
                .ToDictionary(part => part, part => catalogBoxes.OrderByDescending(box => box.IsBaseGame).First(box => box.Components.Supplies(part)?.Value > 0));

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

            foreach (var (part, from) in borrowed)
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
                var teams = boxes.Where(box => box.IsBaseGame).Select(box => box.Ruleset).Distinct().ToList();
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
                var withTokens = cards.Where(card => tokens.Value.Contains(card.Id)).Select(card => card.Name).Distinct().ToList();
                if (withTokens.Count > 0)
                {
                    notes.Add(Note($"Optional: the Token cards could be used with {string.Join(" and ", withTokens)}", tokens.Source, box));
                }
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
            int Supply(Part part) => borrowed.TryGetValue(part, out var from)
                ? from.Components.Supplies(part)!.Value
                : stackBoxes.Sum(box => box.Components.Supplies(part)?.Value ?? 0);
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
                    henchmanGroups.Sum(group => extraHenchmen.Contains(group.Id)
                        ? plan.Scheme.Setup.ExtraHenchmanCards!.Value
                        : ShareOfDeck(plan, group.Id, GroupType.Henchman)),
                    plan.Bystanders,
                    plan.MovedIn(Pile.VillainDeck),
                    plan.MovedOut(Pile.VillainDeck),
                    outside.Where(hero => hero.To == Pile.VillainDeck).Sum(hero => hero.Cards),
                    beside.Sum(cards => cards.FromVillainDeck),
                    outsideMasterminds.Sum(other => other.Tactics ?? 0),
                    plan.Scheme.Setup.OwnTactics?.Value ?? 0),
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
                    Stack(Part.Shards, Supply(Part.Shards)),
                    Stack(Part.Horrors, Supply(Part.Horrors))),
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
                stoodIn.Select(active => active.Rule).ToList(),
                outsideMasterminds);
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

            // The Wound and Bindings stacks hold what the boxes supply unless the Scheme sets their size. A Scheme
            // that sets one stack's size sets that stack only.
            int? wounds = null;
            if (effect.WoundsPerPlayer is { } woundsPerPlayer)
            {
                wounds = woundsPerPlayer.Value * players;
                notes.Add(Card($"{schemeWord} sets the Wound stack to {woundsPerPlayer.Value} per player", woundsPerPlayer.Source));
            }

            if (effect.Wounds is { } woundStack)
            {
                wounds = woundStack.Value;
                notes.Add(Card($"{schemeWord} sets the Wound stack to {woundStack.Value}", woundStack.Source));
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
                    ? HeroNamed(heroId)
                    : $"{count.Value} extra {HeroesOf(count.Value, rule.Team, rule.HeroName ?? (rule.HeroNames is { } any ? string.Join(" or ", any.Value) : null), rule.HeroNameContains)}";
                notes.Add(Card(
                    $"{schemeWord} draws {which} outside the {_terms.HeroDeck} and puts {(count.Value == 1 ? "its" : "their")} cards {Onto(rule.To)}",
                    count.Source));
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
                var group = CatalogGroup(rule.GroupId).Name;
                var what = rule.Card is { } card ? $"{card} of {group}" : $"{each} {group}";
                notes.Add(Card($"{schemeWord} sets {what} beside it{(rule.PerPlayer ? $", {count.Value} per player" : "")}", count.Source));
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
                otherMasterminds,
                (effect.RequiredGroups ?? []).ToDictionary(group => group.GroupId),
                [],
                notes);

            return Add(plan, effect, schemeWord, schemeBox, scheme.Id);
        }

        // The Scheme's plan with the Mastermind's setup effects added after the Scheme's.
        private SetupPlan WithMastermind(SetupPlan plan, Mastermind mastermind)
        {
            plan = plan with { Mastermind = mastermind };
            return mastermind.Setup is { } effects ? Add(plan, effects, mastermind.Name, BoxOf(mastermind.Id), mastermind.Id) : plan;
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
                notes.Add(Note($"{by} adds a setup step: {step.Label}", step.Source, from, cardId));
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

        // Fills one group type's slots: the Scheme's required groups first, then the Always Leads
        // group if a slot is left, then draws for the rest. A required group keeps its slot over
        // the Always Leads group. A required group from a box the setup doesn't include is still drawn, as the
        // player owns it, unless its card gives a substitute: then a group drawn from the pool takes its place, and
        // required records the substitute in its place.
        private List<T> FillSlots<T>(
            List<T> pool, GroupType type, int slots, Scheme scheme, Mastermind mastermind,
            Dictionary<string, RequiredGroup> required, List<RuleNote> notes,
            List<(Mastermind Drained, string Source)> drained) where T : ICard
        {
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
                        $"{SchemeWord(scheme)} uses {standIn.Name} in place of {group.Name}: {from.Name} isn't included",
                        substitute, BoxOf(scheme.Id), scheme.Id));
                    continue;
                }

                chosen.Add(group);
                var text = rule.Cards is { } cards
                    ? $"{SchemeWord(scheme)} requires {group.Name}, {cards} of its {CardName(CardKind.Henchman, cards)} in the {_terms.VillainDeck}"
                    : $"{SchemeWord(scheme)} requires {group.Name}";
                notes.Add(included
                    ? Note(text, rule.Source, BoxOf(scheme.Id), scheme.Id)
                    : Note($"{text}, from {from.Name}, which isn't included", rule.OtherBox!.Source, BoxOf(scheme.Id), scheme.Id));
            }

            // The Always Leads group of each Mastermind the Scheme sets aside, as an extra group of its own.
            foreach (var (other, source) in drained.Where(entry => entry.Drained.AlwaysLeads.GroupType == type))
            {
                var group = (T)CatalogGroup(other.AlwaysLeads.GroupId);
                if (!chosen.Contains(group))
                {
                    chosen.Add(group);
                }

                notes.Add(Note($"{SchemeWord(scheme)} adds {other.Name}'s Always Leads group {group.Name}", source, BoxOf(scheme.Id), scheme.Id));
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
                    var group = (T)CatalogGroup(leads.GroupId);
                    if (!chosen.Contains(group) && chosen.Count >= slots)
                    {
                        notes.Add(Note(
                            $"{SchemeWord(scheme)} requires {string.Join(" and ", chosen.Select(g => g.Name))}, so {mastermind.Name}'s Always Leads group {group.Name} is dropped",
                            rules.Rulings.RequiredGroupDisplacesAlwaysLeads, rulesBox));
                    }
                    else
                    {
                        if (!chosen.Contains(group))
                        {
                            chosen.Add(group);
                        }

                        notes.Add(Note($"{mastermind.Name} always leads {group.Name}", rules.Rulings.AlwaysLeadsFillsSlot, rulesBox));
                    }
                }
            }

            // The Mastermind's second group, one of several: one already chosen serves, or one is drawn into a slot left.
            // Solo's note that it ignores Always Leads covers this group too.
            if (mastermind.AlsoLeads is { } also && also.GroupType == type && !IgnoresAlwaysLeads)
            {
                var options = pool.Where(g => also.GroupIds.Contains(g.Id)).ToList();
                var group = chosen.FirstOrDefault(options.Contains);
                if (group is null && chosen.Count >= slots)
                {
                    notes.Add(Note(
                        $"{SchemeWord(scheme)} requires {string.Join(" and ", chosen.Select(g => g.Name))}, so {mastermind.Name}'s other Always Leads group is dropped",
                        rules.Rulings.RequiredGroupDisplacesAlwaysLeads, rulesBox));
                }
                else
                {
                    if (group is null)
                    {
                        group = DrawOne(options);
                        chosen.Add(group);
                    }

                    notes.Add(Note(
                        $"{mastermind.Name} also always leads {group.Name}, one of {string.Join(" and ", options.Select(g => g.Name))}",
                        also.Source, BoxOf(mastermind.Id), mastermind.Id));
                }
            }

            chosen.AddRange(DrawMany(pool.Except(chosen).ToList(), slots - chosen.Count));
            return chosen;
        }

        // Draws the Hero Deck's Heroes, the Scheme's required Heroes first, then each Hero the Scheme draws
        // outside the Hero Deck. Every draw picks from the Heroes that leave the rest of the setup's Heroes
        // drawable within the Scheme's Hero rules, so the draw never reaches a dead end. The Hero Deck draws from the
        // included Heroes whose parts the included boxes supply, or from every included Hero when the Scheme's Hero
        // rules need one that uses a part none supplies, which the setup then lays out from the box that has it. A
        // draw outside the Hero Deck the box data lets take a Hero from another box draws from the loaded Heroes it
        // allows when no included Hero it can draw has one of its Hero Names (D-scheme-first, #138), and still takes an
        // included one first, as one whose part no included box supplies (#150).
        private (List<Hero> Deck, List<OutsideHero> Outside) DrawHeroes(SetupPlan plan)
        {
            var setup = plan.Scheme.Setup;
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

            while (deck.Count < rules.DeckSlots)
            {
                var options = pool.Where(hero => rules.CanComplete([.. deck, hero], [])).ToList();
                var fromIncluded = options.Where(preferred.Contains).ToList();
                deck.Add(DrawOne(fromIncluded.Count > 0 ? fromIncluded : options));
            }

            // A draw outside the Hero Deck takes a Hero of the included boxes whenever one keeps the Hero rules completable,
            // so a Scheme's named Hero comes from another box only when no included one can be used (#150).
            var outside = new List<Hero>();
            while (outside.Count < rules.OutsideSlots.Count)
            {
                var options = heroes.Where(hero => rules.CanComplete(deck, [.. outside, hero])).ToList();
                var fromIncluded = options.Where(included.Contains).ToList();
                outside.Add(DrawOne(fromIncluded.Count > 0 ? fromIncluded : options));
            }

            return (deck, outside
                .Zip(rules.OutsideSlots, (hero, rule) => new OutsideHero(hero, rule.To, BoxOf(hero.Id).Components.HeroCards.Value))
                .ToList());
        }

        // "X-Men Heroes", "Jean Grey Hero" or "Heroes": what count Heroes of a team or a Hero Name are called.
        // nameContains is a word the Heroes have in their Hero Names: 'Heroes with "Hulk" in their Hero Names'.
        private string HeroesOf(int count, string? team, string? heroName, string? nameContains = null)
        {
            var kind = team is null ? heroName : boxes.SelectMany(box => box.Glossary).FirstOrDefault(term => term.Id == team)?.Name ?? team;
            var heroes = $"{(kind is null ? "" : kind + " ")}{(count == 1 ? _terms.Hero : _terms.Heroes)}";
            return nameContains is null ? heroes : $"{heroes} with \"{nameContains}\" in {(count == 1 ? "its Hero Name" : "their Hero Names")}";
        }

        // Any loaded Hero, since a Scheme's named Hero is drawn even when no included box supplies a part it uses.
        private string HeroNamed(string heroId) => catalogBoxes.SelectMany(box => box.Heroes).Single(hero => hero.Id == heroId).Name;

        private string Onto(Pile to) => to switch
        {
            Pile.VillainDeck => $"into the {_terms.VillainDeck}",
            Pile.BesideScheme => $"beside the {_terms.Scheme}",
            Pile.SetAside => "in a stack set aside",
            Pile.KoPile => "into the KO pile",
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
            if (!IgnoresAlwaysLeads)
            {
                placed.Add(plan.Mastermind!.AlwaysLeads.GroupId);
                placed.UnionWith(plan.Mastermind.AlsoLeads?.GroupIds ?? []);
            }

            return drawn.Where(id => !placed.Contains(id)).TakeLast(extra.Value).ToHashSet();
        }

        // How many cards a group puts in the Villain Deck when the Scheme sets none of them beside it: all of a
        // Villain Group, and of a Henchman Group the Scheme's count, Solo's, or all of it.
        // A Henchman Group the Scheme requires with a count of its own puts that many in.
        private int ShareOfDeck(SetupPlan plan, string groupId, GroupType type) => type == GroupType.Villain
            ? GroupCards(groupId, type)
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
        private Box BoxOf(string id) => catalogBoxes.Single(box => id.StartsWith(box.Id + "_", StringComparison.Ordinal));

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
        // A note a card causes cites the box that declares it but names the included boxes that hold it, as the card's
        // own label does: a reprint's note names the reprinting box when the original isn't included (#153).
        private RuleNote Note(string text, string citation, Box from, string? cardId = null) =>
            new(text, citation, LinkOf(citation, from), boxes.Count > 1 ? BoxLabel(from, cardId) : null);

        private string BoxLabel(Box from, string? cardId)
        {
            var holders = cardId is null ? [] : boxes.Where(box => box.Holds(cardId)).ToList();
            return holders.Count == 0 ? from.Name : string.Join(" or ", holders.Select(box => box.Name));
        }

        private static string? LinkOf(string citation, Box from) =>
            from.Sources.FirstOrDefault(source => source.Key == citation.Split(' ')[0])?.Url;
    }

    // A card is set up only when an included box of its own ruleset supplies every part it uses, or the part has a
    // stand-in some included box supplies, or one that needs no part (D-heroic). Those boxes' stacks are laid out in
    // every setup that draws the card.
    private static bool Supplied(IReadOnlyList<Box> boxes, IReadOnlyDictionary<Part, ActiveStandIn> standIns, ICard card)
    {
        var ruleset = boxes.First(box => box.Holds(card.Id)).Ruleset;
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
        Part.Ambitions => "Ambition",
        Part.Horrors => "Horror",
        _ => throw new ArgumentOutOfRangeException(nameof(part), part, null),
    };

    // The counts a setup uses, from the rules and the setup effects of its Scheme and, once one is
    // paired with it, its Mastermind. HenchmanCards is the Scheme's count of cards of each Henchman
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
