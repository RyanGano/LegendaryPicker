# Weapon X (Oct 2024)

**Research status: Partial.** The official insert verifies contents, Berserk, Weapon X Sequence, and the Enraging Wound supply; Upper Deck's article clarifies two Schemes. The third Scheme, full Mastermind setup text, and card-level Hero metadata remain open.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| WX | [Upper Deck Weapon X rules insert](https://upperdeck.com/wp-content/uploads/2024/12/Marvel_WeaponX_Rulesheet.pdf) | Component counts, Berserk, Weapon X Sequence, Enraging Wounds, and setup of the shared Wound Deck (PDF pp.1–2). |
| WXA | [Upper Deck Weapon X article](https://upperdeck.com/legendary-weapon-x/) | Additional Berserk/Sequence clarifications and effects of two Schemes. |
| UD | [Upper Deck Weapon X product listing](https://upperdeckstore.com/legendary-weapon-x.html) | 100-card expansion total and 1–5 player range. |
| C1 | [master-strike structured weapon-x card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/weaponx.ts) | Per-face structured fields are indexed below; C1 provides no direct image URLs for these records, and its ability prose is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, expansion status, Revised ruleset classification. |

## Catalog inventory

### Official contents (WX p.2)

| Type | Official count |
|---|---:|
| Heroes | 4 groups × 14 = 56 |
| Villain Groups | 2 groups × 8 = 16 |
| Double-Sided Epic Masterminds | 3 sets × 5 = 15 |
| Enraging Wounds | 10 unique cards |
| Schemes | 3 |
| **Total** | **100** |

Each Hero has 1 rare, 3 uncommons, and 5 copies each of two commons (WX p.2). The itemized categories sum to the product's stated 100 cards. The insert lists no Henchman Group.

### Group inventory (C1)

- **Heroes (four):** Fantomex; Marrow; Weapon H; Weapon X (Wolverine).
- **Villain Groups (two):** Berserkers; Weapon Plus.
- **Masterminds (three):** Omega Red; Romulus; Sabretooth.
- **Schemes (three):** Condition Logan into Weapon X; Go After Heroes' Loved Ones; Wipe Heroes' Memories.
- **Enraging Wounds (ten):** Blazing Vengeance; Broken Bones; Concussion; Erratic Powers; Insults and Injuries; Last Breath; Massive Blood Loss; Shell Shock; Sudden Terror; Wild Rage.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. The insert and article provide rules and two Scheme clarifications; the third setup, Always Leads, and Loved Ones components remain open.

## Rules and setup facts

- **Berserk on a Hero (WX p.1):** Discard the deck's top card and gain Attack equal to its printed Attack. Multiple Berserk instances resolve one at a time.
- **Berserk on an Enemy (WX p.1):** When attempting to fight it, discard the deck's top card; the Enemy gains Attack equal to that card's printed Attack. If your total is sufficient, fight normally. If not, you lose your Attack, cannot fight further this turn, and do not resolve Fight/When You Fight/When You Defeat effects. You may still play cards and recruit. Do not attempt a fight without enough Attack to match the Enemy's printed value. Once the fight begins, play no more cards until it resolves.
- **Fail (WX p.1):** Some Berserk Enemies have a Fail effect, which resolves if the Berserk bonus makes an attempted fight fail.
- **Weapon X Sequence for Heroes (WX p.1; WXA):** Gain Attack for the longest run of distinct consecutive printed costs among cards in hand and already played. The run can begin at any value, including 0; duplicates do not extend it. Later card draws cannot retroactively improve a Sequence already used.
- **Weapon X Sequence for Enemies (WX p.2; WXA):** An Enemy gains Attack for the longest run of distinct consecutive printed costs represented in the HQ. Recruiting a Hero can break or change the run. “Doubled Weapon X Sequence” doubles the bonus (WX p.1).
- **Enraging Wounds (WX p.2; WXA):** Shuffle all ten into the Wound Deck with normal Wounds, for at least 40 total Wounds; include other special Wounds in the supply too. Enraging Wounds can be played from hand for their printed effects and have individual Healing conditions. Their KO effects do not prevent recruiting or fighting that turn. They still count as Wounds for other card effects.
- **Go After Heroes' Loved Ones (WXA):** Each Scheme Twist removes a Loved One from the Hero in the rightmost HQ space. A Hero that loses all Loved Ones is removed from the HQ and Hero Deck. The article does not specify the Scheme's Twist count or all setup text.
- **Wipe Heroes' Memories (WXA):** On a Scheme Twist, a Villain or Mastermind Tactic from the player's Victory Pile is turned face down and moved to its bottom, then the Twist is shuffled back into the Villain Deck. If there is no such “memory,” set the Twist beside the Scheme as a Total Memory Wipe.

## Required parts and glossary

- **Wound Deck:** Combine the ten Enraging Wounds with normal and any other special Wounds, with at least 40 cards total (WX p.2).
- **Hero Deck and Victory Pile:** Berserk and Weapon X Sequence use the deck top, hand/played cards, or Victory Pile as described above; Scheme effects also use the Hero Deck and Victory Pile (WX pp.1–2; WXA).
- **Berserk:** A Hero or Enemy discards a deck-top card and changes Attack based on its printed value. (WX p.1)
- **Fail:** Resolve an Enemy's Fail effect when a Berserk fight attempt fails. (WX p.1)
- **Weapon X Sequence:** Gain Attack based on a longest consecutive run of distinct printed costs, using the Hero's cards or the HQ according to card type. (WX pp.1–2)
- **Enraging Wound:** A Wound with its own Healing condition and playable effects; it remains a Wound for other effects. (WX p.2)
- **Loved One / Lost in Grief:** A Scheme state tracked on the rightmost HQ Hero; losing all Loved Ones removes that Hero from the HQ and Hero Deck. (WXA)

The insert specifies no new token or separate shared stack. The Loved Ones tracking method and its physical components need card-level verification.

## Setup and implementation gaps

Verify the full card text and setup for Condition Logan into Weapon X; all three Masterminds' Always Leads/setup effects; complete Hero metadata; the Loved Ones components and exact Scheme setup; and any additional card-driven shared parts. The rules insert and article support the facts above but do not provide full card-level setup data. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Fantomex

- **Sentient Bullets**; type/group: Hero / Fantomex; copies: Unverified; Hero Name: Fantomex; team: X-Force; class icons: Ranged; printed values: Cost 1; Attack 0+; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Three Brains**; type/group: Hero / Fantomex; copies: Unverified; Hero Name: Fantomex; team: X-Force; class icons: Tech; printed values: Cost 2; Recruit 0+; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Misdirection**; type/group: Hero / Fantomex; copies: Unverified; Hero Name: Fantomex; team: X-Force; class icons: Covert; printed values: Cost 3; Attack 0+; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Weapon XIII**; type/group: Hero / Fantomex; copies: Unverified; Hero Name: Fantomex; team: X-Force; class icons: Tech; printed values: Cost 4; Attack 1+; keyword labels: Weapon X Sequence; card image: unavailable in C1.

### Hero group: Marrow

- **Bone Shards**; type/group: Hero / Marrow; copies: Unverified; Hero Name: Marrow; team: X-Force; class icons: Ranged; printed values: Cost 3; Attack 2+; keyword labels: Berserk; card image: unavailable in C1.
- **Hyper-Adaptive Skeleton**; type/group: Hero / Marrow; copies: Unverified; Hero Name: Marrow; team: X-Force; class icons: Strength; printed values: Cost 2; Recruit 1+; Attack 1+; keyword labels: Berserk; card image: unavailable in C1.
- **Osteogenesis**; type/group: Hero / Marrow; copies: Unverified; Hero Name: Marrow; team: X-Force; class icons: Strength; printed values: Cost 6; Recruit 1; Attack 2; card image: unavailable in C1.
- **Metabolic Overdrive**; type/group: Hero / Marrow; copies: Unverified; Hero Name: Marrow; team: X-Force; class icons: Covert; printed values: Cost 7; Attack 3+; keyword labels: Weapon X Sequence; card image: unavailable in C1.

### Hero group: Weapon H

- **Evolving Abilities**; type/group: Hero / Weapon H; copies: Unverified; Hero Name: Weapon H; team: Avengers; class icons: Instinct; printed values: Cost 4; Recruit 0+; Attack 2+; card image: unavailable in C1.
- **The Future of Warfare**; type/group: Hero / Weapon H; copies: Unverified; Hero Name: Weapon H; team: Avengers; class icons: Strength; printed values: Cost 3; Attack 2+; keyword labels: Berserk; card image: unavailable in C1.
- **Slice and Smash**; type/group: Hero / Weapon H; copies: Unverified; Hero Name: Weapon H; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 2+; keyword labels: Berserk; card image: unavailable in C1.
- **Ultimate Killing Machine**; type/group: Hero / Weapon H; copies: Unverified; Hero Name: Weapon H; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 4+; keyword labels: Weapon X Sequence, Berserk; card image: unavailable in C1.

### Hero group: Weapon X (Wolverine)

- **Infuse Skeleton with Adamantium**; type/group: Hero / Weapon X (Wolverine); copies: Unverified; Hero Name: Weapon X (Wolverine); team: Marvel Knights; class icons: Tech; printed values: Cost 5; Attack 2+; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Raging Regeneration**; type/group: Hero / Weapon X (Wolverine); copies: Unverified; Hero Name: Weapon X (Wolverine); team: Marvel Knights; class icons: Instinct; printed values: Cost 4; Attack 2+; keyword labels: Berserk; card image: unavailable in C1.
- **Violent Conditioning**; type/group: Hero / Weapon X (Wolverine); copies: Unverified; Hero Name: Weapon X (Wolverine); team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Attack 2+; keyword labels: Berserk; card image: unavailable in C1.
- **Escape the Weapon X Lab**; type/group: Hero / Weapon X (Wolverine); copies: Unverified; Hero Name: Weapon X (Wolverine); team: Marvel Knights; class icons: Instinct; printed values: Cost 6; Attack 3+; keyword labels: Weapon X Sequence, Berserk; card image: unavailable in C1.

### Villain Group: Berserkers

- **Cyber**; type/group: Villain / Berserkers; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Berserk, Fail; card image: unavailable in C1.
- **Feral**; type/group: Trap / Berserkers; copies: 3; printed values: Attack 2+; Attack 3+; keyword labels: Berserk, Fail; card image: unavailable in C1.
- **Thornn**; type/group: Villain / Berserkers; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Berserk, Fail; card image: unavailable in C1.
- **Wild Child**; type/group: Villain / Berserkers; copies: 2; printed values: Attack 3+; VP 4; keyword labels: Berserk, Fail; card image: unavailable in C1.

### Villain Group: Weapon Plus

- **Daken**; type/group: Villain / Weapon Plus; copies: 1; printed values: Attack 3+; VP 4; keyword labels: Weapon X Sequence, Berserk, Fail; card image: unavailable in C1.
- **Huntsman (Weapon XII)**; type/group: Villain / Weapon Plus; copies: 1; printed values: Attack 2+; VP 2; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Nuke (Weapon VII)**; type/group: Villain / Weapon Plus; copies: 2; printed values: Attack 2+; VP 2; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Skinless Man (Weapon III)**; type/group: Villain / Weapon Plus; copies: 1; printed values: Attack 3+; VP 3; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Typhoid Mary (Weapon IX)**; type/group: Villain / Weapon Plus; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Ultimaton (Weapon XV)**; type/group: Villain / Weapon Plus; copies: 1; printed values: Attack 4+; VP 6; keyword labels: Weapon X Sequence; card image: unavailable in C1.

### Mastermind: Omega Red

- **Omega Red**; type/group: Normal Mastermind face / Omega Red; copies: Unverified; printed values: Attack 10+; VP 6; card image: unavailable in C1.
- **Epic Omega Red**; type/group: Epic Mastermind face / Omega Red; copies: Unverified; printed values: Attack 12+; VP 6; card image: unavailable in C1.
- **Carbonadium Tentacles**; type/group: Mastermind Tactic / Omega Red; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **The Carbonadium Synthesizer**; type/group: Mastermind Tactic / Omega Red; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Death Pheromones**; type/group: Mastermind Tactic / Omega Red; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Drain Life Force**; type/group: Mastermind Tactic / Omega Red; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Mastermind: Romulus

- **Romulus**; type/group: Normal Mastermind face / Romulus; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Epic Romulus**; type/group: Epic Mastermind face / Romulus; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Weapon X Sequence; card image: unavailable in C1.
- **Anoint an Heir to Take My Place**; type/group: Mastermind Tactic / Romulus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Bite of the Muramasa Blade**; type/group: Mastermind Tactic / Romulus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Master of Schemes**; type/group: Mastermind Tactic / Romulus; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Take Over the Weapon X Program**; type/group: Mastermind Tactic / Romulus; copies: Unverified; printed values: not indexed in C1; keyword labels: Weapon X Sequence; card image: unavailable in C1.

### Mastermind: Sabretooth

- **Sabretooth**; type/group: Normal Mastermind face / Sabretooth; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Berserk, Fail; card image: unavailable in C1.
- **Epic Sabretooth**; type/group: Epic Mastermind face / Sabretooth; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Berserk, Fail; card image: unavailable in C1.
- **Adamantium-Laced Claws**; type/group: Mastermind Tactic / Sabretooth; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Lethal Fangs**; type/group: Mastermind Tactic / Sabretooth; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Salivating Prowl**; type/group: Mastermind Tactic / Sabretooth; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.
- **Sudden Savagery**; type/group: Mastermind Tactic / Sabretooth; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Condition Logan into Weapon X

- **Condition Logan into Weapon X**; type/group: Scheme / Condition Logan into Weapon X; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Scheme: Go After Heroes' Loved Ones

- **Go After Heroes' Loved Ones**; type/group: Scheme / Go After Heroes' Loved Ones; copies: Unverified; printed values: not indexed in C1; keyword labels: Berserk; card image: unavailable in C1.

### Scheme: Wipe Heroes' Memories

- **Wipe Heroes' Memories**; type/group: Scheme / Wipe Heroes' Memories; copies: Unverified; printed values: not indexed in C1; card image: unavailable in C1.

### Wound set: Blazing Vengeance

- **Blazing Vengeance**; type/group: Wound / Blazing Vengeance; copies: 1; printed values: Cost 0; Attack 2; card image: unavailable in C1.

### Wound set: Broken Bones

- **Broken Bones**; type/group: Wound / Broken Bones; copies: 1; printed values: Cost 0; Attack 3; card image: unavailable in C1.

### Wound set: Concussion

- **Concussion**; type/group: Wound / Concussion; copies: 1; printed values: Cost 0; Attack 2; card image: unavailable in C1.

### Wound set: Erratic Powers

- **Erratic Powers**; type/group: Wound / Erratic Powers; copies: 1; printed values: Cost 0; Attack 3; card image: unavailable in C1.

### Wound set: Insults and Injuries

- **Insults and Injuries**; type/group: Wound / Insults and Injuries; copies: 1; printed values: Cost 0; Attack 2; card image: unavailable in C1.

### Wound set: Last Breath

- **Last Breath**; type/group: Wound / Last Breath; copies: 1; printed values: Cost 0; Attack 4; card image: unavailable in C1.

### Wound set: Massive Blood Loss

- **Massive Blood Loss**; type/group: Wound / Massive Blood Loss; copies: 1; printed values: Cost 0; Attack 3; card image: unavailable in C1.

### Wound set: Shell Shock

- **Shell Shock**; type/group: Wound / Shell Shock; copies: 1; printed values: Cost 0; Recruit 1; Attack 1; card image: unavailable in C1.

### Wound set: Sudden Terror

- **Sudden Terror**; type/group: Wound / Sudden Terror; copies: 1; printed values: Cost 0; Recruit 2; card image: unavailable in C1.

### Wound set: Wild Rage

- **Wild Rage**; type/group: Wound / Wild Rage; copies: 1; printed values: Cost 0; Attack 2; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
