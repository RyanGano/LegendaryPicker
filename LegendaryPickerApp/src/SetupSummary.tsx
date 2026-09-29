import { useId, type ReactNode, type Ref } from 'react'
import type { Component, RuleNote, Setup } from './api/setupApi.ts'
import { rulesetTerms } from './rulesetTerms.ts'
import { TermChips } from './TermChips.tsx'

// What the players are playing, before how to lay it out: the setup's heading, the drawn Scheme
// and Mastermind as cards, then every Hero and group as its own tile. Under each name sit the chips
// for the glossary terms that component uses. When the setup includes more than one box, the box
// each one comes from sits under its name. Card types use the words of the setup's ruleset. On a wide
// screen this sits beside the checklist.
export function SetupSummary({ setup, headingRef }: { setup: Setup; headingRef?: Ref<HTMLHeadingElement> }) {
  const glossary = new Map(setup.glossary.map((entry) => [entry.id, entry]))
  const t = rulesetTerms(setup)
  const chipsOf = (component: Component) => (
    <TermChips terms={component.terms.flatMap((id) => glossary.get(id) ?? [])} hero={t.hero} />
  )

  return (
    <>
      <h2 ref={headingRef} tabIndex={-1} className="result-heading">
        Setup for {setup.players} {setup.players === 1 ? 'player' : 'players'}
      </h2>
      <section className="summary" aria-label="Drawn cards">
        <div className="headline-cards">
          <HeadlineCard type={t.scheme} kind="scheme" component={setup.scheme} chips={chipsOf(setup.scheme)} />
          <HeadlineCard
            type={t.mastermind}
            kind="mastermind"
            component={setup.mastermind}
            chips={chipsOf(setup.mastermind)}
            tags={alwaysLeadsNotes(setup.notes)}
          />
        </div>
        <TileGroup title={[t.hero, t.heroes]} kind="hero" components={setup.heroes} chipsOf={chipsOf} />
        <TileGroup
          title={[t.villainGroup, t.villainGroups]}
          kind="villain"
          components={setup.villainGroups}
          chipsOf={chipsOf}
        />
        <TileGroup
          title={[t.henchmanGroup, t.henchmanGroups]}
          kind="henchman"
          components={setup.henchmanGroups}
          chipsOf={chipsOf}
        />
      </section>
    </>
  )
}

function HeadlineCard({
  type,
  kind,
  component,
  chips,
  tags = [],
}: {
  type: string
  kind: string
  component: Component
  chips: ReactNode
  tags?: string[]
}) {
  return (
    <div className={`headline-card ${kind}`}>
      <p className="card-type">{type}</p>
      <h3>{component.name}</h3>
      {component.box && <p className="source-box">{component.box}</p>}
      {chips}
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
  chipsOf,
}: {
  title: [string, string]
  kind: string
  components: Component[]
  chipsOf: (component: Component) => ReactNode
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
            {component.box && <span className="source-box">{component.box}</span>}
            {chipsOf(component)}
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
