using Projection.Accounting.Infrastructure.Data;
using Projection.BuildingBlocks.Shared.Exceptions;
using Projection.BuildingBlocks.Shared.Idempotency;

namespace Projection.Accounting.Idempotency;

public class RequestManager : IRequestManager
{
    private readonly AccountingDbContext _context;

    public RequestManager(AccountingDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateRequestForCommandAsync<T>(Guid id)
    {
        var exists = await ExistAsync(id);

        var request = exists ?
            throw new DomainException($"Request with {id} already exists") :
            new ClientRequest()
            {
                Id = id,
                Name = typeof(T).Name,
                Time = DateTime.UtcNow
            };

        _context.Add(request);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        var request = await _context.
            FindAsync<ClientRequest>(id);

        return request != null;
    }
}
