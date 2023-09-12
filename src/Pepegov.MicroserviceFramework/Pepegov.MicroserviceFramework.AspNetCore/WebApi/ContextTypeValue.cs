using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi;

public class ContextTypeValue
{
    public static readonly string[] SupportTypes = new string[]
        { "application", "audio", "image", "message", "multipart", "text", "video", }; 
    
    public string Type { get; set; } = null!;
    public Encoding Encoding { get; set; } = null!;
    public Dictionary<string, string>? Parameters { get; set; }

    public static ContextTypeValue Standart
        => new ContextTypeValue()
        {
            Type = MediaTypeNames.Application.Json,
            Encoding = Encoding.UTF8,
        };
}