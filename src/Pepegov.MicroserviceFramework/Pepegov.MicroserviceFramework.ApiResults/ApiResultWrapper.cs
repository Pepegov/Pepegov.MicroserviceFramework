using System.Net;

namespace Pepegov.MicroserviceFramework.ApiResults;

/// <summary>
/// Helps to give api full response with exceptions and metadata. Have wrapper object
/// </summary>
public class ApiResult<TMessage> : ApiResult
{
    /// <summary>
    /// Generalized message
    /// </summary>
    public TMessage Message { get; set; }

    /// <summary>
    /// Check for exceptions
    /// </summary>
    public override bool IsSuccessful
    {
        get
        {
            if (Exceptions is not null && Exceptions.Count > 0)
            {
                return false;
            }
            if (Metadata is not null && Metadata.Any(x => x.Type == MetadataType.Error))
            {
                return false;
            }
            return Message is not null;
        }
    }
    
    public ApiResult() {}

    public ApiResult(TMessage message)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, int statusCode) : base(statusCode)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, HttpStatusCode statusCode) : base(statusCode)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, params Exception[] exceptions) : this(message)
    {
        AddExceptions(exceptions);
    }
    
    public ApiResult(TMessage message, params  ExceptionData[] exceptions) : this(message)
    {
        AddExceptions(exceptions);
    }
    
    public ApiResult(TMessage message, int statusCode, params Exception[] exceptions) : base(statusCode, exceptions)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, HttpStatusCode statusCode, params Exception[] exceptions) : base(statusCode, exceptions)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, int statusCode, params ExceptionData[] exceptions) : base(statusCode, exceptions)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, HttpStatusCode statusCode, params ExceptionData[] exceptions) : base(statusCode, exceptions)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, int statusCode, params Metadata[] metadata) : base(statusCode, metadata)
    {
        Message = message;
    }
    
    public ApiResult(TMessage message, HttpStatusCode statusCode, params Metadata[] metadata) : base(statusCode, metadata)
    {
        Message = message;
    }

    public ApiResult(int statusCode, params ExceptionData[] exceptions) : base(statusCode, exceptions) { }
    
    public ApiResult(HttpStatusCode statusCode, params ExceptionData[] exceptions) : base(statusCode, exceptions) { }
    
    public ApiResult(int statusCode, params Exception[] exceptions) : base(statusCode, exceptions) { }
    
    public ApiResult(HttpStatusCode statusCode, params Exception[] exceptions) : base(statusCode, exceptions) { }

    /// <summary>
    /// Convert result to another entity type
    /// </summary>
    /// <param name="convertObj"></param>
    /// <typeparam name="TConvert"></typeparam>
    /// <returns></returns>
    public ApiResult<TConvert> Convert<TConvert>(TConvert convertObj)
    {
        var result = new ApiResult<TConvert>
        {
            Metadata = this.Metadata,
            Exceptions = this.Exceptions,
            StatusCode = this.StatusCode,
            Message = convertObj
        };

        return result;
    }
    
    /// <summary>
    /// Convert result to another null entity type
    /// </summary>
    /// <param name="convertObj"></param>
    /// <typeparam name="TConvert"></typeparam>
    /// <returns></returns>
    public ApiResult<TConvert> Convert<TConvert>()
    {
        var result = new ApiResult<TConvert>
        {
            Metadata = this.Metadata,
            Exceptions = this.Exceptions,
            StatusCode = this.StatusCode,
        };

        return result;
    }
}