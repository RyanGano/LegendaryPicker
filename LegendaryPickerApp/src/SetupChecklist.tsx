import { Fragment, useId, useLayoutEffect, useRef, useState, type ReactNode } from 'react'
import type { CardKind, Component, Move, Pile, Setup } from './api/setupApi.ts'
import { TermKind } from './TermChips.tsx'

// The setup as a checklist in the order a player lays it out. Every count and note comes from
// the API response as-is: the client never adds, splits or infers a rule. The ticks are local
// state, so a new setup, which remounts this component, starts unticked. The progress line counts
// the tick boxes on the page, so it can never disagree with the lines shown.
export function SetupChecklist({ setup }: { setup: Setup }) {
  // An API from before card moves, Heroes outside the Hero Deck, Henchman Groups outside the Villain Deck or
  // setup steps leaves them out; the site can deploy first, so read that as none.
  const {
    villainDeck,
    heroDeck,
    stacks,
    playerDeck,
    moves = [],
    outsideHeroes = [],
    outsideHenchmen = [],
    steps = [],
  } = setup
  const article = useRef<HTMLElement>(null)
  const [progress, setProgress] = useState({ ticked: 0, total: 0 })
  const countTicks = () => setProgress(tickProgress(article.current!))

  useLayoutEffect(() => setProgress(tickProgress(article.current!)), [setup])

  return (
    <article className="setup" ref={article} onChange={countTicks}>
      <p className="progress" role="status">
        {progress.ticked} of {progress.total} laid out
      </p>

      <Section title="Scheme and Mastermind" kind="mastermind">
        <Item label={names([setup.scheme])} detail="Scheme" />
        <Item label={names([setup.mastermind])} detail="Mastermind" />
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
        <MovedIn moves={moves} to="villainDeck" />
        <MovedOut moves={moves} from="villainDeck" />
        <OutsideHeroCards count={villainDeck.outsideHeroCards} />
        <Total count={villainDeck.total} />
      </Section>

      {(setup.twistsBesideScheme > 0 || moves.some((move) => move.to === 'besideScheme')) && (
        <Section title="Beside the Scheme" kind="scheme">
          {setup.twistsBesideScheme > 0 && <Item label="Scheme Twists" count={setup.twistsBesideScheme} />}
          <MovedIn moves={moves} to="besideScheme" />
        </Section>
      )}

      <Section title="Hero Deck" kind="hero">
        <Item label={names(setup.heroes)} detail={plural(setup.heroes.length, 'Hero', 'Heroes')} count={heroDeck.heroCards} />
        <MovedIn moves={moves} to="heroDeck" />
        {outsideHenchmen.map(
          (outside) =>
            outside.to === 'heroDeck' && (
              <Item
                key={outside.group.id}
                label={names([outside.group])}
                detail="Henchmen of an extra Henchman Group"
                count={outside.cards}
              />
            ),
        )}
        <MovedOut moves={moves} from="heroDeck" />
        <Total count={heroDeck.total} />
      </Section>

      {outsideHeroes.length > 0 && (
        <Section title="Heroes outside the Hero Deck" kind="hero">
          {outsideHeroes.map((outside) => (
            <Item key={outside.hero.id} label={names([outside.hero])} detail={INTO[outside.to]} count={outside.cards} />
          ))}
        </Section>
      )}

      <Section title="Shared stacks" kind="wound">
        <Item label="Wounds" count={stacks.wounds} />
        <Item label="S.H.I.E.L.D. Officers" count={stacks.officers} />
        <Item label="Bystanders" count={stacks.bystanders} />
        {stacks.sidekicks !== undefined && <Item label="Sidekicks" count={stacks.sidekicks} />}
      </Section>

      <Section title={`Starting deck per player · ${plural(setup.players, 'player')}`} kind="shield">
        <Item label="S.H.I.E.L.D. Agents" count={playerDeck.agents} />
        <Item label="S.H.I.E.L.D. Troopers" count={playerDeck.troopers} />
        <MovedIn moves={moves} to="startingDecks" />
      </Section>

      {steps.length > 0 && (
        <Section title="Other setup steps" kind="scheme">
          {/* Keyed by position: a Scheme and its Mastermind can print the same step. */}
          {steps.map((step, index) => (
            <Item key={index} label={step} />
          ))}
        </Section>
      )}

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
// their names are the label and the detail says what they are. The whole line names the tick box,
// so the spaces sit between the spans: a name computation may trim the text inside each one.
function Item({ label, detail, count }: { label: ReactNode; detail?: string; count?: number }) {
  return (
    <li className="row">
      <label>
        <input type="checkbox" />
        <span className="label">
          {label}
          {detail && <> <span className="detail">{detail}</span></>}
        </span>
        {count !== undefined && <> <span className="count">{count}</span></>}
      </label>
    </li>
  )
}

// The cards the Scheme moves into a pile, each a line to lay out, named with where they come from.
function MovedIn({ moves, to }: { moves: Move[]; to: Pile }) {
  // Keyed by position in the setup's moves: a Scheme can move the same kind of card to one pile twice.
  return moves.map(
    (move, index) =>
      move.to === to && (
        <Item key={index} label={`${CARD_NAMES[move.card]} from the ${PILE_NAMES[move.from]}`} count={move.count} />
      ),
  )
}

// The cards the Scheme moves out of a deck: taken away rather than laid out, so no tick box.
function MovedOut({ moves, from }: { moves: Move[]; from: Pile }) {
  return moves.map(
    (move, index) =>
      move.from === from && (
        <li key={index} className="row">
          <span className="label">Moved {INTO[move.to]}</span> <span className="count">−{move.total}</span>
        </li>
      ),
  )
}

// The cards of the Heroes outside the Hero Deck that go into the Villain Deck. They are laid out, and
// ticked, in their own section, so here they only show what the total holds.
function OutsideHeroCards({ count = 0 }: { count?: number }) {
  return (
    count > 0 && (
      <li className="row">
        <span className="label">Cards of the Heroes outside the Hero Deck</span> <span className="count">{count}</span>
      </li>
    )
  )
}

const CARD_NAMES: Record<CardKind, string> = {
  hero: 'Hero cards',
  henchman: 'Henchmen',
  bystander: 'Bystanders',
  wound: 'Wounds',
  officer: 'S.H.I.E.L.D. Officers',
  sidekick: 'Sidekicks',
}

const PILE_NAMES: Record<Pile, string> = {
  villainDeck: 'Villain Deck',
  heroDeck: 'Hero Deck',
  besideScheme: 'pile beside the Scheme',
  startingDecks: 'starting decks',
  setAside: 'stack set aside',
  bystanders: 'Bystander stack',
  wounds: 'Wound stack',
  officers: 'Officer stack',
  sidekicks: 'Sidekick stack',
}

// Where a move, or a Hero outside the Hero Deck, puts cards. Moves only go to the four destinations and
// those Heroes to the Villain Deck, beside the Scheme or a stack set aside, never to a shared stack.
const INTO: Record<Pile, string> = {
  villainDeck: 'to the Villain Deck',
  heroDeck: 'to the Hero Deck',
  besideScheme: 'beside the Scheme',
  startingDecks: 'to each starting deck',
  setAside: 'to a stack set aside',
  bystanders: 'to the Bystander stack',
  wounds: 'to the Wound stack',
  officers: 'to the Officer stack',
  sidekicks: 'to the Sidekick stack',
}

function Total({ count }: { count: number }) {
  return (
    <li className="row total">
      <span className="label">Total</span> <span className="count">{count}</span>
    </li>
  )
}

// Lines ticked against lines to lay out. Totals, the cards moved out of a deck and the Villain Deck's
// line for the Heroes outside the Hero Deck have no tick box, so they don't count.
function tickProgress(article: HTMLElement) {
  const boxes = [...article.querySelectorAll<HTMLInputElement>('.row input[type="checkbox"]')]
  return { ticked: boxes.filter((box) => box.checked).length, total: boxes.length }
}

// The drawn cards by name; the checklist never lists the cards inside a group or Hero. When the setup
// includes more than one box, each name stands on its own line with its box under it, so the player
// knows which box to pull it from.
function names(components: Component[]) {
  if (!components.some((component) => component.box)) {
    return components.map((component) => component.name).join(', ')
  }
  // The space between names keeps the tick box's name readable: "HYDRA Dark City Skrulls Dark City".
  return components.map((component, index) => (
    <Fragment key={component.id}>
      {index > 0 && ' '}
      <span className="card-name">
        {component.name} <span className="source-box">{component.box}</span>
      </span>
    </Fragment>
  ))
}

function plural(count: number, singular: string, pluralForm = `${singular}s`) {
  return `${count} ${count === 1 ? singular : pluralForm}`
}
