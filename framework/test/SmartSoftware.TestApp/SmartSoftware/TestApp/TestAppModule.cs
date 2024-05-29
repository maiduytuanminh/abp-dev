using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Application;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.AutoMapper;
using SmartSoftware.Domain.Entities.Caching;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.TestApp.Application.Dto;
using SmartSoftware.TestApp.Testing;
using SmartSoftware.Threading;

namespace SmartSoftware.TestApp;

[DependsOn(
    typeof(SmartSoftwareDddApplicationModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutoMapperModule)
    )]
public class TestAppModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureAutoMapper();
        ConfigureDistributedEventBus();
        
        context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, TestMemoryDistributedCache>());
        context.Services.AddEntityCache<Product, Guid>();
        context.Services.AddEntityCache<Product, ProductCacheItem, Guid>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private void ConfigureAutoMapper()
    {
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.Configurators.Add(ctx =>
            {
                ctx.MapperConfiguration.CreateMap<Person, PersonDto>().ReverseMap();
                ctx.MapperConfiguration.CreateMap<Phone, PhoneDto>().ReverseMap();
            });

            options.AddMaps<TestAppModule>();
        });
    }

    private void ConfigureDistributedEventBus()
    {
        Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
        {
            options.AutoEventSelectors.Add<Person>();
            options.EtoMappings.Add<Person, PersonEto>();
        });
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            AsyncHelper.RunSync(() => scope.ServiceProvider
                .GetRequiredService<TestDataBuilder>()
                .BuildAsync());
        }
    }
}
