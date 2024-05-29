using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.Timing;

namespace SmartSoftware.Json.Newtonsoft;

[DependsOn(typeof(SmartSoftwareJsonAbstractionsModule), typeof(SmartSoftwareTimingModule))]
public class SmartSoftwareJsonNewtonsoftModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddOptions<SmartSoftwareNewtonsoftJsonSerializerOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.JsonSerializerSettings.ContractResolver = new SmartSoftwareCamelCasePropertyNamesContractResolver(rootServiceProvider.GetRequiredService<SmartSoftwareDateTimeConverter>());
            });
    }
}
