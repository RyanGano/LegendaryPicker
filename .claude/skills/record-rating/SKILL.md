---
name: record-rating
description: Record how a played LegendaryPicker setup felt against the difficulty and complexity bands it was shown with, as one comment on the playtest ratings issue (#336). Use for /record-rating [game_code] [felt rating], or when the owner says a game they played was rated too hard, too easy or about right.
---

# Record a rating

Usage: `/record-rating [game_code] [felt rating]`, for example
`/record-rating 1-a0dul3-20000-... difficulty harder, complexity about right, note: Twists piled up fast`.

One game, one comment on issue #336. The comment holds the code and the bands, never card text: the code reloads the
setup, so the cards need not be listed.

## 1. Reload the code

The API turns a code into the setup it was shown with: `GET <api>/api/setup/<code>`.

Pick `<api>`:

- **Deployed** (default): the repository variable the Pages build uses,
  `gh variable get VITE_API_BASE_URL`. Never write the hostname into the repo or the comment.
- **Local**: `dotnet run --project LegendaryPickerService` from the repo root, then `http://localhost:5179` (the `http`
  profile, also in `LegendaryPickerApp/.env.development`). Use it when the owner says so, or the deployed API is down.

```powershell
$api = gh variable get VITE_API_BASE_URL   # or 'http://localhost:5179'
$s = Invoke-RestMethod "$api/api/setup/<code>"
$s | Select-Object code, players, @{n='boxes';e={$_.boxes -join ','}}, @{n='difficulty';e={$_.difficulty.band}}, @{n='complexity';e={$_.complexity.band}}
```

(`curl -s "$api/api/setup/<code>"` works too; read the same fields from the JSON.) The deployed API sleeps when idle,
so a first call can take a minute; `GET $api/api/health` wakes it.

Answers:

- **200**: read `players`, `boxes`, `difficulty.band` and `complexity.band`. Use the `code` it returns.
- **409**: the box data changed and the code no longer gives the same cards. Tell the owner and stop; post nothing.
- **400**: not a setup code. Ask for the code again.

## 2. Confirm the shown bands

Show the owner the players, boxes and the two shown bands, so they can check it is the game they played.

## 3. Get the felt rating

Each of difficulty and complexity, against the shown band, on one scale:
`much easier`, `easier`, `about right`, `harder`, `much harder`.

Parse it from the arguments when given ("too hard" is `harder`, "way too easy" is `much easier`; one word for both
applies to both only if the owner said so). Ask for whatever is missing or unclear, and for an optional one-line note.
Never guess a felt value.

## 4. Post one comment

Exactly the #336 format, with no card names or text in the note beyond what the owner wrote (if the note quotes card
text, ask the owner to reword it):

```
code: <code>
players: <n> · boxes: <ids, comma-separated>
shown: difficulty <band> · complexity <band>
felt: difficulty <scale value> · complexity <scale value>
note: <one line, or leave the line out>
```

Show the comment and get a yes before posting, then:

```bash
gh issue comment 336 --body-file <file>
```

Report the comment's URL. Never close #336.
