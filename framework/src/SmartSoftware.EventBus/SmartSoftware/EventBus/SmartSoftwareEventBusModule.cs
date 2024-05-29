using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.BackgroundWorkers;
using SmartSoftware.DistributedLocking;
using SmartSoftware.EventBus.Abstractions;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.EventBus.Local;
using SmartSoftware.Guids;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Reflection;
using SmartSoftware.Threading;

namespace SmartSoftware.EventBus;

[DependsOn(
    typeof(SmartSoftwareEventBusAbstractionsModule),
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareGuidsModule),
    typeof(SmartSoftwareBackgroundWorkersModule),
    typeof(SmartSoftwareDistributedLockingAbstractionsModule)
    )]
public class SmartSoftwareEventBusModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AddEventHandlers(context.Services);
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<OutboxSenderManager>();
        await context.AddBackgroundWorkerAsync<InboxProcessManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    private static void AddEventHandlers(IServiceCollection services)
    {
        var localHandlers = new List<Type>();
        var distributedHandlers = new List<Type>();

        services.OnRegistered(context =>
        {
            if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(ILocalEventHandler<>)))
            {
                localHandlers.Add(context.ImplementationType);
            }

            if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IDistributedEventHandler<>)))
            {
                distributedHandlers.Add(context.ImplementationType);
            }
        });

        services.Configure<SmartSoftwareLocalEventBusOptions>(options =>
        {
            options.Handlers.AddIfNotContains(localHandlers);
        });

        services.Configure<SmartSoftwareDistributedEventBusOptions>(options =>
        {
            options.Handlers.AddIfNotContains(distributedHandlers);
        });
    }
}
