# Realm of Kings (October 2020)

**Research status: Partial.** The official insert verifies the 100-card count and several mechanics, but not card-level Scheme setups, Always Leads, or complete card metadata.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| RK | [Upper Deck Realm of Kings rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/RealmOfKings_Rules_Compressed.pdf) | Contents, When Recruited, Throne's Favor, Abomination, Teleport, and group selection (PDF pp.1–2). |
| C1 | [master-strike structured realm-of-kings card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/realmofkings.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | October 2020 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (RK p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Black Bolt; Medusa; Crystal; Karnak; Gorgon.
- **Villain Groups (two):** Inhuman Rebellion; Shi'ar Imperial Elite.
- **Masterminds (two):** Maximus the Mad; Emperor Vulcan of the Shi'ar.
- **Schemes (four):** Ruin the Perfect Wedding; War of Kings; Tornado of Terrigen Mists; Devolve with Xerogen Crystals.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, individual Scheme/Mastermind setup lines, or the precise Abomination comparison icon.

## Rules and mechanisms

- **When Recruited (RK p.1):** Resolve the special ability after paying the cost, moving the Hero to the discard pile, and refilling the HQ. It does not trigger when the card is later played. Gaining or placing a Hero without recruiting it does not trigger the ability; a discounted or free recruit still does.
- **Throne's Favor (RK p.1):** One shared marker represents it. A player or enemy that gains it takes it from whoever currently has it; gaining it is mandatory. Spending it sets it aside, and the effect must be used at the moment the card specifies.
- **Abomination (RK p.2):** A Villain's value depends on the printed value of the Hero in the HQ space below its city space. Gorgon can reference a named city space; Maximus and Gorgon also use the highest printed value among HQ Heroes. The extracted text loses the value icon, so the precise statistic needs card-level verification.
- **Divided-card clarification (RK p.2):** For the insert's printed-value comparisons, add the two printed values on a Divided card when it is not currently being played.
- **Teleport (RK p.2):** Instead of playing the card, set it aside and add it to the new hand at turn end. Other abilities may teleport a card from hand in the same way.
- **Choose a Villain Group (RK p.2):** Choose one specific Villain Group when counting matching Villains in the Victory Pile. A Henchman Group may be chosen, but the generic “Henchmen” category cannot combine multiple groups; a word shared by two group names does not make them one group.

## Required parts and glossary

- **Throne's Favor:** A single shared marker, not a card or stored component; a convenient object can represent it. (RK p.1)
- The insert names no additional shared stack or token supply beyond the Throne's Favor marker.
- **When Recruited:** An ability resolved after the Hero has been recruited and the HQ refilled. (RK p.1)
- **Throne's Favor:** A unique marker that can move among players and enemies and be spent when instructed. (RK p.1)
- **Abomination:** A Villain value that changes with a printed value in the matching HQ space. (RK p.2)
- **Teleport:** Set a card aside to add it to the new hand at turn end. (RK p.2)

Summaries are original paraphrases under 40 words. Hero teams/classes, Always Leads, the exact Abomination value icon, and individual card-linked components need further evidence.

## Setup and implementation gaps

Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps; both Masterminds' Always Leads/setup effects; and full Hero metadata. Integration must track the unique Throne's Favor marker and account for When Recruited abilities without treating them as play effects. Confirm the Abomination comparison field from legible cards; this record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Black Bolt

- **Break the Silence**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Inhumans; class icons: Ranged; printed values: Cost 3; Recruit 2+; keyword labels: “When Recruited“ Abilities, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black_bolt_rok_01.png).
- **Wordless Murmur**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Inhumans; class icons: Ranged; printed values: Cost 5; Recruit 1; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black_bolt_rok_02.png).
- **Declaration of War**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Inhumans; class icons: Tech; printed values: Cost 4; Attack 2; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black_bolt_rok_03.png).
- **The King's Speech**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Inhumans; class icons: Ranged; printed values: Cost 8; Attack 5; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black_bolt_rok_04.png).

### Hero group: Medusa

- **Queen of the Inhumans**; type/group: Hero / Medusa; copies: Unverified; Hero Name: Medusa; team: Inhumans; class icons: Strength; printed values: Cost 2; Attack 1+; keyword labels: “When Recruited“ Abilities, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/medusa_01.png).
- **Splitting Hairs**; type/group: Hero / Medusa; copies: Unverified; Hero Name: Medusa; team: Inhumans; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: “When Recruited“ Abilities, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/medusa_02.png).
- **Royal Command**; type/group: Hero / Medusa; copies: Unverified; Hero Name: Medusa; team: Inhumans; class icons: Instinct; printed values: Cost 5; Attack 3; keyword labels: “When Recruited“ Abilities, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/medusa_03.png).
- **Headstrong**; type/group: Hero / Medusa; copies: Unverified; Hero Name: Medusa; team: Inhumans; class icons: Instinct; printed values: Cost 7; Attack 4; keyword labels: “When Recruited“ Abilities, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/medusa_04.png).

### Hero group: Crystal

- **Earth, Air, Fire, and Water**; type/group: Hero / Crystal; copies: Unverified; Hero Name: Crystal; team: Inhumans; class icons: Ranged; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/crystal_01.png).
- **Master the Four Elements**; type/group: Hero / Crystal; copies: Unverified; Hero Name: Crystal; team: Inhumans; class icons: Instinct; printed values: Cost 4; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/crystal_02.png).
- **Elemental Princess**; type/group: Hero / Crystal; copies: Unverified; Hero Name: Crystal; team: Inhumans; class icons: Covert; printed values: Cost 6; Attack 3; keyword labels: “When Recruited“ Abilities, Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/crystal_03.png).
- **Weave Four Into One**; type/group: Hero / Crystal; copies: Unverified; Hero Name: Crystal; team: Inhumans; class icons: Strength; printed values: Cost 8; Attack 4; keyword labels: “When Recruited“ Abilities; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/crystal_04.png).

### Hero group: Karnak

- **Brilliant Strategist**; type/group: Hero / Karnak; copies: Unverified; Hero Name: Karnak; team: Inhumans; class icons: Covert; printed values: Cost 2; Attack 1; keyword labels: “When Recruited“ Abilities; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karnak_01.png).
- **Find Fatal Flaw**; type/group: Hero / Karnak; copies: Unverified; Hero Name: Karnak; team: Inhumans; class icons: Instinct; printed values: Cost 4; Recruit 0+; keyword labels: “When Recruited“ Abilities; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karnak_02.png).
- **Shatter the Weak Point**; type/group: Hero / Karnak; copies: Unverified; Hero Name: Karnak; team: Inhumans; class icons: Strength; printed values: Cost 5; Attack 0+; keyword labels: “When Recruited“ Abilities; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karnak_03.png).
- **Seek the Center**; type/group: Hero / Karnak; copies: Unverified; Hero Name: Karnak; team: Inhumans; class icons: Covert; printed values: Cost 7; keyword labels: “When Recruited“ Abilities; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karnak_04.png).

### Hero group: Gorgon

- **Lockjaw, Inhuman's Best Friend**; type/group: Hero / Gorgon; copies: Unverified; Hero Name: Gorgon; team: Inhumans; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: “When Recruited“ Abilities, Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gorgon_01.png).
- **Stomping Shockwave**; type/group: Hero / Gorgon; copies: Unverified; Hero Name: Gorgon; team: Inhumans; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: “When Recruited“ Abilities, Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gorgon_02.png).
- **Trample Underhoof**; type/group: Hero / Gorgon; copies: Unverified; Hero Name: Gorgon; team: Inhumans; class icons: Strength; printed values: Cost 6; Attack 1+; keyword labels: “When Recruited“ Abilities, Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gorgon_03.png).
- **Lead the Inhuman Elite**; type/group: Hero / Gorgon; copies: Unverified; Hero Name: Gorgon; team: Inhumans; class icons: Strength; printed values: Cost 8; Attack 4+; keyword labels: “When Recruited“ Abilities, Abomination, Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gorgon_04.png).

### Villain Group: Inhuman Rebellion

- **Lineage**; type/group: Villain / Inhuman Rebellion; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/inhuman_rebellion_02.png).
- **Omega**; type/group: Villain / Inhuman Rebellion; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/inhuman_rebellion_03.png).
- **Lash**; type/group: Villain / Inhuman Rebellion; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/inhuman_rebellion_01.png).
- **The Unspoken**; type/group: Villain / Inhuman Rebellion; copies: 2; printed values: Attack 5+; VP 5; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/inhuman_rebellion_04.png).

### Villain Group: Shi'ar Imperial Elite

- **Plutonia**; type/group: Villain / Shi'ar Imperial Elite; copies: 2; printed values: Attack 4*; VP 2; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar_imperial_elite_03.png).
- **Starbolt**; type/group: Villain / Shi'ar Imperial Elite; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar_imperial_elite_04.png).
- **Mentor**; type/group: Villain / Shi'ar Imperial Elite; copies: 2; printed values: Attack 5; VP 3; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar_imperial_elite_02.png).
- **Gladiator**; type/group: Villain / Shi'ar Imperial Elite; copies: 2; printed values: Attack 7; VP 5; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar_imperial_elite_01.png).

### Mastermind: Maximus the Mad

- **Maximus the Mad**; type/group: Normal Mastermind face / Maximus the Mad; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maximus_01.png).
- **Epic Maximus the Mad**; type/group: Epic Mastermind face / Maximus the Mad; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maximus_02.png).
- **Echo-Tech Chorus Sentries**; type/group: Mastermind Tactic / Maximus the Mad; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maximus_03.png).
- **Sieve of Secrets**; type/group: Mastermind Tactic / Maximus the Mad; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maximus_06.png).
- **Seize the Inhuman Throne**; type/group: Mastermind Tactic / Maximus the Mad; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maximus_04.png).
- **Terrigen Bomb**; type/group: Mastermind Tactic / Maximus the Mad; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maximus_05.png).

### Mastermind: Emperor Vulcan of the Shi'ar

- **Emperor Vulcan of the Shi'ar**; type/group: Normal Mastermind face / Emperor Vulcan of the Shi'ar; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor_vulcan_01.png).
- **Epic Emperor Vulcan**; type/group: Epic Mastermind face / Emperor Vulcan of the Shi'ar; copies: Unverified; printed values: Attack 12+; VP 6; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor_vulcan_02.png).
- **Blast Every Form of Energy**; type/group: Mastermind Tactic / Emperor Vulcan of the Shi'ar; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor_vulcan_03.png).
- **Vast Wealth of the Shi'ar**; type/group: Mastermind Tactic / Emperor Vulcan of the Shi'ar; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor_vulcan_06.png).
- **Contempt for Weakness**; type/group: Mastermind Tactic / Emperor Vulcan of the Shi'ar; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor_vulcan_04.png).
- **Solar Cage**; type/group: Mastermind Tactic / Emperor Vulcan of the Shi'ar; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor_vulcan_05.png).

### Scheme: Ruin the Perfect Wedding

- **Ruin the Perfect Wedding**; type/group: Scheme / Ruin the Perfect Wedding; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/rok_scheme_02.png).

### Scheme: War of Kings

- **War of Kings**; type/group: Scheme / War of Kings; copies: Unverified; printed values: not indexed in C1; keyword labels: Throne's Favor; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/rok_scheme_04.png).

### Scheme: Tornado of Terrigen Mists

- **Tornado of Terrigen Mists**; type/group: Scheme / Tornado of Terrigen Mists; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/rok_scheme_03.png).

### Scheme: Devolve with Xerogen Crystals

- **Devolve with Xerogen Crystals**; type/group: Scheme / Devolve with Xerogen Crystals; copies: Unverified; printed values: not indexed in C1; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/rok_scheme_01.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
