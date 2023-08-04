using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class addMotivationDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TMotivation",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TMotivation");
        }
    }
}
