namespace Pepegov.MicroserviceFramework.Data.ViewModels;

public class GetByIdViewModel
{
    public Guid Id { get; set; }
    
    public GetByIdViewModel() {}

    public GetByIdViewModel(Guid id)
    {
        Id = id;
    }
}