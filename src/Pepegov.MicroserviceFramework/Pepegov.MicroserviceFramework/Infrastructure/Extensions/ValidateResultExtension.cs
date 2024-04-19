using FluentValidation.Results;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Pepegov.MicroserviceFramework.Infrastructure.Extensions;

public static class ValidateResultExtension
{
    public static List<ExceptionData> ConvertToExceptionData<TModel>(this ValidationResult validationResult)
    {
        var errorData = validationResult.Errors.Select(x => 
            new ExceptionData()
            {
                Message = $"{x.ErrorMessage}:{x.ErrorCode}",
                Source = typeof(TModel).FullName,
                TypeData = new TypeData()
                {
                    Name = $"{typeof(TModel).Name}:{x.PropertyName}",
                    NameSpace = typeof(TModel).Namespace
                }
            });
        return errorData.ToList();
    }
}