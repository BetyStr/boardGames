using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Middleware;

public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddleware> _logger;
    private readonly string _projectInfo;

    public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger, string projectInfo)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _projectInfo = projectInfo;
    }

    public async Task Invoke(HttpContext context)
    {
        var logText = $"[{_projectInfo} at {DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}";
        _logger.LogInformation(logText);

        await _next(context);
    }
}