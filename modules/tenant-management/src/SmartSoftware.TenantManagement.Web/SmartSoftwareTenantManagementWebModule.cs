using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using SmartSoftware.AutoMapper;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.TenantManagement.Localization;
using SmartSoftware.TenantManagement.Web.Navigation;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Threading;

namespace SmartSoftware.TenantManagement.Web;

[DependsOn(typeof(SmartSoftwareTenantManagementApplicationContractsModule))]
[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule))]
[DependsOn(typeof(SmartSoftwareFeatureManagementWebModule))]
[DependsOn(typeof(SmartSoftwareAutoMapperModule))]
public class SmartSoftwareTenantManagementWebModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(SmartSoftwareTenantManagementResource), typeof(SmartSoftwareTenantManagementWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareTenantManagementWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SmartSoftwareTenantManagementWebMainMenuContributor());
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareTenantManagementWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<SmartSoftwareTenantManagementWebModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareTenantManagementWebAutoMapperProfile>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/TenantManagement/Tenants/Index", TenantManagementPermissions.Tenants.Default);
            options.Conventions.AuthorizePage("/TenantManagement/Tenants/CreateModal", TenantManagementPermissions.Tenants.Create);
            options.Conventions.AuthorizePage("/TenantManagement/Tenants/EditModal", TenantManagementPermissions.Tenants.Update);
        });

        Configure<SmartSoftwarePageToolbarOptions>(options =>
        {
            options.Configure<SmartSoftware.TenantManagement.Web.Pages.TenantManagement.Tenants.IndexModel>(
                toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<SmartSoftwareTenantManagementResource>("NewTenant"),
                        icon: "plus",
                        name: "CreateTenant",
                        requiredPolicyName: TenantManagementPermissions.Tenants.Create
                    );
                }
            );
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(TenantManagementRemoteServiceConsts.ModuleName);
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
                    createFormTypes: new[] { typeof(SmartSoftware.TenantManagement.Web.Pages.TenantManagement.Tenants.CreateModalModel.TenantInfoModel) },
                    editFormTypes: new[] { typeof(SmartSoftware.TenantManagement.Web.Pages.TenantManagement.Tenants.EditModalModel.TenantInfoModel) }
                );
        });
    }
}
