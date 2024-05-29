using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Modularity;

namespace SmartSoftware.Caching.StackExchangeRedis;

[DependsOn(
    typeof(SmartSoftwareCachingModule)
    )]
public class SmartSoftwareCachingStackExchangeRedisModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        var redisEnabled = configuration["Redis:IsEnabled"];
        if (string.IsNullOrEmpty(redisEnabled) || bool.Parse(redisEnabled))
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                var redisConfiguration = configuration["Redis:Configuration"];
                if (!redisConfiguration.IsNullOrEmpty())
                {
                    options.Configuration = redisConfiguration;
                }
            });

            context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, SmartSoftwareRedisCache>());
        }
    }
}
