using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projection.Identity.Data.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "TenancyId", "18ce327c-720e-4c87-b0b1-756eabb37c7b", "c0aab6ba-cd71-4010-a9dc-e246997d6183" },
                    { 2, "TenantName", "Tenant-1", "c0aab6ba-cd71-4010-a9dc-e246997d6183" },
                    { 3, "AccountingConnectionString", "Host=192.168.1.19;Port=5432;Database=Projection.Accounting.Dev.Tenant1;User Id=sa;Password=Radeon1GB#;", "c0aab6ba-cd71-4010-a9dc-e246997d6183" }
                });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEK2NUwCuzOAJqLIM4Bl5EVerTTMBr432XKE4VXvcg5M60TzWHPMb/ENDUg/gq+fh1g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFbXOBuLXlKD8duNXcLajnz+nvHGoPa054dWQK8RrxQtJo75IpLzkQ0FiWdC9i2Q7w==");
        }
    }
}
