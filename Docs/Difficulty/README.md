# Difficulty rating

How hard a drawn setup is to win, as a number 1 (easiest) to 5 and a band name. The design and the slices are in
[#263](https://github.com/RyanGano/LegendaryPicker/issues/263); this page records what is built. The rating measures how
likely the players are to lose, not how much there is to learn. Generation guarantees legality, never balance: a rating
never drops or redraws a Scheme.

## What is built (#264)

A first, Hero-Deck-only rating, end to end: `Data/Difficulty/<box-id>.json` (loaded by `Catalog/BoxCatalog`),
`Setup/DifficultyScorer.cs`, a `difficulty` block on every `GET /api/setup` setup response, and a panel on the checklist
with a "Why this rating" list. The opposition, the Heroes' wants and answers, a `difficulty=` target, calibration and the
other boxes are later slices (#265 to #271).

## Data format (schemaVersion 1)

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
  ]
}
```

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
to three reasons in plain words. The web app shows it when present and shows nothing when an older API leaves it out.
