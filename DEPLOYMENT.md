# B2B Deployment Rehberi

## 🌐 Domain
**Production URL:** `https://b2b.vatraotomotiv.com.tr`

---

## 📋 Deployment Öncesi Kontrol Listesi

### 1. Database Ayarları
- [ ] MySQL bağlantı bilgileri güncellendi
- [ ] Production database oluşturuldu
- [ ] Migration'lar çalıştırıldı

### 2. Hosting Ayarları
- [ ] .NET 10.0 Runtime yüklü
- [ ] MySQL/MariaDB erişimi var
- [ ] SSL sertifikası yapılandırıldı
- [ ] Domain DNS ayarları yapıldı

### 3. Dosya Yapısı
- [ ] `wwwroot/images/products/` klasörü oluşturuldu
- [ ] Görseller yüklendi
- [ ] Excel dosyaları hazır

---

## 🚀 Deployment Adımları

### Windows Hosting (Plesk/cPanel)

#### 1. Projeyi Publish Et
```powershell
cd "C:\Users\Burak\Deneme\Documents\MMORPG_Project\B2B_PROJECT\B2B.API"
dotnet publish -c Release -o ./publish
```

#### 2. Dosyaları FTP ile Yükle
- `publish/` klasöründeki tüm dosyaları hosting'e yükle
- Genellikle: `httpdocs/` veya `public_html/` klasörüne

#### 3. Database Bağlantısı
`appsettings.Production.json` dosyasında:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=b2b_db;User=DB_USER;Password=DB_PASSWORD;"
}
```

#### 4. Migration Çalıştır
Hosting panelinden veya SSH ile:
```bash
dotnet ef database update --project B2B.Infrastructure --startup-project B2B.API
```

#### 5. IIS Yapılandırması (Windows Hosting)
- Application Pool: .NET CLR Version = "No Managed Code"
- .NET Version = v10.0
- Start Mode = AlwaysRunning

---

### Linux Hosting

#### 1. Projeyi Publish Et
```bash
dotnet publish -c Release -o ./publish
```

#### 2. Systemd Service Oluştur
`/etc/systemd/system/b2b-api.service`:
```ini
[Unit]
Description=B2B API Service
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /var/www/b2b/B2B.API.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=b2b-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5000

[Install]
WantedBy=multi-user.target
```

#### 3. Nginx Reverse Proxy
`/etc/nginx/sites-available/b2b.vatraotomotiv.com.tr`:
```nginx
server {
    listen 80;
    server_name b2b.vatraotomotiv.com.tr;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name b2b.vatraotomotiv.com.tr;

    ssl_certificate /path/to/certificate.crt;
    ssl_certificate_key /path/to/private.key;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    location /images {
        alias /var/www/b2b/wwwroot/images;
    }
}
```

---

## 🔧 Production Yapılandırması

### appsettings.Production.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DB_HOST;Port=3306;Database=b2b_db;User=DB_USER;Password=DB_PASSWORD;"
  },
  "Domain": {
    "BaseUrl": "https://b2b.vatraotomotiv.com.tr"
  }
}
```

### Environment Variables
```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://localhost:5000
```

---

## ✅ Deployment Sonrası Test

1. **Health Check:**
   ```
   https://b2b.vatraotomotiv.com.tr/health
   ```

2. **API Test:**
   ```
   https://b2b.vatraotomotiv.com.tr/api/products
   ```

3. **Swagger (Development'ta):**
   ```
   https://b2b.vatraotomotiv.com.tr/swagger
   ```

4. **Görsel Test:**
   ```
   https://b2b.vatraotomotiv.com.tr/images/products/PROD001.jpg
   ```

---

## 🐛 Sorun Giderme

### Database Bağlantı Hatası
- MySQL port kontrolü (3306)
- Firewall ayarları
- Kullanıcı yetkileri

### 500 Internal Server Error
- Log dosyalarını kontrol et
- `ASPNETCORE_ENVIRONMENT=Production` ayarını kontrol et
- Database migration'ları kontrol et

### Görseller Yüklenmiyor
- `wwwroot/images/products/` klasörü var mı?
- Dosya izinleri doğru mu?
- Static files middleware aktif mi?

---

**Son Güncelleme:** Production deployment için hazır
