using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Data.Exceptions;

/// <summary>
/// MessageQueue microservice exception
/// </summary>
[Serializable]
public class MicroserviceMessageQueueException : Exception
{
    public MicroserviceMessageQueueException() { }
    public MicroserviceMessageQueueException(string message) : base(message) { }
    public MicroserviceMessageQueueException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceMessageQueueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}