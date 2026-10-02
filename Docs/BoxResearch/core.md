# Marvel Legendary First Edition core box (Nov 2012)

**Research status: Partial.** The official rulebook supports the product's category counts and base setup. The card-by-card check in [#11](https://github.com/RyanGano/LegendaryPicker/issues/11) is not complete: no original cards or clear card-front scans were available in the sources reviewed for this pass.

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

## Open verification gap

The rulebook's contents list does not verify the printed identities or card text. The C1 title index above is not a substitute for checking First Edition core cards or clear scans; per-card ability text, numeric values, Hero metadata, copy ratios, and the still-unknown setup lines and Always Leads mappings remain open. Agreement between C1 and C2, or the current runtime JSON, is not a substitute for those cards. Keep [#11](https://github.com/RyanGano/LegendaryPicker/issues/11) open until that comparison is documented; no runtime catalog changes are made here.

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
