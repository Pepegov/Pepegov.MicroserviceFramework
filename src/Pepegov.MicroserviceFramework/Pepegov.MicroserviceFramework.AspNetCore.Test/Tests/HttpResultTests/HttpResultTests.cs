using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test.Tests.HttpResultTests;

public class HttpResultTests
{
    private TestViewModel _model;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _model = new TestViewModel() { Name = "bob" };
    }
    
    [Test]
    public async Task PositiveXmlBodyTest()
    {
        IResult httpResult = new HttpResult<TestViewModel>(_model, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Xml;

        await httpResult.ExecuteAsync(context);

        var expectedResult = TestHelper.ToXml(_model);
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
        
        Assert.That(expectedResult == body);
    }
    
    [Test]
    public async Task PositiveJsonBodyTest()
    {
        IResult httpResult = new HttpResult<TestViewModel>(_model, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Application.Json;

        await httpResult.ExecuteAsync(context);

        var expectedResult = JsonSerializer.Serialize(_model);
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
        
        Assert.That(expectedResult == body);
    }
    
    [Test]
    public async Task PositiveTextBodyTest()
    {
        var expectedResult = "bob";
        IResult httpResult = new HttpResult<string>(expectedResult, HttpStatusCode.OK);
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.ContentType = MediaTypeNames.Text.Plain;

        await httpResult.ExecuteAsync(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
        
        Assert.That(expectedResult == body);
    }
}