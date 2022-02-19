using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SV.RDW.Migrations.MySQL.Migrations
{
    public partial class ImportVoertuigenFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voertuigen_imports_importId",
                table: "voertuigen");

            migrationBuilder.AlterColumn<int>(
                name: "importId",
                table: "voertuigen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_voertuigen_imports_importId",
                table: "voertuigen",
                column: "importId",
                principalTable: "imports",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voertuigen_imports_importId",
                table: "voertuigen");

            migrationBuilder.AlterColumn<int>(
                name: "importId",
                table: "voertuigen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_voertuigen_imports_importId",
                table: "voertuigen",
                column: "importId",
                principalTable: "imports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
