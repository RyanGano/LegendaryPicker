# Marvel Legendary First Edition core box (Nov 2012)

**Research status: Partial.** The official rulebook supports the product's category counts and base setup. The names, Scheme Twist counts and Setup lines, and Always Leads of the box file were checked against the printed card faces on 2026-10-12 ([#11](https://github.com/RyanGano/LegendaryPicker/issues/11), below); per-card values and copy counts stay open.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| R | [Upper Deck First Edition rulebook](https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf) | Category counts (p.22), standard setup and starting deck (pp.4–6). Archived copy of Upper Deck's rulebook. |
| C1 | [master-strike structured core card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/coreset.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| C2 | [nutki/legendary core set catalog](https://github.com/nutki/legendary/tree/master/texttools/Legendary) | Card and group names/mappings only. |
| #11 | [Physical-card verification issue](https://github.com/RyanGano/LegendaryPicker/issues/11) | Defines the outstanding verification work; not evidence for card facts. |
| Runtime | [`core.json`](../../LegendaryPickerService/Data/Boxes/core.json) | Existing runtime catalog and its cited setup/rules sources; not duplicated here. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release order, product status, and ruleset classification. |

## Verified facts and existing records

The rulebook counts 15 Hero groups of 14 cards, 7 Villain Groups of 8, 4 Henchman Groups of 10, 4 Masterminds, 8 Schemes, 11 Scheme Twists, 5 Master Strikes, 30 Bystanders, 30 Wounds, and 30 S.H.I.E.L.D. Officers (R p.22). The standard setup uses 8 S.H.I.E.L.D. Agents and 4 Troopers per player's starting deck (R pp.4–6). C1 and C2 agree on the names and group mappings recorded in [`Docs/Plan.md`](../Plan.md) and [`core.json`](../../LegendaryPickerService/Data/Boxes/core.json); C1 structured per-face metadata is indexed below; catalog ability prose is not rules evidence.

## Card-by-card catalog

The lists below are C1-listed titles and group mappings, not a verified physical-card manifest. The appended C1 face index records available printed values, Hero/team/class fields, and image links; copy counts absent from C1, card text, and setup/rule facts remain **Unverified**. The First Edition rulebook independently supports only the two card-specific facts called out below.

### Hero faces

- **Black Widow** (Hero): Dangerous Rescue; Mission Accomplished; Covert Operation; Silent Sniper.
- **Captain America** (Hero): Avengers Assemble!; Perfect Teamwork; Diving Block; A Day Unlike Any Other.
- **Cyclops** (Hero): Determination; Optic Blast; Unending Energy; X-Men United.
- **Deadpool** (Hero): Here, Hold This for a Second; Oddball; Hey, Can I Get a Do-Over?; Random Acts of Unkindness.
- **Emma Frost** (Hero): Mental Discipline; Shadowed Thoughts; Psychic Link; Diamond Form.
- **Gambit** (Hero): Card Shark; Stack the Deck; Hypnotic Charm; High Stakes Jackpot.
- **Hawkeye** (Hero): Quick Draw; Team Player; Covering Fire; Impossible Trick Shot.
- **Hulk** (Hero): Growing Anger; Unstoppable Hulk; Crazed Rampage; Hulk Smash!
- **Iron Man** (Hero): Endless Invention; Repulsor Rays; Arc Reactor; Quantum Breakthrough.
- **Nick Fury** (Hero): Battlefield Promotion; High-Tech Weaponry; Legendary Commander; Pure Fury.
- **Rogue** (Hero): Borrowed Brawn; Energy Drain; Copy Powers; Steal Abilities.
- **Spider-Man** (Hero): Astonishing Strength; Great Responsibility; Web-Shooters; The Amazing Spider-Man.
- **Storm** (Hero): Gathering Stormclouds; Lightning Bolt; Spinning Cyclone; Tidal Wave.
- **Thor** (Hero): Odinson; Surge of Power; Call Lightning; God of Thunder.
- **Wolverine** (Hero): Keen Senses; Healing Factor; Frenzied Slashing; Berserker Rage.

### Villain faces

- **Brotherhood** (Villain Group): Blob; Juggernaut; Mystique; Sabretooth.
- **Enemies of Asgard** (Villain Group): Destroyer; Enchantress; Frost Giant; Ymir, Frost Giant King.
- **HYDRA** (Villain Group): Endless Armies of HYDRA; HYDRA Kidnappers; Supreme HYDRA; Viper.
- **Masters of Evil** (Villain Group): Baron Zemo; Melter; Ultron; Whirlwind.
- **Radiation** (Villain Group): Abomination; The Leader; Maestro; Zzzax.
- **Skrulls** (Villain Group): Paibok the Power Skrull; Skrull Queen Veranke; Skrull Shapeshifters; Super-Skrull.
- **Spider-Foes** (Villain Group): Doctor Octopus; Green Goblin; The Lizard; Venom.
- **Henchman Groups**: Doombot Legion; Hand Ninjas; Savage Land Mutates; Sentinel. Individual Henchman faces are included in the appended C1 index where catalogued.

### Mastermind and Tactic faces

- **Dr. Doom** (Mastermind; four Tactics): Dr. Doom; Dark Technology; Monarch's Decree; Secrets of Time Travel; Treasures of Latveria. Always Leads and all per-face values, terms, and abilities are Unverified.
- **Loki** (Mastermind; four Tactics): Loki; Cruel Ruler; Maniacal Tyrant; Vanishing Illusions; Whispers and Lies. Always Leads and all per-face values, terms, and abilities are Unverified.
- **Magneto** (Mastermind; four Tactics): Magneto; Bitter Captor; Crushing Shockwave; Electromagnetic Bubble; Xavier's Nemesis. Always Leads and all per-face values, terms, and abilities are Unverified.
- **Red Skull** (Mastermind; four Tactics): Red Skull; Endless Resources; HYDRA Conspiracy; Negablast Grenades; Ruthless Dictator. The rulebook example says Red Skull Always Leads HYDRA (R p.6); other per-face values, terms, and abilities are Unverified.

### Scheme faces

- **Legacy Virus, The** (C1 catalog form); **Midtown Bank Robbery**; **Negative Zone Prison Breakout**; **Portals to the Dark Dimension**; **Replace Earth's Leaders with Killbots**; **Secret Invasion of the Skrull Shapeshifters**; **Super Hero Civil War**; **Unleash the Power of the Cosmic Cube** (all Scheme faces; setup values remain unverified except where noted).
- **Unleash the Power of the Cosmic Cube:** the rulebook's pictured example specifies eight Twists and a progression that stages Twists beside the Scheme, adds Wounds later, and ends Evil's victory on Twist 8 (R p.6). Other printed values, terms, and abilities remain Unverified.
- For the other seven Schemes, player limits, setup values, required groups/Heroes, moves, terms, and ability summaries remain Unverified.

### Other card types

- **Bystander**, **Wound**, and **S.H.I.E.L.D. Officer** are names in the C1 catalog; the rulebook verifies the shared-stack counts (R p.22). Individual face identities, printed values, terms, and abilities are Unverified. The rulebook setup identifies S.H.I.E.L.D. Agents and Troopers in the starting deck (R pp.4–6); their distinct printed face details are not verified here.

## Card-face check (#11, 2026-10-12)

Method: the shared OCR cache (`_ocr/core.json`, 114 C1-linked faces: Hero, Villain, Henchman, Mastermind and Tactic faces) for titles and Always Leads markers, plus a fresh Windows OCR read of the eight Scheme faces (`Schemes/1Scheme(32)` to `8Scheme(39)`, not in the cache) and the four Mastermind faces, and of one or two faces of each group the cache's title match missed (Captain America, Emma Frost, Nick Fury, Rogue, Spider-Man, Skrulls, Doombot Legion, Hand Ninjas, Savage Land Mutates). The images are the First Edition (2012) faces; no card text is stored.

| Checked | Result |
|---|---|
| 15 Hero, 7 Villain Group and 4 Henchman Group names | Match. The cache's title match covers 17 of the 26 groups; the re-read covers the rest (Villain and Henchman faces print the group, e.g. "Villain - Skrulls"). |
| Scheme names | Match, except that Legacy Virus is printed "The Legacy Virus": the box file's display name now carries the article, as the other boxes' "The ..." titles do (id unchanged). |
| Scheme Twists and Setup lines | All eight match the Schemes table in `Docs/Plan.md`: Legacy Virus 8 Twists, 6 Wounds per player; Midtown Bank Robbery 8, 12 Bystanders; Negative Zone Prison Breakout 8, an extra Henchman Group; Portals to the Dark Dimension 7; Killbots 5, 3 more Twists beside the Scheme, 18 Bystanders; Secret Invasion 8, 6 Heroes, Skrulls required, 12 random Hero cards into the Villain Deck; Super Hero Civil War 8 at 2-3 players and 5 at 4-5, 4 Heroes at 2 players; Cosmic Cube 8. |
| Mastermind Always Leads | Match: Dr. Doom - Doombot Legion, Loki - Enemies of Asgard, Magneto - Brotherhood, Red Skull - HYDRA. |

No Twist count, Setup line or Always Leads differed; the only data change is that display name. Per-card ability text, numeric values, Hero metadata, copy ratios and per-face copy counts remain unchecked.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Black Widow

- **Dangerous Rescue**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-04.png).
- **Mission Accomplished**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Tech; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-03.png).
- **Covert Operation**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-02.png).
- **Silent Sniper**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-widow-01.png).

### Hero group: Captain America

- **Avengers Assemble!**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-04.png).
- **Perfect Teamwork**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Strength; printed values: Cost 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-03.png).
- **Diving Block**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-02.png).
- **A Day Unlike Any Other**; type/group: Hero / Captain America; copies: Unverified; Hero Name: Captain America; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-01.png).

### Hero group: Cyclops

- **Determination**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Strength; printed values: Cost 2; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cyclops-03.png).
- **Optic Blast**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cyclops-04.png).
- **Unending Energy**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cyclops-02.png).
- **X-Men United**; type/group: Hero / Cyclops; copies: Unverified; Hero Name: Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 6+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cyclops-01.png).

### Hero group: Deadpool

- **Here, Hold This for a Second**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Unaffiliated; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-04.png).
- **Oddball**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Unaffiliated; class icons: Covert; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-03.png).
- **Hey, Can I Get a Do-Over?**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Unaffiliated; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-02.png).
- **Random Acts of Unkindness**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Unaffiliated; class icons: Instinct; printed values: Cost 7; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-01.png).

### Hero group: Emma Frost

- **Mental Discipline**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Ranged; printed values: Cost 3; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/emma-frost-03.png).
- **Shadowed Thoughts**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Covert; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/emma-frost-04.png).
- **Psychic Link**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Instinct; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/emma-frost-02.png).
- **Diamond Form**; type/group: Hero / Emma Frost; copies: Unverified; Hero Name: Emma Frost; team: X-Men; class icons: Strength; printed values: Cost 7; Recruit 0+; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/emma-frost-01.png).

### Hero group: Gambit

- **Card Shark**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gambit-03.png).
- **Stack the Deck**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Covert; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gambit-04.png).
- **Hypnotic Charm**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gambit-02.png).
- **High Stakes Jackpot**; type/group: Hero / Gambit; copies: Unverified; Hero Name: Gambit; team: X-Men; class icons: Instinct; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gambit-01.png).

### Hero group: Hawkeye

- **Quick Draw**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-03.png).
- **Team Player**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-04.png).
- **Covering Fire**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-02.png).
- **Impossible Trick Shot**; type/group: Hero / Hawkeye; copies: Unverified; Hero Name: Hawkeye; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hawkeye-01.png).

### Hero group: Hulk

- **Growing Anger**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-03.png).
- **Unstoppable Hulk**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-04.png).
- **Crazed Rampage**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-02.png).
- **Hulk Smash!**; type/group: Hero / Hulk; copies: Unverified; Hero Name: Hulk; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-01.png).

### Hero group: Iron Man

- **Endless Invention**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-04.png).
- **Repulsor Rays**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Ranged; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-03.png).
- **Arc Reactor**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-02.png).
- **Quantum Breakthrough**; type/group: Hero / Iron Man; copies: Unverified; Hero Name: Iron Man; team: Avengers; class icons: Tech; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-01.png).

### Hero group: Nick Fury

- **Battlefield Promotion**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-04.png).
- **High-Tech Weaponry**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-03.png).
- **Legendary Commander**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 6; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-02.png).
- **Pure Fury**; type/group: Hero / Nick Fury; copies: Unverified; Hero Name: Nick Fury; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-01.png).

### Hero group: Rogue

- **Borrowed Brawn**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Strength; printed values: Cost 4; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rogue-03.png).
- **Energy Drain**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rogue-04.png).
- **Copy Powers**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Covert; printed values: Cost 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rogue-02.png).
- **Steal Abilities**; type/group: Hero / Rogue; copies: Unverified; Hero Name: Rogue; team: X-Men; class icons: Strength; printed values: Cost 8; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rogue-01.png).

### Hero group: Spider-Man

- **Astonishing Strength**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Strength; printed values: Cost 2; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-04.png).
- **Great Responsibility**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Instinct; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-03.png).
- **Web-Shooters**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Tech; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-02.png).
- **The Amazing Spider-Man**; type/group: Hero / Spider-Man; copies: Unverified; Hero Name: Spider-Man; team: Spider Friends; class icons: Covert; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-01.png).

### Hero group: Storm

- **Gathering Stormclouds**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Ranged; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/storm-03.png).
- **Lightning Bolt**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/storm-04.png).
- **Spinning Cyclone**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/storm-01.png).
- **Tidal Wave**; type/group: Hero / Storm; copies: Unverified; Hero Name: Storm; team: X-Men; class icons: Ranged; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/storm-02.png).

### Hero group: Thor

- **Odinson**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Strength; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-03.png).
- **Surge of Power**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 4; Recruit 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-04.png).
- **Call Lightning**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 6; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-02.png).
- **God of Thunder**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Avengers; class icons: Ranged; printed values: Cost 8; Recruit 5; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-01.png).

### Hero group: Wolverine

- **Keen Senses**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolverine-04.png).
- **Healing Factor**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolverine-03.png).
- **Frenzied Slashing**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolverine-02.png).
- **Berserker Rage**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 8; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolverine-01.png).

### Villain Group: Brotherhood

- **Blob**; type/group: Villain / Brotherhood; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/brotherhood-04.png).
- **Juggernaut**; type/group: Villain / Brotherhood; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/brotherhood-03.png).
- **Mystique**; type/group: Villain / Brotherhood; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/brotherhood-02.png).
- **Sabretooth**; type/group: Villain / Brotherhood; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/brotherhood-01.png).

### Villain Group: Enemies of Asgard

- **Destroyer**; type/group: Villain / Enemies of Asgard; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-03.png).
- **Enchantress**; type/group: Villain / Enemies of Asgard; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-02.png).
- **Frost Giant**; type/group: Villain / Enemies of Asgard; copies: 3; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-04.png).
- **Ymir, Frost Giant King**; type/group: Villain / Enemies of Asgard; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-01.png).

### Villain Group: HYDRA

- **Endless Armies of HYDRA**; type/group: Villain / HYDRA; copies: 3; printed values: Attack 4; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-02.png).
- **HYDRA Kidnappers**; type/group: Villain / HYDRA; copies: 3; printed values: Attack 3; VP 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-01.png).
- **Supreme HYDRA**; type/group: Villain / HYDRA; copies: 1; printed values: Attack 6; VP 3*; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-03.png).
- **Viper**; type/group: Villain / HYDRA; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-04.png).

### Villain Group: Masters of Evil

- **Baron Zemo**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-02.png).
- **Melter**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-01.png).
- **Ultron**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 6; VP 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-04.png).
- **Whirlwind**; type/group: Villain / Masters of Evil; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-03.png).

### Villain Group: Radiation

- **Abomination**; type/group: Villain / Radiation; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/radiation-04.png).
- **The Leader**; type/group: Villain / Radiation; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/radiation-03.png).
- **Maestro**; type/group: Villain / Radiation; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/radiation-02.png).
- **Zzzax**; type/group: Villain / Radiation; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/radiation-01.png).

### Villain Group: Skrulls

- **Paibok the Power Skrull**; type/group: Villain / Skrulls; copies: 1; printed values: Attack 8; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/skrulls-01.png).
- **Skrull Queen Veranke**; type/group: Villain / Skrulls; copies: 1; printed values: Attack *; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/skrulls-02.png).
- **Skrull Shapeshifters**; type/group: Villain / Skrulls; copies: 3; printed values: Attack *; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/skrulls-03.png).
- **Super-Skrull**; type/group: Villain / Skrulls; copies: 3; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/skrulls-04.png).

### Villain Group: Spider-Foes

- **Doctor Octopus**; type/group: Villain / Spider-Foes; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-foes-01.png).
- **Green Goblin**; type/group: Villain / Spider-Foes; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-foes-03.png).
- **The Lizard**; type/group: Villain / Spider-Foes; copies: 2; printed values: Attack 3; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-foes-02.png).
- **Venom**; type/group: Villain / Spider-Foes; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-foes-04.png).

### Henchman Group: Doombot Legion

- **Doombot Legion**; type/group: Henchman / Doombot Legion; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/doombot-legion.png).

### Henchman Group: Hand Ninjas

- **Hand Ninjas**; type/group: Henchman / Hand Ninjas; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/hand-ninjas.png).

### Henchman Group: Savage Land Mutates

- **Savage Land Mutates**; type/group: Henchman / Savage Land Mutates; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/savage-land-mutants.png).

### Henchman Group: Sentinel

- **Sentinel**; type/group: Henchman / Sentinel; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/sentinel.png).

### Mastermind: Dr. Doom

- **Dr. Doom**; type/group: Normal Mastermind face / Dr. Doom; copies: Unverified; printed values: VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-doom-01.png).
- **Dark Technology**; type/group: Mastermind Tactic / Dr. Doom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-doom-03.png).
- **Monarch's Decree**; type/group: Mastermind Tactic / Dr. Doom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-doom-05.png).
- **Secrets of Time Travel**; type/group: Mastermind Tactic / Dr. Doom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-doom-04.png).
- **Treasures of Latveria**; type/group: Mastermind Tactic / Dr. Doom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-doom-02.png).

### Mastermind: Loki

- **Loki**; type/group: Normal Mastermind face / Loki; copies: Unverified; printed values: VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-01.png).
- **Cruel Ruler**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-02.png).
- **Maniacal Tyrant**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-03.png).
- **Vanishing Illusions**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-04.png).
- **Whispers and Lies**; type/group: Mastermind Tactic / Loki; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-05.png).

### Mastermind: Magneto

- **Magneto**; type/group: Normal Mastermind face / Magneto; copies: Unverified; printed values: VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magneto-01.png).
- **Bitter Captor**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magneto-02.png).
- **Crushing Shockwave**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magneto-03.png).
- **Electromagnetic Bubble**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magneto-04.png).
- **Xavier's Nemesis**; type/group: Mastermind Tactic / Magneto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magneto-05.png).

### Mastermind: Red Skull

- **Red Skull**; type/group: Normal Mastermind face / Red Skull; copies: Unverified; printed values: VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-01.png).
- **Endless Resources**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-03.png).
- **HYDRA Conspiracy**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-05.png).
- **Negablast Grenades**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-04.png).
- **Ruthless Dictator**; type/group: Mastermind Tactic / Red Skull; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-skull-02.png).

### Scheme: Midtown Bank Robbery

- **Midtown Bank Robbery**; type/group: Scheme / Midtown Bank Robbery; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/5Scheme(36).png).

### Scheme: Secret Invasion of the Skrull Shapeshifters

- **Secret Invasion of the Skrull Shapeshifters**; type/group: Scheme / Secret Invasion of the Skrull Shapeshifters; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/2Scheme(33).png).

### Scheme: Legacy Virus, The

- **Legacy Virus, The**; type/group: Scheme / Legacy Virus, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/4Scheme(35).png).

### Scheme: Negative Zone Prison Breakout

- **Negative Zone Prison Breakout**; type/group: Scheme / Negative Zone Prison Breakout; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/8Scheme(39).png).

### Scheme: Portals to the Dark Dimension

- **Portals to the Dark Dimension**; type/group: Scheme / Portals to the Dark Dimension; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/7Scheme(38).png).

### Scheme: Replace Earth's Leaders with Killbots

- **Replace Earth's Leaders with Killbots**; type/group: Scheme / Replace Earth's Leaders with Killbots; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/3Scheme(34).png).

### Scheme: Super Hero Civil War

- **Super Hero Civil War**; type/group: Scheme / Super Hero Civil War; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/1Scheme(32).png).

### Scheme: Unleash the Power of the Cosmic Cube

- **Unleash the Power of the Cosmic Cube**; type/group: Scheme / Unleash the Power of the Cosmic Cube; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/6Scheme(37).png).

### Bystander set: Bystander

- **Bystander**; type/group: Bystander / Bystander; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystanders.png).

### Wound set: Wound

- **Wound**; type/group: Wound / Wound; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/wound.png).

### Officer set: S.H.I.E.L.D. Officer

- **S.H.I.E.L.D. Officer**; type/group: Officer / S.H.I.E.L.D. Officer; copies: 30; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.

## Verified `uses` (#98)

Each card with a `uses` entry in this box's runtime file is listed below with the printed face whose text takes cards from that stack. Every entry was checked against the linked image and none contradicted the data. Source key `Card`.

### Checked against the printed face

- **Deadpool** — Wounds: printed card text on **Random Acts of Unkindness** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-01.png)).
- **Hulk** — Wounds: printed card text on **Unstoppable Hulk** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-04.png)); **Crazed Rampage** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulk-02.png)).
- **Captain America** — Wounds: printed card text on **Diving Block** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-02.png)) (#166).
- **Wolverine** — Wounds: printed card text on **Healing Factor** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolverine-03.png)) (#166).
- **Nick Fury** — S.H.I.E.L.D. Officers: printed card text on **Battlefield Promotion** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nick-fury-04.png)).
- **Brotherhood** — Wounds: printed card text on **Sabretooth** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/brotherhood-01.png)).
- **Enemies of Asgard** — Wounds: printed card text on **Frost Giant** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-04.png)); **Ymir, Frost Giant King** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/enemies-of-asgard-01.png)).
- **Radiation** — Wounds: printed card text on **Zzzax** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/radiation-01.png)).
- **Spider-Foes** — Wounds: printed card text on **The Lizard** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-foes-02.png)); **Venom** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-foes-04.png)).
- **Loki** — Wounds: printed card text on **Loki** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/loki-01.png)).
- **HYDRA** — Wounds, S.H.I.E.L.D. Officers: printed card text on **HYDRA Kidnappers** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-01.png)); **Viper** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hydra-04.png)).
- **Masters of Evil** — Wounds: printed card text on **Ultron** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-04.png)).
- **Magneto** — Wounds: printed card text on **Crushing Shockwave** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magneto-03.png)).
- **Legacy Virus** — Wounds: printed card text on **The Legacy Virus** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/4Scheme(35).png)).
- **Unleash the Power of the Cosmic Cube** — Wounds: printed card text on **Unleash the Power of the Cosmic Cube** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/6Scheme(37).png)).

## Card-face `uses` check (#166, 2026-10-12)

Every Hero, Villain, Henchman, Mastermind and Tactic face was checked for the shared stacks it takes cards from (Wounds, Sidekicks, Officers, Horrors, Bindings, Shards). Method: the shared OCR cache's stack markers per face (`Card`; 114 C1-linked faces in the cache, no card text stored) compared with each card's `uses`; faces the cache could not read were re-read locally. A card whose text only means something when a stack is in play (it KOs a Wound, or reacts to a player gaining one) also brings that stack, and when unsure the part is added (owner ruling, #141, 2026-10-12). Added `uses` wounds, source Card, under that ruling: Captain America (Diving Block, reacts to gaining a Wound) and Wolverine (Healing Factor, KOs a Wound from hand or discard). Faces the cache could not read (Black Widow Mission Accomplished, Captain America A Day Unlike Any Other, Brotherhood Blob) were read by a short-lived reader and name no stack.

## Difficulty data (#264, 2026-10-13)

`LegendaryPickerService/Data/Difficulty/core.json` tags the 60 Hero cards with class, cost and economy, read from the C1 index above (printed values and class icons), and a card with no Attack or Recruit value is `none`. Copies were not read from the box: the C1 catalog has no per-face quantity, so they are assumed 5, 5, 3, 1 by the card image's number (04, 03, 02, 01), which follows rarity for 14 of the 15 Heroes; each Hero's four sum to 14. Storm is the exception: its image numbers put Spinning Cyclone (cost 6, storm-01) as the rare, but its rare is Tidal Wave (cost 7, storm-02), the highest-cost card as for every other core Hero and as the ascending-cost default in #264 gives, so Tidal Wave has 1 copy and Spinning Cyclone 3 (review of #273). Multiclass cards: none found in the core box. No card text was stored. Difficulty: no axis: nothing recorded yet, since this slice tags Hero cards only.

## Difficulty data: opposition (#265, 2026-10-13)

`Data/Difficulty/core.json` (schemaVersion 2) also tags the Masterminds, Villain Groups, Henchman Groups and Schemes. Effects were read from the C2 catalog's ability text for the core set (https://github.com/nutki/legendary/tree/master/texttools/Legendary, the Villains, Henchmen, Masterminds and Schemes files) and recorded only as tags, severities and copy counts, never as text. Copies come from the same files; the Henchman cards carry none (the count is the Villain Deck's henchman cards split over the drawn groups). Severity (1 to 3) and the choice of axis are our judgement, soft until calibration (#268).

Assumptions:

- Master Strikes, Twists, Ambush, Fight and Escape effects that hurt the players are tagged. Tactic Fights that only help the player are not; Magneto's Crushing Shockwave, which hurts the other players, is the one Tactic tagged.
- A "reveal a team or class Hero or suffer X" effect is a `check`; "can't defeat unless you have" is a `gate` (Blob, Venom). Dr. Doom's Strike applies only to a player with exactly 6 cards, so it is a severity 1 check.
- KO of the players' own Heroes is `deck-thinning`; an extra play from the Villain Deck is `escape-pressure`; a Villain that becomes a Twist is `twist-density`; a Hero taken from the HQ is `hq-disruption`; a captured Bystander is `bystander-pressure`.
- Cosmic Cube's Twists only wound from the fifth on, so its `wounds` inflict is severity 2 rather than 3.
- A Scheme's `amplifies` follows its loss condition: Wounds (Legacy Virus), Bystanders (Midtown Bank Robbery), escaped Villains (Negative Zone, Killbots, Secret Invasion), the Twist count (Portals, Cosmic Cube) and the Hero Deck running out (Civil War, `hq-disruption`).

Difficulty: no axis (cards tagged only in part or not at all):

- Enchantress, HYDRA Kidnappers, Supreme HYDRA, Baron Zemo, Melter, Abomination, Paibok, Doctor Octopus, Doombot Legion, Hand Ninjas and Savage Land Mutates: Fight effects that only help the defeating player.
- Whirlwind, The Lizard: effects that depend on which city space the player chooses to fight in, so the players can avoid them.
- Ultron's and Supreme HYDRA's Victory Point bonuses, and Skrull Queen Veranke's and Shapeshifters' Attack equal to a Hero's cost (only the HQ loss is tagged): scoring and Attack changes with no axis.
- Dr. Doom's Tactics and Loki's, Red Skull's and Magneto's other Tactics: bonuses for the player, or effects on other players that do not change a team's chance of winning.

## Difficulty data: Hero wants and answers (#266, 2026-10-13)

`Data/Difficulty/core.json` (now schemaVersion 4) tags 49 of the 60 Hero cards with `wants`, `answers`, `selfCost` and `draw`. Effects were read from the C2 catalog's ability text for the Heroes (https://github.com/nutki/legendary/tree/master/texttools/Legendary, `Heroes_and_Allies.txt`) and recorded only as tags, never as text; the same file's copy counts agree with the copies already in the data. Strengths (1 to 3) and the choice of kind are our judgement, soft until calibration (#268).

Assumptions:

- A `{POWER class}` ability is a `class` want of strength 1, and 2 where the bonus is large (Hulk Smash!, Arc Reactor, Borrowed Brawn, Call Lightning). A `{TEAMPOWER}` ability, or a card that counts teammates (A Day Unlike Any Other, X-Men United, Legendary Commander), is a `team` want of strength 1 to 3 by how much it pays.
- "For each color of Hero you have" is a `diversity` want (Captain America's two commons at 2; Rogue's Copy Powers at 1). Cards that use a Wound in hand (Unstoppable Hulk, Healing Factor) want the `wounds` stack, and answer `wounds` because they KO one or negate one (Diving Block). Covert Operation wants `bystanders`, which is not scored (Bystanders can always be rescued).
- Rescuing or protecting Bystanders (Black Widow's cards, Impossible Trick Shot, Web-Shooters, Spinning Cyclone) answers `bystanders`. Spinning Cyclone's Villain move also answers `villain-flow`. Lightning Bolt and Tidal Wave answer `attack-inflation` (they lower a Villain's Attack).
- `selfCost`: Cyclops's two commons need a discard to play; Crazed Rampage gives a Wound. Deadpool's Do-Over and Random Acts of Unkindness are optional effects, so they carry no `selfCost` (#268).
- `draw` is cards drawn outright, not under a Power or a condition: Mission Accomplished, Mental Discipline, Quick Draw and Endless Invention 1; Stack the Deck and Quantum Breakthrough 2; Berserker Rage 3.

Difficulty: no axis (cards left with no tag, or tagged only in part):

- Unending Energy, Astonishing Strength, Great Responsibility and The Amazing Spider-Man (they draw only a card that costs 2 or less, which depends on costs rather than teams or classes), Surge of Power and God of Thunder (Recruit and Attack conversions), Steal Abilities, Oddball (an odd-cost bonus), Diamond Form, High Stakes Jackpot, Hey, Can I Get a Do-Over? (a hand reset, only the discard is tagged).
- Here, Hold This for a Second (a Villain captures a Bystander): a cost to the players with no `selfCost` value.
- Shadowed Thoughts plays the top of the Villain Deck, a risk its class want does not cover. Pure Fury's KO-pile count is tagged only as a S.H.I.E.L.D. team want.
