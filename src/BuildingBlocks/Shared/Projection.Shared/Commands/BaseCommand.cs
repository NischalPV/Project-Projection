using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Projection.BuildingBlocks.Shared.Commands;

[DataContract]
public class BaseCommand<R>: IRequest<R>
{
    [DataMember]
    public string Id { get; set; }
}
