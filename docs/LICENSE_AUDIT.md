# LICENSE_AUDIT.md

> Status: AUDITED & UPDATED — Phase 2A audit baseline completed.
> Unity Version: Unity 6 (6000.3.16f1)

## Source Repository

- Repo: https://github.com/matthiasbroske/VampireSurvivorsClone
- Author: matthiasbroske

## Source License

- Type: **MIT License**
- File: [LICENSE](../LICENSE) (preserved in this repo root)
- Copyright: Copyright (c) 2023 matthiasbroske
- Obligations: Preserve copyright notice and license text. ✔️ Done.

## Original LICENSE Preserved

- [x] [LICENSE](../LICENSE) file imported and intact
- [x] [README.md](../README.md) attribution added
- [x] [LICENSE](../LICENSE) file manually verified (matches 100%)

---

## Detailed Asset Group Audit & Action Plan

Orijinal projedeki tüm görsel ve işitsel referanslar Vampire Survivors esintili spritelardan oluşmaktadır. Bu görsel varlıkların lisans durumları belirsiz olduğundan, oyun mağazalara (Google Play Store vb.) sürülmeden önce **tamamen değiştirilecek**, kedi barınağı (Patili Köşk) temalı özgün çizimler ve lisanslı assetlerle değiştirilecektir.

### 1. Sprite ve Görsel Dosyaları (Sprites)

| Dosya Yolu (Path) | Türü / Kullanımı | Lisans Durumu | Risk Seviyesi | Aksiyon / Plan |
|---|---|---|---|---|
| `Assets/Sprites/Characters/MainCharacterBlue.png` | Playable Character | self-created simple placeholder (project-owned / MIT-compatible) | **Mitigated** | **Replaced** |
| `Assets/Sprites/Characters/MainCharacterGray.png` | Playable Character | self-created simple placeholder (project-owned / MIT-compatible) | **Mitigated** | **Replaced** |
| `Assets/Sprites/Characters/MainCharacterPurple.png` | Playable Character | self-created simple placeholder (project-owned / MIT-compatible) | **Mitigated** | **Replaced** |
| `Assets/Sprites/Characters/MainCharacterWhite.png` | Playable Character (Unused) | self-created simple placeholder (project-owned / MIT-compatible) | **Mitigated** | **Replaced** |
| `Assets/Sprites/Monsters/CrabOrange.png` | Melee Monster (Basic) | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **HIGH** | **Replace** (Melee Mouse/Rat) |
| `Assets/Sprites/Monsters/CrabRed.png` | Melee Monster (Variant) | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **HIGH** | **Replace** (Melee Mouse/Rat Red) |
| `Assets/Sprites/Monsters/Alien.png` | Melee Monster (Medium) | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **HIGH** | **Replace** (Medium Stray Dog) |
| `Assets/Sprites/Monsters/PunchMonster.png` | Melee Monster (Hard) | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **MEDIUM** | **Replace** (Heavy Bully Animal) |
| `Assets/Sprites/Monsters/GhostFairy.png` | Ranged Monster | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **HIGH** | **Replace** (Ranged Net Thrower) |
| `Assets/Sprites/Monsters/NailHead.png` | Boomerang Monster | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **MEDIUM** | **Replace** (Ranged Toy Thrower) |
| `Assets/Sprites/Monsters/WizardMonster.png` | Gravity Monster | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **MEDIUM** | **Replace** (Area Hazard Spawner) |
| `Assets/Sprites/Monsters/ExplosiveGuy.png` | Explosive Monster | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **MEDIUM** | **Replace** (Exploding Toy Cat) |
| `Assets/Sprites/Monsters/MiniBoss.png` | Elite Miniboss | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **HIGH** | **Replace** (Mini Boss Catcher) |
| `Assets/Sprites/Monsters/Boss.png` | Final Boss | Belirsiz (Vampire Survivors-like identity / clone-feel risk) | **HIGH** | **Replace** (Big Catcher Boss) |
| `Assets/Sprites/Coins/Coin1.png` | Collectible Currency | Belirsiz (RPG style) | **HIGH** | Replace with Wet Food Can (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Coins/Coin2.png` | Collectible Currency | Belirsiz (RPG style) | **HIGH** | Replace with Dry Food Pack (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Coins/Coin5.png` | Collectible Currency | Belirsiz (RPG style) | **HIGH** | Replace with Cat Treat Pocket (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Coins/Coin10.png` | Collectible Currency | Belirsiz (RPG style) | **HIGH** | Replace with Large Salmon Can (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Coins/Coin30.png` | Collectible Currency | Belirsiz (RPG style) | **MEDIUM** | Replace with Premium Fish Bone (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Coins/Coin50.png` | Collectible Currency | Belirsiz (RPG style) | **MEDIUM** | Replace with Cat Golden Trophy (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/UI/UICoin.png` | HUD Coin Icon | Belirsiz (RPG style) | **HIGH** | Replace with UI Dry Food Icon (Candidate: Kenney UI Pack - CC0) |
| `Assets/Sprites/Gems/Gems.png` | Experience Pickups | Belirsiz (RPG style) | **HIGH** | Replace with Yarn Ball / Feathers (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Gems/GemDarkBlue.png` | Exp Gem Variant | Belirsiz (RPG style) | **HIGH** | Replace with Blue Yarn Ball (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Gems/GemLightBlue.png` | Exp Gem Variant | Belirsiz (RPG style) | **HIGH** | Replace with Light Blue Yarn Ball (Candidate: Kenney Generic Items - CC0) |
| `Assets/Sprites/Weapons/Lightsaber.png` | Ability Projectile | self-created simple placeholder (project-owned / MIT-compatible) | **Mitigated** | **Replaced** |
| `Assets/Sprites/Weapons/Bat.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Toy Bat / Ball) |
| `Assets/Sprites/Weapons/Sword.png` | Ability Projectile | Belirsiz (RPG style) | **MEDIUM** | **Replace** (Cat Claw Slash) |
| `Assets/Sprites/Weapons/Bazooka.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Dry Food Launcher) |
| `Assets/Sprites/Weapons/Bomb.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Yarn Bomb) |
| `Assets/Sprites/Weapons/BossGrenade.png` | Boss Projectile | Belirsiz | **MEDIUM** | **Replace** (Boss Rattle Toy) |
| `Assets/Sprites/Weapons/EnemyBoomerang.png`| Enemy Projectile | Belirsiz | **MEDIUM** | **Replace** (Enemy Catch Net) |
| `Assets/Sprites/Weapons/GravityGrenade.png`| Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Catnip Flask) |
| `Assets/Sprites/Weapons/GrenadeBlue.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Toy Rattle Ball) |
| `Assets/Sprites/Weapons/LifestealDagger.png`| Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Cat Scratching Claw) |
| `Assets/Sprites/Weapons/Machete.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Scratching Stick) |
| `Assets/Sprites/Weapons/MachineGun.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Squeeze Bottle Gun) |
| `Assets/Sprites/Weapons/Molotov.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Catnip Spray Bottle) |
| `Assets/Sprites/Weapons/PlayerBomb.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Yarn Bomb Variant) |
| `Assets/Sprites/Weapons/Shuriken.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Spinning Toy Wheel) |
| `Assets/Sprites/Weapons/ShurikenAlt.png` | Ability Projectile | Belirsiz | **MEDIUM** | **Replace** (Spinning Toy Wheel Variant) |
| `Assets/Sprites/Kenney/Magnet.png` | Item Pickup Icon | CC0 (Kenney.nl) | **LOW** | **Keep** / Optional Replace |
| `Assets/Sprites/Kenney/Skull.png` | Game Over Icon | CC0 (Kenney.nl) | **LOW** | **Keep** / Optional Replace |
| `Assets/Sprites/Kenney/VampireTeeth.png` | Lifesteal Icon | CC0 (Kenney.nl) | **MEDIUM** | **Replace** (Thematic Mismatch - Vampire) |
| `Assets/Sprites/Kenney/WizardTilemap.png` | Chest Assets | CC0 (Kenney.nl) | **LOW** | **Keep** / Optional Replace |
| `Assets/Sprites/Kenney/PlatformerTilemap.png`| HUD / UI Sprites | CC0 (Kenney.nl) | **LOW** | **Keep** / Optional Replace |
| `Assets/Sprites/UI/Circle500.png` | Simple UI Circle | CC0 / Built-in | **LOW** | **Keep** |
| `Assets/Sprites/UI/CircleOutline.png` | Simple UI Circle Outline| CC0 / Built-in | **LOW** | **Keep** |
| `Assets/Sprites/UI/PauseButton.png` | UI Button | CC0 / Built-in | **LOW** | **Keep** |
| `Assets/Sprites/UI/PlayButton.png` | UI Button | CC0 / Built-in | **LOW** | **Keep** |
| `Assets/Sprites/UI/Square100.png` | UI Square | CC0 / Built-in | **LOW** | **Keep** |
| `Assets/TextMesh Pro/Sprites/EmojiOne.png` | TMP Default Sheet | CC-BY (TMP Asset) | **LOW** | **Keep** / Audit usage |

### 2. Audio Dosyaları (Audio)

- **Tespit Edilen Audio Dosyaları:** Projede (`.mp3`, `.wav`, `.ogg`, `.aac` vb.) hiçbir ses ve müzik dosyası **bulunmamaktadır**.
- **Ses Dosyası Sayısı:** **0**
- **Kod/Prefab Düzeyindeki Referanslar:**
  1. `Assets/Scripts/Testing/MiscTesting.cs` script dosyasında 37. satırda:
     `GetComponent<AudioSource>().Play();` referansı bulunmaktadır (A tuşu ile test).
  2. `Assets/Prefabs/Exp Gem/經驗球.prefab` (Exp Gem) prefab'ı üzerinde bir `AudioSource` componenti ve script referansı bulunmaktadır:
     `collectedAudio.clip` alanında `guid: 1cc34cd39f4e34929ae51c22b318d5d5` referansı vardır. Bu dosya projede **mevcut değildir (kırık referans/missing)**.
- **Risk Seviyesi:** **MEDIUM** (Telif riski yoktur ancak oyunun ses altyapısı ve ses dosyaları tamamen eksiktir).
- **Aksiyon:** Gelecek fazlarda CC0 veya özgün ses/müzik assetleri projeye dahil edilmeli, kırık referanslar temizlenmeli veya yeni seslerle güncellenmelidir.

### 3. Font Dosyaları (Fonts)

| Font Adı | Dosya Yolu (Path) | Lisans Durumu | Risk Seviyesi | Aksiyon / Plan |
|---|---|---|---|---|
| `NotoSansMonoCJKtc-Regular.otf` | `Assets/Fonts/NotoSansMonoCJKtc-Regular.otf` | SIL Open Font License (OFL) | **LOW** | **Keep** (Ancak 15.6 MB dosya boyutu Android APK boyutu için risk oluşturmaktadır. Optimize edilmeli / Turkish karakter setiyle hafifletilmeli). |
| `NotoSansMonoCJKtc-Regular SDF.asset`| `Assets/Fonts/NotoSansMonoCJKtc-Regular SDF.asset` | SIL OFL (TMP Generated) | **LOW** | **Keep** (33.7 MB SDF doku atlası boyutu Android APK boyutu için risk oluşturmaktadır). |
| `LiberationSans.ttf` | `Assets/TextMesh Pro/Fonts/LiberationSans.ttf` | SIL Open Font License (OFL) | **LOW** | **Keep** (0.33 MB, güvenli). |

### 4. Sahneler (Scenes)

Projede 3 adet sahne dosyası bulunmaktadır:
1. `Assets/Scenes/Game/Main Menu.unity` — Ana menü sahnesi. (Kullanımda, IP temizliği gerektiriyor)
2. `Assets/Scenes/Game/Level 1.unity` — Ana oynanış sahnesi. (Kullanımda, IP temizliği ve kedi barınağı mekanikleri gerektiriyor)
3. `Assets/Scenes/Character Set Generation/Character Set Generation.unity` — Karakter seti oluşturma sahnesi. (Kullanılmayan geliştirici aracı/artık, **Remove-before-store** kapsamındadır).

### 5. Materyal ve Dokular (Materials & Textures)

Tüm materyaller (`Assets/Materials/`) ve dokular (`Assets/Textures/`) Unity 6 Built-in Render Pipeline standartlarına ve shader yapılarına aittir. Herhangi bir telif hakkı riski içermemektedir:
- `Dissolve.mat` / `Player Death.mat` vb. shader tabanlı materyaller güvenlidir.
- Noise dokuları (`PerlinNoise512.jpeg`, `WhiteNoise256.png`) ve flow map dokuları (`FlowMapTest.png`, `VerticalFlow.png`) efektler için kullanılan standart matematiksel dokulardır.
- Zemin dokuları (`DirtTile.png`, `DirtTileReddish.png`, `DirtTileWhite.png`) geçici zemin döşemesidir. Phase 3'te kedi barınağı bahçesi/odaları zemin dokularıyla değiştirilecektir (**Replace**).

### 6. Paketler ve Bağımlılıklar (Packages)

`Packages/manifest.json` dosyası incelenmiştir. Tüm paketler Unity resmi paketleridir (UI, Input System, Addressables, Localization vb.) ve Unity EULA kapsamındadır. Lisans riski yoktur.

---

## Localization (Yerelleştirme) Tablo Analizi

- **Mevcut Diller:** English (en), 简体中文 (zh-Hans), 繁體中文 (zh-Hant).
- **Tespit Edilen Eksiklik:** Oyunda **Türkçe (tr-TR)** yerelleştirme dili ve tabloları **tamamen eksiktir**.
- **Risk Seviyesi:** **MEDIUM** (Türkiye pazarı ve "Patili Köşk" markası için yerelleştirme eksikliği önemli bir kimlik/kullanıcı deneyimi açığıdır).
- **Aksiyon:** Gelecek fazlarda `Assets/Localization/` altına Türkçe (tr-TR) yerelleştirme desteği eklenmelidir.

---

## Risk ID Özeti (License & Identity)

| ID | Risk | Seviye | Aksiyon | Durum |
|---|---|---|---|---|
| **R01** | MIT Lisansının Korunması | LOW | Lisans ve README atıflarını koru. | ✔️ Korundu (LICENSE dosyası kökte) |
| **R02** | Star Wars lightsaber association / IP risk | LOW / Mitigated | Replaced with self-created placeholder | ✔️ Resolved |
| **R03** | Görsel Assetlerin Lisans Belirsizliği (Vampire Survivors-like identity) | **HIGH** | `Assets/Sprites/` altındaki karakter, düşman ve altın spritelarını sıfırdan değiştir. | ⚠️ Açık (P0/P1 Replace) |
| **R04** | Vampire Survivors-like identity / clone-feel risk | **HIGH** | Oynanışı "Kedi Barınağı Savunması"na dönüştür, isim ve metin referanslarını sil. | ⚠️ Açık (Phase 3 Konsept Dönüşümü) |
| **R05** | Android APK Boyutu Şişmesi (Büyük Font Dosyaları) | **HIGH** | 15.6 MB'lık CJK OTF fontunu ve 33.7 MB'lık SDF assetini optimize et / tr-TR odaklı font ile değiştir. | ⚠️ Açık (Gelecek Faz Optimizasyonu) |
| **R06** | Ses Dosyası Eksikliği ve Kırık Referanslar | **MEDIUM** | Projedeki eksik ses/müzik yapısını kur, `經驗球.prefab` üzerindeki kırık GUID ses referansını çöz. | ⚠️ Açık (Phase 2B/3 Ses Sourcing) |
| **R07** | Türkçe Dil Desteği Eksikliği (Thematic Gap) | **MEDIUM** | "Patili Köşk" adıyla uyumlu Türkçe (tr-TR) lokalizasyon tablolarını ve çevirilerini ekle. | ⚠️ Açık (Gelecek Faz Eklemesi) |

## Candidate Packs AI and License Verification Details

> [!WARNING]
> **Kenney Generic Items** ve **Kenney UI Pack** henüz yerel ortama indirilmemiştir.
> Bu pakette yer alan dosyaların kesin dosya yolları (`PNG/...` vb.) ve dosya adları doğrulanmamıştır, hepsi tahmini/taslak niteliğindedir.
> Asset paketleri indirilip çıkarılana kadar bu kolonlar "pending pack extraction" (paket çıkarılması bekleniyor) ve "not imported" (içe aktarılmadı) olarak kalacaktır. Tahmini dosya yolu yazmak kesinlikle yasaktır.

- **Kenney UI Pack (CC0):**
  - License: Creative Commons CC0 (Public Domain)
  - Commercial use: Allowed
  - Attribution: Not required
  - AI Generated: No (Official Kenney confirmation: no generative AI used)
- **Kenney Generic Items (CC0):**
  - License: Creative Commons CC0 (Public Domain)
  - Commercial use: Allowed
  - Attribution: Not required
  - AI Generated: No explicit AI usage claim found on the asset page; license is CC0.


## Audit Update (2026-06-05)

- No new assets imported. Only procedural visuals and existing code are used.
