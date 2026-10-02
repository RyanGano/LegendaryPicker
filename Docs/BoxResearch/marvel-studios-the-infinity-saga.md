# Marvel Studios' The Infinity Saga (Mar 2023)

**Research status: Partial.** The official insert verifies component totals and several mechanics, but card-level setup text and complete card metadata remain unverified.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured marvel-studios-the-infinity-saga card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/msis.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| IS | [Upper Deck Infinity Saga rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/MCU_InfinitySaga_Rulesheet.pdf) | Hero card ratios, Endgame, Sacrifice, Phasing, Multiclass, Divided Cards, Thanos/Infinity Stones relationship, contents (PDF pp.1–2). |
| C2 | [nutki/legendary Infinity Saga name catalog](https://github.com/nutki/legendary/tree/master/texttools/Marvel%20Studios%20The%20Infinity%20Saga) | Card and group names/membership only; not mechanics, component use, or setup values. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (IS p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

Each Hero has 1 rare, 2 copies of each of two uncommons, and 3 copies of each of three commons (14 cards per Hero). The listed categories sum to 100 (IS p.2).

### Group inventory (C2 + C1 face index)

- **Heroes (five groups):** Wanda & Vision; Black Panther; Doctor Strange; Bruce Banner; Captain Marvel.
- **Villain Groups (two):** Children of Thanos; Infinity Stones.
- **Masterminds (two sets):** Ebony Maw; Thanos (C2 also labels the alternate side “Epic Thanos”).
- **Schemes (four):** Halve All Life In The Universe; Sacrifice For The Soul Stone; The Time Heist; Warp Reality Into a TV Show.

C1 structured face metadata is indexed below; C2 remains a names/mappings source and neither catalog's ability prose is rules evidence. The insert confirms that Wanda & Vision are one 14-card Hero stack and that both Masterminds are double-sided Epic Masterminds (IS p.2). Remaining setup gaps include the full card-level Hero Name/team/class values, Scheme setup details, printed Always Leads/setup text, or additional card-driven components.

## Rules and mechanisms

- **Endgame (IS p.1):** Active when the Villain Deck has at most eight cards per player. Enemies with Endgame abilities gain those abilities then. With multiple Villain Decks, any deck meeting the threshold activates Endgame; adding cards back may end it. Certain effects temporarily activate Endgame for all Enemies and Heroes, or for Heroes only.
- **Sacrifice (IS pp.1–2):** When playing a card with Sacrifice, its ability may be used only if another Hero of the required Class was played earlier that turn; using it KOs the card and grants another turn without playing a Villain Deck card at that turn's start. The choice is optional and made when the card is played. Other card effects still resolve, but the KO'd card is no longer a Hero you have.
- **Phasing (IS p.2):** During the player's turn, a Phasing card in hand may be swapped with the top card of their deck. This is not playing, drawing, or putting a card on top of the deck.
- **Multiclass (IS p.2):** A card with multiple Hero Classes counts as each printed Class. The insert notes that Black Panther's cards in this set are all Multiclass; exact class combinations require card-level evidence.
- **Divided Cards (IS p.2):** Wanda and Vision share a single 14-card Hero stack. On play, choose one side and use only that side's effects; the cost is paid once even if both sides show it. Outside play, the card counts as both sides' classes, teams, names, and Hero Names while still being one card.
- **Infinity Stones and Thanos (IS p.2):** The Infinity Stones Villain Group represents the Mastermind pursuing the Stones. For solo play, Thanos may use another Villain Group; his abilities referring to Infinity Stones then apply to that group.

## Required parts and glossary

The insert introduces no separately listed shared token or stack. Sacrifice uses the KO pile; Phasing swaps a card with the top of the player's deck (IS pp.1–2). The Infinity Stones are a Villain Group, not a separate component (IS p.2).

- **Endgame:** The state reached when the Villain Deck is down to eight cards per player. (IS p.1)
- **Sacrifice:** Optionally KO a played Hero to use its special effect when its Class condition is met. (IS pp.1–2)
- **Phasing:** Swap a card in hand with the deck's top card during your turn. (IS p.2)
- **Multiclass:** A card that counts as each of its printed Hero Classes. (IS p.2)
- **Divided Card:** One physical card with two selectable sides and shared identifiers while not in play. (IS p.2)

Summaries are original paraphrases under 40 words. Verify any additional shared parts from the cards, including any card-driven Wounds or other stacks, against allowed card-level evidence.

## Setup and implementation gaps

Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks/parts, and setup steps; both Masterminds' printed Always Leads and setup effects; all Hero metadata and card-level dependencies. The insert establishes Thanos's connection to Infinity Stones but not all printed setup text. Runtime support for Endgame, Sacrifice, Phasing, Multiclass, and Divided Cards is an implementation concern; research does not change runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Doctor Strange

- **Open Portals**; type/group: Hero / Doctor Strange; copies: 3; Hero Name: Doctor Strange; team: Avengers; class icons: Ranged; printed values: Cost 2; Recruit 1+; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeIS_2Common.png).
- **Defend this Dimension**; type/group: Hero / Doctor Strange; copies: 3; Hero Name: Doctor Strange; team: Avengers; class icons: Instinct; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeIS_3Common.png).
- **Sift Futures**; type/group: Hero / Doctor Strange; copies: 3; Hero Name: Doctor Strange; team: Avengers; class icons: Instinct, Ranged; printed values: Cost 4; Attack 2; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeIS_4Common.png).
- **Invoke the Time Stone**; type/group: Hero / Doctor Strange; copies: 2; Hero Name: Doctor Strange; team: Avengers; class icons: Ranged; printed values: Cost 5; Recruit 2; keyword labels: Phasing, Sacrifice; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeIS_5Uncommon.png).
- **Bind Evil**; type/group: Hero / Doctor Strange; copies: 2; Hero Name: Doctor Strange; team: Avengers; class icons: Instinct; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeIS_6Uncommon.png).
- **1 in 14,000,065**; type/group: Hero / Doctor Strange; copies: 1; Hero Name: Doctor Strange; team: Avengers; class icons: Instinct; printed values: Cost 7; Attack 5; keyword labels: Sacrifice; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeIS_1Rare.png).

### Hero group: Black Panther

- **Avengers Reassembled**; type/group: Hero / Black Panther; copies: 3; Hero Name: Black Panther; team: Avengers; class icons: Instinct, Covert; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackPantherIS_3Common.png).
- **Great Many Lives Lost**; type/group: Hero / Black Panther; copies: 3; Hero Name: Black Panther; team: Avengers; class icons: Strength, Instinct; printed values: Cost 4; Recruit 2; keyword labels: Sacrifice; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackPantherIS_4Common.png).
- **Wakanda Forever**; type/group: Hero / Black Panther; copies: 3; Hero Name: Black Panther; team: Avengers; class icons: Covert, Tech; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackPantherIS_2Common.png).
- **Vibranium Nanites**; type/group: Hero / Black Panther; copies: 2; Hero Name: Black Panther; team: Avengers; class icons: Tech, Ranged; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackPantherIS_6Uncommon.png).
- **Council of War**; type/group: Hero / Black Panther; copies: 2; Hero Name: Black Panther; team: Avengers; class icons: Strength, Covert; printed values: Cost 5; Recruit 3+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackPantherIS_5Uncommon.png).
- **Fateful Return**; type/group: Hero / Black Panther; copies: 1; Hero Name: Black Panther; team: Avengers; class icons: Instinct, Tech; printed values: Cost 8; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackPantherIS_1Rare.png).

### Hero group: Bruce Banner

- **Burst of Rage**; type/group: Hero / Bruce Banner; copies: 3; Hero Name: Bruce Banner; team: Avengers; class icons: Strength; printed values: Cost 1; Attack 1+; keyword labels: Sacrifice; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BruceBannerIS_2Common.png).
- **Hulkbuster Armor**; type/group: Hero / Bruce Banner; copies: 3; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BruceBannerIS_4Common.png).
- **Brains and Brawn**; type/group: Hero / Bruce Banner; copies: 3; Hero Name: Bruce Banner; team: Avengers; class icons: Strength, Tech; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BruceBannerIS_3Common.png).
- **Hulk Gets Smashed**; type/group: Hero / Bruce Banner; copies: 2; Hero Name: Bruce Banner; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BruceBannerIS_5Uncommon.png).
- **Crush Puny Weaklings**; type/group: Hero / Bruce Banner; copies: 2; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BruceBannerIS_6Uncommon.png).
- **Reverse the Snap**; type/group: Hero / Bruce Banner; copies: 1; Hero Name: Bruce Banner; team: Avengers; class icons: Tech; printed values: Cost 7; Attack 5; keyword labels: Sacrifice; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BruceBannerIS_1Rare.png).

### Hero group: Captain Marvel

- **Return from the Stars**; type/group: Hero / Captain Marvel; copies: 3; Hero Name: Captain Marvel; team: Avengers; class icons: Strength; printed values: Cost 4; Recruit 2+; Attack 0+; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainMarvelIS_3Common.png).
- **Turning Point**; type/group: Hero / Captain Marvel; copies: 3; Hero Name: Captain Marvel; team: Avengers; class icons: Strength, Ranged; printed values: Cost 5; Attack 2+; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainMarvelIS_4Common.png).
- **Infused by the Tesseract**; type/group: Hero / Captain Marvel; copies: 3; Hero Name: Captain Marvel; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 1; Attack 0+; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainMarvelIS_2Common.png).
- **Moment of Destiny**; type/group: Hero / Captain Marvel; copies: 2; Hero Name: Captain Marvel; team: Avengers; class icons: Strength; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainMarvelIS_6Uncommon.png).
- **Dawning Hope**; type/group: Hero / Captain Marvel; copies: 2; Hero Name: Captain Marvel; team: Avengers; class icons: Ranged; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainMarvelIS_5Uncommon.png).
- **Time to End It**; type/group: Hero / Captain Marvel; copies: 1; Hero Name: Captain Marvel; team: Avengers; class icons: Ranged; printed values: Cost 8; Attack 5+; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainMarvelIS_1Rare.png).

### Hero group: Wanda & Vision

- **We Have to Destroy It**; type/group: Hero / Wanda & Vision; copies: 3; Hero Name: Wanda & Vision; team: Avengers; class icons: Tech; printed values: Cost 1; Recruit 1+; keyword labels: Phasing, Sacrifice; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WandaAndVision_2Common.png).
- **Witchcraft**; type/group: Hero / Wanda & Vision; copies: 3; Hero Name: Wanda & Vision; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WandaAndVision_3Common.png).
- **Hold On**; type/group: Hero / Wanda & Vision; copies: 3; Hero Name: Wanda & Vision; team: Avengers; class icons: Covert; printed values: Cost 3; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WandaAndVision_4Common.png).
- **Let Go**; type/group: Hero / Wanda & Vision; copies: 3; Hero Name: Wanda & Vision; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 2; keyword labels: Phasing, Sacrifice; card image: unavailable in C1.
- **Magic**; type/group: Hero / Wanda & Vision; copies: 2; Hero Name: Wanda & Vision; team: Avengers; class icons: Ranged; printed values: Cost 5; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WandaAndVision_6Uncommon.png).
- **Science**; type/group: Hero / Wanda & Vision; copies: 2; Hero Name: Wanda & Vision; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 2+; keyword labels: Phasing; card image: unavailable in C1.
- **Rage**; type/group: Hero / Wanda & Vision; copies: 2; Hero Name: Wanda & Vision; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WandaAndVision_5Uncommon.png).
- **Grief**; type/group: Hero / Wanda & Vision; copies: 2; Hero Name: Wanda & Vision; team: Avengers; class icons: Tech; printed values: Cost 5; Recruit 2+; keyword labels: Phasing; card image: unavailable in C1.
- **Odd Couple**; type/group: Hero / Wanda & Vision; copies: 1; Hero Name: Wanda & Vision; team: Avengers; class icons: Covert, Ranged; printed values: Cost 7; Attack 4; keyword labels: Phasing; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WandaAndVision_1Rare.png).

### Villain Group: Children of Thanos

- **Endless Armies of Chitauri**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosEndlessArmiesOfChitauri.png).
- **Outriders**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosOutriders.png).
- **Outrider Dropships**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosOutriderDropships.png).
- **Outrider Threshers**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosOutriderThreshers.png).
- **Cull Obsidian**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 6+; VP 5; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosCullObsidian.png).
- **Corvus Glaive**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 6+; VP 5; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosCorvusGlaive.png).
- **Chitauri Gorilla**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosChitauriGorilla.png).
- **Proxima Midnight**; type/group: Villain / Children of Thanos; copies: 1; printed values: Attack 7+; VP 5; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ChildrenOfThanosProximaMidnight.png).

### Villain Group: Infinity Stones

- **The Power Stone**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 6+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesThePowerStone.png).
- **The Space Stone**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 5+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesTheSpaceStone.png).
- **The Reality Stone**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 5+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesTheRealityStone.png).
- **The Time Stone**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesTheTimeStone.png).
- **The Soul Stone**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 5+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesTheSoulStone.png).
- **The Mind Stone**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 1+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesTheMindStone.png).
- **Nebula, Stone Seeker**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesNebulaStoneSeeker.png).
- **Stonekeeper**; type/group: Villain / Infinity Stones; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/InfinityStonesStonekeeper.png).

### Mastermind: Thanos

- **Thanos**; type/group: Normal Mastermind face / Thanos; copies: Unverified; printed values: Attack 11+; VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ThanosIS.png).
- **Epic Thanos**; type/group: Epic Mastermind face / Thanos; copies: Unverified; printed values: Attack 13+; VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ThanosIS_Epic.png).
- **The Snap**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ThanosISTactic3.png).
- **Destiny Arrives All the Same**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ThanosISTactic1.png).
- **Price to Pay**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ThanosISTactic2.png).
- **You Should Have Gone for the Head**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ThanosISTactic4.png).

### Mastermind: Ebony Maw

- **Ebony Maw**; type/group: Normal Mastermind face / Ebony Maw; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/EbonyMaw.png).
- **Epic Ebony Maw**; type/group: Epic Mastermind face / Ebony Maw; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/EbonyMaw_Epic.png).
- **Your Powers are Quaint**; type/group: Mastermind Tactic / Ebony Maw; copies: Unverified; printed values: not indexed in C1; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/EbonyMawTactic4.png).
- **Hear Me and Rejoice**; type/group: Mastermind Tactic / Ebony Maw; copies: Unverified; printed values: not indexed in C1; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/EbonyMawTactic1.png).
- **Smile...Even in Death**; type/group: Mastermind Tactic / Ebony Maw; copies: Unverified; printed values: not indexed in C1; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/EbonyMawTactic2.png).
- **You May Think This is Suffering**; type/group: Mastermind Tactic / Ebony Maw; copies: Unverified; printed values: not indexed in C1; keyword labels: Endgame; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/EbonyMawTactic3.png).

### Scheme: Sacrifice for the Soul Stone

- **Sacrifice for the Soul Stone**; type/group: Scheme / Sacrifice for the Soul Stone; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/SacrificefortheSoulStone.png).

### Scheme: Halve All Life in the Universe

- **Halve All Life in the Universe**; type/group: Scheme / Halve All Life in the Universe; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/HalveAllLifeintheUniverse.png).

### Scheme: Warp Reality into a TV Show

- **Warp Reality into a TV Show**; type/group: Scheme / Warp Reality into a TV Show; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/WarpRealityIntoaTVShow.png).

### Scheme: The Time Heist

- **The Time Heist**; type/group: Scheme / The Time Heist; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/TheTimeHeist.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
