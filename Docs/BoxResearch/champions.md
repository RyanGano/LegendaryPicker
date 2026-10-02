# Champions (February 2018)

**Research status: Partial.** The official insert verifies contents and several mechanics, but not the card-level setup lines or all icon-specific requirements.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| CH | [Upper Deck Champions rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/17-marvel-legendary-champions-rules.pdf) | Contents (PDF p.2), mechanics (PDF pp.1–2). |
| C1 | [master-strike structured champions card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/champions.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | February 2018 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (CH p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Gwenpool; Ms. Marvel; Nova; Totally Awesome Hulk; Viv Vision.
- **Villain Groups (two):** Monsters Unleashed; Wrecking Crew.
- **Masterminds (two):** Fin Fang Foom; Pagliacci. Each includes a normal and Epic card plus four Tactics (CH p.2).
- **Schemes (four):** Clash of the Monsters Unleashed; Divide and Conquer; Hypnotize Every Human; Steal All Oxygen on Earth.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, full Scheme setup lines, or Mastermind setup effects.

## Rules and mechanisms

- **Cheering Crowds (CH p.1):** Some cards may be played twice in a row by returning a Bystander from the player's Victory Pile to the bottom of the Bystander Stack. Each play resolves as a separate play for other card abilities.
- **Versatile (CH p.1):** Choose the printed amount as either Recruit or Attack when playing the card; do not split it. Different Versatile cards can make different choices, including two plays of a card played twice.
- **Size-Changing (CH pp.1–2):** A card can have a reduced Recruit cost or fight value when its stated condition is met. The insert gives a two-point reduction for qualifying played cards/classes; multiple qualifying classes can stack, but having multiple cards of one class does not. After recruitment, the discount has no further effect.
- **Demolish (CH p.2):** Reveal the top Hero Deck card, note its cost, and put it on the bottom. Each player then reveals their hand and discards a card of that cost; reveal only one Hero Deck card for all players.
- **Epic Masterminds (CH p.2):** Either Mastermind may use its normal or Epic side with the same four Tactics.

The text extraction loses the icons that specify Size-Changing's exact qualifying class, cost, or fight-value direction; confirm those from legible product cards before encoding individual effects.

## Required parts and glossary

- Cheering Crowds returns a Bystander to the bottom of the shared Bystander Stack; it does not identify an additional stack or token (CH p.1).
- **Cheering Crowds:** A card ability that can allow the card to be played twice after returning a Bystander. (CH p.1)
- **Versatile:** Choose a card's printed amount as Recruit or Attack when playing it. (CH p.1)
- **Size-Changing:** A conditional reduction to a card's Recruit cost or fight value. (CH pp.1–2)
- **Demolish:** A challenge comparing each player's hand with the cost of a revealed Hero Deck card. (CH p.2)

Summaries are original paraphrases under 40 words. The insert does not establish Hero teams/classes, component dependencies beyond the named Bystander Stack, or card-level setup values.

## Setup and implementation gaps

Verify all four Schemes' player limits, Twist counts, required Heroes/groups, moves, stacks, and setup steps from the product cards. Verify each Mastermind's Always Leads and setup effects, Hero teams/classes/shared Hero Names, and the exact Size-Changing icon requirements. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Gwenpool

- **Come On, Nobody Reads Card Names**; type/group: Hero / Gwenpool; copies: Unverified; Hero Name: Gwenpool; team: Champions; class icons: Covert; printed values: Cost 2; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gwen_04.png).
- **I'll Rescue You If I Feel Like It**; type/group: Hero / Gwenpool; copies: Unverified; Hero Name: Gwenpool; team: Champions; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gwen_03.png).
- **I Heard Keywords Are Powerful**; type/group: Hero / Gwenpool; copies: Unverified; Hero Name: Gwenpool; team: Champions; class icons: Instinct; printed values: Cost 6*; Attack 2; keyword labels: Size-Changing, Cheering Crowds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gwen_02.png).
- **I'm the Best at Board Games**; type/group: Hero / Gwenpool; copies: Unverified; Hero Name: Gwenpool; team: Champions; class icons: Instinct; printed values: Cost 7; Attack 5; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gwen_01.png).

### Hero group: Ms. Marvel

- **Long Arm of the Law**; type/group: Hero / Ms. Marvel; copies: Unverified; Hero Name: Ms. Marvel; team: Champions; class icons: Covert; printed values: Cost 3*; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms-marvel_04.png).
- **Big Impact**; type/group: Hero / Ms. Marvel; copies: Unverified; Hero Name: Ms. Marvel; team: Champions; class icons: Strength; printed values: Cost 4*; Recruit 0+; Attack 0+; keyword labels: Size-Changing, Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms-marvel_03.png).
- **Need to Stretch My Legs**; type/group: Hero / Ms. Marvel; copies: Unverified; Hero Name: Ms. Marvel; team: Champions; class icons: Covert; printed values: Cost 6*; Attack 2; keyword labels: Size-Changing, Cheering Crowds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms-marvel_02.png).
- **Rising Hope**; type/group: Hero / Ms. Marvel; copies: Unverified; Hero Name: Ms. Marvel; team: Champions; class icons: Strength; printed values: Cost 9*; Recruit 0+; Attack 0+; keyword labels: Size-Changing, Versatile, Cheering Crowds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ms-marvel_01.png).

### Hero group: Nova

- **Space Cop**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Champions; class icons: Strength; printed values: Cost 2; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-c-03.png).
- **Interstellar Hero**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Champions; class icons: Ranged; printed values: Cost 4; Recruit 0+; Attack 0+; keyword labels: Versatile, Cheering Crowds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-c-04.png).
- **Holographic Projection**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Champions; class icons: Ranged; printed values: Cost 5; Recruit 0+; Attack 2+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-c-02.png).
- **Growing Nova Force**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Champions; class icons: Ranged; printed values: Cost 9*; Recruit 0+; Attack 0+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-c-01.png).

### Hero group: Totally Awesome Hulk

- **Beloved Behemoth**; type/group: Hero / Totally Awesome Hulk; copies: Unverified; Hero Name: Totally Awesome Hulk; team: Champions; class icons: Strength; printed values: Cost 4*; Attack 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/awesome_hulk_04.png).
- **Incredible Mind, Awesome Body**; type/group: Hero / Totally Awesome Hulk; copies: Unverified; Hero Name: Totally Awesome Hulk; team: Champions; class icons: Tech; printed values: Cost 4*; Recruit 1; keyword labels: Size-Changing, Cheering Crowds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/awesome_hulk_03.png).
- **Growing Pains**; type/group: Hero / Totally Awesome Hulk; copies: Unverified; Hero Name: Totally Awesome Hulk; team: Champions; class icons: Strength; printed values: Cost 5*; Attack 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/awesome_hulk_02.png).
- **7th Smartest Man in the World**; type/group: Hero / Totally Awesome Hulk; copies: Unverified; Hero Name: Totally Awesome Hulk; team: Champions; class icons: Tech; printed values: Cost 9*; Attack 5+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/awesome_hulk_01.png).

### Hero group: Viv Vision

- **Walking Wi-Fi**; type/group: Hero / Viv Vision; copies: Unverified; Hero Name: Viv Vision; team: Champions; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/viv_04.png).
- **Expanding Neural Network**; type/group: Hero / Viv Vision; copies: Unverified; Hero Name: Viv Vision; team: Champions; class icons: Tech; printed values: Cost 4*; Attack 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/viv_03.png).
- **Crowdsourcing**; type/group: Hero / Viv Vision; copies: Unverified; Hero Name: Viv Vision; team: Champions; class icons: Ranged; printed values: Cost 6; Recruit 0+; Attack 0+; keyword labels: Versatile, Cheering Crowds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/viv_02.png).
- **Alter Molecular Density**; type/group: Hero / Viv Vision; copies: Unverified; Hero Name: Viv Vision; team: Champions; class icons: Tech; printed values: Cost 9*; Recruit 5; Attack 0+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/viv_01.png).

### Villain Group: Monsters Unleashed

- **Goom**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-01.png).
- **Groot from Planet X**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 6*; VP 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-02.png).
- **Monsteroso**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 5*; VP 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-05.png).
- **Orrgo**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 2*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-07.png).
- **Sporr**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 7*; VP 3; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-08.png).
- **Tim Boo Ba**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 12*; VP 5; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-04.png).
- **Trull the Unhuman**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 8*; VP 4; keyword labels: Size-Changing, Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-06.png).
- **Zzutak**; type/group: Villain / Monsters Unleashed; copies: 1; printed values: Attack 9*; VP 5; keyword labels: Size-Changing, Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monsters-unleashed-03.png).

### Villain Group: Wrecking Crew

- **Bulldozer**; type/group: Villain / Wrecking Crew; copies: 2; printed values: Attack 4; VP 2; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wc_01.png).
- **Piledriver**; type/group: Villain / Wrecking Crew; copies: 2; printed values: Attack 6; VP 4; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wc_03.png).
- **Thunderball**; type/group: Villain / Wrecking Crew; copies: 2; printed values: Attack 5; VP 3; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wc_02.png).
- **The Wrecker**; type/group: Villain / Wrecking Crew; copies: 2; printed values: Attack 7; VP 5; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wc_04.png).

### Mastermind: Fin Fang Foom

- **Fin Fang Foom**; type/group: Normal Mastermind face / Fin Fang Foom; copies: Unverified; printed values: VP 7; keyword labels: Size-Changing, Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/fin-fang-foom-01.png).
- **Epic Fin Fang Foom**; type/group: Epic Mastermind face / Fin Fang Foom; copies: Unverified; printed values: Attack 24*; VP 7; keyword labels: Size-Changing, Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/fin-fang-foom-02.png).
- **Alien Dragon Technology**; type/group: Mastermind Tactic / Fin Fang Foom; copies: Unverified; printed values: not indexed in C1; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/fin-fang-foom-04.png).
- **Flammable Acid Breath**; type/group: Mastermind Tactic / Fin Fang Foom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/fin-fang-foom-03.png).
- **Multipronged Assault**; type/group: Mastermind Tactic / Fin Fang Foom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/fin-fang-foom-05.png).
- **Supersonic Dive Attack**; type/group: Mastermind Tactic / Fin Fang Foom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/fin-fang-foom-06.png).

### Mastermind: Pagliacci

- **Pagliacci**; type/group: Normal Mastermind face / Pagliacci; copies: Unverified; printed values: VP 6; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/pagliacci-01.png).
- **Epic Pagliacci**; type/group: Epic Mastermind face / Pagliacci; copies: Unverified; printed values: Attack 11; VP 6; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/pagliacci-02.png).
- **Commedia Dell'Morte**; type/group: Mastermind Tactic / Pagliacci; copies: Unverified; printed values: not indexed in C1; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/pagliacci-06.png).
- **Creative Assassin**; type/group: Mastermind Tactic / Pagliacci; copies: Unverified; printed values: not indexed in C1; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/pagliacci-05.png).
- **Insane Clown Has a Posse**; type/group: Mastermind Tactic / Pagliacci; copies: Unverified; printed values: not indexed in C1; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/pagliacci-04.png).
- **Jester of a Twisted Opera**; type/group: Mastermind Tactic / Pagliacci; copies: Unverified; printed values: not indexed in C1; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/pagliacci-03.png).

### Scheme: Clash of the Monsters Unleashed

- **Clash of the Monsters Unleashed**; type/group: Scheme / Clash of the Monsters Unleashed; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/86Scheme(3).png).

### Scheme: Divide and Conquer

- **Divide and Conquer**; type/group: Scheme / Divide and Conquer; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/85Scheme(2).png).

### Scheme: Hypnotize Every Human

- **Hypnotize Every Human**; type/group: Scheme / Hypnotize Every Human; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/84Scheme(1).png).

### Scheme: Steal All Oxygen on Earth

- **Steal All Oxygen on Earth**; type/group: Scheme / Steal All Oxygen on Earth; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/87Scheme(4).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
