using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Draws a random legal setup the way players would at the table: the Scheme from those allowed
// at the player count, then a Mastermind that can complete it, then the groups they require, then
// the remaining Villain and Henchman Groups, then the Heroes. Each draw goes through the
// IRandomSource and picks from the options left, in catalog order, so a fixed sequence of draws
// always gives the same setup.
//
// Every count and rule comes from the box data; nothing here compares a card name.
public sealed class SetupGenerator(BoxCatalog catalog)
{
    public const string CoreBoxId = "core";

    public GenerationResult Generate(int players, IRandomSource random) => Generate(players, [CoreBoxId], random);

    // The included base game supplies the setup rules (the player-count table, Solo, stacks,
    // rulings); every included box contributes its cards, drawn in catalog order whatever order
    // the boxes are named in.
    public GenerationResult Generate(int players, IReadOnlyCollection<string> includedBoxes, IRandomSource random)
    {
        if (CheckBoxes(includedBoxes) is { } problem)
        {
            throw new ArgumentException(problem, nameof(includedBoxes));
        }

        var boxes = catalog.Boxes.Where(box => includedBoxes.Contains(box.Id)).ToList();
        var rulesBox = boxes.Single(box => box.IsBaseGame);
        var rules = rulesBox.Setup!;
        var row = rules.PlayerCounts.SingleOrDefault(r => r.Players == players);
        if (players != 1 && row is null)
        {
            throw new ArgumentOutOfRangeException(nameof(players), players,
                $"Supported player counts are 1 (Solo) and {string.Join(", ", rules.PlayerCounts.Select(r => r.Players))}.");
        }

        return new TableDraw(boxes, rulesBox, rules, players, row, random).Run();
    }

    // Why a set of box ids can't be drawn from, or null when it can. A setup needs exactly one
    // base game for its rules; choosing between two is not supported yet.
    public string? CheckBoxes(IReadOnlyCollection<string> includedBoxes)
    {
        if (includedBoxes.FirstOrDefault(id => catalog.Boxes.All(box => box.Id != id)) is { } unknown)
        {
            return $"No box has id {unknown}.";
        }

        return catalog.Boxes.Count(box => box.IsBaseGame && includedBoxes.Contains(box.Id)) == 1
            ? null
            : "Include exactly one base game.";
    }

    // One table draw: the included cards, the base game whose rules they are drawn under, and the random source.
    // row is the player-count table row, or null in Solo.
    private sealed class TableDraw(IReadOnlyList<Box> boxes, Box rulesBox, SetupRules rules, int players, PlayerCountSetup? row, IRandomSource random)
    {
        private readonly List<VillainGroup> _villainGroups = boxes.SelectMany(box => box.VillainGroups).ToList();
        private readonly List<HenchmanGroup> _henchmanGroups = boxes.SelectMany(box => box.HenchmanGroups).ToList();
        private readonly List<Hero> _heroes = boxes.SelectMany(box => box.Heroes).ToList();

        private bool Solo => row is null;

        private bool IgnoresAlwaysLeads => Solo && rules.Solo.IgnoresAlwaysLeads.Value;

        public GenerationResult Run()
        {
            // A Mastermind whose Always Leads group is not in an included box cannot be set up legally.
            var masterminds = boxes.SelectMany(box => box.Masterminds)
                .Where(m => IgnoresAlwaysLeads || GroupIds().Contains(m.AlwaysLeads.GroupId))
                .ToList();
            // A Scheme is eligible when at least one Mastermind completes it, and the Mastermind is then
            // drawn from those that do, since a Mastermind's setup effects can ask for more cards.
            // Without a Mastermind no Scheme has a legal completion.
            List<SetupPlan> Completions(SetupPlan schemePlan) =>
                masterminds.Select(mastermind => WithMastermind(schemePlan, mastermind)).Where(Fits).ToList();

            var schemes = boxes.SelectMany(box => box.Schemes)
                .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
                .Select(Plan)
                .Where(schemePlan => Completions(schemePlan).Count > 0)
                .ToList();
            if (schemes.Count == 0)
            {
                return new NoEligibleScheme(players);
            }

            var plan = DrawOne(Completions(DrawOne(schemes)));
            var mastermind = plan.Mastermind!;
            var notes = new List<RuleNote>(plan.Notes);

            var villainGroups = FillSlots(_villainGroups, g => g.Id, g => g.Name, GroupType.Villain, plan.VillainGroups, plan.Scheme, mastermind, notes);
            var henchmanGroups = FillSlots(_henchmanGroups, g => g.Id, g => g.Name, GroupType.Henchman, plan.HenchmanGroups, plan.Scheme, mastermind, notes);
            var heroes = DrawMany(_heroes, plan.Heroes);

            if (Solo)
            {
                var ko = rules.Solo.TwistKosHeroCostingAtMost;
                notes.Add(Note($"Solo: after each Twist, KO a Hero costing {ko.Value} or less from the HQ", ko.Source, rulesBox));
            }

            return new SetupResult(
                players,
                plan.Scheme,
                mastermind,
                villainGroups,
                henchmanGroups,
                heroes,
                new VillainDeck(
                    plan.Twists,
                    plan.MasterStrikes,
                    villainGroups.Sum(group => BoxOf(group.Id).Components.VillainGroupCards.Value),
                    henchmanGroups.Sum(group => Solo ? rules.Solo.HenchmanCards.Value : BoxOf(group.Id).Components.HenchmanGroupCards.Value),
                    plan.Bystanders,
                    plan.MovedIn(Pile.VillainDeck),
                    plan.MovedOut(Pile.VillainDeck)),
                new HeroDeck(heroes.Sum(hero => BoxOf(hero.Id).Components.HeroCards.Value), plan.MovedOut(Pile.HeroDeck), plan.MovedIn(Pile.HeroDeck)),
                plan.TwistsBeside,
                new SetupStacks(
                    plan.Wounds - plan.MovedOut(Pile.Wounds),
                    Supply(components => components.Officers) - plan.MovedOut(Pile.Officers),
                    Supply(components => components.Bystanders) - plan.Bystanders - plan.MovedOut(Pile.Bystanders),
                    boxes.Any(box => box.Components.Sidekicks is not null)
                        ? Supply(components => components.Sidekicks) - plan.MovedOut(Pile.Sidekicks)
                        : null),
                new PlayerDeck(rules.StartingDeck.Agents.Value, rules.StartingDeck.Troopers.Value),
                plan.Moves,
                notes,
                boxes);
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
            RuleNote Replaces(string soloText, string text, string source) => Solo
                ? Note($"Scheme overrides Solo: {soloText}", rules.Rulings.SchemeOverridesSolo, rulesBox)
                : Card($"Scheme {text}", source);

            var heroes = Solo ? rules.Solo.Heroes.Value : rules.Heroes.Value;
            if (ForPlayers(effect.Heroes ?? []) is { } schemeHeroes)
            {
                heroes = schemeHeroes.Value;
                notes.Add(Replaces($"{heroes} Heroes", $"uses {heroes} Heroes", schemeHeroes.Source));
            }

            var bystanders = Solo ? rules.Solo.Bystanders.Value : row!.Bystanders;
            if (effect.VillainDeckBystanders is { } schemeBystanders)
            {
                bystanders = schemeBystanders.Value;
                notes.Add(Replaces(
                    $"{bystanders} Bystanders in the Villain Deck", $"puts {bystanders} Bystanders in the Villain Deck", schemeBystanders.Source));
            }

            var wounds = Supply(components => components.Wounds);
            if (effect.WoundsPerPlayer is { } woundsPerPlayer)
            {
                wounds = woundsPerPlayer.Value * players;
                notes.Add(Card($"Scheme sets the Wound stack to {woundsPerPlayer.Value} per player", woundsPerPlayer.Source));
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
                    $"Scheme moves {each} {CardName(move.Card, each)} {Into(move.To)}{(move.PerPlayer ? $", {count.Value} per player" : "")}",
                    count.Source));
            }

            if (effect.TwistsBesideScheme is { } beside)
            {
                notes.Add(Card($"Scheme puts {beside.Value} Twists beside it", beside.Source));
            }

            var twists = ForPlayers(effect.Twists)
                ?? throw new InvalidDataException($"{scheme.Id} has no Twist count for {players} players.");

            var plan = new SetupPlan(
                scheme,
                null,
                heroes,
                Solo ? rules.Solo.VillainGroups.Value : row!.VillainGroups,
                Solo ? rules.Solo.HenchmanGroups.Value : row!.HenchmanGroups,
                twists.Value,
                effect.TwistsBesideScheme?.Value ?? 0,
                Solo ? rules.Solo.MasterStrikes.Value : rules.MasterStrikes.Value,
                bystanders,
                wounds,
                moves,
                notes);

            return Add(plan, effect, "Scheme", schemeBox);
        }

        // The Scheme's plan with the Mastermind's setup effects added after the Scheme's.
        private SetupPlan WithMastermind(SetupPlan plan, Mastermind mastermind)
        {
            plan = plan with { Mastermind = mastermind };
            return mastermind.Setup is { } effects ? Add(plan, effects, mastermind.Name, BoxOf(mastermind.Id)) : plan;
        }

        // Adds each effect that applies at this player count to the plan's counts, with a note citing
        // the card that prints it. An effect only adds to the count, so it replaces no Solo value.
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

            return plan with
            {
                Heroes = plan.Heroes + Extra(effects.ExtraHeroes, "Hero", "Heroes"),
                VillainGroups = plan.VillainGroups + Extra(effects.ExtraVillainGroups, "Villain Group", "Villain Groups"),
                HenchmanGroups = plan.HenchmanGroups + Extra(effects.ExtraHenchmanGroups, "Henchman Group", "Henchman Groups"),
                Bystanders = plan.Bystanders + Extra(
                    effects.ExtraVillainDeckBystanders, "Bystander", "Bystanders", " to the Villain Deck"),
                Notes = notes,
            };
        }

        // A Scheme, or a Scheme and Mastermind pair, the included cards cannot complete is dropped
        // before the draw rather than relaxed.
        private bool Fits(SetupPlan plan)
        {
            var required = plan.Scheme.Setup.RequiredGroups ?? [];
            int Slots(int slots, GroupType type) => Math.Max(slots, required.Count(group => group.GroupType == type));

            return required.All(group => GroupIds().Contains(group.GroupId))
                && Slots(plan.VillainGroups, GroupType.Villain) <= _villainGroups.Count
                && Slots(plan.HenchmanGroups, GroupType.Henchman) <= _henchmanGroups.Count
                && plan.Heroes <= _heroes.Count
                && plan.MovedOut(Pile.HeroDeck) <= plan.Heroes * boxes.Min(box => box.Components.HeroCards.Value)
                && plan.MovedOut(Pile.VillainDeck) <= plan.HenchmanGroups * (Solo
                    ? rules.Solo.HenchmanCards.Value
                    : boxes.Min(box => box.Components.HenchmanGroupCards.Value))
                && plan.Bystanders + plan.MovedOut(Pile.Bystanders) <= Supply(components => components.Bystanders)
                && plan.MovedOut(Pile.Wounds) <= plan.Wounds
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
                notes.Add(Note($"Scheme requires {name(group)}", required.Source, BoxOf(scheme.Id)));
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
                            $"Scheme requires {string.Join(" and ", chosen.Select(name))}, so {mastermind.Name}'s Always Leads group {name(group)} is dropped",
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

        // How many cards of one kind the included boxes hold between them.
        private int Supply(Func<BoxComponents, Sourced<int>?> count) => boxes.Sum(box => count(box.Components)?.Value ?? 0);

        private HashSet<string> GroupIds() =>
            _villainGroups.Select(g => g.Id).Concat(_henchmanGroups.Select(g => g.Id)).ToHashSet();

        private static string CardName(CardKind card, int count) => (card, count == 1) switch
        {
            (CardKind.Hero, true) => "Hero card",
            (CardKind.Hero, false) => "Hero cards",
            (CardKind.Henchman, true) => "Henchman",
            (CardKind.Henchman, false) => "Henchmen",
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

        private static string Into(Pile to) => to switch
        {
            Pile.VillainDeck => "into the Villain Deck",
            Pile.HeroDeck => "into the Hero Deck",
            Pile.BesideScheme => "beside it",
            Pile.StartingDecks => "into each starting deck",
            _ => throw new ArgumentOutOfRangeException(nameof(to), to, null),
        };

        private PlayerCountValue? ForPlayers(IReadOnlyList<PlayerCountValue> values) =>
            values.SingleOrDefault(value => value.Players?.Contains(players) ?? true);

        // Ids start with the declaring box's id, and box ids contain no underscore.
        private Box BoxOf(string id) => boxes.Single(box => id.StartsWith(box.Id + "_", StringComparison.Ordinal));

        // A note cites a source key of the box whose rule it is, since each box lists its own sources.
        // Once more than one box is included, the note also names that box.
        private RuleNote Note(string text, string citation, Box from) => new(
            text,
            citation,
            from.Sources.FirstOrDefault(source => source.Key == citation.Split(' ')[0])?.Url,
            boxes.Count > 1 ? from.Name : null);
    }

    // The counts a setup uses, from the rules and the setup effects of its Scheme and, once one is
    // paired with it, its Mastermind.
    private sealed record SetupPlan(
        Scheme Scheme,
        Mastermind? Mastermind,
        int Heroes,
        int VillainGroups,
        int HenchmanGroups,
        int Twists,
        int TwistsBeside,
        int MasterStrikes,
        int Bystanders,
        int Wounds,
        IReadOnlyList<MovedCards> Moves,
        IReadOnlyList<RuleNote> Notes)
    {
        public int MovedIn(Pile to) => Moves.Where(move => move.To == to).Sum(move => move.Count);

        public int MovedOut(Pile from) => Moves.Where(move => move.From == from).Sum(move => move.Total);
    }
}
