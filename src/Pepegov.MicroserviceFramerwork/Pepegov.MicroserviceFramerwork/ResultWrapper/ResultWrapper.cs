namespace Pepegov.MicroserviceFramerwork.ResultWrapper;

public class ResultWrapper<T> where T : class 
{
    public T? Message { get; set; }

    public bool IsSuccessful
    {
        get
        {
            if (Exceptions is null || Exceptions.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
    
    public int StatusCode { get; set; }

    public List<Exception>? Exceptions { get; private set; }
    public List<Metadata>? Metadatas { get; private set; }

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
    
    public ResultWrapper(T message, Exception exception) : this(message)
    {
        Message = message;
        AddException(exception);
    }
    
    public ResultWrapper(T message, int statusCode, Exception exception) : this(message)
    {
        Message = message;
        StatusCode = statusCode;
        AddException(exception);
    }

    public void AddException(Exception? exception)
    {
        if (exception is null)
        {
            return;
        }
        
        if (Exceptions is null)
        {
            Exceptions = new List<Exception>() { exception };
            return;
        }
        
        Exceptions.Add(exception);
    }

    public void AddMetadatas(params Metadata[] metadatas)
    {
        if (Metadatas is null)
        {
            Metadatas = new List<Metadata>(metadatas);
            return;;
        }
        
        Metadatas.AddRange(metadatas);
    }
}