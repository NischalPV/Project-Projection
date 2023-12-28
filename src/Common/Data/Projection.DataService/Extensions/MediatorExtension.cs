namespace Projection.Common.DataService.Extensions;

// Mediator Extension
public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, BaseDbContext ctx)
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

        //var tasks = domainEvents
        //    .Select(async (domainEvent) =>
        //    {
        //        await mediator.Publish(domainEvent);
        //    });

        //await Task.WhenAll(tasks);
    }
}