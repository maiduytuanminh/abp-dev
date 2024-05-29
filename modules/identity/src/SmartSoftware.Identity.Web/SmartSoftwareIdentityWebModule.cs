using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Identity.Localization;
using SmartSoftware.Identity.Web.Navigation;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.PermissionManagement.Web;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Threading;

namespace SmartSoftware.Identity.Web;

[DependsOn(typeof(SmartSoftwareIdentityApplicationContractsModule))]
[DependsOn(typeof(SmartSoftwareAutoMapperModule))]
[DependsOn(typeof(SmartSoftwarePermissionManagementWebModule))]
[DependsOn(typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule))]
public class SmartSoftwareIdentityWebModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(IdentityResource), typeof(SmartSoftwareIdentityWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareIdentityWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SmartSoftwareIdentityWebMainMenuContributor());
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareIdentityWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<SmartSoftwareIdentityWebModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareIdentityWebAutoMapperProfile>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Identity/Users/Index", IdentityPermissions.Users.Default);
            options.Conventions.AuthorizePage("/Identity/Users/CreateModal", IdentityPermissions.Users.Create);
            options.Conventions.AuthorizePage("/Identity/Users/EditModal", IdentityPermissions.Users.Update);
            options.Conventions.AuthorizePage("/Identity/Roles/Index", IdentityPermissions.Roles.Default);
            options.Conventions.AuthorizePage("/Identity/Roles/CreateModal", IdentityPermissions.Roles.Create);
            options.Conventions.AuthorizePage("/Identity/Roles/EditModal", IdentityPermissions.Roles.Update);
        });


        Configure<SmartSoftwarePageToolbarOptions>(options =>
        {
            options.Configure<SmartSoftware.Identity.Web.Pages.Identity.Users.IndexModel>(
                toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<IdentityResource>("NewUser"),
                        icon: "plus",
                        name: "CreateUser",
                        requiredPolicyName: IdentityPermissions.Users.Create
                    );
                }
            );

            options.Configure<SmartSoftware.Identity.Web.Pages.Identity.Roles.IndexModel>(
                toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<IdentityResource>("NewRole"),
                        icon: "plus",
                        name: "CreateRole",
                        requiredPolicyName: IdentityPermissions.Roles.Create
                    );
                }
            );
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(IdentityRemoteServiceConsts.ModuleName);
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
                    createFormTypes: new[] { typeof(SmartSoftware.Identity.Web.Pages.Identity.Roles.CreateModalModel.RoleInfoModel) },
                    editFormTypes: new[] { typeof(SmartSoftware.Identity.Web.Pages.Identity.Roles.EditModalModel.RoleInfoModel) }
                );

            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    createFormTypes: new[] { typeof(SmartSoftware.Identity.Web.Pages.Identity.Users.CreateModalModel.UserInfoViewModel) },
                    editFormTypes: new[] { typeof(SmartSoftware.Identity.Web.Pages.Identity.Users.EditModalModel.UserInfoViewModel) }
                );
        });
    }
}
