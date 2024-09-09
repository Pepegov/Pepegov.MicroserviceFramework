using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

    private static readonly JsonSerializerOptions DefaultJsonSerializerSettings = new()
    {
        PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
    };

    public override string? GetResponseMessage(HttpContext httpContext)
    {
        var message = GetMessage();
        if (message is null)
        {
            return null;
        }

        JsonSerializerOptions options = DefaultJsonSerializerSettings;
        var jsonOptions = httpContext.RequestServices.GetService<IOptions<JsonOptions>>();
        if (jsonOptions is not null)
        {
            options = jsonOptions.Value.SerializerOptions;
        }
        
        return JsonSerializer.Serialize(message,  options);
    }
}



