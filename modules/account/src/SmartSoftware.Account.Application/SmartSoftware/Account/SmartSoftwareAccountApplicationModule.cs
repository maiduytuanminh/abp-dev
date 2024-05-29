using SmartSoftware.AutoMapper;
using SmartSoftware.Emailing;
using SmartSoftware.Identity;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;
using SmartSoftware.UI.Navigation.Urls;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account;

[DependsOn(
    typeof(SmartSoftwareAccountApplicationContractsModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareUiNavigationModule),
    typeof(SmartSoftwareEmailingModule)
)]
public class SmartSoftwareAccountApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountApplicationModule>();
        });

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareAccountApplicationModuleAutoMapperProfile>(validate: true);
        });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].Urls[AccountUrlNames.PasswordReset] = "Account/ResetPassword";
        });
    }
}
