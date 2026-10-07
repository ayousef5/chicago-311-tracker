-- Schema for the Chicago 311 Service Request Tracker.
-- Exported from the local development database (structure only, no data).

CREATE DATABASE IF NOT EXISTS chicago_311;
USE chicago_311;

CREATE TABLE IF NOT EXISTS `service_requests` (
  `sr_number` varchar(50) NOT NULL,
  `sr_type` varchar(255) NOT NULL,
  `sr_short_code` varchar(50) DEFAULT NULL,
  `owner_department` varchar(255) DEFAULT NULL,
  `status` varchar(50) NOT NULL,
  `origin` varchar(100) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `last_modified_date` datetime DEFAULT NULL,
  `closed_date` datetime DEFAULT NULL,
  `street_address` varchar(255) DEFAULT NULL,
  `city` varchar(100) DEFAULT NULL,
  `state` varchar(50) DEFAULT NULL,
  `zip_code` varchar(20) DEFAULT NULL,
  `community_area` int DEFAULT NULL,
  `ward` int DEFAULT NULL,
  `latitude` decimal(10,7) DEFAULT NULL,
  `longitude` decimal(10,7) DEFAULT NULL,
  PRIMARY KEY (`sr_number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
