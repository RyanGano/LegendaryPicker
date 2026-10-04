import { Fragment, useId, useLayoutEffect, useRef, useState, type ReactNode } from 'react'
import type { CardKind, CardsBeside, Component, Move, Pile, Setup, SetupStacks } from './api/setupApi.ts'
import { cardTerms, setupTerms, startingDeckTerms, type RulesetTerms } from './rulesetTerms.ts'
import { TermKind } from './TermChips.tsx'

// The setup as a checklist in the order a player lays it out. Every count and note comes from
// the API response as-is: the client never adds, splits or infers a rule. Parts of the setup are named
// in the words of its ruleset, so a Villainous setup lays out a Plot and an Adversary Deck; a mixed setup
// names each card by its own ruleset's word and a part holding both kinds by both words. The ticks are
// local state, so a new setup, which remounts this component, starts unticked. The progress line counts
// the tick boxes on the page, so it can never disagree with the lines shown.
export function SetupChecklist({ setup }: { setup: Setup }) {
  // An API from before card moves, Heroes outside the Hero Deck, Henchman Groups outside the Villain Deck, cards
  // of a group beside the Scheme or setup steps leaves them out; the site can deploy first, so read that as none.
  const {
    villainDeck,
    heroDeck,
    stacks,
    playerDeck,
    moves = [],
    outsideHeroes = [],
    outsideHenchmen = [],
    cardsBeside = [],
    steps = [],
  } = setup
  const t = setupTerms(setup)
  const villainTerms = cardTerms(setup, setup.villainGroups)
  const henchmanTerms = cardTerms(setup, setup.henchmanGroups)
  const heroTerms = cardTerms(setup, setup.heroes)
  const decks = startingDeckTerms(setup)
  const article = useRef<HTMLElement>(null)
  const [progress, setProgress] = useState({ ticked: 0, total: 0 })
  const countTicks = () => setProgress(tickProgress(article.current!))

  useLayoutEffect(() => setProgress(tickProgress(article.current!)), [setup])

  return (
    <article className="setup" ref={article} onChange={countTicks}>
      <p className="progress" role="status">
        {progress.ticked} of {progress.total} laid out
      </p>

      <Section title={`${t.scheme} and ${t.mastermind}`} kind="mastermind">
        <Item label={names([setup.scheme])} detail={t.scheme} />
        <Item label={names([setup.mastermind])} detail={t.mastermind} />
      </Section>

      <Section title={t.villainDeck} kind="villain">
        <Item label={t.twists} count={villainDeck.twists} />
        <Item label={t.masterStrikes} count={villainDeck.masterStrikes} />
        <Item
          label={names(setup.villainGroups)}
          detail={plural(setup.villainGroups.length, villainTerms.villainGroup, villainTerms.villainGroups)}
          count={villainDeck.villainCards}
        />
        <Item
          label={names(setup.henchmanGroups)}
          detail={plural(setup.henchmanGroups.length, henchmanTerms.henchmanGroup, henchmanTerms.henchmanGroups)}
          count={villainDeck.henchmanCards}
        />
        <Item label="Bystanders" count={villainDeck.bystanders} />
        <MovedIn moves={moves} to="villainDeck" terms={t} />
        <MovedOut moves={moves} from="villainDeck" terms={t} />
        <SetBeside cardsBeside={cardsBeside} terms={t} />
        <OutsideHeroCards count={villainDeck.outsideHeroCards} terms={t} />
        <Total count={villainDeck.total} />
      </Section>

      {(setup.twistsBesideScheme > 0 || moves.some((move) => move.to === 'besideScheme') || cardsBeside.length > 0) && (
        <Section title={`Beside the ${t.scheme}`} kind="scheme">
          {setup.twistsBesideScheme > 0 && <Item label={t.twists} count={setup.twistsBesideScheme} />}
          <MovedIn moves={moves} to="besideScheme" terms={t} />
          {cardsBeside.map((beside) => (
            <Item
              key={beside.group.id}
              label={beside.card ? <>{beside.card} of {names([beside.group])}</> : names([beside.group])}
              count={beside.count}
            />
          ))}
        </Section>
      )}

      {moves.some((move) => move.to === 'setAside') && (
        <Section title="Set aside" kind="scheme">
          <MovedIn moves={moves} to="setAside" terms={t} />
        </Section>
      )}

      <Section title={t.heroDeck} kind="hero">
        <Item
          label={names(setup.heroes)}
          detail={plural(setup.heroes.length, heroTerms.hero, heroTerms.heroes)}
          count={heroDeck.heroCards}
        />
        <MovedIn moves={moves} to="heroDeck" terms={t} />
        {outsideHenchmen.map(
          (outside) =>
            outside.to === 'heroDeck' && (
              <Item
                key={outside.group.id}
                label={names([outside.group])}
                detail={`${t.henchmen} of an extra ${t.henchmanGroup}`}
                count={outside.cards}
              />
            ),
        )}
        <MovedOut moves={moves} from="heroDeck" terms={t} />
        <Total count={heroDeck.total} />
      </Section>

      {outsideHeroes.length > 0 && (
        <Section title={`${t.heroes} outside the ${t.heroDeck}`} kind="hero">
          {outsideHeroes.map((outside) => (
            <Item key={outside.hero.id} label={names([outside.hero])} detail={into(t)[outside.to]} count={outside.cards} />
          ))}
        </Section>
      )}

      <Section title="Shared stacks" kind="wound">
        {STACKS.map(([stack, label]) => {
          const count = stacks[stack]
          return count !== undefined && <Item key={stack} label={label} count={count} />
        })}
      </Section>

      <Section title={`Starting deck per player · ${plural(setup.players, 'player')}`} kind="shield">
        {decks.length > 1 && (
          <Item label={`Pick one starting team for every player: ${decks.map((deck) => deck.team).join(' or ')}`} />
        )}
        <Item label={decks.map((deck) => deck.agents).join(' or ')} count={playerDeck.agents} />
        <Item label={decks.map((deck) => deck.troopers).join(' or ')} count={playerDeck.troopers} />
        <MovedIn moves={moves} to="startingDecks" terms={t} />
      </Section>

      {steps.length > 0 && (
        // No card type's accent: the steps come from the Scheme and the Mastermind alike, and the API
        // doesn't say which printed each one.
        <Section title="Other setup steps">
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
                <TermKind kind={term.kind} hero={t.hero} />
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
function MovedIn({ moves, to, terms }: { moves: Move[]; to: Pile; terms: RulesetTerms }) {
  // Keyed by position in the setup's moves: a Scheme can move the same kind of card to one pile twice.
  return moves.map(
    (move, index) =>
      move.to === to && (
        <Item
          key={index}
          label={`${cardNames(terms)[move.card]} from the ${pileNames(terms)[move.from]}`}
          count={move.count}
        />
      ),
  )
}

// The cards the Scheme moves out of a deck: taken away rather than laid out, so no tick box.
function MovedOut({ moves, from, terms }: { moves: Move[]; from: Pile; terms: RulesetTerms }) {
  return moves.map(
    (move, index) =>
      move.from === from && (
        <li key={index} className="row">
          <span className="label">Moved {into(terms)[move.to]}</span> <span className="count">−{move.total}</span>
        </li>
      ),
  )
}

// The cards of its drawn groups the Scheme sets beside it, which the group doesn't put in the Villain Deck. They are
// laid out, and ticked, beside the Scheme, so here they are only taken away.
function SetBeside({ cardsBeside, terms }: { cardsBeside: CardsBeside[]; terms: RulesetTerms }) {
  return cardsBeside.map(
    (beside) =>
      beside.fromVillainDeck > 0 && (
        <li key={beside.group.id} className="row">
          <span className="label">{`${beside.card ?? beside.group.name} set beside the ${terms.scheme}`}</span>{' '}
          <span className="count">−{beside.fromVillainDeck}</span>
        </li>
      ),
  )
}

// The cards of the Heroes outside the Hero Deck that go into the Villain Deck. They are laid out, and
// ticked, in their own section, so here they only show what the total holds.
function OutsideHeroCards({ count = 0, terms }: { count?: number; terms: RulesetTerms }) {
  return (
    count > 0 && (
      <li className="row">
        <span className="label">{`Cards of the ${terms.heroes} outside the ${terms.heroDeck}`}</span>{' '}
        <span className="count">{count}</span>
      </li>
    )
  )
}

// The shared stacks in the order they are laid out, each shown only when the setup has it. Bindings,
// Madame HYDRA and New Recruits are Villainous cards of their own, not other names for Wounds and Officers.
const STACKS: [keyof SetupStacks, string][] = [
  ['wounds', 'Wounds'],
  ['bindings', 'Bindings'],
  ['officers', 'S.H.I.E.L.D. Officers'],
  ['madameHydra', 'Madame HYDRA'],
  ['newRecruits', 'New Recruits'],
  ['bystanders', 'Bystanders'],
  ['sidekicks', 'Sidekicks'],
  ['shards', 'Shards'],
]

function cardNames(terms: RulesetTerms): Record<CardKind, string> {
  return {
    hero: `${terms.hero} cards`,
    henchman: terms.henchmen,
    bystander: 'Bystanders',
    wound: 'Wounds',
    officer: 'S.H.I.E.L.D. Officers',
    sidekick: 'Sidekicks',
  }
}

function pileNames(terms: RulesetTerms): Record<Pile, string> {
  return {
    villainDeck: terms.villainDeck,
    heroDeck: terms.heroDeck,
    besideScheme: `pile beside the ${terms.scheme}`,
    startingDecks: 'starting decks',
    setAside: 'stack set aside',
    bystanders: 'Bystander stack',
    wounds: 'Wound stack',
    officers: 'Officer stack',
    sidekicks: 'Sidekick stack',
  }
}

// Where a move, or a Hero outside the Hero Deck, puts cards. Moves only go to the five destinations and
// those Heroes to the Villain Deck, beside the Scheme or a stack set aside, never to a shared stack.
function into(terms: RulesetTerms): Record<Pile, string> {
  return {
    villainDeck: `to the ${terms.villainDeck}`,
    heroDeck: `to the ${terms.heroDeck}`,
    besideScheme: `beside the ${terms.scheme}`,
    startingDecks: 'to each starting deck',
    setAside: 'to a stack set aside',
    bystanders: 'to the Bystander stack',
    wounds: 'to the Wound stack',
    officers: 'to the Officer stack',
    sidekicks: 'to the Sidekick stack',
  }
}

function Total({ count }: { count: number }) {
  return (
    <li className="row total">
      <span className="label">Total</span> <span className="count">{count}</span>
    </li>
  )
}

// Lines ticked against lines to lay out. Totals, the cards moved out of a deck or set beside the Scheme, and the
// Villain Deck's line for the Heroes outside the Hero Deck have no tick box, so they don't count.
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
