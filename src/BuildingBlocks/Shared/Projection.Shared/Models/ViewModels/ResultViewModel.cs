using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.BuildingBlocks.Shared.Models.ViewModels;

public record ResultViewModel<T>
{
    public bool IsSuccess { get; set; }
    public ResultMessage Result { get; set; }
    public T Data { get; set; }
    public int TotalCount { get; set; }
    public int PageCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public record ResultMessage
{
    public string Message { get; set; }
    public int Code { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Field { get; set; }
}
