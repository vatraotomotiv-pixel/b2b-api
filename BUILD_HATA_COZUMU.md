# Build Hatası Çözümü

## 🔴 Sorun
**NuGet Bağlantı Hatası:**
```
Hedef makine etkin olarak reddettiğinden bağlantı kurulamadı. (127.0.0.1:9)
```

Bu hata, NuGet paketlerinin indirilememesi nedeniyle build'in başarısız olmasına neden oluyor.

---

## ✅ Çözümler

### 1. İnternet Bağlantısını Kontrol Et
- İnternet bağlantın aktif mi?
- VPN kullanıyorsan kapat ve tekrar dene

### 2. Proxy Ayarlarını Kontrol Et
Eğer proxy kullanıyorsan, NuGet.config dosyası oluştur:

**B2B_PROJECT klasöründe `NuGet.config` dosyası oluştur:**
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>
```

### 3. NuGet Cache'i Temizle
```powershell
cd "C:\Users\Burak\Deneme\Documents\MMORPG_Project\B2B_PROJECT"
dotnet nuget locals all --clear
```

### 4. Paketleri Manuel İndir (Son Çare)
Eğer yukarıdakiler işe yaramazsa, paketleri manuel olarak indirebilirsin:

1. NuGet.org'dan paketleri indir:
   - `Pomelo.EntityFrameworkCore.MySql.10.0.0.nupkg`
   - `EPPlus.7.5.2.nupkg`
   - `Microsoft.EntityFrameworkCore.10.0.0.nupkg`

2. Local NuGet source oluştur ve paketleri oraya koy

---

## 🚀 Hızlı Test

Build hatası çözüldükten sonra:

```powershell
cd "C:\Users\Burak\Deneme\Documents\MMORPG_Project\B2B_PROJECT\B2B.API"
dotnet build
```

Başarılı olursa:
```powershell
dotnet run
```

---

**Not:** Bu hata genellikle proxy/firewall veya internet bağlantı sorunlarından kaynaklanır.
