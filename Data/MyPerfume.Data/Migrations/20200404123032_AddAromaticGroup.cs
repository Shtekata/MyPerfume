namespace MyPerfume.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAromaticGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AromaticGroups",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AromaticGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfumesAromaticGroups",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    AromaticGroupId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesAromaticGroups", x => new { x.PerfumeId, x.AromaticGroupId });
                    table.ForeignKey(
                        name: "FK_PerfumesAromaticGroups_AromaticGroups_AromaticGroupId",
                        column: x => x.AromaticGroupId,
                        principalTable: "AromaticGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesAromaticGroups_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AromaticGroups_IsDeleted",
                table: "AromaticGroups",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesAromaticGroups_AromaticGroupId",
                table: "PerfumesAromaticGroups",
                column: "AromaticGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesAromaticGroups_IsDeleted",
                table: "PerfumesAromaticGroups",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesAromaticGroups");

            migrationBuilder.DropTable(
                name: "AromaticGroups");
        }
    }
}
