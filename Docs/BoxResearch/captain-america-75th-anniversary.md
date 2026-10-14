# Captain America 75th Anniversary (March 2016)

**Research status: Partial; integrated (#121).** The official rulesheet verifies the aggregate contents and new mechanics; the Scheme Setup lines, Always Leads and part uses in `LegendaryPickerService/Data/Boxes/captain-america-75th-anniversary.json` were read from the C1-linked card faces (`Card`). Per-face copy counts remain open.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured captain-america-75th-anniversary card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/captainamerica.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| CA75 | [Upper Deck Captain America rulesheet](https://upperdeck.com/wp-content/uploads/2024/05/Legendary-Captain-America-Rulesheet.pdf) | Contents (PDF p.2), new keywords and clarifications (PDF pp.1–2). |
| C2 | [nutki Captain America 75th Anniversary catalog](https://github.com/nutki/legendary/tree/master/texttools/Captain%20America%2075th%20Anniversary) | Card and group names/membership only; not mechanics or setup values. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | March 2016 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (CA75 p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The category totals sum to the official 100-card count. No separate token or shared-stack component is listed.

### Group inventory (C2 + C1 face index)

- **Heroes (five):** Agent X-13; Captain America (Falcon); Captain America 1941; Steve Rogers, Director of S.H.I.E.L.D.; Winter Soldier.
- **Villain Groups (two):** Zola's Creations; Masters of Evil (WWII).
- **Masterminds (two):** Arnim Zola; Baron Heinrich Zemo.
- **Schemes (four):** Brainwash the Military; Change the Outcome of WWII; Go Back in Time to Slay Heroes' Ancestors; The Unbreakable Enigma Code.

C1 face metadata is indexed below with image links where supplied; C2 is used only for identifiers. Fields absent from C1, Always Leads, and setup values still require an allowed source.

## Rules and setup facts

- **Man (and Woman) Out of Time (CA75 p.1):** After using the card, set it aside; play it again at the start of the next turn, then discard it. It counts as played on both turns for Superpower abilities.
- **Savior (CA75 p.1):** This ability works when its player has at least three Bystanders in their Victory Pile. Rescues resolved first that turn count toward the threshold.
- **Abomination (CA75 p.1):** A Villain gains Attack equal to the printed Attack of the Hero in the HQ space beneath it. Ultimate Abomination instead counts the printed Attack of all Heroes in the HQ; the Villain's value can change as it moves.
- **Change the Outcome of WWII (CA75 p.2):** Begin in the normal city. Entering a new country changes the city edge; in smaller countries omit spaces, starting with the Bridge. If the capital is conquered, remain in that country until the next Scheme Twist.
- **Hero class clarification (CA75 p.2):** Basic S.H.I.E.L.D. Agents and Troopers have no Hero class, and team icons are not classes. The insert notes this differs from the original Core Set's color-based handling.
- **Arnim Zola clarification (CA75 p.2):** A card without the value a rule asks to inspect counts as zero for that check.

The rulesheet does not give full setup text for the four Schemes or the two Masterminds. Player limits, Twist counts, required groups/Heroes, card moves, other stacks, and setup steps remain open unless stated in the Change the Outcome of WWII clarification.

## Required parts and glossary

- Savior effects depend on the Bystander stack and Victory Piles; the product's contents do not list a new Bystander or other shared-stack supply (CA75 p.1–2).
- **Man (and Woman) Out of Time:** A used card waits aside, returns for one more play next turn, then leaves play. (CA75 p.1)
- **Savior:** An ability checks for at least three Bystanders in its player's Victory Pile. (CA75 p.1)
- **Abomination:** A Villain's Attack can track the printed Attack of the Hero beneath it in the HQ. (CA75 p.1)

Summaries are original paraphrases under 40 words. Any additional keywords found on cards need verification from the printed cards and their governing rules pages.

## Implementation gaps

This is an expansion, so it contributes no separate base-game setup. Scheme Setup lines, Always Leads and part uses were read from the C1-linked card faces (`Card`) and are in the box file; per-face copy counts remain open.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Agent X-13

- **Sniper Squad**; type/group: Hero / Agent X-13; copies: Unverified; Hero Name: Agent X-13; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 3; Recruit 1; Attack 1; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-x-13-04.png).
- **Paramilitary Ops**; type/group: Hero / Agent X-13; copies: Unverified; Hero Name: Agent X-13; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-x-13-03.png).
- **Spy Network**; type/group: Hero / Agent X-13; copies: Unverified; Hero Name: Agent X-13; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 4; Attack 0+; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-x-13-02.png).
- **Mobilize for War**; type/group: Hero / Agent X-13; copies: Unverified; Hero Name: Agent X-13; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 7; Attack 4+; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-x-13-01.png).

### Hero group: Captain America (Falcon)

- **Aerial Catch**; type/group: Hero / Captain America (Falcon); copies: Unverified; Hero Name: Captain America (Falcon); team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-03.png).
- **Winged Salvation**; type/group: Hero / Captain America (Falcon); copies: Unverified; Hero Name: Captain America (Falcon); team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-04.png).
- **Flying Shield Block**; type/group: Hero / Captain America (Falcon); copies: Unverified; Hero Name: Captain America (Falcon); team: Avengers; class icons: Tech; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-02.png).
- **Star-Spangled Hero**; type/group: Hero / Captain America (Falcon); copies: Unverified; Hero Name: Captain America (Falcon); team: Avengers; class icons: Covert; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-01.png).

### Hero group: Captain America 1941

- **Devoted Patriot**; type/group: Hero / Captain America 1941; copies: Unverified; Hero Name: Captain America 1941; team: Avengers; class icons: Strength; printed values: Cost 3; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-1941-03.png).
- **Storm the Beachhead**; type/group: Hero / Captain America 1941; copies: Unverified; Hero Name: Captain America 1941; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 0+; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-1941-04.png).
- **Liberate the Prisoners**; type/group: Hero / Captain America 1941; copies: Unverified; Hero Name: Captain America 1941; team: Avengers; class icons: Covert; printed values: Cost 6; Attack 3; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-1941-02.png).
- **Punch Evil in the Face**; type/group: Hero / Captain America 1941; copies: Unverified; Hero Name: Captain America 1941; team: Avengers; class icons: Instinct; printed values: Cost 8; Attack 5; keyword labels: Savior, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-1941-01.png).

### Hero group: Steve Rogers, Director of S.H.I.E.L.D.

- **International Strike Force**; type/group: Hero / Steve Rogers, Director of S.H.I.E.L.D.; copies: Unverified; Hero Name: Steve Rogers, Director of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/steve-rogers-03.png).
- **Reassign to Civilian Duty**; type/group: Hero / Steve Rogers, Director of S.H.I.E.L.D.; copies: Unverified; Hero Name: Steve Rogers, Director of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/steve-rogers-04.png).
- **Shadow of Wars Past**; type/group: Hero / Steve Rogers, Director of S.H.I.E.L.D.; copies: Unverified; Hero Name: Steve Rogers, Director of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Savior, Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/steve-rogers-02.png).
- **Save the World**; type/group: Hero / Steve Rogers, Director of S.H.I.E.L.D.; copies: Unverified; Hero Name: Steve Rogers, Director of S.H.I.E.L.D.; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 8; Attack 4+; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/steve-rogers-01.png).

### Hero group: Winter Soldier

- **Bionic Arm**; type/group: Hero / Winter Soldier; copies: Unverified; Hero Name: Winter Soldier; team: Unaffiliated; class icons: Strength; printed values: Cost 3; Attack 2; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/winter-soldier-03.png).
- **Sniper Nest**; type/group: Hero / Winter Soldier; copies: Unverified; Hero Name: Winter Soldier; team: Unaffiliated; class icons: Tech; printed values: Cost 4; Recruit 1; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/winter-soldier-04.png).
- **KGB Training**; type/group: Hero / Winter Soldier; copies: Unverified; Hero Name: Winter Soldier; team: Unaffiliated; class icons: Covert; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/winter-soldier-02.png).
- **2>4**; type/group: Hero / Winter Soldier; copies: Unverified; Hero Name: Winter Soldier; team: Unaffiliated; class icons: Tech; printed values: Cost 7; Attack 4; keyword labels: Man/Woman Out of Time; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/winter-soldier-01.png).

### Villain Group: Zola's Creations

- **Captain Zolandia**; type/group: Villain / Zola's Creations; copies: 2; printed values: Attack 6+; VP 5; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/zolas-creations-01.png).
- **Doughboy**; type/group: Villain / Zola's Creations; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/zolas-creations-04.png).
- **Man-Fish**; type/group: Villain / Zola's Creations; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/zolas-creations-03.png).
- **Primus**; type/group: Villain / Zola's Creations; copies: 2; printed values: Attack 3*; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/zolas-creations-02.png).

### Villain Group: Masters of Evil (WWII)

- **Black Knight**; type/group: Villain / Masters of Evil (WWII); copies: 2; printed values: Attack 4; VP 2; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-03-1.png).
- **Executioner**; type/group: Villain / Masters of Evil (WWII); copies: 2; printed values: Attack 6; VP 4; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-04-1.png).
- **Melter (WWII)**; type/group: Villain / Masters of Evil (WWII); copies: 2; printed values: Attack 5; VP 3; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-01-2.png).
- **Radioactive Man**; type/group: Villain / Masters of Evil (WWII); copies: 2; printed values: Attack 5; VP 3; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/masters-of-evil-02-1.png).

### Mastermind: Arnim Zola

- **Arnim Zola**; type/group: Normal Mastermind face / Arnim Zola; copies: Unverified; printed values: Attack 6+; VP 6; keyword labels: Abomination; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arnim-zola-01.png).
- **Dominate the Weak**; type/group: Mastermind Tactic / Arnim Zola; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arnim-zola-02.png).
- **Computer-Uploaded Genius**; type/group: Mastermind Tactic / Arnim Zola; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arnim-zola-05.png).
- **Pet Projects**; type/group: Mastermind Tactic / Arnim Zola; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arnim-zola-04.png).
- **Crush Pacifist Resistance**; type/group: Mastermind Tactic / Arnim Zola; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arnim-zola-03.png).

### Mastermind: Baron Heinrich Zemo

- **Baron Heinrich Zemo**; type/group: Normal Mastermind face / Baron Heinrich Zemo; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-zemo-01.png).
- **Fallen Idols**; type/group: Mastermind Tactic / Baron Heinrich Zemo; copies: Unverified; printed values: not indexed in C1; keyword labels: Savior; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-zemo-02.png).
- **Finding Zemo**; type/group: Mastermind Tactic / Baron Heinrich Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-zemo-03.png).
- **Hatred for the Avengers**; type/group: Mastermind Tactic / Baron Heinrich Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-zemo-05.png).
- **Prisoners of War**; type/group: Mastermind Tactic / Baron Heinrich Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-zemo-04.png).

### Scheme: Brainwash the Military

- **Brainwash the Military**; type/group: Scheme / Brainwash the Military; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/59Scheme(78).png).

### Scheme: Change the Outcome of WWII

- **Change the Outcome of WWII**; type/group: Scheme / Change the Outcome of WWII; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/58Scheme(77).png).

### Scheme: Go Back in Time to Slay Heroes' Ancestors

- **Go Back in Time to Slay Heroes' Ancestors**; type/group: Scheme / Go Back in Time to Slay Heroes' Ancestors; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/57Scheme(76).png).

### Scheme: Unbreakable Enigma Code, The

- **Unbreakable Enigma Code, The**; type/group: Scheme / Unbreakable Enigma Code, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/56Scheme(12).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.

## Card-face `uses` check (#166, 2026-10-12)

Every Hero, Villain, Henchman, Mastermind and Tactic face was checked for the shared stacks it takes cards from (Wounds, Sidekicks, Officers, Horrors, Bindings, Shards). Method: the shared OCR cache's stack markers per face (`Card`; 38 C1-linked faces in the cache, no card text stored) compared with each card's `uses`; faces the cache could not read were re-read locally. A card whose text only means something when a stack is in play (it KOs a Wound, or reacts to a player gaining one) also brings that stack, and when unsure the part is added (owner ruling, #141, 2026-10-12). Added `uses` wounds, source Card, under that ruling: Captain America (Falcon) (Flying Shield Block, reacts to a player gaining a Wound; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-02.png)). Black Knight, unreadable in the cache, was read by a short-lived reader and names no stack.

## Difficulty data (#269, 2026-10-14)

`Data/Difficulty/captain-america-75th-anniversary.json` (format v3, source `Q`). Card facts: the shared OCR cache holds only face-level markers (no card text), so effects were read from the owner's LegendaryAssistant data (`captain-america-75th-allies`, `-adversaries`, `-commanders`, `-schemes`). Names, classes, costs and printed Recruit/Attack icons follow the C1 index above. No card text is stored.

- **Copies: default for Heroes.** Per-face Hero copy counts are unverified, so every Hero uses 5, 5, 3, 1 in ascending cost order (the one tie follows the C1 listing order: Agent X-13's Paramilitary Ops before Spy Network). Villain copies are C1's (2 each).
- **Source conflict.** The owner's data lists only one class on the multiclass cards' Hero-level rows and names a few cards differently (Internalation Strike Force, Reassign to Civillian Duty, Melter (WWWI)); C1's names and classes are used.
- **Economy** follows C1's printed icons with a `0+` icon counted, so Star-Spangled Hero and International Strike Force are `both`, and Spy Network, Storm the Beachhead and Paramilitary Ops are `attack`.
- **Savior** is tagged as a `stack` want for bystanders (strength 1); the scorer does not score it yet. Conditional draws (Savior, Tech) carry `draw`, as the core box does.
- **Abomination** raises a Villain's Attack by the Hero beneath it; tagged `attack-inflation 1` on each Abomination Villain: on Fight, except Man-Fish, whose one tag is on Escape for its Wounds (Fight and Escape weigh the same). Fight effects that only help the defeating player and "each other player" effects that only hurt teammates carry no tag; reveal-or-suffer checks are tagged as in the core box.
- **Heroes' answers.** Flying Shield Block's Wound replacement answers `wounds 1`.

### Difficulty: no axis

- Primus's "also discard a card costing 2, 3, 5 or 7 to fight" is a fight cost with no axis; only its Escape discard is tagged (`hand-disruption 1`).
- Pet Projects' reveal-a-Zola's-Creations-Villain-or-gain-a-Wound check names a Villain Group, which no check kind can; approximated as `wounds 1`.
- Arnim Zola's Ultimate Abomination (Attack from all Heroes in the HQ) and Baron Zemo's +9 Attack for a non-Savior are on the Mastermind card itself, which has no tag slot; not tagged.
- Change the Outcome of WWII's changing city size and conquered capitals are tagged only as `escape-pressure 2` and `twist-density 2` for the Twist's two extra plays.
- The Unbreakable Enigma Code's guess-the-colours Mastermind fight is approximated as `attack-inflation 2`; the Hero Deck's face-down cards are `hq-disruption 1`.
- Brainwash the Military's Officers-as-Villains (Attack 3 plus stacked Twists) have no tag beyond the Twist's `escape-pressure` and `twist-density`.
- Reassign to Civilian Duty's optional KO and Mobilize for War's optional KOs are not self costs; KGB Training's seven-other-cards bonus and 2>4's Man Out of Time grant fit no want.
