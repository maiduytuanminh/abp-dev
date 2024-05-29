using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Json.Newtonsoft;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.NewtonsoftJson;

[DependsOn(typeof(SmartSoftwareJsonNewtonsoftModule), typeof(SmartSoftwareAspNetCoreMvcModule))]
public class SmartSoftwareAspNetCoreMvcNewtonsoftModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMvcCore().AddNewtonsoftJson();

        context.Services.AddOptions<MvcNewtonsoftJsonOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.SerializerSettings.ContractResolver = new SmartSoftwareCamelCasePropertyNamesContractResolver(rootServiceProvider.GetRequiredService<SmartSoftwareDateTimeConverter>());
            });
    }
}
