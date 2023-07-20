using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class addUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username_Email",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Username_Email",
                table: "Users",
                columns: new[] { "Id", "Username", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id_Name",
                table: "Products",
                columns: new[] { "Id", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Username_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Products_Id_Name",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_Email",
                table: "Users",
                columns: new[] { "Username", "Email" },
                unique: true);
        }
    }
}
