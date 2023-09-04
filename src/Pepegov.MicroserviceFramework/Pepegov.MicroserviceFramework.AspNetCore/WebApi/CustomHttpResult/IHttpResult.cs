using Microsoft.AspNetCore.Http;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

public interface IHttpResult : IResult
{
    string? GetResponseMessage();
}