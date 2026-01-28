-- B2B Database - Tüm Tablolar
-- phpMyAdmin'de hedef veritabanını (örn. vatraoto_b2b_db) seçip Import edin.

-- Products Tablosu
CREATE TABLE IF NOT EXISTS `Products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductCode` varchar(50) NOT NULL,
  `Name` varchar(200) NOT NULL,
  `PackageQuantity` int NOT NULL DEFAULT '1',
  `Price` decimal(18,2) NOT NULL DEFAULT '0.00',
  `CurrencyCode` varchar(3) NOT NULL DEFAULT 'USD',
  `ImageUrl` varchar(500) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedAt` datetime(6) NOT NULL,
  `UpdatedAt` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Products_ProductCode` (`ProductCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ProductTranslations Tablosu
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

-- Customers Tablosu
CREATE TABLE IF NOT EXISTS `Customers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(200) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `LanguageCode` varchar(5) NOT NULL DEFAULT 'tr',
  `CurrencyCode` varchar(3) NOT NULL DEFAULT 'USD',
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedAt` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Customers_Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Örnek Veriler
INSERT INTO `Products` (`ProductCode`, `Name`, `PackageQuantity`, `Price`, `CurrencyCode`, `ImageUrl`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
('PROD001', 'Ürün 1', 12, 99.99, 'USD', '/images/products/PROD001.jpg', 1, NOW(), NOW()),
('PROD002', 'Ürün 2', 24, 149.50, 'EUR', '/images/products/PROD002.jpg', 1, NOW(), NOW()),
('PROD003', 'Ürün 3', 6, 75.00, 'TRY', '/images/products/PROD003.jpg', 1, NOW(), NOW())
ON DUPLICATE KEY UPDATE `UpdatedAt` = NOW();

INSERT INTO `ProductTranslations` (`ProductId`, `LanguageCode`, `Name`) VALUES
(1, 'tr', 'Ürün 1 - Türkçe'),
(1, 'en', 'Product 1 - English'),
(2, 'tr', 'Ürün 2 - Türkçe'),
(2, 'en', 'Product 2 - English'),
(3, 'tr', 'Ürün 3 - Türkçe'),
(3, 'en', 'Product 3 - English')
ON DUPLICATE KEY UPDATE `Name` = VALUES(`Name`);
