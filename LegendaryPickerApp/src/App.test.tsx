import { act, render, screen } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { StrictMode } from 'react'
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest'
import App from './App.tsx'
import type { NoEligibleScheme, Setup } from './api/setupApi.ts'

const setup: Setup = {
  kind: 'setup',
  players: 3,
  scheme: { id: 'core_scheme_midtown-bank-robbery', name: 'Midtown Bank Robbery' },
  mastermind: { id: 'core_mastermind_magneto', name: 'Magneto' },
  villainGroups: [{ id: 'core_villain_brotherhood', name: 'Brotherhood' }],
  henchmanGroups: [{ id: 'core_henchman_savage-land-mutates', name: 'Savage Land Mutates' }],
  heroes: [
    { id: 'core_hero_wolverine', name: 'Wolverine' },
    { id: 'core_hero_storm', name: 'Storm' },
    { id: 'core_hero_hulk', name: 'Hulk' },
  ],
  villainDeck: { twists: 8, masterStrikes: 5, villainCards: 8, henchmanCards: 10, bystanders: 12, heroCards: 0, total: 43 },
  heroDeck: { heroCards: 42, movedToVillainDeck: 0, total: 42 },
  twistsBesideScheme: 0,
  stacks: { wounds: 30, officers: 30, bystanders: 18 },
  playerDeck: { agents: 8, troopers: 4 },
  notes: [],
}

const noEligibleScheme: NoEligibleScheme = {
  kind: 'noEligibleScheme',
  players: 3,
  message: 'No Scheme can be set up legally for 3 players with the included boxes.',
}

type SetupAnswer = (signal: AbortSignal) => Promise<Response>

const json = (body: unknown) => Promise.resolve(Response.json(body))
const never: SetupAnswer = () => new Promise<Response>(() => {})

let fetchMock: ReturnType<typeof vi.fn>
let setupAnswers: SetupAnswer[]

// Health always answers; each /api/setup request takes the next queued answer.
beforeEach(() => {
  setupAnswers = []
  fetchMock = vi.fn((url: string, init?: RequestInit) => {
    if (url.endsWith('/api/health')) return json({ status: 'ok' })
    const answer = setupAnswers.shift()
    if (!answer) throw new Error(`Unexpected request ${url}`)
    return answer(init!.signal!)
  })
  vi.stubGlobal('fetch', fetchMock)
})

afterEach(() => {
  vi.useRealTimers()
  vi.unstubAllGlobals()
  vi.restoreAllMocks()
})

const setupRequests = () =>
  fetchMock.mock.calls.map(([url]) => url as string).filter((url) => url.includes('/api/setup'))

function renderApp() {
  return render(
    <StrictMode>
      <App />
    </StrictMode>,
  )
}

describe('App', () => {
  it('pings the health endpoint on mount', () => {
    renderApp()

    expect(fetchMock).toHaveBeenCalledWith(expect.stringMatching(/\/api\/health$/))
  })

  it('keeps Generate disabled until a player count is picked', async () => {
    const user = userEvent.setup()
    renderApp()

    expect(screen.getByRole('button', { name: 'Generate' })).toBeDisabled()

    await user.click(screen.getByRole('button', { name: '2' }))

    expect(screen.getByRole('button', { name: 'Generate' })).toBeEnabled()
  })

  it('requests a setup for the picked count once and shows its Scheme', async () => {
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(await screen.findByText('Midtown Bank Robbery')).toBeInTheDocument()
    expect(screen.getByText('Magneto')).toBeInTheDocument()
    expect(screen.getByLabelText('3 Heroes Wolverine, Storm, Hulk 42')).toBeInTheDocument()
    expect(screen.getByLabelText('1 Villain Group Brotherhood 8')).toBeInTheDocument()
    expect(screen.getByLabelText('1 Henchman Group Savage Land Mutates 10')).toBeInTheDocument()
    expect(setupRequests()).toEqual([expect.stringMatching(/\/api\/setup\?players=3$/)])
  })

  it('draws again for the same count on Generate another', async () => {
    setupAnswers.push(() => json(setup), () => json({ ...setup, scheme: { id: 'core_scheme_x', name: 'Portals to the Dark Dimension' } }))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await user.click(await screen.findByRole('button', { name: 'Generate another' }))

    expect(await screen.findByText('Portals to the Dark Dimension')).toBeInTheDocument()
    expect(setupRequests()).toEqual([
      expect.stringMatching(/players=3$/),
      expect.stringMatching(/players=3$/),
    ])
  })

  it('clears the ticks on Generate another', async () => {
    setupAnswers.push(() => json(setup), () => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await user.click(await screen.findByLabelText('Scheme Twists 8'))
    await user.click(screen.getByLabelText('Wounds 30'))

    expect(screen.getByLabelText('Scheme Twists 8')).toBeChecked()

    await user.click(screen.getByRole('button', { name: 'Generate another' }))
    await screen.findByRole('button', { name: 'Generate another' })

    expect(screen.getAllByRole('checkbox').filter((box) => (box as HTMLInputElement).checked)).toEqual([])
    expect(setupRequests()).toHaveLength(2)
  })

  it('shows the loading text, then the wake-up line on a slow response', async () => {
    vi.useFakeTimers({ shouldAdvanceTime: true })
    setupAnswers.push(never)
    const user = userEvent.setup({ advanceTimers: vi.advanceTimersByTime })
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(screen.getByText('Drawing a setup…')).toBeInTheDocument()
    expect(screen.queryByText(/waking up/)).not.toBeInTheDocument()

    await act(() => vi.advanceTimersByTimeAsync(3_000))

    expect(screen.getByText(/The server may be waking up/)).toBeInTheDocument()
    expect(screen.getByRole('button', { name: 'Generate' })).toBeDisabled()
    expect(screen.getByRole('button', { name: '3' })).toBeDisabled()
  })

  it('gives up after 45 seconds and offers Retry', async () => {
    vi.useFakeTimers({ shouldAdvanceTime: true })
    setupAnswers.push(
      (signal) =>
        new Promise((_, reject) => signal.addEventListener('abort', () => reject(signal.reason))),
    )
    const user = userEvent.setup({ advanceTimers: vi.advanceTimersByTime })
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await act(() => vi.advanceTimersByTimeAsync(44_000))

    expect(screen.queryByRole('button', { name: 'Retry' })).not.toBeInTheDocument()

    await act(() => vi.advanceTimersByTimeAsync(1_000))

    expect(screen.getByRole('button', { name: 'Retry' })).toBeInTheDocument()
  })

  it('shows Retry on a failed request and re-requests the same count', async () => {
    setupAnswers.push(
      () => Promise.reject(new TypeError('Failed to fetch')),
      () => json(setup),
    )
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await user.click(await screen.findByRole('button', { name: 'Retry' }))

    expect(await screen.findByText('Midtown Bank Robbery')).toBeInTheDocument()
    expect(setupRequests()).toEqual([
      expect.stringMatching(/\/api\/setup\?players=3$/),
      expect.stringMatching(/\/api\/setup\?players=3$/),
    ])
  })

  it('treats an error status as a failure', async () => {
    // A well-formed body must not hide the status: only a 2xx counts as an answer.
    setupAnswers.push(() => Promise.resolve(Response.json(setup, { status: 500 })))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(await screen.findByRole('button', { name: 'Retry' })).toBeInTheDocument()
    expect(screen.queryByText('Midtown Bank Robbery')).not.toBeInTheDocument()
  })

  it('explains a noEligibleScheme result', async () => {
    setupAnswers.push(() => json(noEligibleScheme))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(
      await screen.findByText('No Scheme can be set up legally for 3 players with the included boxes.'),
    ).toBeInTheDocument()
    expect(screen.getByText('Pick another player count.')).toBeInTheDocument()
  })

  it('preselects the last picked count on the next visit', async () => {
    const user = userEvent.setup()
    const firstVisit = renderApp()
    await user.click(screen.getByRole('button', { name: '4' }))
    firstVisit.unmount()

    renderApp()

    expect(screen.getByRole('button', { name: '4' })).toHaveAttribute('aria-pressed', 'true')
    expect(screen.getByRole('button', { name: 'Generate' })).toBeEnabled()
  })

  it('works when storage is unavailable', async () => {
    vi.spyOn(Storage.prototype, 'getItem').mockImplementation(() => {
      throw new DOMException('Blocked', 'SecurityError')
    })
    vi.spyOn(Storage.prototype, 'setItem').mockImplementation(() => {
      throw new DOMException('Blocked', 'SecurityError')
    })
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '2' }))

    expect(screen.getByRole('button', { name: '2' })).toHaveAttribute('aria-pressed', 'true')
    expect(screen.getByRole('button', { name: 'Generate' })).toBeEnabled()
  })
})
