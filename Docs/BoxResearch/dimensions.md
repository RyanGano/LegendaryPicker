# Dimensions (May 2019; includes the 3D promo)

**Research status: Partial.** The official insert verifies the 100-card contents and new mechanics; 3D promo faces are indexed from C1, while the Forklift Driver count discrepancy and card-level setup details remain unresolved.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured dimensions card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/dimensions.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| D | [Upper Deck Dimensions rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Legendary_Rules-Dimensions.pdf) | Contents, 3D-returned cards, Switcheroo, Investigate, and Teleport (PDF pp.1–2). |
| C1-Dim | [master-strike Dimensions card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/dimensions.ts) | Structured per-face titles, group/type, printed metadata, and direct image links where supplied; ability prose is not used as rules evidence. |
| C1-3D | [master-strike Marvel 3D card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/3d.ts) | Structured per-face titles, group/type, printed metadata, and direct image links where supplied; ability prose is not used as rules evidence. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | May 2019 release order, expansion status, inclusion of the 3D promo. D5 treats a reprinted card as one physical card, not two copies. |

## Catalog inventory

### Official contents (D p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Henchman Groups | 2 groups × 10 cards = 20 |
| Double-Sided Mastermind | 1 set × 5 cards = 5 |
| Special Bystanders | 5 |
| Total cards | 100 |

The listed categories sum to the official 100-card total. The insert identifies the returning Marvel 3D content as Howard the Duck and Man-Thing, Spider-Slayers and Circus of Crime, plus five Special Bystanders (D p.1).

### Group inventory (C1)

- **Heroes (five across Dimensions and 3D):** Howard the Duck; Jessica Jones; Man-Thing; Ms. America; Squirrel Girl.
- **Henchman Groups (two):** Circus of Crime; Spider-Slayer.
- **Mastermind:** J. Jonah Jameson.
- **Five Special Bystander names from C1-3D:** Bulldozer Driver; Double Agent of S.H.I.E.L.D.; Fortune Teller; Photographer; Stan Lee.
- **Additional C1-Dim Bystander name lead:** Forklift Driver. It is not reconciled with the insert's five Special Bystanders or D5's 3D-promo identity and is not counted in the official 100-card breakdown.

The official insert does not list individual card names for all categories. The C1 face index below adds the main catalog and 3D promo records, with available metadata and image links; J. Jonah Jameson's setup and the Forklift Driver count discrepancy remain unresolved.

## Rules and mechanisms

- **Switcheroo (D p.1):** Instead of playing the card, reveal it from hand and put it on the bottom of the shared Hero Deck; if done, take an HQ Hero with the specified printed cost into hand. This is not a recruit, gives none of the Switcheroo card's other effects, and cannot target Officers or Sidekicks. Use printed HQ cost despite modifiers.
- **Investigate (D pp.1–2):** Inspect the top two cards of the named deck; draw a qualifying card if found and return the others to the top or bottom in any order. If no match is found, the player may still order the inspected cards.
- **Teleport (D p.2):** Instead of playing a card with this ability, set it aside and add it to the new hand at turn end.
- **Howard the Duck clarification (D p.2):** Some of his cards behave similarly to Investigate, but the insert distinguishes those effects from the named Investigate keyword.

## Required parts and glossary

- Switcheroo takes its target from the shared Hero Deck and the face-up HQ; it is not a recruit. The insert names no additional shared stack or token.
- **Switcheroo:** Trade a card from hand for a qualifying Hero in the HQ, without recruiting it. (D p.1)
- **Investigate:** Inspect two cards from a named deck, select a permitted match, and arrange the rest. (D pp.1–2)
- **Teleport:** Hold a card aside and add it to the new hand at turn end. (D p.2)

Summaries are original paraphrases under 40 words. Hero metadata, exact promo identity, and any card-specific stack use need further evidence.

## Setup and implementation gaps

This product contributes no Villain Groups or Schemes in the official contents list. Verify the J. Jonah Jameson Always Leads/setup text, card-level Hero teams/classes/shared Hero Names, and the exact physical 3D promo against an official card reference. C1's extra Forklift Driver entry must not be added to the 100-card count without resolving its relation to D5 and the five official Special Bystanders. Integration must represent Switcheroo and Teleport without treating them as recruitment; this record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Jessica Jones

- **Alter Ego**; type/group: Hero / Jessica Jones; copies: Unverified; Hero Name: Jessica Jones; team: Marvel Knights; class icons: Strength; printed values: Cost 3; Attack 2; keyword labels: Switcheroo; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jessica_04.png).
- **Alias Investigations**; type/group: Hero / Jessica Jones; copies: Unverified; Hero Name: Jessica Jones; team: Marvel Knights; class icons: Covert; printed values: Cost 4; Attack 1; keyword labels: Switcheroo, Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jessica_03.png).
- **Crack the Case**; type/group: Hero / Jessica Jones; copies: Unverified; Hero Name: Jessica Jones; team: Marvel Knights; class icons: Strength; printed values: Cost 5; Recruit 3; keyword labels: Switcheroo, Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jessica_02.png).
- **Uncover Hidden Evil**; type/group: Hero / Jessica Jones; copies: Unverified; Hero Name: Jessica Jones; team: Marvel Knights; class icons: Covert; printed values: Cost 7; Attack 4; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jessica_01.png).

### Hero group: Ms. America

- **Star Power**; type/group: Hero / Ms. America; copies: Unverified; Hero Name: Ms. America; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 1+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms_america_04.png).
- **Search Parallel Dimensions**; type/group: Hero / Ms. America; copies: Unverified; Hero Name: Ms. America; team: Avengers; class icons: Ranged; printed values: Cost 4; Recruit 2; keyword labels: Investigate, Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms_america_03.png).
- **Kick a Hole in Reality**; type/group: Hero / Ms. America; copies: Unverified; Hero Name: Ms. America; team: Avengers; class icons: Strength; printed values: Cost 6; Attack 3; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms_america_02.png).
- **Hyper-Cosmic Awareness**; type/group: Hero / Ms. America; copies: Unverified; Hero Name: Ms. America; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms_america_01.png).

### Hero group: Squirrel Girl

- **Find Tiny Friends**; type/group: Hero / Squirrel Girl; copies: Unverified; Hero Name: Squirrel Girl; team: Avengers; class icons: Instinct; printed values: Cost 2; keyword labels: Switcheroo, Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/squirrel_04.png).
- **Nut Punch**; type/group: Hero / Squirrel Girl; copies: Unverified; Hero Name: Squirrel Girl; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 1+; keyword labels: Switcheroo; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/squirrel_03.png).
- **Squirrelgility**; type/group: Hero / Squirrel Girl; copies: Unverified; Hero Name: Squirrel Girl; team: Avengers; class icons: Covert; printed values: Cost 4; Attack 2+; keyword labels: Switcheroo; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/squirrel_02.png).
- **Unbeatable Squirrel Girl**; type/group: Hero / Squirrel Girl; copies: Unverified; Hero Name: Squirrel Girl; team: Avengers; class icons: Instinct; printed values: Cost 8; Attack 5; keyword labels: Switcheroo; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/squirrel_01.png).

### Mastermind: J. Jonah Jameson

- **J. Jonah Jameson**; type/group: Normal Mastermind face / J. Jonah Jameson; copies: Unverified; printed values: VP 5; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/jameson_01.png).
- **Epic J. Jonah Jameson**; type/group: Epic Mastermind face / J. Jonah Jameson; copies: Unverified; printed values: Attack 5*; VP 5; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/jameson_02.png).
- **Incite Violent Riots**; type/group: Mastermind Tactic / J. Jonah Jameson; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/jameson_04.png).
- **Promote Spider-Slayer Security**; type/group: Mastermind Tactic / J. Jonah Jameson; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/jameson_05.png).
- **Slanderous Editorial**; type/group: Mastermind Tactic / J. Jonah Jameson; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/jameson_03.png).
- **That Menace Spider-Man!**; type/group: Mastermind Tactic / J. Jonah Jameson; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/jameson_06.png).

### Bystander set: Forklift Driver

- **Forklift Driver**; type/group: Bystander / Forklift Driver; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/forklift-driver.png).

### 3D promo faces (C1)

C1-3D records are indexed separately because the product combines the Dimensions expansion with the returning 3D promo faces.

#### Hero group: Howard the Duck

- **Traveling Companion**; type/group: Hero / Howard the Duck; copies: Unverified; Hero Name: Howard the Duck; team: Unaffiliated; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/howard_04.png).
- **Rebel Without a Cause**; type/group: Hero / Howard the Duck; copies: Unverified; Hero Name: Howard the Duck; team: Unaffiliated; class icons: Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/howard_03.png).
- **Right Place, Wrong Time**; type/group: Hero / Howard the Duck; copies: Unverified; Hero Name: Howard the Duck; team: Unaffiliated; class icons: Instinct; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/howard_02.png).
- **Interplanetary Visitor**; type/group: Hero / Howard the Duck; copies: Unverified; Hero Name: Howard the Duck; team: Unaffiliated; class icons: Tech; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/howard_01.png).

#### Hero group: Man-Thing

- **Form from Ooze**; type/group: Hero / Man-Thing; copies: Unverified; Hero Name: Man-Thing; team: Unaffiliated; class icons: Strength; printed values: Cost 2; Attack 0+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/man_thing_04.png).
- **Burn the Fearful**; type/group: Hero / Man-Thing; copies: Unverified; Hero Name: Man-Thing; team: Unaffiliated; class icons: Instinct; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/man_thing_03.png).
- **Travel the Nexus of Realities**; type/group: Hero / Man-Thing; copies: Unverified; Hero Name: Man-Thing; team: Unaffiliated; class icons: Covert; printed values: Cost 5; Recruit 3; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/man_thing_02.png).
- **Eternity of Solitude**; type/group: Hero / Man-Thing; copies: Unverified; Hero Name: Man-Thing; team: Unaffiliated; class icons: Strength; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/man_thing_01.png).

#### Henchman Group: Circus of Crime

- **Circus of Crime**; type/group: Henchman / Circus of Crime; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/circus_of_crime.png).

#### Henchman Group: Spider-Slayer

- **Spider-Slayer**; type/group: Henchman / Spider-Slayer; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/spider-slayer-1.png).

#### Bystander set: Bulldozer Driver

- **Bulldozer Driver**; type/group: Bystander / Bulldozer Driver; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bulldozer-driver.png).

#### Bystander set: Double Agent of S.H.I.E.L.D.

- **Double Agent of S.H.I.E.L.D.**; type/group: Bystander / Double Agent of S.H.I.E.L.D.; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/double-agent-shield.png).

#### Bystander set: Fortune Teller

- **Fortune Teller**; type/group: Bystander / Fortune Teller; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/fortune-teller.png).

#### Bystander set: Photographer

- **Photographer**; type/group: Bystander / Photographer; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/photographer.png).

#### Bystander set: Stan Lee

- **Stan Lee**; type/group: Bystander / Stan Lee; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Promos/Marvel3D/StanLee.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
