# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

LegendaryPicker randomly generates a legal setup for Upper Deck's Marvel Legendary card game. The design is confirmed and implementation is tracked as GitHub issues. The service holds the First Edition core box catalog and setup rules as data, and `Setup/SetupGenerator` draws a random legal setup and its checklist from them; the generator's HTTP API and the game UI do not exist yet.

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
```

`build` runs `tsc -b` before `vite build`, so it doubles as the type check. Lint is oxlint (`.oxlintrc.json`), not ESLint.

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
- Hosting is split. GitHub Actions (`.github/workflows/deploy-pages.yml`) builds and deploys only the frontend to GitHub Pages on every push to `main`; `vite.config.ts` switches `base` to `/LegendaryPicker/` when `GITHUB_ACTIONS` is set. The API is planned for Azure App Service, but no Azure resources exist; provisioning needs explicit user authorization plus SKU/region/cost decisions.
- CI checks come from `.github/workflows/test.yml`, which runs on every pull request and push to `main`: `dotnet test LegendaryPicker.slnx` (job `service`) and `npm ci`, `npm run lint`, `npm run build` in `LegendaryPickerApp` (job `frontend`). It does not gate the Pages deploy.
- CORS origins for the API come from the `Cors:AllowedOrigins` config section.
- Game data (catalog + sourced setup rules) lives in `LegendaryPickerService/Data/Boxes/<box-id>.json`, one file per box, changed only through reviewed commits: no database or admin UI. `Catalog/BoxCatalog` loads every file at startup (a singleton) and rejects unknown fields, missing values, malformed or duplicate ids, and references that don't resolve. Ids have the form `<boxId>_<kind>_<name>` (`core_mastermind_dr-doom`; kinds `hero`, `villain`, `henchman`, `mastermind`, `scheme`; see Ids in `Docs/Plan.md`); every rule value and setup effect carries a `source` key from `Docs/Plan.md`.

## Rules and data constraints

- v1 scope is the Marvel Legendary **First Edition (2013) core box** only, player counts 1–5, with First Edition Solo for one player. Second Edition rules differ; never substitute them.
- Every setup rule needs a source: the First Edition rulebook, an official clarification, or a ruling directly attributed to the designer or an Upper Deck rules representative. Record provenance alongside the data. A community catalog may supply card names and group mappings only.
- Generation guarantees legality, never balance. Never relax a rule to make a Scheme playable; drop Schemes with no legal completion before the draw.
- Store only short factual identifiers (card/group names, counts). Keep rulebook text, card text, and flavor text out of the repo.
- Research downloads (the rulebook PDF and extracted text under `_research_tmp2/`, `resp.json`) stay local and uncommitted; they are not in `.gitignore`, so stage files explicitly.
