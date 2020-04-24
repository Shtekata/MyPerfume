namespace MyPerfume.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPictureShowNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureShowNumber",
                table: "PicturesUrls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureShowNumber",
                table: "PicturesUrls");
        }
    }
}
