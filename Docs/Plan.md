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
- **Result:** Show a complete setup checklist: randomized components/groups, fixed shared piles, and player starting decks/counts. Add concise notes explaining rule-driven inclusions or exclusions, with a short citation label and source link when available. Do not enumerate every card inside a selected group or reproduce an ordered rulebook walkthrough.
- **App boundary:** Generate and present setups only. No turn/game-state tracking, accounts, history, favorites, or saved setups in v1.
- **Use context:** Mobile-first responsive web app; online use is sufficient for v1.
- **Rules authority:** Use the First Edition rulebook and official clarifications. Accept rulings directly attributed to the game's designer or an Upper Deck rules representative when archived elsewhere, and record their provenance. Do not rely on unattributed community interpretations or silently substitute Second Edition rules.
- **Generation architecture:** The C# Minimal API owns the authoritative rules and setup generator; the React app calls it. GitHub Pages hosts only the frontend; the API is planned for Azure App Service.
- **API access:** The setup API is public and does not require sign-in; use basic service-side rate limits if needed.
- **Hosting priority:** Target the lowest-cost compatible App Service plan and accept possible cold starts/limits. Exact SKU and cost remain unselected; no Azure resources have been created or authorized.
- **Game data:** Keep the catalog and sourced setup rules in versioned project data, grouped by box; future additions ship as reviewed project updates, not through an admin UI/database.
- **Release gate:** Do not present v1 as rules-valid until all setup data and rules are sourced for every supported count from one through five, including First Edition Solo. Do not ship a partial count range as the complete randomizer.

## Existing project groundwork

- `LegendaryPickerApp` is the React/TypeScript/Vite frontend.
- `LegendaryPickerService` is the C# Minimal API backend.
- GitHub Pages is configured to deploy the frontend from `main`.
- `CONTEXT.md` records the agreed domain vocabulary so far.
- No game-specific models, card data, setup generator, or game UI have been implemented yet.

## Research so far

The current Upper Deck rulebook is for Marvel Legendary Second Edition (2026), not the selected First Edition. It explicitly distinguishes the editions and says the Second Edition's solo rules replace earlier solo rules, so its card pool and setup table must not be assumed to describe First Edition.

The original First Edition rulebook appears in BoardGameGeek's file archive as `Legendary_Rulebook_FINAL2.pdf`, posted in 2012 by an account carrying an Upper Deck publisher credit, but the file download is currently unavailable. Upper Deck no longer appears to host that rulebook or a formal First Edition errata document.

The BoardGameGeek *Legendary Marvel FAQ* records answers attributed to designer Devin Low and Upper Deck rules representative JeffP300. A directly attributed designer ruling confirms that a Scheme takes precedence over a Mastermind's "Always Leads" requirement. Its example covers one through five players and confirms one Henchman Group for 1–3 players and two for 4–5 players. The FAQ also confirms the First Edition box has its own Solo setup/scoring rules. It references Advanced Solo, but its origin in the First Edition core box is unverified; Advanced Solo is excluded from v1. These are useful, attributed rulings, but they do not provide the full First Edition per-player setup table.

Sources:

- [BoardGameGeek Legendary Marvel FAQ](https://boardgamegeek.com/wiki/page/Legendary_Marvel_FAQ)
- [Designer ruling: Scheme versus Always Leads](https://boardgamegeek.com/thread/993341/article/12653573)
- [First Edition rulebook archive listing](https://boardgamegeek.com/filepage/83353/marvel-legendary-rule-book)
- [Upper Deck Second Edition rulebook](https://upperdeck.com/wp-content/uploads/2026/08/Legendary-Second-Edition-Rulebook.pdf) — for edition comparison only, not as a First Edition rules source.

## Open questions

1. **Complete First Edition setup table:** Verify all per-count quantities for one through five players (including Heroes, Villain Groups, Henchman Groups, and Bystanders) from a sufficiently authoritative source. Do not infer missing values from Second Edition.
2. **Base Solo setup:** Retrieve the exact First Edition Solo setup details from a sufficiently authoritative source. Advanced Solo is out of scope unless its origin and compatibility are later established.
3. **First Edition card catalog:** Find a trustworthy source for core-box Scheme, Mastermind, Hero, Villain Group, and Henchman Group identities and their setup constraints. The generator needs this data, but the UI need only show selected components/groups and quantities.
## Next planning work

1. Resolve the First Edition player-count table and Solo setup from sourced materials.
2. Inventory the core-box setup components and represent each rule dependency with its source.
3. Walk through edge cases (Scheme versus Always Leads, player-count changes, no legal completion) and confirm the proposed uniformity policy against the resulting setup space.
4. Agree on the final domain model and generation behavior before implementing them.
