# Dark City (Jun 2013)

**Research status: Partial.** Runtime integration exists; this note indexes C1 per-face metadata without duplicating the existing catalog and its cited setup values in [`dark-city.json`](../../LegendaryPickerService/Data/Boxes/dark-city.json). Remaining research gaps are listed below.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured dark-city card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/darkcity.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| Runtime | [`dark-city.json`](../../LegendaryPickerService/Data/Boxes/dark-city.json) | Existing runtime catalog and its cited setup/rules sources; not duplicated here. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release order, product status, and ruleset classification. |

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Angel

- **Diving Catch**; type/group: Hero / Angel; copies: Unverified; Hero Name: Angel; team: X-Men; class icons: Strength; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-04.png).
- **High-Speed Chase**; type/group: Hero / Angel; copies: Unverified; Hero Name: Angel; team: X-Men; class icons: Covert; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-03.png).
- **Drop off a Friend**; type/group: Hero / Angel; copies: Unverified; Hero Name: Angel; team: X-Men; class icons: Instinct; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-02.png).
- **Strength of Spirit**; type/group: Hero / Angel; copies: Unverified; Hero Name: Angel; team: X-Men; class icons: Strength; printed values: Cost 7; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/angel-01.png).

### Hero group: Bishop

- **Absorb Energies**; type/group: Hero / Bishop; copies: Unverified; Hero Name: Bishop; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 0+; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bishop-04.png).
- **Whatever the Cost**; type/group: Hero / Bishop; copies: Unverified; Hero Name: Bishop; team: X-Men; class icons: Ranged; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bishop-03.png).
- **Concussive Blast**; type/group: Hero / Bishop; copies: Unverified; Hero Name: Bishop; team: X-Men; class icons: Ranged; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bishop-02.png).
- **Firepower from the Future**; type/group: Hero / Bishop; copies: Unverified; Hero Name: Bishop; team: X-Men; class icons: Tech; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bishop-01.png).

### Hero group: Blade

- **Night Hunter**; type/group: Hero / Blade; copies: Unverified; Hero Name: Blade; team: Marvel Knights; class icons: Strength; printed values: Cost 4; Recruit 0+; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/blade-03.png).
- **Stalk the Prey**; type/group: Hero / Blade; copies: Unverified; Hero Name: Blade; team: Marvel Knights; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/blade-04.png).
- **Nowhere to Hide**; type/group: Hero / Blade; copies: Unverified; Hero Name: Blade; team: Marvel Knights; class icons: Tech; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/blade-02.png).
- **Vampiric Surge**; type/group: Hero / Blade; copies: Unverified; Hero Name: Blade; team: Marvel Knights; class icons: Instinct; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/blade-01.png).

### Hero group: Cable

- **Disaster Survivalist**; type/group: Hero / Cable; copies: Unverified; Hero Name: Cable; team: X-Force; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cable-03.png).
- **Strike at the Heart of Evil**; type/group: Hero / Cable; copies: Unverified; Hero Name: Cable; team: X-Force; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cable-04.png).
- **Rapid Response Force**; type/group: Hero / Cable; copies: Unverified; Hero Name: Cable; team: X-Force; class icons: Covert; printed values: Cost 6; Attack 3+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cable-02.png).
- **Army of One**; type/group: Hero / Cable; copies: Unverified; Hero Name: Cable; team: X-Force; class icons: Ranged; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/cable-01.png).

### Hero group: Colossus

- **Draw Their Fire**; type/group: Hero / Colossus; copies: Unverified; Hero Name: Colossus; team: X-Force; class icons: Strength; printed values: Cost 1; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-04.png).
- **Invulnerability**; type/group: Hero / Colossus; copies: Unverified; Hero Name: Colossus; team: X-Force; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-03.png).
- **Silent Statue**; type/group: Hero / Colossus; copies: Unverified; Hero Name: Colossus; team: X-Force; class icons: Covert; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-02.png).
- **Russian Heavy Tank**; type/group: Hero / Colossus; copies: Unverified; Hero Name: Colossus; team: X-Force; class icons: Strength; printed values: Cost 8; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/colossus-01.png).

### Hero group: Daredevil

- **Backflip**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Marvel Knights; class icons: Strength; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-04.png).
- **Radar Sense**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Marvel Knights; class icons: Instinct; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-03.png).
- **Blind Justice**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Marvel Knights; class icons: Covert; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-02.png).
- **The Man Without Fear**; type/group: Hero / Daredevil; copies: Unverified; Hero Name: Daredevil; team: Marvel Knights; class icons: Instinct; printed values: Cost 8; Attack 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/daredevil-01.png).

### Hero group: Domino

- **Lucky Break**; type/group: Hero / Domino; copies: Unverified; Hero Name: Domino; team: X-Force; class icons: Tech; printed values: Cost 1; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/domino-04.png).
- **Ready for Anything**; type/group: Hero / Domino; copies: Unverified; Hero Name: Domino; team: X-Force; class icons: Instinct; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/domino-03.png).
- **Specialized Ammunition**; type/group: Hero / Domino; copies: Unverified; Hero Name: Domino; team: X-Force; class icons: Tech; printed values: Cost 5; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/domino-02.png).
- **Against All Odds**; type/group: Hero / Domino; copies: Unverified; Hero Name: Domino; team: X-Force; class icons: Covert; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/domino-01.png).

### Hero group: Elektra

- **First Strike**; type/group: Hero / Elektra; copies: Unverified; Hero Name: Elektra; team: Marvel Knights; class icons: Covert; printed values: Cost 1; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elektra-04.png).
- **Ninjitsu**; type/group: Hero / Elektra; copies: Unverified; Hero Name: Elektra; team: Marvel Knights; class icons: Instinct; printed values: Cost 2; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elektra-03.png).
- **Sai Blades**; type/group: Hero / Elektra; copies: Unverified; Hero Name: Elektra; team: Marvel Knights; class icons: Instinct; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elektra-02.png).
- **Silent Meditation**; type/group: Hero / Elektra; copies: Unverified; Hero Name: Elektra; team: Marvel Knights; class icons: Instinct; printed values: Cost 7; Recruit 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elektra-01.png).

### Hero group: Forge

- **Dirty Work**; type/group: Hero / Forge; copies: Unverified; Hero Name: Forge; team: X-Force; class icons: Tech; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/forge-03.png).
- **Reboot**; type/group: Hero / Forge; copies: Unverified; Hero Name: Forge; team: X-Force; class icons: Tech; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/forge-04.png).
- **Overdrive**; type/group: Hero / Forge; copies: Unverified; Hero Name: Forge; team: X-Force; class icons: Tech; printed values: Cost 5; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/forge-02.png).
- **B.F.G.**; type/group: Hero / Forge; copies: Unverified; Hero Name: Forge; team: X-Force; class icons: Tech; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/forge-01.png).

### Hero group: Ghost Rider

- **Blazing Hellfire**; type/group: Hero / Ghost Rider; copies: Unverified; Hero Name: Ghost Rider; team: Marvel Knights; class icons: Ranged; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ghost-rider-03.png).
- **Hell on Wheels**; type/group: Hero / Ghost Rider; copies: Unverified; Hero Name: Ghost Rider; team: Marvel Knights; class icons: Tech; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ghost-rider-04.png).
- **Infernal Chains**; type/group: Hero / Ghost Rider; copies: Unverified; Hero Name: Ghost Rider; team: Marvel Knights; class icons: Strength; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ghost-rider-02.png).
- **Penance Stare**; type/group: Hero / Ghost Rider; copies: Unverified; Hero Name: Ghost Rider; team: Marvel Knights; class icons: Ranged; printed values: Cost 8; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ghost-rider-01.png).

### Hero group: Iceman

- **Deep Freeze**; type/group: Hero / Iceman; copies: Unverified; Hero Name: Iceman; team: X-Men; class icons: Ranged; printed values: Cost 2; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iceman-03.png).
- **Ice Slide**; type/group: Hero / Iceman; copies: Unverified; Hero Name: Iceman; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iceman-04.png).
- **Frost Spike Armor**; type/group: Hero / Iceman; copies: Unverified; Hero Name: Iceman; team: X-Men; class icons: Strength; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iceman-02.png).
- **Impenetrable Ice Wall**; type/group: Hero / Iceman; copies: Unverified; Hero Name: Iceman; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iceman-01.png).

### Hero group: Iron Fist

- **Focus Chi**; type/group: Hero / Iron Fist; copies: Unverified; Hero Name: Iron Fist; team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-fist-04.png).
- **Wield the Iron Fist**; type/group: Hero / Iron Fist; copies: Unverified; Hero Name: Iron Fist; team: Marvel Knights; class icons: Strength; printed values: Cost 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-fist-03.png).
- **Ancient Legacy**; type/group: Hero / Iron Fist; copies: Unverified; Hero Name: Iron Fist; team: Marvel Knights; class icons: Strength; printed values: Cost 1; Recruit 0+; Attack 0+; keyword labels: Versatile; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-fist-02.png).
- **Living Weapon**; type/group: Hero / Iron Fist; copies: Unverified; Hero Name: Iron Fist; team: Marvel Knights; class icons: Strength; printed values: Cost 9; Attack 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/iron-fist-01.png).

### Hero group: Jean Grey

- **Psychic Search**; type/group: Hero / Jean Grey; copies: Unverified; Hero Name: Jean Grey; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jean-grey-03.png).
- **Read Your Thoughts**; type/group: Hero / Jean Grey; copies: Unverified; Hero Name: Jean Grey; team: X-Men; class icons: Covert; printed values: Cost 5; Recruit 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jean-grey-04.png).
- **Mind Over Matter**; type/group: Hero / Jean Grey; copies: Unverified; Hero Name: Jean Grey; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jean-grey-02.png).
- **Telekinetic Mastery**; type/group: Hero / Jean Grey; copies: Unverified; Hero Name: Jean Grey; team: X-Men; class icons: Ranged; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/jean-grey-01.png).

### Hero group: Nightcrawler

- **Bamf!**; type/group: Hero / Nightcrawler; copies: Unverified; Hero Name: Nightcrawler; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nightcrawler-04.png).
- **Blend Into Shadows**; type/group: Hero / Nightcrawler; copies: Unverified; Hero Name: Nightcrawler; team: X-Men; class icons: Covert; printed values: Cost 4; Attack 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nightcrawler-03.png).
- **Swashbuckler**; type/group: Hero / Nightcrawler; copies: Unverified; Hero Name: Nightcrawler; team: X-Men; class icons: Instinct; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nightcrawler-02.png).
- **Along for the Ride**; type/group: Hero / Nightcrawler; copies: Unverified; Hero Name: Nightcrawler; team: X-Men; class icons: Covert; printed values: Cost 7; Attack 5; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/nightcrawler-01.png).

### Hero group: Professor X

- **Psionic Astral Form**; type/group: Hero / Professor X; copies: Unverified; Hero Name: Professor X; team: X-Men; class icons: Ranged; printed values: Cost 2; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/professor-x-03.png).
- **Class Dismissed**; type/group: Hero / Professor X; copies: Unverified; Hero Name: Professor X; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/professor-x-04.png).
- **Telepathic Probe**; type/group: Hero / Professor X; copies: Unverified; Hero Name: Professor X; team: X-Men; class icons: Ranged; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/professor-x-02.png).
- **Mind Control**; type/group: Hero / Professor X; copies: Unverified; Hero Name: Professor X; team: X-Men; class icons: Covert; printed values: Cost 8; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/professor-x-01.png).

### Hero group: Punisher

- **Boom Goes the Dynamite**; type/group: Hero / Punisher; copies: Unverified; Hero Name: Punisher; team: Marvel Knights; class icons: Tech; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/punisher-04.png).
- **Hail of Bullets**; type/group: Hero / Punisher; copies: Unverified; Hero Name: Punisher; team: Marvel Knights; class icons: Tech; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/punisher-03.png).
- **Hostile Interrogation**; type/group: Hero / Punisher; copies: Unverified; Hero Name: Punisher; team: Marvel Knights; class icons: Strength; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/punisher-02.png).
- **The Punisher**; type/group: Hero / Punisher; copies: Unverified; Hero Name: Punisher; team: Marvel Knights; class icons: Tech; printed values: Cost 8; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/punisher-01.png).

### Hero group: Wolverine

- **Animal Instincts**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Force; class icons: Instinct; printed values: Cost 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x-force-wolverine-03.png).
- **Sudden Ambush**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Force; class icons: Covert; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x-force-wolverine-04.png).
- **No Mercy**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Force; class icons: Strength; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x-force-wolverine-02.png).
- **Reckless Abandon**; type/group: Hero / Wolverine; copies: Unverified; Hero Name: Wolverine; team: X-Force; class icons: Covert; printed values: Cost 7; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/x-force-wolverine-01.png).

### Villain Group: Emissaries of Evil

- **Egghead**; type/group: Villain / Emissaries of Evil; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/emissaries-of-evil-03.png).
- **Electro**; type/group: Villain / Emissaries of Evil; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/emissaries-of-evil-02.png).
- **Gladiator**; type/group: Villain / Emissaries of Evil; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/emissaries-of-evil-04.png).
- **Rhino**; type/group: Villain / Emissaries of Evil; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/emissaries-of-evil-01.png).

### Villain Group: Four Horsemen

- **Death**; type/group: Villain / Four Horsemen; copies: 2; printed values: Attack 7; VP 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/four-horsemen-04.png).
- **Famine**; type/group: Villain / Four Horsemen; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/four-horsemen-03-1.png).
- **Pestilence**; type/group: Villain / Four Horsemen; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/four-horsemen-02-1.png).
- **War**; type/group: Villain / Four Horsemen; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/four-horsemen-01-1.png).

### Villain Group: Marauders

- **Blockbuster**; type/group: Villain / Marauders; copies: 2; printed values: Attack 4+; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marauders-03.png).
- **Chimera**; type/group: Villain / Marauders; copies: 2; printed values: Attack 3+; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marauders-02.png).
- **Scalphunter**; type/group: Villain / Marauders; copies: 2; printed values: Attack 4+; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marauders-01.png).
- **Vertigo**; type/group: Villain / Marauders; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/marauders-04.png).

### Villain Group: Mutant Liberation Front

- **Forearm**; type/group: Villain / Mutant Liberation Front; copies: 2; printed values: Attack 4*; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mlf-03.png).
- **Reignfire**; type/group: Villain / Mutant Liberation Front; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mlf-04.png).
- **Wildside**; type/group: Villain / Mutant Liberation Front; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mlf-02.png).
- **Zero**; type/group: Villain / Mutant Liberation Front; copies: 2; printed values: Attack 0*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/mlf-01.png).

### Villain Group: Streets of New York

- **Bullseye**; type/group: Villain / Streets of New York; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/streets-of-new-york-01.png).
- **Hammerhead**; type/group: Villain / Streets of New York; copies: 2; printed values: Attack 5*; VP 2; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/streets-of-new-york-03.png).
- **Jigsaw**; type/group: Villain / Streets of New York; copies: 2; printed values: Attack 11*; VP 5; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/streets-of-new-york-02.png).
- **Tombstone**; type/group: Villain / Streets of New York; copies: 2; printed values: Attack 8*; VP 4; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/streets-of-new-york-04.png).

### Villain Group: Underworld

- **Azazel**; type/group: Villain / Underworld; copies: 2; printed values: Attack 4; VP 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/underworld-03.png).
- **Blackheart**; type/group: Villain / Underworld; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/underworld-01.png).
- **Dracula**; type/group: Villain / Underworld; copies: 2; printed values: Attack 3+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/underworld-02.png).
- **Lilith, Daughter of Dracula**; type/group: Villain / Underworld; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/underworld-04.png).

### Henchman Group: Maggia Goons

- **Maggia Goons**; type/group: Henchman / Maggia Goons; copies: Unverified; printed values: not indexed in C1; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/maggia-goons.png).

### Henchman Group: Phalanx

- **Phalanx**; type/group: Henchman / Phalanx; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/phalanx.png).

### Mastermind: Apocalypse

- **Apocalypse**; type/group: Normal Mastermind face / Apocalypse; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/apocalypse-01.png).
- **Apocalyptic Destruction**; type/group: Mastermind Tactic / Apocalypse; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/apocalypse-02.png).
- **The End of All Things**; type/group: Mastermind Tactic / Apocalypse; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/apocalypse-05.png).
- **Horsemen Are Drawing Nearer**; type/group: Mastermind Tactic / Apocalypse; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/apocalypse-03.png).
- **Immortal and Undefeated**; type/group: Mastermind Tactic / Apocalypse; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/apocalypse-04.png).

### Mastermind: Kingpin

- **Kingpin**; type/group: Normal Mastermind face / Kingpin; copies: Unverified; printed values: VP 6; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kingpin-01.png).
- **Call a Hit**; type/group: Mastermind Tactic / Kingpin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kingpin-02.png).
- **Criminal Empire**; type/group: Mastermind Tactic / Kingpin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kingpin-03.png).
- **Dirty Cops**; type/group: Mastermind Tactic / Kingpin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kingpin-04.png).
- **Mob War**; type/group: Mastermind Tactic / Kingpin; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/kingpin-05.png).

### Mastermind: Mephisto

- **Mephisto**; type/group: Normal Mastermind face / Mephisto; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mephisto-01.png).
- **Damned If You Do...**; type/group: Mastermind Tactic / Mephisto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mephisto-02.png).
- **Devilish Torment**; type/group: Mastermind Tactic / Mephisto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mephisto-03.png).
- **Pain Begets Pain**; type/group: Mastermind Tactic / Mephisto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mephisto-04.png).
- **The Price of Failure**; type/group: Mastermind Tactic / Mephisto; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mephisto-05.png).

### Mastermind: Mr. Sinister

- **Mr. Sinister**; type/group: Normal Mastermind face / Mr. Sinister; copies: Unverified; printed values: Attack 8+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mr-sinister-01.png).
- **Human Experimentation**; type/group: Mastermind Tactic / Mr. Sinister; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mr-sinister-02.png).
- **Master Geneticist**; type/group: Mastermind Tactic / Mr. Sinister; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mr-sinister-03.png).
- **Plans Within Plans**; type/group: Mastermind Tactic / Mr. Sinister; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mr-sinister-04.png).
- **Telepathic Manipulation**; type/group: Mastermind Tactic / Mr. Sinister; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/mr-sinister-05.png).

### Mastermind: Stryfe

- **Stryfe**; type/group: Normal Mastermind face / Stryfe; copies: Unverified; printed values: Attack 7+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/stryfe-01.png).
- **Furious Wrath**; type/group: Mastermind Tactic / Stryfe; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/stryfe-03.png).
- **Psychic Torment**; type/group: Mastermind Tactic / Stryfe; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/stryfe-02.png).
- **Swift Vengeance**; type/group: Mastermind Tactic / Stryfe; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/stryfe-05.png).
- **Tide of Retribution**; type/group: Mastermind Tactic / Stryfe; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/stryfe-04.png).

### Scheme: Capture Baby Hope

- **Capture Baby Hope**; type/group: Scheme / Capture Baby Hope; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/9Scheme(40).png).

### Scheme: Detonate the Helicarrier

- **Detonate the Helicarrier**; type/group: Scheme / Detonate the Helicarrier; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/13Scheme(44).png).

### Scheme: Massive Earthquake Generator

- **Massive Earthquake Generator**; type/group: Scheme / Massive Earthquake Generator; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/15Scheme(46).png).

### Scheme: Organized Crime Wave

- **Organized Crime Wave**; type/group: Scheme / Organized Crime Wave; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/16Scheme(47).png).

### Scheme: Save Humanity

- **Save Humanity**; type/group: Scheme / Save Humanity; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/14Scheme(45).png).

### Scheme: Steal the Weaponized Plutonium

- **Steal the Weaponized Plutonium**; type/group: Scheme / Steal the Weaponized Plutonium; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/12Scheme(43).png).

### Scheme: Transform Citizens Into Demons

- **Transform Citizens Into Demons**; type/group: Scheme / Transform Citizens Into Demons; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/11Scheme(42).png).

### Scheme: X-Cutioner's Song

- **X-Cutioner's Song**; type/group: Scheme / X-Cutioner's Song; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/10Scheme(41).png).

### Bystander set: News Reporter

- **News Reporter**; type/group: Bystander / News Reporter; copies: 4; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-news-reporter.png).

### Bystander set: Paramedic

- **Paramedic**; type/group: Bystander / Paramedic; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-paramedic.png).

### Bystander set: Radiation Scientist

- **Radiation Scientist**; type/group: Bystander / Radiation Scientist; copies: 4; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-radiation-scientist.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
