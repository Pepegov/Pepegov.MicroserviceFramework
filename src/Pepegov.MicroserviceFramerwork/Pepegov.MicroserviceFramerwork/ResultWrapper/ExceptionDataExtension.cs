namespace Pepegov.MicroserviceFramerwork.ResultWrapper;

public static class ExceptionDataExtension
{
    public static ExceptionData ToExceptionData(this Exception ex)
        => ExceptionToExceptionDataConvertor(ex);

    public static ExceptionData[] ToExceptionData(this Exception[] ex)
    {
        var result = new ExceptionData[ex.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = ExceptionToExceptionDataConvertor(ex[i]);
        }
        return result;
    }
    
    private static ExceptionData ExceptionToExceptionDataConvertor(Exception exception)
    {
        var type = exception.GetType();
        return new ExceptionData()
        {
            Message = exception.Message,
            Source = exception.Source,
            TypeData = new TypeData()
            {
                Name = type.Name,
                NameSpace = type.Namespace,
                FullName = type.FullName,
            }
        };
    }
}