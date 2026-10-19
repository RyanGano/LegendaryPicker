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

`GET /api/setup?difficulty=1..5` (anything else is a 400) asks for a rating; the format is unchanged. The first version
weighted each draw toward it. That missed the band most of the time and was replaced by draw, then repair (#329, below).

## Draw, then repair (#329)

`SetupGenerator.Generate` takes the targets, and `Setup/RatingTargets.cs` judges a choice with the scorers the response
uses.

- The setup is drawn plainly first, so the Scheme and Mastermind are the ones an untargeted draw takes from the same
  random source, and they are never changed.
- The open picks are the Hero Deck's Heroes no card requires (fill-ins the players may choose are left alone) and the
  Villain and Henchman Groups drawn into open slots: not a required group, an Always Leads group, a substitute or a group
  from a box that isn't included.
- Each round rates every single swap of an open pick for another of its pool, with `DifficultyScorer.Measure` on the
  plan's real Master Strike, Twist and Henchman counts and, for complexity, `ComplexityMeter` on the drawn cards and the
  stacks they lay out. If some swaps land in every target band, one of them is drawn at random, so setups stay varied;
  otherwise the swap whose continuous rating comes closest is taken. The repair stops in band, when no swap comes closer,
  or after 6 rounds. With both targets the distance is the sum of the two.
- A Hero swap must keep `HeroRules.CanComplete`, so a Scheme's Hero rules still hold; a Henchman Group the Scheme draws
  outside the Villain Deck is never swapped in. Outside Heroes, set-aside Villains, other Masterminds, cards beside the
  Scheme and the stacks are drawn and built once, from the final choice.
- A target the repair doesn't reach keeps the closest choice. The response's `difficulty` and `complexity` carry
  `target`, `reached` and, only when it was not reached, a `note` ("The closest to Secret Wars the draw found with this
  Scheme is Cosmic Threat."), which never names a rule. The web app shows the note beside the band.
- Measured on 2 players, seeds 1000 onward, before and after (hits of the target band):

  | Boxes | Target | Before | After |
  |---|---|---|---|
  | core | difficulty 1 | 5/20 | 19/20 |
  | core | difficulty 5 | 3/20 | 10/20 |
  | core, Dark City, Secret Wars Vol. 1 | difficulty 1 | 2/20 | 15/20 |
  | core, Dark City, Secret Wars Vol. 1 | difficulty 5 | 6/20 | 20/20 |
  | core, Dark City, Secret Wars Vol. 1 | complexity 3 | 2/20 | 16/20 |
  | all boxes | difficulty 1 | 0/4 | 4/4 |
  | all boxes | complexity 1 | 0/4 | 0/4 (3 at band 2) |

  A targeted draw with every box ticked took about 4.6 s before and about 0.6 s after. Core at difficulty 5 stops at
  Cosmic Threat half the time: core's opposition is light once the Scheme and Mastermind are fixed. Complexity with every
  box ticked stops at 2 when the Scheme and Mastermind alone bring that much. Not built: retrying with another Mastermind.

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

The points map to 0..1 between `PointsEasy` (4) and `PointsHard` (40; 20 until #281) and fall into five equal bands: Origin Story, Team-Up,
Crossover, Multiverse, Event Series. The response's `complexity` is `{ rating, band, reasons, target? }`, the reasons
naming the biggest contributors first ("Adds Sidekicks and Shards", "11 keywords to know: ..."). A core-only setup rates 1.

`GET /api/setup?complexity=1..5` (anything else is a 400) repairs the draw toward that band as above (#329), alone or
with a difficulty, and neither target is a filter. The repair reads the points of the drawn cards as a continuous rating
that rounds to the band (1 at no points, k + 0.5 where band k + 1 starts, 5 at `PointsHard`), so it can tell which swap
comes closer. Stacks a stand-in replaces count as the stand-in only. Not built: the count of Solo play rules; calibration of the constants is a later pass.

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
band starts at raw 0 and the others at 0.29, 0.39, 0.58 and 0.74 (see Re-calibration, #281).

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

The scale is relative to the player count (#334): at a count other than 2, `LoadEasy` and `LoadHard` both move up by the
load that count's own Villain Deck adds over the 2-player one (see Relative to the player count, below).

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
calibration of the bands stays with #281 (below).

## Re-calibration with every box (#281)

Refitted once, after every box had difficulty data (#269), the per-effect format (#292) and draw-then-repair (#329)
were in.

Method: 4 seeds (`new Random(1..4)`) of untargeted draws at 1, 2, 4 and 5 players from four selections (core; core, Dark
City and Secret Wars Vol. 1; Villains, Fear Itself and X-Men; every box), 64 draws in all. Each draw's Hero-Deck raw
score and opposition load were recorded once, and `LoadEasy` (10 to 25), `LoadHard` (55 to 110), `OppositionWeight`
(0.25 to 0.5) and the band starts were fitted offline to those numbers, keeping every #268 reference setup in its band.
No new draws were made for the fit. Targeted draws (`difficulty=1` and `5`, same seeds, 2 and 4 players) were run once
before and once after.

What changed:

- Difficulty `BandStarts` 0.28, 0.365, 0.58, 0.74 to 0.29, 0.39, 0.58, 0.74. The first band is a little wider, so
  `difficulty=1` lands more often, and the second takes some of the third. Every reference setup keeps its band (raw
  0.25, 0.32, 0.32, 0.41, 0.55, 0.60, 0.64, 0.83 against starts 0.29, 0.39, 0.58, 0.74); none needed to move.
- `LoadEasy` 15, `LoadHard` 55 and `OppositionWeight` 0.4 are unchanged. The fit's best spread (`OppositionWeight` 0.45,
  starts up to about 0.80) cut the untargeted 5s from 8 to 5 of 64, but `difficulty=5` on the core box then landed 2 of
  4 times at 2 players instead of 4, and a wider `LoadHard` misplaces the reference setups. Both were rejected.
- Complexity `PointsHard` 20 to 40. With many boxes ticked a draw has about 18 to 46 points, so every such draw rated 5,
  Event Series, and `complexity=1` could rarely leave it. The bands stay five equal widths.

Before and after, ratings of the four seeds:

| Boxes | Players | Rating | Before | After |
|---|---|---|---|---|
| core | 2 | difficulty, untargeted | 3, 4, 3, 3 | 3, 4, 3, 3 |
| core | 2 | `difficulty=1` | 1, 1, 1, 2 | 1, 1, 1, 1 |
| core | 2 | `difficulty=5` | 5, 5, 5, 5 | 5, 5, 5, 5 |
| core, Dark City, Secret Wars Vol. 1 | 2 | difficulty, untargeted | 2, 3, 3, 3 | 2, 3, 2, 3 |
| core, Dark City, Secret Wars Vol. 1 | 2 | `difficulty=1` / `5` | 1, 1, 1, 2 / 4, 5, 5, 5 | same |
| every box | 2 | difficulty, untargeted | 4, 4, 4, 4 | 4, 4, 4, 4 |
| every box | 2 | `difficulty=1` / `5` | 2, 1, 1, 1 / 5, 5, 5, 5 | same |
| core | 4 | difficulty, untargeted | 3, 3, 2, 3 | 3, 3, 1, 3 |
| Villains, Fear Itself, X-Men | 4 | `difficulty=1` | 1, 1, 3, 1 | 1, 1, 2, 1 |
| every box | 4 | difficulty, untargeted | 5, 5, 4, 5 | 5, 5, 4, 5 |
| every box | 4 | `difficulty=1` / `5` | 3, 3, 1, 3 / 5, 5, 5, 5 | same |
| Villains, Fear Itself, X-Men | 2 | complexity, untargeted | 4, 4, 5, 5 | 2, 2, 3, 3 |
| every box | 2 | complexity, untargeted | 5, 5, 5, 5 | 4, 4, 3, 4 |
| every box | 2 | `complexity=1` | 1, 5, 1, 5 | 1, 3, 1, 2 |
| every box | 4 | complexity, untargeted | 5, 5, 5, 5 | 3, 5, 4, 4 |
| every box | 4 | `complexity=1` | 3, 5, 5, 5 | 2, 4, 2, 2 |

Over all 64 untargeted draws the difficulty counts by rating 1 to 5 went from 3, 4, 25, 24, 8 to 4, 4, 24, 24, 8.
Targets of 1 and 5 already land almost every time since #329, so the bands had little left to fix at 2 players, where
no untargeted multi-box draw rated 5. The 3-box set at `complexity=5` now stays at 1 (it was 2 or 3): those boxes bring
few rules, so a 5 is not in reach there.

What to watch:

- The 5s come from player count, not from the bands. Every drawn Villain Group adds its full load, so the opposition
  load with every box ticked is 30 to 49 at 2 players, 42 to 73 at 4 and 58 to 87 at 5, past `LoadHard` 55; 6 of the 8
  untargeted draws of every box at 4 and 5 players rated 5. No setting of the band starts, `LoadEasy`, `LoadHard` or
  `OppositionWeight` fixes that and keeps the core reference setups in their bands. The owner decided more players
  should not rate harder; #334 (below) makes the rating relative to the player count.
- A draw from many boxes rates 4 at 2 players because its Heroes cost more (a mean cost of about 4.0 to 4.2, near `MeanCostHard`)
  and share fewer teams. `MeanCostEasy` and `MeanCostHard` were fitted on the core box; they were left alone here.
- No reference setups from other boxes were added: their bands would be guesses for the owner to confirm, and the fit
  above did not need them.

## Relative to the player count (#334)

Owner decision (2026-10-18): the rating is relative to the player count. A 1 or 2 player setup has lower bands than a 4
or 5 player one, so the easiest 5-player setup is about a middle or hard band on the 2-player scale, and a 5-player
draw is not mostly 5s.

How: the band starts stay, and the load scale moves with the Villain Deck the player count's rules build.
`DifficultyInputs.Opposition` reads, from the first loaded base game of the rules the setup follows, how many more
Villain Groups, Henchman cards and Master Strikes that count's table gives than the 2-player row. A Scheme's own changes
to the counts are not taken off, so a Scheme that adds a group still rates harder. `DifficultyScorer` moves `LoadEasy`
and `LoadHard` up by their mean load: `LoadPerVillainGroup` 6.1, `LoadPerHenchmanCard` 0.32 and `LoadPerMasterStrike`
1.7. These were measured once from the model, not guessed: every loaded box's Villain Groups, Henchman Groups and
Masterminds' Master Strikes, each rated alone (no Scheme) against the three #268 reference decks and averaged, an untagged
one counting 0 as the scorer gives it no load. 131 of 132 Villain Groups are tagged (6.16 on average, 1.1 to 19.6); 29
of 48 Henchman Groups are (0.54 per card).

| Players | First Edition and Villains | Revised (Second Edition, What If...?) |
|---|---|---|
| 1 (Solo) | 1 group, 3 Henchman cards and, in First Edition, 1 Master Strike: -15.1 (Villains -8.3) | 1 group, 2 Henchman cards: -8.7 |
| 2 | 0 | 0 |
| 3 | 1 more group: +6.1 | +6.1 |
| 4 | 1 more group, 10 more Henchman cards: +9.3 | 2 more groups, 10 more cards: +15.4 |
| 5 | 2 more groups, 10 more Henchman cards: +15.4 | 3 more groups, 10 more cards: +21.5 |

At 2 players nothing moves, so every #268 reference setup keeps its band and every 2-player draw below is unchanged.

Complexity is unchanged. More players draw more groups, which can add a keyword or a stack, but the measured complexity
with every box ticked was already alike at 2 and 4 players (4, 4, 3, 4 and 3, 5, 4, 4 after #281), so a shift there was
not clearly needed.

Before and after, untargeted, 4 seeds (`new Random(1..4)`, the #281 method), with each draw's opposition load:

| Boxes | Players | Load | Before | After |
|---|---|---|---|---|
| core | 1 | 17 to 28 | 3, 1, 3, 1 | 4, 3, 4, 3 |
| core, Dark City, Secret Wars Vol. 1 | 1 | 15 to 29 | 1, 3, 2, 2 | 3, 4, 3, 3 |
| Villains, Fear Itself, X-Men | 1 | 13 to 34 | 3, 3, 4, 4 | 3, 3, 4, 4 |
| every box | 1 | 12 to 40 | 4, 3, 3, 4 | 4, 3, 3, 5 |
| each of the four | 2 | 22 to 62 | (#281 above) | no change |
| core | 4 | 29 to 33 | 3, 3, 1, 3 | 3, 2, 1, 2 |
| core, Dark City, Secret Wars Vol. 1 | 4 | 29 to 54 | 3, 4, 3, 5 | 2, 4, 3, 4 |
| Villains, Fear Itself, X-Men | 4 | 29 to 44 | 3, 3, 4, 4 | 2, 3, 4, 3 |
| every box | 4 | 42 to 73 | 5, 5, 4, 5 | 5, 5, 3, 5 |
| core | 5 | 33 to 42 | 4, 4, 3, 4 | 3, 3, 2, 3 |
| core, Dark City, Secret Wars Vol. 1 | 5 | 39 to 60 | 3, 4, 4, 5 | 2, 3, 3, 4 |
| Villains, Fear Itself, X-Men | 5 | 39 to 70 | 3, 4, 4, 4 | 3, 3, 4, 3 |
| every box | 5 | 58 to 87 | 4, 5, 5, 5 | 4, 4, 4, 5 |

The mean untargeted rating at 1, 2, 4 and 5 players went from 2.75, 3.31, 3.63, 4.06 to 3.50, 3.31, 3.19, 3.31, so
every count now spreads about the same way. The 5s at 4 and 5 players fell from 8 of 32 to 4.

Targeted, same seeds, `difficulty=1` / `difficulty=5`:

| Boxes | Players | Before | After |
|---|---|---|---|
| core | 1 | 1, 1, 1, 1 / 4, 3, 4, 3 | 1, 2, 1, 2 / 5, 4, 5, 4 |
| core, Dark City, Secret Wars Vol. 1 | 1 | 1, 1, 1, 1 / 4, 5, 4, 5 | 1, 1, 1, 2 / 5, 5, 5, 5 |
| every box | 1 | 1, 1, 1, 1 / 5, 5, 5, 5 | 1, 1, 1, 2 / 5, 5, 5, 5 |
| each of the four | 2 | (#281 above) | no change |
| core | 4 | 1, 1, 1, 1 / 5, 5, 5, 5 | 1, 1, 1, 1 / 4, 4, 4, 4 |
| every box | 4 | 3, 3, 1, 3 / 5, 5, 5, 5 | 1, 1, 1, 1 / 5, 5, 5, 5 |
| core | 5 | 1, 1, 1, 2 / 5, 5, 5, 5 | 1, 1, 1, 1 / 4, 4, 4, 4 |
| every box | 5 | 3, 3, 3, 3 / 5, 5, 5, 5 | 1, 1, 1, 1 / 5, 5, 5, 5 |

Both extremes stay reachable at every count: `DifficultyTargetTests` reaches 1 and 5 at 1, 4 and 5 players with core,
Dark City and Secret Wars Vol. 1, and `DifficultyCalibrationTests` rates the cheap deck against the light opposition 1
and the costly deck against the heavy one 5 with their 5-player groups.

What to watch:

- The core box's Villain Groups are lighter than the catalog's (about 4 against 6.1 on average), so a core-only game at
  4 or 5 players rates about a band lower than before, and `difficulty=5` there stopped at Cosmic Threat in all four
  seeds (it reached Secret Wars before). Secret Wars is still in reach with core alone (the costly deck against the heavy
  opposition rates 5 at 5 players), but the repair keeps the drawn Scheme and Mastermind, and these seeds' were lighter.
- Solo moves furthest, since First Edition Solo has 1 Master Strike against 5: its ratings rose about a band, and
  `difficulty=1` with core alone landed 2 of 4 times instead of 4. Whether Solo should rate like 2 players is untested in
  play.
- The three means were measured against the core reference decks. A deck that fails other boxes' checks more often meets
  more load per extra group than the shift allows, so every box at 4 players still rates 5 in 3 of 4 seeds. Re-measure
  the means when many groups are re-tagged.
