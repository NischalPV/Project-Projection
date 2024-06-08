using Projection.Common.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.ProcessManagement.Core.Entities;

public record Process : BaseEntity<Guid>
{
    public bool IsMandatory { get; set; } = false;
    public bool IsAutomated { get; set; } = false;
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

    public Process()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public void UpdateAdditionalAttributes(string key, object value)
    {
        AdditionalAttributes.Add(key, value);
    }
}
