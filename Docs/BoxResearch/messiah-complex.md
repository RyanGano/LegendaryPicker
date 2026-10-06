# Messiah Complex (January 2022)

**Research status: Integrated ([#186](https://github.com/RyanGano/LegendaryPicker/issues/186)); partial.** Scheme Setup lines, Always Leads and part uses were read from OCR of every C1-linked card face (`Card`); Hero teams and classes come from C1 and the cards. Runtime data: `LegendaryPickerService/Data/Boxes/messiah-complex.json`.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| MC | [Upper Deck Messiah Complex rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Lgd_MessiahComplex_Rulesheet_Compressed.pdf) | Contents, Sidekicks, Clone, Shatter, Tactical Formation, Prey, and Scheme rules (PDF pp.1–2). |
| C1 | [master-strike structured messiah-complex card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/messiahcomplex.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | January 2022 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (MC p.2)

| Type | Official count |
|---|---:|
| Clone Heroes | 4 groups × 14 cards = 56 |
| Other Heroes | 4 groups × 14 cards = 56 |
| Sidekicks | 7 types × 2 copies = 14 |
| Villain Groups | 4 groups × 8 cards = 32 |
| Henchman Groups | 2 groups × 10 cards = 20 |
| Double-Sided Epic Masterminds | 3 sets × 5 cards = 15 |
| Double-Sided Veiled Schemes | 4 |
| Special Bystanders | 3 types × 1 card = 3 |
| Total cards | 200 |

The listed categories sum to the official 200-card total.

### Group inventory (C1)

- **Clone Heroes (four):** Multiple Man; Shatterstar; Stepford Cuckoos; M.
- **Other Heroes (four):** Strong Guy; Warpath; Siryn; Rictor.
- **Villain Groups (four):** Reavers; Purifiers; Acolytes; Clan Yashida.
- **Henchman Groups (two):** Mr. Sinister Clones; Sentinel Squad O∗N∗E∗.
- **Masterminds (three):** Lady Deathstrike; Bastion, Fused Sentinel; Exodus.
- **Veiled/Unveiled Scheme card pairs (four):** Hack Cerebro Servers To... / ...Control the Mutant Messiah; Drain Mutant Powers To... / ...Open Rifts to Future Timelines; Hire Singularity Investigations To... / ...Reveal The Heroes' Evil Clones; Raid Gene Banks To... / ...Unleash an Anti-Mutant Bioweapon.
- **Special Bystanders (three):** Cloning Technician; Opera Singer; Private Investigator.
- **Sidekick types (seven):** Layla Miller; Skids; Rockslide; Darwin; Boom-Boom; Prodigy; Rusty “Firefist” Collins.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Setup values in the runtime data come from the card faces (`Card`) and the insert (MC).

## Rules and mechanisms

- **Clone Heroes (MC p.1):** Gain another copy from the HQ; if none is there, find one in the Hero Deck and shuffle that deck. Gaining means placing it in the discard pile. A Clone that is an Officer or Sidekick searches its own stack instead.
- **When Recruited: Clone (MC p.1):** Resolve the Clone ability immediately after recruiting and refilling the HQ. Gaining or placing a Hero does not trigger it, and a copy gained this way does not recursively Clone.
- **Clone Villains (MC p.1):** A Clone Ambush searches the Villain Deck for a copy to enter the city, ignores further Clone effects on that copy, then shuffles the deck. If no copy is found, continue without one.
- **Shatter (MC p.1):** Halve a Villain's current value, rounding up, until turn end; it can be applied repeatedly. It cannot target a Mastermind. Shatter the Mastermind applies to one fight. The keyword can also halve an HQ Hero's current cost.
- **Tactical Formation (MC p.1):** Use the ability only when the player has the indicated Hero cost pattern; the card with the ability can count. “Heroes you have” includes hand and played cards unless moved elsewhere.
- **Prey (MC p.2):** After the Villain enters the Sewers, players reveal hands and it preys on a player with the fewest of the indicated icon. Anyone may fight it. If it survives through the preyed-on player's turn, resolve its Finish the Prey effect after that player draws, then return it to the Sewers without another Ambush. The relevant icon is missing from text extraction.
- **Chivalrous Duel (MC p.2):** Spend points from Hero cards sharing one Hero Name only. Non-Hero cards and Shards cannot contribute; a Hero Artifact with the matching Hero Name can.
- **Veiled Schemes (MC p.2):** Start with the Veiled side face up. When instructed to transform, remove it and replace it with an Unveiled side selected at random from those available to the players.

## Required parts and glossary

- **Sidekick Stack:** Shuffle the 14 new Special Sidekicks into the stack. Once per turn, a player may pay 2 Recruit to recruit its top card; when played, return it to the bottom. A Sidekick gained by another effect does not use the once-per-turn recruit limit (MC p.2).
- Special Sidekicks count as played for Superpower abilities, but after returning to the stack they are not among “Heroes you have” (MC p.2).
- **Clone:** Gain another copy of a matching Hero or Villain using its corresponding deck or stack. (MC p.1)
- **Shatter:** Halve a target's current value, rounding up, for the relevant duration. (MC p.1)
- **Tactical Formation:** A conditional ability based on the costs of Heroes held or played. (MC p.1)
- **Prey:** Assign a Villain to a player with the fewest qualifying icons, then resolve its consequence if it survives that player's turn. (MC p.2)
- **Veiled Scheme:** A Scheme that begins on its hidden side and later transforms into an Unveiled side. (MC p.2)

Summaries are original paraphrases under 40 words.

## Setup and implementation gaps

Still open: per-face copy counts of the four non-Clone Heroes; the seven Sidekick faces have no C1 image, so their own text was not read (they join the Sidekick stack and change no setup); the Epic sides' own setup effects are not drawn yet (#151). A Veiled Scheme is set up from its Veiled side; the Unveiled side is picked at random from every Unveiled Scheme the players own when it transforms, so setup adds only a step, and the Unveiled sides' "when revealed" draws (an extra Villain Group, an extra Hero) happen in play. Bastion, Fused Sentinel's "any Sentinel Henchmen Group" is read as Sentinel Squad O\*N\*E\* or the core Sentinel when the core box is included. The Opera Singer Bystander's OCR was unreadable; a visual read of the face (review of #187) found it shatters a Villain in the Bank or the HQ Hero below it, so it uses no stack.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Multiple Man

- **Finding Myself**; type/group: Hero / Multiple Man; copies: 4; Hero Name: Multiple Man; team: X-Factor Investigations; class icons: Tech; printed values: Cost 4; Attack 1; keyword labels: Investigate, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/multiple_man_01.png).
- **Me, Myself, and I**; type/group: Hero / Multiple Man; copies: 4; Hero Name: Multiple Man; team: X-Factor Investigations; class icons: Instinct; printed values: Cost 4; Recruit 2; keyword labels: Tactical Formation, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/multiple_man_02.png).
- **Perfect Match**; type/group: Hero / Multiple Man; copies: 4; Hero Name: Multiple Man; team: X-Factor Investigations; class icons: Tech; printed values: Cost 4; Attack 1+; keyword labels: Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/multiple_man_03.png).
- **Reabsorb Duplicates**; type/group: Hero / Multiple Man; copies: 2; Hero Name: Multiple Man; team: X-Factor Investigations; class icons: Instinct; printed values: Cost 4; Attack 2; keyword labels: Tactical Formation, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/multiple_man_04.png).

### Hero group: Shatterstar

- **Strive for Greatness**; type/group: Hero / Shatterstar; copies: 4; Hero Name: Shatterstar; team: X-Force; class icons: Instinct; printed values: Cost 3; Recruit 2+; keyword labels: “When Recruited“ Abilities, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shatterstar_01.png).
- **Bioelectric Surge**; type/group: Hero / Shatterstar; copies: 4; Hero Name: Shatterstar; team: X-Force; class icons: Ranged; printed values: Cost 5; Attack 2+; keyword labels: “When Recruited“ Abilities, Clone, Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shatterstar_02.png).
- **Gladiator's Blades**; type/group: Hero / Shatterstar; copies: 4; Hero Name: Shatterstar; team: X-Force; class icons: Instinct; printed values: Cost 5; Attack 2; keyword labels: “When Recruited“ Abilities, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shatterstar_03.png).
- **Gene-Spliced Creation**; type/group: Hero / Shatterstar; copies: 2; Hero Name: Shatterstar; team: X-Force; class icons: Instinct; printed values: Cost 5; Attack 2; keyword labels: “When Recruited“ Abilities, Clone, Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shatterstar_04.png).

### Hero group: Stepford Cuckoos

- **Find Mutants with Cerebro**; type/group: Hero / Stepford Cuckoos; copies: 4; Hero Name: Stepford Cuckoos; team: X-Men; class icons: Tech; printed values: Cost 2; Attack 1; keyword labels: “When Recruited“ Abilities, Clone, Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stepford_cuckoos_01.png).
- **Shared Thoughts**; type/group: Hero / Stepford Cuckoos; copies: 4; Hero Name: Stepford Cuckoos; team: X-Men; class icons: Covert; printed values: Cost 2; Attack 1+; keyword labels: “When Recruited“ Abilities, Clone, Tactical Formation, Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stepford_cuckoos_02.png).
- **Telepathic Warning**; type/group: Hero / Stepford Cuckoos; copies: 4; Hero Name: Stepford Cuckoos; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2+; keyword labels: “When Recruited“ Abilities, Clone, Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stepford_cuckoos_03.png).
- **Mind Wipe**; type/group: Hero / Stepford Cuckoos; copies: 2; Hero Name: Stepford Cuckoos; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2+; keyword labels: “When Recruited“ Abilities, Clone, Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stepford_cuckoos_04.png).

### Hero group: M

- **Penance Form**; type/group: Hero / M; copies: 4; Hero Name: M; team: X-Factor Investigations; class icons: Strength; printed values: Cost 3; Attack 2+; keyword labels: “When Recruited“ Abilities, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/m_1.png).
- **Three Sisters Combined**; type/group: Hero / M; copies: 4; Hero Name: M; team: X-Factor Investigations; class icons: Strength; printed values: Cost 3; Attack 0+; keyword labels: “When Recruited“ Abilities, Clone, Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/m_2.png).
- **Uncover Family Secrets**; type/group: Hero / M; copies: 4; Hero Name: M; team: X-Factor Investigations; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: “When Recruited“ Abilities, Clone, Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/m_03.png).
- **Interweaving Powers**; type/group: Hero / M; copies: 2; Hero Name: M; team: X-Factor Investigations; class icons: Covert; printed values: Cost 3; Attack 2+; keyword labels: “When Recruited“ Abilities, Clone, Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/m_4.png).

### Hero group: Strong Guy

- **X-Factor Investigations**; type/group: Hero / Strong Guy; copies: Unverified; Hero Name: Strong Guy; team: X-Factor Investigations; class icons: Strength; printed values: Cost 4; Recruit 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/strong_guy_01.png).
- **Absorb Kinetic Energy**; type/group: Hero / Strong Guy; copies: Unverified; Hero Name: Strong Guy; team: X-Factor Investigations; class icons: Strength; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/strong_guy_02.png).
- **Go Big**; type/group: Hero / Strong Guy; copies: Unverified; Hero Name: Strong Guy; team: X-Factor Investigations; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/strong_guy_03.png).
- **Treasure Hunt**; type/group: Hero / Strong Guy; copies: Unverified; Hero Name: Strong Guy; team: X-Factor Investigations; class icons: Strength; printed values: Cost 8; Attack 3; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/strong_guy_04.png).

### Hero group: Warpath

- **Grim Tracker**; type/group: Hero / Warpath; copies: Unverified; Hero Name: Warpath; team: X-Force; class icons: Instinct; printed values: Cost 2; Attack 1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warpath_01.png).
- **Endless Endurance**; type/group: Hero / Warpath; copies: Unverified; Hero Name: Warpath; team: X-Force; class icons: Strength; printed values: Cost 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warpath_02.png).
- **Dangerous Maneuver**; type/group: Hero / Warpath; copies: Unverified; Hero Name: Warpath; team: X-Force; class icons: Covert; printed values: Cost 2; Attack 0+; keyword labels: Tactical Formation; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warpath_03.png).
- **Superhuman Senses**; type/group: Hero / Warpath; copies: Unverified; Hero Name: Warpath; team: X-Force; class icons: Instinct; printed values: Cost 7; Attack 3+; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warpath_04.png).

### Hero group: Siryn

- **Echolocation**; type/group: Hero / Siryn; copies: Unverified; Hero Name: Siryn; team: X-Factor Investigations; class icons: Covert; printed values: Cost 2; Attack 1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/siryn_01.png).
- **Hypnotic Call**; type/group: Hero / Siryn; copies: Unverified; Hero Name: Siryn; team: X-Factor Investigations; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/siryn_02.png).
- **Three-Octave Arpeggio**; type/group: Hero / Siryn; copies: Unverified; Hero Name: Siryn; team: X-Factor Investigations; class icons: Ranged; printed values: Cost 6; Attack 4; keyword labels: Tactical Formation, Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/siryn_03.png).
- **Splintering Shriek**; type/group: Hero / Siryn; copies: Unverified; Hero Name: Siryn; team: X-Factor Investigations; class icons: Covert; printed values: Cost 8; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/siryn_04.png).

### Hero group: Rictor

- **Underground Cave-In**; type/group: Hero / Rictor; copies: Unverified; Hero Name: Rictor; team: X-Factor Investigations; class icons: Ranged; printed values: Cost 3; Attack 2; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rictor_01.png).
- **Unearth Tectonic Power**; type/group: Hero / Rictor; copies: Unverified; Hero Name: Rictor; team: X-Factor Investigations; class icons: Instinct; printed values: Cost 5; Attack 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rictor_02.png).
- **Trace the Fault Lines**; type/group: Hero / Rictor; copies: Unverified; Hero Name: Rictor; team: X-Factor Investigations; class icons: Ranged; printed values: Cost 4; Recruit 2; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rictor_03.png).
- **Massive Earthquake**; type/group: Hero / Rictor; copies: Unverified; Hero Name: Rictor; team: X-Factor Investigations; class icons: Ranged; printed values: Cost 7; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rictor_04.png).

### Villain Group: Reavers

- **Donald Pierce**; type/group: Villain / Reavers; copies: 2; printed values: Attack 6; VP 4; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/reavers_02.png).
- **Bonebreaker**; type/group: Villain / Reavers; copies: 2; printed values: Attack 5; VP 3; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/reavers_01.png).
- **Skullbuster**; type/group: Villain / Reavers; copies: 2; printed values: Attack 5; VP 3; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/reavers_04.png).
- **Pretty Boy**; type/group: Villain / Reavers; copies: 2; printed values: Attack 2; VP 2; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/reavers_03.png).

### Villain Group: Purifiers

- **Predator X (Ranged)**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 3; VP 2; keyword labels: Prey, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_07.png).
- **Predator X (Tech)**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 3; VP 2; keyword labels: Prey, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_06.png).
- **Predator X (Covert)**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 3; VP 2; keyword labels: Prey, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_05.png).
- **Predator X (Instinct)**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 3; VP 2; keyword labels: Prey, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_04.png).
- **Predator X (Strength)**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 3; VP 2; keyword labels: Prey, Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_03.png).
- **Leper Queen**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 4; VP 2; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_02.png).
- **Reverend William Stryker**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 5; VP 3; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_08.png).
- **Cameron Hodge**; type/group: Villain / Purifiers; copies: 1; printed values: Attack 6; VP 4; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/purifiers_01.png).

### Villain Group: Acolytes

- **Unuscione**; type/group: Villain / Acolytes; copies: 2; printed values: Attack 8*; VP 4; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/acolytes_04.png).
- **Tempo**; type/group: Villain / Acolytes; copies: 2; printed values: Attack 16*; VP 2; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/acolytes_03.png).
- **Frenzy**; type/group: Villain / Acolytes; copies: 2; printed values: Attack 12*; VP 3; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/acolytes_01.png).
- **Random**; type/group: Villain / Acolytes; copies: 2; printed values: Attack 10*; VP 4; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/acolytes_02.png).

### Villain Group: Clan Yashida

- **Silver Samurai**; type/group: Villain / Clan Yashida; copies: 2; printed values: Attack 3*; VP 3; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/clan_yashida_04.png).
- **Scarlet Samurai**; type/group: Trap / Clan Yashida; copies: 2; printed values: Attack 2; Attack 3*; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/clan_yashida_03.png).
- **Lord Shingen**; type/group: Villain / Clan Yashida; copies: 2; printed values: Attack 4*; VP 5; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/clan_yashida_02.png).
- **Gorgon**; type/group: Villain / Clan Yashida; copies: 2; printed values: Attack 5*; VP 4; keyword labels: Chivalrous Duel; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/clan_yashida_01.png).

### Henchman Group: Mr. Sinister Clones

- **Mr. Sinister Clones**; type/group: Henchman / Mr. Sinister Clones; copies: Unverified; printed values: not indexed in C1; keyword labels: Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mc_henchman_mr_sinister_clones.png).

### Henchman Group: Sentinel Squad O∗N∗E∗

- **Sentinel Squad O∗N∗E∗**; type/group: Henchman / Sentinel Squad O∗N∗E∗; copies: Unverified; printed values: not indexed in C1; keyword labels: Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/mc_henchman_sentinel_squad_one.png).

### Mastermind: Lady Deathstrike

- **Lady Deathstrike**; type/group: Normal Mastermind face / Lady Deathstrike; copies: Unverified; printed values: VP 6; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/lady_deathstrike_01.png).
- **Epic Lady Deathstrike**; type/group: Epic Mastermind face / Lady Deathstrike; copies: Unverified; printed values: Attack 11; VP 6; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/lady_deathstrike_02.png).
- **Cybernetic Healing Factor**; type/group: Mastermind Tactic / Lady Deathstrike; copies: Unverified; printed values: not indexed in C1; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/lady_deathstrike_03.png).
- **Prey on the Weak**; type/group: Mastermind Tactic / Lady Deathstrike; copies: Unverified; printed values: not indexed in C1; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/lady_deathstrike_04.png).
- **Relentless Assassin**; type/group: Mastermind Tactic / Lady Deathstrike; copies: Unverified; printed values: not indexed in C1; keyword labels: Prey; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/lady_deathstrike_05.png).
- **Stretching Adamantium Claws**; type/group: Mastermind Tactic / Lady Deathstrike; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/lady_deathstrike_06.png).

### Mastermind: Bastion, Fused Sentinel

- **Bastion, Fused Sentinel**; type/group: Normal Mastermind face / Bastion, Fused Sentinel; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/bastion_01.png).
- **Epic Bastion, Fused Sentinel**; type/group: Epic Mastermind face / Bastion, Fused Sentinel; copies: Unverified; printed values: Attack 6+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/bastion_02.png).
- **Master Mold, Sentinel Factory**; type/group: Mastermind Tactic / Bastion, Fused Sentinel; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/bastion_04.png).
- **Template, Infected Sentinel**; type/group: Mastermind Tactic / Bastion, Fused Sentinel; copies: Unverified; printed values: Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/bastion_06.png).
- **Nimrod, Future Sentinel**; type/group: Mastermind Tactic / Bastion, Fused Sentinel; copies: Unverified; printed values: Attack 6+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/bastion_05.png).
- **Machine Man, Sentinel Supereme**; type/group: Mastermind Tactic / Bastion, Fused Sentinel; copies: Unverified; printed values: Attack 7+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/bastion_03.png).

### Mastermind: Exodus

- **Exodus**; type/group: Normal Mastermind face / Exodus; copies: Unverified; printed values: VP 7; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/exodus_06.png).
- **Epic Exodus**; type/group: Epic Mastermind face / Exodus; copies: Unverified; printed values: Attack 36*; VP 7; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/exodus_02.png).
- **Unite All Mutantkind**; type/group: Mastermind Tactic / Exodus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/exodus_01.png).
- **Omega-Level Mutant**; type/group: Mastermind Tactic / Exodus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/exodus_04.png).
- **Avalon, Asteroid Haven**; type/group: Mastermind Tactic / Exodus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/exodus_03.png).
- **Resurrect the Dead**; type/group: Mastermind Tactic / Exodus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/exodus_05.png).

### Scheme: Hack Cerebro Servers To...

- **Hack Cerebro Servers To...**; type/group: Scheme (Veiled face) / Hack Cerebro Servers To...; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_02.png).

### Scheme: ...Control the Mutant Messiah

- **...Control the Mutant Messiah**; type/group: Scheme (Unveiled face) / ...Control the Mutant Messiah; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_02_1.png).

### Scheme: Drain Mutant Powers To...

- **Drain Mutant Powers To...**; type/group: Scheme (Veiled face) / Drain Mutant Powers To...; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_01.png).

### Scheme: ...Open Rifts to Future Timelines

- **...Open Rifts to Future Timelines**; type/group: Scheme (Unveiled face) / ...Open Rifts to Future Timelines; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_01_1.png).

### Scheme: Hire Singularity Investigations To...

- **Hire Singularity Investigations To...**; type/group: Scheme (Veiled face) / Hire Singularity Investigations To...; copies: Unverified; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_03.png).

### Scheme: ...Reveal The Heroes' Evil Clones

- **...Reveal The Heroes' Evil Clones**; type/group: Scheme (Unveiled face) / ...Reveal The Heroes' Evil Clones; copies: Unverified; printed values: not indexed in C1; keyword labels: Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_03_1.png).

### Scheme: Raid Gene Banks To...

- **Raid Gene Banks To...**; type/group: Scheme (Veiled face) / Raid Gene Banks To...; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_04.png).

### Scheme: ...Unleash an Anti-Mutant Bioweapon

- **...Unleash an Anti-Mutant Bioweapon**; type/group: Scheme (Unveiled face) / ...Unleash an Anti-Mutant Bioweapon; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/mc_scheme_04_1.png).

### Bystander set: Cloning Technician

- **Cloning Technician**; type/group: Bystander / Cloning Technician; copies: 1; printed values: not indexed in C1; keyword labels: Clone; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/mc_bystander_cloning_technician.png).

### Bystander set: Opera Singer

- **Opera Singer**; type/group: Bystander / Opera Singer; copies: 1; printed values: not indexed in C1; keyword labels: Shatter; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/mc_bystander_opera_singer.png).

### Bystander set: Private Investigator

- **Private Investigator**; type/group: Bystander / Private Investigator; copies: 1; printed values: not indexed in C1; keyword labels: Investigate; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/mc_bystander_private_investigator.png).

### Sidekick set: Layla Miller

- **Layla Miller**; type/group: Sidekick / Layla Miller; copies: 2; printed values: Cost 2; Attack 1; keyword labels: Investigate; card image: unavailable in C1.

### Sidekick set: Skids

- **Skids**; type/group: Sidekick / Skids; copies: 2; printed values: Cost 2; Recruit 3; card image: unavailable in C1.

### Sidekick set: Rockslide

- **Rockslide**; type/group: Sidekick / Rockslide; copies: 2; printed values: Cost 2; keyword labels: Shatter; card image: unavailable in C1.

### Sidekick set: Darwin

- **Darwin**; type/group: Sidekick / Darwin; copies: 2; printed values: Cost 2; Recruit 0+; Attack 0+; card image: unavailable in C1.

### Sidekick set: Boom-Boom

- **Boom-Boom**; type/group: Sidekick / Boom-Boom; copies: 2; printed values: Cost 2; Attack 0+; card image: unavailable in C1.

### Sidekick set: Prodigy

- **Prodigy**; type/group: Sidekick / Prodigy; copies: 2; printed values: Cost 2; card image: unavailable in C1.

### Sidekick set: Rusty “Firefist“ Collins

- **Rusty “Firefist“ Collins**; type/group: Sidekick / Rusty “Firefist“ Collins; copies: 2; printed values: Cost 2; Attack 1; keyword labels: Investigate; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
