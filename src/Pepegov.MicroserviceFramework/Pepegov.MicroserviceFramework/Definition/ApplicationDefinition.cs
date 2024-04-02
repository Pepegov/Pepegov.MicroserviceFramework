using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.Definition;

public abstract class ApplicationDefinition : IApplicationDefinition
{

    /// <summary>
    /// Value in which the order in which the ApplicationDefinition will be executed is specified (0-10)
    /// </summary>
    public virtual int Priority => 0;
    
    /// <summary>
    /// Flag indicating whether the ApplicationDefinition is being used
    /// </summary>
    /// <remarks>Default values is <c>True</c></remarks>
    public virtual bool Enabled => true;

    /// <summary>
    /// Flag indicating whether the ApplicationDefinition is exported in another project
    /// </summary>
    /// <remarks>Default values is <c>False</c></remarks>
    public virtual bool Exported => false;

    public virtual async Task ConfigureServicesAsync(IDefinitionServiceContext context) { }
    public virtual async Task ConfigureApplicationAsync(IDefinitionApplicationContext context) { }
}