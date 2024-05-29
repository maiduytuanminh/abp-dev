using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AutoMapper;
using SmartSoftware.BlazoriseUI;
using SmartSoftware.Identity.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.PermissionManagement.Blazor;
using SmartSoftware.Threading;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.Identity.Blazor;

[DependsOn(
    typeof(SmartSoftwareIdentityApplicationContractsModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwarePermissionManagementBlazorModule),
    typeof(SmartSoftwareBlazoriseUIModule)
    )]
public class SmartSoftwareIdentityBlazorModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareIdentityBlazorModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareIdentityBlazorAutoMapperProfile>(validate: true);
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SmartSoftwareIdentityWebMainMenuContributor());
        });

        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(SmartSoftwareIdentityBlazorModule).Assembly);
        });
        
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<IdentityResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.Role,
                    createFormTypes: new[] { typeof(IdentityRoleCreateDto) },
                    editFormTypes: new[] { typeof(IdentityRoleUpdateDto) }
                );

            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    createFormTypes: new[] { typeof(IdentityUserCreateDto) },
                    editFormTypes: new[] { typeof(IdentityUserUpdateDto) }
                );
        });
    }
}
