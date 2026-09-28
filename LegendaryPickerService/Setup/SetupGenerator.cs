using LegendaryPickerService.Catalog;

namespace LegendaryPickerService.Setup;

// Draws a random legal setup the way players would at the table: the Scheme from those allowed
// at the player count, then the Mastermind, then the groups they require, then the remaining
// Villain and Henchman Groups, then the Heroes. Each draw goes through the IRandomSource and picks
// from the options left, in catalog order, so a fixed sequence of draws always gives the same setup.
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
            var schemes = boxes.SelectMany(box => box.Schemes)
                .Where(scheme => scheme.Setup.AllowedPlayerCounts?.Value.Contains(players) ?? true)
                .Select(Plan)
                .Where(Fits)
                .ToList();
            // Without a Mastermind no Scheme has a legal completion.
            if (schemes.Count == 0 || masterminds.Count == 0)
            {
                return new NoEligibleScheme(players);
            }

            var plan = DrawOne(schemes);
            var mastermind = DrawOne(masterminds);
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
                    plan.HeroCardsMoved),
                new HeroDeck(heroes.Sum(hero => BoxOf(hero.Id).Components.HeroCards.Value), plan.HeroCardsMoved),
                plan.TwistsBeside,
                new SetupStacks(plan.Wounds, rules.SharedStacks.Officers.Value, rules.SharedStacks.Bystanders.Value - plan.Bystanders),
                new PlayerDeck(rules.StartingDeck.Agents.Value, rules.StartingDeck.Troopers.Value),
                notes,
                boxes);
        }

        // The counts one Scheme sets at this player count: the player-count table or Solo,
        // with the Scheme's Setup line applied on top, because printed card text wins.
        private SchemePlan Plan(Scheme scheme)
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

            var wounds = rules.SharedStacks.Wounds.Value;
            if (effect.WoundsPerPlayer is { } woundsPerPlayer)
            {
                wounds = woundsPerPlayer.Value * players;
                notes.Add(Card($"Scheme sets the Wound stack to {woundsPerPlayer.Value} per player", woundsPerPlayer.Source));
            }

            var henchmanGroups = Solo ? rules.Solo.HenchmanGroups.Value : row!.HenchmanGroups;
            if (effect.ExtraHenchmanGroups is { } extra)
            {
                henchmanGroups += extra.Value;
                notes.Add(Card($"Scheme adds {extra.Value} Henchman Group{(extra.Value == 1 ? "" : "s")}", extra.Source));
            }

            if (effect.HeroCardsInVillainDeck is { } moved)
            {
                notes.Add(Card($"Scheme moves {moved.Value} Hero cards into the Villain Deck", moved.Source));
            }

            if (effect.TwistsBesideScheme is { } beside)
            {
                notes.Add(Card($"Scheme puts {beside.Value} Twists beside it", beside.Source));
            }

            var twists = ForPlayers(effect.Twists)
                ?? throw new InvalidDataException($"{scheme.Id} has no Twist count for {players} players.");

            return new SchemePlan(
                scheme,
                heroes,
                Solo ? rules.Solo.VillainGroups.Value : row!.VillainGroups,
                henchmanGroups,
                twists.Value,
                effect.TwistsBesideScheme?.Value ?? 0,
                Solo ? rules.Solo.MasterStrikes.Value : rules.MasterStrikes.Value,
                bystanders,
                wounds,
                effect.HeroCardsInVillainDeck?.Value ?? 0,
                notes);
        }

        // A Scheme the included cards cannot complete is dropped before the draw rather than relaxed.
        private bool Fits(SchemePlan plan)
        {
            var required = plan.Scheme.Setup.RequiredGroups ?? [];
            int Slots(int slots, GroupType type) => Math.Max(slots, required.Count(group => group.GroupType == type));

            return required.All(group => GroupIds().Contains(group.GroupId))
                && Slots(plan.VillainGroups, GroupType.Villain) <= _villainGroups.Count
                && Slots(plan.HenchmanGroups, GroupType.Henchman) <= _henchmanGroups.Count
                && plan.Heroes <= _heroes.Count
                && plan.HeroCardsMoved <= plan.Heroes * boxes.Min(box => box.Components.HeroCards.Value)
                && plan.Bystanders <= rules.SharedStacks.Bystanders.Value
                && plan.Twists + plan.TwistsBeside <= boxes.Sum(box => box.Components.SchemeTwists.Value);
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

        private HashSet<string> GroupIds() =>
            _villainGroups.Select(g => g.Id).Concat(_henchmanGroups.Select(g => g.Id)).ToHashSet();

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

    private sealed record SchemePlan(
        Scheme Scheme,
        int Heroes,
        int VillainGroups,
        int HenchmanGroups,
        int Twists,
        int TwistsBeside,
        int MasterStrikes,
        int Bystanders,
        int Wounds,
        int HeroCardsMoved,
        IReadOnlyList<RuleNote> Notes);
}
