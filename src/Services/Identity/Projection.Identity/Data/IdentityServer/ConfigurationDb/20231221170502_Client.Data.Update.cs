using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class ClientDataUpdate : Migration
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
                value: new DateTime(2023, 12, 21, 17, 4, 58, 676, DateTimeKind.Utc).AddTicks(9469));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Origin",
                value: "https://localhost:7140");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "GrantType",
                value: "authorization_code");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostLogoutRedirectUri",
                value: "https://localhost:7140/signout-callback-oidc");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "RedirectUri",
                value: "https://localhost:7140/signin-oidc");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Scope",
                value: "AccountingAPI");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "ClientScopes",
                columns: new[] { "Id", "ClientId", "Scope" },
                values: new object[] { 9, 1, "offline_access" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 21, 17, 4, 58, 677, DateTimeKind.Utc).AddTicks(798));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AlwaysIncludeUserClaimsInIdToken", "ClientUri", "RequirePkce" },
                values: new object[] { false, true, true, "https://localhost:7140", false });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdentityTokenLifetime",
                value: 3600);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 9, 20, 30, 52, 656, DateTimeKind.Utc).AddTicks(1865));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Origin",
                value: "https://localhost:6004");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "GrantType",
                value: "implicit");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostLogoutRedirectUri",
                value: "https://localhost:6004/");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "RedirectUri",
                value: "https://localhost:6004/");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Scope",
                value: "API");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 9, 20, 30, 52, 656, DateTimeKind.Utc).AddTicks(3668));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AlwaysIncludeUserClaimsInIdToken", "ClientUri", "RequirePkce" },
                values: new object[] { true, false, false, "https://localhost:6004", true });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdentityTokenLifetime",
                value: 300);
        }
    }
}
