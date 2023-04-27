namespace Pepegov.MicroserviceFramerwork.ResultWrapper;

public class Metadata
{
    public string Description { get; }
    public MetadataType Type { get; }

    public Metadata(string description)
    {
        Type = MetadataType.Info;
        Description = description;
    }
    
    public Metadata(string description, MetadataType type) : this(description)
    {
        Type = type;
    } 
}