using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class UpdateGrantType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 44, 44, 666, DateTimeKind.Utc).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "GrantType",
                value: "implicit");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 44, 44, 666, DateTimeKind.Utc).AddTicks(1060));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 42, 1, 569, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "GrantType",
                value: "authorization_code");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 42, 1, 569, DateTimeKind.Utc).AddTicks(5920));
        }
    }
}
