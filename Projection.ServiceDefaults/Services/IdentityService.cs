using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.ServiceDefaults.Services;

public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string GetUserIdentity()
        => context.HttpContext?.User.FindFirst("sub")?.Value;

    public string GetUserName()
        => context.HttpContext?.User.Identity?.Name;
}
