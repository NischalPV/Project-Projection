﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projection.Accounting.Infrastructure.Data;

#nullable disable

namespace Projection.Accounting.Data.Migrations.AccountingDb
{
    [DbContext(typeof(AccountingDbContext))]
    partial class AccountingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("accounting")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrencyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GSTNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PANNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Accounts", "accounting");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.AccountTransaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Countries", "dbo");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.Currency", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlphabeticCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumericCode")
                        .HasColumnType("int");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Currencies", "dbo");
                });

            modelBuilder.Entity("Projection.Accounting.Core.Entities.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LastStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Multiplier")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StatusChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LastStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("TransactionTypes", "accounting");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 2, 23, 10, 19, 10, 781, DateTimeKind.Utc).AddTicks(6587),
                            Description = "Credit transaction",
                            IsActive = true,
                            Multiplier = 1,
                            Name = "Credit",
                            StatusId = 13
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 2, 23, 10, 19, 10, 781, DateTimeKind.Utc).AddTicks(6596),
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
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Requests", "accounting");
                });

            modelBuilder.Entity("Projection.Common.BaseEntities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses", "dbo");

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
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Notes")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .HasColumnType("nvarchar(max)");

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
