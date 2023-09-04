using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Exceptions;

/// <summary>
/// Argument microservice exception
/// </summary>
[Serializable]
public class MicroserviceArgumentException : Exception
{
    public MicroserviceArgumentException() { }
    public MicroserviceArgumentException(string message) : base(message) { }
    public MicroserviceArgumentException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}