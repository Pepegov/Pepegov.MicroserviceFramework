using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Data.Exceptions;

/// <summary>
/// InvalidOperation microservice exception
/// </summary>
[Serializable]
public class MicroserviceInvalidOperationException : Exception
{
    public MicroserviceInvalidOperationException() { }
    public MicroserviceInvalidOperationException(string message) : base(message) { }
    public MicroserviceInvalidOperationException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}