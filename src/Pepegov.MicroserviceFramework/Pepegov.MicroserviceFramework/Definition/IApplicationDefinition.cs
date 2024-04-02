using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.Definition
{
    public interface IApplicationDefinition
    {
        int Priority { get; }
        bool Enabled { get; }
        
        bool Exported { get; }

        Task ConfigureServicesAsync(IDefinitionServiceContext context);
        Task ConfigureApplicationAsync(IDefinitionApplicationContext context);
    }
}