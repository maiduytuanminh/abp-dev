using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus;
using SmartSoftware.EventBus.Dapr;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.Dapr.EventBus;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcDaprModule),
    typeof(SmartSoftwareEventBusDaprModule)
)]
public class SmartSoftwareAspNetCoreMvcDaprEventBusModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var subscribeOptions = context.Services.ExecutePreConfiguredActions<SmartSoftwareSubscribeOptions>();

        Configure<SmartSoftwareEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                var rootServiceProvider = endpointContext.ScopeServiceProvider.GetRequiredService<IRootServiceProvider>();
                subscribeOptions.SubscriptionsCallback = subscriptions =>
                {
                    var daprEventBusOptions = rootServiceProvider.GetRequiredService<IOptions<SmartSoftwareDaprEventBusOptions>>().Value;
                    foreach (var handler in rootServiceProvider.GetRequiredService<IOptions<SmartSoftwareDistributedEventBusOptions>>().Value.Handlers)
                    {
                        foreach (var @interface in handler.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDistributedEventHandler<>)))
                        {
                            var eventType = @interface.GetGenericArguments()[0];
                            var eventName = EventNameAttribute.GetNameOrDefault(eventType);

                            if (subscriptions.Any(x => x.PubsubName == daprEventBusOptions.PubSubName && x.Topic == eventName))
                            {
                                // Controllers with a [Topic] attribute can replace built-in event handlers.
                                continue;
                            }

                            var subscription = new SmartSoftwareSubscription
                            {
                                PubsubName = daprEventBusOptions.PubSubName,
                                Topic = eventName,
                                Route = SmartSoftwareAspNetCoreMvcDaprPubSubConsts.DaprEventCallbackUrl,
                                Metadata = new SmartSoftwareMetadata
                                {
                                    {
                                        SmartSoftwareMetadata.RawPayload, "true"
                                    }
                                }
                            };
                            subscriptions.Add(subscription);
                        }
                    }

                    return Task.CompletedTask;
                };

                endpointContext.Endpoints.MapSmartSoftwareSubscribeHandler(subscribeOptions);
            });
        });
    }
}
