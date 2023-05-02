using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramerwork.Exceptions;

/// <summary>
/// Unauthorized microservice exception
/// </summary>
[Serializable]
public class MicroserviceUnauthorizedException : Exception
{
    public MicroserviceUnauthorizedException() { }
    public MicroserviceUnauthorizedException(string message) : base(message) { }
    public MicroserviceUnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}