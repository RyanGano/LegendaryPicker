# Noir (February 2017)

**Research status: Partial.** The official insert verifies contents and the Investigate/Hidden Witness rules, but its contents list does not name cards and C1 reports one more Scheme identifier than the official Scheme count.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| N | [Upper Deck Marvel Noir rules insert](https://upperdeck.com/wp-content/uploads/2024/05/2017_LegendaryNOIR_Rules.pdf) | Contents (PDF p.2), Investigate and Hidden Witness rules (PDF pp.1–2), card clarifications. |
| C1 | [master-strike structured noir card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/noir.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | February 2017 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (N p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The category counts sum to 100. The insert does not identify the card names.

### Group inventory (C1)

- **Heroes (five):** Angel Noir; Daredevil Noir; Iron Man Noir; Luke Cage Noir; Spider-Man Noir.
- **Villain Groups (two):** Goblin's Freak Show; X-Men Noir.
- **Masterminds (two):** Charles Xavier, Professor of Crime; The Goblin, Underworld Boss.
- **Scheme candidates from C1:** Find the Split Personality Killer; Silence the Witnesses; Five Families of Crime; Hidden Heart of Darkness; Detective Wolverine.

The official count is four Schemes, and the insert discusses Hidden Heart of Darkness, but it does not identify the four-card roster. The C1 index below records available metadata for the catalogued Scheme faces but does not establish which four are in the box; the fifth identifier remains unresolved against the cards, excluding promo/Organized Play material per #34. C1's face index below records available structured metadata and linked images; the catalog does not establish which of the conflicting Scheme names is in-box.

## Rules and setup facts

- **Investigate (N p.1):** Look at the top two cards of the named deck. Reveal and draw a qualifying card from them; return the rest above or below that deck in any order. A card may specify another deck or a narrower match.
- **Hidden Witnesses (N pp.1–2):** An effect can place the top Bystanders face-down on a Villain, Mastermind, Scheme, or HQ Hero. A Villain with Hidden Witnesses cannot be fought until they are rescued. During their turn, a player may pay 2 Recruit for each witness to rescue any number into their Victory Pile; they remain Bystanders and resolve their rescue effects.
- **Escape and free-defeat clarifications (N p.2):** A Villain escaping with Bystanders, including Hidden Witnesses, causes each player to discard one card; escaped witnesses enter the Escape Pile face-up. A free defeat rescues all Hidden Witnesses without payment.
- **Charles Xavier, Professor of Crime (N p.2):** Hidden Witnesses on HQ Heroes prevent their recruitment and count as Bystanders for his value. If that Hero is KO'd or leaves the HQ, KO its Hidden Witnesses.
- **Hidden Heart of Darkness (N p.2):** It places Mastermind Tactics in the Villain Deck as Villains; they do not gain bonuses to their Mastermind's value. Fighting one returns it to its owner's Tactics discard as a Tactic, not a Villain; ignore effects that would return it to the face-down Tactics during this Scheme.

The insert does not give all four Scheme setup lines or either Mastermind's complete Always Leads and setup text. Player limits, Twist counts, required groups/Heroes, and any other moves or setup steps remain to be checked against the cards.

## Required parts and glossary

- Hidden Witnesses use the existing Bystander supply; the rulesheet lists no additional stack or token count (N pp.1–2).
- **Investigate:** Inspect the top two cards of a named deck, select a permitted card, and arrange the remainder above or below it. (N p.1)
- **Hidden Witness:** A face-down Bystander held by a card; a player may pay 2 Recruit to rescue it into their Victory Pile. (N pp.1–2)

Summaries are original paraphrases under 40 words. Hero metadata, other card terms, and the complete printed inventory need card-level verification and citations to the governing rules.

## Setup and implementation gaps

The C1 Scheme roster must be reconciled with the official four-Scheme count without adding a possible promo. Verify each Scheme and Mastermind's setup effects, Always Leads, player limits, required groups, and Hero metadata from the product cards. This record does not change runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Angel Noir

- **Impetuous Dive**; type/group: Hero / Angel Noir; copies: Unverified; Hero Name: Angel Noir; team: X-Men; class icons: Instinct; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-noir-03.png).
- **Multitalented**; type/group: Hero / Angel Noir; copies: Unverified; Hero Name: Angel Noir; team: X-Men; class icons: Strength; printed values: Cost 4; Recruit 1; Attack 1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-noir-04.png).
- **Identical Twin Brother**; type/group: Hero / Angel Noir; copies: Unverified; Hero Name: Angel Noir; team: X-Men; class icons: Instinct; printed values: Cost 5; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-noir-02.png).
- **Missing Person Case**; type/group: Hero / Angel Noir; copies: Unverified; Hero Name: Angel Noir; team: X-Men; class icons: Covert; printed values: Cost 8; Attack 3; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-noir-01.png).

### Hero group: Daredevil Noir

- **Balancing Act**; type/group: Hero / Daredevil Noir; copies: Unverified; Hero Name: Daredevil Noir; team: Marvel Knights; class icons: Covert; printed values: Cost 3; Recruit 1; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-04-2.png).
- **Listen for Heartbeats**; type/group: Hero / Daredevil Noir; copies: Unverified; Hero Name: Daredevil Noir; team: Marvel Knights; class icons: Instinct; printed values: Cost 4; Attack 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-03-2.png).
- **Discover the Bodies**; type/group: Hero / Daredevil Noir; copies: Unverified; Hero Name: Daredevil Noir; team: Marvel Knights; class icons: Covert; printed values: Cost 5; Recruit 3; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-02-2.png).
- **Hitting Rock Bottom**; type/group: Hero / Daredevil Noir; copies: Unverified; Hero Name: Daredevil Noir; team: Marvel Knights; class icons: Instinct; printed values: Cost 7; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-01-2.png).

### Hero group: Iron Man Noir

- **Steam-Powered Arsenal**; type/group: Hero / Iron Man Noir; copies: Unverified; Hero Name: Iron Man Noir; team: Avengers; class icons: Ranged; printed values: Cost 3; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-noir-04.png).
- **Mechanized Plate-Mail**; type/group: Hero / Iron Man Noir; copies: Unverified; Hero Name: Iron Man Noir; team: Avengers; class icons: Tech; printed values: Cost 4; Recruit 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-noir-03.png).
- **Learn from Enemies**; type/group: Hero / Iron Man Noir; copies: Unverified; Hero Name: Iron Man Noir; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-noir-02.png).
- **Adventurers Assemble!**; type/group: Hero / Iron Man Noir; copies: Unverified; Hero Name: Iron Man Noir; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 4; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-man-noir-01.png).

### Hero group: Luke Cage Noir

- **Follow Big Leads**; type/group: Hero / Luke Cage Noir; copies: Unverified; Hero Name: Luke Cage Noir; team: Marvel Knights; class icons: Strength; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-noir-04.png).
- **Private Investigations**; type/group: Hero / Luke Cage Noir; copies: Unverified; Hero Name: Luke Cage Noir; team: Marvel Knights; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-noir-03.png).
- **Unbreakable Cage**; type/group: Hero / Luke Cage Noir; copies: Unverified; Hero Name: Luke Cage Noir; team: Marvel Knights; class icons: Strength; printed values: Cost 6; Attack 4; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-noir-02.png).
- **Weight of the World**; type/group: Hero / Luke Cage Noir; copies: Unverified; Hero Name: Luke Cage Noir; team: Marvel Knights; class icons: Strength; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-noir-01.png).

### Hero group: Spider-Man Noir

- **Gumshoe's Revolver**; type/group: Hero / Spider-Man Noir; copies: Unverified; Hero Name: Spider-Man Noir; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-noir-03.png).
- **Webs of Darkness**; type/group: Hero / Spider-Man Noir; copies: Unverified; Hero Name: Spider-Man Noir; team: Spider Friends; class icons: Ranged; printed values: Cost 2; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-noir-04.png).
- **Solve the Crime**; type/group: Hero / Spider-Man Noir; copies: Unverified; Hero Name: Spider-Man Noir; team: Spider Friends; class icons: Instinct; printed values: Cost 2; Attack 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-noir-02.png).
- **Spider-Totem's Chosen**; type/group: Hero / Spider-Man Noir; copies: Unverified; Hero Name: Spider-Man Noir; team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-man-noir-01.png).

### Villain Group: Goblin's Freak Show

- **Vulture, Carnival Cannibal**; type/group: Villain / Goblin's Freak Show; copies: 2; printed values: Attack 5*; VP 3; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/goblins-freak-show-06.png).
- **The Chameleon**; type/group: Villain / Goblin's Freak Show; copies: 2; printed values: Attack 4*; VP 2; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/goblins-freak-show-04.png).
- **Kraven, Animal Trainer**; type/group: Villain / Goblin's Freak Show; copies: 1; printed values: Attack *; VP 4; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/goblins-freak-show-01.png).
- **Ox**; type/group: Villain / Goblin's Freak Show; copies: 1; printed values: Attack 5*; VP 3; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/goblins-freak-show-03.png).
- **Montana**; type/group: Villain / Goblin's Freak Show; copies: 1; printed values: Attack 4*; VP 2; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/goblins-freak-show-02.png).
- **Fancy Dan**; type/group: Villain / Goblin's Freak Show; copies: 1; printed values: Attack 1*; VP 2; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/goblins-freak-show-05.png).

### Villain Group: X-Men Noir

- **Bobby “Iceman“ Drake**; type/group: Villain / X-Men Noir; copies: 1; printed values: Attack 4; VP 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-07.png).
- **Comrade Rasputin, Steel Wall**; type/group: Villain / X-Men Noir; copies: 2; printed values: Attack 5; VP 3; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-06.png).
- **Henry “Beast“ McCoy**; type/group: Villain / X-Men Noir; copies: 1; printed values: Attack 5; VP 3; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-05.png).
- **Jean Grey Noir**; type/group: Villain / X-Men Noir; copies: 1; printed values: Attack 5; VP 3; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-01.png).
- **Scott “Cyclops“ Summers**; type/group: Villain / X-Men Noir; copies: 1; printed values: Attack 6; VP 4; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-03.png).
- **Warden Emma Frost**; type/group: Villain / X-Men Noir; copies: 1; printed values: Attack 6; VP 4; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-04.png).
- **Ororo Munroe, Storm-Tossed**; type/group: Villain / X-Men Noir; copies: 1; printed values: Attack 4; VP 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-noir-02.png).

### Mastermind: Charles Xavier, Professor of Crime

- **Charles Xavier, Professor of Crime**; type/group: Normal Mastermind face / Charles Xavier, Professor of Crime; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/charles-xavier-01.png).
- **Commit to the Asylum**; type/group: Mastermind Tactic / Charles Xavier, Professor of Crime; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/charles-xavier-02.png).
- **Master Manipulator**; type/group: Mastermind Tactic / Charles Xavier, Professor of Crime; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/charles-xavier-04.png).
- **Corrupt Weak Minds**; type/group: Mastermind Tactic / Charles Xavier, Professor of Crime; copies: Unverified; printed values: not indexed in C1; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/charles-xavier-03.png).
- **X-Con Men**; type/group: Mastermind Tactic / Charles Xavier, Professor of Crime; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/charles-xavier-05.png).

### Mastermind: The Goblin, Underworld Boss

- **The Goblin, Underworld Boss**; type/group: Normal Mastermind face / The Goblin, Underworld Boss; copies: Unverified; printed values: VP 6; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/goblin-underworld-boss-01.png).
- **Sinister Dreams**; type/group: Mastermind Tactic / The Goblin, Underworld Boss; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/goblin-underworld-boss-05.png).
- **Blackmail the Judges**; type/group: Mastermind Tactic / The Goblin, Underworld Boss; copies: Unverified; printed values: not indexed in C1; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/goblin-underworld-boss-02.png).
- **Carnival of Carnage**; type/group: Mastermind Tactic / The Goblin, Underworld Boss; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/goblin-underworld-boss-04.png).
- **Blind Loyalty**; type/group: Mastermind Tactic / The Goblin, Underworld Boss; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/goblin-underworld-boss-03.png).

### Scheme: Find the Split Personality Killer

- **Find the Split Personality Killer**; type/group: Scheme / Find the Split Personality Killer; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/75Scheme(92).png).

### Scheme: Silence the Witnesses

- **Silence the Witnesses**; type/group: Scheme / Silence the Witnesses; copies: Unverified; printed values: not indexed in C1; keyword labels: Hidden Witness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/72Scheme(89).png).

### Scheme: Five Families of Crime

- **Five Families of Crime**; type/group: Scheme / Five Families of Crime; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/74Scheme(91).png).

### Scheme: Hidden Heart of Darkness

- **Hidden Heart of Darkness**; type/group: Scheme / Hidden Heart of Darkness; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/73Scheme(90).png).

### Bystander set: Detective Wolverine

- **Detective Wolverine**; type/group: Bystander / Detective Wolverine; copies: 1; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-detective-wolverine.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
