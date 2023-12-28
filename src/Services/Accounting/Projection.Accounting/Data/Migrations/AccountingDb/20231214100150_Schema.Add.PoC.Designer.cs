﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Projection.Accounting.Infrastructure.Data;

#nullable disable

namespace Projection.Accounting.Data.Migrations.AccountingDb
{
    [DbContext(typeof(AccountingDbContext))]
    [Migration("20231214100150_Schema.Add.PoC")]
    partial class SchemaAddPoC
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("accounting")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("text");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrencyId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("GSTNumber")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PANNumber")
                        .HasColumnType("text");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Accounts", "accounting");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.AccountTransaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AccountId")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("AccountTransactions", "accounting");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Country", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Countries", "public");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Currency", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AlphabeticCode")
                        .HasColumnType("text");

                    b.Property<string>("CountryId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("NumericCode")
                        .HasColumnType("integer");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Currencies", "public");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Multiplier")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("TransactionTypes", "accounting");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 12, 14, 10, 1, 47, 960, DateTimeKind.Utc).AddTicks(904),
                            Description = "Credit transaction",
                            IsActive = true,
                            Multiplier = 1,
                            Name = "Credit",
                            StatusId = 13
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 12, 14, 10, 1, 47, 960, DateTimeKind.Utc).AddTicks(909),
                            Description = "Debit transaction",
                            IsActive = true,
                            Multiplier = -1,
                            Name = "Debit",
                            StatusId = 13
                        });
                });

            modelBuilder.Entity("Projection.BuildingBlocks.Shared.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Requests", "accounting");
                });

            modelBuilder.Entity("Projection.Common.BaseEntities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Statuses", "public");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Draft"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "WaitingForApproval"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Name = "Published"
                        },
                        new
                        {
                            Id = 5,
                            IsActive = true,
                            Name = "Executed"
                        },
                        new
                        {
                            Id = 6,
                            IsActive = true,
                            Name = "Expired"
                        },
                        new
                        {
                            Id = 7,
                            IsActive = true,
                            Name = "Renewed"
                        },
                        new
                        {
                            Id = 8,
                            IsActive = true,
                            Name = "Terminated"
                        },
                        new
                        {
                            Id = 9,
                            IsActive = true,
                            Name = "Cancelled"
                        },
                        new
                        {
                            Id = 10,
                            IsActive = true,
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 11,
                            IsActive = true,
                            Name = "Deleted"
                        },
                        new
                        {
                            Id = 12,
                            IsActive = true,
                            Name = "Inactive"
                        },
                        new
                        {
                            Id = 13,
                            IsActive = true,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 14,
                            IsActive = true,
                            Name = "InProgress"
                        },
                        new
                        {
                            Id = 15,
                            IsActive = true,
                            Name = "Completed"
                        },
                        new
                        {
                            Id = 16,
                            IsActive = true,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 17,
                            IsActive = true,
                            Name = "Closed"
                        },
                        new
                        {
                            Id = 18,
                            IsActive = true,
                            Name = "Open"
                        },
                        new
                        {
                            Id = 19,
                            IsActive = true,
                            Name = "Reopened"
                        });
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Account", b =>
                {
                    b.HasOne("Projection.Accounting.Core.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "LastStatus")
                        .WithMany()
                        .HasForeignKey("LastStatusId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Projection.Accounting.Core.Entities.PointOfContact", "Contacts", b1 =>
                        {
                            b1.Property<string>("AccountId")
                                .HasColumnType("text");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Email")
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.Property<string>("Notes")
                                .HasColumnType("text");

                            b1.Property<string>("Phone")
                                .HasColumnType("text");

                            b1.HasKey("AccountId", "Id");

                            b1.ToTable("Accounts", "accounting");

                            b1.ToJson("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("Contacts");

                    b.Navigation("Currency");

                    b.Navigation("LastStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.AccountTransaction", b =>
                {
                    b.HasOne("Projection.Accounting.Core.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Projection.Common.BaseEntities.Status", "LastStatus")
                        .WithMany()
                        .HasForeignKey("LastStatusId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projection.Accounting.Core.Entities.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("LastStatus");

                    b.Navigation("Status");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Country", b =>
                {
                    b.HasOne("Projection.Common.BaseEntities.Status", "LastStatus")
                        .WithMany()
                        .HasForeignKey("LastStatusId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LastStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Currency", b =>
                {
                    b.HasOne("Projection.Accounting.Core.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "LastStatus")
                        .WithMany()
                        .HasForeignKey("LastStatusId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("LastStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.TransactionType", b =>
                {
                    b.HasOne("Projection.Common.BaseEntities.Status", "LastStatus")
                        .WithMany()
                        .HasForeignKey("LastStatusId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LastStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}