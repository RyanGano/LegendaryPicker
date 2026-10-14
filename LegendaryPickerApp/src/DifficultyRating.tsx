import type { Complexity, Difficulty } from './api/setupApi.ts'

type Rated = { rating: number; band: string; reasons: string[] }

// A rating the API gives the drawn setup: five pips, the band name, and a "why" the player can open. The client only
// shows what the API says; it never works out a rating.
function RatingPanel({ label, className, rated }: { label: string; className: string; rated: Rated }) {
  const { rating, band, reasons } = rated
  return (
    <section className={className} aria-label={label}>
      <div className="difficulty-head">
        <span className="difficulty-label">{label}</span>
        <span className="difficulty-pips" role="img" aria-label={`${rating} of 5`}>
          {[1, 2, 3, 4, 5].map((pip) => (
            <span key={pip} className={pip <= rating ? 'difficulty-pip on' : 'difficulty-pip'} />
          ))}
        </span>
        <span className="difficulty-band">{band}</span>
      </div>
      {reasons.length > 0 && (
        <details>
          <summary>Why this rating</summary>
          <ul>
            {reasons.map((reason) => (
              <li key={reason}>{reason}</li>
            ))}
          </ul>
        </details>
      )}
    </section>
  )
}

export function DifficultyRating({ difficulty }: { difficulty: Difficulty }) {
  return <RatingPanel label="Difficulty" className="difficulty" rated={difficulty} />
}

// How much there is to learn and remember in the drawn setup, shown beside the difficulty.
export function ComplexityRating({ complexity }: { complexity: Complexity }) {
  return <RatingPanel label="Complexity" className="difficulty complexity" rated={complexity} />
}
