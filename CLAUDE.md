# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

LegendaryPicker randomly generates a legal setup for Upper Deck's Marvel Legendary card game. The design is confirmed and implementation is tracked as GitHub issues. The service holds the First Edition core box catalog and setup rules as data, and `Setup/SetupGenerator` draws a random legal setup and its checklist from them, which `GET /api/setup` serves. The web app lets a player pick a count and renders the draw as the Setup checklist (`SetupChecklist`): deck counts and totals, stacks, starting decks, and the cited Rule notes, with a tick box per line.

## Read before working

- `CONTEXT.md` is the domain glossary (Scheme, Mastermind, Always Leads group, Legal setup, Scheme-first random selection, …). Use its terms exactly in code, docs, and conversation.
- `Docs/Plan.md` is the working plan: scope decisions, randomness rules, open questions.
- `Docs/plan_memory.md` is the detailed handoff snapshot: every user decision to date, verified First Edition facts with sources, and card-catalog status.

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

- v1 scope is the Marvel Legendary **First Edition (2013) core box** only, player counts 1–5, with First Edition Solo for one player. Second Edition rules differ; never substitute them.
- Every setup rule needs a source: the First Edition rulebook, an official clarification, or a ruling directly attributed to the designer or an Upper Deck rules representative. Record provenance alongside the data. A community catalog may supply card names and group mappings only.
- Generation guarantees legality, never balance. Never relax a rule to make a Scheme playable; drop Schemes with no legal completion before the draw.
- Store only short factual identifiers (card/group names, counts). Keep rulebook text, card text, and flavor text out of the repo.
- Glossary term summaries (`glossary` in each box file) are the one exception to names-only: original paraphrases in our own words, at most two short sentences (40 words, enforced at load), never quoted or closely reworded from the rulebook, each with a `source` key and `page` that point to the full rule. Rewrite any summary that reads like copied rulebook text.
- Research downloads (the rulebook PDF and extracted text under `_research_tmp2/`, `resp.json`) stay local and uncommitted; they are not in `.gitignore`, so stage files explicitly.
