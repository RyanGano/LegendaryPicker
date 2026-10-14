# Ant-Man (November 2018)

**Research status: Partial; integrated (#163).** The official insert verifies contents and four mechanics. The Scheme Setup lines, Always Leads and part uses in `LegendaryPickerService/Data/Boxes/ant-man.json` were read from the C1-linked card faces (`Card`): Morgan Le Fay and both Villain Groups use Wounds, and no Ant-Man card uses another part. Per-face copy counts remain unverified.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| AM | [Upper Deck Ant-Man rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Legendary_Rules-Ant-Man.pdf) | Contents (PDF p.2), mechanics and card clarifications (PDF pp.1–2). |
| C1 | [master-strike structured ant-man card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/antman.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | November 2018 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (AM p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Ant-Man; Black Knight; Jocasta; Wasp; Wonder Man.
- **Villain Groups (two):** Queen's Vengeance; Ultron's Legacy.
- **Masterminds (two):** Morgan Le Fay; Ultron. Each has a normal and Epic side with the same four Tactics (AM p.2).
- **Schemes (four):** Age of Ultron; Pull Earth into Medieval Times; Transform Commuters into Giant Ants; Trap Heroes in the Microverse.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, complete Scheme setup lines, Mastermind setup effects, component dependencies, or the icon values lost in text extraction.

## Rules and mechanisms

- **Size-Changing (AM p.1):** A qualifying Hero's Recruit cost or Villain's fight value drops by 2 when its stated condition is met. Only whether a matching Hero was played matters, not how many of that class.
- **Microscopic Size-Changing (AM p.1):** Reduce a qualifying card's cost or fight value by 2 per matching card played, up to the ability's printed limit. A resulting negative value grants that many Recruit or Attack points.
- **Empowered (AM p.1):** Gain +1 per matching-color card in the HQ, checked when the Hero is played or enemy fought. Double/Triple Empowered multiply that bonus. The Ultron clarification counts HQ cards matching any color represented in his Threat Analysis pile.
- **Chivalrous Duel (AM pp.1–2):** Fight an enemy using points from Heroes with one shared Hero Name only; points from non-Hero cards cannot contribute.
- **Epic Masterminds (AM p.2):** Either Mastermind may use its regular or Epic side with the same four Tactics.

## Required parts and glossary

The rules insert lists no additional shared stack or token; individual card effects still need review for component dependencies.

- **Size-Changing:** A conditional reduction in Recruit cost or fight value. (AM p.1)
- **Microscopic Size-Changing:** A larger reduction based on the number of matching cards played, with a printed cap. (AM p.1)
- **Empowered:** A bonus based on matching-color cards in the HQ. (AM p.1)
- **Chivalrous Duel:** A fight restriction requiring points from one Hero Name. (AM pp.1–2)
- **Threat Analysis:** Ultron's pile of Heroes, whose represented colors determine which HQ cards empower him. (AM p.2)

Summaries are original paraphrases under 40 words. Exact qualifying icons, Hero teams/classes, and other card-linked component requirements require card-level sources.

## Setup and implementation gaps

Every Scheme's Twists and Setup line, and both Masterminds' Always Leads and Epic side, were read from the card faces linked below and are in the box file (#163); no Scheme prints a player limit. Hero teams and classes are as C1 gives them.

## Difficulty data (#269, 2026-10-14)

`Data/Difficulty/ant-man.json` (format v3, source `Q`). Card facts: the shared OCR cache (titles and keyword markers only, no card text), the C1 index below for names, classes, costs and printed Recruit/Attack icons and Villain copies, and the owner's LegendaryAssistant data (`ant-man-allies`, `-adversaries`, `-commanders`, `-schemes`) for what each card does. Names follow C1 (Jocasta, Queen's Vengeance, Stolen Tomes of Merlin, Transmogrify). No card text is stored.

- **Copies: default** for all five Heroes (5, 5, 3, 1 in ascending cost order; Black Knight's two cost-3 cards are listed in C1 order). Villain copies are C1's (1 each); the box has no Henchman Group.
- **Heroes.** Printed 0+ icons: Amulet of Avalon, The Ebony Blade and One-Hit Wonder are `attack`; Absorb Ambient Power is `both`; Holographic Image Inducer has no printed icon (`none`, `draw` 2). Draws: Ride the Ants 1, Tiny Winged Justice 1 (a Covert draw), One-Hit Wonder 1 (a choice with Empowered), Flying Steed 3 (a conditional draw reacting to a Master Strike). Wants are class bonuses written on the card (Tech, Covert, Strength, Ranged) and Founding Avenger's Avengers bonus (`team avengers`); Giant Ego wants extra cards drawn.
- **Villains.** Pixie's Fight (reveal Covert or another Villain Deck card) is a `check` for `escape-pressure`; Iron Knight (Escape) and Brutish Ultron-14 (Fight and Escape) are Tech and Strength `check`s for a Wound, severity 2 as in earlier boxes. Blackbird, Gigantus, Original Ultron-1 and Ultron Roboticks KO one of your Heroes (`deck-thinning 1`, Fight); Mordred the Evil hands a discard card to another player (`deck-thinning 1`, Fight and Escape). Future Ultron Prime's Ambush is `hq-disruption 2`; Crimson Cowl's Ambush capture is `bystander-pressure 1`.
- **Masterminds.** Morgan Le Fay's Master Strike (the normal side) is a Covert `check` for a Wound, severity 1; the Epic side is not tagged separately. Ultron's is a Tech `check` that makes him stronger, `attack-inflation`, severity 2. The Tactics only help the fighter or hit the other players and are untagged.
- **Schemes.** Age of Ultron's Evolved Ultron entering the city is `escape-pressure 3` (it can lose the game), amplifies escape; Pull Earth into Medieval Times `escape-pressure 2` (Twists 7 and 8 return Villains), amplifies escape; Transform Commuters into Giant Ants `bystander-pressure 3` (the Giant Ants are Bystanders turned Villains and ten lose the game), amplifies Bystanders; Trap Heroes in the Microverse (play two cards) `escape-pressure 2`, amplifies escape.

### Difficulty: no axis

- Size-Changing and Microscopic Size-Changing (a Hero's cost or a Villain's Attack drops with matching classes) and Empowered (a bonus for matching colours in the HQ) have no slot; the cost and Attack are printed values. Pym Particles and Ultron-Pym granting Size-Changing are untagged.
- Chivalrous Duel (only one Hero Name's points may fight) restricts the fight with no axis; it is untagged on Queen's Vengeance, Morgan Le Fay and Pull Earth into Medieval Times's Twists 1 to 6.
- Reverse the Flow of Time (turn order) and Self-Repairing Legions, Sorcerous Blasts and Transmogrify (only the other players) are untagged.
- Giant Ants' and Age of Ultron's loss counters, and Trap Heroes in the Microverse's Heroes in the Villain Deck, are standing rules; the Twist tag stands for them.
- Absorb Ambient Power, Ionic Energy and 8th Wonder of the World move HQ cards to the bottom of the Hero Deck; helpful, no axis.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Ant-Man

- **Ride the Ants**; type/group: Hero / Ant-Man; copies: Unverified; Hero Name: Ant-Man; team: Avengers; class icons: Tech; printed values: Cost 4*; Attack 1; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ant-man-04.png).
- **Risky Science**; type/group: Hero / Ant-Man; copies: Unverified; Hero Name: Ant-Man; team: Avengers; class icons: Tech; printed values: Cost 5*; Attack 2; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ant-man-03.png).
- **Giant Ego**; type/group: Hero / Ant-Man; copies: Unverified; Hero Name: Ant-Man; team: Avengers; class icons: Strength; printed values: Cost 6*; Attack 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ant-man-02.png).
- **Pym Particles**; type/group: Hero / Ant-Man; copies: Unverified; Hero Name: Ant-Man; team: Avengers; class icons: Tech; printed values: Cost 9*; Attack 5; keyword labels: Microscopic Size-Changing, Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ant-man-01.png).

### Hero group: Black Knight

- **Amulet of Avalon**; type/group: Hero / Black Knight; copies: Unverified; Hero Name: Black Knight; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 0+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-knight-03.png).
- **Defend the Weak**; type/group: Hero / Black Knight; copies: Unverified; Hero Name: Black Knight; team: Avengers; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-knight-04.png).
- **Flying Steed**; type/group: Hero / Black Knight; copies: Unverified; Hero Name: Black Knight; team: Avengers; class icons: Covert; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-knight-02.png).
- **The Ebony Blade**; type/group: Hero / Black Knight; copies: Unverified; Hero Name: Black Knight; team: Avengers; class icons: Instinct; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-knight-01.png).

### Hero group: Jocasta

- **Creation of Ultron**; type/group: Hero / Jocasta; copies: Unverified; Hero Name: Jocasta; team: Avengers; class icons: Tech; printed values: Cost 3; Attack 2+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jocasta-04.png).
- **Reprocess**; type/group: Hero / Jocasta; copies: Unverified; Hero Name: Jocasta; team: Avengers; class icons: Ranged; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jocasta-03.png).
- **Holographic Image Inducer**; type/group: Hero / Jocasta; copies: Unverified; Hero Name: Jocasta; team: Avengers; class icons: Tech; printed values: Cost 6*; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jocasta-02.png).
- **Electromagnetic Eyebeams**; type/group: Hero / Jocasta; copies: Unverified; Hero Name: Jocasta; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jocasta-01.png).

### Hero group: Wasp

- **Bio-Electric Sting**; type/group: Hero / Wasp; copies: Unverified; Hero Name: Wasp; team: Avengers; class icons: Covert; printed values: Cost 3*; Attack 1+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wasp-03.png).
- **Tiny Winged Justice**; type/group: Hero / Wasp; copies: Unverified; Hero Name: Wasp; team: Avengers; class icons: Covert; printed values: Cost 4*; Recruit 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wasp-04.png).
- **Swarm Tactics**; type/group: Hero / Wasp; copies: Unverified; Hero Name: Wasp; team: Avengers; class icons: Ranged; printed values: Cost 6*; Attack 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wasp-02.png).
- **Founding Avenger**; type/group: Hero / Wasp; copies: Unverified; Hero Name: Wasp; team: Avengers; class icons: Covert; printed values: Cost 9*; Attack 4+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wasp-01.png).

### Hero group: Wonder Man

- **One-Hit Wonder**; type/group: Hero / Wonder Man; copies: Unverified; Hero Name: Wonder Man; team: Avengers; class icons: Strength; printed values: Cost 2; Attack 0+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wonder-man-03.png).
- **Ionic Energy**; type/group: Hero / Wonder Man; copies: Unverified; Hero Name: Wonder Man; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wonder-man-04.png).
- **Absorb Ambient Power**; type/group: Hero / Wonder Man; copies: Unverified; Hero Name: Wonder Man; team: Avengers; class icons: Ranged; printed values: Cost 5; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wonder-man-02.png).
- **8th Wonder of the World**; type/group: Hero / Wonder Man; copies: Unverified; Hero Name: Wonder Man; team: Avengers; class icons: Strength; printed values: Cost 8*; Attack 4+; keyword labels: Size-Changing, Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wonder-man-01.png).

### Villain Group: Ultron's Legacy

- **Ultron Roboticks**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 3*; VP 2; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-08.png).
- **Original Ultron-1**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 3+; VP 2; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-07.png).
- **Legions of Ultron**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-05.png).
- **Alkhema**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-06.png).
- **Ultron-Pym**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 6*; VP 3; keyword labels: Microscopic Size-Changing, Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-03.png).
- **Future Ultron Prime**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-04.png).
- **Brutish Ultron-14**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-01.png).
- **Crimson Cowl**; type/group: Villain / Ultron's Legacy; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ultrons-legacy-02.png).

### Villain Group: Queen's Vengeance

- **Daystar**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 5*; VP 2; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-03.png).
- **Blackbird**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 3*; VP 3; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-04.png).
- **Gigantus**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 7*; VP 4; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-02.png).
- **Iron Knight**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 4*; VP 4; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-01.png).
- **Yeoman America**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 5*; VP 5; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-08.png).
- **Star-Knight**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 3*; VP 3; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-07.png).
- **Pixie**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 3*; VP 2; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-06.png).
- **Mordred the Evil**; type/group: Villain / Queen's Vengeance; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/queens-vengeance-05.png).

### Mastermind: Morgan Le Fay

- **Morgan Le Fay**; type/group: Normal Mastermind face / Morgan Le Fay; copies: Unverified; printed values: VP 6; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/morgan-le-fay-01.png).
- **Epic Morgan Le Fay**; type/group: Epic Mastermind face / Morgan Le Fay; copies: Unverified; printed values: Attack 9*; VP 6; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/morgan-le-fay-02.png).
- **Reverse the Flow of Time**; type/group: Mastermind Tactic / Morgan Le Fay; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/morgan-le-fay-06.png).
- **Sorcerous Blasts**; type/group: Mastermind Tactic / Morgan Le Fay; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/morgan-le-fay-03.png).
- **Stolen Tomes of Merlin**; type/group: Mastermind Tactic / Morgan Le Fay; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/morgan-le-fay-05.png).
- **Transmogrify**; type/group: Mastermind Tactic / Morgan Le Fay; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/morgan-le-fay-04.png).

### Mastermind: Ultron

- **Ultron**; type/group: Normal Mastermind face / Ultron; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ultron-01.png).
- **Epic Ultron**; type/group: Epic Mastermind face / Ultron; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ultron-02.png).
- **Arrogant Blindspot**; type/group: Mastermind Tactic / Ultron; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ultron-06.png).
- **Paralyzing Encephalo-Ray**; type/group: Mastermind Tactic / Ultron; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ultron-03.png).
- **Predictive Analysis**; type/group: Mastermind Tactic / Ultron; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ultron-05.png).
- **Self-Repairing Legions**; type/group: Mastermind Tactic / Ultron; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ultron-04.png).

### Scheme: Age of Ultron

- **Age of Ultron**; type/group: Scheme / Age of Ultron; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/96Scheme(5).png).

### Scheme: Pull Earth into Medieval Times

- **Pull Earth into Medieval Times**; type/group: Scheme / Pull Earth into Medieval Times; copies: Unverified; printed values: not indexed in C1; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/97Scheme(6).png).

### Scheme: Transform Commuters into Giant Ants

- **Transform Commuters into Giant Ants**; type/group: Scheme / Transform Commuters into Giant Ants; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/98Scheme(7).png).

### Scheme: Trap Heroes in the Microverse

- **Trap Heroes in the Microverse**; type/group: Scheme / Trap Heroes in the Microverse; copies: Unverified; printed values: not indexed in C1; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/99Scheme(8).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
