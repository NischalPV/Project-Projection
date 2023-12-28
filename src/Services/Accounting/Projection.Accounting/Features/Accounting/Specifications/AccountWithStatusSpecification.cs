using Projection.Accounting.Core.Entities;
using Projection.Common.Specifications;

namespace Projection.Accounting.Features.Accounting.Specifications;

public class AccountWithStatusSpecification : BaseSpecification<Account>
{
    public AccountWithStatusSpecification() : base()
    {
        AddInclude(a => a.Status);
        AddInclude(a => a.Currency);
    }

    public AccountWithStatusSpecification(string id) : base(a => a.Id == id)
    {
        AddInclude(a => a.Status);
        AddInclude(a => a.Currency);
    }
}
