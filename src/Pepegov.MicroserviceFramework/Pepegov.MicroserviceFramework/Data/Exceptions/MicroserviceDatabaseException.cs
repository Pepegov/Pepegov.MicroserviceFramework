using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Exceptions;

/// <summary>
/// Database microservice exception
/// </summary>
[Serializable]
public class MicroserviceDatabaseException : Exception
{
    public MicroserviceDatabaseException() { }
    public MicroserviceDatabaseException(string message) : base(message) { }
    public MicroserviceDatabaseException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}