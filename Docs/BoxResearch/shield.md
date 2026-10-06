# S.H.I.E.L.D. (December 2019)

**Research status: Partial.** The official insert verifies contents, the special Officer stack, and several new mechanics. For #172 the Scheme Setup lines, Always Leads and part uses were read from OCR of every C1-linked face and are in `LegendaryPickerService/Data/Boxes/shield.json`; the special Officer faces (no C1 image) and per-face copy counts remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| SH | [Upper Deck S.H.I.E.L.D. rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2019_Marvel_Legendary_SHIELD_Rules_compressed.pdf) | Contents, special Officer cards, Undercover, Levels, Adapting Masterminds, and clarifications (PDF pp.1–2). |
| C1 | [master-strike structured shield card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/shield.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | December 2019 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (SH p.2)

| Type | Official count |
|---|---:|
| Heroes | 4 groups × 14 cards = 56 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Adapting Masterminds | 2 × 4 Tactics = 8 |
| Schemes | 4 |
| Special S.H.I.E.L.D. Officers | 8 types × 2 copies = 16 |
| Total cards | 100 |

The listed categories sum to the official 100-card total. These 16 Officers join the core game's 30-card Officer stack, for 46 total when this expansion is used (SH p.1).

### Group inventory (C1)

- **Heroes (four):** Agent Phil Coulson; Deathlok; Mockingbird; Quake.
- **Villain Groups (two):** A.I.M., Hydra Offshoot; Hydra Elite.
- **Adapting Masterminds (two):** Hydra High Council; Hydra Super-Adaptoid.
- **Schemes (four):** S.H.I.E.L.D. vs. HYDRA War; Hail Hydra; Hydra Helicarriers Hunt Heroes; Secret Empire of Betrayal.
- **Special Officer types (eight):** Dum Dum Dugan; G.W. Bridge; Grant Ward; Leo Fitz & Jemma Simmons; Melinda May; Sharon Carter; Victoria Hand; “Yo-Yo” Rodriguez.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, complete Scheme/Mastermind setup text, or the special Officer class icons lost in text extraction.

## Rules and mechanisms

- **Special S.H.I.E.L.D. Officers (SH p.1):** Shuffle all 16 new cards into the original 30 Officer cards. Keep the 46-card stack face down; recruit or gain its top card. A returned Officer goes to the bottom. These are Heroes with the S.H.I.E.L.D. team and Hero Classes, not grey cards like the core Officer.
- **Undercover (SH p.1):** Put a Hero from hand into the player's Victory Pile for one point. If the played card sends itself Undercover, its other play effects still resolve.
- **S.H.I.E.L.D. Level (SH p.1):** Count S.H.I.E.L.D. and HYDRA cards in the player's Victory Pile. A card's team icon or S.H.I.E.L.D./Hydra name can qualify it; Level checks do not consume those cards.
- **Hydra Level (SH p.2):** Count qualifying S.H.I.E.L.D. and HYDRA cards in the Escape Pile. A card placed there directly increases the Level but is not a Villain escaping from the city and does not trigger the standard escape KO.
- **Adapting Masterminds (SH p.2):** Each has four Tactics and no separate Mastermind card. Keep the Tactics face up; only the top card supplies the current Mastermind's abilities and values. After a Master Strike or Tactic instructs Adapt, shuffle and randomly select a Tactic to place on top.
- **Escaped Villain counts (SH p.2):** Count Villain cards currently in the Escape Pile, including cards that became Villains and escaped. Do not count non-Villain cards placed there directly or Villains that have since left the pile.

The insert's text extraction loses the Hero Class icons on the special Officers; confirm their exact classes from clear product cards.

## Required parts and glossary

- The shared Officer stack grows from 30 to 46 cards with this expansion included (SH p.1). No separate token stack is listed.
- **Undercover:** Move a Hero from hand to the Victory Pile for its point value. (SH p.1)
- **S.H.I.E.L.D. Level:** Count qualifying S.H.I.E.L.D./HYDRA cards in the Victory Pile. (SH p.1)
- **Hydra Level:** Count qualifying S.H.I.E.L.D./HYDRA cards in the Escape Pile. (SH p.2)
- **Adapt:** Shuffle an Adapting Mastermind's remaining Tactics and choose a new active one at random. (SH p.2)

Summaries are original paraphrases under 40 words. The card-level teams/classes, Always Leads, and any further card-driven stack dependencies remain unresolved.

## Setup and implementation gaps

Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, and setup steps; each Adapting Mastermind's Always Leads and start effect; and all Hero metadata. Integration must add the 16 Special Officers only with this box, preserve their Hero identity/classes in the shared stack, and support a Mastermind whose active identity changes among four Tactics. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Agent Phil Coulson

- **Impeccable Planning**; type/group: Hero / Agent Phil Coulson; copies: Unverified; Hero Name: Agent Phil Coulson; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 3; Attack 2; keyword labels: S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-coulson_04.png).
- **Build the Strike Team**; type/group: Hero / Agent Phil Coulson; copies: Unverified; Hero Name: Agent Phil Coulson; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-coulson_03.png).
- **Approve Orbital Strike**; type/group: Hero / Agent Phil Coulson; copies: Unverified; Hero Name: Agent Phil Coulson; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 6; Attack 0+; keyword labels: Undercover, S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-coulson_02.png).
- **Fake But Inspiring Death**; type/group: Hero / Agent Phil Coulson; copies: Unverified; Hero Name: Agent Phil Coulson; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 8; Attack 4+; keyword labels: Undercover, S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-coulson_01.png).

### Hero group: Deathlok

- **Authorize Lethal Force**; type/group: Hero / Deathlok; copies: Unverified; Hero Name: Deathlok; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 2; Attack 0+; keyword labels: S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deathlok_04.png).
- **Reanimate Into Service**; type/group: Hero / Deathlok; copies: Unverified; Hero Name: Deathlok; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 4; Attack 2; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deathlok_03.png).
- **Headlok**; type/group: Hero / Deathlok; copies: Unverified; Hero Name: Deathlok; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 5; Recruit 3; Attack 0+; keyword labels: S.H.I.E.L.D. Level, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deathlok_02.png).
- **Behind Enemy Lines**; type/group: Hero / Deathlok; copies: Unverified; Hero Name: Deathlok; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 8; Recruit 0+; Attack 5; keyword labels: Undercover, S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deathlok_01.png).

### Hero group: Mockingbird

- **Take Cover**; type/group: Hero / Mockingbird; copies: Unverified; Hero Name: Mockingbird; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mockingbird_04.png).
- **Battle Staves**; type/group: Hero / Mockingbird; copies: Unverified; Hero Name: Mockingbird; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 4; Attack 2; keyword labels: S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mockingbird_03.png).
- **Spymaster**; type/group: Hero / Mockingbird; copies: Unverified; Hero Name: Mockingbird; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 5; Attack 1+; keyword labels: Undercover, S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mockingbird_02.png).
- **Infinity Formula**; type/group: Hero / Mockingbird; copies: Unverified; Hero Name: Mockingbird; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Undercover, S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mockingbird_01.png).

### Hero group: Quake

- **Going Underground**; type/group: Hero / Quake; copies: Unverified; Hero Name: Quake; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 3; Recruit 3; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quake_04.png).
- **Aftershock**; type/group: Hero / Quake; copies: Unverified; Hero Name: Quake; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quake_03.png).
- **Tectonic Wave**; type/group: Hero / Quake; copies: Unverified; Hero Name: Quake; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 6; Attack 2+; keyword labels: S.H.I.E.L.D. Level, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quake_02.png).
- **Roil the Earth**; type/group: Hero / Quake; copies: Unverified; Hero Name: Quake; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 7; Attack 0+; keyword labels: Undercover, S.H.I.E.L.D. Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quake_01.png).

### Villain Group: A.I.M., Hydra Offshoot

- **Taskmaster**; type/group: Villain / A.I.M., Hydra Offshoot; copies: 2; printed values: Attack 3; VP 2; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aim_01.png).
- **Superia**; type/group: Villain / A.I.M., Hydra Offshoot; copies: 2; printed values: Attack 5; VP 3; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aim_02.png).
- **Graviton**; type/group: Villain / A.I.M., Hydra Offshoot; copies: 2; printed values: Attack 6; VP 4; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aim_03.png).
- **Mentallo**; type/group: Villain / A.I.M., Hydra Offshoot; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Hydra Level, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aim_04.png).

### Villain Group: Hydra Elite

- **Growing Man**; type/group: Villain / Hydra Elite; copies: 2; printed values: Attack 0+; VP 3; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-elite_01.png).
- **Crossbones**; type/group: Villain / Hydra Elite; copies: 2; printed values: Attack 4; VP 2; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-elite_02.png).
- **Hive**; type/group: Villain / Hydra Elite; copies: 2; printed values: Attack 5; VP 3; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-elite_03.png).
- **Gorgon**; type/group: Villain / Hydra Elite; copies: 2; printed values: Attack 6; VP 4; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-elite_04.png).

### Mastermind: Hydra High Council

- **Red Skull**; type/group: Mastermind Tactic / Hydra High Council; copies: Unverified; printed values: Attack 7+; keyword labels: Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-high-council_04.png).
- **Viper**; type/group: Mastermind Tactic / Hydra High Council; copies: Unverified; printed values: Attack 9+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-high-council_02.png).
- **Arnim Zola**; type/group: Mastermind Tactic / Hydra High Council; copies: Unverified; printed values: Attack 6+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-high-council_03.png).
- **Baron Helmut Zemo**; type/group: Mastermind Tactic / Hydra High Council; copies: Unverified; printed values: Attack 16*; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-high-council_01.png).

### Mastermind: Hydra Super-Adaptoid

- **Black Widow's Bite**; type/group: Mastermind Tactic / Hydra Super-Adaptoid; copies: Unverified; printed values: Attack 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-super-adaptoid_03.png).
- **Captain America's Shield**; type/group: Mastermind Tactic / Hydra Super-Adaptoid; copies: Unverified; printed values: Attack 10; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-super-adaptoid_02.png).
- **Iron Man's Armor**; type/group: Mastermind Tactic / Hydra Super-Adaptoid; copies: Unverified; printed values: Attack 12; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-super-adaptoid_01.png).
- **Thor's Hammer**; type/group: Mastermind Tactic / Hydra Super-Adaptoid; copies: Unverified; printed values: Attack 14; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hydra-super-adaptoid_04.png).

### Scheme: S.H.I.E.L.D. vs. HYDRA War

- **S.H.I.E.L.D. vs. HYDRA War**; type/group: Scheme / S.H.I.E.L.D. vs. HYDRA War; copies: Unverified; printed values: not indexed in C1; keyword labels: Undercover, Hydra Level; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/114Scheme(23).png).

### Scheme: Hail Hydra

- **Hail Hydra**; type/group: Scheme / Hail Hydra; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/112Scheme(21).png).

### Scheme: Hydra Helicarriers Hunt Heroes

- **Hydra Helicarriers Hunt Heroes**; type/group: Scheme / Hydra Helicarriers Hunt Heroes; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/113Scheme(22).png).

### Scheme: Secret Empire of Betrayal

- **Secret Empire of Betrayal**; type/group: Scheme / Secret Empire of Betrayal; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/115Scheme(24).png).

### Officer set: Dum Dum Dugan

- **Dum Dum Dugan**; type/group: Officer / Dum Dum Dugan; copies: 2; printed values: Cost 3; Recruit 2; Attack 1; keyword labels: Undercover; card image: unavailable in C1.

### Officer set: G.W. Bridge

- **G.W. Bridge**; type/group: Officer / G.W. Bridge; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Grant Ward

- **Grant Ward**; type/group: Officer / Grant Ward; copies: 2; printed values: Cost 3; Recruit 2; keyword labels: Undercover; card image: unavailable in C1.

### Officer set: Leo Fitz & Jemma Simmons

- **Leo Fitz & Jemma Simmons**; type/group: Officer / Leo Fitz & Jemma Simmons; copies: 2; printed values: Cost 3; Recruit 0+; Attack 0+; card image: unavailable in C1.

### Officer set: Melinda May

- **Melinda May**; type/group: Officer / Melinda May; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Sharon Carter

- **Sharon Carter**; type/group: Officer / Sharon Carter; copies: 2; printed values: Cost 3; Recruit 2+; keyword labels: Undercover; card image: unavailable in C1.

### Officer set: Victoria Hand

- **Victoria Hand**; type/group: Officer / Victoria Hand; copies: 2; printed values: Cost 3; Recruit 2; keyword labels: Undercover; card image: unavailable in C1.

### Officer set: “Yo-Yo“ Rodriguez

- **“Yo-Yo“ Rodriguez**; type/group: Officer / “Yo-Yo“ Rodriguez; copies: 2; printed values: Cost 3; Recruit 2; keyword labels: Undercover; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
