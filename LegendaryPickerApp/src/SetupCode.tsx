import { useId, useState } from 'react'

// The setup code under the drawn cards (#335): the code, and buttons that copy it or a link that reloads the setup.
// Copying is a convenience: without clipboard access the code can still be selected and copied by hand.
export function SetupCode({ code }: { code: string }) {
  const id = useId()
  const [copied, setCopied] = useState<string | null>(null)
  const link = `${window.location.origin}${window.location.pathname}?code=${encodeURIComponent(code)}`

  async function copy(text: string, what: string) {
    try {
      await navigator.clipboard.writeText(text)
      setCopied(what)
    } catch {
      setCopied(null)
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
      </div>
      <p className="setup-code-copied" role="status">
        {copied}
      </p>
    </section>
  )
}
