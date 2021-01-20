using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingApp.API.Migrations
{
    public partial class MadeCNPRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CNP",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 616, DateTimeKind.Local).AddTicks(583),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 28, 20, 58, 37, 788, DateTimeKind.Local).AddTicks(191));

            migrationBuilder.AlterColumn<string>(
                name: "CNP",
                table: "Customers",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 573, DateTimeKind.Local).AddTicks(8555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 28, 20, 58, 37, 743, DateTimeKind.Local).AddTicks(359));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CNP",
                table: "Customers",
                column: "CNP",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CNP",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 20, 58, 37, 788, DateTimeKind.Local).AddTicks(191),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 616, DateTimeKind.Local).AddTicks(583));

            migrationBuilder.AlterColumn<string>(
                name: "CNP",
                table: "Customers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "BankAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 20, 58, 37, 743, DateTimeKind.Local).AddTicks(359),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 4, 28, 22, 37, 28, 573, DateTimeKind.Local).AddTicks(8555));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CNP",
                table: "Customers",
                column: "CNP",
                unique: true,
                filter: "[CNP] IS NOT NULL");
        }
    }
}
