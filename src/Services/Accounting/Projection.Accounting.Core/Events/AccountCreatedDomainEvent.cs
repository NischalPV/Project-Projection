using MediatR;
using Projection.Accounting.Core.Aggregates;
using Projection.Accounting.Core.Entities;

namespace Projection.Accounting.Core.Events;

public record class AccountCreatedDomainEvent(Account account): INotification;
