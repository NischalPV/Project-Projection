using Projection.Accounting.Core.Entities;
using Projection.BuildingBlocks.Shared.Commands;
using System.Runtime.Serialization;

namespace Projection.Accounting.Features.Accounting.Commands;

[DataContract]
public class AccountUploadCommand
{
    [DataMember]
    public IFormFile AccountsFile { get; set; }

    [DataMember]
    public string Id { get; set; }

    public AccountUploadCommand(IFormFile file)
    {
        Id = Guid.NewGuid().ToString();
        AccountsFile = file;

    }
}
