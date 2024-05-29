using SmartSoftware.Account.Localization;
using SmartSoftware.Identity;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account;

[DependsOn(
    typeof(SmartSoftwareIdentityApplicationContractsModule)
)]
public class SmartSoftwareAccountApplicationContractsModule : SmartSoftwareModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountApplicationContractsModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AccountResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("/SmartSoftware/Account/Localization/Resources");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.Account", typeof(AccountResource));
        });
    }
    
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.User,
                getApiTypes: new[] { typeof(ProfileDto) },
                updateApiTypes: new[] { typeof(UpdateProfileDto) }
            );
        });
    }
}
