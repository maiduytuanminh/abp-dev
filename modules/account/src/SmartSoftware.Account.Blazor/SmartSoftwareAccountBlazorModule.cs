using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Account.Blazor.Pages.Account;
using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AutoMapper;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.Account.Blazor;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareAccountApplicationContractsModule)
    )]
public class SmartSoftwareAccountBlazorModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareAccountBlazorModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareAccountBlazorAutoMapperProfile>(validate: true);
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SmartSoftwareAccountBlazorUserMenuContributor());
        });

        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(SmartSoftwareAccountBlazorModule).Assembly);
        });
    }
    
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    editFormTypes: new[] { typeof(PersonalInfoModel) }
                );
        });
    }
}
