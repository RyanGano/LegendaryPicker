import { render, screen, within } from '@testing-library/react'
import { describe, expect, it } from 'vitest'
import type { Setup } from './api/setupApi.ts'
import { SetupChecklist } from './SetupChecklist.tsx'
// Real GET /api/setup bodies, each from a scripted draw through the service's HTTP pipeline.
import killbots from './test/fixtures/killbots.json'
import legacyVirusThreePlayers from './test/fixtures/legacyVirusThreePlayers.json'
import soloSecretInvasion from './test/fixtures/soloSecretInvasion.json'
import twoPlayerCosmicCube from './test/fixtures/twoPlayerCosmicCube.json'

function renderChecklist(setup: unknown) {
  render(<SetupChecklist setup={setup as Setup} />)
}

const section = (name: string) => screen.getByRole('region', { name })

// Each line of a section as the player reads it: label, drawn names, count.
const rows = (sectionName: string) =>
  within(section(sectionName)).getAllByRole('listitem').map((row) => row.textContent)

describe('SetupChecklist', () => {
  it('shows the 2-player Cosmic Cube deck totals', () => {
    renderChecklist(twoPlayerCosmicCube)

    expect(rows('Villain Deck')).toContain('Total 41')
    expect(rows('Hero Deck')).toContain('Total 70')
  })

  it('lists the sections in setup order', () => {
    renderChecklist(killbots)

    expect(screen.getAllByRole('heading', { level: 3 }).map((heading) => heading.textContent)).toEqual([
      'Scheme and Mastermind',
      'Villain Deck',
      'Beside the Scheme',
      'Hero Deck',
      'Shared stacks',
      'Starting deck per player · 4 players',
      'Why this setup',
    ])
  })

  it('shows the Solo Secret Invasion Heroes, the Hero cards moved and the D2 note', () => {
    renderChecklist(soloSecretInvasion)

    expect(rows('Hero Deck')).toEqual([
      '6 Heroes Deadpool, Hulk, Cyclops, Storm, Black Widow, Iron Man 84',
      'Moved to the Villain Deck −12',
      'Total 72',
    ])
    expect(rows('Villain Deck')).toEqual([
      'Scheme Twists 8',
      'Master Strikes 1',
      '1 Villain Group Skrulls 8',
      '1 Henchman Group Doombot Legion 3',
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
})
