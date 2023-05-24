using System.Reflection;
using MassTransit;
using Pepegov.MicroserviceFramerwork.Infrastructure;

namespace Pepegov.MicroserviceFramerwork.Extensions;

public static class MassTransitExtensions
{
    public static void AddRequestClientContracts(this IBusRegistrationConfigurator configurator, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var exportedType in assembly.ExportedTypes)
            {
                var interfaces = exportedType.GetInterfaces();
                if(interfaces.Contains(typeof(IContract)))
                {
                    configurator.AddRequestClient(exportedType);
                }
            }
        }
    }
}