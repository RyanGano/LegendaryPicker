# X-Men (June 2017)

**Research status: Partial; integrated (#132).** The official insert provides the 394-card breakdown and extensive mechanics, but not the full card manifest or individual Scheme/Mastermind setup lines. The Scheme Setup lines, Always Leads and part uses are now read from C1 and its card-image links into `LegendaryPickerService/Data/Boxes/x-men.json` (see the X-Men section of `Docs/Plan.md`). C1's Special Bystander names still exceed the official type count, and the token names remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| XM | [Upper Deck X-Men rules insert](https://upperdeck.com/wp-content/uploads/2024/05/2017_Legendary_XMen_Rules.pdf) | Contents (PDF p.2), mechanics and card types (PDF pp.1–2). |
| C1 | [master-strike structured x-men card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/xmen.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | June 2017 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (XM p.2)

| Type | Official count |
|---|---:|
| Heroes | 15 groups × 14 cards = 210 |
| Villain Groups | 7 groups × 8 cards = 56 |
| Henchman Groups | 5 groups × 10 cards = 50 |
| Double-Sided Masterminds | 6 sets × 5 cards = 30 |
| Schemes | 8 |
| Token cards | 9 |
| Master Strike | 1 |
| Scheme Twist | 1 |
| Special Bystanders | 9 types × 1 card = 9 |
| Horrors | 20 |
| Total cards | 394 |

The listed categories sum to the official 394-card total.

### Group inventory (C1)

- **Heroes (15):** Aurora & Northstar; Banshee; Beast; Cannonball; Colossus & Wolverine; Dazzler; Havok; Jubilee; Kitty Pryde; Legion; Longshot; Phoenix; Polaris; Psylocke; X-23.
- **Villain Groups (seven):** Dark Descendants; Hellfire Club; Mojoverse; Murderworld; Shadow-X; Shi'ar Imperial Guard; Sisterhood of Mutants.
- **Henchman Groups (five):** The Brood; Hellfire Cult; Sapien League; Shi'ar Death Commandos; Shi'ar Patrol Craft.
- **Masterminds (six):** Arcade; Dark Phoenix; Deathbird; Mojo; Onslaught; Shadow King. Each has a regular and Epic side using the same Tactics (XM p.2).
- **Schemes (eight):** Alien Brood Encounters; Anti-Mutant Hatred; The Dark Phoenix Saga; Horror of Horrors; Mutant-Hunting Super Sentinels; Nuclear Armageddon; Televised Deathtraps of Mojoworld; X-Men Danger Room Goes Berserk.
- **Special Bystander name leads from C1 (10):** Cypher; Heartless Computer Scientist; Karma; Magik; Magma; Martial Arts Master; Mirage; Sunspot; Warlock; Wolfsbane. The official insert says there are nine Special Bystander types, so this catalog list is not reconciled.

The official insert does not name the nine token cards or the 20 Horrors. The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Missing token identities, fields absent from C1, Always Leads, and Scheme setup effects remain unresolved.

## Rules and mechanisms

- **X-Gene (XM p.1):** A conditional bonus works only when the specified card is already in the player's discard pile. Each X-Gene ability can be used once; cards played this turn do not reach the discard pile until the turn ends.
- **Piercing Energy (XM p.1):** Spend it equal to an enemy's printed Victory Points to fight, ignoring its Attack and modifiers, special fight conditions, and Human Shields. It cannot target cards without printed Victory Points.
- **Berserk (XM p.1):** Discard the deck's top card and gain Attack based on its printed Attack. Resolve repeated Berserk effects in order; plus-values count their printed base.
- **Soaring Flight (XM p.1):** A recruited Hero with this ability waits aside until the end of the turn, then joins its owner's new hand.
- **Lightshow (XM p.1):** Playing at least two Lightshow cards permits one Lightshow ability from those cards that turn, regardless of how many beyond two were played.
- **Dominate (XM p.1):** Put specified Heroes beneath the enemy; they increase its printed value. When it is fought, distribute one Dominated Hero to each player's discard pile and KO any excess. If it escapes, its Dominated Heroes escape too.
- **Human Shields (XM pp.1–2):** An Ambush can capture the top Bystanders face-down. A Villain with any cannot be fought until shields are rescued. A player can rescue one at random by paying the printed requirement; the extracted text does not preserve the payment icon, so its exact resource is unresolved here. Shields remain Bystanders and escape/discard as Bystanders do.
- **Traps (XM p.1):** A Trap drawn from the Villain Deck presents a challenge for that turn. Success sends it to the player's Victory Pile for its points; failure KOs it after the new hand is drawn and applies its listed penalty. A Trap is not a Villain and does not enter the city.
- **Double-Sided Epic Masterminds (XM p.2):** Each of the six can use a normal or Epic side with the same four Tactics. The insert describes the Epic side as an added challenge.
- **Horrors (XM p.2):** The 20 Horror cards add difficulty. Epic Masterminds explicitly add Horrors; players may also choose to include any number at game start.
- **Heroic Bystanders and Token cards (XM p.2):** New Mutant Bystanders become Heroes when rescued. Token cards represent named Villains or Masterminds that effects add during play; the insert marks them optional.
- **Multiple Masterminds (XM p.2):** Certain Villains can ascend into Masterminds. Players must defeat all Masterminds; each Master Strike resolves for each one in an order chosen by the active player.
- **Divided Cards (XM p.2):** These return from Civil War. Choose one side when playing; the sides share a cost, and only the chosen side's effects apply.

## Required parts and glossary

- The product adds nine token cards, one Master Strike, one Scheme Twist, nine Special Bystanders, and 20 Horrors (XM p.2). Exact token names remain unavailable in the insert.
- **X-Gene:** Use a conditional bonus once only if its specified card is already in the discard pile. (XM p.1)
- **Piercing Energy:** A resource that fights enemies using their printed Victory Points instead of their Attack value. (XM p.1)
- **Berserk:** Discard the deck's top card and gain Attack based on that card's printed value. (XM p.1)
- **Lightshow:** Two or more cards played with this term allow one of their Lightshow abilities that turn. (XM p.1)
- **Dominate:** Heroes held beneath an enemy raise its value and are distributed or KO'd when it is fought. (XM p.1)
- **Human Shields:** Face-down Bystanders prevent a Villain fight until rescued. (XM pp.1–2)
- **Trap:** A Villain-Deck card that tests the player against a challenge without entering the city. (XM p.1)

Summaries are original paraphrases under 40 words. Remaining card terms, class/team metadata, and exact Hero/Card associations require the product cards and their governing rules pages.

## Setup and implementation gaps

The insert omits all eight Scheme setup lines and the six Masterminds' Always Leads/setup effects. Verify player limits, counts, required groups/Heroes, and setup steps from printed cards. Reconcile C1's ten Special Bystander names against the official nine-card count; verify token/Horror names and the Human Shields payment icon from clear card references.

Integration must account for Traps, mid-game token cards, Horrors, double-sided Epic Masterminds, ascending Masterminds, and Heroic Bystanders. Optional Horrors and Epic sides remain governed by project decisions D4/D2; this record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Aurora & Northstar

- **Northern Lights**; type/group: Hero / Aurora & Northstar; copies: Unverified; Hero Name: Aurora & Northstar; team: X-Men; class icons: Covert; printed values: Cost 3; Attack 2; keyword labels: Soaring Flight, Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/aurora-and-northstar-04.png).
- **Blazing Flare**; type/group: Hero / Aurora & Northstar; copies: Unverified; Hero Name: Aurora & Northstar; team: X-Men; class icons: Ranged; printed values: Cost 4; Recruit 2+; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/aurora-and-northstar-03-1.png).
- **Blazing Fists**; type/group: Hero / Aurora & Northstar; copies: Unverified; Hero Name: Aurora & Northstar; team: X-Men; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Berserk; card image: unavailable in C1.
- **Twin Blast**; type/group: Hero / Aurora & Northstar; copies: Unverified; Hero Name: Aurora & Northstar; team: X-Men; class icons: Ranged; printed values: Cost 5; Attack 2+; keyword labels: Soaring Flight, Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/aurora-and-northstar-02.png).
- **Mach 10**; type/group: Hero / Aurora & Northstar; copies: Unverified; Hero Name: Aurora & Northstar; team: X-Men; class icons: Instinct; printed values: Cost 7; Recruit 4; Attack 0+; keyword labels: Soaring Flight, Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/aurora-and-northstar-01.png).

### Hero group: Banshee

- **Sonar Detection**; type/group: Hero / Banshee; copies: Unverified; Hero Name: Banshee; team: X-Men; class icons: Covert; printed values: Cost 2; Piercing 0+; keyword labels: Piercing Energy, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/banshee-03.png).
- **Speed of Sound**; type/group: Hero / Banshee; copies: Unverified; Hero Name: Banshee; team: X-Men; class icons: Ranged; printed values: Cost 3; Piercing 2; keyword labels: Soaring Flight, Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/banshee-04.png).
- **Sonic Blastwave**; type/group: Hero / Banshee; copies: Unverified; Hero Name: Banshee; team: X-Men; class icons: Ranged; printed values: Cost 5; Recruit 3; Piercing 0+; keyword labels: Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/banshee-02.png).
- **Bone-Shattering Howl**; type/group: Hero / Banshee; copies: Unverified; Hero Name: Banshee; team: X-Men; class icons: Ranged; printed values: Cost 8; Piercing 4; keyword labels: Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/banshee-01.png).

### Hero group: Beast

- **Captivating Conundrum**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: X-Men; class icons: Tech; printed values: Cost 2; Attack 1; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-04.png).
- **Furry Fury**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: X-Men; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Berserk, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-03.png).
- **Calculated Rage**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: X-Men; class icons: Tech; printed values: Cost 5; Attack 3+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-02.png).
- **Recursive Pummeling**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: X-Men; class icons: Tech; printed values: Cost 8; Attack 3+; keyword labels: Berserk, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-01.png).

### Hero group: Cannonball

- **Kinetic Blast Field**; type/group: Hero / Cannonball; copies: Unverified; Hero Name: Cannonball; team: X-Men; class icons: Instinct; printed values: Cost 3; Attack 1+; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cannonball-04.png).
- **Carry to the Air**; type/group: Hero / Cannonball; copies: Unverified; Hero Name: Cannonball; team: X-Men; class icons: Strength; printed values: Cost 4; Recruit 2; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cannonball-03.png).
- **Natural Leader**; type/group: Hero / Cannonball; copies: Unverified; Hero Name: Cannonball; team: X-Men; class icons: Strength; printed values: Cost 6; Attack 3; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cannonball-02.png).
- **Human Cannon**; type/group: Hero / Cannonball; copies: Unverified; Hero Name: Cannonball; team: X-Men; class icons: Strength; printed values: Cost 8; Attack 4+; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cannonball-01.png).

### Hero group: Colossus & Wolverine

- **Reliable**; type/group: Hero / Colossus & Wolverine; copies: Unverified; Hero Name: Colossus & Wolverine; team: X-Men; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-and-wolverine-04-1.png).
- **Unpredictable**; type/group: Hero / Colossus & Wolverine; copies: Unverified; Hero Name: Colossus & Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 3; Attack 1+; keyword labels: Berserk; card image: unavailable in C1.
- **Fastball Special**; type/group: Hero / Colossus & Wolverine; copies: Unverified; Hero Name: Colossus & Wolverine; team: X-Men; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Soaring Flight, X-Gene, Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-and-wolverine-03.png).
- **Insane Disregard for Danger**; type/group: Hero / Colossus & Wolverine; copies: Unverified; Hero Name: Colossus & Wolverine; team: X-Men; class icons: Instinct; printed values: Cost 6; Attack 4+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-and-wolverine-02.png).
- **Uncanny X-Men**; type/group: Hero / Colossus & Wolverine; copies: Unverified; Hero Name: Colossus & Wolverine; team: X-Men; class icons: Strength; printed values: Cost 7; Attack 3+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-and-wolverine-01.png).

### Hero group: Dazzler

- **Convert Sound to Light**; type/group: Hero / Dazzler; copies: Unverified; Hero Name: Dazzler; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 1; Piercing 0+; keyword labels: Piercing Energy, Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dazzler-03.png).
- **Dazzling Glamour**; type/group: Hero / Dazzler; copies: Unverified; Hero Name: Dazzler; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dazzler-04.png).
- **Citywide Mega-Concert**; type/group: Hero / Dazzler; copies: Unverified; Hero Name: Dazzler; team: X-Men; class icons: Tech; printed values: Cost 5; Attack 3; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dazzler-02.png).
- **Inspire the World**; type/group: Hero / Dazzler; copies: Unverified; Hero Name: Dazzler; team: X-Men; class icons: Ranged; printed values: Cost 7; Attack 5; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dazzler-01.png).

### Hero group: Havok

- **Blinding Burst**; type/group: Hero / Havok; copies: Unverified; Hero Name: Havok; team: X-Men; class icons: Ranged; printed values: Cost 3; Recruit 2; Attack 0+; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/havok-04.png).
- **Unleash Havok**; type/group: Hero / Havok; copies: Unverified; Hero Name: Havok; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/havok-03.png).
- **Concussive Plasma**; type/group: Hero / Havok; copies: Unverified; Hero Name: Havok; team: X-Men; class icons: Ranged; printed values: Cost 5; Attack 2+; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/havok-02.png).
- **Radiation Focus Array**; type/group: Hero / Havok; copies: Unverified; Hero Name: Havok; team: X-Men; class icons: Tech; printed values: Cost 7; Attack 3+; keyword labels: Berserk, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/havok-01.png).

### Hero group: Jubilee

- **Light a Spark**; type/group: Hero / Jubilee; copies: Unverified; Hero Name: Jubilee; team: X-Men; class icons: Covert; printed values: Cost 2; Recruit 0+; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jubilee-03.png).
- **Blasting Fireworks**; type/group: Hero / Jubilee; copies: Unverified; Hero Name: Jubilee; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 1+; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jubilee-04.png).
- **Unexpected Explosion**; type/group: Hero / Jubilee; copies: Unverified; Hero Name: Jubilee; team: X-Men; class icons: Instinct; printed values: Cost 5; Attack 3; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jubilee-02.png).
- **Prismatic Cascade**; type/group: Hero / Jubilee; copies: Unverified; Hero Name: Jubilee; team: X-Men; class icons: Covert; printed values: Cost 7; Recruit 0+; Attack 5+; keyword labels: Lightshow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jubilee-01.png).

### Hero group: Kitty Pryde

- **Intangible Qualities**; type/group: Hero / Kitty Pryde; copies: Unverified; Hero Name: Kitty Pryde; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kitty-pryde-04.png).
- **Going through a Phase**; type/group: Hero / Kitty Pryde; copies: Unverified; Hero Name: Kitty Pryde; team: X-Men; class icons: Covert; printed values: Cost 4; Recruit 1; Attack 1; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kitty-pryde-03.png).
- **Ghost in the Machine**; type/group: Hero / Kitty Pryde; copies: Unverified; Hero Name: Kitty Pryde; team: X-Men; class icons: Tech; printed values: Cost 6; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kitty-pryde-02.png).
- **Lockheed, Kitty's Dragon**; type/group: Hero / Kitty Pryde; copies: Unverified; Hero Name: Kitty Pryde; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 0+; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kitty-pryde-01.png).

### Hero group: Legion

- **Bend Steel**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Strength; printed values: Cost 2; Attack 1+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/legion-04-1.png).
- **Bend Light**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Covert; printed values: Cost 2; Recruit 1+; keyword labels: Lightshow; card image: unavailable in C1.
- **Split Personality**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Tech; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/legion-03-1.png).
- **Split Eardrums**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Ranged; printed values: Cost 3; Piercing 2; keyword labels: Piercing Energy; card image: unavailable in C1.
- **Channel Time**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Instinct; printed values: Cost 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/legion-02-1.png).
- **Channel Fire**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Tech; printed values: Cost 5; Attack 0+; card image: unavailable in C1.
- **Maelstrom of Clashing Powers**; type/group: Hero / Legion; copies: Unverified; Hero Name: Legion; team: X-Men; class icons: Covert; printed values: Cost 8; Attack 3+; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/legion-01.png).

### Hero group: Longshot

- **Fortune Favors the Bold**; type/group: Hero / Longshot; copies: Unverified; Hero Name: Longshot; team: X-Men; class icons: Instinct; printed values: Cost 3; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/longshot-04.png).
- **Flurry of Blades**; type/group: Hero / Longshot; copies: Unverified; Hero Name: Longshot; team: X-Men; class icons: Tech; printed values: Cost 4; Attack 2+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/longshot-03.png).
- **Make My Own Luck**; type/group: Hero / Longshot; copies: Unverified; Hero Name: Longshot; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/longshot-02.png).
- **Escape from Mojo World**; type/group: Hero / Longshot; copies: Unverified; Hero Name: Longshot; team: X-Men; class icons: Tech; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/longshot-01.png).

### Hero group: Phoenix

- **Life & Death Incarnate**; type/group: Hero / Phoenix; copies: Unverified; Hero Name: Phoenix; team: X-Men; class icons: Strength; printed values: Cost 3; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-03.png).
- **Obliterating Fire**; type/group: Hero / Phoenix; copies: Unverified; Hero Name: Phoenix; team: X-Men; class icons: Ranged; printed values: Cost 4; Piercing 4; keyword labels: Soaring Flight, Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-04.png).
- **Reincarnating Phoenix**; type/group: Hero / Phoenix; copies: Unverified; Hero Name: Phoenix; team: X-Men; class icons: Covert; printed values: Cost 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-02.png).
- **Driven Mad by Power**; type/group: Hero / Phoenix; copies: Unverified; Hero Name: Phoenix; team: X-Men; class icons: Strength; printed values: Cost 9; Attack 6+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-01.png).

### Hero group: Polaris

- **Ride the Magnetic Waves**; type/group: Hero / Polaris; copies: Unverified; Hero Name: Polaris; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/polaris-04.png).
- **Electromagnetic Pulse**; type/group: Hero / Polaris; copies: Unverified; Hero Name: Polaris; team: X-Men; class icons: Ranged; printed values: Cost 4; Piercing 2; keyword labels: Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/polaris-03.png).
- **Subtle Attunement**; type/group: Hero / Polaris; copies: Unverified; Hero Name: Polaris; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 2; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/polaris-02.png).
- **Reverse Polarity**; type/group: Hero / Polaris; copies: Unverified; Hero Name: Polaris; team: X-Men; class icons: Covert; printed values: Cost 8; Recruit 4; keyword labels: Soaring Flight, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/polaris-01.png).

### Hero group: Psylocke

- **Psychic Knife**; type/group: Hero / Psylocke; copies: Unverified; Hero Name: Psylocke; team: X-Men; class icons: Instinct; printed values: Cost 2; Piercing 0+; keyword labels: Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psylocke-04.png).
- **Precognition**; type/group: Hero / Psylocke; copies: Unverified; Hero Name: Psylocke; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psylocke-03.png).
- **Butterfly Effect**; type/group: Hero / Psylocke; copies: Unverified; Hero Name: Psylocke; team: X-Men; class icons: Covert; printed values: Cost 5; Piercing 2+; keyword labels: Piercing Energy, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psylocke-02.png).
- **Telepathic Ninjutsu**; type/group: Hero / Psylocke; copies: Unverified; Hero Name: Psylocke; team: X-Men; class icons: Instinct; printed values: Cost 7; Piercing 3; keyword labels: Piercing Energy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/psylocke-01.png).

### Hero group: X-23

- **Adamantium Foot Claws**; type/group: Hero / X-23; copies: Unverified; Hero Name: X-23; team: X-Men; class icons: Tech; printed values: Cost 3; Attack 2; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x23-04.png).
- **Healing Factor Genome**; type/group: Hero / X-23; copies: Unverified; Hero Name: X-23; team: X-Men; class icons: Instinct; printed values: Cost 4; Attack 2+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x23-03.png).
- **Bioengineered Assassin**; type/group: Hero / X-23; copies: Unverified; Hero Name: X-23; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 2+; keyword labels: Berserk, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x23-02.png).
- **Heir to Wolverine**; type/group: Hero / X-23; copies: Unverified; Hero Name: X-23; team: X-Men; class icons: Instinct; printed values: Cost 7; Attack 3+; keyword labels: Berserk, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x23-01.png).

### Villain Group: Dark Descendants

- **Fatale**; type/group: Villain / Dark Descendants; copies: 2; printed values: Attack 5; VP 3; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-descendants-02.png).
- **Havok, Brainwashed**; type/group: Trap / Dark Descendants; copies: 2; printed values: Attack 2+; Attack 6; keyword labels: Dominate, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-descendants-04.png).
- **Nemesis**; type/group: Villain / Dark Descendants; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-descendants-01.png).
- **Psychic Subjugation**; type/group: Villain; subtype Trap / Dark Descendants; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-descendants-05.png).
- **Random**; type/group: Villain / Dark Descendants; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-descendants-03.png).

### Villain Group: Hellfire Club

- **Corrupt the Phoenix Force**; type/group: Villain; subtype Trap / Hellfire Club; copies: 1; printed values: VP 3; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/corrupt-the-phoenix.png).
- **Emma Frost (White Queen)**; type/group: Villain / Hellfire Club; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellfire-club-04.png).
- **Harry Leland (Black Bishop)**; type/group: Villain / Hellfire Club; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellfire-club-03.png).
- **Mastermind (Jason Wyngarde)**; type/group: Villain / Hellfire Club; copies: 1; printed values: Attack 8+; VP 6; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellfire-club-02.png).
- **Sebastian Shaw (Black King)**; type/group: Villain / Hellfire Club; copies: 2; printed values: Attack 3+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellfire-club-01.png).

### Villain Group: Mojoverse

- **Mindwarping TV Broadcast**; type/group: Villain; subtype Trap / Mojoverse; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mojoverse-05.png).
- **Minor Domo**; type/group: Villain / Mojoverse; copies: 2; printed values: Attack 2*; VP 2; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mojoverse-02.png).
- **Major Domo**; type/group: Villain / Mojoverse; copies: 2; printed values: Attack 4*; VP 3; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mojoverse-01.png).
- **Spiral**; type/group: Villain / Mojoverse; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mojoverse-04.png).
- **Warwolves**; type/group: Villain / Mojoverse; copies: 2; printed values: Attack 3*; VP 2; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mojoverse-03.png).

### Villain Group: Murderworld

- **Animatronic Killer Clowns**; type/group: Villain; subtype Trap / Murderworld; copies: 2; printed values: VP 2; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/murderworld-06.png).
- **Guillotine Rollercoaster**; type/group: Villain; subtype Trap / Murderworld; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/murderworld-05.png).
- **Miss Locke**; type/group: Villain / Murderworld; copies: 2; printed values: Attack 2*; VP 2; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/murderworld-04.png).
- **Monstrous Pinball Machine**; type/group: Villain; subtype Trap / Murderworld; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/murderworld-02.png).
- **Sulfuric Acid Water Slide**; type/group: Villain; subtype Trap / Murderworld; copies: 2; printed values: VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/murderworld-01.png).

### Villain Group: Shadow-X

- **Betrayal of the Shadow**; type/group: Villain; subtype Trap / Shadow-X; copies: 1; printed values: VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shadow-x-01.png).
- **Dark Angel**; type/group: Trap / Shadow-X; copies: 2; printed values: Attack 2; Attack 4; keyword labels: X-Gene, Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shadow-x-04.png).
- **Dark Beast**; type/group: Trap / Shadow-X; copies: 1; printed values: Attack 2; Attack 5; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shadow-x-06.png).
- **Dark Cyclops**; type/group: Trap / Shadow-X; copies: 1; printed values: Attack 3; Attack 7; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shadow-x-02.png).
- **Dark Iceman**; type/group: Trap / Shadow-X; copies: 2; printed values: Attack 2; Attack 5; keyword labels: X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shadow-x-05.png).
- **Dark Marvel Girl**; type/group: Trap / Shadow-X; copies: 1; printed values: Attack 2; Attack 4+; keyword labels: Dominate, X-Gene; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shadow-x-03.png).

### Villain Group: Shi'ar Imperial Guard

- **Blackthorn**; type/group: Villain / Shi'ar Imperial Guard; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar-imperial-guard-03.png).
- **Gladiator**; type/group: Villain / Shi'ar Imperial Guard; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar-imperial-guard-05.png).
- **Oracle**; type/group: Villain / Shi'ar Imperial Guard; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar-imperial-guard-04.png).
- **Shi'ar Trial by Combat**; type/group: Villain; subtype Trap / Shi'ar Imperial Guard; copies: 1; printed values: VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar-imperial-guard-01.png).
- **Smasher**; type/group: Villain / Shi'ar Imperial Guard; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shiar-imperial-guard-02.png).

### Villain Group: Sisterhood of Mutants

- **Lady Deathstrike**; type/group: Villain / Sisterhood of Mutants; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sisterhood-of-mutants-04.png).
- **Lady Mastermind**; type/group: Villain / Sisterhood of Mutants; copies: 1; printed values: Attack 7+; VP 5; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sisterhood-of-mutants-02.png).
- **Resurrect Madelyne Pryor**; type/group: Villain; subtype Trap / Sisterhood of Mutants; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sisterhood-of-mutants-05.png).
- **Selene**; type/group: Villain / Sisterhood of Mutants; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sisterhood-of-mutants-03.png).
- **Typhoid Mary**; type/group: Villain / Sisterhood of Mutants; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sisterhood-of-mutants-01.png).

### Henchman Group: Brood, The

- **Brood, The**; type/group: Henchman / Brood, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/the-brood.png).

### Henchman Group: Hellfire Cult

- **Hellfire Cult**; type/group: Henchman / Hellfire Cult; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/hellfire-cult.png).

### Henchman Group: Sapien League

- **Sapien League**; type/group: Henchman / Sapien League; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/sapien-league.png).

### Henchman Group: Shi'ar Death Commandos

- **Shi'ar Death Commandos**; type/group: Henchman / Shi'ar Death Commandos; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/shiar-death-commandos.png).

### Henchman Group: Shi'ar Patrol Craft

- **Shi'ar Patrol Craft**; type/group: Henchman / Shi'ar Patrol Craft; copies: Unverified; printed values: not indexed in C1; keyword labels: Soaring Flight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/shiar-patrol-craft.png).

### Mastermind: Arcade

- **Arcade**; type/group: Normal Mastermind face / Arcade; copies: Unverified; printed values: VP 5; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arcade-01.png).
- **Epic Arcade**; type/group: Epic Mastermind face / Arcade; copies: Unverified; printed values: Attack 4*; VP 5; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arcade-01a.png).
- **I Love a Parade!**; type/group: Mastermind Tactic / Arcade; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arcade-04.png).
- **I Need an Audience**; type/group: Mastermind Tactic / Arcade; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arcade-05.png).
- **Roulette Wheel of Death**; type/group: Mastermind Tactic / Arcade; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arcade-03.png).
- **Welcome to my Theme Park!**; type/group: Mastermind Tactic / Arcade; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/arcade-02.png).

### Mastermind: Dark Phoenix

- **Dark Phoenix**; type/group: Normal Mastermind face / Dark Phoenix; copies: Unverified; printed values: VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/darak-phoenix-01.png).
- **Epic Dark Phoenix**; type/group: Epic Mastermind face / Dark Phoenix; copies: Unverified; printed values: Attack 15; VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/darak-phoenix-01a.png).
- **Burn the World to Ashes**; type/group: Mastermind Tactic / Dark Phoenix; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/darak-phoenix-04.png).
- **Consume an Entire Galaxy**; type/group: Mastermind Tactic / Dark Phoenix; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/darak-phoenix-03.png).
- **Fiery Reincarnation**; type/group: Mastermind Tactic / Dark Phoenix; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/darak-phoenix-02.png).
- **Worship Me as a God**; type/group: Mastermind Tactic / Dark Phoenix; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/darak-phoenix-05.png).

### Mastermind: Deathbird

- **Deathbird**; type/group: Normal Mastermind face / Deathbird; copies: Unverified; printed values: Attack 8+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/deathbird_01.png).
- **Epic Deathbird**; type/group: Epic Mastermind face / Deathbird; copies: Unverified; printed values: Attack 10+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/deathbird_01a.png).
- **Shi'ar Elite Bodyguards**; type/group: Mastermind Tactic / Deathbird; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/deathbird_05.png).
- **Shi'ar Extermination Legion**; type/group: Mastermind Tactic / Deathbird; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/deathbird_04.png).
- **Shi'ar Hovertake Battalion**; type/group: Mastermind Tactic / Deathbird; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/deathbird_03.png).
- **Shi'ar Master Spies**; type/group: Mastermind Tactic / Deathbird; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/deathbird_02.png).

### Mastermind: Mojo

- **Mojo**; type/group: Normal Mastermind face / Mojo; copies: Unverified; printed values: VP 5; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mojo-01.png).
- **Epic Mojo**; type/group: Epic Mastermind face / Mojo; copies: Unverified; printed values: Attack 7*; VP 5; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mojo-01a.png).
- **Billions of TV Viewers**; type/group: Mastermind Tactic / Mojo; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mojo-04.png).
- **Brain-Melting TV Marathon**; type/group: Mastermind Tactic / Mojo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mojo-05.png).
- **Cross-Dimensional Marketing**; type/group: Mastermind Tactic / Mojo; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mojo-02.png).
- **Mojo Branding Opportunity**; type/group: Mastermind Tactic / Mojo; copies: Unverified; printed values: not indexed in C1; keyword labels: Human Shields; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mojo-03.png).

### Mastermind: Onslaught

- **Onslaught**; type/group: Normal Mastermind face / Onslaught; copies: Unverified; printed values: Attack 10+; VP 7; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/onslaught-01.png).
- **Epic Onslaught**; type/group: Epic Mastermind face / Onslaught; copies: Unverified; printed values: Attack 12+; VP 7; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/onslaught-01a.png).
- **Godlike Psionic Entity**; type/group: Mastermind Tactic / Onslaught; copies: Unverified; printed values: not indexed in C1; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/onslaught-05.png).
- **Sins of X-Men Past**; type/group: Mastermind Tactic / Onslaught; copies: Unverified; printed values: not indexed in C1; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/onslaught-02.png).
- **Xavier and Magneto Combined**; type/group: Mastermind Tactic / Onslaught; copies: Unverified; printed values: not indexed in C1; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/onslaught-04.png).
- **Worldwide Mental Control**; type/group: Mastermind Tactic / Onslaught; copies: Unverified; printed values: not indexed in C1; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/onslaught-03.png).

### Mastermind: Shadow King

- **Shadow King**; type/group: Normal Mastermind face / Shadow King; copies: Unverified; printed values: Attack 7+; VP 6; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shadow-king-01.png).
- **Epic Shadow King**; type/group: Epic Mastermind face / Shadow King; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shadow-king-01a.png).
- **Fiend of the Astral Plane**; type/group: Mastermind Tactic / Shadow King; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shadow-king-05.png).
- **Poison their Minds**; type/group: Mastermind Tactic / Shadow King; copies: Unverified; printed values: not indexed in C1; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shadow-king-02.png).
- **Psychic Seduction**; type/group: Mastermind Tactic / Shadow King; copies: Unverified; printed values: not indexed in C1; keyword labels: Dominate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shadow-king-03.png).
- **Telepathic Betrayal**; type/group: Mastermind Tactic / Shadow King; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shadow-king-04.png).

### Scheme: Alien Brood Encounters

- **Alien Brood Encounters**; type/group: Scheme / Alien Brood Encounters; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/81Scheme(96).png).

### Scheme: Anti-Mutant Hatred

- **Anti-Mutant Hatred**; type/group: Scheme / Anti-Mutant Hatred; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/83Scheme(98).png).

### Scheme: Dark Phoenix Saga, The

- **Dark Phoenix Saga, The**; type/group: Scheme / Dark Phoenix Saga, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/77Scheme(16).png).

### Scheme: Horror of Horrors

- **Horror of Horrors**; type/group: Scheme / Horror of Horrors; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/80Scheme(95).png).

### Scheme: Mutant-Hunting Super Sentinels

- **Mutant-Hunting Super Sentinels**; type/group: Scheme / Mutant-Hunting Super Sentinels; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/78Scheme(93).png).

### Scheme: Nuclear Armageddon

- **Nuclear Armageddon**; type/group: Scheme / Nuclear Armageddon; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/79Scheme(94).png).

### Scheme: Televised Deathtraps of Mojoworld

- **Televised Deathtraps of Mojoworld**; type/group: Scheme / Televised Deathtraps of Mojoworld; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/82Scheme(97).png).

### Scheme: X-Men Danger Room Goes Berserk

- **X-Men Danger Room Goes Berserk**; type/group: Scheme / X-Men Danger Room Goes Berserk; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/76Scheme(15).png).

### Bystander set: Cypher

- **Cypher**; type/group: Trap / Cypher; copies: 1; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-cypher.png).

### Bystander set: Heartless Computer Scientist

- **Heartless Computer Scientist**; type/group: Trap / Heartless Computer Scientist; copies: 1; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/heartless-computer-scientist.png).

### Bystander set: Karma

- **Karma**; type/group: Trap / Karma; copies: 1; printed values: Cost 3; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-karma.png).

### Bystander set: Magik

- **Magik**; type/group: Trap / Magik; copies: 1; printed values: Cost 4; Attack 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-magik.png).

### Bystander set: Magma

- **Magma**; type/group: Trap / Magma; copies: 1; printed values: Cost 3; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-magma.png).

### Bystander set: Martial Arts Master

- **Martial Arts Master**; type/group: Trap / Martial Arts Master; copies: 1; printed values: Cost 3; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/martial-arts-master.png).

### Bystander set: Mirage

- **Mirage**; type/group: Trap / Mirage; copies: 1; printed values: Cost 3; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-mirage.png).

### Bystander set: Sunspot

- **Sunspot**; type/group: Trap / Sunspot; copies: 1; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-sunspot.png).

### Bystander set: Warlock

- **Warlock**; type/group: Trap / Warlock; copies: 1; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-warlock.png).

### Bystander set: Wolfsbane

- **Wolfsbane**; type/group: Trap / Wolfsbane; copies: 1; printed values: Cost 3; Attack 0+; keyword labels: Berserk; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-wolfsbane.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
