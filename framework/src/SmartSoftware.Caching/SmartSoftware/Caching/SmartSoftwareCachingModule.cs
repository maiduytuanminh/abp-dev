using Microsoft.Extensions.DependencyInjection;
using System;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Serialization;
using SmartSoftware.Threading;
using SmartSoftware.Uow;

namespace SmartSoftware.Caching;

[DependsOn(
    typeof(SmartSoftwareThreadingModule),
    typeof(SmartSoftwareSerializationModule),
    typeof(SmartSoftwareUnitOfWorkModule),
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareJsonModule))]
public class SmartSoftwareCachingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMemoryCache();
        context.Services.AddDistributedMemoryCache();

        context.Services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
        context.Services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

        context.Services.Configure<SmartSoftwareDistributedCacheOptions>(cacheOptions =>
        {
            cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
        });
    }
}
