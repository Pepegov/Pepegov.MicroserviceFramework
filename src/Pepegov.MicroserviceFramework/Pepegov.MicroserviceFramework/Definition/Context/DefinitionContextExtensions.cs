using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pepegov.MicroserviceFramework.Definition.Context;

public static class DefinitionContextExtensions 
{
    public static TContext Parse<TContext>(this IDefinitionServiceContext context) where TContext : class, IDefinitionServiceContext
    {
        return (TContext)context;
    }
    
    public static bool TryParse<TContext>(this IDefinitionServiceContext context, out TContext? resultContext) where TContext : class, IDefinitionServiceContext
    {
        if (context is TContext result)
        {
            resultContext = result;
            return true;
        }

        resultContext = null;
        return false;
    }
    
    public static TContext Parse<TContext>(this IDefinitionApplicationContext context) where TContext : class, IDefinitionApplicationContext
    {
        return (TContext)context;
    }
    
    public static bool TryParse<TContext>(this IDefinitionApplicationContext context, out TContext? resultContext) where TContext : class, IDefinitionApplicationContext
    {
        if (context is TContext result)
        {
            resultContext = result;
            return true;
        }

        resultContext = null;
        return false;
    }
}