-- B2B Database Tabloları
-- phpMyAdmin'de b2b_db database'ini seç ve bu SQL'i çalıştır

USE b2b_db;

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
