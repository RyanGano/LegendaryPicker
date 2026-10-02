# Guardians of the Galaxy (October 2014)

**Research status: Partial.** The official insert verifies aggregate contents and several rules; local scans record 20 Hero faces, both Mastermind fronts, and all four Scheme fronts. Hero face counts and icon labels, the Tactics and Villain manifests, and the 18-versus-30 Shard supply discrepancy remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| GG | [Upper Deck Guardians of the Galaxy rules insert](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Guardians_of_the_Galaxy.pdf) | Contents and counts (PDF p.2); Shards and Artifacts (PDF p.1); card clarifications (PDF p.2). The insert has no printed page numbers, so citations use PDF pages. |
| C1 | [master-strike structured guardians-of-the-galaxy card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/gotg.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| C2 | [nutki Guardians card catalog](https://github.com/nutki/legendary/tree/master/texttools/Guardians%20of%20the%20Galaxy) | Card and group identifiers only. Not used for rules, card behavior, component use, or setup values. |
| Card | Clear scans of printed Hero, Mastermind, and Scheme fronts under the external `LegendaryPickerResearch\cards\` folder (not committed) | The scanned Hero values and abilities, two Mastermind fronts, and four Scheme fronts; exact scan filenames are cited below. |
| #95 | [Project issue: Add Guardians of the Galaxy (2014), with Shards](https://github.com/RyanGano/LegendaryPicker/issues/95) | Collection lead and project context only; its counts and mechanics are not treated as evidence. |
| Project queue | [`Docs/BoxResearch/README.md`](README.md), release order tracked by [#34](https://github.com/RyanGano/LegendaryPicker/issues/34) | October 2014, expansion status, First Edition classification, and release order. |

## Catalog inventory

### Official contents

GG p.2 lists 100 cards and 18 Shard tokens:

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Masterminds | 2 sets × 5 cards = 10 (one Mastermind and four Tactics per set) |
| Schemes | 4 |
| Total cards | 100 |
| Shard tokens | 18 |

The category totals sum to the stated 100 cards. The insert does not enumerate the printed names of the individual cards.

### Group inventory (C1/C2)

The following identifiers are reported by C1 and C2; the appended C1 face index records available structured metadata and linked images.

- **Heroes (five):** Drax the Destroyer, Gamora, Groot, Rocket Raccoon, Star-Lord.
- **Villain Groups (two):** Kree Starforce; Infinity Gems.
- **C1/C2 Villain-name leads:** Kree Starforce lists Captain Atlas, Demon Druid, Dr. Minerva, Korath the Pursuer, Ronan the Accuser, Shatterax, and Supremor. Infinity Gems lists Mind Gem, Power Gem, Reality Gem, Soul Gem, Space Gem, and Time Gem. These 13 leads do not reconcile to GG p.2's 16 Villain cards and reference to eight Villains; no card fronts were available to resolve the conflict.
- **Masterminds (two):** Supreme Intelligence of the Kree; Thanos.
- **Schemes (four):** Forge the Infinity Gauntlet; Intergalactic Kree Nega-Bomb; The Kree-Skrull War; Unite the Shards.
- **Henchman Groups:** The official contents list has no Henchman Group entry.

The C1/C2 names do not establish Hero teams, classes, card keywords, Always Leads mappings, or card effects.

### Hero card faces from local scans

The available scans show four distinct Hero faces for each of the five Heroes. GG p.2 confirms 14 cards per Hero group (one rare, three uncommon, and five copies of each of two common faces), but does not map those copy counts to titles. Individual face counts are therefore unverified. Each face prints its shared Hero Name; team and class symbols are visible, but their textual labels could not be established from an allowed source. Where an effect has a symbol-gated line, the summary describes the effect without assigning an unverified symbol name.

| Printed Hero Name | Team | Classes |
|---|---|---|
| Drax the Destroyer | Team symbol visible; label unverified | Class symbols visible on faces; names unverified |
| Gamora | Team symbol visible; label unverified | Class symbols visible on faces; names unverified |
| Groot | Team symbol visible; label unverified | Class symbols visible on faces; names unverified |
| Rocket Raccoon | Team symbol visible; label unverified | Class symbols visible on faces; names unverified |
| Star-Lord | Team symbol visible; label unverified | Class symbols visible on faces; names unverified |

| Hero Name and face | Copies | Printed values | Terms and concise ability summary | Card scan |
|---|---|---|---|---|
| **Drax the Destroyer** — Avatar of Destruction | Unverified | Cost 7 | Doubles the Attack you have. | `drax-01.png` |
| Drax the Destroyer — Knives of the Hunter | Unverified | Cost 3 | Artifact; once per turn, +1 Attack. | `drax-02.png` |
| Drax the Destroyer — Interstellar Tracker | Unverified | Cost 3; Recruit 2 | Inspect the top deck card and discard it or return it; its displayed class-icon effect may KO the discarded card. | `drax-03.png` |
| Drax the Destroyer — The Destroyer | Unverified | Cost 6; Attack 4 | Its displayed team-icon line asks each other player to reveal a Hero matching its class icon or discard a controlled Artifact; gain a Shard for each Artifact discarded. Icon labels and trigger rules are unverified. | `drax-04.png` |
| **Gamora** — Bounty Hunter | Unverified | Cost 2; Recruit 2 | A Villain gains a Shard. | `gamora-01.png` |
| Gamora — Deadliest Woman in the Universe | Unverified | Cost 3 | Gain two Shards; the displayed class-icon effect gains one more. | `gamora-02.png` |
| Gamora — Galactic Assassin | Unverified | Cost 5; Attack 3 | A chosen Villain gets no Attack from Shards this turn; its two-icon effect also removes the Mastermind's Attack from Shards this turn. | `gamora-03.png` |
| Gamora — Godslayer Blade | Unverified | Cost 8 | Artifact; once per turn, gain two Shards; once per turn, you may spend five Shards for +10 Attack. | `gamora-04.png` |
| **Groot** — I Am Groot | Unverified | Cost 8; Recruit 5 | The next Hero you recruit this turn grants Shards equal to its cost. | `groot-01.png` |
| Groot — Groot and Branches | Unverified | Cost 4 | Gain two Shards and may spend Shards for Recruit this turn; its displayed class-icon effect lets another player gain a Shard. | `groot-02.png` |
| Groot — Prune the Growths | Unverified | Cost 4; Attack 2 | Its displayed class-icon effect may KO a card from hand or discard pile; if so, gain a Shard. | `groot-03.png` |
| Groot — Surviving Sprig | Unverified | Cost 3; Attack 1 | Draw one extra card when drawing your new hand at the end of this turn. | `groot-04.png` |
| **Rocket Raccoon** — Vengeance Is Rocket | Unverified | Cost 7; Attack 5+ | Gain +1 Attack for each Master Strike in the KO pile or stacked next to the Mastermind. | `rocket-01.png` |
| Rocket Raccoon — Incoming Detector | Unverified | Cost 4 | Artifact; after a Master Strike or Villain Ambush ability resolves, you may gain a Shard. | `rocket-02.png` |
| Rocket Raccoon — Trigger Happy | Unverified | Cost 4; printed value 2 (Recruit/Attack icon unclear) | Gain a Shard for each other Hero with the matching displayed team icon played this turn; the lower-left resource icon is not legible enough to classify. | `rocket-03.png` |
| Rocket Raccoon — Gritty Scavenger | Unverified | Cost 3; Recruit 2 | You may discard a card; if you do, draw a card. | `rocket-04.png` |
| **Star-Lord** — Sentient Starship | Unverified | Cost 8 | Artifact; once per turn, gain a Shard for each Artifact you control. | `star-lord-01.png` |
| Star-Lord — Implanted Memory Chip | Unverified | Cost 6 | Artifact; once per turn, draw a card. | `star-lord-01-1.png` |
| Star-Lord — Element Guns | Unverified | Cost 4 | Artifact; once per turn, gain a Shard. | `star-lord-03.png` |
| Star-Lord — Legendary Outlaw | Unverified | Cost 4; Recruit 2 | Choose an Artifact controlled by any player with a once-per-turn ability and use a copy of one such ability. | `star-lord-04.png` |

The scans establish the printed face details above, not the complete Hero Name/team/class glossary. The Hero group totals are official, but face rarity and copies remain unassigned.

### Villain card identity leads (not card-verified)

C1/C2 support the title and group leads below only. Every individual copy count, printed value, term, and effect is unverified because no Villain fronts were scanned. GG p.2 names Reality Gem, Soul Gem, and Space Gem and confirms only that their Ambush effects resolve after entering the city and pushing other Villains; their printed effect text and values remain unavailable.

| Group / title lead | Copies | Values and terms | Ability / evidence |
|---|---|---|---|
| Kree Starforce — Captain Atlas | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Kree Starforce — Demon Druid | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Kree Starforce — Dr. Minerva | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Kree Starforce — Korath the Pursuer | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Kree Starforce — Ronan the Accuser | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Kree Starforce — Shatterax | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Kree Starforce — Supremor | Unverified | Unverified | C1/C2 identity lead; see the appended C1 face index for available structured fields and images. |
| Infinity Gems — Mind Gem | Unverified | Unverified | C1/C2 identity lead only; GG p.2 gives the group-level Gem rule. |
| Infinity Gems — Power Gem | Unverified | Unverified | C1/C2 identity lead only; GG p.2 gives the group-level Gem rule. |
| Infinity Gems — Reality Gem | Unverified | Unverified | C1/C2 title/group lead; GG p.2 confirms an Ambush effect, not its text. |
| Infinity Gems — Soul Gem | Unverified | Unverified | C1/C2 title/group lead; GG p.2 confirms an Ambush effect, not its text. |
| Infinity Gems — Space Gem | Unverified | Unverified | C1/C2 title/group lead; GG p.2 confirms an Ambush effect, not its text. |
| Infinity Gems — Time Gem | Unverified | Unverified | C1/C2 identity lead only; GG p.2 gives the group-level Gem rule. |

## Setup and rules facts

### Shared Shards

GG p.1 says the 18 Shards form a shared supply. Players, Villains, and Masterminds can gain them. A player may return a Shard for +1 and may keep and spend multiple Shards in a turn; Shards are not worth Victory Points. Each Shard on a Villain or Mastermind adds +1 to its strength. After defeating a Shard-bearing Villain, the player takes one Shard and returns its remaining Shards to the supply. When a Shard-bearing Villain escapes, the Mastermind takes one and the rest return. If the supply is empty, no Shard is gained.

### Artifacts and Infinity Gems

- **Hero Artifacts (GG p.1):** On gaining one, a player puts it in their discard pile. After drawing it, they may play it face-up; it remains in front of them across turns, and its ability is usually usable once on each of their turns. They may decline to use it. A player can control duplicates with the same name. It counts as played only on the turn it is put out.
- **Infinity Gems (GG p.2):** The insert identifies Infinity Gems as a Villain Group. A defeated Gem becomes a zero-cost Artifact in its victor's discard pile; it has no Hero class or color and is no longer a Hero or Villain card. If an effect returns it to the city or Villain Deck, it becomes a Villain card again.
- **Artifact interactions (GG p.1):** A controlled Hero Artifact can satisfy effects referring to revealing or having Heroes. Its once-per-turn ability is not used on another player's turn; effects counting Heroes played that turn count an Artifact only on the turn it entered play.

### Scheme fronts: setup and printed effects

| Scheme face | Copies / player-count evidence | Setup | Other printed Scheme effects, paraphrased | Card scan |
|---|---|---|---|---|
| Forge the Infinity Gauntlet | 1 of 4 Schemes; the card gives no player-count table. | 8 Twists; always include Infinity Gems. | When a Twist resolves, the first eligible player clockwise from the starting player chooses a Gem Artifact in play or their discard pile to enter the city; each Gem Villain there gains a Shard. Evil wins if six Gem Villains are in the city or escape pile, or if a player controls four Gem Artifacts (that player also wins). | `scheme-forge-infinity-gauntlet.png` |
| Intergalactic Kree Nega-Bomb | 1 of 4 Schemes; the card gives no player-count table. | 8 Twists; make a face-down Nega-Bomb Deck from 6 Bystanders. | A Twist is shuffled into that deck, then a random card is revealed: rescue a Bystander or KO a Twist. Each Twist also KOs all Heroes from the HQ and gives each player a Wound. Evil wins when 16 non-grey Heroes are in the KO pile. | `scheme-kree-nega-bomb.png` |
| The Kree-Skrull War | 1 of 4 Schemes; the insert provides special group instructions for 1 and 2 players, not a general player-limit table. | 8 Twists; always include Kree Starforce and Skrull Villains. | Twists 1–7 make Kree and Skrulls escape, then place the Twist as a Kree or Skrull Conquest according to which group is more numerous in the Escape Pile. Twist 8 goes by the side with more Conquests. The card does not state where a Twist goes on a tie. Evil wins at four of either Conquest. | `scheme-kree-skrull-war.png` |
| Unite the Shards | 1 of 4 Schemes; the card gives no player-count table. | 30 Shards in supply; players + 5 Twists (6–10 for 1–5 players). | Each Twist is stacked by the Scheme and gives the Mastermind a Shard. During a turn, you may repeat the printed exchange: pay two of the shown resource for one of the Mastermind's Shards. Evil wins at ten Mastermind Shards or when the supply is empty; the resource icon's name is unverified. | `scheme-unite-shards.png` |

The four scanned Scheme fronts reconcile to the four Scheme cards in GG p.2. None prints a separate player-limit table. The Kree-Skrull War insert gives additional 1- and 2-player group instructions (GG p.2). The 30-Shard supply specified by Unite the Shards conflicts with the 18 physical Shard tokens listed by the insert; no substitution or extra supply is inferred.

### Mastermind Always Leads

| Mastermind | Card-verified Always Leads | Card scan |
|---|---|---|
| Supreme Intelligence of the Kree | Kree Starforce | `mm-supreme-intelligence.png` |
| Thanos | Infinity Gems | `mm-thanos.png` |

### Mastermind fronts and Tactics

| Mastermind front | Copies / printed values | Always Leads and card effect summary | Card scan |
|---|---|---|---|
| Supreme Intelligence of the Kree | 1 per set; Attack 9; 6 VP | Always Leads Kree Starforce. Master Strike: it gains a Shard; each player reveals their hand and discards cards whose cost equals its Shard count or is one greater. | `mm-supreme-intelligence.png` |
| Thanos | 1 per set; Attack 24*; 7 VP | Always Leads Infinity Gems. Its Attack is reduced by 2 per Infinity Gem Artifact controlled by any player. Master Strike: each player reveals their hand and places one non-grey Hero beside Thanos in a Bound Souls pile. | `mm-thanos.png` |

GG p.2 confirms four Tactics per Mastermind set. C1 face titles and available numeric metadata are indexed below; no Tactic setup/effect is inferred from C1, and any missing copy count or printed field remains unverified.

| Mastermind / Tactic title lead | Copies | Values, terms, and effect |
|---|---|---|
| Supreme Intelligence of the Kree — Combined Knowledge of All Kree | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Supreme Intelligence of the Kree — Cosmic Omniscience | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Supreme Intelligence of the Kree — Countermeasure Protocols | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Supreme Intelligence of the Kree — Guide Kree Evolution | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Thanos — Centuries of Envy | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Thanos — God of Death | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Thanos — Keeper of Souls | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |
| Thanos — The Mad Titan | Unverified | Unverified; See the appended C1 face index for available structured fields; setup and ability facts remain unverified. |

### Rulebook clarifications

- **The Kree-Skrull War (GG p.2; card scan):** For 2 players, use Kree Starforce and the core Skrull group even if this overrides Always Leads; for 1 player, use both groups and expect a larger-than-usual Villain Deck. The card scan gives 8 Twists; no special 3–5-player table is printed.
- **Forge the Infinity Gauntlet (GG p.2):** Villains the Scheme brings into the city at unusual times still resolve normal Ambush effects; Villains it makes escape still resolve normal Escape effects.
- **Reality Gem, Soul Gem, Space Gem (GG p.2):** Resolve their Ambush effects after the card enters the city and pushes other Villains; the clarification says these Gems count themselves for that effect.
- **Thanos (GG p.2):** His reduction counts Infinity Gems players currently control, not Gems in their decks or discard piles. In Solo, when using a Villain Group other than Infinity Gems, he gets -2 for each Villain of that group in the player's Victory Pile.

The scans verify the four Scheme fronts, both Mastermind fronts and Always Leads mappings, and the printed details on 20 Hero faces. They do not verify Hero face copy counts, the eight Tactic fronts, or the physical 16-card Villain manifest.

## Required parts and glossary

### Verified parts

- The product adds 18 Shard tokens to the shared supply (GG p.1–2).
- Unite the Shards' card Setup specifies 30 Shards, exceeding the 18 tokens listed in the insert; the mismatch needs an official clarification (Card: `scheme-unite-shards.png`; GG p.2).
- Infinity Gems are Villain cards that can become Artifacts in a player's discard pile (GG p.2).
- No additional shared-stack totals are listed in the official contents roster. Other Scheme-specific shared-stack uses beyond the verified Nega-Bomb Bystander Deck remain unverified.

### Original glossary summaries

Each summary is under 40 words and paraphrases the cited insert.

| Term | Summary | Source |
|---|---|---|
| Shard | A resource token a player can return for +1; a Shard on a Villain or Mastermind adds +1 to its strength. | GG p.1 |
| Artifact | A card played face-up and retained in front of its controller across turns; its ability is usually usable once on each of that player's turns. | GG p.1 |
| Infinity Gem | A Gem defeated from this Villain Group becomes a zero-cost Artifact in the victor's discard pile; returning it to the city or Villain Deck makes it a Villain again. | GG p.2 |

Hero team/class icon labels and per-face copy counts, along with the missing Villain and Tactic fronts, still need verification from official card evidence and their governing rules pages.

## Implementation notes

- This record is research only; it does not change runtime data or code. Guardians is an expansion with First Edition rules, not a separate base-game setup table (project queue).
- The project tracks implementation dependencies in [#87](https://github.com/RyanGano/LegendaryPicker/issues/87) and [#88](https://github.com/RyanGano/LegendaryPicker/issues/88); they do not block research.
- The model described in `Docs/Plan.md` has no Shard-token component field. Its card moves cover Hero, Henchman, Bystander, Wound, Officer, and Sidekick cards, with destinations limited to the Villain Deck, Hero Deck, beside the Scheme, or starting decks. It does not describe a separate set-aside Bystander deck.
- The card model's listed kinds do not represent an Infinity Gem changing from a Villain into an Artifact after defeat. Confirm the exact data requirements during integration rather than assuming an existing field covers this.

## Open questions and evidence gaps

1. Resolve the 30-Shard Scheme supply versus the insert's 18-token component list with an official clarification; do not assume substitute counters.
2. Verify Hero teams/classes, identify the printed class/team symbols from an allowed source, and map the official 14-card-per-Hero distribution to individual face counts. The scans do not establish these mappings.
3. Reconcile the C1/C2 Villain title leads to the official 16-card manifest; no Villain fronts were scanned, and the name leads do not reconcile to the insert's card count.
4. Verify the eight Mastermind Tactic values and setup-relevant effects from allowed card-face sources; C1 metadata is indexed separately and is not rules evidence.
5. Resolve where The Kree-Skrull War places a Twist when the Escape Pile counts tie, and where Twist 8 goes if both sides have the same number of Conquests.
6. Check whether Schemes use shared parts beyond the verified six-Bystander Nega-Bomb Deck and Shards, and whether further glossary terms are needed.

Guardians remains **Partial**: the available Hero, Mastermind, and Scheme scans support the listed card-face facts, but Hero metadata and copies, Villain and Tactic fronts, tie handling in The Kree-Skrull War, and the Shard-supply conflict remain open. These gaps do not block serial research on other products.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Drax the Destroyer

- **Knives of the Hunter**; type/group: Hero / Drax the Destroyer; copies: Unverified; Hero Name: Drax the Destroyer; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 3; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/drax-03.png).
- **Interstellar Tracker**; type/group: Hero / Drax the Destroyer; copies: Unverified; Hero Name: Drax the Destroyer; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/drax-04.png).
- **The Destroyer**; type/group: Hero / Drax the Destroyer; copies: Unverified; Hero Name: Drax the Destroyer; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/drax-02.png).
- **Avatar of Destruction**; type/group: Hero / Drax the Destroyer; copies: Unverified; Hero Name: Drax the Destroyer; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/drax-01.png).

### Hero group: Gamora

- **Bounty Hunter**; type/group: Hero / Gamora; copies: Unverified; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 2; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gamora-04.png).
- **Deadliest Woman in the Universe**; type/group: Hero / Gamora; copies: Unverified; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gamora-03.png).
- **Galactic Assassin**; type/group: Hero / Gamora; copies: Unverified; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gamora-02.png).
- **Godslayer Blade**; type/group: Hero / Gamora; copies: Unverified; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 8; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/gamora-01.png).

### Hero group: Groot

- **Prune the Growths**; type/group: Hero / Groot; copies: Unverified; Hero Name: Groot; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/groot-03.png).
- **Surviving Sprig**; type/group: Hero / Groot; copies: Unverified; Hero Name: Groot; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 3; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/groot-04.png).
- **Groot and Branches**; type/group: Hero / Groot; copies: Unverified; Hero Name: Groot; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/groot-02.png).
- **I Am Groot**; type/group: Hero / Groot; copies: Unverified; Hero Name: Groot; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 8; Recruit 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/groot-01.png).

### Hero group: Rocket Raccoon

- **Gritty Scavenger**; type/group: Hero / Rocket Raccoon; copies: Unverified; Hero Name: Rocket Raccoon; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket-04.png).
- **Trigger Happy**; type/group: Hero / Rocket Raccoon; copies: Unverified; Hero Name: Rocket Raccoon; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket-03.png).
- **Incoming Detector**; type/group: Hero / Rocket Raccoon; copies: Unverified; Hero Name: Rocket Raccoon; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 4; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket-02.png).
- **Vengeance is Rocket**; type/group: Hero / Rocket Raccoon; copies: Unverified; Hero Name: Rocket Raccoon; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket-01.png).

### Hero group: Star-Lord

- **Element Guns**; type/group: Hero / Star-Lord; copies: Unverified; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 4; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/star-lord-03.png).
- **Legendary Outlaw**; type/group: Hero / Star-Lord; copies: Unverified; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/star-lord-04.png).
- **Implanted Memory Chip**; type/group: Hero / Star-Lord; copies: Unverified; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 6; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/star-lord-01.png).
- **Sentient Starship**; type/group: Hero / Star-Lord; copies: Unverified; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 8; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/star-lord-01-1.png).

### Villain Group: Kree Starforce

- **Captain Atlas**; type/group: Villain / Kree Starforce; copies: 1; printed values: Attack 6+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-05.png).
- **Demon Druid**; type/group: Villain / Kree Starforce; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-01.png).
- **Dr. Minerva**; type/group: Villain / Kree Starforce; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-03.png).
- **Korath the Pursuer**; type/group: Villain / Kree Starforce; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-06.png).
- **Ronan the Accuser**; type/group: Villain / Kree Starforce; copies: 1; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-08.png).
- **Shatterax**; type/group: Villain / Kree Starforce; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-02.png).
- **Supremor**; type/group: Villain / Kree Starforce; copies: 2; printed values: Attack 3; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kree-starforce-04.png).

### Villain Group: Infinity Gems

- **Mind Gem**; type/group: Villain / Infinity Gems; copies: 1; printed values: Attack 6; VP 0; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/infinity-gems-01.png).
- **Power Gem**; type/group: Villain / Infinity Gems; copies: 1; printed values: Attack 7; VP 0; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/infinity-gems-05.png).
- **Reality Gem**; type/group: Villain / Infinity Gems; copies: 2; printed values: Attack 5; VP 0; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/infinity-gems-06.png).
- **Soul Gem**; type/group: Villain / Infinity Gems; copies: 1; printed values: Attack 6; VP 0; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/infinity-gems-03.png).
- **Space Gem**; type/group: Villain / Infinity Gems; copies: 2; printed values: Attack 5; VP 0; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/infinity-gems-02.png).
- **Time Gem**; type/group: Villain / Infinity Gems; copies: 1; printed values: Attack 6; VP 0; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/infinity-gems-04.png).

### Mastermind: Supreme Intelligence of the Kree

- **Supreme Intelligence of the Kree**; type/group: Normal Mastermind face / Supreme Intelligence of the Kree; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/supreme-intelligence-of-the-kree-01.png).
- **Combined Knowledge of All Kree**; type/group: Mastermind Tactic / Supreme Intelligence of the Kree; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/supreme-intelligence-of-the-kree-05.png).
- **Cosmic Omniscience**; type/group: Mastermind Tactic / Supreme Intelligence of the Kree; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/supreme-intelligence-of-the-kree-04.png).
- **Countermeasure Protocols**; type/group: Mastermind Tactic / Supreme Intelligence of the Kree; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/supreme-intelligence-of-the-kree-03.png).
- **Guide Kree Evolution**; type/group: Mastermind Tactic / Supreme Intelligence of the Kree; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/supreme-intelligence-of-the-kree-02.png).

### Mastermind: Thanos

- **Thanos**; type/group: Normal Mastermind face / Thanos; copies: Unverified; printed values: VP 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/thanos-01.png).
- **Centuries of Envy**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/thanos-05.png).
- **God of Death**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/thanos-02.png).
- **Keeper of Souls**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/thanos-04.png).
- **The Mad Titan**; type/group: Mastermind Tactic / Thanos; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/thanos-03.png).

### Scheme: Forge the Infinity Gauntlet

- **Forge the Infinity Gauntlet**; type/group: Scheme / Forge the Infinity Gauntlet; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/36Scheme(59).png).

### Scheme: Intergalactic Kree Nega-Bomb

- **Intergalactic Kree Nega-Bomb**; type/group: Scheme / Intergalactic Kree Nega-Bomb; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/34Scheme(57).png).

### Scheme: Kree-Skrull War, The

- **Kree-Skrull War, The**; type/group: Scheme / Kree-Skrull War, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/33Scheme(56).png).

### Scheme: Unite the Shards

- **Unite the Shards**; type/group: Scheme / Unite the Shards; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/35Scheme(58).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
