using Projection.Accounting.Core.Entities;
using Projection.Common.Specifications;

namespace Projection.Accounting.Features.Accounting.Specifications;

public class AccountWithTransactionsSpecification : BaseSpecification<Account>
{
    public AccountWithTransactionsSpecification() : base()
    {
        AddInclude(a => a.Transactions);
    }

    public AccountWithTransactionsSpecification(string id) : base(a => a.Id == id)
    {
        AddInclude(a => a.Transactions);
    }
}
