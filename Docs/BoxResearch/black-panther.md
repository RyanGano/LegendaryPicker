# Black Panther (2022/23)

**Research status: Integrated ([#194](https://github.com/RyanGano/LegendaryPicker/issues/194)); partial.** Scheme Setup lines, Always Leads and part uses were read from OCR of every C1-linked card face (`Card`); Hero teams and classes come from C1. The release month (August 2022) is from the [icv2 announcement](https://icv2.com/articles/news/view/51644/upper-deck-will-release-marvel-legendary-black-panther).

## Sources

| Key | Source | Facts supported |
|---|---|---|
| BP | [Upper Deck Black Panther rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2022-BlackPanther_Rulesheet.pdf) | Contents, Wounds on enemies, Hero Ambush, Multiclass, Empowered, and Throne's Favor (PDF pp.1–2). |
| C1 | [master-strike structured black-panther card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/blackpanther.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (BP p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** King Black Panther; Queen Storm of Wakanda; Princess Shuri; General Okoye; White Wolf.
- **Villain Groups (two):** Killmonger's League; Enemies of Wakanda.
- **Masterminds (two):** Killmonger; Klaw.
- **Schemes (four):** Seize the Wakandan Throne; Poison Lakes with Nanite Microbots; Plunder Wakanda's Vibranium; Provoke a Clash of Nations.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence.

## Rules and mechanisms

- **Hero Ambush (BP p.1):** When a Hero with this ability enters the HQ during the player's turn, its Ambush ability may be used if that player has a Hero in hand, played this turn, or controlled as a Hero Artifact.
- **Wounding enemies (BP p.1):** Put a Wound from the Wound Stack or KO pile onto a Villain; each Wound lowers its value by 1. When it is defeated or leaves the city, return its Wounds to the Wound Stack. Wounded Masterminds return their Wounds after a Tactic is fought.
- **Wound-based Villain fights (BP p.1):** Some Villains cannot be fought until their value is reduced to 0. Their card may let a player spend an amount equal to their current value to place a Wound and gain a bonus; this can be repeated in a turn and is not a Fight or a rescue of captured Bystanders. A Villain at 0 or below remains in play and can be fought for 0, with no refund for negative value.
- **Mixed Wound stacks (BP p.1):** If the Wound Stack includes different Wound types, returned Wounds go to the bottom. Wounds placed on enemies remain face up.
- **Multiclass (BP p.2):** Each Wakandan Hero has a card with multiple Hero Classes; every card for the King Black Panther Hero is Multiclass. Exact icon combinations are not preserved in the insert text.
- **Empowered (BP p.2):** A Hero or enemy gets +1 for each HQ card matching its specified icon/color, checked when the Hero is played or enemy fought. Double or Quadruple Empowered multiply the bonus. Icon extraction does not preserve the matching classes/colors.
- **Throne's Favor (BP p.2):** This is the same single marker used in Realm of Kings. A player or Mastermind gaining it takes it from its current holder; spending it sets it aside and must happen when instructed.

## Required parts and glossary

- Wounds on Villains/Masterminds come from either the Wound Stack or KO pile and return to the Wound Stack when removed (BP p.1).
- The Throne's Favor uses one shared marker across this product and Realm of Kings; it is not a card or deck component (BP p.2).
- **Hero Ambush:** A Hero ability that may trigger when that Hero enters the HQ during your turn. (BP p.1)
- **Multiclass:** A card with multiple Hero Classes that can satisfy the corresponding class requirements. (BP p.2)
- **Empowered:** Gain strength from matching cards in the HQ. (BP p.2)
- **Throne's Favor:** A single marker that can move between players and a Mastermind. (BP p.2)

Summaries are original paraphrases under 40 words. Verify the printed Empowered/Multiclass icons, Hero teams/classes/shared Hero Names, Always Leads, and additional card-linked component dependencies from the cards.

## Setup and implementation gaps

Still open: per-face copy counts for Heroes and Mastermind Tactics (C1 gives none), and the exact Empowered and Multiclass icon combinations printed on each face, which the box file records only as the Hero's class list.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: King Black Panther

- **Unseen Protector**; type/group: Hero / King Black Panther; copies: Unverified; Hero Name: King Black Panther; team: Heroes of Wakanda; class icons: Instinct, Covert; printed values: Cost 2; Recruit 1+; keyword labels: Ambush, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KingBlackPanther_2Common.png).
- **Vibranium Claws**; type/group: Hero / King Black Panther; copies: Unverified; Hero Name: King Black Panther; team: Heroes of Wakanda; class icons: Instinct, Tech; printed values: Cost 4; Attack 2+; keyword labels: Ambush, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KingBlackPanther_3Common.png).
- **Heart-Shaped Herb**; type/group: Hero / King Black Panther; copies: Unverified; Hero Name: King Black Panther; team: Heroes of Wakanda; class icons: Strength, Covert; printed values: Cost 5; Attack 3; keyword labels: Ambush, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KingBlackPanther_4Uncommon.png).
- **Unite the Tribes of Wakanda**; type/group: Hero / King Black Panther; copies: Unverified; Hero Name: King Black Panther; team: Heroes of Wakanda; class icons: Strength, Ranged; printed values: Cost 8; Recruit 0+; Attack 5+; keyword labels: Ambush, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KingBlackPanther_1Rare.png).

### Hero group: Queen Storm of Wakanda

- **Hurricane Winds**; type/group: Hero / Queen Storm of Wakanda; copies: Unverified; Hero Name: Queen Storm of Wakanda; team: Heroes of Wakanda; class icons: Covert; printed values: Cost 3; Recruit 2; Attack 0+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/QueenStormofWakanda_2Common.png).
- **Torrential Downpour**; type/group: Hero / Queen Storm of Wakanda; copies: Unverified; Hero Name: Queen Storm of Wakanda; team: Heroes of Wakanda; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Ambush, Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/QueenStormofWakanda_3Common.png).
- **Forked Lightning**; type/group: Hero / Queen Storm of Wakanda; copies: Unverified; Hero Name: Queen Storm of Wakanda; team: Heroes of Wakanda; class icons: Covert, Ranged; printed values: Cost 6; Attack 3; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/QueenStormofWakanda_4Uncommon.png).
- **Thunderous Tempest**; type/group: Hero / Queen Storm of Wakanda; copies: Unverified; Hero Name: Queen Storm of Wakanda; team: Heroes of Wakanda; class icons: Ranged; printed values: Cost 8; Attack 5+; keyword labels: Throne's Favor, Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/QueenStormofWakanda_1Rare.png).

### Hero group: Princess Shuri

- **Vibranium Experiments**; type/group: Hero / Princess Shuri; copies: Unverified; Hero Name: Princess Shuri; team: Heroes of Wakanda; class icons: Tech; printed values: Cost 2; Attack 0+; keyword labels: Ambush, Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PrincessShuri_2Common.png).
- **Kimoyo Beads**; type/group: Hero / Princess Shuri; copies: Unverified; Hero Name: Princess Shuri; team: Heroes of Wakanda; class icons: Tech, Ranged; printed values: Cost 4; Recruit 0+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PrincessShuri_3Common.png).
- **Shock Net**; type/group: Hero / Princess Shuri; copies: Unverified; Hero Name: Princess Shuri; team: Heroes of Wakanda; class icons: Ranged; printed values: Cost 6; Attack 3+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PrincessShuri_4Uncommon.png).
- **Become the Next Black Panther**; type/group: Hero / Princess Shuri; copies: Unverified; Hero Name: Princess Shuri; team: Heroes of Wakanda; class icons: Instinct; printed values: Cost 7; Recruit 3; Attack 3+; keyword labels: Ambush; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PrincessShuri_1Rare.png).

### Hero group: General Okoye

- **To My Last Breath**; type/group: Hero / General Okoye; copies: Unverified; Hero Name: General Okoye; team: Heroes of Wakanda; class icons: Instinct; printed values: Cost 3; Attack 2+; keyword labels: Ambush; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GeneralOkoye_2Common.png).
- **Lead the Dora Milaje**; type/group: Hero / General Okoye; copies: Unverified; Hero Name: General Okoye; team: Heroes of Wakanda; class icons: Strength, Instinct; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GeneralOkoye_3Common.png).
- **Sovereign Bodyguard**; type/group: Hero / General Okoye; copies: Unverified; Hero Name: General Okoye; team: Heroes of Wakanda; class icons: Strength; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GeneralOkoye_4Uncommon.png).
- **Direct the Agents of Wakanda**; type/group: Hero / General Okoye; copies: Unverified; Hero Name: General Okoye; team: Heroes of Wakanda; class icons: Covert; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GeneralOkoye_1Rare.png).

### Hero group: White Wolf

- **Secret Assignment**; type/group: Hero / White Wolf; copies: Unverified; Hero Name: White Wolf; team: Heroes of Wakanda; class icons: Covert; printed values: Cost 3; Recruit 2+; keyword labels: Ambush; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteWolf_2Common.png).
- **Cloaking Tech Ambush**; type/group: Hero / White Wolf; copies: Unverified; Hero Name: White Wolf; team: Heroes of Wakanda; class icons: Tech; printed values: Cost 4; Attack 2; keyword labels: Ambush; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteWolf_3Common.png).
- **Command the Hatut Zeraze**; type/group: Hero / White Wolf; copies: Unverified; Hero Name: White Wolf; team: Heroes of Wakanda; class icons: Covert, Tech; printed values: Cost 5; Attack 3; keyword labels: Ambush; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteWolf_4Uncommon.png).
- **Reflective Vibranium Armor**; type/group: Hero / White Wolf; copies: Unverified; Hero Name: White Wolf; team: Heroes of Wakanda; class icons: Tech; printed values: Cost 7; Attack 4; keyword labels: Ambush; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteWolf_1Rare.png).

### Villain Group: Killmonger's League

- **Preyy**; type/group: Villain / Killmonger's League; copies: 2; printed values: Attack 3*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/KillmongersLeaguePreyy.png).
- **Malice**; type/group: Villain / Killmonger's League; copies: 2; printed values: Attack 4*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/KillmongersLeagueMalice.png).
- **Baron Macabre**; type/group: Villain / Killmonger's League; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/KillmongersLeagueBaronMacabre.png).
- **Venomm**; type/group: Villain / Killmonger's League; copies: 2; printed values: Attack 9; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/KillmongersLeagueVenomm.png).

### Villain Group: Enemies of Wakanda

- **Nightshade**; type/group: Villain / Enemies of Wakanda; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EnemiesofWakandaNightshade.png).
- **Jakarra**; type/group: Villain / Enemies of Wakanda; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EnemiesofWakandaJakarra.png).
- **Tetu**; type/group: Villain / Enemies of Wakanda; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EnemiesofWakandaTetu.png).
- **Zenzi**; type/group: Villain / Enemies of Wakanda; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EnemiesofWakandaZenzi.png).
- **Reverend Achebe**; type/group: Villain / Enemies of Wakanda; copies: 1; printed values: Attack 6+; VP 5; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EnemiesofWakandaReverendAchebe.png).

### Mastermind: Killmonger

- **Killmonger**; type/group: Normal Mastermind face / Killmonger; copies: Unverified; printed values: VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Killmonger.png).
- **Epic Killmonger**; type/group: Epic Mastermind face / Killmonger; copies: Unverified; printed values: Attack 6*; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Killmonger_Epic.png).
- **A Scar for Every Kill**; type/group: Mastermind Tactic / Killmonger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTactic1.png).
- **Rite of Challenge**; type/group: Mastermind Tactic / Killmonger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTactic3.png).
- **Throw from the Waterfall**; type/group: Mastermind Tactic / Killmonger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTactic4.png).
- **Altar of Resurrection**; type/group: Mastermind Tactic / Killmonger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTactic2.png).

### Mastermind: Klaw

- **Klaw**; type/group: Normal Mastermind face / Klaw; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Klaw.png).
- **Epic Klaw**; type/group: Epic Mastermind face / Klaw; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Klaw_Epic.png).
- **Cohesive Sound Construct**; type/group: Mastermind Tactic / Klaw; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KlawTactic1.png).
- **Convert Matter to Sound**; type/group: Mastermind Tactic / Klaw; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KlawTactic2.png).
- **Ultrasonic Boom**; type/group: Mastermind Tactic / Klaw; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KlawTactic4.png).
- **Cruelty Provokes Resistance**; type/group: Mastermind Tactic / Klaw; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KlawTactic3.png).

### Scheme: Seize the Wakandan Throne

- **Seize the Wakandan Throne**; type/group: Scheme / Seize the Wakandan Throne; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/SeizetheWakandanThrone.png).

### Scheme: Poison Lakes with Nanite Microbots

- **Poison Lakes with Nanite Microbots**; type/group: Scheme / Poison Lakes with Nanite Microbots; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/PoisonLakeswithNaniteMicrobots.png).

### Scheme: Plunder Wakanda's Vibranium

- **Plunder Wakanda's Vibranium**; type/group: Scheme / Plunder Wakanda's Vibranium; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/PlunderWakandasVibranium.png).

### Scheme: Provoke a Clash of Nations

- **Provoke a Clash of Nations**; type/group: Scheme / Provoke a Clash of Nations; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/ProvokeaClashofNations.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
