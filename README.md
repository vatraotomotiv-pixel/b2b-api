# B2B E-Ticaret Sistemi

## 📋 Proje Özeti

B2B e-ticaret platformu - Excel'den ürün çekme, çoklu dil desteği, otomatik görsel yönetimi.

---

## 🎯 Özellikler

1. ✅ **Ürün Yönetimi**
   - Ürün görselleri
   - Ürün adı
   - Paket içi adet
   - Fiyat
   - Para birimi

2. ✅ **Excel Entegrasyonu**
   - Excel'den direkt ürün çekme
   - Otomatik import

3. ✅ **Görsel Yönetimi**
   - Ürün koduna göre otomatik görsel çekme
   - Seri/otomatik işlem

4. ✅ **Çoklu Dil Desteği**
   - Müşteri diline göre içerik
   - Dinamik dil değişimi

---

## 🛠️ Teknoloji Stack

- **Backend:** .NET 10.0 (C#)
- **Frontend:** React + TypeScript
- **Database:** MySQL (XAMPP)
- **Excel:** EPPlus veya ClosedXML

---

## 📁 Proje Yapısı

```
B2B_PROJECT/
├── Backend/
│   ├── B2B.API/              # Web API
│   ├── B2B.Core/             # Entities, Interfaces
│   └── B2B.Infrastructure/  # Services, Data Access
├── Frontend/
│   └── b2b-app/              # React App
└── Excel/
    └── products.xlsx          # Örnek Excel dosyası
```

---

## 🚀 Hızlı Başlangıç

### Backend
```powershell
cd Backend\B2B.API
dotnet run
```

### Frontend
```powershell
cd Frontend\b2b-app
npm install
npm start
```

---

**Geliştirme:** Başlangıç aşaması
