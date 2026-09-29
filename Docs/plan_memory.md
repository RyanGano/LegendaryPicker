# LegendaryPicker plan memory

**Snapshot date:** 2026-09-27
**Phase:** Design confirmed; implementation is tracked as GitHub issues under the "Legendary Picker v1" parent issue.

This is a handoff snapshot. `Docs/Plan.md` holds the decisions, the verified First Edition facts and their sources; `CONTEXT.md` is the domain glossary. Keep facts there, not here.

## Where things stand

- Repository: [RyanGano/LegendaryPicker](https://github.com/RyanGano/LegendaryPicker), branch `main`.
- `LegendaryPickerService` loads the core box catalog and setup rules from `Data/Boxes/core.json`, and serves `/api/health` (GET and HEAD, for the UptimeRobot keep-warm monitor, #31) and the rate-limited `GET /api/setup?players=1..5` (issue #4), which returns a `Setup/SetupGenerator` draw (issue #3). `LegendaryPickerApp` pings `/api/health` on load, lets the player pick a count (#5) and renders the draw as the Setup checklist, with counts, totals, cited Rule notes and per-line ticks that reset on a new draw (#6). Players choose which boxes to include (#8): the API takes `boxes=` and lists the boxes at `/api/boxes`, the core box is always included, the picked expansions are remembered in `localStorage`, and with more than one box each rule note names its box. No real expansion is loaded yet; tests use a made-up fixture expansion. Shared stacks (Bystanders, Wounds, Officers, and Sidekicks when a box has them) are summed over the included boxes' `components` (#35). Setup effects can add to counts as well as set them (extra Heroes, Villain Groups, Henchman Groups and Villain Deck Bystanders), per player count or only in Solo, and a Mastermind can carry them too (#36). Schemes can move cards between stacks and decks during setup (`setup.moves`: Hero cards, Henchmen, Bystanders, Wounds, Officers or Sidekicks into the Villain Deck, the Hero Deck, beside the Scheme or each starting deck), which replaced `heroCardsInVillainDeck` (#37); box files are `schemaVersion` 3. The docs now date the First Edition core box to November 2012 (#34 D6). Each setup also carries the glossary terms its components use: teams, classes and keywords with short original summaries and rulebook citations (#28); the UI for them is #29.
- GitHub Pages deploys the frontend from `main`: https://ryangano.github.io/LegendaryPicker/.
- The API is hosted on Azure App Service Free F1, Linux, West US 2, $0 a month (user decision, 2026-09-27). The web app is the one named by the `AZURE_WEBAPP_NAME` repository variable; the resource names and hostname are kept out of the repo. `.github/workflows/deploy-api.yml` deploys it from `main` through OIDC (the Entra app identified by the `AZURE_CLIENT_ID` repository variable, trusted for `refs/heads/main` only); the Pages build points at it through the `VITE_API_BASE_URL` repository variable (#7). Any other Azure resource needs explicit authorization.

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
