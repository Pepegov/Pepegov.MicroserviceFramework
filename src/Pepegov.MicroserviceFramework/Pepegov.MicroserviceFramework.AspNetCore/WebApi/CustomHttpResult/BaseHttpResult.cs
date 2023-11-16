using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

public abstract class BaseHttpResult<T> : IHttpResult
{
    protected HttpStatusCode StatusCode { get; private set; }
    protected ContextTypeValue? ContextTypeValue;
    private readonly T _message;
    private bool _bff;

    protected BaseHttpResult(T message, HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        _message = message;
    }
    
    public virtual async Task ExecuteAsync(HttpContext httpContext)
    {
        if (ContextTypeValue is null)
        {
            ContextTypeValue = HttpContextHelper.ParseContextType(httpContext);
        }
        
        //parse bff header is it exits
        var bffHeader = httpContext.Request.Headers["Bff"].FirstOrDefault();
        if (bffHeader is not null && bffHeader.ToLower().StartsWith("true"))
        {
            _bff = true;
        }

        var message = GetResponseMessage();
        
        httpContext.Response.ContentType = httpContext.Request.ContentType;
        httpContext.Response.StatusCode = (int)StatusCode;
        if (httpContext.Response.StatusCode <= 0)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Continue;
            return;
        }

        if (message is null)
        {
            return;
        }
        httpContext.Response.ContentLength = ContextTypeValue.Encoding!.GetByteCount(message);
        await httpContext.Response.WriteAsync(message, ContextTypeValue.Encoding, httpContext.RequestAborted);
    }

    protected object? GetMessage()
    {
        if (_bff && _message is ApiResult apiResult)
        {
            if (apiResult.Exceptions is not null)
            {
                StatusCode = HttpStatusCode.BadRequest;
                return apiResult.Exceptions.ToMinimalExceptionData();
            }
            
            var messageProperty = TryGetMessageProperty();
            if (messageProperty is not null)
            {
                var bffMessage = messageProperty.GetValue(_message);
                if (bffMessage is not null)
                {
                    return bffMessage;
                }
                else
                {
                    return GetGeneralApiException();
                }
            }
            else
            {
                StatusCode = HttpStatusCode.NoContent;
                return null;
            }
        }

        if (_message is null)
        {
            return GetGeneralApiException();
        }
        
        return _message;

        List<MinimalExceptionData> GetGeneralApiException()
        {
            StatusCode = HttpStatusCode.BadRequest;
            return new List<MinimalExceptionData> { new MinimalExceptionData()
            {
                ExceptionType = nameof(Exception),
                ExceptionMessage = "General api result exception"
            }};
        }
    }
    protected PropertyInfo? TryGetMessageProperty()
    {
        var entityType = typeof(T);
        PropertyInfo? key;
        key = entityType.GetProperties().FirstOrDefault(p => p
            .GetCustomAttribute(typeof(KeyAttribute)) is not null);
        if (key is not null)
        {
            return key;
        }

        return entityType.GetProperties().FirstOrDefault(p => p.Name == "Message");
    }

    public abstract string? GetResponseMessage();
}