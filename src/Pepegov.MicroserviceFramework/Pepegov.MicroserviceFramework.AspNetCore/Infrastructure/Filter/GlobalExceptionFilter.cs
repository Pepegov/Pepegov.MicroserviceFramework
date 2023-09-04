using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Pepegov.MicroserviceFramework.AspNetCore.Infrastructure.Filter;

public class GlobalExceptionFilter: IAsyncExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        var message =
            $"Exception of type {context.Exception.GetType().Name} was throw in {context.ActionDescriptor.DisplayName}. " +
            $"Message: {context.Exception.Message}. DateTime: {System.DateTime.UtcNow}. User: {context.HttpContext.User.Identity?.Name}";
        
        _logger.LogError(message);
        
        context.ExceptionHandled = true;
        context.Result = new ContentResult 
        {
            Content = message,
            StatusCode = 400
        };

        return Task.CompletedTask;
    }
}