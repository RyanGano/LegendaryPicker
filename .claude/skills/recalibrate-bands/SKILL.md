---
name: recalibrate-bands
description: Read the playtest ratings on issue #336, summarise how setups felt against the bands they were shown, and propose DifficultyTuning / ComplexityTuning band changes as a PR by the #281 method. Use for /recalibrate-bands, or when the owner asks to re-tune the difficulty or complexity bands from playtests.
---

# Recalibrate the bands

Played games, not guessed reference setups, are the evidence (#335). Read `Docs/Difficulty/README.md` first, above all
*Calibration (#268)*, *Re-calibration with every box (#281)* and *Relative to the player count (#334)*: the method and
the constants are there.

## 1. Read the ratings

```bash
gh issue view 336 --json comments --jq '.comments[] | .url + "\n" + .body + "\n---"'
```

Keep only comments in the #336 format (`code:`, `players:`, `shown:`, `felt:`). Skip and list any that are not.

## 2. Reload each code

For each code, `GET <api>/api/setup/<code>` (see the `record-rating` skill for choosing the deployed or a local API;
for this skill run it locally from the branch, `dotnet run --project LegendaryPickerService`, so later steps read the
new tuning). Record the rating number and band now shown. A 409 means the data changed and the code is gone: drop it
and say so. A shown band that differs from the comment's means the bands already moved since that game: use the
current rating, and note it.

## 3. Summarise felt against shown

A table per rating (difficulty, complexity): one row per game, with players, boxes, shown band, felt. Then the counts:
how many felt easier, about right, harder, by shown band and by player count.

Turn felt into the rating it implies: the shown rating plus `much easier` -2, `easier` -1, `about right` 0, `harder` +1,
`much harder` +2, clamped to 1..5.

**Weak evidence.** Fewer than about 10 games for a rating, or fewer than 3 in any band or player count you want to
move, is weak: say so in the summary and in the PR. With weak evidence, propose at most one small change, or none,
and say what more games would settle it. Never change bands without showing the games that call for it.

## 4. Fit, the #281 way

- **No sweeps.** No loops over boxes, player counts, Schemes, Masterminds or random patterns (`CLAUDE.md` → Tests).
- **At most 4 seeds** (`new Random(1..4)`) for any check draws, and only the selections the change touches.
- Fit offline: for each rated game read its raw score and opposition load once (an uncommitted scratch test that
  reloads the code with `SetupCodes.Load` and evaluates it the way `SetupResponse.RateHeroDeck` does), then choose
  `BandStarts` (or `LoadEasy`, `LoadHard`, `OppositionWeight`; for complexity `PointsEasy`, `PointsHard` and `BandStarts`) so the
  felt-implied ratings fit better. No new draws for the fit.
- Keep every `DifficultyCalibrationTests` reference setup in its band. If a change moves one, reject it or say why
  the played evidence outweighs it, and update that test only with the owner's agreement.
- Prefer the smallest change: band starts before the load scale, one constant before several.

## 5. Open the PR

Change `DifficultyTuning.Default` (`LegendaryPickerService/Setup/DifficultyScorer.cs`) or `ComplexityTuning.Default`
(`ComplexityScorer.cs`), run `dotnet test`, and add a section to `Docs/Difficulty/README.md` in the shape of the #281
one:

- the games used (count, by player count and box set), with links to their #336 comments, and how weak they are;
- what changed and why, from the summary;
- before and after: each rated game's shown rating against its felt-implied rating, and the 4-seed check draws (the
  #281 tables);
- what to watch.

Quote the before and after in the PR body too. Reference `#336` (never close it). If nothing should change, say so and
open no PR.
