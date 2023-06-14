namespace Pepegov.MicroserviceFramerwork.ViewModels;

public class GetPagedListViewModel
{
    public GetPagedListViewModel(){}
    
    public GetPagedListViewModel(int pageIndex, int pageSize)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}