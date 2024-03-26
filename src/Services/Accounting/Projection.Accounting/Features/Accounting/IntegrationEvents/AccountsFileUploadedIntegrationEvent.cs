using Google.Apis.Storage.v1.Data;
using Projection.BuildingBlocks.EventBus.Events;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace Projection.Accounting.Features.Accounting.IntegrationEvents;

public record AccountsFileUploadedIntegrationEvent: IntegrationEvent
{
    public string FileName { get; set; }
    public string BucketName { get; set; }
    public string UploadedBy { get; set; }
    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
    public Object? FileObject { get; set; }
}
