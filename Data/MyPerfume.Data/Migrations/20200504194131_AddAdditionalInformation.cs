namespace MyPerfume.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAdditionalInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "PictureUrls",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "PictureUrls");
        }
    }
}
