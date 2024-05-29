using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.PermissionManagement.Web;

[DependsOn(typeof(SmartSoftwarePermissionManagementApplicationContractsModule))]
[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule))]
[DependsOn(typeof(SmartSoftwareAutoMapperModule))]
public class SmartSoftwarePermissionManagementWebModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(SmartSoftwarePermissionManagementResource));
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwarePermissionManagementWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwarePermissionManagementWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<SmartSoftwarePermissionManagementWebModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwarePermissionManagementWebAutoMapperProfile>(validate: true);
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(PermissionManagementRemoteServiceConsts.ModuleName);
        });
    }
}
