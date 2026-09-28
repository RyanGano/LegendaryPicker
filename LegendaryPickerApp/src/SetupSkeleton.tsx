// The shape of a setup while one is being drawn: placeholder Scheme and Mastermind cards, Hero
// tiles and checklist rows in the result's layout, so the wait reads as the result arriving.
// Screen readers hear the loading line beside it instead.
export function SetupSkeleton() {
  return (
    <div className="skeleton" data-testid="setup-skeleton" aria-hidden="true">
      <span className="bone bone-heading" />
      <div className="headline-cards">
        {['scheme', 'mastermind'].map((kind) => (
          <div key={kind} className={`headline-card ${kind}`}>
            <span className="bone bone-label" />
            <span className="bone bone-title" />
          </div>
        ))}
      </div>
      <div className="tiles hero">
        {[0, 1, 2, 3].map((tile) => (
          <div key={tile} className="tile">
            <span className="bone bone-line" />
          </div>
        ))}
      </div>
      <div>
        {[0, 1, 2].map((row) => (
          <div key={row} className="skeleton-row">
            <span className="bone bone-line" />
            <span className="bone bone-count" />
          </div>
        ))}
      </div>
    </div>
  )
}
