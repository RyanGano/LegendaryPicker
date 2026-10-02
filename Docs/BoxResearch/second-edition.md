# Legendary: Marvel Second Edition (Jun 2026)

**Research status: Partial.** The official rulebook and product listing verify the 550-card inventory, base setup, solo mode, and edition-compatibility rules. The complete card-name lists and card-level Scheme/Mastermind facts are not enumerated in the rulebook.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured second-edition card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/coreset2.ts) | Per-face structured fields are indexed below; C1 provides no direct image URLs for these records, and its ability prose is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| SE | [Upper Deck Second Edition rulebook](https://upperdeck.com/wp-content/uploads/2026/08/Legendary-Second-Edition-Rulebook.pdf) | Edition changes (printed p.1), first-game recipe (p.3), setup (pp.3–7 and Quick Reference Guide), classes/teams and contents (p.22), solo rules (pp.20–21). |
| UD | [Upper Deck Second Edition product listing](https://upperdeckstore.com/legendary-a-marvel-deckbuilding-game-second-edition.html) | Core game identity, 1–5 players, 550 playable cards, 60 dividers, rulebook, keyword guide, and playmat. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, June 2026 date, base-game status, and Second Edition ruleset classification. |

The C1 index below records structured metadata for its catalogued faces but has no direct card-image URLs. It does not replace allowed evidence for complete physical-card verification or setup rules.

## Catalog inventory

### Official contents (SE p.22)

| Type | Official count |
|---|---:|
| Heroes | 15 groups × 14 = 210 |
| Villain Groups | 8 groups × 8 = 64 |
| Henchman Groups | 4 groups × 10 = 40 |
| Double-Sided Epic Masterminds | 5 sets: 5 Mastermind cards + 20 Tactics = 25 |
| S.H.I.E.L.D. Agents | 40 |
| S.H.I.E.L.D. Troopers | 20 |
| S.H.I.E.L.D. Officers | 30: 20 normal + 2 each of 5 color Specialists |
| Daring Sidekicks | 24 |
| Bystanders | 42: 30 normal + 4 each of Experimental Geneticist, Kindly Caretaker, and Police Officer |
| Wounds | 30 |
| Schemes | 9 |
| Scheme Twists | 11 |
| Master Strikes | 5 |
| **Total** | **550** |

Each Hero has 1 rare, 3 uncommons, and 5 copies each of two commons (SE p.22). The official product listing separately lists 60 dividers, one rulebook, one keyword reference guide, and one playmat.

### Names verified in the rulebook

- **Hero groups (14 named of 15):** Cyclops; Emma Frost; Gambit; Rogue; Storm; Wolverine (X-Men); Black Widow; Captain America; Hawkeye; Hulk; Iron Man; Thor (Avengers); Spider-Man (Miles Morales); Spider-Man (Peter Parker) (Spider-Friends). The rulebook also defines S.H.I.E.L.D. as a team but does not name its Hero group in this list (SE p.22).
- **Groups named in the first-game recipe:** HYDRA and Sentinels are used at 1 player (2 Sentinels in the Villain Deck and 2 start in the city); add Sinister Spider-Foes at 2 players, Brotherhood of Mutants at 3, Sinister Syndicate and Hand Ninjas at 4 (all 10 Hand Ninjas), and Radiation at 5 (SE p.3). The book identifies Sinister Syndicate as a new Villain Group (p.1), but does not provide the complete names and type mapping for all eight Villain and four Henchman Groups.
- **Masterminds named in rulebook text:** Red Skull (first-game preset); Doctor Octopus (new playable Mastermind); Doctor Doom; Loki; Magneto. The rulebook verifies five double-sided sets but does not enumerate the complete physical card list or each Always Leads mapping.
- **Scheme named in first-game preset:** Unleash the Power of the Cosmic Cube (SE p.3). The book states that eight earlier Schemes were redeveloped and a ninth was added (p.1), but does not list all nine names.
- **Changes identified by Upper Deck (SE p.1):** Spider-Man (Miles Morales) replaces Deadpool in this core set; Doctor Octopus and Sinister Syndicate are new. About 250 cards were redeveloped from First Edition.

## Setup and base-game rules

Each player shuffles a personal deck of 8 S.H.I.E.L.D. Agents and 4 S.H.I.E.L.D. Troopers, then draws 6. Set out the S.H.I.E.L.D. Officer, Bystander, and Wound Decks. The rulebook also references a Sidekick Deck; the box contains 24 Daring Sidekicks (SE pp.14, 22). Choose a Mastermind and its four Tactics, then a Scheme and its specified Twists. Add five Master Strikes and the groups/Bystanders listed for the player count, including the Mastermind's Always Leads group. The Hero Deck uses the listed number of groups; place five cards in the HQ (SE pp.3–7 and Quick Reference Guide).

| Players | Villain Groups | Henchman Groups | Bystanders | Hero groups |
|---:|---:|---:|---:|---:|
| 1 | 1 | 1* | 1 | 3 |
| 2 | 2 | 1 | 2 | 5 |
| 3 | 3 | 1 | 8 | 5 |
| 4 | 4 | 2 | 8 | 5 |
| 5 | 5 | 2 | 16 | 6 |

For the 1-player row, use only two cards from the selected Henchman Group in the Villain Deck; two more enter the city before the first turn. In 4- and 5-player games, each player's first turn is a warmup without a Villain Deck draw (SE Quick Reference Guide).

**Starting HQ Mulligan (SE p.7):** If at least two starting HQ cards cost 7 or more, players may agree to set aside all HQ cards costing 7 or more, refill those spaces, and shuffle the set-aside cards into the Hero Deck. This is a setup-only option.

**Second Edition Solo Mode (SE pp.20–21):** Use 3 random Heroes (42 cards), 1 Villain Group (ignore Always Leads for selection), 2 Henchmen from one random group, 1 Bystander, 5 Master Strikes, and the Scheme's normal number of Twists. Set aside two more Henchmen from that group; they enter the city before the first turn, and the remaining six are unused. After each Scheme Twist, bottom-deck one HQ Hero costing 6 or less; multiple Twists in the same turn still move only one Hero. There is no extra effect after a Master Strike. In Villain/Mastermind/Tactic “each other player” effects, the solo player performs the action; Hero-card effects are unchanged. Apply Mastermind abilities tied to its usual group to the group actually used. The rulebook also describes solo scoring penalties for played Twists and Villains/Bystanders in the Escape Pile.

## Edition distinction, teams, and glossary

The rulebook says Second Edition replaces the First Edition core's rulebook and cards, warns against randomizing both editions' versions of a character, and describes Second Edition as compatible with all Legendary: A Marvel Deck Building Game expansions (SE p.1). This is documented as its own ruleset in the roadmap; compatibility does not make the First Edition core catalog an accurate Second Edition card list. The existing `core.json` remains the First Edition record.

- **Hero Classes / colors (SE p.22):** Strength (green), Instinct (yellow), Covert (red), Tech (black), and Ranged (blue) are the five Hero Classes; grey cards have no Hero Class.
- **Basic Hero (SE p.22):** A grey Hero with no Hero Class; includes ordinary S.H.I.E.L.D. Agents, Troopers, Officers, and Sidekicks.
- **Teams (SE p.22):** X-Men, Avengers, Spider-Friends, and S.H.I.E.L.D. are Hero teams; team icons can be used where card abilities require them.
- **Hero Name (SE pp.19–20):** Name-based effects compare Hero Names; Spider-Man (Peter Parker) and Spider-Man (Miles Morales) are not the same Hero Name.
- **Epic Mastermind side (SE p.20):** The reverse side of a double-sided Mastermind is a more difficult version with stronger abilities, Master Strikes, and Attack.
- **Special Bystanders and Officer Specialists (SE pp.1, 22):** Special Bystanders are shuffled with normal Bystanders; colored Officer Specialists are mixed into the Officer Deck.

Glossary summaries are original paraphrases under 40 words. The rulebook does not map every Hero's Classes or team icons to its individual cards.

## Setup and implementation gaps

Verify all 15 Hero group identities and metadata; the full eight Villain/four Henchman group inventory; all five Mastermind card names, Always Leads, and setup effects; all nine Scheme names and card-level setups; and complete Special Bystander/Officer Specialist identities and card-driven component dependencies. The rulebook provides the count tables and base setup but not those complete card-level lists. The roadmap's Second Edition classification remains separate from First Edition even though the rulebook states expansion compatibility.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Black Widow

- **Covert Operation**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 2+; card image: unavailable in C1.
- **Gather Intel**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Tech; printed values: Cost 4; Recruit 2; card image: unavailable in C1.
- **Dangerous Rescue**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 2; card image: unavailable in C1.
- **Intelligence Network**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 0+; card image: unavailable in C1.

### Hero group: Captain America

- **Avengers Assemble!**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 1+; card image: unavailable in C1.
- **Perfect Teamwork**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Strength; printed values: Cost 4; Attack 1+; card image: unavailable in C1.
- **Vibranium Shield**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 4; card image: unavailable in C1.
- **A Day Unlike Any Other**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 2+; card image: unavailable in C1.

### Hero group: Cyclops

- **Determination**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Strength; printed values: Cost 2; Recruit 3; card image: unavailable in C1.
- **Optic Blast**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 3; card image: unavailable in C1.
- **Unending Energy**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 6; Attack 4; card image: unavailable in C1.
- **X-Men United**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 6+; card image: unavailable in C1.

### Hero group: Emma Frost

- **Mental Discipline**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 1; card image: unavailable in C1.
- **Shadowed Thoughts**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Covert; printed values: Cost 4; Attack 2+; card image: unavailable in C1.
- **Psychic Link**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Instinct; printed values: Cost 6; Attack 3; card image: unavailable in C1.
- **Diamond Form**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Strength; printed values: Cost 7; Attack 4; card image: unavailable in C1.

### Hero group: Gambit

- **Stack the Deck**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Covert; printed values: Cost 2; card image: unavailable in C1.
- **Kinetic Card**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2; card image: unavailable in C1.
- **Hypnotic Charm**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Covert; printed values: Cost 5; Recruit 3; card image: unavailable in C1.
- **High-Stakes Jackpot**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Instinct; printed values: Cost 7; Attack 4+; card image: unavailable in C1.

### Hero group: Hawkeye

- **Trick Arrow**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 2; Attack 1+; card image: unavailable in C1.
- **Quick Draw**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Ranged; printed values: Cost 3; Attack 1; card image: unavailable in C1.
- **Supporting Fire**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Ranged; printed values: Cost 5; Attack 2; card image: unavailable in C1.
- **Pinpoint Precision**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 5+; card image: unavailable in C1.

### Hero group: Hulk

- **Growing Rage**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 2+; card image: unavailable in C1.
- **Don't Make Me Angry**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2+; card image: unavailable in C1.
- **Crazed Rampage**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 4; card image: unavailable in C1.
- **Hulk Smash!**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 5+; card image: unavailable in C1.

### Hero group: Iron Man

- **Endless Invention**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 3; card image: unavailable in C1.
- **Repulsor Rays**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2+; card image: unavailable in C1.
- **Overloaded Unibeam**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 3+; card image: unavailable in C1.
- **Quantum Breakthrough**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 8; card image: unavailable in C1.

### Hero group: Nick Fury

- **Stealth Assault Squad**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 2; Attack 1+; card image: unavailable in C1.
- **Weapon Bank**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 4; Attack 2+; card image: unavailable in C1.
- **Battlefield Promotion**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 5; Attack 2; card image: unavailable in C1.
- **Pure Fury**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 8; card image: unavailable in C1.

### Hero group: Rogue

- **Borrowed Brawn**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Strength; printed values: Cost 4; Attack 1+; card image: unavailable in C1.
- **Energy Drain**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 1+; card image: unavailable in C1.
- **Stolen Powers**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Covert; printed values: Cost 5; card image: unavailable in C1.
- **Grand Larceny**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Covert; printed values: Cost 7; Attack 5; card image: unavailable in C1.

### Hero group: Spider-Man

- **Witty Banter**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Instinct; printed values: Cost 2; Attack 2; card image: unavailable in C1.
- **Web-Shooters**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 1+; card image: unavailable in C1.
- **Astonishing Strength**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 1; card image: unavailable in C1.
- **With Great Power...**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Instinct; printed values: Cost 2; card image: unavailable in C1.

### Hero group: Spider-Man (Miles Morales)

- **Venom Strike**; type/group: Hero / Spider-Man (Miles Morales); copies: Unverified; Hero Name: Spider-Man (Miles Morales); team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 1+; card image: unavailable in C1.
- **Web-Trap**; type/group: Hero / Spider-Man (Miles Morales); copies: Unverified; Hero Name: Spider-Man (Miles Morales); team: Spider Friends; class icons: Instinct; printed values: Cost 2; Attack 1; card image: unavailable in C1.
- **Spider-Camouflage**; type/group: Hero / Spider-Man (Miles Morales); copies: Unverified; Hero Name: Spider-Man (Miles Morales); team: Spider Friends; class icons: Covert; printed values: Cost 2; Attack 2; card image: unavailable in C1.
- **Jump Dimensions**; type/group: Hero / Spider-Man (Miles Morales); copies: Unverified; Hero Name: Spider-Man (Miles Morales); team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 2; card image: unavailable in C1.

### Hero group: Storm

- **Revitalizing Rain**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Ranged; printed values: Cost 3; Recruit 2+; card image: unavailable in C1.
- **Lightning Bolt**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2+; card image: unavailable in C1.
- **Spinning Cyclone**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 4; card image: unavailable in C1.
- **Tidal Wave**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Ranged; printed values: Cost 7; Attack 4+; card image: unavailable in C1.

### Hero group: Thor

- **Odinson**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Strength; printed values: Cost 5; Recruit 3+; card image: unavailable in C1.
- **Glory of Asgard**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Strength; printed values: Cost 3; Recruit 2; Attack 0+; card image: unavailable in C1.
- **Spark of the Divine**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 6; Recruit 3; Attack 0+; card image: unavailable in C1.
- **God of Thunder**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Strength; printed values: Cost 8; Recruit 5; card image: unavailable in C1.

### Hero group: Wolverine

- **Keen Senses**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 2; Attack 1; card image: unavailable in C1.
- **Healing Factor**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 2; card image: unavailable in C1.
- **Frenzied Slashing**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 6; Attack 2; card image: unavailable in C1.
- **Berserker Rage**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 8; Attack 0+; card image: unavailable in C1.

### Villain Group: Brotherhood of Mutants

- **The Blob**; type/group: Villain / Brotherhood of Mutants; copies: 2; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Mystique**; type/group: Villain / Brotherhood of Mutants; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.
- **Sabretooth**; type/group: Villain / Brotherhood of Mutants; copies: 2; printed values: Attack 6; VP 4; card image: unavailable in C1.
- **Juggernaut**; type/group: Villain / Brotherhood of Mutants; copies: 2; printed values: Attack 7; VP 5; card image: unavailable in C1.

### Villain Group: Enemies of Asgard

- **Frost Giant Warrior**; type/group: Villain / Enemies of Asgard; copies: 3; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Enchantress**; type/group: Villain / Enemies of Asgard; copies: 2; printed values: Attack 5+; VP 4; card image: unavailable in C1.
- **Ymir, Frost Giant King**; type/group: Villain / Enemies of Asgard; copies: 2; printed values: Attack 6; VP 4; card image: unavailable in C1.
- **Destroyer**; type/group: Villain / Enemies of Asgard; copies: 1; printed values: Attack 8; VP 5; card image: unavailable in C1.

### Villain Group: Hydra

- **Hydra Kidnappers**; type/group: Villain / Hydra; copies: 3; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Endless Armies of Hydra**; type/group: Villain / Hydra; copies: 2; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Viper**; type/group: Villain / Hydra; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.
- **Baron Strucker, Supreme Hydra**; type/group: Villain / Hydra; copies: 1; printed values: Attack 6+; VP 4; card image: unavailable in C1.

### Villain Group: Masters of Evil

- **Whirlwind**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Melter**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.
- **Baron Zemo**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 4+; VP 3; card image: unavailable in C1.
- **Ultron**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 5+; VP 4; card image: unavailable in C1.

### Villain Group: Radiation

- **The Leader**; type/group: Villain / Radiation; copies: 2; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Zzzax**; type/group: Villain / Radiation; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.
- **Abomination**; type/group: Villain / Radiation; copies: 2; printed values: Attack 5+; VP 4; card image: unavailable in C1.
- **Maestro, Wasteland Hulk**; type/group: Villain / Radiation; copies: 2; printed values: Attack 6; VP 4; card image: unavailable in C1.

### Villain Group: Sinister Spider-Foes

- **The Lizard**; type/group: Villain / Sinister Spider-Foes; copies: 2; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Scorpion**; type/group: Villain / Sinister Spider-Foes; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.
- **Green Goblin**; type/group: Villain / Sinister Spider-Foes; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.
- **Venom**; type/group: Villain / Sinister Spider-Foes; copies: 2; printed values: Attack 5+; VP 4; card image: unavailable in C1.

### Villain Group: Sinister Syndicate

- **Beetle**; type/group: Villain / Sinister Syndicate; copies: 2; printed values: Attack 6; VP 4; card image: unavailable in C1.
- **Boomerang**; type/group: Villain / Sinister Syndicate; copies: 2; printed values: Attack 4; VP 2; card image: unavailable in C1.
- **Hydro-Man**; type/group: Villain / Sinister Syndicate; copies: 2; printed values: Attack 4+; VP 3; card image: unavailable in C1.
- **Speed Demon**; type/group: Villain / Sinister Syndicate; copies: 2; printed values: Attack 5; VP 3; card image: unavailable in C1.

### Villain Group: Skrulls

- **Skrull Shapeshifter**; type/group: Villain / Skrulls; copies: 3; printed values: Attack 1+; VP 2; card image: unavailable in C1.
- **Super-Skrull**; type/group: Villain / Skrulls; copies: 3; printed values: Attack 2+; VP 3; card image: unavailable in C1.
- **Paibok the Power Skrull**; type/group: Villain / Skrulls; copies: 1; printed values: Attack 5+; VP 5; card image: unavailable in C1.
- **Skrull Queen Veranke**; type/group: Villain / Skrulls; copies: 1; printed values: Attack 2+; VP 5; card image: unavailable in C1.

### Henchman Group: Doombot Legion

- **Doombot Legion**; type/group: Henchman / Doombot Legion; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Henchman Group: Hand Ninjas

- **Hand Ninjas**; type/group: Henchman / Hand Ninjas; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Henchman Group: Savage Land Mutates

- **Savage Land Mutates**; type/group: Henchman / Savage Land Mutates; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Mastermind: Red Skull, Hydra Overlord

- **Red Skull, Hydra Overlord**; type/group: Normal Mastermind face / Red Skull, Hydra Overlord; copies: Unverified; printed values: Attack 7+; VP 6; card image: unavailable in C1.
- **Epic Red Skull, Hydra Overlord**; type/group: Epic Mastermind face / Red Skull, Hydra Overlord; copies: Unverified; printed values: Attack 10+; VP 6; card image: unavailable in C1.
- **Dust of Death**; type/group: Mastermind Tactic / Red Skull, Hydra Overlord; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Ruthless Command**; type/group: Mastermind Tactic / Red Skull, Hydra Overlord; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Two More Shall Take Its Place**; type/group: Mastermind Tactic / Red Skull, Hydra Overlord; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Vast Resources**; type/group: Mastermind Tactic / Red Skull, Hydra Overlord; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Mastermind: Magneto

- **Magneto**; type/group: Normal Mastermind face / Magneto; copies: Unverified; printed values: Attack 9+; VP 6; card image: unavailable in C1.
- **Epic Magneto**; type/group: Epic Mastermind face / Magneto; copies: Unverified; printed values: Attack 11+; VP 6; card image: unavailable in C1.
- **Bitter Captor**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Crush In Steel**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Electromagnetic Shockwave**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Imprisoning Sphere**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Mastermind: Doctor Doom

- **Doctor Doom**; type/group: Normal Mastermind face / Doctor Doom; copies: Unverified; printed values: Attack 10+; VP 6; card image: unavailable in C1.
- **Epic Doctor Doom**; type/group: Epic Mastermind face / Doctor Doom; copies: Unverified; printed values: Attack 12+; VP 6; card image: unavailable in C1.
- **Dark Technology**; type/group: Mastermind Tactic / Doctor Doom; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Monarch's Decree**; type/group: Mastermind Tactic / Doctor Doom; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Secrets of Time Travel**; type/group: Mastermind Tactic / Doctor Doom; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Treasures of Latveria**; type/group: Mastermind Tactic / Doctor Doom; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Mastermind: Loki

- **Loki**; type/group: Normal Mastermind face / Loki; copies: Unverified; printed values: Attack 11+; VP 6; card image: unavailable in C1.
- **Epic Loki**; type/group: Epic Mastermind face / Loki; copies: Unverified; printed values: Attack 12+; VP 6; card image: unavailable in C1.
- **Broken Illusions**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Cruel Manipulations**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Scion of the Frost Giants**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Whispers and Lies**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Mastermind: Doctor Octopus

- **Doctor Octopus**; type/group: Normal Mastermind face / Doctor Octopus; copies: Unverified; printed values: Attack 8+; VP 8; card image: unavailable in C1.
- **Epic Doctor Octopus**; type/group: Epic Mastermind face / Doctor Octopus; copies: Unverified; printed values: Attack 8+; VP 8; card image: unavailable in C1.
- **Absolute Octarchy**; type/group: Mastermind Tactic / Doctor Octopus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **High Octane**; type/group: Mastermind Tactic / Doctor Octopus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Octal Octyls**; type/group: Mastermind Tactic / Doctor Octopus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Octet of Valence Electrons**; type/group: Mastermind Tactic / Doctor Octopus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Bank Robbery Hostage Crisis

- **Bank Robbery Hostage Crisis**; type/group: Scheme / Bank Robbery Hostage Crisis; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Enshrouded Identity

- **Enshrouded Identity**; type/group: Scheme / Enshrouded Identity; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Legacy Virus, The

- **Legacy Virus, The**; type/group: Scheme / Legacy Virus, The; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Negative Zone Prison Breakout

- **Negative Zone Prison Breakout**; type/group: Scheme / Negative Zone Prison Breakout; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Portals to the Dark Dimension

- **Portals to the Dark Dimension**; type/group: Scheme / Portals to the Dark Dimension; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Replace Earth's Leaders with Killbots

- **Replace Earth's Leaders with Killbots**; type/group: Scheme / Replace Earth's Leaders with Killbots; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Secret Invasion of the Skrull Shapeshifters

- **Secret Invasion of the Skrull Shapeshifters**; type/group: Scheme / Secret Invasion of the Skrull Shapeshifters; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Superhero Civil War

- **Superhero Civil War**; type/group: Scheme / Superhero Civil War; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Unleash the Power of the Cosmic Cube

- **Unleash the Power of the Cosmic Cube**; type/group: Scheme / Unleash the Power of the Cosmic Cube; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Bystander set: Bystander

- **Bystander**; type/group: Bystander / Bystander; copies: 30; printed values: not indexed in C1; card image: unavailable in C1.

### Bystander set: Experimental Geneticist

- **Experimental Geneticist**; type/group: Bystander / Experimental Geneticist; copies: 4; printed values: not indexed in C1; card image: unavailable in C1.

### Bystander set: Kindly Caretaker

- **Kindly Caretaker**; type/group: Bystander / Kindly Caretaker; copies: 4; printed values: not indexed in C1; card image: unavailable in C1.

### Bystander set: Police Officer

- **Police Officer**; type/group: Bystander / Police Officer; copies: 4; printed values: not indexed in C1; card image: unavailable in C1.

### Wound set: Wound

- **Wound**; type/group: Wound / Wound; copies: 30; printed values: Cost 0; card image: unavailable in C1.

### Officer set: S.H.I.E.L.D. Officer

- **S.H.I.E.L.D. Officer**; type/group: Officer / S.H.I.E.L.D. Officer; copies: 20; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Covert Specialist

- **Covert Specialist**; type/group: Officer / Covert Specialist; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Instinct Specialist

- **Instinct Specialist**; type/group: Officer / Instinct Specialist; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Ranged Specialist

- **Ranged Specialist**; type/group: Officer / Ranged Specialist; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Strength Specialist

- **Strength Specialist**; type/group: Officer / Strength Specialist; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Officer set: Tech Specialist

- **Tech Specialist**; type/group: Officer / Tech Specialist; copies: 2; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Sidekick set: Daring Sidekick

- **Daring Sidekick**; type/group: Sidekick / Daring Sidekick; copies: 24; printed values: Cost 2; Attack 1; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
