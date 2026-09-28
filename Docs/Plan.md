# LegendaryPicker design plan

**Status:** Design confirmed on 2026-09-27. Implementation is tracked as GitHub issues under the "Legendary Picker v1" parent issue. Update this plan when a decision or verified fact changes.

## Product goal

Help a player prepare a legally configured game of Upper Deck's Marvel Legendary: pick a player count, get one random complete setup, and set it up from a checklist that explains rule-driven inclusions and exclusions.

## Decisions

- **Game:** Marvel Legendary First Edition core box (released November 2012) for v1. Expansions are added one box at a time in release order (#34). Players choose which boxes to include per setup (#8): the core box is always included, and an expansion's rules apply only when its box is included. Data and rules are organized so adding a box is mostly adding data.
- **Input:** One to five players. One player uses the First Edition core Solo mode. Advanced Solo is excluded.
- **Rule precedence:** Printed card text overrides the rulebook (a Scheme overrides Always Leads and the Solo setup). Otherwise the base Legendary rules apply. Never substitute Second Edition rules.
- **Generation (table draw):** Draw the Scheme from those allowed at the player count; then the Mastermind; then add the groups the Scheme and Mastermind require (Always Leads is ignored in Solo); then draw the remaining Villain and Henchman Groups from what is left; then draw the Heroes. Each draw is equally likely among the remaining options. No locking or player preferences. Legality only, never balance.
- **No eligible Scheme:** If no Scheme is allowed at the player count, explain it and let the player choose another count. This is unreachable with the core box alone but must hold once expansions exist.
- **Result:** The selected Scheme, Mastermind, Heroes, Villain Groups and Henchman Groups; per-component counts and totals for the Villain Deck and Hero Deck; the Wound, Bystander and S.H.I.E.L.D. Officer stacks; each player's starting deck; short rule notes with a citation label and source link. "Generate another" replaces the result. No card-by-card lists or rulebook walkthrough.
- **App boundary:** Generate and present setups only. No accounts, history, favorites, saved setups or game-state tracking.
- **Use context:** Mobile-first responsive web app, online only.
- **UX:** The page calls the API on load to wake it and to list the boxes. The player picks a count and ticks the expansions to include (the core box is always ticked; the ticked expansions are remembered in `localStorage`), then taps Generate. A loading state covers cold starts; errors show a Retry.
- **Architecture:** The C# Minimal API owns the catalog, rules and generator; the React app calls it. GitHub Pages hosts the frontend. The API runs on Azure App Service, Free F1 on Linux in West US 2, at $0 a month (decided 2026-09-27), as the web app named by the `AZURE_WEBAPP_NAME` repository variable; its resource names and hostname stay out of the repo. F1 has no Always On, so the app idles and cold-starts, and it has a daily CPU quota; the wake-up ping and loading state cover the cold start. An UptimeRobot monitor, set up by the owner outside the repo, keeps the app warm: it sends `HEAD /api/health` to the web app named by the `AZURE_WEBAPP_NAME` repository variable every 5 minutes and expects 200, so `/api/health` answers both GET and HEAD. If the CPU quota bites, lengthen the interval first. `.github/workflows/deploy-api.yml` runs `dotnet test` and then deploys the service on pushes to `main` that touch it, signing in to Azure through OIDC (no stored secret). The Pages build gets the API URL from the `VITE_API_BASE_URL` repository variable. The API is public, without sign-in, with basic rate limits. Any other Azure resource, or a SKU change, needs explicit user authorization.
- **Randomness in tests:** The generator takes an injectable random source. Tests use fixed sequences and assert exact results; no statistical tests. No public seed.
- **Game data:** Catalog and sourced setup rules live in versioned project data grouped by box. Scheme and Mastermind setup effects are structured data, not code keyed on card names. Changes ship through reviewed commits.
- **Setup generator:** `Setup/SetupGenerator` performs the table draw through an `IRandomSource` (production wraps `Random.Shared`). Each draw picks from the options left in catalog order: the Scheme, the Mastermind, each remaining Villain Group, each remaining Henchman Group, each Hero. The included base game supplies the setup rules; every included box contributes cards, in catalog order whatever order the boxes are named in. A setup includes exactly one base game; choosing a rules base between several is future work (#34, G11). A Scheme the included cards cannot complete (too few groups or Heroes, too few Bystanders or Twists, a required group not included) is dropped before the draw. A Scheme's required groups take their slots first; the Always Leads group takes a slot if one is left and is dropped otherwise (D1). If no Scheme is eligible, the generator returns NoEligibleScheme.
- **Setup endpoint:** `GET /api/setup?players=1..5&boxes=<ids>` returns one table draw as camelCase JSON with a `kind` field: `setup` (the Result, each chosen component as id, name and glossary `terms`, plus the setup's `glossary`) or `noEligibleScheme` (the player count and a message), both 200. A missing or invalid `players` returns a 400 ProblemDetails naming the range. `boxes` is a comma-separated list of box ids and defaults to `core`; an unknown id, or a list without exactly one base game, returns a 400 ProblemDetails under `boxes`. When a setup includes more than one box, each rule note and glossary entry also carries `box`, the name of the box its rule or term comes from; with one box the field is left out. `GET /api/boxes` lists every box as `id`, `name` and `baseGame`. Each client IP gets 30 setups per fixed one-minute window, then 429. The client IP is the address App Service's front end forwards in `X-Forwarded-For`, trusted only from the front end's link-local hop (169.254.0.0/16), and an IPv6 client's whole /64 shares one budget; `/api/health` is unlimited so the frontend can wake the app. Responses carry `Cache-Control: no-store`.
- **Glossary:** Each box file has a `glossary` of terms: Hero teams, Hero classes and keywords (`kind` `team`, `class` or `keyword`). A term has an id, a name, a summary, a `source` key from the box's `sources` and the `page` that defines it. Heroes list their `team` (null when unaffiliated, like Deadpool) and `classes`; every Hero, Villain Group, Henchman Group, Mastermind and Scheme lists the keywords its cards use in `terms`. A term is listed only when the rulebook defines it; which cards use it may come from C1/C2. The service refuses to start on a term reference that doesn't resolve to a term of the right kind, a duplicate term id, or a term with no summary, a summary over 40 words, a source the box doesn't link, or no page. `GET /api/setup` gives each component its `terms` (a Hero's team, then classes, then keywords) and adds a `glossary` of every term the setup uses, once each, with `id`, `name`, `kind`, `summary`, `citation` (for example "R p.9") and `link`; both are ordered teams, classes, keywords, each in catalog order.
- **Glossary content rule:** Summaries are original paraphrases in our own words: at most two short sentences (40 words), never quoted or closely reworded from the rulebook. Each carries a source and page that link to the full rule. Every expansion adds its glossary terms in the same change as its cards.
- **Rule notes and citations:** Each box file lists its source keys with links (`sources`), and `setup.rulings` records the sources of how rules combine: Always Leads fills a slot (R p.6), a required group displaces Always Leads (D1), a Scheme overrides Solo (D2). A note cites the value's own source, or D2 when a Scheme value replaces a Solo value. `Card` has no link. A note's link comes from the sources of the box whose rule it is (the Scheme's box for a Scheme effect, the base game for the rulings and Solo), since two boxes may use the same key for different documents. The service refuses to start if a box lists the same source key twice.
- **Ids:** Every Hero, Villain Group, Henchman Group, Mastermind and Scheme has the id `<boxId>_<kind>_<name>`, for example `core_mastermind_dr-doom`. `boxId` is the declaring box's id (the core box is `core`), `kind` is one of `hero`, `villain`, `henchman`, `mastermind` or `scheme`, and `name` is kebab-case. Glossary terms use the kind `term` (`core_term_ambush`). Underscores separate the segments; hyphens stay inside a segment. References (Always Leads, a Scheme's required groups, glossary terms) use the full id, so a box can reference another box's groups and terms. The service refuses to start if an id is malformed or duplicated, or if a reference doesn't resolve to a group of the stated type; a reference into a box that isn't loaded names that box.
- **Expansion box files:** Only a base game's box file has a `setup` section; an expansion's has none. An expansion still lists its `sources`, `components`, cards and `glossary`. Multi-box behavior is tested with a made-up expansion under `LegendaryPickerService.Tests/Fixtures/Boxes`, never with real expansion data.

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

### Glossary terms (R pp.6-18)

- **Teams (R p.18):** Avengers (Black Widow, Captain America, Hawkeye, Hulk, Iron Man, Thor), X-Men (Cyclops, Emma Frost, Gambit, Rogue, Storm, Wolverine), S.H.I.E.L.D. (Nick Fury), Spider Friends (Spider-Man, per C1 and C2). Deadpool is unaffiliated.
- **Classes (R p.18):** Covert, Instinct, Ranged, Strength, Tech. Each Hero's classes come from C1 and C2.
- **Keywords:** Always Leads (R p.6), Ambush (R p.9), Escape (R p.9), Fight (R p.13), Master Strike (R p.10), Mastermind Tactic (R p.14), Rescue a Bystander (R p.15), Scheme Twist (R p.10). Every Villain and Henchman Group uses Fight; every Mastermind uses Always Leads, Fight, Master Strike and Mastermind Tactic; every Scheme uses Scheme Twist. Ambush, Escape and Rescue a Bystander vary by card (C1, C2).

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

None.
