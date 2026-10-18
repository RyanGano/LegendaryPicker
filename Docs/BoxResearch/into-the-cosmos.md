# Into the Cosmos (August 2020)

**Research status: Partial.** The official insert verifies the 200-card count, 18 Shard tokens, and several mechanics. For #179 the Scheme Setup lines, Always Leads and part uses were read from OCR of every C1-linked face and are in `LegendaryPickerService/Data/Boxes/into-the-cosmos.json`; per-face copy counts and the Contest of Champions and Cosmic Threat icons remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| IC | [Upper Deck Into the Cosmos rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/IntoTheCosmos_Rules.pdf) | Contents and Shards (PDF pp.1–2), Danger Sense, Celestial Boons, Contest of Champions, and Cosmic Threat rules (PDF pp.1–2). |
| C1 | [master-strike structured into-the-cosmos card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/intothecosmos.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | August 2020 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (IC p.2)

| Type | Official count |
|---|---:|
| Heroes | 9 groups × 14 cards = 126 |
| Villain Groups | 4 groups × 8 cards = 32 |
| Henchman Groups | 2 groups × 10 cards = 20 |
| Double-Sided Epic Masterminds | 3 sets × 5 cards = 15 |
| Schemes | 4 |
| Special Bystanders | 3 types × 1 card = 3 |
| Total cards | 200 |
| Shard tokens | 18 |

The card categories sum to 200; the 18 Shards are tokens, not cards.

### Group inventory (C1)

- **Heroes (nine):** Adam Warlock; Captain Mar-Vell; Moondragon; Nebula; Nova; Phyla-Vell; Quasar; Ronan the Accuser; Yondu.
- **Villain Groups (four):** Black Order of Thanos; Celestials; Elders of the Universe; From Beyond.
- **Henchman Groups (two):** Sidera Maris, Bridge Builders; Universal Church of Truth.
- **Masterminds (three):** The Beyonder; The Grandmaster; Magus.
- **Schemes (four):** The Contest of Champions; Turn the Soul of Adam Warlock; Destroy the Nova Corps; Annihilation: Conquest.
- **Special Bystanders (three):** Board Gamer; Legendary Game Designer; Pizza Delivery Guy.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index still need an allowed source; Scheme Setup lines, Always Leads and part uses were read from the linked faces (#179), and per-face copy counts and the icon shapes lost in text extraction remain open.

## Rules and mechanisms

- **Shards (IC p.1):** There is no limit to the number in play. A player spends one to gain +1 Recruit, returning it to the supply; it can be used immediately or saved and has no Victory Points. A Villain or Mastermind gains +1 value per Shard placed on it. When defeated, its player takes one Shard and returns the rest; when a Villain escapes, the Mastermind takes one and the rest return. After a Mastermind fight, the player takes one and the rest return before the Tactic's Fight effect.
- **Burn Shards (IC p.1):** Spend the printed number of Shards once that turn to use the listed effect; this does not grant the normal Recruit bonus for the spent Shards.
- **Danger Sense (IC p.1):** Reveal the stated number of Villain Deck cards, gain +1 per revealed Villain, then reorder them on top. A Black Order version instead grants the temporary bonus to Black Order Villains in the city and the Mastermind.
- **Celestial Boons (IC p.2):** A player who fights a Celestial gains its permanent bonus while that Celestial remains in the Victory Pile. Multiple Boons, including duplicates, may be used.
- **Contest of Champions (IC p.2):** Each player selects a card from hand, played this turn, or the top of their deck and compares its printed cost, with the insert's indicated card type doubled. Evil reveals the top two Hero Deck cards and uses the higher score; both are placed on the bottom. Tied-highest scores also win. The exact doubling icon is missing from the text extraction.
- **Cosmic Threat (IC p.2):** Once per turn, revealing the specified card type reduces that enemy's value by 3 per revealed card. A Mastermind's value resets before a later fight that turn; the same revealed cards can affect different Cosmic Threat enemies. The qualifying icon is missing from the text extraction.

## Required parts and glossary

- **Shard supply:** 18 tokens are included, but the insert removes any in-play cap; players may use substitute tokens (IC p.1).
- Shards move between the supply and players, Villains, and Masterminds as described above. The cards and tokens require tracking current per-enemy and per-player counts.
- **Shard:** A token that a player may spend for +1 Recruit or a card effect; enemies gain value from Shards placed on them. (IC p.1)
- **Burn Shards:** Spend the stated number of Shards for a listed effect without receiving their usual Recruit value. (IC p.1)
- **Celestial Boon:** A persistent benefit from a Celestial held in the player's Victory Pile. (IC p.2)
- **Cosmic Threat:** A once-per-turn vulnerability that reduces an enemy's value for each matching card revealed. (IC p.2)

Summaries are original paraphrases under 40 words. The box file reuses Guardians of the Galaxy's Shard, Spider-Man Homecoming's Danger Sense and Fantastic Four's Cosmic Threat terms and adds Burn Shards, Contest of Champions and Celestial Boon. From the card faces (#179): every Hero, both Henchman Groups, the Celestials, the Elders of the Universe, the Grandmaster, Magus and Destroy the Nova Corps use Shards; the Black Order, the Elders, From Beyond, all three Masterminds, The Contest of Champions and Turn the Soul of Adam Warlock (3 Wounds stand in as Twists) use Wounds; Destroy the Nova Corps also puts 2 Wounds and an Officer in each starting deck. No card uses another shared stack. One special Bystander gives a Shard, but the special Bystanders join the Bystander stack and bring no part.

## Setup and implementation gaps

Per-face copy counts for Heroes, Masterminds and Tactics are not indexed by C1 and remain unverified, as do the exact Contest of Champions and Cosmic Threat icons lost in text extraction; neither changes the setup. Shard movement, Celestial Boons and Contests happen during play.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Adam Warlock

- **Transmute Matter**; type/group: Hero / Adam Warlock; copies: Unverified; Hero Name: Adam Warlock; team: Avengers; class icons: Covert; printed values: Cost 3; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/adam-warlock-04.png).
- **Regenerative Cocoon**; type/group: Hero / Adam Warlock; copies: Unverified; Hero Name: Adam Warlock; team: Avengers; class icons: Strength; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/adam-warlock-03.png).
- **Soulblast**; type/group: Hero / Adam Warlock; copies: Unverified; Hero Name: Adam Warlock; team: Avengers; class icons: Covert; printed values: Cost 5; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/adam-warlock-02.png).
- **Manifest the Soul Gem**; type/group: Hero / Adam Warlock; copies: Unverified; Hero Name: Adam Warlock; team: Avengers; class icons: Ranged; printed values: Cost 8; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/adam-warlock-01.png).

### Hero group: Captain Mar-Vell

- **Cosmic Awareness**; type/group: Hero / Captain Mar-Vell; copies: Unverified; Hero Name: Captain Mar-Vell; team: Avengers; class icons: Ranged; printed values: Cost 2; Attack 1; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-mar-vell-03.png).
- **Kree Genetics**; type/group: Hero / Captain Mar-Vell; copies: Unverified; Hero Name: Captain Mar-Vell; team: Avengers; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-mar-vell-04.png).
- **Channel the Nega-Bands**; type/group: Hero / Captain Mar-Vell; copies: Unverified; Hero Name: Captain Mar-Vell; team: Avengers; class icons: Tech; printed values: Cost 6; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-mar-vell-02.png).
- **Protector of the Universe**; type/group: Hero / Captain Mar-Vell; copies: Unverified; Hero Name: Captain Mar-Vell; team: Avengers; class icons: Covert; printed values: Cost 7; Attack 3+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-mar-vell-01.png).

### Hero group: Moondragon

- **Peaceful Meditation**; type/group: Hero / Moondragon; copies: Unverified; Hero Name: Moondragon; team: Avengers; class icons: Covert; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moondragon-04.png).
- **Psionic Warning**; type/group: Hero / Moondragon; copies: Unverified; Hero Name: Moondragon; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 1+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moondragon-03.png).
- **Psychokinetic Blast**; type/group: Hero / Moondragon; copies: Unverified; Hero Name: Moondragon; team: Avengers; class icons: Ranged; printed values: Cost 6; Attack 1+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moondragon-02.png).
- **Lunar Dragon Form**; type/group: Hero / Moondragon; copies: Unverified; Hero Name: Moondragon; team: Avengers; class icons: Strength; printed values: Cost 8; Attack 5+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moondragon-01.png).

### Hero group: Nebula

- **Ruthless Cyborg**; type/group: Hero / Nebula; copies: Unverified; Hero Name: Nebula; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nebula-04.png).
- **Galactic Rogue**; type/group: Hero / Nebula; copies: Unverified; Hero Name: Nebula; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nebula-03.png).
- **Illusion Device**; type/group: Hero / Nebula; copies: Unverified; Hero Name: Nebula; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nebula-02.png).
- **Daring Raid**; type/group: Hero / Nebula; copies: Unverified; Hero Name: Nebula; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 7; Recruit 0+; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nebula-01.png).

### Hero group: Nova

- **Draw From the Worldmind**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Avengers; class icons: Tech; printed values: Cost 2; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-04.png).
- **Electromagnetic Wave**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-03.png).
- **Declare Galactic Threat**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Avengers; class icons: Tech; printed values: Cost 6; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-02.png).
- **Mobilize the Nova Corps**; type/group: Hero / Nova; copies: Unverified; Hero Name: Nova; team: Avengers; class icons: Ranged; printed values: Cost 8; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nova-01.png).

### Hero group: Phyla-Vell

- **Channel Cosmic Power**; type/group: Hero / Phyla-Vell; copies: Unverified; Hero Name: Phyla-Vell; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phylavell-04.png).
- **Quantum Sword**; type/group: Hero / Phyla-Vell; copies: Unverified; Hero Name: Phyla-Vell; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phylavell-03.png).
- **Martyr**; type/group: Hero / Phyla-Vell; copies: Unverified; Hero Name: Phyla-Vell; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phylavell-02.png).
- **Avatar of Oblivion**; type/group: Hero / Phyla-Vell; copies: Unverified; Hero Name: Phyla-Vell; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 8; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phylavell-01.png).

### Hero group: Quasar

- **Manipulate Gravitons**; type/group: Hero / Quasar; copies: Unverified; Hero Name: Quasar; team: Avengers; class icons: Covert; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quasar-04.png).
- **Cosmic Champion**; type/group: Hero / Quasar; copies: Unverified; Hero Name: Quasar; team: Avengers; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quasar-03.png).
- **The Quantum Bands**; type/group: Hero / Quasar; copies: Unverified; Hero Name: Quasar; team: Avengers; class icons: Covert; printed values: Cost 5; Attack 1; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quasar-02.png).
- **The Star Brand**; type/group: Hero / Quasar; copies: Unverified; Hero Name: Quasar; team: Avengers; class icons: Strength; printed values: Cost 7; Attack 4+; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/quasar-01.png).

### Hero group: Ronan the Accuser

- **Universal Weapon**; type/group: Hero / Ronan the Accuser; copies: Unverified; Hero Name: Ronan the Accuser; team: Unaffiliated; class icons: Tech; printed values: Cost 3; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronan-the-accuser-04.png).
- **Rally Kree Warriors**; type/group: Hero / Ronan the Accuser; copies: Unverified; Hero Name: Ronan the Accuser; team: Unaffiliated; class icons: Strength; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronan-the-accuser-03.png).
- **Accuse Enemies of the Empire**; type/group: Hero / Ronan the Accuser; copies: Unverified; Hero Name: Ronan the Accuser; team: Unaffiliated; class icons: Strength; printed values: Cost 6; Attack 4; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronan-the-accuser-02.png).
- **Seek the Infinity Gems**; type/group: Hero / Ronan the Accuser; copies: Unverified; Hero Name: Ronan the Accuser; team: Unaffiliated; class icons: Tech; printed values: Cost 8; Attack 4; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ronan-the-accuser-01.png).

### Hero group: Yondu

- **Whistling Arrow**; type/group: Hero / Yondu; copies: Unverified; Hero Name: Yondu; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 2; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/yondu-04.png).
- **Interstellar Hunter**; type/group: Hero / Yondu; copies: Unverified; Hero Name: Yondu; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 3; Attack 1+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/yondu-03.png).
- **Anticipate Their Movements**; type/group: Hero / Yondu; copies: Unverified; Hero Name: Yondu; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 5; Attack 1+; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/yondu-02.png).
- **Space Pirate**; type/group: Hero / Yondu; copies: Unverified; Hero Name: Yondu; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/yondu-01.png).

### Villain Group: Elders of the Universe

- **The Runner**; type/group: Villain / Elders of the Universe; copies: 2; printed values: Attack 5; VP 3; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/elders-of-the-universe-02.png).
- **The Trader**; type/group: Villain / Elders of the Universe; copies: 2; printed values: Attack 4; VP 2; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/elders-of-the-universe-01.png).
- **The Champion of the Universe**; type/group: Villain / Elders of the Universe; copies: 2; printed values: Attack 7; VP 5; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/elders-of-the-universe-04.png).
- **The Collector**; type/group: Villain / Elders of the Universe; copies: 2; printed values: Attack 6; VP 4; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/elders-of-the-universe-03.png).

### Villain Group: Celestials

- **Nezarr, The Calculator**; type/group: Villain / Celestials; copies: 2; printed values: Attack 11*; VP 4; keyword labels: Cosmic Threat, Celestial Boon; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/celestials-05.png).
- **Gammenon, The Gatherer**; type/group: Villain / Celestials; copies: 2; printed values: Attack 10*; VP 3; keyword labels: Cosmic Threat, Celestial Boon; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/celestials-01.png).
- **Exitar, The Exterminator**; type/group: Villain / Celestials; copies: 2; printed values: Attack 12*; VP 5; keyword labels: Cosmic Threat, Celestial Boon; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/celestials-02.png).
- **Arishem, The Judge**; type/group: Villain / Celestials; copies: 1; printed values: Attack 13*; VP 5; keyword labels: Cosmic Threat, Celestial Boon; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/celestials-03.png).
- **Tiamut, The Dreaming Celestial**; type/group: Villain / Celestials; copies: 1; printed values: Attack 14*; VP 6; keyword labels: Cosmic Threat, Celestial Boon; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/celestials-04.png).

### Villain Group: From Beyond

- **The Mapmakers**; type/group: Villain / From Beyond; copies: 3; printed values: Attack 7*; VP 3; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/from-beyond-03.png).
- **The Shaper of Worlds**; type/group: Villain / From Beyond; copies: 2; printed values: Attack 10*; VP 5; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/from-beyond-02.png).
- **Kubik**; type/group: Villain / From Beyond; copies: 2; printed values: Attack 11*; VP 5; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/from-beyond-01.png).
- **Kosmos**; type/group: Villain / From Beyond; copies: 1; printed values: Attack 13*; VP 6; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/from-beyond-04.png).

### Villain Group: Black Order of Thanos

- **Corvus Glaive**; type/group: Villain / Black Order of Thanos; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/black-order-of-thanos-05.png).
- **Black Dwarf**; type/group: Villain / Black Order of Thanos; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/black-order-of-thanos-04.png).
- **Supergiant**; type/group: Villain / Black Order of Thanos; copies: 2; printed values: Attack 6+; VP 5; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/black-order-of-thanos-03.png).
- **Proxima Midnight**; type/group: Villain / Black Order of Thanos; copies: 1; printed values: Attack 7+; VP 5; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/black-order-of-thanos-02.png).
- **Ebony Maw**; type/group: Villain / Black Order of Thanos; copies: 1; printed values: Attack 6+; VP 5; keyword labels: Danger Sense; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/black-order-of-thanos-01.png).

### Henchman Group: Sidera Maris, Bridge Builders

- **Sidera Maris, Bridge Builders**; type/group: Henchman / Sidera Maris, Bridge Builders; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/sidera-maris.png).

### Henchman Group: Universal Church of Truth

- **Universal Church of Truth**; type/group: Henchman / Universal Church of Truth; copies: Unverified; printed values: not indexed in C1; keyword labels: Burn Shards; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/universal-church-of-truth.png).

### Mastermind: Beyonder, The

- **The Beyonder**; type/group: Normal Mastermind face / Beyonder, The; copies: Unverified; printed values: VP 7; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/beyonder_01.png).
- **Epic Beyonder**; type/group: Epic Mastermind face / Beyonder, The; copies: Unverified; printed values: Attack 24*; VP 7; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/beyonder_02.png).
- **Playthings of a Petulant God**; type/group: Mastermind Tactic / Beyonder, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/beyonder_06.png).
- **Dimensional Collapse**; type/group: Mastermind Tactic / Beyonder, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/beyonder_05.png).
- **Pull Earth Into The Beyond**; type/group: Mastermind Tactic / Beyonder, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/beyonder_03.png).
- **Create the Secret Wars**; type/group: Mastermind Tactic / Beyonder, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/beyonder_04.png).

### Mastermind: Grandmaster, The

- **The Grandmaster**; type/group: Normal Mastermind face / Grandmaster, The; copies: Unverified; printed values: VP 6; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grandmaster_01.png).
- **Epic Grandmaster**; type/group: Epic Mastermind face / Grandmaster, The; copies: Unverified; printed values: Attack 11; VP 6; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grandmaster_02.png).
- **Deal With Death**; type/group: Mastermind Tactic / Grandmaster, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grandmaster_06.png).
- **Galactic Marathon**; type/group: Mastermind Tactic / Grandmaster, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grandmaster_05.png).
- **Cheat Against Thanos**; type/group: Mastermind Tactic / Grandmaster, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grandmaster_04.png).
- **Match Offenders vs. Defenders**; type/group: Mastermind Tactic / Grandmaster, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/grandmaster_03.png).

### Mastermind: Magus

- **Magus**; type/group: Normal Mastermind face / Magus; copies: Unverified; printed values: Attack 9+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magus_01.png).
- **Epic Magus**; type/group: Epic Mastermind face / Magus; copies: Unverified; printed values: Attack 11+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magus_02.png).
- **Dark Side of Adam Warlock**; type/group: Mastermind Tactic / Magus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magus_05.png).
- **Seize Cosmic Power**; type/group: Mastermind Tactic / Magus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magus_06.png).
- **Conjured Shade of Thanos**; type/group: Mastermind Tactic / Magus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magus_03.png).
- **Resurrected as the Child Magus**; type/group: Mastermind Tactic / Magus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/magus_04.png).

### Scheme: Contest of Champions, The

- **Contest of Champions, The**; type/group: Scheme / Contest of Champions, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Contest of Champions; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/125Scheme(29).png).

### Scheme: Turn the Soul of Adam Warlock

- **Turn the Soul of Adam Warlock**; type/group: Scheme / Turn the Soul of Adam Warlock; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/124Scheme(28).png).

### Scheme: Destroy the Nova Corps

- **Destroy the Nova Corps**; type/group: Scheme / Destroy the Nova Corps; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/126Scheme(30).png).

### Scheme: Annihilation: Conquest

- **Annihilation: Conquest**; type/group: Scheme / Annihilation: Conquest; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/127Scheme(31).png).

### Bystander set: Board Gamer

- **Board Gamer**; type/group: Bystander / Board Gamer; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander_03.png).

### Bystander set: Legendary Game Designer

- **Legendary Game Designer**; type/group: Bystander / Legendary Game Designer; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander_01.png).

### Bystander set: Pizza Delivery Guy

- **Pizza Delivery Guy**; type/group: Bystander / Pizza Delivery Guy; copies: 1; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander_02.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.

## Difficulty data (#269, 2026-10-14)

`Data/Difficulty/into-the-cosmos.json` (format v3, source `Q`). Card facts: the shared OCR cache (titles and keyword/stack markers only, no card text), the C1 index above for names, classes, costs, printed Recruit/Attack icons and Villain copies, and the owner's LegendaryAssistant data (`into-the-cosmos-allies`, `-adversaries`, `-commanders`, `-followers`, `-schemes`) for what each card does. Names follow C1 (Kosmos, Pull Earth Into The Beyond, Playthings of a Petulant God).

- **Class conflicts.** None found between C1 and the owner's data; C1 is used.
- **Copies: default.** No source gives Hero copies; each Hero's four slots are 5, 5, 3, 1 in ascending cost order (every Hero's costs are distinct). Villain copies are C1's.
- **Printed 0+ icons.** Daring Raid (Recruit 0+, Attack 5), Draw From the Worldmind (both 0+) and Electromagnetic Wave (Recruit 0+ with a Shard or Attack choice) are `both`. Cards with a printed N+ carry one icon: `attack`. Cards that only gain Shards, which spend as +1 Recruit, or put a Hero in play are `recruit`; cards with neither (Regenerative Cocoon, Declare Galactic Threat, Mobilize the Nova Corps) are `none`.
- **Draws.** Conditional and chosen draws are `draw` as in core: Kree Genetics 1, Peaceful Meditation 1, Draw From the Worldmind 1, Channel Cosmic Power 1, Interstellar Hunter 1, Universal Weapon 2 (after a Burn), Declare Galactic Threat 2, Mobilize the Nova Corps 3, Regenerative Cocoon 2.
- **Wants.** Cards that pay off at a stock of Shards want `stack shards` (strength 2 where they need many, 1 otherwise); Avengers and Guardians payoffs want that team (Space Pirate and Manifest the Soul Gem, three and two icons, strength 3 and 2); class superpowers want that class. Shard wants and Burn Shards are not scored yet.
- **Self costs.** Regenerative Cocoon needs a discard to play: `discard`. Optional KOs and Burn effects are not tagged.
- **Answers.** Martyr cancels a Wound to any player: `wounds` 1.
- **Masterminds.** The Beyonder's Master Strike (reveal a 5+ card or gain a Wound; a Pocket Dimension takes an HQ space and adds an Attack surcharge): `wounds 2`, `hq-disruption 2`, `attack-inflation 1`. The Grandmaster's Strike (Contest, losers gain a Wound, he gains a Shard): `wounds 2`, `attack-inflation 1`; each Tactic is a Contest on one class with the Grandmaster gaining Shards on a win: a `check` on that class failing into `attack-inflation`. Magus's Strike (Wounds if a Villain has Shards, a Cosmic Wraith Villain, Shards on every Villain): `wounds 2`, `attack-inflation 2`, `escape-pressure 1`; his Tactics that give him or Villains Shards are `attack-inflation 1` (Resurrected as the Child Magus, which limits the next fight to Recruit, too).
- **Villain Groups.** Black Order: Danger Sense reveals that play a Master Strike, Villain or Twist are `twist-density` (1, or 2 for Proxima Midnight and Ebony Maw, who play a Twist or all of a chosen type), a captured Bystander `bystander-pressure`, the shared Attack bonus `attack-inflation 1`, Black Dwarf's Fight KO `deck-thinning 1`, Proxima's Escape a `check` on Instinct into Wounds. Celestials: Cosmic Threat is a `gate` on the first of its two classes, severity 1 (two ways to meet it); Escapes are `bystander-pressure 2` (Gammenon), `attack-inflation 2` (Nezarr), `hq-disruption 1` (Arishem) and `hand-disruption 3` (Tiamut, hand size down for good). Elders: each Ambush Contest is a `check` on its icon class (Runner Instinct into discard, Trader Tech into HQ trade, Collector Covert into hand loss, Champion Strength into Wounds), each with the Shard gain `attack-inflation 1`; the Runner's push is `escape-pressure 1`, the Champion's Fight KO `deck-thinning 1`. From Beyond: single-class Cosmic Threat is a `gate`, severity 2 as for the Heralds in Fantastic Four; the Mapmakers' Fight KO `deck-thinning 1`, the Shaper's Escape `hq-disruption 2`, Kubik's Ambush `wounds 1`.
- **Henchman Groups.** Sidera Maris: `attack-inflation 1` (Shard to a Villain) and the Fight KO `deck-thinning 1`; Universal Church of Truth: `attack-inflation 1` (Shard to each Henchman).
- **Schemes.** The Contest of Champions `hand-disruption 3` (discard on each lost Contest, Evil Triumphs end the game), amplifies `hand-disruption`; Turn the Soul of Adam Warlock `attack-inflation 3` (Purify costs double a card's cost or it corrupts), amplifies `attack-inflation`; Destroy the Nova Corps `deck-thinning 2` and `hand-disruption 1`, amplifies `deck-thinning`; Annihilation: Conquest `hq-disruption 2` and `escape-pressure 2`, amplifies `escape-pressure`.

### Difficulty: no axis

- Burn Shards, Shard stock and Danger Sense are Hero-side mechanics with no want kind scored yet; they are tagged as `stack shards` wants only. Celestial Boons, Cosmic Threat on Hero cards and Contest of Champions on Hero cards carry no tag.
- Cosmic Threat on the Beyonder (a 5+/6+ card) has no gate field on a Mastermind; untagged.
- Contest of Champions as a Hero card only hits or helps others (Accuse Enemies of the Empire, Seek the Infinity Gems): untagged.
- Exitar's Escape (KO 5 Henchmen from the Villain Deck) has no axis; the card carries only its gate.
- The Contest of Champions' Evil Triumph counter and Annihilation: Conquest's Phalanx-Infected count are tagged as the closest axes; their loss conditions have no axis. Evil choosing from more Hero Deck cards each Contest has none either.
- Kubik's reveal-two-same-cost Ambush has no class or team requirement, so it is `wounds 1`, not a `check`.
- Tactics untagged because they only hit other players or only help the fighter: Playthings of a Petulant God, Create the Secret Wars. Passive attack bonuses of Magus per Villain with Shards and the Epic faces are tagged on the Master Strike slot.
- Per-effect timing (#292): Black Dwarf, Champion of the Universe, Proxima Midnight and Sidera Maris are listed once per timing, each effect at its own.
