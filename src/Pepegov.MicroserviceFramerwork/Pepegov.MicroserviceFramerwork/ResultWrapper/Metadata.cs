namespace Pepegov.MicroserviceFramerwork.ResultWrapper;

public class Metadata
{
    /// <summary>
    /// Metadata description
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Metadata type
    /// </summary>
    public MetadataType Type { get; }

    /// <summary>
    /// Create metadata with type = info
    /// </summary>
    /// <param name="description"></param>
    public Metadata(string description)
    {
        Type = MetadataType.Info;
        Description = description;
    }
    
    /// <summary>
    /// Create metadata with your type 
    /// </summary>
    /// <param name="description"></param>
    /// <param name="type"></param>
    public Metadata(string description, MetadataType type) : this(description)
    {
        Type = type;
    } 
}