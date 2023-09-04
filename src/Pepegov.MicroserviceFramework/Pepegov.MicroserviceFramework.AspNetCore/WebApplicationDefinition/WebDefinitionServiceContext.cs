using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;

public class WebDefinitionServiceContext : IDefinitionServiceContext
{
    public IServiceCollection ServiceCollection { get; }
    public IConfiguration Configuration { get; }
    public WebApplicationBuilder WebApplicationBuilder { get; }

    public WebDefinitionServiceContext(WebApplicationBuilder webApplicationBuilder)
    {
        WebApplicationBuilder = webApplicationBuilder;
        ServiceCollection = webApplicationBuilder.Services;
        Configuration = webApplicationBuilder.Configuration;
    }
}