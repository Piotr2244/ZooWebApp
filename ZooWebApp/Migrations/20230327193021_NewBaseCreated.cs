using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooWebApp.Migrations
{
    public partial class NewBaseCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack");

            migrationBuilder.DropColumn(
                name: "AnimalGuidId",
                table: "Snack");

            migrationBuilder.RenameColumn(
                name: "SnackId",
                table: "Snack",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Animal",
                newName: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Snack",
                newName: "SnackId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Animal",
                newName: "AnimalId");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Snack",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalGuidId",
                table: "Snack",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Snack_Animal_AnimalId",
                table: "Snack",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "AnimalId");
        }
    }
}
