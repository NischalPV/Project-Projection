using Projection.Accounting.Core.Entities;
using Projection.Common.Specifications;

namespace Projection.Accounting.Features.Accounting.Specifications;

public class AccountWithGSTNumbersSpecification: BaseSpecification<Account>
{

    public AccountWithGSTNumbersSpecification(): base() { }

    public AccountWithGSTNumbersSpecification(List<string> gstNumbers) : base(x => gstNumbers.Contains(x.GSTNumber))
    {

    }
}
