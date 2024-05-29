using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Testing.Utils;
using SmartSoftware.Users;

namespace SmartSoftware.Identity;

public class DistributedUserUpdateHandler : IDistributedEventHandler<EntityUpdatedEto<UserEto>>, ITransientDependency
{
    private readonly ITestCounter _testCounter;

    public DistributedUserUpdateHandler(ITestCounter testCounter)
    {
        _testCounter = testCounter;
    }

    public Task HandleEventAsync(EntityUpdatedEto<UserEto> eventData)
    {
        if (eventData.Entity.UserName == "john.nash")
        {
            _testCounter.Increment("EntityUpdatedEto<UserEto>");
        }

        return Task.CompletedTask;
    }
}
