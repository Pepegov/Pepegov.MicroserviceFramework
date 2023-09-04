namespace Pepegov.MicroserviceFramework.Definition.Context;

public class DefinitionApplicationContext : IDefinitionApplicationContext
{
    public IServiceProvider ServiceProvider { get; }

    public DefinitionApplicationContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}