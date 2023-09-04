using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pepegov.MicroserviceFramework.Definition.Context;

public class DefinitionServiceContext : IDefinitionServiceContext
{
    public IServiceCollection ServiceCollection { get; }
    public IConfiguration Configuration { get; }

    public DefinitionServiceContext(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        ServiceCollection = serviceCollection;
        Configuration = configuration;
    }
}