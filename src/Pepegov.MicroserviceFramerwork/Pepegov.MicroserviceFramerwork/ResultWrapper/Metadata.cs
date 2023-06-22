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
    /// Abstract metadata object
    /// </summary>
    public object Data { get; }

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
    
    /// <summary>
    /// Create metadata with your type & data
    /// </summary>
    /// <param name="description"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    public Metadata(string description, MetadataType type, object data) : this(description, type)
    {
        Data = data;
    }
}