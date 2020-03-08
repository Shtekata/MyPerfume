using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class ModelHeartNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeartNotes",
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
                    table.PrimaryKey("PK_HeartNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfumesHeartNotes",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    HeartNoteId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesHeartNotes", x => new { x.PerfumeId, x.HeartNoteId });
                    table.ForeignKey(
                        name: "FK_PerfumesHeartNotes_HeartNotes_HeartNoteId",
                        column: x => x.HeartNoteId,
                        principalTable: "HeartNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesHeartNotes_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeartNotes_IsDeleted",
                table: "HeartNotes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesHeartNotes_HeartNoteId",
                table: "PerfumesHeartNotes",
                column: "HeartNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesHeartNotes_IsDeleted",
                table: "PerfumesHeartNotes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesHeartNotes");

            migrationBuilder.DropTable(
                name: "HeartNotes");
        }
    }
}
