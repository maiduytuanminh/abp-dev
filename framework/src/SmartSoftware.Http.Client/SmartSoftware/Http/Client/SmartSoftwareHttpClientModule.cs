using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Castle;
using SmartSoftware.Data;
using SmartSoftware.EventBus;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;
using SmartSoftware.Validation;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Http.Client.ClientProxying;
using SmartSoftware.Http.Client.ClientProxying.ExtraPropertyDictionaryConverts;
using SmartSoftware.Http.Client.DynamicProxying;
using SmartSoftware.RemoteServices;

namespace SmartSoftware.Http.Client;

[DependsOn(
    typeof(SmartSoftwareHttpModule),
    typeof(SmartSoftwareCastleCoreModule),
    typeof(SmartSoftwareThreadingModule),
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareExceptionHandlingModule),
    typeof(SmartSoftwareRemoteServicesModule),
    typeof(SmartSoftwareEventBusModule)
    )]
public class SmartSoftwareHttpClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
        context.Services.AddTransient(typeof(DynamicHttpProxyInterceptorClientProxy<>));

        Configure<SmartSoftwareHttpClientProxyingOptions>(options =>
        {
            options.QueryStringConverts.Add(typeof(ExtraPropertyDictionary), typeof(ExtraPropertyDictionaryToQueryString));
            options.FormDataConverts.Add(typeof(ExtraPropertyDictionary), typeof(ExtraPropertyDictionaryToFormData));
        });
    }
}
