using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AutoMapper;
using SmartSoftware.FeatureManagement.Blazor;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.TenantManagement.Blazor.Navigation;
using SmartSoftware.TenantManagement.Localization;
using SmartSoftware.Threading;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.TenantManagement.Blazor;

[DependsOn(
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareTenantManagementApplicationContractsModule),
    typeof(SmartSoftwareFeatureManagementBlazorModule)
)]
public class SmartSoftwareTenantManagementBlazorModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareTenantManagementBlazorModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareTenantManagementBlazorAutoMapperProfile>(validate: true);
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TenantManagementBlazorMenuContributor());
        });

        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(SmartSoftwareTenantManagementBlazorModule).Assembly);
        });
        
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwareTenantManagementResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareFeatureManagementResource),
                    typeof(SmartSoftwareUiResource));
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    TenantManagementModuleExtensionConsts.ModuleName,
                    TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                    createFormTypes: new[] { typeof(TenantCreateDto) },
                    editFormTypes: new[] { typeof(TenantUpdateDto) }
                );
        });
    }
}
