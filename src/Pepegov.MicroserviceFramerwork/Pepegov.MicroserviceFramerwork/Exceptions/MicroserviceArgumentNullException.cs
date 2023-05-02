using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramerwork.Exceptions;

/// <summary>
/// ArgumentNull microservice exception
/// </summary>
[Serializable]
public class MicroserviceArgumentNullException : Exception
{
    public MicroserviceArgumentNullException() { }
    public MicroserviceArgumentNullException(string message) : base(message) { }
    public MicroserviceArgumentNullException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}