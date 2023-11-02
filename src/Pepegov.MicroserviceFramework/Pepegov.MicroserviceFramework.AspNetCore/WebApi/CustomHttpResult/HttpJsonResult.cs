using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType($"{MediaTypeNames.Application.Json}")]
public sealed class HttpJsonResult<T> : BaseHttpResult<T>
{
    private static readonly JsonSerializerOptions _jsonSerializerSettings = new ()
	{
		PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
	};

    public HttpJsonResult(T obj, HttpStatusCode statusCode) : base(obj, statusCode) { } 

    public override string? GetResponseMessage()
    {
        var message = GetMessage();
        if (message is null)
        {
            return null;
        }
        return JsonSerializer.Serialize(message, _jsonSerializerSettings);
    }
}



