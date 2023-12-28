using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class ClientSecretHash : Migration
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
                value: new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Value" },
                values: new object[] { new DateTime(2023, 12, 23, 6, 59, 13, 589, DateTimeKind.Utc).AddTicks(7299), "6d413aaf87a4ac637c1cb9a2928fcba0b102d4a2220f2ab036db5ee1dc23ac76" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Value" },
                values: new object[] { new DateTime(2023, 12, 23, 6, 59, 13, 589, DateTimeKind.Utc).AddTicks(7301), "6d413aaf87a4ac637c1cb9a2928fcba0b102d4a2220f2ab036db5ee1dc23ac76" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 22, 5, 36, 17, 955, DateTimeKind.Utc).AddTicks(3225));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Value" },
                values: new object[] { new DateTime(2023, 12, 22, 5, 36, 17, 955, DateTimeKind.Utc).AddTicks(4358), "projection@2023" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Value" },
                values: new object[] { new DateTime(2023, 12, 22, 5, 36, 17, 955, DateTimeKind.Utc).AddTicks(4360), "projection@2023" });
        }
    }
}
