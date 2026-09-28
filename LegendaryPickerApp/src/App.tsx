import { useEffect, useState } from 'react'
import { getSetup, ping, type Setup } from './api/setupApi.ts'
import { SetupChecklist } from './SetupChecklist.tsx'

const PLAYER_COUNTS = [1, 2, 3, 4, 5]

// A sleeping API answers slowly; past this point the loading state says so.
const WAKE_UP_NOTICE_MS = 3_000

const PLAYER_COUNT_KEY = 'legendaryPicker.playerCount'

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

type Status =
  | { kind: 'idle' }
  | { kind: 'loading' }
  | { kind: 'result'; setup: Setup }
  | { kind: 'noEligibleScheme'; message: string }
  | { kind: 'error' }

function App() {
  const [players, setPlayers] = useState<number | null>(loadPlayerCount)
  const [status, setStatus] = useState<Status>({ kind: 'idle' })
  const [showWakeUpNotice, setShowWakeUpNotice] = useState(false)

  // Wake the API while the player is still choosing a count. Strict mode may repeat this harmlessly.
  useEffect(() => {
    void ping()
  }, [])

  const loading = status.kind === 'loading'

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

  // Generate, Generate another and Retry all draw for the selected count.
  async function generate() {
    if (players === null) return
    setStatus({ kind: 'loading' })
    try {
      const response = await getSetup(players)
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
    <main className="app">
      <header>
        <h1>Legendary Picker</h1>
        <p className="tagline">A random legal setup for the Marvel Legendary core box.</p>
      </header>

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
        <button
          type="button"
          className="primary"
          disabled={players === null || loading}
          onClick={generate}
        >
          Generate
        </button>
      </section>

      <section className="status" aria-live="polite">
        {status.kind === 'loading' && (
          <>
            <p>Drawing a setup…</p>
            {showWakeUpNotice && (
              <p className="notice">The server may be waking up. This can take up to 30 seconds.</p>
            )}
          </>
        )}
        {status.kind === 'result' && (
          <>
            <SetupChecklist setup={status.setup} />
            <button type="button" className="primary" onClick={generate}>
              Generate another
            </button>
          </>
        )}
        {status.kind === 'noEligibleScheme' && (
          <>
            <p>{status.message}</p>
            <p>Pick another player count.</p>
          </>
        )}
        {status.kind === 'error' && (
          <>
            <p>Couldn't get a setup. Check your connection and try again.</p>
            <button type="button" className="primary" onClick={generate}>
              Retry
            </button>
          </>
        )}
      </section>
    </main>
  )
}

export default App
