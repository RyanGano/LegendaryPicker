import type { Component, Setup } from './api/setupApi.ts'

// The drawn components by name. The full layout checklist with counts, stacks and rule notes is #6.
export function SetupSummary({ setup }: { setup: Setup }) {
  return (
    <article className="setup">
      <h2>
        Setup for {setup.players} player{setup.players === 1 ? '' : 's'}
      </h2>
      <dl>
        <dt>Scheme</dt>
        <dd>{setup.scheme.name}</dd>
        <dt>Mastermind</dt>
        <dd>{setup.mastermind.name}</dd>
        <ComponentList title="Heroes" components={setup.heroes} />
        <ComponentList title="Villain Groups" components={setup.villainGroups} />
        <ComponentList title="Henchman Groups" components={setup.henchmanGroups} />
      </dl>
    </article>
  )
}

function ComponentList({ title, components }: { title: string; components: Component[] }) {
  return (
    <>
      <dt>{title}</dt>
      <dd>
        <ul>
          {components.map((component) => (
            <li key={component.id}>{component.name}</li>
          ))}
        </ul>
      </dd>
    </>
  )
}
