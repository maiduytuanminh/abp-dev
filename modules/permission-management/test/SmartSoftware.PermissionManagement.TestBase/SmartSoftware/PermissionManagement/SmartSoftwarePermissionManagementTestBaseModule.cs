﻿using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.PermissionManagement;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementDomainModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule)
    )]
public class SmartSoftwarePermissionManagementTestBaseModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<PermissionManagementOptions>(options =>
        {
            options.ManagementProviders.Add<TestPermissionManagementProvider>();
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
                .GetRequiredService<PermissionTestDataBuilder>()
                .BuildAsync());
        }
    }
}