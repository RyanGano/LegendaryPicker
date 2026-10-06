# Venom (March 2019)

**Research status: Partial; integrated (#165).** The official insert verifies contents and the new mechanics. The Scheme Setup lines, Always Leads and part uses in `LegendaryPickerService/Data/Boxes/venom.json` were read from the C1-linked card faces (`Card`): Hybrid, Poison Thanos and Life Foundation give Wounds, and no Hero or other card uses another part. Per-face copy counts remain unverified.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| V | [Upper Deck Venom rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Legendary_Rules-Venom.pdf) | Contents (PDF p.2), Symbiote Bonds, Digest, Indigestion, and Excessive Violence rules (PDF pp.1–2). |
| C1 | [master-strike structured venom card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/venom.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | March 2019 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (V p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Carnage; Venom; Venom Rocket; Venomized Dr. Strange; Venompool.
- **Villain Groups (two):** Life Foundation; Poisons.
- **Masterminds (two):** Hybrid; Poison Thanos. Each includes a normal and Epic side with the same four Tactics (V p.2).
- **Schemes (four):** Invasion of the Venom Symbiotes; Maximum Carnage; Paralyzing Venom; Symbiotic Absorption.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, Scheme setup lines, Mastermind setup effects, or component dependencies.

## Rules and mechanisms

- **Symbiote Bonds (V p.1):** Attach one Villain to another in the same city space; the combined enemy has both cards' values and abilities and counts as both groups. It cannot include a third Villain. Fighting it requires their combined value, rescues all held Bystanders, defeats one chosen card and resolves its Fight effect; the other remains in the city. Only fighting separates the pair.
- **Bonded Villain escape (V p.1):** The pair causes one escape and one standard HQ KO, while each card's Escape ability resolves. The two cards become unattached in the Escape Pile.
- **Poison Villain clarification (V p.2):** A Poison Villain can bond during its Fight effect; if it cannot bond as instructed, the example card is gained as a Hero instead.
- **Digest (V p.2):** Use a card's Digest effect only when the Victory Pile contains at least the stated number of cards. Every card type there counts; cards are not spent, and each Digest ability is used once.
- **Indigestion (V p.2):** When its Digest threshold is unmet, use its Indigestion effect instead. If the threshold is met, Indigestion is not an alternative choice.
- **Excessive Violence (V p.2):** Once per turn, spend one extra Attack when fighting a Villain or Mastermind to activate all Excessive Violence abilities on cards already played that turn.
- **Cross-set clarification (V p.1):** When fighting a Combined Villain with X-Men's Piercing Energy, use the total Victory Points of both attached cards.

## Required parts and glossary

Symbiote Bonds uses Villain cards in city spaces; the insert lists no new shared stack or token. The product's card-level dependencies still need verification.

- **Symbiote Bonds:** Attach two Villains as one enemy until it is fought. (V p.1)
- **Digest:** A card effect available when the Victory Pile meets its stated size threshold. (V p.2)
- **Indigestion:** An alternate effect used when the related Digest threshold is not met. (V p.2)
- **Excessive Violence:** A once-per-turn bonus triggered by spending one extra Attack on a fight. (V p.2)
- **Combined Villain:** Two bonded Villains fought together, with one card defeated at a time. (V p.1)

Summaries are original paraphrases under 40 words. Hero teams/classes/shared Hero Names and other card terms require card-level verification.

## Setup and implementation gaps

Every Scheme's Twists and Setup line, and both Masterminds' Always Leads and Epic side, were read from the card faces linked below and are in the box file (#165); no Scheme prints a player limit. Symbiotic Absorption sets a Drained Mastermind aside and adds its Always Leads group as an extra Villain Group, recorded as `bringsAlwaysLeads` on its set-aside Mastermind. Hero teams and classes are as C1 gives them. Symbiote Bonds itself is not modelled; the checklist only names the keyword.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Carnage

- **Rending Claws**; type/group: Hero / Carnage; copies: Unverified; Hero Name: Carnage; team: Venomverse; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/carnage-03.png).
- **Carnivore**; type/group: Hero / Carnage; copies: Unverified; Hero Name: Carnage; team: Venomverse; class icons: Strength; printed values: Cost 4; Recruit 0+; keyword labels: Digest, Indigestion; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/carnage-04.png).
- **Gruesome Feast**; type/group: Hero / Carnage; copies: Unverified; Hero Name: Carnage; team: Venomverse; class icons: Covert; printed values: Cost 6; Attack 3; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/carnage-02.png).
- **Feast or Famine**; type/group: Hero / Carnage; copies: Unverified; Hero Name: Carnage; team: Venomverse; class icons: Covert; printed values: Cost 8; Attack 6; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/carnage-01.png).

### Hero group: Venom

- **Devouring Drool**; type/group: Hero / Venom; copies: Unverified; Hero Name: Venom; team: Venomverse; class icons: Instinct; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Digest, Indigestion; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-vn-04.png).
- **Razor Teeth**; type/group: Hero / Venom; copies: Unverified; Hero Name: Venom; team: Venomverse; class icons: Strength; printed values: Cost 4; Recruit 0+; Attack 2; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-vn-03.png).
- **Symbiotic Adaptation**; type/group: Hero / Venom; copies: Unverified; Hero Name: Venom; team: Venomverse; class icons: Instinct; printed values: Cost 6; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-vn-02.png).
- **Insatiable Hunger**; type/group: Hero / Venom; copies: Unverified; Hero Name: Venom; team: Venomverse; class icons: Instinct; printed values: Cost 8; Recruit 0+; Attack 0+; keyword labels: Digest, Indigestion; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-vn-01.png).

### Hero group: Venom Rocket

- **Hungry for Action**; type/group: Hero / Venom Rocket; copies: Unverified; Hero Name: Venom Rocket; team: Venomverse; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: Digest; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-rocket-04.png).
- **Spring the Trap**; type/group: Hero / Venom Rocket; copies: Unverified; Hero Name: Venom Rocket; team: Venomverse; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-rocket-03.png).
- **Serious Overkill**; type/group: Hero / Venom Rocket; copies: Unverified; Hero Name: Venom Rocket; team: Venomverse; class icons: Ranged; printed values: Cost 5; Attack 2; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-rocket-02.png).
- **Ultimate Survivor**; type/group: Hero / Venom Rocket; copies: Unverified; Hero Name: Venom Rocket; team: Venomverse; class icons: Tech; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-rocket-01.png).

### Hero group: Venomized Dr. Strange

- **Cauldron of the Cosmos**; type/group: Hero / Venomized Dr. Strange; copies: Unverified; Hero Name: Venomized Dr. Strange; team: Venomverse; class icons: Ranged; printed values: Cost 2; Recruit 1; keyword labels: Digest; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venomized-dr-strange-03.png).
- **See Future Timelines**; type/group: Hero / Venomized Dr. Strange; copies: Unverified; Hero Name: Venomized Dr. Strange; team: Venomverse; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venomized-dr-strange-04.png).
- **Complete the Grand Ritual**; type/group: Hero / Venomized Dr. Strange; copies: Unverified; Hero Name: Venomized Dr. Strange; team: Venomverse; class icons: Instinct; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venomized-dr-strange-02.png).
- **Crystal of Kadavus**; type/group: Hero / Venomized Dr. Strange; copies: Unverified; Hero Name: Venomized Dr. Strange; team: Venomverse; class icons: Ranged; printed values: Cost 8; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venomized-dr-strange-01.png).

### Hero group: Venompool

- **Digest That Chimichanga**; type/group: Hero / Venompool; copies: Unverified; Hero Name: Venompool; team: Venomverse; class icons: Strength; printed values: Cost 2; Attack 0+; keyword labels: Digest, Indigestion; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venompool-04.png).
- **Shenanigans**; type/group: Hero / Venompool; copies: Unverified; Hero Name: Venompool; team: Venomverse; class icons: Tech; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venompool-03.png).
- **Can I Get a Little Gratitude?**; type/group: Hero / Venompool; copies: Unverified; Hero Name: Venompool; team: Venomverse; class icons: Instinct; printed values: Cost 5; Attack 3; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venompool-02.png).
- **Play to the Crowd**; type/group: Hero / Venompool; copies: Unverified; Hero Name: Venompool; team: Venomverse; class icons: Strength; printed values: Cost 7; Attack 4+; keyword labels: Digest, Indigestion; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venompool-01.png).

### Villain Group: Life Foundation

- **Agony**; type/group: Villain / Life Foundation; copies: 1; printed values: Attack 3; VP 3; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/life-foundation-02.png).
- **Dr. Carlton Drake**; type/group: Villain / Life Foundation; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/life-foundation-03.png).
- **Lasher**; type/group: Villain / Life Foundation; copies: 2; printed values: Attack 2; VP 2; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/life-foundation-01-1.png).
- **Phage**; type/group: Villain / Life Foundation; copies: 1; printed values: Attack 3; VP 3; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/life-foundation-06.png).
- **Riot**; type/group: Villain / Life Foundation; copies: 2; printed values: Attack 2; VP 2; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/life-foundation-04.png).
- **Scream**; type/group: Villain / Life Foundation; copies: 1; printed values: Attack 4; VP 4; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/life-foundation-05.png).

### Villain Group: Poisons

- **Poison Captain America**; type/group: Trap / Poisons; copies: 1; printed values: Attack 0+; Attack 4; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-03.png).
- **Poison Dr. Octopus**; type/group: Trap / Poisons; copies: 1; printed values: Attack 1; Attack 3; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-07.png).
- **Poison Hulk**; type/group: Trap / Poisons; copies: 1; printed values: Attack 2+; Attack 5; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-02.png).
- **Poison Sabretooth**; type/group: Trap / Poisons; copies: 1; printed values: Attack 2; Attack 4; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-05.png).
- **Poison Scarlet Witch**; type/group: Trap / Poisons; copies: 1; printed values: Attack 2; Attack 3; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-06.png).
- **Poison Spider-Man**; type/group: Trap / Poisons; copies: 1; printed values: Attack 2; Attack 2; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-08.png).
- **Poison Storm**; type/group: Trap / Poisons; copies: 1; printed values: Attack 2+; Attack 3; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-04.png).
- **Symbiotic Armor**; type/group: Villain / Poisons; copies: 1; printed values: Attack 1; VP 6; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/poisons-01.png).

### Mastermind: Hybrid

- **Hybrid**; type/group: Normal Mastermind face / Hybrid; copies: Unverified; printed values: VP 6; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hybrid-01.png).
- **Epic Hybrid**; type/group: Epic Mastermind face / Hybrid; copies: Unverified; printed values: Attack 8; VP 6; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hybrid-02.png).
- **Alien Awakening**; type/group: Mastermind Tactic / Hybrid; copies: Unverified; printed values: not indexed in C1; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hybrid-03.png).
- **Escaped Monstrosity**; type/group: Mastermind Tactic / Hybrid; copies: Unverified; printed values: not indexed in C1; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hybrid-04.png).
- **Life Foundation Research**; type/group: Mastermind Tactic / Hybrid; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hybrid-05.png).
- **Symbiotic Call**; type/group: Mastermind Tactic / Hybrid; copies: Unverified; printed values: not indexed in C1; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hybrid-06.png).

### Mastermind: Poison Thanos

- **Poison Thanos**; type/group: Normal Mastermind face / Poison Thanos; copies: Unverified; printed values: Attack 12+; VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/poison-thanos-01.png).
- **Epic Poison Thanos**; type/group: Epic Mastermind face / Poison Thanos; copies: Unverified; printed values: Attack 13+; VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/poison-thanos-02.png).
- **Desperate Rescue**; type/group: Mastermind Tactic / Poison Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/poison-thanos-06.png).
- **Poisoned Loyalties**; type/group: Mastermind Tactic / Poison Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/poison-thanos-03.png).
- **Searing Poisons**; type/group: Mastermind Tactic / Poison Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/poison-thanos-05.png).
- **Soul Seize**; type/group: Mastermind Tactic / Poison Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/poison-thanos-04.png).

### Scheme: Invasion of the Venom Symbiotes

- **Invasion of the Venom Symbiotes**; type/group: Scheme / Invasion of the Venom Symbiotes; copies: Unverified; printed values: not indexed in C1; keyword labels: Symbiote Bonds; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/100Scheme(9).png).

### Scheme: Maximum Carnage

- **Maximum Carnage**; type/group: Scheme / Maximum Carnage; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/101Scheme(10).png).

### Scheme: Paralyzing Venom

- **Paralyzing Venom**; type/group: Scheme / Paralyzing Venom; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/102Scheme(11).png).

### Scheme: Symbiotic Absorption

- **Symbiotic Absorption**; type/group: Scheme / Symbiotic Absorption; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/103Scheme(12).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
