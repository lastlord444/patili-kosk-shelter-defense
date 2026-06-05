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
| **R34** | Programmatic Play Mode simulation bypass risk | MEDIUM | HIGH | Using bypass shortcuts for Play Mode smoke test skips UI/EventSystem verification. Mitigation: Simulate user click flow (onClick.Invoke) via EventSystem. Note: onClick.Invoke is an EventSystem simulation, not a physical touch/pointer event. | ⚠️ Partially Mitigated |
| **R35** | Auto-target filtering wrong-object risk | MEDIUM | HIGH | Explicitly filter the spatial hash grid query using `client is Monster && monster.HP > 0` checks. | ✔️ Mitigated |
| **R36** | Starter weapon swap balance risk | LOW | MEDIUM | Retain old melee abilities in the level upgrade pool so gameplay options are preserved while combat is improved. | ✔️ Mitigated |
| **R37** | Pistol visual not rendering/visible | HIGH | MEDIUM | Runtime procedural GameObject creation timing or disabled components in parent prefab may hide weapon visual. Mitigation: Checked sorting order, parenting logic, and verified visibility in Game View. | ✔️ Mitigated |
| **R38** | Early enemy pressure overwhelming player | HIGH | MEDIUM | High initial wave density in Level 1 may defeat starter pistol players instantly. Mitigation: Reduced Level 1 spawn density in early keyframes and verified 30-60s onboarding window. | ✔️ Mitigated |
| **R39** | Upgrade pool mismatch | MEDIUM | MEDIUM | Level-up choices containing fantasy stats break shooter game feel. Mitigation: Cleaned up Level 1 abilityPrefabs list to contain only themed weapons and passives. | ✔️ Mitigated |
| **R40** | Unity Editor stability / unexpected close | MEDIUM | HIGH | Domain reloads or player builds can crash the editor. Mitigation: Resolved by disabling EditorApplication.Exit(0) in automated SmokeTest.cs scripts. | ✔️ Mitigated |
| **R41** | Clone identity from residual abilities | MEDIUM | LOW | Original fantasy abilities (e.g. garlic, magic water) dilute the shelter rescue theme. Mitigation: Phase out or rename/re-theme abilities in subsequent cleanup steps. | ⚠️ Open |
| **R42** | Fake evidence reporting | HIGH | HIGH | Local `file:///` paths in PR logs do not confirm build/UI functionality for remote reviewers. Mitigation: Screenshots copied to artifact folders, automated test report generated, and verified. | ✔️ Mitigated |
| **R43** | Tabancanın Zayıf Hissedilmesi (Pistol underpowered feel) | MEDIUM | HIGH | Başlangıç tabancasının hasar veya ateş hızının zayıf olması riski. *Çözüm:* Seviye atlandığında seçilebilecek programatik Pistol Upgrade v1 sistemi (hasar, bekleme süresi, mermi hızı, mermi sayısı) ile güç dengesi ayarlandı. | ✔️ Mitigated |
| **R44** | Erken Aşama Canavar Baskısı (Spawn pressure risk) | HIGH | HIGH | Level 1 başlangıcında canavar sayısının oyuncuyu anında ezmesi riski. *Çözüm:* `Level1WaveDirector` ile ilk 10 saniye boyunca 5 saniyede bir canavar doğurulacak şekilde yumuşak geçiş sağlandı. | ✔️ Mitigated |
| **R45** | Alakasız Geliştirme Havuzu (Upgrade mismatch risk) | MEDIUM | HIGH | Nişancı karakter oynarken havuzun büyüsel/yakın dövüş yetenekleriyle dolması riski. *Çözüm:* `AbilityManager` içinde başlangıç tabancası aktifken bu tür öğeleri engelleyen `IsShooterRelevant` filtresi uygulandı. | ✔️ Mitigated |
| **R46** | Elit Canavar Görünürlük Eksikliği (Elite telegraph risk) | MEDIUM | HIGH | Güçlü/elit düşmanın ekrana aniden çıkıp oyuncuyu haksızca öldürmesi riski. *Çözüm:* 60. saniyede prosedürel top-center `EliteWarningUI` ile `"ELIT DUSMAN YAKLASIYOR"` uyarısı verilerek oyuncu hazırlandı. | ✔️ Mitigated |
| **R47** | Ekonomi / Yetersiz Coin Düşüşü (Coin economy/drop risk) | LOW | MEDIUM | Canavarların az coin düşürmesi ve ilk dakikada yükseltme yapılamaması riski. *Çözüm:* MonsterBlueprint loot table kontrol edildi, mıknatıs ve coin toplama akışı manuel test edildi. | ✔️ Mitigated |
| **R48** | Taret & Destek Noktası Görsel Kalitesi (Turret/support point visual quality risk) | HIGH | MEDIUM | Bu PR'da taret kodu eklenmedi, görsel kalitesi ve entegrasyonu sonraki aşamaya ertelendi. | ⚠️ Open |
| **R49** | Kule Savunması Tür Kayması (Tower-defense pivot risk) | MEDIUM | HIGH | Destek noktaları ve taretler sadece yardımcı eleman olarak planlandı, serbest yerleştirme kaldırıldı. Eski turret görselleri shooter hissiyatını bozmamalı. | ⚠️ Open |
| **R50** | Android Derleme Bozulması (Android build regression risk) | HIGH | HIGH | `SmokeTest/BuildAndroid` çalıştırılarak derlemenin hatasız bittiği ve APK'nın üretildiği doğrulandı. | ✔️ Mitigated |
| **R51** | Destek Noktası Arayüz Anlaşılırlığı Riski (Support point UX clarity risk) | MEDIUM | HIGH | Oyuncuların taret yerleşim alanlarını ve interaktif noktaları anlamakta zorluk yaşaması riski. Sonraki PR'da görsel kılavuzlar ve belirgin UI vurguları eklenmeli. | ⚠️ Open |
| **R52** | Şablon Değiştirme / Tür Kayması Riski (Base pivot risk) | LOW | HIGH | Başka bir kule savunma şablonuna geçildiğinde nişancı dinamiklerinin kaybolma riski. *Çözüm:* Mevcut survivors base'i korundu. | ✔️ Mitigated |
| **R53** | Repo Değiştirme Maliyeti Riski (Repo switching cost risk) | LOW | HIGH | Repo değiştirildiğinde mobile input, pool, ve seviye sistemlerini sıfırdan kurmanın getirdiği zaman kaybı riski. *Çözüm:* Mevcut base'in korunmasına karar verildi. | ✔️ Mitigated |
| **R54** | Level 1 Haksız Zorluk Riski (Level 1 unfair difficulty risk) | HIGH | HIGH | Canavar HP'lerinin fazla ve temas hasarının öldürücü olması. *Çözüm:* Melee hasarı %90 düşürüldü, HP'ler ilk 60s için mutlak 10'a, sonraki 60s için 15'e kilitlendi (negatif hpBuff). | ✔️ Mitigated |
| **R55** | Elit Düşmanın Çok Erken Gelmesi (Elite too early risk) | MEDIUM | HIGH | Level 1'de elit düşmanın oyuncuyu ezmesi. *Çözüm:* Elit canavarlar ve uyarıları Seviye 1'de tamamen devre dışı bırakıldı. | ✔️ Mitigated |
| **R56** | Gelişim ve Ekonomi Dengesizliği Riski (XP/coin pacing insufficiency risk) | MEDIUM | HIGH | Oyuncunun yeterli XP toplayamaması veya ilk geliştirmeyi tabanca harici alması. *Çözüm:* İlk upgrade ekranı `firstUpgradeOffered` ile %100 tabanca odaklı yapıldı. | ✔️ Mitigated |
| **R57** | Sonsuz Bölüm Belirsizliği Riski (Endless level clarity risk) | MEDIUM | MEDIUM | Bölümün bitiş süresinin belirsizliği. *Çözüm:* Seviye 1 için 120 saniyelik net win condition (LevelPassed) eklendi. | ✔️ Mitigated |
| **R58** | Nitelik Yansıtma/Kayıt Hatası (Reflection value binding risk) | HIGH | HIGH | C# GetFields yansıtma yönteminin base sınıflardaki (örn. ProjectileAbility) protected alanları bulamaması nedeniyle yükseltmelerin tabancaya etki etmemesi riski. *Çözüm:* Init metodu base type hiyerarşisini tarayacak şekilde güncellendi. | ✔️ Mitigated |
| **R59** | Uzaktan Fırlatan Düşman Riski (Level 1 projectile risk) | HIGH | HIGH | Seviye 1'de uzaktan projectile/nesne fırlatan canavarların haksız ölümlere yol açması. *Çözüm:* Ranged, Throwing, Boomerang düşmanları tamamen yasaklandı. | ✔️ Mitigated |

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
