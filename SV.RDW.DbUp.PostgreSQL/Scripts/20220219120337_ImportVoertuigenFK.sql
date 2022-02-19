START TRANSACTION;

ALTER TABLE voertuigen DROP CONSTRAINT "FK_voertuigen_imports_importId";

ALTER TABLE voertuigen ALTER COLUMN "importId" DROP NOT NULL;

ALTER TABLE voertuigen ADD CONSTRAINT "FK_voertuigen_imports_importId" FOREIGN KEY ("importId") REFERENCES imports (id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220219120337_ImportVoertuigenFK', '6.0.2');

COMMIT;

