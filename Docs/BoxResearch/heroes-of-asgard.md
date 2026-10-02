# Heroes of Asgard (March 2020)

**Research status: Partial.** The official insert verifies contents and several new card types/mechanics, but not card-level Scheme setups, Always Leads, or full Hero metadata.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| HA | [Upper Deck Heroes of Asgard rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2020_Marvel_Legendary_HeroesAsgard_Rules_compressed.pdf) | Contents (PDF p.2), Artifacts, Villainous Weapons, Worthy, and Conqueror rules (PDF pp.1–2). |
| C1 | [master-strike structured heroes-of-asgard card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/heroesofasgard.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | March 2020 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (HA p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards (6 Villains + 2 Villainous Weapons each) = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Beta Ray Bill; Lady Sif; Thor; Valkyrie; The Warriors Three.
- **Villain Groups (two):** Dark Council; Omens of Ragnarok.
- **Masterminds (two):** Hela, Goddess of Death; Malekith the Accursed.
- **Schemes (four):** Asgardian Test of Worth; The Dark World of Svartalfheim; War of the Frost Giants; Ragnarok, Twilight of the Gods.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index and all setup/rules claims still need an allowed source; unresolved areas include Hero metadata, Always Leads, complete Scheme/Mastermind setup lines, or exact card-linked component requirements.

## Rules and mechanisms

- **Artifacts (HA p.1):** A Hero Artifact is gained to the discard pile, then may be played from hand into the player's area. It remains there after the turn. It can be used on the turn played; its Superpower ability activates only on that turn. Once-per-turn abilities cannot be used on other players' turns.
- **Worthy (HA p.1):** A player is Worthy if they have a Hero costing 5 or more in hand, played this turn, or as a controlled Hero Artifact. Heroes in the player's deck or discard pile do not count.
- **Thrown Artifacts (HA p.1):** Put a Thrown Artifact on the bottom of the deck to use its listed effect. It may be thrown on the turn played or a later turn; multiple Artifacts may be thrown during a turn.
- **Villainous Weapons (HA pp.1–2):** Each Villain Group includes two Weapons, which are not Villains. A Weapon played from the Villain Deck is captured by the nearest City Villain or KO'd if the city is empty. It adds its printed bonus to its captor; multiple bonuses combine. If the captor escapes, the Mastermind takes its Weapons; if defeated, the player gains them as Artifacts in their discard pile.
- **Weapon artifacts (HA p.2):** A captured Weapon used as an Artifact has cost 0, no color or Hero Class, and is neither a Hero nor a Villain. Its Villain bonus does not apply to the player, but its printed Artifact ability does.
- **Conqueror (HA p.2):** A location-specific Conqueror ability gives its bonus while any Villain is in the named city space, whether or not the card with the ability is there.
- **Mastermind Tactics as Weapons (HA p.2):** Hela and Malekith each have a Tactic that becomes a Villainous Weapon. The insert says the Mastermind is defeated when no face-down Tactics remain; Weapons made from Tactics elsewhere do not prevent victory.

## Required parts and glossary

Villainous Weapons are included in their Villain Groups and may move under a Villain, to the Mastermind, or into a player's Artifact area. They are not a separate shared stack or token supply (HA pp.1–2).

- **Artifact:** A persistent card controlled in front of a player and usable again on later turns. (HA p.1)
- **Thrown Artifact:** An Artifact returned to the bottom of its owner's deck to use its listed effect. (HA p.1)
- **Villainous Weapon:** A Villain-Group card that empowers an enemy until captured as an Artifact. (HA pp.1–2)
- **Worthy:** Have a cost-5-or-higher Hero in hand, played this turn, or controlled as a Hero Artifact. (HA p.1)
- **Conqueror:** Gain a bonus while a Villain occupies the specified city space. (HA p.2)

Summaries are original paraphrases under 40 words. Exact Hero teams/classes and all card-linked component dependencies remain to be checked against product cards.

## Setup and implementation gaps

Verify the four Schemes' player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps, plus both Masterminds' Always Leads/setup effects and all Hero metadata. Integration must represent Artifact control and Thrown Artifacts, and track Villainous Weapons as attached cards that transfer or become player Artifacts. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Beta Ray Bill

- **Hope of the Korbinites**; type/group: Hero / Beta Ray Bill; copies: Unverified; Hero Name: Beta Ray Bill; team: Heroes of Asgard; class icons: Strength; printed values: Cost 1; Recruit 2; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beta-ray-bill-04-1.png).
- **Bio-Engineered Cyborg**; type/group: Hero / Beta Ray Bill; copies: Unverified; Hero Name: Beta Ray Bill; team: Heroes of Asgard; class icons: Tech; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beta-ray-bill-02-1.png).
- **Stormbreaker**; type/group: Hero / Beta Ray Bill; copies: Unverified; Hero Name: Beta Ray Bill; team: Heroes of Asgard; class icons: Ranged; printed values: Cost 4; keyword labels: Worthy, Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beta-ray-bill-03-1.png).
- **The Warship Skuttlebutt**; type/group: Hero / Beta Ray Bill; copies: Unverified; Hero Name: Beta Ray Bill; team: Heroes of Asgard; class icons: Tech; printed values: Cost 8; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beta-ray-bill-01-1.png).

### Hero group: Lady Sif

- **Dimensional Blade**; type/group: Hero / Lady Sif; copies: Unverified; Hero Name: Lady Sif; team: Heroes of Asgard; class icons: Instinct; printed values: Cost 2; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-sif-04-1.png).
- **Weapons Master**; type/group: Hero / Lady Sif; copies: Unverified; Hero Name: Lady Sif; team: Heroes of Asgard; class icons: Instinct; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-sif-02-1.png).
- **Winged Helm**; type/group: Hero / Lady Sif; copies: Unverified; Hero Name: Lady Sif; team: Heroes of Asgard; class icons: Strength; printed values: Cost 3; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-sif-03-1.png).
- **Golden Apples of Idunn**; type/group: Hero / Lady Sif; copies: Unverified; Hero Name: Lady Sif; team: Heroes of Asgard; class icons: Covert; printed values: Cost 7; keyword labels: Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-sif-01-1.png).

### Hero group: Thor

- **Test of Virtue**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Heroes of Asgard; class icons: Ranged; printed values: Cost 3; Recruit 2; Attack 0+; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-04-1%20%282%29.png).
- **Divine Lightning**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Heroes of Asgard; class icons: Ranged; printed values: Cost 5; Attack 3+; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-02-1%20%282%29.png).
- **Mjolnir**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Heroes of Asgard; class icons: Strength; printed values: Cost 4; keyword labels: Worthy, Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-03-1%20%282%29.png).
- **Royal Decree**; type/group: Hero / Thor; copies: Unverified; Hero Name: Thor; team: Heroes of Asgard; class icons: Ranged; printed values: Cost 8; Attack 5; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thor-01-1%20%282%29.png).

### Hero group: Valkyrie

- **Dragonfang**; type/group: Hero / Valkyrie; copies: Unverified; Hero Name: Valkyrie; team: Heroes of Asgard; class icons: Strength; printed values: Cost 3; keyword labels: Thrown Artifact, Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/valkyrie-04-1.png).
- **Flying Stallion**; type/group: Hero / Valkyrie; copies: Unverified; Hero Name: Valkyrie; team: Heroes of Asgard; class icons: Instinct; printed values: Cost 4; Attack 2+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/valkyrie-03-1.png).
- **Usher to Valhalla**; type/group: Hero / Valkyrie; copies: Unverified; Hero Name: Valkyrie; team: Heroes of Asgard; class icons: Covert; printed values: Cost 6; Attack 2+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/valkyrie-02-1.png).
- **Ride of the Valkyries**; type/group: Hero / Valkyrie; copies: Unverified; Hero Name: Valkyrie; team: Heroes of Asgard; class icons: Instinct; printed values: Cost 7; Attack 4+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/valkyrie-01-1.png).

### Hero group: Warriors Three, The

- **Fandral the Dashing**; type/group: Hero / Warriors Three, The; copies: Unverified; Hero Name: Warriors Three, The; team: Heroes of Asgard; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warriors-three-04-1.png).
- **Hogun the Grim**; type/group: Hero / Warriors Three, The; copies: Unverified; Hero Name: Warriors Three, The; team: Heroes of Asgard; class icons: Covert; printed values: Cost 4; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warriors-three-03-1.png).
- **Volstagg the Valiant**; type/group: Hero / Warriors Three, The; copies: Unverified; Hero Name: Warriors Three, The; team: Heroes of Asgard; class icons: Strength; printed values: Cost 6; Attack 3+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warriors-three-02-1.png).
- **Three Stand as One**; type/group: Hero / Warriors Three, The; copies: Unverified; Hero Name: Warriors Three, The; team: Heroes of Asgard; class icons: Strength; printed values: Cost 8; Attack 4+; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warriors-three-01-1.png).

### Villain Group: Dark Council

- **Laufey, Father Of Loki**; type/group: Villain / Dark Council; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-council-01.png).
- **The Mangog**; type/group: Villain / Dark Council; copies: 1; printed values: Attack 3+; VP 4; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-council-02.png).
- **Ulik, The Troll**; type/group: Villain / Dark Council; copies: 2; printed values: Attack 3+; VP 2; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-council-03.png).
- **Sindr, Fire Giant Queen**; type/group: Villain / Dark Council; copies: 2; printed values: Attack 5; VP 3; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-council-04.png).
- **Jarnbjorn, First Axe of Thor**; type/group: Villain; subtype Villainous Weapon / Dark Council; copies: 1; printed values: Attack +3; keyword labels: Villainous Weapons, Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-weapon-01.png).
- **The Casket of Ancient Winters**; type/group: Villain; subtype Villainous Weapon / Dark Council; copies: 1; printed values: Attack +4; keyword labels: Villainous Weapons, Artifact, Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/dark-weapon-02.png).

### Villain Group: Omens of Ragnarok

- **The Eternal Flame**; type/group: Villain; subtype Villainous Weapon / Omens of Ragnarok; copies: 1; printed values: Attack +4; keyword labels: Villainous Weapons, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/omens-weapon-01.png).
- **The Hel-Crown**; type/group: Villain; subtype Villainous Weapon / Omens of Ragnarok; copies: 1; printed values: Attack +3; keyword labels: Villainous Weapons, Artifact, Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/omens-weapon-02.png).
- **Skurge, The Executioner**; type/group: Villain / Omens of Ragnarok; copies: 2; printed values: Attack 4+; VP 2; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/omens-of-ragnarok-01.png).
- **Surtur, Fire Giant King**; type/group: Villain / Omens of Ragnarok; copies: 1; printed values: Attack 6; keyword labels: Artifact, Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/omens-of-ragnarok-02.png).
- **Jormungand, The World-Serpent**; type/group: Villain / Omens of Ragnarok; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/omens-of-ragnarok-03.png).
- **The Fenris Wolf**; type/group: Villain / Omens of Ragnarok; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/omens-of-ragnarok-04.png).

### Mastermind: Hela, Goddess of Death

- **Hela, Goddess of Death**; type/group: Normal Mastermind face / Hela, Goddess of Death; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hela-01.png).
- **Epic Hela, Goddess of Death**; type/group: Epic Mastermind face / Hela, Goddess of Death; copies: Unverified; printed values: Attack 12+; VP 6; keyword labels: Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hela-02.png).
- **Hela's Cloak**; type/group: Villain / Hela, Goddess of Death; copies: Unverified; printed values: Attack +2; VP -1; keyword labels: Villainous Weapons, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hela-06.png).
- **The Nightsword**; type/group: Villain / Hela, Goddess of Death; copies: Unverified; printed values: Attack +3; VP -1; keyword labels: Villainous Weapons, Thrown Artifact, Conqueror; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hela-05.png).
- **Seize Bifrost, The Rainbow Bridge**; type/group: Mastermind Tactic / Hela, Goddess of Death; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hela-04.png).
- **Naglfar, Longship of Fingernails**; type/group: Mastermind Tactic / Hela, Goddess of Death; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/hela-03.png).

### Mastermind: Malekith the Accursed

- **Malekith the Accursed**; type/group: Normal Mastermind face / Malekith the Accursed; copies: Unverified; printed values: VP 6; keyword labels: Villainous Weapons, Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/malekith-01.png).
- **Epic Malekith the Accursed**; type/group: Epic Mastermind face / Malekith the Accursed; copies: Unverified; printed values: Attack 10; VP 6; keyword labels: Villainous Weapons, Thrown Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/malekith-02.png).
- **Black Hammer of the Accursed**; type/group: Villain / Malekith the Accursed; copies: Unverified; printed values: Attack +4; VP -1; keyword labels: Villainous Weapons, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/malekith-03.png).
- **Dagger of Living Abyss**; type/group: Villain / Malekith the Accursed; copies: Unverified; printed values: Attack +2; VP -1; keyword labels: Villainous Weapons, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/malekith-04.png).
- **The Hunting Horn of Faerie**; type/group: Villain / Malekith the Accursed; copies: Unverified; printed values: Attack +3; VP -1; keyword labels: Villainous Weapons, Artifact; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/malekith-06.png).
- **Vulnerable to Cold Iron**; type/group: Mastermind Tactic / Malekith the Accursed; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/malekith-05.png).

### Scheme: Asgardian Test of Worth

- **Asgardian Test of Worth**; type/group: Scheme / Asgardian Test of Worth; copies: Unverified; printed values: not indexed in C1; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/116Scheme(25).png).

### Scheme: Dark World of Svartalfheim, The

- **Dark World of Svartalfheim, The**; type/group: Scheme / Dark World of Svartalfheim, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/118Scheme(111).png).

### Scheme: War of the Frost Giants

- **War of the Frost Giants**; type/group: Scheme / War of the Frost Giants; copies: Unverified; printed values: not indexed in C1; keyword labels: Worthy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/119Scheme(112).png).

### Scheme: Ragnarok, Twilight of the Gods

- **Ragnarok, Twilight of the Gods**; type/group: Scheme / Ragnarok, Twilight of the Gods; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/117Scheme(26).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
