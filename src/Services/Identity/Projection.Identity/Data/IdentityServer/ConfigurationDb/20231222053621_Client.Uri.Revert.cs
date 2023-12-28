using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class ClientUriRevert : Migration
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
                value: new DateTime(2023, 12, 22, 5, 36, 17, 955, DateTimeKind.Utc).AddTicks(3225));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Origin",
                value: "https://localhost:7140");

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
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 22, 5, 36, 17, 955, DateTimeKind.Utc).AddTicks(4358));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 12, 22, 5, 36, 17, 955, DateTimeKind.Utc).AddTicks(4360));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClientUri",
                value: "https://localhost:7140");
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
                value: new DateTime(2023, 12, 21, 18, 3, 52, 70, DateTimeKind.Utc).AddTicks(2906));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Origin",
                value: "http://localhost:5194");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostLogoutRedirectUri",
                value: "http://localhost:5194/signout-callback-oidc");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "RedirectUri",
                value: "http://localhost:5194/signin-oidc");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 21, 18, 3, 52, 70, DateTimeKind.Utc).AddTicks(4198));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 12, 21, 18, 3, 52, 70, DateTimeKind.Utc).AddTicks(4199));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClientUri",
                value: "http://localhost:5194");
        }
    }
}
