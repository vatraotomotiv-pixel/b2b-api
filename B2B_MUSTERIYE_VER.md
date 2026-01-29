# B2B E-Ticaret – Müşteriye Verme Öncesi Kontrol

## Yapılanlar (Kod Tarafı)

- **API (Render):** SQLite kullanıyor, MySQL zorunlu değil. Render’da `RENDER=true` olduğu için otomatik SQLite + 3 örnek ürün seed ediliyor.
- **Frontend (b2b.vatraotomotiv.com.tr):** API adresi `https://b2b-api-mr0z.onrender.com` olarak ayarlı.
- **CORS:** Sadece `https://b2b.vatraotomotiv.com.tr` ve `http://b2b.vatraotomotiv.com.tr` izinli.

---

## Senin Yapacakların (Tek Seferlik)

### 1. Son kodu GitHub’a at

```text
cd C:\Users\Burak\Deneme\Documents\MMORPG_Project\B2B_PROJECT
git add .
git commit -m "B2B SQLite seed ve RENDER kontrolu"
git push origin main
```

### 2. Render deploy’u bekle

Render → **b2b-api** → **Events**. En üstteki deploy **yeşil** olana kadar bekle (2–3 dk).

### 3. cPanel’de index.html güncel mi kontrol et

- **Dosya Yöneticisi** → **public_html/b2b** → **index.html**.
- Bu dosya API adresini `https://b2b-api-mr0z.onrender.com` olarak kullanıyor olmalı.
- Emin değilsen: Bilgisayardaki `B2B_PROJECT\B2B.API\wwwroot\index.html` dosyasını tekrar yükle (üzerine yaz).

### 4. Canlı test

- **https://b2b.vatraotomotiv.com.tr** aç.
- **3 örnek ürün** (Ürün 1, Ürün 2, Ürün 3) listelenmeli.
- Dil **Türkçe** seçiliyse isimler “Ürün 1 - Türkçe” vb. olmalı.
- **Yenile** ile liste tekrar gelmeli.
- Excel sürükle-bırak alanı çalışıyor olmalı (import API’ye gider).

---

## Müşteriye Vereceğin Adres

- **Site:** https://b2b.vatraotomotiv.com.tr  
- İlk açılışta Render free instance uyuyorsa 30–60 sn sürebilir; sonra normal açılır.

## Sorun Olursa

- **Ürün yok:** https://b2b-api-mr0z.onrender.com/api/products aç; 3 ürünlü JSON gelmeli. Gelmiyorsa Render **Logs**’ta “B2B seed: 3 urun eklendi.” satırına bak.
- **API bağlantısı kurulamadı:** index.html’deki API adresi `https://b2b-api-mr0z.onrender.com` (mr**0**z = sıfır) olmalı; cPanel’e güncel dosyayı yükleyip **Ctrl+Shift+R** ile yenile.
