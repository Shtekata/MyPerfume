namespace MyPerfume.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ModelPerfumeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfumesPerfumers_Perfumer_PerfumerId",
                table: "PerfumesPerfumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfumer",
                table: "Perfumer");

            migrationBuilder.RenameTable(
                name: "Perfumer",
                newName: "Perfumers");

            migrationBuilder.RenameIndex(
                name: "IX_Perfumer_IsDeleted",
                table: "Perfumers",
                newName: "IX_Perfumers_IsDeleted");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PerfumesSeasons",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PerfumesPurposes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PerfumesPerfumers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfumers",
                table: "Perfumers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
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
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfumesCategories",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    CategoryId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesCategories", x => new { x.PerfumeId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PerfumesCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesCategories_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsDeleted",
                table: "Categories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesCategories_CategoryId",
                table: "PerfumesCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesCategories_IsDeleted",
                table: "PerfumesCategories",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfumesPerfumers_Perfumers_PerfumerId",
                table: "PerfumesPerfumers",
                column: "PerfumerId",
                principalTable: "Perfumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfumesPerfumers_Perfumers_PerfumerId",
                table: "PerfumesPerfumers");

            migrationBuilder.DropTable(
                name: "PerfumesCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfumers",
                table: "Perfumers");

            migrationBuilder.RenameTable(
                name: "Perfumers",
                newName: "Perfumer");

            migrationBuilder.RenameIndex(
                name: "IX_Perfumers_IsDeleted",
                table: "Perfumer",
                newName: "IX_Perfumer_IsDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PerfumesSeasons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PerfumesPurposes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PerfumesPerfumers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfumer",
                table: "Perfumer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfumesPerfumers_Perfumer_PerfumerId",
                table: "PerfumesPerfumers",
                column: "PerfumerId",
                principalTable: "Perfumer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
