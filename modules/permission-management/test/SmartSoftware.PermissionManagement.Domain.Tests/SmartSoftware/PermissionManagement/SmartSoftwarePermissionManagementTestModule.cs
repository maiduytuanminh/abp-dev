using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.Uow;

namespace SmartSoftware.PermissionManagement;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementTestBaseModule),
    typeof(SmartSoftwareFeaturesModule),
    typeof(SmartSoftwareGlobalFeaturesModule)
    )]
public class SmartSoftwarePermissionManagementTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddEntityFrameworkInMemoryDatabase();

        var databaseName = Guid.NewGuid().ToString();

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.Configure(ssDbContextConfigurationContext =>
            {
                ssDbContextConfigurationContext.DbContextOptions.UseInMemoryDatabase(databaseName);
            });
        });

        Configure<SmartSoftwareUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled; //EF in-memory database does not support transactions
            });
    }
}
