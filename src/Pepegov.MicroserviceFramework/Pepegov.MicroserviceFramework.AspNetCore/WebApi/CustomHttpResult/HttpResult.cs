using System.Net;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

public class HttpResult<T> : IResult
{
    private readonly T _obj;
    private readonly HttpStatusCode _statusCode;
    
    public HttpResult(T obj, HttpStatusCode statusCode)
    {
        _obj = obj;
        _statusCode = statusCode;
    }
    
    public HttpResult(ApiResult<T> obj)
    {
        _obj = obj.Message!;
        _statusCode = (HttpStatusCode)obj.StatusCode;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        ContextTypeValue contextTypeValue;
        if (httpContext.Request.ContentType is null)
        {
            contextTypeValue = ContextTypeValue.Standart;
        }
        else
        {
            contextTypeValue = HttpContextHelper.ParseContextType(httpContext);
        }

        if (!HttpResultParser.TryParse(contextTypeValue.Type, out var httpResultType))
        {
            throw new KeyNotFoundException($"Not found {nameof(IHttpResult)} instance by type = {contextTypeValue.Type}");
        }

        Type constructedType = httpResultType.MakeGenericType(typeof(T));
        var httpResultInstance = (IHttpResult)Activator.CreateInstance(constructedType, new object[] { _obj, _statusCode });
        ArgumentNullException.ThrowIfNull(httpResultInstance);

        await httpResultInstance.ExecuteAsync(httpContext);
    }
}