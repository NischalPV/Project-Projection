using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projection.Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Infrastructure.EntityConfigurations;

internal class AccountEntityTypeConfiguration: IEntityTypeConfiguration<Account>
{

    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.Ignore(b => b.DomainEvents);

        builder.HasQueryFilter(x => x.IsActive);

        builder.OwnsMany(x => x.Contacts).ToJson("Contacts");
        builder.HasMany(x => x.Transactions).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.Cascade);

    }
}   