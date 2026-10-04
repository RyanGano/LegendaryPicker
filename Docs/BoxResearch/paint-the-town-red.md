# Paint the Town Red (Mar 2014)

**Research status: Partial.** Runtime integration exists; this note indexes C1 per-face metadata without duplicating the existing catalog and its cited setup values in [`paint-the-town-red.json`](../../LegendaryPickerService/Data/Boxes/paint-the-town-red.json). Remaining research gaps are listed below.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured paint-the-town-red card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/pttr.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| Runtime | [`paint-the-town-red.json`](../../LegendaryPickerService/Data/Boxes/paint-the-town-red.json) | Existing runtime catalog and its cited setup/rules sources; not duplicated here. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release order, product status, and ruleset classification. |

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Black Cat

- **Casual Bank Robbery**; type/group: Hero / Black Cat; copies: Unverified; Hero Name: Black Cat; team: Spider Friends; class icons: Covert; printed values: Cost 4; Recruit 2+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-cat-04.png).
- **Pickpocket**; type/group: Hero / Black Cat; copies: Unverified; Hero Name: Black Cat; team: Spider Friends; class icons: Covert; printed values: Cost 1; Attack 0+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-cat-03.png).
- **Jinx**; type/group: Hero / Black Cat; copies: Unverified; Hero Name: Black Cat; team: Spider Friends; class icons: Instinct; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-cat-02.png).
- **Cat Burglar**; type/group: Hero / Black Cat; copies: Unverified; Hero Name: Black Cat; team: Spider Friends; class icons: Covert; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-cat-01.png).

### Hero group: Moon Knight

- **Climbing Claws**; type/group: Hero / Moon Knight; copies: Unverified; Hero Name: Moon Knight; team: Marvel Knights; class icons: Tech; printed values: Cost 3; Recruit 2+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moon-knight-04.png).
- **Lunar Communion**; type/group: Hero / Moon Knight; copies: Unverified; Hero Name: Moon Knight; team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moon-knight-03.png).
- **Crescent Moon Darts**; type/group: Hero / Moon Knight; copies: Unverified; Hero Name: Moon Knight; team: Marvel Knights; class icons: Tech; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moon-knight-02.png).
- **Golden Ankh of Khonshu**; type/group: Hero / Moon Knight; copies: Unverified; Hero Name: Moon Knight; team: Marvel Knights; class icons: Instinct; printed values: Cost 8; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/moon-knight-01.png).

### Hero group: Scarlet Spider

- **Flip Out**; type/group: Hero / Scarlet Spider; copies: Unverified; Hero Name: Scarlet Spider; team: Spider Friends; class icons: Strength; printed values: Cost 2; Recruit 1; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-spider-04.png).
- **Perfect Hunter**; type/group: Hero / Scarlet Spider; copies: Unverified; Hero Name: Scarlet Spider; team: Spider Friends; class icons: Instinct; printed values: Cost 4; Attack 1; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-spider-03.png).
- **Leap from Above**; type/group: Hero / Scarlet Spider; copies: Unverified; Hero Name: Scarlet Spider; team: Spider Friends; class icons: Covert; printed values: Cost 6; Attack 3+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-spider-02.png).
- **Sting of the Spider**; type/group: Hero / Scarlet Spider; copies: Unverified; Hero Name: Scarlet Spider; team: Spider Friends; class icons: Strength; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/scarlet-spider-01.png).

### Hero group: Spider-Woman

- **Bioelectric Shock**; type/group: Hero / Spider-Woman; copies: Unverified; Hero Name: Spider-Woman; team: Spider Friends; class icons: Ranged; printed values: Cost 4; Attack 2; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-woman-03.png).
- **Radioactive Spider**; type/group: Hero / Spider-Woman; copies: Unverified; Hero Name: Spider-Woman; team: Spider Friends; class icons: Strength; printed values: Cost 2; Recruit 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-woman-04.png).
- **Venom Blast**; type/group: Hero / Spider-Woman; copies: Unverified; Hero Name: Spider-Woman; team: Spider Friends; class icons: Ranged; printed values: Cost 6; Attack 3; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-woman-02.png).
- **Arachno-Pheromones**; type/group: Hero / Spider-Woman; copies: Unverified; Hero Name: Spider-Woman; team: Spider Friends; class icons: Covert; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-woman-01.png).

### Hero group: Symbiote Spider-Man

- **Dark Strength**; type/group: Hero / Symbiote Spider-Man; copies: Unverified; Hero Name: Symbiote Spider-Man; team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 1+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/symbiote-spider-man-04.png).
- **Spider-Sense Tingling**; type/group: Hero / Symbiote Spider-Man; copies: Unverified; Hero Name: Symbiote Spider-Man; team: Spider Friends; class icons: Instinct; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/symbiote-spider-man-03.png).
- **Shadowed Spider**; type/group: Hero / Symbiote Spider-Man; copies: Unverified; Hero Name: Symbiote Spider-Man; team: Spider Friends; class icons: Covert; printed values: Cost 2; Attack 1+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/symbiote-spider-man-02.png).
- **Thwip!**; type/group: Hero / Symbiote Spider-Man; copies: Unverified; Hero Name: Symbiote Spider-Man; team: Spider Friends; class icons: Ranged; printed values: Cost 2; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/symbiote-spider-man-01.png).

### Villain Group: Maximum Carnage

- **Carrion**; type/group: Villain / Maximum Carnage; copies: 2; printed values: Attack 4; VP 3; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/maximum-carnage-03.png).
- **Demogoblin**; type/group: Villain / Maximum Carnage; copies: 2; printed values: Attack 5; VP 3; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/maximum-carnage-02.png).
- **Doppelganger**; type/group: Villain / Maximum Carnage; copies: 2; printed values: Attack *; VP 3; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/maximum-carnage-04.png).
- **Shriek**; type/group: Villain / Maximum Carnage; copies: 2; printed values: Attack 6; VP 4; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/maximum-carnage-01.png).

### Villain Group: Sinister Six

- **Chameleon**; type/group: Villain / Sinister Six; copies: 1; printed values: Attack 6; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-04.png).
- **Hobgoblin**; type/group: Villain / Sinister Six; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-06.png).
- **Kraven the Hunter**; type/group: Villain / Sinister Six; copies: 1; printed values: Attack *; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-01.png).
- **Sandman**; type/group: Villain / Sinister Six; copies: 1; printed values: Attack *; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-05.png).
- **Shocker**; type/group: Villain / Sinister Six; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-02.png).
- **Vulture**; type/group: Villain / Sinister Six; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-03.png).

### Mastermind: Carnage

- **Carnage**; type/group: Normal Mastermind face / Carnage; copies: Unverified; printed values: VP 6; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/carnage-01.png).
- **Drooling Jaws**; type/group: Mastermind Tactic / Carnage; copies: Unverified; printed values: not indexed in C1; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/carnage-02.png).
- **Endless Hunger**; type/group: Mastermind Tactic / Carnage; copies: Unverified; printed values: not indexed in C1; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/carnage-03.png).
- **Feed Me**; type/group: Mastermind Tactic / Carnage; copies: Unverified; printed values: not indexed in C1; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/carnage-04.png).
- **Om Nom Nom**; type/group: Mastermind Tactic / Carnage; copies: Unverified; printed values: not indexed in C1; keyword labels: Feast; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/carnage-05.png).

### Mastermind: Mysterio

- **Mysterio**; type/group: Normal Mastermind face / Mysterio; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mysterio-01-1.png).
- **Blurring Images**; type/group: Mastermind Tactic / Mysterio; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mysterio-02-1.png).
- **Captive Audience**; type/group: Mastermind Tactic / Mysterio; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mysterio-03-1.png).
- **Master of Illusions**; type/group: Mastermind Tactic / Mysterio; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mysterio-04-1.png).
- **Mists of Deception**; type/group: Mastermind Tactic / Mysterio; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mysterio-05.png).

### Scheme: Clone Saga, The

- **Clone Saga, The**; type/group: Scheme / Clone Saga, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/31Scheme(54).png).

### Scheme: Invade the Daily Bugle News HQ

- **Invade the Daily Bugle News HQ**; type/group: Scheme / Invade the Daily Bugle News HQ; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/29Scheme(52).png).

### Scheme: Splice Humans with Spider DNA

- **Splice Humans with Spider DNA**; type/group: Scheme / Splice Humans with Spider DNA; copies: Unverified; printed values: not indexed in C1; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/32Scheme(55).png).

### Scheme: Weave a Web of Lies

- **Weave a Web of Lies**; type/group: Scheme / Weave a Web of Lies; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/30Scheme(53).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.

## Verified `uses` (#98)

Each card with a `uses` entry in this box's runtime file is listed below with the printed face whose text takes cards from that stack. Every entry was checked against the linked image and none contradicted the data. Source key `Card`.

### Checked against the printed face

- **Maximum Carnage** — Wounds: printed card text on **Shriek** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/maximum-carnage-01.png)).
- **Sinister Six** — Wounds: printed card text on **Sandman** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-05.png)); **Vulture** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sinister-six-03.png)).
- **Carnage** — Wounds: printed card text on **Carnage** ([card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/carnage-01.png)).
