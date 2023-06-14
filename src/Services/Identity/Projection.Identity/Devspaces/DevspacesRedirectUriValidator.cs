using Duende.IdentityServer.Validation;
using Client = Duende.IdentityServer.Models.Client;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Projection.Identity.Devspaces;

public class DevspacesRedirectUriValidator : IRedirectUriValidator
{
    private readonly ILogger _logger;
    public DevspacesRedirectUriValidator(ILogger<DevspacesRedirectUriValidator> logger)
    {
        _logger = logger;
    }

    public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
    {
        _logger.LogDebug($"IsPostLogoutRedirectUriValidAsync called for {requestedUri} from Client {client.ClientName}");
        return Task.FromResult(true);
    }

    public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
    {
        _logger.LogDebug($"IsRedirectUriValidAsync called for {requestedUri} from Client {client.ClientName}");
        return Task.FromResult(true);
    }
}