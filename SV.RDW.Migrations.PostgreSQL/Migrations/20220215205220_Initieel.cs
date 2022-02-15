using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SV.RDW.Migrations.PostgreSQL.Migrations
{
    public partial class Initieel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "imports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eersteToelatingDatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    totaalImport = table.Column<int>(type: "integer", nullable: false),
                    importSeconden = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "merken",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    naam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merken", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voertuigSoorten",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    naam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voertuigSoorten", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "handelsbenamingen",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    naam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    merkId = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "voertuigen",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kenteken = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    voertuigSoortId = table.Column<int>(type: "integer", nullable: false),
                    merkId = table.Column<int>(type: "integer", nullable: false),
                    handelsbenamingId = table.Column<int>(type: "integer", nullable: false),
                    vervalDatumAPK = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenaamstelling = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    eersteToelating = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inrichting = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    kleur = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true, comment: "Hier staat de kleur."),
                    massaLedig = table.Column<decimal>(type: "numeric", nullable: true),
                    importId = table.Column<int>(type: "integer", nullable: false)
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
                });

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
