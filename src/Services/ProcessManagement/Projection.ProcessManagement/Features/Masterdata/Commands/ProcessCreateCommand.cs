using Projection.BuildingBlocks.Shared.Commands;
using Projection.ProcessManagement.Core.Entities;
using System.Runtime.Serialization;

namespace Projection.ProcessManagement.Features.Masterdata.Commands;

[DataContract]
public class ProcessCreateCommand : BaseCommand<Process>
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string CreatedBy { get; set; }

    [DataMember]
    public bool IsMandatory { get; set; } = false;
    [DataMember]
    public bool IsAutomated { get; set; } = false;
    [DataMember]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

    public ProcessCreateCommand()
    {
    }

    public ProcessCreateCommand(string name, string description, string createdBy)
    {
        Name = name;
        Description = description;
        CreatedBy = createdBy;
        Id = Guid.NewGuid().ToString();
    }
}
