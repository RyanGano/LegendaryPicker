import { useEffect, useRef, useState } from 'react'
import { BoxInfo } from './BoxInfo.tsx'
import { ChosenCards } from './ChosenCards.tsx'
import {
  getBoxes,
  getCards,
  getLimits,
  getRatingBands,
  getSetup,
  getSetupByCode,
  ping,
  SetupCodeError,
  type Box,
  type Card,
  type ChoiceLimits,
  type RatingBands,
  type RuleEdition,
  type Setup,
} from './api/setupApi.ts'
import { RatingPicker } from './RatingPicker.tsx'
import { setupTerms } from './rulesetTerms.ts'
import { SavedSetups } from './SavedSetups.tsx'
import { loadSavedSetups, storeSavedSetups, withSaved, type SavedSetup } from './savedSetups.ts'
import { SetupChecklist } from './SetupChecklist.tsx'
import { SetupCode } from './SetupCode.tsx'
import { SetupSkeleton } from './SetupSkeleton.tsx'
import { SetupSummary } from './SetupSummary.tsx'

const PLAYER_COUNTS = [1, 2, 3, 4, 5]

// The two-column layout in index.css, where the setup starts at the top of the column beside the controls.
const TWO_COLUMNS = '(width >= 56.25rem)'

// A sleeping API answers slowly; past this point the loading state says so.
const WAKE_UP_NOTICE_MS = 3_000

const PLAYER_COUNT_KEY = 'legendaryPicker.playerCount'
// Every box the player included, base games and expansions alike.
const BOXES_KEY = 'legendaryPicker.boxes'
// Whether the player plays the Masterminds' Epic sides (#185).
const EPIC_KEY = 'legendaryPicker.epic'
// The rule edition the player chose for boxes of more than one ruleset, or none for the boxes' default (#240).
const RULES_KEY = 'legendaryPicker.rules'
// The difficulty and complexity the player asked the draw to lean toward, or none for any (#271).
const DIFFICULTY_KEY = 'legendaryPicker.difficulty'
const COMPLEXITY_KEY = 'legendaryPicker.complexity'
// The ids of the cards the player chose to be in the game (#337).
const CHOSEN_KEY = 'legendaryPicker.chosen'
// The rule editions a player can choose, after the default.
const RULE_EDITIONS: { id: RuleEdition; name: string; detail?: string }[] = [
  { id: 'first-edition', name: 'First Edition' },
  { id: 'villains', name: 'Villains' },
  { id: 'revised', name: 'Second Edition', detail: 'Also What If...?' },
]
// The expansions a returning player included before base games became picks (#71). The core box was
// always in the setup then, so it joins them when they move to BOXES_KEY.
const OLD_EXPANSIONS_KEY = 'legendaryPicker.expansions'
// What a first visit includes: the First Edition core box, one base game.
const DEFAULT_BOXES = ['core']

// Source R in LegendaryPickerService/Data/Boxes/core.json: the archived First Edition rulebook.
const RULEBOOK_URL =
  'https://web.archive.org/web/20130127000000id_/http://upperdeck.com/Checklist/Legendary_Rulebook_FINAL.pdf'
// Source VIL in LegendaryPickerService/Data/Boxes/villains.json: the Legendary: Villains rulebook.
const VILLAINS_RULEBOOK_URL = 'https://upperdeck.com/wp-content/uploads/2024/05/Legendary_Rules-Villains.pdf'
// Source WI in LegendaryPickerService/Data/Boxes/marvel-studios-what-if.json: the What If...? rulebook.
const WHAT_IF_RULEBOOK_URL = 'https://upperdeck.com/wp-content/uploads/2024/05/WhatIf_Rulebook.pdf'
// Source SE in LegendaryPickerService/Data/Boxes/second-edition.json: the Second Edition rulebook.
const SECOND_EDITION_RULEBOOK_URL = 'https://upperdeck.com/wp-content/uploads/2026/08/Legendary-Second-Edition-Rulebook.pdf'
const REPO_URL = 'https://github.com/RyanGano/LegendaryPicker'
// The query parameter of a link that reloads a setup from its code (#335).
const CODE_PARAM = 'code'

// The setup code a link opened the page with, taken out of the address so a reload or a later draw doesn't bring it back.
function takeLinkedCode(): string | null {
  const url = new URL(window.location.href)
  const code = url.searchParams.get(CODE_PARAM)
  if (code === null) return null
  url.searchParams.delete(CODE_PARAM)
  window.history.replaceState(window.history.state, '', url)
  return code.trim() || null
}

// Storage is a convenience: private browsing or blocked site data must not break the page.
function loadPlayerCount(): number | null {
  try {
    const stored = Number(localStorage.getItem(PLAYER_COUNT_KEY))
    return PLAYER_COUNTS.includes(stored) ? stored : null
  } catch {
    return null
  }
}

function savePlayerCount(players: number) {
  try {
    localStorage.setItem(PLAYER_COUNT_KEY, String(players))
  } catch {
    // Remembering the count is optional.
  }
}

function loadBoxes(): string[] {
  try {
    migrateExpansions()
    return readIds(BOXES_KEY) ?? DEFAULT_BOXES
  } catch {
    return DEFAULT_BOXES
  }
}

// Moves a returning player's expansions from the old key to the new one once, with the core box they
// always had, then drops the old key. Picks already under the new key win.
function migrateExpansions() {
  const expansions = readIds(OLD_EXPANSIONS_KEY)
  if (expansions && localStorage.getItem(BOXES_KEY) === null) {
    saveBoxes([...DEFAULT_BOXES, ...expansions.filter((id) => !DEFAULT_BOXES.includes(id))])
  }
  localStorage.removeItem(OLD_EXPANSIONS_KEY)
}

// A stored list of box ids, or null when there is none or it isn't a list.
function readIds(key: string): string[] | null {
  try {
    const stored: unknown = JSON.parse(localStorage.getItem(key) ?? 'null')
    return Array.isArray(stored) ? stored.filter((id): id is string => typeof id === 'string') : null
  } catch {
    return null
  }
}

// Why the ticked boxes can't be drawn from, or null when they can. A setup takes its rules from a base game, so it
// needs one; any boxes with one can be drawn together (#240). Until the box list loads there is nothing to check, and
// the API decides.
function boxHint(available: Box[], included: string[]): string | null {
  const ticked = available.filter((box) => included.includes(box.id))
  return available.length > 0 && !ticked.some((box) => box.baseGame) ? 'Pick a base game to draw a setup.' : null
}

// Whether the ticked boxes follow more than one ruleset, so the player can choose which rule edition the setup uses.
function spansRulesets(available: Box[], included: string[]): boolean {
  return new Set(available.filter((box) => included.includes(box.id)).map((box) => box.ruleset)).size > 1
}

function loadRules(): RuleEdition | null {
  try {
    const stored = localStorage.getItem(RULES_KEY)
    return RULE_EDITIONS.some((edition) => edition.id === stored) ? (stored as RuleEdition) : null
  } catch {
    return null
  }
}

function saveRules(rules: RuleEdition | null) {
  try {
    if (rules === null) localStorage.removeItem(RULES_KEY)
    else localStorage.setItem(RULES_KEY, rules)
  } catch {
    // Remembering the choice is optional.
  }
}

function loadRating(key: string): number | null {
  try {
    const stored = Number(localStorage.getItem(key))
    return Number.isInteger(stored) && stored >= 1 && stored <= 5 ? stored : null
  } catch {
    return null
  }
}

function saveRating(key: string, rating: number | null) {
  try {
    if (rating === null) localStorage.removeItem(key)
    else localStorage.setItem(key, String(rating))
  } catch {
    // Remembering the choice is optional.
  }
}

function loadEpic(): boolean {
  try {
    return localStorage.getItem(EPIC_KEY) === 'true'
  } catch {
    return false
  }
}

function saveEpic(epic: boolean) {
  try {
    localStorage.setItem(EPIC_KEY, String(epic))
  } catch {
    // Remembering the option is optional.
  }
}

function loadChosen(): string[] {
  return readIds(CHOSEN_KEY) ?? []
}

function saveChosen(ids: string[]) {
  try {
    localStorage.setItem(CHOSEN_KEY, JSON.stringify(ids))
  } catch {
    // Remembering the chosen cards is optional.
  }
}

// The chosen cards with card added: a card of a kind a setup has one of, a Scheme or a Mastermind, takes the place of
// the one chosen before (#337).
function withChosen(cards: Card[], chosen: string[], card: Card): string[] {
  const single = card.kind === 'scheme' || card.kind === 'mastermind'
  const kept = single ? chosen.filter((id) => cards.find((other) => other.id === id)?.kind !== card.kind) : chosen
  return [...kept, card.id]
}

function saveBoxes(ids: string[]) {
  try {
    localStorage.setItem(BOXES_KEY, JSON.stringify(ids))
  } catch {
    // Remembering the boxes is optional.
  }
}

// A loading state without players is a setup code being reloaded. codeError is a code the API can't reload, with why.
type Status =
  | { kind: 'idle' }
  | { kind: 'loading'; players?: number }
  | { kind: 'result'; setup: Setup }
  | { kind: 'noEligibleScheme'; message: string }
  | { kind: 'chosenCardsConflict'; message: string }
  | { kind: 'codeError'; message: string }
  | { kind: 'error' }

function playerCount(players: number) {
  return players === 1 ? '1 player' : `${players} players`
}

// What the live region reads out for each state. The loading and result lines are short on purpose:
// the checklist itself is read once the player moves into it, starting from the focused heading. That
// heading already says the player count, so the result line leaves it out rather than say it twice.
function announcement(status: Status, showWakeUpNotice: boolean) {
  switch (status.kind) {
    case 'idle':
      return ''
    case 'loading':
      return `${status.players === undefined ? 'Loading the setup code' : `Drawing a setup for ${playerCount(status.players)}`}…${
        showWakeUpNotice ? ' The server may be waking up. This can take up to 30 seconds.' : ''
      }`
    case 'result': {
      const terms = setupTerms(status.setup)
      // A Mastermind that joins mid-game stays hidden, so the line doesn't name it.
      return status.setup.mastermindReveal
        ? `Setup ready. ${terms.scheme}: ${status.setup.scheme.name}.`
        : `Setup ready. ${terms.scheme}: ${status.setup.scheme.name}. ${terms.mastermind}: ${status.setup.mastermind.name}.`
    }
    case 'noEligibleScheme':
      return `${status.message} Pick another player count.`
    case 'chosenCardsConflict':
      return `${status.message} Change the cards under Must include.`
    case 'codeError':
      return status.message
    case 'error':
      return "Couldn't get a setup. Check your connection and try again."
  }
}

function App() {
  const [players, setPlayers] = useState<number | null>(loadPlayerCount)
  const [boxes, setBoxes] = useState<Box[]>([])
  const [included, setIncluded] = useState<string[]>(loadBoxes)
  // Every card a player can choose from the API, and the ids of those chosen to be in the game (#337).
  const [cards, setCards] = useState<Card[]>([])
  const [chosen, setChosen] = useState<string[]>(loadChosen)
  // The most Heroes and groups a player can choose, with the request they answer.
  const [answeredLimits, setAnsweredLimits] = useState<{ request: string; limits: ChoiceLimits } | null>(null)
  const [epic, setEpic] = useState(loadEpic)
  const [rules, setRules] = useState<RuleEdition | null>(loadRules)
  const [difficulty, setDifficulty] = useState<number | null>(() => loadRating(DIFFICULTY_KEY))
  const [complexity, setComplexity] = useState<number | null>(() => loadRating(COMPLEXITY_KEY))
  // Band names from the API; without them the pickers stay hidden and the draw is plain.
  const [bands, setBands] = useState<RatingBands | null>(null)
  const [status, setStatus] = useState<Status>({ kind: 'idle' })
  const [mastermindShown, setMastermindShown] = useState(false)
  // The intro explains the app until there is a setup on screen to explain it instead, so a first draw
  // that fails or finds no legal Scheme keeps it.
  const [shownSetup, setShownSetup] = useState(false)
  const [showWakeUpNotice, setShowWakeUpNotice] = useState(false)
  // The code typed or pasted into Load a setup.
  const [codeInput, setCodeInput] = useState('')
  const [saved, setSaved] = useState<SavedSetup[]>(loadSavedSetups)

  // Wake the API while the player is still choosing a count. Strict mode may repeat this harmlessly.
  useEffect(() => {
    void ping()
  }, [])

  // Without the box list there is nothing to pick, and a setup uses the core box alone; the controls then say the
  // server can't be reached and offer to try again. Generate waits for these requests, so a draw made while a cold
  // start is still answering includes the remembered boxes. Without the card list nothing can be chosen, and without
  // the band names the rating pickers stay hidden.
  const boxesRequest = useRef<Promise<Box[]>>(Promise.resolve([]))
  const cardsRequest = useRef<Promise<Card[]>>(Promise.resolve([]))
  const [catalogFailed, setCatalogFailed] = useState(false)
  function loadCatalog() {
    const boxesAnswer = getBoxes().catch((): Box[] => {
      setCatalogFailed(true)
      return []
    })
    boxesRequest.current = boxesAnswer
    void boxesAnswer.then(setBoxes)
    const cardsAnswer = getCards().catch((): Card[] => [])
    cardsRequest.current = cardsAnswer
    void cardsAnswer.then(setCards)
    void getRatingBands().then(setBands, () => {})
  }
  useEffect(() => {
    loadCatalog()
  }, [])

  // A link with a setup code opens on that setup. The ref keeps strict mode's second run from loading it twice.
  const linkedCode = useRef<string | null | undefined>(undefined)
  useEffect(() => {
    if (linkedCode.current !== undefined) return
    linkedCode.current = takeLinkedCode()
    if (linkedCode.current !== null) void loadCode(linkedCode.current)
  }, [])

  const loading = status.kind === 'loading'

  // A new setup lands below the controls, so bring its heading into view and give it focus. In two
  // columns the setup starts at the top of its column, so scroll the page there instead.
  const resultHeading = useRef<HTMLHeadingElement>(null)
  useEffect(() => {
    if (status.kind !== 'result') return
    const behavior = window.matchMedia('(prefers-reduced-motion: reduce)').matches ? 'auto' : 'smooth'
    resultHeading.current?.focus({ preventScroll: true })
    if (window.matchMedia(TWO_COLUMNS).matches) window.scrollTo({ top: 0, behavior })
    else resultHeading.current?.scrollIntoView({ behavior, block: 'start' })
  }, [status])

  useEffect(() => {
    if (!loading) return
    const timer = setTimeout(() => setShowWakeUpNotice(true), WAKE_UP_NOTICE_MS)
    return () => {
      clearTimeout(timer)
      setShowWakeUpNotice(false)
    }
  }, [loading])

  function choosePlayers(count: number) {
    setPlayers(count)
    savePlayerCount(count)
  }

  function toggleBox(id: string) {
    const next = included.includes(id) ? included.filter((other) => other !== id) : [...included, id]
    setIncluded(next)
    saveBoxes(next)
  }

  // Ticks every expansion, or unticks them all once every one is ticked; the base games stay as they are.
  function toggleAllExpansions() {
    const expansions = boxes.filter((box) => !box.baseGame).map((box) => box.id)
    const next = expansions.every((id) => included.includes(id))
      ? included.filter((id) => !expansions.includes(id))
      : [...included, ...expansions.filter((id) => !included.includes(id))]
    setIncluded(next)
    saveBoxes(next)
  }

  // A card chosen from a box that isn't ticked ticks its box, so the setup draws it as one of its own (#337).
  function addChosen(card: Card) {
    const next = withChosen(cards, chosen, card)
    setChosen(next)
    saveChosen(next)
    if (!card.boxes.some((id) => included.includes(id))) {
      const boxes = [...included, card.boxes[0]]
      setIncluded(boxes)
      saveBoxes(boxes)
    }
  }

  function removeChosen(id: string) {
    const next = chosen.filter((other) => other !== id)
    setChosen(next)
    saveChosen(next)
  }

  function clearChosen() {
    setChosen([])
    saveChosen([])
  }

  function chooseDifficulty(rating: number | null) {
    setDifficulty(rating)
    saveRating(DIFFICULTY_KEY, rating)
  }

  function chooseComplexity(rating: number | null) {
    setComplexity(rating)
    saveRating(COMPLEXITY_KEY, rating)
  }

  function toggleEpic() {
    setEpic(!epic)
    saveEpic(!epic)
  }

  function chooseRules(edition: RuleEdition | null) {
    setRules(edition)
    saveRules(edition)
  }

  // Stored ids the API no longer lists are ignored.
  const isIncluded = (box: Box) => included.includes(box.id)
  const hint = boxHint(boxes, included)
  // The rule edition choice matters only while the ticked boxes follow more than one ruleset.
  const choosesRules = spansRulesets(boxes, included)
  const baseGames = boxes.filter((box) => box.baseGame)
  const expansions = boxes.filter((box) => !box.baseGame)
  const allExpansions = expansions.length > 0 && expansions.every(isIncluded)

  // The most Heroes and groups a player can choose under Must include for the picked count, boxes and rule edition, and the
  // chosen Scheme, Mastermind and groups, which can change them (#337): only a chosen Scheme allows more than the count's
  // usual numbers, and a group the chosen Scheme requires or the chosen Mastermind always leads takes a slot unless it is
  // chosen too. Epic Masterminds'
  // Epic sides can add a group. Until the API answers, or when it can't, nothing is limited.
  const limitBoxes = boxes.filter(isIncluded).map((box) => box.id).join(',')
  const limitRules = choosesRules ? rules : null
  const limitCards = chosen.filter((id) => cards.some((card) => card.id === id && card.kind !== 'hero')).join(',')
  const limitRequest = [players, limitBoxes, epic, limitRules, limitCards].join('|')
  const limits = answeredLimits?.request === limitRequest ? answeredLimits.limits : null
  useEffect(() => {
    if (players === null || limitBoxes === '' || hint !== null) return
    const controller = new AbortController()
    getLimits(players, limitBoxes.split(','), epic, limitRules, limitCards === '' ? [] : limitCards.split(','), controller.signal).then(
      (answer) => setAnsweredLimits({ request: limitRequest, limits: answer }),
      () => {},
    )
    return () => controller.abort()
  }, [players, limitBoxes, epic, limitRules, limitCards, limitRequest, hint])

  const renderBox = (box: Box) => (
    <div key={box.id} className="box-option">
      <label>
        <input type="checkbox" checked={isIncluded(box)} disabled={loading} onChange={() => toggleBox(box.id)} />
        <span className="box-name">{box.name}</span>
      </label>
      <BoxInfo box={box} />
    </div>
  )

  // The setup code Retry loads again when the last request loaded one; null when it was a draw.
  const lastCode = useRef<string | null>(null)

  // Generate, Generate another and Retry after a failed draw all draw for the selected count and boxes.
  async function generate() {
    if (players === null) return
    lastCode.current = null
    setStatus({ kind: 'loading', players })
    setMastermindShown(false)
    try {
      const available = await boxesRequest.current
      const known = await cardsRequest.current
      const drawn = available.filter(isIncluded)
      // A draw started before the box list loaded may turn out to have boxes it can't draw from; the picker now says so.
      if (boxHint(available, included) !== null) {
        setStatus({ kind: 'idle' })
        return
      }
      const response = await getSetup(
        players,
        drawn.map((box) => box.id),
        epic,
        spansRulesets(available, included) ? rules : null,
        bands ? difficulty : null,
        bands ? complexity : null,
        // Stored ids the API no longer lists are left out.
        chosen.filter((id) => known.some((card) => card.id === id)),
      )
      if (response.kind === 'setup') {
        setStatus({ kind: 'result', setup: response })
        setShownSetup(true)
      } else if (response.kind === 'chosenCardsConflict') {
        setStatus({ kind: 'chosenCardsConflict', message: response.message })
      } else {
        setStatus({ kind: 'noEligibleScheme', message: response.message })
      }
    } catch {
      setStatus({ kind: 'error' })
    }
  }

  // Saves the setup on screen under Saved setups; false when the browser won't keep it, and then the list is unchanged.
  // Each change starts from what storage holds now, so a setup saved in another tab since this page loaded is kept.
  function saveSetup(code: string, name: string, note: string): boolean {
    const next = withSaved(loadSavedSetups(), { code, name, note, savedAt: new Date().toISOString() })
    if (!storeSavedSetups(next)) return false
    setSaved(next)
    return true
  }

  function deleteSaved(code: string) {
    const next = loadSavedSetups().filter((entry) => entry.code !== code)
    storeSavedSetups(next)
    setSaved(next)
  }

  // Reloads the setup a code names, as it was shown (#335).
  async function loadCode(code: string) {
    lastCode.current = code
    setStatus({ kind: 'loading' })
    setMastermindShown(false)
    try {
      const setup = await getSetupByCode(code)
      // A link opened before any count was picked would leave Generate another with no count to draw for.
      setPlayers((current) => current ?? setup.players)
      setStatus({ kind: 'result', setup })
      setShownSetup(true)
    } catch (error) {
      setStatus(error instanceof SetupCodeError ? { kind: 'codeError', message: error.message } : { kind: 'error' })
    }
  }

  // The one Generate: it draws for the picked count, and once a setup is on screen it draws another, naming the count
  // when it differs from the setup's.
  const result = status.kind === 'result' ? status.setup : null
  const shownPlayers = result?.players ?? players

  return (
    <div className="app">
      <header className="site-header">
        <img className="mark" src={`${import.meta.env.BASE_URL}favicon.svg`} alt="" width="48" height="48" />
        <div>
          <h1 className="wordmark">
            Legendary <span>Picker</span>
          </h1>
          <p className="tagline">A random legal setup for Marvel Legendary First Edition, Second Edition, Villains, What If...? and their expansions.</p>
        </div>
      </header>

      <main className="content">
        {/* The choices and the one Generate. On a wide screen they stay in view beside the setup, Generate pinned to
            the bottom of the column; on a phone Generate sits at the bottom of the screen. */}
        <div className="rail">
          <section className="controls panel" aria-label="Setup options">
            {!shownSetup && (
              <p className="intro">
                Get a random legal setup and a checklist for laying it out, following the official rules.
              </p>
            )}
            <div className="step">
              <h2 id="players-label">Players</h2>
              <div className="player-counts" role="group" aria-labelledby="players-label">
                {PLAYER_COUNTS.map((count) => (
                  <button
                    key={count}
                    type="button"
                    className="player-count"
                    aria-pressed={players === count}
                    disabled={loading}
                    onClick={() => choosePlayers(count)}
                  >
                    {count}
                  </button>
                ))}
              </div>
            </div>
            {catalogFailed && (
              <div className="catalog-error" role="alert">
                <p>Couldn't reach the server, so the boxes and cards to choose from aren't shown.</p>
                <button
                  type="button"
                  className="secondary"
                  onClick={() => {
                    setCatalogFailed(false)
                    loadCatalog()
                  }}
                >
                  Try again
                </button>
              </div>
            )}
            {bands && (
              <div className="step rating-pickers">
                <RatingPicker
                  label="Difficulty"
                  bands={bands.difficulty}
                  value={difficulty}
                  disabled={loading}
                  onChange={chooseDifficulty}
                />
                <RatingPicker
                  label="Complexity"
                  bands={bands.complexity}
                  value={complexity}
                  disabled={loading}
                  onChange={chooseComplexity}
                />
              </div>
            )}
            {cards.length > 0 && (
              <div className="step">
                <ChosenCards
                  cards={cards}
                  boxes={boxes}
                  included={included}
                  chosen={chosen}
                  limits={limits}
                  players={players}
                  disabled={loading}
                  onAdd={addChosen}
                  onRemove={removeChosen}
                  onClear={clearChosen}
                />
              </div>
            )}
            {boxes.length > 0 && (
              <div className="step">
                <div className="step-heading">
                  <h2 id="boxes-label">Boxes</h2>
                  {expansions.length > 0 && (
                    <button type="button" className="link-button" disabled={loading} onClick={toggleAllExpansions}>
                      {allExpansions ? 'Uncheck all expansions' : 'Check all expansions'}
                    </button>
                  )}
                </div>
                {/* One base game is always needed, so the base games sit in their own group that says so. */}
                <div
                  className={`box-group base-games${hint ? ' needs-choice' : ''}`}
                  role="group"
                  aria-labelledby="base-games-label"
                >
                  <h3 id="base-games-label" className="box-group-label">
                    Base games <span className="box-group-note">Choose at least one</span>
                  </h3>
                  <div className="box-options">{baseGames.map(renderBox)}</div>
                  {hint && <p className="box-hint">{hint}</p>}
                </div>
                {/* The expansion list is long, so it stays folded until the player opens it. */}
                {expansions.length > 0 && (
                  <details className="box-group expansions" role="group" aria-labelledby="expansions-label">
                    <summary>
                      <h3 id="expansions-label" className="box-group-label">
                        Expansions
                      </h3>
                      <span className="box-group-note">
                        {expansions.filter(isIncluded).length} of {expansions.length} ticked
                      </span>
                    </summary>
                    <div className="box-options">{expansions.map(renderBox)}</div>
                  </details>
                )}
              </div>
            )}
            {choosesRules && (
              <div className="step">
                <h2 id="rules-label">Rules</h2>
                <div className="box-options" role="radiogroup" aria-labelledby="rules-label">
                  <label className="box-option">
                    <input
                      type="radio"
                      name="rules"
                      checked={rules === null}
                      disabled={loading}
                      onChange={() => chooseRules(null)}
                    />
                    <span className="box-name">Default</span> <span className="detail">Set by the boxes</span>
                  </label>
                  {RULE_EDITIONS.map((edition) => (
                    <label key={edition.id} className="box-option">
                      <input
                        type="radio"
                        name="rules"
                        checked={rules === edition.id}
                        disabled={loading}
                        onChange={() => chooseRules(edition.id)}
                      />
                      <span className="box-name">{edition.name}</span>
                      {edition.detail && <> <span className="detail">{edition.detail}</span></>}
                    </label>
                  ))}
                </div>
              </div>
            )}
            <div className="step">
              <h2 id="options-label">Options</h2>
              <div className="box-options" role="group" aria-labelledby="options-label">
                <label className="box-option">
                  <input type="checkbox" checked={epic} disabled={loading} onChange={toggleEpic} />
                  <span className="box-name">Epic Masterminds</span>
                  <span className="detail">Play the Epic side</span>
                </label>
              </div>
            </div>
          </section>

          <section className="library panel" aria-label="Saved setups and codes">
            <form
              className="code-form"
              aria-labelledby="code-label"
              onSubmit={(event) => {
                event.preventDefault()
                if (codeInput.trim()) void loadCode(codeInput.trim())
              }}
            >
              <h2 id="code-label">Load a setup</h2>
              <div className="code-row">
                <input
                  type="text"
                  aria-label="Setup code"
                  placeholder="Paste a setup code"
                  autoComplete="off"
                  spellCheck={false}
                  value={codeInput}
                  disabled={loading}
                  onChange={(event) => setCodeInput(event.target.value)}
                />
                <button type="submit" className="secondary" disabled={loading || codeInput.trim() === ''}>
                  Load
                </button>
              </div>
            </form>
            <SavedSetups saved={saved} disabled={loading} onLoad={(code) => void loadCode(code)} onDelete={deleteSaved} />
          </section>

          <div className="action-bar" role="group" aria-label="Setup actions">
            <p className="action-players">{shownPlayers === null ? 'Pick players' : playerCount(shownPlayers)}</p>
            <button
              type="button"
              className="primary generate"
              disabled={players === null || loading || hint !== null}
              onClick={generate}
            >
              {result === null ? 'Generate' : 'Generate another'}
              {result !== null && players !== null && players !== result.players && ` · ${playerCount(players)}`}
            </button>
          </div>
        </div>

        {/* Announces each state change. The status below is not live, so a new checklist isn't read
            out in full. */}
        <p className="visually-hidden" aria-live="polite">
          {announcement(status, showWakeUpNotice)}
        </p>

        <section className="status">
          {status.kind === 'idle' && (
            <div className="empty-state">
              <p className="empty-title">Your setup appears here</p>
              <p>Pick a player count and the boxes you own, then Generate.</p>
            </div>
          )}
          {status.kind === 'loading' && (
            <>
              <p className="loading-line">{status.players === undefined ? 'Loading the setup…' : 'Drawing a setup…'}</p>
              {showWakeUpNotice && (
                <p className="notice">The server may be waking up. This can take up to 30 seconds.</p>
              )}
              <SetupSkeleton />
            </>
          )}
          {result && (
            <>
              <SetupSummary setup={result} headingRef={resultHeading} mastermindShown={mastermindShown} />
              {result.code && (
                <SetupCode
                  key={result.code}
                  code={result.code}
                  defaultName={result.scheme.name}
                  saved={saved.find((entry) => entry.code === result.code)}
                  onSave={(name, note) => saveSetup(result.code!, name, note)}
                />
              )}
              <SetupChecklist setup={result} mastermindShown={mastermindShown} onShowMastermind={() => setMastermindShown(true)} />
            </>
          )}
          {status.kind === 'noEligibleScheme' && (
            <div className="state-card scheme">
              <p>{status.message}</p>
              <p className="state-prompt">Pick another player count.</p>
            </div>
          )}
          {status.kind === 'chosenCardsConflict' && (
            <div className="state-card">
              <p>{status.message}</p>
              <p className="state-prompt">Change the cards under Must include.</p>
            </div>
          )}
          {status.kind === 'codeError' && (
            <div className="state-card">
              <p>{status.message}</p>
              <p className="state-prompt">Generate a new setup instead.</p>
            </div>
          )}
          {status.kind === 'error' && (
            <div className="state-card">
              <p>Couldn't get a setup. Check your connection and try again.</p>
              <button type="button" className="primary" onClick={() => void (lastCode.current === null ? generate() : loadCode(lastCode.current))}>
                Retry
              </button>
            </div>
          )}
        </section>
      </main>

      <footer className="site-footer">
        <p>A fan-made tool, not affiliated with or endorsed by Marvel or Upper Deck.</p>
        <p>
          Setup rules follow the{' '}
          <a href={RULEBOOK_URL} target="_blank" rel="noreferrer">
            First Edition rulebook
          </a>
          , the{' '}
          <a href={VILLAINS_RULEBOOK_URL} target="_blank" rel="noreferrer">
            Villains rulebook
          </a>
          , the{' '}
          <a href={WHAT_IF_RULEBOOK_URL} target="_blank" rel="noreferrer">
            What If...? rulebook
          </a>
          , the{' '}
          <a href={SECOND_EDITION_RULEBOOK_URL} target="_blank" rel="noreferrer">
            Second Edition rulebook
          </a>{' '}
          and official clarifications.
        </p>
        <p>
          <a className="footer-link" href={REPO_URL} target="_blank" rel="noreferrer">
            Source on GitHub
          </a>
        </p>
      </footer>
    </div>
  )
}

export default App
