using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class IsrUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ScheduleRecipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleRecipes_UserId",
                table: "ScheduleRecipes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRecipes_AspNetUsers_UserId",
                table: "ScheduleRecipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleRecipes_AspNetUsers_UserId",
                table: "ScheduleRecipes");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleRecipes_UserId",
                table: "ScheduleRecipes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ScheduleRecipes");
        }
    }
}
