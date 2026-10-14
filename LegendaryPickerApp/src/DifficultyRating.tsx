import type { Difficulty } from './api/setupApi.ts'

// The rating the API gives the drawn Hero Deck: five pips, the band name, and a "why" the player can open. The
// client only shows what the API says; it never works out a rating.
export function DifficultyRating({ difficulty }: { difficulty: Difficulty }) {
  const { rating, band, reasons } = difficulty
  return (
    <section className="difficulty" aria-label="Difficulty">
      <div className="difficulty-head">
        <span className="difficulty-label">Difficulty</span>
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
