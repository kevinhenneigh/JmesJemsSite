using Microsoft.EntityFrameworkCore.Migrations;

namespace JmesJemsSite.Data.Migrations
{
    public partial class addedproductclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtMaterial_Artwork_ArtworkArtId",
                table: "ArtMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Jewelry_JewelryId",
                table: "Material");

            migrationBuilder.RenameColumn(
                name: "JewelryId",
                table: "Material",
                newName: "JewelryProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_JewelryId",
                table: "Material",
                newName: "IX_Material_JewelryProductId");

            migrationBuilder.RenameColumn(
                name: "JewelryId",
                table: "Jewelry",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ArtId",
                table: "Artwork",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ArtworkArtId",
                table: "ArtMaterial",
                newName: "ArtworkProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtMaterial_ArtworkArtId",
                table: "ArtMaterial",
                newName: "IX_ArtMaterial_ArtworkProductId");

            migrationBuilder.AddColumn<int>(
                name: "ArtworkProductId",
                table: "Material",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Jewelry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Artwork",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_ArtworkProductId",
                table: "Material",
                column: "ArtworkProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtMaterial_Artwork_ArtworkProductId",
                table: "ArtMaterial",
                column: "ArtworkProductId",
                principalTable: "Artwork",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Artwork_ArtworkProductId",
                table: "Material",
                column: "ArtworkProductId",
                principalTable: "Artwork",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Jewelry_JewelryProductId",
                table: "Material",
                column: "JewelryProductId",
                principalTable: "Jewelry",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtMaterial_Artwork_ArtworkProductId",
                table: "ArtMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Artwork_ArtworkProductId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Jewelry_JewelryProductId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_ArtworkProductId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ArtworkProductId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Artwork");

            migrationBuilder.RenameColumn(
                name: "JewelryProductId",
                table: "Material",
                newName: "JewelryId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_JewelryProductId",
                table: "Material",
                newName: "IX_Material_JewelryId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Jewelry",
                newName: "JewelryId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Artwork",
                newName: "ArtId");

            migrationBuilder.RenameColumn(
                name: "ArtworkProductId",
                table: "ArtMaterial",
                newName: "ArtworkArtId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtMaterial_ArtworkProductId",
                table: "ArtMaterial",
                newName: "IX_ArtMaterial_ArtworkArtId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtMaterial_Artwork_ArtworkArtId",
                table: "ArtMaterial",
                column: "ArtworkArtId",
                principalTable: "Artwork",
                principalColumn: "ArtId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Jewelry_JewelryId",
                table: "Material",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
