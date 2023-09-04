using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Data.Exceptions;

/// <summary>
/// Standart microservice exception
/// </summary>
[Serializable]
public class MicroserviceException : Exception
{
    public MicroserviceException() { }
    public MicroserviceException(string message) : base(message) { }
    public MicroserviceException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
