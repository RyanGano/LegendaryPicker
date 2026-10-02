# Marvel Studios' Ant-Man and the Wasp (Dec 2023/Jan 2024)

**Research status: Partial.** Upper Deck verifies this is a 200-card expansion and explains five keywords plus Ambush Schemes. Official category counts and card-level setup details remain unavailable.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured marvel-studios-ant-man-and-the-wasp card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/msaw.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| AW | [Upper Deck Ant-Man and the Wasp article](https://upperdeck.com/legendary-marvel-studios-ant-man-and-the-wasp/) | 200-card expansion, five keywords, returning features, Ambush Scheme behavior, and pictured card examples. |
| C2 | [nutki/legendary Ant-Man and the Wasp name catalog](https://github.com/nutki/legendary/tree/master/texttools/Ant-Man%20and%20the%20Wasp) | Card and group names/membership only; not mechanics, component counts, or setup values. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, expansion status, Revised ruleset classification. |

## Catalog inventory

Upper Deck describes a **200-card deluxe box expansion** (AW). An official itemized contents list, rules insert, or complete allowed card-face source was not located, so counts by card type/group and printed card identities are unresolved. C1 face metadata is indexed below with direct image URLs where supplied; C2 remains a names/mappings source:

- **Heroes (eight):** Ant Army; Ant-Man; Cassie Lang; Freedom Fighters; Janet Van Dyne; Jentorra; Scott Lang, Cat Burglar; Wasp.
- **Villain Groups (four):** Armada of Kang; Cross Technologies; Ghost Chasers; Quantum Realm.
- **Henchman Groups (three):** Quantum Hound; Quantumnauts; Tardigrade.
- **Mastermind card faces (six C2 names):** Darren Cross; Yellowjacket; Ghost, Master Thief; Ghost, Intangible; Kang, Quantum Conqueror; Kang, Multiverse Conqueror. Upper Deck describes Transforming Masterminds, but the physical pairings and number of sets need card-level verification.
- **Schemes (four C2 names):** Auction Shrink Tech to Highest Bidder; Escape an Imprisoning Dimension; Safeguard Dark Secrets; Siphon Energy from the Quantum Realm.
- **Ambush Scheme examples (AW):** High-Speed Car Chase (Ghost Chasers); Quantumania (Quantum Realm). The examples do not establish the full Ambush Scheme inventory.
- **Named Bystanders (four C2 names):** Agent Jimmy Woo; Maggie Lang; Officer Jim Paxton; Young Cassie Lang.

The C1 face index below records available Hero Names, teams/classes, printed values, and image links. Fields absent from it and the official component totals remain unresolved; C1 ability prose is not rules evidence.

## Rules and mechanisms

- **Heist (AW):** Compare the nonzero costs among your Heroes with the Victory Point value of a newly revealed Villain to determine the heist result. The article does not specify every result threshold or card-specific condition.
- **Double-Cross (AW):** Each player reveals their hand and discards a highest-cost card whose cost is shared by another card in that hand. The exact handling of ties and card-specific variations needs card-level verification.
- **Explore (AW):** Put an HQ Hero on the bottom of the Hero Deck, reveal two replacements, and choose one to refill the space. The chosen “Found Hero” can enable additional effects.
- **Microscopic Size-Changing (AW):** Each matching Hero Class played this turn reduces the relevant Recruit cost by 2 for each listed Class icon; a Villain version reduces Attack similarly. Recruiting below zero can yield Recruit points. Verify printed icons and exact card-specific values from cards.
- **Antics (AW):** To use an Antics ability when playing its card, have at least three cards costing 1 or 2, or with Size-Changing (including Microscopic Size-Changing). The Antics card counts, as do cards already played and in hand; the ability cannot be used later in the turn.
- **Ambush Schemes (AW):** Shuffle each into the Villain Deck with its associated Villain Group. When revealed, place it next to the main Scheme and resolve its Ambush; each later Scheme Twist triggers both Schemes. An Ambush Scheme can be defeated like a normal Scheme.
- **Returning features (AW):** Upper Deck also identifies Conqueror, Size-Changing, and Transforming Masterminds as returning features; complete rules and card-specific setup effects are not given in the article.

## Required parts and glossary

The described mechanics use the Villain Deck, Hero Deck, and HQ; the article identifies no new shared token or separate stack. An official itemized contents list and card-level checks are still needed to verify any additional shared-component dependencies, including possible Wound use.

- **Heist:** Compare the costs among your Heroes with a revealed Villain's Victory Point value. (AW)
- **Double-Cross:** Reveal hands and discard a highest-cost card sharing its cost with another card in that hand. (AW)
- **Explore:** Replace an HQ Hero by bottom-decking it and choosing from two new Hero cards. (AW)
- **Microscopic Size-Changing:** Reduce cost or Attack using matching Classes played this turn. (AW)
- **Antics:** A card ability gated by having at least three qualifying low-cost or Size-Changing cards. (AW)
- **Ambush Scheme:** A Scheme card that enters from the Villain Deck and triggers alongside the main Scheme on each Twist. (AW)

Summaries are original paraphrases under 40 words.

## Setup and implementation gaps

Verify the 200-card category/group totals, physical Transforming Mastermind pairings, all Ambush Scheme names and group links, complete Scheme player limits/setup values/effects, Mastermind Always Leads/setup effects, and Hero metadata against official contents and product cards. The product article does not provide a base-game setup table; this expansion uses the Revised ruleset indicated by the roadmap. Research does not change runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Scott Lang, Cat Burglar

- **Petty Larceny**; type/group: Hero / Scott Lang, Cat Burglar; copies: 3; Hero Name: Scott Lang, Cat Burglar; team: Crime Syndicate; class icons: Covert; printed values: Cost 1; Recruit 1+; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ScottLangCatBurgler_2Common.png).
- **Shocking Support**; type/group: Hero / Scott Lang, Cat Burglar; copies: 3; Hero Name: Scott Lang, Cat Burglar; team: Crime Syndicate; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ScottLangCatBurgler_3Common.png).
- **X-Con Security Van**; type/group: Hero / Scott Lang, Cat Burglar; copies: 3; Hero Name: Scott Lang, Cat Burglar; team: Crime Syndicate; class icons: Tech; printed values: Cost 5; Attack 2; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ScottLangCatBurgler_4Common.png).
- **Anything for Cassie**; type/group: Hero / Scott Lang, Cat Burglar; copies: 2; Hero Name: Scott Lang, Cat Burglar; team: Crime Syndicate; class icons: Instinct; printed values: Cost 2; Recruit 1; Attack 1; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ScottLangCatBurgler_5Uncommon.png).
- **Putting a Crew Together**; type/group: Hero / Scott Lang, Cat Burglar; copies: 2; Hero Name: Scott Lang, Cat Burglar; team: Crime Syndicate; class icons: Strength; printed values: Cost 6; Attack 3+; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ScottLangCatBurgler_6Uncommon.png).
- **The Big Score**; type/group: Hero / Scott Lang, Cat Burglar; copies: 1; Hero Name: Scott Lang, Cat Burglar; team: Crime Syndicate; class icons: Covert; printed values: Cost 8; Attack 4+; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ScottLangCatBurgler_1Rare.png).

### Hero group: Ant-Man

- **Hitch a Ride**; type/group: Hero / Ant-Man; copies: 3; Hero Name: Ant-Man; team: Avengers; class icons: Covert; printed values: Cost 2*; Recruit 1+; keyword labels: Size-Changing, Antics; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Ant-ManMCU_2Common.png).
- **Look Out for the Little Guy!**; type/group: Hero / Ant-Man; copies: 3; Hero Name: Ant-Man; team: Avengers; class icons: Strength; printed values: Cost 3*; Attack 1; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Ant-ManMCU_3Common.png).
- **Shrink Away**; type/group: Hero / Ant-Man; copies: 3; Hero Name: Ant-Man; team: Avengers; class icons: Covert; printed values: Cost 4*; Attack 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Ant-ManMCU_4Common.png).
- **Bug Swarm**; type/group: Hero / Ant-Man; copies: 2; Hero Name: Ant-Man; team: Avengers; class icons: Strength; printed values: Cost 5*; Recruit 2; keyword labels: Microscopic Size-Changing, Antics; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Ant-ManMCU_5Uncommon.png).
- **Tiny little Risk**; type/group: Hero / Ant-Man; copies: 2; Hero Name: Ant-Man; team: Avengers; class icons: Covert; printed values: Cost 6*; Attack 2; keyword labels: Microscopic Size-Changing, Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Ant-ManMCU_6Uncommon.png).
- **Giant-Man**; type/group: Hero / Ant-Man; copies: 1; Hero Name: Ant-Man; team: Avengers; class icons: Strength; printed values: Cost 9*; Attack 6+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Ant-ManMCU_1Rare.png).

### Hero group: Wasp

- **Flitting Sting**; type/group: Hero / Wasp; copies: 3; Hero Name: Wasp; team: Avengers; class icons: Ranged; printed values: Cost 3*; Recruit 1+; Attack 1+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WaspMCU_2Common.png).
- **Positive Ions**; type/group: Hero / Wasp; copies: 3; Hero Name: Wasp; team: Avengers; class icons: Ranged; printed values: Cost 5*; Attack 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WaspMCU_4Common.png).
- **Master Physicist**; type/group: Hero / Wasp; copies: 3; Hero Name: Wasp; team: Avengers; class icons: Tech; printed values: Cost 4*; Recruit 2+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WaspMCU_3Common.png).
- **Follow my Lead**; type/group: Hero / Wasp; copies: 2; Hero Name: Wasp; team: Avengers; class icons: Ranged; printed values: Cost 2*; Attack 1+; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WaspMCU_5Uncommon.png).
- **Infiltrate**; type/group: Hero / Wasp; copies: 2; Hero Name: Wasp; team: Avengers; class icons: Tech; printed values: Cost 6*; Attack 1+; keyword labels: Microscopic Size-Changing, Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WaspMCU_6Uncommon.png).
- **Hope Returns**; type/group: Hero / Wasp; copies: 1; Hero Name: Wasp; team: Avengers; class icons: Ranged; printed values: Cost 9*; Recruit 4+; Attack 4+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WaspMCU_1Rare.png).

### Hero group: Cassie Lang

- **Start Small**; type/group: Hero / Cassie Lang; copies: 3; Hero Name: Cassie Lang; team: Avengers; class icons: Tech; printed values: Cost 2*; Recruit 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CassieLang_2Common.png).
- **Giant Hug**; type/group: Hero / Cassie Lang; copies: 3; Hero Name: Cassie Lang; team: Avengers; class icons: Strength; printed values: Cost 4*; Recruit 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CassieLang_3Common.png).
- **Colossal Stomp**; type/group: Hero / Cassie Lang; copies: 3; Hero Name: Cassie Lang; team: Avengers; class icons: Strength; printed values: Cost 5*; Attack 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CassieLang_4Common.png).
- **Quantum Beacon**; type/group: Hero / Cassie Lang; copies: 2; Hero Name: Cassie Lang; team: Avengers; class icons: Tech; printed values: Cost 5*; Attack 3; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CassieLang_5Uncommon.png).
- **Learn from the Past**; type/group: Hero / Cassie Lang; copies: 2; Hero Name: Cassie Lang; team: Avengers; class icons: Strength; printed values: Cost 6*; Attack 3; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CassieLang_6Uncommon.png).
- **Inspire Revolution**; type/group: Hero / Cassie Lang; copies: 1; Hero Name: Cassie Lang; team: Avengers; class icons: Strength; printed values: Cost 9*; Attack 5+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CassieLang_1Rare.png).

### Hero group: Janet van Dyne

- **Search for Peace**; type/group: Hero / Janet van Dyne; copies: 3; Hero Name: Janet van Dyne; team: Unaffiliated; class icons: Covert; printed values: Cost 3; Recruit 2+; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/JanetVanDyne_2Common.png).
- **Prepare for War**; type/group: Hero / Janet van Dyne; copies: 3; Hero Name: Janet van Dyne; team: Unaffiliated; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/JanetVanDyne_3Common.png).
- **Wasp of Another Generation**; type/group: Hero / Janet van Dyne; copies: 3; Hero Name: Janet van Dyne; team: Unaffiliated; class icons: Covert; printed values: Cost 5*; Attack 2+; keyword labels: Size-Changing, Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/JanetVanDyne_4Common.png).
- **Subatomic Size**; type/group: Hero / Janet van Dyne; copies: 2; Hero Name: Janet van Dyne; team: Unaffiliated; class icons: Covert; printed values: Cost 2*; Attack 0+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/JanetVanDyne_5Uncommon.png).
- **Quantum Contradiction**; type/group: Hero / Janet van Dyne; copies: 2; Hero Name: Janet van Dyne; team: Unaffiliated; class icons: Ranged; printed values: Cost 6; Recruit 4; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/JanetVanDyne_6Uncommon.png).
- **Finally Found You**; type/group: Hero / Janet van Dyne; copies: 1; Hero Name: Janet van Dyne; team: Unaffiliated; class icons: Covert; printed values: Cost 8; Recruit 4+; Attack 4+; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/JanetVanDyne_1Rare.png).

### Hero group: Freedom Fighters

- **Mystics**; type/group: Hero / Freedom Fighters; copies: 3; Hero Name: Freedom Fighters; team: Unaffiliated; class icons: Ranged; printed values: Cost 3; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FreedomFighters_2Common.png).
- **Steel Warrior**; type/group: Hero / Freedom Fighters; copies: 3; Hero Name: Freedom Fighters; team: Unaffiliated; class icons: Instinct; printed values: Cost 4; Attack 1+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FreedomFighters_3Common.png).
- **Xolum**; type/group: Hero / Freedom Fighters; copies: 3; Hero Name: Freedom Fighters; team: Unaffiliated; class icons: Ranged; printed values: Cost 5; Attack 2+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FreedomFighters_4Common.png).
- **Veb**; type/group: Hero / Freedom Fighters; copies: 2; Hero Name: Freedom Fighters; team: Unaffiliated; class icons: Instinct; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FreedomFighters_5Uncommon.png).
- **Quaz**; type/group: Hero / Freedom Fighters; copies: 2; Hero Name: Freedom Fighters; team: Unaffiliated; class icons: Ranged; printed values: Cost 6; Recruit 2+; Attack 2+; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FreedomFighters_6Uncommon.png).
- **Freedom Forever**; type/group: Hero / Freedom Fighters; copies: 1; Hero Name: Freedom Fighters; team: Unaffiliated; class icons: Instinct; printed values: Cost 7; Attack 5; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FreedomFighters_1Rare.png).

### Hero group: Jentorra

- **Take the High Ground**; type/group: Hero / Jentorra; copies: 3; Hero Name: Jentorra; team: Unaffiliated; class icons: Strength; printed values: Cost 2; Attack 1+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Jentorra_2Common.png).
- **Hit and Run**; type/group: Hero / Jentorra; copies: 3; Hero Name: Jentorra; team: Unaffiliated; class icons: Instinct; printed values: Cost 3; Recruit 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Jentorra_3Common.png).
- **Unite the Oppressed**; type/group: Hero / Jentorra; copies: 3; Hero Name: Jentorra; team: Unaffiliated; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Jentorra_4Common.png).
- **Find Your Courage**; type/group: Hero / Jentorra; copies: 2; Hero Name: Jentorra; team: Unaffiliated; class icons: Instinct; printed values: Cost 5; Recruit 3+; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Jentorra_5Uncommon.png).
- **Lead Powerful Allies**; type/group: Hero / Jentorra; copies: 2; Hero Name: Jentorra; team: Unaffiliated; class icons: Strength; printed values: Cost 6; Attack 2+; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Jentorra_6Uncommon.png).
- **Conquer the Conqueror**; type/group: Hero / Jentorra; copies: 1; Hero Name: Jentorra; team: Unaffiliated; class icons: Instinct; printed values: Cost 7; Attack 5+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Jentorra_1Rare.png).

### Hero group: Ant Army

- **Up the Ante**; type/group: Hero / Ant Army; copies: 3; Hero Name: Ant Army; team: Unaffiliated; class icons: Instinct; printed values: Cost 1; Attack 1; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/AntArmy_2Common.png).
- **Antagonize**; type/group: Hero / Ant Army; copies: 3; Hero Name: Ant Army; team: Unaffiliated; class icons: Instinct; printed values: Cost 2*; Attack 1+; keyword labels: Size-Changing, Antics; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/AntArmy_3Common.png).
- **Anticipate**; type/group: Hero / Ant Army; copies: 3; Hero Name: Ant Army; team: Unaffiliated; class icons: Tech; printed values: Cost 4*; Recruit 2; keyword labels: Size-Changing, Antics; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/AntArmy_4Common.png).
- **Antiproton Experiments**; type/group: Hero / Ant Army; copies: 2; Hero Name: Ant Army; team: Unaffiliated; class icons: Tech; printed values: Cost 5*; Recruit 1+; Attack 1+; keyword labels: Microscopic Size-Changing, Antics; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/AntArmy_5Uncommon.png).
- **Anti-Tank Weapons**; type/group: Hero / Ant Army; copies: 2; Hero Name: Ant Army; team: Unaffiliated; class icons: Tech; printed values: Cost 6*; Attack 2+; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/AntArmy_6Uncommon.png).
- **Revolutionary Anthem**; type/group: Hero / Ant Army; copies: 1; Hero Name: Ant Army; team: Unaffiliated; class icons: Tech; printed values: Cost 9*; Attack 4+; keyword labels: Microscopic Size-Changing, Antics, Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/AntArmy_1Rare.png).

### Villain Group: Cross Technologies

- **Hydra Arms Dealer**; type/group: Villain / Cross Technologies; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/CrossTechnologiesHydraArmsDealer.png).
- **Cross' Security Detail**; type/group: Villain / Cross Technologies; copies: 2; printed values: Attack 5+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/CrossTechnologiesCrosssSecurityDetail.png).
- **Shrinksperiments**; type/group: Villain / Cross Technologies; copies: 2; printed values: Attack 6*; VP 3; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/CT3.png).
- **Yellowjacket Prototype**; type/group: Villain / Cross Technologies; copies: 1; printed values: Attack 9*; VP 5; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/CrossTechnologiesYellowJacketPrototype.png).
- **Take Over Pym Technologies**; type/group: Scheme / Cross Technologies; copies: 1; printed values: VP 2; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/CrossTechnologiesAmbushSchemeTakeOverPymTechnologies.png).

### Villain Group: Ghost Chasers

- **Sonny Burch's Goons**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 4; VP 2; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersSonnyBurchsGoons.png).
- **Anitolov**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 5; VP 3; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GC%202.png).
- **Corrupted Government Agents**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 5; VP 3; keyword labels: Double-Cross; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersCorruptedGovernmentAgents.png).
- **Uzman, with Truth Serum**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersUzmanWithTruthSerum.png).
- **Sonny Burch**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 6; VP 4; keyword labels: Double-Cross, Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersSonnyBurch.png).
- **Dr. Bill Foster**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 0*; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersDrBillFoster.png).
- **Goliath**; type/group: Villain / Ghost Chasers; copies: 1; printed values: Attack 8*; VP 5; keyword labels: Size-Changing, Double-Cross; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersGoliath.png).
- **High-Speed Car Chase**; type/group: Scheme / Ghost Chasers; copies: 1; printed values: VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/GhostChasersAmbushSchemeHigh-SpeedCarChase.png).

### Villain Group: Armada of Kang

- **Quantumnaut Elite**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangQuantumnautElite.png).
- **Troop Ships of Kang**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangTroopShipsOfKang.png).
- **City Defense System**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangCityDefenseSystem.png).
- **Lord Krylar's Yacht**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 5; VP 3; keyword labels: Double-Cross; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangLordKrylarsYacht.png).
- **Energy Shield**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangEnergyShield.png).
- **Pursuit Craft**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangPursuitCraft.png).
- **M.O.D.O.K.**; type/group: Villain / Armada of Kang; copies: 1; printed values: Attack 8*; VP 5; keyword labels: Microscopic Size-Changing, Double-Cross; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangMODOK.png).
- **Build a Conquering Army**; type/group: Scheme / Armada of Kang; copies: 1; printed values: VP 4; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ArmadaOfKangAmbushSchemeBuildAConqueringArmy.png).

### Villain Group: Quantum Realm

- **Axian Bartender**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmAxianBartender.png).
- **Axiam Maitre D'**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 4; VP 2; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmAxianMaitreD.png).
- **Lord Krylar's Valet**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 5; VP 3; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmLordKrylarsValet.png).
- **Sky Manta**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 5; VP 3; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmSkyManta.png).
- **Lord Krylar's Appetizer**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 6*; VP 3; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmLordKrylarsAppetizer.png).
- **Hungering Energy**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 7*; VP 4; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmHungeringEnergy.png).
- **Quantumoeba**; type/group: Villain / Quantum Realm; copies: 1; printed values: Attack 8*; VP 5; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmQuantumoeba.png).
- **Quantumania**; type/group: Scheme / Quantum Realm; copies: 1; printed values: VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/QuantumRealmAmbushSchemeQuantumania.png).

### Henchman Group: Quantumnauts

- **Quantumnauts**; type/group: Henchman / Quantumnauts; copies: Unverified; printed values: not indexed in C1; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/Quantumnauts.png).

### Henchman Group: Quantum Hound

- **Quantum Hound**; type/group: Henchman / Quantum Hound; copies: Unverified; printed values: not indexed in C1; keyword labels: Explore; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/QuantumHound.png).

### Henchman Group: Tardigrade

- **Tardigrade (Covert)**; type/group: Henchman / Tardigrade; copies: Unverified; printed values: not indexed in C1; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/TardigradeCovert.png).
- **Tardigrade (Instinct)**; type/group: Henchman / Tardigrade; copies: Unverified; printed values: not indexed in C1; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/TardigradeInstinct.png).
- **Tardigrade (Ranged)**; type/group: Henchman / Tardigrade; copies: Unverified; printed values: not indexed in C1; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/TardigradeRanged.png).
- **Tardigrade (Strength)**; type/group: Henchman / Tardigrade; copies: Unverified; printed values: not indexed in C1; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/TardigradeStrength.png).
- **Tardigrade (Tech)**; type/group: Henchman / Tardigrade; copies: Unverified; printed values: not indexed in C1; keyword labels: Microscopic Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/TardigradeTech.png).

### Mastermind: Darren Cross

- **Darren Cross**; type/group: Normal Mastermind face / Darren Cross; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Conqueror, Double-Cross, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/DarrenCross.png).
- **Yellowjacket**; type/group: Normal Mastermind face (transformed face) / Darren Cross; copies: Unverified; printed values: Attack 12*; VP 6; keyword labels: Microscopic Size-Changing, Size-Changing, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/DarrenCross_Transformed.png).
- **Corporate Raider**; type/group: Mastermind Tactic / Darren Cross; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/DarrenCrossTactic1.png).
- **Protect My Investments**; type/group: Mastermind Tactic / Darren Cross; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/DarrenCrossTactic2.png).
- **Shrinking Research Budget**; type/group: Mastermind Tactic / Darren Cross; copies: Unverified; printed values: not indexed in C1; keyword labels: Size-Changing, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/DarrenCrossTactic3.png).
- **Steal Pym Particles**; type/group: Mastermind Tactic / Darren Cross; copies: Unverified; printed values: not indexed in C1; keyword labels: Size-Changing, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/DarrenCrossTactic4.png).

### Mastermind: Ghost, Master Thief

- **Ghost, Master Thief**; type/group: Normal Mastermind face / Ghost, Master Thief; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/GhostMasterThief.png).
- **Ghost, Intangible**; type/group: Normal Mastermind face (transformed face) / Ghost, Master Thief; copies: Unverified; printed values: Attack 6*; VP 6; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/GhostMasterThief_Transformed.png).
- **Elaborate Rescue Plan**; type/group: Mastermind Tactic / Ghost, Master Thief; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/GhostMasterThiefTactic2.png).
- **Nightmarish Wraith**; type/group: Mastermind Tactic / Ghost, Master Thief; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/GhostMasterThiefTactic3.png).
- **Shadowy Abduction**; type/group: Mastermind Tactic / Ghost, Master Thief; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/GhostMasterThiefTactic4.png).
- **Draining Quantum Energy Chamber**; type/group: Mastermind Tactic / Ghost, Master Thief; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/GhostMasterThiefTactic1.png).

### Mastermind: Kang, Quantum Conqueror

- **Kang, Quantum Conqueror**; type/group: Normal Mastermind face / Kang, Quantum Conqueror; copies: Unverified; printed values: Attack 11+; VP 7; keyword labels: Conqueror, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KangQuantumConqueror.png).
- **Kang, Multiverse Conqueror**; type/group: Normal Mastermind face (transformed face) / Kang, Quantum Conqueror; copies: Unverified; printed values: Attack 10+; VP 7; keyword labels: Conqueror, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KangQuantumConqueror_Transformed.png).
- **Conqueror's Wrath**; type/group: Mastermind Tactic / Kang, Quantum Conqueror; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Kang%203.png).
- **Kang's Defiance**; type/group: Mastermind Tactic / Kang, Quantum Conqueror; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KangQuantumConquerorTactic2.png).
- **Multiversal Engine Core**; type/group: Mastermind Tactic / Kang, Quantum Conqueror; copies: Unverified; printed values: not indexed in C1; keyword labels: Double-Cross, Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KangQuantumConquerorTactic3.png).
- **The Time Sphere**; type/group: Mastermind Tactic / Kang, Quantum Conqueror; copies: Unverified; printed values: not indexed in C1; keyword labels: Transform; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KangQuantumConquerorTactic4.png).

### Scheme: Auction Shrink Tech to Highest Bidder

- **Auction Shrink Tech to Highest Bidder**; type/group: Scheme / Auction Shrink Tech to Highest Bidder; copies: Unverified; printed values: not indexed in C1; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Auction%20Shrink%20Tech%20To%20Highest%20Bidder.png).

### Scheme: Safeguard Dark Secrets

- **Safeguard Dark Secrets**; type/group: Scheme / Safeguard Dark Secrets; copies: Unverified; printed values: not indexed in C1; keyword labels: Heist; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Safeguard%20Dark%20Secrets.png).

### Scheme: Escape an Imprisoning Dimension

- **Escape an Imprisoning Dimension**; type/group: Scheme / Escape an Imprisoning Dimension; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Escape%20An%20Imprisoning%20Dimension.png).

### Scheme: Siphon Energy from the Quantum Realm

- **Siphon Energy from the Quantum Realm**; type/group: Scheme / Siphon Energy from the Quantum Realm; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Siphon%20Energy%20From%20The%20Quantum%20Realm.png).

### Bystander set: Agent Jimmy Woo

- **Agent Jimmy Woo**; type/group: Bystander / Agent Jimmy Woo; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/Agent%20Jimmy%20Woo.png).

### Bystander set: Maggie Lang

- **Maggie Lang**; type/group: Bystander / Maggie Lang; copies: 2; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/Maggie%20Lang.png).

### Bystander set: Officer Jim Paxton

- **Officer Jim Paxton**; type/group: Bystander / Officer Jim Paxton; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/Officer%20Jim%20Paxton.png).

### Bystander set: Young Cassie Lang

- **Young Cassie Lang**; type/group: Bystander / Young Cassie Lang; copies: 2; printed values: not indexed in C1; keyword labels: Size-Changing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/Young%20Cassie%20Lang.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
