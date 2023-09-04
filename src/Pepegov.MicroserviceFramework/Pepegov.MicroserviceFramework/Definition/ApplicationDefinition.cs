using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pepegov.MicroserviceFramework.Definition;

public abstract class ApplicationDefinition : IApplicationDefinition
{
    private int _priority;
    
    /// <summary>
    /// Value in which the order in which the ApplicationDefinition will be executed is specified (0-10)
    /// </summary>
    public int Priority
    {
        get => _priority;
        set
        {
            if (value <= 0)
            {
                _priority = 0;
            }
            if (value >= 10)
            {
                _priority = 10;
            }

            _priority = value;
        }   
    }
    
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

    public virtual async Task ConfigureServicesAsync(IServiceCollection services, IConfiguration configuration) { }
    public virtual async Task ConfigureApplicationAsync(IServiceProvider provider) { }
}