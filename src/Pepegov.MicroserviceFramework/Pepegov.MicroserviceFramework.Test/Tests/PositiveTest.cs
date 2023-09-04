using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.Definition;

namespace Pepegov.MicroserviceFramework.Test.Tests;

public class PositiveTest
{
    private IServiceProvider _serviceProvider;
    
    [SetUp]
    public void Setup()
    {
        var assembly = typeof(PositiveTest).Assembly;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddApplicationDefinitions(null ,assembly);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Test]
    public void Test()
    {
        Assert.DoesNotThrow(() => _serviceProvider.UseApplicationDefinitions());
    }
}

public class TestDefinition : ApplicationDefinition
{
    public override async Task ConfigureServicesAsync(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new TestViewModel() { Name = "123"});
    }

    public override async Task ConfigureApplicationAsync(IServiceProvider provider)
    {
        var testViewModel = provider.GetService<TestViewModel>();
        ArgumentNullException.ThrowIfNull(testViewModel);
    }
}