using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace SmartSoftware.DistributedLocking;

public class LocalDistributedLock_Tests : SmartSoftwareDistributedLockingAbstractionsTestBase
{
    private readonly ISmartSoftwareDistributedLock _distributedLock;

    public LocalDistributedLock_Tests()
    {
        _distributedLock = GetRequiredService<ISmartSoftwareDistributedLock>();
    }

    [Fact]
    public void Should_Be_Instance_Of_LocalSmartSoftwareDistributedLock()
    {
        _distributedLock.ShouldBeOfType<LocalSmartSoftwareDistributedLock>();
    }

    [Fact]
    public async Task Should_Lock_With_TryAcquire()
    {
        await using (var handle = await _distributedLock.TryAcquireAsync("lock1"))
        {
            handle.ShouldNotBeNull();
        }
    }

    [Fact]
    public async Task Should_Not_Acquire_If_Already_Locked()
    {
        await using (var handle = await _distributedLock.TryAcquireAsync("lock1"))
        {
            handle.ShouldNotBeNull();

            await Task.Run(async () =>
            {
                await using (var handle2 = await _distributedLock.TryAcquireAsync("lock1"))
                {
                    handle2.ShouldBeNull();
                }
            });
        }

        await Task.Run(async () =>
        {
            await using (var handle = await _distributedLock.TryAcquireAsync("lock1"))
            {
                handle.ShouldNotBeNull();
            }
        });
    }

    [Fact]
    public async Task Should_Obtain_Multiple_Locks()
    {
        await using (var handle = await _distributedLock.TryAcquireAsync("lock1"))
        {
            handle.ShouldNotBeNull();

            await Task.Run(async () =>
            {
                await using (var handle2 = await _distributedLock.TryAcquireAsync("lock2"))
                {
                    handle2.ShouldNotBeNull();
                }
            });
        }
    }
}
