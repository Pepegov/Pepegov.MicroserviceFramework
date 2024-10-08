using System.Net;
using System.Net.Mime;
using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;
using Pepegov.MicroserviceFramework.AspNetCore.Infrastructure;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType(MediaTypeNames.Application.Xml)]
public sealed class HttpXmlResult<T> : BaseHttpResult<T>
{ 
    public HttpXmlResult(T obj, HttpStatusCode statusCode) : base(obj, statusCode) { }
    
    public HttpXmlResult(T obj, HttpStatusCode statusCode, bool isDefaultContext = false) : base(obj, statusCode)
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

    public override string? GetResponseMessage(HttpContext httpContext)
    {
        var message = GetMessage();
        if (message is null)
        {
            return null;
        }
        
        ArgumentNullException.ThrowIfNull(ContextTypeValue);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("","");

        using (StringWriter sw = new StringWriterWithEncoding(ContextTypeValue.Encoding))
        {
            xmlSerializer.Serialize(sw, message, ns);
            return sw.ToString();
        }
    }
}