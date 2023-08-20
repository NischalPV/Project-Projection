using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "public");

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: true),
                    GSTNumber = table.Column<string>(type: "text", nullable: true),
                    PANNumber = table.Column<string>(type: "text", nullable: true),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
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
                    StatusChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Multiplier = table.Column<int>(type: "integer", nullable: false),
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
                    StatusChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionTypes_Statuses_LastStatusId",
                        column: x => x.LastStatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionTypes_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                schema: "accounting",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "integer", nullable: false),
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
                    StatusChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
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
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "public",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalSchema: "accounting",
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
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
                    { 1, null, new DateTime(2023, 8, 20, 7, 21, 38, 110, DateTimeKind.Utc).AddTicks(5240), "Credit transaction", true, null, null, null, 1, "Credit", null, null, 13, null },
                    { 2, null, new DateTime(2023, 8, 20, 7, 21, 38, 110, DateTimeKind.Utc).AddTicks(5250), "Debit transaction", true, null, null, null, -1, "Debit", null, null, 13, null }
                });

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
                name: "Accounts",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "TransactionTypes",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "public");
        }
    }
}
