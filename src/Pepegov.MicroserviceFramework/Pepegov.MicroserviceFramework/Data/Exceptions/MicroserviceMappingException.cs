using System.Runtime.Serialization;

namespace Pepegov.MicroserviceFramework.Data.Exceptions;

/// <summary>
/// Mapping microservice exception
/// </summary>
[Serializable]
public class MicroserviceMappingException : Exception
{
    public MicroserviceMappingException() { }
    public MicroserviceMappingException(string message) : base(message) { }
    public MicroserviceMappingException(string message, Exception innerException) : base(message, innerException) { }
    protected MicroserviceMappingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}