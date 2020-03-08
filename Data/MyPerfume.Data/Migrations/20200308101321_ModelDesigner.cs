using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class ModelDesigner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesignerId",
                table: "Perfumes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Designers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perfumes_DesignerId",
                table: "Perfumes",
                column: "DesignerId");

            migrationBuilder.CreateIndex(
                name: "IX_Designers_IsDeleted",
                table: "Designers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Perfumes_Designers_DesignerId",
                table: "Perfumes",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perfumes_Designers_DesignerId",
                table: "Perfumes");

            migrationBuilder.DropTable(
                name: "Designers");

            migrationBuilder.DropIndex(
                name: "IX_Perfumes_DesignerId",
                table: "Perfumes");

            migrationBuilder.DropColumn(
                name: "DesignerId",
                table: "Perfumes");
        }
    }
}
