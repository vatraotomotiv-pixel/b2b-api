# Visual Studio'da Migration Yapma

## Database Oluşturuldu ✅
`b2b_db` database'i oluşturuldu.

---

## Şimdi Tabloları Oluştur

### Yöntem 1: Package Manager Console (EN KOLAY)

1. **Visual Studio'da:**
   - Üst menüden: **Araçlar → NuGet Paket Yöneticisi → Paket Yöneticisi Konsolu**
   - VEYA: **Tools → NuGet Package Manager → Package Manager Console**

2. **Alttaki konsola şu komutları yaz:**

```powershell
# Migration oluştur
Add-Migration InitialCreate -Project B2B.Infrastructure -StartupProject B2B.API

# Database'e uygula
Update-Database -Project B2B.Infrastructure -StartupProject B2B.API
```

---

### Yöntem 2: Manuel SQL (Hızlı)

1. **phpMyAdmin'i aç:**
   - Tarayıcıda: `http://localhost/phpmyadmin`
   - `b2b_db` database'ini seç
   - SQL sekmesine tıkla

2. **Şu SQL'i çalıştır:**

```sql
CREATE TABLE IF NOT EXISTS `Products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductCode` varchar(50) NOT NULL,
  `Name` varchar(200) NOT NULL,
  `PackageQuantity` int NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `CurrencyCode` varchar(3) NOT NULL,
  `ImageUrl` varchar(500) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedAt` datetime(6) NOT NULL,
  `UpdatedAt` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Products_ProductCode` (`ProductCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `ProductTranslations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductId` int NOT NULL,
  `LanguageCode` varchar(5) NOT NULL,
  `Name` varchar(200) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ProductTranslations_ProductId_LanguageCode` (`ProductId`, `LanguageCode`),
  KEY `IX_ProductTranslations_ProductId` (`ProductId`),
  CONSTRAINT `FK_ProductTranslations_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Customers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(200) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `LanguageCode` varchar(5) NOT NULL,
  `CurrencyCode` varchar(3) NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedAt` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Customers_Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

---

## Sonraki Adım

Tablolar oluşturulduktan sonra:
- Visual Studio'da `F5` tuşuna bas
- Backend çalışacak!

---

**Öneri:** Yöntem 2 (Manuel SQL) daha hızlı ve garantili.
