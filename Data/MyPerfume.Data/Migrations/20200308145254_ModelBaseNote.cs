using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class ModelBaseNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseNotes",
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
                    table.PrimaryKey("PK_BaseNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfumesBaseNotes",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    BaseNoteId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesBaseNotes", x => new { x.PerfumeId, x.BaseNoteId });
                    table.ForeignKey(
                        name: "FK_PerfumesBaseNotes_BaseNotes_BaseNoteId",
                        column: x => x.BaseNoteId,
                        principalTable: "BaseNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesBaseNotes_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseNotes_IsDeleted",
                table: "BaseNotes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesBaseNotes_BaseNoteId",
                table: "PerfumesBaseNotes",
                column: "BaseNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesBaseNotes_IsDeleted",
                table: "PerfumesBaseNotes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesBaseNotes");

            migrationBuilder.DropTable(
                name: "BaseNotes");
        }
    }
}
