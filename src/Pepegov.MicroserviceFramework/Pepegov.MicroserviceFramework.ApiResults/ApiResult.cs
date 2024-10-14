using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace Pepegov.MicroserviceFramework.ApiResults;

/// <summary>
/// Helps to give api full response with exceptions and metadata
/// </summary>
public class ApiResult
{
    /// <summary>
    /// Status code. Can be used for webapi or your own interpretation
    /// </summary>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// Check for exceptions
    /// </summary>
    public virtual bool IsSuccessful
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
            return true;
        }
    }
    
    /// <summary>
    /// Exceptions received during result creation
    /// </summary>
    public List<ExceptionData>? Exceptions { get; set; }

    /// <summary>
    /// Additional response configuration data
    /// </summary>
    [JsonPropertyName("Metadatas")]
    public List<Metadata>? Metadata { get; set; }

    public ApiResult() { }

    public ApiResult(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ApiResult(HttpStatusCode statusCode)
    {
        StatusCode = (int)statusCode;
    }
    
    public ApiResult(HttpStatusCode statusCode, params Exception[] exceptions) : this(statusCode)
    {
        AddExceptions(exceptions);
    }

    public ApiResult(int statusCode, params Exception[] exceptions) : this(statusCode)
    {
        AddExceptions(exceptions);
    }
    
    public ApiResult(HttpStatusCode statusCode, params ExceptionData[] exceptions) : this(statusCode)
    {
        AddExceptions(exceptions);
    }
    
    public ApiResult(int statusCode, params ExceptionData[] exceptions) : this(statusCode)
    {
        AddExceptions(exceptions);
    }
    
    public ApiResult(int statusCode, params Metadata[] metadata) : this(statusCode)
    {
        AddMetadata(metadata);
    }
    
    public ApiResult(HttpStatusCode statusCode, params Metadata[] metadata) : this(statusCode)
    {
        AddMetadata(metadata);
    }
    
    /// <summary>
    /// Add exceptions to field Exceptions
    /// </summary>
    /// <param name="exceptions"></param>
    public void AddExceptions(params Exception[]? exceptions)
    {
        if (exceptions is null)
        {
            return;
        }
        
        if (Exceptions is null)
        {
            Exceptions = new List<ExceptionData>(exceptions.ToExceptionData()) { };
            return;
        }
        
        Exceptions.AddRange(exceptions.ToExceptionData());
    }
    
    /// <summary>
    /// Add exception data to field Exceptions
    /// </summary>
    /// <param name="exceptions"></param>
    public void AddExceptions(params ExceptionData[]? exceptions)
    {
        if (exceptions is null)
        {
            return;
        }
        
        if (Exceptions is null)
        {
            Exceptions = new List<ExceptionData>(exceptions) { };
            return;
        }
        
        Exceptions.AddRange(exceptions);
    }

    /// <summary>
    /// Add metadata to field Metadata
    /// </summary>
    /// <param name="metadata"></param>
    public void AddMetadata(params Metadata[] metadata)
    {
        if (Metadata is null)
        {
            Metadata = new List<Metadata>(metadata);
            return;
        }
        
        Metadata.AddRange(metadata);
    }

    /// <summary>
    /// Allows you to pull an error message from all exceptions as a result
    /// </summary>
    /// <returns>Get required description for all exceptions</returns>
    public string GetExceptionMessages()
    {
        if (Exceptions is null)
        {
            return string.Empty;
        }
        
        var errorMessage = new StringBuilder();
        foreach (var exception in Exceptions)
        {
            errorMessage.Append($"Type:{exception.TypeData}. Data:{exception.Message} \n");
        }

        return errorMessage.ToString();
    }
    
    /// <summary>
    /// Convert result to result entity type
    /// </summary>
    /// <param name="convertObj"></param>
    /// <typeparam name="TConvert"></typeparam>
    /// <returns></returns>
    public ApiResult<TConvert> Convert<TConvert>(TConvert? convertObj)
    {
        var result = new ApiResult<TConvert>
        {
            Metadata = this.Metadata,
            Exceptions = this.Exceptions,
            StatusCode = this.StatusCode
        };
        if (convertObj is not null)
        {
            result.Message = convertObj;   
        }

        return result;
    }
}