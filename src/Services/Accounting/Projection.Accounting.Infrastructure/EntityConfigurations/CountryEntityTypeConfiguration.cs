using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projection.Accounting.Core.Entities;
using Projection.Common.GlobalConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Infrastructure.EntityConfigurations;

internal class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {

        builder.ToTable("Countries", Schema.DEFAULT_SCHEMA);
        
        builder.HasQueryFilter(x => x.IsActive);
    }
}

internal class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies", Schema.DEFAULT_SCHEMA);

        builder.HasQueryFilter(x => x.IsActive);
    }
}

