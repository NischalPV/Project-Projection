using Projection.Accounting.Core.Entities;
using Projection.Accounting.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Accounting.Core.Aggregates;

public record class AccountAggregate: Account, IAggregateRoot
{
    public AccountAggregate(): base()
    {
        AddAccountCreatedDomainEvent();
    }

    private void AddAccountCreatedDomainEvent()
    {
        var accountCreatedDomainEvent = new AccountCreatedDomainEvent(this);
        this.AddDomainEvent(accountCreatedDomainEvent);
    }
}
