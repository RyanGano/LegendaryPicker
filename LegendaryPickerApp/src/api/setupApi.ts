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
  // The kind the Scheme names when card stands in for it because no included box supplies it (#258).
  standsInFor?: CardKind
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

// What revealing a Mastermind that joins mid-game shows with it: the groups it always leads, which are not in the
// Villain Deck, notes for the Start of Game effects its card prints, the Scheme's note on when it joins, and the ids
// of the glossary terms only it and those groups bring, which stay out of the glossary until the reveal.
export type MastermindReveal = {
  alwaysLeads: Component[]
  notes: RuleNote[]
  joins: string
  terms: string[]
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

// The Villains of an extra Villain Group a Mastermind sets aside, which the Villain Deck total leaves out.
export type VillainsSetAside = {
  group: Component
  count: number
}

// The special cards one included box shuffles into a shared stack the setup lays out: stack is its key in the setup's
// stacks, box the box's name and cards their printed names.
export type SpecialCards = {
  stack: keyof SetupStacks
  box: string
  cards: string[]
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
// of both. team is the ruleset whose starting cards the players use, present only when the setup's rules come from a
// base game that isn't included and the included one standing for it deals another team's.
export type PlayerDeck = {
  agents: number
  troopers: number
  choices?: Ruleset[]
  team?: Ruleset
}

// The rules a box's cards are played under. A setup follows one ruleset: the rule edition the player chose, or
// else the one its boxes, or its Scheme and Mastermind, decide when the included boxes follow more than one.
export type Ruleset = 'firstEdition' | 'villainous' | 'revised'

// A rule edition the player can choose for the whole setup, as the API's rules= names it.
export type RuleEdition = 'first-edition' | 'villains' | 'revised'

// box names the box the rule comes from, and is present only when the setup includes more than one box.
export type RuleNote = {
  text: string
  citation: string
  link: string | null
  box?: string
}

// How hard the drawn Hero Deck is to win with: rating 1 (easiest) to 5, the band's display name, and up to three
// reasons in plain words. Left out by an API from before the rating, and never says anything about balance.
// target is the rating the request asked for, present only when it asked for one (#267).
export type Difficulty = {
  rating: number
  band: string
  reasons: string[]
  target?: number
}

// How much there is to learn and remember in the drawn setup: rating 1 (simplest) to 5, the band's display name, and up
// to three reasons in plain words. Independent of the difficulty. Left out by an API from before the rating.
export type Complexity = {
  rating: number
  band: string
  reasons: string[]
  target?: number
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
  // Present only when the Scheme adds its Mastermind mid-game: the app hides mastermind until the player reveals it.
  mastermindReveal?: MastermindReveal
  // Present only when the Scheme draws Masterminds besides its own.
  outsideMasterminds?: OutsideMastermind[]
  // Present only when the Mastermind sets aside the Villains of an extra Villain Group.
  villainsSetAside?: VillainsSetAside[]
  // Present only when an included box shuffles special cards into a stack the setup lays out.
  specialCards?: SpecialCards[]
  // The setup steps the Scheme and Mastermind print that change no count, Scheme first, each a short
  // instruction. Left out by an API from before setup steps.
  steps?: string[]
  // Present only when the players choose Heroes of the Hero Deck: the last this many of heroes are fill-ins, used only in place
  // of an earlier listed Hero a player chose.
  fillInHeroes?: number
  notes: RuleNote[]
  // One entry per term any drawn component uses: teams, then classes, then keywords.
  glossary: GlossaryEntry[]
  difficulty?: Difficulty
  complexity?: Complexity
}

// A valid answer, not an error: no Scheme can be set up legally at this count.
export type NoEligibleScheme = {
  kind: 'noEligibleScheme'
  players: number
  message: string
}

export type SetupResponse = Setup | NoEligibleScheme

// A box a setup can include. A base game supplies the setup rules; an expansion adds cards. An API from before
// rulesets leaves ruleset out.
export type Box = {
  id: string
  name: string
  baseGame: boolean
  ruleset?: Ruleset
  firstPublished?: number
  description?: string
}

// The band names of each rating, index 0 for rating 1, as the API's scorers name them.
export type RatingBands = {
  difficulty: string[]
  complexity: string[]
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

// The band names the difficulty and complexity pickers list. Throws like getBoxes.
export async function getRatingBands(): Promise<RatingBands> {
  const controller = new AbortController()
  const timeout = setTimeout(() => controller.abort(), REQUEST_TIMEOUT_MS)
  try {
    const response = await fetch(`${apiBaseUrl}/api/ratings`, { signal: controller.signal })
    if (!response.ok) {
      throw new Error(`GET /api/ratings returned ${response.status}.`)
    }
    return (await response.json()) as RatingBands
  } finally {
    clearTimeout(timeout)
  }
}

// Draws from the named boxes, or from the core box alone when boxes is empty. With epic, a drawn Mastermind that has
// an Epic side is played on that side. With rules, the whole setup follows that rule edition; without, the boxes decide.
// With difficulty or complexity (1 to 5), the draw leans toward that rating; without, the draw is plain.
// Throws on a network failure, a non-2xx status (bad count or boxes, rate limit, server error),
// an unrecognised body, or no answer within REQUEST_TIMEOUT_MS.
export async function getSetup(
  players: number,
  boxes: string[] = [],
  epic = false,
  rules: RuleEdition | null = null,
  difficulty: number | null = null,
  complexity: number | null = null,
): Promise<SetupResponse> {
  const controller = new AbortController()
  const timeout = setTimeout(() => controller.abort(), REQUEST_TIMEOUT_MS)
  const query =
    (boxes.length > 0 ? `&boxes=${boxes.map(encodeURIComponent).join(',')}` : '') +
    (epic ? '&epic=true' : '') +
    (rules ? `&rules=${rules}` : '') +
    (difficulty ? `&difficulty=${difficulty}` : '') +
    (complexity ? `&complexity=${complexity}` : '')
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
