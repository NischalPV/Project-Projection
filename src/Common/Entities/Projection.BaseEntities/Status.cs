using Projection.Common.GlobalConstants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projection.Common.BaseEntities;


[Table("Statuses", Schema = Schema.DEFAULT_SCHEMA)]
public record Status
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public Status()
    {
        IsActive = true;
    }
}

public enum StatusEnum
{
    Draft = 1,
    WaitingForApproval = 2,
    Approved = 3,
    Published = 4,
    Executed = 5,
    Expired = 6,
    Renewed = 7,
    Terminated = 8,
    Cancelled = 9,
    Rejected = 10,
    Deleted = 11,
    Inactive = 12,
    Active = 13,
    InProgress = 14,
    Completed = 15,
    Pending = 16,
    Closed = 17,
    Open = 18,
    Reopened = 19

}