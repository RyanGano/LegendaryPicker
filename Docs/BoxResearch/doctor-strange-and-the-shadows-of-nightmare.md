# Doctor Strange and the Shadows of Nightmare (March 2022)

**Research status: Partial.** The official insert verifies contents and the Astral Plane, Demonic Bargain, and Artifact rules, but not card-level Scheme setups, Always Leads, or full Hero metadata.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| DS | [Upper Deck Doctor Strange rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Legendary_DoctorStrange_Rulesheet.pdf) | Contents, Demonic Bargain, Astral Plane, Artifacts, and Ritual Artifacts (PDF pp.1–2). |
| C1 | [master-strike structured doctor-strange-and-the-shadows-of-nightmare card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/doctorstrange.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | March 2022 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (DS p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Doctor Strange; Doctor Voodoo; Clea; The Ancient One; The Vishanti.
- **Villain Groups (two):** Fear Lords; Lords of the Netherworld.
- **Masterminds (two):** Nightmare; Dormammu.
- **Schemes (four):** War for the Dream Dimension; Claim Souls for Demons; Cursed Pages of the Darkhold Tome; Duels of Science and Magic.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, individual Scheme/Mastermind setup lines, or card-linked component dependencies.

## Rules and mechanisms

- **Demonic Bargain (DS p.1):** The selected player discards their deck's top card. A cost of 1 or more causes a Wound; a cost of 0 avoids it. Either way, that player receives the listed benefit and cannot decline the bargain.
- **Astral Plane (DS p.1):** A single extra space immediately right of the Villain Deck, not part of the city. A Villain or Nightmare there can be fought using Attack rather than Recruit. When a Villain enters, a Villain already there escapes with the usual consequences. Entering the Astral Plane does not trigger Ambush; movement to or from it requires explicit text. An escaping Nightmare also uses its special Escape ability.
- **Artifacts (DS p.2):** A Hero Artifact is gained to the discard pile, then can be played in front of its owner and remains there after turn end. Controlled Artifacts can count as Heroes for effects that reveal or refer to Heroes, but count as played only on the turn played.
- **Ritual Artifacts (DS p.2):** These follow Artifact rules and have a condition that can be fulfilled during a turn. If fulfilled that turn, the player may discard the Artifact for its listed effect or keep it in play for later.
- **Astral Plane interactions (DS p.2):** Some keyword effects can be used there only when their conditions fit the Plane's combat rules; the insert rules out Piercing Energy and certain mixed Recruit/Attack effects.

## Required parts and glossary

- **Astral Plane:** A distinct space next to the Villain Deck, used only when a card refers to it. It is not a city space. (DS p.1)
- The insert lists no new token or shared card stack; it adds a non-city location and persistent Hero Artifacts.
- **Demonic Bargain:** Discard the top card of a deck to determine whether a Wound is gained, then take the listed benefit. (DS p.1)
- **Artifact:** A persistent card controlled in front of its owner and usable on later turns. (DS p.2)
- **Ritual Artifact:** An Artifact that may be discarded for an effect in a turn when its listed condition is fulfilled. (DS p.2)

Summaries are original paraphrases under 40 words. Hero teams/classes/shared Hero Names, Always Leads, and card-linked component dependencies need card-level verification.

## Setup and implementation gaps

Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps, plus both Masterminds' Always Leads/setup effects and all Hero metadata. Integration must represent the Astral Plane separately from the city and track its entry/escape rules, as well as persistent Ritual Artifacts. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Doctor Strange

- **Wand of Watoomb**; type/group: Hero / Doctor Strange; copies: Unverified; Hero Name: Doctor Strange; team: Avengers; class icons: Ranged; printed values: Cost 3; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ds_dr_strange_01.png).
- **Keeper of the Sanctum**; type/group: Hero / Doctor Strange; copies: Unverified; Hero Name: Doctor Strange; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ds_dr_strange_02.png).
- **Book of Cagliostro**; type/group: Hero / Doctor Strange; copies: Unverified; Hero Name: Doctor Strange; team: Avengers; class icons: Instinct; printed values: Cost 2; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ds_dr_strange_03.png).
- **The Eye of Agamotto**; type/group: Hero / Doctor Strange; copies: Unverified; Hero Name: Doctor Strange; team: Avengers; class icons: Ranged; printed values: Cost 8; keyword labels: Ritual Artifact, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ds_dr_strange_04.png).

### Hero group: Doctor Voodoo

- **Commune with the Spirit World**; type/group: Hero / Doctor Voodoo; copies: Unverified; Hero Name: Doctor Voodoo; team: Avengers; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/doctor_voodoo_01.png).
- **Medallion of Many Loas**; type/group: Hero / Doctor Voodoo; copies: Unverified; Hero Name: Doctor Voodoo; team: Avengers; class icons: Tech; printed values: Cost 4; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/doctor_voodoo_02.png).
- **Staff of Legba**; type/group: Hero / Doctor Voodoo; copies: Unverified; Hero Name: Doctor Voodoo; team: Avengers; class icons: Strength; printed values: Cost 5; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/doctor_voodoo_03.png).
- **Posessed by Brother's Spirit**; type/group: Hero / Doctor Voodoo; copies: Unverified; Hero Name: Doctor Voodoo; team: Avengers; class icons: Instinct; printed values: Cost 7; Recruit 0+; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/doctor_voodoo_04.png).

### Hero group: Clea

- **Prepare Dark Magic**; type/group: Hero / Clea; copies: Unverified; Hero Name: Clea; team: Marvel Knights; class icons: Ranged; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/clea_01.png).
- **Demonic Descendant**; type/group: Hero / Clea; copies: Unverified; Hero Name: Clea; team: Marvel Knights; class icons: Covert; printed values: Cost 4; Recruit 2+; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/clea_02.png).
- **Bind the Dark Dimension**; type/group: Hero / Clea; copies: Unverified; Hero Name: Clea; team: Marvel Knights; class icons: Ranged; printed values: Cost 6; Attack 3; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/clea_03.png).
- **The Purple Gem**; type/group: Hero / Clea; copies: Unverified; Hero Name: Clea; team: Marvel Knights; class icons: Covert; printed values: Cost 7; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/clea_04.png).

### Hero group: Ancient One, The

- **Astral Confrontation**; type/group: Hero / Ancient One, The; copies: Unverified; Hero Name: Ancient One, The; team: Unaffiliated; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ancient_one_01.png).
- **Teachings of Kamar-Taj**; type/group: Hero / Ancient One, The; copies: Unverified; Hero Name: Ancient One, The; team: Unaffiliated; class icons: Instinct; printed values: Cost 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ancient_one_02.png).
- **War of the Mind**; type/group: Hero / Ancient One, The; copies: Unverified; Hero Name: Ancient One, The; team: Unaffiliated; class icons: Covert; printed values: Cost 6; Recruit 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ancient_one_03.png).
- **The Orb of Agamotto**; type/group: Hero / Ancient One, The; copies: Unverified; Hero Name: Ancient One, The; team: Unaffiliated; class icons: Instinct; printed values: Cost 8; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ancient_one_04.png).

### Hero group: Vishanti, The

- **Oshtur**; type/group: Hero / Vishanti, The; copies: Unverified; Hero Name: Vishanti, The; team: Unaffiliated; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vishanti_01.png).
- **Hoggoth**; type/group: Hero / Vishanti, The; copies: Unverified; Hero Name: Vishanti, The; team: Unaffiliated; class icons: Instinct; printed values: Cost 5; Attack 2+; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vishanti_02.png).
- **Agamotto**; type/group: Hero / Vishanti, The; copies: Unverified; Hero Name: Vishanti, The; team: Unaffiliated; class icons: Ranged; printed values: Cost 4; Attack 2; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vishanti_03.png).
- **The Book of the Vishanti**; type/group: Hero / Vishanti, The; copies: Unverified; Hero Name: Vishanti, The; team: Unaffiliated; class icons: Covert; printed values: Cost 7; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vishanti_04.png).

### Villain Group: Fear Lords

- **Nox**; type/group: Villain / Fear Lords; copies: 2; printed values: Attack 4; VP 2; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/fear_lords_03.png).
- **D'Spayre**; type/group: Villain / Fear Lords; copies: 2; printed values: Attack 5; VP 3; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/fear_lords_01.png).
- **Dreamstalker**; type/group: Villain / Fear Lords; copies: 2; printed values: Attack 5; VP 3; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/fear_lords_02.png).
- **The Lurking Unknown**; type/group: Villain / Fear Lords; copies: 2; printed values: Attack 2; VP 3; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/fear_lords_04.png).

### Villain Group: Lords of the Netherworld

- **Mindless Ones**; type/group: Villain / Lords of the Netherworld; copies: 2; printed values: Attack 4; VP 2; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lords_of_the_netherworld_02.png).
- **Baron Mordo**; type/group: Villain / Lords of the Netherworld; copies: 2; printed values: Attack 5; VP 3; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lords_of_the_netherworld_01.png).
- **Satana Hellstrom**; type/group: Villain / Lords of the Netherworld; copies: 2; printed values: Attack 5; VP 3; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lords_of_the_netherworld_03.png).
- **Satannish**; type/group: Villain / Lords of the Netherworld; copies: 1; printed values: Attack 6; VP 4; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lords_of_the_netherworld_04.png).
- **Umar**; type/group: Villain / Lords of the Netherworld; copies: 1; printed values: Attack 7; VP 5; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lords_of_the_netherworld_05.png).

### Mastermind: Nightmare

- **Nightmare**; type/group: Normal Mastermind face / Nightmare; copies: Unverified; printed values: VP 6; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nightmare_01.png).
- **Epic Nightmare**; type/group: Epic Mastermind face / Nightmare; copies: Unverified; printed values: Attack 8; VP 6; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nightmare_02.png).
- **Don't Fall Asleep**; type/group: Mastermind Tactic / Nightmare; copies: Unverified; printed values: not indexed in C1; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nightmare_04.png).
- **Dream Weaver**; type/group: Mastermind Tactic / Nightmare; copies: Unverified; printed values: not indexed in C1; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nightmare_05.png).
- **Night Terrors**; type/group: Mastermind Tactic / Nightmare; copies: Unverified; printed values: not indexed in C1; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nightmare_06.png).
- **Deadly Waking Nightmares**; type/group: Mastermind Tactic / Nightmare; copies: Unverified; printed values: not indexed in C1; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nightmare_03.png).

### Mastermind: Dormammu

- **Dormammu**; type/group: Normal Mastermind face / Dormammu; copies: Unverified; printed values: VP 6; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dormammu_01.png).
- **Epic Dormammu**; type/group: Epic Mastermind face / Dormammu; copies: Unverified; printed values: Attack 13; VP 6; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dormammu_02.png).
- **Demonic Hellfire**; type/group: Mastermind Tactic / Dormammu; copies: Unverified; printed values: not indexed in C1; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dormammu_04.png).
- **Flames of Regency**; type/group: Mastermind Tactic / Dormammu; copies: Unverified; printed values: not indexed in C1; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dormammu_05.png).
- **Barter for Souls**; type/group: Mastermind Tactic / Dormammu; copies: Unverified; printed values: not indexed in C1; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dormammu_03.png).
- **Torments of the Dark Dimension**; type/group: Mastermind Tactic / Dormammu; copies: Unverified; printed values: not indexed in C1; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dormammu_06.png).

### Scheme: War for the Dream Dimension

- **War for the Dream Dimension**; type/group: Scheme / War for the Dream Dimension; copies: Unverified; printed values: not indexed in C1; keyword labels: Astral Plane; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/ds_scheme_04.png).

### Scheme: Claim Souls for Demons

- **Claim Souls for Demons**; type/group: Scheme / Claim Souls for Demons; copies: Unverified; printed values: not indexed in C1; keyword labels: Demonic Bargain; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/ds_scheme_01.png).

### Scheme: Cursed Pages of the Darkhold Tome

- **Cursed Pages of the Darkhold Tome**; type/group: Scheme / Cursed Pages of the Darkhold Tome; copies: Unverified; printed values: not indexed in C1; keyword labels: Ritual Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/ds_scheme_02.png).

### Scheme: Duels of Science and Magic

- **Duels of Science and Magic**; type/group: Scheme / Duels of Science and Magic; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/ds_scheme_03.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
