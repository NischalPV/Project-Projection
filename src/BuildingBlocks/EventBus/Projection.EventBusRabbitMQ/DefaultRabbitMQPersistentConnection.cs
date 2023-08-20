using Microsoft.AspNetCore.Http;
using Projection.Common.BaseEntities;
using RabbitMQ.Client;

namespace Projection.BuildingBlocks.EventBusRabbitMQ;

public class DefaultRabbitMQPersistentConnection
       : IRabbitMQPersistentConnection
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _retryCount;
    IConnection _connection;
    bool _disposed;

    object sync_root = new object();

    public DefaultRabbitMQPersistentConnection(ConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger, int retryCount = 5)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _retryCount = retryCount;
        //_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public bool IsConnected
    {
        get
        {
            return _connection != null && _connection.IsOpen && !_disposed;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;

        try
        {
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
        }
    }

    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (sync_root)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                }
            );

            policy.Execute(() =>
            {
                //var settings = GetSettingsFromUserClaims();

                //_connectionFactory.HostName = settings.ConnectionString;
                //_connectionFactory.UserName = settings.Username;
                //_connectionFactory.Password = settings.Password;

                _connection = _connectionFactory
                      .CreateConnection();
            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }
    }

    public IModel CreateModel
    {
        get
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }
    }

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
    }

    void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
    }

    void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        TryConnect();
    }

    //private EventBusSettings GetSettingsFromUserClaims()
    //{
    //    var claims = _httpContextAccessor.HttpContext.User.Claims;
    //    var tenancyJson = claims.FirstOrDefault(c => c.Type == "TenancyJson")?.Value;

    //    if (string.IsNullOrEmpty(tenancyJson))
    //    {
    //        throw new Exception("TenancyJson claim is missing");
    //    }

    //    var tenancy = JsonConvert.DeserializeObject<TenancySettings>(tenancyJson);

    //    return tenancy.EventBusSettings;
    //}
}

