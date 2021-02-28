using Microsoft.EntityFrameworkCore.Migrations;

namespace JmesJemsSite.Data.Migrations
{
    public partial class Sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtMaterial");

            migrationBuilder.AddColumn<string>(
                name: "ProductPicture",
                table: "Jewelry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductPicture",
                table: "Artwork",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "Artwork");

            migrationBuilder.CreateTable(
                name: "ArtMaterial",
                columns: table => new
                {
                    ArtMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtworkProductId = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtMaterial", x => x.ArtMaterialId);
                    table.ForeignKey(
                        name: "FK_ArtMaterial_Artwork_ArtworkProductId",
                        column: x => x.ArtworkProductId,
                        principalTable: "Artwork",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtMaterial_ArtworkProductId",
                table: "ArtMaterial",
                column: "ArtworkProductId");
        }
    }
}
