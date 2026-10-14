import type { Complexity, Difficulty } from './api/setupApi.ts'

type Rated = { rating: number; band: string; reasons: string[]; target?: number }

// A rating the API gives the drawn setup: five pips, the band name, and a "why" the player can open. The client only
// shows what the API says; it never works out a rating.
function RatingPanel({
  label,
  className,
  rated,
  bands,
}: {
  label: string
  className: string
  rated: Rated
  bands?: string[]
}) {
  const { rating, band, reasons, target } = rated
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
      {target !== undefined && target !== rating && (
        <p className="difficulty-request">
          You asked for {bands?.[target - 1] ?? `rating ${target}`}. This is the closest legal setup drawn.
        </p>
      )}
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

export function DifficultyRating({ difficulty, bands }: { difficulty: Difficulty; bands?: string[] }) {
  return <RatingPanel label="Difficulty" className="difficulty" rated={difficulty} bands={bands} />
}

// How much there is to learn and remember in the drawn setup, shown beside the difficulty.
export function ComplexityRating({ complexity, bands }: { complexity: Complexity; bands?: string[] }) {
  return <RatingPanel label="Complexity" className="difficulty complexity" rated={complexity} bands={bands} />
}
