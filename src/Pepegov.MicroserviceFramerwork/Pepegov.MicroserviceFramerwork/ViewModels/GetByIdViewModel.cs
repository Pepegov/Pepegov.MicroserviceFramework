namespace Pepegov.MicroserviceFramerwork.ViewModels;

public class GetByIdViewModel
{
    public Guid Id { get; set; }
    
    public GetByIdViewModel() {}

    public GetByIdViewModel(Guid id)
    {
        Id = id;
    }
}