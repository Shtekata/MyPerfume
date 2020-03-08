using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class ModelPerfumeSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerType",
                table: "Perfumes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PerfumesSeasons",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    SeasonId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Season = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesSeasons", x => new { x.PerfumeId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_PerfumesSeasons_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesSeasons_IsDeleted",
                table: "PerfumesSeasons",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesSeasons");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerType",
                table: "Perfumes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
