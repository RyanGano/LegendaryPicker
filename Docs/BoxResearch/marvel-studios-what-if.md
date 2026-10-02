# Marvel Studios' What If...? (late 2023)

**Research status: Partial.** The official product listing and rulebook verify the 350-card inventory, base-game setup, solo mode, and many rules. Three Scheme setups, two Masterminds' full card text, and complete per-Hero metadata remain unverified.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured marvel-studios-what-if card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/mswi.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| WI | [Upper Deck What If...? rulebook](https://www.boardgame-news.com/rules/Legendary_WhatIf_Rulebook.pdf) (official 2023 UDC rulebook, third-party mirror) | Card contents (printed p.26), setup (p.27), solo rules (p.23–24), classes/teams (p.25), keywords and clarifications (pp.16–22), and pictured Hank Pym, Zombie Scarlet Witch, and Collect an Interstellar Zoo cards (printed pp.5, 6, 14). |
| UD | [Upper Deck What If...? product listing](https://upperdeckstore.com/legendary-what-if.html) | Product name, 350-card count, and 1–5 player range. |
| C2 | [nutki/legendary What If...? name catalog](https://github.com/nutki/legendary/tree/master/texttools/Marvel%20Studios%20What%20If) | Card and group names/membership only; not mechanics, component counts, or setup values. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Late-2023 position, base-game status, and Revised ruleset classification. |

## Catalog inventory

### Official contents (WI printed p.26)

The rulebook lists a rulebook, playmat, dividers, and **350 game cards**:

| Type | Official count |
|---|---:|
| Heroes | 8 groups × 14 = 112 |
| Villain Groups | 5 groups × 8 = 40 |
| Henchman Groups | 3 groups × 10 = 30 |
| Double-Sided Epic Masterminds | 4 sets: 4 Mastermind cards + 16 Tactics = 20 |
| S.H.I.E.L.D. Agents | 40 |
| S.H.I.E.L.D. Troopers | 20 |
| S.H.I.E.L.D. Officers | 8 |
| Bystanders | 30: 25 normal, plus 1 each of five named Special Bystanders |
| Wounds | 30 |
| Schemes | 4 |
| Scheme Twists | 11 |
| Master Strikes | 5 |
| **Total** | **350** |

Each Hero has 1 rare, 2 copies of each of two uncommons, and 3 copies of each of three commons (WI p.26).

### Group inventory (C2 + C1 face index)

- **Heroes (eight):** Apocalyptic Black Widow; Captain Carter; Doctor Strange Supreme; Gamora, Destroyer of Thanos; Killmonger, Spec Ops; Party Thor; Star-Lord T'Challa; Uatu, The Watcher.
- **Villain Groups (five):** Black Order Guards; Intergalactic Party Animals; Rival Overlords; Strange's Demons; Zombie Avengers.
- **Henchman Groups (three):** Giants of Jotunheim; Ultron Sentries; Vibranium Liberator Drones.
- **Masterminds (four):** Hank Pym, Yellowjacket; Killmonger, the Betrayer; Ultron Infinity; Zombie Scarlet Witch.
- **Schemes (four):** Breach the Nexus of All Realities; Collect an Interstellar Zoo; Marvel Zombies; Trash Earth with Hugest Party Ever.
- **Special Bystanders (WI p.26):** Scott Lang's Head; Happy Hogan; Howard the Duck; Howard Stark; Pepper Potts.
- **Other named card types:** S.H.I.E.L.D. Agent; S.H.I.E.L.D. Trooper; S.H.I.E.L.D. Officer; Bystander; Wound; Scheme Twist; Master Strike; Mastermind Tactic.

The C1 face index below records available metadata and linked images; the rulebook glossary names the Guardians of the Multiverse and S.H.I.E.L.D. teams and the Strength, Instinct, Covert, Tech, Ranged, and Basic Hero classes (WI p.25). Fields absent from C1, complete setup effects, and component mappings remain unresolved.

## Setup and base-game rules

The product is listed for 1–5 players. Each player uses a starting deck of 8 S.H.I.E.L.D. Agents and 4 S.H.I.E.L.D. Troopers, then draws 6 cards (WI p.27). Set out the Officer, Bystander, and Wound decks; choose a Mastermind with its four Tactics and a Scheme; add five Master Strikes and the Scheme's specified number of Twists. Include the Mastermind's Always Leads group, then choose remaining groups at random (WI pp.6–8, 27).

| Players | Villain Groups | Henchman Groups | Bystanders | Hero groups |
|---:|---:|---:|---:|---:|
| 1 | 1 | 1* | 1 | 3 |
| 2 | 2 | 1 | 2 | 5 |
| 3 | 3 | 1 | 8 | 5 |
| 4 | 4 | 2 | 8 | 5 |
| 5 | 5 | 2 | 16 | 6 |

For 1 player, the Henchman group is the special solo setup: two cards go in the Villain Deck, two more enter the city before the first turn, and the other six are unused. In 4- and 5-player games, each player's first turn is a warmup without a Villain Deck draw (WI p.27). The usual setup uses all 8 Villain cards per selected group and all 10 cards per selected Henchman group (WI pp.6–7).

**Solo mode (WI pp.23–24):** Use 3 random Heroes (42 cards), 1 Villain Group, 2 Henchmen from one random group (with 2 more entering the city before the first turn), 1 Bystander, 5 Master Strikes, and the Scheme's normal number of Twists. Ignore the Mastermind's Always Leads for group selection; when its special abilities refer to its usual group, apply them to the group actually used. After each Twist effect, put one HQ Hero costing 6 or less on the bottom of the Hero Deck; multiple Twists in one turn still move only one Hero. There is no extra effect after a Master Strike. “Each other player” effects from Villains, Masterminds, and Tactics affect the solo player, but Hero-card effects are unchanged. This Revised solo mode replaces solo rules from the First Edition core, Dark City, Villains, and Phase 1 (WI p.23).

The rulebook's pictured **Hank Pym, Yellowjacket** card says it can Always Lead any Villain Group (WI p.5). The pictured **Zombie Scarlet Witch** card leads Zombie Avengers and has a solo instruction for substituting another group (WI p.14). For **Collect an Interstellar Zoo**, the pictured Scheme sets 11 Twists and wins when five Heroes are in its Zoo; its Twist sends an eligible Hero from hand or discard pile to the Zoo. Icon-dependent eligibility and Twist-specific conditions are not fully recoverable from the text extraction (WI p.6). These examples do not establish the other Masterminds' Always Leads/setup effects or the remaining three Schemes' full setups.

The rulebook explicitly replaces earlier solo rules, but no broader Revised/First Edition cross-ruleset mixing rule was located. Do not infer product compatibility from the solo-mode note.

## Rules and glossary

- **What If...? (WI p.16):** Choose a Hero Class or Hero Name, reveal the deck's top card, and either return it or discard it. If it matches the choice, use the ability; grey zero-cost starting cards cannot trigger it.
- **Soulbind (WI pp.16–17):** After playing a Hero, optionally turn a face-up Villain in the Victory Pile face down and move it to the bottom; then use the Soulbind effect. It does not count as being in the Victory Pile until face-up again for final scoring.
- **Liberate (WI p.17):** Gain an Attack bonus usable only against a Villain holding Bystanders or a Mastermind; the bonus can be used against a Mastermind without Bystanders.
- **Empowered (WI p.17):** A Hero, Villain, or Mastermind gains strength for each matching card in the HQ; check when playing the Hero or fighting the enemy. The printed icon criteria need card-level verification.
- **Multiclass (WI pp.17–18):** A card counts as each of its printed Hero Classes. All Star-Lord T'Challa cards are Multiclass.
- **Cross-Dimensional Rampage (WI p.18):** A Party, Zombie, or Demon Rampage can be avoided by revealing a matching thematic card; otherwise gain a Wound. The insert explains which names qualify, including card, Hero, Villain Group, and Tactic names.
- **Rise of the Living Dead (WI pp.18–19):** Each player checks the top card of their Victory Pile; if it is a Villain with the ability, it returns to the city. It resolves in player order, at most one returning Villain per player, without chain reactions; Mastermind Tactics do not return.
- **Ascending Villains (WI p.19):** Some escaping Villains become additional Masterminds. Defeat all Masterminds to win; an ascending Mastermind has no Tactics and takes one fight to defeat.
- **Hero Classes and teams (WI p.25):** Strength, Instinct, Covert, Tech, and Ranged are Hero Classes/colors. Basic is grey and has no Hero Class; the term covers starting S.H.I.E.L.D. Heroes and Officers. The rulebook also defines the Guardians of the Multiverse and S.H.I.E.L.D. teams.

Wounds, Bystanders, the HQ, the Victory Pile, and standard S.H.I.E.L.D. decks are shared game parts. The insert introduces no additional token or separate shared stack. Complete card-specific Scheme requirements, Always Leads mappings, Hero metadata, and component dependencies remain open.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Captain Carter

- **Super Soldier Serum**; type/group: Hero / Captain Carter; copies: 3; Hero Name: Captain Carter; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 2; Recruit 0+; Attack 0+; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainCarter_2Common.png).
- **Wartime Logistics**; type/group: Hero / Captain Carter; copies: 3; Hero Name: Captain Carter; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 3; Recruit 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainCarter_3Common.png).
- **Coordinated Assault**; type/group: Hero / Captain Carter; copies: 3; Hero Name: Captain Carter; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 4; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainCarter_4Common.png).
- **The Shield of Britain**; type/group: Hero / Captain Carter; copies: 2; Hero Name: Captain Carter; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 5; Attack 3; keyword labels: Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainCarter_5Uncommon.png).
- **Give Them All We've Got**; type/group: Hero / Captain Carter; copies: 2; Hero Name: Captain Carter; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 6; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainCarter_6Uncommon.png).
- **Icon of Hope**; type/group: Hero / Captain Carter; copies: 1; Hero Name: Captain Carter; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 8; Recruit 2+; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/CaptainCarter_1Rare.png).

### Hero group: Star-Lord T'Challa

- **Fight or Flight**; type/group: Hero / Star-Lord T'Challa; copies: 3; Hero Name: Star-Lord T'Challa; team: Guardians of the Multiverse; class icons: Strength, Covert; printed values: Cost 2; Attack 0+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Star-LordTChalla_2Common.png).
- **Interstellar Adventures**; type/group: Hero / Star-Lord T'Challa; copies: 3; Hero Name: Star-Lord T'Challa; team: Guardians of the Multiverse; class icons: Covert, Tech; printed values: Cost 3; Recruit 2+; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Star-LordTChalla_3Common.png).
- **Plan the Heist**; type/group: Hero / Star-Lord T'Challa; copies: 3; Hero Name: Star-Lord T'Challa; team: Guardians of the Multiverse; class icons: Instinct, Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Star-LordTChalla_4Common.png).
- **Unexpected Exit**; type/group: Hero / Star-Lord T'Challa; copies: 2; Hero Name: Star-Lord T'Challa; team: Guardians of the Multiverse; class icons: Strength, Instinct; printed values: Cost 5; Attack 3; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Star-LordTChalla_5Uncommon.png).
- **Cross the Multiverse**; type/group: Hero / Star-Lord T'Challa; copies: 2; Hero Name: Star-Lord T'Challa; team: Guardians of the Multiverse; class icons: Strength, Ranged; printed values: Cost 6; Attack 4+; keyword labels: What If...?, Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Star-LordTChalla_6Uncommon.png).
- **Colliding Dreams**; type/group: Hero / Star-Lord T'Challa; copies: 1; Hero Name: Star-Lord T'Challa; team: Guardians of the Multiverse; class icons: Tech, Ranged; printed values: Cost 7; Attack 4+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Star-LordTChalla_1Rare.png).

### Hero group: Party Thor

- **Forecast Says Thunder**; type/group: Hero / Party Thor; copies: 3; Hero Name: Party Thor; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 2; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PartyThor_2Common.png).
- **Worthy Challenge**; type/group: Hero / Party Thor; copies: 3; Hero Name: Party Thor; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PartyThor_3Common.png).
- **Destructive Feast**; type/group: Hero / Party Thor; copies: 3; Hero Name: Party Thor; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 5; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PartyThor_4Common.png).
- **Asgardian Rager**; type/group: Hero / Party Thor; copies: 2; Hero Name: Party Thor; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 5; Attack 3+; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PartyThor_5Uncommon.png).
- **Only Son**; type/group: Hero / Party Thor; copies: 2; Hero Name: Party Thor; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 6; Attack 3+; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PartyThor_6Uncommon.png).
- **Worthy of the Lightning**; type/group: Hero / Party Thor; copies: 1; Hero Name: Party Thor; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 7; Recruit 5; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/PartyThor_1Rare.png).

### Hero group: Killmonger, Spec Ops

- **Hunt New Prey**; type/group: Hero / Killmonger, Spec Ops; copies: 3; Hero Name: Killmonger, Spec Ops; team: Guardians of the Multiverse; class icons: Strength, Tech; printed values: Cost 2; Recruit 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KillmongerSpecOps_2Common.png).
- **No Matter the Price**; type/group: Hero / Killmonger, Spec Ops; copies: 3; Hero Name: Killmonger, Spec Ops; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KillmongerSpecOps_3Common.png).
- **Violence Leaves Scars**; type/group: Hero / Killmonger, Spec Ops; copies: 3; Hero Name: Killmonger, Spec Ops; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KillmongerSpecOps_4Common.png).
- **Hostage Rescue**; type/group: Hero / Killmonger, Spec Ops; copies: 2; Hero Name: Killmonger, Spec Ops; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 3; Attack 2+; keyword labels: What If...?, Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KillmongerSpecOps_5Uncommon.png).
- **Plot a Betrayal**; type/group: Hero / Killmonger, Spec Ops; copies: 2; Hero Name: Killmonger, Spec Ops; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 6; Attack 4+; keyword labels: Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KillmongerSpecOps_6Uncommon.png).
- **I'm the King Baby!**; type/group: Hero / Killmonger, Spec Ops; copies: 1; Hero Name: Killmonger, Spec Ops; team: Guardians of the Multiverse; class icons: Strength; printed values: Cost 7; Attack 4+; keyword labels: Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/KillmongerSpecOps_1Rare.png).

### Hero group: Apocalyptic Black Widow

- **Humanity's Final Hope**; type/group: Hero / Apocalyptic Black Widow; copies: 3; Hero Name: Apocalyptic Black Widow; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ApocalypticBlackWidow_2Common.png).
- **Plant Hidden Asset**; type/group: Hero / Apocalyptic Black Widow; copies: 3; Hero Name: Apocalyptic Black Widow; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 4; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ApocalypticBlackWidow_3Common.png).
- **Precision Strike**; type/group: Hero / Apocalyptic Black Widow; copies: 3; Hero Name: Apocalyptic Black Widow; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ApocalypticBlackWidow_4Common.png).
- **Relentless**; type/group: Hero / Apocalyptic Black Widow; copies: 2; Hero Name: Apocalyptic Black Widow; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 2; Attack 0+; keyword labels: Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ApocalypticBlackWidow_5Uncommon.png).
- **The Last Avenger**; type/group: Hero / Apocalyptic Black Widow; copies: 2; Hero Name: Apocalyptic Black Widow; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 6; Attack 3+; keyword labels: What If...?, Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ApocalypticBlackWidow_6Uncommon.png).
- **Time to Save the Multiverse**; type/group: Hero / Apocalyptic Black Widow; copies: 1; Hero Name: Apocalyptic Black Widow; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 8; Attack 4+; keyword labels: Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ApocalypticBlackWidow_1Rare.png).

### Hero group: Gamora, Destroyer of Thanos

- **Assassin's Stealth**; type/group: Hero / Gamora, Destroyer of Thanos; copies: 3; Hero Name: Gamora, Destroyer of Thanos; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 2; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GamoraDestroyerOfThanos_2Common.png).
- **Tactical Insight**; type/group: Hero / Gamora, Destroyer of Thanos; copies: 3; Hero Name: Gamora, Destroyer of Thanos; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 3; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GamoraDestroyerOfThanos_3Common.png).
- **Wield the Blade of Thanos**; type/group: Hero / Gamora, Destroyer of Thanos; copies: 3; Hero Name: Gamora, Destroyer of Thanos; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 4; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GamoraDestroyerOfThanos_4Common.png).
- **Titanicide**; type/group: Hero / Gamora, Destroyer of Thanos; copies: 2; Hero Name: Gamora, Destroyer of Thanos; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 5; Attack 2; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GamoraDestroyerOfThanos_5Uncommon.png).
- **Destroy an Infinity Stone**; type/group: Hero / Gamora, Destroyer of Thanos; copies: 2; Hero Name: Gamora, Destroyer of Thanos; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 6; Attack 3; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GamoraDestroyerOfThanos_6Uncommon.png).
- **The Infinity Crusher**; type/group: Hero / Gamora, Destroyer of Thanos; copies: 1; Hero Name: Gamora, Destroyer of Thanos; team: Guardians of the Multiverse; class icons: Tech; printed values: Cost 8; Attack 5+; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/GamoraDestroyerOfThanos_1Rare.png).

### Hero group: Doctor Strange Supreme

- **Seize Infernal Power**; type/group: Hero / Doctor Strange Supreme; copies: 3; Hero Name: Doctor Strange Supreme; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 3; Recruit 2+; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeSupreme_2Common.png).
- **Summon Demon Minions**; type/group: Hero / Doctor Strange Supreme; copies: 3; Hero Name: Doctor Strange Supreme; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeSupreme_3Common.png).
- **Wards of the Vishanti**; type/group: Hero / Doctor Strange Supreme; copies: 3; Hero Name: Doctor Strange Supreme; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 5; Attack 3+; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeSupreme_4Common.png).
- **To Save Christine**; type/group: Hero / Doctor Strange Supreme; copies: 2; Hero Name: Doctor Strange Supreme; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 2; Attack 0+; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeSupreme_5Uncommon.png).
- **Break the Absolute Point in Time**; type/group: Hero / Doctor Strange Supreme; copies: 2; Hero Name: Doctor Strange Supreme; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeSupreme_6Uncommon.png).
- **Stygian Communion**; type/group: Hero / Doctor Strange Supreme; copies: 1; Hero Name: Doctor Strange Supreme; team: Guardians of the Multiverse; class icons: Instinct; printed values: Cost 8; Attack 3; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/DoctorStrangeSupreme_1Rare.png).

### Hero group: Uatu, The Watcher

- **Diverging Timestreams**; type/group: Hero / Uatu, The Watcher; copies: 3; Hero Name: Uatu, The Watcher; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 2; Recruit 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/UatuTheWatcher_2Common.png).
- **Another Dimension Crumbles**; type/group: Hero / Uatu, The Watcher; copies: 3; Hero Name: Uatu, The Watcher; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/UatuTheWatcher_3Common.png).
- **Break the Oath**; type/group: Hero / Uatu, The Watcher; copies: 3; Hero Name: Uatu, The Watcher; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: What If...?; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/UatuTheWatcher_4Common.png).
- **Anoint a Champion**; type/group: Hero / Uatu, The Watcher; copies: 2; Hero Name: Uatu, The Watcher; team: Guardians of the Multiverse; class icons: Covert; printed values: Cost 5; Attack 2+; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/UatuTheWatcher_5Uncommon.png).
- **History Repeats**; type/group: Hero / Uatu, The Watcher; copies: 2; Hero Name: Uatu, The Watcher; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 6; Attack 3+; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/UatuTheWatcher_6Uncommon.png).
- **Convoke the Guardians**; type/group: Hero / Uatu, The Watcher; copies: 1; Hero Name: Uatu, The Watcher; team: Guardians of the Multiverse; class icons: Ranged; printed values: Cost 7; Attack 5+; keyword labels: What If...?, Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/UatuTheWatcher_1Rare.png).

### Villain Group: Intergalactic Party Animals

- **Captain Marvel, End of the Party**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 7+; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsCaptainMarvelEndOfTheParty.png).
- **Frigga, Mother of Thor**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 12; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsFriggaMotherOfThor.png).
- **Party Korg**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 5; VP 3; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsPartyKorg.png).
- **Party Nebula**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsPartyNebula.png).
- **Party Kraglin**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsPartyKraglin.png).
- **Party Korath**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 4+; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsPartyKorath.png).
- **Party Skrull**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 2+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsPartySkrull.png).
- **Party Surtur**; type/group: Villain / Intergalactic Party Animals; copies: 1; printed values: Attack 6; VP 4; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/IntergalacticPartyAnimalsPartySurtur.png).

### Villain Group: Rival Overlords

- **Thanos**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 12*; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsThanos.png).
- **Dormammu**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 11*; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsDormammu.png).
- **Ego**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 10*; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsEgo.png).
- **Loki**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 9*; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsLoki.png).
- **Red Skull, HYDRA Occultist**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 8*; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsRedSkullHydraOccultist.png).
- **Yondu**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 7*; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsYondu.png).
- **Arnim Zola, HYDRA Scientist**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 5*; VP 2; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsArnimZolaHydraScientist.png).
- **Ulysses Klaue**; type/group: Villain / Rival Overlords; copies: 1; printed values: Attack 6*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/RivalOverlordsUlyssesKlaue.png).

### Villain Group: Black Order Guards

- **Cull Obsidian**; type/group: Villain / Black Order Guards; copies: 3; printed values: Attack 2+; VP 2; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/BlackOrderGuardsCullObsidian.png).
- **Corvus Glaive**; type/group: Villain / Black Order Guards; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/BlackOrderGuardsCorvusGlaive.png).
- **Proxima Midnight**; type/group: Villain / Black Order Guards; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/BlackOrderGuardsProximaMidnight.png).
- **Ebony Maw**; type/group: Villain / Black Order Guards; copies: 1; printed values: Attack 7+; VP 6; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/BlackOrderGuardsEbonyMaw.png).

### Villain Group: Zombie Avengers

- **Zombie Wong**; type/group: Villain / Zombie Avengers; copies: 2; printed values: Attack 4; VP 2; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ZombieAvengersWong.png).
- **Zombie Hawkeye**; type/group: Villain / Zombie Avengers; copies: 2; printed values: Attack 5; VP 3; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ZombieAvengersZombieHawkeye.png).
- **Zombie Wasp**; type/group: Villain / Zombie Avengers; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ZombieAvengersZombieWasp.png).
- **Zombie Doctor Strange**; type/group: Villain / Zombie Avengers; copies: 1; printed values: Attack 6; VP 4; keyword labels: Rise of The Living Dead, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsDemonboundDoctorStrange.png).
- **Zombie Iron Man**; type/group: Villain / Zombie Avengers; copies: 1; printed values: Attack 7; VP 5; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ZombieAvengersZombieIronMan.png).
- **Zombie Captain America**; type/group: Villain / Zombie Avengers; copies: 1; printed values: Attack 8*; VP 4; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ZombieAvengersZombieCaptainAmerica.png).

### Villain Group: Strange's Demons

- **Wolf Demon**; type/group: Villain / Strange's Demons; copies: 2; printed values: Attack 4; VP 2; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsWolfDemon.png).
- **Moose Demon**; type/group: Villain / Strange's Demons; copies: 1; printed values: Attack 4; VP 2; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsMooseDemon.png).
- **Two-Headed Ram Demon**; type/group: Villain / Strange's Demons; copies: 1; printed values: Attack 5; VP 3; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsTwo-HeadedRamDemon.png).
- **Skull Demon**; type/group: Villain / Strange's Demons; copies: 1; printed values: Attack 5; VP 3; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsSkullDemon.png).
- **Demon Dragon**; type/group: Villain / Strange's Demons; copies: 1; printed values: Attack 6; VP 4; keyword labels: Soulbind, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsDemonDragon.png).
- **Demonbound Doctor Strange**; type/group: Villain / Strange's Demons; copies: 1; printed values: Attack 7; VP 5; keyword labels: Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsDemonboundDoctorStrange.png).
- **Demon Champion of Hydra**; type/group: Villain / Strange's Demons; copies: 1; printed values: Attack 8; VP 6; keyword labels: Cross-Dimensional Rampage, Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/StrangesDemonsDemonChampionOfHydra.png).

### Henchman Group: Giants of Jotunheim

- **Giants of Jotunheim**; type/group: Henchman / Giants of Jotunheim; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/GiantsOfJotunheim.png).

### Henchman Group: Vibranium Liberator Drones

- **Vibranium Liberator Drones**; type/group: Henchman / Vibranium Liberator Drones; copies: Unverified; printed values: not indexed in C1; keyword labels: Liberate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/VibraniumLiberatorDrones.png).

### Henchman Group: Ultron Sentries

- **Ultron Sentries (Covert)**; type/group: Henchman / Ultron Sentries; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/UltronSentries_Covert.png).
- **Ultron Sentries (Instinct)**; type/group: Henchman / Ultron Sentries; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/UltronSentries_Instinct.png).
- **Ultron Sentries (Ranged)**; type/group: Henchman / Ultron Sentries; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/UltronSentries_Ranged.png).
- **Ultron Sentries (Strength)**; type/group: Henchman / Ultron Sentries; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/UltronSentries_Strength.png).
- **Ultron Sentries (Tech)**; type/group: Henchman / Ultron Sentries; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/UltronSentries_Tech.png).

### Mastermind: Hank Pym, Yellowjacket

- **Hank Pym, Yellowjacket**; type/group: Normal Mastermind face / Hank Pym, Yellowjacket; copies: Unverified; printed values: Attack 4*; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/HankPymYellowjacket.png).
- **Epic Hank Pym, Yellowjacket**; type/group: Epic Mastermind face / Hank Pym, Yellowjacket; copies: Unverified; printed values: Attack 6*; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/HankPymYellowjacket_Epic.png).
- **Microscopic Research**; type/group: Mastermind Tactic / Hank Pym, Yellowjacket; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/HankPymYellowjacket_Tactic1.png).
- **Revenge for Ancient Grievance**; type/group: Mastermind Tactic / Hank Pym, Yellowjacket; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/HankPymYellowjacket_Tactic2.png).
- **Save from Assassination**; type/group: Mastermind Tactic / Hank Pym, Yellowjacket; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/HankPymYellowjacket_Tactic3.png).
- **Vengeful Sting**; type/group: Mastermind Tactic / Hank Pym, Yellowjacket; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/HankPymYellowjacket_Tactic4.png).

### Mastermind: Zombie Scarlet Witch

- **Zombie Scarlet Witch**; type/group: Normal Mastermind face / Zombie Scarlet Witch; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Rise of The Living Dead, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZombieScarletWitch.png).
- **Epic Zombie Scarlet Witch**; type/group: Epic Mastermind face / Zombie Scarlet Witch; copies: Unverified; printed values: Attack 13+; VP 6; keyword labels: Rise of The Living Dead, Cross-Dimensional Rampage, Soulbind; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZombieScarletWitch_Epic.png).
- **Chaos Hex**; type/group: Mastermind Tactic / Zombie Scarlet Witch; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZombieScarletWitchTactic1.png).
- **Even the Odds**; type/group: Mastermind Tactic / Zombie Scarlet Witch; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZombieScarletWitchTactic2.png).
- **Refuse to Accept Death**; type/group: Mastermind Tactic / Zombie Scarlet Witch; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZombieScarletWitchTactic3.png).
- **Wistful Illusion**; type/group: Mastermind Tactic / Zombie Scarlet Witch; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZombieScarletWitchTactic4.png).

### Mastermind: Killmonger, The Betrayer

- **Killmonger, The Betrayer**; type/group: Normal Mastermind face / Killmonger, The Betrayer; copies: Unverified; printed values: Attack 9; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTheBetrayer.png).
- **Epic Killmonger, The Betrayer**; type/group: Epic Mastermind face / Killmonger, The Betrayer; copies: Unverified; printed values: Attack 12; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTheBetrayer_Epic.png).
- **Change in Loyalties**; type/group: Mastermind Tactic / Killmonger, The Betrayer; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTheBetrayerTactic1.png).
- **Pulling the Strings**; type/group: Mastermind Tactic / Killmonger, The Betrayer; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTheBetrayerTactic2.png).
- **See You on the Flip Side**; type/group: Mastermind Tactic / Killmonger, The Betrayer; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTheBetrayerTactic3.png).
- **Sunset Over Wakanda**; type/group: Mastermind Tactic / Killmonger, The Betrayer; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/KillmongerTheBetrayerTactic4.png).

### Mastermind: Ultron Infinity

- **Ultron Infinity**; type/group: Normal Mastermind face / Ultron Infinity; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Empowered, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/UltronInfinity.png).
- **Epic Ultron Infinity**; type/group: Epic Mastermind face / Ultron Infinity; copies: Unverified; printed values: Attack 12+; VP 6; keyword labels: Empowered, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/UltronInfinity_Epic.png).
- **Infinity of Minions**; type/group: Mastermind Tactic / Ultron Infinity; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/UltronInfinityTactic1.png).
- **Struggle for the Infinity Stones**; type/group: Mastermind Tactic / Ultron Infinity; copies: Unverified; printed values: not indexed in C1; keyword labels: Empowered; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/UltronInfinityTactic2.png).
- **Transcend Mortality**; type/group: Mastermind Tactic / Ultron Infinity; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/UltronInfinityTactic3.png).
- **Unfettered Annihilation**; type/group: Mastermind Tactic / Ultron Infinity; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/UltronInfinityTactic4.png).

### Scheme: Trash Earth with Hugest Party Ever

- **Trash Earth with Hugest Party Ever**; type/group: Scheme / Trash Earth with Hugest Party Ever; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Trash%20Earth%20With%20Hugest%20Party%20Ever.png).

### Scheme: Marvel Zombies

- **Marvel Zombies**; type/group: Scheme / Marvel Zombies; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Marvel%20Zombies.png).

### Scheme: Collect an Interstellar Zoo

- **Collect an Interstellar Zoo**; type/group: Scheme / Collect an Interstellar Zoo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Collect%20An%20Interstellar%20Zoo.png).

### Scheme: Breach the Nexus of All Realities

- **Breach the Nexus of All Realities**; type/group: Scheme / Breach the Nexus of All Realities; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/Breach%20The%20Nexus%20Of%20All%20Realities.png).

### Bystander set: Bystander

- **Bystander**; type/group: Bystander / Bystander; copies: 25; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/What%20If%20Bystander.png).

### Bystander set: Happy Hogan

- **Happy Hogan**; type/group: Bystander / Happy Hogan; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/What%20If%20B%20Happy%20Hogan.png).

### Bystander set: Howard Stark

- **Howard Stark**; type/group: Bystander / Howard Stark; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/What%20If%20B%20Howard%20Stark.png).

### Bystander set: Howard the Duck

- **Howard the Duck**; type/group: Bystander / Howard the Duck; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/What%20If%20B%20Howard%20the%20Duck.png).

### Bystander set: Pepper Potts

- **Pepper Potts**; type/group: Bystander / Pepper Potts; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/What%20If%20B%20Pepper%20Potts.png).

### Bystander set: Scott Lang's Head

- **Scott Lang's Head**; type/group: Bystander / Scott Lang's Head; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/What%20If%20B%20Scott%20Langs%20Head.png).

### Wound set: Wound

- **Wound**; type/group: Wound / Wound; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/General%20Cards/What%20If%20Wound.png).

### Officer set: S.H.I.E.L.D. Officer

- **S.H.I.E.L.D. Officer**; type/group: Officer / S.H.I.E.L.D. Officer; copies: 8; printed values: Cost 3; Recruit 2; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
