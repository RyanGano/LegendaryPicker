# Fear Itself (March 2015)

**Research status: Partial; integrated (#96).** The official insert verifies component totals, several mechanics, and Heroic-set substitutions. The three Plot fronts and the Commander front linked below were read for #96: they give each Plot's Twists and Setup line, The Traitor's 2+ player limit, and Always Leads The Mighty. Ally face copy counts remain unverified. Runtime data: `LegendaryPickerService/Data/Boxes/fear-itself.json`.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| FI | [Upper Deck Fear Itself rules insert](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Fear_Itself.pdf) | Contents and counts (PDF p.2); Thrown Artifacts and Uru-Enchanted Weapons (PDF pp.1–2); compatibility and card clarifications (PDF p.2). Page numbers here are PDF pages. |
| VIL | [Upper Deck Legendary: Villains rulebook](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf) | Villainous terms and the Overrun rule (p.9); Ally types and teams (p.21). |
| C1 | [master-strike structured fear-itself card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/fearitself.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| Card | Community-hosted scans of the printed card fronts at [Bageltop CardImages](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/) | Printed titles, values, terms, and card effects; each face is cited by its direct image URL below. The catalog's text fields are not used as evidence. |
| #96 | [Project issue: Add Fear Itself (2015), a Villainous expansion](https://github.com/RyanGano/LegendaryPicker/issues/96) | Collection lead only; its two-player limit and Binding-deck setup claims are not treated as evidence. |
| Project queue | [`Docs/BoxResearch/README.md`](README.md), release order tracked by [#34](https://github.com/RyanGano/LegendaryPicker/issues/34) | March 2015 release date, expansion status, and Villainous ruleset classification. |

## Catalog inventory

### Official contents

FI p.2 lists 100 cards:

| Type | Official count |
|---|---:|
| Allies | 6 groups × 14 cards = 84 |
| Adversary Group | 1 group × 8 unique cards = 8 |
| Commander and Tactics | 1 Commander + 4 Tactics = 5 |
| Plots | 3 |
| Total cards | 100 |

These category totals reconcile to the official total.

### Card-by-card catalog

C1 structured records now index face metadata below. The separately linked card-front scans support the concise original setup/ability summaries; C1 ability prose is not used as evidence.

#### Ally groups and shared Hero Names

| Printed Hero Name / Ally group | Team | Classes |
|---|---|---|
| Greithoth, Breaker of Wills | Symbol visible; label unverified | Symbols visible on faces; names unverified |
| Kuurth, Breaker of Stone | Symbol visible; label unverified | Symbols visible on faces; names unverified |
| Nerkkod, Breaker of Oceans | Symbol visible; label unverified | Symbols visible on faces; names unverified |
| Nul, Breaker of Worlds | Symbol visible; label unverified | Symbols visible on faces; names unverified |
| Skadi | Symbol visible; label unverified | Symbols visible on faces; names unverified |
| Skirn, Breaker of Men | Symbol visible; label unverified | Symbols visible on faces; names unverified |

FI p.2 confirms 14 cards per Ally group (one rare, three uncommon, and five copies of each of two common faces). Four distinct faces are scanned for each group, but the insert does not map a title to its rarity; per-face copy counts remain unverified.

| Hero Name / Ally face | Copies | Printed values | Terms and concise ability summary | Card scan |
|---|---|---|---|---|
| Greithoth, Breaker of Wills — Body of Uru | Unverified | Cost 7; Attack 4+ | Gain +1 Attack for each Artifact controlled by players and each Artifact in the Lair. | [greithoth-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-01.png) |
| Greithoth, Breaker of Wills — Break the Will to Resist | Unverified | Cost 5; Attack 3+ | Each player discards the bottom card of their deck; gain +1 Attack for each non-grey card discarded. The displayed class-icon label is unverified. | [greithoth-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-02.png) |
| Greithoth, Breaker of Wills — Mace of Chains | Unverified | Cost 3 | Thrown Artifact; throwing it grants +2 Recruit. | [greithoth-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-03.png) |
| Greithoth, Breaker of Wills — Absorb Metal | Unverified | Cost 3; Attack 1+ | If you control an Artifact, gain +2 Attack. | [greithoth-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-04.png) |
| Kuurth, Breaker of Stone — Break Every Bone | Unverified | Cost 7; Attack 0+ | Reveal a hand card and the top and bottom cards of your deck; gain Attack equal to their combined costs. | [kuurth-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-01.png) |
| Kuurth, Breaker of Stone — Contest of Strength | Unverified | Cost 5; Attack 3+ | Discard the top card of any player's deck, then reveal your own top or bottom card; if its cost is at least as high, gain +2 Attack. | [kuurth-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-02.png) |
| Kuurth, Breaker of Stone — Reach for Power | Unverified | Cost 4; Attack 2+ | Reveal your top or bottom deck card; if it costs 4 or more, gain +2 Recruit. | [kuurth-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-03.png) |
| Kuurth, Breaker of Stone — Unstoppable Sledge | Unverified | Cost 4 | Thrown Artifact; throwing it grants +2 Attack. | [kuurth-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-04.png) |
| Nerkkod, Breaker of Oceans — Break Their Loyalties | Unverified | Cost 7; Attack 5 | Each other player reveals their hand; take a New Recruit or Madame HYDRA from each hand that contains one. | [nerkkod-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-01.png) |
| Nerkkod, Breaker of Oceans — Cudgel of the Deep | Unverified | Cost 5 | Thrown Artifact; grants +3 Attack usable only against an Adversary on the Bridge or the Commander. | [nerkkod-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-02.png) |
| Nerkkod, Breaker of Oceans — Feed My Undersea Legions | Unverified | Cost 4; Attack 2 | After defeating an Adversary on the Bridge this turn, you may KO one of your cards or a discard-pile card to gain a New Recruit. | [nerkkod-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-03.png) |
| Nerkkod, Breaker of Oceans — Pull of the Tides | Unverified | Cost 3; Recruit 2 | Move an Adversary to an adjacent city space; if occupied, swap the two Adversaries. | [nerkkod-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-04.png) |
| Nul, Breaker of Worlds — Break the World | Unverified | Cost 8; Attack 6 | KO up to two cards from hand or discard pile; for each Binding KO'd this way, Demolish each other player. | [nul-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-01.png) |
| Nul, Breaker of Worlds — Nul Smash! | Unverified | Cost 6; Attack 4 | Everyone slaps a palm on the table; the slowest other player gains a Binding. | [nul-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-02.png) |
| Nul, Breaker of Worlds — Demolition Derby | Unverified | Cost 3; Attack 2 | Choose a player to Demolish; if they discard a card, draw one. | [nul-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-03.png) |
| Nul, Breaker of Worlds — Otherworldly Maul | Unverified | Cost 4 | Thrown Artifact; gain +2 Attack per Ally played this turn with the displayed icon (icon label unverified). | [nul-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-04.png) |
| Skadi — War Banner of HYDRA | Unverified | Cost 7 | Thrown Artifact; gain +1 Attack per other Ally played this turn with the matching printed team icon. | [skadi-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-01.png) |
| Skadi — Hammer of the Serpent | Unverified | Cost 5 | Thrown Artifact; gain +2 Attack per card discarded this turn; throwing this Artifact is not a discard. | [skadi-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-02.png) |
| Skadi — Ancient Oath of HYDRA | Unverified | Cost 5; Attack 1 | You may discard an Ally with the matching displayed team icon to draw two cards. | [skadi-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-03.png) |
| Skadi — Dark Prophecy | Unverified | Cost 3; Recruit 2 | Its displayed icon-gated effect gains a Madame HYDRA; icon label unverified. | [skadi-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-04.png) |
| Skirn, Breaker of Men — Break Your Hopes | Unverified | Cost 7; Attack 4 | Each player reveals an Ally with the displayed icon or discards a card; draw one card per discard. | [skirn-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-01.png) |
| Skirn, Breaker of Men — Titanic Bludgeon | Unverified | Cost 2 | Thrown Artifact; gain +1 Attack per card drawn this turn, excluding the six-card end-of-turn draw. | [skirn-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-02.png) |
| Skirn, Breaker of Men — Towering Leader | Unverified | Cost 3; Recruit 2 | Gain two New Recruits; its displayed icon-gated effect gains a third. | [skirn-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-03.png) |
| Skirn, Breaker of Men — Underhanded Dealings | Unverified | Cost 4; Recruit 2 | Inspect the bottom deck card and discard or return it; its displayed icon-gated effect draws from the bottom of the deck. | [skirn-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-04.png) |

#### Adversary Group: The Mighty

The card fronts print the group name as **The Mighty** (C1 labels it “Mighty, The”). FI p.2 says this group contains eight unique cards, so each scanned face is one copy. An asterisk on Attack denotes a value affected by Uru-Enchanted Weapons; see FI pp.1–2.

| Adversary face | Copies | Printed values | Terms and concise ability summary | Card scan |
|---|---:|---|---|---|
| Hawkeye | 1 | 2 VP; Attack 3*; 1 Uru-Enchanted Weapon | Fight or Fail: choose for each other player to draw a card or discard one. | [the-mighty-01.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-01.png) |
| Wolverine | 1 | 6 VP; Attack 5*; 2 Uru-Enchanted Weapons | Fight or Fail: draw two cards. Overrun: each player reveals an Ally with the displayed icon or gains a Binding; return Wolverine to the top of the Adversary Deck. | [the-mighty-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-02.png) |
| Spider-Man | 1 | 3 VP; Attack 2*; 2 Uru-Enchanted Weapons | Fight or Fail: play revealed cards worth 2 VP or less. Overrun: each player gains a Binding. | [the-mighty-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-03.png) |
| Iron Fist | 1 | 2 VP; Attack 3*; 1 Uru-Enchanted Weapon | Fight or Fail: if the revealed card is an Adversary, KO one of your Allies. | [the-mighty-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-04.png) |
| Black Widow | 1 | 4 VP; Attack 3*; 2 Uru-Enchanted Weapons | Fight or Fail: kidnap any Bystanders revealed by the weapons. | [the-mighty-05.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-05.png) |
| Dr. Strange | 1 | 4 VP; Attack 5*; 1 Uru-Enchanted Weapon | Ambush: inspect the top three Adversary cards, leave the highest-VP Adversary on top, and put the rest below in random order. Fight or Fail: +2 Recruit. | [the-mighty-06.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-06.png) |
| Red She-Hulk | 1 | 2 VP; Attack 4*; 1 Uru-Enchanted Weapon | Fight or Fail: if the weapon reveals an Adversary, put that card in your Victory Pile. | [the-mighty-07.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-07.png) |
| Ms. Marvel | 1 | 3 VP; Attack 4*; 1 Uru-Enchanted Weapon | Fight or Fail: play a revealed Command Strike or Plot Twist. Overrun: each player reveals an Ally with the displayed icon or gains a Binding. | [the-mighty-08.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-08.png) |

#### Commander and Tactics

FI p.2 confirms one Commander and four Commander Tactics. Each of the five distinct scanned faces is one copy.

| Commander / Tactic face | Copies | Printed values | Always Leads / terms and concise effect summary | Card scan |
|---|---:|---|---|---|
| Uru-Enchanted Iron Man | 1 | 6 VP; Attack 7* | Always Leads The Mighty. Command Strike: Demolish each player, stack the Strike by Iron Man, and add one Uru-Enchanted Weapon to him per Strike there. | [uru-enchanted-iron-man-01-1.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-01-1.png) |
| Armor of the Destroyer | 1 | 6 VP; Attack 7 | Fight: for each Ally you have with the displayed icon, Demolish each other player; icon label unverified. | [uru-enchanted-iron-man-02.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-02.png) |
| Pepper Potts in Rescue Armor | 1 | 6 VP; Attack 7 | Fight: turn a Bystander from its stack into a Command Strike and resolve it immediately. | [uru-enchanted-iron-man-03.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-03.png) |
| Quantum Inventions | 1 | 6 VP; Attack 7 | Fight: draw two cards, then draw two more if you reveal an Ally with the displayed icon. | [uru-enchanted-iron-man-04.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-04.png) |
| Repulsor Coils | 1 | 6 VP; Attack 7 | Fight: each other player reveals an Ally with the displayed icon or gains a Binding; icon label unverified. | [uru-enchanted-iron-man-05.png](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-05.png) |

The Commander front has no separate setup effect beyond Always Leads; the Command Strike effect is card text, not a setup instruction.

#### Plot faces

FI p.2 confirms three Plots; the three distinct scans below account for one copy of each. Only The Traitor prints a player-count restriction.

| Plot face | Copies / player limit | Setup | Other printed effects, paraphrased | Card scan |
|---|---|---|---|---|
| Fear Itself | 1; no limit shown | 10 Twists; Fear Level starts at 8, and the Lair holds that many Allies. | Each Twist KOs a Lair Ally and lowers Fear Level by one. Good wins when it reaches zero. | [39Scheme(11).png](<https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/39Scheme(11).png>) |
| Last Stand at Avengers Tower | 1; no limit shown | 6 Twists. | A Twist is stacked above the Rooftops as a StarkTech Defense; if an Adversary is there, KO three Allies from the Lair. While an Adversary occupies the Rooftops, it gains +1 Attack per Defense. Good wins when 13 non-grey Allies are in the KO pile. | [37Scheme(9).png](<https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/37Scheme(9).png>) |
| The Traitor | 1; 2+ players only | 8 Twists; make a Betrayal Deck from three Bindings per player and a ninth Twist. | On Twists 1–3, if no Traitor is revealed, each player takes and privately views a Betrayal Card. During a turn, a player may reveal a Betrayal Twist to become Traitor; all other players then gain the Bindings on their Betrayal Cards. A player may also spend 4 Attack repeatedly to play an extra Adversary card. Twist 8 gives Good a win, with the Traitor also winning; when the other players win, the Traitor reveals and loses. | [38Scheme(10).png](<https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/38Scheme(10).png>) |

## Setup and rules facts

### Thrown Artifacts

FI p.1 describes some Ally cards as Thrown Artifacts. A gained Ally Artifact goes to the discard pile; when drawn, it can be played face-up and kept in front of its controller across turns. To throw one, place it on the bottom of the controller's deck and resolve its throw ability. Players may throw multiple Artifacts during their own turn, not during another player's turn. The insert also says controlled Ally Artifacts can satisfy effects that reveal or refer to a player's Allies; an Artifact counts as played only on the turn it entered play.

### Uru-Enchanted Weapons

FI pp.1–2: when fighting an enemy with N Uru-Enchanted Weapons, reveal N cards from the Adversary Deck; that enemy gains Attack equal to the revealed cards' combined Victory Points. If the player cannot meet the increased value, the fight fails, all Attack is lost, and no further enemy can be fought that turn. Revealed cards go to the bottom of the Adversary Deck in random order, whether the enemy is defeated or not. A Fight-or-Fail effect occurs on either outcome. A fight cannot start unless the player can meet the enemy's printed Attack, and the player cannot play more cards or throw Artifacts after beginning that fight. If the Adversary Deck runs short, shuffle the revealed cards and continue; an empty deck gives no further bonus.

### Plot and card clarifications

- **Fear Itself (FI p.2):** The 6th–8th Allies in the Lair occupy a second row, not individual city spaces. When a Fear result KOs an Ally in the top row, the remaining top-row Allies shift left and the leftmost lower-row Ally moves to the top row's right end. Stop this shifting when Fear Level falls below 5.
- **Spider-Man (FI p.2):** Scheme Twists and Strikes have zero Victory Points for this card's effect, so they are played if revealed.
- **Demolish (FI p.2):** Each player checks the cost of the top Ally card, returns that card beneath its deck, then discards a hand card with the same cost.
- **Heroic-set substitutions (FI p.2):** If the Villains base game is absent and the original Heroic set is used, replace Madame HYDRA gains with S.H.I.E.L.D. Officers, New Recruit gains with +1, Bindings with Wounds, and HYDRA team references with S.H.I.E.L.D. The insert confirms Fear Itself can be combined with Heroic and/or Villainous products.

The insert does not give the Plots' player limits, Twist counts, moves or setup steps; the scanned Plot fronts do (see Plot faces). They confirm the issue's leads: The Traitor is for 2+ players and builds its Betrayal Deck from 3 Bindings per player and a 9th Twist. No Plot requires a group or Ally.

## Required parts and glossary

### Verified parts

- The official insert lists 100 cards and no separate token count (FI p.2).
- Uru-Enchanted Weapons use the Adversary Deck; the Fear Itself Plot uses an additional Lair row of Allies (FI pp.1–2).
- The insert specifies how to substitute Bindings, Madame HYDRA, New Recruits, and HYDRA team references when using the original Heroic set instead (FI p.2). It gives no product-specific count for those shared stacks.

### Original glossary summaries

Each summary is under 40 words and paraphrases the cited insert.

| Term | Summary | Source |
|---|---|---|
| Thrown Artifact | An Ally Artifact can be returned beneath its controller's deck to trigger its throw ability during that player's turn. | FI p.1 |
| Uru-Enchanted Weapons | These weapons make a fight harder according to Victory Points in revealed Adversary Deck cards; the revealed cards return beneath that deck. | FI pp.1–2 |
| Demolish | Each player compares the top Ally card's cost with their hand and discards a hand card of matching cost. | FI p.2 |
| Fight or Fail | Resolve this effect whether the enemy is defeated or the weapon check prevents its defeat. | FI p.2 |

## Implementation notes

- This record is research only. Fear Itself is a Villainous expansion; its own insert also documents substitutions for use with Heroic products.
- The Fear Itself Plot's second Lair row and its Fear Level shift are product-specific setup/display behavior. Confirm a supported representation during integration rather than treating the generic setup-step field as sufficient.
- The issue's dependencies on #87/#88 concern implementation and do not block this serial research queue.

## Open questions and evidence gaps

1. Resolved for #96: each Plot's Twists, player limit and Setup line, and The Traitor's Betrayal Deck, come from the scanned Plot fronts.
2. Resolved for #96: The Traitor prints the 2+ player limit and the Bindings deck.
3. Resolved for #96: the Commander front prints Always Leads The Mighty and no setup effect.
4. Verify Ally shared Hero Names, teams, classes, terms, and all printed card identities from the product cards and cite the applicable rules pages.
5. Reconcile the named roster against the physical 100-card contents; the official insert supplies counts, not a full card manifest.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Ally group: Greithoth, Breaker of Wills

- **Absorb Metal**; type/group: Ally / Greithoth, Breaker of Wills; copies: Unverified; Hero Name: Greithoth, Breaker of Wills; team: Foes of Asgard; class icons: Covert; printed values: Cost 3; Attack 1+; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-04.png).
- **Mace of Chains**; type/group: Ally / Greithoth, Breaker of Wills; copies: Unverified; Hero Name: Greithoth, Breaker of Wills; team: Foes of Asgard; class icons: Instinct; printed values: Cost 3; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-03.png).
- **Break the Will to Resist**; type/group: Ally / Greithoth, Breaker of Wills; copies: Unverified; Hero Name: Greithoth, Breaker of Wills; team: Foes of Asgard; class icons: Strength; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-02.png).
- **Body of Uru**; type/group: Ally / Greithoth, Breaker of Wills; copies: Unverified; Hero Name: Greithoth, Breaker of Wills; team: Foes of Asgard; class icons: Covert; printed values: Cost 7; Attack 4+; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/greithoth-01.png).

### Ally group: Kuurth, Breaker of Stone

- **Reach for Power**; type/group: Ally / Kuurth, Breaker of Stone; copies: Unverified; Hero Name: Kuurth, Breaker of Stone; team: Foes of Asgard; class icons: Strength; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-03.png).
- **Unstoppable Sledge**; type/group: Ally / Kuurth, Breaker of Stone; copies: Unverified; Hero Name: Kuurth, Breaker of Stone; team: Foes of Asgard; class icons: Ranged; printed values: Cost 4; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-04.png).
- **Contest of Strength**; type/group: Ally / Kuurth, Breaker of Stone; copies: Unverified; Hero Name: Kuurth, Breaker of Stone; team: Foes of Asgard; class icons: Strength; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-02.png).
- **Break Every Bone**; type/group: Ally / Kuurth, Breaker of Stone; copies: Unverified; Hero Name: Kuurth, Breaker of Stone; team: Foes of Asgard; class icons: Strength; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kuurth-01.png).

### Ally group: Nerkkod, Breaker of Oceans

- **Pull of the Tides**; type/group: Ally / Nerkkod, Breaker of Oceans; copies: Unverified; Hero Name: Nerkkod, Breaker of Oceans; team: Foes of Asgard; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-04.png).
- **Feed My Undersea Legions**; type/group: Ally / Nerkkod, Breaker of Oceans; copies: Unverified; Hero Name: Nerkkod, Breaker of Oceans; team: Foes of Asgard; class icons: Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-03.png).
- **Cudgel of the Deep**; type/group: Ally / Nerkkod, Breaker of Oceans; copies: Unverified; Hero Name: Nerkkod, Breaker of Oceans; team: Foes of Asgard; class icons: Ranged; printed values: Cost 5; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-02.png).
- **Break Their Loyalties**; type/group: Ally / Nerkkod, Breaker of Oceans; copies: Unverified; Hero Name: Nerkkod, Breaker of Oceans; team: Foes of Asgard; class icons: Instinct; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nerkkod-01.png).

### Ally group: Nul, Breaker of Worlds

- **Demolition Derby**; type/group: Ally / Nul, Breaker of Worlds; copies: Unverified; Hero Name: Nul, Breaker of Worlds; team: Foes of Asgard; class icons: Strength; printed values: Cost 3; Recruit 2; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-03.png).
- **Otherworldly Maul**; type/group: Ally / Nul, Breaker of Worlds; copies: Unverified; Hero Name: Nul, Breaker of Worlds; team: Foes of Asgard; class icons: Instinct; printed values: Cost 4; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-04.png).
- **Nul Smash!**; type/group: Ally / Nul, Breaker of Worlds; copies: Unverified; Hero Name: Nul, Breaker of Worlds; team: Foes of Asgard; class icons: Strength; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-02.png).
- **Break the World**; type/group: Ally / Nul, Breaker of Worlds; copies: Unverified; Hero Name: Nul, Breaker of Worlds; team: Foes of Asgard; class icons: Instinct; printed values: Cost 8; Attack 6; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nul-01.png).

### Ally group: Skadi

- **Dark Prophecy**; type/group: Ally / Skadi; copies: Unverified; Hero Name: Skadi; team: HYDRA; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-04.png).
- **Ancient Oath of HYDRA**; type/group: Ally / Skadi; copies: Unverified; Hero Name: Skadi; team: HYDRA; class icons: Tech; printed values: Cost 5; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-03.png).
- **Hammer of the Serpent**; type/group: Ally / Skadi; copies: Unverified; Hero Name: Skadi; team: HYDRA; class icons: Strength; printed values: Cost 5; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-02.png).
- **War Banner of HYDRA**; type/group: Ally / Skadi; copies: Unverified; Hero Name: Skadi; team: HYDRA; class icons: Covert; printed values: Cost 7; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skadi-01.png).

### Ally group: Skirn, Breaker of Men

- **Towering Leader**; type/group: Ally / Skirn, Breaker of Men; copies: Unverified; Hero Name: Skirn, Breaker of Men; team: Foes of Asgard; class icons: Instinct; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-03.png).
- **Underhanded Dealings**; type/group: Ally / Skirn, Breaker of Men; copies: Unverified; Hero Name: Skirn, Breaker of Men; team: Foes of Asgard; class icons: Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-04.png).
- **Titanic Bludgeon**; type/group: Ally / Skirn, Breaker of Men; copies: Unverified; Hero Name: Skirn, Breaker of Men; team: Foes of Asgard; class icons: Ranged; printed values: Cost 2; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-02.png).
- **Break Your Hopes**; type/group: Ally / Skirn, Breaker of Men; copies: Unverified; Hero Name: Skirn, Breaker of Men; team: Foes of Asgard; class icons: Strength; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/skirn-01.png).

### Adversary Group: Mighty, The

- **Black Widow**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 3*; VP 4; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-05.png).
- **Dr. Strange**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 5*; VP 4; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-06.png).
- **Hawkeye**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 3*; VP 2; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-01.png).
- **Iron Fist**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 3*; VP 2; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-04.png).
- **Ms. Marvel**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 4*; VP 3; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-08.png).
- **Red She-Hulk**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 4*; VP 2; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-07.png).
- **Spider-Man**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 2*; VP 3; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-03.png).
- **Wolverine**; type/group: Adversary / Mighty, The; copies: 1; printed values: Attack 5*; VP 6; keyword labels: Uru-Enchanted Weapons, Fight or Fail; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-mighty-02.png).

### Commander: Uru-Enchanted Iron Man

- **Uru-Enchanted Iron Man**; type/group: Normal Commander face / Uru-Enchanted Iron Man; copies: Unverified; printed values: VP 6; keyword labels: Demolish, Uru-Enchanted Weapons; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-01-1.png).
- **Armor of the Destroyer**; type/group: Commander Tactic / Uru-Enchanted Iron Man; copies: Unverified; printed values: not indexed in C1; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-02.png).
- **Pepper Potts in Rescue Armor**; type/group: Commander Tactic / Uru-Enchanted Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-03.png).
- **Quantum Inventions**; type/group: Commander Tactic / Uru-Enchanted Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-04.png).
- **Repulsor Coils**; type/group: Commander Tactic / Uru-Enchanted Iron Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/uru-enchanted-iron-man-05.png).

### Plot: Fear Itself

- **Fear Itself**; type/group: Plot / Fear Itself; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/39Scheme(11).png).

### Plot: Last Stand at Avengers Tower

- **Last Stand at Avengers Tower**; type/group: Plot / Last Stand at Avengers Tower; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/37Scheme(9).png).

### Plot: Traitor, The

- **Traitor, The**; type/group: Plot / Traitor, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/38Scheme(10).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
