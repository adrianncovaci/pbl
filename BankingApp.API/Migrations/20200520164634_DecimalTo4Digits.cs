using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.API.Migrations
{
    public partial class DecimalTo4Digits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 20, 19, 46, 33, 980, DateTimeKind.Local).AddTicks(3181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 5, 20, 19, 43, 53, 322, DateTimeKind.Local).AddTicks(2857));

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
                defaultValue: new DateTime(2020, 5, 20, 19, 46, 33, 930, DateTimeKind.Local).AddTicks(4524),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 5, 20, 19, 43, 53, 279, DateTimeKind.Local).AddTicks(2056));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 20, 19, 43, 53, 322, DateTimeKind.Local).AddTicks(2857),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 20, 19, 46, 33, 980, DateTimeKind.Local).AddTicks(3181));

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
                defaultValue: new DateTime(2020, 5, 20, 19, 43, 53, 279, DateTimeKind.Local).AddTicks(2056),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 20, 19, 46, 33, 930, DateTimeKind.Local).AddTicks(4524));
        }
    }
}
