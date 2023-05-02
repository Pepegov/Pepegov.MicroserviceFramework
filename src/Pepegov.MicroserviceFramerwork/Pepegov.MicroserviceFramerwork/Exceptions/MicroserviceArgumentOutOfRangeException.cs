using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramerwork.Exceptions;

/// <summary>
/// ArgumentOutOfRange microservice exception
/// </summary>
[Serializable]
public class MicroserviceArgumentOutOfRangeException : Exception
{
    public MicroserviceArgumentOutOfRangeException() { }
    public MicroserviceArgumentOutOfRangeException(string message) : base(message) { }
    public MicroserviceArgumentOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceArgumentOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}