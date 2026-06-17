-- MySQL dump 10.13  Distrib 8.0.45, for Win64 (x86_64)
--
-- Host: localhost    Database: DemoExamDB
-- ------------------------------------------------------
-- Server version	8.0.45

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `companies`
--

DROP TABLE IF EXISTS `companies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companies` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ExternalId` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `INN` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Address` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Phone` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IsSalesman` tinyint(1) DEFAULT '0',
  `IsBuyer` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `ExternalId` (`ExternalId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companies`
--

LOCK TABLES `companies` WRITE;
/*!40000 ALTER TABLE `companies` DISABLE KEYS */;
INSERT INTO `companies` VALUES (1,'000000001','╨Ю╨Ю╨Ю \"╨Я╨╛╤Б╤В╨░╨▓╨║╨░\"','','╨│.╨Я╤П╤В╨╕╨│╨╛╤А╤Б╨║','+79198634592',1,1),(2,'000000002','╨Ю╨Ю╨Ю \"╨Ъ╨╕╨╜╨╛╤В╨╡╨░╤В╤А ╨Ъ╨▓╨░╨╜╤В\"','26320045123','╨│. ╨Ц╨╡╨╗╨╡╨╖╨╜╨╛╨▓╨╛╨┤╤Б╨║, ╤Г╨╗. ╨Ь╨╕╤А╨░, 123','+79884581555',1,0),(3,'000000008','╨Ю╨Ю╨Ю \"╨Э╨╛╨▓╤Л╨╣ JDTO\"','26320045111','╨│. ╨Ц╨╡╨╗╨╡╨╖╨╜╨╛╨▓╨╛╨┤╤Б╤Г','+79884581555',1,0),(4,'000000003','╨Ю╨Ю╨Ю \"╨а╨╛╨╝╨░╤И╨║╨░\"','4140784214','╨│. ╨Ю╨╝╤Б╨║, ╤Г╨╗. ╨б╤В╤А╨╛╨╕╤В╨╡╨╗╨╡╨╣, 294','+79882584546',0,1),(5,'000000009','╨Ю╨Ю╨Ю \"╨Ш╨┐╨┐╨╛╨┤╤А╨╛╨╝\"','5874045632','╨│. ╨г╤Д╨░, ╤Г╨╗. ╨Э╨░╨▒╨╡╤А╨╡╨╢╨╜╨░╤П,  37','+79627486389',1,1),(6,'000000010','╨Ю╨Ю╨Ю \"╨Р╤Б╤Б╨╛╨╗╤М\"','2629011278','╨│. ╨Ъ╨░╨╗╤Г╨│╨░, ╤Г╨╗. ╨Я╤Г╤И╨║╨╕╨╜╨░, 94','+79184572398',0,1);
/*!40000 ALTER TABLE `companies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `materials`
--

DROP TABLE IF EXISTS `materials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `materials` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Unit` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materials`
--

LOCK TABLES `materials` WRITE;
/*!40000 ALTER TABLE `materials` DISABLE KEYS */;
INSERT INTO `materials` VALUES (1,'╨Ч╨░╨║╨▓╨░╤Б╨║╨░ ╤Б╨╝╨╡╤В╨░╨╜╨╜╨░╤П',45.00,'╤И╤В'),(2,'╨Ш╨╖╤О╨╝',150.00,'╨║╨│'),(3,'╨Ь╨░╤Б╨╗╨╛ ╤Б╨╗╨╕╨▓╨╛╤З╨╜╨╛╨╡',124.00,'╨║╨│'),(4,'╨Ь╨╛╨╗╨╛╨║╨╛ ╨╜╨╛╤А╨╝╨░╨╗╨╕╨╖╨╛╨▓╨░╨╜╨╜╨╛╨╡',34.00,'╨║╨│'),(5,'╨Ь╤Г╨║╨░',220.00,'╨║╨│'),(6,'╨б╨╛╨┤╨░',60.00,'╤И╤В'),(7,'╨п╨╣╤Ж╨░',80.00,'╤И╤В');
/*!40000 ALTER TABLE `materials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrderId` int NOT NULL,
  `ProductId` int NOT NULL,
  `Quantity` int NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Sum` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `OrderId` (`OrderId`),
  KEY `ProductId` (`ProductId`),
  CONSTRAINT `orderitems_ibfk_1` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `orderitems_ibfk_2` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
INSERT INTO `orderitems` VALUES (1,1,4,8,45.00,360.00),(2,1,6,7,47.00,329.00);
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrderNumber` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `OrderDate` date NOT NULL,
  `BuyerId` int NOT NULL,
  `ShopId` int NOT NULL,
  `Total` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `BuyerId` (`BuyerId`),
  KEY `ShopId` (`ShopId`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`BuyerId`) REFERENCES `companies` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`ShopId`) REFERENCES `companies` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,'╨Ч╨░╨║╨░╨╖ тДЦ 3','2025-06-07',4,1,689.00);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `production`
--

DROP TABLE IF EXISTS `production`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `production` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductionNumber` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductionDate` date NOT NULL,
  `ProductId` int NOT NULL,
  `CompanyId` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `ProductId` (`ProductId`),
  KEY `CompanyId` (`CompanyId`),
  CONSTRAINT `production_ibfk_1` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `production_ibfk_2` FOREIGN KEY (`CompanyId`) REFERENCES `companies` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `production`
--

LOCK TABLES `production` WRITE;
/*!40000 ALTER TABLE `production` DISABLE KEYS */;
INSERT INTO `production` VALUES (1,'╨Я╤А╨╛╨╕╨╖╨▓╨╛╨┤╤Б╤В╨▓╨╛ тДЦ 1','2025-06-09',2,1,1);
/*!40000 ALTER TABLE `production` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productmaterials`
--

DROP TABLE IF EXISTS `productmaterials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productmaterials` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProductId` int NOT NULL,
  `MaterialId` int NOT NULL,
  `Quantity` decimal(10,3) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `ProductId` (`ProductId`),
  KEY `MaterialId` (`MaterialId`),
  CONSTRAINT `productmaterials_ibfk_1` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `productmaterials_ibfk_2` FOREIGN KEY (`MaterialId`) REFERENCES `materials` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productmaterials`
--

LOCK TABLES `productmaterials` WRITE;
/*!40000 ALTER TABLE `productmaterials` DISABLE KEYS */;
INSERT INTO `productmaterials` VALUES (1,2,2,0.020),(2,2,3,0.020),(3,2,4,0.150),(4,2,5,0.100),(5,2,6,0.005),(6,2,7,0.250);
/*!40000 ALTER TABLE `productmaterials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Unit` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'╨С╨░╤В╨╛╨╜ ╨╜╨░╤А╨╡╨╖╨╜╨╛╨╣',45.00,'╤И╤В'),(2,'╨С╤Г╨╗╨╛╤З╨║╨░ ╤Б ╨╕╨╖╤О╨╝╨╛╨╝',35.00,'╤И╤В'),(3,'╨С╤Г╨╗╨╛╤З╨║╨░ ╤Б ╨║╨╛╤А╨╕╤Ж╨╡╨╣',35.00,'╤И╤В'),(4,'╨е╨╗╨╡╨▒ ╨▒╨╡╨╗╤Л╨╣ 1 ╨║╨│.',42.00,'╤И╤В'),(5,'╨е╨╗╨╡╨▒ ╨Ъ╤А╨╛╨╜╤И╤В╨░╨┤╤В╤Б╨║╨╕╨╣ 1 ╨║╨│.',120.00,'╤И╤В'),(6,'╨е╨╗╨╡╨▒ ╤А╨╢╨░╨╜╨╛╨╣ 800╨│.',47.00,'╤И╤В');
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Password` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL DEFAULT '0',
  `IsBlocked` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Login` (`Login`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin','admin',1,0),(2,'user','user',0,0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'DemoExamDB'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-17 20:52:09
