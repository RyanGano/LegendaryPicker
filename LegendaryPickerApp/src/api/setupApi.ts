// Typed client for LegendaryPickerService. The API is the rules authority:
// the app only asks it for a setup and never builds one itself.

// The types mirror LegendaryPickerService/Setup/SetupResponse.cs, serialized camelCase.

// A chosen Scheme, Mastermind, group or Hero: its catalog id, display name, and the ids of the
// glossary terms it uses (for a Hero, its team and classes too), in glossary order. box names the box
// it comes from, and is present only when the setup includes more than one box. ruleset is its box's
// ruleset, present only in a mixed setup. notIncluded is true only for a card the Scheme requires from a box
// the setup doesn't include, which the player owns; box is then always present.
export type Component = {
  id: string
  name: string
  terms: string[]
  box?: string
  ruleset?: Ruleset
  notIncluded?: boolean
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

// Each deck's total counts the cards the setup's moves put in it and take out of it. outsideHeroCards
// counts the cards of the Heroes outside the Hero Deck that go into the Villain Deck; an API from
// before them leaves it out. mastermindTactics counts the Tactics of other Masterminds the Scheme shuffles in,
// present only when there are any. ownTactics counts the drawn Mastermind's own Tactics the Scheme shuffles in, also
// present only when there are any.
export type VillainDeck = {
  twists: number
  masterStrikes: number
  villainCards: number
  henchmanCards: number
  bystanders: number
  outsideHeroCards?: number
  mastermindTactics?: number
  ownTactics?: number
  total: number
}

// total counts the Henchmen of the Henchman Groups outside the Villain Deck that go into the Hero Deck.
export type HeroDeck = {
  heroCards: number
  total: number
}

export type CardKind = 'hero' | 'henchman' | 'bystander' | 'wound' | 'officer' | 'sidekick' | 'binding' | 'twist' | 'ambition'

// The destinations a move can put cards in, among them a stack set aside, which Heroes outside the Hero
// Deck can go to as well, then the shared stacks a move can take cards from and the Twists the Villain Deck doesn't
// use. Hero cards come from the Hero Deck and Henchmen from the Villain Deck.
export type Pile =
  | 'villainDeck'
  | 'heroDeck'
  | 'besideScheme'
  | 'startingDecks'
  | 'setAside'
  | 'bystanders'
  | 'wounds'
  | 'officers'
  | 'sidekicks'
  | 'bindings'
  | 'twists'
  | 'ambitions'
  | 'koPile'

// Cards a Scheme moves during setup. count is what the destination gets (into the starting decks,
// what each player's deck gets); total is what leaves from.
export type Move = {
  card: CardKind
  from: Pile
  to: Pile
  count: number
  total: number
}

// A Hero a Scheme draws outside the Hero Deck: all its cards go to one pile, the Villain Deck, beside
// the Scheme, or a stack set aside.
export type OutsideHero = {
  hero: Component
  to: Pile
  cards: number
}

// A Henchman Group a Scheme draws outside the Villain Deck: cards of its Henchmen go to one pile (the
// Hero Deck), and the rest of the group stays out of the game.
// to is the Hero Deck, or the KO pile.
export type OutsideHenchmen = {
  group: Component
  to: Pile
  cards: number
}

// A Mastermind a Scheme draws besides its own: set aside whole until the Scheme brings it into play, or tactics of
// its Tactics into the Villain Deck, where they play as Villains.
export type OutsideMastermind = {
  mastermind: Component
  to: Pile
  tactics?: number
  // When a set-aside Mastermind comes into play ("Twist 1", "Twists 1-3"), when the Scheme says.
  joins?: string
}

// Cards of a group the Scheme sets beside it, whether or not the group is drawn: count of them, or, when card is
// present, that one card of the group. fromVillainDeck is how many fewer cards the group puts in the Villain Deck
// because of it (0 when it isn't drawn there); the Villain Deck's total already leaves them out.
export type CardsBeside = {
  group: Component
  card?: string
  count: number
  fromVillainDeck: number
}

// A stack is left out when nothing in the setup uses it: its rules use their recruit stacks (S.H.I.E.L.D.
// Officers, or Madame HYDRA and New Recruits), and a drawn card brings any other stack it uses, the Shard
// supply and X-Men's Horrors included. Every setup lays out Bystanders.
export type SetupStacks = {
  wounds?: number
  officers?: number
  bystanders: number
  sidekicks?: number
  bindings?: number
  madameHydra?: number
  newRecruits?: number
  shards?: number
  horrors?: number
}

// A part the drawn cards use that no included box supplies, and the part that stands in for it, null when none
// does (a New Recruit gain gives +1 Recruit instead). Only stacks with a tick box have a stand-in to name.
export type StandIn = {
  part: keyof SetupStacks
  source: string
  with: keyof SetupStacks | null
}

// The two kinds of starting card each player gets: S.H.I.E.L.D. Agents and Troopers, or under the
// Villainous ruleset HYDRA Operatives and Soldiers. choices lists the rulesets whose starting decks the
// players choose between, present only in a mixed setup under the Villains rules that includes base games
// of both.
export type PlayerDeck = {
  agents: number
  troopers: number
  choices?: Ruleset[]
}

// The rules a box's cards are played under. A setup follows one ruleset, which its Scheme and Mastermind
// decide when the included boxes follow more than one.
export type Ruleset = 'firstEdition' | 'villainous'

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
  // Left out by an API from before rulesets, which drew First Edition setups only.
  ruleset?: Ruleset
  // Present, and true, only when the drawn cards come from more than one ruleset.
  mixed?: boolean
  // Why the setup follows its ruleset, present only when the included boxes follow more than one.
  rulesReason?: RuleNote
  // Present only when a drawn card uses a part no included box supplies, such as Bindings with no Villains box.
  standIns?: StandIn[]
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
  // Left out by an API from before Heroes outside the Hero Deck.
  outsideHeroes?: OutsideHero[]
  // Left out by an API from before Henchman Groups outside the Villain Deck.
  outsideHenchmen?: OutsideHenchmen[]
  // Left out by an API from before cards of a group beside the Scheme.
  cardsBeside?: CardsBeside[]
  // Present only when the Scheme draws Masterminds besides its own.
  outsideMasterminds?: OutsideMastermind[]
  // The setup steps the Scheme and Mastermind print that change no count, Scheme first, each a short
  // instruction. Left out by an API from before setup steps.
  steps?: string[]
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

// A box a setup can include. A base game supplies the setup rules; an expansion adds cards. mixesRulesets is true
// for a base game with rules for mixing rulesets, which a setup with boxes of more than one ruleset needs, unless
// every box of the ruleset with no ticked base game playsWithOtherRulesets: an expansion that can be played under the
// other ruleset's base game. An API from before rulesets leaves ruleset out, and one from before mixesRulesets or
// playsWithOtherRulesets leaves that out.
export type Box = {
  id: string
  name: string
  baseGame: boolean
  ruleset?: Ruleset
  mixesRulesets?: boolean
  playsWithOtherRulesets?: boolean
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
