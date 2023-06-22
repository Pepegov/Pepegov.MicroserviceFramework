namespace Pepegov.MicroserviceFramerwork.ResultWrapper;

public class ExceptionData
{
    public string Message { get; set; } = null!;
    public string? Source { get; set; } = null!;
    public TypeData TypeData { get; set; } = null!;
}