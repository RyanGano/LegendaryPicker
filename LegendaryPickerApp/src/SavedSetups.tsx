import type { SavedSetup } from './savedSetups.ts'

type SavedSetupsProps = {
  saved: SavedSetup[]
  disabled: boolean
  onLoad: (code: string) => void
  onDelete: (code: string) => void
}

// The setups saved on this device (#335), under Load a setup: each one's name, note and date, with buttons that
// reload it from its code or delete it. Nothing shows until a setup has been saved.
export function SavedSetups({ saved, disabled, onLoad, onDelete }: SavedSetupsProps) {
  if (saved.length === 0) return null
  return (
    <section className="saved-setups" aria-labelledby="saved-label">
      <h2 id="saved-label">Saved setups</h2>
      <ul className="saved-list">
        {saved.map((entry) => (
          <li key={entry.code} className="saved-entry">
            <div className="saved-text">
              <span className="saved-name">{entry.name}</span>
              {entry.note && <span className="saved-note">{entry.note}</span>}
              <time className="saved-date" dateTime={entry.savedAt}>
                Saved {new Date(entry.savedAt).toLocaleDateString(undefined, { dateStyle: 'medium' })}
              </time>
            </div>
            <div className="saved-actions">
              <button
                type="button"
                className="secondary"
                aria-label={`Load ${entry.name}`}
                disabled={disabled}
                onClick={() => onLoad(entry.code)}
              >
                Load
              </button>
              <button
                type="button"
                className="link-button"
                aria-label={`Delete ${entry.name}`}
                disabled={disabled}
                onClick={() => onDelete(entry.code)}
              >
                Delete
              </button>
            </div>
          </li>
        ))}
      </ul>
    </section>
  )
}
