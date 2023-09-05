using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Pepegov.MicroserviceFramework.Definition;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;

public static class WebApplicationDefinitionExtensions
{
    public static async Task AddApplicationDefinitions(this WebApplicationBuilder builder,
        params Assembly[] assemblies)
    {
       await builder.Services.AddApplicationDefinitions(new WebDefinitionServiceContext(builder), assemblies);
    }

    public static async Task UseApplicationDefinitions(this WebApplication application)
    {
        await application.Services.UseApplicationDefinitions(new WebDefinitionApplicationContext(application));
    }
}