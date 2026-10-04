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
// Drawn from the core box and the service's test-only Hero rules fixture expansion.
import twoBoxesTestSong from './test/fixtures/twoBoxesTestSong.json'
import twoBoxesTestVault from './test/fixtures/twoBoxesTestVault.json'
// Drawn from the core box and the service's test-only setup-steps fixture expansion.
import twoBoxesTestVigil from './test/fixtures/twoBoxesTestVigil.json'
// Drawn from the core box and Paint the Town Red: Invade the Daily Bugle News HQ.
import twoBoxesDailyBugle from './test/fixtures/twoBoxesDailyBugle.json'
// A live draw from the core box and Guardians of the Galaxy with 2 players: Intergalactic Kree Nega-Bomb with
// Thanos, which sets 6 Bystanders aside and lays out the Shard supply for Groot and Infinity Gems.
import twoBoxesNegaBomb from './test/fixtures/twoBoxesNegaBomb.json'
// A live draw from Legendary: Villains and Fear Itself with 2 players: The Traitor with Dr. Strange, whose Betrayal
// Deck sets 3 Bindings per player and a 9th Twist aside.
import twoBoxesTraitor from './test/fixtures/twoBoxesTraitor.json'
// A live draw from Legendary: Villains alone, on the Villainous ruleset: Graduation at Xavier's X-Academy with Odin.
import twoPlayerVillainsGraduation from './test/fixtures/twoPlayerVillainsGraduation.json'
// Live draws from Legendary: Villains alone with 2 players whose Plot sets cards of a group beside it: Cage
// Villains in Power-Suppressing Cells with Dr. Strange and Cops drawn too, and Crown Thor King of Asgard with Nick
// Fury, who brings the Avengers.
import twoPlayerVillainsCageVillains from './test/fixtures/twoPlayerVillainsCageVillains.json'
import twoPlayerVillainsCrownThor from './test/fixtures/twoPlayerVillainsCrownThor.json'
// A live mixed draw from the core box and Legendary: Villains with 3 players: the Plot Crush HYDRA with the
// Mastermind Magneto, and Heroes and Allies together, under the Villains rules.
import mixedCrushHydra from './test/fixtures/mixedCrushHydra.json'
// A live draw from the core box and Legendary: Villains with 2 players whose Scheme and Mastermind are Heroic,
// so it follows the First Edition rules: Replace Earth's Leaders with Killbots and Loki, and only core cards.
import heroicOnlyWithVillains from './test/fixtures/heroicOnlyWithVillains.json'

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

// Every two-box fixture draws its familiar cards from the core box.
const CORE = 'Marvel Legendary First Edition core box'
const VILLAINS = 'Legendary: Villains'

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

  it('shows a Hero drawn outside the Hero Deck after the others, tagged with where its cards go', () => {
    renderChecklist(twoBoxesTestSong)

    expect(tileNames('Heroes')).toEqual(['Black Widow', 'Captain America', 'Cyclops', 'Deadpool', 'Emma Frost', 'Gambit'])
    expect(tile('Heroes', 'Gambit')).toHaveTextContent(new RegExp(`^Gambit${CORE}X-MenCovertInstinctRangedGoes to Villain Deck$`))
    expect(chips(tile('Heroes', 'Gambit'))).toEqual(['X-Men', 'Covert', 'Instinct', 'Ranged'])
    expect(within(section('Heroes')).getAllByText('Goes to Villain Deck')).toHaveLength(1)
  })

  it("names where an outside card goes in the setup's ruleset's words", () => {
    renderChecklist({ ...twoBoxesTestSong, ruleset: 'villainous' })

    expect(within(tile('Allies', 'Gambit')).getByText('Goes to Adversary Deck')).toBeInTheDocument()
  })

  it('tags Heroes set aside outside the Hero Deck', () => {
    renderChecklist(twoBoxesTestVault)

    expect(tileNames('Heroes').slice(-2)).toEqual(['Gambit', 'Rogue'])
    expect(within(tile('Heroes', 'Gambit')).getByText('Set aside')).toBeInTheDocument()
    expect(within(tile('Heroes', 'Rogue')).getByText('Set aside')).toBeInTheDocument()
  })

  it('shows a Henchman Group drawn outside the Villain Deck after the others, tagged with where its cards go', () => {
    renderChecklist(twoBoxesDailyBugle)

    expect(tileNames('Henchman Groups')).toEqual(['Hand Ninjas', 'Savage Land Mutates'])
    expect(tile('Henchman Groups', 'Savage Land Mutates')).toHaveTextContent(
      new RegExp(`^Savage Land Mutates${CORE}FightGoes to Hero Deck$`),
    )
    expect(tile('Henchman Groups', 'Hand Ninjas')).toHaveTextContent(new RegExp(`^Hand Ninjas${CORE}Fight$`))
  })

  it('tags no tile when the Scheme draws nothing outside the decks', () => {
    renderChecklist(killbots)

    expect(section('Drawn cards').querySelector('.tile .destination')).toBeNull()
  })

  it('renders a setup from an API that predates cards drawn outside the decks with only the drawn tiles', () => {
    // This body was captured before the API listed Heroes outside the Hero Deck.
    renderChecklist(twoPlayerCosmicCube)

    expect(tileNames('Heroes')).toHaveLength(5)
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

  it('names a Villainous setup in the Villains rulebook\'s words', () => {
    renderChecklist(twoPlayerVillainsGraduation)

    expect(screen.getAllByRole('heading', { level: 3 }).map((heading) => heading.textContent)).toEqual([
      "Graduation at Xavier's X-Academy",
      'Odin',
      'Allies',
      'Adversary Groups',
      'Backup Adversary group',
      'Plot and Commander',
      'Adversary Deck',
      'Beside the Plot',
      'Ally Deck',
      'Shared stacks',
      'Starting deck per player · 2 players',
      'Why this setup',
      'Terms in this setup',
    ])
    expect(within(card("Graduation at Xavier's X-Academy")).getByText('Plot')).toBeInTheDocument()
    expect(within(card('Odin')).getByText('Commander')).toBeInTheDocument()
    expect(rows('Adversary Deck')).toEqual([
      'Plot Twists 8',
      'Command Strikes 5',
      'Marvel Knights, X-Men First Class 2 Adversary Groups 16',
      'Asgardian Warriors 1 Backup Adversary group 10',
      'Bystanders 2',
      'Total 41',
    ])
    expect(rows('Beside the Plot')).toEqual(['Bystanders from the Bystander stack 8'])
    expect(rows('Ally Deck')).toEqual(['Ultron, Enchantress, Electro, Mystique, Magneto 5 Allies 70', 'Total 70'])
    expect(rows('Starting deck per player · 2 players')).toEqual(['HYDRA Operatives 8', 'HYDRA Soldiers 4'])
    expect(within(section('Terms in this setup')).getAllByText('Ally class')).toHaveLength(5)
    expect(within(section('Terms in this setup')).queryByText('Hero class')).not.toBeInTheDocument()
  })

  it('lays out the Villainous stacks and no Wounds or S.H.I.E.L.D. Officers', () => {
    renderChecklist(twoPlayerVillainsGraduation)

    expect(rows('Shared stacks')).toEqual(['Bindings 30', 'Madame HYDRA 12', 'New Recruits 15', 'Bystanders 31'])
  })

  it('lays out the Cops a Plot sets beside it and takes them out of the drawn Cops in the Adversary Deck', () => {
    renderChecklist(twoPlayerVillainsCageVillains)

    expect(rows('Beside the Plot')).toEqual(['Cops 4'])
    expect(rows('Adversary Deck')).toEqual([
      'Plot Twists 8',
      'Command Strikes 5',
      'Defenders, X-Men First Class 2 Adversary Groups 16',
      'Cops 1 Backup Adversary group 10',
      'Bystanders 2',
      'Cops set beside the Plot −4',
      'Total 37',
    ])
  })

  it('names the one card of a group a Plot sets beside it', () => {
    renderChecklist(twoPlayerVillainsCrownThor)

    expect(rows('Beside the Plot')).toEqual(['Thor of Avengers 1'])
    expect(rows('Adversary Deck')).toContain('Thor set beside the Plot −1')
    expect(rows('Adversary Deck')).toContain('Total 40')
  })

  it('counts the cards beside the Plot as a line to tick, and the cards set aside from the deck as none', () => {
    renderChecklist(twoPlayerVillainsCageVillains)

    expect(within(section('Beside the Plot')).getAllByRole('checkbox')).toHaveLength(1)
    const setAside = within(section('Adversary Deck')).getByText('Cops set beside the Plot').closest('li')!
    expect(within(setAside).queryByRole('checkbox')).not.toBeInTheDocument()
  })

  it("names each card of a mixed setup by its own side's word and a mixed part by both", () => {
    renderChecklist(mixedCrushHydra)

    expect(screen.getAllByRole('heading', { level: 3 }).map((heading) => heading.textContent)).toEqual([
      'Crush HYDRA',
      'Magneto',
      'Heroes or Allies',
      'Villain or Adversary Groups',
      'Henchman Group',
      'Plot and Mastermind',
      'Villain or Adversary Deck',
      'Hero or Ally Deck',
      'Shared stacks',
      'Starting deck per player · 3 players',
      'Why this setup',
      'Terms in this setup',
    ])
    expect(within(card('Crush HYDRA')).getByText('Plot')).toBeInTheDocument()
    expect(within(card('Magneto')).getByText('Mastermind')).toBeInTheDocument()
    expect(rows('Villain or Adversary Deck')).toEqual([
      'Scheme Twists or Plot Twists 8',
      'Master Strikes or Command Strikes 5',
      `Brotherhood ${CORE} Uncanny X-Men ${VILLAINS} X-Men First Class ${VILLAINS} 3 Villain or Adversary Groups 24`,
      `Hand Ninjas ${CORE} 1 Henchman Group 10`,
      'Bystanders 8',
      'Total 55',
    ])
    expect(rows('Hero or Ally Deck')[0]).toMatch(/^Sabretooth .* 5 Heroes or Allies 70$/)
  })

  it('lays out every stack of both base games in a mixed setup', () => {
    renderChecklist(mixedCrushHydra)

    expect(rows('Shared stacks')).toEqual([
      'Wounds 30',
      'Bindings 30',
      'S.H.I.E.L.D. Officers 30',
      'Madame HYDRA 12',
      'New Recruits 15',
      'Bystanders 63',
    ])
  })

  it('asks the players to pick a starting team when a mixed setup includes both base games', () => {
    renderChecklist(mixedCrushHydra)

    expect(rows('Starting deck per player · 3 players')).toEqual([
      'Pick one starting team for every player: S.H.I.E.L.D. or HYDRA',
      'S.H.I.E.L.D. Agents or HYDRA Operatives 8',
      'S.H.I.E.L.D. Troopers or HYDRA Soldiers 4',
    ])
  })

  it('says which rules the setup follows and why when the included boxes follow more than one ruleset', () => {
    renderChecklist(mixedCrushHydra)

    const reason = screen.getByText(/^Villains rules: the Plot is Crush HYDRA/)
    expect(within(reason).getByRole('link', { name: 'D-mixed' })).toHaveAttribute(
      'href',
      'https://github.com/RyanGano/LegendaryPicker/issues/88',
    )
  })

  it('lays out a Heroic-only draw with Villains included as a First Edition setup', () => {
    renderChecklist(heroicOnlyWithVillains)

    expect(screen.getByText(/^First Edition rules: no Villainous Plot or Commander/)).toBeInTheDocument()
    expect(rows('Shared stacks')).toEqual(['Wounds 30', 'S.H.I.E.L.D. Officers 30', 'Bystanders 12'])
    expect(rows('Starting deck per player · 2 players')).toEqual(['S.H.I.E.L.D. Agents 8', 'S.H.I.E.L.D. Troopers 4'])
    expect(tileNames('Heroes')).toEqual(['Thor', 'Cyclops', 'Nick Fury', 'Black Widow', 'Hulk'])
  })

  it('says nothing about rules when the included boxes follow one ruleset', () => {
    renderChecklist(twoPlayerVillainsGraduation)

    expect(screen.queryByText(/ rules: /)).not.toBeInTheDocument()
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
      `HYDRA ${CORE} Skrulls ${CORE} 2 Villain Groups 16`,
      `Sentinel ${CORE} 1 Henchman Group 10`,
      'Bystanders 2',
      'Moved to the Hero Deck −6',
      'Total 35',
    ])
    expect(rows('Hero Deck')).toEqual([
      `Rogue ${CORE} Hulk ${CORE} Emma Frost ${CORE} Black Widow ${CORE} Iron Man ${CORE} 5 Heroes 70`,
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

  it('lays out the Shard supply when a drawn card uses Shards', () => {
    renderChecklist(twoBoxesNegaBomb)

    expect(rows('Shared stacks')).toEqual(['Wounds 30', 'S.H.I.E.L.D. Officers 30', 'Bystanders 22', 'Shards 18'])
  })

  it('leaves out the Shard supply when no drawn card uses Shards', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(within(section('Shared stacks')).queryByText('Shards')).not.toBeInTheDocument()
  })

  it('lays out the Bystanders a Scheme sets aside in a section of their own, after the Villain Deck', () => {
    renderChecklist(twoBoxesNegaBomb)

    expect(rows('Set aside')).toEqual(['Bystanders from the Bystander stack 6'])
    expect(within(section('Set aside')).getAllByRole('checkbox')).toHaveLength(1)
    expect(rows('Other setup steps')).toEqual(['Shuffle the set-aside Bystanders face down as the Nega-Bomb Deck'])
    const headings = screen.getAllByRole('heading', { level: 3 }).map((heading) => heading.textContent)
    expect(headings.slice(headings.indexOf('Villain Deck'), headings.indexOf('Hero Deck') + 1)).toEqual([
      'Villain Deck',
      'Set aside',
      'Hero Deck',
    ])
  })

  it('lays out the Bindings and the Twist a Plot sets aside, and leaves the Bindings stack the rest', () => {
    renderChecklist(twoBoxesTraitor)

    expect(rows('Set aside')).toEqual(['Bindings from the Bindings stack 6', 'Plot Twists from the unused Twists 1'])
    expect(rows('Adversary Deck')).toContain('Plot Twists 8')
    expect(rows('Shared stacks')).toContain('Bindings 24')
    expect(rows('Other setup steps')).toEqual(['Shuffle the set-aside Bindings and Twist face down as the Betrayal Deck'])
  })

  it('leaves out the Set aside section when the Scheme sets no cards aside', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(screen.queryByRole('region', { name: 'Set aside' })).not.toBeInTheDocument()
  })

  it('explains Shards in the terms the setup uses', () => {
    renderChecklist(twoBoxesNegaBomb)

    const shard = within(section('Terms in this setup')).getByText('Shard').closest('li')!
    expect(shard).toHaveTextContent('A token from a shared supply.')
    expect(within(shard).getByRole('link', { name: 'GG p.1' })).toHaveAttribute(
      'href',
      'https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Guardians_of_the_Galaxy.pdf',
    )
  })

  it('leaves out the Sidekick stack when no included box has Sidekicks', () => {
    renderChecklist(legacyVirusThreePlayers)

    expect(within(section('Shared stacks')).queryByText('Sidekicks')).not.toBeInTheDocument()
  })

  it("lists a Hero outside the Hero Deck in its own section and counts its cards in the Villain Deck's total", () => {
    renderChecklist(twoBoxesTestSong)

    expect(rows('Heroes outside the Hero Deck')).toEqual([`Gambit ${CORE} to the Villain Deck 14`])
    expect(rows('Villain Deck')).toEqual([
      'Scheme Twists 8',
      'Master Strikes 5',
      `Brotherhood ${CORE} Enemies of Asgard ${CORE} 2 Villain Groups 16`,
      `Doombot Legion ${CORE} 1 Henchman Group 10`,
      'Bystanders 2',
      'Cards of the Heroes outside the Hero Deck 14',
      'Total 55',
    ])
    // The Hero is laid out, and ticked, in its own section; the Villain Deck line only adds up the total.
    expect(within(section('Heroes outside the Hero Deck')).getAllByRole('checkbox')).toHaveLength(1)
    expect(within(section('Villain Deck')).getAllByRole('checkbox')).toHaveLength(5)
  })

  it('lays out Henchmen of an extra Henchman Group in the Hero Deck and counts them in its total', () => {
    renderChecklist(twoBoxesDailyBugle)

    const ptr = 'Paint the Town Red'
    expect(rows('Hero Deck')).toEqual([
      `Black Cat ${ptr} Nick Fury ${CORE} Emma Frost ${CORE} Scarlet Spider ${ptr} Hawkeye ${CORE} 5 Heroes 70`,
      `Savage Land Mutates ${CORE} Henchmen of an extra Henchman Group 6`,
      'Total 76',
    ])
    expect(within(section('Hero Deck')).getAllByRole('checkbox')).toHaveLength(2)
    // The extra group stays out of the Villain Deck.
    expect(rows('Villain Deck')).toContain(`Hand Ninjas ${CORE} 1 Henchman Group 10`)
  })

  it('lists Heroes set aside outside the Hero Deck without adding them to a deck', () => {
    renderChecklist(twoBoxesTestVault)

    expect(rows('Heroes outside the Hero Deck')).toEqual([
      `Gambit ${CORE} to a stack set aside 14`,
      `Rogue ${CORE} to a stack set aside 14`,
    ])
    expect(rows('Villain Deck')).not.toContain('Cards of the Heroes outside the Hero Deck 0')
    expect(rows('Villain Deck')).toContain('Total 41')
  })

  it('renders a setup from an API that predates Heroes outside the Hero Deck as having none', () => {
    // This body was captured before the API listed Heroes outside the Hero Deck.
    renderChecklist(twoPlayerCosmicCube)

    expect(screen.queryByRole('region', { name: 'Heroes outside the Hero Deck' })).not.toBeInTheDocument()
  })

  it('renders a setup from an API that predates moves as having none', () => {
    // The site and the API deploy separately, so the site can briefly talk to an API without moves.
    const { moves: _moves, ...withoutMoves } = soloSecretInvasion
    renderChecklist(withoutMoves)

    expect(rows('Hero Deck')).toEqual(['Deadpool, Hulk, Cyclops, Storm, Black Widow, Iron Man 6 Heroes 84', 'Total 72'])
  })

  it('heads the setup steps like the notes, with no card type accent, since a step can come from either card', () => {
    renderChecklist(twoBoxesTestVigil)

    expect(section('Other setup steps')).not.toHaveClass('scheme')
    expect(section('Other setup steps')).not.toHaveClass('mastermind')
    expect(section('Other setup steps').className).toBe(section('Why this setup').className)
  })

  it('lists the setup steps the Scheme and Mastermind print as lines to tick, after the starting decks', async () => {
    const user = userEvent.setup()
    renderChecklist(twoBoxesTestVigil)

    expect(rows('Other setup steps')).toEqual([
      'Place the test beacon token on the Scheme',
      'Split the Villain Deck into two equal piles',
      'Put the test lookout token on the first city space',
    ])
    const headings = screen.getAllByRole('heading', { level: 3 }).map((heading) => heading.textContent)
    expect(headings.slice(headings.indexOf('Starting deck per player · 2 players'))).toEqual([
      'Starting deck per player · 2 players',
      'Other setup steps',
      'Why this setup',
      'Terms in this setup',
    ])
    expect(screen.getByRole('status')).toHaveTextContent('0 of 16 laid out')

    await user.click(screen.getByRole('checkbox', { name: 'Split the Villain Deck into two equal piles' }))

    expect(screen.getByRole('status')).toHaveTextContent('1 of 16 laid out')
    expect(rows('Why this setup')).toContain(
      'Scheme adds a setup step: Split the Villain Deck into two equal piles R p.2Steps Fixture',
    )
  })

  it('renders a setup from an API that predates setup steps as having none', () => {
    // This body was captured before the API listed setup steps.
    renderChecklist(twoBoxesTestSong)

    expect(screen.queryByRole('region', { name: 'Other setup steps' })).not.toBeInTheDocument()
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

  it('names the box each drawn card comes from when the setup includes more than one box', () => {
    renderChecklist(twoBoxesTestHeist)

    expect(within(card('Test Heist')).getByText('Fixture Expansion')).toHaveClass('source-box')
    expect(within(card('Test Tyrant')).getByText('Fixture Expansion')).toHaveClass('source-box')
    expect(tile('Villain Groups', 'HYDRA')).toHaveTextContent(new RegExp(`^HYDRA${CORE}`))
    expect(tile('Villain Groups', 'Test Cult')).toHaveTextContent(/^Test CultFixture Expansion/)
    expect(tile('Heroes', 'Storm')).toHaveTextContent(new RegExp(`^Storm${CORE}`))
    expect(rows('Scheme and Mastermind')).toEqual(['Test Heist Fixture Expansion Scheme', 'Test Tyrant Fixture Expansion Mastermind'])
    expect(within(section('Villain Deck')).getAllByRole('checkbox')[2]).toHaveAccessibleName(
      `HYDRA ${CORE} Test Cult Fixture Expansion 2 Villain Groups 16`,
    )
  })

  it('leaves the box off every drawn card when the setup includes one box', () => {
    renderChecklist(twoPlayerCosmicCube)

    expect(screen.queryByText(CORE)).not.toBeInTheDocument()
    expect(document.querySelector('.source-box')).toBeNull()
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
