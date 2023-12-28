using Projection.Accounting.Core.Entities;
using Projection.Accounting.Infrastructure.Data;

namespace Projection.Accounting.Features.Accounting.Interfaces;

public interface IAccountRepository: IBaseEntityAsyncRepository<Account, string, AccountingDbContext>
{
}
