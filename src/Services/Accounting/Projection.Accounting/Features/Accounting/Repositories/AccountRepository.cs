using Microsoft.EntityFrameworkCore;
using Projection.Accounting.Core.Entities;
using Projection.Accounting.Features.Accounting.Interfaces;
using Projection.Accounting.Infrastructure.Data;

namespace Projection.Accounting.Features.Accounting.Repositories;

public class AccountRepository : BaseEntityEfRepository<Account, string, AccountingDbContext>, IAccountRepository
{

    private readonly ILogger<AccountRepository> _logger;

    public AccountRepository(AccountingDbContext ctx, ILogger<AccountRepository> logger) : base(ctx, logger)
    {
        _logger = logger;
    }
}
