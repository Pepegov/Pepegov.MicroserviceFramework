using System.Reflection;

namespace Pepegov.MicroserviceFramework.Definition;

/// <summary>
/// Information about <see cref="IApplicationDefinition"/>
/// </summary>
/// <param name="ApplicationDefinition"></param>
/// <param name="Assembly"></param>
/// <param name="Enabled"></param>
/// <param name="Exported"></param>
public sealed record ApplicationDefinitionInformation(IApplicationDefinition ApplicationDefinition, Assembly Assembly, bool Enabled, bool Exported);