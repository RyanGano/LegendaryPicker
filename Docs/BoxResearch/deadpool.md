# Deadpool (October 2016)

**Research status: Partial.** The official rulesheet verifies the contents and two new mechanics, but full Scheme/Mastermind setup lines and card-level metadata remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| DP | [Upper Deck Deadpool rulesheet](https://upperdeck.com/wp-content/uploads/2024/05/Deadpool_RulesSheet.pdf) | Contents (PDF p.2), Excessive Violence and fractional values (PDF p.1), Revenge and Scheme clarification (PDF p.2). |
| C1 | [master-strike structured deadpool card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/deadpool.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | October 2016 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (DP p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official total. The insert lists no new Henchman Group or separate shared-stack component.

### Group inventory (C1)

- **Heroes (five):** Bob, Agent of HYDRA; Deadpool; Slapstick; Solo; Stingray.
- **Villain Groups (two):** Deadpool's Friends (C1 prints quotation marks around “Friends”); Evil Deadpool Corpse.
  - **Deadpool's Friends members:** Blind Al and Deuce; Sluggo; Taskmaster; Weasel.
  - **Evil Deadpool Corpse members:** D.E.A.D.P.O.O.L.; The Deadpool Kid; Ultimate Deadpool; Wolverinepool.
- **Masterminds (two):** Evil Deadpool; Macho Gomez.
- **Schemes (four):** Deadpool Kills the Marvel Universe; Deadpool Wants a Chimichanga; Deadpool Writes a Scheme; Everybody Hates Deadpool.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Missing card fields, Always Leads, and Scheme/Mastermind setup values remain unresolved unless supported by an allowed source.

## Rules and mechanisms

- **Fractional Attack and Recruit (DP p.1):** Add fractional values normally when playing cards; a combined total can reach a whole point for a fight or recruit.
- **Excessive Violence (DP p.1):** Once per turn, a player may spend one Attack beyond the amount needed to defeat an enemy. If so, resolve the Excessive Violence abilities on cards played that turn. No qualifying fight or no extra Attack means none apply; cards played afterward are too late.
- **Enemy Excessive Violence (DP p.2):** A Villain's or Mastermind's printed Excessive Violence effect also resolves when its fight is paid with one extra Attack.
- **Revenge (DP p.2):** A Villain with Revenge gains +1 Attack for each Villain from the named group in that player's Victory Pile. Masterminds are not Villains for this check.
- **Everybody Hates Deadpool clarification (DP p.2):** If the setup includes Hand Ninjas or Half-Eaten Burrito Warriors, their group-specific Revenge effects apply to their members. This clarification does not make either group mandatory.

The rulesheet gives no complete setup lines for the four Schemes or either Mastermind. Player limits, Twist counts, required groups/Heroes, moves, other stacks, setup steps, and Always Leads mappings remain unverified.

## Required parts and glossary

- The official contents list adds no new shared stack; Revenge uses Villains already in a player's Victory Pile (DP p.2).
- **Excessive Violence:** A fight paid with one extra Attack can activate all eligible abilities on cards played that turn, once per turn. (DP p.1)
- **Revenge:** A Villain gains Attack based on members of its named group already in its controller's Victory Pile. (DP p.2)

Summaries are original paraphrases under 40 words. Other card terms and abilities need verification from printed cards and their governing rules pages.

## Setup and implementation gaps

Verify every Scheme's player-count limits and setup effects, both Masterminds' Always Leads and setup, Hero metadata, and the complete printed card identities. The insert's use of group-specific Revenge does not establish a Scheme's required groups. This record does not change runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Bob, Agent of HYDRA

- **Bullets Flying, Bob Hiding**; type/group: Hero / Bob, Agent of HYDRA; copies: Unverified; Hero Name: Bob, Agent of HYDRA; team: HYDRA; class icons: Covert; printed values: Cost 3; Recruit 2½; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bob-04.png).
- **HYDRA Half-Wit**; type/group: Hero / Bob, Agent of HYDRA; copies: Unverified; Hero Name: Bob, Agent of HYDRA; team: HYDRA; class icons: Tech; printed values: Cost 2; Attack 1½; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bob-03.png).
- **How Do I Get Out of Here??**; type/group: Hero / Bob, Agent of HYDRA; copies: Unverified; Hero Name: Bob, Agent of HYDRA; team: HYDRA; class icons: Covert; printed values: Cost 6; Attack 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bob-02.png).
- **Epic Middle Manager**; type/group: Hero / Bob, Agent of HYDRA; copies: Unverified; Hero Name: Bob, Agent of HYDRA; team: HYDRA; class icons: Covert; printed values: Cost 8; Attack 5; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/bob-01.png).

### Hero group: Deadpool

- **Nighttime Is the Right Time**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Mercs for Money; class icons: Tech; printed values: Cost 3; Recruit 0+; Attack 0+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-03-1.png).
- **It'll Grow Back**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Mercs for Money; class icons: Instinct; printed values: Cost 4; Attack 2½+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-04-1.png).
- **Running Commentary**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Mercs for Money; class icons: Covert; printed values: Cost 5; Attack 3½+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-02-1.png).
- **Deadpool Rage!**; type/group: Hero / Deadpool; copies: Unverified; Hero Name: Deadpool; team: Mercs for Money; class icons: Strength; printed values: Cost 7; Attack 5; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/deadpool-01-1.png).

### Hero group: Slapstick

- **Napoleon Complex**; type/group: Hero / Slapstick; copies: Unverified; Hero Name: Slapstick; team: Mercs for Money; class icons: Ranged; printed values: Cost 4; Recruit 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/slapstick-04.png).
- **Saturday Morning Harpoons**; type/group: Hero / Slapstick; copies: Unverified; Hero Name: Slapstick; team: Mercs for Money; class icons: Ranged; printed values: Cost 3; Attack 2½; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/slapstick-03.png).
- **Surprise Chainsaw**; type/group: Hero / Slapstick; copies: Unverified; Hero Name: Slapstick; team: Mercs for Money; class icons: Strength; printed values: Cost 6; Attack 4½; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/slapstick-02.png).
- **Electroplasmic Insanity**; type/group: Hero / Slapstick; copies: Unverified; Hero Name: Slapstick; team: Mercs for Money; class icons: Ranged; printed values: Cost 8; Attack 5; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/slapstick-01.png).

### Hero group: Solo

- **Half-Cocked**; type/group: Hero / Solo; copies: Unverified; Hero Name: Solo; team: Mercs for Money; class icons: Tech; printed values: Cost 2; Recruit 1½ ; Attack 1½; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/solo-03.png).
- **Merc's Gotta Get Paid**; type/group: Hero / Solo; copies: Unverified; Hero Name: Solo; team: Mercs for Money; class icons: Instinct; printed values: Cost 3; Recruit 0+; Attack 2½; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/solo-04.png).
- **Guns on My Guns**; type/group: Hero / Solo; copies: Unverified; Hero Name: Solo; team: Mercs for Money; class icons: Tech; printed values: Cost 5; Attack 3½; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/solo-02.png).
- **Cut in Half**; type/group: Hero / Solo; copies: Unverified; Hero Name: Solo; team: Mercs for Money; class icons: Instinct; printed values: Cost 7; Attack 2½; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/solo-01.png).

### Hero group: Stingray

- **Deck Chairs on the Titanic**; type/group: Hero / Stingray; copies: Unverified; Hero Name: Stingray; team: Mercs for Money; class icons: Tech; printed values: Cost 4; Attack 1½; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stingray-04.png).
- **Superpowered Swimsuit**; type/group: Hero / Stingray; copies: Unverified; Hero Name: Stingray; team: Mercs for Money; class icons: Tech; printed values: Cost 2; Attack ½+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stingray-03.png).
- **Sting of the Stingray's Sting**; type/group: Hero / Stingray; copies: Unverified; Hero Name: Stingray; team: Mercs for Money; class icons: Ranged; printed values: Cost 5; Recruit 3; keyword labels: Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stingray-02.png).
- **PhD in Oceanography**; type/group: Hero / Stingray; copies: Unverified; Hero Name: Stingray; team: Mercs for Money; class icons: Tech; printed values: Cost 8; Attack 4+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/stingray-01.png).

### Villain Group: Deadpool's “Friends“

- **Blind Al and Deuce**; type/group: Villain / Deadpool's “Friends“; copies: 2; printed values: Attack 2+; VP 3; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-friends-04.png).
- **Sluggo**; type/group: Villain / Deadpool's “Friends“; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Revenge, Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-friends-02.png).
- **Taskmaster**; type/group: Villain / Deadpool's “Friends“; copies: 2; printed values: Attack 3+; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-friends-01.png).
- **Weasel**; type/group: Villain / Deadpool's “Friends“; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/deadpools-friends-03.png).

### Villain Group: Evil Deadpool Corpse

- **D.E.A.D.P.O.O.L.**; type/group: Villain / Evil Deadpool Corpse; copies: 2; printed values: Attack 4+; VP 3; keyword labels: Revenge, Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/evil-deadpool-corpse-02.png).
- **The Deadpool Kid**; type/group: Villain / Evil Deadpool Corpse; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/evil-deadpool-corpse-01.png).
- **Ultimate Deadpool**; type/group: Villain / Evil Deadpool Corpse; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Revenge, Excessive Violence; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/evil-deadpool-corpse-04.png).
- **Wolverinepool**; type/group: Villain / Evil Deadpool Corpse; copies: 2; printed values: Attack 7+; VP 6; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/evil-deadpool-corpse-03.png).

### Mastermind: Evil Deadpool

- **Evil Deadpool**; type/group: Normal Mastermind face / Evil Deadpool; copies: Unverified; printed values: Attack 11+; VP 6; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/evil-deadpool-01.png).
- **Evil Even Oddball**; type/group: Mastermind Tactic / Evil Deadpool; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/evil-deadpool-03.png).
- **Hyper-Insane Healing Factor**; type/group: Mastermind Tactic / Evil Deadpool; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/evil-deadpool-02.png).
- **Of Course it's Corpse**; type/group: Mastermind Tactic / Evil Deadpool; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/evil-deadpool-04.png).
- **Stitched from Dead (Pool) Parts**; type/group: Mastermind Tactic / Evil Deadpool; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/evil-deadpool-05.png).

### Mastermind: Macho Gomez

- **Macho Gomez**; type/group: Normal Mastermind face / Macho Gomez; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/macho-gomez-01.png).
- **Bounty Payout**; type/group: Mastermind Tactic / Macho Gomez; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/macho-gomez-02.png).
- **Interstellar Assassin**; type/group: Mastermind Tactic / Macho Gomez; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/macho-gomez-03.png).
- **Renegotiate the Contract**; type/group: Mastermind Tactic / Macho Gomez; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/macho-gomez-04.png).
- **Super Macho Man**; type/group: Mastermind Tactic / Macho Gomez; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/macho-gomez-05.png).

### Scheme: Deadpool Kills the Marvel Universe

- **Deadpool Kills the Marvel Universe**; type/group: Scheme / Deadpool Kills the Marvel Universe; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/60Scheme(13).png).

### Scheme: Deadpool Wants a Chimichanga

- **Deadpool Wants a Chimichanga**; type/group: Scheme / Deadpool Wants a Chimichanga; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/61Scheme(79).png).

### Scheme: Deadpool Writes a Scheme

- **Deadpool Writes a Scheme**; type/group: Scheme / Deadpool Writes a Scheme; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/62Scheme(80).png).

### Scheme: Everybody Hates Deadpool

- **Everybody Hates Deadpool**; type/group: Scheme / Everybody Hates Deadpool; copies: Unverified; printed values: not indexed in C1; keyword labels: Revenge; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/63Scheme(81).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
