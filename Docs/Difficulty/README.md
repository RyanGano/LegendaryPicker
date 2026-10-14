# Difficulty rating

How hard a drawn setup is to win, as a number 1 (easiest) to 5 and a band name. The design and the slices are in
[#263](https://github.com/RyanGano/LegendaryPicker/issues/263); this page records what is built. The rating measures how
likely the players are to lose, not how much there is to learn. Generation guarantees legality, never balance: a rating
never drops or redraws a Scheme.

## What is built (#264)

A first, Hero-Deck-only rating, end to end: `Data/Difficulty/<box-id>.json` (loaded by `Catalog/BoxCatalog`),
`Setup/DifficultyScorer.cs`, a `difficulty` block on every `GET /api/setup` setup response, and a panel on the checklist
with a "Why this rating" list. The Heroes' wants and answers, a `difficulty=` target, calibration and the other boxes
are later slices (#266 to #271).

## What is built (#265)

The opposition: what the Mastermind, Villain Groups, Henchman Groups and Scheme check for, gate on and inflict, so one
Hero Deck rates differently against different oppositions. The format is schemaVersion 2 (below); `Setup/DifficultyScorer.cs`
takes a `ScoredOpposition`, and the checklist's "Why this rating" lists the opposition's biggest contributors first.

## Data format (schemaVersion 2)

One file per box, `LegendaryPickerService/Data/Difficulty/<box-id>.json`. A box with no file is valid and rates neutral
(3). Tags and numbers only, never card text.

```json
{
  "schemaVersion": 1,
  "box": "core",
  "source": "Q",
  "heroes": [
    { "hero": "core_hero_cyclops",
      "cards": [ { "name": "Determination", "copies": 5, "class": "strength", "cost": 2, "economy": "recruit" } ] }
  ],
  "masterminds": [
    { "mastermind": "core_mastermind_magneto",
      "masterStrike": { "check": { "requires": ["team", "x-men"], "onFail": "hand-disruption" }, "severity": 2 },
      "tactics": [ { "name": "Crushing Shockwave", "check": { "requires": ["team", "x-men"], "onFail": "wounds" }, "severity": 1 } ] }
  ],
  "villainGroups": [
    { "group": "core_villain_brotherhood",
      "cards": [
        { "name": "Blob", "copies": 2, "gate": ["team", "x-men"], "severity": 2 },
        { "name": "Juggernaut", "copies": 2, "inflicts": [["deck-thinning", 2]], "on": ["ambush", "escape"] } ] }
  ],
  "henchmanGroups": [
    { "group": "core_henchman_sentinel", "cards": [ { "name": "Sentinel", "inflicts": [["deck-thinning", 1]], "on": ["fight"] } ] }
  ],
  "schemes": [
    { "scheme": "core_scheme_legacy-virus",
      "twist": { "check": { "requires": ["class", "tech"], "onFail": "wounds" }, "severity": 2 },
      "amplifies": ["wounds"] }
  ]
}
```

The opposition lists are optional. Each tag has at least one of `check`, `gate`, `inflicts`:

- `check`: `{ requires: [team|class, value], onFail: axis }`, shown from the Hero Deck or suffered; `gate`: `[team|class, value]`,
  needed to defeat the card; `severity` (1..3) belongs to a check or gate; `inflicts` is `[[axis, severity 1..3], ...]`.
- `on`: `ambush`, `fight`, `escape` for a Villain or Henchman card (required, except a gate, which is a Fight);
  `strike` for a Master Strike and `twist` for a Scheme's Twist (both implied); a Tactic is a `fight`.
- Axes: `wounds`, `bystander-pressure`, `hq-disruption`, `hand-disruption`, `attack-inflation`, `escape-pressure`,
  `deck-thinning`, `twist-density`. A Scheme's `amplifies` lists axes its loss condition makes worse.
- A team value is a team term of the box (`<box>_term_<value>`); a class value is a Hero class. A Villain card says its
  `copies`; a Henchman card does not (the Villain Deck's henchman cards are split over the drawn groups).
- The loader rejects an unknown axis, timing or kind, a part the box does not declare, a team the box does not have, a
  tag with no effect, a severity outside 1..3, a timing that does not fit the card, and a card with no `on`.

- `source` is a key in the box file's `sources` for where the cards were read.
- `copies` is how many of the Hero's 14 cards are that named card; the loader does not check they sum to 14 (#140).
- `class` is `covert`, `instinct`, `ranged`, `strength` or `tech`; a multiclass card lists an array.
- `cost` is the printed recruit cost; `economy` is `recruit`, `attack`, `both` or `none`.
- The loader rejects unknown fields, missing values, an unknown class or economy, a source the box does not list, a
  Hero the box does not declare, a Hero or card listed twice, and a file for a box that is not loaded.

## Scorer v0

Inputs are the drawn Hero Deck only (fill-in Heroes are left out; a Hero whose box has no file is left out; none left
rates 3). Every share is weighted by copies:

| Measure | Reads as |
|---|---|
| mean cost | higher is harder |
| share of cards costing 6 or more | higher is harder |
| share of the deck in its largest team | higher is easier (team bonuses fire) |
| share of the deck in its largest class | higher is easier |
| distinct classes present | all five is harder |

Each maps to 0 (easy) .. 1 (hard) between two constants, and a weighted sum gives the raw score. All constants, the band
starts and the band names live in one record, `DifficultyTuning`. They are starting guesses; the calibration slice (#268)
sets them. The first band starts at raw 0 and the others at 0.2, 0.4, 0.6 and 0.8.

## Scorer v1: the opposition

Each tag adds a load: occurrences * severity. Occurrences are derived from the draw, not tagged: a Master Strike tag counts
the Master Strikes in the Villain Deck, a Twist tag its Twists, a card its copies, and each is scaled by what the game
sees (`OccurrenceShare`, 0.7) and by when it fires (Ambush, Strike and Twist 1.0; Fight and Escape 0.5, since the players
choose whom to fight). A check or gate is multiplied by the chance the players cannot show it,
`1 - chanceToShow` with `chanceToShow = 1 - (1 - A * share)^H`, where `share` is the Hero Deck's copy-weighted share of the
team or class, `A` = 0.6 (`HeroDeckShare`, the starting deck dilutes the rest) and `H` = 6 (`HandSize`). Axes the Scheme
amplifies count 1.5 times. The total load maps to 0..1 (`LoadEasy` 4, `LoadHard` 24) and joins the Hero Deck's raw score
half and half (`OppositionWeight`). A setup with none of its opposition tagged rates on the Hero Deck alone.

Watch for the opposition load dominating the Hero Deck terms; a note for calibration (#268). With the core box's fifteen
Heroes, most draws rate in the top bands because the Hero Deck terms of #264 already lean hard.

## Bands

| Rating | Band |
|---|---|
| 1 | Sidekick |
| 2 | Street-Level |
| 3 | Heavy Hitter |
| 4 | Cosmic Threat |
| 5 | Secret Wars |

Band names are display strings only, renameable in `DifficultyTuning`.

## Response

`difficulty` is always present on a setup: `{ "rating": 4, "band": "Cosmic Threat", "reasons": ["...", "..."] }`, with up
to three reasons about the opposition followed by up to three about the Hero Deck, in plain words. The web app shows it when present and shows nothing when an older API leaves it out.
