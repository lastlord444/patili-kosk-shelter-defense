# ASSET_IDENTITY_AUDIT.md

> Last updated: 2026-05-30
> Status: COMPLETED (Phase 2A Audit)

This document tracks the audit and replacement process of third-party assets to remove Vampire Survivors identity and other unlicensed/risky assets.

---

## Status Summary

- **Vampire Survivors Baseline Assets:** Active but marked for replacement once curated assets are ready.
- **Kenney CC0 Placeholder Trial:** **REJECTED / DEFERRED**. The automated/mass replacement of character and enemy sprites with Kenney CC0 placeholders reduced visual quality and collapsed the distinct identities of multiple enemy types into a single sprite.
- **Current Action:** Reverted to original/base asset references. Future replacements will follow the curated, per-asset mapping protocol.
- **Detailed Asset Matrix:** A complete asset-by-asset analysis containing 28+ files, replacement directions, candidates, and visibility checks is located in **[ASSET_REPLACEMENT_MATRIX.md](ASSET_REPLACEMENT_MATRIX.md)**.

---

## Key Identity Risks Audited

1. **Vampire Survivors Branding/Aesthetics:**
   - Playable characters (`MainCharacterBlue.png` etc.) and weapon styles directly mirror VS styling. These are store-blocking risks if left unchanged.
   - Specific items like `VampireTeeth.png` are direct identity references.

2. **Copyrighted IP Reference:**
   - `Lightsaber.png` poses a copyright risk (Star Wars). This is a P0 must-replace asset.

3. **Enemy Silhouette Collapsing:**
   - Mass placeholder replacements (using a single dummy sprite for all monsters) destroyed tactical readability. We must preserve individual silhouettes for Melee, Ranged, Boomerang, Throwing, and Boss enemies.

---

## Future Guidelines for Asset Sourcing

1. **Aesthetics & Theme:** Cute, readable, cat/animal shelter-themed mobile defense look (e.g., Cat Guardians, Food Coins, Yarn Balls/Feather toys).
2. **Kenney CC0 Assets:** May still remain as a candidate source for generic UI/icons and simple elements, but must not be used to collapse visual variety.
3. **Curated Protocol:** Every asset replacement must document:
   - Asset path and current source.
   - Replacement source and exact license.
   - Screenshot before/after in Unity Editor.
   - Verification that it doesn't trigger compilation/runtime console errors.
   - Android readability check.
