using System.Net;
using Microsoft.AspNetCore.Http;
using Pepegov.MicroserviceFramework.ApiResults;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi.CustomHttpResult;

namespace Pepegov.MicroserviceFramework.AspNetCore.WebApi;

public static class ResultExtension
{
    #region Common
    
    public static IResult Custom<TMessage>(this IResultExtensions resultExtensions, TMessage message, HttpStatusCode statusCode)
        => new HttpResult<TMessage>(message, statusCode);
    
    public static IResult Custom(this IResultExtensions resultExtensions, ApiResult message)
        => new HttpResult<ApiResult>(message, (HttpStatusCode)message.StatusCode);
    
    public static IResult Custom<TMessage>(this IResultExtensions resultExtensions, ApiResult<TMessage> message)
        => new HttpResult<ApiResult<TMessage>>(message, (HttpStatusCode)message.StatusCode);

    #endregion

    #region TextPlan
    
    public static IResult CustomAsTextPlan(this IResultExtensions resultExtensions, string message, HttpStatusCode statusCode)
        =>  new HttpTextPlanResult<string>(message, statusCode); 
    
    public static IResult CustomAsTextPlan<TMessage>(this IResultExtensions resultExtensions, TMessage message, HttpStatusCode statusCode)
        =>  new HttpTextPlanResult<TMessage>(message, statusCode);

    public static IResult CustomAsTextPlan(this IResultExtensions resultExtensions, ApiResult message)
        => new HttpTextPlanResult<ApiResult>(message, (HttpStatusCode)message.StatusCode);
    
    public static IResult CustomAsTextPlan<TMessage>(this IResultExtensions resultExtensions, ApiResult<TMessage> message)
        =>  new HttpTextPlanResult<ApiResult<TMessage>>(message, (HttpStatusCode)message.StatusCode);

    #endregion

    #region Json
    
    public static IResult CustomAsJson<TMessage>(this IResultExtensions resultExtensions, TMessage message, HttpStatusCode statusCode)
        => new HttpJsonResult<TMessage>(message, statusCode);
    
    public static IResult CustomAsJson(this IResultExtensions resultExtensions, ApiResult message)
        => new HttpJsonResult<ApiResult>(message, (HttpStatusCode)message.StatusCode);

    
    public static IResult CustomAsJson<TMessage>(this IResultExtensions resultExtensions, ApiResult<TMessage> message, HttpStatusCode statusCode)
        => new HttpJsonResult<ApiResult<TMessage>>(message, (HttpStatusCode)message.StatusCode);
    
    #endregion

    #region Xml
    
    public static IResult CustomAsXml<TMessage>(this IResultExtensions resultExtensions, TMessage message, HttpStatusCode statusCode)
        => new HttpXmlResult<TMessage>(message, statusCode);

    public static IResult CustomAsXml(this IResultExtensions resultExtensions, ApiResult message)
        => new HttpXmlResult<ApiResult>(message, (HttpStatusCode)message.StatusCode);
    
    public static IResult CustomAsXml<TMessage>(this IResultExtensions resultExtensions, ApiResult<TMessage> message)
        => new HttpXmlResult<ApiResult<TMessage>>(message, (HttpStatusCode)message.StatusCode);
    
    #endregion
    
}