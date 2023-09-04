using Microsoft.AspNetCore.Builder;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;

public class WebDefinitionApplicationContext : IDefinitionApplicationContext
{
    public IServiceProvider ServiceProvider { get; }

    public WebApplication WebApplication { get; }

    public WebDefinitionApplicationContext(WebApplication webApplication)
    {
        WebApplication = webApplication;
        ServiceProvider = webApplication.Services;
    }
}