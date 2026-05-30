# ASSET_IDENTITY_AUDIT.md

> Last updated: 2026-05-30

This document tracks the audit and replacement process of third-party assets to remove Vampire Survivors identity and other unlicensed assets.

## Status Summary

- **Vampire Survivors Baseline Assets:** Active but marked for replacement once curated assets are ready.
- **Kenney CC0 Placeholder Trial:** **REJECTED / DEFERRED**. The automated/mass replacement of character and enemy sprites with Kenney CC0 placeholders reduced visual quality and collapsed the distinct identities of multiple enemy types into a single sprite.
- **Current Action:** Reverted to original/base asset references. Future replacements will follow the curated, per-asset mapping protocol.

---

## Asset Mapping & Audit Table

| Asset Path | Original Reference (Base) | Kenney CC0 Trial Reference | Audit Status | Reversion Status |
|---|---|---|---|---|
| `Assets/Blueprints/Characters/Main Character Blueprint.asset` | Main Character Sprite | cat_guardian_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Characters/Test Character Blueprint 1.asset` | Test Character Sprite 1 | cat_guardian_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Characters/Test Character Blueprint 2.asset` | Test Character Sprite 2 | cat_guardian_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Coin/Coin.asset` | Base Coin Sprite | food_coin_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Exp Gem/Exp Gem.asset` | Base Exp Gem Sprite | exp_gem_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Boomerang/Boomerang Monster.asset` | Boomerang Monster Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Boss/Final Boss.asset` | Final Boss Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Boss/Mini Boss.asset` | Mini Boss Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Melee/中級小兵.asset` | Melee Medium Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Melee/初級小兵.asset` | Melee Easy Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Melee/高級小兵.asset` | Melee Hard Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Ranged/射擊小兵.asset` | Ranged Monster Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |
| `Assets/Blueprints/Monsters/Throwing/Gravity Monster.asset` | Throwing Monster Sprite | shadow_enemy_placeholder.png | Rejected | Restored to Base |

---

## Future Guidelines for Asset Sourcing

1. **Kenney CC0 Assets:** May still remain as a candidate source for UI/icons and simple elements, but must not be used to collapse visual variety.
2. **Quality & Silhouette:** Any replacement sprite must respect the original silhouette and keep visual difference high between different monsters.
3. **Approval:** All changes must comply with the `docs/VISUAL_DIRECTION.md` protocol.
