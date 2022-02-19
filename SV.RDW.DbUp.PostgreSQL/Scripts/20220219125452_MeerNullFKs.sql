START TRANSACTION;

ALTER TABLE voertuigen DROP CONSTRAINT "FK_voertuigen_handelsbenamingen_handelsbenamingId";

ALTER TABLE voertuigen DROP CONSTRAINT "FK_voertuigen_voertuigSoorten_voertuigSoortId";

ALTER TABLE voertuigen ALTER COLUMN "voertuigSoortId" DROP NOT NULL;

ALTER TABLE voertuigen ALTER COLUMN "handelsbenamingId" DROP NOT NULL;

ALTER TABLE voertuigen ADD CONSTRAINT "FK_voertuigen_handelsbenamingen_handelsbenamingId" FOREIGN KEY ("handelsbenamingId") REFERENCES handelsbenamingen (id);

ALTER TABLE voertuigen ADD CONSTRAINT "FK_voertuigen_voertuigSoorten_voertuigSoortId" FOREIGN KEY ("voertuigSoortId") REFERENCES "voertuigSoorten" (id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220219125452_MeerNullFKs', '6.0.2');

COMMIT;

