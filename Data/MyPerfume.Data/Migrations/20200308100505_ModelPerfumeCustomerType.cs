using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPerfume.Data.Migrations
{
    public partial class ModelPerfumeCustomerType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerType",
                table: "Perfumes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "Perfumes");
        }
    }
}
