using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class fixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TExpense_TUser_UserDataId",
                table: "TExpense");

            migrationBuilder.DropIndex(
                name: "IX_TExpense_UserDataId",
                table: "TExpense");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "TExpense");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TExpense",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TExpense_UserId",
                table: "TExpense",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TExpense_TUser_UserId",
                table: "TExpense",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TExpense_TUser_UserId",
                table: "TExpense");

            migrationBuilder.DropIndex(
                name: "IX_TExpense_UserId",
                table: "TExpense");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TExpense");

            migrationBuilder.AddColumn<string>(
                name: "UserDataId",
                table: "TExpense",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TExpense_UserDataId",
                table: "TExpense",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_TExpense_TUser_UserDataId",
                table: "TExpense",
                column: "UserDataId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
