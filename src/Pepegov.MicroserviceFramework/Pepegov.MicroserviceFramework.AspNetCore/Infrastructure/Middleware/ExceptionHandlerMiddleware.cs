using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Pepegov.MicroserviceFramework.AspNetCore.Infrastructure.Middleware;

public sealed class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            try
            {
                _logger.LogError($"Exception {exception.Message}");
            }
            catch (Exception innerException)
            {
                _logger.LogError(0, innerException, "Exception handler error");
            }
            throw;
        }
    }
}
