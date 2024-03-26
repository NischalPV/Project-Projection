using Projection.Accounting.Core.Entities;
using Projection.BuildingBlocks.Shared.Commands;
using System.Runtime.Serialization;

namespace Projection.Accounting.Features.Accounting.Commands;

[DataContract]
public class AccountUpdateCommand: BaseCommand<Account>
{ 
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string GSTNumber { get; set; }

    [DataMember]
    public string PANNumber { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string ModifiedBy { get; private set; }

    [DataMember]
    private readonly List<PointOfContact> _contacts;

    [DataMember]
    public IEnumerable<PointOfContact> Contacts => _contacts;

    public AccountUpdateCommand()
    {
        _contacts = new List<PointOfContact>();
    }

    public AccountUpdateCommand(string id, string name, string gstNumber, string panNumber, string modifiedBy, string description, List<PointOfContact> contacts): this()
    {
        Id = id;
        Name = name;
        GSTNumber = gstNumber;
        PANNumber = panNumber;
        Description = description;
        ModifiedBy = modifiedBy;
        _contacts = contacts;
    }
}
