using MassTransit;
using Projection.BuildingBlocks.EventBus.Events;

namespace Projection.Accounting.Features.Accounting.BackgroundTasks
{
    public abstract class BaseConsumer<T> : IConsumer<T> where T : IntegrationEvent
    {
        public virtual Task Consume(ConsumeContext<T> context)
        {
            throw new NotImplementedException();
        }
    }
}
