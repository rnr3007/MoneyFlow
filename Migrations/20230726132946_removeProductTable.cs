using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class removeProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "TUser");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Id_Username_Email",
                table: "TUser",
                newName: "IX_TUser_Id_Username_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUser",
                table: "TUser",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TExpense",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserDataId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    CostType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TExpense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TExpense_TUser_UserDataId",
                        column: x => x.UserDataId,
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TExpense_UserDataId",
                table: "TExpense",
                column: "UserDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUser",
                table: "TUser");

            migrationBuilder.RenameTable(
                name: "TUser",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_TUser_Id_Username_Email",
                table: "Users",
                newName: "IX_Users_Id_Username_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserExpenses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDataId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExpenses_Users_UserDataId",
                        column: x => x.UserDataId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id_Name",
                table: "Products",
                columns: new[] { "Id", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenses_UserDataId",
                table: "UserExpenses",
                column: "UserDataId");
        }
    }
}
