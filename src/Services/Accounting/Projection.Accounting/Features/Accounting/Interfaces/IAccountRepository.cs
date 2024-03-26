using Projection.Accounting.Core.Entities;
using Projection.Accounting.Infrastructure.Data;
using Projection.BuildingBlocks.Shared.Entities;
using Currency = Projection.Accounting.Core.Entities.Currency;

namespace Projection.Accounting.Features.Accounting.Interfaces;

public interface IAccountRepository: IBaseEntityAsyncRepository<Account, string, AccountingDbContext>
{
    Task<Currency> GetCurrencyAsync(string alphabeticCode);
}
