using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Account.Localization;
using SmartSoftware.Account.Web.Pages.Account;
using SmartSoftware.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using SmartSoftware.Account.Web.ProfileManagement;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using SmartSoftware.AutoMapper;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Identity.AspNetCore;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account.Web;

[DependsOn(
    typeof(SmartSoftwareAccountApplicationContractsModule),
    typeof(SmartSoftwareIdentityAspNetCoreModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(SmartSoftwareExceptionHandlingModule)
    )]
public class SmartSoftwareAccountWebModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(AccountResource), typeof(SmartSoftwareAccountWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAccountWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountWebModule>();
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SmartSoftwareAccountUserMenuContributor());
        });

        Configure<SmartSoftwareToolbarOptions>(options =>
        {
            options.Contributors.Add(new AccountModuleToolbarContributor());
        });

        ConfigureProfileManagementPage();

        context.Services.AddAutoMapperObjectMapper<SmartSoftwareAccountWebModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareAccountWebAutoMapperProfile>(validate: true);
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(AccountRemoteServiceConsts.ModuleName);
        });
    }

    private void ConfigureProfileManagementPage()
    {
        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Account/Manage");
        });

        Configure<ProfileManagementPageOptions>(options =>
        {
            options.Contributors.Add(new AccountProfileManagementPageContributor());
        });

        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options.ScriptBundles
                .Configure(typeof(ManageModel).FullName,
                    configuration =>
                    {
                        configuration.AddFiles("/client-proxies/account-proxy.js");
                        configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/Password/Default.js");
                        configuration.AddFiles("/Pages/Account/Components/ProfileManagementGroup/PersonalInfo/Default.js");
                    });
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
                    editFormTypes: new[] { typeof(AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel) }
                );
        });
    }
}
