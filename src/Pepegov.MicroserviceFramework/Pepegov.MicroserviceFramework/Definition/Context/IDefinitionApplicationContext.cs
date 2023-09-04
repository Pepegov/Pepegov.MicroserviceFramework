namespace Pepegov.MicroserviceFramework.Definition.Context;

public interface IDefinitionApplicationContext
{
    public IServiceProvider ServiceProvider { get; }
}