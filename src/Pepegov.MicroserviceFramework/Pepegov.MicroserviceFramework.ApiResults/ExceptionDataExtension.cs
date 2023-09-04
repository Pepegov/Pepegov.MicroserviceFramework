namespace Pepegov.MicroserviceFramework.ApiResults;

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

    public static MinimalExceptionData[] ToMinimalExceptionData(this List<ExceptionData> ed)
        => ToMinimalMinimalExceptionData(ed.ToArray());
    
    public static MinimalExceptionData[] ToMinimalMinimalExceptionData(this ExceptionData[] ed)
    {
        var result = new MinimalExceptionData[ed.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = ExceptionDataToMinimalExceptionData(ed[i]);
        }

        return result;
    }

    private static MinimalExceptionData ExceptionDataToMinimalExceptionData(ExceptionData exceptionData)
        => new MinimalExceptionData()
        {
            ExceptionType = exceptionData.TypeData.Name,
            ExceptionMessage = exceptionData.Message,
        };
    
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
            }
        };
    }
}