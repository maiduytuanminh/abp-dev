﻿using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Pipeline;
using Rebus.Pipeline.Receive;
using SmartSoftware.Modularity;

namespace SmartSoftware.EventBus.Rebus;

[DependsOn(
    typeof(SmartSoftwareEventBusModule))]
public class SmartSoftwareEventBusRebusModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(IHandleMessages<>), typeof(RebusDistributedEventHandlerAdapter<>));

        var preActions = context.Services.GetPreConfigureActions<SmartSoftwareRebusEventBusOptions>();
        Configure<SmartSoftwareRebusEventBusOptions>(rebusOptions =>
        {
            preActions.Configure(rebusOptions);
        });

        context.Services.AddRebus(configure =>
        {
            configure.Options(options =>
            {
                options.Decorate<IPipeline>(d =>
                {
                    var step = new SmartSoftwareRebusEventHandlerStep();
                    var pipeline = d.Get<IPipeline>();

                    return new PipelineStepInjector(pipeline).OnReceive(step, PipelineRelativePosition.After, typeof(ActivateHandlersStep));
                });
            });

            preActions.Configure().Configurer?.Invoke(configure);
            return configure;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<RebusDistributedEventBus>()
            .Initialize();

        context.ServiceProvider.StartRebus();
    }
}
