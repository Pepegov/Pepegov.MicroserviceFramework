using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;

namespace Pepegov.MicroserviceFramework.Test.Tests;

public class PositiveApplicationDefinitionTest
{
    private IServiceProvider _serviceProvider;
    
    [SetUp]
    public void Setup()
    {
        var assembly = typeof(PositiveApplicationDefinitionTest).Assembly;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddApplicationDefinitions(null ,assembly);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Test]
    public void Test()
    {
        Assert.DoesNotThrowAsync(() => _serviceProvider.UseApplicationDefinitions());
    }
}

public class TestDefinition : ApplicationDefinition
{
    public override async Task ConfigureServicesAsync(IDefinitionServiceContext context)
    {
        context.ServiceCollection.AddSingleton(new TestViewModel() { Name = "123"});
    }

    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var testViewModel = context.ServiceProvider.GetService<TestViewModel>();
        ArgumentNullException.ThrowIfNull(testViewModel);
    }
}