# LICENSE_AUDIT.md

> Status: VERIFIED — Unity 6 (6000.3.16f1) inspection completed.

## Source Repository

- Repo: https://github.com/matthiasbroske/VampireSurvivorsClone
- Author: matthiasbroske

## Source License

- Type: **MIT License**
- File: `LICENSE` (preserved in this repo root)
- Copyright: Copyright (c) 2023 matthiasbroske
- Obligations: Preserve copyright notice and license text. ✔️ Done.

## Original LICENSE Preserved

- [x] LICENSE file imported and intact
- [x] README attribution added
- [x] LICENSE file manually verified (text diff against original matches 100%)

---

## Third-Party Art Credits

Orijinal projedeki tüm görsel assetler Vampire Survivors esintili spritelardan oluşmaktadır. Bu görsel varlıkların lisans durumları net olmamakla birlikte, oyun ticari bir ürüne dönüştürülmeden önce (Phase 2 kapsamında) **tamamen değiştirilecek**, kedi barınağı temalı orijinal çizimler ve lisanslı assetlerle değiştirilecektir.

| Asset | Source | License | Status |
|---|---|---|---|
| Monsters / Character Sprites | VampireSurvivorsClone | Unclear (VS IP) | ⚠️ To be replaced in Phase 2 |
| Kenney Sprites (Magnet, Skull, etc.) | Kenney.nl | CC0 (Public Domain) | ✔️ Safe for commercial use |
| Gold Treasure Icons | Bonsaiheldin (OpenGameArt) | CC0 / Custom | ⚠️ To be replaced in Phase 2 |

Detailed file-by-file licensing and risk types are documented in the master **[ASSET_REPLACEMENT_MATRIX.md](ASSET_REPLACEMENT_MATRIX.md)**.

---

## Third-Party Font Credits

Projede tespit edilen fontlar ve lisans durumları:

| Font | Source | License | Status |
|---|---|---|---|
| NotoSansMonoCJKtc-Regular.otf | Google Fonts | SIL Open Font License (OFL) | ✔️ Safe for commercial use |
| LiberationSans SDF | Unity / TMPro | SIL Open Font License (OFL) | ✔️ Safe for commercial use |

---

## Packages and Dependencies Audit

Unity 6'ya yükseltme sonrası `Packages/manifest.json` dosyasındaki bağımlılıklar incelenmiştir. Tüm paketler Unity Package Manager üzerinden sunulan resmi Unity modülleri ve kütüphaneleridir, Unity Software License Agreement (EULA) kapsamındadır. Projede harici veya lisans riski taşıyan üçüncü parti paket (örneğin uPools veya DOTween) bulunmamaktadır.

## Unknowns Resolved during Unity Inspection

- **Assets/ folder contents:** Görsel ve işitsel varlıkların tamamı Phase 2'de kedi barınağı konseptiyle sıfırdan değiştirilecektir. Mevcut assetler geçiş sürecinde sadece test amaçlı kullanılmaktadır.
- **Packages/ folder:** Resmi Unity paketleri dışında dış kütüphane bağımlılığı yoktur.
- **ProjectSettings/:** Unity sürümü 2021.3.21f1'den Unity 6 (6000.3.16f1) sürümüne yükseltilmiş ve ayarlar otomatik güncellenmiştir. Herhangi bir fikri mülkiyet (IP) riski bulunmamaktadır.

---

## Risk Summary

| ID | Risk | Level | Action | Status |
|---|---|---|---|---|
| R01 | MIT base used | LOW | Preserved LICENSE | ✔️ Resolved |
| R02 | Third-party art assets | HIGH | Replace all visual assets in Phase 2 | ⚠️ Open (To be resolved in Phase 2) |
| R03 | Vampire Survivors IP | HIGH | Remove all VS branding/identity in Phase 2 | ⚠️ Open (To be resolved in Phase 2) |
| R04 | Font licenses | LOW | Verified Noto Sans and LiberationSans are OFL | ✔️ Resolved |
| R05 | Audio licenses | MEDIUM | Audit and replace all sound effects in Phase 2 | ⚠️ Open (To be resolved in Phase 2) |
