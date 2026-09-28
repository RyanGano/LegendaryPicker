import { useEffect, useId, useRef, useState, type CSSProperties } from 'react'
import type { GlossaryEntry } from './api/setupApi.ts'

// A drawn component's glossary terms as chips. Tapping one opens its explanation: the term's
// name, summary and citation exactly as the API returns them. The explanation is a popover under
// the chip, or a bottom sheet on a phone, and closes on Escape, an outside tap or its close
// button, handing focus back to the chip.
export function TermChips({ terms }: { terms: GlossaryEntry[] }) {
  if (terms.length === 0) return null
  return (
    <ul className="term-chips" aria-label="Terms">
      {terms.map((term) => (
        <li key={term.id}>
          <TermChip term={term} />
        </li>
      ))}
    </ul>
  )
}

function TermChip({ term }: { term: GlossaryEntry }) {
  const [position, setPosition] = useState<{ top: number; left: number } | null>(null)
  const chip = useRef<HTMLButtonElement>(null)
  const popover = useRef<HTMLDivElement>(null)
  const id = useId()
  const open = position !== null

  function show() {
    const rect = chip.current!.getBoundingClientRect()
    setPosition({ top: rect.bottom + window.scrollY, left: rect.left + window.scrollX })
  }

  function close() {
    setPosition(null)
    chip.current?.focus()
  }

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
    return () => {
      document.removeEventListener('keydown', onKeyDown)
      document.removeEventListener('click', onClick)
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
        onClick={() => (open ? close() : show())}
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
              <TermKind kind={term.kind} />
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
        </div>
      )}
    </>
  )
}

const KIND_LABELS: Record<GlossaryEntry['kind'], string> = {
  team: 'Team',
  class: 'Hero class',
  keyword: 'Keyword',
}

export function TermKind({ kind }: { kind: GlossaryEntry['kind'] }) {
  return KIND_LABELS[kind]
}
