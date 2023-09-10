using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test.Tests.HttpResultTests;

public class CustomHttpResultTests
{
    private TestViewModel _model;
    
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

        await httpResult.ExecuteAsync(context);
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var expectedResult = JsonSerializer.Serialize(_model);
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

        await httpResult.ExecuteAsync(context);
        
        Assert.That(context.Response.StatusCode == (int)HttpStatusCode.Continue);
    }
}