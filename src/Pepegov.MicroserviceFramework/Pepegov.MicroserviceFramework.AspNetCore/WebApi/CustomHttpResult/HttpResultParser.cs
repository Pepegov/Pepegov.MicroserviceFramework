using System.Data;
using System.Reflection;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

public static class HttpResultParser
{
    private static readonly Dictionary<string, Type> CommandsCache;

    static HttpResultParser()
    {
        var contextTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t.GetInterfaces().Any(t => t == typeof(IHttpResult)));

        CommandsCache = ParseTypes(contextTypes);
    }
    
    private static Dictionary<string, Type> ParseTypes(IEnumerable<Type> types)
    {
        if (types is null)
        {
            throw new ArgumentNullException(nameof(types));
        }
        Dictionary<string, Type> result = new Dictionary<string, Type>();

        foreach (var type in types)
        {
            var httpContextTypeAttribute = type.GetCustomAttribute<HttpContextTypeAttribute>();
            if (httpContextTypeAttribute == null)
                continue;

            if (result.ContainsKey(httpContextTypeAttribute.ContextType))
                throw new InvalidConstraintException($"Repeating the command name: {httpContextTypeAttribute.ContextType}");

            result.Add(httpContextTypeAttribute.ContextType, type);
        }

        return result;
    }
    
    public static bool TryParse(string contextType, out Type httpResultType)
    {
        return CommandsCache.TryGetValue(contextType, out httpResultType!);
    }
}