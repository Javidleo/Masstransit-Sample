using MassTransit;
using ServiceA.Contracts;

namespace ServiceA.Consumers;

public class PingConsumer : IConsumer<Ping>
{
    private readonly ILogger<PingConsumer> _logger;

    public PingConsumer(ILogger<PingConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<Ping> context)
    {
        _logger.LogInformation(context.Message.Name);

        return Task.CompletedTask;
    }
}
