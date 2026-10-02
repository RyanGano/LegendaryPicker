# Spider-Man Homecoming (October 2017)

**Research status: Partial.** The official insert verifies the product counts and several mechanics; printed card images supply the setup facts summarized below.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| Card | [Printed Scheme face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-distract-the-hero.png); the other faces are linked individually below | Scheme setup lines and Mastermind Always Leads; direct links identify each printed face. |
| SM | [Upper Deck Spider-Man Homecoming rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2017_Legendary_SMHC_Rules.pdf) | Contents (PDF p.2), mechanics and clarifications (PDF pp.1–2). |
| C1 | [master-strike structured spider-man-homecoming card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/spiderhomecoming.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | October 2017 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (SM p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Happy Hogan; High Tech Spider-Man; Peter Parker, Homecoming; Peter's Allies; Tony Stark.
- **Villain Groups (two):** Salvagers; Vulture Tech.
- **Masterminds (two):** Adrian Toomes; Vulture. Each includes a normal and Epic card plus four Tactics (SM p.2).
- **Schemes (four):** Distract the Hero; Explosion at the Washington Monument; Ferry Disaster; Scavenge Alien Weaponry.

C1 supplies structured face metadata, with a direct printed-face link for each catalogued face below. Ability prose is not reproduced or treated as rules evidence. The Scheme setup and Mastermind Always Leads entries below cite the corresponding printed faces.

## Rules and mechanisms

- **Danger Sense (SM p.1):** Reveal the specified number of cards from the Villain Deck; gain +1 for each revealed Villain, then return the cards to the top in any order. Specific cards may add other effects.
- **Striker (SM p.1):** Villains and Masterminds gain +1 per Master Strike in the KO pile or stacked next to the Mastermind. Face-up Master Strikes in unusual locations also count; some cards grant this ability to Heroes or multiply its bonus.
- **Wall-Crawl (SM pp.1–2):** When recruiting a Hero with this ability, its player may put it on top of their deck. This does not permit using its other abilities, and gaining it without recruiting does not trigger the ability.
- **Coordinate (SM p.1):** During another player's turn, discard a Coordinate card and draw a replacement to offer that player a copy of the card. Each other player may offer one; the active player may decline. Solo play permits one discard-to-draw per turn. Coordinate is unavailable during Final Showdown.
- **Epic Masterminds (SM p.2):** Either Mastermind can use its normal or Epic side with the same four Tactics.
- **Clarifications (SM p.2):** Liz cannot apply Coordinate more than once to the same Coordinate. Master Strikes KO'd by Watchful Eye still count for Striker. Adrian Toomes's Master Strike does not make City Villains leave the city or KO HQ Heroes.

## Required parts and glossary

The insert lists no product-specific token stack. The printed Scheme setup faces require the shared Bystander and Wound stacks and an additional Henchman Group:

- Explosion at the Washington Monument uses 18 Bystanders and 14 Wounds in its Floor decks.
- Ferry Disaster uses the Bystander Stack as the Ferry.
- Scavenge Alien Weaponry adds a 10-card Henchman Group as Smugglers.

These are existing game components, not new token types. Other component references in individual gameplay abilities are not exhaustively catalogued.

- **Danger Sense:** Inspect the stated number of cards from the Villain Deck, gain a bonus for revealed Villains, and reorder the cards on top. (SM p.1)
- **Striker:** A bonus that grows with face-up Master Strikes in the KO pile or beside the Mastermind. (SM p.1)
- **Wall-Crawl:** A recruited Hero may be put on top of its owner's deck. (SM pp.1–2)
- **Coordinate:** Offer another player a copy of a card by replacing the Coordinate card in hand. (SM p.1)

Summaries are original paraphrases under 40 words. The insert does not establish Hero teams/classes or printed face values; the C1 card-face index records the available metadata and marks missing fields.

## Setup and implementation gaps

Player-count restrictions, if any, remain unverified. Hero face counts are not available from C1; each Hero Group's 14-card total does not verify the count of each printed face. C1 does not index all visible icons, so check the linked images where fields are absent. Existing runtime data and sourced effects are unchanged.

## Scheme and Mastermind setup effects

The following are short setup-only summaries of the printed card faces; they omit the cards' gameplay text.

### Schemes

- **Distract the Hero:** Use 8 Scheme Twists and include at least one Spider Friends Hero in the Hero Deck. [Printed face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-distract-the-hero.png) · copies: 1 (SM p.2).
- **Explosion at the Washington Monument:** Use 8 Scheme Twists; divide 18 Bystanders and 14 Wounds among eight face-down Floor decks. [Printed face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-explosion-at-the-washington-monument.png) · copies: 1 (SM p.2).
- **Ferry Disaster:** Use 9 Scheme Twists and place the Bystander Stack above the Sewers as the Ferry. [Printed face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-ferry-disaster.png) · copies: 1 (SM p.2).
- **Scavenge Alien Weaponry:** Use 7 Scheme Twists and add a 10-card Henchman Group as Smugglers. [Printed face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-scavenge-alien-weaponry.png) · copies: 1 (SM p.2).

### Masterminds

- **Adrian Toomes (normal or Epic face):** Always Leads Salvagers; both faces use the same four Tactics (SM p.2). [Normal face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-01.png) · [Epic face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-02.png).
- **Vulture (normal or Epic face):** Always Leads Vulture Tech; both faces use the same four Tactics (SM p.2). [Normal face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-01.png) · [Epic face](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-02.png).

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Happy Hogan

- **Head of Security**; type/group: Hero / Happy Hogan; copies: Unverified; Hero Name: Happy Hogan; team: Unaffiliated; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/happy-hogan-04.png).
- **Watchful Eye**; type/group: Hero / Happy Hogan; copies: Unverified; Hero Name: Happy Hogan; team: Unaffiliated; class icons: Instinct; printed values: Cost 4; Attack 2+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/happy-hogan-03.png).
- **Loyal Friend**; type/group: Hero / Happy Hogan; copies: Unverified; Hero Name: Happy Hogan; team: Unaffiliated; class icons: Tech; printed values: Cost 5; Attack 0+; keyword labels: Coordinate, Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/happy-hogan-02.png).
- **Asset Management**; type/group: Hero / Happy Hogan; copies: Unverified; Hero Name: Happy Hogan; team: Unaffiliated; class icons: Instinct; printed values: Cost 5; Attack 0+; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/happy-hogan-01.png).

### Hero group: High Tech Spider-Man

- **Advanced Targeting System**; type/group: Hero / High Tech Spider-Man; copies: Unverified; Hero Name: High Tech Spider-Man; team: Spider Friends; class icons: Covert; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ht-spiderman_03.png).
- **Recon Drone Connection**; type/group: Hero / High Tech Spider-Man; copies: Unverified; Hero Name: High Tech Spider-Man; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 0+; keyword labels: Wall-Crawl, Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ht-spiderman_04.png).
- **Spider-Grip**; type/group: Hero / High Tech Spider-Man; copies: Unverified; Hero Name: High Tech Spider-Man; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 2; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ht-spiderman_02.png).
- **Friendly Neighborhood...**; type/group: Hero / High Tech Spider-Man; copies: Unverified; Hero Name: High Tech Spider-Man; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 0+; keyword labels: Wall-Crawl, Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ht-spiderman_01.png).

### Hero group: Peter Parker, Homecoming

- **Avenger in Training**; type/group: Hero / Peter Parker, Homecoming; copies: Unverified; Hero Name: Peter Parker, Homecoming; team: Spider Friends; class icons: Instinct; printed values: Cost 2; Attack 2+; keyword labels: Wall-Crawl, Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-hc-03.png).
- **Heightened Senses**; type/group: Hero / Peter Parker, Homecoming; copies: Unverified; Hero Name: Peter Parker, Homecoming; team: Spider Friends; class icons: Covert; printed values: Cost 2; Attack 0+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-hc-04.png).
- **Homemade Web-Shooters**; type/group: Hero / Peter Parker, Homecoming; copies: Unverified; Hero Name: Peter Parker, Homecoming; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 2+; keyword labels: Wall-Crawl, Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-hc-02.png).
- **Something is Happening**; type/group: Hero / Peter Parker, Homecoming; copies: Unverified; Hero Name: Peter Parker, Homecoming; team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 0+; keyword labels: Wall-Crawl, Coordinate, Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-hc-01.png).

### Hero group: Peter's Allies

- **Ned**; type/group: Hero / Peter's Allies; copies: Unverified; Hero Name: Peter's Allies; team: Spider Friends; class icons: Covert; printed values: Cost 2; Recruit 1+; keyword labels: Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peters-allies-03.png).
- **Michelle**; type/group: Hero / Peter's Allies; copies: Unverified; Hero Name: Peter's Allies; team: Spider Friends; class icons: Covert; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peters-allies-04.png).
- **Liz**; type/group: Hero / Peter's Allies; copies: Unverified; Hero Name: Peter's Allies; team: Spider Friends; class icons: Instinct; printed values: Cost 6; Recruit 4; keyword labels: Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peters-allies-02.png).
- **May Parker**; type/group: Hero / Peter's Allies; copies: Unverified; Hero Name: Peter's Allies; team: Spider Friends; class icons: Covert; printed values: Cost 7; Recruit 5; keyword labels: Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peters-allies-01.png).

### Hero group: Tony Stark

- **Genius, Billionaire...**; type/group: Hero / Tony Stark; copies: Unverified; Hero Name: Tony Stark; team: Avengers; class icons: Tech; printed values: Cost 2; Recruit 1; Attack 1; keyword labels: Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tony-stark-03.png).
- **Stay Out of Trouble**; type/group: Hero / Tony Stark; copies: Unverified; Hero Name: Tony Stark; team: Avengers; class icons: Tech; printed values: Cost 4; Attack 2+; keyword labels: Coordinate, Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tony-stark-04.png).
- **Little Grey Area**; type/group: Hero / Tony Stark; copies: Unverified; Hero Name: Tony Stark; team: Avengers; class icons: Ranged; printed values: Cost 5; Attack 3+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tony-stark-02.png).
- **As Usual, I Did All the Work**; type/group: Hero / Tony Stark; copies: Unverified; Hero Name: Tony Stark; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 5; keyword labels: Coordinate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tony-stark-01.png).

### Villain Group: Salvagers

- **Hybrid Alien Tech**; type/group: Villain / Salvagers; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/salvagers-04.png).
- **Shocker #1**; type/group: Villain / Salvagers; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/salvagers-03.png).
- **Shocker #2**; type/group: Villain / Salvagers; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/salvagers-02.png).
- **Tinkerer**; type/group: Villain / Salvagers; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/salvagers-01.png).

### Villain Group: Vulture Tech

- **Chitauri Weapon Assault**; type/group: Villain / Vulture Tech; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/vulture-tech-03.png).
- **High Tech Helmet**; type/group: Villain / Vulture Tech; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Striker, Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/vulture-tech-02.png).
- **Razor Talons**; type/group: Villain / Vulture Tech; copies: 2; printed values: Attack 2+; VP 2; keyword labels: Striker, Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/vulture-tech-01.png).
- **Turbine Powered**; type/group: Villain / Vulture Tech; copies: 2; printed values: Attack 5+; VP 5; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/vulture-tech-04.png).

### Mastermind: Adrian Toomes

- **Adrian Toomes**; type/group: Normal Mastermind face / Adrian Toomes; copies: 1 physical double-sided card (SM p.2); printed values: Attack 5+; VP 6; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-01.png).
- **Epic Adrian Toomes**; type/group: Epic Mastermind face / Adrian Toomes; copies: 1 physical double-sided card (SM p.2); printed values: Attack 5+; VP 6; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-02.png).
- **Don't Interfere**; type/group: Mastermind Tactic / Adrian Toomes; copies: 1 (SM p.2); printed values: not indexed in C1; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-05.png).
- **More Harm than Good**; type/group: Mastermind Tactic / Adrian Toomes; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-03.png).
- **Take Everything**; type/group: Mastermind Tactic / Adrian Toomes; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-06.png).
- **The World's Changed**; type/group: Mastermind Tactic / Adrian Toomes; copies: 1 (SM p.2); printed values: not indexed in C1; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/adrian-toomes-04.png).

### Mastermind: Vulture

- **Vulture**; type/group: Normal Mastermind face / Vulture; copies: 1 physical double-sided card (SM p.2); printed values: Attack 8+; VP 6; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-01.png).
- **Epic Vulture**; type/group: Epic Mastermind face / Vulture; copies: 1 physical double-sided card (SM p.2); printed values: Attack 10+; VP 6; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-02.png).
- **Bird of Prey**; type/group: Mastermind Tactic / Vulture; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-05.png).
- **Lurking Shadow**; type/group: Mastermind Tactic / Vulture; copies: 1 (SM p.2); printed values: not indexed in C1; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-06.png).
- **Mid Air Heist**; type/group: Mastermind Tactic / Vulture; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-03.png).
- **Winged Assault**; type/group: Mastermind Tactic / Vulture; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/vulture-04.png).

### Scheme: Distract the Hero

- **Distract the Hero**; type/group: Scheme / Distract the Hero; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-distract-the-hero.png).

### Scheme: Explosion at the Washington Monument

- **Explosion at the Washington Monument**; type/group: Scheme / Explosion at the Washington Monument; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-explosion-at-the-washington-monument.png).

### Scheme: Ferry Disaster

- **Ferry Disaster**; type/group: Scheme / Ferry Disaster; copies: 1 (SM p.2); printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-ferry-disaster.png).

### Scheme: Scavenge Alien Weaponry

- **Scavenge Alien Weaponry**; type/group: Scheme / Scavenge Alien Weaponry; copies: 1 (SM p.2); printed values: not indexed in C1; keyword labels: Striker; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/scheme-scavenge-alien-weaponry.png).

### Bystander set: Damage Control

- **Damage Control**; type/group: Bystander / Damage Control; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/scheme-damage-control.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
