using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projection.Accounting.Data.Migrations.AccountingDb
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounting");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LastStatusId = table.Column<int>(type: "int", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Multiplier = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LastStatusId = table.Column<int>(type: "int", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionTypes_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionTypes_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumericCode = table.Column<int>(type: "int", nullable: true),
                    AlphabeticCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LastStatusId = table.Column<int>(type: "int", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Currencies_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Currencies_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LastStatusId = table.Column<int>(type: "int", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contacts = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "dbo",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LastStatusId = table.Column<int>(type: "int", nullable: true),
                    StatusChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalSchema: "accounting",
                        principalTable: "TransactionTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Statuses",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Draft" },
                    { 2, true, "WaitingForApproval" },
                    { 3, true, "Approved" },
                    { 4, true, "Published" },
                    { 5, true, "Executed" },
                    { 6, true, "Expired" },
                    { 7, true, "Renewed" },
                    { 8, true, "Terminated" },
                    { 9, true, "Cancelled" },
                    { 10, true, "Rejected" },
                    { 11, true, "Deleted" },
                    { 12, true, "Inactive" },
                    { 13, true, "Active" },
                    { 14, true, "InProgress" },
                    { 15, true, "Completed" },
                    { 16, true, "Pending" },
                    { 17, true, "Closed" },
                    { 18, true, "Open" },
                    { 19, true, "Reopened" }
                });

            migrationBuilder.InsertData(
                schema: "accounting",
                table: "TransactionTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "LastStatusId", "ModifiedBy", "ModifiedDate", "Multiplier", "Name", "StatusChangedBy", "StatusChangedDate", "StatusId", "UniqueIdentifier" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 2, 23, 10, 19, 10, 781, DateTimeKind.Utc).AddTicks(6587), "Credit transaction", true, null, null, null, 1, "Credit", null, null, 13, null },
                    { 2, null, new DateTime(2024, 2, 23, 10, 19, 10, 781, DateTimeKind.Utc).AddTicks(6596), "Debit transaction", true, null, null, null, -1, "Debit", null, null, 13, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                schema: "accounting",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastStatusId",
                schema: "accounting",
                table: "Accounts",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_StatusId",
                schema: "accounting",
                table: "Accounts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_AccountId",
                schema: "accounting",
                table: "AccountTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_LastStatusId",
                schema: "accounting",
                table: "AccountTransactions",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_StatusId",
                schema: "accounting",
                table: "AccountTransactions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_TransactionTypeId",
                schema: "accounting",
                table: "AccountTransactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_LastStatusId",
                schema: "dbo",
                table: "Countries",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_StatusId",
                schema: "dbo",
                table: "Countries",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CountryId",
                schema: "dbo",
                table: "Currencies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_LastStatusId",
                schema: "dbo",
                table: "Currencies",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_StatusId",
                schema: "dbo",
                table: "Currencies",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_LastStatusId",
                schema: "accounting",
                table: "TransactionTypes",
                column: "LastStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_StatusId",
                schema: "accounting",
                table: "TransactionTypes",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransactions",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "TransactionTypes",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "dbo");
        }
    }
}
