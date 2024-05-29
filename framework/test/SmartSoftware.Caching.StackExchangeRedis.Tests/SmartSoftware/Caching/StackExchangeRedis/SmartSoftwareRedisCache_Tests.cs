using Microsoft.Extensions.Caching.Distributed;
using Shouldly;
using Xunit;

namespace SmartSoftware.Caching.StackExchangeRedis;

public class SmartSoftwareRedisCache_Tests : SmartSoftwareCachingStackExchangeRedisTestBase
{
    private readonly IDistributedCache _distributedCache;

    public SmartSoftwareRedisCache_Tests()
    {
        _distributedCache = GetRequiredService<IDistributedCache>();
    }

    [Fact]
    public void Should_Replace_RedisCache()
    {
        (_distributedCache is SmartSoftwareRedisCache).ShouldBeTrue();
    }
}
