# VISUAL_DIRECTION.md

> Last updated: 2026-05-30
> Status: APPROVED (Phase 2A Gate)

## Product Visual Direction

**Patili Köşk Shelter Defense** should move toward a cute, readable, animal/shelter-themed mobile defense look inspired by the user-provided reference video.
The core aesthetic principles are defined below:

- **Reference Only:** The reference video/game serves solely as a direction/feel reference and inspiration for the quality, color harmony, and readability. It is NOT an asset source.
- **No Direct Copying:** Direct copying of art, UI, animation, or game-specific identity from the reference video is strictly forbidden to prevent IP and license risks.
- **Preserve Visual Variety:** The game has multiple enemy types with distinct visual identities (Melee, Ranged, Boomerang, Throwing, Boss, etc.). This visual differentiation is crucial for gameplay. We must not collapse multiple enemy types into a single generic placeholder asset.
- **Android Readability:** The game is mobile-first. Assets must have clear silhouettes, large and readable UI, distinct pickable items (coins, exp gems), and easily recognizable enemies on small mobile screens.
- **Curated Replacement:** Placeholder or final asset replacements must be curated, reviewed, and easily reversible.

---

## Asset Replacement Protocol

To prevent low-quality or over-aggressive asset replacements, all future asset replacements must be documented in a matrix and verify the following fields:

1. **Asset Path:** The target path inside `Assets/` (e.g., `Assets/Blueprints/Coin/Coin.asset`).
2. **Current Source / License:** License status of the current asset (e.g., VS IP, Base game, unclear).
3. **Replacement Source / License:** The source of the new asset and its exact license (e.g., Kenney CC0, Custom Art).
4. **Screenshot Before/After:** Visual evidence of the change inside the Unity Editor or device.
5. **Unity Console Evidence:** Verification that the replacement does not trigger compilation or runtime exceptions.
6. **Android Readability Check:** Verification that the replacement remains readable on target mobile screen sizes.

---

## Current Decision

- **Kenney Placeholder Rollback:** The initial mass replacement of character, enemy, coin, and exp gem blueprint references with a single generic Kenney placeholder sprite has been rejected. It collapsed enemy variety and reduced visual quality.
- **Restoration of Original Assets:** All blueprint asset references have been restored to their original/base state.
- **No Active Replacements:** The project will use base assets as the baseline for testing until a proper, detailed asset replacement matrix and visual direction assets are prepared and approved.
