-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: mynance
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `payments`
--

DROP TABLE IF EXISTS `payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payments` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(16) NOT NULL,
  `budget_id` int NOT NULL,
  `assigned_budget_id` int NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `date_time` bigint NOT NULL DEFAULT '0',
  `outgoing` tinyint(1) NOT NULL DEFAULT '0',
  `note` varchar(255) DEFAULT '',
  PRIMARY KEY (`id`),
  KEY `username` (`username`),
  KEY `budget_id` (`budget_id`),
  KEY `assigned_budget_id` (`assigned_budget_id`),
  CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`username`) REFERENCES `users` (`username`),
  CONSTRAINT `payments_ibfk_2` FOREIGN KEY (`budget_id`) REFERENCES `budgets` (`id`),
  CONSTRAINT `payments_ibfk_3` FOREIGN KEY (`assigned_budget_id`) REFERENCES `assigned_budgets` (`id`),
  CONSTRAINT `payments_chk_1` CHECK ((`amount` > 0.00))
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payments`
--

LOCK TABLES `payments` WRITE;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
INSERT INTO `payments` VALUES (1,'ognjen',1,1,250.00,1733698199,0,'Stipendija'),(3,'ognjen',1,1,25.00,1733699199,1,'Sturja'),(4,'ognjen',1,1,25.00,1733700240,1,'Note'),(5,'ognjen',1,1,25.00,1733700241,1,'Note'),(6,'ognjen',1,2,25.00,1733700243,1,'Note'),(7,'ognjen',1,3,25.00,1733700245,1,'Note'),(8,'ognjen',1,4,25.00,1733700247,1,'Note'),(9,'ognjen',1,5,25.00,1733700249,1,'Note'),(10,'ognjen',1,1,15.00,1733702209,1,'Note'),(11,'ognjen',1,1,20.00,1733703779,0,'naso na trotoaru'),(12,'ognjen',1,5,5.00,1733703918,1,'kava');
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-09  1:32:26
