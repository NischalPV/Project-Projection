using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class ClientSeretUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 21, 17, 26, 4, 554, DateTimeKind.Utc).AddTicks(1854));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClientId", "Created" },
                values: new object[] { 1, new DateTime(2023, 12, 21, 17, 26, 4, 554, DateTimeKind.Utc).AddTicks(4504) });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "ClientSecrets",
                columns: new[] { "Id", "ClientId", "Created", "Description", "Expiration", "Type", "Value" },
                values: new object[] { 2, 2, new DateTime(2023, 12, 21, 17, 26, 4, 554, DateTimeKind.Utc).AddTicks(4510), null, null, "SharedSecret", "projection@2023" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 21, 17, 4, 58, 676, DateTimeKind.Utc).AddTicks(9469));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClientId", "Created" },
                values: new object[] { 2, new DateTime(2023, 12, 21, 17, 4, 58, 677, DateTimeKind.Utc).AddTicks(798) });
        }
    }
}
