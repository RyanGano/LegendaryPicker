# Fantastic Four (Oct 2013)

**Research status: Partial.** Runtime integration exists; this note indexes C1 per-face metadata without duplicating the existing catalog and its cited setup values in [`fantastic-four.json`](../../LegendaryPickerService/Data/Boxes/fantastic-four.json). Remaining research gaps are listed below.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured fantastic-four card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/ff.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| Runtime | [`fantastic-four.json`](../../LegendaryPickerService/Data/Boxes/fantastic-four.json) | Existing runtime catalog and its cited setup/rules sources; not duplicated here. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release order, product status, and ruleset classification. |

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Human Torch

- **Call for Backup**; type/group: Hero / Human Torch; copies: Unverified; Hero Name: Human Torch; team: Fantastic Four; class icons: Instinct; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/human-torch-03.png).
- **Hothead**; type/group: Hero / Human Torch; copies: Unverified; Hero Name: Human Torch; team: Fantastic Four; class icons: Ranged; printed values: Cost 4; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/human-torch-04.png).
- **Flame On!**; type/group: Hero / Human Torch; copies: Unverified; Hero Name: Human Torch; team: Fantastic Four; class icons: Ranged; printed values: Cost 6; Attack 4+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/human-torch-02.png).
- **Nova Flame**; type/group: Hero / Human Torch; copies: Unverified; Hero Name: Human Torch; team: Fantastic Four; class icons: Ranged; printed values: Cost 8; Attack 6+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/human-torch-01.png).

### Hero group: Invisible Woman

- **Disappearing Act**; type/group: Hero / Invisible Woman; copies: Unverified; Hero Name: Invisible Woman; team: Fantastic Four; class icons: Covert; printed values: Cost 4; Recruit 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/invisible-woman-03.png).
- **Four of a Kind**; type/group: Hero / Invisible Woman; copies: Unverified; Hero Name: Invisible Woman; team: Fantastic Four; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/invisible-woman-04.png).
- **Unseen Rescue**; type/group: Hero / Invisible Woman; copies: Unverified; Hero Name: Invisible Woman; team: Fantastic Four; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/invisible-woman-02.png).
- **Invisible Barrier**; type/group: Hero / Invisible Woman; copies: Unverified; Hero Name: Invisible Woman; team: Fantastic Four; class icons: Covert; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/invisible-woman-01.png).

### Hero group: Mr. Fantastic

- **Twisting Equations**; type/group: Hero / Mr. Fantastic; copies: Unverified; Hero Name: Mr. Fantastic; team: Fantastic Four; class icons: Tech; printed values: Cost 3; Recruit 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mr-fantastic-04.png).
- **Unstable Molecules**; type/group: Hero / Mr. Fantastic; copies: Unverified; Hero Name: Mr. Fantastic; team: Fantastic Four; class icons: Tech; printed values: Cost 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mr-fantastic-03.png).
- **One Gigantic Hand**; type/group: Hero / Mr. Fantastic; copies: Unverified; Hero Name: Mr. Fantastic; team: Fantastic Four; class icons: Instinct; printed values: Cost 5; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mr-fantastic-02.png).
- **Ultimate Nullifier**; type/group: Hero / Mr. Fantastic; copies: Unverified; Hero Name: Mr. Fantastic; team: Fantastic Four; class icons: Tech; printed values: Cost 7; Attack 4+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mr-fantastic-01.png).

### Hero group: Silver Surfer

- **Warp Speed**; type/group: Hero / Silver Surfer; copies: Unverified; Hero Name: Silver Surfer; team: Unaffiliated; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silver-surfer-03.png).
- **Epic Destiny**; type/group: Hero / Silver Surfer; copies: Unverified; Hero Name: Silver Surfer; team: Unaffiliated; class icons: Strength; printed values: Cost 4; Recruit 2; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silver-surfer-04.png).
- **The Power Cosmic**; type/group: Hero / Silver Surfer; copies: Unverified; Hero Name: Silver Surfer; team: Unaffiliated; class icons: Ranged; printed values: Cost 6; Recruit 3; Attack 0+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silver-surfer-02.png).
- **Energy Surge**; type/group: Hero / Silver Surfer; copies: Unverified; Hero Name: Silver Surfer; team: Unaffiliated; class icons: Ranged; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silver-surfer-01.png).

### Hero group: Thing

- **It Started on Yancy Street**; type/group: Hero / Thing; copies: Unverified; Hero Name: Thing; team: Fantastic Four; class icons: Instinct; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thing-04.png).
- **Knuckle Sandwich**; type/group: Hero / Thing; copies: Unverified; Hero Name: Thing; team: Fantastic Four; class icons: Strength; printed values: Cost 5; Recruit 3; Attack 0+; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thing-03.png).
- **Crime Stopper**; type/group: Hero / Thing; copies: Unverified; Hero Name: Thing; team: Fantastic Four; class icons: Strength; printed values: Cost 6; Attack 4; keyword labels: Focus; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thing-02.png).
- **It's Clobberin' Time!**; type/group: Hero / Thing; copies: Unverified; Hero Name: Thing; team: Fantastic Four; class icons: Strength; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thing-01.png).

### Villain Group: Heralds of Galactus

- **Firelord**; type/group: Villain / Heralds of Galactus; copies: 2; printed values: Attack 9*; VP 4; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heralds-of-galactus-02.png).
- **Morg**; type/group: Villain / Heralds of Galactus; copies: 2; printed values: Attack 12*; VP 6; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heralds-of-galactus-01.png).
- **Stardust**; type/group: Villain / Heralds of Galactus; copies: 2; printed values: Attack 10*; VP 5; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heralds-of-galactus-04.png).
- **Terrax the Tamer**; type/group: Villain / Heralds of Galactus; copies: 2; printed values: Attack 11*; VP 5; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/heralds-of-galactus-03.png).

### Villain Group: Subterranea

- **Giganto**; type/group: Villain / Subterranea; copies: 2; printed values: Attack 7; VP 4; keyword labels: Burrow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/subterranea-01.png).
- **Megataur**; type/group: Villain / Subterranea; copies: 2; printed values: Attack 6; VP 4; keyword labels: Burrow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/subterranea-03.png).
- **Moloids**; type/group: Villain / Subterranea; copies: 2; printed values: Attack 3; VP 2; keyword labels: Burrow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/subterranea-02.png).
- **Ra'ktar the Molan King**; type/group: Villain / Subterranea; copies: 2; printed values: Attack 4; VP 2; keyword labels: Burrow; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/subterranea-04.png).

### Mastermind: Galactus

- **Galactus**; type/group: Normal Mastermind face / Galactus; copies: Unverified; printed values: VP 7; keyword labels: Cosmic Threat; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/galactus-01.png).
- **Cosmic Entity**; type/group: Mastermind Tactic / Galactus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/galactus-02.png).
- **Force of Eternity**; type/group: Mastermind Tactic / Galactus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/galactus-03.png).
- **Panicked Mobs**; type/group: Mastermind Tactic / Galactus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/galactus-05.png).
- **Sunder the Earth**; type/group: Mastermind Tactic / Galactus; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/galactus-04.png).

### Mastermind: Mole Man

- **Mole Man**; type/group: Normal Mastermind face / Mole Man; copies: Unverified; printed values: Attack 7+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mole-man-01.png).
- **Dig to Freedom**; type/group: Mastermind Tactic / Mole Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mole-man-04.png).
- **Master of Monsters**; type/group: Mastermind Tactic / Mole Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mole-man-05.png).
- **Secret Tunnel**; type/group: Mastermind Tactic / Mole Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mole-man-03.png).
- **Underground Riches**; type/group: Mastermind Tactic / Mole Man; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mole-man-02.png).

### Scheme: Bathe the Earth in Cosmic Rays

- **Bathe the Earth in Cosmic Rays**; type/group: Scheme / Bathe the Earth in Cosmic Rays; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/17Scheme(48).png).

### Scheme: Flood the Planet with Melted Glaciers

- **Flood the Planet with Melted Glaciers**; type/group: Scheme / Flood the Planet with Melted Glaciers; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/18Scheme(49).png).

### Scheme: Invincible Force Field

- **Invincible Force Field**; type/group: Scheme / Invincible Force Field; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/19Scheme(50).png).

### Scheme: Pull Reality Into the Negative Zone

- **Pull Reality Into the Negative Zone**; type/group: Scheme / Pull Reality Into the Negative Zone; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/20Scheme(51).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
