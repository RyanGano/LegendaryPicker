# Revelations (August 2019)

**Research status: Partial.** The official insert verifies the 200-card breakdown and new rules. For #170 the Scheme Setup lines (both sides), Always Leads and part uses were read from OCR of every C1-linked face and are in `LegendaryPickerService/Data/Boxes/revelations.json`; per-face copy counts and printed Hyperspeed icons remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| R | [Upper Deck Revelations rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2019_Marvel_Legendary_Revelations_Rules_compressed.pdf) | Contents (PDF p.2), mechanics and clarifications (PDF pp.1–2). |
| C1 | [master-strike structured revelations card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/revelations.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | August 2019 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (R p.2)

| Type | Official count |
|---|---:|
| Heroes | 9 groups × 14 cards = 126 |
| Villain Groups | 4 groups × 8 cards = 32 |
| Henchman Groups | 2 groups × 10 cards = 20 |
| Double-Sided Epic Masterminds | 3 sets × 5 cards = 15 |
| Double-Sided Transforming Schemes | 4 |
| Special Bystanders | 3 types × 1 card = 3 |
| Total cards | 200 |

The listed categories sum to the official 200-card total.

### Group inventory (C1)

- **Heroes (nine):** Captain Marvel, Agent of S.H.I.E.L.D.; Darkhawk; Hellcat; Photon; Quicksilver; Ronin; Scarlet Witch; Speed; War Machine.
- **Villain Groups (four):** Army of Evil; Dark Avengers; Hood's Gang; Lethal Legion.
- **Henchman Groups (two):** HYDRA Base; Mandarin's Rings.
- **Masterminds (three):** Grim Reaper; The Hood; Mandarin.
- **Schemes (four):** Earthquake Drains the Ocean; House of M; Secret HYDRA Corruption; The Korvac Saga.
- **Special Bystanders (three):** Dog Show Judge; Lawyer; Rocket Test Pilot.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, individual Scheme/Mastermind setup lines, or the icon shapes lost in text extraction.

## Rules and mechanisms

- **Hyperspeed (R p.1):** Reveal the specified number of cards from your deck, gain +1 for each card with the required icon, then discard the revealed cards. Cards can specify multiple icons; the extracted insert text loses those icon shapes.
- **Dark Memories (R p.1):** Gain +1 for each distinct Hero Class represented in the player's discard pile, not for each card; grey Heroes have no class and do not add to the bonus.
- **Last Stand (R pp.1–2):** Villains, Masterminds, or Heroes gain +1 for each empty city space. A city space without a Villain is empty even when a Location is above it; a city space removed by another effect is not empty.
- **Locations (R pp.1–2):** A new card type. Each Villain Group contains at least one. A Location played from the Villain Deck attaches above the nearest city space without one; it stays there, does not move Villains, and can be fought for its listed fight amount and Fight effect. Locations are not Villains. If every city space already has a Location, the insert directs the player to KO one with the lowest value; the extracted text loses the comparison icon, so its exact field needs card-level verification.
- **Location/Mastermind clarification (R p.2):** Each Mastermind has at least one Tactic that becomes a Location. Defeat the Mastermind when it has no face-down Tactics left; Locations created by Tactics do not also need to be fought.
- **Transforming Schemes (R p.2):** All four Schemes have two sides. Start on the Setup side; when instructed, flip the Scheme and use only the currently face-up rules.
- **Mandarin's Rings (R p.2):** This Henchman Group has ten unique cards rather than ten identical copies.

## Required parts and glossary

- Locations are a new card type in the Villain Deck and city, not Villains. The insert identifies no new shared token stack (R pp.1–2).
- **Hyperspeed:** Inspect a specified number of cards from your deck and gain a bonus for matching icons before discarding them. (R p.1)
- **Dark Memories:** Gain strength based on distinct Hero Classes in your discard pile. (R p.1)
- **Last Stand:** Gain strength based on empty city spaces. (R pp.1–2)
- **Location:** A Villain-Deck card that occupies a fixed spot above a city space and can be fought. (R pp.1–2)
- **Transforming Scheme:** A two-sided Scheme whose current face determines the active rules. (R p.2)

Summaries are original paraphrases under 40 words. Verify the printed icons and each card's exact text from clear product-card sources.

## Setup and implementation gaps

Verify all four Schemes' player limits, Twist counts, required groups/Heroes, moves, stacks, setup steps, and both sides' setup-dependent behavior. Verify each Mastermind's Always Leads/setup effects and Hero metadata. The catalog/generator must represent Locations as a distinct card type and allow two-sided Schemes to transform during play; this record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Captain Marvel, Agent of S.H.I.E.L.D.

- **The Sword of S.H.I.E.L.D.**; type/group: Hero / Captain Marvel, Agent of S.H.I.E.L.D.; copies: Unverified; Hero Name: Captain Marvel, Agent of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cpt-marvel-04.png).
- **Radiant Blast**; type/group: Hero / Captain Marvel, Agent of S.H.I.E.L.D.; copies: Unverified; Hero Name: Captain Marvel, Agent of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cpt-marvel-03.png).
- **Dominate the Battlefield**; type/group: Hero / Captain Marvel, Agent of S.H.I.E.L.D.; copies: Unverified; Hero Name: Captain Marvel, Agent of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 6; Attack 2+; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cpt-marvel-02.png).
- **Higher, Further, Faster**; type/group: Hero / Captain Marvel, Agent of S.H.I.E.L.D.; copies: Unverified; Hero Name: Captain Marvel, Agent of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 7; Attack 0+; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cpt-marvel-01.png).

### Hero group: Darkhawk

- **Balance the Darkforce**; type/group: Hero / Darkhawk; copies: Unverified; Hero Name: Darkhawk; team: Avengers; class icons: Tech; printed values: Cost 3; Recruit 1; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/darkhawk-04.png).
- **Hawk Dive**; type/group: Hero / Darkhawk; copies: Unverified; Hero Name: Darkhawk; team: Avengers; class icons: Covert; printed values: Cost 4; Recruit 0+; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/darkhawk-03.png).
- **Travel to Nullspace**; type/group: Hero / Darkhawk; copies: Unverified; Hero Name: Darkhawk; team: Avengers; class icons: Tech; printed values: Cost 6; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/darkhawk-02.png).
- **Warflight**; type/group: Hero / Darkhawk; copies: Unverified; Hero Name: Darkhawk; team: Avengers; class icons: Tech; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/darkhawk-01.png).

### Hero group: Hellcat

- **Catlike Agility**; type/group: Hero / Hellcat; copies: Unverified; Hero Name: Hellcat; team: Avengers; class icons: Instinct; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hellcat_04.png).
- **Part-Time PI**; type/group: Hero / Hellcat; copies: Unverified; Hero Name: Hellcat; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hellcat_03.png).
- **Demon Sight**; type/group: Hero / Hellcat; copies: Unverified; Hero Name: Hellcat; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hellcat_02.png).
- **Second Chance at Life**; type/group: Hero / Hellcat; copies: Unverified; Hero Name: Hellcat; team: Avengers; class icons: Instinct; printed values: Cost 8; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hellcat_01.png).

### Hero group: Photon

- **Infrared Conversation**; type/group: Hero / Photon; copies: Unverified; Hero Name: Photon; team: Avengers; class icons: Ranged; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/photon-04.png).
- **Ultraviolet Radiation**; type/group: Hero / Photon; copies: Unverified; Hero Name: Photon; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 3+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/photon-03.png).
- **Light the Way**; type/group: Hero / Photon; copies: Unverified; Hero Name: Photon; team: Avengers; class icons: Covert; printed values: Cost 6; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/photon-02.png).
- **Coruscating Vengeance**; type/group: Hero / Photon; copies: Unverified; Hero Name: Photon; team: Avengers; class icons: Ranged; printed values: Cost 8; Attack 6+; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/photon-01.png).

### Hero group: Quicksilver

- **Too Fast to See**; type/group: Hero / Quicksilver; copies: Unverified; Hero Name: Quicksilver; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quicksilver-04.png).
- **Perpetual Motion**; type/group: Hero / Quicksilver; copies: Unverified; Hero Name: Quicksilver; team: Avengers; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quicksilver-03.png).
- **Jittery Impatience**; type/group: Hero / Quicksilver; copies: Unverified; Hero Name: Quicksilver; team: Avengers; class icons: Instinct; printed values: Cost 6; Recruit 2; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quicksilver-02.png).
- **Around the World Punch**; type/group: Hero / Quicksilver; copies: Unverified; Hero Name: Quicksilver; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quicksilver-01.png).

### Hero group: Ronin

- **Mysterious Identity**; type/group: Hero / Ronin; copies: Unverified; Hero Name: Ronin; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronin-04.png).
- **Storm of Arrows**; type/group: Hero / Ronin; copies: Unverified; Hero Name: Ronin; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronin-03.png).
- **Haunted by Loss**; type/group: Hero / Ronin; copies: Unverified; Hero Name: Ronin; team: Avengers; class icons: Instinct; printed values: Cost 5; Attack 2+; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronin-02.png).
- **Brooding Fury**; type/group: Hero / Ronin; copies: Unverified; Hero Name: Ronin; team: Avengers; class icons: Strength; printed values: Cost 7; Attack 3+; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronin-01.png).

### Hero group: Scarlet Witch

- **Hex Bolt**; type/group: Hero / Scarlet Witch; copies: Unverified; Hero Name: Scarlet Witch; team: Avengers; class icons: Ranged; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-witch-03.png).
- **Alter Reality**; type/group: Hero / Scarlet Witch; copies: Unverified; Hero Name: Scarlet Witch; team: Avengers; class icons: Covert; printed values: Cost 3; Recruit 2; Attack 0+; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-witch-04.png).
- **Chaos Magic**; type/group: Hero / Scarlet Witch; copies: Unverified; Hero Name: Scarlet Witch; team: Avengers; class icons: Covert; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-witch-02.png).
- **Warp Time and Space**; type/group: Hero / Scarlet Witch; copies: Unverified; Hero Name: Scarlet Witch; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 0+; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-witch-01.png).

### Hero group: Speed

- **Accelerate**; type/group: Hero / Speed; copies: Unverified; Hero Name: Speed; team: Avengers; class icons: Instinct; printed values: Cost 2; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speed_03.png).
- **Speedy Delivery**; type/group: Hero / Speed; copies: Unverified; Hero Name: Speed; team: Avengers; class icons: Instinct; printed values: Cost 4; Recruit 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speed_04.png).
- **Race to the Rescue**; type/group: Hero / Speed; copies: Unverified; Hero Name: Speed; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speed_02.png).
- **Break the Sound Barrier**; type/group: Hero / Speed; copies: Unverified; Hero Name: Speed; team: Avengers; class icons: Covert; printed values: Cost 8; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speed_01.png).

### Hero group: War Machine

- **Simulated Target Practice**; type/group: Hero / War Machine; copies: Unverified; Hero Name: War Machine; team: Avengers; class icons: Tech; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/war-machine-04.png).
- **Military-Industrial Complex**; type/group: Hero / War Machine; copies: Unverified; Hero Name: War Machine; team: Avengers; class icons: Tech; printed values: Cost 4; Recruit 0+; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/war-machine-03.png).
- **Hypersonic Cannon**; type/group: Hero / War Machine; copies: Unverified; Hero Name: War Machine; team: Avengers; class icons: Ranged; printed values: Cost 5; Attack 0+; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/war-machine-02.png).
- **Overwhelming Firepower**; type/group: Hero / War Machine; copies: Unverified; Hero Name: War Machine; team: Avengers; class icons: Tech; printed values: Cost 8; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/war-machine-01.png).

### Villain Group: Army of Evil

- **Mister Hyde**; type/group: Villain / Army of Evil; copies: 2; printed values: Attack 6*; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/army-of-evil-01.png).
- **Klaw**; type/group: Villain / Army of Evil; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/army-of-evil-03.png).
- **Dome of Darkforce**; type/group: Villain; subtype Location / Army of Evil; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/location-army-of-evil.png).
- **Count Nefaria**; type/group: Villain / Army of Evil; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/army-of-evil-04.png).
- **Blackout**; type/group: Villain / Army of Evil; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/army-of-evil-02.png).

### Villain Group: Dark Avengers

- **Ares**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 6+; VP 6; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-02.png).
- **Captain Marvel (Noh-Varr)**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 3+; VP 3; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-06.png).
- **Dark Hawkeye (Bullseye)**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-03.png).
- **Dark Ms. Marvel (Moonnstone)**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-05.png).
- **Dark Spider-Man (Scorpion)**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 2+; VP 2; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-07.png).
- **Dark Wolverine (Daken)**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-01.png).
- **Sentry**; type/group: Villain / Dark Avengers; copies: 1; printed values: Attack 7+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-avengers-04.png).
- **Sentry's Watchtower**; type/group: Villain; subtype Location / Dark Avengers; copies: 1; printed values: Attack 8; VP 5; keyword labels: Last Stand; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/location-dark-avengers.png).

### Villain Group: Hood's Gang

- **Cancer**; type/group: Villain / Hood's Gang; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hoods-gang-01.png).
- **Chemistro**; type/group: Villain / Hood's Gang; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hoods-gang-04.png).
- **Madam Masque**; type/group: Villain / Hood's Gang; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hoods-gang-03.png).
- **The Brothers Grimm**; type/group: Villain / Hood's Gang; copies: 1; printed values: Attack 2*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hoods-gang-02.png).
- **The Dark Dimension**; type/group: Villain; subtype Location / Hood's Gang; copies: 1; printed values: Attack 9; VP 5; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hoods-gang-location.png).

### Villain Group: Lethal Legion

- **Carnival of Wonders**; type/group: Villain; subtype Location / Lethal Legion; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lethal-legion-04.png).
- **Laser Maze**; type/group: Villain; subtype Location / Lethal Legion; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/location-lethal-legion-02.png).
- **Living Laser**; type/group: Villain / Lethal Legion; copies: 1; printed values: Attack 6+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lethal-legion-01.png).
- **M'Baku**; type/group: Villain / Lethal Legion; copies: 1; printed values: Attack 5+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lethal-legion-05.png).
- **Power Man (Erik Josten)**; type/group: Villain / Lethal Legion; copies: 1; printed values: Attack 5+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lethal-legion-02.png).
- **Swordsman**; type/group: Villain / Lethal Legion; copies: 1; printed values: Attack 4+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lethal-legion-06.png).
- **“The Raft“ Prison**; type/group: Villain; subtype Location / Lethal Legion; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/lethal-legion-03.png).
- **White Gorilla Cult**; type/group: Villain; subtype Location / Lethal Legion; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/location-lethal-legion-01.png).

### Henchman Group: HYDRA Base

- **HYDRA Base**; type/group: Henchman; subtype Location / HYDRA Base; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/location-hydra-base.png).

### Henchman Group: Mandarin's Rings

- **Daimonic, The White Light**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-10.png).
- **Incandescence, The Flame Blast**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-09.png).
- **Influence, The Impact Beam**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-05.png).
- **Liar, The Mento-Intensifier**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-06.png).
- **Lightning, The Electro-Blast**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-07.png).
- **Nightbringer, The Black Light**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-08.png).
- **Remaker, The Matter Rearranger**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-03.png).
- **Spectral, The Disintegration Beam**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-04.png).
- **Spin, The Vortex Beam**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-01.png).
- **Zero, The Ice Blast**; type/group: Henchman / Mandarin's Rings; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandarins-rings-02.png).

### Mastermind: Grim Reaper

- **Grim Reaper**; type/group: Normal Mastermind face / Grim Reaper; copies: Unverified; printed values: Attack 8+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grim-reaper-01.png).
- **Epic Grim Reaper**; type/group: Epic Mastermind face / Grim Reaper; copies: Unverified; printed values: Attack 9+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grim-reaper-02.png).
- **Carnival of Concussions**; type/group: Villain / Grim Reaper; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grim-reaper-03.png).
- **Cult of Skulls**; type/group: Villain / Grim Reaper; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grim-reaper-04.png).
- **Maze of Bones**; type/group: Villain / Grim Reaper; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grim-reaper-05.png).
- **Prison of Coffins**; type/group: Villain / Grim Reaper; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grim-reaper-06.png).

### Mastermind: Hood, The

- **The Hood**; type/group: Normal Mastermind face / Hood, The; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-hood-01.png).
- **Epic Hood**; type/group: Epic Mastermind face / Hood, The; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-hood-02.png).
- **Demonic Revelation**; type/group: Mastermind Tactic / Hood, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-hood-05.png).
- **Focus Magic Through Guns**; type/group: Mastermind Tactic / Hood, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-hood-04.png).
- **Paean to Dormammu**; type/group: Mastermind Tactic / Hood, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-hood-03.png).
- **The Hood's Warehouse**; type/group: Villain / Hood, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-hood-06.png).

### Mastermind: Mandarin

- **Mandarin**; type/group: Normal Mastermind face / Mandarin; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mandarin-01.png).
- **Epic Mandarin**; type/group: Epic Mastermind face / Mandarin; copies: Unverified; printed values: Attack 26*; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mandarin-02.png).
- **Circles Unbroken**; type/group: Mastermind Tactic / Mandarin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mandarin-04.png).
- **Dragon of Heaven Spaceship**; type/group: Villain / Mandarin; copies: Unverified; printed values: Attack 9; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mandarin-03.png).
- **Intertwining Powers**; type/group: Mastermind Tactic / Mandarin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mandarin-05.png).
- **Rings Seek Their True Hand**; type/group: Mastermind Tactic / Mandarin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mandarin-06.png).

### Scheme: Earthquake Drains the Ocean

- **Earthquake Drains the Ocean**; type/group: Scheme / Earthquake Drains the Ocean; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/104Scheme(19).png).
- **Tsunami Crushes the Coast**; type/group: Scheme (transformed face) / Earthquake Drains the Ocean; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/105Scheme(105).png).

### Scheme: House of M

- **House of M**; type/group: Scheme / House of M; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/108Scheme(107).png).
- **”No More Mutants”**; type/group: Scheme (transformed face) / House of M; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/109Scheme(108).png).

### Scheme: Secret HYDRA Corruption

- **Secret HYDRA Corruption**; type/group: Scheme / Secret HYDRA Corruption; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/110Scheme(109).png).
- **Open HYDRA Revolution**; type/group: Scheme (transformed face) / Secret HYDRA Corruption; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/111Scheme(110).png).

### Scheme: Korvac Saga, The

- **The Korvac Saga**; type/group: Scheme / Korvac Saga, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/106Scheme(20).png).
- **Korvac Revealed**; type/group: Scheme (transformed face) / Korvac Saga, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/107Scheme(106).png).

### Bystander set: Dog Show Judge

- **Dog Show Judge**; type/group: Bystander / Dog Show Judge; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/dog-show-judge.png).

### Bystander set: Lawyer

- **Lawyer**; type/group: Bystander / Lawyer; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/lawyer.png).

### Bystander set: Rocket Test Pilot

- **Rocket Test Pilot**; type/group: Bystander / Rocket Test Pilot; copies: 1; printed values: not indexed in C1; keyword labels: Hyperspeed; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/rocket-test-pilot.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
