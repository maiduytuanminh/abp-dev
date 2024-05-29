using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Data;
using SmartSoftware.EventBus.Abstractions;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy.ConfigurationStore;
using SmartSoftware.Security;
using SmartSoftware.Settings;

namespace SmartSoftware.MultiTenancy;

[DependsOn(
    typeof(SmartSoftwareDataModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareSettingsModule),
    typeof(SmartSoftwareEventBusAbstractionsModule),
    typeof(SmartSoftwareMultiTenancyAbstractionsModule)
    )]
public class SmartSoftwareMultiTenancyModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentTenantAccessor>(AsyncLocalCurrentTenantAccessor.Instance);

        var configuration = context.Services.GetConfiguration();
        Configure<SmartSoftwareDefaultTenantStoreOptions>(configuration);

        Configure<SmartSoftwareSettingOptions>(options =>
        {
            options.ValueProviders.InsertAfter(t => t == typeof(GlobalSettingValueProvider), typeof(TenantSettingValueProvider));
        });

        Configure<SmartSoftwareTenantResolveOptions>(options =>
        {
            options.TenantResolvers.Insert(0, new CurrentUserTenantResolveContributor());
        });
    }
}
