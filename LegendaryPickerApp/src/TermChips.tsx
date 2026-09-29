import { useEffect, useId, useLayoutEffect, useRef, useState, type CSSProperties } from 'react'
import type { GlossaryEntry } from './api/setupApi.ts'

// A drawn component's glossary terms as chips. Tapping one opens its explanation: the term's
// name, summary and citation exactly as the API returns them. The explanation is a popover under
// the chip, or a bottom sheet on a phone, and closes on Escape, an outside tap or its close
// button, handing focus back to the chip. hero is what the setup's ruleset calls a Hero, which names
// the class kind ("Hero class", or "Ally class" under the Villainous ruleset).

// Pixels between a chip and its popover.
const POPOVER_GAP = 4
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
  const [position, setPosition] = useState<{ top: number; left: number } | null>(null)
  const chip = useRef<HTMLButtonElement>(null)
  const popover = useRef<HTMLDivElement>(null)
  const id = useId()
  const open = position !== null

  // The popover is fixed to the viewport, so a scrolling or sticky column can't clip or offset it,
  // and it moves with its chip on any scroll. It opens under the chip, or above it when it would run
  // off the bottom of the screen and there is room above.
  function place() {
    const rect = chip.current!.getBoundingClientRect()
    const height = popover.current?.offsetHeight ?? 0
    const below = rect.bottom + POPOVER_GAP
    const above = rect.top - POPOVER_GAP - height
    setPosition({ top: below + height > window.innerHeight && above >= 0 ? above : below, left: rect.left })
  }

  function close() {
    setPosition(null)
    chip.current?.focus()
  }

  // The first placement can't know the popover's height, so place it again once it is drawn.
  useLayoutEffect(() => {
    if (open) place()
  }, [open])

  useEffect(() => {
    if (!open) return
    popover.current?.focus()

    function onKeyDown(event: KeyboardEvent) {
      if (event.key === 'Escape') close()
    }
    // A tap on something else that takes focus, such as another chip, keeps it there.
    function onClick(event: MouseEvent) {
      const target = event.target as Node
      if (popover.current?.contains(target) || chip.current?.contains(target)) return
      setPosition(null)
      if (document.activeElement === document.body || popover.current?.contains(document.activeElement)) {
        chip.current?.focus()
      }
    }
    document.addEventListener('keydown', onKeyDown)
    document.addEventListener('click', onClick)
    // Capture sees scrolls inside a column as well as the page's.
    document.addEventListener('scroll', place, true)
    window.addEventListener('resize', place)
    return () => {
      document.removeEventListener('keydown', onKeyDown)
      document.removeEventListener('click', onClick)
      document.removeEventListener('scroll', place, true)
      window.removeEventListener('resize', place)
    }
  }, [open])

  return (
    <>
      <button
        ref={chip}
        type="button"
        className={`term-chip ${term.kind}`}
        aria-haspopup="dialog"
        aria-expanded={open}
        aria-controls={open ? id : undefined}
        onClick={() => (open ? close() : place())}
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
