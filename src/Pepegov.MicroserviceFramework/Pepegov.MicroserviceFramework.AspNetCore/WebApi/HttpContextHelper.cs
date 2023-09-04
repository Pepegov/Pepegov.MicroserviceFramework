using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi;

public static class HttpContextHelper
{
    public static ContextTypeValue ParseContextType(HttpContext context)
    {
        ArgumentException.ThrowIfNullOrEmpty(context.Request.ContentType);
        var result = new ContextTypeValue();
        var contextTypeParameters = context.Request.ContentType.Split(";");

        //parse type value
        if (ContextTypeValue.SupportTypes.All(x => x != contextTypeParameters.FirstOrDefault()?.Split("/").FirstOrDefault()))
        {
            result.Type = MediaTypeNames.Text.Plain;
            result.Encoding = Encoding.UTF8;
            //throw new ArgumentException("Invalid ContextType value. missing message type");
            return result;
        }
        result.Type = contextTypeParameters.First();
        if (contextTypeParameters.Length == 1)
        {
            result.Encoding = Encoding.UTF8;
            return result;
        }

        //get all parameters
        result.Parameters = new Dictionary<string, string>();
        foreach (var parameter in contextTypeParameters)
        {
            if (!parameter.Contains("=")) continue;
            var split = parameter.Split("=");
            result.Parameters.Add(split[0], split[1]);
        }

        //parse charset parameter 
        //TODO: сделать по свойству в BodyName в классе Encoding
        if (result.Parameters.ContainsKey("charset"))
        {
            switch (result.Parameters["charset"].ToLower())
            {
                case "ascii":
                    result.Encoding = Encoding.ASCII;
                    break;;
                case "utf-32":
                    result.Encoding = Encoding.UTF32;
                    break;
                case "unicode":
                case "utf-16":
                    result.Encoding = Encoding.Unicode;
                    break;;
                default:
                    result.Encoding = Encoding.UTF8;
                    break;
            }   
        }
        else
        {
            result.Encoding = Encoding.UTF8;
        }

        return result;
    }
}