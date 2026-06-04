# Patili Köşk Shelter Defense

> **⚠️ Base Repository Notice & Licensing**
> - This project is based on the **MIT-licensed** [VampireSurvivorsClone](https://github.com/matthiasbroske/VampireSurvivorsClone) by **matthiasbroske**.
> - Original LICENSE and credits are **preserved** (see [LICENSE](LICENSE)).
> - This project is a controlled conversion of the base. It is **not** a tower defense, **not** pull-the-pin, **not** match-3, and **not** a direct Survivor.io clone.

---

## Project Positioning

**Patili Köşk Shelter Defense** is an Android-first top-down mobile arena defense shooter. 

Instead of melee swipe mechanics, the game pivots to a top-down shooter feel where the player acts as a guardian protecting a central Cat Shelter from incoming waves of threats. The game features an emotional rescue/shelter hook integrated with a satisfying core combat and upgrade loop.

## Core Gameplay Loop

1. **Move:** The player moves the guardian character around the central shelter using virtual controls.
2. **Auto-Aim & Shoot:** The player's weapons (starting with the Basic Auto Pistol) automatically lock onto and fire at the nearest valid enemy (`Monster`).
3. **Protect:** Düşmanlar hem oyuncuyu hem de merkezi barınağı (`Shelter`) hedef alır. Can sıfırlanmadan barınak korunmalıdır.
4. **Collect Coins:** Slain monsters drop experience gems and coins.
5. **Upgrade:** Coins are spent to buy permanent or in-run upgrades for weapons, shelter health, and defenses.

## Current Known Issues (MVP Prototype)

- **Pistol Visual Verification Pending:** The procedural visual representation of the pistol may need further refinement/verification in diverse game conditions.
- **Aggressive Early Enemy Pacing:** The first wave's intensity can be too high for a starter weapon without onboarding buffer space.
- **Upgrade Pool Hygiene:** The level-up choices currently include original fantasy/magic weapon stats from the base project that do not align with a shooter-themed game loop.
- **Visual Quality:** Visual assets are currently prototype/programmer-art grade and will be replaced with themed CC0/custom cute animal rescue assets.

---

## Development & Play Testing Rules

- **Play Mode Testing:** DO NOT run `Level 1.unity` (or any gameplay scene) standalone. Game managers, data, and singletons are initialized in `Main Menu.unity`. Always load the `Main Menu` scene first, start Play Mode, click "Start" (or select select options) to enter the game correctly and prevent `NullReferenceException` loops.
- **Android Builds:** Build verifying checks are compiled locally to produce `Build/android_smoke.apk`.
