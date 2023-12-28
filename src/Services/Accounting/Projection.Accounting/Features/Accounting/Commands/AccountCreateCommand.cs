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
    public string AccountNumber { get; private set; }

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

    [DataMember]
    private readonly List<PointOfContact> _contacts;

    [DataMember]
    public IEnumerable<PointOfContact> Contacts => _contacts;

    public AccountCreateCommand()
    {
        _contacts = new List<PointOfContact>();
    }

    public AccountCreateCommand(string name, string gstNumber, string panNumber, string currencyId, double balance, string createdBy, string description, List<PointOfContact> contacts): this()
    {
        Name = name;
        GSTNumber = gstNumber;
        PANNumber = panNumber;
        CurrencyId = currencyId;
        Balance = balance;
        Description = description;
        CreatedBy = createdBy;
        Id = Guid.NewGuid().ToString();
        _contacts = contacts;
        AccountNumber = AccountNumberGenerator(15);
    }


    private static Random random = new Random();

    private static string AccountNumberGenerator(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
