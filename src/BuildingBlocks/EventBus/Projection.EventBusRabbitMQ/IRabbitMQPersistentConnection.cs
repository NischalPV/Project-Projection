using RabbitMQ.Client;
namespace Projection.BuildingBlocks.EventBusRabbitMQ;
public interface IRabbitMQPersistentConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel { get; }
}