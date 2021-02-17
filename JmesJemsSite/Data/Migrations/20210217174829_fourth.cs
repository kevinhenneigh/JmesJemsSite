using Microsoft.EntityFrameworkCore.Migrations;

namespace JmesJemsSite.Data.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Artwork_ArtworkArtId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_ArtworkArtId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ArtworkArtId",
                table: "Material");

            migrationBuilder.CreateTable(
                name: "ArtMaterial",
                columns: table => new
                {
                    ArtMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtworkArtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtMaterial", x => x.ArtMaterialId);
                    table.ForeignKey(
                        name: "FK_ArtMaterial_Artwork_ArtworkArtId",
                        column: x => x.ArtworkArtId,
                        principalTable: "Artwork",
                        principalColumn: "ArtId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtMaterial_ArtworkArtId",
                table: "ArtMaterial",
                column: "ArtworkArtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtMaterial");

            migrationBuilder.AddColumn<int>(
                name: "ArtworkArtId",
                table: "Material",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_ArtworkArtId",
                table: "Material",
                column: "ArtworkArtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Artwork_ArtworkArtId",
                table: "Material",
                column: "ArtworkArtId",
                principalTable: "Artwork",
                principalColumn: "ArtId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
