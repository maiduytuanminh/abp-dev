using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwarePermissionManagementTestModule)
)]
public class SmartSoftwarePermissionManagementApplicationTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();

        context.Services.Configure<PermissionManagementOptions>(options =>
        {
            options.ProviderPolicies[UserPermissionValueProvider.ProviderName] = UserPermissionValueProvider.ProviderName;
            options.ProviderPolicies["Test"] = "Test";
            options.ManagementProviders.Add<TestPermissionManagementProvider>();
        });
    }
}
