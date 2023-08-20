using System;
using Microsoft.EntityFrameworkCore;
using Projection.Accounting.Core;
using Projection.Accounting.Core.Entities;
using Projection.Common.BaseEntities;

namespace Projection.Accounting.Infrastructure.Data
{
    public class Masterdata
    {
        /// <summary>
        /// This method is to be used in AccountingDbContext, gets invoked in OnModelCreating override.
        /// Generates migration and keeps track of changes in master data
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedUsingMigration(ModelBuilder modelBuilder)
        {
            DefaultStatuses(modelBuilder);
            DefaultTransactionTypes(modelBuilder);
        }

        private static void DefaultStatuses(ModelBuilder modelBuilder)
        {
            List<Status> statuses = new();

            // iterate over StatusEnum
            foreach (var status in Enum.GetValues(typeof(StatusEnum)))
            {
                statuses.Add(new Status()
                {
                    Name = status.ToString(),
                    Id = (int)status,
                    IsActive = true
                });
            }

            foreach (var status in statuses)
            {
                modelBuilder.Entity<Status>().HasData(status);
            }
        }

        private static void DefaultTransactionTypes(ModelBuilder modelBuilder)
        {
            List<TransactionType> transactionTypes = new();

            transactionTypes.AddRange(new List<TransactionType>
            {
                new TransactionType(id: 1)
                {
                    Name = "Credit",
                    Multiplier = 1,
                    Description = "Credit transaction",
                    StatusId = (int)StatusEnum.Active,
                    IsActive = true
                },
                new TransactionType(id: 2)
                {
                    Name = "Debit",
                    Multiplier = -1,
                    Description = "Debit transaction",
                    StatusId = (int)StatusEnum.Active,
                    IsActive = true
                }
            });

            foreach (var transactionType in transactionTypes)
            {
                modelBuilder.Entity<TransactionType>().HasData(transactionType);
            }
        }

    }
}

