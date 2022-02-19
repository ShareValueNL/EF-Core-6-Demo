START TRANSACTION;

ALTER TABLE `voertuigen` DROP FOREIGN KEY `FK_voertuigen_handelsbenamingen_handelsbenamingId`;

ALTER TABLE `voertuigen` DROP FOREIGN KEY `FK_voertuigen_voertuigSoorten_voertuigSoortId`;

ALTER TABLE `voertuigen` MODIFY COLUMN `voertuigSoortId` int NULL;

ALTER TABLE `voertuigen` MODIFY COLUMN `handelsbenamingId` int NULL;

ALTER TABLE `voertuigen` ADD CONSTRAINT `FK_voertuigen_handelsbenamingen_handelsbenamingId` FOREIGN KEY (`handelsbenamingId`) REFERENCES `handelsbenamingen` (`id`);

ALTER TABLE `voertuigen` ADD CONSTRAINT `FK_voertuigen_voertuigSoorten_voertuigSoortId` FOREIGN KEY (`voertuigSoortId`) REFERENCES `voertuigSoorten` (`id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220219125331_MeerNullFKs', '6.0.2');

COMMIT;

