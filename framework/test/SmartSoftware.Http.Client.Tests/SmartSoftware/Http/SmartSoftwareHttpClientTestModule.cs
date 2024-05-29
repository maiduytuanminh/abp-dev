using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.ClientProxying;
using SmartSoftware.Http.DynamicProxying;
using SmartSoftware.Http.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.TestApp;
using SmartSoftware.TestApp.Application.Dto;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Http;

[DependsOn(
    typeof(SmartSoftwareHttpClientModule),
    typeof(SmartSoftwareAspNetCoreMvcTestModule)
    )]
public class SmartSoftwareHttpClientTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(typeof(TestAppModule).Assembly);
        context.Services.AddHttpClientProxy<IRegularTestController>();

        Configure<SmartSoftwareRemoteServiceOptions>(options =>
        {
            options.RemoteServices.Default = new RemoteServiceConfiguration("/");
        });


        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareHttpClientTestModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<HttpClientTestResource>("en")
                .AddVirtualJson("/SmartSoftware/Http/Localization");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.Http.DynamicProxying", typeof(HttpClientTestResource));
        });

        Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateFileInput));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateMultipleFileInput));
        });

        Configure<SmartSoftwareHttpClientProxyingOptions>(options =>
        {
            options.QueryStringConverts.Add(typeof(List<GetParamsNameValue>), typeof(TestObjectToQueryString));
            options.FormDataConverts.Add(typeof(List<GetParamsNameValue>), typeof(TestObjectToFormData));
            options.PathConverts.Add(typeof(int), typeof(TestObjectToPath));
        });
    }
}
