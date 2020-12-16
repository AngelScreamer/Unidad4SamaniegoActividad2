-- MySQL dump 10.13  Distrib 8.0.17, for Win64 (x86_64)
--
-- Host: localhost    Database: rolessamaniego
-- ------------------------------------------------------
-- Server version	8.0.17

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
-- Table structure for table `alumno`
--

DROP TABLE IF EXISTS `alumno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `alumno` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NumeroDeControl` varchar(8) NOT NULL,
  `Nombre` varchar(30) NOT NULL,
  `IdDocente` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_IdDocente` (`IdDocente`),
  CONSTRAINT `fk_IdDocente` FOREIGN KEY (`IdDocente`) REFERENCES `docente` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alumno`
--

LOCK TABLES `alumno` WRITE;
/*!40000 ALTER TABLE `alumno` DISABLE KEYS */;
INSERT INTO `alumno` VALUES (1,'171G0634','Emma Cantú De La Cruz',1),(2,'171G0635','Ali Eduardo Rojas Delgado',2),(10,'171G0637','David Ríos Gonzalez',4),(11,'171G0638','Javier Guzman Cortes',3),(12,'171G0633','Angel Samaniego Hernández',1),(14,'171G0639','Nathaly Palacios',2),(15,'171G0640','Luis Samaniego Cepeda',5);
/*!40000 ALTER TABLE `alumno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `director`
--

DROP TABLE IF EXISTS `director`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `director` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Clave` int(11) NOT NULL,
  `Nombre` varchar(30) NOT NULL,
  `Contrasena` varchar(200) NOT NULL COMMENT 'root',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `director`
--

LOCK TABLES `director` WRITE;
/*!40000 ALTER TABLE `director` DISABLE KEYS */;
INSERT INTO `director` VALUES (1,1234,'Angel Samaniego Hernández','4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2');
/*!40000 ALTER TABLE `director` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `docente`
--

DROP TABLE IF EXISTS `docente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `docente` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Clave` int(11) NOT NULL,
  `Nombre` varchar(30) NOT NULL,
  `Contrasena` varchar(200) NOT NULL,
  `Activo` bit(1) DEFAULT b'1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `docente`
--

LOCK TABLES `docente` WRITE;
/*!40000 ALTER TABLE `docente` DISABLE KEYS */;
INSERT INTO `docente` VALUES (1,7777,'Ernestina Leija Ramirez','4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2',_binary ''),(2,1111,'Adriana Hernández Ramírez','4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2',_binary ''),(3,2222,'Ranulfo Borrego González','4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2',_binary ''),(4,3333,'Juan Carlos Sifuentes Garcia','4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2',_binary ''),(5,5555,'Armando González Ruiz','4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2',_binary '');
/*!40000 ALTER TABLE `docente` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-12-16  5:08:24
