using System.Text.Json.Serialization;

namespace Pepegov.MicroserviceFramework.ApiResults;

public class TypeData
{
    [JsonIgnore]
    public string FullName => $"{NameSpace}.{Name}";

    public string Name { get; set; } = null!;
    public string? NameSpace { get; set; }
}