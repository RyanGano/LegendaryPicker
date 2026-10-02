# Box research queue

This queue tracks the sourced facts needed to build each product's box data. The release order and generator-capability roadmap remain in [issue #34](https://github.com/RyanGano/LegendaryPicker/issues/34); this file tracks research and points to its record.

For an integrated product, the `Data record` points to its runtime catalog JSON. New product research goes in its own `Docs/BoxResearch/<slug>.md` file; this queue holds only its status and path. A completed research record is not the same as an implemented box.

## What to collect

Research records are compact, card-by-card indexes that make the facts needed for implementation easy to find without replacing the printed cards.

- Product name, release order/date, base game or expansion, and ruleset. Link the official rules PDF and any official contents list or card reference used.
- Component totals and card-group counts, with exact display names and physical card identities. Cover Heroes (including shared Hero Name, team, classes, and terms), Villain Groups, Henchman Groups, Masterminds, Schemes, and any product-specific card types.
- For every distinct printed card face, record its title, type/group, verified copy count, Hero Name/team/classes where applicable, printed type and keyword icons, and visible numeric metadata such as cost, Recruit, Attack, or Victory Points. Link directly to a readable card image or other view of the printed face so the full ability remains available at its source.
- For every Mastermind, its Always Leads group and type, plus any setup effect. For every Scheme, player-count limits and every setup effect: counts, required groups or Heroes, constraints, moves, stacks or parts set aside, and setup steps. Record printed card effects only as short paraphrases.
- Shared stacks, tokens, and other parts the product supplies or its cards use; glossary teams, classes, and terms, with original summaries and rulebook page citations.
- For a base game, its player-count setup, starting deck, Solo rules, and any documented ruleset-mixing rules. An expansion normally contributes cards and components, not a second base-game setup table.
- Cite the source for every setup value and interpretation. Use official rulebooks, the product's rules insert, official clarifications, or directly attributed designer/publisher rulings for rules. C1/C2 structured metadata may support card identities, group membership, printed icons, teams/classes, and numeric values in the research index, preferably cross-linked to the corresponding card image. Their ability text is not reproduced or treated as authority for setup rules or gameplay interpretations. If a rule cannot be sourced, record the gap instead of inferring it.
- Do not reproduce card, rulebook, or flavor text, or create a comprehensive paraphrase of every gameplay ability. Link each card face for its complete wording; summarize only setup-relevant effects and new mechanics needed to identify required components or represent a legal setup, in original concise wording with a source.
- If a face, icon, copy count, or field cannot be verified, identify it and mark it unverified rather than filling it from inference. Treat opposite sides and distinct Tactics/Schemes as separate faces, while recording identical copies as a count.

Use one concise entry per distinct printed card face, for example:

`- **[Printed title]** — [type/group]; copies: [count]; Hero Name/team/classes: [verified values or Unverified]; printed values/icons: [verified values or Unverified]; source: [direct card-face image or official source].`

**Research is complete** only when the component list is reconciled against an official contents list, every distinct card face has its identity and available printed metadata/source link recorded, all applicable Scheme and Mastermind setup effects and required components are accounted for, and each gameplay claim has an allowed source and citation. Mark unresolved facts `Partial` or `Blocked`; do not guess. An unresolved product may remain `Partial` or `Blocked` while the single `Next` marker advances to the next release-order product; missing evidence must not stall later research.
When the final listed product has been processed, leave `Next` unset until another product is added to the roadmap.

## Product inventory

`Integrated` means a runtime box file exists, not that every product feature is complete. `Research complete` means the per-product research record meets the criteria above; implementation may still be pending. Paths for queued products are reserved destinations and are created when their research starts.

| Product (release order) | Product / ruleset | Status | Data record |
|---|---|---|---|
| Marvel Legendary First Edition core box (Nov 2012) | Base game · First Edition | Integrated; Research partial: C1 per-face values and Hero metadata indexed; missing copy counts/fields and sourced setup interpretations remain incomplete; [#11](https://github.com/RyanGano/LegendaryPicker/issues/11) remains open; [card-face research](./core.md) | `LegendaryPickerService/Data/Boxes/core.json` |
| Dark City (Jun 2013) | Expansion · First Edition | Integrated; Research partial: C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./dark-city.md) | `LegendaryPickerService/Data/Boxes/dark-city.json` |
| Fantastic Four (Oct 2013) | Expansion · First Edition | Integrated; Research partial: C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./fantastic-four.md) | `LegendaryPickerService/Data/Boxes/fantastic-four.json` |
| Paint the Town Red (Mar 2014) | Expansion · First Edition | Integrated; Research partial: C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./paint-the-town-red.md) | `LegendaryPickerService/Data/Boxes/paint-the-town-red.json` |
| Legendary: Villains (Jul 2014) | Base game · Villainous | Integrated; Research partial: C1 per-face metadata indexed; the official rulebook resolves the Cops/Backup question, but two Plot setup lists and remaining card/setup details still need allowed-source verification (#84); [card-face research](./villains.md) | `LegendaryPickerService/Data/Boxes/villains.json` |
| Guardians of the Galaxy (Oct 2014) | Expansion · First Edition | Partial: scans record 20 Hero faces, both Mastermind fronts, and all four Schemes; Hero face counts/team/class labels, eight Tactics, the Villain manifest, and the 30-Shard vs. 18-token discrepancy remain open · [#95](https://github.com/RyanGano/LegendaryPicker/issues/95); implementation depends on [#87](https://github.com/RyanGano/LegendaryPicker/issues/87) and [#88](https://github.com/RyanGano/LegendaryPicker/issues/88); C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./guardians-of-the-galaxy.md) | `Docs/BoxResearch/guardians-of-the-galaxy.md` |
| Fear Itself (Mar 2015) | Expansion · Villainous | Partial: official insert facts are recorded; Plot setups, Always Leads, and card-level Ally metadata still need card sources · [#96](https://github.com/RyanGano/LegendaryPicker/issues/96); implementation depends on #87 and #88; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./fear-itself.md) | `Docs/BoxResearch/fear-itself.md` |
| Secret Wars Volume 1 (Aug 2015) | Expansion · First Edition | Partial: insert categories total 348 vs. stated 350; card-front evidence for setups, Always Leads, and full roster is unavailable · roadmap [#34](https://github.com/RyanGano/LegendaryPicker/issues/34); C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./secret-wars-volume-1.md) | `Docs/BoxResearch/secret-wars-volume-1.md` |
| Secret Wars Volume 2 (Dec 2015) | Expansion · First Edition | Partial: official counts and mechanics recorded; Scheme setups, Always Leads, Hero metadata, and card roster need card sources · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./secret-wars-volume-2.md) | `Docs/BoxResearch/secret-wars-volume-2.md` |
| Captain America 75th Anniversary (Mar 2016) | Expansion · First Edition | Partial: card-level Scheme setups, Always Leads, and full physical card identities remain open · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./captain-america-75th-anniversary.md) | `Docs/BoxResearch/captain-america-75th-anniversary.md` |
| Civil War (Aug 2016) | Expansion · First Edition | Partial: complete Scheme/Mastermind setups and card metadata need card-level sources · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./civil-war.md) | `Docs/BoxResearch/civil-war.md` |
| Deadpool (Oct 2016) | Expansion · First Edition | Partial: card-level Scheme setups, Always Leads, and Hero metadata need verification · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./deadpool.md) | `Docs/BoxResearch/deadpool.md` |
| Noir (Feb 2017) | Expansion · First Edition | Partial: verify the four in-box Schemes against C1's fifth name, plus card-level setups and Always Leads · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./noir.md) | `Docs/BoxResearch/noir.md` |
| X-Men (Jun 2017) | Expansion · First Edition | Partial: verify Special Bystander identities/count, token names, and card-level setup/Always Leads · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./x-men.md) | `Docs/BoxResearch/x-men.md` |
| Spider-Man Homecoming (Oct 2017) | Expansion · First Edition | Partial: Scheme setup counts/parts and Always Leads are indexed from printed faces; player limits and unindexed card fields remain open · roadmap #34; per-card metadata indexed from C1; [card-face research](./spider-man-homecoming.md) | `Docs/BoxResearch/spider-man-homecoming.md` |
| Champions (Feb 2018) | Expansion · First Edition | Partial: card-level Scheme setups, Always Leads, Hero metadata, and Size-Changing icon requirements need verification · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./champions.md) | `Docs/BoxResearch/champions.md` |
| World War Hulk (Jun 2018) | Expansion · First Edition | Partial: card-level Scheme setups, Always Leads, Hero metadata, and exact Transformation Pile/card requirements need verification · roadmap #34; C1 face metadata indexed; image, copy-count, and setup gaps remain as documented; [card-face research](./world-war-hulk.md) | `Docs/BoxResearch/world-war-hulk.md` |
| Marvel Studios Phase 1 (2018) | Base game · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/marvel-studios-phase-1.md` |
| Ant-Man (Nov 2018) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/ant-man.md` |
| Venom (Mar 2019) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/venom.md` |
| Dimensions (May 2019; includes the 3D promo) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/dimensions.md` |
| Revelations (Aug 2019) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/revelations.md` |
| S.H.I.E.L.D. (Dec 2019) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/shield.md` |
| Heroes of Asgard (Mar 2020) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/heroes-of-asgard.md` |
| The New Mutants (Apr 2020) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/the-new-mutants.md` |
| Into the Cosmos (Aug 2020) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/into-the-cosmos.md` |
| Realm of Kings (Oct 2020) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/realm-of-kings.md` |
| Annihilation (Sep 2021) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/annihilation.md` |
| Messiah Complex (Jan 2022) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/messiah-complex.md` |
| Doctor Strange and the Shadows of Nightmare (Mar 2022) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/doctor-strange-and-the-shadows-of-nightmare.md` |
| Marvel Studios' Guardians of the Galaxy (Jun 2022) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/marvel-studios-guardians-of-the-galaxy.md` |
| Black Panther (2022/23) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/black-panther.md` |
| Black Widow (2022/23) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/black-widow.md` |
| Marvel Studios' The Infinity Saga (Mar 2023) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/marvel-studios-the-infinity-saga.md` |
| Midnight Sons (May 2023) | Expansion · First Edition | Queued · roadmap #34 | `Docs/BoxResearch/midnight-sons.md` |
| Marvel Studios' What If...? (late 2023) | Base game · Revised | Queued · roadmap #34; Revised rules are a separate ruleset | `Docs/BoxResearch/marvel-studios-what-if.md` |
| Marvel Studios' Ant-Man and the Wasp (Dec 2023/Jan 2024) | Expansion · Revised | Queued · roadmap #34 | `Docs/BoxResearch/marvel-studios-ant-man-and-the-wasp.md` |
| 2099 (Apr 2024) | Expansion · Revised | Queued · roadmap #34 | `Docs/BoxResearch/2099.md` |
| Weapon X (Oct 2024) | Expansion · Revised | Queued · roadmap #34 | `Docs/BoxResearch/weapon-x.md` |
| Legendary: Marvel Second Edition (Jun 2026) | Base game · Revised | Queued · roadmap #34; never substitute its rules for First Edition | `Docs/BoxResearch/second-edition.md` |

## Open collection and implementation work

- Core-box card names, Scheme setups, and Always Leads mappings still need verification against First Edition cards or clear scans (#11).
- Two Villainous Plots remain out of the catalog pending a supported setup effect and a sourced ruling about Cops (#84).
- The owner-directed stack-use and card-draw rules are being implemented in #87 and #88. They affect how collected product facts are represented; they do not authorize unsourced rules readings.
- Guardians of the Galaxy and Fear Itself have product issues (#95 and #96). Their dependency on #87/#88 is an implementation dependency; research can proceed in release order without fabricating missing rules.
- Later capability gates and product ordering remain in #34. Individual product issues are filed as each preceding wave lands.