using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SV.RDW.Migrations.PostgreSQL.Migrations
{
    public partial class MeerNullFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voertuigen_handelsbenamingen_handelsbenamingId",
                table: "voertuigen");

            migrationBuilder.DropForeignKey(
                name: "FK_voertuigen_voertuigSoorten_voertuigSoortId",
                table: "voertuigen");

            migrationBuilder.AlterColumn<int>(
                name: "voertuigSoortId",
                table: "voertuigen",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "handelsbenamingId",
                table: "voertuigen",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_voertuigen_handelsbenamingen_handelsbenamingId",
                table: "voertuigen",
                column: "handelsbenamingId",
                principalTable: "handelsbenamingen",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_voertuigen_voertuigSoorten_voertuigSoortId",
                table: "voertuigen",
                column: "voertuigSoortId",
                principalTable: "voertuigSoorten",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voertuigen_handelsbenamingen_handelsbenamingId",
                table: "voertuigen");

            migrationBuilder.DropForeignKey(
                name: "FK_voertuigen_voertuigSoorten_voertuigSoortId",
                table: "voertuigen");

            migrationBuilder.AlterColumn<int>(
                name: "voertuigSoortId",
                table: "voertuigen",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "handelsbenamingId",
                table: "voertuigen",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_voertuigen_handelsbenamingen_handelsbenamingId",
                table: "voertuigen",
                column: "handelsbenamingId",
                principalTable: "handelsbenamingen",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_voertuigen_voertuigSoorten_voertuigSoortId",
                table: "voertuigen",
                column: "voertuigSoortId",
                principalTable: "voertuigSoorten",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
