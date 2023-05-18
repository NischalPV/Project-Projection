using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Projection.Common.ActionResults;

public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object error) : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
