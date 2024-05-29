using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Identity.Localization;
using SmartSoftware.Identity.Settings;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.Settings;
using SmartSoftware.Threading;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Uow;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareIdentityEntityFrameworkCoreTestModule),
    typeof(SmartSoftwareIdentityTestBaseModule),
    typeof(SmartSoftwarePermissionManagementDomainIdentityModule)
    )]
public class SmartSoftwareIdentityDomainTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
        {
            options.AutoEventSelectors.Add<IdentityUser>();
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareIdentityDomainTestModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<IdentityResource>()
                .AddVirtualJson("/SmartSoftware/Identity/LocalizationExtensions");
        });

        Configure<PermissionManagementOptions>(options =>
        {
            options.IsDynamicPermissionStoreEnabled = false;
            options.SaveStaticPermissionsToDatabase = false;
        });

        Configure<SmartSoftwareUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<SmartSoftwareSettingOptions>(options =>
        {
            options.ValueProviders.Add<TestSettingValueProvider>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            AsyncHelper.RunSync(() => scope.ServiceProvider
                .GetRequiredService<TestPermissionDataBuilder>()
                .Build());
        }
    }
}
