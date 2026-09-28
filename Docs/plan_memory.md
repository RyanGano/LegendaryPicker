# LegendaryPicker plan memory

**Snapshot date:** 2026-09-27
**Phase:** Design confirmed; implementation is tracked as GitHub issues under the "Legendary Picker v1" parent issue.

This is a handoff snapshot. `Docs/Plan.md` holds the decisions, the verified First Edition facts and their sources; `CONTEXT.md` is the domain glossary. Keep facts there, not here.

## Where things stand

- Repository: [RyanGano/LegendaryPicker](https://github.com/RyanGano/LegendaryPicker), branch `main`.
- `LegendaryPickerService` loads the core box catalog and setup rules from `Data/Boxes/core.json`, and serves `/api/health` and the rate-limited `GET /api/setup?players=1..5` (issue #4), which returns a `Setup/SetupGenerator` draw (issue #3). `LegendaryPickerApp` pings `/api/health` on load, lets the player pick a count and shows the drawn components by name (#5). The full result checklist is #6; the production API URL is #7, so the deployed Pages site cannot reach an API yet.
- GitHub Pages deploys the frontend from `main`: https://ryangano.github.io/LegendaryPicker/.
- No Azure resources exist. The user chose App Service on the lowest-cost compatible plan; SKU, region and cost are undecided and provisioning needs explicit authorization.

## Resolved in the 2026-09-27 session

- The whole core-box catalog is confirmed. An earlier note wrongly listed Sentinel and Hand Ninjas as Villain Groups; they are Henchman Groups.
- Solo uses 1 Master Strike, not 5.
- A Scheme's setup overrides the Solo setup (designer ruling for *Secret Invasion*; the user accepted applying it to Bank Robbery, Killbots and Legacy Virus).
- Always Leads groups and Scheme-required groups fill slots rather than adding groups; the user accepted the extension to Skrulls.
- Generation is a table draw (Scheme, Mastermind, required groups, remaining groups, Heroes), replacing the earlier "uniform over distinct setups" wording.
- Unsourced conflicts fall back to the base Legendary rules; expansion rules apply only when that box is included.
- The planning "cases" are GitHub issues; each issue lists the fixed-result test cases it must pass.

## Research artifacts

Rulebook downloads and extracts stay outside the repository and are never committed or redistributed. Keep only short factual notes and source links.
