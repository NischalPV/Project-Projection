using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Identity.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "public",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFbXOBuLXlKD8duNXcLajnz+nvHGoPa054dWQK8RrxQtJo75IpLzkQ0FiWdC9i2Q7w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "public",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDj8STA+HdJCj6HotQOLrRZ0HzhnEqgOfloDax8fLMuMQI3w55/WLTSOo4pUcrR46Q==");
        }
    }
}
