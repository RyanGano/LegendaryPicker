import { useId, useState, type KeyboardEvent } from 'react'
import type { Box, Card, ChoiceLimits, ChoosableKind } from './api/setupApi.ts'
import { termsFor, type RulesetTerms } from './rulesetTerms.ts'

// The most matches the filter lists at once; typing more of the name narrows them.
const MAX_MATCHES = 8

const KIND_WORD: Record<ChoosableKind, (terms: RulesetTerms) => string> = {
  scheme: (terms) => terms.scheme,
  mastermind: (terms) => terms.mastermind,
  villain: (terms) => terms.villainGroup,
  henchman: (terms) => terms.henchmanGroup,
  hero: (terms) => terms.hero,
}

// The kinds a setup holds a limited number of, by their field in the limits, with their plural words.
const LIMITED: Partial<Record<ChoosableKind, { limit: keyof ChoiceLimits; words: (terms: RulesetTerms) => string }>> = {
  hero: { limit: 'heroes', words: (terms) => terms.heroes },
  villain: { limit: 'villainGroups', words: (terms) => terms.villainGroups },
  henchman: { limit: 'henchmanGroups', words: (terms) => terms.henchmanGroups },
}

type Props = {
  cards: Card[]
  boxes: Box[]
  // The ticked boxes' ids.
  included: string[]
  // The chosen cards' ids, in the order chosen.
  chosen: string[]
  // The most Heroes and groups a setup of the picked player count and boxes holds, or null while unknown.
  limits: ChoiceLimits | null
  players: number | null
  disabled: boolean
  onAdd: (card: Card) => void
  onRemove: (id: string) => void
  onClear: () => void
}

// The cards that must be in the game (#337), under an expander that starts closed unless cards are chosen: a filter box
// that matches part of a name across every box, ticked or not, and the chosen cards as removable chips. Each card shows
// its kind, in its own ruleset's words, and its box. Once as many Heroes or groups of a kind are chosen as any setup of
// the player count holds, more of that kind can't be chosen.
export function ChosenCards({ cards, boxes, included, chosen, limits, players, disabled, onAdd, onRemove, onClear }: Props) {
  const [expanded, setExpanded] = useState(chosen.length > 0)
  const [query, setQuery] = useState('')
  const [active, setActive] = useState(0)
  const listId = useId()
  const labelId = useId()

  const boxOf = (card: Card) => boxes.find((box) => box.id === card.boxes[0])
  const kindOf = (card: Card) => KIND_WORD[card.kind](termsFor(boxOf(card)?.ruleset))

  // Names that start with the text first, then those that hold it, each in the API's order.
  const text = query.trim().toLowerCase()
  const matches =
    text === ''
      ? []
      : cards
          .filter((card) => !chosen.includes(card.id) && card.name.toLowerCase().includes(text))
          .sort((a, b) => Number(!a.name.toLowerCase().startsWith(text)) - Number(!b.name.toLowerCase().startsWith(text)))
          .slice(0, MAX_MATCHES)
  const open = matches.length > 0
  const current = Math.min(active, matches.length - 1)

  const chosenCards = chosen.map((id) => cards.find((card) => card.id === id)).filter((card): card is Card => card !== undefined)
  // How many more of a kind the setup has room for, or null when the kind isn't limited or the limits aren't known.
  const room = (kind: ChoosableKind) => {
    const limited = LIMITED[kind]
    return limited && limits ? limits[limited.limit] - chosenCards.filter((card) => card.kind === kind).length : null
  }
  const full = (card: Card) => (room(card.kind) ?? 1) <= 0
  const atPlayers = players === 1 ? 'in Solo' : `at ${players} players`
  // The kinds chosen past what a setup holds, as after a smaller player count is picked.
  const over = (Object.keys(LIMITED) as ChoosableKind[])
    .map((kind) => ({ kind, extra: -(room(kind) ?? 0), card: chosenCards.find((card) => card.kind === kind) }))
    .filter((entry) => entry.extra > 0 && entry.card)

  function add(card: Card) {
    if (full(card)) return
    onAdd(card)
    setQuery('')
    setActive(0)
  }

  function onKeyDown(event: KeyboardEvent<HTMLInputElement>) {
    if (event.key === 'ArrowDown' && open) {
      event.preventDefault()
      setActive((current + 1) % matches.length)
    } else if (event.key === 'ArrowUp' && open) {
      event.preventDefault()
      setActive((current - 1 + matches.length) % matches.length)
    } else if (event.key === 'Enter' && open) {
      event.preventDefault()
      add(matches[current])
    } else if (event.key === 'Escape') {
      setQuery('')
    }
  }

  // The limit of a card's kind with its word, in the card's ruleset's words: "1 Hero", "6 Heroes".
  const limitOf = (card: Card) => {
    const most = limits![LIMITED[card.kind]!.limit]
    return `${most} ${most === 1 ? kindOf(card) : LIMITED[card.kind]!.words(termsFor(boxOf(card)?.ruleset))}`
  }

  return (
    <details className="chosen-cards" open={expanded} onToggle={(event) => setExpanded(event.currentTarget.open)}>
      <summary>
        <h2 id={labelId}>Must include</h2>
        {chosenCards.length > 0 && <span className="detail">{chosenCards.length} chosen</span>}
      </summary>
      <div className="chosen-heading">
        <p className="chosen-hint">
          {chosenCards.length === 0
            ? 'Type part of a card name to choose a Scheme, Mastermind, group or Hero for the setup.'
            : `${chosenCards.length} chosen. The rest of the setup is drawn around ${chosenCards.length === 1 ? 'it' : 'them'}.`}
        </p>
        {chosenCards.length > 0 && (
          <button type="button" className="link-button" disabled={disabled} onClick={onClear}>
            Clear
          </button>
        )}
      </div>
      <div className="chosen-filter">
        <input
          type="text"
          role="combobox"
          aria-labelledby={labelId}
          aria-autocomplete="list"
          aria-expanded={open}
          aria-controls={listId}
          aria-activedescendant={open ? `${listId}-${current}` : undefined}
          placeholder="Find a card"
          autoComplete="off"
          spellCheck={false}
          value={query}
          disabled={disabled}
          onChange={(event) => {
            setQuery(event.target.value)
            setActive(0)
          }}
          onKeyDown={onKeyDown}
        />
        {open && (
          <ul className="chosen-matches" id={listId} role="listbox" aria-labelledby={labelId}>
            {matches.map((card, index) => (
              <li
                key={card.id}
                id={`${listId}-${index}`}
                role="option"
                aria-selected={index === current}
                aria-disabled={full(card) || undefined}
                className={`chosen-match kind-${card.kind}`}
                // Pressing down would blur the input before the click lands.
                onMouseDown={(event) => event.preventDefault()}
                onClick={() => add(card)}
              >
                <span className="chosen-name">{card.name}</span>
                <span className="detail">
                  {kindOf(card)} · {boxOf(card)?.name ?? card.boxes[0]}
                  {full(card) && ` · No room: a setup ${atPlayers} holds at most ${limitOf(card)}`}
                </span>
              </li>
            ))}
          </ul>
        )}
      </div>
      {chosenCards.length > 0 && (
        <ul className="chosen-chips" aria-label="Chosen cards">
          {chosenCards.map((card) => (
            <li key={card.id} className={`chosen-chip kind-${card.kind}`}>
              <span className="chosen-name">{card.name}</span>
              <span className="detail">
                {kindOf(card)} · {boxOf(card)?.name ?? card.boxes[0]}
                {!card.boxes.some((id) => included.includes(id)) && ', added to the draw'}
              </span>
              <button
                type="button"
                className="chip-remove"
                aria-label={`Remove ${card.name}`}
                disabled={disabled}
                onClick={() => onRemove(card.id)}
              >
                ×
              </button>
            </li>
          ))}
        </ul>
      )}
      {over.map(({ kind, extra, card }) => (
        <p key={kind} className="chosen-over">
          A setup {atPlayers} holds at most {limitOf(card!)}: remove {extra}.
        </p>
      ))}
    </details>
  )
}
