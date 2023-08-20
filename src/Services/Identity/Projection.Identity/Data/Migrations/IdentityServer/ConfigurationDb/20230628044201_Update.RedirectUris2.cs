using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class UpdateRedirectUris2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 42, 1, 569, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "RedirectUri",
                value: "http://localhost:6002/swagger/oauth2-redirect.html");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "RedirectUri",
                value: "https://projection360-api.azurewebsites.net/swagger/oauth2-redirect.html");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 28, 4, 42, 1, 569, DateTimeKind.Utc).AddTicks(5920));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 27, 15, 25, 23, 861, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "RedirectUri",
                value: "http://localhost:6002/swagger/oauth2-redirect");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "RedirectUri",
                value: "https://projection360-api.azurewebsites.net/swagger/oauth2-redirect");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 27, 15, 25, 23, 861, DateTimeKind.Utc).AddTicks(7590));
        }
    }
}
