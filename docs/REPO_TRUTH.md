# REPO_TRUTH.md

| Alan | Değer |
|---|---|
| **Product** | Patili Köşk Shelter Defense |
| **Base** | MIT-licensed VampireSurvivorsClone by matthiasbroske |
| **Base repo** | https://github.com/matthiasbroske/VampireSurvivorsClone |
| **Base license** | MIT (preserved in LICENSE file) |
| **Current state** | Controlled conversion from VS base to shooter shelter defense |
| **Target genre** | Android-first top-down mobile shooter / shelter defense / horde defense |
| **Platform** | Android (mobile-first) |
| **Engine** | Unity |

## Core Conversion Goal

- Player acts as a guardian protecting a central cat shelter (`Shelter`) from wave spawns.
- Guardian moves, automated weapons aim at the nearest valid enemy (`Monster`), defeats them, collects coins, and upgrades the shelter, walls, and weapons.
- Game feel focuses on automatic-targeting top-down mobile shooter mechanics instead of melee/swipe feel.

## Product Decisions & Guidelines

1. **Top-Down Shooter Combat:**
   - Default combat feel is automated shooter (Basic Auto Pistol starter weapon).
   - Melee weapons and swipe actions are sidelined from the initial onboarding experience.
   - Upgrade options must feel cohesive with a shooter loop rather than a fantasy survival pool.

2. **Onboarding & Early Level Curve:**
   - Real-time weeks are excluded.
   - Region/chapter structure consists of 5–7 short game missions/levels.
   - Level 1 starts with a 30–60 second low-intensity onboarding curve (fewer enemies, manageable HP, so player learns shooting + coin collection + upgrades).

3. **No Fake Ad Gameplay:**
   - Core gameplay is real: player protects the shelter in a mobile shooter loop. There is no fake ad gameplay (no pull-the-pin, no match-3).

## What This Game Is NOT

- NOT a Vampire Survivors clone (sidelined)
- NOT a pure tower placement/tower defense game
- NOT pull-the-pin / puzzle
- NOT match-3
- NOT a direct copy of Survivor.io

## Development & Testing Rules

- **Play Mode Testing:** Load `Main Menu.unity` first, enter Play Mode, start the level, select select/upgrade options. Avoid running `Level 1.unity` standalone to prevent managers initialization failures.
- **Android Builds:** Build verifying checks are compiled locally to produce `Build/android_smoke.apk`.
