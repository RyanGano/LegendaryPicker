import { useEffect, useRef, useState } from 'react'
import { getBoxes, getSetup, ping, type Box, type Setup } from './api/setupApi.ts'
import { rulesetTerms } from './rulesetTerms.ts'
import { SetupChecklist } from './SetupChecklist.tsx'
import { SetupSkeleton } from './SetupSkeleton.tsx'
import { SetupSummary } from './SetupSummary.tsx'

const PLAYER_COUNTS = [1, 2, 3, 4, 5]

// The two-column layout in index.css, where the result's heading sits in the rail under the controls.
const TWO_COLUMNS = '(width >= 56.25rem)'

// A sleeping API answers slowly; past this point the loading state says so.
const WAKE_UP_NOTICE_MS = 3_000

const PLAYER_COUNT_KEY = 'legendaryPicker.playerCount'
// Every box the player included, base games and expansions alike.
const BOXES_KEY = 'legendaryPicker.boxes'
// The expansions a returning player included before base games became picks (#71). The core box was
// always in the setup then, so it joins them when they move to BOXES_KEY.
const OLD_EXPANSIONS_KEY = 'legendaryPicker.expansions'
// What a first visit includes: the First Edition core box, one base game.
const DEFAULT_BOXES = ['core']

// Source R in LegendaryPickerService/Data/Boxes/core.json: the archived First Edition rulebook.
const RULEBOOK_URL =
  'https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf'
// Source VIL in LegendaryPickerService/Data/Boxes/villains.json: the Legendary: Villains rulebook.
const VILLAINS_RULEBOOK_URL = 'https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf'
const REPO_URL = 'https://github.com/RyanGano/LegendaryPicker'

// Storage is a convenience: private browsing or blocked site data must not break the page.
function loadPlayerCount(): number | null {
  try {
    const stored = Number(localStorage.getItem(PLAYER_COUNT_KEY))
    return PLAYER_COUNTS.includes(stored) ? stored : null
  } catch {
    return null
  }
}

function savePlayerCount(players: number) {
  try {
    localStorage.setItem(PLAYER_COUNT_KEY, String(players))
  } catch {
    // Remembering the count is optional.
  }
}

function loadBoxes(): string[] {
  try {
    migrateExpansions()
    return readIds(BOXES_KEY) ?? DEFAULT_BOXES
  } catch {
    return DEFAULT_BOXES
  }
}

// Moves a returning player's expansions from the old key to the new one once, with the core box they
// always had, then drops the old key. Picks already under the new key win.
function migrateExpansions() {
  const expansions = readIds(OLD_EXPANSIONS_KEY)
  if (expansions && localStorage.getItem(BOXES_KEY) === null) {
    saveBoxes([...DEFAULT_BOXES, ...expansions.filter((id) => !DEFAULT_BOXES.includes(id))])
  }
  localStorage.removeItem(OLD_EXPANSIONS_KEY)
}

// A stored list of box ids, or null when there is none or it isn't a list.
function readIds(key: string): string[] | null {
  try {
    const stored: unknown = JSON.parse(localStorage.getItem(key) ?? 'null')
    return Array.isArray(stored) ? stored.filter((id): id is string => typeof id === 'string') : null
  } catch {
    return null
  }
}

function saveBoxes(ids: string[]) {
  try {
    localStorage.setItem(BOXES_KEY, JSON.stringify(ids))
  } catch {
    // Remembering the boxes is optional.
  }
}

type Status =
  | { kind: 'idle' }
  | { kind: 'loading'; players: number }
  | { kind: 'result'; setup: Setup }
  | { kind: 'noEligibleScheme'; message: string }
  | { kind: 'error' }

// What the live region reads out for each state. The loading and result lines are short on purpose:
// the checklist itself is read once the player moves into it, starting from the focused heading.
function announcement(status: Status, showWakeUpNotice: boolean) {
  const playerCount = (players: number) => (players === 1 ? '1 player' : `${players} players`)

  switch (status.kind) {
    case 'idle':
      return ''
    case 'loading':
      return `Drawing a setup for ${playerCount(status.players)}…${
        showWakeUpNotice ? ' The server may be waking up. This can take up to 30 seconds.' : ''
      }`
    case 'result': {
      const terms = rulesetTerms(status.setup)
      return `Setup for ${playerCount(status.setup.players)} ready. ${terms.scheme}: ${status.setup.scheme.name}. ${terms.mastermind}: ${status.setup.mastermind.name}.`
    }
    case 'noEligibleScheme':
      return `${status.message} Pick another player count.`
    case 'error':
      return "Couldn't get a setup. Check your connection and try again."
  }
}

function App() {
  const [players, setPlayers] = useState<number | null>(loadPlayerCount)
  const [boxes, setBoxes] = useState<Box[]>([])
  const [included, setIncluded] = useState<string[]>(loadBoxes)
  const [status, setStatus] = useState<Status>({ kind: 'idle' })
  const [showWakeUpNotice, setShowWakeUpNotice] = useState(false)

  // Wake the API while the player is still choosing a count. Strict mode may repeat this harmlessly.
  useEffect(() => {
    void ping()
  }, [])

  // Without the box list there is nothing to pick, and a setup uses the core box alone.
  // Generate waits for this request, so a draw made while a cold start is still answering
  // includes the remembered boxes.
  const boxesRequest = useRef<Promise<Box[]>>(Promise.resolve([]))
  useEffect(() => {
    const request = getBoxes().catch((): Box[] => [])
    boxesRequest.current = request
    void request.then(setBoxes)
  }, [])

  const loading = status.kind === 'loading'

  // A new setup lands below the controls, so bring its heading into view and give it focus. In two
  // columns the setup starts at the top of both, so scroll there instead: scrolling the heading to
  // the top would scroll the controls out of the rail.
  const resultHeading = useRef<HTMLHeadingElement>(null)
  useEffect(() => {
    if (status.kind !== 'result') return
    const behavior = window.matchMedia('(prefers-reduced-motion: reduce)').matches ? 'auto' : 'smooth'
    resultHeading.current?.focus({ preventScroll: true })
    if (window.matchMedia(TWO_COLUMNS).matches) window.scrollTo({ top: 0, behavior })
    else resultHeading.current?.scrollIntoView({ behavior, block: 'start' })
  }, [status])

  useEffect(() => {
    if (!loading) return
    const timer = setTimeout(() => setShowWakeUpNotice(true), WAKE_UP_NOTICE_MS)
    return () => {
      clearTimeout(timer)
      setShowWakeUpNotice(false)
    }
  }, [loading])

  function choosePlayers(count: number) {
    setPlayers(count)
    savePlayerCount(count)
  }

  function toggleBox(id: string) {
    const next = included.includes(id) ? included.filter((other) => other !== id) : [...included, id]
    setIncluded(next)
    saveBoxes(next)
  }

  // Stored ids the API no longer lists are ignored.
  const isIncluded = (box: Box) => included.includes(box.id)
  // A setup takes its rules from a base game, so it needs at least one. Until the box list loads
  // there is nothing to check, and the API decides.
  const needsBaseGame = boxes.length > 0 && !boxes.some((box) => box.baseGame && isIncluded(box))
  // Boxes of different rulesets, such as the core box and Villains, can't be combined yet.
  const mixesRulesets = new Set(boxes.filter(isIncluded).map((box) => box.ruleset ?? 'firstEdition')).size > 1
  const blocked = needsBaseGame || mixesRulesets
  // Base games first, then expansions, each in the API's order.
  const listedBoxes = [...boxes.filter((box) => box.baseGame), ...boxes.filter((box) => !box.baseGame)]

  // Generate, Generate another and Retry all draw for the selected count and boxes.
  async function generate() {
    if (players === null) return
    setStatus({ kind: 'loading', players })
    try {
      const available = await boxesRequest.current
      const drawn = available.filter(isIncluded)
      // A draw started before the box list loaded may turn out to have no base game, or boxes that can't be
      // combined; the picker now says so.
      const rulesets = new Set(drawn.map((box) => box.ruleset ?? 'firstEdition'))
      if (available.length > 0 && (!drawn.some((box) => box.baseGame) || rulesets.size > 1)) {
        setStatus({ kind: 'idle' })
        return
      }
      const response = await getSetup(players, drawn.map((box) => box.id))
      setStatus(
        response.kind === 'setup'
          ? { kind: 'result', setup: response }
          : { kind: 'noEligibleScheme', message: response.message },
      )
    } catch {
      setStatus({ kind: 'error' })
    }
  }

  return (
    <div className="app">
      <header className="site-header">
        <img className="mark" src={`${import.meta.env.BASE_URL}favicon.svg`} alt="" width="48" height="48" />
        <div>
          <h1 className="wordmark">
            Legendary <span>Picker</span>
          </h1>
          <p className="tagline">A random legal setup for Marvel Legendary First Edition, Villains and their expansions.</p>
        </div>
      </header>

      <main className="content">
        {/* The controls and the drawn cards. On a wide screen they stay in view beside the checklist. */}
        <div className="rail">
          <section className="controls">
            <h2 id="players-label">Players</h2>
            <div className="player-counts" role="group" aria-labelledby="players-label">
              {PLAYER_COUNTS.map((count) => (
                <button
                  key={count}
                  type="button"
                  className="player-count"
                  aria-pressed={players === count}
                  disabled={loading}
                  onClick={() => choosePlayers(count)}
                >
                  {count}
                </button>
              ))}
            </div>
            {boxes.length > 0 && (
              <>
                <h2 id="boxes-label">Boxes</h2>
                <div className="box-options" role="group" aria-labelledby="boxes-label">
                  {listedBoxes.map((box) => (
                    <label key={box.id} className="box-option">
                      <input
                        type="checkbox"
                        checked={isIncluded(box)}
                        disabled={loading}
                        onChange={() => toggleBox(box.id)}
                      />
                      <span className="box-name">{box.name}</span>
                      {box.baseGame && <> <span className="detail">Base game</span></>}
                    </label>
                  ))}
                </div>
                {needsBaseGame && <p className="box-hint">Pick a base game to draw a setup.</p>}
                {!needsBaseGame && mixesRulesets && (
                  <p className="box-hint">
                    The ticked boxes follow different rules, First Edition and Villainous, which can't be combined yet.
                  </p>
                )}
              </>
            )}
            <button
              type="button"
              className="primary"
              disabled={players === null || loading || blocked}
              onClick={generate}
            >
              Generate
            </button>
            {status.kind === 'idle' && (
              <p className="intro">
                Get a random legal setup and a checklist for laying it out, following the official rules.
              </p>
            )}
          </section>
          {status.kind === 'result' && <SetupSummary setup={status.setup} headingRef={resultHeading} />}
        </div>

        {/* Announces each state change. The status below is not live, so a new checklist isn't read
            out in full. */}
        <p className="visually-hidden" aria-live="polite">
          {announcement(status, showWakeUpNotice)}
        </p>

        <section className="status">
          {status.kind === 'loading' && (
            <>
              <p className="loading-line">Drawing a setup…</p>
              {showWakeUpNotice && (
                <p className="notice">The server may be waking up. This can take up to 30 seconds.</p>
              )}
              <SetupSkeleton />
            </>
          )}
          {status.kind === 'result' && <SetupChecklist setup={status.setup} />}
          {status.kind === 'noEligibleScheme' && (
            <div className="state-card scheme">
              <p>{status.message}</p>
              <p className="state-prompt">Pick another player count.</p>
            </div>
          )}
          {status.kind === 'error' && (
            <div className="state-card">
              <p>Couldn't get a setup. Check your connection and try again.</p>
              <button type="button" className="primary" onClick={generate}>
                Retry
              </button>
            </div>
          )}
        </section>

        {/* Outside the status so it can stick while the drawn cards above the checklist scroll by. */}
        {status.kind === 'result' && (
          <div className="action-bar" role="group" aria-label="Setup actions">
            <p className="action-players">{players === 1 ? '1 player' : `${players} players`}</p>
            <button type="button" className="primary" onClick={generate}>
              Generate another
            </button>
          </div>
        )}
      </main>

      <footer className="site-footer">
        <p>A fan-made tool, not affiliated with or endorsed by Marvel or Upper Deck.</p>
        <p>
          Setup rules follow the{' '}
          <a href={RULEBOOK_URL} target="_blank" rel="noreferrer">
            First Edition rulebook
          </a>
          , the{' '}
          <a href={VILLAINS_RULEBOOK_URL} target="_blank" rel="noreferrer">
            Villains rulebook
          </a>{' '}
          and official clarifications.
        </p>
        <p>
          <a className="footer-link" href={REPO_URL} target="_blank" rel="noreferrer">
            Source on GitHub
          </a>
        </p>
      </footer>
    </div>
  )
}

export default App
