# CONVERSION_PLAN.md

## Overview

This document outlines the phased plan to convert the MIT-licensed VampireSurvivorsClone base into **Patili Köşk Shelter Defense**, an Android-first mobile arena/shelter defense game.

---

## Phase 1: Import & Build Verification

**Goal:** Confirm the imported project builds and runs in Unity.

### Tasks
- [x] Open project in Unity (confirm compatible Unity version from ProjectSettings/ - Original: 2021.3.21f1)
- [x] Resolve any missing package errors (Successfully upgraded to Unity 6 [6000.3.16f1] LTS)
- [x] Build and run on Android device or emulator (Android Build verified: Succeeded with 0 Errors)
- [x] Document Unity version, render pipeline, and dependencies (Unity 6, Built-in Render Pipeline, official UGUI/InputSystem)
- [x] Verify all scenes load without errors (Level 1 and Main Menu scenes load successfully)
- [x] Record baseline: what the game currently does (Play Mode verified: character movement, enemy spawning, and object pooling run with 0 runtime exceptions)

### Exit Criteria
- Project builds successfully for Android ✔️
- No critical compile errors ✔️
- Baseline gameplay documented ✔️

---

## Phase 2: Remove / Replace Vampire Survivors Identity and Risky Assets

**Goal:** Eliminate all Vampire Survivors-derived IP and audit/replace third-party assets.

### Tasks
- [ ] Complete LICENSE_AUDIT.md (identify all third-party assets)
- [ ] Replace all sprites/artwork with original or properly licensed art (Cat shelter theme)
- [ ] Replace all audio/music with original or CC0/MIT-licensed audio
- [ ] Replace/audit all fonts (Confirmed Noto Sans and LiberationSans are OFL)
- [ ] Remove Vampire Survivors name, branding, UI references
- [ ] Replace character designs with cat/animal shelter theme
- [ ] Update project name in Unity (PlayerSettings)
- [ ] Update all in-game text to Patili Köşk branding

### Exit Criteria
- No Vampire Survivors-derived visual or audio assets remain
- All third-party assets documented with confirmed licenses
- Project still builds after asset replacement

---

## Phase 3: Convert Gameplay to Shelter Defense

**Goal:** Transform core loop from auto-attack survivor to player-controlled shelter defense.

### Tasks
- [ ] Design shelter entity: health, position, upgrade system
- [ ] Modify enemy AI: enemies target shelter, not only player
- [ ] Add shelter damage / game-over condition
- [ ] Convert weapon system to player-controlled attacks
- [ ] Implement coin collection and shelter upgrade UI
- [ ] Design wave progression for shelter defense context
- [ ] Playtest core loop: move → protect → defeat waves → collect coins → upgrade shelter
- [ ] Polish for Android: touch controls, screen scaling, performance

### Exit Criteria
- Core loop playable: player protects shelter from waves
- Shelter upgrade system functional
- Android build passes basic playtest

---

## Timeline & Next Steps

### Timeline
- **Phase 1 (Verification):** Completed on 2026-05-28. ✔️
- **Phase 2 (Asset Replacement):** Estimated duration: 2 weeks. (Focusing on removing Vampire Survivors identity and establishing cat shelter visual assets).
- **Phase 3 (Gameplay Conversion):** Estimated duration: 3 weeks. (Focusing on shelter defense mechanics, upgrades, and progression).

### Immediate Next Step
- Initiate Phase 2: Design and import placeholder cat shelter assets, and begin replacing character/monster sprites.
