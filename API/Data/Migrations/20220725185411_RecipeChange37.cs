using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class RecipeChange37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
