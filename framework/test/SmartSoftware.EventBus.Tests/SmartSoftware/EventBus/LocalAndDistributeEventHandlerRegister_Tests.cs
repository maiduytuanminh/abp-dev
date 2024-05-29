using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shouldly;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.EventBus.Local;
using Xunit;

namespace SmartSoftware.EventBus;

public class LocalAndDistributeEventHandlerRegister_Tests : EventBusTestBase
{
    [Fact]
    public void Should_Register_Both_Local_And_Distribute()
    {
        var localOptions = GetRequiredService<IOptions<SmartSoftwareLocalEventBusOptions>>();
        var distributedOptions = GetRequiredService<IOptions<SmartSoftwareDistributedEventBusOptions>>();

        localOptions.Value.Handlers.ShouldContain(x => x == typeof(MyEventHandle));
        distributedOptions.Value.Handlers.ShouldContain(x => x == typeof(MyEventHandle));
    }

    class MyEventDate
    {

    }

    class MyEventHandle : ILocalEventHandler<MyEventDate>, IDistributedEventHandler<MyEventDate>, ITransientDependency
    {
        Task ILocalEventHandler<MyEventDate>.HandleEventAsync(MyEventDate eventData)
        {
            return Task.CompletedTask;
        }

        Task IDistributedEventHandler<MyEventDate>.HandleEventAsync(MyEventDate eventData)
        {
            return Task.CompletedTask;
        }
    }
}
