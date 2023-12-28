using Projection.Accounting.Core.Events;
using Projection.Common.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projection.Accounting.Core.Entities;

public record Account : BaseEntity<string>
{
    public string AccountNumber { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public double Balance { get; set; }

    [ForeignKey(nameof(Currency))]
    public string CurrencyId { get; set; }

    public IList<AccountTransaction> Transactions { get; set; }

    public IList<PointOfContact> Contacts { get; set; }

    public virtual Currency Currency { get; set; }

    public Account()
    {
        Id = Guid.NewGuid().ToString();
        CreatedDate = DateTime.UtcNow;

        AddAccountCreatedDomainEvent();
    }

    private void AddAccountCreatedDomainEvent()
    {
        var accountCreatedDomainEvent = new AccountCreatedDomainEvent(this);
        this.AddDomainEvent(accountCreatedDomainEvent);
    }
}

