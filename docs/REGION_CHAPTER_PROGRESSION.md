# REGION/CHAPTER PROGRESSION CONTRACT

## 1. Amaç
Patili Köşk Shelter Defense için bölge/chapter ilerleme modelini dokümana kilitlemek ve ürün/tasarım sözleşmesi ile repo handoff süreçlerini standardize etmek.

## 2. Non-goals / MVP Dışı Şeyler
- Gerçek zamanlı 1 hafta sistemi (Real-time week system).
- Pet collection.
- Full shelter simülasyonu.
- City builder mekanikleri.
- Complex AI.
- MVP'de IAP, elmas, reklam, analytics, cloud save, leaderboard.

## 3. Core Progression Contract
Bölge/chapter sistemi tamamen oyun içi görevler (in-game missions) üzerinden çalışacaktır. İlerleme, "Rescue Progress" adı verilen bir birimle ölçülecek ve her görev oyuncuyu hedef kurtarma miktarına yaklaştıracaktır.

## 4. Region Formatı
Her bölge (Region) 5–7 kısa mobil görevden oluşur. Her bölge belirli bir hayvan/tema etrafında tasarlanır.

## 5. Mission Session Length Hedefi
Oyun süreleri "kısa mobil oturum" hedefine uygun olacak, oyuncunun toplu taşıma, kısa aralar gibi sürelerde bir görevi tamamlayabilmesi hedeflenecektir.

## 6. Rescue Progress Modeli
Ana ilerleme mekaniği Rescue Progress'tir. Görev başına toplanan progress puanları, bölge hedefine ulaştığında o bölge tamamlanmış sayılır. Ana kurtarma görevleri veya ilerlemeler kesinlikle premium para birimi (elmas vb.) ile kilitlenmeyecektir.

## 7. Bölge Tamamlama Modeli
Rescue Progress barı dolduğunda, bölge için bir "Rescue Final" görevi veya narrative conclusion (örn. kedi kurtarma kartı/animasyonu) tetiklenir ve bir sonraki bölgenin kilidi açılır.

## 8. Chapter 1 Vertical Slice
**Bölge Adı:** Patili Köşk Bahçesi
**Tema:** Kedi kolonisini koruma
**Görev Sayısı:** 5
**Progress Hedefi:** 100 Rescue Progress
**Görev Başı Progress:** 15–25
**Final:** Rescue Final / kedi kurtarma kartı
**Gameplay Hedefi:**
- Shelter haritanın merkezindedir.
- Düşmanlar shelter'a baskı yapar.
- Oyuncu serbestçe hareket eder, otomatik saldırır, coin toplar.
- Coin ile shelter, fence gibi basit upgredeler (run içi veya kalıcı, meta dizayna bağlı) yapılır.

## 9. Future Regions
Chapter 1 sonrasında eklenecek olası bölgeler:
- Köpek Barınağı
- Kuş Cenneti
- Orman Kenarı
- Sahil Kasabası

## 10. Upgrade Kategorileri
Oyun içi toplanan kaynaklarla veya ilerlemeyle yapılabilecek shelter upgrade'leri:
- **Shelter HP:** Ana üssün dayanıklılığını artırır.
- **Fence:** İlk hasarı emen, shelter'a gelen düşmanları yavaşlatan/durduran ekstra savunma katmanı (first damage buffer).
- **Feeding Station:** Düşmanlardan veya dalgalardan düşen coin/kaynak miktarını artırır (coin-drop bonus).
- **Volunteer Post:** Shelter etrafında hafif bir savunma desteği sağlar (light defensive assist).

## 11. Monetization Boundary
- **MVP:** IAP (In-App Purchase), elmas veya reklam kesinlikle yoktur.
- **Future Design:** Elmaslar yalnızca oyunu hızlandırma ve oyuncuya konfor sağlama (acceleration/comfort) amacıyla kullanılabilir.
- **Kesin Sınırlar:** Gacha, lootbox mekanikleri ve paywall yoktur. Rescue (kurtarma) görevleri asla elmasla kilitlenmez.

## 12. Scope Guard
Bu sözleşme ile şu tür feature-creep unsurları engellenmiştir:
- City builder mekanikleri YOKTUR.
- Full shelter simulation YOKTUR.
- Pet collection (tam teşekküllü) YOKTUR.
- Gerçek zamanlı hafta ilerlemesi YOKTUR.
- Online/Event sistemi YOKTUR.

## 13. Implementation Implication
Geliştirme sırasında aşağıdaki sıra izlenecektir:
1. Önce veri sözleşmesi (Data contract)
2. Sonra kayıt/ilerleme sistemi (Save/progress)
3. Sonra Shelter entitesinin oyuna aktarılması (Shelter entity)
4. En son UI/ilerleme ekranlarının (UI/progress screen) tasarlanıp kodlanması.

## 14. Acceptance Criteria
- Bölge/görev sistemi koda ve UI'a aktarılırken yukarıdaki kurallara sadık kalınması.
- Rescue progress barının çalışması ve chapter 1 vertical slice'ın baştan sona oynanabilir (playtest edilebilir) olması.
- Asset ve feature değişikliklerinin bu progression contract dışına çıkmaması.
