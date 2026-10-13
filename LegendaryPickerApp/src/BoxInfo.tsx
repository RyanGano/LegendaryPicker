import type { CSSProperties } from 'react'
import type { Box } from './api/setupApi.ts'
import { usePopover } from './usePopover.ts'

// A box's info button and the panel it opens: the box's name, the year it was first published and a
// short description, laid out like a glossary term's explanation (usePopover). It sits beside the
// box's checkbox label rather than inside it, so tapping it never ticks the box. An API that sends no
// description gets no button. A box that plays by the Revised rules says so here too, as "Second Edition rules"
// (#247); on a phone the picker's row drops that tag, so each row keeps at most one tag beside its name (#244).
export function BoxInfo({ box }: { box: Box }) {
  const { id, open, position, trigger, popover, toggle, close } = usePopover()
  if (!box.description) return null

  return (
    <>
      <button
        ref={trigger}
        type="button"
        className="info-button"
        aria-label={`About ${box.name}`}
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
            {box.firstPublished && <p className="term-kind">First published {box.firstPublished}</p>}
            <button type="button" className="term-close" aria-label="Close" onClick={close}>
              ×
            </button>
          </div>
          <h4 id={`${id}-name`}>{box.name}</h4>
          <p>{box.description}</p>
          {box.ruleset === 'revised' && <p className="box-info-ruleset">Second Edition rules</p>}
        </div>
      )}
    </>
  )
}
