using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class AddViewUserAndMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText("./Migrations/SqlScripts/AddViewUserAndMoney.sql"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS UserAndMoney;");
        }
    }
}
