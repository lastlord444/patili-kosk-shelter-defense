# RISK_REGISTER.md

> Last updated: 2026-05-31
> Status: UPDATED (Phase 2A Baseline Audit + Template Evaluation Spike)

## Risk Table

| ID | Risk | Probability | Impact | Mitigation | Status |
|---|---|---|---|---|---|
| **R01** | Third-party art assets with unclear license | MEDIUM | HIGH | Enforce the per-asset replacements detailed in [ASSET_REPLACEMENT_MATRIX.md](ASSET_REPLACEMENT_MATRIX.md). | ⚠️ Open (Phase 2B Focus) |
| **R02** | Vampire Survivors-like identity / clone-feel risk / unclear provenance | LOW | HIGH | Replace all Vampire Survivors-like character/enemy sprites and convert core gameplay to shelter defense. | ⚠️ Open (Phase 2B/3 Focus) |
| **R03** | Font license not suitable for commercial use | LOW | MEDIUM | Verified Noto Sans & LiberationSans are OFL (safe for commercial use). | ✔️ Mitigated |
| **R04** | Audio/music not cleared for commercial use | LOW | MEDIUM | Verified there are **0 audio files** in the repo. Audio licensing risk is non-existent, but missing audio is a development risk. | ✔️ Mitigated (License) |
| **R07** | Scope creep — project remains VS-like clone instead of shelter defense | MEDIUM | HIGH | Enforce [REPO_TRUTH.md](REPO_TRUTH.md) and [CONVERSION_PLAN.md](CONVERSION_PLAN.md) in all PRs. | ⚠️ Open |
| **R08** | Android performance issues from base project | LOW | MEDIUM | Profile after core gameplay conversion. | ⚠️ Open |
| **R09** | Mobile virtual joystick input wiring issue | LOW | MEDIUM | Resolved - Uncommented SendValueToControl calls in TouchJoystick.cs. | ✔️ Mitigated |
| **R15** | Unity MCP removal breaks editor-state verification | LOW | HIGH | Do not remove MCP package from verification branches. | ⚠️ Open |
| **R16** | TextMesh Pro auto-generated shader noise clutters PR diffs | HIGH | LOW | Persist TMP Essential Resources in repository to avoid recurring import prompts. | ✔️ Mitigated |
| **R17** | Visual variety collapse from over-aggressive placeholders | HIGH | HIGH | Reject mass placeholder trial; enforce curated asset mapping protocol. | ✔️ Mitigated |
| **R18** | Android APK size inflation due to large CJK fonts | HIGH | MEDIUM | `NotoSansMonoCJKtc-Regular.otf` is 15.6 MB and its SDF Asset is 33.7 MB. Replacing these with optimized, Turkish/English-only fonts will reduce final build size. | ⚠️ Open |
| **R19** | Missing Turkish (tr-TR) localization support | MEDIUM | MEDIUM | The game has Turkish naming ("Patili Köşk") but only English/Chinese localization. Add `tr-TR` locales. | ⚠️ Open |
| **R20** | Broken/Missing audio asset references in prefabs | HIGH | LOW | `經驗球.prefab` contains a broken reference to a missing audio clip GUID (`1cc34cd39f4e34929ae51c22b318d5d5`). Remove or replace during audio sourcing. | ⚠️ Open |
| **R21** | Chinese file naming convention footprint | LOW | LOW | Legacy file/folder names (e.g., `寶箱.prefab`, `初級小兵.asset`) represent a clone footprint. Rename files to English in a future cleanup phase. | ⚠️ Open |
| **R23** | Unity MCP connection returns stdio EOF | HIGH | HIGH | Establish connection or run fallback local audits and request restart from user. | ✔️ Verified working |
| **R24** | Missing themed collectible replacement assets | HIGH | HIGH | Document Kenney CC0 Packs as candidate source packs and plan subset import. | ⚠️ Open |
| **R25** | Unverified exact asset filenames | HIGH | MEDIUM | Exact asset filenames are not verified until the Kenney packs are downloaded/extracted and file list is recorded. | ⚠️ Open |
| **R26** | Variant collapse risk | HIGH | HIGH | Require explicit source-to-target mapping for every sprite/frame before import or replacement. | ⚠️ Open |
| **R27** | Template pivot — switching to a different survivor template base | LOW | HIGH | Evaluated 3 alternatives (survivors-roguelike-kit, Monster Survivors, Survival.io). All rejected: AI-generated assets (blocker), Asset Store EULA (no repo commit), pivot cost exceeds gap-fill cost. Decision: continue with current base. See [TEMPLATE_EVALUATION.md](TEMPLATE_EVALUATION.md). | ✅ Mitigated |
| **R28** | Region/chapter scope creep | MEDIUM | HIGH | Enforce non-goals in [REGION_CHAPTER_PROGRESSION.md](REGION_CHAPTER_PROGRESSION.md). Ensure region system does not turn into a city builder or full shelter simulation. | ⚠️ Open |
| **R29** | Monetization ethics / pay-to-win risk | LOW | HIGH | Diamonds are strictly for future acceleration/comfort. Strictly prohibit gacha, lootboxes, and locking rescue missions behind paywalls. | ⚠️ Open |
| **R30** | Progression before core gameplay risk | MEDIUM | HIGH | Too much meta-design could delay the core shelter defense loop. Complete Region/Chapter contract and move swiftly to shelter entity implementation. | ⚠️ Open |
| **R31** | Runtime layer hack risk for Shelter targeting | HIGH | HIGH | Rejected runtime layer hacks to avoid physics/collision side effects. Mitigated by using explicit targeting (`TargetTransform`) and dynamic `IDamageable` collision queries. | ✔️ Mitigated |
| **R32** | Force-killing Unity Editor / MCP instance confusion | MEDIUM | HIGH | Force-killing active Unity process under MCP control causes instance tracking loss and deadlock. Mitigation: Explicitly bind/select active instance, never force-kill Unity. | ⚠️ Open |
| **R33** | Incomplete Android build evidence reporting | MEDIUM | HIGH | Reporting partial/compiling state as successful creates regression risk. Mitigation: Only mark as success when full APK/AAB is generated on disk with file verification. | ✔️ Mitigated |
| **R34** | Programmatic Play Mode simulation bypass risk | MEDIUM | HIGH | Using bypass shortcuts for Play Mode smoke test skips UI/EventSystem verification. Mitigation: Simulate exact user click flow (onClick.Invoke) via EventSystem to trigger scenes. | ✔️ Mitigated |




## Closed Risks

| ID | Risk | Resolution |
|---|---|---|
| **R00** | MIT license not preserved | Resolved — LICENSE file preserved in import. |
| **R05** | Unity version incompatibility on dev machine | Resolved — Successfully upgraded from 2021.3.21f1 to Unity 6 (6000.3.16f1) with 0 compiler errors. |
| **R06** | Unity package dependencies with incompatible licenses | Resolved — Audited Packages/manifest.json; all dependencies are official Unity packages under Unity EULA. |
| **R09** | Mobile virtual joystick input wiring issue | Resolved — Uncommented SendValueToControl calls in TouchJoystick.cs. Verified in Play Mode. |
| **R16** | TextMesh Pro auto-generated shader noise clutters PR diffs | Resolved — Persisted TMP Essential Resources directly in the repository to eliminate recurring editor import prompts. |
| **R17** | Visual variety collapse from over-aggressive placeholders | Resolved — Rolled back the Kenney placeholder trial and restored original assets to protect gameplay variety. |
| **R22** | Star Wars lightsaber association / IP risk | Resolved — Lightsaber.png replaced with a self-created safe laser pointer beam placeholder (MIT-compatible). |

## Notes

- **Android Build Size Clarification:** The actual output APK size of `Build/android_smoke.apk` is **60.8 MB**. The custom build pipeline logs `Output Size: 782473327 bytes` (782 MB) because it includes all Gradle intermediates, IL2CPP temporary files, and debug symbol files generated under the `Build/` or `Temp/` folder. The final APK size is normal for a 2D Unity project but can be further optimized by addressing **R18**.
- Art and IP risks (R01, R02) will be addressed systematically under the new curated asset mapping protocol (`docs/VISUAL_DIRECTION.md`) and the master replacement matrix (`docs/ASSET_REPLACEMENT_MATRIX.md`).
- The success of the Android build verified that there are no compiler blockers or SDK/NDK integration blockers on the development environment.
- Mobile virtual joystick input wiring blocker was successfully resolved and verified via Play Mode smoke test simulation.
- **Main Character Sprite Replacement (Phase 2B-2):** The Vampire Survivors-like playable character sprites (`MainCharacterBlue.png`, `MainCharacterGray.png`, `MainCharacterPurple.png`, `MainCharacterWhite.png`) have been replaced with 100% project-owned, MIT-compatible cute cat guardian / shelter defender placeholders (animated walk cycles). This mitigates the store-blocking playable character identity risk.
