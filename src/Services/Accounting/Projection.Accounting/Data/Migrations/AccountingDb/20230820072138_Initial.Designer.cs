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
    [Migration("20230820072138_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("accounting")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Projection.Accounting.Core.Account", b =>
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

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Accounts", "accounting");
                });

            modelBuilder.Entity("Projection.Accounting.Core.AccountTransaction", b =>
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

            modelBuilder.Entity("Projection.Accounting.Core.TransactionType", b =>
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
                            CreatedDate = new DateTime(2023, 8, 20, 7, 21, 38, 110, DateTimeKind.Utc).AddTicks(5240),
                            Description = "Credit transaction",
                            IsActive = true,
                            Multiplier = 1,
                            Name = "Credit",
                            StatusId = 13
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 8, 20, 7, 21, 38, 110, DateTimeKind.Utc).AddTicks(5250),
                            Description = "Debit transaction",
                            IsActive = true,
                            Multiplier = -1,
                            Name = "Debit",
                            StatusId = 13
                        });
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

            modelBuilder.Entity("Projection.Accounting.Core.Account", b =>
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

            modelBuilder.Entity("Projection.Accounting.Core.AccountTransaction", b =>
                {
                    b.HasOne("Projection.Accounting.Core.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "LastStatus")
                        .WithMany()
                        .HasForeignKey("LastStatusId");

                    b.HasOne("Projection.Common.BaseEntities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projection.Accounting.Core.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("LastStatus");

                    b.Navigation("Status");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Projection.Accounting.Core.TransactionType", b =>
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
#pragma warning restore 612, 618
        }
    }
}
