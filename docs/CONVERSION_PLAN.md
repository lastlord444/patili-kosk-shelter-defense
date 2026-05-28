# CONVERSION_PLAN.md

## Overview

This document outlines the phased plan to convert the MIT-licensed VampireSurvivorsClone base into **Patili Köşk Shelter Defense**, an Android-first mobile arena/shelter defense game.

---

## Phase 1: Import & Build Verification

**Goal:** Confirm the imported project builds and runs in Unity.

### Tasks
- [ ] Open project in Unity (confirm compatible Unity version from ProjectSettings/)
- [ ] Resolve any missing package errors
- [ ] Build and run on Android device or emulator
- [ ] Document Unity version, render pipeline, and dependencies
- [ ] Verify all scenes load without errors
- [ ] Record baseline: what the game currently does

### Exit Criteria
- Project builds successfully for Android
- No critical compile errors
- Baseline gameplay documented

---

## Phase 2: Remove / Replace Vampire Survivors Identity and Risky Assets

**Goal:** Eliminate all Vampire Survivors-derived IP and audit/replace third-party assets.

### Tasks
- [ ] Complete LICENSE_AUDIT.md (identify all third-party assets)
- [ ] Replace all sprites/artwork with original or properly licensed art
- [ ] Replace all audio/music with original or CC0/MIT-licensed audio
- [ ] Replace/audit all fonts
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

## Timeline

> To be determined after Phase 1 build verification.
