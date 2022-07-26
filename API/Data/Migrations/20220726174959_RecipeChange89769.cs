﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class RecipeChange89769 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProducts_Recipes_RecipeId",
                table: "RecipeProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProducts_Recipes_RecipeId",
                table: "RecipeProducts",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProducts_Recipes_RecipeId",
                table: "RecipeProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProducts_Products_ProductId",
                table: "RecipeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProducts_Recipes_RecipeId",
                table: "RecipeProducts",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
