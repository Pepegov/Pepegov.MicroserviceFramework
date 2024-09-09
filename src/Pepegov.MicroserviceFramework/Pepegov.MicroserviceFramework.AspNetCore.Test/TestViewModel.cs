namespace Pepegov.MicroserviceFramework.AspNetCore.Test;

public class TestViewModel
{
    public string Name { get; set; }
    public string? SecondName { get; }

    public TestViewModel() {}
    
    public TestViewModel(string name, string? secondName)
    {
        Name = name;
        SecondName = secondName;
    }
}