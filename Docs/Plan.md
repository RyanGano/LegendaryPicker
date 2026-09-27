# LegendaryPicker design plan

**Status:** Draft for discussion. This records the current understanding, not an implementation authorization. Update it as design decisions and rule research are resolved.

## Product goal

Help a player prepare a legally configured game of Upper Deck's Marvel Legendary by randomly selecting a complete setup and explaining any setup components required or excluded by the selected rules.

## Current scope and decisions

- **Game:** Marvel Legendary First Edition (2013) core box only. Other expansions and other Legendary game lines are out of scope for the first version. When expansion support is added later, players choose which boxes to include per setup.
- **Input:** Ask for the number of players for each setup, supporting one through five players. A one-player game uses the First Edition core box's own Solo rules; multiplayer is also in scope. Advanced Solo is excluded from v1.
- **Generation:** Generate one complete random setup at a time. Players do not lock a Scheme or Mastermind first and do not add their own must-include or must-exclude preferences. A player can generate another setup, replacing the current one without retaining history.
- **Legality:** Satisfy applicable First Edition setup rules. Do not filter for subjective balance or difficulty.
- **Randomness:** For the entered player count, select equally among Schemes that have at least one complete legal setup. For the selected Scheme, select uniformly among distinct complete legal setups compatible with it. The chosen components define a setup; draw order and shuffle order do not make a new setup. Never relax rules to make an impossible Scheme playable.
- **No eligible Scheme:** If no Scheme has any complete legal setup for the entered player count, show a clear explanation and let the player choose another count; do not relax rules or switch modes.
- **Result:** Show a complete setup checklist: randomized components/groups, fixed shared piles, and player starting decks/counts. Add concise notes explaining rule-driven inclusions or exclusions, with a short citation label and source link when available. Do not enumerate every card inside a selected group or reproduce an ordered rulebook walkthrough.
- **App boundary:** Generate and present setups only. No turn/game-state tracking, accounts, history, favorites, or saved setups in v1.
- **Use context:** Mobile-first responsive web app; online use is sufficient for v1.
- **Rules authority:** Use the First Edition rulebook and official clarifications. Accept rulings directly attributed to the game's designer or an Upper Deck rules representative when archived elsewhere, and record their provenance. Do not rely on unattributed community interpretations or silently substitute Second Edition rules.
- **Generation architecture:** The C# Minimal API owns the authoritative rules and setup generator; the React app calls it. GitHub Pages hosts only the frontend; the API is planned for Azure App Service.
- **API access:** The setup API is public and does not require sign-in; use basic service-side rate limits if needed.
- **Hosting priority:** Target the lowest-cost compatible App Service plan and accept possible cold starts/limits. Exact SKU and cost remain unselected; no Azure resources have been created or authorized.
- **Game data:** Keep the catalog and sourced setup rules in versioned project data, grouped by box; future additions ship as reviewed project updates, not through an admin UI/database. A cross-checked community catalog may supply card identities/group mappings, but not rule interpretations.
- **Release gate:** Do not present v1 as rules-valid until all setup data and rules are sourced for every supported count from one through five, including First Edition Solo. Do not ship a partial count range as the complete randomizer.

## Existing project groundwork

- `LegendaryPickerApp` is the React/TypeScript/Vite frontend.
- `LegendaryPickerService` is the C# Minimal API backend.
- GitHub Pages is configured to deploy the frontend from `main`.
- `CONTEXT.md` records the agreed domain vocabulary so far.
- No game-specific models, card data, setup generator, or game UI have been implemented yet.

## Research so far

The First Edition rulebook has now been recovered from its original Upper Deck URL through the Wayback Machine. It is the primary rules source for this project. Its contents manifest identifies 15 Heroes, 7 Villain Groups, 4 Henchman Villain Groups, 4 Masterminds, and 8 Schemes. The rulebook is not a complete card checklist, so the names and relationships of every core-box group still need to be verified.

The First Edition rulebook's standard setup uses five Heroes; its Solo section uses three Heroes (42 cards). Its printed 2–5 player table specifies Villain Groups, Henchman Groups, and Bystanders as follows:

| Players | Villain Groups | Henchman Groups | Bystanders |
|---:|---:|---:|---:|
| 2 | 2 | 1 | 2 |
| 3 | 3 | 1 | 8 |
| 4 | 3 | 2 | 8 |
| 5 | 4 | 2 | 12 |

Solo has its own setup rules: use three Heroes, one Villain Group, three Henchman cards from one Henchman Group, one Bystander, and the Scheme's normal number of Twists; it ignores the Mastermind's "Always Leads" ability and disallows the Schemes *Super Hero Civil War* and *Negative Zone Prison Breakout*. The general setup calls for five Master Strikes; verify its application to Solo, as well as all Scheme-specific count overrides, before implementation.

The BoardGameGeek *Legendary Marvel FAQ* records additional answers attributed to designer Devin Low and Upper Deck rules representative JeffP300. One directly attributed designer ruling confirms that a Scheme takes precedence over a Mastermind's "Always Leads" requirement. Advanced Solo is mentioned in community rules material, but its origin in the First Edition core box is unverified and it is excluded from v1.

Sources:

- [Archived Upper Deck First Edition rulebook](https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf)
- [BoardGameGeek Legendary Marvel FAQ](https://boardgamegeek.com/wiki/page/Legendary_Marvel_FAQ)
- [Designer ruling: Scheme versus Always Leads](https://boardgamegeek.com/thread/993341/article/12653573)
- [First Edition rulebook archive listing](https://boardgamegeek.com/filepage/83353/marvel-legendary-rule-book) — secondary archive listing.
- [Upper Deck Second Edition rulebook](https://upperdeck.com/wp-content/uploads/2026/08/Legendary-Second-Edition-Rulebook.pdf) — for edition comparison only, not as a First Edition rules source.

## Open questions

1. **Solo setup and Scheme overrides:** Confirm which general setup components carry into Solo (especially Master Strikes) and verify every Scheme-specific count change. Do not infer from Second Edition.
2. **First Edition card catalog:** Validate the full core-box Scheme, Mastermind, Hero, Villain Group, and Henchman Group identities/group mappings, preserving the source for each. The UI need only show selected components/groups and quantities; rule constraints still require rulebook or attributed-ruling support.
3. **Backend deployment details:** Choose a region and compatible low-cost App Service SKU and review its cost before any provisioning.

## Next planning work

1. Finish verifying the First Edition Solo setup and Scheme-specific count overrides.
2. Find and cross-check a complete core-box card/group catalog; keep source provenance with every setup rule.
3. Agree on the final domain model and generation behavior before implementing them.
