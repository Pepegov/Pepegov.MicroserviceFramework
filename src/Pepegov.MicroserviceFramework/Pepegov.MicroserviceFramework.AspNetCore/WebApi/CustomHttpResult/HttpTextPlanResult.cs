using System.Net;
using System.Net.Mime;
using System.Text;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType(MediaTypeNames.Text.Plain)]
public class HttpTextPlanResult<T> : BaseHttpResult<T>
{
    public HttpTextPlanResult(T obj, HttpStatusCode statusCode) : base(obj, statusCode) { }
    
    public HttpTextPlanResult(T message, HttpStatusCode statusCode, bool isDefaultContext = false) : base(message, statusCode)
    {
        if (isDefaultContext)
        {
            base.ContextTypeValue = new()
            {
                Type = MediaTypeNames.Text.Plain,
                Encoding = Encoding.UTF8,
            };
        }
    }
    public override string? GetResponseMessage()
    {
        return GetMessage()?.ToString();
    }
}
