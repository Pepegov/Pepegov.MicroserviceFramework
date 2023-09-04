using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pepegov.MicroserviceFramework.Definition.Context;

public interface IDefinitionServiceContext
{
    public IServiceCollection ServiceCollection { get; }
    public IConfiguration Configuration { get; }
}