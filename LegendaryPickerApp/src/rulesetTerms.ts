import type { Component, Ruleset, Setup } from './api/setupApi.ts'

// The words a ruleset's rulebook uses for the parts of a setup. The API keeps the First Edition names
// in its fields; a Villainous setup calls the same parts by the Villains rulebook's equivalent terms
// (an Ally is a Hero, an Adversary Group a Villain Group, a Plot a Scheme, and so on), so the page does too.
// team is the team the ruleset's starting deck belongs to.
export type RulesetTerms = {
  scheme: string
  mastermind: string
  hero: string
  heroes: string
  villainGroup: string
  villainGroups: string
  henchmanGroup: string
  henchmanGroups: string
  henchmen: string
  villainDeck: string
  heroDeck: string
  twists: string
  masterStrikes: string
  team: string
  agents: string
  troopers: string
}

const TERMS: Record<Ruleset, RulesetTerms> = {
  firstEdition: {
    scheme: 'Scheme',
    mastermind: 'Mastermind',
    hero: 'Hero',
    heroes: 'Heroes',
    villainGroup: 'Villain Group',
    villainGroups: 'Villain Groups',
    henchmanGroup: 'Henchman Group',
    henchmanGroups: 'Henchman Groups',
    henchmen: 'Henchmen',
    villainDeck: 'Villain Deck',
    heroDeck: 'Hero Deck',
    twists: 'Scheme Twists',
    masterStrikes: 'Master Strikes',
    team: 'S.H.I.E.L.D.',
    agents: 'S.H.I.E.L.D. Agents',
    troopers: 'S.H.I.E.L.D. Troopers',
  },
  villainous: {
    scheme: 'Plot',
    mastermind: 'Commander',
    hero: 'Ally',
    heroes: 'Allies',
    villainGroup: 'Adversary Group',
    villainGroups: 'Adversary Groups',
    henchmanGroup: 'Backup Adversary group',
    henchmanGroups: 'Backup Adversary groups',
    henchmen: 'Backup Adversaries',
    villainDeck: 'Adversary Deck',
    heroDeck: 'Ally Deck',
    twists: 'Plot Twists',
    masterStrikes: 'Command Strikes',
    team: 'HYDRA',
    agents: 'HYDRA Operatives',
    troopers: 'HYDRA Soldiers',
  },
}

// A mixed setup's cards come from both rulesets, so a part that isn't one card is named by both words, as
// the API's rule notes do. Its starting deck is named from the setup's choices instead.
const MIXED: RulesetTerms = {
  scheme: 'Scheme or Plot',
  mastermind: 'Mastermind or Commander',
  hero: 'Hero or Ally',
  heroes: 'Heroes or Allies',
  villainGroup: 'Villain or Adversary Group',
  villainGroups: 'Villain or Adversary Groups',
  henchmanGroup: 'Henchman or Backup Adversary group',
  henchmanGroups: 'Henchman or Backup Adversary groups',
  henchmen: 'Henchmen or Backup Adversaries',
  villainDeck: 'Villain or Adversary Deck',
  heroDeck: 'Hero or Ally Deck',
  twists: 'Scheme Twists or Plot Twists',
  masterStrikes: 'Master Strikes or Command Strikes',
  team: 'S.H.I.E.L.D. or HYDRA',
  agents: 'S.H.I.E.L.D. Agents or HYDRA Operatives',
  troopers: 'S.H.I.E.L.D. Troopers or HYDRA Soldiers',
}

// The words for the setup as a whole: its ruleset's, or both rulesets' when its cards are mixed. An API from
// before rulesets leaves the field out; every setup it drew was First Edition.
export function rulesetTerms(setup: Setup): RulesetTerms {
  return setup.mixed ? MIXED : TERMS[setup.ruleset ?? 'firstEdition']
}

// The words for some drawn cards: their own ruleset's when they share one, so a mixed setup still calls a
// Plot a Plot, or the setup's words otherwise.
export function cardTerms(setup: Setup, components: Component[]): RulesetTerms {
  const rulesets = new Set(components.map((component) => component.ruleset))
  const [only] = rulesets
  return rulesets.size === 1 && only ? TERMS[only] : rulesetTerms(setup)
}

// The setup's words, with its one Scheme and one Mastermind each named by their own ruleset's word.
export function setupTerms(setup: Setup): RulesetTerms {
  return {
    ...rulesetTerms(setup),
    scheme: cardTerms(setup, [setup.scheme]).scheme,
    mastermind: cardTerms(setup, [setup.mastermind]).mastermind,
  }
}

// The starting decks' words: each choice's in a mixed setup that includes base games of both rulesets,
// otherwise the setup's ruleset's.
export function startingDeckTerms(setup: Setup): RulesetTerms[] {
  const choices = setup.playerDeck.choices ?? []
  return choices.length > 0 ? choices.map((ruleset) => TERMS[ruleset]) : [TERMS[setup.ruleset ?? 'firstEdition']]
}
