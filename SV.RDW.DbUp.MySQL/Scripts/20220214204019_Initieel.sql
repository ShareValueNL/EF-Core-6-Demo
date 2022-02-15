CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `imports` (
    `id` int NOT NULL AUTO_INCREMENT,
    `eersteToelatingDatum` DATE NOT NULL,
    `totaalImport` int NOT NULL,
    `importSeconden` decimal(65,30) NOT NULL,
    CONSTRAINT `PK_imports` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `merken` (
    `id` int NOT NULL AUTO_INCREMENT,
    `naam` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_merken` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `voertuigSoorten` (
    `id` int NOT NULL AUTO_INCREMENT,
    `naam` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_voertuigSoorten` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `handelsbenamingen` (
    `id` int NOT NULL AUTO_INCREMENT,
    `naam` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `merkId` int NOT NULL,
    CONSTRAINT `PK_handelsbenamingen` PRIMARY KEY (`id`),
    CONSTRAINT `FK_handelsbenamingen_merken_merkId` FOREIGN KEY (`merkId`) REFERENCES `merken` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `voertuigen` (
    `id` int NOT NULL AUTO_INCREMENT,
    `kenteken` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
    `voertuigSoortId` int NOT NULL,
    `merkId` int NOT NULL,
    `handelsbenamingId` int NOT NULL,
    `vervalDatumAPK` DATE NULL,
    `tenaamstelling` DATE NOT NULL,
    `eersteToelating` DATE NOT NULL,
    `inrichting` varchar(50) CHARACTER SET utf8mb4 NULL,
    `kleur` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT 'Hier staat de kleur.',
    `massaLedig` decimal(65,30) NULL,
    `importId` int NOT NULL,
    CONSTRAINT `PK_voertuigen` PRIMARY KEY (`id`),
    CONSTRAINT `FK_voertuigen_handelsbenamingen_handelsbenamingId` FOREIGN KEY (`handelsbenamingId`) REFERENCES `handelsbenamingen` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_voertuigen_imports_importId` FOREIGN KEY (`importId`) REFERENCES `imports` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_voertuigen_merken_merkId` FOREIGN KEY (`merkId`) REFERENCES `merken` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_voertuigen_voertuigSoorten_voertuigSoortId` FOREIGN KEY (`voertuigSoortId`) REFERENCES `voertuigSoorten` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_handelsbenamingen_merkId` ON `handelsbenamingen` (`merkId`);

CREATE INDEX `IX_voertuigen_handelsbenamingId` ON `voertuigen` (`handelsbenamingId`);

CREATE INDEX `IX_voertuigen_importId` ON `voertuigen` (`importId`);

CREATE INDEX `IX_voertuigen_merkId` ON `voertuigen` (`merkId`);

CREATE INDEX `IX_voertuigen_voertuigSoortId` ON `voertuigen` (`voertuigSoortId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220214204019_Initieel', '6.0.2');

COMMIT;

