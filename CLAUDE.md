# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

LegendaryPicker randomly generates a legal setup for Upper Deck's Marvel Legendary card game. The design is confirmed and implementation is tracked as GitHub issues. The service holds the First Edition core box catalog and setup rules, the Legendary: Villains base game's (on its own Villainous ruleset), and each added expansion's catalog (Dark City, Fantastic Four, Paint the Town Red, Guardians of the Galaxy and the Villainous Fear Itself so far), as data, and `Setup/SetupGenerator` draws a random legal setup and its checklist from them, which `GET /api/setup` serves. `GET /api/setup` takes the included boxes as `boxes=` (default `core`; at least one must be a base game, and the core box is never mandatory; an expansion's box file has no `setup` section, and every box names its `ruleset`; boxes of different rulesets can be combined when an included base game has mixing rules (`setup.mixing`), and Villains rules apply only where needed: a Villains Plot or Commander means Villains rules, otherwise First Edition rules (#85, #88); other Villains cards bring only the parts of the game they use (#87); an expansion with an `otherRuleset` section, such as Fear Itself, can also be played under the other ruleset's base game alone, with stand-ins like Wounds for Bindings (#110)), and `GET /api/boxes` lists them. Each generator capability is tested with made-up expansions under `LegendaryPickerService.Tests/Fixtures`; each real expansion also gets its own tests against its box file (`DarkCityTests`, `FantasticFourTests`, `PaintTheTownRedTests`, `GuardiansOfTheGalaxyTests`, `FearItselfTests`, `VillainsTests`, `MixedRulesetsTests`). The web app lets a player pick a count and the boxes to include (base games and expansions), and renders the draw as the Setup checklist (`SetupChecklist`): deck counts and totals, stacks, starting decks, and the cited Rule notes, with a tick box per line.

## Read before working

- `CONTEXT.md` is the domain glossary (Scheme, Mastermind, Always Leads group, Legal setup, Scheme-first random selection, …). Use its terms exactly in code, docs, and conversation.
- `Docs/Plan.md` is the working plan: scope decisions, randomness rules, open questions.
- `Docs/plan_memory.md` is the detailed handoff snapshot: every user decision to date, verified First Edition facts with sources, and card-catalog status.
- `Docs/BoxResearch/README.md` is the release-order data-collection queue and source guide. Follow its product-record path when researching a box; collected research lives in `Docs/BoxResearch/<slug>.md`, while implemented runtime data lives in `LegendaryPickerService/Data/Boxes/<box-id>.json`. Check existing box data before duplicating facts.

When a decision or verified fact changes, update `Docs/Plan.md` and `CONTEXT.md` (and the snapshot) in the same change.

## Commands

Frontend (`LegendaryPickerApp`, Vite + React 19 + TypeScript, run from that folder):

```bash
npm install
npm run dev
npm run build
npm run lint
npm test
```

`build` runs `tsc -b` before `vite build`, so it doubles as the type check. Lint is oxlint (`.oxlintrc.json`), not ESLint. `npm test` runs Vitest with Testing Library in jsdom (config in `vite.config.ts`); tests mock `fetch` and never call the API. `npm run dev` reads the API origin from `VITE_API_BASE_URL` in `.env.development` (`http://localhost:5179`, the service's `http` profile).

Backend (`LegendaryPickerService`, ASP.NET Core Minimal API on .NET 10, run from the repo root):

```bash
dotnet run --project LegendaryPickerService
```

Tests (xUnit, `LegendaryPickerService.Tests`) run from the repo root through `LegendaryPicker.slnx`:

```bash
dotnet test
```

## Architecture

- The C# API is the rules authority: it owns the catalog, setup rules, and generator. The React app only calls it and renders the result checklist.
- The web app's look comes from the design tokens at the top of `LegendaryPickerApp/src/index.css`: type and spacing scales, radii, shadows and one accent per card type, each defined for light and dark. Style with the tokens rather than raw values. Headings use the Bangers display face from Google Fonts; body text stays the system sans. The footer's fan-made disclaimer stays; the app carries no Marvel or Upper Deck logos, art or trade dress.
- Hosting is split. GitHub Actions (`.github/workflows/deploy-pages.yml`) builds and deploys only the frontend to GitHub Pages on every push to `main`; `vite.config.ts` switches `base` to `/LegendaryPicker/` when `GITHUB_ACTIONS` is set. `.github/workflows/deploy-api.yml` runs `dotnet test` and then deploys `LegendaryPickerService` to Azure App Service (Free F1 Linux, West US 2; the web app named by the `AZURE_WEBAPP_NAME` repository variable) on pushes to `main` that touch the service. It signs in with OIDC from the `AZURE_*` repository variables; the Azure credential trusts only `refs/heads/main`, so don't add a GitHub environment to that job. The Pages build reads the API URL from the `VITE_API_BASE_URL` repository variable. Keep Azure resource names and the API hostname out of the repo; refer to the repository variables. Creating or changing any other Azure resource needs explicit user authorization.
- CI checks come from `.github/workflows/test.yml`, which runs on every pull request and push to `main`: `dotnet test LegendaryPicker.slnx` (job `service`) and `npm ci`, `npm run lint`, `npm test`, `npm run build` in `LegendaryPickerApp` (job `frontend`). It does not gate the Pages deploy.
- CORS origins for the API come from the `Cors:AllowedOrigins` config section.
- `/api/setup` is rate-limited per client IP. Behind App Service the IP comes from `X-Forwarded-For`, trusted only from the platform front end (169.254.0.0/16); IPv6 clients are bucketed by /64.
- Game data (catalog + sourced setup rules) lives in `LegendaryPickerService/Data/Boxes/<box-id>.json`, one file per box, changed only through reviewed commits: no database or admin UI. `Catalog/BoxCatalog` loads every file at startup (a singleton) and rejects unknown fields, missing values, malformed or duplicate ids, and references that don't resolve. Ids have the form `<boxId>_<kind>_<name>` (`core_mastermind_dr-doom`; kinds `hero`, `villain`, `henchman`, `mastermind`, `scheme`, `term`; see Ids in `Docs/Plan.md`); every rule value and setup effect carries a `source` key from `Docs/Plan.md`.

## Rules and data constraints

- Scope is the Marvel Legendary **First Edition core box** (released November 2012) plus its expansions, added one box at a time in release order (#34; Dark City, Fantastic Four, Paint the Town Red and Guardians of the Galaxy so far on the First Edition ruleset, and Fear Itself on the Villainous one), and the Legendary: Villains base game on the Villainous ruleset (#72): player counts 1–5, with First Edition Solo, or Villains' own Solo in a Villainous setup, for one player. A Villainous setup uses the Villains rulebook's words (Plot, Commander, Ally, Adversary Deck, …); the code keeps the First Edition names. Second Edition rules differ; never substitute them.
- Every setup rule needs a source: the First Edition rulebook, the box's own rulebook or rules insert, an official clarification, or a ruling directly attributed to the designer or an Upper Deck rules representative. Record provenance alongside the data. For research indexes, C1/C2 may supply structured card metadata (names, group mappings, icons, teams/classes, and printed numeric values) with a direct card-image link; their ability text is not a rules authority.
- If a card in the setup uses another part of the game (a stack such as Wounds, Bindings, Officers or Sidekicks, a token, or another card type), that part must be in the setup; a part no card uses is left out (owner rule, 2026-09-29, #87). Each card's `uses` in its box file lists the stacks its text takes cards from, a base game's `setup.uses` the stacks its rules use, and a Hero, group or Mastermind whose parts no included box supplies is dropped before the draw; a Scheme and the cards it requires are drawn regardless, the part laid out from the base game that has it (#138).
- Every included Hero, Ally, Villain Group, Adversary Group, Henchman and Backup Adversary group stays drawable whatever Scheme and Mastermind are drawn; a card is never locked out for not matching them (owner rule, 2026-09-29, #88).
- Generation guarantees legality, never balance. Never relax a rule to make a Scheme playable. The Scheme is drawn first, from every included Scheme its card allows at the player count, then a Mastermind it doesn't exclude; nothing is checked per draw (owner decision D-scheme-first, #138). Every requirement is met inside its own box, which the loader enforces, unless the box data allows it from another box (`otherBox`): that card is still in the setup when its box isn't ticked, labelled as required by the Scheme from that box, or replaced by an included one when the card gives a substitute. A Scheme's `excludesMasterminds` is recorded when the box is entered and checked by a test that derives it from the card data.
- Runtime box data stores short factual identifiers, counts, and sourced setup effects. Product research notes may additionally index printed card metadata (type, team/class/keyword icons, numeric values) and link to each card face. Keep card, rulebook, and flavor text out of the repo; summarize only setup-relevant effects or mechanics needed to determine setup components, in original concise wording.
- Every card a setup names must tell the player which physical card to use. No two Heroes, Villain Groups, Henchman Groups, Masterminds or Schemes share a display `name` across the box files (compared ignoring case; the loader refuses to start otherwise). A new version of an existing character takes a distinguishing name, printed card title first ("Symbiote Spider-Man"), or the title with its version in brackets when the title is the same ("Wolverine (X-Force)"); `heroName` keeps the shared character for Hero rules. When a setup includes more than one box, each drawn card also shows its box.
- Glossary term summaries (`glossary` in each box file) are one exception to names-only: original paraphrases in our own words, at most two short sentences (40 words, enforced at load), never quoted or closely reworded from the rulebook, each with a `source` key and `page` that point to the full rule. Rewrite any summary that reads like copied rulebook text.
- Setup step labels (`steps` in a Scheme's or Mastermind's `setup`) and Solo play rule labels (`solo.playRules`) are the other: one short instruction in our own words (15 words, enforced at load), never card or rulebook text, each with a `source`.
- Research downloads (the rulebook PDF and extracted text under `_research_tmp2/`, `resp.json`) stay local and uncommitted; they are not in `.gitignore`, so stage files explicitly.
