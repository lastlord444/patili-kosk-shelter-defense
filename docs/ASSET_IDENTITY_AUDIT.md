# ASSET_IDENTITY_AUDIT.md

Bu doküman, projede yer alan mevcut görsel varlıkların (art assets) Vampire Survivors klonu kimlik risklerini, lisans durumlarını ve Phase 2 kapsamında Kenney CC0 varlıklarıyla değiştirilme planını içerir.

## Varlık Kimlik Denetim Tablosu (Asset Identity Audit Table)

| Existing Asset Path | Current Purpose | Vampire/Clone Identity Risk | License Status | Replacement Priority | Proposed Replacement Source / Placeholder |
|---|---|---|---|---|---|
| `Assets/Sprites/Characters/MainCharacterBlue.png` | Oyuncu Karakteri (Mavi) | **HIGH** (Vampire Survivors klonu karakter tasarımı) | MIT / Unknown | **HIGH** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/cat_guardian_placeholder.png` (Kenney CC0 - Pixel Platformer) |
| `Assets/Sprites/Characters/MainCharacterGray.png` | Oyuncu Karakteri (Gri) | **HIGH** (Vampire Survivors klonu karakter tasarımı) | MIT / Unknown | **HIGH** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/cat_guardian_placeholder.png` |
| `Assets/Sprites/Characters/MainCharacterPurple.png` | Oyuncu Karakteri (Mor) | **HIGH** (Vampire Survivors klonu karakter tasarımı) | MIT / Unknown | **HIGH** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/cat_guardian_placeholder.png` |
| `Assets/Sprites/Characters/MainCharacterWhite.png` | Oyuncu Karakteri (Beyaz) | **HIGH** (Vampire Survivors klonu karakter tasarımı) | MIT / Unknown | **HIGH** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/cat_guardian_placeholder.png` |
| `Assets/Sprites/Monsters/Alien.png` | Düşman (Uzaylı) | **MEDIUM** (Base klon düşman tasarımı) | MIT / Unknown | **HIGH** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/shadow_enemy_placeholder.png` (Kenney CC0 - Pixel Platformer) |
| `Assets/Sprites/Monsters/Boss.png` | Boss Canavarı | **MEDIUM** (Base klon düşman tasarımı) | MIT / Unknown | **MEDIUM** | Gelecek fazlarda kedi temalı bir boss ile değiştirilecek |
| `Assets/Sprites/Coins/Coin1.png` | Altın (Küçük) | **LOW** (Standart altın) | MIT / Unknown | **MEDIUM** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/food_coin_placeholder.png` (Kenney CC0 - Pixel Platformer / Yiyecek) |
| `Assets/Sprites/Coins/Coin10.png` | Altın (Büyük) | **LOW** (Standart altın) | MIT / Unknown | **MEDIUM** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/food_coin_placeholder.png` |
| `Assets/Sprites/Gems/GemLightBlue.png` | Deneyim Kristali | **LOW** (Standart kristal) | MIT / Unknown | **MEDIUM** | `Assets/_PatiliKosk/Art/Placeholders/Kenney/exp_gem_placeholder.png` (Kenney CC0 - Pixel Platformer / Ayrı Görsel) |
| `Assets/Sprites/Kenney/Skull.png` | Kurukafa Görseli | **LOW** (Kenney CC0) | CC0 (Kenney) | **LOW** | Olduğu gibi kalacak (Lisanslı ve temiz) |
| `Assets/Sprites/Kenney/Magnet.png` | Mıknatıs Görseli | **LOW** (Kenney CC0) | CC0 (Kenney) | **LOW** | Olduğu gibi kalacak (Lisanslı ve temiz) |

## Planlanan Değişiklikler (Phase 2A)

> [!IMPORTANT]
> **Rollback Kararı (30.05.2026):** Kenney tekil placeholder uygulaması, tüm düşman çeşitliliğini (Alien, Melee, Boss vb.) tek bir görsel kimliğe indirgediği ve görsel kaliteyi düşürdüğü için **iptal edilmiş ve geri alınmıştır**. 
> - Oyuncu karakterleri, düşman canavarlar, coin ve deneyim kristali referansları orijinal/base haline geri döndürülmüştür.
> - `PatiliKoskPlaceholderApplier.cs` aracı projeden silinmiştir.
> - Kedi barınağı konseptine uygun özgün görsel kimlik çalışması gelecek aşamada doğrudan final özel varlıklarla yapılacaktır.

## Static Placeholder Risk (Statik Placeholder Riski)

> [!WARNING]
> Phase 2A kapsamında Kenney asset göçüyle birlikte oyuncu ve düşman yürüme sprite dizilimleri 4 kareden 1 kare statik görsele düşmüştür. Bu durum yürüme animasyonlarının tamamen kaybolmasına yol açmıştır.
> - **Geçici Çözüm (Procedural Motion):** Phase 2B kapsamında, karakterlerin cansız görünmesini engellemek için `SpriteAnimator.cs` içerisine programatik olarak sinüs dalgası tabanlı "Squash, Stretch, Bobbing & Wobble/Tilt" (yaylanma, büzülme ve dönme) hareketi eklenmiş ve sadece karakterlerde `enableProceduralMotion = true` olarak opt-in edilmiştir.
> - **Kısıtlar:** Bu procedural hareket gerçek bir yürüme animasyonu (walk animation) değildir ve görsel progression eksikliği ile zayıf animasyon kalitesi riski devam etmektedir.
> - **Kalıcı Çözüm:** Çok kareli (multi-frame) karakter/düşman sprite setlerinin entegre edilmesi veya final art pass yapılması gerekmektedir.
