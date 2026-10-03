import { act, render, screen, within } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { StrictMode } from 'react'
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest'
import App from './App.tsx'
import type { Box, NoEligibleScheme, Setup } from './api/setupApi.ts'
import mixedCrushHydra from './test/fixtures/mixedCrushHydra.json'

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

const core: Box = { id: 'core', name: 'Marvel Legendary First Edition core box', baseGame: true, ruleset: 'firstEdition' }
const fixture: Box = { id: 'fixture', name: 'Fixture Expansion', baseGame: false, ruleset: 'firstEdition' }
const villains: Box = { id: 'villains', name: 'Legendary: Villains', baseGame: true, ruleset: 'villainous' }

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

// The app's aria-live line: what a screen reader announces as the draw's state changes. (The
// checklist's progress line is a separate role="status" region.)
const announced = () => document.querySelector('[aria-live]')!.textContent

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
    expect(within(footer).getByRole('link', { name: 'Villains rulebook' })).toHaveAttribute(
      'href',
      'https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf',
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

  it('announces the draw and then the result through the live region, leaving the count to the focused heading', async () => {
    let answer!: () => void
    setupAnswers.push(() => new Promise((resolve) => (answer = () => resolve(Response.json(setup)))))
    const user = userEvent.setup()
    renderApp()

    expect(announced()).toBe('')

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(announced()).toBe('Drawing a setup for 3 players…')

    answer()
    expect(await screen.findByRole('heading', { name: 'Setup for 3 players' })).toHaveFocus()
    expect(announced()).toBe('Setup ready. Scheme: Midtown Bank Robbery. Mastermind: Magneto.')
  })

  it('announces a failed draw and a count with no legal Scheme', async () => {
    setupAnswers.push(
      () => Promise.reject(new TypeError('Failed to fetch')),
      () => json(noEligibleScheme),
    )
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    const retry = await screen.findByRole('button', { name: 'Retry' })

    expect(announced()).toBe("Couldn't get a setup. Check your connection and try again.")

    await user.click(retry)
    await screen.findByText('Pick another player count.')

    expect(announced()).toBe(
      'No Scheme can be set up legally for 3 players with the included boxes. Pick another player count.',
    )
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

  it('names the count Generate another draws once it differs from the setup on screen', async () => {
    setupAnswers.push(() => json(setup), () => json({ ...setup, players: 4 }))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    const actionBar = await screen.findByRole('group', { name: 'Setup actions' })
    await user.click(screen.getByRole('button', { name: '4' }))

    expect(screen.getByRole('heading', { name: 'Setup for 3 players' })).toBeInTheDocument()
    expect(within(actionBar).getByRole('paragraph')).toHaveTextContent('3 players')

    await user.click(within(actionBar).getByRole('button', { name: 'Generate another · 4 players' }))

    expect(await screen.findByRole('heading', { name: 'Setup for 4 players' })).toBeInTheDocument()
    const nextBar = screen.getByRole('group', { name: 'Setup actions' })
    expect(within(nextBar).getByRole('paragraph')).toHaveTextContent('4 players')
    expect(within(nextBar).getByRole('button', { name: 'Generate another' })).toBeInTheDocument()
    expect(setupRequests().at(-1)).toMatch(/players=4&boxes=core$/)
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
      'Get a random legal setup and a checklist for laying it out, following the official rules.'

    expect(screen.getByText(intro)).toBeInTheDocument()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })

    expect(screen.queryByText(intro)).not.toBeInTheDocument()
  })

  it('keeps the intro when the first draw fails', async () => {
    setupAnswers.push(() => Promise.reject(new TypeError('Failed to fetch')))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await screen.findByRole('button', { name: 'Retry' })

    expect(
      screen.getByText('Get a random legal setup and a checklist for laying it out, following the official rules.'),
    ).toBeInTheDocument()
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

    expect(screen.getByText('The server may be waking up. This can take up to 30 seconds.')).toBeInTheDocument()
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

  it('lists base games above expansions, with only the core box ticked at first', async () => {
    boxesAnswer = () => json([fixture, core])
    renderApp()

    await screen.findByRole('checkbox', { name: 'Fixture Expansion' })

    expect(
      within(screen.getByRole('group', { name: 'Boxes' }))
        .getAllByRole('checkbox')
        .map((box) => [box.closest('label')!.textContent, (box as HTMLInputElement).checked]),
    ).toEqual([
      ['Marvel Legendary First Edition core box Base game', true],
      ['Fixture Expansion', false],
    ])
  })

  it('lets the core box be unticked, and disables Generate until a base game is ticked', async () => {
    boxesAnswer = () => json([core, fixture])
    const user = userEvent.setup()
    renderApp()
    await user.click(screen.getByRole('button', { name: '3' }))
    const coreBox = await screen.findByRole('checkbox', { name: 'Marvel Legendary First Edition core box Base game' })

    await user.click(coreBox)

    expect(coreBox).not.toBeChecked()
    expect(screen.getByRole('button', { name: 'Generate' })).toBeDisabled()
    expect(screen.getByText('Pick a base game to draw a setup.')).toBeInTheDocument()

    await user.click(coreBox)

    expect(screen.getByRole('button', { name: 'Generate' })).toBeEnabled()
    expect(screen.queryByText('Pick a base game to draw a setup.')).not.toBeInTheDocument()
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

  it('remembers the ticked boxes on the next visit and forgets boxes the API no longer lists', async () => {
    boxesAnswer = () => json([core, fixture])
    const user = userEvent.setup()
    const firstVisit = renderApp()
    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(await screen.findByRole('checkbox', { name: 'Fixture Expansion' }))
    firstVisit.unmount()
    expect(localStorage.getItem('legendaryPicker.boxes')).toBe('["core","fixture"]')
    localStorage.setItem('legendaryPicker.boxes', JSON.stringify(['fixture', 'retired', 'core']))
    setupAnswers.push(() => json(setup))

    renderApp()

    expect(await screen.findByRole('checkbox', { name: 'Fixture Expansion' })).toBeChecked()
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    await screen.findByRole('heading', { name: 'Midtown Bank Robbery' })
    expect(setupRequests()).toEqual([expect.stringMatching(/players=3&boxes=core,fixture$/)])
  })

  it('restores a remembered selection without the core box', async () => {
    localStorage.setItem('legendaryPicker.boxes', JSON.stringify(['fixture']))
    boxesAnswer = () => json([core, fixture])
    renderApp()

    expect(await screen.findByRole('checkbox', { name: 'Fixture Expansion' })).toBeChecked()
    expect(screen.getByRole('checkbox', { name: 'Marvel Legendary First Edition core box Base game' })).not.toBeChecked()
    expect(screen.getByText('Pick a base game to draw a setup.')).toBeInTheDocument()
  })

  it('moves expansions remembered before base games were picks into the box list once, with the core box', async () => {
    localStorage.setItem('legendaryPicker.expansions', JSON.stringify(['fixture']))
    boxesAnswer = () => json([core, fixture])
    renderApp()

    expect(await screen.findByRole('checkbox', { name: 'Fixture Expansion' })).toBeChecked()
    expect(screen.getByRole('checkbox', { name: 'Marvel Legendary First Edition core box Base game' })).toBeChecked()
    expect(localStorage.getItem('legendaryPicker.boxes')).toBe('["core","fixture"]')
    expect(localStorage.getItem('legendaryPicker.expansions')).toBeNull()
  })

  it('keeps the boxes already remembered and drops the old expansions key', async () => {
    localStorage.setItem('legendaryPicker.boxes', JSON.stringify(['villains']))
    localStorage.setItem('legendaryPicker.expansions', JSON.stringify(['fixture']))
    boxesAnswer = () => json([core, villains, fixture])
    renderApp()

    expect(await screen.findByRole('checkbox', { name: 'Legendary: Villains Base game' })).toBeChecked()
    expect(screen.getByRole('checkbox', { name: 'Fixture Expansion' })).not.toBeChecked()
    expect(localStorage.getItem('legendaryPicker.expansions')).toBeNull()
  })

  it('draws from the core box and Villains together, and from Villains with a Heroic expansion', async () => {
    boxesAnswer = () => json([core, villains, fixture])
    setupAnswers.push(() => json(mixedCrushHydra), () => json(mixedCrushHydra))
    const user = userEvent.setup()
    renderApp()
    await user.click(screen.getByRole('button', { name: '3' }))

    await user.click(await screen.findByRole('checkbox', { name: 'Legendary: Villains Base game' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))

    expect(await screen.findByRole('heading', { name: 'Setup for 3 players', level: 2 })).toBeInTheDocument()
    expect(announced()).toBe('Setup ready. Plot: Crush HYDRA. Mastermind: Magneto.')

    await user.click(screen.getByRole('checkbox', { name: 'Marvel Legendary First Edition core box Base game' }))
    await user.click(screen.getByRole('checkbox', { name: 'Fixture Expansion' }))
    await user.click(screen.getByRole('button', { name: 'Generate another' }))

    expect(setupRequests()).toEqual([
      expect.stringMatching(/players=3&boxes=core,villains$/),
      expect.stringMatching(/players=3&boxes=villains,fixture$/),
    ])
  })

  it('draws nothing when a slow box list shows the remembered boxes have no base game', async () => {
    localStorage.setItem('legendaryPicker.boxes', JSON.stringify(['fixture']))
    let answerBoxes!: () => void
    const boxesListed = new Promise<void>((resolve) => (answerBoxes = resolve))
    boxesAnswer = () => boxesListed.then(() => Response.json([core, fixture]))
    const user = userEvent.setup()
    renderApp()

    await user.click(screen.getByRole('button', { name: '3' }))
    await user.click(screen.getByRole('button', { name: 'Generate' }))
    answerBoxes()

    expect(await screen.findByText('Pick a base game to draw a setup.')).toBeInTheDocument()
    expect(screen.getByRole('button', { name: 'Generate' })).toBeDisabled()
    expect(setupRequests()).toEqual([])
  })

  it('waits for a slow box list so a remembered expansion is still included', async () => {
    localStorage.setItem('legendaryPicker.boxes', JSON.stringify(['core', 'fixture']))
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
