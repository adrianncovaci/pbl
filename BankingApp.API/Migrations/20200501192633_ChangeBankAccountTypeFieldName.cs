using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.API.Migrations
{
    public partial class ChangeBankAccountTypeFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "BankAccountTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 1, 22, 26, 32, 442, DateTimeKind.Local).AddTicks(6247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 616, DateTimeKind.Local).AddTicks(583));

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loans",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialInterestRate",
                table: "BankAccountTypes",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "BankAccountTypes",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 1, 22, 26, 32, 374, DateTimeKind.Local).AddTicks(8369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 573, DateTimeKind.Local).AddTicks(8555));

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "AccountType",
                value: "Savings");

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccountType",
                value: "Checkings");

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccountType",
                value: "Retirement");

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccountType",
                value: "Loan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankAccountTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 616, DateTimeKind.Local).AddTicks(583),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 1, 22, 26, 32, 442, DateTimeKind.Local).AddTicks(6247));

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loans",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialInterestRate",
                table: "BankAccountTypes",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "BankAccountTypes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "BankAccounts",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "BankAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 573, DateTimeKind.Local).AddTicks(8555),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 1, 22, 26, 32, 374, DateTimeKind.Local).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Savings");

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: "Checkings");

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "Retirement");

            migrationBuilder.UpdateData(
                table: "BankAccountTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Type",
                value: "Loan");
        }
    }
}
