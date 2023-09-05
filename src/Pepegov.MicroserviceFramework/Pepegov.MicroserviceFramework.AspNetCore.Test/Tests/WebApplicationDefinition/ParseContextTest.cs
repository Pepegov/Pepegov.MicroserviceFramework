using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test.Tests.WebApplicationDefinition;

public class ParseContextTest
{
    [Test]
    public void PositiveTest()
    {
        var builder = WebApplication.CreateBuilder();
        var assembly = typeof(ParseContextTest).Assembly;
        
        Assert.DoesNotThrowAsync(() => builder.AddApplicationDefinitions(assembly));
        
        var app = builder.Build();
        
        Assert.DoesNotThrowAsync(() => app.UseApplicationDefinitions());
    }
    
    [Test]
    public void NegativeTest()
    {
        var builder = WebApplication.CreateBuilder();
        var assembly = typeof(ParseContextTest).Assembly;
        
        Assert.ThrowsAsync<InvalidCastException>( () => builder.Services.AddApplicationDefinitions(
            new DefinitionServiceContext(builder.Services, builder.Configuration),assembly));

        builder?.AddApplicationDefinitions(assembly);
        
        var app = builder?.Build();
        
        Assert.ThrowsAsync<InvalidCastException>( () => app.Services.UseApplicationDefinitions());
    }
}

public class ParseTestDefinition : ApplicationDefinition
{
    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        context.Parse<WebDefinitionServiceContext>();
    }

    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        context.Parse<WebDefinitionApplicationContext>();
    }
}