using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class AddStoreProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText("./Migrations/SqlScripts/20230818092703_AddStoreProcedure.sql"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE BuyDream;");
        }
    }
}
