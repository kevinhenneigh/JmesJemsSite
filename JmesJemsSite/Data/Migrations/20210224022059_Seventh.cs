using Microsoft.EntityFrameworkCore.Migrations;

namespace JmesJemsSite.Data.Migrations
{
    public partial class Seventh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "Jewelry");

            migrationBuilder.RenameColumn(
                name: "ProductPicture",
                table: "Artwork",
                newName: "ArtImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArtImage",
                table: "Artwork",
                newName: "ProductPicture");

            migrationBuilder.AddColumn<string>(
                name: "ProductPicture",
                table: "Jewelry",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
