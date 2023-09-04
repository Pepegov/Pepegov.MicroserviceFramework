namespace Pepegov.MicroserviceFramework.Data.ViewModels;

public class DeleteByIdViewModel
{
    public Guid Id { get; set; }
    
    public DeleteByIdViewModel() {}

    public DeleteByIdViewModel(Guid id)
    {
        Id = id;
    }
}