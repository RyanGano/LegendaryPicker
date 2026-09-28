# LegendaryPicker design plan

**Status:** Design confirmed on 2026-09-27. Implementation is tracked as GitHub issues under the "Legendary Picker v1" parent issue. Update this plan when a decision or verified fact changes.

## Product goal

Help a player prepare a legally configured game of Upper Deck's Marvel Legendary: pick a player count, get one random complete setup, and set it up from a checklist that explains rule-driven inclusions and exclusions.

## Decisions

- **Game:** Marvel Legendary First Edition (2013) core box only for v1. When expansion support is added, players choose which boxes to include per setup, and an expansion's rules apply only when its box is included. Data and rules are organized so adding a box is mostly adding data.
- **Input:** One to five players. One player uses the First Edition core Solo mode. Advanced Solo is excluded.
- **Rule precedence:** Printed card text overrides the rulebook (a Scheme overrides Always Leads and the Solo setup). Otherwise the base Legendary rules apply. Never substitute Second Edition rules.
- **Generation (table draw):** Draw the Scheme from those allowed at the player count; then the Mastermind; then add the groups the Scheme and Mastermind require (Always Leads is ignored in Solo); then draw the remaining Villain and Henchman Groups from what is left; then draw the Heroes. Each draw is equally likely among the remaining options. No locking or player preferences. Legality only, never balance.
- **No eligible Scheme:** If no Scheme is allowed at the player count, explain it and let the player choose another count. This is unreachable with the core box alone but must hold once expansions exist.
- **Result:** The selected Scheme, Mastermind, Heroes, Villain Groups and Henchman Groups; per-component counts and totals for the Villain Deck and Hero Deck; the Wound, Bystander and S.H.I.E.L.D. Officer stacks; each player's starting deck; short rule notes with a citation label and source link. "Generate another" replaces the result. No card-by-card lists or rulebook walkthrough.
- **App boundary:** Generate and present setups only. No accounts, history, favorites, saved setups or game-state tracking.
- **Use context:** Mobile-first responsive web app, online only.
- **UX:** The page calls the API on load to wake it. The player picks a count, then taps Generate. A loading state covers cold starts; errors show a Retry.
- **Architecture:** The C# Minimal API owns the catalog, rules and generator; the React app calls it. GitHub Pages hosts the frontend; the API is planned for Azure App Service on the lowest-cost compatible plan. The API is public, without sign-in, with basic rate limits. No Azure resources exist; provisioning needs an explicit SKU, region and cost decision.
- **Randomness in tests:** The generator takes an injectable random source. Tests use fixed sequences and assert exact results; no statistical tests. No public seed.
- **Game data:** Catalog and sourced setup rules live in versioned project data grouped by box. Scheme and Mastermind setup effects are structured data, not code keyed on card names. Changes ship through reviewed commits.
- **Ids:** Every Hero, Villain Group, Henchman Group, Mastermind and Scheme has the id `<boxId>_<kind>_<name>`, for example `core_mastermind_dr-doom`. `boxId` is the declaring box's id (the core box is `core`), `kind` is one of `hero`, `villain`, `henchman`, `mastermind` or `scheme`, and `name` is kebab-case. Underscores separate the segments; hyphens stay inside a segment. References (Always Leads, a Scheme's required groups) use the full id, so a box can reference another box's groups. The service refuses to start if an id is malformed or duplicated, or if a reference doesn't resolve to a group of the stated type.

## Sources

- **R:** [Archived Upper Deck First Edition rulebook](https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf). Primary rules source; page numbers below are its printed pages.
- **F:** [BoardGameGeek Legendary Marvel FAQ](https://boardgamegeek.com/wiki/page/Legendary_Marvel_FAQ) ([archived copy](https://web.archive.org/web/20210519195532id_/https://boardgamegeek.com/wiki/page/Legendary_Marvel_FAQ)). Designer (Devin Low) and Upper Deck rulings only.
- **D1:** [Designer ruling: Scheme over Always Leads](https://boardgamegeek.com/thread/993341/article/12653573).
- **D2:** Designer rulings that a Scheme's setup overrides the Solo setup: [thread 884926](https://boardgamegeek.com/thread/884926), [thread 898520](https://boardgamegeek.com/thread/898520).
- **Card:** The printed card itself: a Scheme's Twist count and Setup line, a Mastermind's Always Leads. The Schemes table and Masterminds list below record it.
- **C1/C2:** Community card catalogs for names and group membership only: [master-strike core set](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/coreset.ts), [nutki/legendary text files](https://github.com/nutki/legendary/tree/master/texttools/Legendary).

## First Edition core box

Counts (R p.22): 15 Heroes × 14 cards, 7 Villain Groups × 8, 4 Henchman Groups × 10, 4 Masterminds, 8 Schemes, 11 Scheme Twists, 5 Master Strikes, 30 Bystanders, 30 Wounds, 30 S.H.I.E.L.D. Officers, and per-player starting cards. Names confirmed by C1 and C2 against those counts:

- **Heroes:** Black Widow, Captain America, Cyclops, Deadpool, Emma Frost, Gambit, Hawkeye, Hulk, Iron Man, Nick Fury, Rogue, Spider-Man, Storm, Thor, Wolverine.
- **Villain Groups:** Brotherhood, Enemies of Asgard, HYDRA, Masters of Evil, Radiation, Skrulls, Spider-Foes.
- **Henchman Groups:** Doombot Legion, Hand Ninjas, Savage Land Mutates, Sentinel.
- **Masterminds (Always Leads):** Dr. Doom (Doombot Legion, a Henchman Group), Loki (Enemies of Asgard), Magneto (Brotherhood), Red Skull (HYDRA).

R p.2 "Your First Game" lists Villain and Henchman groups together; Sentinel and Hand Ninjas are Henchman Groups.

### Standard setup (R pp.4–6)

Each player starts with 8 S.H.I.E.L.D. Agents and 4 S.H.I.E.L.D. Troopers. The Villain Deck holds the Scheme's Twists, 5 Master Strikes, the Villain Groups, all 10 cards of each Henchman Group, and Bystanders. The Hero Deck is 5 Heroes (70 cards). The Always Leads group counts as one of the groups (R p.6).

| Players | Villain Groups | Henchman Groups | Bystanders |
|---:|---:|---:|---:|
| 2 | 2 | 1 | 2 |
| 3 | 3 | 1 | 8 |
| 4 | 3 | 2 | 8 |
| 5 | 4 | 2 | 12 |

### Solo setup (R p.20)

3 Heroes (42 cards); 1 Villain Group; 3 cards from one Henchman Group; 1 Bystander; 1 Master Strike; the Scheme's normal Twists. Ignore Always Leads. *Super Hero Civil War* and *Negative Zone Prison Breakout* are not allowed. Play rule: after each Twist, KO a Hero costing 6 or less from the HQ. A Scheme's Setup line overrides these values (D2; applied by extension to Schemes other than *Secret Invasion*).

### Schemes

| Scheme | Twists | Setup effect |
|---|---:|---|
| Legacy Virus | 8 | Wound stack is 6 per player |
| Midtown Bank Robbery | 8 | 12 Bystanders in the Villain Deck |
| Negative Zone Prison Breakout | 8 | One extra Henchman Group; not in Solo |
| Portals to the Dark Dimension | 7 | — |
| Replace Earth's Leaders with Killbots | 5 | 3 more Twists beside the Scheme; 18 Bystanders in the Villain Deck |
| Secret Invasion of the Skrull Shapeshifters | 8 | 6 Heroes; Skrulls required; 12 random Hero cards moved from the Hero Deck into the Villain Deck |
| Super Hero Civil War | 8 at 2–3 players, 5 at 4–5 | 4 Heroes at 2 players; not in Solo |
| Unleash the Power of the Cosmic Cube | 8 | — |

A Scheme's required group fills a slot rather than adding one (Devin Low ruling in F on a non-core Scheme, applied by extension). Each group exists once, so no group is drawn twice.

## Open questions

1. **Backend hosting:** Choose the App Service region and SKU and review cost before any provisioning.
