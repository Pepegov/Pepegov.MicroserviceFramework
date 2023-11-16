using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType($"{MediaTypeNames.Application.Json}")]
public sealed class HttpJsonResult<T> : BaseHttpResult<T>
{
    public HttpJsonResult(T obj, HttpStatusCode statusCode) : base(obj, statusCode) { }
    
    public HttpJsonResult(T obj, HttpStatusCode statusCode, bool isDefaultContext = false) : base(obj, statusCode)
    {
        if (isDefaultContext)
        {
            base.ContextTypeValue = new()
            {
                Type = MediaTypeNames.Application.Json,
                Encoding = Encoding.UTF8,
            };
        }
    }

    private static readonly JsonSerializerOptions _jsonSerializerSettings = new()
    {
        PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
    };

    public override string? GetResponseMessage()
    {
        var message = GetMessage();
        if (message is null)
        {
            return null;
        }
        return JsonSerializer.Serialize(message,  _jsonSerializerSettings);
    }
}



