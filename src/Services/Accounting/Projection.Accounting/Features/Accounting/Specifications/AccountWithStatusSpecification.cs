using Projection.Accounting.Core.Entities;
using Projection.Common.Specifications;
using Projection.GlobalConstants;

namespace Projection.Accounting.Features.Accounting.Specifications;

public class AccountWithStatusSpecification : BaseSpecification<Account>
{
    public AccountWithStatusSpecification(int page, int pageSize = ResultView.DefaultPageSize) : base()
    {
        AddInclude(a => a.Status);
        AddInclude(a => a.Currency);
        ApplyPaging(page, pageSize);
        AddOrderByDescending(x => x.CreatedDate);
    }

    public AccountWithStatusSpecification(string id) : base(a => a.Id == id)
    {
        AddInclude(a => a.Status);
        AddInclude(a => a.Currency);
    }
}
