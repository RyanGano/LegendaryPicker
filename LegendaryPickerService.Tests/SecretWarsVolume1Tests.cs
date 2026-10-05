using LegendaryPickerService.Catalog;
using LegendaryPickerService.Setup;

namespace LegendaryPickerService.Tests;

// The Secret Wars Volume 1 box file (Data/Boxes/secret-wars-volume-1.json), pinned to the rules insert (SW1), the
// card catalog and the card faces it links, and drawn with the core box. Catalog order puts its cards after the core
// box's, so at 2–5 players its Schemes are 8 Build an Army of Annihilation to 15 Smash Two Dimensions Together;
// Solo allows 6 core Schemes, so there they are 6 to 13. Its Masterminds are 4 Madelyne Pryor, Goblin Queen,
// 5 Nimrod, Super Sentinel, 6 Wasteland Hulk and 7 Zombie Green Goblin.
public class SecretWarsVolume1Tests
{
    private const string SecretWarsName = "Secret Wars Volume 1";
    private const string CoreName = "Marvel Legendary First Edition core box";
    private const string Rulebook = "https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf";

    private static readonly BoxCatalog Catalog = BoxCatalog.Load(BoxCatalog.DefaultDirectory);
    private static readonly Box SecretWars = Catalog.Boxes.Single(box => box.Id == "secret-wars-volume-1");
    private static readonly SetupGenerator Generator = new(Catalog);

    private static readonly string[] Boxes = ["core", "secret-wars-volume-1"];

    private static readonly string[] SchemeNames =
    [
        "Build an Army of Annihilation",
        "Corrupt the Next Generation of Heroes",
        "Crush Them With My Bare Hands",
        "Dark Alliance",
        "Fragmented Realities",
        "Master of Tyrants",
        "Pan-Dimensional Plague",
        "Smash Two Dimensions Together",
    ];

    private const int Madelyne = 4;
    private const int Nimrod = 5;

    // Catalog

    [Fact]
    public void Secret_Wars_Volume_1_is_an_expansion_with_the_Sidekicks_of_the_rules_insert()
    {
        Assert.Equal(SecretWarsName, SecretWars.Name);
        Assert.Equal(7, SecretWars.SchemaVersion);
        Assert.False(SecretWars.IsBaseGame);
        Assert.Equal(Ruleset.FirstEdition, SecretWars.Ruleset);
        Assert.Equal(new Sourced<string>("2015-08", "Q"), SecretWars.About.Released);

        var components = SecretWars.Components;
        Assert.Equal(new Sourced<int>(14, "SW1 p.2"), components.HeroCards);
        Assert.Equal(new Sourced<int>(8, "SW1 p.2"), components.VillainGroupCards);
        Assert.Equal(new Sourced<int>(10, "SW1 p.2"), components.HenchmanGroupCards);
        Assert.Equal(new Sourced<int>(0, "SW1 p.2"), components.SchemeTwists);
        Assert.Equal(new Sourced<int>(1, "SW1 p.2"), components.Bystanders);
        Assert.Equal(new Sourced<int>(15, "SW1 p.2"), components.Sidekicks);
        Assert.Null(components.Wounds);
        Assert.Null(components.Officers);
    }

    [Fact]
    public void Secret_Wars_Volume_1_has_the_14_Heroes_6_Villain_Groups_3_Henchman_Groups_4_Masterminds_and_its_8_Schemes()
    {
        Assert.Equal(
            [
                "Apocalyptic Kitty Pryde", "Black Bolt", "Black Panther", "Captain Marvel", "Dr. Strange (Illuminati)", "Lady Thor", "Magik",
                "Maximus", "Namor, the Sub-Mariner", "Old Man Logan", "Proxima Midnight", "Superior Iron Man", "Thanos (Cabal)", "Ultimate Spider-Man",
            ],
            SecretWars.Heroes.Select(hero => hero.Name));
        Assert.Equal(
            ["The Deadlands", "Domain of Apocalypse", "Limbo", "Manhattan (Earth-1610)", "Sentinel Territories", "Wasteland"],
            SecretWars.VillainGroups.Select(group => group.Name));
        Assert.Equal(["Ghost Racers", "M.O.D.O.K.s", "Thor Corps"], SecretWars.HenchmanGroups.Select(group => group.Name));
        Assert.Equal(
            ["Madelyne Pryor, Goblin Queen", "Nimrod, Super Sentinel", "Wasteland Hulk", "Zombie Green Goblin"],
            SecretWars.Masterminds.Select(mastermind => mastermind.Name));
        Assert.All(SecretWars.Masterminds, mastermind => Assert.Null(mastermind.Setup));
        Assert.Equal(SchemeNames, SecretWars.Schemes.Select(scheme => scheme.Name));
    }

    // Two Heroes share a name with an earlier box's card: the Villains Commander Dr. Strange and the Guardians
    // Mastermind Thanos. Each takes its team as its version and keeps the character as its Hero Name (#76).
    [Theory]
    [InlineData("secret-wars-volume-1_hero_dr-strange-illuminati", "Dr. Strange (Illuminati)", "Dr. Strange")]
    [InlineData("secret-wars-volume-1_hero_thanos-cabal", "Thanos (Cabal)", "Thanos")]
    public void A_Hero_whose_name_an_earlier_box_uses_takes_a_distinguishing_name(string id, string name, string heroName)
    {
        var hero = SecretWars.Heroes.Single(h => h.Id == id);

        Assert.Equal(name, hero.Name);
        Assert.Equal(heroName, hero.NameOfHero);
    }

    // Sidekicks: four Heroes gain them and Corrupt the Next Generation moves them (its Setup line). Wounds: the
    // Cross-Dimensional Rampages, Madelyne's Master Strike, Zombie Green Goblin's The Hungry Dead, the Deadlands'
    // Zombie Loki and Zombie Venom, and Pan-Dimensional Plague.
    [Fact]
    public void Secret_Wars_cards_list_the_parts_they_use()
    {
        ICard[] cards = [.. SecretWars.Heroes, .. SecretWars.VillainGroups, .. SecretWars.HenchmanGroups, .. SecretWars.Masterminds, .. SecretWars.Schemes];
        Assert.Equal(
            [
                "secret-wars-volume-1_hero_magik: Sidekicks",
                "secret-wars-volume-1_hero_maximus: Sidekicks",
                "secret-wars-volume-1_hero_namor-the-sub-mariner: Sidekicks",
                "secret-wars-volume-1_hero_old-man-logan: Wounds",
                "secret-wars-volume-1_hero_ultimate-spider-man: Sidekicks",
                "secret-wars-volume-1_villain_the-deadlands: Wounds",
                "secret-wars-volume-1_villain_domain-of-apocalypse: Wounds",
                "secret-wars-volume-1_villain_manhattan-earth-1610: Wounds",
                "secret-wars-volume-1_villain_sentinel-territories: Wounds",
                "secret-wars-volume-1_villain_wasteland: Wounds",
                "secret-wars-volume-1_mastermind_madelyne-pryor-goblin-queen: Wounds",
                "secret-wars-volume-1_mastermind_wasteland-hulk: Wounds",
                "secret-wars-volume-1_mastermind_zombie-green-goblin: Wounds",
                "secret-wars-volume-1_scheme_corrupt-the-next-generation-of-heroes: Sidekicks",
                "secret-wars-volume-1_scheme_pan-dimensional-plague: Wounds",
            ],
            cards.Where(card => card.Parts.Any()).Select(card => $"{card.Id}: {string.Join(" ", card is Scheme scheme ? scheme.Parts : card.Parts)}"));
        Assert.All(cards.SelectMany(card => card.Uses ?? []), use => Assert.Equal("Card", use.Source));
    }

    [Fact]
    public void Each_Scheme_carries_the_Setup_line_on_its_card()
    {
        var schemes = SecretWars.Schemes.ToDictionary(scheme => scheme.Name);

        // The 10 Henchmen go in the KO pile; which group they come from is the owner's decision (D-ko-henchmen).
        var army = schemes["Build an Army of Annihilation"];
        Assert.Equal(["all: 9 Card"], Twists(army));
        var koPile = Assert.Single(army.Setup.OutsideHenchmen!);
        Assert.Equal(Pile.KoPile, koPile.To);
        Assert.Equal([new PlayerCountValue(null, 10, "Card")], koPile.Cards);
        Assert.Equal([new SetupStep("Any Henchman Group outside the Villain Deck stands in for the Annihilation Wave", "D-ko-henchmen")], army.Setup.Steps);

        var corrupt = schemes["Corrupt the Next Generation of Heroes"];
        Assert.Equal(["all: 8 Card"], Twists(corrupt));
        var move = Assert.Single(corrupt.Setup.Moves!);
        Assert.Equal((CardKind.Sidekick, Pile.VillainDeck, false), (move.Card, move.To, move.PerPlayer));
        Assert.Equal([new PlayerCountValue(null, 10, "Card")], move.Count);

        var crush = schemes["Crush Them With My Bare Hands"];
        Assert.Equal(["all: 5 Card"], Twists(crush));
        var soloGroup = Assert.Single(crush.Setup.ExtraVillainGroups!);
        Assert.Equal("1: 1 Card", $"{string.Join(",", soloGroup.Players!)}: {soloGroup.Value} {soloGroup.Source}");

        var alliance = schemes["Dark Alliance"];
        Assert.Equal(["all: 8 Card"], Twists(alliance));
        var second = Assert.Single(alliance.Setup.OutsideMasterminds!);
        Assert.Equal((Pile.SetAside, (Sourced<int>?)null), (second.To, second.Tactics));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], second.Count);
        Assert.Equal(new Sourced<string>("Twist 1", "Card"), second.Joins);

        // 2 Twists in each player's Villain Deck.
        var fragmented = schemes["Fragmented Realities"];
        Assert.Equal(["1: 2 Card", "2: 4 Card", "3: 6 Card", "4: 8 Card", "5: 10 Card"], Twists(fragmented));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], fragmented.Setup.ExtraVillainGroups);
        Assert.Equal([new SetupStep("Deal the Villain Deck into one deck per player, each with 2 Twists", "Card")], fragmented.Setup.Steps);

        // 3 other Masterminds' 12 Tactics.
        var tyrants = schemes["Master of Tyrants"];
        Assert.Equal(["all: 8 Card"], Twists(tyrants));
        var others = Assert.Single(tyrants.Setup.OutsideMasterminds!);
        Assert.Equal((Pile.VillainDeck, new Sourced<int>(4, "Card")), (others.To, others.Tactics));
        Assert.Equal([new PlayerCountValue(null, 3, "Card")], others.Count);

        Assert.Equal(["all: 10 Card"], Twists(schemes["Pan-Dimensional Plague"]));

        var smash = schemes["Smash Two Dimensions Together"];
        Assert.Equal(["all: 8 Card"], Twists(smash));
        Assert.Equal([new PlayerCountValue(null, 1, "Card")], smash.Setup.ExtraVillainGroups);
        Assert.Equal([new SetupStep("Put the Villain Deck on the Bank space", "Card")], smash.Setup.Steps);

        Assert.All(SecretWars.Schemes, scheme => Assert.Null(scheme.Setup.AllowedPlayerCounts));
        Assert.All(SecretWars.Schemes, scheme => Assert.Null(scheme.Setup.RequiredGroups));

        static IEnumerable<string> Twists(Scheme scheme) =>
            scheme.Setup.Twists.Select(value => $"{(value.Players is null ? "all" : string.Join(",", value.Players))}: {value.Value} {value.Source}");
    }

    [Theory]
    [InlineData("Madelyne Pryor, Goblin Queen", "Limbo")]
    [InlineData("Nimrod, Super Sentinel", "Sentinel Territories")]
    [InlineData("Wasteland Hulk", "Wasteland")]
    [InlineData("Zombie Green Goblin", "The Deadlands")]
    public void Mastermind_Always_Leads_the_Villain_Group_on_its_card(string mastermind, string group)
    {
        var alwaysLeads = SecretWars.Masterminds.Single(m => m.Name == mastermind).AlwaysLeads;

        Assert.Equal(GroupType.Villain, alwaysLeads.GroupType);
        Assert.Equal("Card", alwaysLeads.Source);
        Assert.Equal(group, SecretWars.VillainGroups.Single(g => g.Id == alwaysLeads.GroupId).Name);
    }

    [Fact]
    public void Ids_name_the_Secret_Wars_box_and_the_rules_insert_is_its_source()
    {
        var ids = SecretWars.Heroes.Select(x => x.Id)
            .Concat(SecretWars.VillainGroups.Select(x => x.Id))
            .Concat(SecretWars.HenchmanGroups.Select(x => x.Id))
            .Concat(SecretWars.Masterminds.Select(x => x.Id))
            .Concat(SecretWars.Schemes.Select(x => x.Id))
            .Concat(SecretWars.Glossary.Select(x => x.Id));

        Assert.All(ids, id => Assert.Matches("^secret-wars-volume-1_(hero|villain|henchman|mastermind|scheme|term)_[a-z0-9]+(-[a-z0-9]+)*$", id));
        Assert.Equal(
            [new SourceLink("SW1", "https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules_Secret_Wars_v1.pdf"), new SourceLink("D-ko-henchmen", "https://github.com/RyanGano/LegendaryPicker/issues/116"), new SourceLink("Q", "https://github.com/RyanGano/LegendaryPicker/blob/main/Docs/BoxResearch/README.md")],
            SecretWars.Sources);
        Assert.Equal("SW1 p.2; C1", SecretWars.CatalogSource);
    }

    // Teleport and Bribe are Dark City's terms, which its cards reuse.
    [Fact]
    public void Secret_Wars_glossary_adds_its_teams_and_keywords_from_the_rules_insert()
    {
        Assert.Equal(
            [
                "Illuminati team SW1 p.1", "Cabal team SW1 p.1", "Multiclass keyword SW1 p.1", "Sidekick keyword SW1 p.1",
                "Rise of the Living Dead keyword SW1 p.1", "Cross-Dimensional Rampage keyword SW1 p.1", "Ambition keyword SW1 p.1", "Multiple Masterminds keyword SW1 p.1",
            ],
            SecretWars.Glossary.Select(term => $"{term.Name} {term.Kind.ToString().ToLowerInvariant()} {term.Source} p.{term.Page}"));
    }

    [Theory]
    [InlineData("Apocalyptic Kitty Pryde", "X-Men", "Covert Tech", "Multiclass")]
    [InlineData("Black Bolt", "Illuminati", "Covert Ranged Strength", "Multiclass")]
    [InlineData("Black Panther", "Illuminati", "Covert Instinct Strength Tech", "Multiclass")]
    [InlineData("Captain Marvel", "Avengers", "Ranged Strength", "Multiclass")]
    [InlineData("Dr. Strange (Illuminati)", "Illuminati", "Covert Instinct Ranged", "Teleport Multiclass")]
    [InlineData("Lady Thor", "Avengers", "Ranged Strength", "Multiclass")]
    [InlineData("Magik", "X-Men", "Covert Ranged", "Teleport Sidekick Multiclass")]
    [InlineData("Maximus", "Cabal", "Covert Tech", "Sidekick Multiclass")]
    [InlineData("Namor, the Sub-Mariner", "Cabal", "Instinct Strength", "Sidekick Multiclass")]
    [InlineData("Old Man Logan", "X-Men", "Covert Instinct", "Cross-Dimensional Rampage Multiclass")]
    [InlineData("Proxima Midnight", "Cabal", "Covert Instinct", "Multiclass")]
    [InlineData("Superior Iron Man", "Illuminati", "Ranged Tech", "Multiclass")]
    [InlineData("Thanos (Cabal)", "Cabal", "Ranged Strength", "Teleport Multiclass")]
    [InlineData("Ultimate Spider-Man", "Spider Friends", "Covert Instinct Strength Tech", "Sidekick Multiclass")]
    public void Hero_lists_its_team_classes_and_keywords(string hero, string team, string classes, string keywords)
    {
        var entry = SecretWars.Heroes.Single(h => h.Name == hero);

        Assert.Equal(team, TermName(entry.Team!));
        Assert.Equal(classes, string.Join(" ", entry.Classes.Select(TermName)));
        Assert.Equal(keywords, string.Join(" ", entry.Terms.Select(TermName)));
    }

    // Fixed-result draws: one per Scheme at 1, 2 and 5 players, each with Madelyne (who leads Limbo, except in Solo)
    // and the first remaining option for every later draw. The core box gives 11 Twists, 30 Wounds, 30 Officers and
    // 30 Bystanders, and this box 1 Bystander and 15 Sidekicks. Madelyne uses Wounds, so every setup lays them out; the
    // first core Heroes use no Sidekicks, so only Corrupt the Next Generation lays them out, less the 10 it moves.

    [Theory]
    [InlineData("Build an Army of Annihilation", 1, 9, 1, 8, 3, 1, 0, 0, 22, 30, null)]
    [InlineData("Build an Army of Annihilation", 2, 9, 5, 16, 10, 2, 0, 0, 42, 29, null)]
    [InlineData("Build an Army of Annihilation", 5, 9, 5, 32, 20, 12, 0, 0, 78, 19, null)]
    [InlineData("Corrupt the Next Generation of Heroes", 1, 8, 1, 8, 3, 1, 10, 0, 31, 30, 5)]
    [InlineData("Corrupt the Next Generation of Heroes", 2, 8, 5, 16, 10, 2, 10, 0, 51, 29, 5)]
    [InlineData("Corrupt the Next Generation of Heroes", 5, 8, 5, 32, 20, 12, 10, 0, 87, 19, 5)]
    [InlineData("Crush Them With My Bare Hands", 1, 5, 1, 16, 3, 1, 0, 0, 26, 30, null)]
    [InlineData("Crush Them With My Bare Hands", 2, 5, 5, 16, 10, 2, 0, 0, 38, 29, null)]
    [InlineData("Crush Them With My Bare Hands", 5, 5, 5, 32, 20, 12, 0, 0, 74, 19, null)]
    [InlineData("Dark Alliance", 1, 8, 1, 8, 3, 1, 0, 0, 21, 30, null)]
    [InlineData("Dark Alliance", 2, 8, 5, 16, 10, 2, 0, 0, 41, 29, null)]
    [InlineData("Dark Alliance", 5, 8, 5, 32, 20, 12, 0, 0, 77, 19, null)]
    [InlineData("Fragmented Realities", 1, 2, 1, 16, 3, 1, 0, 0, 23, 30, null)]
    [InlineData("Fragmented Realities", 2, 4, 5, 24, 10, 2, 0, 0, 45, 29, null)]
    [InlineData("Fragmented Realities", 5, 10, 5, 40, 20, 12, 0, 0, 87, 19, null)]
    [InlineData("Master of Tyrants", 1, 8, 1, 8, 3, 1, 0, 12, 33, 30, null)]
    [InlineData("Master of Tyrants", 2, 8, 5, 16, 10, 2, 0, 12, 53, 29, null)]
    [InlineData("Master of Tyrants", 5, 8, 5, 32, 20, 12, 0, 12, 89, 19, null)]
    [InlineData("Pan-Dimensional Plague", 1, 10, 1, 8, 3, 1, 0, 0, 23, 30, null)]
    [InlineData("Pan-Dimensional Plague", 2, 10, 5, 16, 10, 2, 0, 0, 43, 29, null)]
    [InlineData("Pan-Dimensional Plague", 5, 10, 5, 32, 20, 12, 0, 0, 79, 19, null)]
    [InlineData("Smash Two Dimensions Together", 1, 8, 1, 16, 3, 1, 0, 0, 29, 30, null)]
    [InlineData("Smash Two Dimensions Together", 2, 8, 5, 24, 10, 2, 0, 0, 49, 29, null)]
    [InlineData("Smash Two Dimensions Together", 5, 8, 5, 40, 20, 12, 0, 0, 85, 19, null)]
    public void Scheme_lays_out_its_decks_and_stacks(
        string scheme, int players, int twists, int strikes, int villainCards, int henchmanCards, int bystanders, int sidekicksIn,
        int tactics, int villainDeck, int bystanderStack, int? sidekicks)
    {
        var setup = Draw(players, scheme, Madelyne);

        Assert.Equal(scheme, setup.Scheme.Name);
        Assert.Equal("Madelyne Pryor, Goblin Queen", setup.Mastermind.Name);
        Assert.Equal(new VillainDeck(twists, strikes, villainCards, henchmanCards, bystanders, sidekicksIn, MastermindTactics: tactics), setup.VillainDeck);
        Assert.Equal(villainDeck, setup.VillainDeck.Total);
        Assert.Equal(players == 1 ? 42 : 70, setup.HeroDeck.Total);
        Assert.Equal(new SetupStacks(30, 30, bystanderStack, Sidekicks: sidekicks), setup.Stacks);
    }

    // The Sidekick-stack case: the 10 Sidekicks the Scheme moves into the Villain Deck leave 5 in the stack.
    [Fact]
    public void Corrupt_the_Next_Generation_of_Heroes_moves_10_Sidekicks_into_the_Villain_Deck()
    {
        var setup = Draw(3, "Corrupt the Next Generation of Heroes", Madelyne);

        Assert.Equal([new MovedCards(CardKind.Sidekick, Pile.Sidekicks, Pile.VillainDeck, 10, 10)], setup.Moves);
        Assert.Equal(5, setup.Stacks.Sidekicks);
        Assert.Contains(new RuleNote("Scheme moves 10 Sidekicks into the Villain Deck", "Card", null, SecretWarsName), setup.Notes);
    }

    // A Hero that gains Sidekicks brings the whole stack: Magik is the 7th Secret Wars Hero after the core box's 15.
    [Fact]
    public void A_Hero_that_gains_Sidekicks_lays_out_the_Sidekick_stack()
    {
        var setup = Assert.IsType<SetupResult>(Generator.Generate(2, Boxes, new ScriptedRandom(14, Madelyne, 0, 0, 21)));

        Assert.Equal("Magik", setup.Heroes[0].Name);
        Assert.Equal(15, setup.Stacks.Sidekicks);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Crush_Them_With_My_Bare_Hands_adds_a_Villain_Group_only_in_Solo(int players)
    {
        var setup = Draw(players, "Crush Them With My Bare Hands", Madelyne);

        var extra = new RuleNote("Scheme adds 1 Villain Group", "Card", null, SecretWarsName);
        if (players == 1)
        {
            Assert.Equal(2, setup.VillainGroups.Count);
            Assert.Contains(extra, setup.Notes);
        }
        else
        {
            Assert.DoesNotContain(extra, setup.Notes);
        }
    }

    // The second Mastermind is drawn from the others and set aside until the first Twist; its Always Leads group isn't
    // added, since the Mastermind arrives mid-game.
    [Fact]
    public void Dark_Alliance_sets_a_second_Mastermind_aside()
    {
        var setup = Draw(2, "Dark Alliance", Madelyne);

        var second = Assert.Single(setup.OutsideMasterminds!);
        Assert.Equal(("Dr. Doom", Pile.SetAside, (int?)null), (second.Mastermind.Name, second.To, second.Tactics));
        Assert.Equal(["Limbo", "Brotherhood"], setup.VillainGroups.Select(group => group.Name));
        Assert.Contains(new RuleNote("Scheme draws 1 other Mastermind and sets it aside", "Card", null, SecretWarsName), setup.Notes);
        Assert.Equal("Twist 1", second.Joins);
        Assert.Contains(new RuleNote("Scheme: the Mastermind set aside joins on Twist 1", "Card", null, SecretWarsName), setup.Notes);
    }

    // The 10 Annihilation Wave Henchmen are 10 cards of any one Henchman Group the Villain Deck doesn't use (D-ko-henchmen).
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void Build_an_Army_of_Annihilation_puts_the_Henchmen_of_a_group_the_Villain_Deck_lacks_in_the_KO_pile(int players)
    {
        var setup = Draw(players, "Build an Army of Annihilation", Madelyne);

        var outside = Assert.Single(setup.OutsideHenchmen);
        Assert.Equal((Pile.KoPile, 10), (outside.To, outside.Cards));
        Assert.DoesNotContain(outside.Group, setup.HenchmanGroups);
        Assert.Equal(new HeroDeck(players == 1 ? 42 : 70, 0), setup.HeroDeck);
        Assert.Contains(
            new RuleNote("Scheme draws 1 extra Henchman Group outside the Villain Deck and puts 10 of its Henchmen into the KO pile", "Card", null, SecretWarsName),
            setup.Notes);
        Assert.Contains(
            new RuleNote("Scheme adds a setup step: Any Henchman Group outside the Villain Deck stands in for the Annihilation Wave", "D-ko-henchmen", "https://github.com/RyanGano/LegendaryPicker/issues/116", SecretWarsName),
            setup.Notes);
    }

    [Fact]
    public void Master_of_Tyrants_shuffles_12_Tactics_of_3_other_Masterminds_into_the_Villain_Deck()
    {
        var setup = Draw(2, "Master of Tyrants", Madelyne);

        Assert.Equal(["Dr. Doom", "Loki", "Magneto"], setup.OutsideMasterminds!.Select(other => other.Mastermind.Name));
        Assert.All(setup.OutsideMasterminds!, other => Assert.Equal((Pile.VillainDeck, (int?)4), (other.To, other.Tactics)));
        Assert.Equal(12, setup.VillainDeck.MastermindTactics);
        Assert.Contains(
            new RuleNote("Scheme draws 3 other Masterminds and shuffles 12 of their Tactics into the Villain Deck", "Card", null, SecretWarsName),
            setup.Notes);
    }

    [Theory]
    [InlineData("Fragmented Realities", "Deal the Villain Deck into one deck per player, each with 2 Twists")]
    [InlineData("Smash Two Dimensions Together", "Put the Villain Deck on the Bank space")]
    public void Scheme_adds_its_setup_step_and_an_extra_Villain_Group(string scheme, string step)
    {
        var setup = Draw(3, scheme, Madelyne);

        Assert.Equal([step], setup.Steps);
        Assert.Equal(4, setup.VillainGroups.Count);
        Assert.Contains(new RuleNote("Scheme adds 1 Villain Group", "Card", null, SecretWarsName), setup.Notes);
    }

    [Theory]
    [InlineData(Madelyne, "Madelyne Pryor, Goblin Queen", "Limbo")]
    [InlineData(Nimrod, "Nimrod, Super Sentinel", "Sentinel Territories")]
    [InlineData(6, "Wasteland Hulk", "Wasteland")]
    [InlineData(7, "Zombie Green Goblin", "The Deadlands")]
    public void Mastermind_brings_its_Always_Leads_group(int mastermindDraw, string mastermind, string group)
    {
        var setup = Draw(2, "Pan-Dimensional Plague", mastermindDraw);

        Assert.Equal(mastermind, setup.Mastermind.Name);
        Assert.Equal([group, "Brotherhood"], setup.VillainGroups.Select(g => g.Name));
        Assert.Contains(new RuleNote($"{mastermind} always leads {group}", "R p.6", Rulebook, CoreName), setup.Notes);
    }

    // The exclusion case: with every other box of its ruleset included, Secret Wars Volume 1 changes nothing.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Without_Secret_Wars_Volume_1_included_it_changes_no_draw(int players)
    {
        using var withoutIt = new DirectoryWithout("secret-wars-volume-1.json", "secret-wars-volume-2.json");
        var neverLoaded = new SetupGenerator(BoxCatalog.Load(withoutIt.Path));
        string[] boxes = ["core", "dark-city", "fantastic-four", "paint-the-town-red", "guardians-of-the-galaxy"];

        for (var seed = 0; seed < 40; seed++)
        {
            var withItLoaded = Assert.IsType<SetupResult>(Generator.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));
            var expected = Assert.IsType<SetupResult>(neverLoaded.Generate(players, boxes, new CyclingRandom(seed, 3, 1, 4, 1, 5, 9, 2, 6)));

            Assert.Equivalent(expected, withItLoaded, strict: true);
            Assert.DoesNotContain(ComponentIds(withItLoaded), id => id.StartsWith("secret-wars-volume-1_"));
            Assert.Null(withItLoaded.Stacks.Sidekicks);
        }
    }

    private static SetupResult Draw(int players, string scheme, int mastermind)
    {
        var index = (players == 1 ? 6 : 8) + Array.IndexOf(SchemeNames, scheme);
        return Assert.IsType<SetupResult>(Generator.Generate(players, Boxes, new ScriptedRandom(index, mastermind)));
    }

    private static string TermName(string id) =>
        Catalog.Boxes.SelectMany(box => box.Glossary).Single(term => term.Id == id).Name;

    private static IEnumerable<string> ComponentIds(SetupResult setup) =>
        new[] { setup.Scheme.Id, setup.Mastermind.Id }
            .Concat(setup.VillainGroups.Select(group => group.Id))
            .Concat(setup.HenchmanGroups.Select(group => group.Id))
            .Concat(setup.Heroes.Select(hero => hero.Id))
            .Concat(setup.OutsideHeroes.Select(outside => outside.Hero.Id));

    // A copy of the box directory without one box file and the later box that reuses its terms, so a catalog loaded from
    // it has never seen that box.
    private sealed class DirectoryWithout : IDisposable
    {
        public string Path { get; } = Directory.CreateTempSubdirectory("legendary-without-").FullName;

        public DirectoryWithout(string fileName, string dependent)
        {
            foreach (var file in Directory.GetFiles(BoxCatalog.DefaultDirectory, "*.json").Where(file => System.IO.Path.GetFileName(file) != fileName && System.IO.Path.GetFileName(file) != dependent))
            {
                File.Copy(file, System.IO.Path.Combine(Path, System.IO.Path.GetFileName(file)));
            }
        }

        public void Dispose() => Directory.Delete(Path, recursive: true);
    }
}
