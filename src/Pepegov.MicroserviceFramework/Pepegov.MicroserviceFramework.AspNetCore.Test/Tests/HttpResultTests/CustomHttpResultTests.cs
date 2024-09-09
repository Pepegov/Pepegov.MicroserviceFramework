using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.ApiResults;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test.Tests.HttpResultTests;

public class CustomHttpResultTests
{
    private TestViewModel _model;
    private readonly JsonSerializerOptions _jsonSerializerSettings = new()
    {
        PropertyNamingPolicy  = JsonNamingPolicy.CamelCase,
    };
    private readonly IServiceProvider DefaultServiceProvider = (new ServiceCollection()).BuildServiceProvider();
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _model = new TestViewModel() { Name = "bob" };
    }

    [Test]
    public async Task PositiveJsonResultBodyTest()
    {
        IHttpResult httpResult = new HttpJsonResult<TestViewModel>(_model, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Json;
        context.RequestServices = DefaultServiceProvider;
        
        await httpResult.ExecuteAsync(context);
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var expectedResult = JsonSerializer.Serialize(_model, _jsonSerializerSettings);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.That(expectedResult == body);
    }

    [Test]
    public async Task PositiveXmlResultBodyTest()
    {
        IHttpResult httpResult = new HttpXmlResult<TestViewModel>(_model, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Xml;
        context.RequestServices = DefaultServiceProvider;
        
        await httpResult.ExecuteAsync(context);
        var expectedResult = TestHelper.ToXml(_model);
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.That(expectedResult == body);
    }

    [Test]
    public async Task NegativeStatusCodeTest()
    {
        const int statusCode = 0;
        IResult httpResult = new HttpResult<TestViewModel>(new ApiResult<TestViewModel>(_model, statusCode));
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Xml;
        context.RequestServices = DefaultServiceProvider;

        await httpResult.ExecuteAsync(context);

        context.Response.Body.Position = 0;

        Assert.That(context.Response.StatusCode == (int)HttpStatusCode.Continue);
        Assert.That(context.Response.ContentLength == null);
        Assert.That((new StreamReader(context.Response.Body).ReadToEndAsync().Result == string.Empty));
    }
}