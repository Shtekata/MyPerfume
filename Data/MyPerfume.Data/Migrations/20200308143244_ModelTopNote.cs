namespace MyPerfume.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ModelTopNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopNotes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfumesTopNotes",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    TopNoteId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesTopNotes", x => new { x.PerfumeId, x.TopNoteId });
                    table.ForeignKey(
                        name: "FK_PerfumesTopNotes_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesTopNotes_TopNotes_TopNoteId",
                        column: x => x.TopNoteId,
                        principalTable: "TopNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesTopNotes_IsDeleted",
                table: "PerfumesTopNotes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesTopNotes_TopNoteId",
                table: "PerfumesTopNotes",
                column: "TopNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TopNotes_IsDeleted",
                table: "TopNotes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesTopNotes");

            migrationBuilder.DropTable(
                name: "TopNotes");
        }
    }
}
