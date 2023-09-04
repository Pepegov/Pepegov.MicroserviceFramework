using System.Net;
using System.Net.Mime;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType(MediaTypeNames.Text.Plain)]
public class HttpTextPlanResult<T> : BaseHttpResult<T>
{
    public HttpTextPlanResult(T message, HttpStatusCode statusCode) : base(message, statusCode) { }

    public override string? GetResponseMessage()
    {
        return GetMessage()?.ToString();
    }
}
