# Civil War (August 2016)

**Research status: Partial; integrated (#125).** The official insert verifies 370 cards, several new mechanisms, and shared-stack additions. The Scheme Setup lines, Always Leads and part uses were read from the C1-linked card faces and the C2 card index and are in `LegendaryPickerService/Data/Boxes/civil-war.json`; per-face copy counts remain as indexed below.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| CW | [Upper Deck Civil War rules insert](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Civil_War_Rules.pdf) | Contents (PDF p.2), Divided Cards and keywords (PDF pp.1–2), Sidekick and Wound stack rules (PDF p.2). |
| C1 | [master-strike structured civil-war card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/civilwar.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | August 2016 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (CW p.2)

| Type | Official count |
|---|---:|
| Heroes | 16 groups × 14 cards = 224 |
| Villain Groups | 7 groups × 8 cards = 56 |
| Henchman Groups | 2 groups × 10 cards = 20 |
| Masterminds | 5 sets × 5 cards = 25 |
| Schemes | 8 |
| Special Sidekicks | 7 types, 15 cards total |
| Grievous Wounds | 7 types, 15 cards total |
| Special Bystanders | 2 types, 7 cards total |
| Total cards | 370 |

The detailed categories sum to the official 370-card total.

### Group inventory (C1)

- **Heroes (16):** Captain America, Secret Avenger; Cloak & Dagger; Daredevil; Falcon; Goliath; Hercules; Hulkling; Luke Cage; Patriot; Peter Parker; Speedball; Stature; Storm & Black Panther; Tigra; Vision; Wiccan.
- **Villain Groups (seven):** CSA Special Marshals; Great Lakes Avengers; Heroes for Hire; Registration Enforcers; S.H.I.E.L.D. Elite; Superhuman Registration Act; Thunderbolts.
- **Henchman Groups (two):** Mandroid; Cape-Killers.
- **Masterminds (five):** Authoritarian Iron Man; Baron Helmut Zemo; Maria Hill, Director of S.H.I.E.L.D.; Misty Knight; Ragnarok.
- **Schemes (eight):** Avengers vs. X-Men; Dark Reign of H.A.M.M.E.R. Officers; Epic Super Hero Civil War; Imprison Unregistered Superhumans; Nitro the Supervillain Threatens Crowds; Predict Future Crime; Reveal Heroes' Secret Identities; United States Split by Civil War.
- **Special Sidekicks (seven types):** Hairball; Lockheed; Lockjaw; Ms. Lion; Redwing; Throg; Zabu.
- **Grievous Wound types (seven):** Blinding Flash; Blunt Force Trauma; Corrosive Webbing; Fatal Blow; Psychic Trauma; Spreading Nanovirus; Subdermal Tracker.
- **Special Bystanders (two types):** Aspiring Hero; Comic Shop Keeper.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from C1, Always Leads, and card-level setup values remain unresolved unless supported by an allowed source.

## Rules and mechanisms

- **Divided Cards (CW p.1):** Each physical card has two sides. Choose one side when playing and use only its abilities; while in hand or the HQ it counts as both sides' classes, teams, and Hero Names. It remains one card for hand size and draw/discard counts. When sorting/setup needs its Hero Name, use the left side.
- **Size-Changing (CW p.1):** A Hero can be recruited for 2 less, or a Villain fought for 2 less, if its owner played a card that turn. A Divided Card's side discounts are separate; only the chosen side's applies.
- **Phasing (CW p.1):** During the player's turn, a Phasing card in hand can swap with the deck's top card. This is neither playing nor drawing a card.
- **Fortify (CW p.2):** A Villain can be placed by a named location and prevent the Mastermind from being fought while it remains there. Defeating it ends the effect; a location already fortified is not fortified again.
- **S.H.I.E.L.D. Clearance (CW p.2):** To fight a Villain with this requirement, discard a Hero as an extra cost; Double Clearance requires two. Heroes can be discarded instead when playing with Heroes.
- **Bribe (CW p.2):** A Villain with Bribe can be fought using any combination of Attack and Recruit points.
- **Pet Avengers Sidekicks (CW p.2):** Shuffle the 15 special Sidekicks into a face-down Sidekick Stack. Once per turn, a player may pay 2 to recruit its top card. Played Sidekicks return to the bottom; a card effect that grants a Sidekick instead puts the top card in the player's discard pile and does not use the recruit limit. The special Sidekicks join the Secret Wars Volume 1 Sidekick Stack if both products are used.
- **Grievous Wounds (CW p.2):** Shuffle 15 into the Wound Stack, making 45 total with the standard 30. The insert distinguishes their Healing from a normal Wound; a normal Wound in hand is needed to use its normal “KO all your Wounds” ability on them.
- **Scheme clarification (CW p.2):** Change the Outcome of WWII begins in the normal city and marks a new edge when entering another country. In smaller countries, remove spaces starting with the Bridge; after a capital falls, remain in that country until another Scheme Twist.

## Required parts and glossary

- The product adds 15 Special Sidekicks to the shared Sidekick Stack, 15 Grievous Wounds to the Wound Stack, and 7 Special Bystanders across two types (CW p.2).
- **Divided Card:** Choose one side when playing; before play, the card represents both sides' listed Hero identities and attributes. (CW p.1)
- **Size-Changing:** A qualifying played card enables a 2-point discount to recruit or fight. (CW p.1)
- **Phasing:** A card in hand can exchange places with the deck's top card during its owner's turn. (CW p.1)
- **Fortify:** A Villain temporarily protects a named location until defeated. (CW p.2)
- **S.H.I.E.L.D. Clearance:** Fighting the marked enemy requires discarding the specified number of Heroes. (CW p.2)

Summaries are original paraphrases under 40 words. Other terms on the card faces need verification from the cards and their governing rules pages.

## Setup and implementation gaps

The insert does not provide all eight Scheme setup lines or the five Masterminds' Always Leads/setup effects. Verify player limits, Twist values, required groups/Heroes, moves, constraints, and setup steps from the printed cards. The insert mentions Hero cards as substitutions for S.H.I.E.L.D. Clearance, but their complete setup role is not documented here. Exact Healing symbols/costs on Grievous Wound cards also need a clear card reference.

The Special Sidekick, Grievous Wound, and Special Bystander stacks need conditional shared-stack representation. Divided Cards also need their left-side Hero Name and side-specific metadata represented without treating the two sides as two cards. No runtime data or code is changed in this record.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Captain America, Secret Avenger

- **Bold Leadership**; type/group: Hero / Captain America, Secret Avenger; copies: Unverified; Hero Name: Captain America, Secret Avenger; team: Avengers; class icons: Covert; printed values: Cost 2; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-secret-avenger-03.png).
- **Inspire a Nation**; type/group: Hero / Captain America, Secret Avenger; copies: Unverified; Hero Name: Captain America, Secret Avenger; team: Avengers; class icons: Strength; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-secret-avenger-04.png).
- **Inspire a Man**; type/group: Hero / Captain America, Secret Avenger; copies: Unverified; Hero Name: Captain America, Secret Avenger; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 2; card image: unavailable in C1.
- **Secret Avengers Assemble!**; type/group: Hero / Captain America, Secret Avenger; copies: Unverified; Hero Name: Captain America, Secret Avenger; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-secret-avenger-02.png).
- **Freedom Never Dies**; type/group: Hero / Captain America, Secret Avenger; copies: Unverified; Hero Name: Captain America, Secret Avenger; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-america-secret-avenger-01.png).

### Hero group: Cloak & Dagger

- **Above**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cloak-and-dagger-03.png).
- **Below**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Marvel Knights; class icons: Ranged; printed values: Cost 3; Recruit 0+; card image: unavailable in C1.
- **Flee**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Avengers; class icons: Covert; printed values: Cost 4; Recruit 2+; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cloak-and-dagger-04.png).
- **Fight**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Marvel Knights; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Phasing; card image: unavailable in C1.
- **Darkness**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Avengers; class icons: Covert; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cloak-and-dagger-02.png).
- **Light**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Marvel Knights; class icons: Ranged; printed values: Cost 6; Recruit 3; card image: unavailable in C1.
- **Penumbra**; type/group: Hero / Cloak & Dagger; copies: Unverified; Hero Name: Cloak & Dagger; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cloak-and-dagger-01.png).

### Hero group: Daredevil

- **Dual Existence**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Avengers; class icons: Instinct; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-04-1.png).
- **Roundhouse Side Kick**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Avengers; class icons: Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-03-1.png).
- **Hidden Identity**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Avengers; class icons: Instinct; printed values: Cost 6; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-02-1.png).
- **Revealed Identity**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Marvel Knights; class icons: Strength; printed values: Cost 6; Attack 0+; card image: unavailable in C1.
- **Master of Martial Arts**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Avengers; class icons: Covert; printed values: Cost 8; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-01-1.png).

### Hero group: Falcon

- **Rapid Reinforcements**; type/group: Hero / Falcon; copies: Unverified; Hero Name: Falcon; team: Avengers; class icons: Tech; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-04-1.png).
- **Talk with Birds**; type/group: Hero / Falcon; copies: Unverified; Hero Name: Falcon; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-03-1.png).
- **Squawk Back**; type/group: Hero / Falcon; copies: Unverified; Hero Name: Falcon; team: Avengers; class icons: Instinct; printed values: Cost 4; card image: unavailable in C1.
- **Scout the Battlefield**; type/group: Hero / Falcon; copies: Unverified; Hero Name: Falcon; team: Avengers; class icons: Ranged; printed values: Cost 6; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-02-1.png).
- **Fly in a Friend**; type/group: Hero / Falcon; copies: Unverified; Hero Name: Falcon; team: Avengers; class icons: Instinct; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/falcon-01-1.png).

### Hero group: Goliath

- **Brilliant Biochemist**; type/group: Hero / Goliath; copies: Unverified; Hero Name: Goliath; team: Avengers; class icons: Tech; printed values: Cost 4*; Recruit 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/goliath-04.png).
- **Massive Warrior**; type/group: Hero / Goliath; copies: Unverified; Hero Name: Goliath; team: Avengers; class icons: Strength; printed values: Cost 4*; Attack 2; keyword labels: Size-Changing; card image: unavailable in C1.
- **Growth Industry**; type/group: Hero / Goliath; copies: Unverified; Hero Name: Goliath; team: Avengers; class icons: Tech; printed values: Cost 5*; Attack 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/goliath-03.png).
- **Being Big is Best**; type/group: Hero / Goliath; copies: Unverified; Hero Name: Goliath; team: Avengers; class icons: Strength; printed values: Cost 6*; Attack 3+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/goliath-02.png).
- **Enormous Implications**; type/group: Hero / Goliath; copies: Unverified; Hero Name: Goliath; team: Avengers; class icons: Strength; printed values: Cost 8*; Attack 0+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/goliath-01.png).

### Hero group: Hercules

- **Manly Dullard**; type/group: Hero / Hercules; copies: Unverified; Hero Name: Hercules; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hercules-03.png).
- **Boy Genius**; type/group: Hero / Hercules; copies: Unverified; Hero Name: Hercules; team: Unaffiliated; class icons: Tech; printed values: Cost 3; card image: unavailable in C1.
- **Crowd Favorite**; type/group: Hero / Hercules; copies: Unverified; Hero Name: Hercules; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hercules-04.png).
- **Prince of Power**; type/group: Hero / Hercules; copies: Unverified; Hero Name: Hercules; team: Avengers; class icons: Strength; printed values: Cost 5; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hercules-02.png).
- **Son of Zeus**; type/group: Hero / Hercules; copies: Unverified; Hero Name: Hercules; team: Avengers; class icons: Strength; printed values: Cost 7; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hercules-01.png).

### Hero group: Hulkling

- **Half-Kree**; type/group: Hero / Hulkling; copies: Unverified; Hero Name: Hulkling; team: Avengers; class icons: Strength; printed values: Cost 4*; Recruit 3; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkling-03.png).
- **Half-Skrull**; type/group: Hero / Hulkling; copies: Unverified; Hero Name: Hulkling; team: Avengers; class icons: Covert; printed values: Cost 4*; Attack 2; keyword labels: Size-Changing; card image: unavailable in C1.
- **Cellular Regeneration**; type/group: Hero / Hulkling; copies: Unverified; Hero Name: Hulkling; team: Avengers; class icons: Strength; printed values: Cost 5*; Attack 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkling-04.png).
- **Impersonate**; type/group: Hero / Hulkling; copies: Unverified; Hero Name: Hulkling; team: Avengers; class icons: Covert; printed values: Cost 6*; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkling-02.png).
- **Enormous Shapeshifter**; type/group: Hero / Hulkling; copies: Unverified; Hero Name: Hulkling; team: Avengers; class icons: Covert; printed values: Cost 8*; Attack 4+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/hulkling-01.png).

### Hero group: Luke Cage

- **Cautious**; type/group: Hero / Luke Cage; copies: Unverified; Hero Name: Luke Cage; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-03.png).
- **Reckless**; type/group: Hero / Luke Cage; copies: Unverified; Hero Name: Luke Cage; team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Attack 3; card image: unavailable in C1.
- **Take a Bullet for the Team**; type/group: Hero / Luke Cage; copies: Unverified; Hero Name: Luke Cage; team: Avengers; class icons: Strength; printed values: Cost 4; Recruit 1; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-04.png).
- **Sweet Christmas**; type/group: Hero / Luke Cage; copies: Unverified; Hero Name: Luke Cage; team: Avengers; class icons: Instinct; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-02.png).
- **Unbreakable Skin**; type/group: Hero / Luke Cage; copies: Unverified; Hero Name: Luke Cage; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 6; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/luke-cage-01.png).

### Hero group: Patriot

- **New Generation of Heroes**; type/group: Hero / Patriot; copies: Unverified; Hero Name: Patriot; team: Avengers; class icons: Strength; printed values: Cost 2; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/patriot-04.png).
- **Intuitive Tactician**; type/group: Hero / Patriot; copies: Unverified; Hero Name: Patriot; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/patriot-03.png).
- **Incredible Effort**; type/group: Hero / Patriot; copies: Unverified; Hero Name: Patriot; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/patriot-02.png).
- **Effortless**; type/group: Hero / Patriot; copies: Unverified; Hero Name: Patriot; team: Avengers; class icons: Tech; printed values: Cost 5; Recruit 3; card image: unavailable in C1.
- **Lead the Young Avengers**; type/group: Hero / Patriot; copies: Unverified; Hero Name: Patriot; team: Avengers; class icons: Tech; printed values: Cost 8; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/patriot-01.png).

### Hero group: Peter Parker

- **Conflicted Loyalties**; type/group: Hero / Peter Parker; copies: Unverified; Hero Name: Peter Parker; team: Avengers; class icons: Tech; printed values: Cost 2; Recruit 1; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-04.png).
- **Spider-Man Unmasked**; type/group: Hero / Peter Parker; copies: Unverified; Hero Name: Peter Parker; team: Avengers; class icons: Instinct; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-03.png).
- **Protect My Family**; type/group: Hero / Peter Parker; copies: Unverified; Hero Name: Peter Parker; team: Avengers; class icons: Tech; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-02.png).
- **Hot Bowl of Soup**; type/group: Hero / Peter Parker; copies: Unverified; Hero Name: Peter Parker; team: Spider Friends; class icons: Instinct; printed values: Cost 2; Recruit 1; card image: unavailable in C1.
- **Reluctant Celebrity**; type/group: Hero / Peter Parker; copies: Unverified; Hero Name: Peter Parker; team: Avengers; class icons: Instinct; printed values: Cost 2; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/peter-parker-01.png).

### Hero group: Speedball

- **Reckless Rescue Attempt**; type/group: Hero / Speedball; copies: Unverified; Hero Name: Speedball; team: New Warriors; class icons: Ranged; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speedball-03.png).
- **Bounce Around**; type/group: Hero / Speedball; copies: Unverified; Hero Name: Speedball; team: New Warriors; class icons: Covert; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speedball-04.png).
- **Double Down**; type/group: Hero / Speedball; copies: Unverified; Hero Name: Speedball; team: New Warriors; class icons: Ranged; printed values: Cost 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speedball-02.png).
- **Bubble Up**; type/group: Hero / Speedball; copies: Unverified; Hero Name: Speedball; team: New Warriors; class icons: Covert; printed values: Cost 5; Attack 0+; card image: unavailable in C1.
- **Kinetic Force Field**; type/group: Hero / Speedball; copies: Unverified; Hero Name: Speedball; team: New Warriors; class icons: Ranged; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/speedball-01.png).

### Hero group: Stature

- **Shrink to Nothing**; type/group: Hero / Stature; copies: Unverified; Hero Name: Stature; team: Avengers; class icons: Tech; printed values: Cost 2*; Attack 0+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stature-04.png).
- **Crush Ants**; type/group: Hero / Stature; copies: Unverified; Hero Name: Stature; team: Avengers; class icons: Strength; printed values: Cost 5*; Attack 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stature-03.png).
- **Crush File Sizes**; type/group: Hero / Stature; copies: Unverified; Hero Name: Stature; team: Avengers; class icons: Tech; printed values: Cost 5*; Recruit 2; keyword labels: Size-Changing; card image: unavailable in C1.
- **Growing Confidence**; type/group: Hero / Stature; copies: Unverified; Hero Name: Stature; team: Avengers; class icons: Strength; printed values: Cost 6*; Attack 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stature-02.png).
- **Trample the Tiny**; type/group: Hero / Stature; copies: Unverified; Hero Name: Stature; team: Avengers; class icons: Strength; printed values: Cost 8*; Attack 5; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stature-01.png).

### Hero group: Storm & Black Panther

- **Gathering Rain Clouds**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: X-Men; class icons: Ranged; printed values: Cost 2; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-and-storm-04.png).
- **Gathering Clues**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: Avengers; class icons: Instinct; printed values: Cost 2; Attack 1; card image: unavailable in C1.
- **Lightning Strike**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-and-storm-03.png).
- **Pouncing Strike**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: Avengers; class icons: Instinct; printed values: Cost 3; Recruit 2; card image: unavailable in C1.
- **Tsunami of Water**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: X-Men; class icons: Ranged; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-and-storm-02.png).
- **Tsunami of Justice**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 3; card image: unavailable in C1.
- **King & Queen of Wakanda**; type/group: Hero / Storm & Black Panther; copies: Unverified; Hero Name: Storm & Black Panther; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-and-storm-01-1.png).

### Hero group: Tigra

- **Friendship**; type/group: Hero / Tigra; copies: Unverified; Hero Name: Tigra; team: Avengers; class icons: Covert; printed values: Cost 2; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tigra-03.png).
- **Ferocity**; type/group: Hero / Tigra; copies: Unverified; Hero Name: Tigra; team: Avengers; class icons: Instinct; printed values: Cost 2; Attack 1; card image: unavailable in C1.
- **Supernatural Senses**; type/group: Hero / Tigra; copies: Unverified; Hero Name: Tigra; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tigra-04.png).
- **Can't Surprise a Cat**; type/group: Hero / Tigra; copies: Unverified; Hero Name: Tigra; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tigra-02.png).
- **Mystic Talisman**; type/group: Hero / Tigra; copies: Unverified; Hero Name: Tigra; team: Avengers; class icons: Covert; printed values: Cost 7; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/tigra-01.png).

### Hero group: Vision

- **Solar Energy**; type/group: Hero / Vision; copies: Unverified; Hero Name: Vision; team: Avengers; class icons: Ranged; printed values: Cost 3; Attack 1+; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vision-04.png).
- **Through Solid Objects**; type/group: Hero / Vision; copies: Unverified; Hero Name: Vision; team: Avengers; class icons: Tech; printed values: Cost 4; Recruit 2; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vision-03.png).
- **Lighter than Air**; type/group: Hero / Vision; copies: Unverified; Hero Name: Vision; team: Avengers; class icons: Ranged; printed values: Cost 6*; Recruit 3; keyword labels: Size-Changing, Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vision-02.png).
- **Harder than Diamond**; type/group: Hero / Vision; copies: Unverified; Hero Name: Vision; team: Avengers; class icons: Tech; printed values: Cost 6*; Attack 3; keyword labels: Size-Changing, Phasing; card image: unavailable in C1.
- **Insubstantial Accomplishments**; type/group: Hero / Vision; copies: Unverified; Hero Name: Vision; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 4; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/vision-01.png).

### Hero group: Wiccan

- **Sorcerous Illusions**; type/group: Hero / Wiccan; copies: Unverified; Hero Name: Wiccan; team: Avengers; class icons: Covert; printed values: Cost 2; Recruit 1+; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wiccan-04.png).
- **Astral Projection**; type/group: Hero / Wiccan; copies: Unverified; Hero Name: Wiccan; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wiccan-03.png).
- **Supersonic Spells**; type/group: Hero / Wiccan; copies: Unverified; Hero Name: Wiccan; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wiccan-02.png).
- **Supersonic Speed**; type/group: Hero / Wiccan; copies: Unverified; Hero Name: Wiccan; team: Avengers; class icons: Covert; printed values: Cost 4; card image: unavailable in C1.
- **Clairvoyance**; type/group: Hero / Wiccan; copies: Unverified; Hero Name: Wiccan; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wiccan-01.png).

### Villain Group: CSA Special Marshals

- **Bullseye**; type/group: Villain / CSA Special Marshals; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/csa-special-marshals-03.png).
- **Moonstone**; type/group: Villain / CSA Special Marshals; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/csa-special-marshals-04.png).
- **Penance**; type/group: Villain / CSA Special Marshals; copies: 2; printed values: Attack 2+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/csa-special-marshals-01.png).
- **Venom**; type/group: Villain / CSA Special Marshals; copies: 2; printed values: Attack 7*; VP 4; keyword labels: Size-Changing, Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/csa-special-marshals-02.png).

### Villain Group: Great Lakes Avengers

- **Big Bertha**; type/group: Villain / Great Lakes Avengers; copies: 2; printed values: Attack 7*; VP 4; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/great-lakes-avengers-01.png).
- **Flatman**; type/group: Villain / Great Lakes Avengers; copies: 2; printed values: Attack 5*; VP 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/great-lakes-avengers-04.png).
- **Mr. Immortal**; type/group: Villain / Great Lakes Avengers; copies: 2; printed values: Attack 2; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/great-lakes-avengers-03.png).
- **Squirrel Girl**; type/group: Villain / Great Lakes Avengers; copies: 2; printed values: Attack 3*; VP 2; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/great-lakes-avengers-02.png).

### Villain Group: Heroes for Hire

- **Colleen Wing**; type/group: Villain / Heroes for Hire; copies: 2; printed values: Attack 9*; VP 5; keyword labels: Bribe, Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heroes-for-hire-04.png).
- **Humbug**; type/group: Villain / Heroes for Hire; copies: 2; printed values: Attack 5*; VP 3; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heroes-for-hire-03.png).
- **Shang-Chi**; type/group: Villain / Heroes for Hire; copies: 2; printed values: Attack 3*; VP 2; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heroes-for-hire-02.png).
- **Tarantula**; type/group: Villain / Heroes for Hire; copies: 2; printed values: Attack 7*; VP 5; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heroes-for-hire-01.png).

### Villain Group: Registration Enforcers

- **Blade**; type/group: Villain / Registration Enforcers; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/registration-enforcers-03.png).
- **Captain Marvel**; type/group: Villain / Registration Enforcers; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/registration-enforcers-04.png).
- **Deadpool**; type/group: Villain / Registration Enforcers; copies: 2; printed values: Attack 5; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/registration-enforcers-01.png).
- **Micromax**; type/group: Villain / Registration Enforcers; copies: 2; printed values: Attack 6*; VP 3; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/registration-enforcers-02.png).

### Villain Group: S.H.I.E.L.D. Elite

- **Agent Eric Marshall**; type/group: Villain / S.H.I.E.L.D. Elite; copies: 2; printed values: Attack 1*; VP 2; keyword labels: S.H.I.E.L.D. Clearance; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shield-elite-02.png).
- **Agent Gabe Jones**; type/group: Villain / S.H.I.E.L.D. Elite; copies: 2; printed values: Attack 2*; VP 2; keyword labels: S.H.I.E.L.D. Clearance; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shield-elite-01.png).
- **Dum Dum Dugan**; type/group: Villain / S.H.I.E.L.D. Elite; copies: 2; printed values: Attack 4*; VP 3; keyword labels: S.H.I.E.L.D. Clearance; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shield-elite-04.png).
- **Sharon Carter, Agent 13**; type/group: Villain / S.H.I.E.L.D. Elite; copies: 2; printed values: Attack 6*; VP 5; keyword labels: S.H.I.E.L.D. Clearance, Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/shield-elite-03.png).

### Villain Group: Superhuman Registration Act

- **Iron Spider**; type/group: Villain / Superhuman Registration Act; copies: 2; printed values: Attack 2; VP 3; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/superhuman-registration-act-02.png).
- **Ms. Marvel**; type/group: Villain / Superhuman Registration Act; copies: 2; printed values: Attack 5; VP 3; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/superhuman-registration-act-03.png).
- **She-Hulk**; type/group: Villain / Superhuman Registration Act; copies: 2; printed values: Attack 8*; VP 5; keyword labels: Size-Changing, Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/superhuman-registration-act-04.png).
- **Yellowjacket**; type/group: Villain / Superhuman Registration Act; copies: 2; printed values: Attack 7*; VP 4; keyword labels: Size-Changing, Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/superhuman-registration-act-01.png).

### Villain Group: Thunderbolts

- **Fixer**; type/group: Villain / Thunderbolts; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/thunderbolts-02.png).
- **Mach-IV**; type/group: Villain / Thunderbolts; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/thunderbolts-04.png).
- **Radioactive Man**; type/group: Villain / Thunderbolts; copies: 2; printed values: Attack 6; VP 4; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/thunderbolts-03.png).
- **Songbird**; type/group: Villain / Thunderbolts; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/thunderbolts-01.png).

### Henchman Group: Mandroid

- **Mandroid**; type/group: Henchman / Mandroid; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mandroid.png).

### Henchman Group: Cape-Killers

- **Cape-Killers**; type/group: Henchman / Cape-Killers; copies: Unverified; printed values: not indexed in C1; keyword labels: S.H.I.E.L.D. Clearance; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/cape-killers.png).

### Mastermind: Authoritarian Iron Man

- **Authoritarian Iron Man**; type/group: Normal Mastermind face / Authoritarian Iron Man; copies: Unverified; printed values: VP 6; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/authoritarian-iron-man-01.png).
- **Armada of Armors**; type/group: Mastermind Tactic / Authoritarian Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/authoritarian-iron-man-02.png).
- **Freeze Domestic Assets**; type/group: Mastermind Tactic / Authoritarian Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/authoritarian-iron-man-03.png).
- **Man the Fortifications**; type/group: Mastermind Tactic / Authoritarian Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/authoritarian-iron-man-04.png).
- **Recall to Service**; type/group: Mastermind Tactic / Authoritarian Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/authoritarian-iron-man-05.png).

### Mastermind: Baron Helmut Zemo

- **Baron Helmut Zemo**; type/group: Normal Mastermind face / Baron Helmut Zemo; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-helmut-zemo-01.png).
- **Blasted Henchmen!**; type/group: Mastermind Tactic / Baron Helmut Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-helmut-zemo-02.png).
- **Cursed Dynasty**; type/group: Mastermind Tactic / Baron Helmut Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-helmut-zemo-03.png).
- **Endless Minions**; type/group: Mastermind Tactic / Baron Helmut Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-helmut-zemo-04.png).
- **Revenge for My Father**; type/group: Mastermind Tactic / Baron Helmut Zemo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/baron-helmut-zemo-05.png).

### Mastermind: Maria Hill, Director of S.H.I.E.L.D.

- **Maria Hill, Director of S.H.I.E.L.D.**; type/group: Normal Mastermind face / Maria Hill, Director of S.H.I.E.L.D.; copies: Unverified; printed values: VP 6; keyword labels: S.H.I.E.L.D. Clearance; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maria-hill-01.png).
- **Crash the Helicarrier**; type/group: Mastermind Tactic / Maria Hill, Director of S.H.I.E.L.D.; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maria-hill-02.png).
- **Declare Martial Law**; type/group: Mastermind Tactic / Maria Hill, Director of S.H.I.E.L.D.; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maria-hill-03.png).
- **Evacuation Code Epsilon**; type/group: Mastermind Tactic / Maria Hill, Director of S.H.I.E.L.D.; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maria-hill-04.png).
- **Rapid Response Team**; type/group: Mastermind Tactic / Maria Hill, Director of S.H.I.E.L.D.; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/maria-hill-05.png).

### Mastermind: Misty Knight

- **Misty Knight**; type/group: Normal Mastermind face / Misty Knight; copies: Unverified; printed values: VP 6; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/misty-knight-01.png).
- **Bionic Repulsor Field**; type/group: Mastermind Tactic / Misty Knight; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/misty-knight-02.png).
- **Cyborg Detective**; type/group: Mastermind Tactic / Misty Knight; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/misty-knight-03.png).
- **Trusted Bodyguard**; type/group: Mastermind Tactic / Misty Knight; copies: Unverified; printed values: not indexed in C1; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/misty-knight-04.png).
- **Vibranium Cyber Arm**; type/group: Mastermind Tactic / Misty Knight; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/misty-knight-05.png).

### Mastermind: Ragnarok

- **Ragnarok**; type/group: Normal Mastermind face / Ragnarok; copies: Unverified; printed values: Attack 6+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ragnarok-01.png).
- **Electrical Charge**; type/group: Mastermind Tactic / Ragnarok; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ragnarok-02.png).
- **God of Cyborg Thunder**; type/group: Mastermind Tactic / Ragnarok; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ragnarok-03.png).
- **Hammer Goliath**; type/group: Mastermind Tactic / Ragnarok; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ragnarok-04.png).
- **Unnatural Storm Clouds**; type/group: Mastermind Tactic / Ragnarok; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ragnarok-05.png).

### Scheme: Avengers vs. X-Men

- **Avengers vs. X-Men**; type/group: Scheme / Avengers vs. X-Men; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/70Scheme(87).png).

### Scheme: Dark Reign of H.A.M.M.E.R. Officers

- **Dark Reign of H.A.M.M.E.R. Officers**; type/group: Scheme / Dark Reign of H.A.M.M.E.R. Officers; copies: Unverified; printed values: not indexed in C1; keyword labels: S.H.I.E.L.D. Clearance; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/71Scheme(88).png).

### Scheme: Epic Super Hero Civil War

- **Epic Super Hero Civil War**; type/group: Scheme / Epic Super Hero Civil War; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/69Scheme(86).png).

### Scheme: Imprison Unregistered Superhumans

- **Imprison Unregistered Superhumans**; type/group: Scheme / Imprison Unregistered Superhumans; copies: Unverified; printed values: not indexed in C1; keyword labels: Fortify; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/64Scheme(14).png).

### Scheme: Nitro the Supervillain Threatens Crowds

- **Nitro the Supervillain Threatens Crowds**; type/group: Scheme / Nitro the Supervillain Threatens Crowds; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/67Scheme(84).png).

### Scheme: Predict Future Crime

- **Predict Future Crime**; type/group: Scheme / Predict Future Crime; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/66Scheme(83).png).

### Scheme: Reveal Heroes' Secret Identities

- **Reveal Heroes' Secret Identities**; type/group: Scheme / Reveal Heroes' Secret Identities; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/65Scheme(82).png).

### Scheme: United States Split by Civil War

- **United States Split by Civil War**; type/group: Scheme / United States Split by Civil War; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/68Scheme(85).png).

### Bystander set: Aspiring Hero

- **Aspiring Hero**; type/group: Bystander / Aspiring Hero; copies: 4; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-aspiring-hero.png).

### Bystander set: Comic Shop Keeper

- **Comic Shop Keeper**; type/group: Bystander / Comic Shop Keeper; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-comic-shop-keeper.png).

### Wound set: Blinding Flash

- **Blinding Flash**; type/group: Wound / Blinding Flash; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_blindingflash.png).

### Wound set: Blunt Force Trauma

- **Blunt Force Trauma**; type/group: Wound / Blunt Force Trauma; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_bluntforcetrauma.png).

### Wound set: Corrosive Webbing

- **Corrosive Webbing**; type/group: Wound / Corrosive Webbing; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_corrosivewebbing.png).

### Wound set: Fatal Blow

- **Fatal Blow**; type/group: Wound / Fatal Blow; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_fatalblow.png).

### Wound set: Psychic Trauma

- **Psychic Trauma**; type/group: Wound / Psychic Trauma; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_psychictrauma.png).

### Wound set: Spreading Nanovirus

- **Spreading Nanovirus**; type/group: Wound / Spreading Nanovirus; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_spreadingnanovirus.png).

### Wound set: Subdermal Tracker

- **Subdermal Tracker**; type/group: Wound / Subdermal Tracker; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/gw_subdermaltracker.png).

### Sidekick set: Hairball

- **Hairball**; type/group: Sidekick / Hairball; copies: 3; printed values: Cost 2; Attack 1; card image: unavailable in C1.

### Sidekick set: Lockheed

- **Lockheed**; type/group: Sidekick / Lockheed; copies: 2; printed values: Cost 2; Attack 2+; card image: unavailable in C1.

### Sidekick set: Lockjaw

- **Lockjaw**; type/group: Sidekick / Lockjaw; copies: 2; printed values: Cost 2; Attack 2; keyword labels: Phasing; card image: unavailable in C1.

### Sidekick set: Ms. Lion

- **Ms. Lion**; type/group: Sidekick / Ms. Lion; copies: 2; printed values: Cost 2; card image: unavailable in C1.

### Sidekick set: Redwing

- **Redwing**; type/group: Sidekick / Redwing; copies: 2; printed values: Cost 2; card image: unavailable in C1.

### Sidekick set: Throg

- **Throg**; type/group: Sidekick / Throg; copies: 2; printed values: Cost 2; Recruit 2; Attack 0+; card image: unavailable in C1.

### Sidekick set: Zabu

- **Zabu**; type/group: Sidekick / Zabu; copies: 2; printed values: Cost 2; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
