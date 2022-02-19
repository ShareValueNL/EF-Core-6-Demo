using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SV.RDW.Migrations.MySQL.Migrations
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
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "handelsbenamingId",
                table: "voertuigen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "handelsbenamingId",
                table: "voertuigen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
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
