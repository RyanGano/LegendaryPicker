import { useId } from 'react'
import type { Component, RuleNote, Setup } from './api/setupApi.ts'

// What the players are playing, before how to lay it out: the drawn Scheme and Mastermind as
// cards, then every Hero and group as its own tile. Each card and tile keeps its name in a
// column of its own, so a row of term chips can sit under the name later.
export function SetupSummary({ setup }: { setup: Setup }) {
  return (
    <section className="summary" aria-label="Drawn cards">
      <div className="headline-cards">
        <HeadlineCard type="Scheme" kind="scheme" name={setup.scheme.name} />
        <HeadlineCard
          type="Mastermind"
          kind="mastermind"
          name={setup.mastermind.name}
          tags={alwaysLeadsNotes(setup.notes)}
        />
      </div>
      <TileGroup title={['Hero', 'Heroes']} kind="hero" components={setup.heroes} />
      <TileGroup title={['Villain Group', 'Villain Groups']} kind="villain" components={setup.villainGroups} />
      <TileGroup title={['Henchman Group', 'Henchman Groups']} kind="henchman" components={setup.henchmanGroups} />
    </section>
  )
}

function HeadlineCard({ type, kind, name, tags = [] }: { type: string; kind: string; name: string; tags?: string[] }) {
  return (
    <div className={`headline-card ${kind}`}>
      <p className="card-type">{type}</p>
      <h3>{name}</h3>
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
}: {
  title: [string, string]
  kind: string
  components: Component[]
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
