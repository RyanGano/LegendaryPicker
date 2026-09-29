import type { Ruleset, Setup } from './api/setupApi.ts'

// The words a ruleset's rulebook uses for the parts of a setup. The API keeps the First Edition names
// in its fields; a Villainous setup calls the same parts by the Villains rulebook's equivalent terms
// (an Ally is a Hero, an Adversary Group a Villain Group, a Plot a Scheme, and so on), so the page does too.
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
    agents: 'HYDRA Operatives',
    troopers: 'HYDRA Soldiers',
  },
}

// An API from before rulesets leaves the field out; every setup it drew was First Edition.
export function rulesetTerms(setup: Setup): RulesetTerms {
  return TERMS[setup.ruleset ?? 'firstEdition']
}
