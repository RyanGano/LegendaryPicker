# Legendary: Villains (Jul 2014)

**Research status: Partial.** The official rulebook resolves the Cops/Backup Adversary question and clarifies part of Crown Thor's setup, and the Setup lines of the two Plots #84 added were read from clear card images (Card, linked below). Other per-face values remain unverified.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| VIL | [Upper Deck Legendary: Villains rulebook](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf) | Rules and setup (pp.5–7), specific Plot clarifications (p.17), contents (p.22). |
| C1 | [master-strike structured villains card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/villains.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #84 | [Missing Villains Plots issue](https://github.com/RyanGano/LegendaryPicker/issues/84) | Identified the two Plots then absent; not evidence for card facts. |
| Card | The printed card: [Cage Villains in Power-Suppressing Cells](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/23Scheme(3).png) and [Crown Thor King of Asgard](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/26Scheme(6).png) card images | The two Plots' printed Setup lines. |
| Catalog | [`villains.json`](../../LegendaryPickerService/Data/Boxes/villains.json) | Existing runtime catalog pointer; not independent evidence for card text. |
| Runtime | [`villains.json`](../../LegendaryPickerService/Data/Boxes/villains.json) | Existing runtime catalog and its cited setup/rules sources; not duplicated here. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release order, product status, and ruleset classification. |

## Officially verified facts

- **Cage Villains in Power-Suppressing Cells:** Cops placed beside this Plot do not count as a Backup Adversary Group in the Adversary Deck (VIL p.17). This resolves the issue's question about whether the set-aside Cops occupy a Backup-group slot; it does not establish the Plot's exact setup counts.
- **Crown Thor King of Asgard:** The rulebook's clarification says the Thor Adversary is set beside this Plot whether or not the Avengers group is in the Adversary Deck (VIL p.17). An overrun during the Plot also resolves Thor's regular Overrun effects along with the Plot's added effects (VIL p.17).
- The runtime catalog contains all 8 of the product's Plots; their card-level setup details are not copied here; see `villains.json`.

## Plot Setup lines read from card images

Each value below was read from the linked card image's Setup line and is cited `Card` in `villains.json` (#84).

| Plot | Plot Twists | Set beside the Plot | Card image |
|---|---:|---|---|
| Cage Villains in Power-Suppressing Cells | 8 | 2 Cops per player | [image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/23Scheme(3).png) |
| Crown Thor King of Asgard | 8 | The Thor Adversary (of the Avengers) | [image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/26Scheme(6).png) |

## Card-by-card catalog

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Printed values or setup facts absent from the index remain **Unverified**; C1 ability prose and `villains.json` do not establish rules.

### Ally faces

- **Bullseye:** Fulfill the Contract; Everything's a Weapon; Specialist Assassin; Perfect Aim.
- **Dr. Octopus:** Brilliant Research; Crazed Experiments; Eighth Time's a Charm; Octo-Pulverize.
- **Electro:** Electroshock Therapy; Shocking Robbery; Supercharge; Anti-Matter.
- **Enchantress:** Enchant the Senses; Soul Sacrifice; Irresistible Bribe; Unending Anguish.
- **Green Goblin:** Pumpkin Bombs; Goblin Glider; Unstable Kidnapper; Experimental Goblin Serum.
- **Juggernaut:** Crimson Gem of Cyttorak; Size Matters; Runaway Train; Unstoppable Force.
- **Kingpin:** Pull the Strings; Recruitment Day; Import Illegal Weapons; Endless Underlings.
- **Kraven:** Ceaseless Tracker; Corner the Prey; Hunt Down; He's the Best Around.
- **Loki:** All Humans Are Expendable; Illusionary Bindings; Father of Lies; God of Mischief.
- **Magneto:** Magnetic Levitation; Mutants Will Rule; Weapons from Scrap Metal; Master of Magnetism.
- **Mysterio:** Psychedelic Mist; Shifting Decoy; Holographic Illusion; False Reflection.
- **Mystique:** Show Your True Colors; Hidden Weapons; Turn the Tide; Spy Games.
- **Sabretooth:** Leap of the Tiger; Take One for the Team; Stealthy Predator; Upper Hand.
- **Ultron:** Encephalo-Ray; Army of Ultrons; Genetic Experimentation; Molecular Rearrangement.
- **Venom:** Symbiote Takeover; Devour; Horrify the Populace; Ravenous Greed.

For each listed Ally, card-face numeric values, exact terms, class metadata, and ability summary are **Unverified**; VIL p.22 supports the aggregate Ally-group counts only.

### Adversary faces

- **Avengers:** Ant-Man; Captain America; Hulk; Iron Man; Thor; Wasp.
- **Defenders:** Daredevil; Iron Fist; Namor, The Sub-Mariner; Luke Cage.
- **Marvel Knights:** Black Panther; Elektra; Punisher; Ghost Rider.
- **Spider Friends:** Black Cat; Firestar; Moon Knight; Spider-Man.
- **Uncanny Avengers:** Havok; Rogue; Scarlet Witch; Wolverine.
- **Uncanny X-Men:** Colossus; Nightcrawler; Shadowcat; Storm.
- **X-Men First Class:** Angel; Iceman; Jean Grey; Beast; Cyclops.

The entries above are C1-listed Adversary titles and group mappings. Their printed values, terms, Ambush/Overrun/escape abilities, and copy counts are **Unverified** here.

### Mastermind, Tactic, Henchman, and Plot faces

- **Dr. Strange:** Dr. Strange; Book of the Vishanti; Crimson Bands of Cyttorak; Eye of Agamotto; Winds of Watoomb.
- **Nick Fury:** Nick Fury; Bounty on Fury's Head; Purge Hydra; The Avengers Initiative; Total Fury.
- **Odin:** Odin; Divine Justice; Might of Valhalla; Riches of Asgard; Ride of the Valkyries.
- **Professor X:** Professor X; Cerebro Device; Mental Dominance; Mightiest Mutant Mind; Telepathic Imprisonment.
- **Henchman Groups:** Asgardian Warriors; Cops; Multiple Man; S.H.I.E.L.D. Assault Squad. Individual Henchman faces are included in the appended C1 index where catalogued.
- **Plots:** Build an Underground MegaVault Prison; Cage Villains in Power-Suppressing Cells; Crown Thor King of Asgard; Crush HYDRA; Graduation at Xavier's X-Academy; Infiltrate the Lair with Spies; Mass Produce War Machine Armor; Resurrect Heroes with Norn Stones.
- **Other named shared-card faces in C1:** Bystander; Computer Hacker; Engineer; Public Speaker; Rock Star; Bindings; Madame HYDRA; New Recruit. C1 does not establish the relevant supply counts or printed card behavior.

The rulebook verifies that **Cage Villains in Power-Suppressing Cells** excludes set-aside Cops from the Backup Adversary Group count (VIL p.17); the Plot's own card gives its counts (see *Plot Setup lines read from card images*). It verifies that **Crown Thor King of Asgard** sets Thor beside the Plot regardless of whether Avengers are in the Adversary Deck and that an Overrun resolves Thor's regular Overrun effects as well as the Plot effect (VIL p.17). Other per-face values, setup details, terms, and abilities remain **Unverified**.

## Open evidence gap

None for the Plots' Setup lines, which the card images above settle. Per-face values listed as **Unverified** elsewhere in this record stay unverified.

The setup table lists 12 Madame HYDRA and 41 Bystanders (VIL p.5); the contents list gives 15 and 42, respectively (VIL p.22). The existing catalog follows the Game Setup instructions, as recorded in `Docs/Plan.md`. This research does not change that existing decision.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Ally group: Bullseye

- **Fulfill the Contract**; type/group: Ally / Bullseye; copies: Unverified; Hero Name: Bullseye; team: Crime Syndicate; class icons: Instinct; printed values: Cost 2; Recruit 0+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bullseye-03.png).
- **Everything's a Weapon**; type/group: Ally / Bullseye; copies: Unverified; Hero Name: Bullseye; team: Crime Syndicate; class icons: Ranged; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bullseye-04.png).
- **Specialist Assassin**; type/group: Ally / Bullseye; copies: Unverified; Hero Name: Bullseye; team: Crime Syndicate; class icons: Covert; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bullseye-02.png).
- **Perfect Aim**; type/group: Ally / Bullseye; copies: Unverified; Hero Name: Bullseye; team: Crime Syndicate; class icons: Ranged; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bullseye-01.png).

### Ally group: Dr. Octopus

- **Brilliant Research**; type/group: Ally / Dr. Octopus; copies: Unverified; Hero Name: Dr. Octopus; team: Sinister Six; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-octopus-03.png).
- **Crazed Experiments**; type/group: Ally / Dr. Octopus; copies: Unverified; Hero Name: Dr. Octopus; team: Sinister Six; class icons: Tech; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-octopus-04.png).
- **Eighth Time's a Charm**; type/group: Ally / Dr. Octopus; copies: Unverified; Hero Name: Dr. Octopus; team: Sinister Six; class icons: Strength; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-octopus-02.png).
- **Octo-Pulverize**; type/group: Ally / Dr. Octopus; copies: Unverified; Hero Name: Dr. Octopus; team: Sinister Six; class icons: Tech; printed values: Cost 8; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-octopus-01.png).

### Ally group: Electro

- **Electroshock Therapy**; type/group: Ally / Electro; copies: Unverified; Hero Name: Electro; team: Sinister Six; class icons: Ranged; printed values: Cost 2; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/electro-03.png).
- **Shocking Robbery**; type/group: Ally / Electro; copies: Unverified; Hero Name: Electro; team: Sinister Six; class icons: Ranged; printed values: Cost 3; Attack 0+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/electro-04.png).
- **Supercharge**; type/group: Ally / Electro; copies: Unverified; Hero Name: Electro; team: Sinister Six; class icons: Instinct; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/electro-02.png).
- **Anti-Matter**; type/group: Ally / Electro; copies: Unverified; Hero Name: Electro; team: Sinister Six; class icons: Ranged; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/electro-01.png).

### Ally group: Enchantress

- **Enchant the Senses**; type/group: Ally / Enchantress; copies: Unverified; Hero Name: Enchantress; team: Foes of Asgard; class icons: Ranged; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/enchantress-04.png).
- **Soul Sacrifice**; type/group: Ally / Enchantress; copies: Unverified; Hero Name: Enchantress; team: Foes of Asgard; class icons: Covert; printed values: Cost 4; Recruit 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/enchantress-03.png).
- **Irresistible Bribe**; type/group: Ally / Enchantress; copies: Unverified; Hero Name: Enchantress; team: Foes of Asgard; class icons: Covert; printed values: Cost 6; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/enchantress-02.png).
- **Unending Anguish**; type/group: Ally / Enchantress; copies: Unverified; Hero Name: Enchantress; team: Foes of Asgard; class icons: Covert; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/enchantress-01.png).

### Ally group: Green Goblin

- **Pumpkin Bombs**; type/group: Ally / Green Goblin; copies: Unverified; Hero Name: Green Goblin; team: Sinister Six; class icons: Tech; printed values: Cost 3; Attack 1+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/green-goblin-04.png).
- **Goblin Glider**; type/group: Ally / Green Goblin; copies: Unverified; Hero Name: Green Goblin; team: Sinister Six; class icons: Tech; printed values: Cost 4; Attack 2; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/green-goblin-03.png).
- **Unstable Kidnapper**; type/group: Ally / Green Goblin; copies: Unverified; Hero Name: Green Goblin; team: Sinister Six; class icons: Instinct; printed values: Cost 5; Recruit 3; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/green-goblin-02.png).
- **Experimental Goblin Serum**; type/group: Ally / Green Goblin; copies: Unverified; Hero Name: Green Goblin; team: Sinister Six; class icons: Tech; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/green-goblin-01.png).

### Ally group: Juggernaut

- **Crimson Gem of Cyttorak**; type/group: Ally / Juggernaut; copies: Unverified; Hero Name: Juggernaut; team: Brotherhood; class icons: Strength; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/juggernaut-03.png).
- **Size Matters**; type/group: Ally / Juggernaut; copies: Unverified; Hero Name: Juggernaut; team: Brotherhood; class icons: Strength; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/juggernaut-04.png).
- **Runaway Train**; type/group: Ally / Juggernaut; copies: Unverified; Hero Name: Juggernaut; team: Brotherhood; class icons: Strength; printed values: Cost 5; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/juggernaut-02.png).
- **Unstoppable Force**; type/group: Ally / Juggernaut; copies: Unverified; Hero Name: Juggernaut; team: Brotherhood; class icons: Strength; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/juggernaut-01.png).

### Ally group: Kingpin

- **Pull the Strings**; type/group: Ally / Kingpin; copies: Unverified; Hero Name: Kingpin; team: Crime Syndicate; class icons: Covert; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kingpin-03-1.png).
- **Recruitment Day**; type/group: Ally / Kingpin; copies: Unverified; Hero Name: Kingpin; team: Crime Syndicate; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kingpin-04-1.png).
- **Import Illegal Weapons**; type/group: Ally / Kingpin; copies: Unverified; Hero Name: Kingpin; team: Crime Syndicate; class icons: Tech; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kingpin-02-1.png).
- **Endless Underlings**; type/group: Ally / Kingpin; copies: Unverified; Hero Name: Kingpin; team: Crime Syndicate; class icons: Strength; printed values: Cost 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kingpin-01-1.png).

### Ally group: Kraven

- **Ceaseless Tracker**; type/group: Ally / Kraven; copies: Unverified; Hero Name: Kraven; team: Sinister Six; class icons: Instinct; printed values: Cost 2; Recruit 1+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kraven-03.png).
- **Corner the Prey**; type/group: Ally / Kraven; copies: Unverified; Hero Name: Kraven; team: Sinister Six; class icons: Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kraven-04.png).
- **Hunt Down**; type/group: Ally / Kraven; copies: Unverified; Hero Name: Kraven; team: Sinister Six; class icons: Strength; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kraven-02.png).
- **He's the Best Around**; type/group: Ally / Kraven; copies: Unverified; Hero Name: Kraven; team: Sinister Six; class icons: Instinct; printed values: Cost 8; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/kraven-01.png).

### Ally group: Loki

- **All Humans Are Expendable**; type/group: Ally / Loki; copies: Unverified; Hero Name: Loki; team: Foes of Asgard; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/loki-03-1.png).
- **Illusionary Bindings**; type/group: Ally / Loki; copies: Unverified; Hero Name: Loki; team: Foes of Asgard; class icons: Ranged; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/loki-04-1.png).
- **Father of Lies**; type/group: Ally / Loki; copies: Unverified; Hero Name: Loki; team: Foes of Asgard; class icons: Covert; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/loki-02-1.png).
- **God of Mischief**; type/group: Ally / Loki; copies: Unverified; Hero Name: Loki; team: Foes of Asgard; class icons: Covert; printed values: Cost 8; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/loki-01-1.png).

### Ally group: Magneto

- **Magnetic Levitation**; type/group: Ally / Magneto; copies: Unverified; Hero Name: Magneto; team: Brotherhood; class icons: Ranged; printed values: Cost 3; Attack 1+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magneto-03-1.png).
- **Mutants Will Rule**; type/group: Ally / Magneto; copies: Unverified; Hero Name: Magneto; team: Brotherhood; class icons: Strength; printed values: Cost 4; Recruit 2+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magneto-04-1.png).
- **Weapons from Scrap Metal**; type/group: Ally / Magneto; copies: Unverified; Hero Name: Magneto; team: Brotherhood; class icons: Ranged; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magneto-02-1.png).
- **Master of Magnetism**; type/group: Ally / Magneto; copies: Unverified; Hero Name: Magneto; team: Brotherhood; class icons: Ranged; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magneto-01-1.png).

### Ally group: Mysterio

- **Psychedelic Mist**; type/group: Ally / Mysterio; copies: Unverified; Hero Name: Mysterio; team: Sinister Six; class icons: Ranged; printed values: Cost 2; Attack 1+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mysterio-04.png).
- **Shifting Decoy**; type/group: Ally / Mysterio; copies: Unverified; Hero Name: Mysterio; team: Sinister Six; class icons: Covert; printed values: Cost 3; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mysterio-03.png).
- **Holographic Illusion**; type/group: Ally / Mysterio; copies: Unverified; Hero Name: Mysterio; team: Sinister Six; class icons: Tech; printed values: Cost 5; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mysterio-02.png).
- **False Reflection**; type/group: Ally / Mysterio; copies: Unverified; Hero Name: Mysterio; team: Sinister Six; class icons: Instinct; printed values: Cost 7; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mysterio-01.png).

### Ally group: Mystique

- **Show Your True Colors**; type/group: Ally / Mystique; copies: Unverified; Hero Name: Mystique; team: Brotherhood; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mystique-04.png).
- **Hidden Weapons**; type/group: Ally / Mystique; copies: Unverified; Hero Name: Mystique; team: Brotherhood; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mystique-03.png).
- **Turn the Tide**; type/group: Ally / Mystique; copies: Unverified; Hero Name: Mystique; team: Brotherhood; class icons: Instinct; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mystique-02.png).
- **Spy Games**; type/group: Ally / Mystique; copies: Unverified; Hero Name: Mystique; team: Brotherhood; class icons: Covert; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mystique-01.png).

### Ally group: Sabretooth

- **Leap of the Tiger**; type/group: Ally / Sabretooth; copies: Unverified; Hero Name: Sabretooth; team: Brotherhood; class icons: Instinct; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sabretooth-03.png).
- **Take One for the Team**; type/group: Ally / Sabretooth; copies: Unverified; Hero Name: Sabretooth; team: Brotherhood; class icons: Instinct; printed values: Cost 4; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sabretooth-04.png).
- **Stealthy Predator**; type/group: Ally / Sabretooth; copies: Unverified; Hero Name: Sabretooth; team: Brotherhood; class icons: Covert; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sabretooth-02.png).
- **Upper Hand**; type/group: Ally / Sabretooth; copies: Unverified; Hero Name: Sabretooth; team: Brotherhood; class icons: Strength; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sabretooth-01.png).

### Ally group: Ultron

- **Encephalo-Ray**; type/group: Ally / Ultron; copies: Unverified; Hero Name: Ultron; team: Unaffiliated; class icons: Tech; printed values: Cost 2; Attack 0+; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultron-04.png).
- **Army of Ultrons**; type/group: Ally / Ultron; copies: Unverified; Hero Name: Ultron; team: Unaffiliated; class icons: Tech; printed values: Cost 3; Recruit 2; keyword labels: Dodge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultron-03.png).
- **Genetic Experimentation**; type/group: Ally / Ultron; copies: Unverified; Hero Name: Ultron; team: Unaffiliated; class icons: Tech; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultron-02.png).
- **Molecular Rearrangement**; type/group: Ally / Ultron; copies: Unverified; Hero Name: Ultron; team: Unaffiliated; class icons: Tech; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultron-01.png).

### Ally group: Venom

- **Symbiote Takeover**; type/group: Ally / Venom; copies: Unverified; Hero Name: Venom; team: Sinister Six; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-03.png).
- **Devour**; type/group: Ally / Venom; copies: Unverified; Hero Name: Venom; team: Sinister Six; class icons: Instinct; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-04.png).
- **Horrify the Populace**; type/group: Ally / Venom; copies: Unverified; Hero Name: Venom; team: Sinister Six; class icons: Strength; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-02.png).
- **Ravenous Greed**; type/group: Ally / Venom; copies: Unverified; Hero Name: Venom; team: Sinister Six; class icons: Instinct; printed values: Cost 7; Recruit 0+; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/venom-01.png).

### Adversary Group: Avengers

- **Ant-Man**; type/group: Adversary / Avengers; copies: 2; printed values: Attack 3*; VP 2; keyword labels: Elusive; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/avengers-04.png).
- **Captain America**; type/group: Adversary / Avengers; copies: 1; printed values: Attack 4+; VP 5; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/avengers-01.png).
- **Hulk**; type/group: Adversary / Avengers; copies: 1; printed values: Attack 8; VP 6; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/avengers-05.png).
- **Iron Man**; type/group: Adversary / Avengers; copies: 1; printed values: Attack 7; VP 5; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/avengers-06.png).
- **Thor**; type/group: Adversary / Avengers; copies: 1; printed values: Attack 7; VP 5; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/avengers-02.png).
- **Wasp**; type/group: Adversary / Avengers; copies: 2; printed values: Attack 1*; VP 4; keyword labels: Elusive; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/avengers-03.png).

### Adversary Group: Defenders

- **Daredevil**; type/group: Adversary / Defenders; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/defenders-02.png).
- **Iron Fist**; type/group: Adversary / Defenders; copies: 2; printed values: Attack 3*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/defenders-03.png).
- **Namor, The Sub-Mariner**; type/group: Adversary / Defenders; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/defenders-01.png).
- **Luke Cage**; type/group: Adversary / Defenders; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/defenders-04.png).

### Adversary Group: Marvel Knights

- **Black Panther**; type/group: Adversary / Marvel Knights; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marvel-knights-03.png).
- **Elektra**; type/group: Adversary / Marvel Knights; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marvel-knights-02.png).
- **Punisher**; type/group: Adversary / Marvel Knights; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marvel-knights-04.png).
- **Ghost Rider**; type/group: Adversary / Marvel Knights; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marvel-knights-01.png).

### Adversary Group: Spider Friends

- **Black Cat**; type/group: Adversary / Spider Friends; copies: 2; printed values: Attack 2*; VP 2; keyword labels: Elusive; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-friends-03.png).
- **Firestar**; type/group: Adversary / Spider Friends; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-friends-01.png).
- **Moon Knight**; type/group: Adversary / Spider Friends; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-friends-02.png).
- **Spider-Man**; type/group: Adversary / Spider Friends; copies: 2; printed values: Attack 2; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/spider-friends-04.png).

### Adversary Group: Uncanny Avengers

- **Havok**; type/group: Adversary / Uncanny Avengers; copies: 2; printed values: Attack 4+; VP 2; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-avengers-01.png).
- **Rogue**; type/group: Adversary / Uncanny Avengers; copies: 2; printed values: Attack 4+; VP 2; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-avengers-04.png).
- **Scarlet Witch**; type/group: Adversary / Uncanny Avengers; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-avengers-02.png).
- **Wolverine**; type/group: Adversary / Uncanny Avengers; copies: 2; printed values: Attack 7+; VP 5; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-avengers-03.png).

### Adversary Group: Uncanny X-Men

- **Colossus**; type/group: Adversary / Uncanny X-Men; copies: 2; printed values: Attack 5+; VP 3; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-x-men-03.png).
- **Nightcrawler**; type/group: Adversary / Uncanny X-Men; copies: 2; printed values: Attack 4+; VP 2; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-x-men-02.png).
- **Shadowcat**; type/group: Adversary / Uncanny X-Men; copies: 2; printed values: Attack 2*+*; VP 2; keyword labels: X-Treme Attack, Elusive; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-x-men-01.png).
- **Storm**; type/group: Adversary / Uncanny X-Men; copies: 2; printed values: Attack 4+; VP 2; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/uncanny-x-men-04.png).

### Adversary Group: X-Men First Class

- **Angel**; type/group: Adversary / X-Men First Class; copies: 2; printed values: Attack 4+; VP 2; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-first-class-03.png).
- **Iceman**; type/group: Adversary / X-Men First Class; copies: 2; printed values: Attack 5+; VP 3; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-first-class-01.png).
- **Jean Grey**; type/group: Adversary / X-Men First Class; copies: 1; printed values: Attack 6+; VP 4; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-first-class-04.png).
- **Beast**; type/group: Adversary / X-Men First Class; copies: 2; printed values: Attack 5+; VP 3; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-first-class-05.png).
- **Cyclops**; type/group: Adversary / X-Men First Class; copies: 1; printed values: Attack 6+; VP 4; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-first-class-02.png).

### Backup Adversary group: Asgardian Warriors

- **Asgardian Warriors**; type/group: Henchman / Asgardian Warriors; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/asgardian-warrior.png).

### Backup Adversary group: Cops

- **Cops**; type/group: Henchman / Cops; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/cops.png).

### Backup Adversary group: Multiple Man

- **Multiple Man**; type/group: Henchman / Multiple Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/multiple-man.png).

### Backup Adversary group: S.H.I.E.L.D. Assault Squad

- **S.H.I.E.L.D. Assault Squad**; type/group: Henchman / S.H.I.E.L.D. Assault Squad; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/shield-assault-squad.png).

### Commander: Dr. Strange

- **Dr. Strange**; type/group: Normal Commander face / Dr. Strange; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-strange-01.png).
- **Book of the Vishanti**; type/group: Commander Tactic / Dr. Strange; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-strange-05.png).
- **Crimson Bands of Cyttorak**; type/group: Commander Tactic / Dr. Strange; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-strange-03.png).
- **Eye of Agamotto**; type/group: Commander Tactic / Dr. Strange; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-strange-04.png).
- **Winds of Watoomb**; type/group: Commander Tactic / Dr. Strange; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/dr-strange-02.png).

### Commander: Nick Fury

- **Nick Fury**; type/group: Normal Commander face / Nick Fury; copies: Unverified; printed values: VP 6; keyword labels: Demolish; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nick-fury-01-1.png).
- **Bounty on Fury's Head**; type/group: Commander Tactic / Nick Fury; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nick-fury-03-1.png).
- **Purge Hydra**; type/group: Commander Tactic / Nick Fury; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nick-fury-05.png).
- **The Avengers Initiative**; type/group: Commander Tactic / Nick Fury; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nick-fury-02-1.png).
- **Total Fury**; type/group: Commander Tactic / Nick Fury; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nick-fury-04-1.png).

### Commander: Odin

- **Odin**; type/group: Normal Commander face / Odin; copies: Unverified; printed values: Attack 10+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/odin-01.png).
- **Divine Justice**; type/group: Commander Tactic / Odin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/odin-02.png).
- **Might of Valhalla**; type/group: Commander Tactic / Odin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/odin-03.png).
- **Riches of Asgard**; type/group: Commander Tactic / Odin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/odin-04.png).
- **Ride of the Valkyries**; type/group: Commander Tactic / Odin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/odin-05.png).

### Commander: Professor X

- **Professor X**; type/group: Normal Commander face / Professor X; copies: Unverified; printed values: Attack 8+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/professor-x-01-1.png).
- **Cerebro Device**; type/group: Commander Tactic / Professor X; copies: Unverified; printed values: not indexed in C1; keyword labels: X-Treme Attack; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/professor-x-05.png).
- **Mental Dominance**; type/group: Commander Tactic / Professor X; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/professor-x-03-1.png).
- **Mightiest Mutant Mind**; type/group: Commander Tactic / Professor X; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/professor-x-04-1.png).
- **Telepathic Imprisonment**; type/group: Commander Tactic / Professor X; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/professor-x-02-1.png).

### Plot: Build an Underground MegaVault Prison

- **Build an Underground MegaVault Prison**; type/group: Plot / Build an Underground MegaVault Prison; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/21Scheme(1).png).

### Plot: Cage Villains in Power-Suppressing Cells

- **Cage Villains in Power-Suppressing Cells**; type/group: Plot / Cage Villains in Power-Suppressing Cells; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/23Scheme(3).png).

### Plot: Crown Thor King of Asgard

- **Crown Thor King of Asgard**; type/group: Plot / Crown Thor King of Asgard; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/26Scheme(6).png).

### Plot: Crush HYDRA

- **Crush HYDRA**; type/group: Plot / Crush HYDRA; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/22Scheme(2).png).

### Plot: Graduation at Xavier's X-Academy

- **Graduation at Xavier's X-Academy**; type/group: Plot / Graduation at Xavier's X-Academy; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/24Scheme(4).png).

### Plot: Infiltrate the Lair with Spies

- **Infiltrate the Lair with Spies**; type/group: Plot / Infiltrate the Lair with Spies; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/25Scheme(5).png).

### Plot: Mass Produce War Machine Armor

- **Mass Produce War Machine Armor**; type/group: Plot / Mass Produce War Machine Armor; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/28Scheme(8).png).

### Plot: Resurrect Heroes with Norn Stones

- **Resurrect Heroes with Norn Stones**; type/group: Plot / Resurrect Heroes with Norn Stones; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/27Scheme(7).png).

### Bystander set: Bystander

- **Bystander**; type/group: Bystander / Bystander; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander.png).

### Bystander set: Computer Hacker

- **Computer Hacker**; type/group: Bystander / Computer Hacker; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-computer-hacker.png).

### Bystander set: Engineer

- **Engineer**; type/group: Bystander / Engineer; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-engineer.png).

### Bystander set: Public Speaker

- **Public Speaker**; type/group: Bystander / Public Speaker; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-public-speaker.png).

### Bystander set: Rock Star

- **Rock Star**; type/group: Bystander / Rock Star; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-rock-star.png).

### Wound set: Bindings

- **Bindings**; type/group: Wound / Bindings; copies: 30; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Wounds/wounds.png).

### Officer set: Madame HYDRA

- **Madame HYDRA**; type/group: Officer / Madame HYDRA; copies: 15; printed values: Cost 3; Recruit 2; keyword labels: Dodge; card image: unavailable in C1.

### Sidekick set: New Recruit

- **New Recruit**; type/group: Sidekick / New Recruit; copies: 15; printed values: Cost 2; Attack 1; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
