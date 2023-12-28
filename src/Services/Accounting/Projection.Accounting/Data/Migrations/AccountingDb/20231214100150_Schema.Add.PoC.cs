using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Accounting.Data.Migrations.AccountingDb
{
    /// <inheritdoc />
    public partial class SchemaAddPoC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                schema: "accounting",
                table: "AccountTransactions");

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                schema: "accounting",
                table: "Accounts",
                type: "jsonb",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 14, 10, 1, 47, 960, DateTimeKind.Utc).AddTicks(904));

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 14, 10, 1, 47, 960, DateTimeKind.Utc).AddTicks(909));

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                schema: "accounting",
                table: "AccountTransactions",
                column: "AccountId",
                principalSchema: "accounting",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                schema: "accounting",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "Contacts",
                schema: "accounting",
                table: "Accounts");

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 13, 11, 46, 2, 608, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 13, 11, 46, 2, 608, DateTimeKind.Utc).AddTicks(4964));

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                schema: "accounting",
                table: "AccountTransactions",
                column: "AccountId",
                principalSchema: "accounting",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
