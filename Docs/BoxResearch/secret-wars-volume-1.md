# Secret Wars Volume 1 (August 2015)

**Research status: Partial.** The official insert gives extensive rules and aggregate counts, but its listed categories total 348 rather than its stated 350 cards. For integration (#113) the Scheme Setup lines, each Always Leads and the parts cards use were read from the card faces C1 links; the runtime data is `LegendaryPickerService/Data/Boxes/secret-wars-volume-1.json`. Build an Army of Annihilation (#116) names 10 extra "Annihilation Wave" Henchmen, a group no box has; the owner decided any one Henchman Group not in the Villain Deck stands in, with its 10 cards in the KO pile (D-ko-henchmen). Its Setup line is 9 Twists (card face above), and Dark Alliance's second Mastermind joins at Twist 1.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| SW1 | [Upper Deck Secret Wars Volume 1 rules insert](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules_Secret_Wars_v1.pdf) | Contents (PDF p.2), keywords and card clarifications (PDF pp.1–2), and optional Ambition mode (PDF p.2). |
| C1 | [master-strike structured secret-wars-volume-1 card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/sw1.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | August 2015 date, expansion status, and First Edition classification. |

## Catalog inventory

### Official contents (SW1 p.2)

| Type | Official count |
|---|---:|
| Heroes | 14 groups × 14 cards = 196 |
| Villain Groups | 6 groups × 8 cards = 48 |
| Henchman Groups | 3 groups × 10 cards = 30 |
| Masterminds | 4 sets × 5 cards = 20 |
| Schemes | 8 |
| Ambition cards | 30 |
| Sidekick cards | 15 |
| Banker Bystander | 1 |
| Sum of listed counts | 348 |
| Insert's stated total | 350 |

The insert's detailed categories are two cards short of its stated total. No source reviewed explains the difference; the count is left unresolved rather than adjusted.

### Group inventory (C1)

- **Heroes (14):** Apocalyptic Kitty Pryde; Black Bolt; Black Panther; Captain Marvel; Dr. Strange; Lady Thor; Magik; Maximus; Namor, the Sub-Mariner; Old Man Logan; Proxima Midnight; Superior Iron Man; Thanos; Ultimate Spider-Man.
- **Villain Groups (six):** The Deadlands (C1: “Deadlands, The”); Domain of Apocalypse; Limbo; Manhattan (Earth-1610); Sentinel Territories; Wasteland.
- **Henchman Groups (three):** Ghost Racers; M.O.D.O.K.s; Thor Corps.
- **Masterminds (four):** Madelyne Pryor, Goblin Queen; Nimrod, Super Sentinel; Wasteland Hulk; Zombie Green Goblin.
- **Schemes (eight):** Build an Army of Annihilation; Corrupt the Next Generation of Heroes; Crush Them With My Bare Hands; Dark Alliance; Fragmented Realities; Master of Tyrants; Pan-Dimensional Plague; Smash Two Dimensions Together.
- **Other card types:** 30 Ambition cards, 15 Sidekick cards, and one Banker Bystander (SW1 p.2).

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Card fields absent from C1, each Master's Always Leads, and individual Scheme setup lines still need allowed-source verification.

## Rules and mechanisms

- **Multiclass cards (SW1 p.1):** A card can satisfy either of the two Hero classes shown on it, supporting Superpower abilities that require those classes.
- **Villains gained as Heroes (SW1 p.1):** The Ultimates and Thor Corps begin as Villains; when defeated, they join the victor as Heroes. If an effect asks for their Hero cost, use their former Villain Attack value.
- **Teleport (SW1 p.1):** A player may set aside a card with Teleport instead of playing it, then add it to the hand they draw at turn end.
- **Bribe (SW1 p.1):** A Villain with Bribe can be fought using any mix of Attack and Recruit points.
- **Rise of the Living Dead (SW1 p.1):** Each player checks the top Victory Pile card; a qualifying Villain returns to the city, but Mastermind Tactics do not. If a Villain carrying Bystanders enters a Victory Pile, its owner chooses their order.
- **Multiple Masterminds (SW1 p.1):** Some Villains that escape become Masterminds, and a Scheme can also add one. Players must defeat every Mastermind to win; each resolves its Master Strike, with the active player choosing their order.
- **Sidekick stack (SW1 p.2):** The product adds 15 Sidekick cards. A player may recruit at most one per turn; Sidekicks gained through card effects do not use that limit.
- **Cross-Dimensional Rampage clarifications (SW1 p.1):** A Hulk Rampage accepts a Hulk Hero or a Hulk card in a player's Victory Pile, including Maestro and Fear Itself's Nul; otherwise that player gains a Wound. Wolverine Rampage also counts Weapon X and Old Man Logan.
- **Optional mode:** “A Player is the Mastermind” uses the 30 Ambition cards. The roadmap excludes this optional mode from the default setup scope (#34, D4).

## Required parts and glossary

- The product adds a 15-card Sidekick stack and one Banker Bystander (SW1 p.2). It also includes 30 Ambition cards for the optional mode (SW1 p.2).
- **Teleport:** A card can wait outside the played cards until it joins its owner's end-of-turn hand. (SW1 p.1)
- **Bribe:** A fight may use Recruit points as well as Attack points. (SW1 p.1)
- **Rise of the Living Dead:** A qualifying Villain at the top of a Victory Pile can return to the city. (SW1 p.1)
- **Multiclass:** A Hero card with two class icons can meet a Superpower requirement for either printed class. (SW1 p.1)
- **Multiple Masterminds:** A setup may have more than one Mastermind; victory requires defeating them all. (SW1 p.1)

Summaries above are original paraphrases, each under 40 words. Other teams, classes, and card terms need card-level verification and their governing rule citations.

## Setup and implementation gaps

The insert does not provide a catalog of all eight Scheme setup lines, four Always Leads groups and setup effects, or the player-count effects on those cards. Its multiple-Mastermind rule identifies a new capability but not which individual Villains or Schemes trigger it. The detailed card roster also needs reconciliation with the 350-card total. No physical cards or clear scans were available to resolve those gaps; C1's structured face metadata is indexed below; its ability prose is not rules evidence.

The roadmap marks Secret Wars Volume 1 as needing G6 (richer group selection), G7 (additional Masterminds drawn by a Scheme), and G9 (Mastermind variants). Optional “A Player is the Mastermind” mode remains excluded. Integration is separate from this research record.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Apocalyptic Kitty Pryde

- **Phase Out**; type/group: Hero / Apocalyptic Kitty Pryde; copies: Unverified; Hero Name: Apocalyptic Kitty Pryde; team: X-Men; class icons: Covert; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/apocalyptic-kitty-pryde-04.png).
- **Infiltrate HQ**; type/group: Hero / Apocalyptic Kitty Pryde; copies: Unverified; Hero Name: Apocalyptic Kitty Pryde; team: X-Men; class icons: Tech; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/apocalyptic-kitty-pryde-03.png).
- **Disrupt Circuits**; type/group: Hero / Apocalyptic Kitty Pryde; copies: Unverified; Hero Name: Apocalyptic Kitty Pryde; team: X-Men; class icons: Covert, Tech; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/apocalyptic-kitty-pryde-02.png).
- **Untouchable**; type/group: Hero / Apocalyptic Kitty Pryde; copies: Unverified; Hero Name: Apocalyptic Kitty Pryde; team: X-Men; class icons: Covert; printed values: Cost 7; Recruit 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/apocalyptic-kitty-pryde-01.png).

### Hero group: Black Bolt

- **Destructive Whisper**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Illuminati; class icons: Ranged; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-bolt-04.png).
- **Speak No Words**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Illuminati; class icons: Covert, Ranged; printed values: Cost 4; Recruit 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-bolt-03.png).
- **Silence is Golden**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Illuminati; class icons: Strength; printed values: Cost 6; Recruit 0+; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-bolt-02.png).
- **Hypersonic Scream**; type/group: Hero / Black Bolt; copies: Unverified; Hero Name: Black Bolt; team: Illuminati; class icons: Ranged; printed values: Cost 8; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-bolt-01.png).

### Hero group: Black Panther

- **Catlike Reflexes**; type/group: Hero / Black Panther; copies: Unverified; Hero Name: Black Panther; team: Illuminati; class icons: Instinct, Covert; printed values: Cost 3; Recruit 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-03.png).
- **Multifaceted Genius**; type/group: Hero / Black Panther; copies: Unverified; Hero Name: Black Panther; team: Illuminati; class icons: Strength, Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-04.png).
- **Stalk the Urban Jungle**; type/group: Hero / Black Panther; copies: Unverified; Hero Name: Black Panther; team: Illuminati; class icons: Strength, Covert; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-02.png).
- **King of Wakanda**; type/group: Hero / Black Panther; copies: Unverified; Hero Name: Black Panther; team: Illuminati; class icons: Instinct, Tech; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-panther-01.png).

### Hero group: Captain Marvel

- **Absorb Energies**; type/group: Hero / Captain Marvel; copies: Unverified; Hero Name: Captain Marvel; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-marvel-03.png).
- **Supersonic Flight**; type/group: Hero / Captain Marvel; copies: Unverified; Hero Name: Captain Marvel; team: Avengers; class icons: Strength, Ranged; printed values: Cost 3; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-marvel-04.png).
- **Marvelous Strength**; type/group: Hero / Captain Marvel; copies: Unverified; Hero Name: Captain Marvel; team: Avengers; class icons: Strength; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-marvel-02.png).
- **Cosmic Energies**; type/group: Hero / Captain Marvel; copies: Unverified; Hero Name: Captain Marvel; team: Avengers; class icons: Ranged; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-marvel-01.png).

### Hero group: Dr. Strange

- **Cloak of Levitation**; type/group: Hero / Dr. Strange; copies: Unverified; Hero Name: Dr. Strange; team: Illuminati; class icons: Ranged; printed values: Cost 4; Attack 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-strange-03-1.png).
- **Trust Me, I'm a Doctor**; type/group: Hero / Dr. Strange; copies: Unverified; Hero Name: Dr. Strange; team: Illuminati; class icons: Instinct, Ranged; printed values: Cost 2; Recruit 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-strange-04-1.png).
- **Fight the Future**; type/group: Hero / Dr. Strange; copies: Unverified; Hero Name: Dr. Strange; team: Illuminati; class icons: Instinct; printed values: Cost 5; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-strange-02-1.png).
- **Sorcerer Supreme**; type/group: Hero / Dr. Strange; copies: Unverified; Hero Name: Dr. Strange; team: Illuminati; class icons: Covert; printed values: Cost 7; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-strange-01-1.png).

### Hero group: Lady Thor

- **Mysterious Origin**; type/group: Hero / Lady Thor; copies: Unverified; Hero Name: Lady Thor; team: Avengers; class icons: Ranged; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-thor-03.png).
- **Chosen by Asgard**; type/group: Hero / Lady Thor; copies: Unverified; Hero Name: Lady Thor; team: Avengers; class icons: Strength; printed values: Cost 4; Recruit 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-thor-04.png).
- **Heir to the Hammer**; type/group: Hero / Lady Thor; copies: Unverified; Hero Name: Lady Thor; team: Avengers; class icons: Strength, Ranged; printed values: Cost 6; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-thor-02.png).
- **Living Thunderstorm**; type/group: Hero / Lady Thor; copies: Unverified; Hero Name: Lady Thor; team: Avengers; class icons: Strength; printed values: Cost 8; Recruit 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/lady-thor-01.png).

### Hero group: Magik

- **Rally the New Mutants**; type/group: Hero / Magik; copies: Unverified; Hero Name: Magik; team: X-Men; class icons: Covert; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magik-04.png).
- **Travel through Limbo**; type/group: Hero / Magik; copies: Unverified; Hero Name: Magik; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 1+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magik-03.png).
- **Dimensional Portal**; type/group: Hero / Magik; copies: Unverified; Hero Name: Magik; team: X-Men; class icons: Covert, Ranged; printed values: Cost 5; Attack 2+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magik-02.png).
- **Wield the Soulsword**; type/group: Hero / Magik; copies: Unverified; Hero Name: Magik; team: X-Men; class icons: Covert; printed values: Cost 7; Attack 2+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/magik-01.png).

### Hero group: Maximus

- **Mental Domination**; type/group: Hero / Maximus; copies: Unverified; Hero Name: Maximus; team: Cabal; class icons: Covert; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/maximus-04.png).
- **Enslave the Will**; type/group: Hero / Maximus; copies: Unverified; Hero Name: Maximus; team: Cabal; class icons: Tech; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/maximus-03.png).
- **Pieces on a Chessboard**; type/group: Hero / Maximus; copies: Unverified; Hero Name: Maximus; team: Cabal; class icons: Covert, Tech; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/maximus-02.png).
- **Inhuman Mastery**; type/group: Hero / Maximus; copies: Unverified; Hero Name: Maximus; team: Cabal; class icons: Tech; printed values: Cost 7; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/maximus-01.png).

### Hero group: Namor, the Sub-Mariner

- **Lead the Armies of Atlantis**; type/group: Hero / Namor, the Sub-Mariner; copies: Unverified; Hero Name: Namor, the Sub-Mariner; team: Cabal; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namor-04.png).
- **Ruler of the Seas**; type/group: Hero / Namor, the Sub-Mariner; copies: Unverified; Hero Name: Namor, the Sub-Mariner; team: Cabal; class icons: Strength; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namor-03.png).
- **Feed the Sharks**; type/group: Hero / Namor, the Sub-Mariner; copies: Unverified; Hero Name: Namor, the Sub-Mariner; team: Cabal; class icons: Strength, Instinct; printed values: Cost 6; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namor-02.png).
- **Imperius Rex**; type/group: Hero / Namor, the Sub-Mariner; copies: Unverified; Hero Name: Namor, the Sub-Mariner; team: Cabal; class icons: Strength; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/namor-01.png).

### Hero group: Old Man Logan

- **Last Survivor**; type/group: Hero / Old Man Logan; copies: Unverified; Hero Name: Old Man Logan; team: X-Men; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/old-man-logan-03.png).
- **Loner**; type/group: Hero / Old Man Logan; copies: Unverified; Hero Name: Old Man Logan; team: X-Men; class icons: Instinct, Covert; printed values: Cost 5; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/old-man-logan-04.png).
- **Rage Out**; type/group: Hero / Old Man Logan; copies: Unverified; Hero Name: Old Man Logan; team: X-Men; class icons: Instinct; printed values: Cost 6; Attack 3+; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/old-man-logan-02.png).
- **No More Heroes**; type/group: Hero / Old Man Logan; copies: Unverified; Hero Name: Old Man Logan; team: X-Men; class icons: Instinct; printed values: Cost 7; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/old-man-logan-01.png).

### Hero group: Proxima Midnight

- **Inspiration Through Power**; type/group: Hero / Proxima Midnight; copies: Unverified; Hero Name: Proxima Midnight; team: Cabal; class icons: Instinct, Covert; printed values: Cost 2; Recruit 1; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/proxima-midnight-04.png).
- **Master Combatant**; type/group: Hero / Proxima Midnight; copies: Unverified; Hero Name: Proxima Midnight; team: Cabal; class icons: Instinct; printed values: Cost 4; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/proxima-midnight-03.png).
- **General of the Black Order**; type/group: Hero / Proxima Midnight; copies: Unverified; Hero Name: Proxima Midnight; team: Cabal; class icons: Covert; printed values: Cost 5; Recruit 0+; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/proxima-midnight-02.png).
- **Supernova Spear**; type/group: Hero / Proxima Midnight; copies: Unverified; Hero Name: Proxima Midnight; team: Cabal; class icons: Instinct; printed values: Cost 8; Recruit 4+; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/proxima-midnight-01.png).

### Hero group: Superior Iron Man

- **Armor Upgrades**; type/group: Hero / Superior Iron Man; copies: Unverified; Hero Name: Superior Iron Man; team: Illuminati; class icons: Tech; printed values: Cost 2; Attack 1+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/superior-iron-man-04.png).
- **Optimized Technology**; type/group: Hero / Superior Iron Man; copies: Unverified; Hero Name: Superior Iron Man; team: Illuminati; class icons: Tech, Ranged; printed values: Cost 5; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/superior-iron-man-03.png).
- **Superior to Others**; type/group: Hero / Superior Iron Man; copies: Unverified; Hero Name: Superior Iron Man; team: Illuminati; class icons: Ranged; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/superior-iron-man-02.png).
- **#Humblebrag**; type/group: Hero / Superior Iron Man; copies: Unverified; Hero Name: Superior Iron Man; team: Illuminati; class icons: Tech; printed values: Cost 8; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/superior-iron-man-01.png).

### Hero group: Thanos

- **Transdimensional Overlord**; type/group: Hero / Thanos; copies: Unverified; Hero Name: Thanos; team: Cabal; class icons: Strength; printed values: Cost 5; Attack 2+; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thanos-04-1.png).
- **Revel in Destruction**; type/group: Hero / Thanos; copies: Unverified; Hero Name: Thanos; team: Cabal; class icons: Strength, Ranged; printed values: Cost 3; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thanos-03-1.png).
- **Galactic Domination**; type/group: Hero / Thanos; copies: Unverified; Hero Name: Thanos; team: Cabal; class icons: Ranged; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thanos-02-1.png).
- **Utter Annihilation**; type/group: Hero / Thanos; copies: Unverified; Hero Name: Thanos; team: Cabal; class icons: Ranged; printed values: Cost 8; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/thanos-01-1.png).

### Hero group: Ultimate Spider-Man

- **Leaping Spider**; type/group: Hero / Ultimate Spider-Man; copies: Unverified; Hero Name: Ultimate Spider-Man; team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultimate-spider-man-04.png).
- **Marvel Team-Up**; type/group: Hero / Ultimate Spider-Man; copies: Unverified; Hero Name: Ultimate Spider-Man; team: Spider Friends; class icons: Strength, Instinct; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultimate-spider-man-03.png).
- **Web-Slinger**; type/group: Hero / Ultimate Spider-Man; copies: Unverified; Hero Name: Ultimate Spider-Man; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultimate-spider-man-02.png).
- **Hero from Another Dimension**; type/group: Hero / Ultimate Spider-Man; copies: Unverified; Hero Name: Ultimate Spider-Man; team: Spider Friends; class icons: Covert; printed values: Cost 2; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ultimate-spider-man-05.png).

### Villain Group: Deadlands, The

- **Zombie Baron Zemo**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 6; VP 4; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-06.png).
- **Zombie Loki**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 8; VP 6; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-04.png).
- **Zombie Madame Hydra**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 4; VP 2; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-03.png).
- **Zombie Mr. Sinister**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 7; VP 5; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-01.png).
- **Zombie M.O.D.O.K.**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 5; VP 3; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-02.png).
- **Zombie Mysterio**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 6; VP 6; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-07.png).
- **Zombie Thanos**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 9; VP 6; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-08.png).
- **Zombie Venom**; type/group: Villain / Deadlands, The; copies: 1; printed values: Attack 5*; VP 3; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/the-deadlands-05.png).

### Villain Group: Domain of Apocalypse

- **Apocalyptic Blink**; type/group: Villain / Domain of Apocalypse; copies: 3; printed values: Attack 5; VP 3; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/domain-of-apocalypse-04.png).
- **Apocalyptic Magneto**; type/group: Villain / Domain of Apocalypse; copies: 1; printed values: Attack 8; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/domain-of-apocalypse-02.png).
- **Apocalyptic Rogue**; type/group: Villain / Domain of Apocalypse; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/domain-of-apocalypse-01.png).
- **Apocalyptic Weapon X**; type/group: Villain / Domain of Apocalypse; copies: 2; printed values: Attack 7; VP 5; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/domain-of-apocalypse-03.png).

### Villain Group: Limbo

- **Inferno Colossus**; type/group: Villain / Limbo; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-01.png).
- **Inferno Cyclops**; type/group: Villain / Limbo; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-02.png).
- **Inferno Darkchilde**; type/group: Villain / Limbo; copies: 2; printed values: Attack 5; VP 3; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-04.png).
- **Inferno Nightcrawler**; type/group: Villain / Limbo; copies: 2; printed values: Attack 4; VP 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-03.png).

### Villain Group: Manhattan (Earth-1610)

- **Ultimate Captain America**; type/group: Trap / Manhattan (Earth-1610); copies: 2; printed values: Attack 0+; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/manhattan-earth-1610-04.png).
- **Ultimate Captain Marvel**; type/group: Trap / Manhattan (Earth-1610); copies: 2; printed values: Recruit 2; Attack 4; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/manhattan-earth-1610-01.png).
- **Ultimate Thor**; type/group: Trap / Manhattan (Earth-1610); copies: 2; printed values: Attack 3+; Attack 7; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/manhattan-earth-1610-03.png).
- **Ultimate Wasp**; type/group: Trap / Manhattan (Earth-1610); copies: 2; printed values: Attack 2+; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/manhattan-earth-1610-02.png).

### Villain Group: Sentinel Territories

- **Colossus of Future Past**; type/group: Villain / Sentinel Territories; copies: 2; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sentinel-territories-02.png).
- **Kate Pryde of Future Past**; type/group: Villain / Sentinel Territories; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sentinel-territories-01.png).
- **Rachel Summers of Future Past**; type/group: Villain / Sentinel Territories; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sentinel-territories-04.png).
- **Wolverine of Future Past**; type/group: Villain / Sentinel Territories; copies: 2; printed values: Attack 7; VP 5; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/sentinel-territories-03.png).

### Villain Group: Wasteland

- **The Hulk Gang**; type/group: Villain / Wasteland; copies: 3; printed values: Attack 5; VP 3; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wasteland-01.png).
- **Wasteland Kingpin**; type/group: Villain / Wasteland; copies: 1; printed values: Attack 11*; VP 6; keyword labels: Bribe; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wasteland-04.png).
- **Wasteland Hawkeye**; type/group: Villain / Wasteland; copies: 2; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wasteland-02.png).
- **Wasteland Spider-Girl**; type/group: Villain / Wasteland; copies: 2; printed values: Attack 4; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/wasteland-03.png).

### Henchman Group: Ghost Racers

- **Ghost Racers**; type/group: Henchman / Ghost Racers; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/ghost-racers.png).

### Henchman Group: M.O.D.O.K.s

- **M.O.D.O.K.s**; type/group: Henchman / M.O.D.O.K.s; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/modoks.png).

### Henchman Group: Thor Corps

- **Thor Corps**; type/group: Trap / Thor Corps; copies: Unverified; printed values: Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/thor-corps.png).

### Mastermind: Madelyne Pryor, Goblin Queen

- **Madelyne Pryor, Goblin Queen**; type/group: Normal Mastermind face / Madelyne Pryor, Goblin Queen; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/madelyne-pryor-01.png).
- **City of Demon Goblins**; type/group: Mastermind Tactic / Madelyne Pryor, Goblin Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/madelyne-pryor-02.png).
- **Corrupted Clone of Jean Grey**; type/group: Mastermind Tactic / Madelyne Pryor, Goblin Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/madelyne-pryor-03.png).
- **Everyone's a Demon on the Inside**; type/group: Mastermind Tactic / Madelyne Pryor, Goblin Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/madelyne-pryor-04.png).
- **Gather the Harvest**; type/group: Mastermind Tactic / Madelyne Pryor, Goblin Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/madelyne-pryor-05.png).

### Mastermind: Nimrod, Super Sentinel

- **Nimrod, Super Sentinel**; type/group: Normal Mastermind face / Nimrod, Super Sentinel; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nimrod-01.png).
- **Adapt and Destroy**; type/group: Mastermind Tactic / Nimrod, Super Sentinel; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nimrod-02.png).
- **Detect Mutation**; type/group: Mastermind Tactic / Nimrod, Super Sentinel; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nimrod-03.png).
- **Scatter the Mutants**; type/group: Mastermind Tactic / Nimrod, Super Sentinel; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nimrod-04.png).
- **Teleport and Incarcerate**; type/group: Mastermind Tactic / Nimrod, Super Sentinel; copies: Unverified; printed values: not indexed in C1; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/nimrod-05.png).

### Mastermind: Wasteland Hulk

- **Wasteland Hulk**; type/group: Normal Mastermind face / Wasteland Hulk; copies: Unverified; printed values: Attack 7+; VP 6; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/wasteland-hulk-01.png).
- **Brutal Beating**; type/group: Mastermind Tactic / Wasteland Hulk; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/wasteland-hulk-02.png).
- **Memories of Pain**; type/group: Mastermind Tactic / Wasteland Hulk; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/wasteland-hulk-03.png).
- **Radioactive Regeneration**; type/group: Mastermind Tactic / Wasteland Hulk; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/wasteland-hulk-04.png).
- **Revert to Bruce Banner**; type/group: Mastermind Tactic / Wasteland Hulk; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/wasteland-hulk-05.png).

### Mastermind: Zombie Green Goblin

- **Zombie Green Goblin**; type/group: Normal Mastermind face / Zombie Green Goblin; copies: Unverified; printed values: Attack 11+; VP 6; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/zombie-green-goblin-01.png).
- **Army of Cadavers**; type/group: Mastermind Tactic / Zombie Green Goblin; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/zombie-green-goblin-03.png).
- **The Hungry Dead**; type/group: Mastermind Tactic / Zombie Green Goblin; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/zombie-green-goblin-05.png).
- **Love to Have You for Dinner**; type/group: Mastermind Tactic / Zombie Green Goblin; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/zombie-green-goblin-02.png).
- **Reign of Terror**; type/group: Mastermind Tactic / Zombie Green Goblin; copies: Unverified; printed values: not indexed in C1; keyword labels: Rise of The Living Dead; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/zombie-green-goblin-04.png).

### Scheme: Build an Army of Annihilation

- **Build an Army of Annihilation**; type/group: Scheme / Build an Army of Annihilation; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/45Scheme(65).png).

### Scheme: Corrupt the Next Generation of Heroes

- **Corrupt the Next Generation of Heroes**; type/group: Scheme / Corrupt the Next Generation of Heroes; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/46Scheme(66).png).

### Scheme: Crush Them With My Bare Hands

- **Crush Them With My Bare Hands**; type/group: Scheme / Crush Them With My Bare Hands; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/47Scheme(67).png).

### Scheme: Dark Alliance

- **Dark Alliance**; type/group: Scheme / Dark Alliance; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/43Scheme(63).png).

### Scheme: Fragmented Realities

- **Fragmented Realities**; type/group: Scheme / Fragmented Realities; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/42Scheme(62).png).

### Scheme: Master of Tyrants

- **Master of Tyrants**; type/group: Scheme / Master of Tyrants; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/41Scheme(61).png).

### Scheme: Pan-Dimensional Plague

- **Pan-Dimensional Plague**; type/group: Scheme / Pan-Dimensional Plague; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/40Scheme(60).png).

### Scheme: Smash Two Dimensions Together

- **Smash Two Dimensions Together**; type/group: Scheme / Smash Two Dimensions Together; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/44Scheme(64).png).

### Bystander set: Banker

- **Banker**; type/group: Bystander / Banker; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-banker.png).

### Sidekick set: Sidekick

- **Sidekick**; type/group: Sidekick / Sidekick; copies: 15; printed values: Cost 2; card image: unavailable in C1.

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
