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

Bu aşamada projedeki tüm varlıkları silmeden, sadece ana oynanış döngüsünü doğrulayabileceğimiz minimum placeholder değişimleri yapılmıştır:
1. **Oyuncu Karakteri:** `MainCharacterBlue` ve diğer ana kedi karakter prefablara bağlanacak kedi görseli (`cat_guardian_placeholder.png`).
2. **Düşman Görseli:** Bir adet temel düşman sprite'ı yerine (`shadow_enemy_placeholder.png`).
3. **Yiyecek/Coin Görseli:** Toplanabilir ödül görseli yerine kedi maması/yiyecek temalı coin placeholder'ı (`food_coin_placeholder.png`).
4. **Deneyim Kristali:** Ayrı bir nesne olarak takip edilmesi için (`exp_gem_placeholder.png`).

