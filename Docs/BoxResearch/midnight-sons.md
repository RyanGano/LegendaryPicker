# Midnight Sons (May 2023)

**Research status: Partial.** The official product page verifies a 100-card product and 1–5 player range; the insert documents several mechanics. Category counts, full setups, and card-level metadata remain unverified.

## Sources

| Key | Source | Facts supported |
|---|---|---|
| MS | [Upper Deck Midnight Sons rules insert](https://theupperdeckco.wpenginepowered.com/wp-content/uploads/2024/05/MidnightSons_Rulesheet.pdf) | Blood Frenzy, Hunt for Victims, Haunt, Moonlight/Sunlight, and pictured card examples (PDF pp.1–2). |
| UD | [Upper Deck Midnight Sons product listing](https://upperdeckstore.com/legendary-midnight-sons.html) | Product name, 100-card total, and 1–5 player range. |
| C1 | [master-strike structured midnight-sons card catalog](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/definitions/cards/midnightsons.ts) | Per-face titles, group/type, indexed printed numeric values/icons and direct image links; ability prose is omitted and is not rules evidence. |
| C1 metadata | [teams](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/teams.ts), [Hero classes](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/heroClasses.ts), [keywords](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/keywords.ts), and [card types](https://github.com/emfmesquita/master-strike/blob/master/packages/data/src/metadata/cardTypes.ts) | Labels for C1 team/class/keyword/type icon identifiers. |
| #34 / queue | [Release-order roadmap](https://github.com/RyanGano/LegendaryPicker/issues/34) and [`Docs/BoxResearch/README.md`](README.md) | Release-order position, expansion status, First Edition classification. |

## Catalog inventory

The official product listing gives **100 cards** and **1–5 players**; the insert does not break down the 100 by card type or group. C1 face metadata is indexed below with linked images where supplied; the 100-card composition remains unresolved:

- **Heroes (five):** Blade, Daywalker; Elsa Bloodstone; Morbius; Werewolf by Night; Wong, Master of the Mystic Arts.
- **Villain Groups (two):** Lilin; Fallen.
- **Masterminds (two):** Lilith, Mother of Demons; Zarathos.
- **Schemes (four):** Sire Vampires at the Blood Bank; Ritual Sacrifice to Summon Chthon; Midnight Massacre; Wager at Blackjack for Heroes' Souls.

C1 does not support the 100-card total, per-group card counts, mechanics, or setup values. The insert's pictured Morbius and Elsa Bloodstone examples are limited evidence, not full card-face sources; no allowed source recorded here verifies complete card identities, Hero Names, teams/classes, group inventories, or setup lines.

## Rules and mechanisms

- **Blood Frenzy (MS p.1):** On a Hero, gain +1 for each distinct Victory Point value among cards in the player's Victory Pile. On a Villain, it gains +1 during that player's turn for each such distinct value. Bystanders count; use a card's current value, and a card with no value counts as 0.
- **Hunt for Victims (MS p.1):** KO a Bystander captured by any Villain or Mastermind, or in the Escape Pile. If none is available, capture a Bystander instead. The insert notes Lilith and the Lilin can benefit from the number of Bystanders in the KO pile.
- **Haunt (MS p.1):** A Haunting Villain tucks beneath an unhaunted Hero in the HQ, preventing its recruitment while there. Exorcise it by spending Recruit equal to that Hero's cost; the Hero is KO'd or gained by a chosen player, and the Villain enters the city without using its Ambush. It cannot be fought while haunting, and exorcising is not a fight. Zarathos can haunt from Master Strikes or Tactics; a departing Haunted Hero is replaced by a new Haunted Hero in that HQ space.
- **Moonlight and Sunlight (MS p.2):** Moonlight is active when most HQ Heroes have odd printed costs; Sunlight when most have even costs. A tie activates neither. Cost-changing effects do not change the check; Haunted Heroes count, and a Divided Card counts once.
- **Card examples (MS p.2):** The pictured Midnight Sons cards show Sunlight gaining a Wound for a bonus and Moonlight removing a Wound from hand or discard pile for a bonus. Their full printed values and class icons need card-level verification.

## Required parts and glossary

- **Bystanders:** Hunt for Victims checks captured Bystanders and the Escape Pile, and the insert describes resulting use of the KO pile (MS p.1).
- **Wounds:** Pictured cards use Wounds from the shared supply and from hand/discard pile (MS p.2). The insert does not specify a product-specific quantity.
- **HQ and Victory Pile:** Haunt uses an HQ Hero; Blood Frenzy checks the player's Victory Pile (MS p.1).
- **Blood Frenzy:** Gain strength according to the distinct Victory Point values in the Victory Pile. (MS p.1)
- **Hunt for Victims:** KO an available captured Bystander or capture one when none can be KO'd. (MS p.1)
- **Haunt:** A Villain occupies an HQ Hero's space from beneath it and blocks recruitment until exorcised. (MS p.1)
- **Exorcise:** Spend Recruit matching the Haunted Hero's cost to remove it and move its Haunting Villain into the city. (MS p.1)
- **Moonlight / Sunlight:** HQ conditions based on whether most Heroes have odd / even printed costs. (MS p.2)

Summaries are original paraphrases under 40 words. The insert names no new token or separate product stack.

## Setup and implementation gaps

Verify the 100-card category and group totals against an official contents list or product cards; the official listing and insert reviewed here do not provide that breakdown. Verify each Scheme's player limits, Twist counts, required groups/Heroes, moves, stacks, and setup steps; both Masterminds' Always Leads and setup effects; and complete Hero metadata and card-driven component requirements. The rules insert is sufficient for the mechanics above, but does not establish those card-specific facts. This record changes no runtime data or code.

## Structured card-face metadata (C1)

This index uses C1 structured fields for printed identity, group, available values/icons, and the directly linked card face. `copies: Unverified` is deliberate when the catalog has no per-face quantity. Keyword labels are factual tags only; card ability prose is not reproduced, summarized, or used as rules authority. Missing C1 fields remain unverified rather than inferred.

### Hero group: Blade, Daywalker

- **Where Monsters Lurk**; type/group: Hero / Blade, Daywalker; copies: Unverified; Hero Name: Blade, Daywalker; team: Marvel Knights; class icons: Strength; printed values: Cost 4; Recruit 2+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BladeDaywalker_2Common.png).
- **Ride by Moonlight**; type/group: Hero / Blade, Daywalker; copies: Unverified; Hero Name: Blade, Daywalker; team: Marvel Knights; class icons: Tech; printed values: Cost 5; Attack 3+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BladeDaywalker_3Common.png).
- **Hunt High And Low**; type/group: Hero / Blade, Daywalker; copies: Unverified; Hero Name: Blade, Daywalker; team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Attack 2; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BladeDaywalker_4Uncommon.png).
- **Creature of Dawn and Dusk**; type/group: Hero / Blade, Daywalker; copies: Unverified; Hero Name: Blade, Daywalker; team: Marvel Knights; class icons: Strength; printed values: Cost 7; Attack 4+; keyword labels: Moonlight and Sunlight, Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/BladeDaywalker_1Rare.png).

### Hero group: Elsa Bloodstone

- **Axe of the Slayer**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: Marvel Knights; class icons: Instinct; printed values: Cost 3; Recruit 0+; Attack 0+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ElsaBloodstoneMK_2Common.png).
- **Silver Bullets**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: Marvel Knights; class icons: Tech; printed values: Cost 4; Attack 2+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ElsaBloodstoneMK_3Common.png).
- **Stalk the Night Stalkers**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: Marvel Knights; class icons: Tech; printed values: Cost 6; Attack 3; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ElsaBloodstoneMK_4Uncommon.png).
- **Vengeance of the Bloodstone Gem**; type/group: Hero / Elsa Bloodstone; copies: Unverified; Hero Name: Elsa Bloodstone; team: Marvel Knights; class icons: Instinct; printed values: Cost 8; Recruit 0+; Attack 4+; keyword labels: Patrol, Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/ElsaBloodstoneMK_1Rare.png).

### Hero group: Morbius

- **Mesmerize**; type/group: Hero / Morbius; copies: Unverified; Hero Name: Morbius; team: Marvel Knights; class icons: Covert; printed values: Cost 3; Recruit 2+; keyword labels: Moonlight and Sunlight, Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Morbius_2Common.png).
- **Insatiable Craving**; type/group: Hero / Morbius; copies: Unverified; Hero Name: Morbius; team: Marvel Knights; class icons: Covert; printed values: Cost 5; Attack 2+; keyword labels: Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Morbius_3Common.png).
- **Scalded by Sunlight**; type/group: Hero / Morbius; copies: Unverified; Hero Name: Morbius; team: Marvel Knights; class icons: Strength; printed values: Cost 4; Attack 2+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Morbius_4Uncommon.png).
- **It's Morbin' Time!**; type/group: Hero / Morbius; copies: Unverified; Hero Name: Morbius; team: Marvel Knights; class icons: Covert; printed values: Cost 7; Attack 3; keyword labels: Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/Morbius_1Rare.png).

### Hero group: Werewolf by Night

- **Starlit Path**; type/group: Hero / Werewolf by Night; copies: Unverified; Hero Name: Werewolf by Night; team: Marvel Knights; class icons: Instinct; printed values: Cost 2; Attack 1+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WerewolfByNight_2Common.png).
- **Snarling Fangs**; type/group: Hero / Werewolf by Night; copies: Unverified; Hero Name: Werewolf by Night; team: Marvel Knights; class icons: Strength; printed values: Cost 3; Attack 2; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WerewolfByNight_3Common.png).
- **Release the Beast**; type/group: Hero / Werewolf by Night; copies: Unverified; Hero Name: Werewolf by Night; team: Marvel Knights; class icons: Instinct; printed values: Cost 5; Recruit 0+; Attack 0+; keyword labels: Moonlight and Sunlight, Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WerewolfByNight_4Uncommon.png).
- **Track the Captives**; type/group: Hero / Werewolf by Night; copies: Unverified; Hero Name: Werewolf by Night; team: Marvel Knights; class icons: Instinct; printed values: Cost 7; Attack 5+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WerewolfByNight_1Rare.png).

### Hero group: Wong, Master of the Mystic Arts

- **Bridge Between Dimensions**; type/group: Hero / Wong, Master of the Mystic Arts; copies: Unverified; Hero Name: Wong, Master of the Mystic Arts; team: Marvel Knights; class icons: Ranged; printed values: Cost 2; Attack 1; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WongMasteroftheMysticArts_2Common.png).
- **Searing Shards of Sunlight**; type/group: Hero / Wong, Master of the Mystic Arts; copies: Unverified; Hero Name: Wong, Master of the Mystic Arts; team: Marvel Knights; class icons: Ranged; printed values: Cost 4; Attack 2+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WongMasteroftheMysticArts_3Common.png).
- **Seal the Rift**; type/group: Hero / Wong, Master of the Mystic Arts; copies: Unverified; Hero Name: Wong, Master of the Mystic Arts; team: Marvel Knights; class icons: Covert; printed values: Cost 5; Attack 0+; keyword labels: Patrol; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WongMasteroftheMysticArts_4Uncommon.png).
- **Face Your Demons**; type/group: Hero / Wong, Master of the Mystic Arts; copies: Unverified; Hero Name: Wong, Master of the Mystic Arts; team: Marvel Knights; class icons: Ranged; printed values: Cost 8; Attack 6+; keyword labels: Moonlight and Sunlight; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Heroes/WongMasteroftheMysticArts_1Rare.png).

### Villain Group: Lilin

- **Meatmarket**; type/group: Villain / Lilin; copies: 2; printed values: Attack 3+; VP 3; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/LilinMeatMarket.png).
- **Outcast**; type/group: Villain / Lilin; copies: 2; printed values: Attack 4; VP 2; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/LilinOutcast.png).
- **Sister Nil**; type/group: Villain / Lilin; copies: 1; printed values: Attack 5+; VP 4; keyword labels: Moonlight and Sunlight, Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/LilinSisterNil.png).
- **Skinner**; type/group: Villain / Lilin; copies: 2; printed values: Attack 5+; VP 4; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/LilinSkinner.png).
- **Blackout**; type/group: Villain / Lilin; copies: 1; printed values: Attack 7+; VP 5; keyword labels: Moonlight and Sunlight, Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/LilinBlackout.png).

### Villain Group: Fallen

- **Metarchus**; type/group: Villain / Fallen; copies: 2; printed values: Attack 3+; VP 4; keyword labels: Blood Frenzy, Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/FallenMetarchus.png).
- **Atrocity**; type/group: Villain / Fallen; copies: 2; printed values: Attack 3; VP 2; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/FallenAtrocity.png).
- **Patriarch**; type/group: Villain / Fallen; copies: 3; printed values: Attack 4; VP 3; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/FallenPatriarch.png).
- **Salomé, Sorceress Supreme**; type/group: Villain / Fallen; copies: 1; printed values: Attack 6+; VP 5; keyword labels: Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Villains/FallenSalomeSorceressSupreme.png).

### Mastermind: Lilith, Mother of Demons

- **Lilith, Mother of Demons**; type/group: Normal Mastermind face / Lilith, Mother of Demons; copies: Unverified; printed values: Attack 8+; VP 6; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/LilithMotherofDemons.png).
- **Epic Lilith**; type/group: Epic Mastermind face / Lilith, Mother of Demons; copies: Unverified; printed values: Attack 10+; VP 6; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/LilithMotherofDemons_Epic.png).
- **Connoisseur of Souls**; type/group: Mastermind Tactic / Lilith, Mother of Demons; copies: Unverified; printed values: not indexed in C1; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/LilithMotherofDemonsTactic1.png).
- **Offer of Corruption**; type/group: Mastermind Tactic / Lilith, Mother of Demons; copies: Unverified; printed values: not indexed in C1; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/LilithMotherofDemonsTactic3.png).
- **Mesopotamian Demon Goddess**; type/group: Mastermind Tactic / Lilith, Mother of Demons; copies: Unverified; printed values: not indexed in C1; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/LilithMotherofDemonsTactic2.png).
- **Respawn Demonic Offspring**; type/group: Mastermind Tactic / Lilith, Mother of Demons; copies: Unverified; printed values: not indexed in C1; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/LilithMotherofDemonsTactic4.png).

### Mastermind: Zarathos

- **Zarathos**; type/group: Normal Mastermind face / Zarathos; copies: Unverified; printed values: Attack 7; VP 6; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Zarathos.png).
- **Epic Zarathos**; type/group: Epic Mastermind face / Zarathos; copies: Unverified; printed values: Attack 9; VP 6; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/Zarathos_Epic.png).
- **Eruption of Hellfire**; type/group: Mastermind Tactic / Zarathos; copies: Unverified; printed values: not indexed in C1; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZarathosTactic3.png).
- **Corrupted Spirit of Vengeance**; type/group: Mastermind Tactic / Zarathos; copies: Unverified; printed values: not indexed in C1; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZarathosTactic2.png).
- **Imprison in the Soul Crystal**; type/group: Mastermind Tactic / Zarathos; copies: Unverified; printed values: not indexed in C1; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZarathosTactic4.png).
- **Demonic Essence of Ghost Rider**; type/group: Mastermind Tactic / Zarathos; copies: Unverified; printed values: not indexed in C1; keyword labels: Haunt; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Masterminds/ZarathosTactic1.png).

### Scheme: Sire Vampires at the Blood Bank

- **Sire Vampires at the Blood Bank**; type/group: Scheme / Sire Vampires at the Blood Bank; copies: Unverified; printed values: not indexed in C1; keyword labels: Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/SireVampiresattheBloodBank.png).

### Scheme: Ritual Sacrifice to Summon Chthon

- **Ritual Sacrifice to Summon Chthon**; type/group: Scheme / Ritual Sacrifice to Summon Chthon; copies: Unverified; printed values: not indexed in C1; keyword labels: Hunt for Victims; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/RitualSacrificetoSummonChthon.png).
- **Great Old One Chthon**; type/group: Location (transformed face) / Ritual Sacrifice to Summon Chthon; copies: Unverified; printed values: Attack 27; VP 13; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/GreatOldOneChthon.png).

### Scheme: Midnight Massacre

- **Midnight Massacre**; type/group: Scheme / Midnight Massacre; copies: Unverified; printed values: not indexed in C1; keyword labels: Moonlight and Sunlight, Blood Frenzy; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/MidnightMassacre.png).

### Scheme: Wager at Blackjack for Heroes' Souls

- **Wager at Blackjack for Heroes' Souls**; type/group: Scheme / Wager at Blackjack for Heroes' Souls; copies: Unverified; printed values: not indexed in C1; [card image](https://nyc3.digitaloceanspaces.com/bageltop/CardImages/Schemes/WageratBlackjackforHeroesSouls.png).

### Setup interpretation boundary

Use official rulebooks/inserts, official clarifications, or the directly linked printed Scheme/Mastermind face for setup effects. C1 ability prose is not a rules source. Any setup value not already supported by such a source remains unverified; runtime data and its citations are linked above rather than duplicated.
