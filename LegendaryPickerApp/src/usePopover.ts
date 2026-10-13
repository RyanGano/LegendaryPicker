import { useEffect, useId, useLayoutEffect, useRef, useState } from 'react'

// The open, place and close behavior of a popover opened from a button: a term chip's explanation or a
// box's about panel. The panel is fixed to the viewport, so a scrolling or sticky column can't clip or
// offset it, and it moves with its button on any scroll. It opens under the button, or above it when it
// would run off the bottom of the screen and there is room above. It closes on Escape, an outside tap or
// its close button, handing focus back to the button. Pixels between a button and its panel:
const POPOVER_GAP = 4

export function usePopover() {
  const [position, setPosition] = useState<{ top: number; left: number } | null>(null)
  const trigger = useRef<HTMLButtonElement>(null)
  const popover = useRef<HTMLDivElement>(null)
  const id = useId()
  const open = position !== null

  function place() {
    const rect = trigger.current!.getBoundingClientRect()
    const height = popover.current?.offsetHeight ?? 0
    const below = rect.bottom + POPOVER_GAP
    const above = rect.top - POPOVER_GAP - height
    setPosition({ top: below + height > window.innerHeight && above >= 0 ? above : below, left: rect.left })
  }

  function close() {
    setPosition(null)
    trigger.current?.focus()
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
      if (popover.current?.contains(target) || trigger.current?.contains(target)) return
      setPosition(null)
      if (document.activeElement === document.body || popover.current?.contains(document.activeElement)) {
        trigger.current?.focus()
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

  return { id, open, position, trigger, popover, toggle: () => (open ? close() : place()), close }
}
