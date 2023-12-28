using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projection.Common.ApiExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projection.ServiceDefaults;

public class HttpGlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<HttpGlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public HttpGlobalExceptionHandler(ILogger<HttpGlobalExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(new EventId(exception.HResult),
            exception,
            exception.Message);

        if (exception.GetType() == typeof(ApiDomainException))
        {
            var problemDetails = new ValidationProblemDetails()
            {
                Instance = context.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Detail = "Please refer to the errors property for additional details."
            };

            problemDetails.Errors.Add("DomainValidations", new string[] { exception.Message.ToString() });

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        }
        else
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
            };

            if (_env.IsDevelopment())
            {
                problemDetails.Detail = exception.StackTrace;
                problemDetails.Type = exception.GetType().Name;
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        }
        return true;
    }
}
