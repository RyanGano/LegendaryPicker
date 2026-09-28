import { useId, type ReactNode } from 'react'
import type { Component, Setup } from './api/setupApi.ts'
import { SetupSummary } from './SetupSummary.tsx'
import { TermKind } from './TermChips.tsx'

// The drawn cards, then the setup as a checklist in the order a player lays it out. Every count and note comes from
// the API response as-is: the client never adds, splits or infers a rule. The ticks are local
// state, so a new setup, which remounts this component, starts unticked.
export function SetupChecklist({ setup }: { setup: Setup }) {
  const { villainDeck, heroDeck, stacks, playerDeck } = setup

  return (
    <article className="setup">
      <h2>Setup for {plural(setup.players, 'player')}</h2>

      <SetupSummary setup={setup} />

      <Section title="Scheme and Mastermind">
        <Item label={setup.scheme.name} detail="Scheme" />
        <Item label={setup.mastermind.name} detail="Mastermind" />
      </Section>

      <Section title="Villain Deck">
        <Item label="Scheme Twists" count={villainDeck.twists} />
        <Item label="Master Strikes" count={villainDeck.masterStrikes} />
        <Item
          label={names(setup.villainGroups)}
          detail={plural(setup.villainGroups.length, 'Villain Group')}
          count={villainDeck.villainCards}
        />
        <Item
          label={names(setup.henchmanGroups)}
          detail={plural(setup.henchmanGroups.length, 'Henchman Group')}
          count={villainDeck.henchmanCards}
        />
        <Item label="Bystanders" count={villainDeck.bystanders} />
        {villainDeck.heroCards > 0 && <Item label="Hero cards from the Hero Deck" count={villainDeck.heroCards} />}
        <Total count={villainDeck.total} />
      </Section>

      {setup.twistsBesideScheme > 0 && (
        <Section title="Beside the Scheme">
          <Item label="Scheme Twists" count={setup.twistsBesideScheme} />
        </Section>
      )}

      <Section title="Hero Deck">
        <Item label={names(setup.heroes)} detail={plural(setup.heroes.length, 'Hero', 'Heroes')} count={heroDeck.heroCards} />
        {heroDeck.movedToVillainDeck > 0 && (
          <li className="row">
            <span className="label">Moved to the Villain Deck</span>{' '}
            <span className="count">−{heroDeck.movedToVillainDeck}</span>
          </li>
        )}
        <Total count={heroDeck.total} />
      </Section>

      <Section title="Shared stacks">
        <Item label="Wounds" count={stacks.wounds} />
        <Item label="S.H.I.E.L.D. Officers" count={stacks.officers} />
        <Item label="Bystanders" count={stacks.bystanders} />
      </Section>

      <Section title={`Starting deck per player · ${plural(setup.players, 'player')}`}>
        <Item label="S.H.I.E.L.D. Agents" count={playerDeck.agents} />
        <Item label="S.H.I.E.L.D. Troopers" count={playerDeck.troopers} />
      </Section>

      {setup.notes.length > 0 && (
        <Section title="Why this setup" className="notes">
          {setup.notes.map((note) => (
            <li key={`${note.text}|${note.citation}`}>
              {note.text}{' '}
              <cite>
                {note.link ? (
                  <a href={note.link} target="_blank" rel="noreferrer">
                    {note.citation}
                  </a>
                ) : (
                  note.citation
                )}
              </cite>
            </li>
          ))}
        </Section>
      )}

      {setup.glossary.length > 0 && (
        <Section title="Terms in this setup" className="glossary">
          {setup.glossary.map((term) => (
            <li key={term.id} className={term.kind}>
              <span className="term-name">{term.name}</span>{' '}
              <span className="detail">
                <TermKind kind={term.kind} />
              </span>{' '}
              {term.summary}{' '}
              <cite>
                <a href={term.link} target="_blank" rel="noreferrer">
                  {term.citation}
                </a>
              </cite>
            </li>
          ))}
        </Section>
      )}
    </article>
  )
}

function Section({
  title,
  className = 'checklist',
  children,
}: {
  title: string
  className?: string
  children: ReactNode
}) {
  const id = useId()
  return (
    <section aria-labelledby={id}>
      <h3 id={id}>{title}</h3>
      <ul className={className}>{children}</ul>
    </section>
  )
}

// One line to lay out, with a box to tick once it is on the table. Where the line is drawn cards,
// their names are the label and the detail says what they are.
function Item({ label, detail, count }: { label: string; detail?: string; count?: number }) {
  return (
    <li className="row">
      <label>
        <input type="checkbox" />
        <span className="label">
          {label}
          {detail && <span className="detail"> {detail}</span>}
        </span>
        {count !== undefined && <span className="count"> {count}</span>}
      </label>
    </li>
  )
}

function Total({ count }: { count: number }) {
  return (
    <li className="row total">
      <span className="label">Total</span> <span className="count">{count}</span>
    </li>
  )
}

// The drawn groups or Heroes by name; the checklist never lists the cards inside them.
function names(components: Component[]) {
  return components.map((component) => component.name).join(', ')
}

function plural(count: number, singular: string, pluralForm = `${singular}s`) {
  return `${count} ${count === 1 ? singular : pluralForm}`
}
