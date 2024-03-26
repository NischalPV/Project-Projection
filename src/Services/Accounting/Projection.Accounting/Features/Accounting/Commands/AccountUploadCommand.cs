using Projection.Accounting.Core.Entities;
using Projection.BuildingBlocks.Shared.Commands;
using System.Runtime.Serialization;
using static Projection.Accounting.Features.Accounting.Commands.AccountUploadCommandHandler;

namespace Projection.Accounting.Features.Accounting.Commands;

[DataContract]
public class AccountUploadCommand : BaseCommand<bool>
{
    [DataMember]
    public string AccountsFile { get; set; }

    public AccountUploadCommand(string file)
    {
        Id = Guid.NewGuid().ToString();
        AccountsFile = file;

    }
}
