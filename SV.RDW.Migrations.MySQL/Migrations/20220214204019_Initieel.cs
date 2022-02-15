using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SV.RDW.Migrations.MySQL.Migrations
{
    public partial class Initieel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "imports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    eersteToelatingDatum = table.Column<DateTime>(type: "DATE", nullable: false),
                    totaalImport = table.Column<int>(type: "int", nullable: false),
                    importSeconden = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imports", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "merken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naam = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merken", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "voertuigSoorten",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naam = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voertuigSoorten", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "handelsbenamingen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naam = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    merkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_handelsbenamingen", x => x.id);
                    table.ForeignKey(
                        name: "FK_handelsbenamingen_merken_merkId",
                        column: x => x.merkId,
                        principalTable: "merken",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "voertuigen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kenteken = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    voertuigSoortId = table.Column<int>(type: "int", nullable: false),
                    merkId = table.Column<int>(type: "int", nullable: false),
                    handelsbenamingId = table.Column<int>(type: "int", nullable: false),
                    vervalDatumAPK = table.Column<DateTime>(type: "DATE", nullable: true),
                    tenaamstelling = table.Column<DateTime>(type: "DATE", nullable: false),
                    eersteToelating = table.Column<DateTime>(type: "DATE", nullable: false),
                    inrichting = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kleur = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Hier staat de kleur.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    massaLedig = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    importId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voertuigen", x => x.id);
                    table.ForeignKey(
                        name: "FK_voertuigen_handelsbenamingen_handelsbenamingId",
                        column: x => x.handelsbenamingId,
                        principalTable: "handelsbenamingen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_voertuigen_imports_importId",
                        column: x => x.importId,
                        principalTable: "imports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_voertuigen_merken_merkId",
                        column: x => x.merkId,
                        principalTable: "merken",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_voertuigen_voertuigSoorten_voertuigSoortId",
                        column: x => x.voertuigSoortId,
                        principalTable: "voertuigSoorten",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_handelsbenamingen_merkId",
                table: "handelsbenamingen",
                column: "merkId");

            migrationBuilder.CreateIndex(
                name: "IX_voertuigen_handelsbenamingId",
                table: "voertuigen",
                column: "handelsbenamingId");

            migrationBuilder.CreateIndex(
                name: "IX_voertuigen_importId",
                table: "voertuigen",
                column: "importId");

            migrationBuilder.CreateIndex(
                name: "IX_voertuigen_merkId",
                table: "voertuigen",
                column: "merkId");

            migrationBuilder.CreateIndex(
                name: "IX_voertuigen_voertuigSoortId",
                table: "voertuigen",
                column: "voertuigSoortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voertuigen");

            migrationBuilder.DropTable(
                name: "handelsbenamingen");

            migrationBuilder.DropTable(
                name: "imports");

            migrationBuilder.DropTable(
                name: "voertuigSoorten");

            migrationBuilder.DropTable(
                name: "merken");
        }
    }
}
