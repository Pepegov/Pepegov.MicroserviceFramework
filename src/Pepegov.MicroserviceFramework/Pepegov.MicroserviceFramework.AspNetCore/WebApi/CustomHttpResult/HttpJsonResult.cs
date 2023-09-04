using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType($"{MediaTypeNames.Application.Json}")]
public sealed class HttpJsonResult<T> : BaseHttpResult<T>
{
    public HttpJsonResult(T obj, HttpStatusCode statusCode) : base(obj, statusCode) { } 

    public override string? GetResponseMessage()
    {
        var message = GetMessage();
        if (message is null)
        {
            return null;
        }
        return JsonSerializer.Serialize(message);
    }
}



