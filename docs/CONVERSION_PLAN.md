# CONVERSION_PLAN.md

## Overview

This document outlines the phased plan to convert the MIT-licensed VampireSurvivorsClone base into **Patili Köşk Shelter Defense**, an Android-first mobile arena/shelter defense shooter.

---

## Phase 1: Import & Build Verification (Completed)
Confirmed imported project builds and runs on Android and Editor.

---

## Phase 2: Remove / Replace Vampire Survivors Identity and Risky Assets (In Progress)
Eliminate VS-derived IP and replace artwork/audio with properly licensed CC0/custom assets.

---

## Phase 3: Convert Gameplay to Shelter Defense & Shooter Pivot

**Goal:** Transform the core loop from a melee auto-attack survivor clone to a top-down mobile defense shooter centered around protecting the cat shelter.

### Roadmap Steps

1. **Shelter Readability & HP Bar (Completed - PR #24)**
   - Visual separation of Shelter (Hut shape) from character.
   - World-space compact HP bar with color transitions.
   - Melee monsters target and damage the shelter.

2. **Basic Auto Pistol Starter Feel (Current)**
   - Wire the Pistol as the default starting weapon.
   - Implement automatic target aiming and Y-scale flip checks.
   - Ensure the starting combat feel is immediately recognizable as a top-down shooter.

3. **Early Game Enemy Curve & Level 1 Pacing (Upcoming)**
   - Tune the first 60 seconds of Level 1.
   - Reduce initial wave density (40-60% reduction) and scale HP/movespeed down to provide an onboarding buffer.

4. **Weapon-Specific Upgrade Pool Hygiene (Upcoming)**
   - Clean up Level 1 ability selection pool.
   - Ensure starter pistol players receive relevant weapon upgrades (damage, cooldown, count) rather than mismatching magic/fantasy stats.

5. **Shelter & Weapon Coin Upgrades (Upcoming)**
   - Implement a simple coin economy to purchase in-run upgrades for the pistol and shelter.

6. **Fixed Shelter Support Turrets (Upcoming)**
   - Add a basic auto-targeting shelter defense point/turret.

7. **Region & Chapter Mission Contract (Upcoming)**
   - Define a chapter system consisting of 5–7 short missions per region.

---

## Out-of-Scope (Excluded from MVP)

- Ads, In-App Purchases (IAP), cloud save, leaderboards, online multiplayer, battlepasses.
- Full shelter simulation (e.g. city-building elements, full animal management).
- Complex AI state machines.
- Adding major external packages (e.g. DOTween, uPools, Asset Store kits).
- Fake ad gameplay mechanics (e.g. puzzle pull-the-pin, match-3, or direct tower placement).
