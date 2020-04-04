namespace MyPerfume.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddDesignerAndPerfumNamesToPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesignerAndPerfumeNames",
                table: "PictureUrl",
                maxLength: 100,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<int>(
                name: "PictureNumber",
                table: "PictureUrl",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignerAndPerfumeNames",
                table: "PictureUrl");

            migrationBuilder.DropColumn(
                name: "PictureNumber",
                table: "PictureUrl");
        }
    }
}
