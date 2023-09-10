using MassTransit;

namespace Pepegov.MicroserviceFramework.ApiResults.Test.Consumers;

public class ApiResultConsumer : IConsumer<ApiResult>
{
    public async Task Consume(ConsumeContext<ApiResult> context)
    {
        await context.RespondAsync(context.Message);
    }
}