using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramerwork.Exceptions;

/// <summary>
/// NotFound microservice exception
/// </summary>
[Serializable]
public class MicroserviceNotFoundException : Exception
{
    public MicroserviceNotFoundException() { }
    public MicroserviceNotFoundException(string message) : base(message) { }
    public MicroserviceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}