# LegendaryPicker plan memory

**Snapshot date:** 2026-09-27
**Phase:** Domain discovery and rules research; implementation has not started.
**Previous design checkpoint:** `72fa6e7` (`Update Legendary setup decisions`); this memory snapshot was subsequently committed and pushed.

This is a handoff snapshot of the current shared understanding, verified facts, and unresolved work. `Docs/Plan.md` is the concise working plan; `CONTEXT.md` is the domain glossary. Update both as decisions and facts change. Do not implement the generator until the user confirms the final design.

## Repository and current implementation

- Repository: [RyanGano/LegendaryPicker](https://github.com/RyanGano/LegendaryPicker), branch `main`.
- Frontend: `LegendaryPickerApp`, a React/TypeScript/Vite starter app.
- Backend: `LegendaryPickerService`, an ASP.NET Core Minimal API with only `/api/health` and CORS configured; no game rules or game data are implemented.
- GitHub Pages deploys the frontend from `main`: https://ryangano.github.io/LegendaryPicker/.
- The user selected Azure App Service for the eventual API host and prefers the lowest-cost compatible plan, accepting cold starts/limits. No Azure resources have been created or authorized; SKU, region, and cost remain unselected.

## User decisions to date

### Game and rules scope

- Support **Marvel Legendary First Edition (2013) core box only** in v1. Other Legendary game lines and expansions are excluded.
- Support player counts **1–5**. One player uses the First Edition core Solo mode; multiplayer uses the First Edition rules.
- Exclude **Advanced Solo** from v1 because its origin in the core box is unverified.
- Require complete, sourced coverage for every supported player count before calling v1 rules-valid; do not publish a partial count range as the complete randomizer.
- Use the First Edition rulebook and official clarifications. Accept rulings directly attributed to the game's designer or an Upper Deck rules representative even when archived elsewhere, recording provenance. Do not rely on unattributed community interpretations or substitute Second Edition rules.

### Generation and result

- Generate exactly one complete random setup at a time. Players cannot choose/lock a Scheme or Mastermind first and cannot add their own must-include/must-exclude card preferences.
- Guarantee setup legality, not a subjective balance or difficulty level.
- For the entered player count, choose equally among Schemes with at least one complete legal setup. Then choose uniformly among the distinct complete legal setups compatible with the selected Scheme. Different selection/shuffle order is not a distinct setup.
- If an individual Scheme has no legal completion at that player count, exclude it before the equal-probability draw; never relax a rule.
- If no Scheme at all has a legal completion for the entered player count, show a clear explanation and offer another count; do not silently change modes or relax a rule.
- Show a complete setup checklist: randomized components/groups, fixed shared piles, and player starting decks/counts. Add concise notes for rule-driven inclusions/exclusions, with a short citation label and a source link when available. Do not enumerate every card within a selected group or provide an ordered rulebook walkthrough.
- Let the player generate another setup, replacing the current result. Setups are transient: no accounts, game-state tracking, history, favorites, or saved setups.

### Platform and data maintenance

- Mobile-first responsive browser experience; online use is sufficient.
- The C# Minimal API owns authoritative rules and generation; React calls the API. The API is public and does not require sign-in; consider basic service-side rate limits.
- Keep the catalog/rules in versioned project data grouped by box; changes ship through reviewed updates, not an admin UI or database.
- When expansion support is added later, players choose which boxes to include in a setup.
- User selected Azure App Service with a lowest-cost hosting priority. Actual resource provisioning still requires a separate explicit request and later SKU/region/cost decisions.

## First Edition facts confirmed so far

The original First Edition rulebook was recovered from its official Upper Deck URL via the Wayback Machine:

- [Archived Upper Deck First Edition rulebook](https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf)
- [BoardGameGeek Legendary Marvel FAQ](https://boardgamegeek.com/wiki/page/Legendary_Marvel_FAQ)
- [Designer ruling on Scheme versus Always Leads](https://boardgamegeek.com/thread/993341/article/12653573)

The archived rulebook's contents manifest specifies 15 Heroes (14 cards per Hero), 7 Villain Groups (8 cards each), 4 Henchman Groups (10 cards each), 4 Masterminds, and 8 Schemes. It also identifies the basic shared stacks and player starting decks; the complete catalog of named setup groups is not enumerated in the rulebook.

For the fixed checklist, the rulebook establishes 8 S.H.I.E.L.D. Agents plus 4 S.H.I.E.L.D. Troopers in each player's starting deck; shared stacks include 30 S.H.I.E.L.D. Officers, 30 Wounds, and 30 Bystanders. The game includes five Master Strikes, and the chosen Scheme determines the number of Scheme Twists. A selected Hero contributes 14 cards; a Villain Group contributes 8; a Henchman Group contributes 10.

For standard non-Solo setups, the rulebook says to use five Heroes. Its printed 2–5 player table gives:

| Players | Villain Groups | Henchman Groups | Bystanders |
|---:|---:|---:|---:|
| 2 | 2 | 1 | 2 |
| 3 | 3 | 1 | 8 |
| 4 | 3 | 2 | 8 |
| 5 | 4 | 2 | 12 |

The one-player setup is separate. The rulebook specifies a three-Hero (42-card) Hero Deck, one Villain Group, three Henchman cards from one Henchman Group, one Bystander, and the selected Scheme's normal number of Twists. Solo ignores a Mastermind's Always Leads ability and excludes *Super Hero Civil War* and *Negative Zone Prison Breakout*. The general setup calls for five Master Strikes; confirm how this applies in Solo when validating the full checklist.

The designer-attributed FAQ ruling confirms a Scheme takes precedence over a conflicting Mastermind Always Leads requirement. The FAQ's example also confirms one Henchman Group at 1–3 players and two at 4–5 players. Use the archived First Edition table above, not the Second Edition table: the editions have different card pools and setup rules.

## Card catalog status

The official rulebook establishes the total number of groups but is not a full checklist. Current catalog research reports:

- **Heroes:** 15 identities have been identified in the rulebook; the Hero team affiliation of Spider-Man and Deadpool needs careful confirmation.
- **Villain Groups:** HYDRA, Spider-Foes, Sentinels, Skrulls, Hand Ninjas, Masters of Evil, and Brotherhood are identified; Brotherhood is evidenced by a Mastermind's Always Leads relationship.
- **Masterminds:** Red Skull and Magneto are confirmed by card fragments; two of the four remain unidentified.
- **Schemes:** five of eight have been identified: *Unleash the Power of the Cosmic Cube*, *Bank Robbery Hostage Crisis*, *Secret Invasion of the Skrull Shapeshifters*, *Super Hero Civil War*, and *Negative Zone Prison Breakout*.
- **Henchman Groups:** their total is four, but none of their names is yet confirmed. A HYDRA-themed card used to illustrate card anatomy is not enough evidence to classify a Henchman Group.
- Exact setup metadata for every Scheme and Always Leads relationship still needs a trustworthy catalog source and cross-check.

Names above are short factual identifiers only; do not copy full card text or flavor text into the repository.

## Remaining work before design confirmation

1. Validate which standard setup components are inherited by Solo (especially Master Strikes) and extract all Scheme-driven changes to Hero/Villain/Henchman counts.
2. Find and cross-check a complete First Edition core-box card/group catalog, with source provenance per relationship. A previous report found a fan-made card reference but did not validate it; distinguish catalog discovery from authoritative rule interpretation.
3. Update `Docs/Plan.md` and `CONTEXT.md` as new facts are verified, then summarize the complete design and ask the user to confirm shared understanding.
4. Only after confirmation, implement the generator/API/UI and tests. Plan deployment to the selected App Service target separately; do not provision Azure resources without explicit authorization.

## Temporary research artifacts

The full rulebook PDF and extracted text were downloaded for research under `_research_tmp2/`; they are not project source and must not be committed or redistributed. Keep only concise factual notes and source links. `resp.json` is unrelated research metadata and should also stay out of commits.
