using System.Reflection;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.ApiResults.Test.Consumers;

namespace Pepegov.MicroserviceFramework.ApiResults.Test.Tests;

public class Tests
{
    private ITestHarness _testHarness;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var masstransitCollection = new ServiceCollection(); 
        var massTransitProvider = masstransitCollection.AddMassTransitTestHarness(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.SetInMemorySagaRepositoryProvider();

                x.AddConsumer<ApiResultWrapperConsumer>();
                x.AddConsumer<ApiResultConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host($"rabbitmq://localhost/", h =>
                    {
                        h.Username("rmuser");
                        h.Password("rmpassword");
                    });

                    cfg.ConfigureEndpoints(context, KebabCaseEndpointNameFormatter.Instance);
                    cfg.UseJsonSerializer();
                });
            })
            .BuildServiceProvider(true);
        
        var harness = massTransitProvider.GetRequiredService<ITestHarness>();
        harness.Start();

        _testHarness = harness;
    }

    [Test]
    public async Task StatusCodeApiResultConsumerTest()
    {
        var expectedResult = new ApiResult(200);
        var requestClient = _testHarness.GetRequestClient<ApiResult>();

        var response = await requestClient.GetResponse<ApiResult>(expectedResult);
        
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.True(response.Message.IsSuccessful);
    }
    
    [Test]
    public async Task ExceptionsApiResultConsumerTest()
    {
        var expectedResult = new ApiResult(200, new Exception("Test text"));
        var requestClient = _testHarness.GetRequestClient<ApiResult>();

        var response = await requestClient.GetResponse<ApiResult>(expectedResult);
        
        Assert.False(response.Message.Exceptions is null);

        for (int i = 0; i < response.Message.Exceptions!.Count; i++)
        {
            Assert.That(response.Message.Exceptions[i].Message == expectedResult.Exceptions![i].Message);   
        }
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.False(response.Message.IsSuccessful);
    }
    
    [Test]
    public async Task MetadataApiResultConsumerTest()
    {
        var expectedResult = new ApiResult(200);
        expectedResult.AddMetadata(new Metadata("Test text"));
        var requestClient = _testHarness.GetRequestClient<ApiResult>();

        var response = await requestClient.GetResponse<ApiResult>(expectedResult);
        
        Assert.False(response.Message.Metadata is null);

        for (int i = 0; i < response.Message.Metadata!.Count; i++)
        {
            Assert.That(response.Message.Metadata[i].Description == expectedResult.Metadata![i].Description);   
        }
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.True(response.Message.IsSuccessful);
    }
    
    [Test]
    public async Task StatusCodeApiResultWrapperConsumerTest()
    {
        var expectedResult = new ApiResult<TestViewModel>(new TestViewModel() { Name = "Test test"},200);
        var requestClient = _testHarness.GetRequestClient<ApiResult<TestViewModel>>();

        var response = await requestClient.GetResponse<ApiResult<TestViewModel>>(expectedResult);
        
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.True(response.Message.IsSuccessful);
    }
    
    [Test]
    public async Task ExceptionsApiResultWrapperConsumerTest()
    {
        var expectedResult = new ApiResult<TestViewModel>(new TestViewModel() { Name = "Test test"}, 200, new Exception("Test text"));
        var requestClient = _testHarness.GetRequestClient<ApiResult<TestViewModel>>();

        var response = await requestClient.GetResponse<ApiResult<TestViewModel>>(expectedResult);
        
        Assert.False(response.Message.Exceptions is null);

        for (int i = 0; i < response.Message.Exceptions!.Count; i++)
        {
            Assert.That(response.Message.Exceptions[i].Message == expectedResult.Exceptions![i].Message);   
        }
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.False(response.Message.IsSuccessful);
    }
    
    [Test]
    public async Task MetadataApiResultWrapperConsumerTest()
    {
        var expectedResult = new ApiResult<TestViewModel>(new TestViewModel() { Name = "Test test"},200);
        expectedResult.AddMetadata(new Metadata("Test text"));
        var requestClient = _testHarness.GetRequestClient<ApiResult<TestViewModel>>();

        var response = await requestClient.GetResponse<ApiResult>(expectedResult);
        
        Assert.False(response.Message.Metadata is null);

        for (int i = 0; i < response.Message.Metadata!.Count; i++)
        {
            Assert.That(response.Message.Metadata[i].Description == expectedResult.Metadata![i].Description);   
        }
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.True(response.Message.IsSuccessful);
    }
    
    [Test]
    public async Task MessageApiResultWrapperConsumerTest()
    {
        var expectedResult = new ApiResult<TestViewModel>(new TestViewModel() { Name = "Test test"},200);
        var requestClient = _testHarness.GetRequestClient<ApiResult<TestViewModel>>();

        var response = await requestClient.GetResponse<ApiResult<TestViewModel>>(expectedResult);
        
        Assert.That(expectedResult.StatusCode == response.Message.StatusCode);
        Assert.That(expectedResult.Message.Name == response.Message.Message.Name);
        Assert.True(response.Message.IsSuccessful);
    }
}