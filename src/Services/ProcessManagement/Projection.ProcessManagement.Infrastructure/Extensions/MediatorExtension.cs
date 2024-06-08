using MediatR;
using Projection.Common.BaseEntities;
using Projection.ProcessManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.ProcessManagement.Infrastructure.Extensions;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ProcessManagementDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<IBaseEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
