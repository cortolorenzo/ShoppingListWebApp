using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class RecipeChange4539 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeProducts",
                table: "RecipeProducts");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeProductId",
                table: "RecipeProducts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeProducts",
                table: "RecipeProducts",
                column: "RecipeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProducts_ProductId",
                table: "RecipeProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeProducts",
                table: "RecipeProducts");

            migrationBuilder.DropIndex(
                name: "IX_RecipeProducts_ProductId",
                table: "RecipeProducts");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeProductId",
                table: "RecipeProducts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeProducts",
                table: "RecipeProducts",
                columns: new[] { "ProductId", "RecipeId" });
        }
    }
}
