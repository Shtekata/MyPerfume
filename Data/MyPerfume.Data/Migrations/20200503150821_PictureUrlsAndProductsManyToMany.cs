namespace MyPerfume.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PictureUrlsAndProductsManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PicturesUrls_Perfumes_PerfumeId",
                table: "PicturesUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_PicturesUrls_Products_ProductId",
                table: "PicturesUrls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PicturesUrls",
                table: "PicturesUrls");

            migrationBuilder.DropIndex(
                name: "IX_PicturesUrls_PerfumeId",
                table: "PicturesUrls");

            migrationBuilder.DropIndex(
                name: "IX_PicturesUrls_ProductId",
                table: "PicturesUrls");

            migrationBuilder.RenameColumn(
                name: "DesignerAndPerfumeNames",
                table: "PicturesUrls",
                newName: "DesignerName");

            migrationBuilder.DropColumn(
                name: "PerfumeId",
                table: "PicturesUrls");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PicturesUrls");

            migrationBuilder.RenameTable(
                name: "PicturesUrls",
                newName: "PictureUrls");

            migrationBuilder.RenameIndex(
                name: "IX_PicturesUrls_IsDeleted",
                table: "PictureUrls",
                newName: "IX_PictureUrls_IsDeleted");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PerfumesTopNotes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DesignerName",
                table: "PictureUrls",
                maxLength: 50,
                oldMaxLength: 100,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "PerfumeName",
                table: "PictureUrls",
                maxLength: 50,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PictureUrls",
                table: "PictureUrls",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PerfumesPictureUrls",
                columns: table => new
                {
                    PerfumeId = table.Column<string>(nullable: false),
                    PictureUrlId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumesPictureUrls", x => new { x.PerfumeId, x.PictureUrlId });
                    table.ForeignKey(
                        name: "FK_PerfumesPictureUrls_Perfumes_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerfumesPictureUrls_PictureUrls_PictureUrlId",
                        column: x => x.PictureUrlId,
                        principalTable: "PictureUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsPictureUrls",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    PictureUrlId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsPictureUrls", x => new { x.ProductId, x.PictureUrlId });
                    table.ForeignKey(
                        name: "FK_ProductsPictureUrls_PictureUrls_PictureUrlId",
                        column: x => x.PictureUrlId,
                        principalTable: "PictureUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsPictureUrls_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesPictureUrls_IsDeleted",
                table: "PerfumesPictureUrls",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumesPictureUrls_PictureUrlId",
                table: "PerfumesPictureUrls",
                column: "PictureUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsPictureUrls_IsDeleted",
                table: "ProductsPictureUrls",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsPictureUrls_PictureUrlId",
                table: "ProductsPictureUrls",
                column: "PictureUrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfumesPictureUrls");

            migrationBuilder.DropTable(
                name: "ProductsPictureUrls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PictureUrls",
                table: "PictureUrls");

            migrationBuilder.RenameColumn(
                name: "DesignerName",
                table: "PictureUrls",
                newName: "DesignerAndPerfumeNames");

            migrationBuilder.DropColumn(
                name: "PerfumeName",
                table: "PictureUrls");

            migrationBuilder.RenameTable(
                name: "PictureUrls",
                newName: "PicturesUrls");

            migrationBuilder.RenameIndex(
                name: "IX_PictureUrls_IsDeleted",
                table: "PicturesUrls",
                newName: "IX_PicturesUrls_IsDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PerfumesTopNotes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "DesignerAndPerfumeNames",
                table: "PicturesUrls",
                type: "nvarchar(100)",
                maxLength: 100,
                oldMaxLength: 50,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "PerfumeId",
                table: "PicturesUrls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "PicturesUrls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PicturesUrls",
                table: "PicturesUrls",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PicturesUrls_PerfumeId",
                table: "PicturesUrls",
                column: "PerfumeId");

            migrationBuilder.CreateIndex(
                name: "IX_PicturesUrls_ProductId",
                table: "PicturesUrls",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PicturesUrls_Perfumes_PerfumeId",
                table: "PicturesUrls",
                column: "PerfumeId",
                principalTable: "Perfumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PicturesUrls_Products_ProductId",
                table: "PicturesUrls",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
