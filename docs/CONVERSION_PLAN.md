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

## Phase 2B: Visual Direction Lock & Asset Replacement Matrix

**Goal:** Lock visual style guidelines and prevent low-quality or variety-destroying asset replacement.

### Tasks
- [x] Create Visual Direction document (`docs/VISUAL_DIRECTION.md`)
- [ ] Create detailed Asset Replacement Matrix in `docs/ASSET_IDENTITY_AUDIT.md`
- [x] Roll back mass placeholder replacements that collapsed enemy variety
- [ ] Establish per-asset review process with screenshots and Android readability checks

### Exit Criteria
- Visual direction document exists and is approved ✔️
- Asset replacement matrix exists and is updated
- No active gameplay blueprint is mass-replaced
- Enemy variety and silhouettes preserved
- Android screenshots reviewed for readability

---

## Phase 2.5: Region/Chapter Progression Contract

**Goal:** Lock the region and chapter progression model before core gameplay implementation.

### Tasks
- [x] Create Region/Chapter Progression document (`docs/REGION_CHAPTER_PROGRESSION.md`)
- [ ] Align on short mobile session lengths and rescue progress model
- [ ] Approve the progression contract before starting Phase 3 implementation

### Exit Criteria
- Region/Chapter Progression Contract is merged and accepted
- Phase 3 cannot begin without this contract's approval

---

## Phase 3: Convert Gameplay to Shelter Defense

**Goal:** Transform core loop from auto-attack survivor to top-down arena defense shooter (automatic targeting starter pistol, player movement, coins, and shelter upgrades).

### Tasks
- [x] Design shelter entity: health, position, upgrade system (PR #20 minimal shelter)
- [x] Modify enemy AI: enemies target shelter, not only player (PR #22 melee targeting)
- [x] Add shelter damage / game-over condition (PR #22 game over)
- [x] Convert weapon system to top-down auto-targeting arena shooter attacks (Pistol implemented in feature/basic-auto-pistol)
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
- **Phase 2.5 (Progression Contract):** In Progress. (Locking the region and chapter progression model).
- **Phase 3 (Gameplay Conversion):** Estimated duration: 3 weeks. (Focusing on shelter defense mechanics, upgrades, and progression).

### Immediate Next Step
- Region/Chapter Progression Contract tamamlandıktan sonra MCP restore + controlled asset replacement veya shelter conversion plan.
