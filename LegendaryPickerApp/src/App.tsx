import { useEffect, useRef, useState } from 'react'
import { getBoxes, getSetup, ping, type Box, type Setup } from './api/setupApi.ts'
import { SetupChecklist } from './SetupChecklist.tsx'
import { SetupSkeleton } from './SetupSkeleton.tsx'

const PLAYER_COUNTS = [1, 2, 3, 4, 5]

// A sleeping API answers slowly; past this point the loading state says so.
const WAKE_UP_NOTICE_MS = 3_000

const PLAYER_COUNT_KEY = 'legendaryPicker.playerCount'
// The expansions the player included. Base games are always included, so they are not stored.
const EXPANSIONS_KEY = 'legendaryPicker.expansions'

// Source R in LegendaryPickerService/Data/Boxes/core.json: the archived First Edition rulebook.
const RULEBOOK_URL =
  'https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf'
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

function loadExpansions(): string[] {
  try {
    const stored: unknown = JSON.parse(localStorage.getItem(EXPANSIONS_KEY) ?? '[]')
    return Array.isArray(stored) ? stored.filter((id): id is string => typeof id === 'string') : []
  } catch {
    return []
  }
}

function saveExpansions(ids: string[]) {
  try {
    localStorage.setItem(EXPANSIONS_KEY, JSON.stringify(ids))
  } catch {
    // Remembering the boxes is optional.
  }
}

type Status =
  | { kind: 'idle' }
  | { kind: 'loading' }
  | { kind: 'result'; setup: Setup }
  | { kind: 'noEligibleScheme'; message: string }
  | { kind: 'error' }

function App() {
  const [players, setPlayers] = useState<number | null>(loadPlayerCount)
  const [boxes, setBoxes] = useState<Box[]>([])
  const [expansions, setExpansions] = useState<string[]>(loadExpansions)
  const [status, setStatus] = useState<Status>({ kind: 'idle' })
  const [showWakeUpNotice, setShowWakeUpNotice] = useState(false)

  // Wake the API while the player is still choosing a count. Strict mode may repeat this harmlessly.
  useEffect(() => {
    void ping()
  }, [])

  // Without the box list there is nothing to pick, and a setup uses the core box alone.
  // Generate waits for this request, so a draw made while a cold start is still answering
  // includes the remembered expansions.
  const boxesRequest = useRef<Promise<Box[]>>(Promise.resolve([]))
  useEffect(() => {
    const request = getBoxes().catch((): Box[] => [])
    boxesRequest.current = request
    void request.then(setBoxes)
  }, [])

  const loading = status.kind === 'loading'

  // A new setup lands below the controls, so bring its heading into view and give it focus.
  const resultHeading = useRef<HTMLHeadingElement>(null)
  useEffect(() => {
    if (status.kind !== 'result') return
    const reduceMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches
    resultHeading.current?.focus({ preventScroll: true })
    resultHeading.current?.scrollIntoView({ behavior: reduceMotion ? 'auto' : 'smooth', block: 'start' })
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

  function toggleExpansion(id: string) {
    const next = expansions.includes(id) ? expansions.filter((included) => included !== id) : [...expansions, id]
    setExpansions(next)
    saveExpansions(next)
  }

  const isIncluded = (box: Box) => box.baseGame || expansions.includes(box.id)

  // Generate, Generate another and Retry all draw for the selected count and boxes.
  async function generate() {
    if (players === null) return
    setStatus({ kind: 'loading' })
    try {
      const available = await boxesRequest.current
      const response = await getSetup(players, available.filter(isIncluded).map((box) => box.id))
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
          <p className="tagline">A random legal setup for the Marvel Legendary core box.</p>
        </div>
      </header>

      <main className="content">
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
                {boxes.map((box) => (
                  <label key={box.id} className="box-option">
                    <input
                      type="checkbox"
                      checked={isIncluded(box)}
                      disabled={box.baseGame || loading}
                      onChange={() => toggleExpansion(box.id)}
                    />
                    <span className="box-name">{box.name}</span>
                    {box.baseGame && <span className="detail">Always included</span>}
                  </label>
                ))}
              </div>
            </>
          )}
          <button
            type="button"
            className="primary"
            disabled={players === null || loading}
            onClick={generate}
          >
            Generate
          </button>
          {status.kind === 'idle' && (
            <p className="intro">
              Get a random legal setup and a checklist for laying it out, following the First Edition rules.
            </p>
          )}
        </section>

        <section className="status" aria-live="polite">
          {status.kind === 'loading' && (
            <>
              <p className="loading-line">Drawing a setup…</p>
              {showWakeUpNotice && (
                <p className="notice">The server may be waking up. This can take up to 30 seconds.</p>
              )}
              <SetupSkeleton />
            </>
          )}
          {status.kind === 'result' && (
            <>
              <SetupChecklist setup={status.setup} headingRef={resultHeading} />
              <div className="action-bar" role="group" aria-label="Setup actions">
                <p className="action-players">{players === 1 ? '1 player' : `${players} players`}</p>
                <button type="button" className="primary" onClick={generate}>
                  Generate another
                </button>
              </div>
            </>
          )}
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
      </main>

      <footer className="site-footer">
        <p>A fan-made tool, not affiliated with or endorsed by Marvel or Upper Deck.</p>
        <p>
          Setup rules follow the{' '}
          <a href={RULEBOOK_URL} target="_blank" rel="noreferrer">
            First Edition rulebook
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
