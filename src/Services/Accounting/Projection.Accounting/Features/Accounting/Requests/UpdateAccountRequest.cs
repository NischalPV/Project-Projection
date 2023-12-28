using Projection.Accounting.Core.Entities;

namespace Projection.Accounting.Features.Accounting.Requests;

public record UpdateAccountRequest
{
    public string Id { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public List<PointOfContact> Contacts { get; set; }
}
