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

## What is built (#266)

What the Heroes want and answer. Format version 3 (now 4, below): Hero cards gain optional `wants`, `answers`,
`selfCost` and `draw`, and every core Hero card is tagged. `Setup/DifficultyScorer.cs` rates how well the Hero Deck meets
its own wants, lets its answers cut the opposition's load, charges self costs, and names the biggest Hero-side causes in
"Why this rating". Slice #267's weighting reads the same `Expected`, so a target draw sees the new terms too.

## What is built (#267)

`GET /api/setup?difficulty=1..5` (anything else is a 400) weights the draw toward that rating; the format is unchanged.
`SetupGenerator.Generate` takes the target, and `Setup/DifficultyWeights.cs` does the weighting with the scorer's
`Expected` (a continuous rating, n - 0.5 to n + 0.5 across band n).

- Order is unchanged: Scheme, Mastermind, open Villain and Henchman Groups, then the Hero Deck's Heroes. Each of these is
  one weighted pick among the options the rules left, weight `exp(-|expected - target| / Temperature)`. Draws outside the
  Hero Deck (outside Heroes, set-aside Masterminds, outside Henchmen, a group an Always Leads or a Scheme requires) stay uniform.
- An option's expected rating is the setup so far plus the option plus the pool-average Hero Deck for the Heroes not yet
  drawn (each pool Hero counted at an equal share of the open slots). The Villain Deck's counts are not known yet, so
  `EstimatedMasterStrikes`, `EstimatedTwists` and `EstimatedHenchmanCards` stand in. Early draws therefore see only part
  of the opposition and rate low in absolute terms; only their order against each other matters, and calibration (#268)
  can revisit it.
- With a target, each weighted draw takes a number below `WeightResolution` (1000) from the random source and walks the
  cumulative weights. Without one nothing changes, draw for draw.
- An unreachable target returns the closest legal draw and its real rating, never an error. The response's `difficulty`
  gains `target` (omitted when none was asked for).
- `Temperature` starts at 0.5 (a Hero spread of about 1.4 rating points across the core box's Heroes). Watch for the same
  few Masterminds or Heroes at an extreme target and raise it if so.
- On four fixed seeds in the core box, 2 players, `difficulty=1` drew ratings 3, 4, 4, 4 and `difficulty=5` drew 5, 4, 4,
  5; with no target 4, 5, 4, 4. The core box's opposition keeps most draws in the top bands (see Scorer v1), which
  calibration (#268) addressed.

## Calibration (#268)

Fitted once, on the core box, to eight reference setups (`DifficultyCalibrationTests`). The expected bands are proposed
from the cards' tags and game knowledge, not owner-confirmed; the owner is asked to confirm them in the PR.

The setups are three Hero decks against three oppositions, 8 of the 9 pairings. The decks: five cheap Avengers that
share a team (Black Widow, Iron Man, Hawkeye, Hulk, Spider-Man); a mixed deck (Storm, Wolverine, Captain America, Emma
Frost, Spider-Man); and five scattered, costly Heroes (Nick Fury, Cyclops, Rogue, Thor, Deadpool). The oppositions are
by the tagged load of the Mastermind, Scheme and groups against the mixed deck: light (Loki, Portals to the Dark
Dimension, Enemies of Asgard, Masters of Evil, Sentinels), middle (Dr. Doom, Negative Zone Prison Breakout, HYDRA,
Spider-Foes, Sentinels) and heavy (Red Skull, Replace Earth's Leaders with Killbots, Skrulls, Brotherhood, Sentinels),
all at 2 players with 5 Master Strikes, 8 Twists and 10 Henchman cards.

| Deck | Light | Middle | Heavy |
|---|---|---|---|
| Cheap, one team | 1 | 2 | 3 |
| Mixed | 2 | 3 | 4 |
| Scattered, costly | not used | 4 | 5 |

What was tuned, once:

- `LoadEasy` 4 to 15 and `LoadHard` 24 to 55. Every core opposition already saturated the old scale (loads of 20 to 53),
  so the opposition term could not tell a light opposition from a heavy one.
- `OppositionWeight` 0.5 to 0.4. At 0.5 a cheap one-team deck against the heavy opposition rated above a scattered,
  costly deck against the middle one; the Hero Deck needs more say.
- `BandStarts` from 0.2, 0.4, 0.6, 0.8 to 0.28, 0.365, 0.58, 0.74. The reference setups' raw scores run from 0.22 to 0.86,
  so the bands are no longer equal: the first is narrower and the top is wider.
- `DifficultyScorer.Expected` now follows `BandStarts`: a band runs from rating n - 0.5 at its start to n + 0.5 at its
  end, which equals 0.5 + raw * 5 for equal bands, so a target draw weighs options by the same bands `Score` uses.
- Deadpool's two self costs are untagged: Do-Over's discard and Random Acts of Unkindness's Wound are optional effects,
  not a cost of playing the card. The two Cyclops commons (a discard to play) and Crazed Rampage's Wound stay.

Before and after, the core box, 2 players:

| Reference setup | Expected | Before | After |
|---|---|---|---|
| Cheap one-team deck, light | 1 | 4 | 1 |
| Mixed deck, light | 2 | 4 | 2 |
| Cheap one-team deck, middle | 2 | 4 | 2 |
| Mixed deck, middle | 3 | 4 | 3 |
| Scattered costly deck, middle | 4 | 5 | 4 |
| Cheap one-team deck, heavy | 3 | 4 | 3 |
| Mixed deck, heavy | 4 | 4 | 4 |
| Scattered costly deck, heavy | 5 | 5 | 5 |

Four fixed seeds (the `DifficultyTargetTests` pattern), ratings drawn: no target 4, 5, 4, 4 before and 3, 4, 3, 3 after;
`difficulty=1` 3, 3, 4, 4 before and 1, 2, 3, 4 after; `difficulty=5` 5, 4, 4, 5 before and 3, 4, 4, 5 after. Both
extremes are reachable, 1 at target 1 and 5 at target 5, but a target of 5 mostly lands on 4: only a scattered, costly
deck against a heavy opposition reaches 5, and the weighted draw has to find all of it.

Remaining disagreements and what to watch:

- Two constants do most of the work: `LoadEasy` and `LoadHard` set where the opposition's weight bites, and the mean cost
  weight sets the Hero Deck's spread (0.29 to 0.8 raw across the three decks). Neither was searched; both were set from
  the printed loads and raw scores of the reference setups.
- The Scheme dominates the opposition's load (9 to 26 per Scheme against 0.5 to 7 per Mastermind and 1 to 6 per
  group), because its Twists fire 8 times. The Mastermind barely moves a rating. Left for the Mastermind's tactics and
  Fight effects to be tagged more fully.
- Legacy Virus and the Cosmic Cube rate as light Schemes (their checks and wounds are often met by a Tech or
  Strength-heavy deck); in play they are widely held to be hard. The tags measure what the data says, so the rating
  follows it.
- The engine term (`EngineUnmet*`, `EngineWeight`) is not changed: most core wants fall on a class and five Heroes
  usually carry all five, so it rarely moves a rating. Unchanged on purpose; a later box's wants will test it.
- Only the core box is tagged. A box with no difficulty file rates 3.

## What is built (#270)

A second rating beside the difficulty: complexity, how much there is to learn and remember. It is independent: it lives in
`Setup/ComplexityScorer.cs` and `ComplexityTuning`, reads no difficulty tag, and the difficulty reads none of its weights.
There is no data file; it reads what the drawn cards already carry.

| Measure | Points |
|---|---|
| distinct keyword terms of an expansion box (a base game's own keywords are baseline) | 1 each |
| stacks beyond the baseline (Wounds and Officers): Sidekicks, Bindings, Shards, Horrors, Madame HYDRA, New Recruits | 2 each |
| rulesets mixed beyond the first | 3 |
| stand-in parts (`otherRuleset`) | 1.5 each |
| boxes beyond the first that supply a drawn card | 0.5 each |
| setup steps of the Scheme and Mastermind | 0.75 each |

The points map to 0..1 between `PointsEasy` (4) and `PointsHard` (20) and fall into five equal bands: Origin Story, Team-Up,
Crossover, Multiverse, Event Series. The response's `complexity` is `{ rating, band, reasons, target? }`, the reasons
naming the biggest contributors first ("Adds Sidekicks and Shards", "11 keywords to know: ..."). A core-only setup rates 1.

`GET /api/setup?complexity=1..5` (anything else is a 400) adds `|expected complexity - target|` to the difficulty's
distance in each weighted draw (`DifficultyWeights.Distance`), so both targets can be asked for at once and neither is a
filter. The expected complexity is the cards drawn so far plus the pool's average extra points for each Hero slot still
open, mapped to a continuous rating that rounds to the band (1 at no points, k + 0.5 where band k + 1 starts, 5 at
`PointsHard`), so a target of 2 leans toward Team-Up setups. Stacks a stand-in replaces count as the stand-in only. Not built: the count of Solo play rules; calibration of the constants is a later pass.

## Data format (schemaVersion 4)

One file per box, `LegendaryPickerService/Data/Difficulty/<box-id>.json`. A box with no file is valid and rates neutral
(3). Tags and numbers only, never card text.

```json
{
  "schemaVersion": 4,
  "box": "core",
  "source": "Q",
  "heroes": [
    { "hero": "core_hero_cyclops",
      "cards": [
        { "name": "Determination", "copies": 5, "class": "strength", "cost": 2, "economy": "recruit", "selfCost": ["discard"] },
        { "name": "X-Men United", "copies": 1, "class": "ranged", "cost": 8, "economy": "attack", "wants": [["team", "x-men", 3]] } ] }
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
        { "name": "Juggernaut", "copies": 2, "inflicts": [["deck-thinning", 2]], "on": ["ambush", "escape"] },
        { "name": "Mystique", "copies": 2, "inflicts": [["bystander-pressure", 1]], "on": ["ambush"] },
        { "name": "Mystique", "copies": 2, "inflicts": [["wounds", 1]], "on": ["escape"] } ] }
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
- A tag's effects share one `on`. A card whose effects fire at different times is listed once per timing (#292, the
  change from version 3, where one `on` covered every effect of a card): same `name` and `copies`, each entry with its own
  effects and `on`, as Mystique above. The scorer reads each entry as its own part, so an effect counts only at its own
  timing; the loader rejects entries of one card that disagree on `copies`.
- Axes: `wounds`, `bystander-pressure`, `hq-disruption`, `hand-disruption`, `attack-inflation`, `escape-pressure`,
  `deck-thinning`, `twist-density`. A Scheme's `amplifies` lists axes its loss condition makes worse.
- A team value is the `<value>` of a team term `<box>_term_<value>` of any loaded box, since a box's Heroes can belong
  to a team another box defines (#269); a class value is a Hero class. A Villain card says its
  `copies`; a Henchman card does not (the Villain Deck's henchman cards are split over the drawn groups).
- The loader rejects an unknown axis, timing or kind, a part the box does not declare, a team no loaded box has, a
  tag with no effect, a severity outside 1..3, a timing that does not fit the card, and a card with no `on`.

- `source` is a key in the box file's `sources` for where the cards were read.
- `copies` is how many of the Hero's 14 cards are that named card; the loader does not check they sum to 14 (#140).
- `class` is `covert`, `instinct`, `ranged`, `strength` or `tech`; a multiclass card lists an array.
- `cost` is the printed recruit cost; `economy` is `recruit`, `attack`, `both` or `none`.
- `wants` is `[[kind, value, strength 1..3], ...]`, evaluated against the other drawn cards (a Hero's own cards never meet
  its wants): `team` (a team of any loaded box), `class`, `keyword` (a keyword term id of the box), `stack` (wounds, bystanders,
  shards, sidekicks, officers, bindings), `board` (villains-in-city, heroes-in-hq, ko-pile), `diversity` (value `class`),
  `draw` (value `extra-cards`). `answers` is `[[axis, strength 1..3], ...]` with axis `wounds`, `bystanders`, `hq`,
  `villain-flow`, `hand`, `attack-inflation` or `escape`. `selfCost` lists `discard`, `wound`, `ko-hand`; `draw` is the
  extra cards (0..3) the card draws outright. Effects that fit no axis are listed in the box's research record under
  "Difficulty: no axis".
- The loader also rejects an empty `wants`, `answers` or `selfCost`, an unknown want kind, answer axis or self cost, a
  want value its kind cannot name (a team the box lacks, an unknown stack), a strength outside 1..3 and a `draw` outside 0..3.
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
starts and the band names live in one record, `DifficultyTuning`. Calibration (#268) set them (see Calibration). The first
band starts at raw 0 and the others at 0.28, 0.365, 0.58 and 0.74.

## Scorer v1: the opposition

Each tag adds a load: occurrences * severity. Occurrences are derived from the draw, not tagged: a Master Strike tag counts
the Master Strikes in the Villain Deck, a Twist tag its Twists, a card its copies, and each is scaled by what the game
sees (`OccurrenceShare`, 0.7) and by when it fires (Ambush, Strike and Twist 1.0; Fight and Escape 0.5, since the players
choose whom to fight). A check or gate is multiplied by the chance the players cannot show it,
`1 - chanceToShow` with `chanceToShow = 1 - (1 - A * share)^H`, where `share` is the Hero Deck's copy-weighted share of the
team or class, `A` = 0.6 (`HeroDeckShare`, the starting deck dilutes the rest) and `H` = 6 (`HandSize`). Axes the Scheme
amplifies count 1.5 times. The total load maps to 0..1 (`LoadEasy` 15, `LoadHard` 55) and counts 0.4 of the raw score
against the Hero Deck's 0.6 (`OppositionWeight`). A setup with none of its opposition tagged rates on the Hero Deck alone.

These were 4, 24 and half and half until calibration (#268): the opposition load saturated that scale on the core box.

## Scorer v2: the Heroes' engine and answers

Wants are scored against the other drawn cards, copy-weighted. A `team` or `class` want is met by
`min(1, share / target)`, with `share` the other cards' share having that team or class and the targets
`TeamWantTarget` and `ClassWantTarget` (0.25); `diversity` counts each class present at `DiversityClassShare` (0.10) of
the other cards and is met at `DiversityTarget` (4) classes. A `stack` want for Wounds is met by the opposition's Wounds
load (`StackWantLoad`, 3); keyword, board, draw and the other stacks are accepted by the loader but not scored yet and
count as met. Each want adds `copies * strength * (1 - met)` to the unmet total; per deck card that maps to 0..1
(`EngineUnmetEasy` 0.1, `EngineUnmetHard` 0.6) and joins the Hero Deck terms at weight `EngineWeight` (0.25). A deck with
no wants tagged leaves the term out, as does a deck with no `draw` for the extra-cards term (`DrawWeight` 0.10, easy at
`DrawMeanEasy` 0.5 cards per card).

Answers cut the opposition's load. An answer maps to an opposition axis (`wounds` to Wounds, `bystanders` to Bystanders
carried off, `hq` to HQ disruption, `hand` to hand disruption, `attack-inflation`, and `escape` and `villain-flow` to
Villains pushed toward escape). The copy-weighted `strength / 3` share cuts that axis's check and inflict load by up to
`MaxMitigation` (50%), reached at `AnswerTarget` (0.15). Deck thinning and extra Twists have no answer, and gates have no
axis, so none is cut. A card's `selfCost` entries add `SelfCostPerCopy` (0.1) each to the opposition load, only when some
opposition is tagged.

Reasons: up to two Hero-side reasons (the biggest unmet wants, the answers that saved the most load, the self cost) come
after the opposition's and before the Hero Deck measures, which are trimmed to keep the list short.

Watch for the engine term never moving: most core wants fall on a class, and five Heroes usually carry all five, so
class wants are mostly near met. Calibration (#268) left it unchanged.

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

`difficulty` is always present on a setup: `{ "rating": 4, "band": "Cosmic Threat", "reasons": ["...", "..."], "target": 1 }` (`target` only when the request had `difficulty=`), with up
to three reasons about the opposition followed by up to three about the Hero Deck, in plain words. The web app shows it when present and shows nothing when an older API leaves it out.

`GET /api/ratings` lists the band names of both ratings by rating, for the web app's Difficulty and Complexity pickers
(#271). A picked band is sent as `difficulty=` or `complexity=`; when the response's `target` differs from its `rating`, the
panel names the band asked for and says the closest legal setup was drawn.

## Per-effect timing (#292)

Format version 4. Version 3 allowed one `on` list per opposition card, so a card with an Ambush effect and an Escape
effect counted both at both. A card now appears once per timing (see the format above), and every Data/Difficulty file
moved to version 4; the service reads only version 4. 78 cards were split across the 40 box files, assigning each tagged
effect to the timing of the effect it came from, never adding a timing a card did not already list. A card kept as one
entry where its effects share their timings, where the effect is the same at each timing, or where no card text was
available to split it by (Wolverinepool, Umar, Cross' Security Detail, Demonbound Doctor Strange, Rand K'ai, S'ym,
Ultimaton, Juggernaut, Scorpion and Venom of Second Edition, Shi'ar Death Commandos). The static Attack raises
(`attack-inflation`) sit at Fight where a card lists it. Ratings fall slightly on the affected cards and nowhere else;
calibration of the bands stays with #281.
