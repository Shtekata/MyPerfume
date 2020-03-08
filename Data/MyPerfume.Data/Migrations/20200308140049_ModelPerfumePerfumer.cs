using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class ModelPerfumePerfumer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfumer",
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
                    table.PrimaryKey("PK_Perfumer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfumesPerfumers",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    PerfumerId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesPerfumers", x => new { x.PerfumeId, x.PerfumerId });
                    table.ForeignKey(
                        name: "FK_PerfumesPerfumers_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesPerfumers_Perfumer_PerfumerId",
                        column: x => x.PerfumerId,
                        principalTable: "Perfumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perfumer_IsDeleted",
                table: "Perfumer",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesPerfumers_IsDeleted",
                table: "PerfumesPerfumers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesPerfumers_PerfumerId",
                table: "PerfumesPerfumers",
                column: "PerfumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesPerfumers");

            migrationBuilder.DropTable(
                name: "Perfumer");
        }
    }
}
