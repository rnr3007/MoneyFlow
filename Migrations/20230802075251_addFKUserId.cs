using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class addFKUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TMotivation",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TMotivation_UserId",
                table: "TMotivation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TMotivation_TUser_UserId",
                table: "TMotivation",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMotivation_TUser_UserId",
                table: "TMotivation");

            migrationBuilder.DropIndex(
                name: "IX_TMotivation_UserId",
                table: "TMotivation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TMotivation");
        }
    }
}
