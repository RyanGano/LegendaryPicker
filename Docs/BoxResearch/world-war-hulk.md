# World War Hulk (June 2018)

**Research status: Partial.** The official insert verifies the 400-card breakdown and several rules, but not individual card setup text or the complete component manifest.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| WWH | [Upper Deck World War Hulk rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Legendary_Rules-World_War_Hulk.pdf) | Contents (PDF p.2), mechanics and card types (PDF pp.1–2). |
| C1 | [master-strike structured world-war-hulk card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/wwhulk.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | June 2018 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (WWH p.2)

| Type | Official count |
|---|---:|
| Heroes | 15 groups × 14 cards = 210 |
| Transformed Hero cards | 62 |
| Villain Groups | 7 groups × 8 cards = 56, including Traps |
| Henchman Groups | 3 groups × 10 cards = 30 |
| Transforming Masterminds | 6 sets × 5 cards = 30 |
| Schemes | 8 |
| Special Bystander types | 4 × 1 card = 4 |
| Total cards | 400 |

The listed categories sum to the official 400-card total.

### Group inventory (C1)

- **Heroes (15):** Amadeus Cho; Bruce Banner; Caiera; Gladiator Hulk; Hiroim; Hulkbuster Iron Man; Joe Fixit, Grey Hulk; Korg; Miek, The Unhived; Namora; No-Name, Brood Queen; Rick Jones; Sentry; She-Hulk; Skaar, Son of Hulk.
- **Villain Groups (seven):** Aspects of the Void; Code Red; Illuminati; Intelligencia; Sakaar Imperial Guard; U-Foes; Warbound.
- **Henchman Groups (three):** Cytoplasm Spikes; Death's Heads; Sakaaran Hivelings.
- **Masterminds (six):** General “Thunderbolt” Ross; Illuminati, Secret Society; King Hulk, Sakaarson; M.O.D.O.K.; Red King, The; Sentry, The.
- **Schemes (eight):** Break the Planet Asunder; Cytoplasm Spike Invasion; Fall of the Hulks; Gladiator Pits of Sakaar; Mutating Gamma Rays; Shoot Hulk into Space; Subjugate with Obedience Disks; World War Hulk.
- **Special Bystanders (four names from C1):** Actor; Animal Trainer; Tourist Couple; Triage Nurse.

The official insert confirms four Special Bystander types but does not name them. The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Transformation Pile associations, setup values, and fields absent from C1 remain unresolved.

## Rules and mechanisms

- **Transforming Heroes (WWH p.1):** Set aside each used Hero's Transformed cards in a Transformation Pile rather than shuffling them into the Hero Deck. When instructed, finish the original card, remove it from the game to that pile, and put the new card into hand; it can be played immediately. The original still counts as played for turn-based effects. Only the transformed version can be revealed. Use only Transformed cards for Heroes in the game.
- **Transforming Masterminds (WWH p.1):** Start with the side showing Always Leads face up. A Master Strike or Tactic can flip the Mastermind; resolve the effect that instructed the flip, not the newly face-up side's Master Strike. Only the face-up side's abilities and values apply.
- **Outwit (WWH p.1):** Use an Outwit ability only if revealing Heroes with three different costs; the Outwit card itself can count. A player may decline to use it.
- **Smash (WWH p.2):** Discard another card from hand to gain the printed Attack amount.
- **Wounded Fury (WWH p.2):** Each Wound in the discard pile adds +1 to the relevant Hero bonus or enemy value.
- **Cross-Dimensional Hulk Rampage (WWH p.2):** Each player reveals a Hulk Hero or a Hulk card in their Victory Pile, or gains a Wound. The insert's identifying rule is the word “Hulk” in the relevant card/group/Tactic name, with stated exceptions.
- **Feast (WWH p.2):** KO the top card of your deck; “Feast on each player” applies this to every player's deck.
- **Traps (WWH pp.1–2):** Each Villain Group includes a Trap. A Trap drawn from the Villain Deck sets a challenge for that turn; success puts it in the player's Victory Pile for its points, and failure applies its penalty at turn end after drawing a new hand. Traps do not move city Villains.
- **Grey Heroes (WWH p.2):** This means grey-colored cards without a Hero Class, such as Agents, Troopers, or Officers; Grey Hulk and Jean Grey do not qualify.

## Required parts and glossary

- **Transformation Pile:** A separate pile for removed Transforming Heroes and their unused transformed forms. It is not a recruitable Hero Deck (WWH p.1).
- **Traps:** Trap cards are included within each Villain Group's eight-card count; they are not a separate city-moving group (WWH pp.1–2).
- **Special Bystanders:** The official insert lists four types with one card each; identities require card-level verification (WWH p.2).
- **Transform:** Replace a played Hero with its corresponding Transformed card from the Transformation Pile, which may be played immediately. (WWH p.1)
- **Outwit:** A conditional ability available when revealing Heroes with three different costs. (WWH p.1)
- **Smash:** Discard another hand card to gain the printed Attack amount. (WWH p.2)
- **Wounded Fury:** Gain strength from Wounds in the discard pile. (WWH p.2)
- **Cross-Dimensional Hulk Rampage:** Each player proves access to a Hulk card or gains a Wound. (WWH p.2)
- **Feast:** KO the top card of a deck. (WWH p.2)
- **Trap:** A Villain-Deck card with a turn-long challenge and a success reward or failure penalty. (WWH pp.1–2)

Summaries are original paraphrases under 40 words. The insert does not establish Hero teams/classes, exact card-linked component dependencies, Always Leads groups, or card-level Scheme and Mastermind setup values.

## Setup and implementation gaps

Verify every Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps, and every Mastermind's Always Leads mapping and setup effect from the product cards. Verify the Special Bystander card identities, Hero metadata, and exact Transformation Pile requirements/card associations. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Amadeus Cho

- **Extrapolate**; type/group: Hero / Amadeus Cho; copies: Unverified; Hero Name: Amadeus Cho; team: Champions; class icons: Instinct; printed values: Cost 2; Recruit 1; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/amadeus-cho-03.png).
- **Gamma-Draining Nanites**; type/group: Hero / Amadeus Cho; copies: Unverified; Hero Name: Amadeus Cho; team: Champions; class icons: Tech; printed values: Cost 3; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/amadeus-cho-04.png).
- **Like Totally Smart Hulk**; type/group: Hero (transformed face) / Amadeus Cho; copies: Unverified; Hero Name: Amadeus Cho; team: Champions; class icons: Strength; printed values: Cost 5; Attack 2+; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/amadeus-cho-01a.png).
- **Renegade Genius**; type/group: Hero / Amadeus Cho; copies: Unverified; Hero Name: Amadeus Cho; team: Champions; class icons: Tech; printed values: Cost 6; Attack 0+; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/amadeus-cho-02.png).
- **Visualize the Variables**; type/group: Hero / Amadeus Cho; copies: Unverified; Hero Name: Amadeus Cho; team: Champions; class icons: Tech; printed values: Cost 8; Attack 4; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/amadeus-cho-01.png).

### Hero group: Bruce Banner

- **Solve the Impossible**; type/group: Hero / Bruce Banner; copies: Unverified; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 2; Attack 1; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bruce-banner-03.png).
- **Gamma Bomb Disaster**; type/group: Hero / Bruce Banner; copies: Unverified; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 4; Recruit 2; keyword labels: Outwit, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bruce-banner-04.png).
- **Savage Hulk Unleashed**; type/group: Hero (transformed face) / Bruce Banner; copies: Unverified; Hero Name: Bruce Banner; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bruce-banner-01a.png).
- **Dangerous Testing**; type/group: Hero / Bruce Banner; copies: Unverified; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bruce-banner-02.png).
- **Gamma Ray Experiment**; type/group: Hero / Bruce Banner; copies: Unverified; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 4; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bruce-banner-01.png).

### Hero group: Caiera

- **Shadow Queen**; type/group: Hero / Caiera; copies: Unverified; Hero Name: Caiera; team: Warbound; class icons: Covert; printed values: Cost 2; Attack 1; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/caiera-03.png).
- **Shadowforged Blade**; type/group: Hero / Caiera; copies: Unverified; Hero Name: Caiera; team: Warbound; class icons: Covert; printed values: Cost 4; Attack 2+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/caiera-04.png).
- **Focus The Old Power**; type/group: Hero / Caiera; copies: Unverified; Hero Name: Caiera; team: Warbound; class icons: Strength; printed values: Cost 6; Attack 2; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/caiera-02.png).
- **Dutiful Protector**; type/group: Hero / Caiera; copies: Unverified; Hero Name: Caiera; team: Warbound; class icons: Instinct; printed values: Cost 7; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/caiera-01.png).
- **Vengeful Destructor**; type/group: Hero (transformed face) / Caiera; copies: Unverified; Hero Name: Caiera; team: Warbound; class icons: Strength; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/caiera-01a.png).

### Hero group: Gladiator Hulk

- **Don't Make Me Angry**; type/group: Hero / Gladiator Hulk; copies: Unverified; Hero Name: Gladiator Hulk; team: Warbound; class icons: Strength; printed values: Cost 3; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gladiator-hulk-03.png).
- **Seize The Throne**; type/group: Hero / Gladiator Hulk; copies: Unverified; Hero Name: Gladiator Hulk; team: Warbound; class icons: Instinct; printed values: Cost 4; Attack 0+; keyword labels: Smash, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gladiator-hulk-04.png).
- **Hulk Is King**; type/group: Hero (transformed face) / Gladiator Hulk; copies: Unverified; Hero Name: Gladiator Hulk; team: Warbound; class icons: Strength; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gladiator-hulk-01a.png).
- **The Green Scar**; type/group: Hero / Gladiator Hulk; copies: Unverified; Hero Name: Gladiator Hulk; team: Warbound; class icons: Strength; printed values: Cost 5; Attack 3+; keyword labels: Cross-Dimensional Rampage, Wounded Fury; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gladiator-hulk-02.png).
- **Double-Fisted Smashing**; type/group: Hero / Gladiator Hulk; copies: Unverified; Hero Name: Gladiator Hulk; team: Warbound; class icons: Strength; printed values: Cost 8; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gladiator-hulk-01.png).

### Hero group: Hiroim

- **Seek Redemption**; type/group: Hero / Hiroim; copies: Unverified; Hero Name: Hiroim; team: Warbound; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hiroim-04.png).
- **Save from the Rubble**; type/group: Hero / Hiroim; copies: Unverified; Hero Name: Hiroim; team: Warbound; class icons: Covert; printed values: Cost 4; Recruit 1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hiroim-03.png).
- **Hiroim Redeemed**; type/group: Hero (transformed face) / Hiroim; copies: Unverified; Hero Name: Hiroim; team: Warbound; class icons: Strength; printed values: Cost 5; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hiroim-01a.png).
- **Mystic Shadow Priest**; type/group: Hero / Hiroim; copies: Unverified; Hero Name: Hiroim; team: Warbound; class icons: Covert; printed values: Cost 6; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hiroim-02.png).
- **Blade of the People**; type/group: Hero / Hiroim; copies: Unverified; Hero Name: Hiroim; team: Warbound; class icons: Instinct; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hiroim-01.png).

### Hero group: Hulkbuster Iron Man

- **Pound for Pound**; type/group: Hero / Hulkbuster Iron Man; copies: Unverified; Hero Name: Hulkbuster Iron Man; team: Avengers; class icons: Strength; printed values: Cost 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkbuster-iron-man-03.png).
- **Attune Techtonic Transducer**; type/group: Hero / Hulkbuster Iron Man; copies: Unverified; Hero Name: Hulkbuster Iron Man; team: Avengers; class icons: Tech; printed values: Cost 4; Attack 2+; keyword labels: Outwit, Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkbuster-iron-man-04.png).
- **Build the Suit**; type/group: Hero / Hulkbuster Iron Man; copies: Unverified; Hero Name: Hulkbuster Iron Man; team: Avengers; class icons: Tech; printed values: Cost 5; Recruit 3; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkbuster-iron-man-02.png).
- **Ultra-Massive Armor**; type/group: Hero (transformed face) / Hulkbuster Iron Man; copies: Unverified; Hero Name: Hulkbuster Iron Man; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkbuster-iron-man-01a.png).
- **Final Battle**; type/group: Hero / Hulkbuster Iron Man; copies: Unverified; Hero Name: Hulkbuster Iron Man; team: Avengers; class icons: Tech; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkbuster-iron-man-01.png).

### Hero group: Joe Fixit, Grey Hulk

- **Carefully Considered Smashing**; type/group: Hero / Joe Fixit, Grey Hulk; copies: Unverified; Hero Name: Joe Fixit, Grey Hulk; team: Crime Syndicate; class icons: Strength; printed values: Cost 3; Recruit 2; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/grey-hulk-03.png).
- **Threaten And Bribe**; type/group: Hero / Joe Fixit, Grey Hulk; copies: Unverified; Hero Name: Joe Fixit, Grey Hulk; team: Crime Syndicate; class icons: Covert; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/grey-hulk-04.png).
- **Ambitious Enforcer**; type/group: Hero / Joe Fixit, Grey Hulk; copies: Unverified; Hero Name: Joe Fixit, Grey Hulk; team: Crime Syndicate; class icons: Strength; printed values: Cost 6; Attack 3; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/grey-hulk-02.png).
- **Underworld Boss**; type/group: Hero (transformed face) / Joe Fixit, Grey Hulk; copies: Unverified; Hero Name: Joe Fixit, Grey Hulk; team: Crime Syndicate; class icons: Instinct; printed values: Cost 6; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/grey-hulk-01a.png).
- **Hulk Runs This Town**; type/group: Hero / Joe Fixit, Grey Hulk; copies: Unverified; Hero Name: Joe Fixit, Grey Hulk; team: Crime Syndicate; class icons: Covert; printed values: Cost 7; Recruit 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/grey-hulk-01.png).

### Hero group: Korg

- **Nothing Beats Rock**; type/group: Hero / Korg; copies: Unverified; Hero Name: Korg; team: Warbound; class icons: Strength; printed values: Cost 2; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/korg-03.png).
- **Move Mountains**; type/group: Hero / Korg; copies: Unverified; Hero Name: Korg; team: Warbound; class icons: Strength; printed values: Cost 4; Attack 2; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/korg-04.png).
- **Forged by Fire**; type/group: Hero / Korg; copies: Unverified; Hero Name: Korg; team: Warbound; class icons: Strength; printed values: Cost 3; Recruit 2; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/korg-02.png).
- **Lord of Granite**; type/group: Hero (transformed face) / Korg; copies: Unverified; Hero Name: Korg; team: Warbound; class icons: Covert; printed values: Cost 5; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/korg-01a.png).
- **Kronan Tactician**; type/group: Hero / Korg; copies: Unverified; Hero Name: Korg; team: Warbound; class icons: Strength; printed values: Cost 8; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/korg-01.png).

### Hero group: Miek, The Unhived

- **This Bug Smashes You**; type/group: Hero / Miek, The Unhived; copies: Unverified; Hero Name: Miek, The Unhived; team: Warbound; class icons: Instinct; printed values: Cost 3; Attack 2+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/miek-the-unhived-03.png).
- **Devouring Frenzy**; type/group: Hero / Miek, The Unhived; copies: Unverified; Hero Name: Miek, The Unhived; team: Warbound; class icons: Instinct; printed values: Cost 4; Recruit 2; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/miek-the-unhived-04.png).
- **Endless Appetite**; type/group: Hero / Miek, The Unhived; copies: Unverified; Hero Name: Miek, The Unhived; team: Warbound; class icons: Instinct; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/miek-the-unhived-02.png).
- **Metamorphosis**; type/group: Hero / Miek, The Unhived; copies: Unverified; Hero Name: Miek, The Unhived; team: Warbound; class icons: Covert; printed values: Cost 7; Recruit 5; keyword labels: Feast, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/miek-the-unhived-01.png).
- **Hive King Miek**; type/group: Hero (transformed face) / Miek, The Unhived; copies: Unverified; Hero Name: Miek, The Unhived; team: Warbound; class icons: Strength; printed values: Cost 8; Attack 6; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/miek-the-unhived-01a.png).

### Hero group: Namora

- **Crushing Tsunami**; type/group: Hero / Namora; copies: Unverified; Hero Name: Namora; team: Champions; class icons: Ranged; printed values: Cost 3; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namora-04.png).
- **Heart of the Ocean**; type/group: Hero / Namora; copies: Unverified; Hero Name: Namora; team: Champions; class icons: Covert; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namora-03.png).
- **Herculean Effort**; type/group: Hero / Namora; copies: Unverified; Hero Name: Namora; team: Champions; class icons: Ranged; printed values: Cost 5; Recruit 3; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namora-02.png).
- **Master of Depths**; type/group: Hero (transformed face) / Namora; copies: Unverified; Hero Name: Namora; team: Champions; class icons: Strength; printed values: Cost 6; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namora-01a.png).
- **Turning The Tide**; type/group: Hero / Namora; copies: Unverified; Hero Name: Namora; team: Champions; class icons: Covert; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namora-01.png).

### Hero group: No-Name, Brood Queen

- **Surprise Attack**; type/group: Hero / No-Name, Brood Queen; copies: Unverified; Hero Name: No-Name, Brood Queen; team: Warbound; class icons: Covert; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brood-queen-04.png).
- **Appetite for Destruction**; type/group: Hero / No-Name, Brood Queen; copies: Unverified; Hero Name: No-Name, Brood Queen; team: Warbound; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brood-queen-03.png).
- **Bursting with Life**; type/group: Hero / No-Name, Brood Queen; copies: Unverified; Hero Name: No-Name, Brood Queen; team: Warbound; class icons: Strength; printed values: Cost 3; Recruit 2; keyword labels: Feast, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brood-queen-02.png).
- **Torrent of Broodlings**; type/group: Hero (transformed face) / No-Name, Brood Queen; copies: Unverified; Hero Name: No-Name, Brood Queen; team: Warbound; class icons: Covert; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brood-queen-01a.png).
- **World-Spanning Hunger**; type/group: Hero / No-Name, Brood Queen; copies: Unverified; Hero Name: No-Name, Brood Queen; team: Warbound; class icons: Instinct; printed values: Cost 8; Attack 4+; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/brood-queen-01.png).

### Hero group: Rick Jones

- **Hacktivist**; type/group: Hero / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-03.png).
- **Seek the Nega-Bands**; type/group: Hero / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 4; Recruit 2; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-04.png).
- **Captain Marvel**; type/group: Hero (transformed face) / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: Avengers; class icons: Ranged; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-01a.png).
- **Irradiated Blood**; type/group: Hero / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 5; Attack 3; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-02.png).
- **A-Bomb**; type/group: Hero (transformed face) / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: Unaffiliated; class icons: Strength; printed values: Cost 6; Attack 0+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-01b.png).
- **Caught in Kree-Skrull War**; type/group: Hero / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 7; Attack 4; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-01.png).
- **The Destiny Force**; type/group: Hero (transformed face) / Rick Jones; copies: Unverified; Hero Name: Rick Jones; team: Avengers; class icons: Ranged; printed values: Cost 9; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rick-jones-01c.png).

### Hero group: Sentry

- **Agoraphobia**; type/group: Hero / Sentry; copies: Unverified; Hero Name: Sentry; team: Avengers; class icons: Covert; printed values: Cost 2; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sentry-03-1.png).
- **Golden Guardian of Good**; type/group: Hero (transformed face) / Sentry; copies: Unverified; Hero Name: Sentry; team: Avengers; class icons: Strength; printed values: Cost 6; Attack 0+; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sentry-01a-1.png).
- **Rival Personalities**; type/group: Hero / Sentry; copies: Unverified; Hero Name: Sentry; team: Avengers; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sentry-04-1.png).
- **Mournful Sentinel**; type/group: Hero / Sentry; copies: Unverified; Hero Name: Sentry; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 2; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sentry-02-1.png).
- **The Void Unchained**; type/group: Hero (transformed face) / Sentry; copies: Unverified; Hero Name: Sentry; team: Unaffiliated; class icons: Covert; printed values: Cost 5; Attack 3; keyword labels: Feast, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sentry-01b.png).
- **Vast Unstable Power**; type/group: Hero / Sentry; copies: Unverified; Hero Name: Sentry; team: Avengers; class icons: Ranged; printed values: Cost 8; Attack 0+; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sentry-01-1.png).

### Hero group: She-Hulk

- **Hurl Legal Objections**; type/group: Hero / She-Hulk; copies: Unverified; Hero Name: She-Hulk; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/she-hulk-04.png).
- **Hurl Trucks**; type/group: Hero (transformed face) / She-Hulk; copies: Unverified; Hero Name: She-Hulk; team: Avengers; class icons: Strength; printed values: Cost 6; Attack 2+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/she-hulk-01a.png).
- **Window of Opportunity**; type/group: Hero / She-Hulk; copies: Unverified; Hero Name: She-Hulk; team: Avengers; class icons: Strength; printed values: Cost 4; Recruit 2; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/she-hulk-03.png).
- **Radioactive Riot**; type/group: Hero / She-Hulk; copies: Unverified; Hero Name: She-Hulk; team: Avengers; class icons: Strength; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/she-hulk-02.png).
- **Jade Giantess**; type/group: Hero / She-Hulk; copies: Unverified; Hero Name: She-Hulk; team: Avengers; class icons: Strength; printed values: Cost 8; Recruit 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/she-hulk-01.png).

### Hero group: Skaar, Son of Hulk

- **Anger Management**; type/group: Hero / Skaar, Son of Hulk; copies: Unverified; Hero Name: Skaar, Son of Hulk; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 1+; keyword labels: Smash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skaar-03.png).
- **Scarred Past**; type/group: Hero / Skaar, Son of Hulk; copies: Unverified; Hero Name: Skaar, Son of Hulk; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 2+; keyword labels: Wounded Fury; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skaar-04.png).
- **Mood Swings**; type/group: Hero / Skaar, Son of Hulk; copies: Unverified; Hero Name: Skaar, Son of Hulk; team: Avengers; class icons: Instinct; printed values: Cost 5; Recruit 3; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skaar-02.png).
- **Raging Savage**; type/group: Hero (transformed face) / Skaar, Son of Hulk; copies: Unverified; Hero Name: Skaar, Son of Hulk; team: Avengers; class icons: Strength; printed values: Cost 6; Attack 3+; keyword labels: Wounded Fury; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skaar-01a.png).
- **Planetary-Level Revenge**; type/group: Hero / Skaar, Son of Hulk; copies: Unverified; Hero Name: Skaar, Son of Hulk; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 4+; keyword labels: Wounded Fury; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skaar-01.png).

### Villain Group: Aspects of the Void

- **Black Anti-Hurricane**; type/group: Villain / Aspects of the Void; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aspects-of-the-void-04.png).
- **Demonform**; type/group: Villain / Aspects of the Void; copies: 1; printed values: Attack 7; VP 5; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aspects-of-the-void-05.png).
- **Infini-Tendrils**; type/group: Villain / Aspects of the Void; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Wounded Fury; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aspects-of-the-void-03.png).
- **Psychotic Break**; type/group: Villain; subtype Trap / Aspects of the Void; copies: 1; printed values: VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aspects-of-the-void-01.png).
- **Shadow Man**; type/group: Villain / Aspects of the Void; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/aspects-of-the-void-02.png).

### Villain Group: Code Red

- **Caught Red-Handed**; type/group: Villain; subtype Trap / Code Red; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/code-red-06.png).
- **Crimson Dynamo**; type/group: Villain / Code Red; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/code-red-04.png).
- **Elektra, Red Blades**; type/group: Villain / Code Red; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/code-red-05.png).
- **Punisher, Red Dot Sniper**; type/group: Villain / Code Red; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/code-red-02.png).
- **Red She-Hulk**; type/group: Villain / Code Red; copies: 1; printed values: Attack 6+; VP 5; keyword labels: Wounded Fury; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/code-red-03.png).
- **Thundra**; type/group: Villain / Code Red; copies: 1; printed values: Attack 4+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/code-red-01.png).

### Villain Group: Illuminati

- **Black Bolt**; type/group: Villain / Illuminati; copies: 2; printed values: Attack 13*; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/illuminati-04.png).
- **Dr. Strange**; type/group: Villain / Illuminati; copies: 2; printed values: Attack 5; VP 3; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/illuminati-01.png).
- **Dr. Strange, Possessed by Zom**; type/group: Villain / Illuminati; copies: 1; printed values: Attack 5+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/illuminati-02.png).
- **Enchain the Hulk**; type/group: Villain; subtype Trap / Illuminati; copies: 1; printed values: VP 4; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/illuminati-03.png).
- **Hulkbuster Iron Man**; type/group: Villain / Illuminati; copies: 2; printed values: Attack 6+; VP 4; keyword labels: Outwit, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/illuminati-05.png).

### Villain Group: Intelligencia

- **Battle of Wits**; type/group: Villain; subtype Trap / Intelligencia; copies: 2; printed values: VP 3; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/intelligencia-02.png).
- **Cosmic Hulk Robot**; type/group: Villain / Intelligencia; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Wounded Fury, Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/intelligencia-01.png).
- **Doc Samson**; type/group: Villain / Intelligencia; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/intelligencia-04.png).
- **The Leader, Gamma Fiend**; type/group: Villain / Intelligencia; copies: 2; printed values: Attack 5; VP 3; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/intelligencia-03.png).

### Villain Group: Sakaar Imperial Guard

- **Gladiators' Colosseum**; type/group: Villain; subtype Trap / Sakaar Imperial Guard; copies: 1; printed values: VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sakaar-imperial-guard-05.png).
- **Great Devil Corker**; type/group: Villain / Sakaar Imperial Guard; copies: 2; printed values: Attack 6; VP 4; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sakaar-imperial-guard-03.png).
- **Headman Charr**; type/group: Villain / Sakaar Imperial Guard; copies: 2; printed values: Attack 2+; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sakaar-imperial-guard-01.png).
- **Lieutenant Caiera**; type/group: Villain / Sakaar Imperial Guard; copies: 1; printed values: Attack 7; VP 5; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sakaar-imperial-guard-04.png).
- **Primus Vand**; type/group: Villain / Sakaar Imperial Guard; copies: 2; printed values: Attack 3+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sakaar-imperial-guard-02.png).

### Villain Group: U-Foes

- **Ironclad**; type/group: Villain / U-Foes; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/u-foes-03.png).
- **Vapor**; type/group: Villain / U-Foes; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/u-foes-05.png).
- **Vector**; type/group: Villain / U-Foes; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/u-foes-02.png).
- **Unidentified Flying U-Foes**; type/group: Villain; subtype Trap / U-Foes; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/u-foes-04.png).
- **X-Ray**; type/group: Villain / U-Foes; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/u-foes-01.png).

### Villain Group: Warbound

- **Elloe Kaifi**; type/group: Villain / Warbound; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/warbound-03.png).
- **Hiroim**; type/group: Villain / Warbound; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/warbound-05.png).
- **Korg**; type/group: Villain / Warbound; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/warbound-04.png).
- **Miek The Unhived**; type/group: Villain / Warbound; copies: 2; printed values: Attack 5; VP 3; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/warbound-02.png).
- **No-Name, Brood Queen**; type/group: Villain / Warbound; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Wounded Fury, Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/warbound-01.png).
- **Warbound Rescue**; type/group: Villain; subtype Trap / Warbound; copies: 1; printed values: VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/warbound-06.png).

### Henchman Group: Cytoplasm Spikes

- **Cytoplasm Spikes**; type/group: Henchman / Cytoplasm Spikes; copies: Unverified; printed values: not indexed in C1; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/cytoplasm-spikes.png).

### Henchman Group: Death's Heads

- **Death's Heads**; type/group: Henchman / Death's Heads; copies: Unverified; printed values: not indexed in C1; keyword labels: Outwit; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/deaths-heads.png).

### Henchman Group: Sakaaran Hivelings

- **Sakaaran Hivelings**; type/group: Henchman / Sakaaran Hivelings; copies: Unverified; printed values: not indexed in C1; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/sakaaran-hivelings.png).

### Mastermind: General “Thunderbolt” Ross

- **General “Thunderbolt” Ross**; type/group: Normal Mastermind face / General “Thunderbolt” Ross; copies: Unverified; printed values: VP 6; keyword labels: Transform, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/general-ross-01.png).
- **Red Hulk**; type/group: Normal Mastermind face (transformed face) / General “Thunderbolt” Ross; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Wounded Fury, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/red-hulk-01a.png).
- **Bust You Down to Private**; type/group: Mastermind Tactic / General “Thunderbolt” Ross; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/general-ross-03.png).
- **Call Out the Army**; type/group: Mastermind Tactic / General “Thunderbolt” Ross; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/general-ross-04.png).
- **Personal Arsenal**; type/group: Mastermind Tactic / General “Thunderbolt” Ross; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/general-ross-02.png).
- **Urban Warfare**; type/group: Mastermind Tactic / General “Thunderbolt” Ross; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/general-ross-05.png).

### Mastermind: Illuminati, Secret Society

- **Illuminati, Secret Society**; type/group: Normal Mastermind face / Illuminati, Secret Society; copies: Unverified; printed values: Attack 11+; VP 7; keyword labels: Outwit, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/illuminati-01-1.png).
- **Illuminati, Open Warfare**; type/group: Normal Mastermind face (transformed face) / Illuminati, Secret Society; copies: Unverified; printed values: Attack 13; VP 7; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/illuminati-01a.png).
- **Black Bolt's Omni-Shout**; type/group: Mastermind Tactic / Illuminati, Secret Society; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/illuminati-02-1.png).
- **Dr. Strange's Orb of Agamotto**; type/group: Mastermind Tactic / Illuminati, Secret Society; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/illuminati-03-1.png).
- **Hulkbuster's Hammer Fist**; type/group: Mastermind Tactic / Illuminati, Secret Society; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/illuminati-04-1.png).
- **Zom's Manacles of Living Bondage**; type/group: Mastermind Tactic / Illuminati, Secret Society; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/illuminati-05-1.png).

### Mastermind: King Hulk, Sakaarson

- **King Hulk, Sakaarson**; type/group: Normal Mastermind face / King Hulk, Sakaarson; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hulk-01.png).
- **King Hulk, Worldbreaker**; type/group: Normal Mastermind face (transformed face) / King Hulk, Sakaarson; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Wounded Fury, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hulk-01a.png).
- **Fury of the Green Scar**; type/group: Mastermind Tactic / King Hulk, Sakaarson; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hulk-02.png).
- **Oath of the Warbound**; type/group: Mastermind Tactic / King Hulk, Sakaarson; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hulk-03.png).
- **Revenge from the Stars**; type/group: Mastermind Tactic / King Hulk, Sakaarson; copies: Unverified; printed values: not indexed in C1; keyword labels: Cross-Dimensional Rampage, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hulk-04.png).
- **Rule By the Strongest**; type/group: Mastermind Tactic / King Hulk, Sakaarson; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hulk-05.png).

### Mastermind: M.O.D.O.K.

- **M.O.D.O.K.**; type/group: Normal Mastermind face / M.O.D.O.K.; copies: Unverified; printed values: VP 6; keyword labels: Outwit, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/modok-01.png).
- **M.O.D.O.K., Network Nightmare**; type/group: Normal Mastermind face (transformed face) / M.O.D.O.K.; copies: Unverified; printed values: Attack 8*; VP 6; keyword labels: Outwit, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/modok-01a.png).
- **Brain Scramble**; type/group: Mastermind Tactic / M.O.D.O.K.; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/modok-02.png).
- **Designed Only For...K.O.ING**; type/group: Mastermind Tactic / M.O.D.O.K.; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/modok-04.png).
- **Don't Get a Big head About It**; type/group: Mastermind Tactic / M.O.D.O.K.; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/modok-03.png).
- **Redundancy Algorithim**; type/group: Mastermind Tactic / M.O.D.O.K.; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/modok-05.png).

### Mastermind: Red King, The

- **The Red King**; type/group: Normal Mastermind face / Red King, The; copies: Unverified; printed values: VP 6; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-red-king-01.png).
- **The Red King, Power Armored**; type/group: Normal Mastermind face (transformed face) / Red King, The; copies: Unverified; printed values: Attack 10; VP 6; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-red-king-01a.png).
- **Haughty Spite**; type/group: Mastermind Tactic / Red King, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-red-king-03.png).
- **Royal Bodyguard**; type/group: Mastermind Tactic / Red King, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-red-king-02.png).
- **Treasury of Sakaar**; type/group: Mastermind Tactic / Red King, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-red-king-04.png).
- **Vast Armies of Sakaar**; type/group: Mastermind Tactic / Red King, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/the-red-king-05.png).

### Mastermind: Sentry, The

- **The Sentry**; type/group: Normal Mastermind face / Sentry, The; copies: Unverified; printed values: VP 6; keyword labels: Transform, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/sentry-01.png).
- **The Void**; type/group: Normal Mastermind face (transformed face) / Sentry, The; copies: Unverified; printed values: Attack 11+; VP 6; keyword labels: Wounded Fury, Feast, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/sentry-01a.png).
- **Pacifying Light**; type/group: Mastermind Tactic / Sentry, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/sentry-04.png).
- **Power of a Million Exploding Suns**; type/group: Mastermind Tactic / Sentry, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/sentry-03.png).
- **Reflexive Teleportation**; type/group: Mastermind Tactic / Sentry, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/sentry-02.png).
- **Repressed Darkness**; type/group: Mastermind Tactic / Sentry, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/sentry-05.png).

### Scheme: Break the Planet Asunder

- **Break the Planet Asunder**; type/group: Scheme / Break the Planet Asunder; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/91Scheme(100).png).

### Scheme: Cytoplasm Spike Invasion

- **Cytoplasm Spike Invasion**; type/group: Scheme / Cytoplasm Spike Invasion; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/90Scheme(99).png).

### Scheme: Fall of the Hulks

- **Fall of the Hulks**; type/group: Scheme / Fall of the Hulks; copies: Unverified; printed values: not indexed in C1; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/89Scheme(18).png).

### Scheme: Gladiator Pits of Sakaar

- **Gladiator Pits of Sakaar**; type/group: Scheme / Gladiator Pits of Sakaar; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/88Scheme(17).png).

### Scheme: Mutating Gamma Rays

- **Mutating Gamma Rays**; type/group: Scheme / Mutating Gamma Rays; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/92Scheme(101).png).

### Scheme: Shoot Hulk into Space

- **Shoot Hulk into Space**; type/group: Scheme / Shoot Hulk into Space; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/93Scheme(102).png).

### Scheme: Subjugate with Obedience Disks

- **Subjugate with Obedience Disks**; type/group: Scheme / Subjugate with Obedience Disks; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/94Scheme(103).png).

### Scheme: World War Hulk

- **World War Hulk**; type/group: Scheme / World War Hulk; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/95Scheme(104).png).

### Bystander set: Actor

- **Actor**; type/group: Bystander / Actor; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/actor.png).

### Bystander set: Animal Trainer

- **Animal Trainer**; type/group: Bystander / Animal Trainer; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/animal-trainer.png).

### Bystander set: Tourist Couple

- **Tourist Couple**; type/group: Bystander / Tourist Couple; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/tourist-couple.png).

### Bystander set: Triage Nurse

- **Triage Nurse**; type/group: Bystander / Triage Nurse; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/triage-nurse.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
