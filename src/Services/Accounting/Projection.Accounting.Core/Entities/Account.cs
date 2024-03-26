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

        AccountNumber = AccountNumberGenerator(15);
    }

    private void AddAccountCreatedDomainEvent()
    {
        var accountCreatedDomainEvent = new AccountCreatedDomainEvent(this);
        this.AddDomainEvent(accountCreatedDomainEvent);
    }

    private static Random random = new Random();

    private static string AccountNumberGenerator(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

