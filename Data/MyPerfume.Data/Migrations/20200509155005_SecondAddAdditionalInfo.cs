using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class SecondAddAdditionalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalInformation",
                table: "PictureUrls",
                newName: "AdditionalInfo");

            migrationBuilder.AddColumn<string>(
                name: "SecondAdditionalInfo",
                table: "PictureUrls",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "AdditionalInfo",
               table: "PictureUrls",
               newName: "AdditionalInformation");

            migrationBuilder.DropColumn(
                name: "SecondAdditionalInfo",
                table: "PictureUrls");
        }
    }
}
