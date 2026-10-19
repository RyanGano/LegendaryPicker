import { useId, useState, type KeyboardEvent } from 'react'
import type { Box, Card, ChoosableKind } from './api/setupApi.ts'
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

type Props = {
  cards: Card[]
  boxes: Box[]
  // The ticked boxes' ids.
  included: string[]
  // The chosen cards' ids, in the order chosen.
  chosen: string[]
  disabled: boolean
  onAdd: (card: Card) => void
  onRemove: (id: string) => void
  onClear: () => void
}

// The cards that must be in the game (#337): a filter box that matches part of a name across every box, ticked or not,
// and the chosen cards as removable chips. Each card shows its kind, in its own ruleset's words, and its box.
export function ChosenCards({ cards, boxes, included, chosen, disabled, onAdd, onRemove, onClear }: Props) {
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

  function add(card: Card) {
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

  const chosenCards = chosen.map((id) => cards.find((card) => card.id === id)).filter((card): card is Card => card !== undefined)

  return (
    <div className="chosen-cards">
      <div className="boxes-heading">
        <h2 id={labelId}>Must include</h2>
        {chosenCards.length > 0 && (
          <button type="button" className="link-button" disabled={disabled} onClick={onClear}>
            Clear
          </button>
        )}
      </div>
      <p className="chosen-hint">
        {chosenCards.length === 0
          ? 'Type part of a card name to choose a Scheme, Mastermind, group or Hero for the setup.'
          : `${chosenCards.length} chosen. The rest of the setup is drawn around ${chosenCards.length === 1 ? 'it' : 'them'}.`}
      </p>
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
                className={`chosen-match kind-${card.kind}`}
                // Pressing down would blur the input before the click lands.
                onMouseDown={(event) => event.preventDefault()}
                onClick={() => add(card)}
              >
                <span className="chosen-name">{card.name}</span>
                <span className="detail">
                  {kindOf(card)} · {boxOf(card)?.name ?? card.boxes[0]}
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
    </div>
  )
}
