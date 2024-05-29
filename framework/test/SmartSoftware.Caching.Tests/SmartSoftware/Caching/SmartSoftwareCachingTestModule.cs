using Microsoft.Extensions.Caching.Distributed;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Modularity;

namespace SmartSoftware.Caching;

[DependsOn(typeof(SmartSoftwareCachingModule))]
public class SmartSoftwareCachingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDistributedCacheOptions>(option =>
        {
            option.CacheConfigurators.Add(cacheName =>
            {
                if (cacheName == CacheNameAttribute.GetCacheName(typeof(Sail.Testing.Caching.PersonCacheItem)))
                {
                    return new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Parse("2099-01-01 12:00:00")
                    };
                }

                return null;
            });

            option.GlobalCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(20));
        });

        context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, TestMemoryDistributedCache>());
    }
}
