# B2B Backend - Tarayıcıda Açılacak Adresler

## Backend çalışıyorsa şu adresleri dene

Visual Studio'dan F5 ile başlattıysan **port 5069** kullanılıyor:

| Sayfa | URL |
|-------|-----|
| **OpenAPI (API dokümanı)** | http://localhost:5069/openapi/v1.json |
| **Health Check** | http://localhost:5069/health |
| **Ürünler API** | http://localhost:5069/api/products |

---

## HTTPS kullanıyorsan (yeşil kilit)

| Sayfa | URL |
|-------|-----|
| **OpenAPI** | https://localhost:7278/openapi/v1.json |
| **Health Check** | https://localhost:7278/health |
| **Ürünler API** | https://localhost:7278/api/products |

---

## Kontrol listesi

1. **Backend çalışıyor mu?**  
   Visual Studio'da F5 bastıktan sonra altta "Now listening on: http://localhost:5069" yazıyor mu?

2. **Database hazır mı?**  
   `b2b_db` oluşturuldu ve tablolar eklendi mi? (phpMyAdmin'de TABLOLARI_OLUSTUR.sql çalıştırıldı mı?)

3. **Doğru adres**  
   Tarayıcıda **http://localhost:5069/health** adresini aç.  
   `{"status":"healthy",...}` dönüyorsa backend çalışıyor.

---

## Hâlâ açılmıyorsa

- Backend’i durdur (Shift+F5), tekrar F5 ile başlat.
- Tarayıcıda doğrudan yaz: **http://localhost:5069/health**
- Firewall / antivirüs localhost’u engelliyor olabilir; geçici kapatıp dene.
