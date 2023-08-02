using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class addMotivationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMotivation",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    TargetName = table.Column<string>(nullable: false),
                    TargetPrice = table.Column<long>(nullable: false),
                    TargetImage = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMotivation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMotivation");
        }
    }
}
