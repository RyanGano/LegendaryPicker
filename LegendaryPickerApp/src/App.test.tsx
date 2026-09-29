import { act, render, screen, within } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { StrictMode } from 'react'
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest'
import App from './App.tsx'
import type { Box, NoEligibleScheme, Setup } from './api/setupApi.ts'

const setup: Setup = {
  kind: 'setup',
  players: 3,
  scheme: { id: 'core_scheme_midtown-bank-robbery', name: 'Midtown Bank Robbery', terms: [] },
  mastermind: { id: 'core_mastermind_magneto', name: 'Magneto', terms: [] },
  villainGroups: [{ id: 'core_villain_brotherhood', name: 'Brotherhood', terms: [] }],
  henchmanGroups: [{ id: 'core_henchman_savage-land-mutates', name: 'Savage Land Mutates', terms: [] }],
  heroes: [
    { id: 'core_hero_wolverine', name: 'Wolverine', terms: [] },
    { id: 'core_hero_storm', name: 'Storm', terms: [] },
    { id: 'core_hero_hulk', name: 'Hulk', terms: [] },
  ],
  villainDeck: { twists: 8, masterStrikes: 5, villainCards: 8, henchmanCards: 10, bystanders: 12, total: 43 },
  heroDeck: { heroCards: 42, total: 42 },
  twistsBesideScheme: 0,
  stacks: { wounds: 30, officers: 30, bystanders: 18 },
  playerDeck: { agents: 8, troopers: 4 },
  moves: [],
  notes: [],
  glossary: [],
}

const noEligibleScheme: NoEligibleScheme = {
  kind: 'noEligibleScheme',
  players: 3,
  message: 'No Scheme can be set up legally for 3 players with the included boxes.',
}

const core: Box = { id: 'core', name: 'Marvel Legendary First Edition core box', baseGame: true }
const fixture: Box = { id: 'fixture', name: 'Fixture Expansion', baseGame: false }

type SetupAnswer = (signal: AbortSignal) => Promise<Response>

const json = (body: unknown) => Promise.resolve(Response.json(body))
const never: SetupAnswer = () => new Promise<Response>(() => {})

let fetchMock: ReturnType<typeof vi.fn>
let setupAnswers: SetupAnswer[]
let boxesAnswer: () => Promise<Response>

// Health always answers, /api/boxes lists the core box unless a test says otherwise, and each
// /api/setup request takes the next queued answer.
beforeEach(() => {
  setupAnswers = []
  boxesAnswer = () => json([core])
  fetchMock = vi.fn((url: string, init?: RequestInit) => {
    if (url.endsWith('/api/health')) return json({ status: 'ok' })
    if (url.endsWith('/api/boxes')) return boxesAnswer()
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

  it('says in the footer that it is a fan tool and links the rulebook and repo', () => {
    renderApp()

    const footer = screen.getByRole('contentinfo')
    expect(footer).toHaveTextContent('A fan-made tool, not affiliated with or endorsed by Marvel or Upper Deck.')
    expect(within(footer).getByRole('link', { name: 'First Edition rulebook' })).toHaveAttribute(
      'href',
      'https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf',
    )
    expect(within(footer).getByRole('link', { name: 'Source on GitHub' })).toHaveAttribute(
      'href',
      'https://github.com/RyanGano/LegendaryPicker',
    )
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

    expect(await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })).toBeInTheDocument()
    expect(screen.getByRole('heading', { name: 'Magneto' })).toBeInTheDocument()
    expect(screen.getByLabelText('Wolverine, Storm, Hulk 3 Heroes 42')).toBeInTheDocument()
    expect(screen.getByLabelText('Brotherhood 1 Villain Group 8')).toBeInTheDocument()
    expect(screen.getByLabelText('Savage Land Mutates 1 Henchman Group 10')).toBeInTheDocument()
    expect(setupRequests()).toEqual([expect.stringMatching(/\/api\/setup\?players=3&boxes=core$/)])
  })

  it('moves focus to the result heading after Generate', async () => {
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(await screen.findByRole('heading', { name: 'Setup for 3 players' })).toHaveFocus()
  })

  it.each([
    { reduceMotion: false, behavior: 'smooth' },
    { reduceMotion: true, behavior: 'auto' },
  ])('scrolls the result heading to the top, $behavior when reduced motion is $reduceMotion', async ({ reduceMotion, behavior }) => {
    vi.stubGlobal('matchMedia', (query: string) => ({
      matches: reduceMotion && query === '(prefers-reduced-motion: reduce)',
      media: query,
    }))
    const scrolled: { heading: string | null; options: unknown }[] = []
    vi.spyOn(Element.prototype, 'scrollIntoView').mockImplementation(function (this: Element, options) {
      scrolled.push({ heading: this.textContent, options })
    })
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await screen.findByRole('heading', { name: 'Setup for 3 players' })

    expect(scrolled.at(-1)).toEqual({ heading: 'Setup for 3 players', options: { behavior, block: 'start' } })
  })

  it('scrolls the page to the top instead in the two-column layout', async () => {
    vi.stubGlobal('matchMedia', (query: string) => ({ matches: query === '(width >= 56.25rem)', media: query }))
    const scrollIntoView = vi.spyOn(Element.prototype, 'scrollIntoView')
    const scrollTo = vi.spyOn(window, 'scrollTo').mockImplementation(() => {})
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(await screen.findByRole('heading', { name: 'Setup for 3 players' })).toHaveFocus()
    expect(scrollTo).toHaveBeenLastCalledWith({ top: 0, behavior: 'smooth' })
    expect(scrollIntoView).not.toHaveBeenCalled()
  })

  it('draws again for the same count from the action bar', async () => {
    setupAnswers.push(() => json(setup), () => json({ ...setup, scheme: { id: 'core_scheme_x', name: 'Portals to the Dark Dimension', terms: [] } }))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    const actionBar = await screen.findByRole('group', { name: 'Setup actions' })

    expect(actionBar).toHaveTextContent('3 players')

    await user.click(within(actionBar).getByRole('button', { name: 'Generate another' }))

    expect(await screen.findByRole('heading', { name: 'Portals to the Dark Dimension' })).toBeInTheDocument()
    expect(setupRequests()).toEqual([
      expect.stringMatching(/players=3&boxes=core$/),
      expect.stringMatching(/players=3&boxes=core$/),
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

    expect(
      within(screen.getByRole('article'))
        .getAllByRole('checkbox')
        .filter((box) => (box as HTMLInputElement).checked),
    ).toEqual([])
    expect(setupRequests()).toHaveLength(2)
  })

  it('explains what the app gives until the first setup is shown', async () => {
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()
    const intro =
      'Get a random legal setup and a checklist for laying it out, following the First Edition rules.'

    expect(screen.getByText(intro)).toBeInTheDocument()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })

    expect(screen.queryByText(intro)).not.toBeInTheDocument()
  })

  it('shows a skeleton of the result while a request is pending', async () => {
    let answer!: () => void
    setupAnswers.push(() => new Promise((resolve) => (answer = () => resolve(Response.json(setup)))))
    const user = userEvent.setup()
    renderApp()

    expect(screen.queryByTestId('setup-skeleton')).not.toBeInTheDocument()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(screen.getByTestId('setup-skeleton')).toBeInTheDocument()

    answer()
    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })

    expect(screen.queryByTestId('setup-skeleton')).not.toBeInTheDocument()
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

    expect(await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })).toBeInTheDocument()
    expect(setupRequests()).toEqual([
      expect.stringMatching(/\/api\/setup\?players=3&boxes=core$/),
      expect.stringMatching(/\/api\/setup\?players=3&boxes=core$/),
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
    expect(screen.queryByRole('heading', { name: 'Midtown Bank Robbery' })).not.toBeInTheDocument()
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

  it('always includes the core box and lists expansions unticked', async () => {
    boxesAnswer = () => json([core, fixture])
    renderApp()

    const coreBox = await screen.findByRole('checkbox', { name: /^Marvel Legendary First Edition core box/ })
    expect(coreBox).toBeChecked()
    expect(coreBox).toBeDisabled()
    expect(screen.getByRole('checkbox', { name: 'Fixture Expansion' })).not.toBeChecked()
  })

  it('draws from the core box and every ticked expansion', async () => {
    boxesAnswer = () => json([core, fixture])
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(await screen.findByRole('checkbox', { name: 'Fixture Expansion' }))
    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })
    expect(setupRequests()).toEqual([expect.stringMatching(/\/api\/setup\?players=3&boxes=core,fixture$/)])
  })

  it('remembers the ticked expansions on the next visit and forgets boxes the API no longer lists', async () => {
    boxesAnswer = () => json([core, fixture])
    const user = userEvent.setup()
    const firstVisit = renderApp()
    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(await screen.findByRole('checkbox', { name: 'Fixture Expansion' }))
    firstVisit.unmount()
    expect(localStorage.getItem('legendaryPicker.expansions')).toBe('["fixture"]')
    localStorage.setItem('legendaryPicker.expansions', JSON.stringify(['fixture', 'retired']))
    setupAnswers.push(() => json(setup))

    renderApp()

    expect(await screen.findByRole('checkbox', { name: 'Fixture Expansion' })).toBeChecked()
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })
    expect(setupRequests()).toEqual([expect.stringMatching(/players=3&boxes=core,fixture$/)])
  })

  it('waits for a slow box list so a remembered expansion is still included', async () => {
    localStorage.setItem('legendaryPicker.expansions', JSON.stringify(['fixture']))
    let answerBoxes!: () => void
    const boxesListed = new Promise<void>((resolve) => (answerBoxes = resolve))
    boxesAnswer = () => boxesListed.then(() => Response.json([core, fixture]))
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    expect(setupRequests()).toEqual([])
    answerBoxes()

    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })
    expect(setupRequests()).toEqual([expect.stringMatching(/players=3&boxes=core,fixture$/)])
  })

  it('draws from the core box alone when the box list cannot be loaded', async () => {
    boxesAnswer = () => Promise.reject(new TypeError('Failed to fetch'))
    setupAnswers.push(() => json(setup))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })
    expect(screen.queryByRole('group', { name: 'Boxes' })).not.toBeInTheDocument()
    expect(setupRequests()).toEqual([expect.stringMatching(/\/api\/setup\?players=3$/)])
  })

  it('works when storage is unavailable', async () => {
    boxesAnswer = () => json([core, fixture])
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

    await user.click(await screen.findByRole('checkbox', { name: 'Fixture Expansion' }))

    expect(screen.getByRole('checkbox', { name: 'Fixture Expansion' })).toBeChecked()
  })
})
