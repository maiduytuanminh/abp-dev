using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.Data;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.TenantManagement;

[DependsOn(typeof(SmartSoftwareMultiTenancyModule))]
[DependsOn(typeof(SmartSoftwareTenantManagementDomainSharedModule))]
[DependsOn(typeof(SmartSoftwareDataModule))]
[DependsOn(typeof(SmartSoftwareDddDomainModule))]
[DependsOn(typeof(SmartSoftwareAutoMapperModule))]
[DependsOn(typeof(SmartSoftwareCachingModule))]
public class SmartSoftwareTenantManagementDomainModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareTenantManagementDomainModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddProfile<SmartSoftwareTenantManagementDomainMappingProfile>(validate: true);
        });

        Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<Tenant, TenantEto>();
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                TenantManagementModuleExtensionConsts.ModuleName,
                TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                typeof(Tenant)
            );
        });
    }
}
