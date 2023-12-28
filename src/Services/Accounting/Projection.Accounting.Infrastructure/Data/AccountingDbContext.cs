using MediatR;
using Microsoft.EntityFrameworkCore;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Infrastructure.EntityConfigurations;
using Projection.Accounting.Infrastructure.Extensions;
using Projection.Common.BaseEntities;
using Projection.Common.DataService.Contexts;
using Projection.Common.GlobalConstants;
using System.Linq.Expressions;

namespace Projection.Accounting.Infrastructure.Data;

public class AccountingDbContext : BaseDbContext
{
    #region Properties
    internal readonly IMediator _mediator;
    #endregion

    #region ctors
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("AccountingDbContext::ctor ->" + this.GetHashCode());
    }

    public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
    {
        System.Diagnostics.Debug.WriteLine("AccountingDbContext::ctor ->" + this.GetHashCode());
    }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.ACCOUNTING_SCHEMA);

        //ApplyGlobalQueryFilters(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
        Masterdata.SeedUsingMigration(modelBuilder);
    }

    public override async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountTransaction> AccountTransactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Country> Countries { get; set; }

}

