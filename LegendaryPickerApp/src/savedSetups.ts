// Saved setups (#335): setups a player kept on this device, each stored as its setup code with the name and note the
// player gave it and when it was saved. No cards are stored: loading one reloads its code from the API.
export type SavedSetup = { code: string; name: string; note: string; savedAt: string }

const SAVED_SETUPS_KEY = 'legendaryPicker.savedSetups'

// The saved setups, newest first. Storage is a convenience: without it, or with an entry this version can't read,
// the list is shorter rather than broken.
export function loadSavedSetups(): SavedSetup[] {
  try {
    const stored: unknown = JSON.parse(localStorage.getItem(SAVED_SETUPS_KEY) ?? '[]')
    return Array.isArray(stored) ? stored.filter(isSavedSetup) : []
  } catch {
    return []
  }
}

// Whether the list was kept; private browsing or blocked site data can refuse it.
export function storeSavedSetups(saved: SavedSetup[]): boolean {
  try {
    localStorage.setItem(SAVED_SETUPS_KEY, JSON.stringify(saved))
    return true
  } catch {
    return false
  }
}

// The list with this setup saved at the top. Saving a code again renames it rather than listing it twice.
export function withSaved(saved: SavedSetup[], entry: SavedSetup): SavedSetup[] {
  return [entry, ...saved.filter((other) => other.code !== entry.code)]
}

function isSavedSetup(value: unknown): value is SavedSetup {
  if (typeof value !== 'object' || value === null) return false
  const entry = value as Record<string, unknown>
  return (
    typeof entry.code === 'string' &&
    typeof entry.name === 'string' &&
    typeof entry.note === 'string' &&
    typeof entry.savedAt === 'string'
  )
}
