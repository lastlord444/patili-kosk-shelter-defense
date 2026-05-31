# ASSET_REPLACEMENT_MATRIX.md

> Last updated: 2026-05-30
> Status: ACTIVE (Phase 2B Visual Lock)

This matrix defines the audit status, risk type, replacement direction, and priority for all risky and temporary visual assets in **Patili Köşk Shelter Defense**.

> [!WARNING]
> **Kenney Generic Items** ve **Kenney UI Pack** henüz yerel ortama indirilmemiştir.
> Bu pakette yer alan dosyaların kesin dosya yolları (`PNG/...` vb.) ve dosya adları doğrulanmamıştır, hepsi tahmini/taslak niteliğindedir.
> Asset paketleri indirilip çıkarılana kadar bu kolonlar "pending pack extraction" (paket çıkarılması bekleniyor) ve "not imported" (içe aktarılmadı) olarak kalacaktır. Tahmini dosya yolu yazmak kesinlikle yasaktır.

---

## Asset Replacement Matrix

| Current Asset Path | Current Usage / Referenced By | Risk Type | Current License Status | Replacement Need | Replacement Direction | Candidate Source Type | Evidence Needed Before Replacement | Android Readability Concern | Priority | Notes |
|---|---|---|---|---|---|---|---|---|---|---|
| `Assets/Sprites/Characters/MainCharacterBlue.png` | `Main Character Blueprint.asset`, `Character Card.prefab` | Identity (Vampire Survivors style) | MIT / Unknown | **Must Replace** | Cat Guardian (Hero) | Original Art / Paid Asset Store | 4-frame Walk Sprite, Play Mode test, build check | High silhouette clarity | **P0** | Main playable character (blue). Store-blocking identity. |
| `Assets/Sprites/Characters/MainCharacterGray.png` | `Test Character Blueprint 1.asset` | Identity (Vampire Survivors style) | MIT / Unknown | **Must Replace** | Cat Guardian (Hero variant) | Original Art / Paid Asset Store | 4-frame Walk Sprite, Play Mode test, build check | High silhouette clarity | **P1** | Secondary playable character. Store-blocking identity. |
| `Assets/Sprites/Characters/MainCharacterPurple.png` | `Test Character Blueprint 2.asset`, `Level 1.unity` | Identity (Vampire Survivors style) | MIT / Unknown | **Must Replace** | Cat Guardian (Hero variant) | Original Art / Paid Asset Store | 4-frame Walk Sprite, Play Mode test, build check | High silhouette clarity | **P1** | Secondary playable character. Store-blocking identity. |
| `Assets/Sprites/Characters/MainCharacterWhite.png` | Unused | Identity | MIT / Unknown | **Should Replace** / Delete | Cat Guardian (Hero variant) | Delete / Keep | Unity build verification | - | **P2** | Currently unused sprite. Safe to delete or replace later. |
| `Assets/Sprites/Monsters/CrabOrange.png` | `初級小兵.asset` (Melee Easy), `Default Melee Monster.prefab` | Identity | MIT / Unknown | **Must Replace** | Enemy (Easy Melee Mouse/Rat) | Original Art / CC0 / Paid Asset Store | Walk Sprite, Play Mode test, prefab wiring | High visibility on ground | **P0** | Primary wave enemy. Store-blocking identity. |
| `Assets/Sprites/Monsters/Alien.png` | `中級小兵.asset` (Melee Medium) | Identity | MIT / Unknown | **Must Replace** | Enemy (Medium Melee Stray Dog) | Original Art / CC0 / Paid Asset Store | Walk Sprite, Play Mode test | High visibility on ground | **P0** | Mid-tier wave enemy. Store-blocking identity. |
| `Assets/Sprites/Monsters/PunchMonster.png` | `高級小兵.asset` (Melee Hard) | Identity | MIT / Unknown | **Must Replace** | Enemy (Hard Melee Bully Animal) | Original Art / CC0 / Paid Asset Store | Walk Sprite, Play Mode test | High visibility on ground | **P1** | Heavy wave enemy. Store-blocking identity. |
| `Assets/Sprites/Monsters/GhostFairy.png` | `射擊小兵.asset` (Ranged) | Identity | MIT / Unknown | **Must Replace** | Enemy (Ranged Catcher/Net thrower) | Original Art / CC0 / Paid Asset Store | Projectile firing sync check | Projectile must be highly visible | **P0** | Ranged projectile enemy. Store-blocking identity. |
| `Assets/Sprites/Monsters/NailHead.png` | `Boomerang Monster.asset` | Identity | MIT / Unknown | **Must Replace** | Enemy (Medium Ranged) | Original Art / CC0 / Paid Asset Store | Attack sprite sync check | Boomerang trajectory readability | **P1** | Boomerang thrower enemy. Store-blocking identity. |
| `Assets/Sprites/Monsters/WizardMonster.png` | `Gravity Monster.asset` | Identity | MIT / Unknown | **Must Replace** | Enemy (Throwing/AOE) | Original Art / CC0 / Paid Asset Store | AOE circle visibility check | Heavy ground indicator contrast | **P1** | Gravity area enemy. Store-blocking identity. |
| `Assets/Sprites/Monsters/MiniBoss.png` | `Mini Boss.asset` | Identity | MIT / Unknown | **Must Replace** | Enemy Boss (Mini) | Original Art / Paid Asset Store | Boss scale test, HUD icon check | Big size silhouette clarity | **P0** | Elite miniboss. Store-blocking identity. |
| `Assets/Sprites/Monsters/Boss.png` | `Final Boss.asset`, `Default Boss Monster.prefab` | Identity | MIT / Unknown | **Must Replace** | Enemy Boss (Final) | Original Art / Paid Asset Store | Boss scale test, HUD icon check | Big size silhouette clarity | **P0** | Level boss. Store-blocking identity. |
| `Assets/Sprites/Coins/Coin1.png` | `Coin.asset` | Identity (generic RPG) | MIT / Unknown | **Must Replace** | Food Coin (Wet Food) | Source pack: Kenney Generic Items, Candidate type: food/can/box/fish/yarn-like collectible candidate, Exact source file: pending pack extraction, Import status: not imported | Pickup scale, collision check | Large enough to see on ground | **P0** | Collectible currency. |
| `Assets/Sprites/Coins/Coin2.png` | `Coin.asset` | Identity (generic RPG) | MIT / Unknown | **Must Replace** | Food Coin (Dry Food Box) | Source pack: Kenney Generic Items, Candidate type: food/can/box/fish/yarn-like collectible candidate, Exact source file: pending pack extraction, Import status: not imported | Pickup scale, collision check | Large enough to see on ground | **P0** | Collectible currency. |
| `Assets/Sprites/Coins/Coin5.png` | `Coin.asset` | Identity (generic RPG) | MIT / Unknown | **Must Replace** | Food Coin (Premium Treat) | Source pack: Kenney Generic Items, Candidate type: food/can/box/fish/yarn-like collectible candidate, Exact source file: pending pack extraction, Import status: not imported | Pickup scale, collision check | Large enough to see on ground | **P0** | Collectible currency. |
| `Assets/Sprites/Coins/Coin30.png` | `Coin.asset` | Identity (generic RPG) | MIT / Unknown | **Must Replace** | Food Coin (Salmon Can) | Source pack: Kenney Generic Items, Candidate type: food/can/box/fish/yarn-like collectible candidate, Exact source file: pending pack extraction, Import status: not imported | Pickup scale, collision check | Large enough to see on ground | **P1** | Collectible currency (large). |
| `Assets/Sprites/Coins/Coin50.png` | `Coin.asset` | Identity (generic RPG) | MIT / Unknown | **Must Replace** | Food Coin (Golden Fish Bone) | Source pack: Kenney Generic Items, Candidate type: food/can/box/fish/yarn-like collectible candidate, Exact source file: pending pack extraction, Import status: not imported | Pickup scale, collision check | Large enough to see on ground | **P1** | Collectible currency (very large). |
| `Assets/Sprites/Gems/Gems.png` | `Exp Gem.asset` | Identity (RPG gems) | MIT / Unknown | **Must Replace** | Exp Toy (Yarn Ball / Feather) | Source pack: Kenney Generic Items, Candidate type: food/can/box/fish/yarn-like collectible candidate, Exact source file: pending pack extraction, Import status: not imported | Magnet pull test, particle test | Color coding for XP value | **P0** | Experience items. |
| `Assets/Sprites/Kenney/Magnet.png` | `Magnet.prefab`, `Level 1.unity` | Safe | CC0 (Kenney.nl) | **Can Keep** | Item Magnet | Keep | - | Ground readability | **P2** | Safe licensed asset. |
| `Assets/Sprites/Kenney/Skull.png` | `Game Over Dialog.prefab`, `Level 1.unity`, `Main Menu.unity` | Safe | CC0 (Kenney.nl) | **Can Keep** | UI Death Icon / Decor | Keep | - | - | **P2** | Safe licensed asset. |
| `Assets/Sprites/Kenney/VampireTeeth.png` | `Lifesteal Ability.prefab` | Identity (Vampire) | CC0 (Kenney.nl) | **Should Replace** | Cat Scratch / Bandage Icon | CC0 / Original Art | HUD and ability slot check | - | **P1** | High identity risk (vampire references). |
| `Assets/Sprites/Kenney/WizardTilemap.png` | Chest Assets | Safe | CC0 (Kenney.nl) | **Can Keep** | Chest sprite | CC0 / Original Art | Chest open animation check | Distinct from ground | **P2** | Chest tiles are safe but replacement could improve quality. |
| `Assets/Sprites/Kenney/PlatformerTilemap.png` | Upgrade/Icon sprites | Safe | CC0 (Kenney.nl) | **Can Keep** / Replace | Ability HUD Icons | CC0 / Original Art | HUD slot resolution | High clarity on small UI | **P2** | Icons are generic CC0, can keep but custom is better. |
| `Assets/Sprites/UI/UICoin.png` | `Level 1.unity`, `Main Menu.unity` | Identity (Gold coin) | MIT / Unknown | **Must Replace** | UI Currency (Dry Food) | Source pack: Kenney UI Pack, Candidate type: coin/health/shield/upgrade UI icon candidate, Exact source file: pending pack extraction, Import status: not imported | HUD scaling check | - | **P0** | Must match the in-game pickup sprite. |
| `Assets/Sprites/Weapons/Bat.png` | `Bat Ability.prefab`, `Main Menu.unity` | Safe / Identity | MIT / Unknown | **Should Replace** | Wooden Plank / Toy Bat | CC0 / Original Art | Weapon attack VFX check | Visibility during swing | **P1** | Generic weapon. |
| `Assets/Sprites/Weapons/Sword.png` | `Fixed Direction Stab Ability.prefab`, `Level 1.unity` | Identity (VS style) | MIT / Unknown | **Should Replace** | Cat Claw Slash / Toy Sword | CC0 / Original Art | Attack reach indicator | Visibility during swing | **P1** | Store-blocking VS reference. |
| `Assets/Sprites/Weapons/Lightsaber.png` | `Lightsaber Ability.prefab` | Identity (Star Wars IP) | MIT / Unknown | **Must Replace** | Energy Ring / Laser Pointer | CC0 / Original Art | Beam scale check | Laser path readability | **P0** | IP/Copyright risk (Star Wars). |
| `Assets/Sprites/Weapons/Bazooka.png` | `Bazooka Gun Ability.prefab` | Safe | MIT / Unknown | **Should Replace** | Food Cannon / Milk Launcher | CC0 / Original Art | Projectile launch alignment | Bazooka projectile scaling | **P1** | Generic gun. |

---

## Verification Protocol for Asset Replacement

No asset replacement commit will be accepted unless it provides the following verification logs:

1. **Editor Play Mode Smoke Test:** Zero NullReferenceExceptions or missing references in console.
2. **Android Silhouette Check:** Screenshots showing 1:1 mobile resolution visibility in both light and dark backgrounds.
3. **Walk Anim Check:** Confirm walk sequence contains the required frame slices (if multi-frame).
4. **Compile check:** Gradle build success.

---

## Asset Variant Preservation Contract

Rules:

1. Every source sprite, PNG, spritesheet frame, and sliced sprite must have an explicit target mapping before replacement.
2. Do not collapse multiple gameplay variants into one visual unless the matrix explicitly marks them as intentionally merged and explains why.
3. Enemy variants must preserve gameplay readability:
   * size tier
   * color/tint tier
   * HP/damage/speed role if known
   * elite/boss distinction
   * collider/scale implications
4. Collectible variants must preserve value/readability:
   * Coin1, Coin2, Coin5, Coin10, Coin30, Coin50 must remain visually distinguishable.
   * XP gem frames must remain visually distinguishable.
   * UI icon must remain separate from world pickup sprites if the project currently treats it separately.
5. For every replacement candidate, the matrix must include:
   * Current asset path
   * Current GUID if available
   * Current gameplay role
   * Current prefab / ScriptableObject / material reference
   * Source pack
   * Exact source file path, only after pack extraction
   * Target repo path
   * License
   * Import/slicing settings
   * Variant preservation note
   * Test evidence required
6. If a source pack contains multiple relevant variants, the implementer must map each useful variant separately or explicitly reject it with a reason.
7. No “single generic monster/coin/gem replacement for all variants” is allowed.
8. No AI-generated or unverified asset may be used as a final repo asset.

