import { useId, useLayoutEffect, useRef, useState, type ReactNode, type Ref } from 'react'
import type { Component, Setup } from './api/setupApi.ts'
import { SetupSummary } from './SetupSummary.tsx'
import { TermKind } from './TermChips.tsx'

// The drawn cards, then the setup as a checklist in the order a player lays it out. Every count and note comes from
// the API response as-is: the client never adds, splits or infers a rule. The ticks are local
// state, so a new setup, which remounts this component, starts unticked. The progress line counts
// the tick boxes on the page, so it can never disagree with the lines shown.
export function SetupChecklist({ setup, headingRef }: { setup: Setup; headingRef?: Ref<HTMLHeadingElement> }) {
  const { villainDeck, heroDeck, stacks, playerDeck } = setup
  const article = useRef<HTMLElement>(null)
  const [progress, setProgress] = useState({ ticked: 0, total: 0 })
  const countTicks = () => setProgress(tickProgress(article.current!))

  useLayoutEffect(() => setProgress(tickProgress(article.current!)), [setup])

  return (
    <article className="setup" ref={article} onChange={countTicks}>
      <h2 ref={headingRef} tabIndex={-1}>
        Setup for {plural(setup.players, 'player')}
      </h2>

      <SetupSummary setup={setup} />

      <p className="progress" role="status">
        {progress.ticked} of {progress.total} laid out
      </p>

      <Section title="Scheme and Mastermind" kind="mastermind">
        <Item label={setup.scheme.name} detail="Scheme" />
        <Item label={setup.mastermind.name} detail="Mastermind" />
      </Section>

      <Section title="Villain Deck" kind="villain">
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
        <Section title="Beside the Scheme" kind="scheme">
          <Item label="Scheme Twists" count={setup.twistsBesideScheme} />
        </Section>
      )}

      <Section title="Hero Deck" kind="hero">
        <Item label={names(setup.heroes)} detail={plural(setup.heroes.length, 'Hero', 'Heroes')} count={heroDeck.heroCards} />
        {heroDeck.movedToVillainDeck > 0 && (
          <li className="row">
            <span className="label">Moved to the Villain Deck</span>{' '}
            <span className="count">−{heroDeck.movedToVillainDeck}</span>
          </li>
        )}
        <Total count={heroDeck.total} />
      </Section>

      <Section title="Shared stacks" kind="wound">
        <Item label="Wounds" count={stacks.wounds} />
        <Item label="S.H.I.E.L.D. Officers" count={stacks.officers} />
        <Item label="Bystanders" count={stacks.bystanders} />
      </Section>

      <Section title={`Starting deck per player · ${plural(setup.players, 'player')}`} kind="shield">
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
              {note.box && <span className="source-box">{note.box}</span>}
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
              {term.box && <span className="source-box">{term.box}</span>}
            </li>
          ))}
        </Section>
      )}
    </article>
  )
}

// A checklist section takes the accent of the card type it lays out.
function Section({
  title,
  kind,
  className = 'checklist',
  children,
}: {
  title: string
  kind?: string
  className?: string
  children: ReactNode
}) {
  const id = useId()
  return (
    <section aria-labelledby={id} className={kind && `checklist-section ${kind}`}>
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

// Lines ticked against lines to lay out. Totals and the Hero cards moved have no tick box, so they
// don't count.
function tickProgress(article: HTMLElement) {
  const boxes = [...article.querySelectorAll<HTMLInputElement>('.row input[type="checkbox"]')]
  return { ticked: boxes.filter((box) => box.checked).length, total: boxes.length }
}

// The drawn groups or Heroes by name; the checklist never lists the cards inside them.
function names(components: Component[]) {
  return components.map((component) => component.name).join(', ')
}

function plural(count: number, singular: string, pluralForm = `${singular}s`) {
  return `${count} ${count === 1 ? singular : pluralForm}`
}
