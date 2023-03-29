using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooWebApp.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Snack",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Snack",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
