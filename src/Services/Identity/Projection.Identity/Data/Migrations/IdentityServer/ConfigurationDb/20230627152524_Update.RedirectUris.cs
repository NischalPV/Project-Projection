using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class UpdateRedirectUris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 6, 27, 15, 25, 23, 861, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "PostLogoutRedirectUri",
                value: "http://localhost:6002/swagger/");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 5, 22, 5, 55, 14, 273, DateTimeKind.Utc).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "PostLogoutRedirectUri",
                value: "http://localhost:6002/docs/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "RedirectUri",
                value: "http://localhost:6002/docs/oauth2-redirect");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "RedirectUri",
                value: "https://projection360-api.azurewebsites.net/docs/oauth2-redirect");

            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 5, 22, 5, 55, 14, 273, DateTimeKind.Utc).AddTicks(6950));
        }
    }
}
