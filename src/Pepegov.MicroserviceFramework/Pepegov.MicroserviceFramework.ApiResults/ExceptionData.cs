namespace Pepegov.MicroserviceFramework.ApiResults;

/// <summary>
/// Simple exception data for api
/// </summary>
public class ExceptionData
{
    public ExceptionData() {}

    public ExceptionData(string message)
    {
        Message = message;
    }

    public ExceptionData(string message, TypeData typeData, string? source = null)
    {
        Message = message;
        TypeData = typeData;
        Source = source;
    }

    public string Message { get; set; } = null!;
    public TypeData TypeData { get; set; } = null!;
    public string? Source { get; set; }

    /// <summary>
    /// Get exception required data
    /// </summary>
    /// <returns>Return error message like: "TypeData.Name: Message" or "TypeData.Name: Message; Source = Source"</returns>
    public override string ToString()
    {
        var errorMessage = $"{TypeData.Name}: {Message}";
        if (Source is not null)
        {
            errorMessage += $"; Source = {Source}";
        }
        return errorMessage;
    }
}

public class MinimalExceptionData
{
    public string ExceptionType { get; set; } = null!;
    public string ExceptionMessage { get; set; } = null!;
}