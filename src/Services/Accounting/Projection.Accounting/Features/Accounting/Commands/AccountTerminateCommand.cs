using Projection.BuildingBlocks.Shared.Commands;
using System.Runtime.Serialization;

namespace Projection.Accounting.Features.Accounting.Commands;

[DataContract]
public class AccountTerminateCommand: BaseCommand<string>
{
    public AccountTerminateCommand()
    {
    }

    public AccountTerminateCommand(string id): this()
    {
        Id = id;
    }
}
