import { useId } from 'react'
import type { Component, GlossaryEntry, RuleNote, Setup } from './api/setupApi.ts'
import { TermChips } from './TermChips.tsx'

// What the players are playing, before how to lay it out: the drawn Scheme and Mastermind as
// cards, then every Hero and group as its own tile. Under each name sit the chips for the glossary
// terms that component uses.
export function SetupSummary({ setup }: { setup: Setup }) {
  const glossary = new Map(setup.glossary.map((entry) => [entry.id, entry]))
  const termsOf = (component: Component) => component.terms.flatMap((id) => glossary.get(id) ?? [])

  return (
    <section className="summary" aria-label="Drawn cards">
      <div className="headline-cards">
        <HeadlineCard type="Scheme" kind="scheme" name={setup.scheme.name} terms={termsOf(setup.scheme)} />
        <HeadlineCard
          type="Mastermind"
          kind="mastermind"
          name={setup.mastermind.name}
          terms={termsOf(setup.mastermind)}
          tags={alwaysLeadsNotes(setup.notes)}
        />
      </div>
      <TileGroup title={['Hero', 'Heroes']} kind="hero" components={setup.heroes} termsOf={termsOf} />
      <TileGroup
        title={['Villain Group', 'Villain Groups']}
        kind="villain"
        components={setup.villainGroups}
        termsOf={termsOf}
      />
      <TileGroup
        title={['Henchman Group', 'Henchman Groups']}
        kind="henchman"
        components={setup.henchmanGroups}
        termsOf={termsOf}
      />
    </section>
  )
}

function HeadlineCard({
  type,
  kind,
  name,
  terms,
  tags = [],
}: {
  type: string
  kind: string
  name: string
  terms: GlossaryEntry[]
  tags?: string[]
}) {
  return (
    <div className={`headline-card ${kind}`}>
      <p className="card-type">{type}</p>
      <h3>{name}</h3>
      <TermChips terms={terms} />
      {tags.length > 0 && (
        <ul className="tags">
          {tags.map((tag) => (
            <li key={tag} className="tag">
              {tag}
            </li>
          ))}
        </ul>
      )}
    </div>
  )
}

function TileGroup({
  title: [singular, plural],
  kind,
  components,
  termsOf,
}: {
  title: [string, string]
  kind: string
  components: Component[]
  termsOf: (component: Component) => GlossaryEntry[]
}) {
  const id = useId()
  return (
    <section aria-labelledby={id}>
      <h3 id={id} className="tile-heading">
        {components.length === 1 ? singular : plural}
      </h3>
      <ul className={`tiles ${kind}`}>
        {components.map((component) => (
          <li key={component.id} className="tile">
            <span className="tile-name">{component.name}</span>
            <TermChips terms={termsOf(component)} />
          </li>
        ))}
      </ul>
    </section>
  )
}

// The API's notes about the Mastermind's Always Leads group (the group it brings, the Scheme
// displacing it, or Solo ignoring it), word for word. The client picks the notes out by the rule's
// name only; it never works out the group itself.
function alwaysLeadsNotes(notes: RuleNote[]) {
  return notes.map((note) => note.text).filter((text) => text.toLowerCase().includes('always leads'))
}
