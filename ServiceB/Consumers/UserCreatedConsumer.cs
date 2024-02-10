using Contracts;
using MassTransit;

namespace ServiceB.Consumers;

public class UserCreatedConsumer : IConsumer<UserAdded>
{
    private readonly UserStorage _storage;

    public UserCreatedConsumer(UserStorage storage) => _storage = storage;

    public Task Consume(ConsumeContext<UserAdded> context)
    {
        _storage.AddUser(new User(context.Message.name, context.Message.family));

        return Task.CompletedTask;
    }
}
