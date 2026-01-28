# Domain Yapılandırması

## 🌐 Domain Bilgileri
**Production URL:** `https://b2b.vatraotomotiv.com.tr`

---

## 📋 DNS Ayarları

### A Record (IPv4)
```
b2b.vatraotomotiv.com.tr → [Hosting IP Adresi]
```

### AAAA Record (IPv6) - Opsiyonel
```
b2b.vatraotomotiv.com.tr → [IPv6 Adresi]
```

### CNAME - Alternatif
```
b2b → vatraotomotiv.com.tr
```

---

## 🔒 SSL Sertifikası

### Let's Encrypt (Ücretsiz)
```bash
certbot --nginx -d b2b.vatraotomotiv.com.tr
```

### cPanel/Plesk
- SSL/TLS bölümünden sertifika oluştur
- Let's Encrypt veya ücretli sertifika seç
- Domain'e otomatik yükle

---

## ⚙️ Hosting Yapılandırması

### 1. Application Pool (IIS)
- **.NET CLR Version:** No Managed Code
- **Managed Pipeline Mode:** Integrated
- **Start Mode:** AlwaysRunning

### 2. Site Ayarları
- **Binding:** `b2b.vatraotomotiv.com.tr`
- **Port:** 443 (HTTPS), 80 (HTTP redirect)
- **SSL Certificate:** Yüklü sertifika

### 3. .NET Runtime
- **Version:** .NET 10.0
- **Path:** `/usr/bin/dotnet` (Linux) veya otomatik (Windows)

---

## 🔧 CORS Ayarları

Production'da sadece domain'e izin veriliyor:
- ✅ `https://b2b.vatraotomotiv.com.tr`
- ✅ `http://b2b.vatraotomotiv.com.tr` (redirect için)

Development'ta tüm origin'lere izin var.

---

## 📁 Dosya Yapısı (Hosting)

```
/
├── B2B.API.dll
├── B2B.API.exe (Windows)
├── appsettings.Production.json
├── web.config (IIS için)
├── wwwroot/
│   └── images/
│       └── products/
│           ├── PROD001.jpg
│           ├── PROD002.jpg
│           └── ...
└── [Diğer DLL'ler]
```

---

## ✅ Test Checklist

- [ ] DNS yayıldı mı? (`ping b2b.vatraotomotiv.com.tr`)
- [ ] SSL aktif mi? (`https://b2b.vatraotomotiv.com.tr`)
- [ ] API çalışıyor mu? (`/api/products`)
- [ ] Görseller yükleniyor mu? (`/images/products/`)
- [ ] CORS çalışıyor mu? (Frontend'den test)

---

**Sonraki Adım:** Hosting'e deploy et ve test et.
