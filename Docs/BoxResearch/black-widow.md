# Black Widow (2022/23)

**Research status: Partial.** The official insert verifies contents and several mechanics, but not card-level Scheme setups, Always Leads, or complete Hero metadata.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| BW | [Upper Deck Black Widow rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2022_BlackWidow_Rulesheet.pdf) | Contents, Undercover, Unleash, When Recruited, Dodge, Dark Memories, and Divided Cards (PDF pp.1–2). |
| C1 | [master-strike structured black-widow card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/blackwidow.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (BW p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Black Widow; Yelena Belova; Red Guardian; White Tiger; Falcon & Winter Soldier.
- **Villain Groups (two):** Taskmaster's Thunderbolts; Elite Assassins.
- **Masterminds (two):** Taskmaster; Indestructible Man.
- **Schemes (four):** Corrupt the Spy Agencies; Train Black Widows in the Red Room; Sniper Rifle Assassins; Frame Heroes for Murder.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, individual Scheme/Mastermind setup lines, or printed Divided-card values.

## Rules and mechanisms

- **Undercover (BW p.1):** Put a Hero from hand into the Victory Pile, where it is worth one point. If the played card sends itself Undercover, its other play effects still resolve.
- **Unleash (BW p.1):** Return a Hero from the Victory Pile to hand. It may be played that turn, and one qualifying event can unleash any number of cards waiting for that event.
- **When Recruited: Send This Undercover (BW p.1):** After recruiting the Hero and refilling the HQ, put it in the Victory Pile for one point. If multiple recruitment effects would send it to different destinations, choose one.
- **Dodge (BW p.2):** During your turn, discard the card from hand to draw another. Ignore its other text unless it specifically applies to Dodge; the card was not played and does not count for Hero Classes/colors or Superpower abilities.
- **Dark Memories (BW p.2):** A Hero or Villain gains +1 for each distinct Hero Class represented in the player's discard pile; multiple cards of one class count once, and grey cards do not count. Reshuffling the discard pile resets the bonus.
- **Divided Cards (BW p.2):** Falcon & Winter Soldier share one physical card. In hand, deck, or HQ it counts as one card with both sides' names, Hero Names, teams, and classes. In play, choose one side and use only its abilities; Dodge is available if either side has it.

## Required parts and glossary

The insert names no new shared stack or token. Undercover uses the existing Victory Pile; Unleash returns a Hero from there (BW p.1).

- **Undercover:** Move a Hero from hand to the Victory Pile for one point. (BW p.1)
- **Unleash:** Return an Undercover Hero from the Victory Pile to hand. (BW p.1)
- **Dodge:** Replace a card in hand by discarding it and drawing a card, without playing it. (BW p.2)
- **Dark Memories:** Gain strength based on distinct Hero Classes in the discard pile. (BW p.2)
- **Divided Card:** One card with two selectable sides and shared identifiers while not played. (BW p.2)

Summaries are original paraphrases under 40 words. Hero teams/classes/shared Hero Names, Always Leads, printed Divided-card values, and card-specific dependencies need card-level verification.

## Setup and implementation gaps

Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps, plus both Masterminds' Always Leads/setup effects and Hero metadata. Integration must handle Heroes moving between the Victory Pile and hand and the destination choice for conflicting When Recruited effects. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Black Widow

- **Evasive Acrobatics**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackWidowShield_2Common.png).
- **Widow's Bite**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 4; Attack 1+; keyword labels: Dodge, Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackWidowShield_3Common.png).
- **Weave a Web of Spies**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 5; Attack 2; keyword labels: Dodge, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackWidowShield_4Uncommon.png).
- **Infiltrate the Conspiracy**; type/group: Hero / Black Widow; copies: Unverified; Hero Name: Black Widow; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 7; Attack 4+; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BlackWidowShield_1Rare.png).

### Hero group: Yelena Belova

- **Strike and Fade**; type/group: Hero / Yelena Belova; copies: Unverified; Hero Name: Yelena Belova; team: S.H.I.E.L.D.; class icons: Covert; printed values: Cost 2; Attack 3; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/YelenaBelova_2Common.png).
- **Unveil Identity**; type/group: Hero / Yelena Belova; copies: Unverified; Hero Name: Yelena Belova; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/YelenaBelova_3Common.png).
- **Twilight Ops**; type/group: Hero / Yelena Belova; copies: Unverified; Hero Name: Yelena Belova; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 6; Attack 3; keyword labels: Dodge, Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/YelenaBelova_4Uncommon.png).
- **Destroy the Red Room**; type/group: Hero / Yelena Belova; copies: Unverified; Hero Name: Yelena Belova; team: S.H.I.E.L.D.; class icons: Tech; printed values: Cost 8; Attack 4; keyword labels: Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/YelenaBelova_1Rare.png).

### Hero group: Red Guardian

- **Sleeper Agent**; type/group: Hero / Red Guardian; copies: Unverified; Hero Name: Red Guardian; team: Unaffiliated; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: “When Recruited“ Abilities, When Recruited: Send This Undercover, Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/RedGuardian_2Common.png).
- **Magnetic Shield**; type/group: Hero / Red Guardian; copies: Unverified; Hero Name: Red Guardian; team: Unaffiliated; class icons: Covert; printed values: Cost 4; Attack 2+; keyword labels: “When Recruited“ Abilities, When Recruited: Send This Undercover, Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/RedGuardian_3Common.png).
- **Death Was Only a Ruse**; type/group: Hero / Red Guardian; copies: Unverified; Hero Name: Red Guardian; team: Unaffiliated; class icons: Strength; printed values: Cost 6; Attack 3; keyword labels: “When Recruited“ Abilities, When Recruited: Send This Undercover, Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/RedGuardian_4Uncommon.png).
- **Champion of the Winter Guard**; type/group: Hero / Red Guardian; copies: Unverified; Hero Name: Red Guardian; team: Unaffiliated; class icons: Covert; printed values: Cost 8; Attack 4+; keyword labels: “When Recruited“ Abilities, When Recruited: Send This Undercover, Unleash, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/RedGuardian_1Rare.png).

### Hero group: White Tiger

- **Amulets of the Tiger God**; type/group: Hero / White Tiger; copies: Unverified; Hero Name: White Tiger; team: Marvel Knights; class icons: Strength; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteTiger_3Common.png).
- **Dark Influence of the Hand**; type/group: Hero / White Tiger; copies: Unverified; Hero Name: White Tiger; team: Marvel Knights; class icons: Ranged; printed values: Cost 3; Attack 0+; keyword labels: Dodge, Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteTiger_2Common.png).
- **Camouflaged Huntress**; type/group: Hero / White Tiger; copies: Unverified; Hero Name: White Tiger; team: Marvel Knights; class icons: Covert; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteTiger_4Uncommon.png).
- **Shadowed Resurrection**; type/group: Hero / White Tiger; copies: Unverified; Hero Name: White Tiger; team: Marvel Knights; class icons: Instinct; printed values: Cost 8; Attack 3; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WhiteTiger_1Rare.png).

### Hero group: Falcon & Winter Soldier

- **Attune**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FalconandWinterSoldier_2Common.png).
- **Atone**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 0+; keyword labels: Dark Memories; card image: unavailable in C1.
- **Relocate**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FalconandWinterSoldier_3Common.png).
- **Reload**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Tech; printed values: Cost 4; Attack 2; card image: unavailable in C1.
- **New Wings**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Tech; printed values: Cost 5; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FalconandWinterSoldier_4Uncommon.png).
- **New Plan**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Covert; printed values: Cost 5; card image: unavailable in C1.
- **Captain America's Legacy**; type/group: Hero / Falcon & Winter Soldier; copies: Unverified; Hero Name: Falcon & Winter Soldier; team: Avengers; class icons: Strength; printed values: Cost 7; Attack 2+; keyword labels: Dark Memories; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/FalconandWinterSoldier_1Rare.png).

### Villain Group: Taskmaster's Thunderbolts

- **Jester**; type/group: Villain / Taskmaster's Thunderbolts; copies: 2; printed values: Attack 2; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/TaskmastersThunderboltsJester.png).
- **Joystick**; type/group: Villain / Taskmaster's Thunderbolts; copies: 2; printed values: Attack 3; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/TaskmastersThunderboltsJoystick.png).
- **Jack O'Lantern**; type/group: Villain / Taskmaster's Thunderbolts; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/TaskmastersThunderboltsJackOLantern.png).
- **Bullseye**; type/group: Villain / Taskmaster's Thunderbolts; copies: 2; printed values: Attack 4+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/TaskmastersThunderboltsBullseye.png).

### Villain Group: Elite Assassins

- **Blue Talon**; type/group: Villain / Elite Assassins; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Dark Memories, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EliteAssassinsBlueTalon.png).
- **Iron Maiden**; type/group: Villain / Elite Assassins; copies: 2; printed values: Attack 4+; VP 2; keyword labels: Dark Memories, Undercover, Unleash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EliteAssassinsIronMaiden.png).
- **Snapdragon**; type/group: Villain / Elite Assassins; copies: 2; printed values: Attack 4; VP 2; keyword labels: Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EliteAssassinsSnapdragon.png).
- **Black Lotus**; type/group: Villain / Elite Assassins; copies: 2; printed values: Attack 6; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/EliteAssassinsBlackLotus.png).

### Mastermind: Taskmaster

- **Taskmaster**; type/group: Normal Mastermind face / Taskmaster; copies: Unverified; printed values: Attack 5+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Taskmaster.png).
- **Epic Taskmaster**; type/group: Epic Mastermind face / Taskmaster; copies: Unverified; printed values: Attack 5+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Taskmaster_Epic.png).
- **S.H.I.E.L.D. Initiative Trainer**; type/group: Mastermind Tactic / Taskmaster; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/TaskmasterTactic3.png).
- **Photographic Reflexes**; type/group: Mastermind Tactic / Taskmaster; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/TaskmasterTactic2.png).
- **Teacher and Assassin**; type/group: Mastermind Tactic / Taskmaster; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/TaskmasterTactic4.png).
- **Henchman Instructor**; type/group: Mastermind Tactic / Taskmaster; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/TaskmasterTactic1.png).

### Mastermind: Indestructible Man

- **Indestructible Man**; type/group: Normal Mastermind face / Indestructible Man; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/IndestructibleMan.png).
- **Epic Indestructible Man**; type/group: Epic Mastermind face / Indestructible Man; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/IndestructibleMan_Epic.png).
- **Manipulate Murderous Mad Monk**; type/group: Mastermind Tactic / Indestructible Man; copies: Unverified; printed values: Attack 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/IndestructibleManTactic2.png).
- **Secrets of Indestructibility**; type/group: Mastermind Tactic / Indestructible Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/IndestructibleManTactic3.png).
- **International Arms Dealer**; type/group: Mastermind Tactic / Indestructible Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/IndestructibleManTactic1.png).
- **Unveil Project Four**; type/group: Mastermind Tactic / Indestructible Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/IndestructibleManTactic4.png).

### Scheme: Corrupt the Spy Agencies

- **Corrupt the Spy Agencies**; type/group: Scheme / Corrupt the Spy Agencies; copies: Unverified; printed values: not indexed in C1; keyword labels: Undercover, Unleash; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/CorruptTheSpyAgencies.png).

### Scheme: Train Black Widows in the Red Room

- **Train Black Widows in the Red Room**; type/group: Scheme / Train Black Widows in the Red Room; copies: Unverified; printed values: not indexed in C1; keyword labels: Dark Memories, Undercover; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/TrainBlackWidowsintheRedRoom.png).

### Scheme: Sniper Rifle Assassins

- **Sniper Rifle Assassins**; type/group: Scheme / Sniper Rifle Assassins; copies: Unverified; printed values: not indexed in C1; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/SniperRifleAssassins.png).

### Scheme: Frame Heroes for Murder

- **Frame Heroes for Murder**; type/group: Scheme / Frame Heroes for Murder; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/FrameHeroesforMurder.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
