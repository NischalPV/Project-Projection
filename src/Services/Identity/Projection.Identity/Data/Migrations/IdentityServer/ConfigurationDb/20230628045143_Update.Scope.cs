using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class UpdateScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiResourceScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Scope",
                value: "AccountingAPI");

            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DisplayName", "Name" },
                values: new object[] { new DateTime(2023, 6, 28, 4, 51, 43, 189, DateTimeKind.Utc).AddTicks(2000), "Accounting API Scope", "AccountingAPI" });

            migrationBuilder.UpdateData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Scope",
                value: "AccountingAPI");

            migrationBuilder.UpdateData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Scope",
                value: "AccountingAPI");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 51, 43, 189, DateTimeKind.Utc).AddTicks(2580));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClientId",
                value: "projection-accounting-api");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClientId",
                value: "projection-accounting-api--prod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiResourceScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Scope",
                value: "API");

            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DisplayName", "Name" },
                values: new object[] { new DateTime(2023, 6, 28, 4, 44, 44, 666, DateTimeKind.Utc).AddTicks(380), "API Scope", "API" });

            migrationBuilder.UpdateData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Scope",
                value: "API");

            migrationBuilder.UpdateData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Scope",
                value: "API");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 44, 44, 666, DateTimeKind.Utc).AddTicks(1060));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClientId",
                value: "projection-api");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClientId",
                value: "projection-api--prod");
        }
    }
}
