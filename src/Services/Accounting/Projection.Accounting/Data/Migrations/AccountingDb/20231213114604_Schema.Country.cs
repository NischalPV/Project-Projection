using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projection.Accounting.Data.Migrations.AccountingDb
{
    /// <inheritdoc />
    public partial class SchemaCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyId",
                schema: "accounting",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    LastStatusId = table.Column<int>(type: "integer", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "text", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    NumericCode = table.Column<int>(type: "integer", nullable: true),
                    AlphabeticCode = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    LastStatusId = table.Column<int>(type: "integer", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "text", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "public",
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Currencies_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Currencies_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 13, 11, 46, 2, 608, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 13, 11, 46, 2, 608, DateTimeKind.Utc).AddTicks(4964));

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                schema: "accounting",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_LastStatusId",
                schema: "public",
                table: "Countries",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_StatusId",
                schema: "public",
                table: "Countries",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CountryId",
                schema: "public",
                table: "Currencies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_LastStatusId",
                schema: "public",
                table: "Currencies",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_StatusId",
                schema: "public",
                table: "Currencies",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                schema: "accounting",
                table: "Accounts",
                column: "CurrencyId",
                principalSchema: "public",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                schema: "accounting",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CurrencyId",
                schema: "accounting",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "accounting",
                table: "Accounts");

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 9, 20, 35, 38, 598, DateTimeKind.Utc).AddTicks(9240));

            migrationBuilder.UpdateData(
                schema: "accounting",
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 9, 20, 35, 38, 598, DateTimeKind.Utc).AddTicks(9245));
        }
    }
}
