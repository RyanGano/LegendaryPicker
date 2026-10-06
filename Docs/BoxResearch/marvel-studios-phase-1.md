# Marvel Studios Phase 1 (August 2018)

**Research status: Partial; integrated (#147).** The official rulebook verifies the base-game setup and 393-card contents. Its contents and setup sections disagree on the Bystander and Master Strike counts; the box file uses the setup numbers (41 and 5). Every Scheme Setup line, Always Leads and part use was read from the C1-linked card faces (`Card`); per-face copy counts remain unverified.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| P1 | [Upper Deck Phase 1 rulebook](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Legendary_Rules-Marvel_Studios_the_First_Ten_Years.pdf) | Player count (PDF p.2), game setup and group counts (PDF pp.6–8), Solo (PDF p.17), contents (PDF p.18), printed Red Skull/Scheme example (PDF p.6). |
| C1 | [master-strike structured marvel-studios-phase-1 card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/marvelstudios.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| AIPT | [AIPT, 3 August 2018](https://aiptcomics.com/2018/08/03/legendary-marvel-studios-phase-1-is-here-for-better-or-worse/) | Release month: the box went on sale at Gen Con in August 2018. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | 2018 release-order position, base-game status, First Edition classification. |

## Catalog inventory

### Official contents (P1, PDF p.18)

| Type | Official count |
|---|---:|
| Heroes | 7 groups × 14 cards = 98 |
| Villain Groups | 5 groups × 8 cards = 40 |
| Henchman Groups | 4 groups × 10 cards = 40 |
| S.H.I.E.L.D. Agents | 48 |
| S.H.I.E.L.D. Troopers | 24 |
| S.H.I.E.L.D. Officers | 30 |
| Bystanders | 30 |
| Special Bystanders | 12 |
| Wounds | 30 |
| Masterminds | 3 sets × 5 cards = 15 |
| Schemes | 8 |
| Scheme Twists | 12 |
| Master Strikes | 6 |
| Total cards | 393 |

The card categories sum to 393. The box also contains a rulebook, game board, and 60 dividers (P1, PDF p.18).

### Group inventory (C1)

- **Heroes (seven):** Black Widow; Captain America; Hawkeye; Hulk; Iron Man; Nick Fury; Thor.
- **Villain Groups (five):** Chitauri; Gamma Hunters; Enemies of Asgard; HYDRA; Iron Foes.
- **Henchman Groups (four):** Hammer Drone Army; HYDRA Pilots; HYDRA Spies; Ten Rings Fanatics.
- **Masterminds (three):** Iron Monger; Loki; Red Skull.
- **Schemes (eight):** Asgard Under Siege; Destroy the Cities of Earth!; Enslave Minds with the Chitauri Scepter; Invade Asgard; Radioactive Palladium Poisoning; Replace Earth's Leaders with HYDRA; Super Hero Civil War; Unleash the Power of the Cosmic Cube.
- **Special Bystander names (C1):** Happy Hogan; Jane Foster; Peggy Carter; Pepper Potts.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. The rulebook's single Red Skull/Scheme example remains separately cited; fields absent from C1 and other printed setup effects remain unverified.

## Rules and setup facts

- **Game and player count (P1, PDF p.2):** First Edition Marvel Legendary base game for 1–5 players.
- **Starting deck (P1, PDF p.6):** Each player starts with 8 S.H.I.E.L.D. Agents and 4 S.H.I.E.L.D. Troopers.
- **Shared stacks (P1, PDF p.6):** Setup says to use 30 S.H.I.E.L.D. Officers, 30 Wounds, and 41 Bystanders.
- **Mastermind and Scheme (P1, PDF p.6):** Randomly choose one Mastermind and one Scheme. The Scheme's printed setup determines its Twist count; other setup instructions are followed before play.
- **Villain Deck group counts (P1, PDF p.7):**

  | Players | Villain Groups | Henchman Groups | Bystanders in Villain Deck |
  |---:|---:|---:|---:|
  | 2 | 2 | 1 | 2 |
  | 3 | 3 | 1 | 8 |
  | 4 | 3 | 2 | 8 |
  | 5 | 4 | 2 | 12 |

- **Hero Deck (P1, PDF p.8):** Use five randomly chosen Heroes (70 cards); with five players, add a sixth Hero (84 cards).
- **Printed card example (P1, PDF p.6):** Red Skull Always Leads HYDRA. *Unleash the Power of the Cosmic Cube* uses eight Twists; its printed progression puts Twists beside the Scheme, gives Wounds on later Twists, and ends evil's victory on Twist 8.
- **Solo setup (P1, PDF p.17):** Use three Heroes (42 cards), ignore Always Leads, use one Villain Group, three Henchman cards from one Henchman Group, one Bystander, one Master Strike, and the Scheme's normal Twist count. Exclude *Super Hero Civil War* and *Asgard Under Siege*. After a Twist, KO an HQ Hero costing 6 or less.
- **Advanced Solo (P1, PDF p.17):** Optional variant uses all five Master Strikes, plays another Villain Deck card after each Strike, applies effects intended for other players to the solo player, and puts a qualifying HQ Hero beneath the Hero Deck after a Twist instead of KO'ing it.
- **Standard Master Strikes (P1, PDF pp.7, 18):** Setup adds five Master Strikes to the Villain Deck, while the contents list includes six; the extra card's use is not explained in these sections.

## Required parts and glossary

- The game uses S.H.I.E.L.D. Officer, Wound, and Bystander stacks; individual card dependencies and Special Bystander inclusion remain to be checked against the cards (P1, PDF pp.6, 18).
- The official contents list has 30 Bystanders and 12 Special Bystanders, but setup calls for 41 Bystanders. Whether one of the 42 listed cards is set aside or the contents/setup figure is erroneous is unresolved.
- **Always Leads:** The Mastermind's named Villain or Henchman Group is included in setup. (P1, PDF p.7)
- **Scheme Twist:** A card added to the Villain Deck in the count printed by the chosen Scheme; its effect follows that Scheme's text. (P1, PDF p.6)

These are original paraphrases. The rulebook's generic terms do not verify all product-specific Hero teams, classes, card terms, or card-to-component dependencies.

## Card-face setup facts (Card, read from the C1 image links)

- **Schemes:** Asgard Under Siege 8 Twists, one extra Henchman Group; Destroy the Cities of Earth! 8 Twists, 12 Bystanders in the Villain Deck; Enslave Minds with the Chitauri Scepter 8 Twists, 6 Heroes, Chitauri required, 12 random Hero Deck cards shuffled into the Villain Deck; Invade Asgard 7 Twists; Radioactive Palladium Poisoning 8 Twists, Wound stack of 6 per player; Replace Earth's Leaders with HYDRA 5 Twists, 3 more beside the Scheme, 18 Bystanders in the Villain Deck; Super Hero Civil War 8 Twists at 2-3 players, 5 at 4-5, 4 Heroes at 2 players; Unleash the Power of the Cosmic Cube 8 Twists.
- **Always Leads:** Iron Monger leads Iron Foes; Loki leads Enemies of Asgard; Red Skull leads HYDRA.
- **Parts used:** Wounds are gained from Hulk (Crazed Rampage), Chitauri Leviathan, Laufey and Frost Giant, Thunderbolt Ross, HYDRA Tank, Whiplash, Iron Monger and Loki (Master Strikes), Radioactive Palladium Poisoning and the Cosmic Cube. S.H.I.E.L.D. Officers are gained from Nick Fury (Battlefield Promotion) and the HYDRA Motorcycle Squad. Happy Hogan only KOs a Wound a player already has.
- **Reprints (#34 D5):** the 7 Heroes, Loki, Red Skull, Enemies of Asgard, HYDRA, Super Hero Civil War and Unleash the Power of the Cosmic Cube reprint core box cards. Their team, classes, keywords, Twists, Setup lines, Always Leads and parts used match the core box file, so the box file lists them under `reprints` as the core cards.
- **Conqueror (P1 PDF p.13):** a Villain or Mastermind with it gets the named bonus while any Villain occupies the named city space.

## Setup and implementation gaps

The official contents/setup Bystander counts (42 listed versus 41 used) and six-versus-five Master Strike counts need reconciliation from the physical cards or another official list. Verify the other seven Schemes' player limits, Twist counts, required groups/Heroes, moves and effects, plus the other Masterminds' Always Leads/setup lines and all Hero metadata. The rulebook does not document mixing this base game with other boxes; do not infer mixing support. The current core runtime record has one generic Bystander count, so the Special Bystander representation also needs review before integration.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Black Widow

- **Dangerous Rescue**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-03-1.png).
- **Mission Accomplished**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Tech; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-04-1.png).
- **Covert Operation**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-02-1.png).
- **Silent Sniper**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-01-1.png).

### Hero group: Captain America

- **Avengers Assemble!**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-03-1.png).
- **Perfect Teamwork**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Strength; printed values: Cost 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-04-1.png).
- **Diving Block**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-02-1.png).
- **A Day Unlike Any Other**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-01-1.png).

### Hero group: Hawkeye

- **Quick Draw**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-03-1.png).
- **Team Player**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-04-1.png).
- **Covering Fire**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-02-1.png).
- **Impossible Trick Shot**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-01-1.png).

### Hero group: Hulk

- **Growing Anger**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-03-1.png).
- **Unstoppable Hulk**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-04-1.png).
- **Crazed Rampage**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-02-1.png).
- **Hulk Smash!**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-01-1.png).

### Hero group: Iron Man

- **Endless Invention**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-04-1.png).
- **Repulsor Rays**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Ranged; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-03-1.png).
- **Arc Reactor**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-02-1.png).
- **Quantum Breakthrough**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-01-1.png).

### Hero group: Nick Fury

- **Battlefield Promotion**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-03-2.png).
- **High-Tech Weaponry**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-04-2.png).
- **Legendary Commander**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 6; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-02-2.png).
- **Pure Fury**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-01-2.png).

### Hero group: Thor

- **Odinson**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Strength; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-03-1.png).
- **Surge of Power**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 4; Recruit 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-04-1.png).
- **Call Lightning**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 6; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-02-1.png).
- **God of Thunder**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 8; Recruit 5; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-01-1.png).

### Villain Group: Chitauri

- **Chitauri Soldier**; type/group: Villain / Chitauri; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/chitauri-03.png).
- **Chitauri Commander**; type/group: Villain / Chitauri; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/chitauri-04.png).
- **Chitauri Leviathan**; type/group: Villain / Chitauri; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/chitauri-01.png).
- **Chitauri Chariot**; type/group: Villain / Chitauri; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/chitauri-02.png).

### Villain Group: Gamma Hunters

- **Sonic Cannon**; type/group: Villain / Gamma Hunters; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/gamma-hunters-01.png).
- **Lt. Gen “Thunderbolt“ Ross**; type/group: Villain / Gamma Hunters; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/gamma-hunters-03.png).
- **Abomination, Raging Monster**; type/group: Villain / Gamma Hunters; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/gamma-hunters-04.png).
- **Fighter Jet**; type/group: Villain / Gamma Hunters; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/gamma-hunters-02.png).

### Villain Group: Enemies of Asgard

- **Destroyer**; type/group: Villain / Enemies of Asgard; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-02-1.png).
- **Enslaved Hawkeye**; type/group: Villain / Enemies of Asgard; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-04-1.png).
- **Frost Giant**; type/group: Villain / Enemies of Asgard; copies: 3; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-03-1.png).
- **Laufey, Frost Giant King**; type/group: Villain / Enemies of Asgard; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-01-1.png).

### Villain Group: HYDRA

- **Endless Armies of HYDRA**; type/group: Villain / HYDRA; copies: 3; printed values: Attack 4; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-03-1.png).
- **HYDRA Motorcycle Squad**; type/group: Villain / HYDRA; copies: 3; printed values: Attack 3; VP 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-04-1.png).
- **Arnim Zola**; type/group: Villain / HYDRA; copies: 1; printed values: Attack 6; VP 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-01-1.png).
- **HYDRA Tank**; type/group: Villain / HYDRA; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-02-1.png).

### Villain Group: Iron Foes

- **Hammer Drone Marine**; type/group: Villain / Iron Foes; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/iron-foes-01.png).
- **Raza, Ten Rings Leader**; type/group: Villain / Iron Foes; copies: 2; printed values: Attack 4+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/iron-foes-02.png).
- **Whiplash**; type/group: Villain / Iron Foes; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/iron-foes-03.png).
- **Justin Hammer**; type/group: Villain / Iron Foes; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/iron-foes-04.png).

### Henchman Group: Hammer Drone Army

- **Hammer Drone Army**; type/group: Henchman / Hammer Drone Army; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/hammer-drone-army.png).

### Henchman Group: HYDRA Pilots

- **HYDRA Pilots**; type/group: Henchman / HYDRA Pilots; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/hydra-pilots.png).

### Henchman Group: HYDRA Spies

- **HYDRA Spies**; type/group: Henchman / HYDRA Spies; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/hydra-spies.png).

### Henchman Group: Ten Rings Fanatics

- **Ten Rings Fanatics**; type/group: Henchman / Ten Rings Fanatics; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/ten-rings-fanatics.png).

### Mastermind: Iron Monger

- **Iron Monger**; type/group: Normal Mastermind face / Iron Monger; copies: Unverified; printed values: Attack 9+; VP 5; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/iron-monger-01.png).
- **Hostile Takeover**; type/group: Mastermind Tactic / Iron Monger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/iron-monger-02.png).
- **Overloaded Arsenal**; type/group: Mastermind Tactic / Iron Monger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/iron-monger-05.png).
- **Sonic Stunner**; type/group: Mastermind Tactic / Iron Monger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/iron-monger-04.png).
- **Unexpected Betrayal**; type/group: Mastermind Tactic / Iron Monger; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/iron-monger-03.png).

### Mastermind: Loki

- **Loki**; type/group: Normal Mastermind face / Loki; copies: Unverified; printed values: VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-01-2.png).
- **Cruel Ruler**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-04-2.png).
- **Maniacal Tyrant**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-03-2.png).
- **Vanishing Illusions**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-02-2.png).
- **Whispers and Lies**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-05-1.png).

### Mastermind: Red Skull

- **Red Skull**; type/group: Normal Mastermind face / Red Skull; copies: Unverified; printed values: VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-01-1.png).
- **Endless Resources**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-05-1.png).
- **HYDRA Conspiracy**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-03-1.png).
- **Negablast Grenades**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-02-1.png).
- **Ruthless Dictator**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-04-1.png).

### Scheme: Asgard Under Siege

- **Asgard Under Siege**; type/group: Scheme / Asgard Under Siege; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-03-11.png).

### Scheme: Destroy the Cities of Earth!

- **Destroy the Cities of Earth!**; type/group: Scheme / Destroy the Cities of Earth!; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-02-11.png).

### Scheme: Enslave Minds with the Chitauri Scepter

- **Enslave Minds with the Chitauri Scepter**; type/group: Scheme / Enslave Minds with the Chitauri Scepter; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-06-5.png).

### Scheme: Invade Asgard

- **Invade Asgard**; type/group: Scheme / Invade Asgard; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-07-5.png).

### Scheme: Radioactive Palladium Poisoning

- **Radioactive Palladium Poisoning**; type/group: Scheme / Radioactive Palladium Poisoning; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-01-11.png).

### Scheme: Replace Earth's Leaders with HYDRA

- **Replace Earth's Leaders with HYDRA**; type/group: Scheme / Replace Earth's Leaders with HYDRA; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-05-5.png).

### Scheme: Super Hero Civil War

- **Super Hero Civil War**; type/group: Scheme / Super Hero Civil War; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-04-11.png).

### Scheme: Unleash the Power of the Cosmic Cube

- **Unleash the Power of the Cosmic Cube**; type/group: Scheme / Unleash the Power of the Cosmic Cube; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-08-5.png).

### Bystander set: Bystander

- **Bystander**; type/group: Bystander / Bystander; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystanders-01.png).

### Bystander set: Happy Hogan

- **Happy Hogan**; type/group: Bystander / Happy Hogan; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-happy-hogan.png).

### Bystander set: Jane Foster

- **Jane Foster**; type/group: Bystander / Jane Foster; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystyander-jane-foster.png).

### Bystander set: Peggy Carter

- **Peggy Carter**; type/group: Bystander / Peggy Carter; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-peggy-carter.png).

### Bystander set: Pepper Potts

- **Pepper Potts**; type/group: Bystander / Pepper Potts; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-pepper-potts.png).

### Wound set: Wound

- **Wound**; type/group: Wound / Wound; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/wound-1.png).

### Officer set: S.H.I.E.L.D. Officer

- **S.H.I.E.L.D. Officer**; type/group: Officer / S.H.I.E.L.D. Officer; copies: 30; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
