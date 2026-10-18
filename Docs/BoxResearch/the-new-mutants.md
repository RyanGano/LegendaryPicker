# The New Mutants (April 2020)

**Research status: Partial.** The official insert verifies the 100-card breakdown and three mechanics. For #177 the Scheme Setup lines, Always Leads and part uses were read from OCR of every C1-linked face and are in `LegendaryPickerService/Data/Boxes/the-new-mutants.json`; per-face copy counts remain open.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| NM | [Upper Deck The New Mutants rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/2020_Marvel_Legendary_NewMutants_Rules_compressed.pdf) | Contents (PDF p.2), Moonlight/Sunlight and Waking Nightmare rules (PDF pp.1–2). |
| C1 | [master-strike structured the-new-mutants card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/newmutants.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | April 2020 date, expansion status, First Edition classification. |

## Catalog inventory

### Official contents (NM p.2)

| Type | Official count |
|---|---:|
| Heroes | 5 groups × 14 cards = 70 |
| Villain Groups | 2 groups × 8 cards = 16 |
| Double-Sided Epic Masterminds | 2 sets × 5 cards = 10 |
| Schemes | 4 |
| Total cards | 100 |

The listed categories sum to the official 100-card total.

### Group inventory (C1)

- **Heroes (five):** Karma; Mirage; Sunspot; Warlock; Wolfsbane.
- **Villain Groups (two):** Demons of Limbo; Hellions.
- **Masterminds (two):** Belasco, Demon Lord of Limbo; Emma Frost, The White Queen.
- **Schemes (four):** The Demon Bear Saga; Crash the Moon into the Sun; Trapped in the Insane Asylum; Superhuman Baseball Game.

The C1 face index below records available printed titles, group/type, numeric values, and team/class/keyword metadata, with direct card-image URLs where supplied. C1 ability prose is not rules evidence. Fields absent from the index still need an allowed source; Scheme Setup lines, Always Leads and part uses were read from the linked faces (#177), and per-face copy counts remain open.

## Rules and mechanisms

- **Moonlight and Sunlight (NM p.1):** Moonlight applies when most Heroes in the HQ have odd printed costs; Sunlight applies when most have even printed costs. A tie means neither applies. Cost modifiers do not count, and a Divided card counts once. Check the condition when each ability resolves; an earlier ability may change which condition applies to a later one on the same card.
- **Waking Nightmare (NM p.2):** Discard a non-grey Hero from hand; if a Hero is discarded this way, draw a card. Some card-specific versions add other benefits.
- **Conflicting effects (NM p.2):** If simultaneous effects direct a card to different destinations, the player chooses which effect to apply.
- **“Your cards” (NM p.2):** This includes cards in hand and cards played this turn, but not cards in the deck or discard pile.

## Required parts and glossary

The official contents list identifies no new shared stack, token, or card type beyond the Heroes, Villains, Masterminds, and Schemes (NM p.2).

- **Moonlight:** A condition based on most HQ Heroes having odd printed costs. (NM p.1)
- **Sunlight:** A condition based on most HQ Heroes having even printed costs. (NM p.1)
- **Waking Nightmare:** Discard a non-grey Hero from hand and draw a card if one was discarded. (NM p.2)

Summaries are original paraphrases under 40 words. From the card faces (#177): both Villain Groups and an Emma Frost Tactic give Wounds; no card uses another shared stack.

## Setup and implementation gaps

Per-face copy counts for Heroes, Masterminds and Tactics are not indexed by C1 and remain unverified. Moonlight and Sunlight are evaluated during play and have no setup effect.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Karma

- **Sow Rivalry**; type/group: Hero / Karma; copies: Unverified; Hero Name: Karma; team: X-Men; class icons: Covert; printed values: Cost 3; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karma-04.png).
- **Temporary Possession**; type/group: Hero / Karma; copies: Unverified; Hero Name: Karma; team: X-Men; class icons: Covert; printed values: Cost 4; Attack 2+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karma-03.png).
- **Karmic Balance**; type/group: Hero / Karma; copies: Unverified; Hero Name: Karma; team: X-Men; class icons: Ranged; printed values: Cost 6; Recruit 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karma-02.png).
- **Control Like a Puppet**; type/group: Hero / Karma; copies: Unverified; Hero Name: Karma; team: X-Men; class icons: Ranged; printed values: Cost 8; Attack 5+; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/karma-01.png).

### Hero group: Mirage

- **Dreams Made Real**; type/group: Hero / Mirage; copies: Unverified; Hero Name: Mirage; team: X-Men; class icons: Ranged; printed values: Cost 3; Attack 2; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mirage-03.png).
- **Empathic Link**; type/group: Hero / Mirage; copies: Unverified; Hero Name: Mirage; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mirage-04.png).
- **Nightmare Wolves**; type/group: Hero / Mirage; copies: Unverified; Hero Name: Mirage; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 1+; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mirage-02.png).
- **Haunted By the Demon Bear**; type/group: Hero / Mirage; copies: Unverified; Hero Name: Mirage; team: X-Men; class icons: Covert; printed values: Cost 7; Attack 4+; keyword labels: Moonlight and Sunlight, Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/mirage-01.png).

### Hero group: Sunspot

- **Absorb Radiation**; type/group: Hero / Sunspot; copies: Unverified; Hero Name: Sunspot; team: X-Men; class icons: Ranged; printed values: Cost 2; Recruit 1; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sunspot-04.png).
- **Solar-Powered**; type/group: Hero / Sunspot; copies: Unverified; Hero Name: Sunspot; team: X-Men; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sunspot-03.png).
- **Thermokinetic Fury**; type/group: Hero / Sunspot; copies: Unverified; Hero Name: Sunspot; team: X-Men; class icons: Ranged; printed values: Cost 6; Attack 4+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sunspot-02.png).
- **Empyreal Force**; type/group: Hero / Sunspot; copies: Unverified; Hero Name: Sunspot; team: X-Men; class icons: Strength; printed values: Cost 8; Attack 3+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/sunspot-01.png).

### Hero group: Warlock

- **Earthling Choices**; type/group: Hero / Warlock; copies: Unverified; Hero Name: Warlock; team: X-Men; class icons: Tech; printed values: Cost 2; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warlock-04.png).
- **Analyze Planetary Rotation**; type/group: Hero / Warlock; copies: Unverified; Hero Name: Warlock; team: X-Men; class icons: Tech; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warlock-03.png).
- **Techno-Organic Adaptation**; type/group: Hero / Warlock; copies: Unverified; Hero Name: Warlock; team: X-Men; class icons: Covert; printed values: Cost 6; Attack 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warlock-02.png).
- **Nanite Shapeshifter**; type/group: Hero / Warlock; copies: Unverified; Hero Name: Warlock; team: X-Men; class icons: Tech; printed values: Cost 7; Recruit 0+; Attack 0+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/warlock-01.png).

### Hero group: Wolfsbane

- **Night Vision**; type/group: Hero / Wolfsbane; copies: Unverified; Hero Name: Wolfsbane; team: X-Men; class icons: Strength; printed values: Cost 3; Attack 2; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolfsbane-03.png).
- **Wolf Out**; type/group: Hero / Wolfsbane; copies: Unverified; Hero Name: Wolfsbane; team: X-Men; class icons: Instinct; printed values: Cost 3; Recruit 2; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolfsbane-04.png).
- **Howl at the Moon**; type/group: Hero / Wolfsbane; copies: Unverified; Hero Name: Wolfsbane; team: X-Men; class icons: Covert; printed values: Cost 5; Attack 3; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolfsbane-02.png).
- **Nocturnal Savagery**; type/group: Hero / Wolfsbane; copies: Unverified; Hero Name: Wolfsbane; team: X-Men; class icons: Instinct; printed values: Cost 7; Attack 4+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/wolfsbane-01.png).

### Villain Group: Demons of Limbo

- **Crotus**; type/group: Villain / Demons of Limbo; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-01 (2).png).
- **N'astirh**; type/group: Villain / Demons of Limbo; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-02 (2).png).
- **S'ym**; type/group: Villain / Demons of Limbo; copies: 1; printed values: Attack 7; VP 5; keyword labels: Moonlight and Sunlight, Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-03 (2).png).
- **Demon Bear**; type/group: Villain / Demons of Limbo; copies: 1; printed values: Attack 5+; VP 5; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-04 (2).png).
- **Witchfire**; type/group: Villain / Demons of Limbo; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/limbo-05.png).

### Villain Group: Hellions

- **Catseye**; type/group: Villain / Hellions; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellions-01.png).
- **Thunderbird**; type/group: Villain / Hellions; copies: 2; printed values: Attack 4+; VP 4; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellions-02.png).
- **Tarot**; type/group: Villain / Hellions; copies: 1; printed values: Attack 5; VP 3; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellions-03.png).
- **Roulette**; type/group: Villain / Hellions; copies: 1; printed values: Attack 5; VP 3; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellions-04.png).
- **Empath**; type/group: Villain / Hellions; copies: 1; printed values: Attack 4+; VP 4; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellions-05.png).
- **Jetstream**; type/group: Villain / Hellions; copies: 1; printed values: Attack 6; VP 4; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/hellions-06.png).

### Mastermind: Belasco, Demon Lord of Limbo

- **Belasco, Demon Lord of Limbo**; type/group: Normal Mastermind face / Belasco, Demon Lord of Limbo; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Moonlight and Sunlight, Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/belasco-01.png).
- **Epic Belasco**; type/group: Epic Mastermind face / Belasco, Demon Lord of Limbo; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Moonlight and Sunlight, Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/belasco-02.png).
- **A Demon's Mercy**; type/group: Mastermind Tactic / Belasco, Demon Lord of Limbo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/belasco-06.png).
- **Bargain for Souls**; type/group: Mastermind Tactic / Belasco, Demon Lord of Limbo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/belasco-05.png).
- **Rescue from Limbo**; type/group: Mastermind Tactic / Belasco, Demon Lord of Limbo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/belasco-03.png).
- **Cleaving Demonblade**; type/group: Mastermind Tactic / Belasco, Demon Lord of Limbo; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/belasco-04.png).

### Mastermind: Emma Frost, The White Queen

- **Emma Frost, The White Queen**; type/group: Normal Mastermind face / Emma Frost, The White Queen; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emma-01.png).
- **Epic Emma Frost**; type/group: Epic Mastermind face / Emma Frost, The White Queen; copies: Unverified; printed values: Attack 9+; VP 6; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emma-02.png).
- **Tempting Bargain**; type/group: Mastermind Tactic / Emma Frost, The White Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emma-04.png).
- **Psychic X-Men Link**; type/group: Mastermind Tactic / Emma Frost, The White Queen; copies: Unverified; printed values: not indexed in C1; keyword labels: Waking Nightmare; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emma-06.png).
- **Assume Diamond Form**; type/group: Mastermind Tactic / Emma Frost, The White Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emma-05.png).
- **Contempt for Weaklings**; type/group: Mastermind Tactic / Emma Frost, The White Queen; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/emma-03.png).

### Scheme: Demon Bear Saga, The

- **Demon Bear Saga, The**; type/group: Scheme / Demon Bear Saga, The; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/122Scheme(114).png).

### Scheme: Crash the Moon into the Sun

- **Crash the Moon into the Sun**; type/group: Scheme / Crash the Moon into the Sun; copies: Unverified; printed values: not indexed in C1; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/120Scheme(27).png).

### Scheme: Trapped in the Insane Asylum

- **Trapped in the Insane Asylum**; type/group: Scheme / Trapped in the Insane Asylum; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/123Scheme(115).png).

### Scheme: Superhuman Baseball Game

- **Superhuman Baseball Game**; type/group: Scheme / Superhuman Baseball Game; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/121Scheme(113).png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.

## Difficulty data (#269, 2026-10-14)

`Data/Difficulty/the-new-mutants.json` (format v3, source `Q`). Card facts: the shared OCR cache (titles and keyword markers only, no card text), the C1 index above for names, classes, costs, printed Recruit/Attack icons and Villain copies, and the owner's LegendaryAssistant data (`new-mutants-allies`, `-adversaries`, `-commanders`, `-schemes`) for what each card does. Names follow C1 (S'ym, Cleaving Demonblade, Haunted By the Demon Bear, Solar-Powered).

- **Class conflict.** Empathic Link is Instinct in C1 and Covert in the owner's data; C1 is used.
- **Copies: default.** No source gives Hero copies; each Hero's four slots are 5, 5, 3, 1 in ascending cost order (ties: Mirage's two cost-3 cards and Wolfsbane's two cost-3 cards take 5 and 5). Villain copies are C1's.
- **Printed 0+ icons.** Analyze Planetary Rotation and Nanite Shapeshifter (Recruit 0+, Attack 0+) are `both`; other N+ cards carry one icon: `attack`. Earthling Choices has no icon: `none`.
- **Draws.** Conditional and optional draws are `draw` as in core: Dreams Made Real 1, Absorb Radiation 1, Wolf Out 1, Earthling Choices 1, Nanite Shapeshifter 3.
- **Wants.** Moonlight and Sunlight cards want that keyword (strength 2 where it is the whole payoff); Waking Nightmare cards want the Waking Nightmare keyword; X-Men team superpowers want `team x-men`; Karma's Villain-count cards want `board villains-in-city`; Warlock's Tech payoffs want `class tech`. Keyword wants are not scored yet.
- **Self costs.** Thermokinetic Fury requires a card put on the bottom of the deck to play: `discard`. Nightmare Wolves' and Karmic Balance's optional effects are not tagged.
- **Answers.** Cards that put HQ Heroes on the bottom of the Hero Deck are `hq` (Absorb Radiation, Wolf Out 1; Empyreal Force 2).
- **Masterminds.** Belasco's Master Strike: `deck-thinning 2`, `hand-disruption 1` (Waking Nightmares) and `attack-inflation 1` (his KO-pile bonus); Cleaving Demonblade hits the HQ (`hq-disruption 1`). Emma Frost's Master Strike: `hand-disruption 2` (the stacked Strikes escalate) and `attack-inflation 2` (+Attack per grey Hero).
- **Villain Groups.** S'ym (reveal Strength or Wound, plus Waking Nightmares) is a `check` on ambush and escape; Empath (reveal Covert or Waking Nightmare) a `check` on ambush and escape; Jetstream's Escape (discard an X-Men or Wound) a `check` on `x-men`. Crotus, Witchfire (HQ and bystander pressure) are ambush; Demon Bear's Waking Nightmares and Attack bonus ambush; N'astirh, Catseye and Thunderbird KO a Hero on Fight (`deck-thinning 1`) and carry their Moonlight/Sunlight Attack bonus as `attack-inflation 1`; Tarot and Roulette's reveals are Wounds and Twist/Waking Nightmare pressure.
- **Schemes.** The Demon Bear Saga `escape-pressure 2`, amplifies `escape-pressure`; Crash the Moon into the Sun `twist-density 2`, amplifies `twist-density`; Trapped in the Insane Asylum `hand-disruption 2`, amplifies `hand-disruption`; Superhuman Baseball Game `escape-pressure 2`, amplifies `escape-pressure`.

### Difficulty: no axis

- Moonlight and Sunlight are HQ-parity conditions, not a team or class: no check, gate or want axis names them; they are tagged as keyword wants only.
- Moonlight/Sunlight Attack bonuses on Crotus and Witchfire, and Empath's +Attack per grey Hero, are untagged: their other tagged effects fire at Ambush or Escape, and format version 3 gave a card one timing. Since #292 they could be tagged at Fight. The same bonuses on N'astirh, Catseye and Thunderbird are tagged on their Fight; Emma Frost's per-grey-Hero bonus is on her Master Strike slot.
- Jetstream's Ambush reposition on the Bridge, Witchfire's and Demon Bear's captured-Hero KO on Escape, Crotus's and N'astirh's Sunlight recruit surcharge: no axis beyond what is tagged.
- Crash the Moon into the Sun's parity-driven Altered Orbits: tagged `twist-density` as the closest axis; the HQ-manipulation counterplay has no axis.
- Trapped in the Insane Asylum's Sanity Test and passed Psychotic Breaks (a forced random play from hand): tagged `hand-disruption`; passing the Twist between players has no axis.
- Tactics untagged because they only hit other players or only help the fighter: A Demon's Mercy, Bargain for Souls, Rescue from Limbo, Contempt for Weaklings, Psychic X-Men Link, Tempting Bargain, Assume Diamond Form. Belasco's and Emma Frost's per-Hero Attack scaling is a passive, tagged on the Master Strike.
