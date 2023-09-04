using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pepegov.MicroserviceFramework.Definition
{
    public interface IApplicationDefinition
    {
        public int Priority { get; }
        bool Enabled { get; }
        
        bool Exported { get; }

        Task ConfigureServicesAsync(IServiceCollection services, IConfiguration configuration);
        Task ConfigureApplicationAsync(IServiceProvider provider);
    }
}