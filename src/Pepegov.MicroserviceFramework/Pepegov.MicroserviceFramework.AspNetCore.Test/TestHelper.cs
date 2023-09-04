using System.Text;
using System.Xml.Serialization;
using Pepegov.MicroserviceFramework.AspNetCore.Infrastructure;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test;

public static class TestHelper
{
    public static string ToXml<T>(T model)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(model.GetType());
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("","");
        using (StringWriter sw = new StringWriterWithEncoding(Encoding.UTF8))
        {
            xmlSerializer.Serialize(sw, model, ns);
            return sw.ToString();
        }
    }
}