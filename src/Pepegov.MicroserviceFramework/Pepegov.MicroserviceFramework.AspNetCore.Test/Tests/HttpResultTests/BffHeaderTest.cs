using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Pepegov.MicroserviceFramework.ApiResults;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;
using Pepegov.MicroserviceFramework.Data.Exceptions;

namespace Pepegov.MicroserviceFramework.AspNetCore.Test.Tests.HttpResultTests;

public class BffHeaderTest
{
    private ApiResult<TestViewModel> _model;
    private HttpContext _context;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var testViewModel = new TestViewModel() { Name = "bob" };
        _model = new ApiResult<TestViewModel>(testViewModel, (int)HttpStatusCode.OK);
    }

    [SetUp]
    public void SetUp()
    {
        _context = new DefaultHttpContext();
        _context.Response.Body = new MemoryStream();
        _context.Request.ContentType = MediaTypeNames.Application.Json;
        _context.Request.Headers.Add("Bff", new StringValues("true"));
    }

    [Test]
    public async Task PositiveMessageTest()
    {
        IResult httpResult = new HttpResult<ApiResult<TestViewModel>>(_model, HttpStatusCode.OK);

        await httpResult.ExecuteAsync(_context);

        var expectedResult = JsonSerializer.Serialize(_model.Message);
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(_context.Response.Body).ReadToEndAsync();

        Assert.That(expectedResult == body);
    }

    [Test]
    public async Task NegativeNullDataTest()
    {
        IResult httpResult = new HttpResult<ApiResult<TestViewModel>>(null, HttpStatusCode.OK);

        await httpResult.ExecuteAsync(_context);
        
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        
        Assert.That(body.Contains(nameof(Exception)));
        Assert.That(_context.Response.StatusCode == (int)HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task NegativeWrappedVoidDataTest()
    {
        var expectedStatusCode = HttpStatusCode.OK;
        IResult httpResult = new HttpResult<ApiResult<TestViewModel>>(new ApiResult<TestViewModel>(null, expectedStatusCode), expectedStatusCode);

        await httpResult.ExecuteAsync(_context);
        
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        
        Assert.That(body.Contains(nameof(Exception)));
        Assert.That(_context.Response.StatusCode == (int)HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task NegativeVoidDataTest()
    {
        var expectedStatusCode = HttpStatusCode.NoContent;
        IResult httpResult = new HttpResult<ApiResult>(new ApiResult(), HttpStatusCode.OK);

        await httpResult.ExecuteAsync(_context);
        
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        
        Assert.That(body == string.Empty);
        Assert.That(_context.Response.StatusCode == (int)expectedStatusCode);
    }

    [Test]
    public async Task PositiveExceptionTest()
    {
        var model = new ApiResult(400, new MicroserviceException("Test exception")); 
        IResult httpResult = new HttpResult<ApiResult>(model, HttpStatusCode.OK);

        await httpResult.ExecuteAsync(_context);

        var expectedResult = JsonSerializer.Serialize(model.Exceptions?.ToMinimalExceptionData());
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        
        Assert.That(expectedResult == body);
    }
}