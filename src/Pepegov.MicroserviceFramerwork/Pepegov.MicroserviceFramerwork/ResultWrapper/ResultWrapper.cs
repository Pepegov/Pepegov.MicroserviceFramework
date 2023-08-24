using MassTransit.SagaStateMachine;

namespace Pepegov.MicroserviceFramerwork.ResultWrapper;

/// <summary>
/// Helps to give api full response with exeptions and metadata 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResultWrapper<T> where T : class 
{
    /// <summary>
    /// Generalized message
    /// </summary>
    public T? Message { get; set; }

    /// <summary>
    /// Check for exceptions
    /// </summary>
    public bool IsSuccessful
    {
        get
        {
            if (Exceptions is not null && Exceptions.Count > 0)
            {
                return false;
            }
            if (Metadatas != null && Metadatas.Any(x => x.Type == MetadataType.Error))
            {
                return false;
            }
            return Message is not null;
        }
    }
    
    /// <summary>
    /// Status code. Can be used for webapi or your own interpretation
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Exceptions received during result creation
    /// </summary>
    public List<ExceptionData>? Exceptions { get; set; }
    
    /// <summary>
    /// Additional response configuration data
    /// </summary>
    public List<Metadata>? Metadatas { get; set; }

    public ResultWrapper() {}

    public ResultWrapper(T message)
    {
        Message = message;
    }
    
    public ResultWrapper(T message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
    
    public ResultWrapper(T message, params Exception[] exceptions) : this(message)
    {
        Message = message;
        AddExceptions(exceptions);
    }
    
    public ResultWrapper(T message, params  ExceptionData[] exceptions) : this(message)
    {
        Message = message;
        AddExceptions(exceptions);
    }
    
    public ResultWrapper(T message, int statusCode, params Exception[] exceptions) : this(message)
    {
        Message = message;
        StatusCode = statusCode;
        AddExceptions(exceptions);
    }
    
    public ResultWrapper(T message, int statusCode, params ExceptionData[] exceptions) : this(message)
    {
        Message = message;
        StatusCode = statusCode;
        AddExceptions(exceptions);
    }
    
    /// <summary>
    /// Add exceptions to field Exceptions
    /// </summary>
    /// <param name="exceptions"></param>
    public void AddExceptions(params Exception[] exceptions)
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
    /// Add exception datas to field Exceptions
    /// </summary>
    /// <param name="exceptions"></param>
    public void AddExceptions(params ExceptionData[] exceptions)
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
    /// Add metadata to field Metadatas
    /// </summary>
    /// <param name="metadatas"></param>
    public void AddMetadatas(params Metadata[] metadatas)
    {
        if (Metadatas is null)
        {
            Metadatas = new List<Metadata>(metadatas);
            return;;
        }
        
        Metadatas.AddRange(metadatas);
    }

    public ResultWrapper<TC> Convert<TC>(TC converObj) where TC : class
    {
        var result = new ResultWrapper<TC>();
        result.Metadatas = this.Metadatas;
        result.Exceptions = this.Exceptions;
        result.StatusCode = this.StatusCode;
        result.Message = converObj;

        return result;
    }
}