// Typed client for LegendaryPickerService. The API is the rules authority:
// the app only asks it for a setup and never builds one itself.

// The types mirror LegendaryPickerService/Setup/SetupResponse.cs, serialized camelCase.

// A chosen Scheme, Mastermind, group or Hero: its catalog id, display name, and the ids of the
// glossary terms it uses (for a Hero, its team and classes too), in glossary order.
export type Component = {
  id: string
  name: string
  terms: string[]
}

// One glossary term the setup uses: an original short summary, cited by source key and page
// (for example "R p.18"), with the link where that source is published. box names the box that
// defines the term, and is present only when the setup includes more than one box.
export type GlossaryEntry = {
  id: string
  name: string
  kind: 'team' | 'class' | 'keyword'
  summary: string
  citation: string
  link: string
  box?: string
}

// Each deck's total counts the cards the setup's moves put in it and take out of it.
export type VillainDeck = {
  twists: number
  masterStrikes: number
  villainCards: number
  henchmanCards: number
  bystanders: number
  total: number
}

export type HeroDeck = {
  heroCards: number
  total: number
}

export type CardKind = 'hero' | 'henchman' | 'bystander' | 'wound' | 'officer' | 'sidekick'

// The destinations a move can put cards in, then the shared stacks it can take them from. Hero cards
// come from the Hero Deck and Henchmen from the Villain Deck.
export type Pile =
  | 'villainDeck'
  | 'heroDeck'
  | 'besideScheme'
  | 'startingDecks'
  | 'bystanders'
  | 'wounds'
  | 'officers'
  | 'sidekicks'

// Cards a Scheme moves during setup. count is what the destination gets (into the starting decks,
// what each player's deck gets); total is what leaves from.
export type Move = {
  card: CardKind
  from: Pile
  to: Pile
  count: number
  total: number
}

export type SetupStacks = {
  wounds: number
  officers: number
  bystanders: number
}

export type PlayerDeck = {
  agents: number
  troopers: number
}

// box names the box the rule comes from, and is present only when the setup includes more than one box.
export type RuleNote = {
  text: string
  citation: string
  link: string | null
  box?: string
}

export type Setup = {
  kind: 'setup'
  players: number
  scheme: Component
  mastermind: Component
  villainGroups: Component[]
  henchmanGroups: Component[]
  heroes: Component[]
  villainDeck: VillainDeck
  heroDeck: HeroDeck
  twistsBesideScheme: number
  stacks: SetupStacks
  playerDeck: PlayerDeck
  // Left out by an API from before card moves.
  moves?: Move[]
  notes: RuleNote[]
  // One entry per term any drawn component uses: teams, then classes, then keywords.
  glossary: GlossaryEntry[]
}

// A valid answer, not an error: no Scheme can be set up legally at this count.
export type NoEligibleScheme = {
  kind: 'noEligibleScheme'
  players: number
  message: string
}

export type SetupResponse = Setup | NoEligibleScheme

// A box a setup can include. A base game supplies the setup rules; an expansion adds cards.
export type Box = {
  id: string
  name: string
  baseGame: boolean
}

// A sleeping App Service instance takes 10-30 seconds to wake, so give up well after that.
export const REQUEST_TIMEOUT_MS = 45_000

const apiBaseUrl = (import.meta.env.VITE_API_BASE_URL ?? '').replace(/\/+$/, '')

// Wakes the API. Never throws: the result is irrelevant and a failure here must not reach the page.
export async function ping(): Promise<void> {
  try {
    await fetch(`${apiBaseUrl}/api/health`)
  } catch {
    // The setup request reports any real outage.
  }
}

// The boxes the API holds. Throws on a network failure, a non-2xx status, or no answer
// within REQUEST_TIMEOUT_MS.
export async function getBoxes(): Promise<Box[]> {
  const controller = new AbortController()
  const timeout = setTimeout(() => controller.abort(), REQUEST_TIMEOUT_MS)
  try {
    const response = await fetch(`${apiBaseUrl}/api/boxes`, { signal: controller.signal })
    if (!response.ok) {
      throw new Error(`GET /api/boxes returned ${response.status}.`)
    }
    return (await response.json()) as Box[]
  } finally {
    clearTimeout(timeout)
  }
}

// Draws from the named boxes, or from the core box alone when boxes is empty.
// Throws on a network failure, a non-2xx status (bad count or boxes, rate limit, server error),
// an unrecognised body, or no answer within REQUEST_TIMEOUT_MS.
export async function getSetup(players: number, boxes: string[] = []): Promise<SetupResponse> {
  const controller = new AbortController()
  const timeout = setTimeout(() => controller.abort(), REQUEST_TIMEOUT_MS)
  const query = boxes.length > 0 ? `&boxes=${boxes.map(encodeURIComponent).join(',')}` : ''
  try {
    const response = await fetch(`${apiBaseUrl}/api/setup?players=${players}${query}`, {
      signal: controller.signal,
    })
    if (!response.ok) {
      throw new Error(`GET /api/setup returned ${response.status}.`)
    }
    const body = (await response.json()) as SetupResponse
    if (body.kind !== 'setup' && body.kind !== 'noEligibleScheme') {
      throw new Error('GET /api/setup returned an unknown result.')
    }
    return body
  } finally {
    clearTimeout(timeout)
  }
}
