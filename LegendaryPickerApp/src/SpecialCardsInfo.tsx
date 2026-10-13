import type { CSSProperties } from 'react'
import type { SpecialCards } from './api/setupApi.ts'
import { usePopover } from './usePopover.ts'

// A shared stack's info button and the panel it opens: the special cards the included boxes shuffle into the
// stack, by box, laid out like a glossary term's explanation (usePopover). It sits beside the stack's tick box
// rather than inside its label, so tapping it never ticks the line. `stack` is the line's label, such as "Bystanders".
export function SpecialCardsInfo({ stack, special }: { stack: string; special: SpecialCards[] }) {
  const { id, open, position, trigger, popover, toggle, close } = usePopover()

  return (
    <>
      <button
        ref={trigger}
        type="button"
        className="info-button"
        aria-label={`Special ${stack} in this setup`}
        aria-haspopup="dialog"
        aria-expanded={open}
        aria-controls={open ? id : undefined}
        onClick={toggle}
      >
        <span className="info-mark" aria-hidden="true">
          i
        </span>
      </button>
      {position && (
        <div
          ref={popover}
          id={id}
          role="dialog"
          aria-labelledby={`${id}-name`}
          tabIndex={-1}
          className="term-popover box-info"
          style={{ '--popover-top': `${position.top}px`, '--popover-left': `${position.left}px` } as CSSProperties}
        >
          <div className="term-popover-header">
            <p className="term-kind">Shuffled into the {stack}</p>
            <button type="button" className="term-close" aria-label="Close" onClick={close}>
              ×
            </button>
          </div>
          <h4 id={`${id}-name`}>Special {stack}</h4>
          {special.map((cards) => (
            <div key={cards.box}>
              <p className="special-box">{cards.box}</p>
              <ul className="special-cards">
                {cards.cards.map((card) => (
                  <li key={card}>{card}</li>
                ))}
              </ul>
            </div>
          ))}
        </div>
      )}
    </>
  )
}
