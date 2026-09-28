import '@testing-library/jest-dom/vitest'
import { cleanup } from '@testing-library/react'
import { afterEach } from 'vitest'

// jsdom lays nothing out: it has no media queries and no scrolling. The app asks for both after a
// generate, so answer as a browser with no motion preference would, and scroll nowhere.
window.matchMedia = (query: string) =>
  ({ matches: false, media: query, addEventListener() {}, removeEventListener() {} }) as unknown as MediaQueryList
Element.prototype.scrollIntoView = () => {}

afterEach(() => {
  cleanup()
  localStorage.clear()
})
