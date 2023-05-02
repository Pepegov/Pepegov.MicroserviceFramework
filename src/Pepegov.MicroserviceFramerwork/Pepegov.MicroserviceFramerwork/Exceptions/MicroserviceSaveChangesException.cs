using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramerwork.Exceptions;

/// <summary>
/// SaveChanges microservice exception
/// </summary>
[Serializable]
public class MicroserviceSaveChangesException : Exception
{
    public MicroserviceSaveChangesException() { }
    public MicroserviceSaveChangesException(string message) : base(message) { }
    public MicroserviceSaveChangesException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceSaveChangesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}