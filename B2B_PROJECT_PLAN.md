# B2B E-Ticaret Projesi - Plan

## 📋 Proje Özeti

Excel'den ürün çekme, çoklu dil desteği, otomatik görsel yönetimi olan B2B e-ticaret sistemi.

---

## 🎯 Özellikler

### 1. Ürün Yönetimi
- ✅ Ürün görselleri (ürün koduna göre otomatik)
- ✅ Ürün adı
- ✅ Paket içi adet
- ✅ Fiyat
- ✅ Para birimi

### 2. Excel Entegrasyonu
- ✅ Excel'den direkt ürün import
- ✅ Otomatik görsel eşleştirme (ürün kodu → görsel)

### 3. Çoklu Dil Desteği
- ✅ Müşteri diline göre içerik
- ✅ Dinamik dil değişimi

---

## 🗄️ Database Yapısı

### Products Tablosu
- Id (PK)
- ProductCode (Ürün kodu - görsel için)
- Name (Ürün adı)
- PackageQuantity (Paket içi adet)
- Price (Fiyat)
- CurrencyCode (Para birimi)
- ImageUrl (Görsel URL)
- IsActive
- CreatedAt, UpdatedAt

### ProductTranslations Tablosu
- Id (PK)
- ProductId (FK)
- LanguageCode (tr, en, de, vb.)
- Name (Çevrilmiş ad)

### Customers Tablosu
- Id (PK)
- CompanyName
- Email
- LanguageCode (Müşteri dili)
- CurrencyCode (Müşteri para birimi)
- IsActive
- CreatedAt

---

## 📁 Excel Formatı

Excel dosyasında şu kolonlar olmalı:
- ProductCode (Ürün kodu)
- Name (Ürün adı)
- PackageQuantity (Paket içi adet)
- Price (Fiyat)
- CurrencyCode (Para birimi)

---

## 🖼️ Görsel Yönetimi

Görseller şu formatta olmalı:
- Klasör: `wwwroot/images/products/`
- Dosya adı: `{ProductCode}.jpg` veya `{ProductCode}.png`
- Örnek: `PROD001.jpg`, `ABC123.png`

---

## 🚀 API Endpoints

- `GET /api/products` - Tüm ürünler (dil ve para birimine göre)
- `GET /api/products/{id}` - Tek ürün
- `POST /api/products/import` - Excel'den import
- `GET /api/products/search?q={query}` - Ürün arama

---

## 📦 Teknoloji Stack

- **Backend:** .NET 10.0 (C#)
- **Frontend:** React + TypeScript
- **Database:** MySQL (XAMPP)
- **Excel:** EPPlus veya ClosedXML
- **Görsel:** wwwroot/images/products/

---

**Durum:** Proje yapısı oluşturuldu, entity'ler hazırlanıyor.
