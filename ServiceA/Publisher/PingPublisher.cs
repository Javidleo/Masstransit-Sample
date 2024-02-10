using Contracts;
using MassTransit;

namespace ServiceA.Publisher;

public class PingPublisher
{
    private readonly IBus _bus;

    public PingPublisher(IBus bus)
    {
        _bus = bus;
    }

    public void Publish(string name)
    {
        _bus.Publish(new Ping(name));
    }
}
