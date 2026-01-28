# B2B API’yi Yayına Alma (Render + cPanel MySQL)

Arayüz zaten **b2b.vatraotomotiv.com.tr**’de. API’yi Render’da çalıştırıp MySQL’e bağlamak için adımlar.

---

## 1. cPanel’de Remote MySQL aç

1. cPanel → **“Remote MySQL®”** / **“Uzak MySQL”** bölümüne gir.
2. **“Erişim Konakları” / “Access Hosts”** kısmında **“Ekle” / “Add Host”** de.
3. **%** yazıp ekle (Render’ın IP’leri değişebilir; % tüm IP’lere izin verir).  
   Hosting % kabul etmiyorsa Render IP’lerini eklemen gerekir (Render dokümanına bak).

---

## 2. MySQL bağlantı bilgisi

API’de kullanacağın connection string:

- **Sunucu:** Hosting panelinde yazan MySQL sunucu adı (çoğu zaman **localhost** veya **fr-collins.guzelhosting.com** gibi).
- **Veritabanı:** `vatraoto_b2b_db`
- **Kullanıcı:** `vatraoto_burak`
- **Şifre:** cPanel’de oluşturduğun şifre

Örnek (şifreyi kendi değerinle değiştir):

```
Server=SUNUCU_ADI;Port=3306;Database=vatraoto_b2b_db;User=vatraoto_burak;Password=ŞİFREN;
```

Sunucu adını bilmiyorsan: cPanel’de **“Remote MySQL”** veya **“MySQL Veritabanları”** sayfasında genelde yazar; veya hosting destekten sor.

---

## 3. Render’da API’yi deploy et

1. **https://render.com** → Giriş / Kayıt.
2. **Dashboard** → **“New +”** → **“Web Service”**.
3. Repo bağla: B2B_PROJECT’i içeren GitHub/GitLab repo’sunu seç (veya **“Build and deploy from a Git repository”** yerine **“Docker”** seçip aşağıdaki gibi ayarla).
4. Ayarlar:
   - **Name:** `b2b-api` (veya istediğin isim).
   - **Environment:** **Docker**.
   - **Dockerfile path:** `Dockerfile` (B2B_PROJECT kökündeki Dockerfile).
   - **Root Directory:** Repo’da B2B_PROJECT bir klasörse, o klasörü yaz (örn. `B2B_PROJECT`).
5. **Environment** sekmesinde ekle:
   - `ASPNETCORE_ENVIRONMENT` = `Production`
   - `ConnectionStrings__DefaultConnection` = yukarıdaki connection string (tek satır, tırnak yok).
6. **Create Web Service** de. İlk deploy birkaç dakika sürebilir.
7. Bittiğinde **URL** çıkar (örn. `https://b2b-api-xxxx.onrender.com`). Bunu kopyala.

---

## 4. Frontend’te API adresini yaz

1. Bilgisayarında aç:  
   `B2B_PROJECT\B2B.API\wwwroot\index.html`
2. Şu satırı bul:  
   `? 'https://B2B-API-RENDER-URL.onrender.com'`
3. **B2B-API-RENDER-URL** kısmını Render’dan aldığın gerçek URL ile değiştir (örn. `b2b-api-xxxx`).  
   Sonuç örnek:  
   `? 'https://b2b-api-xxxx.onrender.com'`
4. Dosyayı kaydet.

---

## 5. index.html’i tekrar cPanel’e at

1. cPanel → **Dosya Yöneticisi** → **public_html/b2b**.
2. Mevcut **index.html** üzerine yeni **index.html**’i yükle (üzerine yaz).

---

## 6. Test

Tarayıcıda **https://b2b.vatraotomotiv.com.tr** aç.  
Ürün listesi geliyorsa API bağlantısı çalışıyordur.

---

**Not:** Render ücretsiz planda bir süre istek gelmezse uykuya girer; ilk istek 30–60 sn sürebilir.
