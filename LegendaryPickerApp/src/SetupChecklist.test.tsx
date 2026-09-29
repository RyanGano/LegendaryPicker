import { fireEvent, render, screen, within } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { afterEach, describe, expect, it, vi } from 'vitest'
import type { Setup } from './api/setupApi.ts'
import { SetupChecklist } from './SetupChecklist.tsx'
import { SetupSummary } from './SetupSummary.tsx'
// Real GET /api/setup bodies, each from a scripted draw through the service's HTTP pipeline.
import killbots from './test/fixtures/killbots.json'
import legacyVirusThreePlayers from './test/fixtures/legacyVirusThreePlayers.json'
import soloSecretInvasion from './test/fixtures/soloSecretInvasion.json'
import twoPlayerCosmicCube from './test/fixtures/twoPlayerCosmicCube.json'
// Drawn from the core box and the service's test-only fixture expansion.
import twoBoxesTestHeist from './test/fixtures/twoBoxesTestHeist.json'
// Drawn from the core box and the service's test-only card-moves fixture expansion.
import twoBoxesHenchmanArmy from './test/fixtures/twoBoxesHenchmanArmy.json'
import twoBoxesTestWounded from './test/fixtures/twoBoxesTestWounded.json'

// The drawn cards and the checklist, in the order the app shows them on a phone.
function renderChecklist(setup: unknown) {
  render(
    <>
      <SetupSummary setup={setup as Setup} />
      <SetupChecklist setup={setup as Setup} />
    </>,
  )
}

const section = (name: string) => screen.getByRole('region', { name })

// Each line of a section as the player reads it: label, drawn names, count.
const rows = (sectionName: string) =>
  within(section(sectionName)).getAllByRole('listitem').map((row) => row.textContent)

// The drawn cards or groups under a summary heading, by name alone.
const tileNames = (sectionName: string) =>
  [...section(sectionName).querySelectorAll('.tile-name')].map((name) => name.textContent)

// A drawn card by its heading, and the tags on it, leaving out its term chips.
const card = (name: string) => screen.getByRole('heading', { name, level: 3 }).parentElement!
const tags = (name: string) => {
  const terms = within(card(name)).queryByRole('list', { name: 'Terms' })
  return within(card(name))
    .queryAllByRole('listitem')
    .filter((item) => !terms?.contains(item))
    .map((tag) => tag.textContent)
}

// The term chips on a drawn card or tile, by name.
const chips = (container: HTMLElement) =>
  within(within(container).getByRole('list', { name: 'Terms' }))
    .getAllByRole('button')
    .map((chip) => chip.textContent)

const tile = (sectionName: string, name: string) => within(section(sectionName)).getByText(name).closest('li')!

describe('SetupChecklist', () => {
  it('puts the drawn Scheme and Mastermind names first, as headings', () => {
    renderChecklist(legacyVirusThreePlayers)

    const summary = section('Drawn cards')
    expect(within(summary).getAllByRole('heading', { level: 3 }).slice(0, 2).map((h) => h.textContent)).toEqual([
      'Legacy Virus',
      'Magneto',
    ])
    expect(screen.getAllByRole('heading', { level: 3 })[0]).toHaveTextContent('Legacy Virus')
  })

  it('shows each drawn Hero and group as its own item under its type', () => {
    renderChecklist(killbots)

    expect(tileNames('Heroes')).toEqual(['Hulk', 'Deadpool', 'Hawkeye', 'Captain America', 'Black Widow'])
    expect(tileNames('Villain Groups')).toEqual(['Masters of Evil', 'Enemies of Asgard', 'Radiation'])
    expect(tileNames('Henchman Groups')).toEqual(['Doombot Legion', 'Hand Ninjas'])
  })

  it('names a single drawn group in the singular', () => {
    renderChecklist(soloSecretInvasion)

    expect(tileNames('Villain Group')).toEqual(['Skrulls'])
    expect(tileNames('Henchman Group')).toEqual(['Doombot Legion'])
  })

  it("tags the Mastermind card with the API's Always Leads note, word for word", () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(tags('Magneto')).toEqual([
      'Magneto always leads Brotherhood',
    ])
  })

  it('tags the Mastermind card when Solo ignores its Always Leads', () => {
    renderChecklist(soloSecretInvasion)

    expect(tags('Loki')).toEqual([
      "Solo ignores Loki's Always Leads",
    ])
  })

  it('tags the Mastermind card when a required group displaces its Always Leads group', () => {
    // No fixture draws this case, so the notes follow the text SetupGenerator writes for it.
    renderChecklist({
      ...legacyVirusThreePlayers,
      notes: [
        { text: 'Scheme requires Skrulls', citation: 'Card', link: null },
        {
          text: "Scheme requires Skrulls, so Magneto's Always Leads group Brotherhood is dropped",
          citation: 'D1',
          link: null,
        },
      ],
    })

    expect(tags('Magneto')).toEqual([
      "Scheme requires Skrulls, so Magneto's Always Leads group Brotherhood is dropped",
    ])
  })

  it('leaves the Scheme card untagged', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(tags('Legacy Virus')).toEqual([])
  })

  it('lists the Scheme and Mastermind by name in the checklist', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(rows('Scheme and Mastermind')).toEqual(['Legacy Virus Scheme', 'Magneto Mastermind'])
    expect(rows('Hero Deck')).toContain('Hawkeye, Captain America, Black Widow, Cyclops, Deadpool 5 Heroes 70')
  })

  it('counts the ticked lines against the lines to lay out, leaving totals out', async () => {
    const user = userEvent.setup()
    renderChecklist(legacyVirusThreePlayers)

    expect(screen.getByRole('status')).toHaveTextContent('0 of 13 laid out')

    const [twists, strikes] = within(section('Villain Deck')).getAllByRole('checkbox')
    await user.click(twists)
    await user.click(strikes)

    expect(screen.getByRole('status')).toHaveTextContent('2 of 13 laid out')

    await user.click(twists)

    expect(screen.getByRole('status')).toHaveTextContent('1 of 13 laid out')
  })

  it('names each tick box by its row, so a screen reader says what the box is for', () => {
    renderChecklist(legacyVirusThreePlayers)

    const expected = [
      'Legacy Virus Scheme',
      'Magneto Mastermind',
      'Scheme Twists 8',
      'Master Strikes 5',
      'Brotherhood, HYDRA, Radiation 3 Villain Groups 24',
      'Sentinel 1 Henchman Group 10',
      'Bystanders 8',
      'Hawkeye, Captain America, Black Widow, Cyclops, Deadpool 5 Heroes 70',
      'Wounds 18',
      'S.H.I.E.L.D. Officers 30',
      'Bystanders 22',
      'S.H.I.E.L.D. Agents 8',
      'S.H.I.E.L.D. Troopers 4',
    ]
    const boxes = screen.getAllByRole('checkbox')
    expect(boxes).toHaveLength(expected.length)
    boxes.forEach((box, i) => expect(box).toHaveAccessibleName(expected[i]))
  })

  it('shows the 2-player Cosmic Cube deck totals', () => {
    renderChecklist(twoPlayerCosmicCube)

    expect(rows('Villain Deck')).toContain('Total 41')
    expect(rows('Hero Deck')).toContain('Total 70')
  })

  it('lists the sections in setup order', () => {
    renderChecklist(killbots)

    expect(screen.getAllByRole('heading', { level: 3 }).map((heading) => heading.textContent)).toEqual([
      "Replace Earth's Leaders with Killbots",
      'Dr. Doom',
      'Heroes',
      'Villain Groups',
      'Henchman Groups',
      'Scheme and Mastermind',
      'Villain Deck',
      'Beside the Scheme',
      'Hero Deck',
      'Shared stacks',
      'Starting deck per player · 4 players',
      'Why this setup',
      'Terms in this setup',
    ])
  })

  it('shows the Solo Secret Invasion Heroes, the Hero cards moved and the D2 note', () => {
    renderChecklist(soloSecretInvasion)

    expect(rows('Hero Deck')).toEqual([
      'Deadpool, Hulk, Cyclops, Storm, Black Widow, Iron Man 6 Heroes 84',
      'Moved to the Villain Deck −12',
      'Total 72',
    ])
    expect(rows('Villain Deck')).toEqual([
      'Scheme Twists 8',
      'Master Strikes 1',
      'Skrulls 1 Villain Group 8',
      'Doombot Legion 1 Henchman Group 3',
      'Bystanders 1',
      'Hero cards from the Hero Deck 12',
      'Total 33',
    ])

    const d2 = within(section('Why this setup')).getByRole('link', { name: 'D2' })
    expect(d2).toHaveAttribute('href', 'https://boardgamegeek.com/thread/884926')
    expect(d2.closest('li')).toHaveTextContent('Scheme overrides Solo: 6 Heroes D2')
  })

  it('shows exactly the notes the API returns, linking only those with a source URL', () => {
    renderChecklist(soloSecretInvasion)

    const notes = within(section('Why this setup')).getAllByRole('listitem')
    expect(notes.map((note) => note.textContent)).toEqual([
      'Scheme overrides Solo: 6 Heroes D2',
      'Scheme moves 12 Hero cards into the Villain Deck Card',
      'Scheme requires Skrulls Card',
      "Solo ignores Loki's Always Leads R p.20",
      'Solo: after each Twist, KO a Hero costing 6 or less from the HQ R p.20',
    ])
    expect(within(notes[1]).queryByRole('link')).not.toBeInTheDocument()
  })

  it('shows Henchmen moved out of the Villain Deck as a line to lay out in the Hero Deck', () => {
    renderChecklist(twoBoxesHenchmanArmy)

    expect(rows('Villain Deck')).toEqual([
      'Scheme Twists 8',
      'Master Strikes 5',
      'HYDRA, Skrulls 2 Villain Groups 16',
      'Sentinel 1 Henchman Group 10',
      'Bystanders 2',
      'Moved to the Hero Deck −6',
      'Total 35',
    ])
    expect(rows('Hero Deck')).toEqual([
      'Rogue, Hulk, Emma Frost, Black Widow, Iron Man 5 Heroes 70',
      'Henchmen from the Villain Deck 6',
      'Total 76',
    ])
    // The line moved in gets a tick box; the one moved out does not.
    expect(within(section('Hero Deck')).getAllByRole('checkbox')).toHaveLength(2)
    expect(within(section('Villain Deck')).getAllByRole('checkbox')).toHaveLength(5)
    // Like every other line, the moved-in line names its tick box.
    expect(within(section('Hero Deck')).getAllByRole('checkbox')[1]).toHaveAccessibleName(
      'Henchmen from the Villain Deck 6',
    )
  })

  it("adds a moved card to each player's starting deck and leaves the rest in its stack", () => {
    renderChecklist(twoBoxesTestWounded)

    expect(rows('Starting deck per player · 2 players')).toEqual([
      'S.H.I.E.L.D. Agents 8',
      'S.H.I.E.L.D. Troopers 4',
      'Wounds from the Wound stack 1',
    ])
    expect(rows('Shared stacks')).toEqual(['Wounds 28', 'S.H.I.E.L.D. Officers 30', 'Bystanders 28'])
  })

  it('adds a Sidekick stack to lay out when an included box has Sidekicks', async () => {
    const user = userEvent.setup()
    renderChecklist(twoBoxesTestHeist)

    expect(rows('Shared stacks')).toEqual(['Wounds 30', 'S.H.I.E.L.D. Officers 30', 'Bystanders 28', 'Sidekicks 16'])
    expect(screen.getByRole('status')).toHaveTextContent('0 of 14 laid out')

    await user.click(within(section('Shared stacks')).getByRole('checkbox', { name: 'Sidekicks 16' }))

    expect(screen.getByRole('status')).toHaveTextContent('1 of 14 laid out')
  })

  it('leaves out the Sidekick stack when no included box has Sidekicks', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(within(section('Shared stacks')).queryByText('Sidekicks')).not.toBeInTheDocument()
  })

  it('renders a setup from an API that predates moves as having none', () => {
    // The site and the API deploy separately, so the site can briefly talk to an API without moves.
    const { moves: _moves, ...withoutMoves } = soloSecretInvasion
    renderChecklist(withoutMoves)

    expect(rows('Hero Deck')).toEqual(['Deadpool, Hulk, Cyclops, Storm, Black Widow, Iron Man 6 Heroes 84', 'Total 72'])
  })

  it('lists two moves of the same kind of card to one deck as two lines', () => {
    const error = vi.spyOn(console, 'error').mockImplementation(() => {})
    const heroCards = soloSecretInvasion.moves[0]
    renderChecklist({
      ...soloSecretInvasion,
      moves: [heroCards, { ...heroCards, count: 2, total: 2 }],
    })

    expect(rows('Villain Deck')).toContain('Hero cards from the Hero Deck 2')
    expect(rows('Hero Deck')).toContain('Moved to the Villain Deck −2')
    expect(within(section('Villain Deck')).getAllByText('Hero cards from the Hero Deck')).toHaveLength(2)
    expect(within(section('Hero Deck')).getAllByText('Moved to the Villain Deck')).toHaveLength(2)
    // React warns about duplicate keys through console.error.
    const calls = error.mock.calls.length
    error.mockRestore()
    expect(calls).toBe(0)
  })

  it('names the box each note comes from when the setup includes more than one box', () => {
    renderChecklist(twoBoxesTestHeist)

    const notes = within(section('Why this setup')).getAllByRole('listitem')
    expect(notes.map((note) => note.textContent)).toEqual([
      'Scheme requires HYDRA R p.3Fixture Expansion',
      'Test Tyrant always leads Test Cult R p.6Marvel Legendary First Edition core box',
    ])
    expect(within(notes[0]).getByRole('link', { name: 'R p.3' })).toHaveAttribute('href', 'https://example.test/fixture-rules.pdf')
  })

  it('names the box each term comes from when the setup includes more than one box', async () => {
    const user = userEvent.setup()
    renderChecklist(twoBoxesTestHeist)

    const terms = within(section('Terms in this setup')).getAllByRole('listitem')
    expect(terms.find((term) => term.textContent!.startsWith('Overdrive'))!.textContent).toBe(
      'Overdrive Keyword A made-up keyword that exists only in this test fixture. R p.2Fixture Expansion',
    )
    expect(terms.find((term) => term.textContent!.startsWith('Avengers'))).toHaveTextContent(
      /R p\.18Marvel Legendary First Edition core box$/,
    )

    await user.click(screen.getAllByRole('button', { name: 'Overdrive' })[0])

    expect(screen.getByRole('dialog', { name: 'Overdrive' })).toHaveTextContent(/R p\.2Fixture Expansion$/)
  })

  it('shows the Killbots Twists beside the Scheme and a Bystander stack of 12', () => {
    renderChecklist(killbots)

    expect(rows('Beside the Scheme')).toEqual(['Scheme Twists 3'])
    expect(rows('Shared stacks')).toContain('Bystanders 12')
  })

  it('leaves out Twists beside the Scheme when there are none', () => {
    renderChecklist(twoPlayerCosmicCube)

    expect(screen.queryByRole('region', { name: 'Beside the Scheme' })).not.toBeInTheDocument()
  })

  it('shows a Wound stack of 18 for Legacy Virus at 3 players', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(rows('Shared stacks')).toEqual(['Wounds 18', 'S.H.I.E.L.D. Officers 30', 'Bystanders 22'])
    expect(rows('Starting deck per player · 3 players')).toEqual(['S.H.I.E.L.D. Agents 8', 'S.H.I.E.L.D. Troopers 4'])
  })

  it("shows a Hero's team and classes as chips on its tile", () => {
    renderChecklist(killbots)

    expect(chips(tile('Heroes', 'Hulk'))).toEqual(['Avengers', 'Instinct', 'Strength'])
    expect(chips(tile('Heroes', 'Black Widow'))).toEqual(['Avengers', 'Covert', 'Tech', 'Rescue a Bystander'])
  })

  it("shows the Villain Groups', Scheme's and Mastermind's keywords as chips", () => {
    renderChecklist(killbots)

    expect(chips(tile('Villain Groups', 'Enemies of Asgard'))).toEqual(['Ambush', 'Escape', 'Fight'])
    expect(chips(card("Replace Earth's Leaders with Killbots"))).toEqual(['Scheme Twist'])
    expect(chips(card('Dr. Doom'))).toEqual(['Always Leads', 'Fight', 'Master Strike', 'Mastermind Tactic'])
  })

  it("opens a chip's explanation with its name, summary and cited source", async () => {
    const user = userEvent.setup()
    renderChecklist(killbots)

    const chip = within(tile('Heroes', 'Hulk')).getByRole('button', { name: 'Strength' })
    await user.click(chip)

    const explanation = screen.getByRole('dialog', { name: 'Strength' })
    expect(within(explanation).getByRole('heading', { name: 'Strength' })).toBeInTheDocument()
    expect(explanation).toHaveTextContent('Hero class')
    expect(explanation).toHaveTextContent(
      'A Hero class of fighters whose power is might, whether of body or of will and leadership.',
    )
    expect(within(explanation).getByRole('link', { name: 'R p.18' })).toHaveAttribute('href', 'https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf')
    expect(chip).toHaveAttribute('aria-expanded', 'true')
  })

  it('closes the explanation on Escape and returns focus to its chip', async () => {
    const user = userEvent.setup()
    renderChecklist(killbots)

    const chip = within(card('Dr. Doom')).getByRole('button', { name: 'Master Strike' })
    await user.click(chip)
    expect(screen.getByRole('dialog', { name: 'Master Strike' })).toHaveFocus()

    await user.keyboard('{Escape}')

    expect(screen.queryByRole('dialog')).not.toBeInTheDocument()
    expect(chip).toHaveFocus()
    expect(chip).toHaveAttribute('aria-expanded', 'false')
  })

  it('closes the explanation with its close button or a tap outside it', async () => {
    const user = userEvent.setup()
    renderChecklist(killbots)

    const chip = within(card('Dr. Doom')).getByRole('button', { name: 'Fight' })
    await user.click(chip)
    await user.click(within(screen.getByRole('dialog', { name: 'Fight' })).getByRole('button', { name: 'Close' }))
    expect(screen.queryByRole('dialog')).not.toBeInTheDocument()
    expect(chip).toHaveFocus()

    await user.click(chip)
    await user.click(screen.getByRole('heading', { name: 'Villain Deck' }))
    expect(screen.queryByRole('dialog')).not.toBeInTheDocument()
    expect(chip).toHaveFocus()
  })

  describe('explanation placement', () => {
    // jsdom lays nothing out, so give the chip a place on an 800px-tall screen and the popover a height.
    function layOut(chip: HTMLElement, rect: { top: number; bottom: number; left: number }) {
      vi.spyOn(chip, 'getBoundingClientRect').mockImplementation(() => rect as DOMRect)
      vi.spyOn(HTMLElement.prototype, 'offsetHeight', 'get').mockReturnValue(200)
      vi.stubGlobal('innerHeight', 800)
    }
    const placement = (dialog: HTMLElement) => ({
      top: dialog.style.getPropertyValue('--popover-top'),
      left: dialog.style.getPropertyValue('--popover-left'),
    })

    afterEach(() => {
      vi.restoreAllMocks()
      vi.unstubAllGlobals()
    })

    it('opens under the chip when there is room below', async () => {
      const user = userEvent.setup()
      renderChecklist(killbots)
      const chip = within(card('Dr. Doom')).getByRole('button', { name: 'Fight' })
      layOut(chip, { top: 100, bottom: 124, left: 40 })

      await user.click(chip)

      expect(placement(screen.getByRole('dialog', { name: 'Fight' }))).toEqual({ top: '128px', left: '40px' })
    })

    it('opens above the chip when it would run off the bottom of the screen', async () => {
      const user = userEvent.setup()
      renderChecklist(killbots)
      const chip = within(card('Dr. Doom')).getByRole('button', { name: 'Fight' })
      layOut(chip, { top: 700, bottom: 724, left: 40 })

      await user.click(chip)

      expect(placement(screen.getByRole('dialog', { name: 'Fight' }))).toEqual({ top: '496px', left: '40px' })
    })

    it('follows its chip when a column scrolls', async () => {
      const user = userEvent.setup()
      renderChecklist(killbots)
      const chip = within(card('Dr. Doom')).getByRole('button', { name: 'Fight' })
      layOut(chip, { top: 100, bottom: 124, left: 40 })
      await user.click(chip)

      layOut(chip, { top: 50, bottom: 74, left: 40 })
      fireEvent.scroll(card('Dr. Doom'))

      expect(placement(screen.getByRole('dialog', { name: 'Fight' }))).toEqual({ top: '78px', left: '40px' })
    })
  })

  it('lists every term in the setup once, after the rule notes, with its summary and citation', () => {
    renderChecklist(killbots)

    const terms = within(section('Terms in this setup')).getAllByRole('listitem')
    expect(terms.map((term) => term.querySelector('.term-name')!.textContent)).toEqual([
      'Avengers',
      'Covert',
      'Instinct',
      'Strength',
      'Tech',
      'Always Leads',
      'Ambush',
      'Escape',
      'Fight',
      'Master Strike',
      'Mastermind Tactic',
      'Rescue a Bystander',
      'Scheme Twist',
    ])
    expect(terms[8]).toHaveTextContent('Fight Keyword Triggers when a player spends enough Attack to defeat this card. R p.13')
    expect(within(terms[8]).getByRole('link', { name: 'R p.13' })).toHaveAttribute('href', 'https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf')
  })
})
