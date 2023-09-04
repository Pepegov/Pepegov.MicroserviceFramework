using System.Net;
using System.Net.Mime;
using System.Xml.Serialization;
using Pepegov.MicroserviceFramework.ApiResults;
using Pepegov.MicroserviceFramework.AspNetCore.Infrastructure;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

[HttpContextType(MediaTypeNames.Application.Xml)]
public sealed class HttpXmlResult<T> : BaseHttpResult<T>
{ 
    public HttpXmlResult(T obj, HttpStatusCode statusCode) : base(obj, statusCode) { }

    public override string? GetResponseMessage()
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