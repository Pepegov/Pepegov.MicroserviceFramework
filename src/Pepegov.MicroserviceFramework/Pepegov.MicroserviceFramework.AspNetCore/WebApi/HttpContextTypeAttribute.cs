namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi;

public class HttpContextTypeAttribute : Attribute
{
    public string ContextType { get; }

    public HttpContextTypeAttribute(string contextType)
    {
        ContextType = contextType;
    }
}