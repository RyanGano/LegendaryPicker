import type { CSSProperties } from 'react'
import type { GlossaryEntry } from './api/setupApi.ts'
import { usePopover } from './usePopover.ts'

// A drawn component's glossary terms as chips. Tapping one opens its explanation: the term's
// name, summary and citation exactly as the API returns them. The explanation is a popover under
// the chip, or a bottom sheet on a phone, and closes on Escape, an outside tap or its close
// button, handing focus back to the chip (usePopover). hero is what the setup's ruleset calls a Hero, which names
// the class kind ("Hero class", or "Ally class" under the Villainous ruleset).

export function TermChips({ terms, hero }: { terms: GlossaryEntry[]; hero: string }) {
  if (terms.length === 0) return null
  return (
    <ul className="term-chips" aria-label="Terms">
      {terms.map((term) => (
        <li key={term.id}>
          <TermChip term={term} hero={hero} />
        </li>
      ))}
    </ul>
  )
}

function TermChip({ term, hero }: { term: GlossaryEntry; hero: string }) {
  const { id, open, position, trigger, popover, toggle, close } = usePopover()

  return (
    <>
      <button
        ref={trigger}
        type="button"
        className={`term-chip ${term.kind}`}
        aria-haspopup="dialog"
        aria-expanded={open}
        aria-controls={open ? id : undefined}
        onClick={toggle}
      >
        {term.name}
      </button>
      {position && (
        <div
          ref={popover}
          id={id}
          role="dialog"
          aria-labelledby={`${id}-name`}
          tabIndex={-1}
          className={`term-popover ${term.kind}`}
          style={{ '--popover-top': `${position.top}px`, '--popover-left': `${position.left}px` } as CSSProperties}
        >
          <div className="term-popover-header">
            <p className="term-kind">
              <TermKind kind={term.kind} hero={hero} />
            </p>
            <button type="button" className="term-close" aria-label="Close" onClick={close}>
              ×
            </button>
          </div>
          <h4 id={`${id}-name`}>{term.name}</h4>
          <p>{term.summary}</p>
          <cite>
            <a href={term.link} target="_blank" rel="noreferrer">
              {term.citation}
            </a>
          </cite>
          {term.box && <span className="source-box">{term.box}</span>}
        </div>
      )}
    </>
  )
}

export function TermKind({ kind, hero }: { kind: GlossaryEntry['kind']; hero: string }) {
  const labels: Record<GlossaryEntry['kind'], string> = { team: 'Team', class: `${hero} class`, keyword: 'Keyword' }
  return labels[kind]
}
