import { useId, useState } from 'react'
import type { SavedSetup } from './savedSetups.ts'

type SetupCodeProps = {
  code: string
  // The name a new save starts with.
  defaultName: string
  // This setup's entry in Saved setups, when it has one.
  saved?: SavedSetup
  // Saves the setup on this device; false when the browser won't keep it.
  onSave: (name: string, note: string) => boolean
}

// The setup code under the drawn cards (#335): the code, buttons that copy it or a link that reloads the setup, and
// a small form that saves it on this device with a name and note. Copying is a convenience: without clipboard access
// the code can still be selected and copied by hand.
export function SetupCode({ code, defaultName, saved, onSave }: SetupCodeProps) {
  const id = useId()
  const [message, setMessage] = useState<string | null>(null)
  const [saving, setSaving] = useState(false)
  const [name, setName] = useState('')
  const [note, setNote] = useState('')
  const link = `${window.location.origin}${window.location.pathname}?code=${encodeURIComponent(code)}`

  async function copy(text: string, what: string) {
    try {
      await navigator.clipboard.writeText(text)
      setMessage(what)
    } catch {
      setMessage(null)
    }
  }

  function startSaving() {
    setName(saved?.name ?? defaultName)
    setNote(saved?.note ?? '')
    setMessage(null)
    setSaving(true)
  }

  function save() {
    if (onSave(name.trim() || defaultName, note.trim())) {
      setSaving(false)
      setMessage('Saved on this device')
    } else {
      setMessage("Couldn't save: this browser isn't keeping site data.")
    }
  }

  return (
    <section className="setup-code" aria-labelledby={id}>
      <h3 id={id} className="setup-code-heading">
        Setup code
      </h3>
      <p className="setup-code-value">
        <code>{code}</code>
      </p>
      <div className="setup-code-actions">
        <button type="button" className="secondary" onClick={() => void copy(code, 'Code copied')}>
          Copy code
        </button>
        <button type="button" className="secondary" onClick={() => void copy(link, 'Link copied')}>
          Copy link
        </button>
        {!saving && (
          <button type="button" className="secondary" onClick={startSaving}>
            {saved ? 'Edit save' : 'Save'}
          </button>
        )}
      </div>
      {saving && (
        <form
          className="save-form"
          aria-label="Save this setup"
          onSubmit={(event) => {
            event.preventDefault()
            save()
          }}
        >
          <label>
            Name
            <input type="text" maxLength={80} autoComplete="off" value={name} onChange={(event) => setName(event.target.value)} />
          </label>
          <label>
            Note <span className="detail">Optional</span>
            <input type="text" maxLength={300} autoComplete="off" value={note} onChange={(event) => setNote(event.target.value)} />
          </label>
          <div className="setup-code-actions">
            <button type="submit" className="secondary">
              Save on this device
            </button>
            <button type="button" className="link-button" onClick={() => setSaving(false)}>
              Cancel
            </button>
          </div>
        </form>
      )}
      <p className="setup-code-copied" role="status">
        {message}
      </p>
    </section>
  )
}
