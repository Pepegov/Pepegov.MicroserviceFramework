using MassTransit;

namespace Pepegov.MicroserviceFramework.ApiResults.Test.Consumers;

public class ApiResultWrapperConsumer : IConsumer<ApiResult<TestViewModel>>
{
    public async Task Consume(ConsumeContext<ApiResult<TestViewModel>> context)
    {
        await context.RespondAsync(context.Message);
    }
}