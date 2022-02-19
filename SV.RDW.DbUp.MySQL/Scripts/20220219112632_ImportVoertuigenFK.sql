START TRANSACTION;

ALTER TABLE `voertuigen` DROP FOREIGN KEY `FK_voertuigen_imports_importId`;

ALTER TABLE `voertuigen` MODIFY COLUMN `importId` int NULL;

ALTER TABLE `voertuigen` ADD CONSTRAINT `FK_voertuigen_imports_importId` FOREIGN KEY (`importId`) REFERENCES `imports` (`id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220219112632_ImportVoertuigenFK', '6.0.2');

COMMIT;

