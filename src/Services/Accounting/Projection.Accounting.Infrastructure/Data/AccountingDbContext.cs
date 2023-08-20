using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Projection.Accounting.Core.Entities;
using Projection.Common.DataService.Contexts;

namespace Projection.Accounting.Infrastructure.Data;

public class AccountingDbContext : BaseDbContext
{
    #region Properties
    internal readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    #region ctors
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options, IHttpContextAccessor httpContextAccessor, IMediator mediator) : base(options)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine($"{this.GetType().Name}::ctor ->" + this.GetHashCode());
    }

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
        modelBuilder.HasDefaultSchema(Common.GlobalConstants.Schema.ACCOUNTING_SCHEMA);
        base.OnModelCreating(modelBuilder);
        Masterdata.SeedUsingMigration(modelBuilder);
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountTransaction> AccountTransactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
}

