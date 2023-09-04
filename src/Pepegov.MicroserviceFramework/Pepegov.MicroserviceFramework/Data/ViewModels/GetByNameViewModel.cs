namespace Pepegov.MicroserviceFramework.Data.ViewModels;

public class GetByNameViewModel
{
    public string Name { get; set; } = null!;
    
    public GetByNameViewModel() {}

    public GetByNameViewModel(string name)
    {
        Name = name;
    }
}