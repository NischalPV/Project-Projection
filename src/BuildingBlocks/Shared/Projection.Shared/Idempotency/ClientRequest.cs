using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.BuildingBlocks.Shared.Idempotency;

public class ClientRequest
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime Time { get; set; }
}
