# TEMPLATE_EVALUATION.md — PR #16 Spike

> **Amaç:** Patili Köşk Shelter Defense için mevcut `matthiasbroske/VampireSurvivorsClone` tabanında devam mı edeceğiz, yoksa daha güçlü bir Unity survivor template tabanına pivot mı edeceğiz?
>
> **Sonuç:** Bu doküman kanıta dayalı karar verir. Production'a kod veya asset import edilmez.

---

## 1. Mevcut Taban: VampireSurvivorsClone (matthiasbroske)

| Alan | Değer |
|---|---|
| **Repo** | https://github.com/matthiasbroske/VampireSurvivorsClone |
| **Lisans** | MIT |
| **Unity Sürümü** | Orijinal: 2021.3.21f1 → Projede yükseltildi: Unity 6 (6000.3.16f1) |
| **Render Pipeline** | Built-in |
| **C# Script Sayısı** | ~140 |
| **Toplam Dosya (Assets/)** | ~990 |
| **Sahne Sayısı** | 3 (Main Menu, Level 1, Character Set Generation) |
| **Prefab Sayısı** | 69 |
| **ScriptableObject Asset Sayısı** | 119 |
| **PNG Sprite Sayısı** | ~57 |
| **GitHub Stars** | ~400+ |

### Mevcut Taban — Güçlü Yönler

| # | Sistem | Detay |
|---|---|---|
| 1 | **Object Pooling** | `Pool` base class + 9 somut pool (MonsterPool, CoinPool, ExpGemPool, ProjectilePool, BoomerangPool, ThrowablePool, ChestPool, DamageTextPool, ExplosiveProjectilePool). `UnityEngine.Pool` namespace kullanıyor. |
| 2 | **ScriptableObject Mimarisi** | `CharacterBlueprint`, `LevelBlueprint`, `MonsterBlueprint`, `CoinBlueprint`, `ExpGemBlueprint`, `ChestBlueprint` + 6 CollectableType SO. Inspector'dan veri girişi hazır. |
| 3 | **Enemy Çeşitliliği** | 5 farklı monster tipi: `MeleeMonster`, `RangedMonster`, `BoomerangMonster`, `ThrowingMonster`, `BossMonster` + 5 boss ability pattern (BulletHell, Charge, Grenade, Shotgun, Walk). |
| 4 | **Spawn Sistemi** | `MonsterSpawnTable` keyframe bazlı spawn rate. Oyuncu hareket yönüne ağırlıklı spawn. Off-screen spawn. |
| 5 | **Ability Sistemi** | 20+ ability: Dagger, Slash, Stab, Gun, MachineGun, Bazooka, Molotov, Book, Boomerang, Shuriken, GravityWell, GrenadeTrowable + passive upgrade'ler (Damage, Armor, AOE, Cooldown, Knockback, ProjectileCount, Speed, Recovery, Lifesteal, IceSkates). |
| 6 | **Collectible Sistemi** | Coin (6 tier: Bronze1→Gold50), ExpGem (multiple tiers), Health, Magnet, Bomb, RedPotion. Magnetic pickup mekanizması. |
| 7 | **Spatial Hash Grid** | `SpatialHashGrid` — performans optimizasyonu için spatial partitioning. |
| 8 | **Mobile Input** | `TouchJoystick` — custom virtual joystick implementasyonu. SafeArea desteği. |
| 9 | **Localization** | Unity Localization package + 3 dil (en, zh-Hans, zh-Hant). |
| 10 | **Infinite Background** | Shader-based infinite scrolling background. |
| 11 | **Android Build** | Phase 1'de doğrulanmış: Android build başarılı, 0 runtime exception. |

### Mevcut Taban — Eksiklikler

| # | Eksik Sistem | Shelter Defense İçin Gerekli mi? | Kritiklik |
|---|---|---|---|
| 1 | **Save/Load Sistemi** | EVET — Shelter upgrade progress, currency persistence | **KRİTİK** |
| 2 | **Meta-game Shop/Upgrade** | EVET — Shelter upgrade menüsü, permanent progression | **KRİTİK** |
| 3 | **Currency Economy** | EVET — Oyun sonrası coin toplama var (`PlayerPrefs.SetInt("Coins")`) ama harcama/shop yok | **YÜKSEK** |
| 4 | **Buff/Debuff Sistemi** | ORTA — Burn, freeze, slow yok. Shelter defense'de düşman slow/freeze olabilir | **ORTA** |
| 5 | **Procedural Map** | DÜŞÜK — Infinite background var ama tile-based map generation yok | **DÜŞÜK** |
| 6 | **Weapon Evolution** | DÜŞÜK — Ability yükseltmesi var ama evolution/combo sistemi yok | **DÜŞÜK** |
| 7 | **Ses/Müzik Altyapısı** | EVET — Sıfır ses dosyası, kırık AudioSource referansı | **YÜKSEK** |
| 8 | **Tutorial/Onboarding** | EVET — Mobil oyun için gerekli | **ORTA** |

---

## 2. Aday Alternatif: survivors-roguelike-kit (Roo-Roo-Roo)

| Alan | Değer |
|---|---|
| **Repo** | https://github.com/Roo-Roo-Roo/survivors-roguelike-kit |
| **Lisans** | MIT |
| **Fiyat** | Ücretsiz (eski Asset Store satışı: $30, açık kaynak oldu) |
| **Unity Sürümü** | Unity 6 (ayrıca 2021/2022 uyumluluğu planlanıyor) |
| **Render Pipeline** | Built-in |
| **GitHub Stars** | 5 |
| **Forks** | 4 |
| **DOTween Bağımlılığı** | Var — ticari kullanım için ayrı DOTween lisansı gerekiyor |

### survivors-roguelike-kit — Güçlü Yönler

| # | Sistem | Detay |
|---|---|---|
| 1 | **Save/Settings Sistemi** | Yerleşik save ve settings management. Mevcut tabanda tamamen eksik. |
| 2 | **Buff Sistemi** | Burn, freeze, slow ve diğer buff örnekleri. |
| 3 | **Skill Evrimi** | Aktif skill + pasif skill → evolution weapon sistemi. |
| 4 | **Procedural Map** | Ağırlıklı tile generation ile prosedürel harita oluşturma. |
| 5 | **Event-Channel Runtime** | Event-channel tabanlı runtime sistem, decouple edilmiş mimari. |
| 6 | **ScriptableObject Mimarisi** | Tamamen SO-driven, Inspector'dan hızlı prototyping. |
| 7 | **Dokümantasyon** | GitBook üzerinde kapsamlı doküman: yeni karakter, düşman, stage, map, level, skill, buff ekleme rehberleri. |

### survivors-roguelike-kit — KRİTİK BLOCKER'LAR

| # | Blocker | Detay | Seviye |
|---|---|---|---|
| 1 | **AI-Generated Asset'ler** | README'de açıkça belirtilmiş: "The pixel art assets and UI assets were generated with AI tools." Bu proje politikamıza göre **kesinlikle YASAK**. Bu assetler kullanılamaz, import edilemez. | **BLOCKER** |
| 2 | **DOTween Ticari Lisans Gereksinimi** | DOTween dahil edilmiş. Ticari kullanımda ayrı lisans satın alınması gerekiyor. Mevcut tabanda böyle bir 3. parti bağımlılık yok. | **YÜKSEK** |
| 3 | **Düşük Community Adoption** | 5 star, 4 fork, 7 commit. Mevcut taban 400+ star ile çok daha güvenilir/battle-tested. | **ORTA** |
| 4 | **Pivot Maliyeti** | Mevcut projede Phase 1 tamamen tamamlanmış: Unity 6 upgrade, Android build doğrulama, 3 sprite değişikliği, 4 doküman dosyası, 15 PR. Yeni tabana geçiş bu ilerlemeyi sıfırlar. | **YÜKSEK** |
| 5 | **Karmaşıklık vs İhtiyaç** | Procedural map, weapon evolution gibi sistemler shelter defense için gerekli değil. Ekstra karmaşıklık, ekstra bakım maliyeti. | **ORTA** |

---

## 3. Aday Alternatif: Monster Survivors - Full Game (October Studio)

| Alan | Değer |
|---|---|
| **Platform** | Unity Asset Store |
| **Lisans** | Unity Asset Store EULA (Standard License) |
| **Fiyat** | ~$99 USD |
| **Unity Sürümü** | Unity 6 / 6.1+ uyumlu |
| **Geliştirici** | October Studio |

### Monster Survivors — Güçlü Yönler

| # | Sistem | Detay |
|---|---|---|
| 1 | **Tam Oyun Şablonu** | 36 ability (12 aktif, 12 pasif, 12 evolution), 14 düşman, 6 boss. |
| 2 | **Jobs + Burst Optimizasyonu** | Damage indicator ve drop sistemleri için Unity Jobs System ve Burst Compiler. Performans-kritik yüksek yoğunluklu sahneler için avantaj. |
| 3 | **Stage Creator** | Unity Timeline tabanlı stage oluşturma sistemi. |
| 4 | **Upgrade Sistemi** | Genişletilebilir permanent upgrade/progression sistemi. |
| 5 | **Mobil/Desktop Uyumlu** | Her iki platform için optimize. |
| 6 | **Orijinal Art Asset'ler** | Dahili sanat varlıkları orijinal (AI-generated değil). |

### Monster Survivors — Dezavantajlar

| # | Dezavantaj | Detay | Seviye |
|---|---|---|---|
| 1 | **Ücretli** | $99 maliyet. | **ORTA** |
| 2 | **Asset Store EULA** | MIT gibi serbest değil. Kaynak kod yeniden dağıtılamaz, repo'ya commit edilemez. License audit karmaşıklaşır. | **YÜKSEK** |
| 3 | **Pivot Maliyeti** | Mevcut Phase 1 ilerlemesi sıfırlanır. | **YÜKSEK** |
| 4 | **Jobs/Burst Karmaşıklığı** | Shelter defense için aşırı mühendislik. Basit Pool sistemi yeterli. | **ORTA** |
| 5 | **Kapalı Kaynak Community** | Asset Store ürünü, issue tracker yok, community feedback sınırlı. | **ORTA** |

---

## 4. Survival.io Tarzı Template'ler (Çeşitli)

| Alan | Değer |
|---|---|
| **Platform** | Unity Asset Store, Fab.com, sellunitysourcecode.com |
| **Fiyat Aralığı** | $13–$300+ |
| **Lisans** | Platforma göre değişir (Asset Store EULA, vb.) |

### Survival.io Template'ler — Genel Değerlendirme

Bu kategori çeşitli satıcılardan gelen "reskin-ready" complete project template'leri içerir. Genel sorunlar:

| # | Sorun | Detay |
|---|---|---|
| 1 | **Lisans Belirsizliği** | Her satıcının farklı EULA'sı. Kaynak kodu açık repo'ya koymak genellikle yasak. |
| 2 | **Asset Provenance** | Dahili asset'lerin kaynağı/lisansı doğrulanamaz. AI-generated olma riski. |
| 3 | **Bakım/Destek Riski** | Tek geliştirici, güncelleme sıklığı değişken. |
| 4 | **Aşırı Mühendislik** | IAP, reklam entegrasyonu, SHA256 save encryption gibi shelter defense için gereksiz sistemler. |

---

## 5. Karşılaştırma Matrisi

| Kriter | Mevcut Taban (VampireSurvivorsClone) | survivors-roguelike-kit | Monster Survivors | Survival.io Template'ler |
|---|---|---|---|---|
| **Lisans** | ✅ MIT | ✅ MIT (ama DOTween ek lisans) | ⚠️ Asset Store EULA | ⚠️ Değişken |
| **Fiyat** | ✅ Ücretsiz | ✅ Ücretsiz | ⚠️ $99 | ⚠️ $13–$300 |
| **AI-Generated Asset** | ✅ Yok (Kenney CC0 + orijinal) | ❌ **VAR — BLOCKER** | ✅ Yok (orijinal) | ❓ Doğrulanamaz |
| **Repo'ya Commit** | ✅ Evet | ✅ Evet | ❌ Hayır (EULA) | ❌ Genellikle hayır |
| **Phase 1 İlerlemesi** | ✅ Tamamlandı | ❌ Sıfırdan başla | ❌ Sıfırdan başla | ❌ Sıfırdan başla |
| **Android Build** | ✅ Doğrulandı | ❓ Test edilmedi | ✅ Optimize | ❓ Değişken |
| **Save Sistemi** | ❌ Yok | ✅ Var | ✅ Var | ✅ Genellikle var |
| **Shop/Upgrade** | ❌ Yok | ✅ Var | ✅ Var | ✅ Genellikle var |
| **Object Pooling** | ✅ Custom (9 pool) | ✅ Var | ✅ Var (Jobs/Burst) | ✅ Genellikle var |
| **ScriptableObjects** | ✅ Kapsamlı (119 asset) | ✅ Kapsamlı | ✅ Var | ❓ Değişken |
| **Spatial Optimization** | ✅ SpatialHashGrid | ❓ Bilinmiyor | ✅ Jobs/Burst | ❓ Değişken |
| **Enemy Çeşitliliği** | ✅ 5 tip + 5 boss pattern | ✅ Melee/Ranged/Charge | ✅ 14 düşman + 6 boss | ❓ Değişken |
| **Ability Sistemi** | ✅ 20+ ability | ✅ Aktif + Pasif + Evolution | ✅ 36 ability | ❓ Değişken |
| **Dokümantasyon** | ⚠️ Minimal README | ✅ GitBook | ✅ Asset Store docs | ❓ Değişken |
| **Community** | ✅ 400+ stars | ❌ 5 stars | ⚠️ Kapalı kaynak | ❌ Minimal |
| **Shelter Defense Uyumu** | ✅ Kolay dönüşüm | ⚠️ Fazla karmaşık | ⚠️ Fazla karmaşık | ⚠️ Fazla karmaşık |

---

## 6. GAP Analizi: Mevcut Tabanda Eksik Olanlar ve Çözüm Yolu

Mevcut tabanda eksik olan kritik sistemler ve bunların **kendimizan yazılması** ile ilgili effort tahmini:

| Eksik Sistem | Tahmini Effort | Çözüm Yaklaşımı |
|---|---|---|
| **Save/Load Sistemi** | 1–2 gün | `JsonUtility.ToJson/FromJson` + `Application.persistentDataPath`. Basit `ShelterSaveData` sınıfı: shelter level, coin count, upgrade states. |
| **Shop/Upgrade Menüsü** | 2–3 gün | `ShelterUpgradeBlueprint` SO + UGUI shop panel. Mevcut `AbilitySelectionDialog` referans alınarak benzer pattern. |
| **Currency Economy** | 0.5–1 gün | Zaten `PlayerPrefs.SetInt("Coins")` var. Bu, `ShelterSaveData`'ya taşınacak. |
| **Buff/Debuff** | 1–2 gün | `IBuff` interface + `BuffManager`. Gerekirse eklenecek. |
| **Ses/Müzik** | 1–2 gün | `AudioManager` singleton + CC0 ses kaynakları. Kırık referans temizliği. |
| **Shelter Entity** | 2–3 gün | `Shelter` MonoBehaviour: HP, position, upgrade level. Düşman AI'da hedef olarak eklenir. |
| **Toplam Tahmini** | **~8–13 gün** | — |

Bu effort, tamamen yeni bir tabana pivot etmenin maliyetinden (Phase 1 tekrarı: ~3–5 gün + yeni codebase öğrenme eğrisi: ~3–5 gün + asset/lisans denetimi: ~2–3 gün = **~8–13 gün minimum**) eşdeğer veya daha düşüktür. Üstelik mevcut tabanda bu sistemler doğrudan ihtiyaca göre yazılır, gereksiz karmaşıklık taşımaz.

---

## 7. Karar ve Öneri

### ✅ KARAR: Mevcut taban ile devam et (VampireSurvivorsClone)

**Gerekçeler:**

1. **AI-Generated Asset BLOCKER:** `survivors-roguelike-kit` assetleri AI-generated olduğunu açıkça beyan ediyor. Proje politikamız bunu kesinlikle yasaklıyor. Bu tek başına bu adayı eliyor.

2. **Lisans Temizliği:** Mevcut taban MIT, repo'da commit, açık kaynak, temiz. Asset Store ürünleri EULA nedeniyle repo'ya commit edilemez.

3. **Sıfır Pivot Maliyeti:** Phase 1 tamamlandı, 15 PR merge edildi, Android build doğrulandı, 4 doküman dosyası yazıldı, 3 sprite değiştirildi. Bu ilerleme korunur.

4. **Yeterli Mimari:** Object pooling (9 pool), ScriptableObject mimarisi (119 asset), Spatial Hash Grid, 5 enemy tipi, 20+ ability, mobile input — shelter defense için fazlasıyla yeterli temel.

5. **Eksiklikler Yönetilebilir:** Save sistemi, shop menüsü, currency economy gibi eksikler ~8–13 günde shelter defense ihtiyacına özel olarak yazılabilir. Bu, pivot maliyetine eşdeğer.

6. **Karmaşıklık Kontrolü:** Mevcut taban ~140 script ile yönetilebilir boyutta. Alternatifler gereksiz sistemler (procedural map, weapon evolution, IAP, reklam) taşıyor.

### ❌ Elenen Adaylar

| Aday | Eleme Sebebi |
|---|---|
| **survivors-roguelike-kit** | AI-generated pixel art ve UI asset'leri — proje politikası gereği kesinlikle kullanılamaz |
| **Monster Survivors** | Asset Store EULA — repo'ya commit edilemez, $99 maliyet, aşırı mühendislik |
| **Survival.io Template'ler** | Lisans belirsizliği, asset provenance doğrulanamaz, repo'ya commit edilemez |

---

## 8. Sonraki Adımlar (Post-Decision)

Mevcut tabanla devam kararı onaylandıktan sonra:

1. **Phase 2 devam:** Kalan sprite/asset değişimleri (Kenney CC0 collectible'lar, monster sprite'ları).
2. **Phase 3 hazırlık:** `ShelterSaveData`, `ShelterUpgradeBlueprint`, `Shelter` entity tasarımı başlayacak.
3. **docs/REFERENCE_BASE.md güncelle:** Spike sonucunu yansıt — alternatifler değerlendirildi, mevcut taban ile devam kararı alındı.

---

> **Spike Durumu:** Tamamlandı. Production'a asset/code import yapılmadı.
> **Tarih:** 2026-05-31
