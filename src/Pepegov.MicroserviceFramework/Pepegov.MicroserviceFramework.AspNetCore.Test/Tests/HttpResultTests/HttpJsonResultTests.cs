using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test.Tests.HttpResultTests;

public class HttpJsonResultTests
{
    private TestViewModel _model;
    private readonly JsonSerializerOptions _defaultJsonSerializerSettings = new()
    {
        PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
    };
    private readonly JsonSerializerOptions _ignoreReadonlyFieldsSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IgnoreReadOnlyProperties = true,
        IgnoreReadOnlyFields = true,
    };
    
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _model = new TestViewModel("bob", "pop");
    }
    
    [Test]
    public async Task PositiveJsonSettingsResultBodyTest()
    {
        IHttpResult httpResult = new HttpJsonResult<TestViewModel>(_model, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Json;
        context.RequestServices = CreateProviderWithJsonOptions();
        
        await httpResult.ExecuteAsync(context);
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var expectedResult = JsonSerializer.Serialize(_model, _ignoreReadonlyFieldsSerializerOptions);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
        
        Assert.That(expectedResult == body);
    }
    
    [Test]
    public async Task PositiveNonJsonSettingsResultBodyTest()
    {
        IHttpResult httpResult = new HttpJsonResult<TestViewModel>(_model, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Json;
        context.RequestServices = CreateEmptyProvider();
        
        await httpResult.ExecuteAsync(context);
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var expectedResult = JsonSerializer.Serialize(_model, _defaultJsonSerializerSettings);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
        
        Assert.That(expectedResult == body);
    }

    private IServiceProvider CreateProviderWithJsonOptions()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = _ignoreReadonlyFieldsSerializerOptions.PropertyNamingPolicy;
            options.SerializerOptions.IgnoreReadOnlyFields = _ignoreReadonlyFieldsSerializerOptions.IgnoreReadOnlyFields;
            options.SerializerOptions.IgnoreReadOnlyProperties = _ignoreReadonlyFieldsSerializerOptions.IgnoreReadOnlyProperties;
        });
        return serviceCollection.BuildServiceProvider();
    }

    private IServiceProvider CreateEmptyProvider()
    {
        var collection = new ServiceCollection();
        return collection.BuildServiceProvider();
    }
}