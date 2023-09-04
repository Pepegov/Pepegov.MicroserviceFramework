using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Pepegov.MicroserviceFramework.Definition;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;

public static class WebApplicationDefinitionExtensions
{
    public static void AddDefinitions(this WebApplicationBuilder builder,
        params Assembly[] assemblies)
    {
        builder.Services.AddApplicationDefinitions(builder.Configuration, assemblies);
    }
    
    public static void UseDefinitions(this WebApplication application)
    {
        application.Services.UseApplicationDefinitions();
    }
}