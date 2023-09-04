using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Exceptions;

/// <summary>
/// EntityValidation microservice exception
/// </summary>
[Serializable]
public class MicroserviceEntityValidationException : Exception
{
    public MicroserviceEntityValidationException() { }
    public MicroserviceEntityValidationException(string message) : base(message) { }
    public MicroserviceEntityValidationException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceEntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}