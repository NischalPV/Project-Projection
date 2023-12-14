using MediatR;
using Projection.Accounting.Commands;
using Projection.Accounting.Core.Entities;
using Projection.BuildingBlocks.Shared.Commands;
using System.Runtime.Serialization;

namespace Projection.Accounting.Features.Accounting.Commands;

[DataContract]
public class AccountCreateCommand :  BaseCommand<Account>
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string AccountNumber { get; set; }

    [DataMember]
    public string GSTNumber { get; set; }

    [DataMember]
    public string PANNumber { get; set; }

    [DataMember]
    public string CurrencyId { get; set; }

    [DataMember]
    public double Balance { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string CreatedBy { get; private set; }

    public AccountCreateCommand(string name, string accountNumber, string gstNumber, string panNumber, string currencyId, double balance, string createdBy, string description)
    {
        Name = name;
        AccountNumber = accountNumber;
        GSTNumber = gstNumber;
        PANNumber = panNumber;
        CurrencyId = currencyId;
        Balance = balance;
        Description = description;
        CreatedBy = createdBy;
        Id = Guid.NewGuid().ToString();
    }
}
