# Difficulty data: draft and prototype (#263)

Working notes, not committed rules. Tags are identifiers only; no card text is stored.
Card facts (class, cost, effect) were read from the owner's LegendaryAssistant data
(card-text fallback) and are authored here as tags, never as text.

## What the prototype showed

Three 3-Hero decks (42 cards each, copies 5/5/3/1 per Hero) against two oppositions that
share the Legacy Virus Scheme:

- **A** = Cyclops, Wolverine, Rogue (X-Men; classes Strength, Ranged, Instinct, Covert; no Tech)
- **B** = Hulk, Captain America, Black Widow (Avengers; has Tech copies)
- **C** = Cyclops, Black Widow, Hulk (mixed)
- **X** = Magneto + Brotherhood + Legacy Virus (X-Men checks, Tech check)
- **Y** = Dr. Doom + Spider-Foes + Legacy Virus (Tech checks, Covert gate)

| Deck | vs X (raw difficulty) | vs Y |
|---|---|---|
| A | 18.8 | 21.4 |
| B | 27.2 | 11.7 |
| C | 19.6 | 15.7 |

The same deck is among the easiest against one opposition and the hardest against the other.
That is the point of the feature, and it only works because copies and per-card classes are
counted (B has 8 Tech copies of 42; A has none).

## Findings that change the plan

1. **`wants` must be measured against the other cards**, not the card's own Hero. Counted
   naively, a Hero's own class cards satisfy its wants and the engine score barely moves
   (0.89 to 0.97 across all three decks).
2. **Opposition effects are "checks".** "Reveal an X-Men Hero or gain a Wound" and "can't
   defeat unless you have a Covert Hero" are one shape: `requires` a team or class, and a
   named consequence on failure. The scorer turns the share of that team or class in the
   Hero Deck into the chance a player can show it, and so into how often the consequence
   lands. This replaces the vaguer `punishes` tag.
3. **Starting decks dilute everything.** 12 of a player's first cards have no team or class.
   The prototype assumed 60% of a mid-game deck comes from the Hero Deck; this constant is a
   tuning knob.
4. **Counts of occurrences come from data we already have** (Twist count, Master Strikes,
   Ambush and Escape on the cards), so opposition tags don't need a count.
5. **Hero drawbacks matter** (Cyclops must discard to play; Crazed Rampage gives a Wound).
   Tag them `self-cost`.
6. **Diversity cards exist** (Captain America counts distinct classes). One extra want kind.

## Sample data

### Hero cards (per card: copies, class, cost, wants, answers)

```json
{
  "hero": "core_hero_cyclops",
  "cards": [
    { "name": "Determination",   "copies": 5, "class": "strength", "cost": 2, "economy": "recruit", "selfCost": ["discard"] },
    { "name": "Optic Blast",     "copies": 5, "class": "ranged",   "cost": 3, "economy": "attack",  "selfCost": ["discard"] },
    { "name": "Unending Energy", "copies": 3, "class": "ranged",   "cost": 6, "economy": "attack",  "answers": [["hand", 2]] },
    { "name": "X-Men United",    "copies": 1, "class": "ranged",   "cost": 8, "economy": "attack",  "wants": [["team", "x-men", 3]] }
  ]
}
{
  "hero": "core_hero_captain-america",
  "cards": [
    { "name": "Avengers Assemble",      "copies": 5, "class": "instinct", "cost": 3, "economy": "recruit", "wants": [["diversity", "class", 2]] },
    { "name": "Perfect Teamwork",       "copies": 5, "class": "strength", "cost": 4, "economy": "attack",  "wants": [["diversity", "class", 2]] },
    { "name": "Diving Block",           "copies": 3, "class": "tech",     "cost": 6, "economy": "attack",  "answers": [["wounds", 2]] },
    { "name": "A Day Unlike Any Other", "copies": 1, "class": "covert",   "cost": 7, "economy": "attack",  "wants": [["team", "avengers", 3]] }
  ]
}
```

### Opposition (checks and inflicts)

```json
{
  "mastermind": "core_mastermind_magneto",
  "masterStrike": { "inflicts": [["hand-disruption", 2]], "check": { "requires": ["team", "x-men"], "onFail": "hand-disruption" } }
}
{
  "villainGroup": "core_villain_brotherhood",
  "cards": [
    { "name": "Blob",       "gate": ["team", "x-men"], "severity": 2 },
    { "name": "Sabretooth", "check": { "requires": ["team", "x-men"], "onFail": "wounds" }, "severity": 2, "on": ["fight", "escape"] },
    { "name": "Mystique",   "inflicts": [["escape-pressure", 2]], "on": ["escape"] },
    { "name": "Juggernaut", "inflicts": [["deck-thinning", 3]], "on": ["ambush", "escape"] }
  ]
}
{
  "scheme": "core_scheme_legacy-virus",
  "twist": { "check": { "requires": ["class", "tech"], "onFail": "wounds" }, "severity": 2 },
  "amplifies": ["wounds"]
}
```

Prototype scorer: `chance to show X = 1 - (1 - 0.6 * share_of_X)^6`; each check adds
`occurrences x severity x (1 - chance)`; `answers-<axis>` cuts that axis by up to 50%; engine
strength scales the total. All constants are guesses to be calibrated once on a few real
setups, not swept.
