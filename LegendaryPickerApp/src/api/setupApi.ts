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
// (for example "R p.18"), with the link where that source is published.
export type GlossaryEntry = {
  id: string
  name: string
  kind: 'team' | 'class' | 'keyword'
  summary: string
  citation: string
  link: string
}

export type VillainDeck = {
  twists: number
  masterStrikes: number
  villainCards: number
  henchmanCards: number
  bystanders: number
  heroCards: number
  total: number
}

export type HeroDeck = {
  heroCards: number
  movedToVillainDeck: number
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

export type RuleNote = {
  text: string
  citation: string
  link: string | null
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

// Throws on a network failure, a non-2xx status (bad count, rate limit, server error),
// an unrecognised body, or no answer within REQUEST_TIMEOUT_MS.
export async function getSetup(players: number): Promise<SetupResponse> {
  const controller = new AbortController()
  const timeout = setTimeout(() => controller.abort(), REQUEST_TIMEOUT_MS)
  try {
    const response = await fetch(`${apiBaseUrl}/api/setup?players=${players}`, {
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
