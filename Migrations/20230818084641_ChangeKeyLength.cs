using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyFlow.Migrations
{
    public partial class ChangeKeyLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TExpense_TUser_UserId",
                table: "TExpense"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_TIncome_TUser_UserId",
                table: "TIncome"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_TMotivation_TUser_UserId",
                table: "TMotivation"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUser",
                table: "TUser"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMotivation",
                table: "TMotivation"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TExpense",
                table: "TExpense"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIncome",
                table: "TIncome"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TUser",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TMotivation",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TMotivation",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TIncome",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TIncome",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TExpense",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TExpense",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUser",
                table: "TUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TExpense",
                table: "TExpense",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIncome",
                table: "TIncome",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMotivation",
                table: "TMotivation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TExpense_TUser_UserId",
                table: "TExpense",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TIncome_TUser_UserId",
                table: "TIncome",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_TExpense_TUser_UserId",
                table: "TExpense"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_TIncome_TUser_UserId",
                table: "TIncome"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_TMotivation_TUser_UserId",
                table: "TMotivation"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUser",
                table: "TUser"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMotivation",
                table: "TMotivation"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TExpense",
                table: "TExpense"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIncome",
                table: "TIncome"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TMotivation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TMotivation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TIncome",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TIncome",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TExpense",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TExpense",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUser",
                table: "TUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TExpense",
                table: "TExpense",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIncome",
                table: "TIncome",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMotivation",
                table: "TMotivation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TExpense_TUser_UserId",
                table: "TExpense",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TIncome_TUser_UserId",
                table: "TIncome",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TMotivation_TUser_UserId",
                table: "TMotivation",
                column: "UserId",
                principalTable: "TUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
