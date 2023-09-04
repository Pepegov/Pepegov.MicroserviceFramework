namespace Pepegov.MicroserviceFramework.Definition;

public class ApplicationDefinitionInformationRecords
{
    public IList<ApplicationDefinitionInformation> Items { get; } = new List<ApplicationDefinitionInformation>();
    
    public void AddApplicationDefinitionInformation(ApplicationDefinitionInformation definition)
    {
        var exists = Items.FirstOrDefault(x => x == definition);
        if (exists is null)
        {
            Items.Add(definition);
        }
    }
}