using Microsoft.EntityFrameworkCore.Migrations;

namespace JmesJemsSite.Data.Migrations
{
    public partial class Eighth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JewelryImage",
                table: "Jewelry",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JewelryImage",
                table: "Jewelry");
        }
    }
}
