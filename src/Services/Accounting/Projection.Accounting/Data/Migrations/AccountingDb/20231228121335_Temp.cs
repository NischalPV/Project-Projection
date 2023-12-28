using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Accounting.Data.Migrations.AccountingDb
{
    /// <inheritdoc />
    public partial class Temp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 28, 12, 13, 33, 690, DateTimeKind.Utc).AddTicks(3441));

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 28, 12, 13, 33, 690, DateTimeKind.Utc).AddTicks(3448));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
