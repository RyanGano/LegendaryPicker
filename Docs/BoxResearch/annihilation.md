# Annihilation (September 2021)

**Research status: Partial.** The official insert verifies the 100-card breakdown and several mechanics, but not card-level Scheme setups, Always Leads, or full Hero metadata.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| AN | [Upper Deck Annihilation rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Annihilation_Rules.pdf) | Contents, Focus, Man/Woman Out of Time, Momentum, Conqueror, and card clarifications (PDF pp.1–2). |
| C1 | [master-strike structured annihilation card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/annihilation.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | September 2021 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (AN p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Brainstorm; Fantastic Four United; Heralds of Galactus; Psi-Lord; Super-Skrull.
- **Villain Groups (two):** Annihilation Wave; Timelines of Kang.
- **Masterminds (two):** Annihilus; Kang the Conqueror.
- **Schemes (four):** Pulse Waves From the Negative Zone; Sneak Attack the Heroes' Homes; Put Humanity on Trial; Breach Parallel Dimensions.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, individual Scheme/Mastermind setup lines, or exact Conqueror/Momentum icons.

## Rules and mechanisms

- **Focus (AN p.1):** Spend the printed Recruit cost to use the paired effect; it may be used repeatedly during the turn while Recruit points remain.
- **Man/Woman Out of Time (AN p.1):** After using the card, set it aside. At the start of the next turn, after the Villain card is played and before the hand, play the card again, then discard it. It cannot be used to replay a copied card and only gives two plays total.
- **Momentum (AN p.1):** A Villain with Momentum gets its printed bonus if it entered any new city space this turn, whether from the Villain Deck, a push, or another effect. Multiple space moves do not multiply the bonus.
- **Mass Momentum (AN p.1):** Annihilus gains the stated amount for each Villain in the city that entered a new space this turn; escaped Villains do not count.
- **Conqueror (AN p.2):** A location-specific Conqueror ability grants its bonus while any Villain is in the named city space.
- **Destroyed city spaces (AN p.2):** Two Galactus cards can destroy city spaces. Treat the space as nonexistent, and Villains pushed past the new final space escape normally.
- **Multiple Villain Decks (AN p.2):** When a card effect refers to the Villain Deck in a setup with multiple such decks, the active player chooses one deck and uses that same deck for all effects on the card.
- **Played-card location clarification (AN p.2):** A card that was played and then moved elsewhere still counts as played that turn for relevant effects, but no longer counts as a Hero in hand or in play and cannot be revealed, KO'd, or moved unless an effect specifically permits it.

## Required parts and glossary

- Man/Woman Out of Time cards need a set-aside area until the following turn.
- **Focus:** Pay Recruit points to use a paired effect, potentially more than once that turn. (AN p.1)
- **Man/Woman Out of Time:** Set aside a played card and play it once more at the next turn's start. (AN p.1)
- **Momentum:** A bonus that applies when a Villain entered a different city space this turn. (AN p.1)
- **Mass Momentum:** A bonus based on the number of city Villains that entered a new space this turn. (AN p.1)
- **Conqueror:** Gain a bonus while a Villain occupies a specified city space. (AN p.2)

Summaries are original paraphrases under 40 words. Hero teams/classes, Always Leads, card-level setup effects, and exact Conqueror/Momentum icons need card-level verification.

## Setup and implementation gaps

Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps; both Masterminds' Always Leads/setup effects; and Hero metadata. Integration must model set-aside cards returning next turn, Villains entering spaces, and city-space destruction; this record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Brainstorm

- **Time Loop Experiments**; type/group: Hero / Brainstorm; copies: Unverified; Hero Name: Brainstorm; team: Fantastic Four; class icons: Tech; printed values: Cost 2; Recruit 1; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brainstorm_01.png).
- **Borrow from the Future**; type/group: Hero / Brainstorm; copies: Unverified; Hero Name: Brainstorm; team: Fantastic Four; class icons: Ranged; printed values: Cost 3; Attack 1; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brainstorm_02.png).
- **Reprogram Doombot Legions**; type/group: Hero / Brainstorm; copies: Unverified; Hero Name: Brainstorm; team: Fantastic Four; class icons: Tech; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brainstorm_03.png).
- **Protégé of Dr. Doom**; type/group: Hero / Brainstorm; copies: Unverified; Hero Name: Brainstorm; team: Fantastic Four; class icons: Tech; printed values: Cost 8; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brainstorm_04.png).

### Hero group: Fantastic Four United

- **Human Torch**; type/group: Hero / Fantastic Four United; copies: Unverified; Hero Name: Fantastic Four United; team: Fantastic Four; class icons: Ranged; printed values: Cost 4; Recruit 2; Attack 0+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ffunited_01.png).
- **Thing**; type/group: Hero / Fantastic Four United; copies: Unverified; Hero Name: Fantastic Four United; team: Fantastic Four; class icons: Strength; printed values: Cost 4; Recruit 2+; Attack 0+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ffunited_02.png).
- **Invisible Woman**; type/group: Hero / Fantastic Four United; copies: Unverified; Hero Name: Fantastic Four United; team: Fantastic Four; class icons: Covert; printed values: Cost 4; Recruit 2+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ffunited_03.png).
- **Mr. Fantastic**; type/group: Hero / Fantastic Four United; copies: Unverified; Hero Name: Fantastic Four United; team: Fantastic Four; class icons: Tech; printed values: Cost 7; Recruit 2; Attack 0+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ffunited_04.png).

### Hero group: Heralds of Galactus

- **Firelord**; type/group: Hero / Heralds of Galactus; copies: Unverified; Hero Name: Heralds of Galactus; team: Unaffiliated; class icons: Ranged; printed values: Cost 3; Attack 2+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hogalactus_01.png).
- **Silver Surfer**; type/group: Hero / Heralds of Galactus; copies: Unverified; Hero Name: Heralds of Galactus; team: Unaffiliated; class icons: Ranged; printed values: Cost 4; Recruit 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hogalactus_02.png).
- **Stardust**; type/group: Hero / Heralds of Galactus; copies: Unverified; Hero Name: Heralds of Galactus; team: Unaffiliated; class icons: Covert; printed values: Cost 6; Recruit 4; Attack 0+; keyword labels: Focus, Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hogalactus_03.png).
- **Galactus Hungers**; type/group: Hero / Heralds of Galactus; copies: Unverified; Hero Name: Heralds of Galactus; team: Unaffiliated; class icons: Ranged; printed values: Cost 10; Attack 8+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hogalactus_04.png).

### Hero group: Psi-Lord

- **Avert Future Tragedy**; type/group: Hero / Psi-Lord; copies: Unverified; Hero Name: Psi-Lord; team: Fantastic Four; class icons: Instinct; printed values: Cost 3; Recruit 2; Attack 0+; keyword labels: Focus, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psilord_01.png).
- **Interdimensional Rescue**; type/group: Hero / Psi-Lord; copies: Unverified; Hero Name: Psi-Lord; team: Fantastic Four; class icons: Covert; printed values: Cost 4; Attack 2+; keyword labels: Focus, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psilord_02.png).
- **Slip the Timestream**; type/group: Hero / Psi-Lord; copies: Unverified; Hero Name: Psi-Lord; team: Fantastic Four; class icons: Covert; printed values: Cost 6; Recruit 3; Attack 0+; keyword labels: Focus, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psilord_03.png).
- **Reshape Reality**; type/group: Hero / Psi-Lord; copies: Unverified; Hero Name: Psi-Lord; team: Fantastic Four; class icons: Instinct; printed values: Cost 7; Recruit 3; Attack 3+; keyword labels: Focus, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psilord_04.png).

### Hero group: Super-Skrull

- **Stretching Credibility**; type/group: Hero / Super-Skrull; copies: Unverified; Hero Name: Super-Skrull; team: Unaffiliated; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sskrull_01.png).
- **Rock Solid**; type/group: Hero / Super-Skrull; copies: Unverified; Hero Name: Super-Skrull; team: Unaffiliated; class icons: Strength; printed values: Cost 4; Recruit 0+; Attack 2+; keyword labels: Conqueror, Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sskrull_02.png).
- **Transparent Motives**; type/group: Hero / Super-Skrull; copies: Unverified; Hero Name: Super-Skrull; team: Unaffiliated; class icons: Covert; printed values: Cost 5; Attack 2+; keyword labels: Conqueror, Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sskrull_03.png).
- **Put to the Torch**; type/group: Hero / Super-Skrull; copies: Unverified; Hero Name: Super-Skrull; team: Unaffiliated; class icons: Ranged; printed values: Cost 7; Attack 4+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sskrull_04.png).

### Villain Group: Annihilation Wave

- **Annihilation Armada**; type/group: Villain / Annihilation Wave; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Momentum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/annihilation_wave_01.png).
- **Queens of Annihilation**; type/group: Villain / Annihilation Wave; copies: 3; printed values: Attack 4+; VP 3; keyword labels: Momentum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/annihilation_wave_02.png).
- **Ravenous**; type/group: Villain / Annihilation Wave; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Momentum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/annihilation_wave_03.png).
- **Weaponized Galactus**; type/group: Villain / Annihilation Wave; copies: 1; printed values: Attack 9+; VP 7; keyword labels: Momentum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/annihilation_wave_04.png).

### Villain Group: Timelines of Kang

- **Immortus**; type/group: Villain / Timelines of Kang; copies: 2; printed values: Attack 5+; VP 5; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/timelines_of_kang_01.png).
- **Iron Lad**; type/group: Trap / Timelines of Kang; copies: 2; printed values: Attack 2; Attack 4; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/timelines_of_kang_02.png).
- **Pharaoh Rama-Tut**; type/group: Villain / Timelines of Kang; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Conqueror, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/timelines_of_kang_03.png).
- **Scarlet Centurion**; type/group: Villain / Timelines of Kang; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Conqueror, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/timelines_of_kang_04.png).

### Mastermind: Annihilus

- **Annihilus**; type/group: Normal Mastermind face / Annihilus; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Momentum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/annihilus_01.png).
- **Epic Annihilus**; type/group: Epic Mastermind face / Annihilus; copies: Unverified; printed values: Attack 12+; VP 6; keyword labels: Momentum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/annihilus_02.png).
- **The Cosmic Control Rod**; type/group: Mastermind Tactic / Annihilus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/annihilus_06.png).
- **Surging Annihilation**; type/group: Mastermind Tactic / Annihilus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/annihilus_05.png).
- **Deploy the Planet Killer**; type/group: Mastermind Tactic / Annihilus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/annihilus_03.png).
- **Pull Into the Negative Zone**; type/group: Mastermind Tactic / Annihilus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/annihilus_04.png).

### Mastermind: Kang the Conqueror

- **Kang the Conqueror**; type/group: Normal Mastermind face / Kang the Conqueror; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kang_01.png).
- **Epic Kang the Conqueror**; type/group: Epic Mastermind face / Kang the Conqueror; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kang_02.png).
- **Iron Lad Grows Up to Become Kang**; type/group: Mastermind Tactic / Kang the Conqueror; copies: Unverified; printed values: not indexed in C1; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kang_03.png).
- **Leap Into the Timestream**; type/group: Mastermind Tactic / Kang the Conqueror; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kang_04.png).
- **Pull From the Future**; type/group: Mastermind Tactic / Kang the Conqueror; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kang_05.png).
- **Savior From Another Timeline**; type/group: Mastermind Tactic / Kang the Conqueror; copies: Unverified; printed values: not indexed in C1; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kang_06.png).

### Scheme: Pulse Waves From the Negative Zone

- **Pulse Waves From the Negative Zone**; type/group: Scheme / Pulse Waves From the Negative Zone; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/annihilation_scheme_02.png).

### Scheme: Sneak Attack the Heroes' Homes

- **Sneak Attack the Heroes' Homes**; type/group: Scheme / Sneak Attack the Heroes' Homes; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/annihilation_scheme_04.png).

### Scheme: Put Humanity on Trial

- **Put Humanity on Trial**; type/group: Scheme / Put Humanity on Trial; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/annihilation_scheme_03.png).

### Scheme: Breach Parallel Dimensions

- **Breach Parallel Dimensions**; type/group: Scheme / Breach Parallel Dimensions; copies: Unverified; printed values: not indexed in C1; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/annihilation_scheme_01.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
