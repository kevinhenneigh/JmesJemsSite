using Microsoft.EntityFrameworkCore.Migrations;

namespace JmesJemsSite.Data.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtworkArtId",
                table: "Material",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    ArtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.ArtId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Artwork_ArtworkArtId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "Artwork");

            migrationBuilder.DropIndex(
                name: "IX_Material_ArtworkArtId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ArtworkArtId",
                table: "Material");
        }
    }
}
