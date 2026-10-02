# Marvel Studios' Guardians of the Galaxy (June 2022)

**Research status: Partial.** The official insert verifies contents and several mechanics, but not individual Scheme setups, Always Leads, or full Hero metadata.

## Sources
| Key | Source | Facts supported |
|---|---|---|
| C1 | [master-strike structured marvel-studios-guardians-of-the-galaxy card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/msgotg.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| MG | [Upper Deck Marvel Studios' Guardians of the Galaxy rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/Lgd_MCU_GOTG_Rulesheet.pdf) | Contents, Hero card distribution, Divided Cards, Artifacts, Villainous Weapons, Excessive Violence/Kindness, Command, and city changes (PDF pp.1–2). |
| C2 | [nutki Guardians of the Galaxy card catalog](https://github.com/nutki/legendary/tree/master/texttools/Marvel%20Studios%27%20Guardians%20of%20the%20Galaxy) | Card and group names/membership only; not behavior, component use, or setup values. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | June 2022 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (MG p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × (5 Villains + 3 Villainous Weapons) = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

Each Hero's 14 cards use a 1/2/2/3/3/3 copy distribution across one rare, two uncommons, and three commons (MG p.1).

### Group inventory (C2 + C1 face index)

- **Heroes (five):** Star-Lord; Gamora; Rocket & Groot; Drax; Mantis.
- **Villain Groups (two):** Followers of Ronan; Ravagers.
- **Masterminds (two):** Ronan the Accuser; Ego, The Living Planet.
- **Schemes (four):** Inescapable "Kyln" Space Prison; Provoke the Sovereign War Fleet; Star-Lord's Awesome Mix Tape; Unleash the Abilisk Space Monster.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, full Scheme/Mastermind setup lines, or the tracked Scheme hard case. The 2014 comic-art Guardians set is a separate product with different cards and gameplay (MG p.2).

## Rules and mechanisms

- **Divided Cards (MG p.1):** A physical card has two sides. In the HQ, hand, or deck it counts as one card with both sides' names, Hero Names, teams, and classes. The insert's example says a card with cost 3 on each side costs 3 to recruit, not 6. When played, choose one side and use only that side's abilities; text extraction loses the icon for a separate printed-value comparison.
- **Artifacts (MG p.1):** A Hero Artifact is gained to the discard pile, then may be played in front of its owner and remains there after turn end. It counts as a Hero for relevant effects but counts as played only on the turn it is played.
- **Triggered Artifacts (MG p.1):** All Artifacts in this set use a trigger. While controlled, each time the trigger occurs, resolve the listed effect. A copied Artifact can trigger once without remaining in play.
- **Villainous Weapons (MG pp.1–2):** Each Villain Group includes three distinct Weapons; they are not Villains and have Ambush effects when played from the Villain Deck and captured by a city Villain. They add their printed bonus to the captor. Escape transfers them to the Mastermind; defeating the captor gives the player the Weapons as Artifacts. As Artifacts, they have no cost, color, Hero Class, Hero/Villain identity, or enemy bonus.
- **Excessive Violence and Excessive Kindness (MG p.2):** Once per turn, spend one extra Attack to fight a Villain or Mastermind to activate all Excessive Violence abilities already played; spend one extra Recruit to recruit a Hero to activate Excessive Kindness. Resolve the opponent's effect and the triggered abilities in either order.
- **Command (MG p.2):** The leftmost city Villain from a Villain Group Commands that group and receives its Command abilities. It still Commands when it is the only Villain from that Group in the city.
- **Ego, the Living Planet (MG p.2):** Ego can change the number of city spaces without changing the HQ. The insert says not to combine Ego with a Scheme that also changes city-space count.

## Required parts and glossary

- Villainous Weapons are cards inside Villain Groups and may be attached to Villains, transferred to the Mastermind, or become player Artifacts. They do not form a separate stack (MG pp.1–2).
- Ego can alter the city-space count; the insert suggests marking removed or added spaces with the Mastermind or Master Strikes (MG p.2).
- **Divided Card:** One physical card with two selectable sides; it counts as one card before play and only the chosen side while played. (MG p.1)
- **Triggered Artifact:** A persistent Artifact that activates whenever its listed trigger occurs while controlled. (MG p.1)
- **Villainous Weapon:** A Villain-Group card that empowers an enemy, then can become a player's Artifact when captured. (MG pp.1–2)
- **Command:** A Villain leads its group while it is the leftmost Villain from that group in the city. (MG p.2)

Summaries are original paraphrases under 40 words. Verify Hero teams/classes/shared Hero Names and all card-linked component dependencies from product cards.

## Setup and implementation gaps

Verify all four Schemes' player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps, plus both Masterminds' Always Leads/setup effects and Hero metadata. The listed Scheme *Star-Lord's Awesome Mix Tape* is a tracked hard case in #34; its setup must be checked against the card rather than inferred from the insert. Integration must represent Triggered Artifacts, Weapon capture/transfer, and city-size changes. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Star-Lord

- **Starship Sensors**; type/group: Hero / Star-Lord; copies: 3; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 2; keyword labels: Triggered Artifact, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_star_lord_01.png).
- **Borrowed Nova Blaster**; type/group: Hero / Star-Lord; copies: 3; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 3; keyword labels: Triggered Artifact, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_star_lord_02.png).
- **Expandable Helmet**; type/group: Hero / Star-Lord; copies: 3; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 5; keyword labels: Triggered Artifact, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_star_lord_03.png).
- **Give**; type/group: Hero / Star-Lord; copies: 2; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 4; Recruit 2+; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_star_lord_04.png).
- **Take**; type/group: Hero / Star-Lord; copies: 2; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 4; Attack 2; keyword labels: Artifact; card image: unavailable in C1.
- **Don't Need that Stuff**; type/group: Hero / Star-Lord; copies: 2; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 6; Attack 3+; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_star_lord_05.png).
- **Hadron Enforcer**; type/group: Hero / Star-Lord; copies: 1; Hero Name: Star-Lord; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 8; keyword labels: Triggered Artifact, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_star_lord_06.png).

### Hero group: Gamora

- **Sharpen Blades**; type/group: Hero / Gamora; copies: 3; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 3; Recruit 2; keyword labels: Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_gamora_01.png).
- **Resourceful Fugitive**; type/group: Hero / Gamora; copies: 3; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_gamora_02.png).
- **Retractable Sword**; type/group: Hero / Gamora; copies: 3; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 5; keyword labels: Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_gamora_03.png).
- **Forgive**; type/group: Hero / Gamora; copies: 2; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 2; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_gamora_04.png).
- **Resent**; type/group: Hero / Gamora; copies: 2; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 2; Attack 1+; card image: unavailable in C1.
- **Stolen Necroblaster**; type/group: Hero / Gamora; copies: 2; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_gamora_05.png).
- **Guardians Escape**; type/group: Hero / Gamora; copies: 1; Hero Name: Gamora; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_gamora_06.png).

### Hero group: Rocket & Groot

- **Baby Groot**; type/group: Hero / Rocket & Groot; copies: 3; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Excessive Kindness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket_groot_02.png).
- **Passion**; type/group: Hero / Rocket & Groot; copies: 3; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 2; Attack 1; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket_groot_01.png).
- **Compassion**; type/group: Hero / Rocket & Groot; copies: 3; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 2; Recruit 1; keyword labels: Excessive Kindness; card image: unavailable in C1.
- **Don't Press this Button**; type/group: Hero / Rocket & Groot; copies: 3; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket_groot_03.png).
- **Press the Button**; type/group: Hero / Rocket & Groot; copies: 3; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 4; Recruit 1; card image: unavailable in C1.
- **Gravity Mines**; type/group: Hero / Rocket & Groot; copies: 2; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 4; keyword labels: Triggered Artifact, Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket_groot_04.png).
- **Tricky**; type/group: Hero / Rocket & Groot; copies: 2; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Tech; printed values: Cost 5; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket_groot_05.png).
- **Simple**; type/group: Hero / Rocket & Groot; copies: 2; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 5; Attack 3; card image: unavailable in C1.
- **We are Groot**; type/group: Hero / Rocket & Groot; copies: 1; Hero Name: Rocket & Groot; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/rocket_groot_06.png).

### Hero group: Drax

- **Nothing Goes over my Head**; type/group: Hero / Drax; copies: 3; Hero Name: Drax; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_drax_01.png).
- **Prison Riot**; type/group: Hero / Drax; copies: 3; Hero Name: Drax; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 5; Recruit 0+; Attack 3; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_drax_03.png).
- **I am Invisible**; type/group: Hero / Drax; copies: 3; Hero Name: Drax; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 4; Attack 2; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_drax_02.png).
- **Xandar is Invincible**; type/group: Hero / Drax; copies: 3; Hero Name: Drax; team: Unaffiliated; class icons: Tech; printed values: Cost 4; Recruit 2+; card image: unavailable in C1.
- **Remove his Spine**; type/group: Hero / Drax; copies: 2; Hero Name: Drax; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 6; Attack 3; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_drax_05.png).
- **Also Illegal**; type/group: Hero / Drax; copies: 2; Hero Name: Drax; team: Unaffiliated; class icons: Instinct; printed values: Cost 6; Recruit 3+; card image: unavailable in C1.
- **Dual Knives**; type/group: Hero / Drax; copies: 2; Hero Name: Drax; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 4; keyword labels: Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_drax_04.png).
- **Revenge for my Family**; type/group: Hero / Drax; copies: 1; Hero Name: Drax; team: Guardians of the Galaxy; class icons: Strength; printed values: Cost 8; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/msgg_drax_06.png).

### Hero group: Mantis

- **Empathic Bond**; type/group: Hero / Mantis; copies: 3; Hero Name: Mantis; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 3; Recruit 2; keyword labels: Excessive Kindness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mantis_01.png).
- **Selfless**; type/group: Hero / Mantis; copies: 3; Hero Name: Mantis; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mantis_02.png).
- **Selfish**; type/group: Hero / Mantis; copies: 3; Hero Name: Mantis; team: Unaffiliated; class icons: Ranged; printed values: Cost 4; Attack 2; card image: unavailable in C1.
- **Inspire Courage**; type/group: Hero / Mantis; copies: 3; Hero Name: Mantis; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 5; Recruit 3; Attack 0+; keyword labels: Excessive Kindness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mantis_03.png).
- **Sleep**; type/group: Hero / Mantis; copies: 2; Hero Name: Mantis; team: Guardians of the Galaxy; class icons: Covert; printed values: Cost 5; Recruit 2; keyword labels: Excessive Kindness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mantis_04.png).
- **Emotional Wave**; type/group: Hero / Mantis; copies: 2; Hero Name: Mantis; team: Guardians of the Galaxy; class icons: Ranged; printed values: Cost 6; Recruit 3; Attack 0+; keyword labels: Excessive Kindness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mantis_05.png).
- **Discover the Dead**; type/group: Hero / Mantis; copies: 1; Hero Name: Mantis; team: Guardians of the Galaxy; class icons: Instinct; printed values: Cost 7; Attack 2+; keyword labels: Excessive Kindness; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mantis_06.png).

### Villain Group: Followers of Ronan

- **Exolon Attendants**; type/group: Villain / Followers of Ronan; copies: 1; printed values: Attack 3+; VP 2; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_02.png).
- **Exolon Monks**; type/group: Villain / Followers of Ronan; copies: 1; printed values: Attack 3+; VP 2; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_03.png).
- **Sakaaran Mercenaries**; type/group: Villain / Followers of Ronan; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_07.png).
- **Korath the Pursuer**; type/group: Villain / Followers of Ronan; copies: 1; printed values: Attack 4+; VP 3; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_04.png).
- **Nebula**; type/group: Villain / Followers of Ronan; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Command, Villainous Weapons, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_06.png).
- **Korath's Disrupter Rifle**; type/group: Villain; subtype Villainous Weapon / Followers of Ronan; copies: 1; printed values: Attack +3; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_05.png).
- **“The Dark Aster“ Flagship**; type/group: Villain; subtype Villainous Weapon / Followers of Ronan; copies: 1; printed values: Attack +4; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_01.png).
- **The Orb**; type/group: Villain; subtype Villainous Weapon / Followers of Ronan; copies: 1; printed values: Attack +6; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/followers_of_ronan_08.png).

### Villain Group: Ravagers

- **Gef**; type/group: Villain / Ravagers; copies: 1; printed values: Attack 3+; VP 3; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_01.png).
- **Tullk**; type/group: Villain / Ravagers; copies: 1; printed values: Attack 3+; VP 3; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_06.png).
- **Kraglin Obfonteri**; type/group: Villain / Ravagers; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_02.png).
- **Taserface**; type/group: Villain / Ravagers; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_05.png).
- **Yondu Udonta**; type/group: Villain / Ravagers; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Command; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_08.png).
- **Scavanged Blade**; type/group: Villain; subtype Villainous Weapon / Ravagers; copies: 1; printed values: Attack +2; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_04.png).
- **Ravager Starship “Eclector“**; type/group: Villain; subtype Villainous Weapon / Ravagers; copies: 1; printed values: Attack +3; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_03.png).
- **Yaka Arrow**; type/group: Villain; subtype Villainous Weapon / Ravagers; copies: 1; printed values: Attack +4; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/ravagers_07.png).

### Mastermind: Ronan the Accuser

- **Ronan the Accuser**; type/group: Normal Mastermind face / Ronan the Accuser; copies: Unverified; printed values: not indexed in C1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/msgg_ronan_01.png).
- **Epic Ronan the Accuser**; type/group: Epic Mastermind face / Ronan the Accuser; copies: Unverified; printed values: Attack 7; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/msgg_ronan_02.png).
- **Ronan's Throne**; type/group: Villain / Ronan the Accuser; copies: Unverified; printed values: Attack +3; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/msgg_ronan_05.png).
- **Hood of the Accuser**; type/group: Villain / Ronan the Accuser; copies: Unverified; printed values: Attack +4; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/msgg_ronan_04.png).
- **Ancient Kree Armor**; type/group: Villain / Ronan the Accuser; copies: Unverified; printed values: Attack +5; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/msgg_ronan_03.png).
- **The Cosmi-Rod Warhammer**; type/group: Villain / Ronan the Accuser; copies: Unverified; printed values: Attack +6; VP -1; keyword labels: Villainous Weapons, Triggered Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/msgg_ronan_06.png).

### Mastermind: Ego, the Living Planet

- **Ego, the Living Planet**; type/group: Normal Mastermind face / Ego, the Living Planet; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ego_01.png).
- **Epic Ego, the Living Planet**; type/group: Epic Mastermind face / Ego, the Living Planet; copies: Unverified; printed values: Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ego_02.png).
- **I'm a Celestial, Sweetheart**; type/group: Mastermind Tactic / Ego, the Living Planet; copies: Unverified; printed values: Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ego_04.png).
- **Cover All That Exists**; type/group: Mastermind Tactic / Ego, the Living Planet; copies: Unverified; printed values: Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ego_03.png).
- **The Expansion is My Purpose**; type/group: Mastermind Tactic / Ego, the Living Planet; copies: Unverified; printed values: Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ego_05.png).
- **Until Everything is... Me!**; type/group: Mastermind Tactic / Ego, the Living Planet; copies: Unverified; printed values: Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ego_06.png).

### Scheme: Inescapable “Kyln“ Space Prison

- **Inescapable “Kyln“ Space Prison**; type/group: Scheme / Inescapable “Kyln“ Space Prison; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/msgg_scheme_01.png).

### Scheme: Provoke the Sovereign War Fleet

- **Provoke the Sovereign War Fleet**; type/group: Scheme / Provoke the Sovereign War Fleet; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/msgg_scheme_02.png).

### Scheme: Star-Lord's Awesome Mix Tape

- **Star-Lord's Awesome Mix Tape**; type/group: Scheme / Star-Lord's Awesome Mix Tape; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/msgg_scheme_03.png).

### Scheme: Unleash the Abilisk Space Monster

- **Unleash the Abilisk Space Monster**; type/group: Scheme / Unleash the Abilisk Space Monster; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/msgg_scheme_04.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
