using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramerwork.Exceptions;

/// <summary>
/// Already Exists microservice exception
/// </summary>
[Serializable]
public class MicroserviceAlreadyExistsException : Exception
{
    public MicroserviceAlreadyExistsException() { }
    public MicroserviceAlreadyExistsException(string message) : base(message) { }
    public MicroserviceAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}