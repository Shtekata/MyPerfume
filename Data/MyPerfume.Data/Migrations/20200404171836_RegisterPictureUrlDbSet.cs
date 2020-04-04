namespace MyPerfume.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RegisterPictureUrlDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PictureUrl_Perfumes_PerfumeId",
                table: "PictureUrl");

            migrationBuilder.DropForeignKey(
                name: "FK_PictureUrl_Products_ProductId",
                table: "PictureUrl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PictureUrl",
                table: "PictureUrl");

            migrationBuilder.RenameTable(
                name: "PictureUrl",
                newName: "PicturesUrls");

            migrationBuilder.RenameIndex(
                name: "IX_PictureUrl_ProductId",
                table: "PicturesUrls",
                newName: "IX_PicturesUrls_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PictureUrl_PerfumeId",
                table: "PicturesUrls",
                newName: "IX_PicturesUrls_PerfumeId");

            migrationBuilder.RenameIndex(
                name: "IX_PictureUrl_IsDeleted",
                table: "PicturesUrls",
                newName: "IX_PicturesUrls_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PicturesUrls",
                table: "PicturesUrls",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "PicturesUrls",
                newName: "PictureUrl");

            migrationBuilder.RenameIndex(
                name: "IX_PicturesUrls_ProductId",
                table: "PictureUrl",
                newName: "IX_PictureUrl_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PicturesUrls_PerfumeId",
                table: "PictureUrl",
                newName: "IX_PictureUrl_PerfumeId");

            migrationBuilder.RenameIndex(
                name: "IX_PicturesUrls_IsDeleted",
                table: "PictureUrl",
                newName: "IX_PictureUrl_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PictureUrl",
                table: "PictureUrl",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PictureUrl_Perfumes_PerfumeId",
                table: "PictureUrl",
                column: "PerfumeId",
                principalTable: "Perfumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PictureUrl_Products_ProductId",
                table: "PictureUrl",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
