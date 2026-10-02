# Secret Wars Volume 2 (December 2015)

**Research status: Partial.** Official counts reconcile to 350 cards and the insert defines several mechanics, but no card-front scans were available in this pass to verify complete setups, Always Leads, Hero metadata, or the full card roster.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| SW2 | [Upper Deck Secret Wars Volume 2 rules insert](https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules_Secret_Wars_v2.pdf) | Contents (PDF p.2), keywords and clarifications (PDF pp.1–2), continuing multiple-Mastermind rules (PDF p.2). |
| C1 | [master-strike structured secret-wars-volume-2 card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/sw2.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | December 2015 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (SW2 p.2)

| Type | Official count |
|---|---:|
| Heroes | 16 groups × 14 cards = 224 |
| Villain Groups | 6 groups × 8 cards = 48 |
| Henchman Groups | 3 groups × 10 cards = 30 |
| Masterminds | 4 sets × 5 cards = 20 |
| Schemes | 8 |
| Ambition cards | 10 |
| Special Bystanders | 3 types, 10 cards total |
| Total cards | 350 |

The listed category counts sum to the insert's 350-card total.

### Group inventory (C1)

- **Heroes (16):** Agent Venom; Arkon the Magnificent; Beast; Black Swan; The Captain and the Devil; Captain Britain; Corvus Glaive; Dr. Punisher, Soldier Supreme; Elsa Bloodstone; Phoenix Force Cyclops; Ruby Summers; Shang-Chi; Silk; Soulsword Colossus; Spider-Gwen; Time-Traveling Jean Grey.
- **Villain Groups (six):** Deadpool's Secret Secret Wars; Guardians of Knowhere; K'un-Lun; Monster Metropolis; Utopolis; X-Men '92.
- **Henchman Groups (three):** Khonshu Guardians; Magma Men; Spider-Infected.
- **Masterminds (four):** Immortal Emperor Zheng-Zhu; King Hyperion; Shiklah, the Demon Bride; Spider-Queen.
- **Schemes (eight):** Deadlands Hordes Charge the Wall; Enthrone the Barons of Battleworld; The Fountain of Eternal Life; The God-Emperor of Battleworld; The Mark of Khonshu; Master the Mysteries of Kung-Fu; Secret Wars; Sinister Ambitions.
- **Special Bystander types (three):** Alligator Trapper; Shapeshifted Copycat; Undercover Agent.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. The printed card roster and any fields absent from C1, along with Always Leads and complete Scheme setups, still need verification.

## Rules and mechanisms

- **Spectrum (SW2 p.1):** A player can use Spectrum abilities with at least three distinct Hero classes among cards in hand and cards played that turn. The insert says Grey S.H.I.E.L.D. Heroes, HYDRA Allies, New Recruits, and Sidekicks have no classes; multiclass cards can help reach the threshold.
- **Patrol (SW2 p.1):** A Patrol ability checks whether its named city space or pile is empty when the ability is used. A space made nonexistent by a Scheme or Mastermind cannot be patrolled.
- **Wall-Crawl and Teleport (SW2 p.1):** Wall-Crawl may put a recruited Hero atop its owner's deck. Teleport can hold a card aside until it joins the owner's end-of-turn hand.
- **Circle of Kung-Fu / Quack-Fu (SW2 p.1):** A numbered Circle raises an enemy's Attack unless the player reveals a Hero costing at least that number. When multiple Circles apply, use only the highest.
- **Fateful Resurrection (SW2 p.1):** A Villain with this Fight effect checks the top Villain Deck card; a Twist or Strike sends it back to the city. Its Bystanders and other Fight effects still resolve. A returned Villain enters through the Sewers and resolves Ambush; a returned Tactic rejoins the face-down Tactics. An ascended Mastermind stays a Mastermind.
- **Charge (SW2 p.1):** After entering the Sewers, a charging Villain advances the stated extra spaces, pushing other Villains and potentially causing escapes.
- **Villains gained as Heroes (SW2 p.2):** Some X-Men '92 Villains join the victor as Heroes when defeated. For effects asking for their Hero cost, use their former Villain Attack value.
- **Cross-Dimensional Rampage (SW2 p.2):** Colossus Rampage accepts a Colossus Hero or a Colossus card in a player's Victory Pile; otherwise that player gains a Wound. Wolverine Rampage also counts Weapon X and Old Man Logan.
- **Multiple Masterminds (SW2 p.2):** Continue the Volume 1 rule: defeat every Mastermind to win and resolve each Master Strike in an order chosen by the active player. An ascended Mastermind keeps its other abilities, including Fateful Resurrection and Circle of Kung-Fu.
- **Special Bystanders (SW2 p.2):** When a Bystander becomes a Villain, defeating it still resolves its rescue effects; it remains a Bystander in the Victory Pile.
- **King Hyperion (SW2 p.2):** He pushes city Villains forward while present. If he escapes, normal HQ KO and carried-Bystander discard effects still apply, but effects referring to Villains do not affect him while he is a Mastermind.
- **Ambition cards:** The product adds 10 Ambition cards for the optional “A Player is the Mastermind” mode introduced in Volume 1; that mode is excluded from the default scope (#34, D4).

## Required parts and glossary

- The product adds 10 Special Bystanders across three types (SW2 p.2). The 10 Ambition cards belong to the optional mode, not a normal setup.
- **Spectrum:** Use this ability only with three or more different Hero classes among the player's hand and played cards. (SW2 p.1)
- **Patrol:** An ability tied to an empty named space or pile must be used while that place is empty. (SW2 p.1)
- **Circle of Kung-Fu:** A numbered Circle raises an enemy's Attack unless its player reveals a Hero of matching or higher cost; only the highest Circle applies. (SW2 p.1)
- **Fateful Resurrection:** A qualifying Villain checks the top of the Villain Deck and can return to the city when it reveals a Twist or Strike. (SW2 p.1)
- **Charge:** A Villain can move additional spaces forward after entering the city, pushing the line and risking escapes. (SW2 p.1)

Summaries are original paraphrases under 40 words. Card-level class/term usage and all remaining product terms need verification from printed cards and their governing rules pages.

## Setup and implementation gaps

The insert does not list any of the eight Scheme setup lines or four Masterminds' Always Leads and setup effects. Verify player limits, counts, required groups/Heroes, and any moves or set-aside parts from the physical cards. The catalog names and official aggregate counts do not establish the complete printed card manifest; no physical cards or clear scans were available in this pass.

Volume 2 continues the roadmap's multiple-Mastermind and Mastermind-variant capabilities (#34, G7/G9); optional Ambition mode remains excluded. This record does not change runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Agent Venom

- **Multi-Gun**; type/group: Hero / Agent Venom; copies: Unverified; Hero Name: Agent Venom; team: Spider Friends; class icons: Tech; printed values: Cost 2; Recruit 1+; Attack 1+; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-venom-04.png).
- **Government Payroll**; type/group: Hero / Agent Venom; copies: Unverified; Hero Name: Agent Venom; team: Spider Friends; class icons: Strength, Instinct; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Wall-Crawl, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-venom-03.png).
- **Big Slimeportunity**; type/group: Hero / Agent Venom; copies: Unverified; Hero Name: Agent Venom; team: Spider Friends; class icons: Instinct; printed values: Cost 6; Recruit 2; Attack 2; keyword labels: Wall-Crawl, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-venom-02.png).
- **Shapeshifting Symbiote**; type/group: Hero / Agent Venom; copies: Unverified; Hero Name: Agent Venom; team: Spider Friends; class icons: Strength; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/agent-venom-01.png).

### Hero group: Arkon the Magnificent

- **All-Terrain Barbarian**; type/group: Hero / Arkon the Magnificent; copies: Unverified; Hero Name: Arkon the Magnificent; team: Unaffiliated; class icons: Strength, Covert; printed values: Cost 3; Recruit 2+; keyword labels: Wall-Crawl, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/arkon-04.png).
- **Quiver of Thunderbolts**; type/group: Hero / Arkon the Magnificent; copies: Unverified; Hero Name: Arkon the Magnificent; team: Unaffiliated; class icons: Ranged; printed values: Cost 3; Attack 2; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/arkon-03.png).
- **Warlord of Open Spaces**; type/group: Hero / Arkon the Magnificent; copies: Unverified; Hero Name: Arkon the Magnificent; team: Unaffiliated; class icons: Instinct; printed values: Cost 5; Attack 3+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/arkon-02.png).
- **Lord of Dragons**; type/group: Hero / Arkon the Magnificent; copies: Unverified; Hero Name: Arkon the Magnificent; team: Unaffiliated; class icons: Instinct; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/arkon-01.png).

### Hero group: Beast

- **Balanced Attack**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: Illuminati; class icons: Strength, Tech; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-sw2-04.png).
- **Upside-down Thinking**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: Illuminati; class icons: Strength, Tech; printed values: Cost 4; keyword labels: Wall-Crawl, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-sw2-03.png).
- **Doctor of Beatdown**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: Illuminati; class icons: Strength, Tech; printed values: Cost 6; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-sw2-02.png).
- **Multi-Variable Smackulus**; type/group: Hero / Beast; copies: Unverified; Hero Name: Beast; team: Illuminati; class icons: Strength, Tech; printed values: Cost 8; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/beast-sw2-01.png).

### Hero group: Black Swan

- **Apocalyptic Vision**; type/group: Hero / Black Swan; copies: Unverified; Hero Name: Black Swan; team: Cabal; class icons: Ranged; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-swan-04.png).
- **Witness the End**; type/group: Hero / Black Swan; copies: Unverified; Hero Name: Black Swan; team: Cabal; class icons: Instinct; printed values: Cost 5; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-swan-03.png).
- **Dark Foretelling**; type/group: Hero / Black Swan; copies: Unverified; Hero Name: Black Swan; team: Cabal; class icons: Instinct, Ranged; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-swan-02.png).
- **Telepathic Control**; type/group: Hero / Black Swan; copies: Unverified; Hero Name: Black Swan; team: Cabal; class icons: Covert; printed values: Cost 7; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/black-swan-01.png).

### Hero group: Captain and the Devil, The

- **Jurassic America**; type/group: Hero / Captain and the Devil, The; copies: Unverified; Hero Name: Captain and the Devil, The; team: Avengers; class icons: Strength, Tech; printed values: Cost 2; Recruit 1+; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-and-the-devil-03.png).
- **Patriotic Chomp**; type/group: Hero / Captain and the Devil, The; copies: Unverified; Hero Name: Captain and the Devil, The; team: Avengers; class icons: Instinct; printed values: Cost 4; Attack 2+; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-and-the-devil-04.png).
- **Feeding Grounds**; type/group: Hero / Captain and the Devil, The; copies: Unverified; Hero Name: Captain and the Devil, The; team: Avengers; class icons: Covert; printed values: Cost 6; Attack 3; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-and-the-devil-02.png).
- **Dino-Roar of Triumph**; type/group: Hero / Captain and the Devil, The; copies: Unverified; Hero Name: Captain and the Devil, The; team: Avengers; class icons: Ranged; printed values: Cost 8; Attack 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-and-the-devil-01.png).

### Hero group: Captain Britain

- **Transatlantic Savior**; type/group: Hero / Captain Britain; copies: Unverified; Hero Name: Captain Britain; team: Illuminati; class icons: Covert; printed values: Cost 3; Recruit 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-britain-04.png).
- **Combined Strength**; type/group: Hero / Captain Britain; copies: Unverified; Hero Name: Captain Britain; team: Illuminati; class icons: Strength; printed values: Cost 4; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-britain-03.png).
- **Raise the Union Jack**; type/group: Hero / Captain Britain; copies: Unverified; Hero Name: Captain Britain; team: Illuminati; class icons: Strength, Covert; printed values: Cost 5; Attack 3; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-britain-02.png).
- **Lead the Captain Britain Corps**; type/group: Hero / Captain Britain; copies: Unverified; Hero Name: Captain Britain; team: Illuminati; class icons: Strength; printed values: Cost 7; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/captain-britain-01.png).

### Hero group: Corvus Glaive

- **Let None Escape You**; type/group: Hero / Corvus Glaive; copies: Unverified; Hero Name: Corvus Glaive; team: Cabal; class icons: Strength, Instinct; printed values: Cost 2; Recruit 0+; Attack 0+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/corvus-glaive-04.png).
- **Culling Blade**; type/group: Hero / Corvus Glaive; copies: Unverified; Hero Name: Corvus Glaive; team: Cabal; class icons: Instinct; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/corvus-glaive-03.png).
- **Rictus Grin**; type/group: Hero / Corvus Glaive; copies: Unverified; Hero Name: Corvus Glaive; team: Cabal; class icons: Strength; printed values: Cost 6; Attack 3+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/corvus-glaive-02.png).
- **Atom-Splitting Glaive**; type/group: Hero / Corvus Glaive; copies: Unverified; Hero Name: Corvus Glaive; team: Cabal; class icons: Tech; printed values: Cost 8; Attack 6+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/corvus-glaive-01.png).

### Hero group: Dr. Punisher, Soldier Supreme

- **Sweep the Streets of Trash**; type/group: Hero / Dr. Punisher, Soldier Supreme; copies: Unverified; Hero Name: Dr. Punisher, Soldier Supreme; team: Marvel Knights; class icons: Tech, Ranged; printed values: Cost 2; Recruit 1; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-punisher-soldier-supreme-04.png).
- **You're a Slow Learner**; type/group: Hero / Dr. Punisher, Soldier Supreme; copies: Unverified; Hero Name: Dr. Punisher, Soldier Supreme; team: Marvel Knights; class icons: Tech; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-punisher-soldier-supreme-03.png).
- **Ice Magic**; type/group: Hero / Dr. Punisher, Soldier Supreme; copies: Unverified; Hero Name: Dr. Punisher, Soldier Supreme; team: Marvel Knights; class icons: Ranged; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-punisher-soldier-supreme-02.png).
- **Calm Before the Storm**; type/group: Hero / Dr. Punisher, Soldier Supreme; copies: Unverified; Hero Name: Dr. Punisher, Soldier Supreme; team: Marvel Knights; class icons: Ranged; printed values: Cost 7; Attack 5+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/dr-punisher-soldier-supreme-01.png).

### Hero group: Elsa Bloodstone

- **Monster Hunter**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: S.H.I.E.L.D.; class icons: Covert, Tech; printed values: Cost 3; Attack 2; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elsa-bloodstone-04.png).
- **Bloodstone Pendant**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: S.H.I.E.L.D.; class icons: Instinct; printed values: Cost 5; Recruit 2+; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elsa-bloodstone-03.png).
- **Defend the S.H.I.E.L.D. Wall**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: S.H.I.E.L.D.; class icons: Ranged; printed values: Cost 6; Attack 0+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elsa-bloodstone-02.png).
- **Prodigy of Ulysses Bloodstone**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: S.H.I.E.L.D.; class icons: Strength; printed values: Cost 8; Attack 6+; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/elsa-bloodstone-01.png).

### Hero group: Phoenix Force Cyclops

- **Reincarnate**; type/group: Hero / Phoenix Force Cyclops; copies: Unverified; Hero Name: Phoenix Force Cyclops; team: X-Men; class icons: Covert; printed values: Cost 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-force-cyclops-04.png).
- **Burn Out**; type/group: Hero / Phoenix Force Cyclops; copies: Unverified; Hero Name: Phoenix Force Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-force-cyclops-03.png).
- **Rise from the Ashes**; type/group: Hero / Phoenix Force Cyclops; copies: Unverified; Hero Name: Phoenix Force Cyclops; team: X-Men; class icons: Instinct, Ranged; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-force-cyclops-02.png).
- **Destruction Is Creation**; type/group: Hero / Phoenix Force Cyclops; copies: Unverified; Hero Name: Phoenix Force Cyclops; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/phoenix-force-cyclops-01.png).

### Hero group: Ruby Summers

- **Guerrilla Warfare**; type/group: Hero / Ruby Summers; copies: Unverified; Hero Name: Ruby Summers; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2; keyword labels: Teleport; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ruby-summers-03.png).
- **Heir to Legends**; type/group: Hero / Ruby Summers; copies: Unverified; Hero Name: Ruby Summers; team: X-Men; class icons: Strength, Ranged; printed values: Cost 5; Recruit 2; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ruby-summers-04.png).
- **Form of Solid Ruby**; type/group: Hero / Ruby Summers; copies: Unverified; Hero Name: Ruby Summers; team: X-Men; class icons: Strength; printed values: Cost 6; Recruit 0+; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ruby-summers-02.png).
- **Extinction Blast**; type/group: Hero / Ruby Summers; copies: Unverified; Hero Name: Ruby Summers; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 10; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ruby-summers-01.png).

### Hero group: Shang-Chi

- **Shuffling Footwork**; type/group: Hero / Shang-Chi; copies: Unverified; Hero Name: Shang-Chi; team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shang-chi-03.png).
- **Acrobatic Kung-Fu**; type/group: Hero / Shang-Chi; copies: Unverified; Hero Name: Shang-Chi; team: Marvel Knights; class icons: Instinct, Covert; printed values: Cost 4; Attack 2+; keyword labels: Wall-Crawl, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shang-chi-04.png).
- **Seek the Empty Mind**; type/group: Hero / Shang-Chi; copies: Unverified; Hero Name: Shang-Chi; team: Marvel Knights; class icons: Covert; printed values: Cost 5; Attack 3+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shang-chi-02.png).
- **Muscle Memory**; type/group: Hero / Shang-Chi; copies: Unverified; Hero Name: Shang-Chi; team: Marvel Knights; class icons: Instinct; printed values: Cost 7; Attack 5; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/shang-chi-01.png).

### Hero group: Silk

- **Long-Range Spider-Sense**; type/group: Hero / Silk; copies: Unverified; Hero Name: Silk; team: Spider Friends; class icons: Ranged; printed values: Cost 2; Attack 2; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silk-04.png).
- **Cascading Maneuver**; type/group: Hero / Silk; copies: Unverified; Hero Name: Silk; team: Spider Friends; class icons: Strength, Instinct; printed values: Cost 2; Attack 1; keyword labels: Wall-Crawl, Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silk-03.png).
- **Silk Stalking**; type/group: Hero / Silk; copies: Unverified; Hero Name: Silk; team: Spider Friends; class icons: Covert; printed values: Cost 2; Attack 1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silk-02.png).
- **Borrowed Cloaking Device**; type/group: Hero / Silk; copies: Unverified; Hero Name: Silk; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 1; keyword labels: Wall-Crawl, Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/silk-01.png).

### Hero group: Soulsword Colossus

- **Invade the Inferno**; type/group: Hero / Soulsword Colossus; copies: Unverified; Hero Name: Soulsword Colossus; team: X-Men; class icons: Covert; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/soulsword-colossus-03.png).
- **Steel Interception**; type/group: Hero / Soulsword Colossus; copies: Unverified; Hero Name: Soulsword Colossus; team: X-Men; class icons: Strength, Covert; printed values: Cost 4; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/soulsword-colossus-04.png).
- **Possessed by the Soulsword**; type/group: Hero / Soulsword Colossus; copies: Unverified; Hero Name: Soulsword Colossus; team: X-Men; class icons: Strength; printed values: Cost 6; Attack 3+; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/soulsword-colossus-02.png).
- **Rescue My Family from Hell**; type/group: Hero / Soulsword Colossus; copies: Unverified; Hero Name: Soulsword Colossus; team: X-Men; class icons: Instinct; printed values: Cost 7; Attack 5; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/soulsword-colossus-01.png).

### Hero group: Spider-Gwen

- **Fateful Bridge**; type/group: Hero / Spider-Gwen; copies: Unverified; Hero Name: Spider-Gwen; team: Spider Friends; class icons: Instinct, Tech; printed values: Cost 2; Attack 2; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-gwen-03.png).
- **Save the Day**; type/group: Hero / Spider-Gwen; copies: Unverified; Hero Name: Spider-Gwen; team: Spider Friends; class icons: Tech; printed values: Cost 2; Attack 1+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-gwen-04.png).
- **First Adventure**; type/group: Hero / Spider-Gwen; copies: Unverified; Hero Name: Spider-Gwen; team: Spider Friends; class icons: Strength; printed values: Cost 2; Attack 1+; keyword labels: Wall-Crawl, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-gwen-02.png).
- **Intertwining Webs**; type/group: Hero / Spider-Gwen; copies: Unverified; Hero Name: Spider-Gwen; team: Spider Friends; class icons: Covert; printed values: Cost 2; Attack 0+; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/spider-gwen-01.png).

### Hero group: Time-Traveling Jean Grey

- **Throw Over the Railing**; type/group: Hero / Time-Traveling Jean Grey; copies: Unverified; Hero Name: Time-Traveling Jean Grey; team: X-Men; class icons: Covert; printed values: Cost 3; Attack 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/time-traveling-jean-grey-03.png).
- **Bridge to a Better Future**; type/group: Hero / Time-Traveling Jean Grey; copies: Unverified; Hero Name: Time-Traveling Jean Grey; team: X-Men; class icons: Instinct; printed values: Cost 4; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/time-traveling-jean-grey-04.png).
- **Telekinesis**; type/group: Hero / Time-Traveling Jean Grey; copies: Unverified; Hero Name: Time-Traveling Jean Grey; team: X-Men; class icons: Covert, Ranged; printed values: Cost 5; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/time-traveling-jean-grey-02.png).
- **Change History**; type/group: Hero / Time-Traveling Jean Grey; copies: Unverified; Hero Name: Time-Traveling Jean Grey; team: X-Men; class icons: Covert; printed values: Cost 7; Attack 5; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/time-traveling-jean-grey-01.png).

### Villain Group: Deadpool's Secret Secret Wars

- **Deadpool**; type/group: Villain / Deadpool's Secret Secret Wars; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Circle of Kung-Fu (and Quack-Fu), Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-secret-wars-02.png).
- **Doop**; type/group: Villain / Deadpool's Secret Secret Wars; copies: 2; printed values: Attack 2*; VP 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-secret-wars-04.png).
- **Howard the Duck**; type/group: Villain / Deadpool's Secret Secret Wars; copies: 2; printed values: Attack 1+; VP 4; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-secret-wars-01.png).
- **Pink Sphinx**; type/group: Villain / Deadpool's Secret Secret Wars; copies: 3; printed values: Attack 4; VP 2; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-secret-wars-03.png).

### Villain Group: Guardians of Knowhere

- **Angela**; type/group: Villain / Guardians of Knowhere; copies: 1; printed values: Attack 7; VP 5; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/guardians-of-knowhere-05.png).
- **Drax the Destroyer**; type/group: Villain / Guardians of Knowhere; copies: 2; printed values: Attack 5; VP 4; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/guardians-of-knowhere-02.png).
- **Gamora**; type/group: Villain / Guardians of Knowhere; copies: 1; printed values: Attack 6; VP 4; keyword labels: Charge, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/guardians-of-knowhere-03.png).
- **Groot**; type/group: Villain / Guardians of Knowhere; copies: 2; printed values: Attack 5; VP 4; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/guardians-of-knowhere-01.png).
- **Rocket Raccoon**; type/group: Villain / Guardians of Knowhere; copies: 2; printed values: Attack 4; VP 2; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/guardians-of-knowhere-04.png).

### Villain Group: K'un-Lun

- **Laughing Skull**; type/group: Villain / K'un-Lun; copies: 2; printed values: Attack 5+; VP 5; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kun-lun-03.png).
- **Rand K'ai**; type/group: Villain / K'un-Lun; copies: 2; printed values: Attack 6+; VP 6; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kun-lun-04.png).
- **Razor Fist**; type/group: Villain / K'un-Lun; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kun-lun-01.png).
- **Red Sai**; type/group: Villain / K'un-Lun; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/kun-lun-02.png).

### Villain Group: Monster Metropolis

- **Bug, Shiklah's Dragon**; type/group: Villain / Monster Metropolis; copies: 2; printed values: Attack 4; VP 3; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monster-metropolis-01.png).
- **Ghost Deadpool**; type/group: Villain / Monster Metropolis; copies: 2; printed values: Attack 5; VP 5; keyword labels: Fateful Resurrection, Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monster-metropolis-02.png).
- **Man-Thing**; type/group: Villain / Monster Metropolis; copies: 2; printed values: Attack 5; VP 4; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monster-metropolis-03.png).
- **Marcus Symbiote Centaur**; type/group: Villain / Monster Metropolis; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Charge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/monster-metropolis-04.png).

### Villain Group: Utopolis

- **Doctor Spectrum**; type/group: Villain / Utopolis; copies: 3; printed values: Attack 6; VP 4; keyword labels: Charge, Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/utopolis-02.png).
- **Nighthawk**; type/group: Villain / Utopolis; copies: 2; printed values: Attack 4+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/utopolis-04.png).
- **Warrior Woman**; type/group: Villain / Utopolis; copies: 1; printed values: Attack 8; VP 6; keyword labels: Charge, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/utopolis-03.png).
- **Whizzer**; type/group: Villain / Utopolis; copies: 2; printed values: Attack 5; VP 3; keyword labels: Charge, Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/utopolis-01.png).

### Villain Group: X-Men '92

- **'92 Beast**; type/group: Trap / X-Men '92; copies: 2; printed values: Attack 2; Attack 5; keyword labels: Charge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-92-01.png).
- **'92 Jubilee**; type/group: Trap / X-Men '92; copies: 3; printed values: Attack 2+; Attack 4; keyword labels: Spectrum; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-92-02.png).
- **'92 Professor X**; type/group: Villain / X-Men '92; copies: 1; printed values: Attack 8+; VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-92-03.png).
- **'92 Wolverine**; type/group: Trap / X-Men '92; copies: 2; printed values: Attack 2; Attack 7; keyword labels: Cross-Dimensional Rampage; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/x-men-92-04.png).

### Henchman Group: Khonshu Guardians

- **Khonshu Guardians**; type/group: Henchman / Khonshu Guardians; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/khonshu-guardians.png).

### Henchman Group: Magma Men

- **Magma Men**; type/group: Henchman / Magma Men; copies: Unverified; printed values: not indexed in C1; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/magma-men.png).

### Henchman Group: Spider-Infected

- **Spider-Infected**; type/group: Henchman / Spider-Infected; copies: Unverified; printed values: not indexed in C1; keyword labels: Wall-Crawl; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Henchmen/spider-infected.png).

### Mastermind: Immortal Emperor Zheng-Zhu

- **Immortal Emperor Zheng-Zhu**; type/group: Normal Mastermind face / Immortal Emperor Zheng-Zhu; copies: Unverified; printed values: VP 5; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor-zheng-zhu-01.png).
- **Ultimate Kung-Fu Mastery**; type/group: Mastermind Tactic / Immortal Emperor Zheng-Zhu; copies: Unverified; printed values: not indexed in C1; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor-zheng-zhu-02.png).
- **Emperor's Justice**; type/group: Mastermind Tactic / Immortal Emperor Zheng-Zhu; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor-zheng-zhu-03.png).
- **Humble the Pretenders**; type/group: Mastermind Tactic / Immortal Emperor Zheng-Zhu; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor-zheng-zhu-04.png).
- **Imperial Edict**; type/group: Mastermind Tactic / Immortal Emperor Zheng-Zhu; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emperor-zheng-zhu-05.png).

### Mastermind: King Hyperion

- **King Hyperion**; type/group: Normal Mastermind face / King Hyperion; copies: Unverified; printed values: VP 6; keyword labels: Charge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hyperion-01.png).
- **Worshipped by Millions**; type/group: Mastermind Tactic / King Hyperion; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hyperion-05.png).
- **Royal Treasury**; type/group: Mastermind Tactic / King Hyperion; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hyperion-02.png).
- **Monarch of Utopolis**; type/group: Mastermind Tactic / King Hyperion; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hyperion-04.png).
- **Rule with an Iron Fist**; type/group: Mastermind Tactic / King Hyperion; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/king-hyperion-03.png).

### Mastermind: Shiklah, the Demon Bride

- **Shiklah, the Demon Bride**; type/group: Normal Mastermind face / Shiklah, the Demon Bride; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shiklah-01.png).
- **Shiklah's Husband, Deadpool**; type/group: Trap / Shiklah, the Demon Bride; copies: Unverified; printed values: Attack 5+; VP -1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shiklah-05.png).
- **Enslavement Beam**; type/group: Mastermind Tactic / Shiklah, the Demon Bride; copies: Unverified; printed values: not indexed in C1; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shiklah-02.png).
- **Drain Life**; type/group: Mastermind Tactic / Shiklah, the Demon Bride; copies: Unverified; printed values: not indexed in C1; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shiklah-03.png).
- **Infernal Power**; type/group: Mastermind Tactic / Shiklah, the Demon Bride; copies: Unverified; printed values: not indexed in C1; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/shiklah-04.png).

### Mastermind: Spider-Queen

- **Spider-Queen**; type/group: Normal Mastermind face / Spider-Queen; copies: Unverified; printed values: VP 6; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/spider-queen-01.png).
- **Sonic Scream**; type/group: Mastermind Tactic / Spider-Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/spider-queen-04.png).
- **Infect the Entire City**; type/group: Mastermind Tactic / Spider-Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/spider-queen-03.png).
- **Control Arachnid Genes**; type/group: Mastermind Tactic / Spider-Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/spider-queen-02.png).
- **Web the Skyscrapers**; type/group: Mastermind Tactic / Spider-Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/spider-queen-05.png).

### Scheme: Deadlands Hordes Charge the Wall

- **Deadlands Hordes Charge the Wall**; type/group: Scheme / Deadlands Hordes Charge the Wall; copies: Unverified; printed values: not indexed in C1; keyword labels: Charge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/49Scheme(69).png).

### Scheme: Enthrone the Barons of Battleworld

- **Enthrone the Barons of Battleworld**; type/group: Scheme / Enthrone the Barons of Battleworld; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/50Scheme(70).png).

### Scheme: Fountain of Eternal Life, The

- **Fountain of Eternal Life, The**; type/group: Scheme / Fountain of Eternal Life, The; copies: Unverified; printed values: not indexed in C1; keyword labels: Fateful Resurrection; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/52Scheme(72).png).

### Scheme: God-Emperor of Battleworld, The

- **God-Emperor of Battleworld, The**; type/group: Scheme / God-Emperor of Battleworld, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/55Scheme(75).png).

### Scheme: Mark of Khonshu, The

- **Mark of Khonshu, The**; type/group: Scheme / Mark of Khonshu, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/48Scheme(68).png).

### Scheme: Master the Mysteries of Kung-Fu

- **Master the Mysteries of Kung-Fu**; type/group: Scheme / Master the Mysteries of Kung-Fu; copies: Unverified; printed values: not indexed in C1; keyword labels: Circle of Kung-Fu (and Quack-Fu); [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/53Scheme(73).png).

### Scheme: Secret Wars

- **Secret Wars**; type/group: Scheme / Secret Wars; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/54Scheme(74).png).

### Scheme: Sinister Ambitions

- **Sinister Ambitions**; type/group: Scheme / Sinister Ambitions; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/51Scheme(71).png).

### Bystander set: Alligator Trapper

- **Alligator Trapper**; type/group: Bystander / Alligator Trapper; copies: 3; printed values: not indexed in C1; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-alligator-trapper.png).

### Bystander set: Shapeshifted Copycat

- **Shapeshifted Copycat**; type/group: Bystander / Shapeshifted Copycat; copies: 4; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-shapeshifter-copycat.png).

### Bystander set: Undercover Agent

- **Undercover Agent**; type/group: Bystander / Undercover Agent; copies: 3; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Bystanders/bystander-undercover-agent.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
