using MediatR;
using Microsoft.EntityFrameworkCore;
using Projection.Common.DataService.Contexts;
using Projection.Common.GlobalConstants;
using Projection.ProcessManagement.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.ProcessManagement.Infrastructure.Data;

public class ProcessManagementDbContext : BaseDbContext
{
    #region Properties
    internal readonly IMediator _mediator;
    #endregion

    #region ctors
    public ProcessManagementDbContext(DbContextOptions<ProcessManagementDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("ProcessManagementDbContext::ctor ->" + this.GetHashCode());
    }

    public ProcessManagementDbContext(DbContextOptions<ProcessManagementDbContext> options) : base(options)
    {
        System.Diagnostics.Debug.WriteLine("ProcessManagementDbContext::ctor ->" + this.GetHashCode());
    }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.PROCESS_MANAGEMENT_SCHEMA);

        //ApplyGlobalQueryFilters(modelBuilder);

        base.OnModelCreating(modelBuilder);
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
}
