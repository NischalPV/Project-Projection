using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.Migrations.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class UpdateClientIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 7, 25, 10, 30, 59, 249, DateTimeKind.Utc).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 7, 25, 10, 30, 59, 249, DateTimeKind.Utc).AddTicks(8880));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 51, 43, 189, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 51, 43, 189, DateTimeKind.Utc).AddTicks(2580));
        }
    }
}
